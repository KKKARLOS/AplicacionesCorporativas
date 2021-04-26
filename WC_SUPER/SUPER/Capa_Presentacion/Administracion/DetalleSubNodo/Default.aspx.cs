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
    public string sErrores = "";
    public SqlConnection oConn;
    public SqlTransaction tr;
    public int SN4 = 0, SN3 = 0, SN2 = 0, SN1 = 0, Nodo = 0, nIDItem = 0;
    public string sOrigen = "";

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
                //nNivel = int.Parse(Request.QueryString["nNivel"].ToString());localhost:52484/SUPER/Capa_Presentacion/Administracion/DetalleSubNodo/Default.aspx?SN4=MQ==&SN3=Mw==&SN2=MTY=&SN1=Nzg=&Nodo=Mzc1&ID=MTMxNg==&origen=codpar(MantNodo)
                SN4 = int.Parse(Utilidades.decodpar(Request.QueryString["SN4"].ToString()));
                SN3 = int.Parse(Utilidades.decodpar(Request.QueryString["SN3"].ToString()));
                SN2 = int.Parse(Utilidades.decodpar(Request.QueryString["SN2"].ToString()));
                SN1 = int.Parse(Utilidades.decodpar(Request.QueryString["SN1"].ToString()));
                Nodo = int.Parse(Utilidades.decodpar(Request.QueryString["Nodo"].ToString()));
                nIDItem = int.Parse(Utilidades.decodpar(Request.QueryString["ID"].ToString()));

                CargarDatosEstructura();
                if (nIDItem > 0) CargarDatosItem();
                else
                {
                    chkActivo.Checked = true;
                    imgManiobra.Style.Add("visibility","hidden");

                    if (Utilidades.decodpar(Request.QueryString["origen"]) != "")
                    {
                        sOrigen = Utilidades.decodpar(Request.QueryString["origen"]);
                        if (sOrigen == "MantNodo")
                        {
                            hdnIDResponsable.Text = Session["UsuarioActual"].ToString();
                            txtDesResponsable.Text = Session["APELLIDO1"].ToString() + " " + Session["APELLIDO2"].ToString() + ", " + Session["NOMBRE"].ToString();
                        }
                    }
                }

                if (Utilidades.decodpar(Request.QueryString["origen"]) == "MantFiguras")
                {
                    tsPestanas.SelectedIndex = 1;
                    tsPestanas.Items[0].Disabled = true;
                }
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener los datos de la estructura", ex);
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
                sResultado += Grabar(aArgs[1], aArgs[2], aArgs[3]);
                break;
            case ("getDatosPestana"):
                switch (int.Parse(aArgs[1]))
                {
                    case 0://GENERAL
                        //nada porque al ser la primera pestaña se carga directamente en el Page_Load
                        break;
                    case 1://Figuras
                        sResultado += obtenerFigurasItem(aArgs[1], aArgs[2]);
                        break;
                    case 2://Figuras virtuales de proyecto
                        sResultado += obtenerFigurasPSN_SubNodo(aArgs[1], aArgs[2]);
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
        //3º Damos contenido a la variable que se envía de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }

    private void CargarDatosEstructura()
    {
        if (SN4 > 0)
        {
            SUPERNODO4 oSN4Aux = SUPERNODO4.Select(tr, SN4);
            txtDesSN4.Text = oSN4Aux.t394_denominacion;
            hdnIDSN4.Text = oSN4Aux.t394_idsupernodo4.ToString();
        }
        if (SN3 > 0)
        {
            SUPERNODO3 oSN3Aux = SUPERNODO3.Select(tr, SN3);
            txtDesSN3.Text = oSN3Aux.t393_denominacion;
            hdnIDSN3.Text = oSN3Aux.t393_idsupernodo3.ToString();
        }
        if (SN2 > 0)
        {
            SUPERNODO2 oSN2Aux = SUPERNODO2.Select(tr, SN2);
            txtDesSN2.Text = oSN2Aux.t392_denominacion;
            hdnIDSN2.Text = oSN2Aux.t392_idsupernodo2.ToString();
        }
        if (SN1 > 0)
        {
            SUPERNODO1 oSN1Aux = SUPERNODO1.Select(tr, SN1);
            txtDesSN1.Text = oSN1Aux.t391_denominacion;
            hdnIDSN1.Text = oSN1Aux.t391_idsupernodo1.ToString();
        }
        if (Nodo > 0)
        {
            NODO oNODOAux = NODO.Select(tr, Nodo);
            txtDesNodo.Text = oNODOAux.t303_denominacion;
            hdnIDNodo.Text = oNODOAux.t303_idnodo.ToString();
            if (oNODOAux.t303_representativo)
            {
                lblEmpresa.Attributes["class"] = "";
                lblEmpresa.Attributes["onclick"] = "";
                lblEmpresa.Attributes["style"] = "cursor: default;";
                lblEmpresa.Attributes["title"] = "No se puede asignar empresa en un " + Estructura.getDefLarga(Estructura.sTipoElem.SUBNODO) + " cuyo " + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + " está marcado como representantivo";
                imgGomaPlantilla.Attributes["style"] = "display: none;";

            }
        }
    }
    private void CargarDatosItem()
    {
        SUBNODO oSubNodo = SUBNODO.Obtener(null, nIDItem);
        txtID.Text = oSubNodo.t304_idsubnodo.ToString();
        txtDenominacion.Text = oSubNodo.t304_denominacion;
        hdnIDResponsable.Text = oSubNodo.t314_idusuario_responsable.ToString();
        txtDesResponsable.Text = oSubNodo.DesResponsable;
        if ((bool)oSubNodo.t304_estado) chkActivo.Checked = true;
        else chkActivo.Checked = false;
        txtOrden.Text = oSubNodo.t304_orden.ToString();
        hdnManiobra.Text = oSubNodo.t304_maniobra.ToString();
        if (hdnManiobra.Text != "0")
        {
            //txtDenominacion.ReadOnly = true;
            //txtDenominacion.Attributes.Add("onKeyUp", "");
            //chkActivo.Attributes.Add("disabled", "true");
            //imgManiobra.Style.Add("visibility", "visible");
            imgManiobra.ToolTip = Estructura.getDefLarga(Estructura.sTipoElem.SUBNODO) + " de maniobra creada por el sistema. No permite la modificación de la denominación ni del estado.";
        }
        hdnIDEmpresa.Text = oSubNodo.t313_idempresa.ToString();
        txtDesEmpresa.Text = oSubNodo.t313_denominacion;
//        txtDesResponsable.Text = oSubNodo.t313_denominacion;
    }
    private string obtenerFigurasItem(string sPestana, string sIDItem)
    {
        StringBuilder sb = new StringBuilder();
        StringBuilder sbuilder = new StringBuilder();
        sbuilder.Append(" aFigIni = new Array();");
        int i = 0;
        try
        {
            SqlDataReader dr = FIGURASUBNODO.CatalogoFiguras(int.Parse(sIDItem.Replace(".","")));

            sb.Append("<TABLE id='tblFiguras2' class='texto MM' style='WIDTH: 420px;' mantenimiento='1'>");
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
                    sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' bd='' style='height:22px;' onclick='mm(event)' onmousedown='DD(event);' ");
//                    sb.Append(" title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle'>  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");

                    sb.Append("><td><img src='../../../images/imgFN.gif'></td>");
                    sb.Append("<td align='center'>");

                    if (dr["t001_sexo"].ToString() == "V")
                    {
                        //if (dr["t001_tiporecurso"].ToString() == "I")
                        //    sb.Append("<img src='../../../images/imgUsuIV.gif'>");
                        //else
                        //    sb.Append("<img src='../../../images/imgUsuEV.gif'>");
                        switch (dr["tipo"].ToString())
                        {
                            case "P":
                                sb.Append("<img src='../../../images/imgUsuPV.gif'>");
                                break;
                            case "E":
                                sb.Append("<img src='../../../images/imgUsuEV.gif'>");
                                break;
                            case "F":
                                sb.Append("<img src='../../../images/imgUsuFV.gif'>");
                                break;
                        }
                    }
                    else
                    {
                        //if (dr["t001_tiporecurso"].ToString() == "I")
                        //    sb.Append("<img src='../../../images/imgUsuIM.gif'>");
                        //else
                        //    sb.Append("<img src='../../../images/imgUsuEM.gif'>");
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

                    sb.Append("</td><td><span class='NBR W275' ");
                    sb.Append(" title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle'>  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");

                    sb.Append(">" + dr["Profesional"].ToString() + "</span></td>");


                    //Figuras
                    sb.Append("<td><div><ul id='box-" + dr["t314_idusuario"].ToString() + "'>");

                    switch (dr["figura"].ToString())
                    {
                        case "D": sb.Append("<li id='D' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgDelegado.gif' title='Delegado' /></li>"); break;
                        case "I": sb.Append("<li id='I' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgInvitado.gif' title='Invitado' /></li>"); break;
                        case "G": sb.Append("<li id='G' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgGestor.gif' title='Gestor' /></li>"); break;
                    }

                    nUsuario = (int)dr["t314_idusuario"];
                }
                else
                {
                    switch (dr["figura"].ToString())
                    {
                        case "D": sb.Append("<li id='D' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgDelegado.gif' title='Delegado' /></li>"); break;
                        case "I": sb.Append("<li id='I' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgInvitado.gif' title='Invitado' /></li>"); break;
                        case "G": sb.Append("<li id='G' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgGestor.gif' title='Gestor' /></li>"); break;
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
    private string obtenerFigurasPSN_SubNodo(string sPestana, string sIDItem)
    {
        StringBuilder sb = new StringBuilder();
        StringBuilder sbuilder = new StringBuilder();
        sbuilder.Append(" aFigIniV = new Array();");
        int i = 0;
        try
        {
            SqlDataReader dr = FIGURAPSN_SUBNODO.CatalogoFiguras(int.Parse(sIDItem.Replace(".","")));

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
                    sb.Append("tipo='" + dr["tipo"].ToString() + "' ");
                    //if (dr["t303_denominacion"].ToString() == "") sb.Append("tipo='E' ");
                    //else sb.Append("tipo='I' ");

                    sb.Append(" onclick='mm(event)' onmousedown='DD(event);'>");
                    //sb.Append("<td><img src='../../../images/imgFN.gif'></td>");
                    //sb.Append("<td></td>");
                    sb.Append("<td><img src='../../../images/imgFN.gif'></td>");
                    sb.Append("<td align='center'>");

                    if (dr["t001_sexo"].ToString() == "V")
                    {
                        //if (dr["t001_tiporecurso"].ToString() == "I")
                        //    sb.Append("<img src='../../../images/imgUsuIV.gif'>");
                        //else
                        //    sb.Append("<img src='../../../images/imgUsuEV.gif'>");
                        switch (dr["tipo"].ToString())
                        {
                            case "P":
                                sb.Append("<img src='../../../images/imgUsuPV.gif'>");
                                break;
                            case "E":
                                sb.Append("<img src='../../../images/imgUsuEV.gif'>");
                                break;
                            case "F":
                                sb.Append("<img src='../../../images/imgUsuFV.gif'>");
                                break;
                        }
                    }
                    else
                    {
                        //if (dr["t001_tiporecurso"].ToString() == "I")
                        //    sb.Append("<img src='../../../images/imgUsuIM.gif'>");
                        //else
                        //    sb.Append("<img src='../../../images/imgUsuEM.gif'>");
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
                    sb.Append("<td style='padding-left:3px;'><span class='NBR W280' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:60px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:60px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:60px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["Profesional"].ToString() + "</span></td>");// ondblclick='insertarFigura(this.parentNode.parentNode)'
                    //sb.Append("<td>" + dr["Profesional"].ToString() + "</td>");
                    //Figuras
                    sb.Append("<td><div><ul id='boxv-" + dr["t314_idusuario"].ToString() + "'>");

                    switch (dr["figura"].ToString())
                    {
                        case "D": sb.Append("<li id='DV' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgDelegado.gif' title='Delegado' /></li>"); break;
                        case "C": sb.Append("<li id='CV' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgColaborador.gif' title='Colaborador' /></li>"); break;
                        case "J": sb.Append("<li id='JV' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgJefeProyecto.gif' title='Jefe' /></li>"); break;
                        case "M": sb.Append("<li id='MV' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgSubjefeProyecto.gif' title='Responsable técnico de proyecto económico' /></li>"); break;
                        case "B": sb.Append("<li id='BV' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgBitacorico.gif' title='Bitacórico' /></li>"); break;
                        case "S": sb.Append("<li id='SV' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgSecretaria.gif' title='Asistente' /></li>"); break;
                        case "I": sb.Append("<li id='IV' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgInvitado.gif' title='Invitado' /></li>"); break;
                    }

                    nUsuario = (int)dr["t314_idusuario"];
                }
                else
                {
                    switch (dr["figura"].ToString())
                    {
                        case "D": sb.Append("<li id='DV' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgDelegado.gif' title='Delegado' /></li>"); break;
                        case "C": sb.Append("<li id='CV' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgColaborador.gif' title='Colaborador' /></li>"); break;
                        case "J": sb.Append("<li id='JV' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgJefeProyecto.gif' title='Jefe' /></li>"); break;
                        case "M": sb.Append("<li id='MV' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgSubjefeProyecto.gif' title='Responsable técnico de proyecto económico' /></li>"); break;
                        case "B": sb.Append("<li id='BV' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgBitacorico.gif' title='Bitacórico' /></li>"); break;
                        case "S": sb.Append("<li id='SV' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgSecretaria.gif' title='Secretaria' /></li>"); break;
                        case "I": sb.Append("<li id='IV' value='" + dr["orden"].ToString() + "'><img src='../../../Images/imgInvitado.gif' title='Invitado' /></li>"); break;
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

                sb.Append("<td style='padding-left:5px;'>");
                //sb.Append("<nobr ondblclick='insertarFiguraV(this.parentNode.parentNode)' class='NBR W380'  title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle'>  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["empresa"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["Profesional"].ToString() + "</nobr></td>");
                sb.Append("<span ondblclick='insertarFiguraV(this.parentNode.parentNode)' class='NBR W375'  title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle'>  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["Profesional"].ToString() + "</span></td>");
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
    private string obtenerProfesionalesFigura(string sAp1, string sAp2, string sNombre)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = USUARIO.ObtenerUsuarioActivos(Utilidades.unescape(sAp1), Utilidades.unescape(sAp2), Utilidades.unescape(sNombre), false);

            sb.Append("<TABLE id='tblFiguras1' class='texto MAM' style='width: 400px;'>");
            sb.Append("<colgroup><col style='width: 20px' /><col style='width: 380px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' style='height:22px;' ");
                //if (dr["t303_denominacion"].ToString() == "")
                //    sb.Append(" tipo ='E'");
                //else
                //    sb.Append(" tipo ='I'");
                sb.Append(" tipo ='" + dr["tipo"].ToString() + "'");
                sb.Append(" sexo ='" + dr["t001_sexo"].ToString() + "'>");

                //sb.Append(" onclick='mmse(this)' ondblclick='insertarFigura(this)' onmousedown='DD(this);'>");
                sb.Append("<td></td>");

                sb.Append("<td style='padding-left:5px;'>");
                //sb.Append("<nobr ondblclick='insertarFigura(this.parentNode.parentNode)' class='NBR W380'  title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle'>  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["empresa"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["Profesional"].ToString() + "</nobr></td>");
                sb.Append("<span ondblclick='insertarFigura(this.parentNode.parentNode)' class='NBR W375'  title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle'>  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["Profesional"].ToString() + "</span></td>");
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
    private string Grabar(string strDatosBasicos, string strFiguras, string strFigurasV)
    {
        string sResul = "";
        int nID = -1;
        string[] aDatosBasicos = null;

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
                aDatosBasicos = Regex.Split(strDatosBasicos, "##");
                ///aDatosBasicos[0] = ID
                ///aDatosBasicos[1] = Denominacion
                ///aDatosBasicos[2] = IDResponsable
                ///aDatosBasicos[3] = Activo
                ///aDatosBasicos[4] = Orden
                ///aDatosBasicos[5] = IDPadre
                ///aDatosBasicos[6] = t313_idempresa
                ///
                if (aDatosBasicos[0] == "") //insert
                {
                    nID = SUBNODO.Insert(tr,
                                    Utilidades.unescape(aDatosBasicos[1]),
                                    int.Parse(aDatosBasicos[5]),
                                    int.Parse(aDatosBasicos[4]),
                                    (aDatosBasicos[3] == "1") ? true : false,
                                    0,
                                    int.Parse(aDatosBasicos[2]),
                                    (aDatosBasicos[6]=="")? null : (int?)int.Parse(aDatosBasicos[6])
                                    );
                }
                else //update
                {
                    nID = int.Parse(aDatosBasicos[0].Replace(".",""));
                    SUBNODO.Update(tr,
                                nID,
                                Utilidades.unescape(aDatosBasicos[1]),
                                int.Parse(aDatosBasicos[5]),
                                int.Parse(aDatosBasicos[4]),
                                (aDatosBasicos[3] == "1") ? true : false,
                                int.Parse(aDatosBasicos[2]),
                                (aDatosBasicos[6] == "") ? null : (int?)int.Parse(aDatosBasicos[6])
                                );
                }
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
                    //FIGURASUBNODO.DeleteUsuario(tr, nID, int.Parse(aFig[1]));
                    //if (aFig[0] != "D")
                    //{
                    //    string[] aFiguras = Regex.Split(aFig[2], ",");
                    //    foreach (string oFigura in aFiguras)
                    //    {
                    //        if (oFigura == "") continue;
                    //        FIGURASUBNODO.Insert(tr, nID, int.Parse(aFig[1]), oFigura);
                    //    }
                    //}
                    if (aFig[0] == "D")
                        FIGURASUBNODO.DeleteUsuario(tr, nID, int.Parse(aFig[1]));
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
                                FIGURASUBNODO.Delete(tr, nID, int.Parse(aFig[1]), aFig2[1]);
                            else
                                FIGURASUBNODO.Insert(tr, nID, int.Parse(aFig[1]), aFig2[1]);
                        }
                    }
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
                        FIGURAPSN_SUBNODO.DeleteUsuario(tr, nID, int.Parse(aFigV[1]));
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
                                FIGURAPSN_SUBNODO.Delete(tr, nID, int.Parse(aFigV[1]), aFig2V[1]);
                            else
                                FIGURAPSN_SUBNODO.Insert(tr, nID, int.Parse(aFigV[1]), aFig2V[1]);
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
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos del elemento de estructura", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }

}
