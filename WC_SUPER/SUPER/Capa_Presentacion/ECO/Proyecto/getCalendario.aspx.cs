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

namespace SUPER.Capa_Presentacion.ECO.Proyecto
{
	/// <summary>
	/// Descripción breve de obtenerRecurso.
	/// </summary>
    public partial class getCalendario : System.Web.UI.Page, ICallbackEventHandler
	{
        private string _callbackResultado = null;
        public string sErrores="";
        public string strTablaHtml;
        public string strTitulo, strColumna="Denominación";

		private void Page_Load(object sender, System.EventArgs e)
		{
            try
            {
                if (!Page.IsPostBack){
                    if (Session["IDRED"] == null)
                    {
                        try
                        {
                            Response.Redirect("~/SesionCaducadaModal.aspx", true);
                        }
                        catch (System.Threading.ThreadAbortException) { return; }
                    }

                    if (Request.QueryString["nodo"] != null)
                        this.hdnIdNodo.Value = Request.QueryString["nodo"].ToString();

                    if (Request.QueryString["nOpcion"] != null)
                    {
                        if (Request.QueryString["nOpcion"].ToString() == "1") //Solo calendarios empresariales
                        {
                            rdbTipoBusqueda.Items[0].Selected = true;
                            rdbTipoBusqueda.Enabled = false;
                            string strTabla = ObtenerCalendarios("E", "", "0");
                            string[] aTabla = Regex.Split(strTabla, "@#@");
                            if (aTabla[0] == "OK") this.strTablaHtml = aTabla[1];
                            else sErrores += Errores.mostrarError(aTabla[1]);

                        }
                    }
                    //1º Se indican (por este orden) la función a la que se va a devolver el resultado
                    //   y la función que va a acceder al servidor
                    string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
                    string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
                    //2º Se "registra" la función que va a acceder al servidor.
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
                }
            }
            catch (Exception ex)
            {
                sErrores = Errores.mostrarError("Error al obtener los calendarios.", ex);
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
                case ("getCal"):
                    sResultado += ObtenerCalendarios(aArgs[1], aArgs[2], aArgs[3]);
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

        private string ObtenerCalendarios(string sTipo, string sIdFicepi, string sNodo)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                SqlDataReader dr = null;

                sb.Append("<table id='tblDatos' class='texto MA' style='width: 400px;'>");
                sb.Append("<colgroup><col  style='width: 400px;' /></colgroup>");
                sb.Append("<tbody>");
                if (sTipo == "E") dr = Calendario.Catalogo(null, "", true, "E", null, "", null, 2, 0);
                else
                {
                    if (sNodo == "") sNodo = "-1";
                    dr = Calendario.Asignacion(int.Parse(sNodo), int.Parse(sIdFicepi));
                }
                while (dr.Read())
                {
                    sb.Append("<tr id='" + dr["t066_idcal"].ToString() + "' onclick='ms(this)' ondblclick='aceptarClick(this.rowIndex)' onmouseover='TTip(event);' style='height:16px'>");
                    sb.Append("<td style='padding-left:5px;'><nobr style='width:380px;' class='NBR'>" + dr["t066_descal"].ToString() + "</nobr></td></tr>");
                }
                dr.Close();
                dr.Dispose();
                sb.Append("</tbody>");
                sb.Append("</table>");

                return "OK@#@" + sb.ToString();
            }
            catch (Exception ex)
            {
                return "Error@#@" + Errores.mostrarError("Error al obtener los Calendarios", ex);
            }
        }
    }
}
