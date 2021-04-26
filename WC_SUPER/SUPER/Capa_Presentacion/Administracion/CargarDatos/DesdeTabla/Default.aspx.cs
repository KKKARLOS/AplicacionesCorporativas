using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
//
using System.Text;
using System.Text.RegularExpressions;

using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;

//Para el StreamReader
using System.IO;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores="";
    public SqlConnection oConn;
    public SqlTransaction tr;
    public StringBuilder sb = new StringBuilder();
    public StringBuilder sbE = new StringBuilder();

    //public Hashtable htProyectoSubNodo, htClaseEconomica;
    public Hashtable htProveedor, htProyectoSubNodo, htClaseEconomica, htNodoDestino;

    public Proveedor oProveedor = null;
    public ProyectoSubNodo oProyectoSubNodo = null;
    public ClaseEconomica oClaseEconomica = null;

    public int iCont = 0, iNumOk = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            sErrores = "";
            Master.nBotonera = 43;
            Master.bFuncionesLocales = true;
            Master.TituloPagina = "Carga datos en DATOECO desde tabla";

            if (!Page.IsPostBack)
            {
                try
                {
                    CargarDataEco();
                }
                catch (Exception ex)
                {
                    Master.sErrores = Errores.mostrarError("Error al obtener los datos", ex);
                }
            }
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        string[] aArgs = Regex.Split(eventArg, "@#@");

        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        switch (aArgs[0])
        {
            case ("procesar"):
                sResultado += Procesar();
                break;

            case ("mostrarTabla"):
                sResultado += mostrarTabla();
                break;

        }
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }
    private void CargarDataEco()
    {
        StringBuilder sb = new StringBuilder();

        #region Obtenión de dataset con empresas, proyectos y clientes y creación de HASTABLES

        oProyectoSubNodo = null;
        oClaseEconomica = null;
        oProveedor = null;

        DataSet ds = AddDATAECO.ValidarTabla();

        htProyectoSubNodo = new Hashtable();
        foreach (DataRow dsProyectoSubNodo in ds.Tables[0].Rows)//Recorro tabla de proyectos-subnodos 
        {
            htProyectoSubNodo.Add(dsProyectoSubNodo["t301_idproyecto"].ToString() + @"/" + dsProyectoSubNodo["t303_idnodo"].ToString(), new ProyectoSubNodo((int)dsProyectoSubNodo["t301_idproyecto"], (int)dsProyectoSubNodo["t305_idproyectosubnodo"],
                                 (int)dsProyectoSubNodo["t303_idnodo"], dsProyectoSubNodo["t305_cualidad"].ToString())
                                 );
        }

        htClaseEconomica = new Hashtable();
        foreach (DataRow dsClaseEconomica in ds.Tables[1].Rows)//Recorro tabla de Clases económicas
        {
            htClaseEconomica.Add(dsClaseEconomica["t329_idclaseeco"].ToString(), new ClaseEconomica((int)dsClaseEconomica["t329_idclaseeco"],
                                 dsClaseEconomica["t329_necesidad"].ToString(), bool.Parse(dsClaseEconomica["t329_visiblecarruselC"].ToString()), bool.Parse(dsClaseEconomica["t329_visiblecarruselJ"].ToString()), bool.Parse(dsClaseEconomica["t329_visiblecarruselP"].ToString()))
                                 );

        }

        htProveedor = new Hashtable();
        foreach (DataRow dsProveedor in ds.Tables[2].Rows) //Recorro tabla de proveedores
        {
            htProveedor.Add(dsProveedor["t315_codigoexterno"].ToString(), new Proveedor((int)dsProveedor["t315_idproveedor"],
                            dsProveedor["t315_codigoexterno"].ToString())
                            );
        }
        ds.Dispose();
        #endregion

        // Lectura, validación y conteo, de las filas de la tabla

        SqlDataReader dr = AddDATAECO.Catalogo();
        while (dr.Read())
        {
            iCont++;
            if (validarCampos(dr)) iNumOk++;
        }
        dr.Close();
        dr.Dispose();

        sb.Append("</table>");
        this.divB.InnerHtml = cabErrores() + sbE + "</table>";
        cldTotalLin.InnerText = AddDATAECO.numFilas(null).ToString("##,###,##0");
        cldLinOK.InnerText = iNumOk.ToString("##,###,##0");
        cldLinErr.InnerText = (iCont - iNumOk).ToString("##,###,##0");
    }

    private string Procesar()
    {
        string sResul = "";
        int iNumLin = 1;
        try
        {
			oProyectoSubNodo = null;
			DataSet ds = AddDATAECO.ValidarTabla();

			htProyectoSubNodo = new Hashtable();
			foreach (DataRow dsProyectoSubNodo in ds.Tables[0].Rows)//Recorro tabla de proyectos-subnodos 
			{
				htProyectoSubNodo.Add(dsProyectoSubNodo["t301_idproyecto"].ToString() + @"/" + dsProyectoSubNodo["t303_idnodo"].ToString(), new ProyectoSubNodo((int)dsProyectoSubNodo["t301_idproyecto"], 
                                                                                                                                                                (int)dsProyectoSubNodo["t305_idproyectosubnodo"],
    									                                                                                                                        (int)dsProyectoSubNodo["t303_idnodo"],
                                                                                                                                                                dsProyectoSubNodo["t305_cualidad"].ToString()
                                                                                                                                                                )
									 );
			}

            #region Abro transaccion
            try
            {
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccion(oConn);
            }
            catch (Exception ex)
            {
                if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
                sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
                return sResul;
            }
            #endregion
			
			string sEstadoMes = "";
			int nSegMesProy = 0;
			
			SqlDataReader dr = AddDATAECO.Catalogo();
			while (dr.Read())
            {
				oProyectoSubNodo = (ProyectoSubNodo)htProyectoSubNodo[dr["t301_idproyecto"].ToString() + "/" + dr["t303_idnodo"].ToString()];
                if (oProyectoSubNodo != null)
                {
                    nSegMesProy = SEGMESPROYECTOSUBNODO.ExisteSegMesProy(tr, oProyectoSubNodo.t305_idproyectosubnodo, (int)dr["t325_anomes"]);
                    if (nSegMesProy == 0)
                    {
                        sEstadoMes = SEGMESPROYECTOSUBNODO.EstadoMesACrear(tr, oProyectoSubNodo.t305_idproyectosubnodo, (int)dr["t325_anomes"]);
                        nSegMesProy = SEGMESPROYECTOSUBNODO.Insert(tr, oProyectoSubNodo.t305_idproyectosubnodo, (int)dr["t325_anomes"], sEstadoMes, 0, 0, false, 0, 0);
                    }

                    int? iNodoDestino = null;
                    if (dr["t303_idnodo_destino"] != DBNull.Value) iNodoDestino = (int)dr["t303_idnodo_destino"];

                    int? iProvedor = null;
                    if (dr["t315_idproveedor"] != DBNull.Value) iProvedor = (int)dr["t315_idproveedor"];
                    
                    DATOECO.Insert(tr, nSegMesProy, (int)dr["t329_idclaseeco"], dr["t376_motivo"].ToString(), (decimal)dr["t376_importe"], iNodoDestino, iProvedor, Constantes.FicheroDatos);
                }
                else
                {
                    string sMsg = "No existe un proyectosubnodo correspondiente al proyecto " + dr["t301_idproyecto"].ToString() + " y al " + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + " " + dr["t303_idnodo"].ToString() + ".";
                    dr.Close();
                    dr.Dispose();
                    throw new Exception(sMsg);
                }
                iNumLin++;
			}
            dr.Close();
            dr.Dispose();

            sResul = "OK@#@";
            Conexion.CommitTransaccion(tr);
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al procesar el fichero en la línea " + iNumLin.ToString(), ex);
            Conexion.CerrarTransaccion(tr);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    private string cabErrores()
    {
        return "<table id='tblErrores' style='width: 950px;'><col style='width:40px' /><col style='width:65px' /><col style='width:75px' /><col style='width:65px' /><col style='width:65px' /><col style='width:65px' /><col style='width:65px' /><col style='width:150px' /><col style='width:360px' />";
    }

    private bool validarCampos(SqlDataReader dr)
    {
		// EL RESTO DE LAS VALIDACIONES SE HACEN POR INTEGRIDAD REFERENCIAL CON SUS RESPECTIVAS TABLAS

        oProyectoSubNodo = (ProyectoSubNodo)htProyectoSubNodo[dr["t301_idproyecto"].ToString() + "/" + dr["t303_idnodo"].ToString()];
        if (oProyectoSubNodo == null)
        {
            sbE.Append(ponerFilaError(dr, "No existe un proyectosubnodo correspondiente al proyecto " + dr["t301_idproyecto"].ToString() + " y al " + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + " " + dr["t303_idnodo"].ToString() + "."));
            return false;
        }

        oClaseEconomica = (ClaseEconomica)htClaseEconomica[dr["t329_idclaseeco"].ToString()];
        if (oClaseEconomica != null)
        {
            if ((oClaseEconomica.t329_necesidad == "N") && (dr["t303_idnodo_destino"] == DBNull.Value))
			{
				sbE.Append(ponerFilaError(dr, "El nodo destino no se ha especificado y la clase lo exige"));
				return false;
			}
            if ((oClaseEconomica.t329_necesidad != "N") && (dr["t303_idnodo_destino"] != DBNull.Value))
            {
                sbE.Append(ponerFilaError(dr, "La clase económica especificada debiera exigir nodo"));
                return false;
            }
            if ((oClaseEconomica.t329_necesidad == "P") && (dr["t315_codigoexterno"] == DBNull.Value))
            {
                sbE.Append(ponerFilaError(dr, "El codigo externo del proveedor no se ha especificado y la clase lo exige"));
                return false;
            }
            if ((oClaseEconomica.t329_necesidad != "P") && (dr["t315_codigoexterno"] != DBNull.Value))
            {
                sbE.Append(ponerFilaError(dr, "La clase económica especificada debiera exigir proveedor"));
                return false;
            }
            if ((dr["t303_idnodo_destino"] != DBNull.Value) && (dr["t315_codigoexterno"] != DBNull.Value))
            {
                sbE.Append(ponerFilaError(dr, "No es posible especificar proveedor y nodo destino a la vez"));
                return false;
            }

            if (oProyectoSubNodo.t305_cualidad == "C" && oClaseEconomica.t329_visiblecarruselC == false)
            {
                sbE.Append(ponerFilaError(dr, "La clase económica (" + oClaseEconomica.t329_idclaseeco + ") para este proyecto/nodo (Contratante) no tiene visibilidad en el carrusel."));
                return false;
            }
            if (oProyectoSubNodo.t305_cualidad == "J" && oClaseEconomica.t329_visiblecarruselJ == false)
            {
                sbE.Append(ponerFilaError(dr, "La clase económica (" + oClaseEconomica.t329_idclaseeco + ") para este proyecto/nodo (Replicado sin gestión) no tiene visibilidad en el carrusel."));
                return false;
            }
            if (oProyectoSubNodo.t305_cualidad == "P" && oClaseEconomica.t329_visiblecarruselP == false)
            {
                sbE.Append(ponerFilaError(dr, "La clase económica (" + oClaseEconomica.t329_idclaseeco + ") para este proyecto/nodo (Replicado con gestión propia) no tiene visibilidad en el carrusel."));
                return false;
            }
		}


        if (!Fechas.ValidarAnnomes((int)dr["t325_anomes"]))
        {
            sbE.Append(ponerFilaError(dr, "Año o mes de la fecha incorrecto (" + dr["t325_anomes"].ToString().Substring(0, 4) + ")"));
            return false;
        }

        return true;
    }
    private string ponerFilaError(SqlDataReader dr, string sMens)
    {
        try
        {
            sb.Length = 0;
            sb.Append("<tr style='height:16px' id=" + iCont.ToString() + "><td>");
            sb.Append(dr["t303_idnodo"].ToString());
            sb.Append("</td><td>");
            sb.Append(dr["t301_idproyecto"].ToString());
            sb.Append("</td><td>");
            sb.Append(dr["t325_anomes"].ToString());
            sb.Append("</td><td>");
            sb.Append(dr["t329_idclaseeco"].ToString());
            sb.Append("</td><td>");
			sb.Append(decimal.Parse(dr["t376_importe"].ToString()).ToString("##,###,###,##0.00"));
            sb.Append("</td><td>");
            sb.Append(dr["t303_idnodo_destino"].ToString());			           
            sb.Append("</td><td>");
            sb.Append(dr["t315_codigoexterno"].ToString());

            sb.Append("</td><td><nobr  style='cursor:default' title='" + dr["t376_motivo"].ToString() + "' class='NBR W140'>");
            sb.Append(dr["t376_motivo"].ToString());
            sb.Append("</nobr>");

            sb.Append("</td><td><nobr style='cursor:default' title='" + sMens + "' class='NBR W350'>");
            sb.Append(sMens);
            sb.Append("</nobr></td></tr>");
        }
        catch (Exception ex)
        {
            sb.Append("Error@#@" + Errores.mostrarError("Error al ponerFilaError", ex));
        }
        return sb.ToString();
    }
    protected string mostrarTabla()
    {
        StringBuilder sb = new StringBuilder();
        try
        {
			SqlDataReader dr = AddDATAECO.Catalogo();
            sb.Append("<table id='tblDatos' style='font-family:Arial;font-size:8pt;' cellSpacing='0' cellPadding='0' border='1'>");
            sb.Append("<tbody>");
            sb.Append("<tr align='center'>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + "</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Proyecto</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Año/Mes</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Clase</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Importe</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + " de destino</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Proveedor</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Motivo</td>");
            sb.Append("</tr>");

            while (dr.Read())
            {
                sb.Append("<tr>");
                sb.Append("<td>" + dr["t303_idnodo"].ToString() + "</td>");
                sb.Append("<td>" + dr["t301_idproyecto"].ToString() + "</td>");
                sb.Append("<td>" + dr["t325_anomes"].ToString() + "</td>");
                sb.Append("<td>" + dr["t329_idclaseeco"].ToString() + "</td>");
                sb.Append("<td>" + dr["t376_importe"].ToString() + "</td>");
                sb.Append("<td>" + dr["t303_idnodo_destino"].ToString() + "</td>");
                sb.Append("<td>" + dr["t315_codigoexterno"].ToString() + "</td>");                
                sb.Append("<td>" + dr["t376_motivo"].ToString() + "</td>");           
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            string sIdCache = "EXCEL_CACHE_" + Session["IDFICEPI_ENTRADA"].ToString() + "_" + DateTime.Now.ToString();
            Session[sIdCache] = sb.ToString(); ;

            return "OK@#@cacheado@#@" + sIdCache + "@#@" + sb.ToString(); 
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los datos de T494_DATOECOTABLA.", ex);
        }
    }
}

