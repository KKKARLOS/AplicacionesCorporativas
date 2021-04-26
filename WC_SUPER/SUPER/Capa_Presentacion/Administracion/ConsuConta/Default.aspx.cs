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

    public Hashtable htProyectoSubNodo, htClaseEconomica;

    public ProyectoSubNodo oProyectoSubNodo = null;

    public int iCont = 0, iNumOk = 0;
    public int nAnno = DateTime.Now.Year;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            sErrores = "";
            Master.nBotonera = 56;
            Master.bFuncionesLocales = true;
            Master.TituloPagina = "Carga de consumos de contabilidad";

            if (!Page.IsPostBack)
            {
                try
                {
                    this.txtAnnoVisible.Text = nAnno.ToString();
                    //CargarConsuConta();
                    //string[] aTabla = Regex.Split(CargarConsuConta(), "@#@");
                    //if (aTabla[0] == "OK")
                    //{
                    //    this.divB.InnerHtml = aTabla[1];
                    //    cldTotalLin.InnerText = aTabla[2];
                    //    cldLinOK.InnerText = aTabla[3];
                    //    cldLinErr.InnerText = aTabla[4];
                    //}
                    //else Master.sErrores += Errores.mostrarError(aTabla[1]);

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
                nAnno = int.Parse(aArgs[1]);
                sResultado += Procesar();
                break;
            case ("mostrarTabla"):
                sResultado += mostrarTabla();
                break;
            case ("getConsuContaCoro"):
                nAnno = int.Parse(aArgs[1]);
                sResultado += CargarConsuConta();
                break;
        }
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }
    private string CargarConsuConta()
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            #region Obtenión de dataset con proyectosubnodo y creación de HASTABLE
            oProyectoSubNodo = null;
            DataSet ds = CONSUCONTACORO.GetDatosParaValidacion();

            htProyectoSubNodo = new Hashtable();
            foreach (DataRow dsProyectoSubNodo in ds.Tables[0].Rows)//Recorro tabla de proyectos-subnodos 
            {
                htProyectoSubNodo.Add(dsProyectoSubNodo["t301_idproyecto"].ToString() + @"/" + dsProyectoSubNodo["t303_idnodo"].ToString(), 
                                        new ProyectoSubNodo((int)dsProyectoSubNodo["t301_idproyecto"], 
                                                            (int)dsProyectoSubNodo["t305_idproyectosubnodo"],
                                                            (int)dsProyectoSubNodo["t303_idnodo"],
                                                            dsProyectoSubNodo["t305_cualidad"].ToString()
                                                            )
                                     );
            }

            ds.Dispose();
            #endregion

            // Lectura, validación y conteo, de las filas de la tabla
            //SqlDataReader dr = CONSUCONTACORO.GetCatalogo(null);
            DataSet ds1 = CONSUCONTACORO.GetCatalogo(null);
            foreach (DataRow oFila in ds1.Tables[0].Rows)
            {
                iCont++;
                if (validarCampos(oFila)) iNumOk++;
            }
            ds1.Dispose();

            sb.Append("</table>");

            return "OK@#@" + cabErrores() + sbE + "</table>@#@" + CONSUCONTACORO.numFilas(null).ToString("#,##0") + "@#@" + iNumOk.ToString("#,##0") + "@#@" + (iCont - iNumOk).ToString("#,##0");
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener el resumen económico de los proyectos.", ex);
        }
    }

    private string Procesar()
    {
        string sResul = "";
        try
        {
            #region Obtenión de dataset con proyectosubnodo y creación de HASTABLE
            oProyectoSubNodo = null;
            DataSet ds = CONSUCONTACORO.GetDatosParaValidacion();

            htProyectoSubNodo = new Hashtable();
            foreach (DataRow dsProyectoSubNodo in ds.Tables[0].Rows)//Recorro tabla de proyectos-subnodos 
            {
                htProyectoSubNodo.Add(dsProyectoSubNodo["t301_idproyecto"].ToString() + @"/" + dsProyectoSubNodo["t303_idnodo"].ToString(),
                                        new ProyectoSubNodo((int)dsProyectoSubNodo["t301_idproyecto"],
                                                            (int)dsProyectoSubNodo["t305_idproyectosubnodo"],
                                                            (int)dsProyectoSubNodo["t303_idnodo"],
                                                            dsProyectoSubNodo["t305_cualidad"].ToString()
                                                            )
                                     );
            }

            ds.Dispose();
            #endregion

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

            CONSUCONTA.DeleteByAnno(tr, nAnno);

            DataSet ds2 = CONSUCONTACORO.GetCatalogo(tr);
            foreach (DataRow oFila in ds2.Tables[0].Rows)
            {
				oProyectoSubNodo = (ProyectoSubNodo)htProyectoSubNodo[oFila["t301_idproyecto"].ToString() + "/" + oFila["t303_idnodo"].ToString()];
				if (oProyectoSubNodo != null)
				{
                    if (oFila["t478_descripcion"].ToString() != "" && int.Parse(oFila["t325_anomes"].ToString().Substring(0, 4)) == nAnno)
                    {
                        nSegMesProy = SEGMESPROYECTOSUBNODO.ExisteSegMesProy(tr, oProyectoSubNodo.t305_idproyectosubnodo, (int)oFila["t325_anomes"]);
                        if (nSegMesProy == 0)
                        {
                            sEstadoMes = SEGMESPROYECTOSUBNODO.EstadoMesACrear(tr, oProyectoSubNodo.t305_idproyectosubnodo, (int)oFila["t325_anomes"]);
                            nSegMesProy = SEGMESPROYECTOSUBNODO.Insert(tr, oProyectoSubNodo.t305_idproyectosubnodo, (int)oFila["t325_anomes"], sEstadoMes, 0, 0, false, 0, 0);
                        }
                        CONSUCONTA.Insert(tr, nSegMesProy, (int)oFila["t315_idproveedor"], (int)oFila["t478_nconsumo"], (decimal)oFila["t478_importe"], (int)oFila["t329_idclaseeco"], (int)oFila["t313_idempresa"], (int)oFila["t478_ndocumento"], oFila["t478_descripcion"].ToString());
                    }
				}
                //else
                //{
                //    string sMsg = "No existe un proyectosubnodo correspondiente al proyecto " + dr["t301_idproyecto"].ToString() + " y al " + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + " " + dr["t303_idnodo"].ToString() + ".";
                //    dr.Close();
                //    dr.Dispose();
                //    throw new Exception(sMsg);
                //}
			}
            ds2.Dispose();

            sResul = "OK@#@";
            Conexion.CommitTransaccion(tr);
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al procesar los datos", ex);
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
        return @"<table id='tblErrores' style='WIDTH: 950px;'>
                    <colgroup>
                        <col style='width:40px;' />
                        <col style='width:65px;' />
                        <col style='width:75px;' />
                        <col style='width:65px;' />
                        <col style='width:65px;' />
                        <col style='width:65px;' />
                        <col style='width:215px;' />
                        <col style='width:360px;' />
                    </colgroup>";
    }

    private bool validarCampos(DataRow oFila)
    {
		// EL RESTO DE LAS VALIDACIONES SE HACEN POR INTEGRIDAD REFERENCIAL CON SUS RESPECTIVAS TABLAS

        //oClaseEconomica = (ClaseEconomica)htClaseEconomica[oFila["t329_idclaseeco"].ToString()];
        //if (oClaseEconomica != null)
        //{
        //    sbE.Append(ponerFilaError(oFila, "La clase económica especificada no existe"));
        //    return false;
        //}

        //if (!Fechas.ValidarAnnomes((int)oFila["t325_anomes"])){
        //    sbE.Append(ponerFilaError(oFila, "Año o mes de la fecha incorrecto (" + oFila["t325_anomes"].ToString().Substring(0, 4) + ")"));
        //    return false;
        //}

        oProyectoSubNodo = (ProyectoSubNodo)htProyectoSubNodo[oFila["t301_idproyecto"].ToString() + "/" + oFila["t303_idnodo"].ToString()];
        if (oProyectoSubNodo == null)
        {
            sbE.Append(ponerFilaError(oFila, "No existe un proyectosubnodo correspondiente al proyecto "+ oFila["t301_idproyecto"].ToString() +" y al "+ Estructura.getDefCorta(Estructura.sTipoElem.NODO) +" "+ oFila["t303_idnodo"].ToString() +"."));
            return false;
        }

        if (oFila["t478_descripcion"].ToString()=="")//.Trim()
        {
            sbE.Append(ponerFilaError(oFila, "La descripción es obligatoria."));
            return false;
        }

        if (int.Parse(oFila["t325_anomes"].ToString().Substring(0,4)) != nAnno)
        {
            sbE.Append(ponerFilaError(oFila, "El año del consumo no se corresponde con el año de referencia."));
            return false;
        }


        return true;
    }
    private string ponerFilaError(DataRow oFila, string sMens)
    {
        try
        {
            sb.Length = 0;
            sb.Append("<tr id='" + iCont.ToString() + "' style='height:16px;' onmouseover='TTip(event)'>");
            sb.Append("<td style='text-align:right;'>" + oFila["t303_idnodo"].ToString() + "</td>");
            sb.Append("<td style='text-align:right;'>" + oFila["t301_idproyecto"].ToString() + "</td>");
            sb.Append("<td style='text-align:right;'>" + oFila["t325_anomes"].ToString() + "</td>");
            sb.Append("<td style='text-align:right;'>" + oFila["t329_idclaseeco"].ToString() + "</td>");
            sb.Append("<td style='text-align:right;'>" + decimal.Parse(oFila["t478_importe"].ToString()).ToString("#,##0.00") + "</td>");
            sb.Append("<td style='text-align:right;padding-right:4px'>" + oFila["t315_idproveedor"].ToString() + "</td>");
            sb.Append("<td style='text-align:left;'><div class='NBR W200'>" + oFila["t478_descripcion"].ToString() + "</div></td>");
            sb.Append("<td style='text-align:left;'><div class='NBR W350'>" + sMens + "</div></td>");
            sb.Append("</tr>");
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
            DataSet ds = CONSUCONTACORO.GetCatalogo(null);

            sb.Append("<table id='tblDatos' style='font-family:Arial;font-size:8pt;' border='1'>");
            sb.Append("<tbody>");
            sb.Append("<tr align='center'>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>"+ Estructura.getDefCorta(Estructura.sTipoElem.NODO) +"</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Proyecto</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Año/Mes</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Proveedor</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>N. consumo</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Importe</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Clase</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Empresa</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>N. documento</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Descripción</td>");
            sb.Append("</tr>");

            foreach (DataRow oFila in ds.Tables[0].Rows)
            {
                sb.Append("<tr>");
                sb.Append("<td>" + oFila["t303_idnodo"].ToString() + "</td>");
                sb.Append("<td>" + oFila["t301_idproyecto"].ToString() + "</td>");
                sb.Append("<td>" + oFila["t325_anomes"].ToString() + "</td>");
                sb.Append("<td>" + oFila["t315_idproveedor"].ToString() + "</td>");
                sb.Append("<td>" + oFila["t478_nconsumo"].ToString() + "</td>");
                sb.Append("<td>" + oFila["t478_importe"].ToString() + "</td>");
                sb.Append("<td>" + oFila["t329_idclaseeco"].ToString() + "</td>");
                sb.Append("<td>" + oFila["t313_idempresa"].ToString() + "</td>");
                sb.Append("<td>" + oFila["t478_ndocumento"].ToString() + "</td>");
                sb.Append("<td>" + oFila["t478_descripcion"].ToString() + "</td>");
                sb.Append("</tr>");
            }
            ds.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            //return "OK@#@" + sb.ToString();
            string sIdCache = "EXCEL_CACHE_" + Session["IDFICEPI_ENTRADA"].ToString() + "_" + DateTime.Now.ToString();
            Session[sIdCache] = sb.ToString(); ;

            return "OK@#@cacheado@#@" + sIdCache + "@#@" + sb.ToString(); 
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los datos de T495_CONSUCONTACORO.", ex);
        }
    }
}

