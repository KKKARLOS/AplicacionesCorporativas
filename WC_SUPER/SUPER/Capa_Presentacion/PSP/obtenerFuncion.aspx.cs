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
	/// Descripci�n breve de obtenerFuncion
	/// </summary>
    public partial class obtenerFuncion : System.Web.UI.Page, ICallbackEventHandler
	{
        private string _callbackResultado = null;
        public string strErrores, sErrores;
        public string strTablaHtml;

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

                cargarNodos();
            }
            catch (Exception ex)
            {
                strErrores = Errores.mostrarError("Error al obtener los grupos funcionales", ex);
            }
            //1� Se indican (por este orden) la funci�n a la que se va a devolver el resultado
            //   y la funci�n que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

            //2� Se "registra" la funci�n que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
        public void RaiseCallbackEvent(string eventArg)
        {
            string sResultado = "";
            //1� Si hubiera argumentos, se recogen y tratan.
            string[] aArgs = Regex.Split(eventArg, "@#@");
            sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
            //2� Aqu� realizar�amos el acceso a BD, etc,...
            switch (aArgs[0])
            {
                case ("getNodo"):
                    sResultado += listaFunciones(aArgs[1]);
                    break;
            }
            //3� Damos contenido a la variable que se env�a de vuelta al cliente.
            _callbackResultado = sResultado;
        }
        public string GetCallbackResult()
        {
            //Se env�a el resultado al cliente.
            return _callbackResultado;
        }
        private string listaFunciones(string sCR)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<table id='tblDatos' class='texto MA' style='width: 396px;'>");
            sb.Append("<colgroup><col style='width:396px;' /></colgroup>");
            sb.Append("<tbody>");
            SqlDataReader dr = FUNCIONES.Catalogo(null, "", short.Parse(sCR), 2, 0);
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t356_idfuncion"].ToString() + "' onclick='ms(this)' ondblclick='aceptarClick(this.rowIndex)' onmouseover=TTip(event);>");
                sb.Append("<td><span style='width:390px;' class='NBR'>" + dr["t356_desfuncion"].ToString() + "</span></td></tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            strTablaHtml = sb.ToString();
            return "OK@#@" + sb.ToString();
        }
        private void cargarNodos()
        {
            try
            {
                bool bSeleccionado = false;
                //Cargo la denominacion del label Nodo
                this.lblNodo.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
                this.lblNodo.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.NODO));
                //Cargar el combo de nodos accesibles
                ListItem oLI = null;
                SqlDataReader dr = NODO.ObtenerNodosUsuarioSegunVisionProyectosTEC(null, (int)Session["UsuarioActual"], false, false);
                while (dr.Read())
                {
                    //oLI = new ListItem(dr["Denominacion"].ToString(), dr["ID"].ToString());
                    oLI = new ListItem(dr["denominacion"].ToString(), dr["identificador"].ToString());
                    if (!bSeleccionado)
                    {
                        oLI.Selected = true;
                        bSeleccionado = true;
                        listaFunciones(dr["identificador"].ToString());
                    }
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
	}
}
