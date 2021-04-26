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
using Microsoft.JScript;
using GESTAR.Capa_Negocio;

namespace GESTAR.Capa_Presentacion
{
    /// <summary>
    /// Descripción breve de CatGenericoBusq.
    /// </summary>
    public partial class CatGenericoBusq : System.Web.UI.Page, ICallbackEventHandler
    {
        private string _callbackResultado = null;
        public string strTitulo;
        public string strCabecera;
        SqlDataReader dr;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            try
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

                    hdnOpcion.Text = Request.QueryString["OPCION"].ToString();

                    if (hdnOpcion.Text == "Proveedor")
                    {
                        strTitulo = "proveedores";
                        //strCabecera = "Proveedores";
                    }
                    else if (hdnOpcion.Text == "Cliente")
                    {
                        strTitulo = "clientes";
                        //strCabecera = "Clientes";
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
                hdnErrores.Text = Errores.mostrarError("Error al cargar la página", ex);
            }
        }
        public void RaiseCallbackEvent(string eventArg)
        {
            string sResultado = "";
            //1º Si hubiera argumentos, se recogen y tratan.
            //string MisArg = eventArg;
            string[] aArgs = Regex.Split(eventArg, "@@");
            sResultado = aArgs[0] + @"@@";
            sResultado += ObtenerDatos(aArgs[1], aArgs[2]);
            //3º Damos contenido a la variable que se envía de vuelta al cliente.
            _callbackResultado = sResultado;
        }
         public string GetCallbackResult()
        {
            //Se envía el resultado al cliente.
            return _callbackResultado;
        }

        private string ObtenerDatos(string sDenominacion, string sTipo)
        {
            string sResul = "";
            try
            {            
                StringBuilder strBuilder = new StringBuilder();
                int i = 0;

                strBuilder.Append("<table id='tblDatos' class='texto' style='width: 396px; BORDER-COLLAPSE: collapse;' cellSpacing='0' border='0'>");
                strBuilder.Append("<colgroup><col style='width:100%;' /></colgroup>");

                if (hdnOpcion.Text == "Cliente")
                    dr = Cliente.Catalogo(sDenominacion, sTipo);
                else if (hdnOpcion.Text == "Proveedor")
                    dr = Proveedor.Catalogo(sDenominacion, sTipo);

                while (dr.Read())
                {
                    if (i % 2 == 0) strBuilder.Append("<tr class=FA ");
                    else strBuilder.Append("<tr class=FB ");

                    strBuilder.Append("id='" + dr["ID"].ToString() + "' onclick=\"ms(this)\" ondblclick=\"aceptarClick(this.rowIndex)\" onmouseover=TTip(event);>");

                    strBuilder.Append("<td style=padding-left:5px;' ><nobr style='width:390px;' class='NBR'>" + dr["DESCRIPCION"].ToString() + "</nobr></td>");

                    strBuilder.Append("</tr>");
                    i++;
                }
                dr.Close();
                dr.Dispose();

                strBuilder.Append("</table>");
                sResul = "OK@@" + strBuilder.ToString();
            }
            catch (Exception ex)
            {
                sResul = "Error@@" + ex.ToString();
            }
            return sResul;
        }

    }
}
