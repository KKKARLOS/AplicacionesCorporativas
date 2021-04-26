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
using SUPER.Capa_Negocio;
//Para el stringbuilder
using System.Text;
using System.Text.RegularExpressions;

namespace SUPER.Capa_Presentacion
{
    /// <summary>
    /// Descripción breve de obtenerRecurso.
    /// </summary>
    public partial class obtenerFotos : System.Web.UI.Page, ICallbackEventHandler
    {
        private string _callbackResultado = null;
        public string strErrores, strTablaHtml;

        private void Page_Load(object sender, System.EventArgs e)
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

                strTablaHtml = listaFotos(int.Parse(Utilidades.decodpar(Request.QueryString["p"].ToString())));
            }
            catch (Exception ex)
            {
                strErrores = Errores.mostrarError("Error al obtener las fotos", ex);
            }

            //1º Se indican (por este orden) la función a la que se va a devolver el resultado
            //   y la función que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

            //2º Se "registra" la función que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }

        public string listaFotos(int nPE)
        {
            //this.rptOpciones.DataSource = FOTOSEGPE.Catalogo(null, null, null, nPE, null, 2, 1);
            //this.rptOpciones.DataBind();
            try
            {
                StringBuilder sb = new StringBuilder();
                SqlDataReader dr = null;

                sb.Append("<table id='tblOpciones' class='texto MA' style='width:500px;'>");
                sb.Append("<colgroup><col style='width:120px;' /><col style='width:280px;' /><col style='width:100px;' /></colgroup>");
                sb.Append("<tbody>");
                dr = FOTOSEGPE.Catalogo(null, null, null, nPE, null, 2, 1);
                while (dr.Read())
                {
                    sb.Append("<tr id='" + dr["T373_idFotoPE"].ToString() + "' ");
                    sb.Append("usuario='" + dr["t314_idusuario"].ToString() + "' onclick='ms(this)' ");
                    sb.Append(" ondblclick='aceptarClick(this.rowIndex)' ");
                    sb.Append(" style='height:16px;'>");
                    sb.Append("<td style='padding-left:3px;'>" + dr["T373_fecha"].ToString() + "</td>");
                    sb.Append("<td><nobr class='NBR W280'>" + dr["Autor"].ToString() + "</nobr></td>");
                    sb.Append("<td style='text-align:right; padding-right:5px;'>" + dr["fecIAP"].ToString() + "</td></tr>");
                }
                dr.Close();
                dr.Dispose();
                sb.Append("</tbody>");
                sb.Append("</table>");

                return sb.ToString();
            }
            catch (Exception ex)
            {
                return "Error@#@" + Errores.mostrarError("Error al obtener las instantáneas", ex);
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
                case ("eliminar"):
                    sResultado += eliminarFoto(aArgs[1]);
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

        protected string eliminarFoto(string sIDFotoPE)
        {
            string sResul = "";
            try
            {
                int nResul = FOTOSEGPE.Delete(null, int.Parse(sIDFotoPE));

                sResul = "OK";
            }
            catch (Exception ex)
            {
                sResul = "Error@#@" + Errores.mostrarError("Error al eliminar la foto de proyecto económico", ex);
            }
            return sResul;
        }

    }
}
