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

using System.Text.RegularExpressions;
using System.Text;
using EO.Web;
using SUPER.Capa_Negocio;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHTML = "";
    public string sErrores = "";
    public SqlConnection oConn;
    public SqlTransaction tr;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            try
            {
                if (Session["IDRED"] == null)
                {
                    try
                    {
                        Response.Redirect("~/SesionCaducadaModal.aspx", true);
                    }
                    catch (System.Threading.ThreadAbortException) { return; }
                }
                if (Session["OCULTAR_AUDITORIA"].ToString() == "1")
                {
                    this.cldAuditoria.Visible = false;
                    this.btnAuditoria.Visible = false;
                }
                // Leer Contrato 
                hdnID.Text = Utilidades.decodpar(Request.QueryString["ID"].ToString());
                CONTRATO oCONTRATO = CONTRATO.Select(null, int.Parse(hdnID.Text));

                txtCodigo.Text = oCONTRATO.t306_idcontrato.ToString();
                txtDenominacion.Text = oCONTRATO.Contrato;
                //Centro de responsabilidad
                this.hdnIdCR.Value = oCONTRATO.t303_idnodo.ToString();
                txtNodo.Text = oCONTRATO.Nodo;
                //Usuario responsable de contrato
                this.hdnIdResp.Value = oCONTRATO.t314_idusuario_responsable.ToString();
                txtResponsable.Text = oCONTRATO.Responsable;
                //Cliente
                this.hdnIdCli.Value = oCONTRATO.t302_idcliente_contrato.ToString();
                txtCliente.Text = oCONTRATO.Cliente;
                //gestor de producción
                txtGestorProdu.Text = oCONTRATO.GestorProdu;
                hdnIDGestorProdu.Text = oCONTRATO.t314_idusuario_gestorprod.ToString();
                //Comercial HERMES
                this.hdnIdComer.Value = oCONTRATO.t314_idusuario_ComercialHERMES.ToString();
                this.txtComercial.Text = oCONTRATO.ComercialHERMES;

                chkVisionReplicas.Checked = oCONTRATO.t306_visionreplicas;

                //Organización comercial
                this.txtCodOrgComer.Text = oCONTRATO.ta212_idorganizacioncomercial.ToString();
                this.txtCodExtOrgComer.Text = oCONTRATO.ta212_codigoexterno;
                this.txtDenOrgComer.Text = oCONTRATO.ta212_denominacion;
                //Nueva Línea de Oferta
                this.txtCodNLO.Text = oCONTRATO.t195_idlineaoferta.ToString();
                this.txtCodExtNLO.Text = oCONTRATO.t195_codigoexterno.ToString();
                this.txtDenNLO.Text = oCONTRATO.t195_denominacion;
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener los datos de los contratos", ex);
            }

            //1º Se indican (por este orden) la función a la que se va a devolver el resultado
            //   y la función que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
            //2º Se "registra" la función que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
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
            case ("grabar"):
                sResultado += Grabar(aArgs[1], aArgs[2], aArgs[3], aArgs[4]);
                break;
            case ("getDatosPestana"):
                switch (int.Parse(aArgs[1]))
                {
                    case 0://GENERAL
                        //nada porque al ser la primera pestaña se carga directamente en el Page_Load
                        break;
                    case 1://Figuras
                        sResultado += obtenerFigurasItem(aArgs[1]);
                        break;
                    case 2://Extensiones (NO quitar pues en principio se pidió)
                        sResultado += ObtenerExtensiones(aArgs[1]);
                        break;
                    case 3://Figuras virtuales de proyecto
                        sResultado += obtenerFigurasPSN_Contrato(aArgs[1], aArgs[2]);
                        break; 
                }
                break;
            case ("tecnicos"):
                sResultado += obtenerProfesionalesFigura(aArgs[1], aArgs[2], aArgs[3]);
                break;
            case ("tecnicosV"):
                sResultado += obtenerProfesionalesFiguraV(aArgs[1], aArgs[2], aArgs[3]);
                break;
        }
        //3º Damos contenido a la variable que se envía de vuelta al contrato.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al contrato.
        return _callbackResultado;
    }
    private string ObtenerExtensiones(string sPestana)
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblDatos' class='texto MANO' style='width: 870px; BORDER-COLLAPSE: collapse; table-layout:fixed;' mantenimiento='1'>");
            sb.Append("<colgroup><col style='width:10px;' />");
            sb.Append("<col style='width:430px;' />");
            sb.Append("<col style='width:50px;' />");
            sb.Append("<col style='width:60px;' />");
            sb.Append("<col style='width:80px;' />");
            sb.Append("<col style='width:80px;' />");
            sb.Append("<col style='width:80px;' />");
            sb.Append("<col style='width:80px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");
            SqlDataReader dr = null;
            dr = CONTRATO.ObtenerExtensiones(int.Parse(hdnID.Text));

            while (dr.Read())
            {
                sb.Append("<tr style='height:20px;' bd='' ");
                sb.Append(" id='" + dr["t377_idextension"].ToString() + "'");
                sb.Append(" onclick='mm(event)'>");

                sb.Append("<td><img src='../../../../images/imgFN.gif'></td>");

                if ((int)dr["t377_idextension"] == 0) sb.Append("<td><nobr style='margin-left:10px;' class='NBR W320'>");
                else sb.Append("<td><nobr style='margin-left:30px;' class='NBR W300'>");

                sb.Append(dr["t377_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td>" + int.Parse(dr["t377_idextension"].ToString()).ToString("#,###,##0") + "</td>");

                string strFecha = "";
                if (dr["t377_fechacontratacion"] == System.DBNull.Value) strFecha = "";
                else strFecha = DateTime.Parse(dr["t377_fechacontratacion"].ToString()).ToShortDateString();

                if (Session["BTN_FECHA"].ToString() == "I")
                    sb.Append("<td><input id='fC" + dr["t377_idextension"].ToString() + "' type='text' class='txtFecL' style='width:60px; cursor: url(../../../../images/imgManoAzul2.cur)' value='" + strFecha + "' Calendar='oCal' ondblclick='mc(event);' onchange='mod(this)' readonly /></td>");
                else
                    sb.Append("<td><input id='fC" + dr["t377_idextension"].ToString() + "' type='text' class='txtFecL' style='width:60px; cursor: url(../../../../images/imgManoAzul2.cur)' value='" + strFecha + "' Calendar='oCal' onchange='mod(this)' onfocus='focoFecha(event)' onmousedown='mc1(event)' /></td>"); 

                sb.Append("<td style='text-align:right; padding-right:5px;'><input type='text' class='txtNumL' onfocus='fn(this,9, 2)' style='width:75px' value=\"" + double.Parse(dr["t377_importepro"].ToString()).ToString("N") + "\" onKeyUp='mod(this);'></td>");
                sb.Append("<td style='text-align:right; padding-right:5px;'><input type='text' class='txtNumL' onfocus='fn(this,9, 2)' style='width:75px' value=\"" + double.Parse(dr["t377_marprepro"].ToString()).ToString("N") + "\"  onKeyUp='mod(this);'></td>");
                sb.Append("<td style='text-align:right; padding-right:5px;'><input type='text' class='txtNumL' onfocus='fn(this,9, 2)' style='width:75px' value=\"" + double.Parse(dr["t377_importeser"].ToString()).ToString("N") + "\" onKeyUp='mod(this);'></td>");
                sb.Append("<td style='text-align:right; padding-right:5px;'><input type='text' class='txtNumL' onfocus='fn(this,9, 2)' style='width:75px' value=\"" + double.Parse(dr["t377_marpreser"].ToString()).ToString("N") + "\" onKeyUp='mod(this);'></td>");

                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sPestana + "@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener las extensiones de los contratos", ex);
        }
    }
    private string obtenerFigurasItem(string sPestana)
    {
        StringBuilder sb = new StringBuilder();
        StringBuilder sbuilder = new StringBuilder();
        sbuilder.Append(" aFigIni = new Array();");
        int i = 0;
        try
        {
            SqlDataReader dr = FIGURACONTRATO.CatalogoFiguras(int.Parse(hdnID.Text));
            sb.Append("<TABLE id='tblFiguras2' class='texto MM' style='width: 420px;' mantenimiento='1' cellpadding='0'>");
            sb.Append("<colgroup><col style='width: 20px' /><col style='width: 20px' /><col style='width: 280px;' /><col style='width: 100px;' /></colgroup>");
            sb.Append("<tbody>");
            int nUsuario = 0;
            bool bHayFilas = false;
            while (dr.Read())
            {
                bHayFilas = true;
                sbuilder.Append("aFigIni[" + i.ToString() + "] = {idUser:\"" + dr["t314_idusuario"].ToString() + "\"," +
                                "sFig:\"" + dr["figura"].ToString() + "\"};");
                i++;
                if ((int)dr["t314_idusuario"] != nUsuario)
                {
                    if (nUsuario != 0)
                    {
                        sb.Append("</ul></div></td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' bd='' style='height:20px;' onclick='mm(event)' onmousedown='DD(event);' ");
                    //sb.Append(" title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle'>  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["empresa"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
                    sb.Append(" title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle'>  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");

                    sb.Append("><td><img src='../../../../images/imgFN.gif'></td>");
                    sb.Append("<td align='center'>");

                    if (dr["t001_sexo"].ToString() == "V")
                    {
                        //sb.Append("<img src='../../../../images/imgUsuIV.gif'>");
                        switch (dr["tipo"].ToString())
                        {
                            case "P":
                                sb.Append("<img src='../../../../images/imgUsuPV.gif'>");
                                break;
                            case "E":
                                sb.Append("<img src='../../../../images/imgUsuEV.gif'>");
                                break;
                            case "F":
                                sb.Append("<img src='../../../../images/imgUsuFV.gif'>");
                                break;
                        }
                    }
                    else
                    {
                        //sb.Append("<img src='../../../../images/imgUsuIM.gif'>");
                        switch (dr["tipo"].ToString())
                        {
                            case "P":
                                sb.Append("<img src='../../../images/imgUsuPM.gif'>");
                                break;
                            case "E":
                                sb.Append("<img src='../../../images/imgUsuEM.gif'>");
                                break;
                            case "F":
                                sb.Append("<img src='../../../images/imgUsuFM.gif'>");
                                break;
                        }
                    }
                    sb.Append("</td><td><nobr class='NBR W280'>" + dr["Profesional"].ToString() + "</nobr></td>");

                    //Figuras

                    sb.Append("<td><div style='height:20px;'><ul id='box-" + dr["t314_idusuario"].ToString() + "'>");

                    switch (dr["figura"].ToString())
                    {
                        case "D": sb.Append("<li id='D' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgDelegado.gif' title='Delegado' /></li>"); break;
                        case "I": sb.Append("<li id='I' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgInvitado.gif' title='Invitado' /></li>"); break;
                    }

                    nUsuario = (int)dr["t314_idusuario"];
                }
                else
                {
                    switch (dr["figura"].ToString())
                    {
                        case "D": sb.Append("<li id='D' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgDelegado.gif' title='Delegado' /></li>"); break;
                        case "I": sb.Append("<li id='I' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgInvitado.gif' title='Invitado' /></li>"); break;
                    }
                }
            }
            dr.Close();
            dr.Dispose();
            if (bHayFilas)
            {
                sb.Append("</ul></div></td>");
                sb.Append("</tr>");
            }
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sPestana + "@#@" + sb.ToString() + "///" + sbuilder.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la relación de figuras.", ex);
        }
    }
    private string obtenerFigurasPSN_Contrato(string sPestana, string sIDItem)
    {
        StringBuilder sb = new StringBuilder();
        StringBuilder sbuilder = new StringBuilder();
        sbuilder.Append(" aFigIniV = new Array();");
        int i = 0;
        try
        {
            SqlDataReader dr = FIGURAPSN_CONTRATO.CatalogoFiguras(int.Parse(sIDItem));

            sb.Append("<TABLE id='tblFiguras2V' class='texto MM' style='WIDTH: 420px;' mantenimiento='1'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width: 10px' />");
            sb.Append("    <col style='width: 20px' />");
            sb.Append("    <col style='width: 290px;' />");
            sb.Append("    <col style='width: 100px;' />");
            sb.Append("</colgroup>");
            int nUsuario = 0;
            bool bHayFilas = false;
            string sColor = "black";
            while (dr.Read())
            {
                bHayFilas = true;
                sbuilder.Append("aFigIniV[" + i.ToString() + "] = {idUser:\"" + dr["t314_idusuario"].ToString() + "\"," +
                                "sFig:\"" + dr["figura"].ToString() + "\"};");
                i++;
                sColor = "black";
                if ((int)dr["t314_idusuario"] != nUsuario)
                {
                    if (nUsuario != 0)
                    {
                        sb.Append("</ul></div></td>");
                        sb.Append("</tr>");
                    }
                    if (dr["baja"].ToString() == "1") sColor = "red";
                    sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' bd='' style='height:22px;color:" + sColor + "' ");
                    sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");

                    if (dr["t303_denominacion"].ToString() == "") sb.Append("tipo='E' ");
                    else sb.Append("tipo='I' ");

                    sb.Append(" onclick='mm(event)' onmousedown='DD(event);'>");
                    //sb.Append("<td><img src='../../../images/imgFN.gif'></td>");
                    //sb.Append("<td></td>");
                    sb.Append("<td><img src='../../../../images/imgFN.gif'></td>");
                    sb.Append("<td align='center'>");

                    if (dr["t001_sexo"].ToString() == "V")
                    {
                        //if (dr["t001_tiporecurso"].ToString() == "I")
                        //    sb.Append("<img src='../../../../images/imgUsuIV.gif'>");
                        //else
                        //    sb.Append("<img src='../../../../images/imgUsuEV.gif'>");
                        switch (dr["tipo"].ToString())
                        {
                            case "P":
                                sb.Append("<img src='../../../../images/imgUsuPV.gif'>");
                                break;
                            case "E":
                                sb.Append("<img src='../../../../images/imgUsuEV.gif'>");
                                break;
                            case "F":
                                sb.Append("<img src='../../../../images/imgUsuFV.gif'>");
                                break;
                        }
                    }
                    else
                    {
                        //if (dr["t001_tiporecurso"].ToString() == "I")
                        //    sb.Append("<img src='../../../../images/imgUsuIM.gif'>");
                        //else
                        //    sb.Append("<img src='../../../../images/imgUsuEM.gif'>");
                        switch (dr["tipo"].ToString())
                        {
                            case "P":
                                sb.Append("<img src='../../../images/imgUsuPM.gif'>");
                                break;
                            case "E":
                                sb.Append("<img src='../../../images/imgUsuEM.gif'>");
                                break;
                            case "F":
                                sb.Append("<img src='../../../images/imgUsuFM.gif'>");
                                break;
                        }
                    }
                    sb.Append("</td>");
                    //sb.Append("<td><nobr class='NBR W280' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:60px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:60px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:60px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:60px;'>Empresa:</label>" + dr["empresa"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["Profesional"].ToString() + "</nobr></td>");// ondblclick='insertarFigura(this.parentNode.parentNode)'
                    sb.Append("<td style='padding-left:3px;'><span class='NBR W275' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:60px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:60px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:60px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["Profesional"].ToString() + "</span></td>");// ondblclick='insertarFigura(this.parentNode.parentNode)'

                    //sb.Append("<td>" + dr["Profesional"].ToString() + "</td>");
                    //Figuras
                    sb.Append("<td><div><ul id='boxv-" + dr["t314_idusuario"].ToString() + "'>");

                    switch (dr["figura"].ToString())
                    {
                        case "D": sb.Append("<li id='DV' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgDelegado.gif' title='Delegado' /></li>"); break;
                        case "C": sb.Append("<li id='CV' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgColaborador.gif' title='Colaborador' /></li>"); break;
                        case "J": sb.Append("<li id='JV' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgJefeProyecto.gif' title='Jefe' /></li>"); break;
                        case "M": sb.Append("<li id='MV' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgSubjefeProyecto.gif' title='Responsable técnico de proyecto económico' /></li>"); break;
                        case "B": sb.Append("<li id='BV' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgBitacorico.gif' title='Bitacórico' /></li>"); break;
                        case "S": sb.Append("<li id='SV' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgSecretaria.gif' title='Asistente' /></li>"); break;
                        case "I": sb.Append("<li id='IV' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgInvitado.gif' title='Invitado' /></li>"); break;
                    }

                    nUsuario = (int)dr["t314_idusuario"];
                }
                else
                {
                    switch (dr["figura"].ToString())
                    {
                        case "D": sb.Append("<li id='DV' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgDelegado.gif' title='Delegado' /></li>"); break;
                        case "C": sb.Append("<li id='CV' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgColaborador.gif' title='Colaborador' /></li>"); break;
                        case "J": sb.Append("<li id='JV' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgJefeProyecto.gif' title='Jefe' /></li>"); break;
                        case "M": sb.Append("<li id='MV' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgSubjefeProyecto.gif' title='Responsable técnico de proyecto económico' /></li>"); break;
                        case "B": sb.Append("<li id='BV' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgBitacorico.gif' title='Bitacórico' /></li>"); break;
                        case "S": sb.Append("<li id='SV' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgSecretaria.gif' title='Asistente' /></li>"); break;
                        case "I": sb.Append("<li id='IV' value='" + dr["orden"].ToString() + "'><img src='../../../../Images/imgInvitado.gif' title='Invitado' /></li>"); break;
                    }
                }
            }
            dr.Close();
            dr.Dispose();
            if (bHayFilas)
            {
                sb.Append("</ul></div></td>");
                sb.Append("</tr>");
            }
            sb.Append("</table>");

            return "OK@#@" + sPestana + "@#@" + sb.ToString() + "///" + sbuilder.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la relación de figuras virtuales.", ex);
        }
    }    
    private string obtenerProfesionalesFigura(string sAp1, string sAp2, string sNombre)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = USUARIO.ObtenerUsuarioActivos(Utilidades.unescape(sAp1), Utilidades.unescape(sAp2), Utilidades.unescape(sNombre), false);

            sb.Append("<TABLE id='tblFiguras1' class='texto MAM' style='WIDTH: 400px; BORDER-COLLAPSE: collapse;table-layout:fixed;' cellSpacing='0' cellPadding='0' border='0' >");
            sb.Append("<colgroup><col style='width: 20px' /><col style='width: 380px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' style='height:20px; noWrap:true;' ");
                //if (dr["t303_denominacion"].ToString() == "")
                //    sb.Append(" tipo ='E'");
                //else
                //    sb.Append(" tipo ='I'");
                sb.Append(" tipo ='" + dr["tipo"].ToString() + "'");
                sb.Append(" sexo ='" + dr["t001_sexo"].ToString() + "'>");

                //sb.Append(" onclick='mmse(this)' ondblclick='insertarFigura(this)' onmousedown='DD(this);'>");
                sb.Append("<td></td>");

                sb.Append("<td style='padding-left:5px;'>");
                //sb.Append("<nobr ondblclick='insertarFigura(this.parentNode.parentNode)' class='NBR W380'  title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle'>  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["empresa"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["Profesional"].ToString() + "</nobr></td>");
                sb.Append("<nobr ondblclick='insertarFigura(this.parentNode.parentNode)' class='NBR W380'  title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle'>  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["Profesional"].ToString() + "</nobr></td>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la relación de profesionales.", ex);
        }
    }
    private string obtenerProfesionalesFiguraV(string sAp1, string sAp2, string sNombre)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = USUARIO.ObtenerUsuarioActivos(Utilidades.unescape(sAp1), Utilidades.unescape(sAp2), Utilidades.unescape(sNombre), false);

            sb.Append("<TABLE id='tblFiguras1V' class='texto MAM' style='WIDTH: 400px;'>");
            sb.Append("<colgroup><col style='width: 20px' /><col style='width: 380px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' style='height:22px; noWrap:true;' ");
                //if (dr["t303_denominacion"].ToString() == "")
                //    sb.Append(" tipo ='E'");
                //else
                //    sb.Append(" tipo ='I'");
                sb.Append(" tipo ='" + dr["tipo"].ToString() + "'");
                sb.Append(" sexo ='" + dr["t001_sexo"].ToString() + "'>");

                //sb.Append(" onclick='mmse(this)' ondblclick='insertarFigura(this)' onmousedown='DD(this);'>");
                sb.Append("<td></td>");

                sb.Append("<td  style='padding-left:5px;'>");
                //sb.Append("<nobr ondblclick='insertarFiguraV(this.parentNode.parentNode)' class='NBR W380'  title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle'>  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["empresa"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["Profesional"].ToString() + "</nobr></td>");
                sb.Append("<span ondblclick='insertarFiguraV(this.parentNode.parentNode)' class='NBR W375'  title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle'>  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["Profesional"].ToString() + "</span></td>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la relación de profesionales.", ex);
        }
    }
    private string Grabar(string strDatosBasicos, string strFiguras, string strExtensiones, string strFigurasV)
    {
        string sResul = "";
        int nID = -1;
        //string[] aDatosBasicos = null;


        #region abrir conexión y transacción
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
        try
        {
            #region Datos Generales
            if (strDatosBasicos != "")//No se ha modificado nada de la pestaña general
            {
                string[] aDatosBasicos = Regex.Split(strDatosBasicos, "##");
                ///aDatosBasicos[0] = hdnIDGestorProd
                ///aDatosBasicos[1] = hdnIdCR
                ///aDatosBasicos[2] = hdnIdCli
                ///aDatosBasicos[3] = hdnIdResp
                ///aDatosBasicos[4] = hdnIdComer
                ///aDatosBasicos[5] = chkVisionReplicas
                //CONTRATO oContrato = CONTRATO.Select(tr, int.Parse(hdnID.Text));
                //CONTRATO.Update(tr, int.Parse(hdnID.Text),
                //                oContrato.t303_idnodo,
                //                oContrato.t302_idcliente_contrato,
                //                oContrato.t314_idusuario_responsable,
                //                int.Parse(aDatosBasicos[0]),
                //                (aDatosBasicos[1] == "1") ? true : false);
                CONTRATO.Update(tr, int.Parse(hdnID.Text),
                                int.Parse(aDatosBasicos[1]),
                                int.Parse(aDatosBasicos[2]),
                                int.Parse(aDatosBasicos[3]),
                                int.Parse(aDatosBasicos[0]),
                                int.Parse(aDatosBasicos[4]),
                                (aDatosBasicos[5] == "1") ? true : false);
            }

            #endregion

            #region Datos Figuras
            if (strFiguras != "")//No se ha modificado nada de la pestaña de Figuras
            {
                string[] aUsuarios = Regex.Split(strFiguras, "///");
                foreach (string oUsuario in aUsuarios)
                {
                    if (oUsuario == "") continue;
                    string[] aFig = Regex.Split(oUsuario, "##");

                    ///aFig[0] = bd
                    ///aFig[1] = idUsuario
                    ///aFig[2] = Figuras
                
                    //FIGURACONTRATO.Delete(tr, int.Parse(hdnID.Text), int.Parse(aFig[1]));
                    //if (aFig[0] != "D")
                    //{
                    //    string[] aFiguras = Regex.Split(aFig[2], ",");
                    //    foreach (string oFigura in aFiguras)
                    //    {
                    //        if (oFigura == "") continue;
                    //        FIGURACONTRATO.Insert(tr, int.Parse(hdnID.Text), int.Parse(aFig[1]), oFigura);
                    //    }
                    //}
                    if (aFig[0] == "D")
                        FIGURACONTRATO.Delete(tr, int.Parse(hdnID.Text), int.Parse(aFig[1]));
                    else
                    {
                        string[] aFiguras = Regex.Split(aFig[2], ",");
                        foreach (string oFigura in aFiguras)
                        {
                            if (oFigura == "") continue;
                            string[] aFig2 = Regex.Split(oFigura, "@");
                            ///aFig2[0] = bd
                            ///aFig2[1] = Figura
                            if (aFig2[0] == "D")
                                FIGURACONTRATO.Delete(tr, int.Parse(hdnID.Text), int.Parse(aFig[1]));
                            else
                                FIGURACONTRATO.Insert(tr, int.Parse(hdnID.Text), int.Parse(aFig[1]), aFig2[1]);
                        }
                    }
                }
            }

            #endregion

            #region Datos Extensiones
            if (strExtensiones != "") //No se ha modificado nada de la pestaña de Extensiones
            {
                string[] aExtensiones = Regex.Split(strExtensiones, "///");
                foreach (string oExtension in aExtensiones)
                {
                    if (oExtension == "") continue;
                    string[] aExt = Regex.Split(oExtension, "##");

                    ///aExt[0] = bd
                    ///aExt[1] = idExtension
                    ///aExt[2] = Importe producto
                    ///aExt[3] = Margen producto
                    ///aExt[4] = Importe servicio
                    ///aExt[5] = Margen servicio
                    ///aExt[6] = Fecha Contrato
                    if (aExt[0] == "D") EXTENSIONCONTRATO.Delete(tr, int.Parse(hdnID.Text), int.Parse(aExt[1]));
                    if (aExt[0] == "U") EXTENSIONCONTRATO.Update(tr, int.Parse(hdnID.Text), int.Parse(aExt[1]), (aExt[4] == "") ? 0 : decimal.Parse(aExt[4]), (aExt[5] == "") ? 0 : decimal.Parse(aExt[5]), (aExt[2] == "") ? 0 : decimal.Parse(aExt[2]), (aExt[3] == "") ? 0 : decimal.Parse(aExt[3]), DateTime.Parse(aExt[6]));
                }
            }

            #endregion

            #region Datos Figuras Virtuales
            if (strFigurasV != "")//No se ha modificado nada de la pestaña de Figuras virtuales
            {
                string[] aUsuariosV = Regex.Split(strFigurasV, "///");
                foreach (string oUsuarioV in aUsuariosV)
                {
                    if (oUsuarioV == "") continue;
                    string[] aFigV = Regex.Split(oUsuarioV, "##");

                    ///aFig[0] = bd
                    ///aFig[1] = idUsuario
                    ///aFig[2] = Figuras

                    if (aFigV[0] == "D")
                        FIGURAPSN_CONTRATO.DeleteUsuario(tr, int.Parse(hdnID.Text), int.Parse(aFigV[1]));
                    else
                    {
                        string[] aFigurasV = Regex.Split(aFigV[2], ",");
                        foreach (string oFiguraV in aFigurasV)
                        {
                            if (oFiguraV == "") continue;
                            string[] aFig2V = Regex.Split(oFiguraV, "@");
                            ///aFig2[0] = bd
                            ///aFig2[1] = Figura
                            if (aFig2V[0] == "D")
                                FIGURAPSN_CONTRATO.Delete(tr, int.Parse(hdnID.Text), int.Parse(aFigV[1]), aFig2V[1]);
                            else
                                FIGURAPSN_CONTRATO.Insert(tr, int.Parse(hdnID.Text), int.Parse(aFigV[1]), aFig2V[1]);
                        }
                    }
                }
            }

            #endregion

            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@" + nID.ToString("#,###");
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos del contrato", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
}
