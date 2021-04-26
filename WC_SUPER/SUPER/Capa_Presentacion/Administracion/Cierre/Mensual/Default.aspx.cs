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
using EO.Web;
using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;


public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHTML;
    public SqlConnection oConn;
    public SqlTransaction tr;
    public int nAnoMesActual = DateTime.Now.Year * 100 + DateTime.Now.Month;
    public int nAnoMesActualECO;
    public int nAnoMesActualIAP;
	
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.nBotonera = 41;// 9;

            Master.TituloPagina = "Cierre mensual";
            Master.FicherosCSS.Add("Capa_Presentacion/ECO/Avance/Avance.css");
            Master.bFuncionesLocales = true;
            if (!Page.IsPostBack)
            {
                try
                {
                    PARAMETRIZACIONSUPER oPar = PARAMETRIZACIONSUPER.Select(null);
                    nAnoMesActualECO = oPar.t725_ultcierreempresa_ECO;
                    nAnoMesActualIAP = oPar.t725_ultcierreempresa_IAP;
                    this.lblNodo.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
                    this.lblNodo.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.NODO));
                    this.lblNodo2.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
                    this.lblNodo2.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.NODO));
                    //strTablaHTML = CatalogoNodos(nAnoMesActual);

                    string[] aTabla = Regex.Split(CatalogoNodos(nAnoMesActual), "@#@");
                    if (aTabla[0] == "OK") this.strTablaHTML = aTabla[1];
                    else Master.sErrores += Errores.mostrarError(aTabla[1]);

                }
                catch (Exception ex)
                {
                    Master.sErrores = Errores.mostrarError("Error al obtener los datos", ex);
                }
            }
            //1º Se indican (por este orden) la función a la que se va a devolver el resultado
            //   y la función que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

            //2º Se "registra" la función que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
    }
    protected void Botonera_Click(object sender, EO.Web.ToolBarEventArgs e)
    {
        switch (e.Item.CommandName.ToLower())
        {
            case "regresar":
                try
                {
                    Response.Redirect(HistorialNavegacion.Leer(), true);
                }
                catch (System.Threading.ThreadAbortException) { }
                break;
        }
    }
    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        switch (aArgs[0])
        {
            //case ("grabar"):
            //    sResultado += Grabar(aArgs[1], aArgs[2], aArgs[3], aArgs[4]);//anomes de cierre y lista de Nodos a cerrar separados por ##
            //    break;
            case ("grabarEco"):
                sResultado += GrabarEco(aArgs[1], aArgs[2], aArgs[3]);//anomes de cierre y lista de Nodos a cerrar separados por ##
                break;
            case ("getNodos"):
                sResultado += CatalogoNodos(int.Parse(aArgs[1]));
                break;
            //case ("setAutoTraspIAP"):
            //    sResultado += setAutoTraspIAP(aArgs[1], aArgs[2]);
            //    break;
            //case ("traspasoNodosIAP"):
            //    sResultado += traspasoNodosIAP(aArgs[1], aArgs[2], aArgs[3]);
            //    break;
            case ("cierreIAP"):
                sResultado += cierreIAP(aArgs[1], aArgs[2], aArgs[3], aArgs[4]);
                break;
            case ("setUMC_ECO"):
                sResultado += setUMC_ECO(aArgs[1]);
                break;
            case ("setUMC_IAP"):
                sResultado += setUMC_IAP(aArgs[1]);
                break;
        }
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }

    //protected string Grabar(string sAnoMes, string sCadena, string nAnoMesActual, string sNodosTraspIAP)
    //{
    //    string sResul = "OK@#@";

    //    #region conexion
    //    try
    //    {
    //        oConn = Conexion.Abrir();
    //        tr = Conexion.AbrirTransaccion(oConn);
    //    }
    //    catch (Exception ex)
    //    {
    //        sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
    //        return sResul;
    //    }
    //    #endregion
    //    try
    //    {
    //        string[] aNodos = Regex.Split(sCadena, "##");
    //        for (int i = 0; i < aNodos.Length; i++)
    //        {
    //            if (aNodos[i] != "")
    //                NODO.UpdateCierreIAP(tr, int.Parse(aNodos[i]), int.Parse(sAnoMes));
    //        }

    //        if (sNodosTraspIAP != "")
    //            NODO.TraspasarConsumosIAP(tr, sNodosTraspIAP, "1");

    //        Conexion.CommitTransaccion(tr);
    //        //sResul += CatalogoNodos(int.Parse(nAnoMesActual));
    //        sResul += Regex.Split(CatalogoNodos(int.Parse(nAnoMesActual)), "@#@")[1];
    //    }
    //    catch (Exception ex)
    //    {
    //        Conexion.CerrarTransaccion(tr);
    //        sResul = "Error@#@" + Errores.mostrarError("Error al grabar", ex);
    //    }
    //    finally
    //    {
    //        Conexion.Cerrar(oConn);
    //    }
    //    return sResul;
    //}
    protected string GrabarEco(string sAnoMes, string sCadena, string nAnoMesActual)
    {
        string sResul = "OK@#@";

        #region conexion
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccion(oConn);
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
            return sResul;
        }
        #endregion
        try
        {
            string[] aNodos = Regex.Split(sCadena, "##");
            for (int i = 0; i < aNodos.Length; i++)
            {
                if (aNodos[i] != "")
                    NODO.UpdateCierreEco(tr, int.Parse(aNodos[i]), int.Parse(sAnoMes));
            }
            Conexion.CommitTransaccion(tr);
            //sResul += CatalogoNodos(int.Parse(nAnoMesActual));
            sResul += Regex.Split(CatalogoNodos(int.Parse(nAnoMesActual)), "@#@")[1];
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    private string CatalogoNodos(int nAnoMesActual)
    {
        string sUltCierre, sUltCierreEco, sUltTraspasoIAP;
        StringBuilder sb=new StringBuilder();
        try
        {

            sb.Append("<table id='tblDatos' class='texto' style='WIDTH: 970px;'>");
            sb.Append("<colgroup>");
            sb.Append("     <col style='width:70px;' />");
            sb.Append("     <col style='width:70px;' />");
            sb.Append("     <col style='width:40px;' />");
            sb.Append("     <col style='width:340px;' />");
            sb.Append("     <col style='width:40px;' />");
            sb.Append("     <col style='width:90px;' />");
            sb.Append("     <col style='width:90px;' />");
            sb.Append("     <col style='width:70px;' />");
            sb.Append("     <col style='width:70px;' />");
            sb.Append("     <col style='width:90px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>" + (char)10);
            SqlDataReader dr = NODO.CatalogoCierreMensual(nAnoMesActual);
            while (dr.Read())
            {
                sUltCierre = dr["t303_ultcierreiap"].ToString();
                if (sUltCierre != ""){
                    sUltCierre = Fechas.AnnomesAFechaDescLarga(int.Parse(sUltCierre));
                }

                sb.Append("<tr id='" + dr["t303_idnodo"].ToString() + "' ");
                sb.Append("uc='" + dr["t303_ultcierreiap"].ToString() + "' ");
                sb.Append("npma=" + dr["num_proy_meses_abiertos"].ToString() + " ");
                sb.Append("utpn=" + dr["t303_utpnIAP"].ToString() + " ");
                if ((bool)dr["t303_cierreIAPestandar"])
                    sb.Append(" m='1'");
                else
                    sb.Append(" m='0'");
                
                sUltCierreEco = dr["t303_ultcierreeco"].ToString();
                if (sUltCierreEco != ""){
                    sUltCierreEco = Fechas.AnnomesAFechaDescLarga(int.Parse(sUltCierreEco));
                }

                sb.Append(" uce='" + dr["t303_ultcierreeco"].ToString() + "'");
                if ((bool)dr["t303_cierreECOestandar"])
                    sb.Append(" me='1'");
                else
                    sb.Append(" me='0'");
                sb.Append(" style='height:16px'>");

                sb.Append("<td align='center'><input type='checkbox' class='checkTabla'");
                if ((bool)dr["t303_cierreIAPestandar"]) sb.Append(" checked='true' ");
                sb.Append("onclick='setIAP(this)'></td>");

                sb.Append("<td align='center'><input type='checkbox' class='checkTabla'");
                if ((bool)dr["t303_cierreECOestandar"]) sb.Append(" checked='true' ");
                sb.Append("onclick='setECO(this)'></td>");

                sb.Append("<td style=\"border-right:'';text-align:right;\">" + dr["t303_idnodo"].ToString() + "</td>");
                sb.Append("<td style='padding-left:10px;text-align:left;'><div class='NBR W330'>" + dr["t303_denominacion"].ToString() + "</div></td>");
                if ((int)dr["num_proy_meses_abiertos"] > 0) sb.Append("<td style='padding-right:5px;text-align:right;' class='MA' ondblclick='getPMA(this);'>" + dr["num_proy_meses_abiertos"].ToString() + "</td>");
                else sb.Append("<td style='padding-right:5px;text-align:right;'></td>");
                sb.Append("<td align='center'>" + sUltCierre + "</td>");
                sb.Append("<td align='center'>" + sUltCierreEco + "</td>");

                sb.Append("<td align='center'><input type='checkbox' class='checkTabla'");
                if ((bool)dr["t303_autotraspasoIAP"]) 
                    sb.Append(" checked='true' ");
                //sb.Append("onclick='setAutoTraspIAP(this)'></td>");
                sb.Append("></td>");

                sb.Append("<td align='center'><input type='checkbox' class='checkTabla' ></td>");

                sUltTraspasoIAP = dr["t303_utpnIAP"].ToString();
                if (sUltTraspasoIAP != "")
                {
                    sUltTraspasoIAP = Fechas.AnnomesAFechaDescLarga(int.Parse(sUltTraspasoIAP));
                }
                sb.Append("<td style=\"border-right:'';text-align:right;\">" + sUltTraspasoIAP + "</td>");
                sb.Append("</tr>" + (char)10);
            }
            dr.Close(); 
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los nodos.", ex);
        }
    }
    protected string setUMC_ECO(string sUMC_ECO)
    {
        try
        {
            PARAMETRIZACIONSUPER.UpdateCierreEmpresaECO(null, int.Parse(sUMC_ECO));
            return "OK@#@";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al modificar el último mes cerrado para recursos externos", ex);
        }
    }
    protected string setUMC_IAP(string sUMC_IAP)
    {
        try
        {
            PARAMETRIZACIONSUPER.UpdateCierreEmpresaIAP(null, int.Parse(sUMC_IAP));
            return "OK@#@";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al modificar el último mes de cierre de empresa para IAP", ex);
        }
    }
    //protected string setAutoTraspIAP(string sNodo, string sAutoTraspIAP)
    //{
    //    try
    //    {
    //        NODO.UpdateAutoTraspasoIAP(null, int.Parse(sNodo), (sAutoTraspIAP == "1") ? true : false);
    //        return "OK@#@";
    //    }
    //    catch (Exception ex)
    //    {
    //        return "Error@#@" + Errores.mostrarError("Error al modificar el traspaso automático de consumos IAP", ex);
    //    }
    //}
    //protected string traspasoNodosIAP(string nAnoMesActual, string sTraspasoIAP, string sNodosACerrar)
    //{
    //    string sResul = "OK@#@";

    //    #region conexion
    //    try
    //    {
    //        oConn = Conexion.Abrir();
    //        tr = Conexion.AbrirTransaccion(oConn);
    //    }
    //    catch (Exception ex)
    //    {
    //        sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
    //        return sResul;
    //    }
    //    #endregion

    //    try
    //    {
    //        bool bCerrarIAPNodos=false, bTraspasarEsfuerzos=true;
    //        if (sTraspasoIAP == "N")
    //            bTraspasarEsfuerzos = false;

    //        NODO.TraspasarConsumosIAP(tr, int.Parse(nAnoMesActual), int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString()), bCerrarIAPNodos, bTraspasarEsfuerzos, sNodosACerrar);

    //        Conexion.CommitTransaccion(tr);
    //        sResul += Regex.Split(CatalogoNodos(int.Parse(nAnoMesActual)), "@#@")[1];
    //    }
    //    catch (Exception ex)
    //    {
    //        Conexion.CerrarTransaccion(tr);
    //        sResul = "Error@#@" + Errores.mostrarError("Error al realizar el traspaso de consumos IAP", ex);
    //    }
    //    finally
    //    {
    //        Conexion.Cerrar(oConn);
    //    }
    //    return sResul;
    //}

    protected string cierreIAP(string sAnoMes, string sCerrarIAP, string sTraspasoIAP, string sNodosACerrar)
    {
        string sResul = "OK@#@";

        #region conexion
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccion(oConn);
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
            return sResul;
        }
        #endregion
        try
        {
            bool bCerrarIAP = true, bTraspasarEsfuerzos = true;
            if (sTraspasoIAP == "N")
                bTraspasarEsfuerzos = false;
            if (sCerrarIAP == "N")
                bCerrarIAP = false;

            NODO.TraspasarConsumosIAP(tr, int.Parse(sAnoMes), int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString()), bCerrarIAP, bTraspasarEsfuerzos, sNodosACerrar);

            Conexion.CommitTransaccion(tr);
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error en el cierre IAP nodo a nodo", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }

}