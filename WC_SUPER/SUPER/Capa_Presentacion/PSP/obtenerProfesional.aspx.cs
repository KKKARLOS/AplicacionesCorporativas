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

namespace SUPER.Capa_Presentacion
{
	/// <summary>
	/// Descripción breve de obtenerProfesional
	/// </summary>
    public partial class obtenerProfesional : System.Web.UI.Page, ICallbackEventHandler
    {
        private string _callbackResultado = null;

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

            //if (!(bool)Session["FORANEOS"])
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
                    sResultado += obtenerRecursosReconexion(aArgs[1], aArgs[2], aArgs[3]);
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

        private string obtenerRecursosReconexion(string sAp1, string sAp2, string sNombre)
        {
            string sResul = "";
            StringBuilder sb = new StringBuilder();
            int i = 0;
            //sb.Append("<div style='background-image:url(../../Images/imgFT20.gif); width:396px'>");
            sb.Append("<table id='tblDatos' class='texto MA' style='width: 396px;'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:376px;' /></colgroup>");
            sb.Append("<tbody>");
            try
            {
                SqlDataReader dr = Recurso.ObtenerProfesionales(Utilidades.unescape(sAp1), Utilidades.unescape(sAp2), Utilidades.unescape(sNombre));

                while (dr.Read())
                {
                    sb.Append("<tr style='height:20px;noWrap:true;' id='" + dr["num_empleado"].ToString() + "' codred='" + dr["T001_CODRED"].ToString() + "'");
                    sb.Append("tipo='" + dr["tipo"].ToString() + "' ");
                    sb.Append("sexo='" + dr["t001_sexo"].ToString() + "' ");
                    //sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["tecnico"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["num_empleado"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["EMPRESA"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\" ");
                    sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["tecnico"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["num_empleado"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\" ");
                    sb.Append(" onclick=\"ms(this)\" ondblclick=\"aceptarClick(this.rowIndex)\" >");
                    sb.Append("<td></td><td><span style='width:370px;' class='NBR'>" + dr["tecnico"].ToString() + "</span></td>");
                    sb.Append("</tr>");
                    i++;
                }
                dr.Close();
                dr.Dispose();
                sb.Append("</tbody>");
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
}
