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
    /// Pantalla de selecci?n de proyectos t?cnicos
	/// </summary>
public partial class obtenerPT2 : System.Web.UI.Page, ICallbackEventHandler
    {
        private string _callbackResultado = null;
        public string strErrores, sDesPE,sAux;
        public int nPE;

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
                strErrores = Errores.mostrarError("Error al obtener los proyectos t?cnicos", ex);
            }

            //1? Se indican (por este orden) la funci?n a la que se va a devolver el resultado
            //   y la funci?n que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

            //2? Se "registra" la funci?n que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
        public void RaiseCallbackEvent(string eventArg)
        {
            string sResultado = "";
            //1? Si hubiera argumentos, se recogen y tratan.
            //string MisArg = eventArg;
            string[] aArgs = Regex.Split(eventArg, "@#@");
            sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

            //2? Aqu? realizar?amos el acceso a BD, etc,...
            switch (aArgs[0])
            {
                case ("PT"):
                    sResultado += ObtenerPTs(aArgs[1], aArgs[2]);
                    break;
            }

            //3? Damos contenido a la variable que se env?a de vuelta al cliente.
            _callbackResultado = sResultado;
        }
        public string GetCallbackResult()
        {
            //Se env?a el resultado al cliente.
            return _callbackResultado;
        }

        private string ObtenerPTs(string sTipoBusqueda, string strNomPT)
        {
            string sResul = "";//sPermiso
            SqlDataReader dr;
            try
            {
                StringBuilder strBuilder = new StringBuilder();
                int iUser = (int)Session["UsuarioActual"];
                strBuilder.Append("<table id='tblDatos' class='texto MA' style='width: 780px;'>");
                strBuilder.Append("<colgroup><col style='width:390px;'><col style='width:390px;'></colgroup>");
                strBuilder.Append("<tbody>");
                dr = ProyTec.Catalogo(null, strNomPT, null, iUser, sTipoBusqueda);
                while (dr.Read())
                {
                    nPE = int.Parse(dr["cod_pe"].ToString());
                    strBuilder.Append("<tr id='" + dr["cod_pt"].ToString() + "' onclick='ms(this)' ondblclick='aceptarClick(this.rowIndex)' ");
                    strBuilder.Append("des='" + dr["nom_pt"].ToString() + "' ");
                    strBuilder.Append("idT305PE='" + dr["t305_idproyectosubnodo"].ToString() + "' ");
                    strBuilder.Append("idPE='" + dr["cod_pe"].ToString() + "' ");
                    strBuilder.Append("desPE='" + dr["nom_pe"].ToString() + "' ");
                    strBuilder.Append("est='" + dr["t301_estado"].ToString() + "' ");
                    strBuilder.Append("une='" + dr["cod_une"].ToString() + "' ");
                    if (dr["t305_admiterecursospst"] != DBNull.Value)
                        if ((bool)dr["t305_admiterecursospst"])
                            strBuilder.Append("bRecPST='S'>");
                        else
                            strBuilder.Append("bRecPST='N'>");
                    else
                        strBuilder.Append("bRecPST='S'>");
                    strBuilder.Append("<td><nobr class='NBR W390'>" + nPE.ToString("#,###") + " " + dr["nom_pe"].ToString() + "</nobr></td>");
                    strBuilder.Append("<td><nobr class='NBR W390'>" + dr["nom_pt"].ToString() + "</nobr></td></tr>");
                }
                dr.Close();
                dr.Dispose();
                strBuilder.Append("</tbody>");
                strBuilder.Append("</table>");

                sResul = "OK@#@" + strBuilder.ToString();
            }
            catch (Exception ex)
            {
                sResul = "Error@#@" + Errores.mostrarError("Error al obtener los proyectos t?cnicos", ex);
            }
            return sResul;
        }
	}

