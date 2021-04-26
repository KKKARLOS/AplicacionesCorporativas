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
using SUPER.Capa_Negocio;

public partial class Capa_Presentacion_Administracion_EstructuraOrg_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public int nNE = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                Master.nBotonera = 35;
                Master.bFuncionesLocales = true;
                Master.bEstilosLocales = true;
                Master.TituloPagina = "Estructura organizativa";
                Master.FuncionesJavaScript.Add("Javascript/dhtmltooltip.js");

                nNE = int.Parse(Request.QueryString["nNE"].ToString());
                string strTabla = GenerarArbol(false, nNE);
                string[] aTabla = Regex.Split(strTabla, "@#@");
                if (aTabla[0] != "Error")
                {
                    string[] aTablasDatos = Regex.Split(aTabla[1], "{{septabla}}");

                    divBodyFijo.InnerHtml = aTablasDatos[0];
                    divTituloMovil.InnerHtml = aTablasDatos[1];
                    divBodyMovil.InnerHtml = aTablasDatos[2];
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
            case "setAlerta":
                sResultado += setAlerta(int.Parse(aArgs[1]), int.Parse(aArgs[2]), byte.Parse(aArgs[3]), (aArgs[4] == "1") ? true : false);
                break;
            case "setTrasladarAlertas":
                sResultado += setTrasladarAlertas(byte.Parse(aArgs[1]), byte.Parse(aArgs[2]), int.Parse(aArgs[3]));
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
        try
        {
            StringBuilder sbTM = new StringBuilder();
            StringBuilder sbTMF1 = new StringBuilder();
            StringBuilder sbBF = new StringBuilder();
            StringBuilder sbBM = new StringBuilder();

            bool bColgroupCreado = false;
            int nWidthBM = 0;
            int nID = 0;

            DataSet ds = Estructura.GetEstructuraOrganizativa_DS(bMostrarInactivos, true);
            //StringBuilder sb = new StringBuilder();
            string sColor = "black";
            string sDisplay = "";

            foreach (DataRow oFila in ds.Tables[0].Rows)
            {
                if (!bColgroupCreado)
                {
                    bColgroupCreado = true;

                    #region tblTituloMovil
                    nWidthBM = ds.Tables[1].Rows.Count * 40;
                    sbTM.Append("<table id='tblTituloMovil' class='TBLINI' style='width:" + nWidthBM.ToString() + "px; display:block;' cellpadding='0' cellspacing='0' border='1'>");
                    sbTM.Append("    <colgroup>");
                    #endregion

                    #region tblBodyFijo
                    sbBF.Append("<table id='tblBodyFijo' style='width:500px; cursor:url(../../../images/imgManoAzul2.cur),pointer;' cellpadding='0' cellspacing='0' border='0'>");
                    sbBF.Append("<colgroup>");
                    sbBF.Append("   <col style='width:500px;' />");
                    sbBF.Append("</colgroup>");
                    sbBF.Append("<tbody>");

                    #endregion

                    #region tblBodyMovil
                    sbBM.Append("<table id='tblBodyMovil' style='width:" + nWidthBM.ToString() + "px;' cellpadding='0' cellspacing='0' border='0'>");
                    sbBM.Append("    <colgroup>");
                    #endregion

                    #region Creacion Colgroups Móviles
                    sbTMF1.Append("<tr style='height:17px;'>");

                    foreach (DataRow oFilaAlerta in ds.Tables[1].Rows)//Tabla de ALERTAS
                    {
                        sbTM.Append("   <col style='width:40px;' />");
                        //sbTMF1.Append("   <td style='width:40px;text-align:center; background-color:red;' onmouseover='showTTE(\"" + Utilidades.escape("<label style=width:50px>Grupo:</label>" + oFilaAlerta["t821_denominacion"].ToString() + "<br/><label style=width:50px>Asunto:</label>" + oFilaAlerta["t820_denominacion"].ToString()) + "\")' onMouseout=\"hideTTE()\">A" + oFilaAlerta["t820_idalerta"].ToString() + "</td>");
                        string sTooltip = Utilidades.escape("<label style='width:50px'>Grupo:</label>" + oFilaAlerta["t821_denominacion"].ToString() + "<br/><label style=width:50px>Asunto:</label>" + oFilaAlerta["t820_denominacion"].ToString());
                        sbTMF1.Append("   <td style='width:40px;text-align:center;' onmouseover=showTTE(\"" + sTooltip + "\",null,null,300) onMouseout=\"hideTTE()\">A" + oFilaAlerta["t820_idalerta"].ToString() + "</td>");

                        sbBM.Append("   <col style='width:40px;' />");
                    }

                    sbTM.Append("</colgroup>");

                    sbTMF1.Append("</tr>");
                    sbTM.Append(sbTMF1.ToString());
                    sbTM.Append("</table>");

                    sbBM.Append("</colgroup>");

                    #endregion
                }

                #region tblBodyFijo
                sColor = "black";
                if (!(bool)oFila["ESTADO"]) sColor = "gray";
                if ((int)oFila["INDENTACION"] <= nNivelExp)
                {
                    sbBF.Append("<tr id='" + oFila["SN4"].ToString() + "-" + oFila["SN3"].ToString() + "-" + oFila["SN2"].ToString() + "-" + oFila["SN1"].ToString() + "-" + oFila["NODO"].ToString() + "-" + oFila["SUBNODO"].ToString() + "' ");
                    sbBF.Append("SN4='" + oFila["SN4"].ToString() + "' ");
                    sbBF.Append(" style='DISPLAY: table-row; HEIGHT: 20px; vertical-align:middle;' nivel=" + oFila["INDENTACION"].ToString() + " ");
                    if (oFila["SN4"].ToString() != "0")
                        sbBF.Append("onclick='setFilaFija(this)' ");
                    sbBF.Append("ondblclick='mdn(this)'>");
                    sDisplay = "table-row";
                    if ((int)oFila["INDENTACION"] < 6)
                    {
                        if ((int)oFila["INDENTACION"] < nNivelExp) sbBF.Append("<td><IMG class='N" + oFila["INDENTACION"].ToString() + "' onclick=mostrar(this,true) src='../../../images/minus.gif' style='cursor:pointer;'>");
                        else sbBF.Append("<td><IMG class='N" + oFila["INDENTACION"].ToString() + "' onclick=mostrar(this,true) src='../../../images/plus.gif' style='cursor:pointer;'>");
                    }
                    else sbBF.Append("<td><IMG class='N" + oFila["INDENTACION"].ToString() + "' src='../../../images/imgSeparador.gif'>");
                }
                else
                {
                    sbBF.Append("<tr id='" + oFila["SN4"].ToString() + "-" + oFila["SN3"].ToString() + "-" + oFila["SN2"].ToString() + "-" + oFila["SN1"].ToString() + "-" + oFila["NODO"].ToString() + "-" + oFila["SUBNODO"].ToString() + "' ");
                    sbBF.Append("SN4='" + oFila["SN4"].ToString() + "' ");
                    sbBF.Append(" style='DISPLAY: none; HEIGHT: 20px; vertical-align:middle;' nivel=" + oFila["INDENTACION"].ToString() + " ");
                    if (oFila["SN4"].ToString() != "0")
                        sbBF.Append("onclick='setFilaFija(this)' ");
                    sbBF.Append("ondblclick='mdn(this)'>");
                    sDisplay = "none";
                    sbBF.Append("<td>");
                    if ((int)oFila["INDENTACION"] < 6) sbBF.Append("<IMG class='N" + oFila["INDENTACION"].ToString() + "' onclick=mostrar(this,true) src='../../../images/plus.gif' style='cursor:pointer;'>");
                    else sbBF.Append("<IMG class='N" + oFila["INDENTACION"].ToString() + "' src='../../../images/imgSeparador.gif'>");
                }

                //if ((int)oFila["INDENTACION"] < 5) sbBF.Append("<IMG src='../../../images/imgSN" + oFila["sufijoimg"].ToString() + ".gif' style='margin-left:3px;margin-right:3px;'>");
                //else sbBF.Append("<IMG src='../../../images/imgNodo.gif' style='margin-left:3px;margin-right:3px;'>");
                switch ((int)oFila["INDENTACION"])
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        sbBF.Append("<IMG src='../../../images/imgSN" + oFila["sufijoimg"].ToString() + ".gif' style='margin-left:3px;margin-right:3px;'>");
                        break;
                    case 5:
                        sbBF.Append("<IMG src='../../../images/imgNodo.gif' style='margin-left:3px;margin-right:3px;'>");
                        break;
                    case 6:
                        sbBF.Append("<IMG src='../../../images/imgSubNodo.gif' style='margin-left:3px;margin-right:3px;'>");
                        break;
                }

                sbBF.Append("<label class='NBR ");
                switch ((int)oFila["INDENTACION"])
                {
                    case 1: sbBF.Append("W455"); break;
                    case 2: sbBF.Append("W440"); break;
                    case 3: sbBF.Append("W425"); break;
                    case 4: sbBF.Append("W410"); break;
                    case 5: sbBF.Append("W395"); break;
                    case 6: sbBF.Append("W380"); break;
                }
                sbBF.Append("' onclick='marcarLabel(this)' style='cursor:url(../../../images/imgManoAzul2.cur),pointer;color:" + sColor + ";'>" + oFila["DENOMINACION"].ToString() + "</label></td>");
                sbBF.Append("</tr>");
                #endregion

                #region tblBodyMovil
                //sbBM.Append("<tr style='height:20px;'>");
                nID++;
                sbBM.Append("<tr id='" + nID.ToString() + "' style='height:20px; display: " + sDisplay + "; cursor:" + ((oFila["SN4"].ToString()!="0") ? "pointer" : "default") + ";' ");
                if (oFila["SN4"].ToString() != "0")
                    sbBM.Append("onclick='setFilaMovil(this)' ");
                sbBM.Append("nivel='" + oFila["indentacion"].ToString() + "' ");
                sbBM.Append("SN4='" + oFila["SN4"].ToString() + "' ");
                sbBM.Append("SN3='" + oFila["SN3"].ToString() + "' ");
                sbBM.Append("SN2='" + oFila["SN2"].ToString() + "' ");
                sbBM.Append("SN1='" + oFila["SN1"].ToString() + "' ");
                sbBM.Append("NODO='" + oFila["NODO"].ToString() + "' ");
                sbBM.Append(">");


                foreach (DataRow oFilaAlerta in ds.Tables[1].Rows)
                {
                    sbBM.Append("<td style='width:40px; text-align:center;'>");
                    if (oFila["SN4"].ToString() == "0") continue;

                    if ((int)oFila["indentacion"] < 6)
                    {
                        if ((int)oFila["indentacion"] == 5)
                        {
                            string sTooltip = "";
                            if (oFila["param1_alerta_" + oFilaAlerta["t820_idalerta"].ToString()] != DBNull.Value)
                                sTooltip += "<label style='width:auto'>" + oFilaAlerta["t820_parametro1"].ToString() + ":&nbsp;&nbsp;</label>" + oFila["param1_alerta_" + oFilaAlerta["t820_idalerta"].ToString()].ToString();
                            if (oFila["param2_alerta_" + oFilaAlerta["t820_idalerta"].ToString()] != DBNull.Value)
                                sTooltip += "<br><label style='width:auto'>" + oFilaAlerta["t820_parametro2"].ToString() + ":&nbsp;&nbsp;</label>" + oFila["param2_alerta_" + oFilaAlerta["t820_idalerta"].ToString()].ToString();
                            if (oFila["param3_alerta_" + oFilaAlerta["t820_idalerta"].ToString()] != DBNull.Value)
                                sTooltip += "<br><label style='width:auto'>" + oFilaAlerta["t820_parametro3"].ToString() + ":&nbsp;&nbsp;</label>" + oFila["param3_alerta_" + oFilaAlerta["t820_idalerta"].ToString()].ToString();
                            if (sTooltip != "")
                                sbBM.Append("<input type='checkbox' disabled='true' style='cursor:pointer;' alerta='" + oFilaAlerta["t820_idalerta"].ToString() + "' onclick=\"setAlerta(this)\" onmouseover=showTTE(\"" + Utilidades.escape(sTooltip) + "\",null,null,300) onMouseout=\"hideTTE()\" ");
                            else
                                sbBM.Append("<input type='checkbox' disabled='true' style='cursor:pointer;' alerta='" + oFilaAlerta["t820_idalerta"].ToString() + "' onclick=\"setAlerta(this)\" ");
                        }
                        else
                        {
                            sbBM.Append("<input type='checkbox' disabled='true' style='cursor:pointer;' alerta='" + oFilaAlerta["t820_idalerta"].ToString() + "' onclick=\"setAlerta(this)\" ");
                        }
                        //    Utilidades.escape("<label style='width:50px'>" + oFilaAlerta["t820_parametro1"].ToString()+":</label>" + oFilaAlerta["t821_denominacion"].ToString() + "<br/><label style=width:50px>Asunto:</label>" + oFilaAlerta["t820_denominacion"].ToString());
                        //sbTMF1.Append("   <td style='width:40px;text-align:center;' onmouseover=showTTE(\"" + sTooltip + "\",null,null,300) onMouseout=\"hideTTE()\">A" + oFilaAlerta["t820_idalerta"].ToString() + "</td>");

                        if ((bool)oFila["alerta_" + oFilaAlerta["t820_idalerta"].ToString()])
                            sbBM.Append(" checked");
                        sbBM.Append(" />");
                    }
                    sbBM.Append("</td>");
                }

                sbBM.Append("</tr>");
                #endregion
            }
            //dr.Close();
            ds.Dispose();
            sbBF.Append("</tbody>");
            sbBF.Append("</table>");
            sbBM.Append("</table>");

            return "OK@#@" + "<div style=\"background-image: url('../../../Images/imgFT20.gif'); background-repeat:repeat; width:500px; height:auto;\">" + sbBF.ToString() +"</div>{{septabla}}"
                    + sbTM.ToString() + "{{septabla}}"
                    + "<div style=\"background-image: url('../../../Images/imgFT20.gif'); background-repeat:repeat; width:" + nWidthBM.ToString() + "px; height:auto;\">" + sbBM.ToString() + "</div>" + "{{septabla}}";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la estructura organizativa", ex);
        }
    }
    private string Eliminar(int nNivel, int nIDItem)
    {
        try
        {
            switch (nNivel)
            {
                case 1: SUPERNODO4.Delete(null, nIDItem); break;
                case 2: SUPERNODO3.Delete(null, nIDItem); break;
                case 3: SUPERNODO2.Delete(null, nIDItem); break;
                case 4: SUPERNODO1.Delete(null, nIDItem); break;
                case 5: NODO.Delete(null, nIDItem); break;
                case 6: SUBNODO.Delete(null, nIDItem); break;
            }

            return "OK@#@";
        }
        catch (Exception ex)
        {
            //return "Error@#@" + Errores.mostrarError("Error al obtener la estructura organizativa", ex);
            if (Errores.EsErrorIntegridad(ex)) return "Error@#@Operación rechazada.\n\n" + Errores.mostrarError("Error al obtener la estructura organizativa", ex); //ex.Message;
            else return "Error@#@" + Errores.mostrarError("Error al obtener la estructura organizativa", ex);
        }
    }
    private string setAlerta(int nNivel, int nCodigo, byte nAlerta, bool bHabilitada)
    {
        try
        {
            PSNALERTAS.EstablecerAlertaEstructura(nNivel, nCodigo, nAlerta, bHabilitada);

            return "OK@#@";
        }
        catch (Exception ex)
        {
             return "Error@#@" + Errores.mostrarError("Error al habilitar/deshabilitar la alerta.", ex);
        }
    }
    private string setTrasladarAlertas(byte nOpcion, byte nNivel, int nCodigo)
    {
        try
        {
            PSNALERTAS.TrasladarAlertaEstructura(nOpcion, nNivel, nCodigo);

            return "OK@#@";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al trasladar las alertas.", ex);
        }
    }

}
