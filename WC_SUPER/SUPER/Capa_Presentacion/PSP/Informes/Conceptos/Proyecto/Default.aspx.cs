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
using SUPER.Capa_Negocio;
using System.Text;
using System.Text.RegularExpressions;


public partial class Capa_Presentacion_PSP_Conceptos_Proyecto_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores, sNodoFijo = "0", sMostrarBitacoricos = "0", sModulo = "";

    protected void Page_Load(object sender, EventArgs e)
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

            sErrores = "";
            try
            {
                sModulo = Request.QueryString["sMod"].ToString().ToLower();
                if (sModulo == "pge" || sModulo == "cm")
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

                if (Request.QueryString["cualidad"] != null)
                {
                    cboCualidad.SelectedValue = Request.QueryString["cualidad"].ToString();
                    if (Request.QueryString["habCualidad"]==null) cboCualidad.Enabled = false;
                }
                //Cargo la denominacion del label Nodo
                string sAux = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
                if (sAux.Trim() != "")
                {
                    this.lblNodo.InnerText = sAux;
                    this.lblNodo.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.NODO));
                    this.gomaNodo.Attributes.Add("title", "Borra el " + Estructura.getDefLarga(Estructura.sTipoElem.NODO));
                    //this.lblNodo2.InnerText = sAux;
                    //this.lblNodo2.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.NODO));
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
                bool bEsAdminProduccion = SUPER.Capa_Negocio.Utilidades.EsAdminProduccion();
                if (bEsAdminProduccion)
                {
                    cboCR.Visible = false;
                    hdnIdNodo.Visible = true;
                    txtDesNodo.Visible = true;
                    gomaNodo.Visible = true;
                }
                else
                {
                    cboCR.Visible = true;
                    hdnIdNodo.Visible = false;
                    txtDesNodo.Visible = false;
                    gomaNodo.Visible = false;
                    cargarNodos(sModulo);
                }
                if (bEsAdminProduccion)
                {
                    if (hdnIdNodo.Text != "")
                    {
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
                rdbTipoBusqueda.Items[1].Selected = true;
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
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case "PROYECTOS":
                sResultado += ObtenerProyectos(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8], aArgs[9], aArgs[10], aArgs[11], aArgs[12], aArgs[19]);
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
    private void cargarNodos(string sModulo)
    {
        try
        {
            //Cargar el combo de nodos accesibles
            ListItem oLI = null;
            SqlDataReader dr;
            if (sModulo.ToLower() == "pge")
                dr = NODO.ObtenerNodosUsuarioSegunVisionProyectosECO(null, (int)Session["UsuarioActual"], false);
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
    private string ObtenerProyectos(string sModulo, string sNodo, string sEstado, string sCategoria, string sIdCliente, 
                                    string sIdResponsable, string sNumPE, string sDesPE, string sTipoBusqueda, 
                                    string sCualidad, string sIDContrato, string sIdHorizontal, string sModeloContratacion)
    {
        string sResul = "";
        try
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            SqlDataReader dr = PROYECTO.ObtenerProyectos
            (
                sModulo,
                (sNodo != "") ? (int?)int.Parse(sNodo) : null,
                sEstado,
                sCategoria,
                (sIdCliente != "") ? (int?)int.Parse(sIdCliente) : null,
                (sIdResponsable != "") ? (int?)int.Parse(sIdResponsable) : null,
                (sNumPE != "" && sNumPE != "0") ? (int?)int.Parse(sNumPE) : null,
                Utilidades.unescape(sDesPE),
                sTipoBusqueda,
                sCualidad,
                (sIDContrato != "") ? (int?)int.Parse(sIDContrato) : null,
                (sIdHorizontal != "") ? (int?)int.Parse(sIdHorizontal) : null,
                (int)Session["UsuarioActual"],
                false, false, null, null, null, null, null, false, null,
                (sModeloContratacion != "") ? (int?)int.Parse(sModeloContratacion) : null,
                null, null
            );

            sb.Append("<table id='tblDatos' class='texto MAM' style='WIDTH: 450px;'>" + (char)10);
            sb.Append("<colgroup>" + (char)10);
            sb.Append("<col style='width:20px' />");
            sb.Append("<col style='width:20px' />");
            sb.Append("<col style='width:20px' />");
            sb.Append("<col style='width:50px;' />");
            sb.Append("<col style='width:340px;' />");
            sb.Append("</colgroup>" + (char)10);

            //int i = 0;
            //bool bExcede = false;
            while (dr.Read())
            {
                if (sModulo != "pge" && sModulo != "cm" && dr["t305_cualidad"].ToString() == "J") continue;

                sb.Append("<tr id='" + dr["t305_idproyectosubnodo"].ToString() + "'");
                sb.Append("categoria='" + dr["t301_categoria"].ToString() + "' ");
                sb.Append("cualidad='" + dr["t305_cualidad"].ToString() + "' ");
                sb.Append("estado='" + dr["t301_estado"].ToString() + "' ");

                //sb.Append("ondblclick='insertarItem(this)' onclick='mmse(this)' onmousedown='DD(this)' ");
                sb.Append("style='height:20px'>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td");
                if (ConfigurationManager.AppSettings["MOSTRAR_MOTIVO_PROY"] == "1")
                    sb.Append(" title=\"" + dr["desmotivo"].ToString() + "\"");
                sb.Append(" style='text-align:right; padding-right:5px;'>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + "</td>");
                sb.Append("<td><div class='NBR W320' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Proyecto:</label>" + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Responsable:</label>" + dr["responsable"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Cliente:</label>" + dr["t302_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\" ondblclick='mdpsn(this)'>" + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "</div></td>");
                sb.Append("</tr>" + (char)10);
                //i++;
                //if (i > Constantes.nNumMaxTablaCatalogoProyectos)
                //{
                //    //bExcede = true;
                //    break;
                //}
            }
            dr.Close();
            dr.Dispose();
            //if (!bExcede)
            //{
                sb.Append("</table>");
            //}
            //else
            //{
            //    sb.Length = 0;
            //    sb.Append("EXCEDE");
            //}

            sResul = "OK@#@" + sb.ToString();
        }
        catch (System.Exception objError)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al leer obtenerProyectos ", objError);
        }
        return sResul;
    }
}
