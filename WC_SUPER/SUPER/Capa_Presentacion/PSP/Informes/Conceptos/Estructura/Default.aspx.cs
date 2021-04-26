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


public partial class Capa_Presentacion_PSP_CONCEP_ESTRUC_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores;

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
                //rdbAmbito.Items[1].Text = Estructura.getDefCorta(Estructura.sTipoElem.NODO) + "&nbsp;&nbsp;&nbsp;";
                //this.lblCR.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
                //this.lblCR.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.NODO));
                //hdnNodoActual.Value = Request.QueryString["idNodo"].ToString();
                //hdnDesNodoActual.Value = Request.QueryString["desNodo"].ToString();
                cboNivelEstru.DataValueField = "T400_IDESTRUCTURA";
                cboNivelEstru.DataTextField = "T400_DENLARGA";
                cboNivelEstru.DataSource = Estructura.ListaActiva();
                cboNivelEstru.DataBind();
                cboNivelEstru.Items.Insert(0, new ListItem("", "0"));
                cboNivelEstru.SelectedValue = "0";


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
            case "SUBNODO":
            case "NODO":
            case "SNN1":
            case "SNN2":
            case "SNN3":
            case "SNN4":
                sResultado += obtenerEstructura(aArgs[0], (aArgs[1] == "1") ? true : false);
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

    private string obtenerEstructura(string sNivel, bool bMostrarInactivos)
    {
        string sResul = "";
        try
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            SqlDataReader dr = null;

            if (sNivel == "SUBNODO")
                dr = SUBNODO.ObtenerSubNodosUsuarioSegunVisionProyectosTEC(null, (int)Session["UsuarioActual"], false, !(bMostrarInactivos));
            else if (sNivel == "NODO")
                dr = NODO.ObtenerNodosUsuarioSegunVisionProyectosTEC(null, (int)Session["UsuarioActual"], false, !(bMostrarInactivos));
            else if (sNivel == "SNN1")
                dr = SUPERNODO1.ObtenerSuperNodo1UsuarioSegunVisionProyectosTEC(null, (int)Session["UsuarioActual"], false, !(bMostrarInactivos));
            else if (sNivel == "SNN2")
                dr = SUPERNODO2.ObtenerSuperNodo2UsuarioSegunVisionProyectosTEC(null, (int)Session["UsuarioActual"], false, !(bMostrarInactivos));
            else if (sNivel == "SNN3")
                dr = SUPERNODO3.ObtenerSuperNodo3UsuarioSegunVisionProyectosTEC(null, (int)Session["UsuarioActual"], false, !(bMostrarInactivos));
            else if (sNivel == "SNN4")
                dr = SUPERNODO4.ObtenerSuperNodo4UsuarioSegunVisionProyectosTEC(null, (int)Session["UsuarioActual"], false, !(bMostrarInactivos));

            sb.Append("<table id='tblDatos' class='texto MA' style='width: 350px;'>" + (char)10);
            sb.Append("<colgroup><col style='width:350px;' /></colgroup>" + (char)10);
            sb.Append("<tbody>");
            string sTootTip = "";
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["IDENTIFICADOR"].ToString() + "'");
                sb.Append("onclick='mm(event)' ondblclick='insertarItem(this)' onmousedown='DD(event)' ");
                sTootTip = "";
                switch (sNivel)
                {
                    case "SNN4":
                        if (Utilidades.EstructuraActiva("SN4")) sTootTip = "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO4) + ":</label> " + dr["DES_SN4"].ToString();
                        break;
                    case "SNN3":
                        if (Utilidades.EstructuraActiva("SN4")) sTootTip = "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO4) + ":</label> " + dr["DES_SN4"].ToString() + "<br>";
                        if (Utilidades.EstructuraActiva("SN3")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO3) + ":</label> " + dr["DES_SN3"].ToString();
                        break;
                    case "SNN2":
                        if (Utilidades.EstructuraActiva("SN4")) sTootTip = "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO4) + ":</label> " + dr["DES_SN4"].ToString() + "<br>";
                        if (Utilidades.EstructuraActiva("SN3")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO3) + ":</label> " + dr["DES_SN3"].ToString() + "<br>";
                        if (Utilidades.EstructuraActiva("SN2")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO2) + ":</label> " + dr["DES_SN2"].ToString();
                        break;
                    case "SNN1":
                        if (Utilidades.EstructuraActiva("SN4")) sTootTip = "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO4) + ":</label> " + dr["DES_SN4"].ToString() + "<br>";
                        if (Utilidades.EstructuraActiva("SN3")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO3) + ":</label> " + dr["DES_SN3"].ToString() + "<br>";
                        if (Utilidades.EstructuraActiva("SN2")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO2) + ":</label> " + dr["DES_SN2"].ToString() + "<br>";
                        if (Utilidades.EstructuraActiva("SN1")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO1) + ":</label> " + dr["DES_SN1"].ToString();
                        break;
                    case "NODO":
                        if (Utilidades.EstructuraActiva("SN4")) sTootTip = "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO4) + ":</label> " + dr["DES_SN4"].ToString() + "<br>";
                        if (Utilidades.EstructuraActiva("SN3")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO3) + ":</label> " + dr["DES_SN3"].ToString() + "<br>";
                        if (Utilidades.EstructuraActiva("SN2")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO2) + ":</label> " + dr["DES_SN2"].ToString() + "<br>";
                        if (Utilidades.EstructuraActiva("SN1")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO1) + ":</label> " + dr["DES_SN1"].ToString() + "<br>";
                        sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label> " + dr["DES_NODO"].ToString();
                        break;
                    case "SUBNODO":
                        if (Utilidades.EstructuraActiva("SN4")) sTootTip = "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO4) + ":</label> " + dr["DES_SN4"].ToString() + "<br>";
                        if (Utilidades.EstructuraActiva("SN3")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO3) + ":</label> " + dr["DES_SN3"].ToString() + "<br>";
                        if (Utilidades.EstructuraActiva("SN2")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO2) + ":</label> " + dr["DES_SN2"].ToString() + "<br>";
                        if (Utilidades.EstructuraActiva("SN1")) sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO1) + ":</label> " + dr["DES_SN1"].ToString() + "<br>";
                        sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label> " + dr["DES_NODO"].ToString() + "<br>";
                        sTootTip += "<label style='width:60px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUBNODO) + ":</label> " + dr["DES_SUBNODO"].ToString();
                        break;
                }

                sb.Append("style='height:16px;noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../../images/info.gif' style='vertical-align:middle'>  Estructura] body=[" + sTootTip + "] hideselects=[off]\">");
                sb.Append("<td style='padding-left:5px; cursor:pointer;' >" + dr["DENOMINACION"].ToString() + "</td>");
                sb.Append("</tr>" + (char)10);
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            sResul = "OK@#@" + sb.ToString();
        }
        catch (System.Exception objError)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al leer el catálogo nivel: " + sNivel, objError);
        }
        return sResul;
    }
}
