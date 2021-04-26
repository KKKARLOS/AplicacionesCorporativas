using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SUPER.Capa_Negocio;
using System.Text;
using System.Text.RegularExpressions;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores, strHTMLCaso = "", sIdDialogo = "";
    public string sEsGestorDialogo = "false", sEsInterlocutorProyecto = "false", gsEsAdm = "false";
    public SqlConnection oConn;
    public SqlTransaction tr;

    protected void Page_Load(object sender, EventArgs e) 
    {
        try
        {
            if (!Page.IsCallback)
            {
                if (Session["IDRED"] == null)
                {
                    try
                    {
                        Response.Redirect("~/SesionCaducadaModal.aspx", true);
                    }
                    catch (System.Threading.ThreadAbortException) { return; }
                }
                if (Utilidades.EsAdminProduccionEntrada())
                    gsEsAdm = "true";

                if (Request.QueryString["id"] != null){
                    sIdDialogo = Utilidades.decodpar(Request.QueryString["id"].ToString());
                    hdnIdDialogo.Value = sIdDialogo;
                    if (DIALOGOALERTAS.EsGestorDeDialogoAlerta(int.Parse(sIdDialogo), int.Parse(Session["UsuarioActual"].ToString())))
                        sEsGestorDialogo = "true";
                }

                if (sIdDialogo != "")
                {
                    if (DIALOGOALERTAS.NumDocs(int.Parse(sIdDialogo)) > 0)
                    {
                        imgDocFact.Src = Session["strServer"].ToString() + "Images/imgCarpetaDoc32.png";
                        imgDocFact.Attributes.Add("title", "Existen documentos asociados");
                        hdnHayDocs.Value = "S";
                    }

                    string[] aTabla = Regex.Split(getDatosDialogo(false, int.Parse(sIdDialogo), (sEsGestorDialogo == "true") ? true : false), "@#@");
                    if (aTabla[0] == "OK")
                    {
                        this.strHTMLCaso = aTabla[1];
                    }
                    else sErrores += Errores.mostrarError(aTabla[1]);

                    if (hdnIdDAlerta.Value == "0" && sEsGestorDialogo == "true")
                    {
                        txtAsunto.Style.Add("display", "none");
                        cboAsunto.Style.Add("display", "inline-block");
                        CargarAlertas();
                        cboAsunto.SelectedValue = hdnIdDAlerta.Value;
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
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al cargar la página ", ex);
        }
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        string[] aArgs = Regex.Split(eventArg, @"@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        switch (aArgs[0])
        {
            case ("getDatosEstado"):
                sResultado += getDatosEstado(int.Parse(aArgs[1]));
                break;
            case ("getDialogo"):
                sResultado += getDatosDialogo(true, int.Parse(aArgs[1]), false);
                break;
            case ("grabar"):
                sResultado += grabar(aArgs[1], aArgs[2], aArgs[3]);
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
    public string getDatosDialogo(bool bSoloTextos, int nIdDialogo, bool bEsGestor)
    {
        string sResultado = "";
        try
        {
            DataSet ds = DIALOGOALERTAS.ObtenerDialogoAlerta(nIdDialogo, int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString()), bEsGestor);
            #region Datos cabecera del diálogo
            if (!bSoloTextos)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow oFila = ds.Tables[0].Rows[0];
                    txtIdDialogo.Text = ((int)oFila["t831_iddialogoalerta"]).ToString("#,###");
                    if (oFila["t831_flr"] != DBNull.Value)
                        txtFLR.Text = ((DateTime)oFila["t831_flr"]).ToShortDateString();
                    txtProyecto.Text = ((int)oFila["t301_idproyecto"]).ToString("#,###") +" - " + oFila["t301_denominacion"].ToString();

                    string sTooltip = "<label style=width:70px;>Proyecto:</label>" + ((int)oFila["t301_idproyecto"]).ToString("#,###") + " - " + oFila["t301_denominacion"].ToString().Replace("\"", "&#34;").Replace(((char)39).ToString(), "&#39;"); //(char)34, (char)39
                    sTooltip += "<br><label style=width:70px;>Responsable:</label>" + oFila["Responsable"].ToString().Replace("\"", "&#34;");
                    sTooltip += "<br><label style=width:70px;>Interlocutor:</label>" + oFila["Interlocutor"].ToString().Replace("\"", "&#34;");
                    sTooltip += "<br><label style=width:70px;>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + oFila["t303_denominacion"].ToString().Replace("\"", "&#34;").Replace(((char)39).ToString(), "&#39;");
                    sTooltip += "<br><label style=width:70px;>Cliente:</label>" + oFila["t302_denominacion"].ToString().Replace("\"", "&#34;").Replace(((char)39).ToString(), "&#39;");

                    txtProyecto.Attributes.Add("onmouseover", "showTTE('" + Utilidades.escape(sTooltip) + "');");
                    txtProyecto.Attributes.Add("onmouseout", "hideTTE();");
                    

                    txtAsunto.Text = oFila["t820_denominacion"].ToString();
                    txtEstado.Text = oFila["t827_denominacion"].ToString();
                    txtEstado.ToolTip = oFila["t827_descripcion"].ToString();
                    hdnIdEstado.Value = oFila["t827_idestadodialogoalerta"].ToString();
                    hdnEntePromotor.Value = oFila["t831_entepromotor"].ToString();
                    hdnIdResponsableProy.Value = oFila["t314_idusuario_responsable"].ToString();
                    txtMes.Text = (oFila["t831_anomesdecierre"] != DBNull.Value)? Fechas.AnnomesAFechaDescLarga((int)oFila["t831_anomesdecierre"]) :"";
                    hdnIdDAlerta.Value = oFila["t820_idalerta"].ToString(); 
                    //if (oFila["t314_idusuario_responsable"].ToString() == Session["UsuarioActual"].ToString())
                    //    sEsResponsableProyecto = "true";
                    if ((bool)oFila["bInterlocutor"])
                        sEsInterlocutorProyecto = "true";
                }
            }
            #endregion

            #region Textos del diálogo
            StringBuilder sb = new StringBuilder();
            DateTime dtAux = DateTime.Parse("01/01/1900");
            string sCellWidth = "";

            sb.Append("<table id='tblTextos' class='texto' style='width:880px; background-color:#ece4de' cellspacing='0' cellpadding='0' border='0'>");
            sb.Append(@"<colgroup>
                            <col style='width:60px;' />
                            <col style='width:160px;' />
                            <col style='width:220px;' />
                            <col style='width:220px;' />
                            <col style='width:160px;' />
                            <col style='width:60px;' />
                        </colgroup>");

            foreach (DataRow oFila in ds.Tables[1].Rows){
                sCellWidth = (oFila["t832_texto"].ToString().Length > 40) ? "455px" : (oFila["t832_texto"].ToString().Length * 11).ToString() + "px";

                if (((DateTime)oFila["t832_fechacreacion"]).ToShortDateString() != dtAux.ToShortDateString())
                {
                    dtAux = (DateTime)oFila["t832_fechacreacion"];
                    sb.Append("<tr>");
                    sb.Append("<td colspan='6' style='padding-top:4px;'>");
                    sb.Append("<div style='margin-left:270px; width:300px; height:34px; background-image:url(../../../../Images/imgFondoFecha.gif);background-repeat:no-repeat; text-align:center; padding-top:8px; font-size:13pt;'>" + ((DateTime)oFila["t832_fechacreacion"]).ToLongDateString() + "</div>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                }

                sb.Append("<tr>");
                if (oFila["t832_posicion"].ToString() == "I")
                {
                    sb.Append("<td style='vertical-align:bottom; padding-bottom: 10px;'>");
                    
                    if (oFila["t001_idficepi"] == DBNull.Value)
                        sb.Append(@"<img src='../../../../Images/imgDiamanteDialogo.png' style='width:60px; height:90px;' border='0' />");
                    else
                        sb.Append(@"<img src='" + (((int)oFila["tiene_foto"] == 0) ? "../../../../Images/imgSmile.gif" : "../../../Inicio/ObtenerFotoByFicepi.aspx?nF=" + oFila["t001_idficepi"].ToString()) + @"' style='width:60px; height:auto;' border='0' />");
                    sb.Append(@"<nobr class='NBR W60' style='text-align:center' title='" + oFila["Profesional"].ToString() + @"'>" + oFila["t001_nombre"].ToString() + @"</nobr>");

                    sb.Append("</td>");

                    sb.Append("<td colspan='3' style='padding-left:5px;'>");
                    sb.Append(@"<table class='texto' style='float:left; margin-bottom:20px;' cellspacing='0' cellpadding='0' border='0'>
                            <tr style='height:9px;'>
                                <td background='../../../../Images/Tabla/ConvIzda/1.gif' style='width:34px;'></td>
                                <td background='../../../../Images/Tabla/ConvIzda/2.gif' style='width:" + sCellWidth + @";'></td>
                                <td background='../../../../Images/Tabla/ConvIzda/3.gif' style='width:81px; height:9px;background-repeat:no-repeat;'></td>
                            </tr>
                            <tr>
                                <td background='../../../../Images/Tabla/ConvIzda/4.gif' style='width:34px;'>&nbsp;</td>
                                <td background='../../../../Images/Tabla/ConvIzda/5.gif' style='font-size:11pt; width:" + sCellWidth + @";'>
                                    <!-- Inicio del contenido propio de la tabla -->
                                    " + oFila["t832_texto"].ToString().Replace(((char)10).ToString(), "<br>") + @"
                                    <!-- Fin del contenido propio de la tabla -->
                                </td>
                                <td background='../../../../Images/Tabla/ConvIzda/6.gif' style='width:81px;background-repeat:repeat-y;'></td>
                            </tr>
                            <tr style='height:55px;'>
                                <td background='../../../../Images/Tabla/ConvIzda/7.gif' style='width:34px;'></td>
                                <td background='../../../../Images/Tabla/ConvIzda/8.gif' style='width:" + sCellWidth + @";'></td>
                                <td background='../../../../Images/Tabla/ConvIzda/9.gif' style='width:81px; font-size:13pt; vertical-align:top; padding-left:15px; padding-top:10px; background-repeat:no-repeat;'>" + Fechas.HoraACadenaLarga((DateTime)oFila["t832_fechacreacion"]) + @"</td>
                            </tr>
                        </table>");
                    sb.Append("</td>");
                    sb.Append("<td colspan='2'></td>");
                }
                else //lado derecho
                {
                    sb.Append("<td colspan='2'></td>");
                    sb.Append("<td colspan='3' style='text-align:right; padding-right:5px;'>");// vertical-align: text-bottom;
                    //sb.Append("<img src='../../../../Images/imgW2.gif' />");
                    
                    sb.Append(@"<table class='texto' style='table-layout:fixed; float:right; margin-bottom:20px;' cellspacing='0' cellpadding='0' border='0'>
                        <tr style='height:11px;'>
                            <td background='../../../../Images/Tabla/ConvDcha/1.gif' style='width:80px;'></td>
                            <td background='../../../../Images/Tabla/ConvDcha/2.gif' style='width:" + sCellWidth + @";'></td>
                            <td background='../../../../Images/Tabla/ConvDcha/3.gif' style='width:37px;'></td>
                        </tr>
                        <tr>
                            <td background='../../../../Images/Tabla/ConvDcha/4.gif' style='width:80px;'>&nbsp;</td>
                            <td background='../../../../Images/Tabla/ConvDcha/5.gif' style='font-size:11pt; text-align: left;width:" + sCellWidth + @";'>
                                <!-- Inicio del contenido propio de la tabla -->" +
                                    oFila["t832_texto"].ToString().Replace(((char)10).ToString(), "<br>") + @"
                                <!-- Fin del contenido propio de la tabla -->
                            </td>
                            <td background='../../../../Images/Tabla/ConvDcha/6.gif' style='width:37px;'></td>
                        </tr>
                        <tr style='height:54px;'>
                            <td background='../../../../Images/Tabla/ConvDcha/7.gif' 
                                style='width:80px; font-size:13pt; vertical-align:top; padding-top:10px;'>
                                <span style='margin-right:15px;'>" + Fechas.HoraACadenaLarga((DateTime)oFila["t832_fechacreacion"]) + @"</span></td>
                            <td background='../../../../Images/Tabla/ConvDcha/8.gif' style='width:" + sCellWidth + @";'></td>
                            <td background='../../../../Images/Tabla/ConvDcha/9.gif' style='width:37px;'></td>
                        </tr>
                        </table>");

                    sb.Append("</td>");
                    sb.Append("<td style='vertical-align:bottom; padding-bottom: 10px;'>");
                    
                    if (oFila["t001_idficepi"] == DBNull.Value)
                        sb.Append(@"<img src='../../../../Images/imgDiamanteDialogo.png' style='width:60px; height:90px;' border='0' />");
                    else
                        sb.Append(@"<img src='" + (((int)oFila["tiene_foto"] == 0) ? "../../../../Images/imgSmile.gif" : "../../../Inicio/ObtenerFotoByFicepi.aspx?nF=" + oFila["t001_idficepi"].ToString()) + @"' style='width:60px; height:auto;' border='0' />");
                    sb.Append(@"<nobr class='NBR W60' style='text-align:center' title='" + oFila["Profesional"].ToString() + @"'>" + oFila["t001_nombre"].ToString() + @"</nobr>");
                    sb.Append("</td>");
                }
                sb.Append("</tr>");
            }
            ds.Dispose();
            sb.Append("</table>");

            sResultado = "OK@#@" + sb.ToString();

            #endregion

        }
        catch (Exception ex)
        {
            sResultado = "Error@#@" + Errores.mostrarError("Error al obtener los datos del diálogo.", ex);
        }
        return sResultado;
    }
    public string getDatosEstado(int nIdDialogo)
    {
        string sResultado = "";
        try
        {
            DIALOGOALERTAS oDA = DIALOGOALERTAS.ObtenerDatosDialogoAlerta(nIdDialogo);
            sResultado = "OK@#@" + oDA.t827_idestadodialogoalerta + "@#@" + Utilidades.escape(oDA.t827_denominacion) + "@#@" + Utilidades.escape(oDA.t827_descripcion);
        }
        catch (Exception ex)
        {
            sResultado = "Error@#@" + Errores.mostrarError("Error al obtener los datos del diálogo.", ex);
        }
        return sResultado;
    }
    protected void CargarAlertas()
    {
        SqlDataReader dr = SUPER.Capa_Datos.DIALOGOALERTAS.ObtenerCatalogoAlertas(null, null);
        while (dr.Read())
        {
            cboAsunto.Items.Add(new ListItem(dr["t820_denominacion"].ToString(), dr["t820_idalerta"].ToString()));
        }
        dr.Close();
        dr.Dispose();
    }
    public string grabar(string sIdDialogo, string sIdAlerta, string sFLR)
    {
        try
        {
            DIALOGOALERTAS.ActualizarFechaTipo(int.Parse(sIdDialogo), byte.Parse(sIdAlerta), ((sFLR=="")? null: (DateTime?)DateTime.Parse(sFLR)) );
            return "OK@#@";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al actualizar los datos del diálogo.", ex);
        }
    }
}
