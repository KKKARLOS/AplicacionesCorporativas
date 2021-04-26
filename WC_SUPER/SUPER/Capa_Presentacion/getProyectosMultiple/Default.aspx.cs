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

using System.Text.RegularExpressions;

using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;
using System.Text;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHTML = "<table id='tblDatos'></table>", sHayPreferencia = "false";
    public string sErrores = "", sModulo = "", sMostrarBitacoricos="0", sNodoFijo = "0";
    public SqlConnection oConn;
    public SqlTransaction tr;
    private bool bHayPreferencia = false;
    public short nPantallaPreferencia = 4;

    protected void Page_Load(object sender, EventArgs e)
    {
        bool bEsAdminProduccion = false;
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
                if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                    bEsAdminProduccion = true;

                sModulo = Request.QueryString["mod"].ToString();
                if (sModulo == "pge")
                {
                    cboCualidad.Items.Add(new ListItem("Contratante", "C"));
                    cboCualidad.Items.Add(new ListItem("Replicado sin gestión", "J"));
                    cboCualidad.Items.Add(new ListItem("Replicado con gestión", "P"));
                }
                else
                {
                    cboCualidad.Items.Add(new ListItem("Contratante", "C"));
                    cboCualidad.Items.Add(new ListItem("Replicado con gestión", "P"));
                }

                if (Request.QueryString["sMostrarBitacoricos"] != null)
                    sMostrarBitacoricos = Request.QueryString["sMostrarBitacoricos"].ToString();

                //Cargo la denominacion del label Nodo
                string sAux = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
                if (sAux.Trim() != "")
                {
                    this.lblNodo.InnerText = sAux;
                    this.lblNodo.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.NODO));
                    this.gomaNodo.Attributes.Add("title", "Borra el " +Estructura.getDefLarga(Estructura.sTipoElem.NODO));
                    this.lblNodo2.InnerText = sAux;
                    this.lblNodo2.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.NODO));
                }
                //Cargo el combo de modelos de contratación
                cboModContratacion.DataTextField = "t316_denominacion";
                cboModContratacion.DataValueField = "t316_idmodalidad";
                cboModContratacion.DataSource = MODALIDADCONTRATO.Catalogo(null, "", true, 2, 0);
                cboModContratacion.DataBind();

                #region ocultar cualificadores de estructura que no está en uso
                if (!Utilidades.EstructuraActiva("SN4"))
                {
                    lblCSN4P.Style.Add("visibility", "hidden");
                    txtCSN4P.Style.Add("visibility", "hidden");
                    imgGomaCSN4P.Style.Add("visibility", "hidden");
                }
                if (!Utilidades.EstructuraActiva("SN3"))
                {
                    lblCSN3P.Style.Add("visibility", "hidden");
                    txtCSN3P.Style.Add("visibility", "hidden");
                    imgGomaCSN3P.Style.Add("visibility", "hidden");
                }
                if (!Utilidades.EstructuraActiva("SN2"))
                {
                    lblCSN2P.Style.Add("visibility", "hidden");
                    txtCSN2P.Style.Add("visibility", "hidden");
                    imgGomaCSN2P.Style.Add("visibility", "hidden");
                }
                if (!Utilidades.EstructuraActiva("SN1"))
                {
                    lblCSN1P.Style.Add("visibility", "hidden");
                    txtCSN1P.Style.Add("visibility", "hidden");
                    imgGomaCSN1P.Style.Add("visibility", "hidden");
                }
                #endregion

                //if (Session["ADMINISTRADOR_PC_ACTUAL"].ToString() == "A")
                if (bEsAdminProduccion)
                {
                    cboCR.Visible = false;
                    this.chkNodoAct.Visible = false;
                    this.lblNodoAct.Visible = false;
                    hdnIdNodo.Visible = true;
                    txtDesNodo.Visible = true;
                    gomaNodo.Visible = true;
                }
                else
                {
                    cboCR.Visible = true;
                    this.chkNodoAct.Visible = true;
                    this.lblNodoAct.Visible = true;
                    hdnIdNodo.Visible = false;
                    txtDesNodo.Visible = false;
                    gomaNodo.Visible = false;
                    cargarNodos(false);
                }

                rdbTipoBusqueda.Items[1].Selected = true;

                if (Request.QueryString["nNodo"] != null){
                    sHayPreferencia = "true"; //Si venimos a mostrar los proyectos de un nodo en concreto, simulamos que hay preferencia, para que no se muestren los criterios de inicio.
                    sNodoFijo = "1";
                    if (bEsAdminProduccion)
                    {
                        hdnIdNodo.Text = Request.QueryString["nNodo"].ToString();
                        txtDesNodo.Text = Utilidades.unescape(Request.QueryString["sNodo"].ToString());
                    }
                    else
                    {
                        cboCR.SelectedValue = Request.QueryString["nNodo"].ToString();
                        cboCR.Enabled = false;
                        this.chkNodoAct.Disabled = true;
                    }
                    gomaNodo.Style.Add("visibility", "hidden");
                    string strTabla2 = ObtenerProyectos(sModulo,
                                            (bEsAdminProduccion) ? hdnIdNodo.Text : cboCR.SelectedValue,
                                            "A",
                                            "",
                                            "",
                                            "",
                                            "",
                                            "",
                                            "",
                                            "",
                                            "",
                                            "",
                                            "",
                                            "",
                                            "",
                                            "",
                                            "",
                                            "",
                                            "",
                                            "");

                    string[] aTabla2 = Regex.Split(strTabla2, "@#@");
                    if (aTabla2[0] != "Error") this.strTablaHTML = aTabla2[1];
                    else sErrores = aTabla2[1];

                }
                else if (Request.QueryString["nPE"] == null)
                {
                    string[] aDatosPref = Regex.Split(getPreferencia(""), "@#@");
                    if (bHayPreferencia && aDatosPref[0] == "OK")
                    {
                        sHayPreferencia = "true";
                        if (bEsAdminProduccion)
                        {
                            hdnIdNodo.Text = aDatosPref[1];
                            txtDesNodo.Text = aDatosPref[2];
                        }
                        else
                        {
                            cboCR.SelectedValue = aDatosPref[1];
                        }

                        cboEstado.SelectedValue = aDatosPref[3];
                        cboCategoria.SelectedValue = aDatosPref[4];
                        hdnIdCliente.Text = aDatosPref[5];
                        txtDesCliente.Text = aDatosPref[6];
                        txtIDContrato.Text = aDatosPref[7];
                        txtDesContrato.Text = aDatosPref[8];
                        hdnIdResponsable.Text = aDatosPref[9];
                        txtResponsable.Text = aDatosPref[10];
                        hdnIdHorizontal.Text = aDatosPref[11];
                        txtDesHorizontal.Text = aDatosPref[12];
                        if (aDatosPref[13] != "") txtNumPE.Text = int.Parse(aDatosPref[13]).ToString("#,###");
                        txtDesPE.Text = aDatosPref[14];
                        chkActuAuto.Checked = (aDatosPref[15] == "1") ? true : false;
                        //if (chkActuAuto.Checked) btnObtener.Disabled = true;
                        cboCualidad.SelectedValue = aDatosPref[16];
                        if (aDatosPref[17] == "I") rdbTipoBusqueda.Items[0].Selected = true;
                        else rdbTipoBusqueda.Items[1].Selected = true;
                        txtDesPE.Text = aDatosPref[18];
                        hdnCNP.Text = aDatosPref[19];
                        txtCNP.Text = aDatosPref[20];
                        hdnCSN1P.Text = aDatosPref[21];
                        txtCSN1P.Text = aDatosPref[22];
                        hdnCSN2P.Text = aDatosPref[23];
                        txtCSN2P.Text = aDatosPref[24];
                        hdnCSN3P.Text = aDatosPref[25];
                        txtCSN3P.Text = aDatosPref[26];
                        hdnCSN4P.Text = aDatosPref[27];
                        txtCSN4P.Text = aDatosPref[28];
                        hdnIdNaturaleza.Text = aDatosPref[29];
                        txtDesNaturaleza.Text = aDatosPref[30];
                        cboModContratacion.SelectedValue = aDatosPref[31];

                        if (Request.QueryString["sSoloAbiertos"] != null)
                        {
                            cboEstado.SelectedValue = "A";
                            cboEstado.Enabled = false;
                        }

                        if (chkActuAuto.Checked)
                        {
                            string strTabla = ObtenerProyectos(sModulo,
                                                    (bEsAdminProduccion) ? hdnIdNodo.Text : cboCR.SelectedValue,
                                                    cboEstado.SelectedValue,
                                                    cboCategoria.SelectedValue,
                                                    hdnIdCliente.Text,
                                                    hdnIdResponsable.Text,
                                                    txtNumPE.Text.Replace(".", ""),
                                                    txtDesPE.Text,
                                                    (rdbTipoBusqueda.Items[0].Selected) ? "I" : "C",
                                                    cboCualidad.SelectedValue,
                                                    txtIDContrato.Text,
                                                    hdnIdHorizontal.Text,
                                                    sMostrarBitacoricos,
                                                   hdnCNP.Text,
                                                   hdnCSN1P.Text,
                                                   hdnCSN2P.Text,
                                                   hdnCSN3P.Text,
                                                   hdnCSN4P.Text,
                                                   hdnIdNaturaleza.Text,
                                                   cboModContratacion.SelectedValue);

                            string[] aTabla = Regex.Split(strTabla, "@#@");
                            if (aTabla[0] != "Error") this.strTablaHTML = aTabla[1];
                            else sErrores = aTabla[1];
                        }
                    }
                    else if (aDatosPref[0] == "Error") this.sErrores += Errores.mostrarError(aDatosPref[1]);
                }
                else
                {
                    sHayPreferencia = "true";
                    txtNumPE.Text = Request.QueryString["nPE"].ToString();
                    string strTabla2 = ObtenerProyectos(sModulo,
                                            (bEsAdminProduccion) ? hdnIdNodo.Text : cboCR.SelectedValue,
                                            "",
                                            "",
                                            "",
                                            "",
                                            Request.QueryString["nPE"].ToString(),
                                            "",
                                            "C",
                                            "",
                                            "",
                                            "",
                                            "",
                                            "",
                                            "",
                                            "",
                                            "",
                                            "",
                                            "",
                                            "");

                    string[] aTabla2 = Regex.Split(strTabla2, "@#@");
                    if (aTabla2[0] != "Error") this.strTablaHTML = aTabla2[1];
                    else sErrores = aTabla2[1];
                }

                if (bEsAdminProduccion)
                {
                    if (hdnIdNodo.Text!= ""){
                        NODO oNodo = NODO.Select(null, int.Parse(hdnIdNodo.Text));
                        oNodo.ObtenerCualificadoresEstructura();
                        lblCNP.InnerText = oNodo.t303_denominacion_CNP;
                        lblCSN1P.InnerText = oNodo.t391_denominacion_CSN1P;
                        lblCSN2P.InnerText = oNodo.t392_denominacion_CSN2P;
                        lblCSN3P.InnerText = oNodo.t393_denominacion_CSN3P;
                        lblCSN4P.InnerText = oNodo.t394_denominacion_CSN4P;
                    }
                }
                else
                {
                    if (cboCR.SelectedValue != "")
                    {
                        NODO oNodo = NODO.Select(null, int.Parse(cboCR.SelectedValue));
                        oNodo.ObtenerCualificadoresEstructura();
                        lblCNP.InnerText = oNodo.t303_denominacion_CNP;
                        lblCSN1P.InnerText = oNodo.t391_denominacion_CSN1P;
                        lblCSN2P.InnerText = oNodo.t392_denominacion_CSN2P;
                        lblCSN3P.InnerText = oNodo.t393_denominacion_CSN3P;
                        lblCSN4P.InnerText = oNodo.t394_denominacion_CSN4P;
                    }
                }

            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener los datos", ex);
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
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        switch (aArgs[0])
        {
            case ("buscar"):
                sResultado += ObtenerProyectos(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8], aArgs[9], aArgs[10], aArgs[11], aArgs[12], aArgs[13], aArgs[14], aArgs[15], aArgs[16], aArgs[17], aArgs[18], aArgs[19], aArgs[20]);
                break;
            //case ("setPSN"):
            //    Session["ID_PROYECTOSUBNODO"] = aArgs[1];
            //    Session["MODOLECTURA_PROYECTOSUBNODO"] = (aArgs[2] == "1") ? true : false;
            //    Session["RTPT_PROYECTOSUBNODO"] = false;
            //    sResultado += "OK";
            //    break;
            case ("setPreferencia"):
                sResultado += setPreferencia(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8], aArgs[9], aArgs[10], aArgs[11], aArgs[12], aArgs[13], aArgs[14], aArgs[15], aArgs[16], aArgs[17], aArgs[18], aArgs[19]);
                break;
            case ("delPreferencia"):
                sResultado += delPreferencia();
                break;
            case ("getPreferencia"):
                sResultado += getPreferencia(aArgs[1]);
                break;
            case ("recargarNodos"):
                sResultado += ReCargarNodos(aArgs[1], aArgs[2]);
                break;
        }
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        return _callbackResultado;
    }

    private string ObtenerProyectos(string sModulo, string sNodo, string sEstado, string sCategoria, string sIdCliente, string sIdResponsable, 
                                    string sNumPE, string sDesPE, string sTipoBusqueda, string sCualidad, string sIDContrato, 
                                    string sIdHorizontal, string sMostrarBitacoricos,
                                    string sCNP, string sCSN1P, string sCSN2P, string sCSN3P, string sCSN4P,
                                    string sIdNaturaleza, string sModeloContratacion)
    {
        StringBuilder sb = new StringBuilder();
        int? idNodo = null;
        int? idCliente = null;
        int? idResponsable = null;
        int? numPE = null;
        int? nIDContrato = null;
        int? nIdHorizontal = null;
        int? nIdNaturaleza = null;
        int? nIdModeloContratacion = null;

        try
        {
            if (sNodo != "") idNodo = int.Parse(sNodo);
            if (sIdCliente != "") idCliente = int.Parse(sIdCliente);
            if (sIdResponsable != "") idResponsable = int.Parse(sIdResponsable);
            if (sNumPE != "" && sNumPE != "0") numPE = int.Parse(sNumPE);
            if (sIDContrato != "") nIDContrato = int.Parse(sIDContrato);
            if (sIdHorizontal != "") nIdHorizontal = int.Parse(sIdHorizontal);
            if (sIdNaturaleza != "") nIdNaturaleza = int.Parse(sIdNaturaleza);
            if (sModeloContratacion != "") nIdModeloContratacion = int.Parse(sModeloContratacion);

            sb.Append("<table id='tblDatos' class='texto MANO' style='WIDTH: 960px;'>");
            sb.Append("<colgroup>");
			sb.Append("<col style='width:20px' />");
			sb.Append("<col style='width:20px' />");
			sb.Append("<col style='width:20px' />");
			sb.Append("<col style='width:65px;' />");
			sb.Append("<col style='width:355px' />");
            sb.Append("<col style='width:220px' />");
			sb.Append("<col style='width:260px' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");
            SqlDataReader dr = PROYECTO.ObtenerProyectos(sModulo, idNodo, sEstado, sCategoria, idCliente, idResponsable, numPE, 
                        Utilidades.unescape(sDesPE), sTipoBusqueda, sCualidad, nIDContrato, nIdHorizontal, (int)Session["UsuarioActual"], 
                        (sMostrarBitacoricos == "1") ? false : true,
                        false,
                        (sCNP == "") ? null : (int?)int.Parse(sCNP),
                        (sCSN1P == "") ? null : (int?)int.Parse(sCSN1P),
                        (sCSN2P == "") ? null : (int?)int.Parse(sCSN2P),
                        (sCSN3P == "") ? null : (int?)int.Parse(sCSN3P),
                        (sCSN4P == "") ? null : (int?)int.Parse(sCSN4P),
                        false,
                        nIdNaturaleza, nIdModeloContratacion, null, null);

            while (dr.Read()) 
            {
                sb.Append("<tr style='height:20px' ");
                sb.Append("idPSN='" + dr["t305_idproyectosubnodo"].ToString() + "' ");
                sb.Append("idPE='" + dr["t301_idproyecto"].ToString() + "' ");
                sb.Append("categoria='" + dr["t301_categoria"].ToString() + "' ");
                sb.Append("cualidad='" + dr["t305_cualidad"].ToString() + "' ");
                sb.Append("estado='" + dr["t301_estado"].ToString() + "' ");
                sb.Append(">");

                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");

                sb.Append("<td style=' text-align:right; padding-right:5px;'");
                if (ConfigurationManager.AppSettings["MOSTRAR_MOTIVO_PROY"] == "1")
                    sb.Append(" title=\"" + dr["desmotivo"].ToString() + "\"");
                sb.Append(">" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + "</td>");

                sb.Append("<td><nobr class='NBR W350' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Proyecto:</label>" + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Responsable:</label>" + dr["Responsable"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "</nobr></td>");

                sb.Append("<td onmouseover='TTip(event)'><nobr class='NBR W210'>" + dr["t302_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td onmouseover='TTip(event)'><nobr class='NBR W250'>" + dr["t303_denominacion"].ToString() + "</nobr></td>");
                sb.Append("</tr>");
            }
            dr.Close(); 
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@"+sb.ToString();
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los proyectos económicos", ex);
            return "Error@#@Error al obtener los proyectos económicos";
        }
    }

    private void cargarNodos(bool bSoloActivos)
    {
        try
        {
            //Cargar el combo de nodos accesibles
            ListItem oLI = null;
            SqlDataReader dr;
            if (sModulo.ToLower() == "pge")
                dr = NODO.ObtenerNodosUsuarioSegunVisionProyectosECO(null, (int)Session["UsuarioActual"], bSoloActivos);
            else
                dr = NODO.ObtenerNodosUsuarioSegunVisionProyectosTEC(null, (int)Session["UsuarioActual"], (sMostrarBitacoricos == "1") ? false : true, false);
            while (dr.Read())
            {
                oLI = new ListItem(dr["denominacion"].ToString(), dr["identificador"].ToString());
                oLI.Attributes.Add("CNP", Utilidades.escape(dr["t303_denominacion_CNP"].ToString()));
                oLI.Attributes.Add("CSN1P", Utilidades.escape(dr["t391_denominacion_CSN1P"].ToString()));
                oLI.Attributes.Add("CSN2P", Utilidades.escape(dr["t392_denominacion_CSN2P"].ToString()));
                oLI.Attributes.Add("CSN3P", Utilidades.escape(dr["t393_denominacion_CSN3P"].ToString()));
                oLI.Attributes.Add("CSN4P", Utilidades.escape(dr["t394_denominacion_CSN4P"].ToString()));
                cboCR.Items.Add(oLI);
            }
            dr.Close();
            dr.Dispose();
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al cargar los nodos", ex);
        }
    }
    private string ReCargarNodos(string sModulo, string sSoloActivos)
    {
        try
        {
            //Genero un churro para recargar el combo desde javascript
            bool bSoloActivos = true;
            if (sSoloActivos == "N")
                bSoloActivos = false;
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr;
            if (sModulo.ToLower() == "pge")
                dr = NODO.ObtenerNodosUsuarioSegunVisionProyectosECO(null, (int)Session["UsuarioActual"], bSoloActivos);
            else
                dr = NODO.ObtenerNodosUsuarioSegunVisionProyectosTEC(null, (int)Session["UsuarioActual"], (sMostrarBitacoricos == "1") ? false : true, bSoloActivos);
            while (dr.Read())
            {
                sb.Append(dr["identificador"].ToString() + "@/@" + dr["denominacion"].ToString() + "@/@");
                sb.Append(Utilidades.escape(dr["t303_denominacion_CNP"].ToString()) + "@/@");
                sb.Append(Utilidades.escape(dr["t391_denominacion_CSN1P"].ToString()) + "@/@");
                sb.Append(Utilidades.escape(dr["t392_denominacion_CSN2P"].ToString()) + "@/@");
                sb.Append(Utilidades.escape(dr["t393_denominacion_CSN3P"].ToString()) + "@/@");
                sb.Append(Utilidades.escape(dr["t394_denominacion_CSN4P"].ToString()) + "@%@");
            }
            dr.Close();
            dr.Dispose();
            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al recargar nodos.", ex, false);
        }
    }

    private string setPreferencia(string sNodo, string sEstado, string sCategoria, string sIdCliente, string sIdResponsable, 
                                  string sNumPE, string sTipoBusqueda, string sActuAuto, string sCualidad, string sIdContrato, 
                                  string sIdHorizontal, string sDenominacion, string sCNP, string sCSN1P, string sCSN2P,
                                  string sCSN3P, string sCSN4P, string sIdNaturaleza, string sIdModeloContratacion)
    {
        string sResul = "";
        string strNodo = null, strEstado = null, strCategoria = null, strIdCliente = null, strIdResponsable = null, 
                strNumPE = null, strTipoBusqueda = null, strActuAuto = null, strCualidad = null, strIdContrato = null,
                strIdHorizontal = null, strDenominacion = null, strIdNaturaleza = null, strIdModeloContratacion = null;

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
            if (sNodo != "") strNodo = sNodo;
            if (sEstado != "") strEstado = sEstado;
            if (sCategoria != "") strCategoria = sCategoria;
            if (sIdCliente != "") strIdCliente = sIdCliente;
            if (sIdResponsable != "") strIdResponsable = sIdResponsable;
            if (sNumPE != "0" && sNumPE != "") strNumPE = sNumPE;
            if (sTipoBusqueda != "") strTipoBusqueda = sTipoBusqueda;
            if (sActuAuto != "") strActuAuto = sActuAuto;
            if (sCualidad != "") strCualidad = sCualidad;
            if (sIdContrato != "") strIdContrato = sIdContrato;
            if (sIdHorizontal != "") strIdHorizontal = sIdHorizontal;
            if (sDenominacion != "") strDenominacion = Utilidades.unescape(sDenominacion);
            if (sIdNaturaleza != "") strIdNaturaleza = sIdNaturaleza;
            if (sIdModeloContratacion != "") strIdModeloContratacion = sIdModeloContratacion;

            int nPref = PREFERENCIAUSUARIO.Insertar(tr, (int)Session["IDFICEPI_PC_ACTUAL"], 4, strNodo, strEstado, strCategoria, strIdCliente,
                                        sIdContrato, strIdResponsable, sIdHorizontal,  strNumPE, strActuAuto, strCualidad,
                                        strTipoBusqueda, strDenominacion,
                                        sCNP,
                                        sCSN1P,
                                        sCSN2P,
                                        sCSN3P,
                                        sCSN4P,
                                        strIdNaturaleza, strIdModeloContratacion, null, null);

            Conexion.CommitTransaccion(tr);

            sResul = "OK@#@" + nPref.ToString();
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al guardar la preferencia.", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    private string delPreferencia()
    {
        try
        {
            PREFERENCIAUSUARIO.DeleteAll(tr, (int)Session["IDFICEPI_PC_ACTUAL"], 4);
            return "OK@#@";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al eliminar la preferencia", ex);
        }
    }
    private string getPreferencia(string sIdPrefUsuario)
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            SqlDataReader dr = PREFERENCIAUSUARIO.Obtener(null, (sIdPrefUsuario == "") ? null : (int?)int.Parse(sIdPrefUsuario),
                                                            (int)Session["IDFICEPI_PC_ACTUAL"], 4);
            if (dr.Read())
            {
                bHayPreferencia = true;

                sb.Append(dr["idNodo"].ToString() + "@#@"); //0
                sb.Append(dr["t303_denominacion"].ToString() + "@#@"); //1
                sb.Append(dr["estado"].ToString() + "@#@"); //2
                sb.Append(dr["categoria"].ToString() + "@#@"); //3
                sb.Append(dr["idCliente"].ToString() + "@#@"); //4
                sb.Append(dr["t302_denominacion"].ToString() + "@#@"); //5
                sb.Append(dr["idContrato"].ToString() + "@#@"); //6
                sb.Append(dr["t377_denominacion"].ToString() + "@#@"); //7
                sb.Append(dr["idResponsable"].ToString() + "@#@"); //8
                sb.Append(dr["descResponsable"].ToString() + "@#@"); //9
                sb.Append(dr["idHorizontal"].ToString() + "@#@"); //10
                sb.Append(dr["t307_denominacion"].ToString() + "@#@"); //11
                sb.Append(dr["idProyecto"].ToString() + "@#@"); //12
                sb.Append(dr["t301_denominacion"].ToString() + "@#@"); //13
                sb.Append(dr["ActuAuto"].ToString() + "@#@"); //14
                sb.Append(dr["cualidad"].ToString() + "@#@"); //15
                sb.Append(dr["sTipoBusqueda"].ToString() + "@#@"); //16
                sb.Append(dr["sDenominacionProyecto"].ToString() + "@#@"); //17
                sb.Append(dr["t476_idcnp"].ToString() + "@#@"); //18
                sb.Append(dr["t476_denominacion"].ToString() + "@#@"); //19
                sb.Append(dr["t485_idcsn1p"].ToString() + "@#@"); //20
                sb.Append(dr["t485_denominacion"].ToString() + "@#@"); //21
                sb.Append(dr["t487_idcsn2p"].ToString() + "@#@"); //22
                sb.Append(dr["t487_denominacion"].ToString() + "@#@"); //23
                sb.Append(dr["t489_idcsn3p"].ToString() + "@#@"); //24
                sb.Append(dr["t489_denominacion"].ToString() + "@#@"); //25
                sb.Append(dr["t491_idcsn4p"].ToString() + "@#@"); //26
                sb.Append(dr["t491_denominacion"].ToString() + "@#@"); //27
                sb.Append(dr["idNaturaleza"].ToString() + "@#@"); //28
                sb.Append(dr["t323_denominacion"].ToString() + "@#@"); //29
                sb.Append(dr["t316_idmodalidad"].ToString() + "@#@"); //30
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la preferencia.", ex, false);
        }
    }
}
