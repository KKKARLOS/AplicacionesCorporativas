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

public partial class getProfesional : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores = "";

    protected void Page_Load(object sender, System.EventArgs e)
    {
        //Session.Clear();
        //Session.Abandon();
        try
        {
            if (Session["IDRED"] == null)
            {
                Response.Redirect("~/SesionCaducadaModal.aspx", true);
                return;
            }
            if (Request.QueryString["T"] != null)
            {
                this.hdnTipoProf.Value = Request.QueryString["T"].ToString();
                if (this.hdnTipoProf.Value=="CP")
                    Page.Title = " ::: SUPER ::: - Selección de comerciales preventa";
            }
        }
        catch (System.Threading.ThreadAbortException) { return; }


        //En esta pantalla, la posibilidad de ver foráneos es un mix de la configuración general
        //y si la llamada al formulario indica si se deben ver foráneos o no
        //bool bVerForaneos = (bool)Session["FORANEOS"];//Esto es la parametrización general de la aplicación
        //if (Request.QueryString["F"] != null)//Esto indica si el proceso llamante quiere ver foráneos
        //{
        //    this.hdnForaneos.Value = Request.QueryString["F"].ToString();
        //    if (this.hdnForaneos.Value == "N")
        //        bVerForaneos = false;
        //}
        //else//Si no se pasa parámetro, por defecto no mostramos foráneos
        //    bVerForaneos = false;

        //if (!bVerForaneos)
        //{
        //    this.imgForaneo.Visible = false;
        //    this.lblForaneo.Visible = false;
        //}
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
            case ("profesionales"):
                sResultado += obtenerUsuarios(aArgs[1], aArgs[2], aArgs[3], this.hdnForaneos.Value, this.hdnTipoProf.Value);
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

    private string obtenerUsuarios(string sAp1, string sAp2, string sNombre, string sForaneos, string sTipoProfesional)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();
        bool bForaneos = false;
        if (sForaneos == "S") bForaneos = true;
        sb.Append("<table id='tblDatos' class='texto MA' style='width:420px;'><colgroup><col style='width:20px;' /><col style='width:400px;' /></colgroup>");
        //sb.Append("<tbody>");
        try
        {
            SqlDataReader dr;
            if (sTipoProfesional == "CP")//Comerciales preventa (TA218)
                dr = USUARIO.ObtenerComercialesPreventa(Utilidades.unescape(sAp1), Utilidades.unescape(sAp2),
                                                   Utilidades.unescape(sNombre));
            else
                dr = USUARIO.ObtenerUsuarioActivos(Utilidades.unescape(sAp1), Utilidades.unescape(sAp2),
                                                   Utilidades.unescape(sNombre), bForaneos);

            while (dr.Read())
            {
                sb.Append("<tr style='noWrap:true; height:18px' tipo='" + dr["tipo"].ToString() + "' sexo='" + dr["t001_sexo"].ToString() + "'");
                sb.Append(" id='" + dr["t314_idusuario"].ToString() + "' idficepi='" + dr["t001_idficepi"].ToString() + "'");
                sb.Append(" onclick='ms(this)' ondblclick='aceptarClick(this.rowIndex)'");
                sb.Append(" title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../images/info.gif' style='vertical-align:middle'> ");
                sb.Append("Información] body=[Profesional:&nbsp;");
                sb.Append(int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###"));
                sb.Append(" - " + dr["Profesional"].ToString().Replace((char)34, (char)39) + "<br>");
                sb.Append(Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                sb.Append(dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">");
                if (sTipoProfesional == "CP")
                {
                    if (dr["baja"].ToString() == "1")
                        sb.Append("<td></td><td style='color:red;'>" + dr["Profesional"].ToString() + "</td></tr>");
                    else
                        sb.Append("<td></td><td>" + dr["Profesional"].ToString() + "</td></tr>");
                }
                else
                    sb.Append("<td></td><td>" + dr["Profesional"].ToString() + "</td></tr>");
            }
            dr.Close();
            dr.Dispose();
            //sb.Append("</tbody>");
            sb.Append("</table>");
            sResul = "OK@#@"+ sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + ex.ToString();
        }
        return sResul;
    }

}
