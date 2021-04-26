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
public partial class obtenerPT : System.Web.UI.Page, ICallbackEventHandler
    {
        private string _callbackResultado = null;
        public string strErrores;

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
                //Recojo el codigo del proyectosubnodo
                this.txtPSN.Text = Utilidades.decodpar(Request.QueryString["nPSN"].ToString());
                //Recojo el codigo y descripción de proyecto económico
                this.txtNumPE.Text = Utilidades.decodpar(Request.QueryString["nPE"].ToString());
                this.txtPE.Text = Utilidades.decodpar(Request.QueryString["sPE"].ToString());
            }
            catch (Exception ex)
            {
                strErrores = Errores.mostrarError("Error al obtener los proyectos técnicos", ex);
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
                case ("PT"):
                    sResultado += ObtenerPTs(aArgs[2], aArgs[1], aArgs[3]);
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

        private string ObtenerPTs(string sTipoBusqueda, string sPSN, string strNomPT)
        {
            string sResul = "";//sPermiso
            int? nPSN = null;
            SqlDataReader dr;
            try
            {
                StringBuilder strBuilder = new StringBuilder();
                int iUser = (int)Session["UsuarioActual"];
                if (sPSN != "")
                    nPSN = int.Parse(sPSN);
                strBuilder.Append("<div style='background-image:url(../../../../Images/imgFT18.gif); width: 396px;'>");
                strBuilder.Append("<table id='tblDatos' class='texto MA' style='width: 396px;'>");
                strBuilder.Append("<colgroup><col style='width: 396px;''></colgroup>");
                strBuilder.Append("<tbody>");
                //dr = ProyTec.CatalogoBitacora(nPSN, iUser, strNomPT, sTipoBusqueda);
                dr = ProyTec.Catalogo(null, strNomPT, nPSN, iUser, sTipoBusqueda);
                while (dr.Read())
                {
                    strBuilder.Append("<tr id='" + dr["cod_pt"].ToString());
                    strBuilder.Append("' une='" + dr["cod_une"].ToString());
                    strBuilder.Append("' nPE='" + dr["cod_pe"].ToString());
                    strBuilder.Append("' dPE='" + dr["nom_pe"].ToString());
                    strBuilder.Append("' est='" + dr["t301_estado"].ToString());
                    strBuilder.Append("' idP='" + dr["t305_idproyectosubnodo"].ToString());
                    strBuilder.Append("' sAccesoBitacoraPT='" + dr["t305_accesobitacora_pst"].ToString());
                    strBuilder.Append("' onclick='ms(this)' ondblclick='aceptarClick(this.rowIndex)' onmouseover='TTip(event)'>");
                    strBuilder.Append("<td><span class='NBR' style='width:380px;'>" + HttpUtility.HtmlEncode(dr["nom_pt"].ToString()) + "</span></td></tr>");
                }
                dr.Close();
                dr.Dispose();
                strBuilder.Append("</tbody>");
                strBuilder.Append("</table></div>");

                sResul = "OK@#@" + strBuilder.ToString();
            }
            catch (Exception ex)
            {
                sResul = "Error@#@" + Errores.mostrarError("Error al obtener los proyectos técnicos", ex);
            }
            return sResul;
        }
	}

