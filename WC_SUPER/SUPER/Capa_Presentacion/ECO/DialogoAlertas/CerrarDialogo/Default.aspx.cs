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
    public string sErrores, strHTMLCaso = "", sIdDialogo = "", strTablaHTMLAlertas = "", strTablaHTMLOtrosDialogos = "", sTooltipInfoProy = "";
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

                if (Request.QueryString["id"] != null){
                    sIdDialogo = Utilidades.decodpar(Request.QueryString["id"].ToString());
                    hdnIdDialogo.Value = sIdDialogo;
                }else
                    throw (new Exception("No se ha podido determinar el diálogo a cerrar."));

                ObtenerAlertas();
                
                ObtenerOtrosDialogosAbiertos();

                getDatosEstadoCerrado(int.Parse(hdnIdDialogo.Value));

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
            case ("cerrarDialogo"):
                sResultado += DIALOGOALERTAS.CerrarDialogo(aArgs[1], aArgs[2], aArgs[3], aArgs[4]);
                break;
            //case ("borrarAlertas"):
            //    sResultado += BorrarAlertas(aArgs[1], aArgs[2]);
            //    break;
        }
        //3º Damos contenido a la variable que se envía de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }

    public void ObtenerAlertas()
    {
        StringBuilder sb = new StringBuilder();

        #region Cabecera tabla HTML
        sb.Append(@"<table id='tblOtrasAlertas' style='width:500px;' border='1'>
            <colgroup>
                <col style='width: 215px;' />
                <col style='width: 25px;' />
                <col style='width: 100px;' />
                <col style='width: 120px;' />
                <col style='width: 40px;' />
            </colgroup>");
        #endregion

        DataSet ds = PSNALERTAS.ObtenerCatalogoByDialogo(int.Parse(hdnIdDialogo.Value), int.Parse(Session["UsuarioActual"].ToString()));
        int i = 0;
        foreach (DataRow oFila in ds.Tables[0].Rows)
        {
            if (i == 0)
            {
                txtAsunto.Text = oFila["t820_denominacion"].ToString();
                chkHabilitada.Checked = (bool)oFila["t830_habilitada"];
                txtIniStby.Text = (oFila["t830_inistandby"] != DBNull.Value) ? Fechas.AnnomesAFechaDescLarga((int)oFila["t830_inistandby"]) : "";
                txtFinStby.Text = (oFila["t830_finstandby"] != DBNull.Value) ? Fechas.AnnomesAFechaDescLarga((int)oFila["t830_finstandby"]) : "";
                hdnGrupoOC.Value = oFila["grupoOC"].ToString();
                this.hdnTipoOCFA.Value = oFila["t820_tipo"].ToString();

                ObtenerMotivos(this.hdnTipoOCFA.Value);

                this.hdnMotivoOCFA.Value = oFila["t840_idmotivo"].ToString();
                if (this.hdnMotivoOCFA.Value == "" && (hdnGrupoOC.Value == "1"))
                {
                    //Si el dialogo es de OC o FA y no tiene asignado motivo busco si existe un dialogo anterior cerrado del que heredar el motivo por defecto
                    this.hdnMotivoOCFA.Value = PSNALERTAS.MotivoCierreDefecto(null, int.Parse(oFila["t305_idproyectosubnodo"].ToString()), int.Parse(oFila["t820_idalerta"].ToString()));
                }
                cboMotivo.SelectedValue = this.hdnMotivoOCFA.Value;

                if ((bool)oFila["t830_habilitada"])
                {
                    txtIniStby.Style.Add("cursor", "pointer");
                    txtIniStby.Attributes.Add("onclick", "getPeriodo(this.parentNode.parentNode)");
                    txtFinStby.Style.Add("cursor", "pointer");
                    txtFinStby.Attributes.Add("onclick", "getPeriodo(this.parentNode.parentNode)");
                }
                chkSeguimiento.Checked = (oFila["t830_txtseguimiento"].ToString() != "") ? true : false;
                imgSegAlertaActual.Style.Add("visibility", (oFila["t830_txtseguimiento"].ToString() != "") ? "visible" : "hidden");

                trAlertaActual.Attributes.Add("idPSN", oFila["t305_idproyectosubnodo"].ToString());
                trAlertaActual.Attributes.Add("idAlerta", oFila["t820_idalerta"].ToString());
                trAlertaActual.Attributes.Add("inistandby", oFila["t830_inistandby"].ToString());
                trAlertaActual.Attributes.Add("finstandby", oFila["t830_finstandby"].ToString());
                trAlertaActual.Attributes.Add("seg", Utilidades.escape(oFila["t830_txtseguimiento"].ToString()));

                sTooltipInfoProy = "<label style=width:80px;>Proyecto:</label>" + ((int)ds.Tables[1].Rows[0]["t301_idproyecto"]).ToString("#,###") + " - " + ds.Tables[1].Rows[0]["t301_denominacion"].ToString();
                sTooltipInfoProy += "<br><label style=width:80px;>Responsable:</label>" + ds.Tables[1].Rows[0]["Responsable"].ToString();
                sTooltipInfoProy += "<br><label style=width:80px;>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + ds.Tables[1].Rows[0]["t303_denominacion"].ToString();
                sTooltipInfoProy += "<br><label style=width:80px;>Cliente:</label>" + ds.Tables[1].Rows[0]["t302_denominacion"].ToString();

                sTooltipInfoProy = Utilidades.escape(sTooltipInfoProy);
            }
            else
            {
                sb.Append("<tr bd='' idPSN='" + oFila["t305_idproyectosubnodo"].ToString() + "' ");
                sb.Append("idAlerta='" + oFila["t820_idalerta"].ToString() + "' ");
                sb.Append("a=" + (((bool)oFila["t830_habilitada"]) ? "1" : "0") + " ");
                sb.Append("inistandby='" + oFila["t830_inistandby"].ToString() + "' ");
                sb.Append("finstandby='" + oFila["t830_finstandby"].ToString() + "' ");
                sb.Append("seg=\"" + Utilidades.escape(oFila["t830_txtseguimiento"].ToString()) + "\">");

                sb.Append("<td style='text-align:left;'><nobr class='NBR W200' onmouseover='TTip(event)'>" + oFila["t820_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td><input type='checkbox' class='check' " + (((bool)oFila["t830_habilitada"]) ? "checked" : "") + " onclick='setHabilitada(this);' style='cursor:pointer;' /></td>");
                //if ((bool)oFila["t830_habilitada"])
                //{
                //    sb.Append("<td style='cursor:pointer;' onclick='getPeriodo(this.parentNode)'>" + ((oFila["t830_inistandby"] != DBNull.Value) ? Fechas.AnnomesAFechaDescLarga((int)oFila["t830_inistandby"]) : "") + "</td>");
                //    sb.Append("<td style='cursor:pointer;' onclick='getPeriodo(this.parentNode)'>" + ((oFila["t830_finstandby"] != DBNull.Value) ? Fechas.AnnomesAFechaDescLarga((int)oFila["t830_finstandby"]) : "") + "</td>");
                //}
                //else
                //{
                sb.Append("<td style='text-align:left;'></td>");
                sb.Append("<td style='text-align:left;'></td>");
                //}
                sb.Append("<td>");
                sb.Append("<input type='checkbox' class='check' " + ((oFila["t830_txtseguimiento"].ToString() != "") ? "checked" : "") + " onclick='setSeguimiento()' style='cursor:pointer;' />");
                sb.Append("<img src='../../../../images/imgSeguimiento.png' onclick='ModificarSeguimiento(this)' style='cursor:pointer;vertical-align:middle; margin-left:2px; border: 0px;visibility:" + ((oFila["t830_txtseguimiento"].ToString() != "") ? "visible" : "hidden") + "; width:16px; height:16px;' />");
                sb.Append("</td>");

                sb.Append("</tr>");
            }
            i++;
        }
        ds.Dispose();

        sb.Append("</table>");
        strTablaHTMLAlertas = sb.ToString();
    }
    public void ObtenerOtrosDialogosAbiertos()
    {
        strTablaHTMLOtrosDialogos = DIALOGOALERTAS.ObtenerOtrosDialogosAbiertos(int.Parse(hdnIdDialogo.Value), int.Parse(Session["UsuarioActual"].ToString()));
    }
//    public string ObtenerDatosDialogo(bool bSoloTextos, int nIdDialogo)
//    {
//        string sResultado = "";
//        try
//        {
//            DataSet ds = DIALOGOALERTAS.ObtenerDialogoAlerta(nIdDialogo, int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString()));
//            #region Datos cabecera del diálogo
//            if (!bSoloTextos)
//            {
//                if (ds.Tables[0].Rows.Count > 0)
//                {
//                    DataRow oFila = ds.Tables[0].Rows[0];
//                    txtIdDialogo.Text = ((int)oFila["t831_iddialogoalerta"]).ToString("#,###");
//                    if (oFila["t831_flr"] != DBNull.Value)
//                        txtFLR.Text = ((DateTime)oFila["t831_flr"]).ToShortDateString();
//                    txtProyecto.Text = ((int)oFila["t301_idproyecto"]).ToString("#,###") +" - " + oFila["t301_denominacion"].ToString();
//                    txtProyecto.Attributes.Add("title", "cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Proyecto:</label>" + ((int)oFila["t301_idproyecto"]).ToString("#,###") + " - " + oFila["t301_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Responsable:</label>" + oFila["responsable"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + oFila["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Cliente:</label>" + oFila["t302_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]");
//                    txtAsunto.Text = oFila["t820_denominacion"].ToString();
//                    txtEstado.Text = oFila["t827_denominacion"].ToString();
//                    txtEstado.ToolTip = oFila["t827_descripcion"].ToString();
//                    hdnIdEstado.Value = oFila["t827_idestadodialogoalerta"].ToString();
//                    hdnEntePromotor.Value = oFila["t831_entepromotor"].ToString();
//                    hdnIdResponsableProy.Value = oFila["t314_idusuario_responsable"].ToString();

//                    if (oFila["t314_idusuario_responsable"].ToString() == Session["UsuarioActual"].ToString())
//                        sEsResponsableProyecto = "true";
//                }
//            }
//            #endregion

//            #region Textos del diálogo
//            StringBuilder sb = new StringBuilder();
//            DateTime dtAux = DateTime.Parse("01/01/1900");
//            string sCellWidth = "";

//            sb.Append("<table id='tblTextos' class='texto' style='width:880px; background-color:#ece4de' cellspacing='0' cellpadding='0' border='0'>");
//            sb.Append(@"<colgroup>
//                            <col style='width:60px;' />
//                            <col style='width:160px;' />
//                            <col style='width:220px;' />
//                            <col style='width:220px;' />
//                            <col style='width:160px;' />
//                            <col style='width:60px;' />
//                        </colgroup>");

//                foreach (DataRow oFila in ds.Tables[1].Rows){
//                    sCellWidth = (oFila["t832_texto"].ToString().Length > 40) ? "455px" : (oFila["t832_texto"].ToString().Length * 11).ToString() + "px";

//                    if (((DateTime)oFila["t832_fechacreacion"]).ToShortDateString() != dtAux.ToShortDateString())
//                    {
//                        dtAux = (DateTime)oFila["t832_fechacreacion"];
//                        sb.Append("<tr>");
//                        sb.Append("<td colspan='6' style='padding-top:4px;'>");
//                        sb.Append("<div style='margin-left:270px; width:300px; height:34px; background-image:url(../../../../Images/imgFondoFecha.gif);background-repeat:no-repeat; text-align:center; padding-top:8px; font-size:13pt;'>" + ((DateTime)oFila["t832_fechacreacion"]).ToLongDateString() + "</div>");
//                        sb.Append("</td>");
//                        sb.Append("</tr>");
//                    }

//                    sb.Append("<tr>");
//                    if (oFila["t832_posicion"].ToString() == "I")
//                    {
//                        sb.Append("<td style='vertical-align:bottom; padding-bottom: 10px;'>");
                        
//                        if (oFila["t001_idficepi"] == DBNull.Value)
//                            sb.Append(@"<img src='../../../../Images/imgDiamanteDialogo.png' style='width:60px; height:90px;' border='0' />");
//                        else
//                            sb.Append(@"<img src='" + (((int)oFila["tiene_foto"] == 0) ? "../../../../Images/imgSmile.gif" : "../../../Inicio/ObtenerFotoByFicepi.aspx?nF=" + oFila["t001_idficepi"].ToString()) + @"' style='width:60px; height:auto;' border='0' />");
//                        sb.Append(@"<nobr class='NBR W60' style='text-align:center' title='" + oFila["Profesional"].ToString() + @"'>" + oFila["t001_nombre"].ToString() + @"</nobr>");

//                        sb.Append("</td>");

//                        sb.Append("<td colspan='3' style='padding-left:5px;'>");
//                        sb.Append(@"<table class='texto' style='float:left; margin-bottom:20px;' cellspacing='0' cellpadding='0' border='0'>
//                                <tr style='height:9px;'>
//                                    <td background='../../../../Images/Tabla/ConvIzda/1.gif' style='width:34px;'></td>
//                                    <td background='../../../../Images/Tabla/ConvIzda/2.gif' style='width:" + sCellWidth + @";'></td>
//                                    <td background='../../../../Images/Tabla/ConvIzda/3.gif' style='width:81px; height:9px;background-repeat:no-repeat;'></td>
//                                </tr>
//                                <tr>
//                                    <td background='../../../../Images/Tabla/ConvIzda/4.gif' style='width:34px;'>&nbsp;</td>
//                                    <td background='../../../../Images/Tabla/ConvIzda/5.gif' style='font-size:11pt; width:" + sCellWidth + @";'>
//                                        <!-- Inicio del contenido propio de la tabla -->
//                                        " + oFila["t832_texto"].ToString().Replace(((char)10).ToString(), "<br>") + @"
//                                        <!-- Fin del contenido propio de la tabla -->
//                                    </td>
//                                    <td background='../../../../Images/Tabla/ConvIzda/6.gif' style='width:81px;background-repeat:repeat-y;'></td>
//                                </tr>
//                                <tr style='height:55px;'>
//                                    <td background='../../../../Images/Tabla/ConvIzda/7.gif' style='width:34px;'></td>
//                                    <td background='../../../../Images/Tabla/ConvIzda/8.gif' style='width:" + sCellWidth + @";'></td>
//                                    <td background='../../../../Images/Tabla/ConvIzda/9.gif' style='width:81px; font-size:13pt; vertical-align:top; padding-left:15px; padding-top:10px; background-repeat:no-repeat;'>" + Fechas.HoraACadenaLarga((DateTime)oFila["t832_fechacreacion"]) + @"</td>
//                                </tr>
//                            </table>");
//                        sb.Append("</td>");
//                        sb.Append("<td colspan='2'></td>");
//                    }
//                    else //lado derecho
//                    {
//                        sb.Append("<td colspan='2'></td>");
//                        sb.Append("<td colspan='3' style='text-align:right; padding-right:5px;'>");// vertical-align: text-bottom;
//                        //sb.Append("<img src='../../../../Images/imgW2.gif' />");
                        
//                        sb.Append(@"<table class='texto' style='table-layout:fixed; float:right; margin-bottom:20px;' cellspacing='0' cellpadding='0' border='0'>
//                            <tr style='height:11px;'>
//                                <td background='../../../../Images/Tabla/ConvDcha/1.gif' style='width:80px;'></td>
//                                <td background='../../../../Images/Tabla/ConvDcha/2.gif' style='width:" + sCellWidth + @";'></td>
//                                <td background='../../../../Images/Tabla/ConvDcha/3.gif' style='width:37px;'></td>
//                            </tr>
//                            <tr>
//                                <td background='../../../../Images/Tabla/ConvDcha/4.gif' style='width:80px;'>&nbsp;</td>
//                                <td background='../../../../Images/Tabla/ConvDcha/5.gif' style='font-size:11pt; text-align: left;width:" + sCellWidth + @";'>
//                                    <!-- Inicio del contenido propio de la tabla -->" +
//                                        oFila["t832_texto"].ToString().Replace(((char)10).ToString(), "<br>") + @"
//                                    <!-- Fin del contenido propio de la tabla -->
//                                </td>
//                                <td background='../../../../Images/Tabla/ConvDcha/6.gif' style='width:37px;'></td>
//                            </tr>
//                            <tr style='height:54px;'>
//                                <td background='../../../../Images/Tabla/ConvDcha/7.gif' 
//                                    style='width:80px; font-size:13pt; vertical-align:top; padding-top:10px;'>
//                                    <span style='margin-right:15px;'>" + Fechas.HoraACadenaLarga((DateTime)oFila["t832_fechacreacion"]) + @"</span></td>
//                                <td background='../../../../Images/Tabla/ConvDcha/8.gif' style='width:" + sCellWidth + @";'></td>
//                                <td background='../../../../Images/Tabla/ConvDcha/9.gif' style='width:37px;'></td>
//                            </tr>
//                            </table>");

//                        sb.Append("</td>");
//                        sb.Append("<td style='vertical-align:bottom; padding-bottom: 10px;'>");
                        
//                        if (oFila["t001_idficepi"] == DBNull.Value)
//                            sb.Append(@"<img src='../../../../Images/imgDiamanteDialogo.png' style='width:60px; height:90px;' border='0' />");
//                        else
//                            sb.Append(@"<img src='" + (((int)oFila["tiene_foto"] == 0) ? "../../../../Images/imgSmile.gif" : "../../../Inicio/ObtenerFotoByFicepi.aspx?nF=" + oFila["t001_idficepi"].ToString()) + @"' style='width:60px; height:auto;' border='0' />");
//                        sb.Append(@"<nobr class='NBR W60' style='text-align:center' title='" + oFila["Profesional"].ToString() + @"'>" + oFila["t001_nombre"].ToString() + @"</nobr>");
//                        sb.Append("</td>");
//                    }
//                    sb.Append("</tr>");
//                }
//            //dr.Close();
//            ds.Dispose();
//            sb.Append("</table>");
//            sResultado = "OK@#@" + sb.ToString();

//            #endregion

//        }
//        catch (Exception ex)
//        {
//            sResultado = "Error@#@" + Errores.mostrarError("Error al obtener los datos del diálogo.", ex);
//        }
//        return sResultado;
//    }

    //****En el caso de diálogos cerrados, obtener los datos "Se justifica", "Causas" y "Acciones acordadas" (ya definido, no se le llamaba)

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

    public void getDatosEstadoCerrado(int nIdDialogo)
    {
            SqlDataReader dr = DIALOGOALERTAS.ObtenerDatosDialogoCerrado(nIdDialogo);

            if (dr.Read())
            {
                //Si el diálogo está cerrado, todo en modo lectura y si es OC o FA se cargan los datos
                if (dr["bAbierto"].ToString() == "False")
                {
                    btnNewAnot.Disabled = true;
                    btnDocumentacion.Disabled = true;
                    pnDatos.Enabled = false;
                    
                    if (hdnGrupoOC.Value == "1")
                    {
                        if (dr["t831_justificada"].ToString() == "True")
                            rdbOC.SelectedValue = "1";
                        else
                            rdbOC.SelectedValue = "0";

                        txtCausa.Text = dr["t831_causamotivoOC"].ToString();
                        txtAcciones.Text = dr["t831_accionesacordadas"].ToString();
                    }
                }
            }
            dr.Close();
            dr.Dispose();
   }

    private void ObtenerMotivos(string sTipo)
    {
        cboMotivo.Items.Add(new ListItem("", ""));
        cboMotivo.DataValueField = "t840_idmotivo";
        cboMotivo.DataTextField = "t840_descripcion";
        //cboMotivo.DataSource = TARIFAPROY.SelectByt301_idproyecto(nProyEco);
        IB.SUPER.APP.BLL.MOTIVOOCFA bMotivo = new IB.SUPER.APP.BLL.MOTIVOOCFA();
        IB.SUPER.APP.Models.MOTIVOOCFA oMOTIVOOCFAFilter = new IB.SUPER.APP.Models.MOTIVOOCFA();
        try
        {
            oMOTIVOOCFAFilter.t820_tipo = sTipo;

            //cboMotivo.DataSource = bMotivo.Catalogo(oMOTIVOOCFAFilter);
            //cboMotivo.DataBind();
            List<IB.SUPER.APP.Models.MOTIVOOCFA> lista = bMotivo.Catalogo(oMOTIVOOCFAFilter);
            ListItem oLI = null;
            foreach (IB.SUPER.APP.Models.MOTIVOOCFA oElem in lista)
            {
                oLI = new ListItem(oElem.t840_descripcion, oElem.t840_idmotivo.ToString());
                cboMotivo.Items.Add(oLI);
            }
        }
        catch { }
        finally
        {
            bMotivo.Dispose();
        }
    }


}
