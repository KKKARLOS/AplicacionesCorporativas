using System;
using System.Data;
using System.Data.SqlClient;
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
//Para el List<>
using System.Collections.Generic;

using SUPER.Capa_Negocio;

public partial class Capa_Presentacion_Administracion_Preventa_Estructura_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public int nNE = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                Master.nBotonera = 38;//35
                Master.bFuncionesLocales = true;
                //Master.bEstilosLocales = true;
                Master.TituloPagina = "Estructura preventa";
                Master.FuncionesJavaScript.Add("Javascript/dhtmltooltip.js");

                //nNE = int.Parse(Request.QueryString["nNE"].ToString());
                nNE = 1;
                string strTabla = GenerarArbol(false, nNE);
                string[] aTabla = Regex.Split(strTabla, "@#@");
                if (aTabla[0] != "Error")
                {
                    divBodyFijo.InnerHtml = aTabla[1];
                }
                else Master.sErrores = aTabla[1];
            }
        }
        catch (Exception ex)
        {
            Master.sErrores += Errores.mostrarError("Error al cargar los datos", ex);
        }
        //1º Se indican (por este orden) la función a la que se va a devolver el resultado
        //   y la función que va a acceder al servidor
        string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
        string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

        //2º Se "registra" la función que va a acceder al servidor.
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);

    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case "getEstructura":
                sResultado += GenerarArbol((aArgs[1] == "1") ? true : false, int.Parse(aArgs[2]));
                break;
            case "eliminar":
                sResultado += Eliminar(int.Parse(aArgs[1]), int.Parse(aArgs[2]));
                break;
        }

        //3º Damos contenido a la variable que se envía de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }

    private string GenerarArbol(bool bMostrarInactivos, int nNivelExp)
    {
        bool bMostrarElemento = false;
        IB.SUPER.ADM.SIC.BLL.EstructuraPreventa oEstructura = new IB.SUPER.ADM.SIC.BLL.EstructuraPreventa();
        try
        {
            StringBuilder sbBF = new StringBuilder();
            List<IB.SUPER.ADM.SIC.Models.EstructuraPreventa> oLista = oEstructura.Arbol(bMostrarInactivos);

            string sColor = "black";
            sbBF.Append("<table id='tblBodyFijo' style='width:500px; cursor:url(../../../images/imgManoAzul2.cur),pointer;' cellpadding='0' cellspacing='0' border='0'>");
            sbBF.Append("<colgroup>");
            sbBF.Append("   <col style='width:500px;' />");
            sbBF.Append("</colgroup>");
            sbBF.Append("<tbody>");
            foreach (IB.SUPER.ADM.SIC.Models.EstructuraPreventa oElem in oLista)
            {
                bMostrarElemento = true;
                //if (oElem.indentacion == 1 && oElem.unidad <= 0) bMostrarElemento = false;
                //else
                //{
                    if (oElem.indentacion == 2 && oElem.area <= 0) bMostrarElemento = false;
                    else
                    {
                        if (oElem.indentacion == 3 && oElem.subarea <= 0) bMostrarElemento = false;
                    }
                //}
                if (bMostrarElemento)
                {
                    #region tblBodyFijo
                    sColor = "black";
                    if (!oElem.estado) sColor = "gray";
                    if (oElem.indentacion <= nNivelExp)
                    {
                        if (oElem.indentacion == 1)//&& oElem.unidad==0
                            sbBF.Append("<tr id='" + oElem.unidad.ToString() + "--' ");
                        else if (oElem.indentacion == 2)//&& oElem.area == 0
                            sbBF.Append("<tr id='" + oElem.unidad.ToString() + "-" + oElem.area.ToString() + "-' ");
                        else
                            sbBF.Append("<tr id='" + oElem.unidad.ToString() + "-" + oElem.area.ToString() + "-" + oElem.subarea.ToString() + "' ");
                        sbBF.Append("Unidad='" + oElem.unidad.ToString() + "' ");
                        sbBF.Append(" style='DISPLAY: table-row; HEIGHT: 20px; vertical-align:middle;' nivel=" + oElem.indentacion.ToString() + " ");
                        //if (oElem.unidad != 0)
                        sbBF.Append("onclick='setFilaFija(this)' ");
                        sbBF.Append("ondblclick='mdn(this)'>");
                        if (oElem.indentacion < 3)
                        {
                            if (oElem.indentacion < nNivelExp)
                                sbBF.Append("<td><IMG class='N" + oElem.indentacion.ToString() + "' onclick=mostrar(this,true) src='../../../../images/minus.gif' style='cursor:pointer;'>");
                            else
                            {
                                if (oElem.indentacion == 1 && oElem.unidad <=0)
                                    sbBF.Append("<td><IMG class='N" + oElem.indentacion.ToString() + "' onclick=mostrar(this,true) src='../../../../images/minus.gif' style='cursor:pointer;'>");
                                else
                                    sbBF.Append("<td><IMG class='N" + oElem.indentacion.ToString() + "' onclick=mostrar(this,true) src='../../../../images/plus.gif' style='cursor:pointer;'>");
                            }
                        }
                        else sbBF.Append("<td><IMG class='N" + oElem.indentacion.ToString() + "' src='../../../../images/imgSeparador.gif'>");
                    }
                    else
                    {
                        //sbBF.Append("<tr id='" + oElem.unidad.ToString() + "-" + oElem.area.ToString() + "-" + oElem.subarea.ToString() + "' ");
                        if (oElem.indentacion == 1)//&& oElem.unidad==0
                            sbBF.Append("<tr id='" + oElem.unidad.ToString() + "--' ");
                        else if (oElem.indentacion == 2)//&& oElem.area == 0
                            sbBF.Append("<tr id='" + oElem.unidad.ToString() + "-" + oElem.area.ToString() + "-' ");
                        else
                            sbBF.Append("<tr id='" + oElem.unidad.ToString() + "-" + oElem.area.ToString() + "-" + oElem.subarea.ToString() + "' ");

                        sbBF.Append("Unidad='" + oElem.unidad.ToString() + "' ");
                        sbBF.Append(" style='DISPLAY: none; HEIGHT: 20px; vertical-align:middle;' nivel=" + oElem.indentacion.ToString() + " ");
                        //if (oElem.unidad != 0)
                        sbBF.Append("onclick='setFilaFija(this)' ");
                        sbBF.Append("ondblclick='mdn(this)'>");
                        sbBF.Append("<td>");
                        if (oElem.indentacion < 3)
                            sbBF.Append("<IMG class='N" + oElem.indentacion.ToString() + "' onclick=mostrar(this,true) src='../../../../images/plus.gif' style='cursor:pointer;'>");
                        else
                            sbBF.Append("<IMG class='N" + oElem.indentacion.ToString() + "' src='../../../../images/imgSeparador.gif'>");
                    }

                    switch (oElem.indentacion)
                    {
                        case 1:
                            sbBF.Append("<IMG src='../../../../images/Unidad.gif' style='margin-left:3px;margin-right:3px;'>");
                            break;
                        case 2:
                            sbBF.Append("<IMG src='../../../../images/Area.gif' style='margin-left:3px;margin-right:3px;'>");
                            break;
                        case 3:
                            sbBF.Append("<IMG src='../../../../images/Subarea.gif' style='margin-left:3px;margin-right:3px;'>");
                            break;
                    }

                    sbBF.Append("<label class='NBR ");
                    switch (oElem.indentacion)
                    {
                        case 1: sbBF.Append("W455"); break;
                        case 2: sbBF.Append("W440"); break;
                        case 3: sbBF.Append("W425"); break;
                    }
                    sbBF.Append("' onclick='marcarLabel(this)' style='cursor:url(../../../../images/imgManoAzul2.cur),pointer;color:" + sColor + ";'>" + oElem.denominacion + "</label></td>");
                    sbBF.Append("</tr>");
                    #endregion
                }
            }
            sbBF.Append("</tbody></table>");
            
            return "OK@#@" +
                "<div style=\"background-image: url('../../../../Images/imgFT20.gif'); background-repeat:repeat; width:500px; height:auto;\">" + sbBF.ToString() + "</div>";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la estructura preventa", ex);
        }
        finally
        {
            oEstructura.Dispose();
        }
    }
    private string Eliminar(int nNivel, int nIDItem)
    {
        IB.SUPER.ADM.SIC.BLL.UnidadPreventa oUnidad = new IB.SUPER.ADM.SIC.BLL.UnidadPreventa();
        IB.SUPER.ADM.SIC.BLL.AreaPreventa oArea = new IB.SUPER.ADM.SIC.BLL.AreaPreventa();
        IB.SUPER.ADM.SIC.BLL.SubareaPreventa oSubArea = new IB.SUPER.ADM.SIC.BLL.SubareaPreventa();
        try
        {
            switch (nNivel)
            {
                case 1: 
                    oUnidad.Delete((short)nIDItem);
                    oUnidad.Dispose();
                    break;
                case 2:
                    oArea.Delete(nIDItem);
                    oArea.Dispose();
                    break;
                case 3: 
                    oSubArea.Delete(nIDItem);
                    oSubArea.Dispose();
                    break;
            }

            return "OK@#@";
        }
        catch (Exception ex)
        {
            if (Errores.EsErrorIntegridad(ex)) 
                return "Error@#@Operación rechazada.\n\n" + Errores.mostrarError("Error al obtener la estructura preventa", ex);
            else 
                return "Error@#@" + Errores.mostrarError("Error al obtener la estructura preventa", ex);
        }
        finally
        {
            switch (nNivel)
            {
                case 1:
                    oUnidad.Dispose();
                    break;
                case 2:
                    oArea.Dispose();
                    break;
                case 3:
                    oSubArea.Dispose();
                    break;
            }
        }
    }
}