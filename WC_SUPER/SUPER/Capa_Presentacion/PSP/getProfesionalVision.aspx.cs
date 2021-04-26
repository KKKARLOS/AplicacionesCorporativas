using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Text.RegularExpressions;

using SUPER.Capa_Negocio;

public partial class getProfesionalVision : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores = "";

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (Session["IDRED"] == null)
        {
            try
            {
                Response.Redirect("~/SesionCaducadaModal.aspx", true);
            }
            catch (System.Threading.ThreadAbortException) { return; }
        }

        //1º Se indican (por este orden) la función a la que se va a devolver el resultado
        //   y la función que va a acceder al servidor
        string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
        string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

        //if (!(bool)Session["FORANEOS"])
        //{
        //    this.imgForaneo.Visible = false;
        //    this.lblForaneo.Visible = false;
        //}
        gomaNodo.Attributes.Add("title", "Borra el " + Estructura.getDefLarga(Estructura.sTipoElem.NODO));
        this.lblNodo.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.NODO); ;
        this.lblNodo.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.NODO));
        if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
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
            cargarNodos();
        }

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
            case ("profesionales"):
                sResultado += getProfesionales(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5]);
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
    private void cargarNodos()
    {
        try
        {
            //Obtener los datos necesarios
            //Cargo la denominacion del label Nodo
            this.lblNodo.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
            this.lblNodo.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.NODO));

            //Cargar el combo de nodos accesibles
            ListItem oLI = null;
            SqlDataReader dr = NODO.ObtenerNodosUsuarioSegunVisionProyectosTEC(null, (int)Session["UsuarioActual"], false, false);
            while (dr.Read())
            {
                oLI = new ListItem(dr["denominacion"].ToString(), dr["identificador"].ToString());
                cboCR.Items.Add(oLI);
            }
            dr.Close();
            dr.Dispose();
        }
        catch (Exception ex)
        {
            this.sErrores += Errores.mostrarError("Error al cargar los nodos", ex);
        }
    }

    private string getProfesionales(string sAp1, string sAp2, string sNombre, string sNodo, string sBajas)
    {
        string sResul = "";
        bool bMostrarBajas = false;
        StringBuilder sb = new StringBuilder();
        int? iNodo = null;
        sb.Append("");
        sb.Append("<table id='tblDatos' class='texto MA' style='WIDTH: 600px;'>");
        sb.Append("<colgroup><col style='width:20px;'/><col style='width:580px;'/></colgroup>");

        try
        {
            if (sNodo != "")
                iNodo = int.Parse(sNodo);
            if (sBajas == "1")
                bMostrarBajas = true;

            SqlDataReader dr = USUARIO.GetProfVisibles((int)Session["UsuarioActual"], iNodo,
                                                       Utilidades.unescape(sAp1), Utilidades.unescape(sAp2), Utilidades.unescape(sNombre),
                                                       bMostrarBajas);
            while (dr.Read())
            {
                sb.Append("<tr id=" + dr["t314_idusuario"].ToString() + " style='DISPLAY: table-row; height:20px;'");
                //if (dr["t303_denominacion"].ToString() == "")
                //    sb.Append(" tipo ='E'");
                //else
                //    sb.Append(" tipo ='I'");
                sb.Append(" tipo ='" + dr["tipo"].ToString() + "'");
                sb.Append(" sexo ='" + dr["t001_sexo"].ToString() + "'");
                sb.Append(" baja ='" + dr["baja"].ToString() + "' ondblclick='aceptarClick(this.rowIndex)'>");//onclick='msse(this)' 
                sb.Append("<td></td>");
                //sb.Append("<td><nobr ondblclick='aceptarClick(this.parentNode.parentNode.rowIndex)' class='NBR W520' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["empresa"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\" >" + dr["profesional"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr ondblclick='aceptarClick(this.parentNode.parentNode.rowIndex)' class='NBR W520' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\" >" + dr["profesional"].ToString() + "</nobr></td>");
                //sb.Append("<td>" + dr["profesional"].ToString() + "</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</table>");
            sResul = "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + ex.ToString();
        }
        return sResul;
    }

}
