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
    /// Descripción breve de obtenerCliente.
	/// </summary>
    public partial class getProveedor : System.Web.UI.Page, ICallbackEventHandler
    {
        private string _callbackResultado = null;
        public string strErrores;
        public int nProfesionales = 0;

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

                string sProf = Request.QueryString["nProfesionales"];
                if (sProf != null)
                {
                    nProfesionales = int.Parse(sProf);
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
                case ("proveedor"):
                    sResultado += getProveedores(aArgs[1], aArgs[2], aArgs[3]);
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

        private string getProveedores(string sTipoBusqueda, string strProv, string nProfesionales)
        {
            string sResul = "";
            try
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("<table id='tblDatos' class='texto MA' style='WIDTH: 500px;'>");
                sb.Append("<colgroup><col style='width:100px;'><col style='width:400px;'></colgroup>");
                sb.Append("<tbody>");
                SqlDataReader dr = PROVEEDOR.SelectByNombre(null, strProv, 2, 0, sTipoBusqueda, (nProfesionales=="1")?true:false);

                int i = 0;
                bool bExcede = false;
                while (dr.Read())
                {
                    sb.Append("<tr id='" + dr["t315_idproveedor"].ToString() + "' ");
                    if (!(bool)dr["bloqueado"])
                        sb.Append(" onclick=\"ms(this)\" ondblclick=\"aceptarClick(this.rowIndex)\" style='height:16px' ");
                    else
                        sb.Append(" onclick=\"mmoff('Inf','Proveedor bloqueado en SAP',230);\" style='cursor:default;height:16px;color:gray;' ");
                    sb.Append(" >");
                    sb.Append("<td style='padding-left:5px;'>" + dr["t315_codigoexterno"].ToString() + "</td>");
                    sb.Append("<td>" + dr["t315_denominacion"].ToString() + "</td>");
                    sb.Append("</tr>");
                    i++;
                    if (i > Constantes.nNumMaxTablaCatalogo)
                    {
                        bExcede = true;
                        break;
                    }
                }
                dr.Close();
                dr.Dispose();
                if (!bExcede)
                {
                    sb.Append("</tbody>");
                    sb.Append("</table>");
                }
                else
                {
                    sb.Length = 0;
                    sb.Append("EXCEDE");
                }

                sResul = "OK@#@" + sb.ToString();
            }
            catch (Exception ex)
            {
                sResul = "Error@#@" + Errores.mostrarError("Error al obtener los proveedores", ex);
            }
            return sResul;
        }
	}
