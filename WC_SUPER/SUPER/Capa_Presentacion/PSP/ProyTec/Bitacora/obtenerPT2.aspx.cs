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
public partial class obtenerPT2 : System.Web.UI.Page, ICallbackEventHandler
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
                    sResultado += ObtenerPTs(aArgs[1], aArgs[2]);
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

        private string ObtenerPTs(string sTipoBusqueda, string strNomPT)
        {
            string sResul = "";//sPermiso
            SqlDataReader dr;
            int nPE;
            try
            {
                StringBuilder strBuilder = new StringBuilder();
                int iUser = (int)Session["UsuarioActual"];
                strBuilder.Append("<div style='background-image:url(../../../../Images/imgFT18.gif); width:596px;'>");
                strBuilder.Append("<table id='tblDatos' class='texto MA' style='width: 596px;'>");
                strBuilder.Append("<colgroup><col style='width=50%;'><col style='width=50%;'></colgroup>");
                strBuilder.Append("<tbody>");
                //dr = ProyTec.CatalogoBitacora(null, iUser, strNomPT, sTipoBusqueda);
                dr = ProyTec.Catalogo(null, strNomPT, null, iUser, sTipoBusqueda);
                while (dr.Read())
                {
                    nPE = int.Parse(dr["cod_pe"].ToString());
                    strBuilder.Append("<tr id='" + dr["cod_pt"].ToString() + "' onclick='ms(this)' ondblclick='aceptarClick(this.rowIndex)' onmouseover='TTip(event)'");
                    strBuilder.Append("des='" + dr["nom_pt"].ToString() + "' ");
                    strBuilder.Append("idT305PE='" + dr["t305_idproyectosubnodo"].ToString() + "' ");
                    strBuilder.Append("idPE='" + dr["cod_pe"].ToString() + "' ");
                    strBuilder.Append("desPE='" + dr["nom_pe"].ToString() + "' ");
                    strBuilder.Append("est='" + dr["t301_estado"].ToString() + "' ");
                    strBuilder.Append("une='" + dr["cod_une"].ToString() + "' ");
                    strBuilder.Append("sAccesoBitacoraPT='" + dr["t305_accesobitacora_pst"].ToString() + "' ");
                    strBuilder.Append("desune='" + dr["t303_denominacion"].ToString() + "'>");
                    strBuilder.Append("<td><span class='NBR' style='width:290px;'>" + nPE.ToString("#,###") + " " + dr["nom_pe"].ToString() + "</span></td>");
                    strBuilder.Append("<td><span class='NBR' style='width:290px;'>" + HttpUtility.HtmlEncode(dr["nom_pt"].ToString()) + "</span></td></tr>");
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

