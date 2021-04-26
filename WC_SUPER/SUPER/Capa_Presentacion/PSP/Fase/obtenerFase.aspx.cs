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
    /// Pantalla de selección de proyectos técnicos
	/// </summary>
public partial class obtenerFase : System.Web.UI.Page, ICallbackEventHandler
    {
        private string _callbackResultado = null;
        public string strErrores, sDesPE,sDesPT,sAux;
        public int nPE,nPT;

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

                //Recojo el codigo y descripción de proyecto económico
                sAux = Request.QueryString["nPE"].ToString();
                this.txtNumPE.Text = sAux;
                sAux = sAux.Replace(".", "");
                nPE = int.Parse(sAux);
                sDesPE = Request.QueryString["sPE"].ToString();
                this.txtPE.Text = sDesPE;
                //Recojo el codigo y descripción de proyecto técnico
                sAux = Request.QueryString["nPT"].ToString();
                this.txtNumPT.Text = sAux;
                sAux = sAux.Replace(".", "");
                nPT = int.Parse(sAux);
                sDesPT = Request.QueryString["sPT"].ToString();
                this.txtPT.Text = sDesPT;
            }
            catch (Exception ex)
            {
                strErrores = Errores.mostrarError("Error al obtener las fases", ex);
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
            string[] aArgs = Regex.Split(eventArg, "@#@");
            sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

            //2º Aquí realizaríamos el acceso a BD, etc,...
            switch (aArgs[0])
            {
                case ("F"):
                    sResultado += ObtenerFases(aArgs[2], aArgs[1], aArgs[3]);
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

        private string ObtenerFases(string sTipoBusqueda, string num_proy_tec, string strNomFase)
        {
            string sResul = "",sPT;
            int nPT;
            try
            {
                StringBuilder strBuilder = new StringBuilder();

                sPT=num_proy_tec.Replace(".","");
                nPT = int.Parse(sPT);
                strBuilder.Append("<table id='tblDatos' class='texto MA' style='width: 410px;'>");
                strBuilder.Append("<colgroup><col style='width;410px;'></colgroup>");
                strBuilder.Append("<tbody>");
                SqlDataReader dr = FASEPSP.Catalogo(null, strNomFase, nPT, 2, 0, sTipoBusqueda);
                while (dr.Read())
                {
                    strBuilder.Append("<tr id='" + dr["t334_idfase"].ToString() + "' onclick='ms(this)' ondblclick='aceptarClick(this.rowIndex)' >");
                    strBuilder.Append("<td>" + dr["t334_desfase"].ToString() + "</td>");
                    strBuilder.Append("</tr>");
                }
                dr.Close();
                dr.Dispose();
                strBuilder.Append("</tbody>");
                strBuilder.Append("</table>");

                sResul = "OK@#@" + strBuilder.ToString();
            }
            catch (Exception ex)
            {
                sResul = "Error@#@" + Errores.mostrarError("Error al obtener las fases", ex);
            }
            return sResul;
        }
	}

