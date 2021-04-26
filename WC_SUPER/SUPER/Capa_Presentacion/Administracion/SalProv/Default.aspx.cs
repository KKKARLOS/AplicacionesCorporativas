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
            Master.nBotonera = 43;
            Master.bFuncionesLocales = true;
            Master.TituloPagina = "Saldo de proveedores";

            if (!Page.IsPostBack)
            {
                try
                {
                    string[] aTabla = Regex.Split(CargarSaldoProveedores(), "@#@");
                    if (aTabla[0] == "OK")
                    {
                        this.divB.InnerHtml = aTabla[1];
                        cldTotalLin.InnerText = aTabla[2];
                        cldLinOK.InnerText = aTabla[3];
                        cldLinErr.InnerText = aTabla[4];
                    }
                    else Master.sErrores += Errores.mostrarError(aTabla[1]);

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
            case ("getSalProvCoro"):
                sResultado += CargarSaldoProveedores();
                break;
        }
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }
    private string CargarSaldoProveedores()
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            #region Obtenión de dataset con proyectosubnodo y creación de HASTABLE
            oProyectoSubNodo = null;
            DataSet ds = SALPROVCORO.GetDatosParaValidacion();

            htProyectoSubNodo = new Hashtable();
            foreach (DataRow dsProyectoSubNodo in ds.Tables[0].Rows)//Recorro tabla de proyectos-subnodos 
            {
                htProyectoSubNodo.Add((int)dsProyectoSubNodo["t301_idproyecto"], 
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
            SqlDataReader dr = SALPROVCORO.GetCatalogo();
            while (dr.Read())
            {
                iCont++;
                if (validarCampos(dr)) iNumOk++;
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</table>");

            return "OK@#@" + cabErrores() + sbE + "</table>@#@" + SALPROVCORO.numFilas(null).ToString("#,##0") + "@#@" + iNumOk.ToString("#,##0") + "@#@" + (iCont - iNumOk).ToString("#,##0");
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener el resumen económico de los proyectos.", ex);
        }
    }

    private string Procesar()
    {
        string sResul = "";
        int iNumLin = 1;
        try
        {
            #region Obtenión de dataset con proyectosubnodo y creación de HASTABLE
            oProyectoSubNodo = null;
            DataSet ds = SALPROVCORO.GetDatosParaValidacion();

            htProyectoSubNodo = new Hashtable();
            foreach (DataRow dsProyectoSubNodo in ds.Tables[0].Rows)//Recorro tabla de proyectos-subnodos 
            {
                htProyectoSubNodo.Add((int)dsProyectoSubNodo["t301_idproyecto"],
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
			
            //string sEstadoMes = "";
            //int nSegMesProy = 0;

            SALPROV.DeleteGlobal(tr);

            SqlDataReader dr = SALPROVCORO.GetCatalogo();
			while (dr.Read())
            {
				oProyectoSubNodo = (ProyectoSubNodo)htProyectoSubNodo[(int)dr["t301_idproyecto"]];
				if (oProyectoSubNodo != null)
				{				
                    SALPROV.Insert(tr, oProyectoSubNodo.t305_idproyectosubnodo, (int)dr["t315_idproveedor"], (int)dr["t329_idclaseeco"], (decimal)dr["t479_importe"]);
				}
                else
                {
                    string sMsg = "No existe una instancia contratante correspondiente al proyecto " + dr["t301_idproyecto"].ToString() + ".";
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
        return  @"<table id='tblErrores' style='WIDTH: 950px;'>
                    <colgroup>
                        <col style='width:80px;' />
                        <col style='width:80px;' />
                        <col style='width:60px;' />
                        <col style='width:100px;' />
                        <col style='width:630px;' />
                    </colgroup>";
    }

    private bool validarCampos(SqlDataReader dr)
    {
		// EL RESTO DE LAS VALIDACIONES SE HACEN POR INTEGRIDAD REFERENCIAL CON SUS RESPECTIVAS TABLAS

        //oClaseEconomica = (ClaseEconomica)htClaseEconomica[dr["t329_idclaseeco"].ToString()];
        //if (oClaseEconomica != null)
        //{
        //    sbE.Append(ponerFilaError(dr, "La clase económica especificada no existe"));
        //    return false;
        //}

        //if (!Fechas.ValidarAnnomes((int)dr["t325_anomes"])){
        //    sbE.Append(ponerFilaError(dr, "Año o mes de la fecha incorrecto (" + dr["t325_anomes"].ToString().Substring(0, 4) + ")"));
        //    return false;
        //}

        oProyectoSubNodo = (ProyectoSubNodo)htProyectoSubNodo[(int)dr["t301_idproyecto"]];
        if (oProyectoSubNodo == null)
        {
            sbE.Append(ponerFilaError(dr, "No existe una instancia contratante correspondiente al proyecto "+ dr["t301_idproyecto"].ToString() +"."));
            return false;
        }

        return true;
    }
    private string ponerFilaError(SqlDataReader dr, string sMens)
    {
        try
        {
            sb.Length = 0;
            sb.Append("<tr id=" + iCont.ToString() + " style='height:16px;' onmouseover='TTip(event)'>");
            sb.Append("<td style='text-align:right;'>" + dr["t301_idproyecto"].ToString() +"</td>");
            sb.Append("<td style='text-align:right;'>" + dr["t315_idproveedor"].ToString() + "</td>");
            sb.Append("<td style='text-align:right;'>" + dr["t329_idclaseeco"].ToString() + "</td>");
            sb.Append("<td style='text-align:right;'>" + decimal.Parse(dr["t479_importe"].ToString()).ToString("#,##0.00") + "</td>");
            sb.Append("<td style='padding-left:10px;'><div class='NBR W600'>" + sMens + "</div></td>");
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
            SqlDataReader dr = SALPROVCORO.GetCatalogo();
            sb.Append("<table id='tblDatos' style='font-family:Arial;font-size:8pt;' cellSpacing='0' cellPadding='0' border='1'>");
            sb.Append("<tbody>");
            sb.Append("<tr align='center'>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Proyecto</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Proveedor</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Clase</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Importe</td>");
            sb.Append("</tr>");

            while (dr.Read())
            {
                sb.Append("<tr>");
                sb.Append("<td>" + dr["t301_idproyecto"].ToString() + "</td>");
                sb.Append("<td>" + dr["t315_idproveedor"].ToString() + "</td>");
                sb.Append("<td>" + dr["t329_idclaseeco"].ToString() + "</td>");
                sb.Append("<td>" + dr["t479_importe"].ToString() + "</td>");
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
            return "Error@#@" + Errores.mostrarError("Error al obtener los datos de T496_SALPROVCORO.", ex);
        }
    }
}

