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
using Microsoft.JScript;
using GESTAR.Capa_Negocio;

namespace GESTAR.Capa_Presentacion
{
	/// <summary>
	/// Descripción breve de obtenerRecurso.
	/// </summary>
    public partial class obtenerProfesional : System.Web.UI.Page, ICallbackEventHandler
    {
        private string _callbackResultado = null;
        public string sPerfil = "";

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

            //2º Se "registra" la función que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }

        public void RaiseCallbackEvent(string eventArg)
        {
            string sResultado = "";
            //1º Si hubiera argumentos, se recogen y tratan.
            //string MisArg = eventArg;
            string[] aArgs = Regex.Split(eventArg, "@@");
            sResultado = aArgs[0] + @"@@";

            //2º Aquí realizaríamos el acceso a BD, etc,...
            switch (aArgs[0])
            {
                case ("profesionales"):
                    sResultado += obtenerRecursosReconexion(Microsoft.JScript.GlobalObject.unescape(aArgs[1]), Microsoft.JScript.GlobalObject.unescape(aArgs[2]), Microsoft.JScript.GlobalObject.unescape(aArgs[3]));
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
            StringBuilder strBuilder = new StringBuilder();
            int i = 0;

            strBuilder.Append("<table id='tblDatos' class='texto' style='width: 396px; BORDER-COLLAPSE: collapse; ' cellSpacing='0' border='0'>");
            strBuilder.Append("<colgroup><col style='width:40px;' /><col style='width:340px;padding-left:5px;' /></colgroup>");

            try
            {

		        SqlDataReader dr = Recursos.CargarRecursos(sAp1, sAp2, sNombre, 1, 0);

                while (dr.Read())
                {
                    if (i % 2 == 0) strBuilder.Append("<tr class=FA ");
                    else strBuilder.Append("<tr class=FB ");

                    strBuilder.Append("id='" + dr["num_empleado"].ToString() + "' codred='" + dr["T001_CODRED"].ToString() + "' onclick=\"ms(this)\" ondblclick=\"aceptarClick(this.rowIndex)\" onmouseover=TTip(event);>");

                    strBuilder.Append("<td style='text-align:right;padding-right:5px;'>" + dr["num_empleado"].ToString() + "</td><td><nobr style='width:340px;' class='NBR'>" + dr["tecnico"].ToString() + "</nobr></td>");

                    strBuilder.Append("</tr>");
                    i++;
                }
                dr.Close();
                dr.Dispose();

                strBuilder.Append("</table>");
                sResul = "OK@@"+ strBuilder.ToString();
            }
            catch (Exception ex)
            {
                sResul = "Error@@" + ex.ToString();
            }
            return sResul;
        }

	}
}
