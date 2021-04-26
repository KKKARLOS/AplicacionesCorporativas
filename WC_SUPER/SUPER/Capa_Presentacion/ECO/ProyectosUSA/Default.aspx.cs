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
using EO.Web;
using SUPER.Capa_Negocio;
using System.Text.RegularExpressions;
using System.Text;


public partial class USA : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaProyectos;
    public bool bEsSAT = false, bEsSAA = false;
    public SqlConnection oConn;
    public SqlTransaction tr;
	
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.TituloPagina = "Relación de proyectos";
            Master.bFuncionesLocales = true;
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");

            try
            {
                bEsSAT = USUARIO.bEsSAT(null, (int)Session["UsuarioActual"]);
                bEsSAA = USUARIO.bEsSAA(null, (int)Session["UsuarioActual"]);

                if (Session["FiltroProyectosUSA"] != null)
                {
                    string[] aFiltros = (string[])Session["FiltroProyectosUSA"];
                    chkSAT.Checked = (aFiltros[0] == "1") ? true : false;
                    chkSAA.Checked = (aFiltros[1] == "1") ? true : false;
                    chkExternalizable.Checked = (aFiltros[2] == "1") ? true : false;
                    chkExternalizado.Checked = (aFiltros[3] == "1") ? true : false;
                }
            }
            catch (Exception ex)
            {
                Master.sErrores = Errores.mostrarError("Error al cargar los datos", ex);
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
            case ("getProyectos"):
                sResultado += ObtenerProyectos(aArgs[1], aArgs[2], aArgs[3], aArgs[4]);
                Session["FiltroProyectosUSA"] = new string[] { aArgs[1], aArgs[2], aArgs[3], aArgs[4] };
                break;
            case ("setPSN"):
                Session["ID_PROYECTOSUBNODO"] = aArgs[1];
                Session["MODOLECTURA_PROYECTOSUBNODO"] = false;
                Session["RTPT_PROYECTOSUBNODO"] = false;
                Session["MONEDA_PROYECTOSUBNODO"] = aArgs[2];

                sResultado += "OK";
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
    protected string ObtenerProyectos(string sSAT, string sSAA, string sExternalizable, string sExternalizado)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = PROYECTOSUBNODO.ObtenerProyectosRelacionUSA((int)Session["UsuarioActual"], (sSAT == "1") ? true : false, (sSAA == "1") ? true : false, (sExternalizable == "1") ? true : false, (sExternalizado == "1") ? true : false);

            sb.Append("<table id='tblDatos' class='texto MA' style='width: 970px;'>");
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:20px;' />");
            sb.Append("    <col style='width:20px;' />");
            sb.Append("    <col style='width:20px;' />");
            sb.Append("    <col style='width:65px; ' />");
            sb.Append("    <col style='width:295px;' />");
            sb.Append("    <col style='width:150px;' />");
            sb.Append("    <col style='width:150px;' />");
            sb.Append("    <col style='width:150px;' />");
            sb.Append("    <col style='width:100px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");

            while (dr.Read())
            {
                sb.Append("<tr idPSN='" + dr["t305_idproyectosubnodo"].ToString() + "' ");
                sb.Append("idProy='" + dr["t301_idproyecto"].ToString() + "' ");
                sb.Append("categoria='" + dr["t301_categoria"].ToString() + "' ");
                sb.Append("cualidad='" + dr["t305_cualidad"].ToString() + "' ");
                sb.Append("estado='" + dr["t301_estado"].ToString() + "' ");
                sb.Append("nodo=\"" + Utilidades.escape(dr["t303_denominacion"].ToString()) + "\" ");
                sb.Append("responsable=\"" + Utilidades.escape(dr["responsable"].ToString()) + "\" ");
                sb.Append("moneda_proyecto='" + dr["t422_idmoneda_proyecto"].ToString() + "' ");
                sb.Append("ondblclick='setPSN(this)' style='height:20px' >");

                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td style='text-align:right; padding-right:10px;'>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + "</td>");
                sb.Append("<td><nobr class='NBR W290' ondblclick='setPSN(this)' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Proyecto:</label>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " - " + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Responsable:</label>" + dr["responsable"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Cliente:</label>" + dr["t302_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>SAT:</label>" + dr["denSAT"].ToString() + "<br><label style='width:70px;'>SAA:</label>" + dr["denSAA"].ToString() + "] hideselects=[off]\">" + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W140' ondblclick='setPSN(this)'>" + dr["t302_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W140' ondblclick='setPSN(this)'>" + dr["denSAT"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W140' ondblclick='setPSN(this)'>" + dr["denSAA"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W90' ondblclick='setPSN(this)'>" + dr["UMC"].ToString() + "</nobr></td>");

                sb.Append("</tr>");
            }

            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            sResul = "OK@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener la relación de proyectos.", ex, false);
        }

        return sResul;
    }
}
