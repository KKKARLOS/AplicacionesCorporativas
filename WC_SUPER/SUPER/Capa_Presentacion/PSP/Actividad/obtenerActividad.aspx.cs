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
public partial class obtenerActividad : System.Web.UI.Page, ICallbackEventHandler
    {
        private string _callbackResultado = null;
        public string strErrores, sDesPE,sDesPT,sDesFase,sAux;
        public int nPE, nPT, nFase;

		private void Page_Load(object sender, System.EventArgs e)
		{
            try
            {
                //Recojo el codigo y descripción de proyecto económico
                if (Session["IDRED"] == null)
                {
                    try
                    {
                        Response.Redirect("~/SesionCaducadaModal.aspx", true);
                    }
                    catch (System.Threading.ThreadAbortException) { return; }
                }

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
                //Recojo el codigo y descripción de fase
                sAux = Request.QueryString["nFase"].ToString();
                if (sAux != "")
                {
                    sAux = sAux.Replace(".", "");
                    nFase = int.Parse(sAux);
                }
                else
                {
                    nFase = -1;
                }
                this.txtNumFase.Text = nFase.ToString();
                sDesFase = Request.QueryString["sFase"].ToString();
                this.txtFase.Text = sDesFase;
            }
            catch (Exception ex)
            {
                strErrores = Errores.mostrarError("Error al obtener las actividades", ex);
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
            string[] aArgs = Regex.Split(eventArg, "@#@");
            sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
            //2º Aquí realizaríamos el acceso a BD, etc,...
            switch (aArgs[0])
            {
                case ("A"):
                    sResultado += ObtenerActividades(aArgs[3], aArgs[1], aArgs[2], aArgs[4]);
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

    private string ObtenerActividades(string sTipoBusqueda, string num_proy_tec, string num_fase, string strNomActividad)
        {
            string sResul = "",sPT;
            int nPT;
            SqlDataReader dr;
            try
            {
                StringBuilder strBuilder = new StringBuilder();

                sPT=num_proy_tec.Replace(".","");
                nPT = int.Parse(sPT);
                strBuilder.Append("<table id='tblDatos' class='texto MA' style='width: 410px;'>");
                strBuilder.Append("<tbody>");

                if (num_fase=="" || num_fase=="-1")
                {
                    dr = ACTIVIDADPSP.Catalogo(null,strNomActividad,nPT,null,2,0,sTipoBusqueda);
                }
                else
                {
                    dr = ACTIVIDADPSP.Catalogo(null, strNomActividad, nPT, int.Parse(num_fase), 2, 0, sTipoBusqueda);
                }
                while (dr.Read())
                {
                    strBuilder.Append("<tr id='" + dr["t335_idactividad"].ToString() + "' ");
                    strBuilder.Append("codFase='" + dr["t334_idfase"].ToString() + "' ");
                    strBuilder.Append("desFase='" + dr["t334_desfase"].ToString() + "' onclick='ms(this)' ondblclick='aceptarClick(this.rowIndex)' >");
                    strBuilder.Append("<td>" + dr["t335_desactividad"].ToString() + "</td>");
                    strBuilder.Append("</tr>");
                }
                dr.Close();
                dr.Dispose();
                strBuilder.Append("<tbody>");
                strBuilder.Append("</table>");

                sResul = "OK@#@" + strBuilder.ToString();
            }
            catch (Exception ex)
            {
                sResul = "Error@#@" + Errores.mostrarError("Error al obtener las actividades", ex);
            }
            return sResul;
        }
	}

