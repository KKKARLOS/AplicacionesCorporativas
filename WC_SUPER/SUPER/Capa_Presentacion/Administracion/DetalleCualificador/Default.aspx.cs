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

using SUPER.Capa_Negocio;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores = "";
    public SqlConnection oConn;
    public SqlTransaction tr;
    public int nCualificador = 0, nIdEstructura = 0;
    public string sTitle = "::: SUPER ::: - ", sNivel = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        sNivel = Request.QueryString["sNivel"].ToString();
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
                switch (sNivel)
                {
                    case "0":
                        sTitle += "Cualificador de proyectos a nivel de " + Estructura.getDefLarga(Estructura.sTipoElem.NODO);
                        this.lblEstructura.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
                        this.lblEstructura.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.NODO));
                        break;
                    case "1":
                        sTitle += "Cualificador de proyectos a nivel de " + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO1);
                        this.lblEstructura.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO1);
                        this.lblEstructura.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO1));
                        break;
                    case "2":
                        sTitle += "Cualificador de proyectos a nivel de " + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO2);
                        this.lblEstructura.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO2);
                        this.lblEstructura.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO2));
                        break;
                    case "3":
                        sTitle += "Cualificador de proyectos a nivel de " + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO3);
                        this.lblEstructura.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO3);
                        this.lblEstructura.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO3));
                        break;
                    case "4":
                        sTitle += "Cualificador de proyectos a nivel de " + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO4);
                        this.lblEstructura.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO4);
                        this.lblEstructura.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO4));
                        break;
                }
                for (int i = 0; i < 500; i++)
                {
                    sTitle += "&nbsp;";
                }

                nCualificador = int.Parse(Request.QueryString["nCualificador"].ToString());
                nIdEstructura = int.Parse(Request.QueryString["nIdEstructura"].ToString());
                hdnIDEstructura.Value = nIdEstructura.ToString();

                if (nCualificador > 0) CargarDatosCualificador();
                else
                {
                    switch (sNivel)
                    {
                        case "0":
                            NODO oNodo = NODO.ObtenerNodo(null, nIdEstructura);
                            lblDenominacion.Text = oNodo.t303_denominacion;
                            break;
                        case "1":
                            SUPERNODO1 oSUPERNODO1 = SUPERNODO1.Obtener(null, nIdEstructura);
                            lblDenominacion.Text = oSUPERNODO1.t391_denominacion;
                            break;
                        case "2":
                            SUPERNODO2 oSUPERNODO2 = SUPERNODO2.Obtener(null, nIdEstructura);
                            lblDenominacion.Text = oSUPERNODO2.t392_denominacion;
                            break;
                        case "3":
                            SUPERNODO3 oSUPERNODO3 = SUPERNODO3.Obtener(null, nIdEstructura);
                            lblDenominacion.Text = oSUPERNODO3.t393_denominacion;
                            break;
                        case "4":
                            SUPERNODO4 oSUPERNODO4 = SUPERNODO4.Obtener(null, nIdEstructura);
                            lblDenominacion.Text = oSUPERNODO4.t394_denominacion;
                            break;
                    }
                    hdnIDResponsable.Text = Session["UsuarioActual"].ToString();
                    txtDesResponsable.Text = Session["APELLIDO1"].ToString() + " " + Session["APELLIDO2"].ToString() + ", " + Session["NOMBRE"].ToString();
                }
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener los datos generales.", ex);
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
                sResultado += Grabar(aArgs[1], aArgs[2]);
                break;
            case ("getDatosPestana"):
                switch (int.Parse(aArgs[1]))
                {
                    case 0://GENERAL
                        //nada porque al ser la primera pestaña se carga directamente en el Page_Load
                        break;
                    case 1://Figuras
                        sResultado += obtenerFigurasCualificador(aArgs[1], aArgs[2]);
                        break;
                }
                break;
            case ("tecnicos"):
                sResultado += obtenerProfesionalesFigura(aArgs[1], aArgs[2], aArgs[3]);
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

    private void CargarDatosCualificador()
    {
        switch (sNivel)
        {
            case "0":
                CDP oCDP = CDP.Obtener(null, nCualificador);
                NODO oNodo = NODO.ObtenerNodo(null, oCDP.t303_idnodo);
                hdnID.Value = oCDP.t476_idcnp.ToString();
                txtDenominacion.Text = oCDP.t476_denominacion;
                lblDenominacion.Text = oNodo.t303_denominacion;
                hdnIDResponsable.Text = oCDP.t314_idusuario_responsable.ToString();
                txtDesResponsable.Text = oCDP.DesResponsable;
                if ((bool)oCDP.t476_activo) chkActivo.Checked = true;
                else chkActivo.Checked = false;
                txtOrden.Text = oCDP.t476_orden.ToString();
                break;
            case "1":
                CSN1P oCSN1P = CSN1P.Select(null, nCualificador);
                SUPERNODO1 oSUPERNODO1 = SUPERNODO1.Obtener(null, oCSN1P.t391_idsupernodo1);

                hdnID.Value = oCSN1P.t485_idcsn1p.ToString();
                txtDenominacion.Text = oCSN1P.t485_denominacion;
                lblDenominacion.Text = oSUPERNODO1.t391_denominacion;
                hdnIDResponsable.Text = oCSN1P.t314_idusuario_responsable.ToString();
                txtDesResponsable.Text = oCSN1P.DesResponsable;
                if ((bool)oCSN1P.t485_activo) chkActivo.Checked = true;
                else chkActivo.Checked = false;
                txtOrden.Text = oCSN1P.t485_orden.ToString();
                break;
            case "2":
                CSN2P oCSN2P = CSN2P.Select(null, nCualificador);
                SUPERNODO2 oSUPERNODO2 = SUPERNODO2.Obtener(null, oCSN2P.t392_idsupernodo2);

                hdnID.Value = oCSN2P.t487_idcsn2p.ToString();
                txtDenominacion.Text = oCSN2P.t487_denominacion;
                lblDenominacion.Text = oSUPERNODO2.t392_denominacion;
                hdnIDResponsable.Text = oCSN2P.t314_idusuario_responsable.ToString();
                txtDesResponsable.Text = oCSN2P.DesResponsable;
                if ((bool)oCSN2P.t487_activo) chkActivo.Checked = true;
                else chkActivo.Checked = false;
                txtOrden.Text = oCSN2P.t487_orden.ToString();
                break;

            case "3":
                CSN3P oCSN3P = CSN3P.Select(null, nCualificador);
                SUPERNODO3 oSUPERNODO3 = SUPERNODO3.Obtener(null, oCSN3P.t393_idsupernodo3);

                hdnID.Value = oCSN3P.t489_idcsn3p.ToString();
                txtDenominacion.Text = oCSN3P.t489_denominacion;
                lblDenominacion.Text = oSUPERNODO3.t393_denominacion;
                hdnIDResponsable.Text = oCSN3P.t314_idusuario_responsable.ToString();
                txtDesResponsable.Text = oCSN3P.DesResponsable;
                if ((bool)oCSN3P.t489_activo) chkActivo.Checked = true;
                else chkActivo.Checked = false;
                txtOrden.Text = oCSN3P.t489_orden.ToString();
                break;

            case "4":
                CSN4P oCSN4P = CSN4P.Select(null, nCualificador);
                SUPERNODO4 oSUPERNODO4 = SUPERNODO4.Obtener(null, oCSN4P.t394_idsupernodo4);

                hdnID.Value = oCSN4P.t491_idcsn4p.ToString();
                txtDenominacion.Text = oCSN4P.t491_denominacion;
                lblDenominacion.Text = oSUPERNODO4.t394_denominacion;
                hdnIDResponsable.Text = oCSN4P.t314_idusuario_responsable.ToString();
                txtDesResponsable.Text = oCSN4P.DesResponsable;
                if ((bool)oCSN4P.t491_activo) chkActivo.Checked = true;
                else chkActivo.Checked = false;
                txtOrden.Text = oCSN4P.t491_orden.ToString();
                break;
        }
        bool bAdmin = SUPER.Capa_Negocio.Utilidades.EsAdminProduccion();
        if (((int)Session["UsuarioActual"] != int.Parse(hdnIDResponsable.Text)) && !bAdmin && Request.QueryString["edicion"].ToString()=="N")
        {
            tsPestanas.SelectedIndex = 1;
            tsPestanas.Items[0].Disabled = true;

        }

    }
    private string obtenerFigurasCualificador(string sPestana, string sCualificador)
    {
        StringBuilder sb = new StringBuilder();
        StringBuilder sbuilder = new StringBuilder();
        sbuilder.Append(" aFigIni = new Array();");
        int i = 0;
        try
        {
            SqlDataReader dr=null;
            switch (sNivel)
            {
                case "0":
                    dr = FIGURASCDP.CatalogoFiguras(int.Parse(sCualificador));
                    break;
                case "1":
                    dr = FIGURASCSN1P.CatalogoFiguras(int.Parse(sCualificador));
                    break;
                case "2":
                    dr = FIGURASCSN2P.CatalogoFiguras(int.Parse(sCualificador));
                    break;
                case "3":
                    dr = FIGURASCSN3P.CatalogoFiguras(int.Parse(sCualificador));
                    break;
                case "4":
                    dr = FIGURASCSN4P.CatalogoFiguras(int.Parse(sCualificador));
                    break;
            }
            sb.Append("<TABLE id='tblFiguras2' class='texto MM' style='WIDTH: 420px; ' mantenimiento='1'>");
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
                    //sb.Append(" title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle'>  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["empresa"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
                    sb.Append(" title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle'>  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");

                    sb.Append("><td><img src='../../../images/imgFN.gif'></td>");
                    sb.Append("<td style='text-align:center;'>");

                    if (dr["t001_sexo"].ToString() == "V")
                    {
                        //sb.Append("<img src='../../../images/imgUsuIV.gif'>");
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
                        //sb.Append("<img src='../../../images/imgUsuIM.gif'>");
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
    private string obtenerProfesionalesFigura(string sAp1, string sAp2, string sNombre)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = USUARIO.ObtenerUsuarioActivos(Utilidades.unescape(sAp1), Utilidades.unescape(sAp2), Utilidades.unescape(sNombre), false);

            sb.Append("<TABLE id='tblFiguras1' class='texto MAM' style='width:400px;'>");
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
                //sb.Append("<nobr ondblclick='insertarFigura(this.parentNode.parentNode)' class='NBR W380'  title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle'>  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["empresa"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["Profesional"].ToString() + "</nobr></td>");
                sb.Append("<nobr ondblclick='insertarFigura(this.parentNode.parentNode)' class='NBR W380'  title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle'>  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["Profesional"].ToString() + "</nobr></td>");
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
    private string Grabar(string strDatosBasicos, string strFiguras)
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
                ///aDatosBasicos[5] = IdEstructura

                if (aDatosBasicos[0] == "0") //insert
                {
                    switch (sNivel)
                    {
                        case "0":
                            nID = CDP.Insert(tr,
                                            Utilidades.unescape(aDatosBasicos[1]),
                                            int.Parse(aDatosBasicos[5]),
                                            int.Parse(aDatosBasicos[2]),
                                            (aDatosBasicos[3] == "1") ? true : false,
                                            byte.Parse(aDatosBasicos[4]));
                            break;
                        case "1":
                            nID = CSN1P.Insert(tr,
                                            Utilidades.unescape(aDatosBasicos[1]),
                                            int.Parse(aDatosBasicos[5]),
                                            int.Parse(aDatosBasicos[2]),
                                            (aDatosBasicos[3] == "1") ? true : false,
                                            byte.Parse(aDatosBasicos[4]));
                            break;
                        case "2":
                            nID = CSN2P.Insert(tr,
                                            Utilidades.unescape(aDatosBasicos[1]),
                                            int.Parse(aDatosBasicos[5]),
                                            int.Parse(aDatosBasicos[2]),
                                            (aDatosBasicos[3] == "1") ? true : false,
                                            byte.Parse(aDatosBasicos[4]));
                            break;
                        case "3":
                            nID = CSN3P.Insert(tr,
                                            Utilidades.unescape(aDatosBasicos[1]),
                                            int.Parse(aDatosBasicos[5]),
                                            int.Parse(aDatosBasicos[2]),
                                            (aDatosBasicos[3] == "1") ? true : false,
                                            byte.Parse(aDatosBasicos[4]));
                            break;
                        case "4":
                            nID = CSN4P.Insert(tr,
                                            Utilidades.unescape(aDatosBasicos[1]),
                                            int.Parse(aDatosBasicos[5]),
                                            int.Parse(aDatosBasicos[2]),
                                            (aDatosBasicos[3] == "1") ? true : false,
                                            byte.Parse(aDatosBasicos[4]));
                            break;
                    }
                }
                else //update
                {
                    nID = int.Parse(aDatosBasicos[0]);
                    switch (sNivel)
                    {
                        case "0":
                            CDP.Update(tr,
                                nID,
                                Utilidades.unescape(aDatosBasicos[1]),
                                int.Parse(aDatosBasicos[5]),
                                int.Parse(aDatosBasicos[2]),
                                (aDatosBasicos[3] == "1") ? true : false,
                                byte.Parse(aDatosBasicos[4]));
                            break;
                        case "1":
                            CSN1P.Update(tr,
                                nID,
                                Utilidades.unescape(aDatosBasicos[1]),
                                int.Parse(aDatosBasicos[5]),
                                int.Parse(aDatosBasicos[2]),
                                (aDatosBasicos[3] == "1") ? true : false,
                                byte.Parse(aDatosBasicos[4]));
                            break;
                        case "2":
                            CSN2P.Update(tr,
                                nID,
                                Utilidades.unescape(aDatosBasicos[1]),
                                int.Parse(aDatosBasicos[5]),
                                int.Parse(aDatosBasicos[2]),
                                (aDatosBasicos[3] == "1") ? true : false,
                                byte.Parse(aDatosBasicos[4]));
                            break;
                        case "3":
                            CSN3P.Update(tr,
                                nID,
                                Utilidades.unescape(aDatosBasicos[1]),
                                int.Parse(aDatosBasicos[5]),
                                int.Parse(aDatosBasicos[2]),
                                (aDatosBasicos[3] == "1") ? true : false,
                                byte.Parse(aDatosBasicos[4]));
                            break;
                        case "4":
                            CSN4P.Update(tr,
                                nID,
                                Utilidades.unescape(aDatosBasicos[1]),
                                int.Parse(aDatosBasicos[5]),
                                int.Parse(aDatosBasicos[2]),
                                (aDatosBasicos[3] == "1") ? true : false,
                                byte.Parse(aDatosBasicos[4]));
                            break;
                    }
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

                    switch (sNivel)
                    {
                        case "0":
                            if (aFig[0] == "D")
                                FIGURASCDP.DeleteUsuario(tr, nID, int.Parse(aFig[1]));
                            else
                            {
                                string[] aFiguras = Regex.Split(aFig[2], ",");
                                foreach (string oFigura in aFiguras)
                                {
                                    if (oFigura == "") continue;
                                    //FIGURASCDP.Insert(tr, nID, int.Parse(aFig[1]), oFigura);
                                    string[] aFig2 = Regex.Split(oFigura, "@");
                                    ///aFig2[0] = bd
                                    ///aFig2[1] = Figura
                                    if (aFig2[0] == "D")
                                        FIGURASCDP.Delete(tr, nID, int.Parse(aFig[1]), aFig2[1]);
                                    else
                                        FIGURASCDP.Insert(tr, nID, int.Parse(aFig[1]), aFig2[1]);
                                }
                            }
                            break;
                        case "1":
                            if (aFig[0] == "D")
                                FIGURASCSN1P.DeleteUsuario(tr, nID, int.Parse(aFig[1]));
                            else
                            {
                                string[] aFiguras = Regex.Split(aFig[2], ",");
                                foreach (string oFigura in aFiguras)
                                {
                                    if (oFigura == "") continue;
                                    //FIGURASCSN1P.Insert(tr, nID, int.Parse(aFig[1]), oFigura);
                                    string[] aFig2 = Regex.Split(oFigura, "@");
                                    ///aFig2[0] = bd
                                    ///aFig2[1] = Figura
                                    if (aFig2[0] == "D")
                                        FIGURASCSN1P.Delete(tr, nID, int.Parse(aFig[1]), aFig2[1]);
                                    else
                                        FIGURASCSN1P.Insert(tr, nID, int.Parse(aFig[1]), aFig2[1]);
                                }
                            }
                            break;
                        case "2":
                            if (aFig[0] == "D")
                                FIGURASCSN2P.DeleteUsuario(tr, nID, int.Parse(aFig[1]));
                            else
                            {
                                string[] aFiguras = Regex.Split(aFig[2], ",");
                                foreach (string oFigura in aFiguras)
                                {
                                    if (oFigura == "") continue;
                                    //FIGURASCSN2P.Insert(tr, nID, int.Parse(aFig[1]), oFigura);
                                    string[] aFig2 = Regex.Split(oFigura, "@");
                                    ///aFig2[0] = bd
                                    ///aFig2[1] = Figura
                                    if (aFig2[0] == "D")
                                        FIGURASCSN2P.Delete(tr, nID, int.Parse(aFig[1]), aFig2[1]);
                                    else
                                        FIGURASCSN2P.Insert(tr, nID, int.Parse(aFig[1]), aFig2[1]);
                                }
                            }
                            break;
                        case "3":
                            if (aFig[0] == "D")
                                FIGURASCSN3P.DeleteUsuario(tr, nID, int.Parse(aFig[1]));
                            else
                            {
                                string[] aFiguras = Regex.Split(aFig[2], ",");
                                foreach (string oFigura in aFiguras)
                                {
                                    if (oFigura == "") continue;
                                    //FIGURASCSN3P.Insert(tr, nID, int.Parse(aFig[1]), oFigura);
                                    string[] aFig2 = Regex.Split(oFigura, "@");
                                    ///aFig2[0] = bd
                                    ///aFig2[1] = Figura
                                    if (aFig2[0] == "D")
                                        FIGURASCSN3P.Delete(tr, nID, int.Parse(aFig[1]), aFig2[1]);
                                    else
                                        FIGURASCSN3P.Insert(tr, nID, int.Parse(aFig[1]), aFig2[1]);
                                }
                            }
                            break;
                        case "4":
                            if (aFig[0] == "D")
                                FIGURASCSN4P.DeleteUsuario(tr, nID, int.Parse(aFig[1]));
                            else
                            {
                                string[] aFiguras = Regex.Split(aFig[2], ",");
                                foreach (string oFigura in aFiguras)
                                {
                                    if (oFigura == "") continue;
                                    //FIGURASCSN4P.Insert(tr, nID, int.Parse(aFig[1]), oFigura);
                                    string[] aFig2 = Regex.Split(oFigura, "@");
                                    ///aFig2[0] = bd
                                    ///aFig2[1] = Figura
                                    if (aFig2[0] == "D")
                                        FIGURASCSN4P.Delete(tr, nID, int.Parse(aFig[1]), aFig2[1]);
                                    else
                                        FIGURASCSN4P.Insert(tr, nID, int.Parse(aFig[1]), aFig2[1]);
                                }
                            }
                            break;
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
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos del cualificador", ex, false);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }

}
