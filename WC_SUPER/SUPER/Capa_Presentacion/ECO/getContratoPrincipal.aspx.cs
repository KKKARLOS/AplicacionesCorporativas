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

	/// <summary>
    /// Descripción breve de obtenerContrato
	/// </summary>
    public partial class getContratoPrincipal : System.Web.UI.Page, ICallbackEventHandler
    {
        private string _callbackResultado = null;
        public string strErrores;

		private void Page_Load(object sender, System.EventArgs e)
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
                case ("contrato"):
                    sResultado += getContratos(aArgs[1], aArgs[2]);
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

        private string getContratos(string sTipoBusqueda, string strContrato)
        {
            string sResul = "";
            try
            {
                StringBuilder strBuilder = new StringBuilder();

                strBuilder.Append("<table id='tblDatos' class='texto MA' style='width:500px;'>");
                SqlDataReader dr = CONTRATO.ObtenerExtensionPadreByNombre(strContrato, sTipoBusqueda);

                while (dr.Read())
                {
                    strBuilder.Append("<tr id='" + dr["t306_idcontrato"].ToString() + "' IdCliente='" + dr["t302_idcliente"].ToString() + "' Cliente='" + dr["t302_denominacion"].ToString() + "' style='height:16px;' onclick='ms(this)' ondblclick='aceptarClick(this.rowIndex)' >");
                    strBuilder.Append("<td style='padding-left:5px;'>" + dr["t377_denominacion"].ToString() + "</td></tr>");
                }
                dr.Close();
                dr.Dispose();
                strBuilder.Append("</table>");

                sResul = "OK@#@" + strBuilder.ToString();
            }
            catch (Exception ex)
            {
                sResul = "Error@#@" + Errores.mostrarError("Error al obtener los contratos", ex);
            }
            return sResul;
        }
	}
