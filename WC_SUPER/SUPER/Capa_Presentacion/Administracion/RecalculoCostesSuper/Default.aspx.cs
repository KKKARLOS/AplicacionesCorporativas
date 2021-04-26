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
    public string sErrores = "", strTablaHTML="", strNodoDesc="";
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
            strNodoDesc = Estructura.getDefCorta(Estructura.sTipoElem.NODO); 
            Master.nBotonera = 43;
            Master.bFuncionesLocales = true;
            Master.TituloPagina = "Recálculo de costes de personal, base SUPER";

            if (!Page.IsPostBack)
            {
                try
                {
                    //string sResultado = ProcesarFicepi("201210", "3");

                    string[] aTabla = Regex.Split(ObtenerUsuarios(DateTime.Now.Year * 100 + 1), "@#@");
                    if (aTabla[0] == "OK")
                    {
                        strTablaHTML = aTabla[1];
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
                sResultado += Procesar(aArgs[1], aArgs[2]);
                break;
            case ("mostrarTabla"):
                sResultado += mostrarTabla();
                break;
            case ("cargar"):
                sResultado += Cargar(aArgs[1]);
                break;
            case ("getUsuarios"):
                sResultado += ObtenerUsuarios(int.Parse(aArgs[1]));
                break;
        }
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }
    private string ObtenerUsuarios(int iAnoMes)
    {
        StringBuilder sb = new StringBuilder();
        int nTotal = 0, nKO = 0;
        try
        {
            sb.Append("<table id='tblErrores' style='WIDTH: 950px;'>");
	        sb.Append("<colgroup>");
		    sb.Append("    <col style='width:100px;' />");
		    sb.Append("    <col style='width:400px' />");
		    sb.Append("    <col style='width:450px' />");
            sb.Append("</colgroup>");

            DataSet ds = RECALCULOCOSTESSUPER.ValidarSuperCostes(iAnoMes);

            foreach (DataRow oFila in ds.Tables[0].Rows)//Recorro tabla de códigos SUPER no validados
            {
                sb.Append("<tr id='" + oFila["t314_idusuario"] + "' style='height:16px;'>");
                sb.Append("<td style='text-align:right;padding-right:10px;'>" + oFila["t314_idusuario"] + "</td>");
                sb.Append("<td>" + oFila["Profesional"] + "</td>");
                sb.Append("<td>" + oFila["desmotivo"] + "</td>");
                sb.Append("</tr>");
            }
            sb.Append("</table>");

            nTotal = (int)ds.Tables[1].Rows[0]["Filas_Super"];
            nKO = (int)ds.Tables[1].Rows[0]["Filas_Erroneas"];

            ds.Dispose();

            return "OK@#@" + sb.ToString() + "@#@" + nTotal.ToString("#,##0") + "@#@" + (nTotal - nKO).ToString("#,##0") + "@#@" + nKO.ToString("#,##0");
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los datos de validación.", ex);
        }
    }

    private string Procesar(string sMesInicio, string sCaso)
    {
        string sResul = "";
        try
        {
            #region Abro transaccion
            try
            {
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccionSerializable(oConn);
            }
            catch (Exception ex)
            {
                if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
                sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
                return sResul;
            }
            #endregion

            RECALCULOCOSTESSUPER.Procesar(tr, int.Parse(sMesInicio), byte.Parse(sCaso));

            sResul = "OK@#@";
            Conexion.CommitTransaccion(tr);
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al procesar el recálculo de costes de personal.", ex);
            Conexion.CerrarTransaccion(tr);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    protected string mostrarTabla()
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            SqlDataReader dr = RECALCULOCOSTESSUPER.GetCatalogo();
            sb.Append("<table id='tblDatos' style='font-family:Arial;font-size:8pt;' cellSpacing='0' cellPadding='0' border='1'>");
            sb.Append("<tbody>");
            sb.Append("<tr align='center'>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Cód. SUPER</td>");
            sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Coste</td>");
            sb.Append("</tr>");

            while (dr.Read())
            {
                sb.Append("<tr>");
                sb.Append("<td>" + dr["t314_idusuario"].ToString() + "</td>");
                sb.Append("<td>" + dr["t609_costeanual"].ToString() + "</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los datos de T609_RECALCULOCOSTESSUPER.", ex);
        }
    }
    private string Cargar(string sDatos)
    {
        string sResul = "";
        try
        {
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

            RECALCULOCOSTESSUPER.DeleteAll(tr);

            string[] aProfesionales = Regex.Split(sDatos, "#sal#");
            foreach (string oProf in aProfesionales)
            {
                if (oProf == "") continue;
                string[] aProf = Regex.Split(oProf, "#tab#");

                RECALCULOCOSTESSUPER.Insert(tr, int.Parse(aProf[0]), decimal.Parse(aProf[1]));
            }

            sResul = "OK@#@";
            Conexion.CommitTransaccion(tr);
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al cargar la lista de recálculo de costes.", ex);
            Conexion.CerrarTransaccion(tr);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }

    //private string ProcesarFicepi(string sMesInicio, string sCaso)
    //{
    //    string sResul = "";
    //    try
    //    {
    //        #region Abro transaccion
    //        try
    //        {
    //            oConn = Conexion.Abrir();
    //            tr = Conexion.AbrirTransaccionSerializable(oConn);
    //        }
    //        catch (Exception ex)
    //        {
    //            if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
    //            sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
    //            return sResul;
    //        }
    //        #endregion

    //        RECALCULOCOSTES.ProcesarFicepi(tr, int.Parse(sMesInicio), byte.Parse(sCaso));

    //        sResul = "OK@#@";
    //        Conexion.CommitTransaccion(tr);
    //    }
    //    catch (Exception ex)
    //    {
    //        sResul = "Error@#@" + Errores.mostrarError("Error al procesar el recálculo de costes de personal.", ex);
    //        Conexion.CerrarTransaccion(tr);
    //    }
    //    finally
    //    {
    //        Conexion.Cerrar(oConn);
    //    }
    //    return sResul;
    //}

}

