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
using GESTAR.Capa_Negocio;
using Microsoft.JScript;
using System.Text.RegularExpressions;

namespace GESTAR.Capa_Presentacion.ASPX
{
	/// <summary>
    /// Summary description for CatEntradas.
	/// </summary>
	public partial class CatEntradas: System.Web.UI.Page, ICallbackEventHandler
	{
        private string _callbackResultado = null;
        public SqlConnection oConn;
        public SqlTransaction tr;
        public string strTablaHtmlCatalogo;
        SqlDataReader dr = null;
        
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
                    hdnIDArea.Text = Request.QueryString["IDAREA"].ToString();
                    if (Request.QueryString["PROMOTOR"]== null)
                        this.hdnPromotor.Text = "N";
                    else
                        this.hdnPromotor.Text = Request.QueryString["PROMOTOR"].ToString();

                    strTablaHtmlCatalogo = ObtenerEntradas(int.Parse(hdnIDArea.Text), 1, 0);
                    if (Session["MODOLECTURA"]!=null)
                        hdnModoLectura.Text = Session["MODOLECTURA"].ToString();

                    //1º Se indican (por este orden) la función a la que se va a devolver el resultado y la función que va a acceder al servidor
                    string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context");
                    string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

                //2º Se "registra" la función que va a acceder al servidor.
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
                }
            }
            catch (Exception ex)
            {
                hdnErrores.Text = Errores.mostrarError("Error al obtener los datos", ex);
            }

		}
        public void RaiseCallbackEvent(string eventArg)
        {
            //1º Si hubiera argumentos, se recogen y tratan.
            string[] aArgs = Regex.Split(eventArg, @"@@");

            //2º Aquí realizaríamos el acceso a BD, etc,...


            System.Text.StringBuilder strbTabla = new System.Text.StringBuilder();
            strbTabla.Length = 0;

            switch (aArgs[0])
            {
                case "Borrar": // borrar
                    strbTabla.Append(Borrar(Microsoft.JScript.GlobalObject.unescape(aArgs[1])));
                    break;
                case "Leer": // leer
                    strbTabla.Append(ObtenerEntradas(int.Parse(hdnIDArea.Text), byte.Parse(aArgs[1]), byte.Parse(aArgs[2])));
                    break;
            }

            //3º Damos contenido a la variable que se envía de vuelta al cliente.
            try
            {
                if (strbTabla.ToString().Substring(0, 1) != "N") _callbackResultado = aArgs[0] + "@@OK@@" + strbTabla.ToString();
                else _callbackResultado = aArgs[0] + "@@" + strbTabla.ToString();
            }
            catch
            {
                _callbackResultado = aArgs[0] + "@@OK"; //
            }
        }

        public string GetCallbackResult()
        {
            //Se envía el resultado al cliente.
            return _callbackResultado;
        }
        private string ObtenerEntradas(int intIdArea, byte intOrden, byte intAscDesc)
        {
            //Catalogo(Nullable<string> T074_DENOMINACION, Nullable<short> T075_ORIGEN, Nullable<int> T042_IDAREA, Nullable<int> T074_CREADOR, byte nOrden, byte nAscDesc)
            dr = null;
            dr = ENTRADA.Catalogo( intIdArea, intOrden, intAscDesc);

            int i = 0;
            System.Text.StringBuilder strBuilderCatalogo = new System.Text.StringBuilder();

            strBuilderCatalogo.Append("<table id='tblCatalogoEntrada' style='width: 830px;text-align:left'>" + (char)13);
            strBuilderCatalogo.Append("<colgroup><col style='width:45%;' /><col style='width:25%;' /><col style='width:30%;' /></colgroup>" + (char)13);

            while (dr.Read())
            {
                strBuilderCatalogo.Append("<tr id='" + dr["T074_CREADOR"].ToString() + "/" + dr["ID"].ToString() + "' ");

                //if (i % 2 == 0)
                //    strBuilderCatalogo.Append("class='FA' ");
                //else
                //    strBuilderCatalogo.Append("class='FB' ");

                i++;
                strBuilderCatalogo.Append(" onclick=mm(event); ");
                strBuilderCatalogo.Append(" ondblclick=this.className='FS';Det_Entrada(this); ");
                strBuilderCatalogo.Append(" style='cursor: pointer;height:16px'>");

                strBuilderCatalogo.Append("<td style='padding-left:5px'>&nbsp;&nbsp;" + dr["DESCRIPCION"].ToString() + "</td>");
                strBuilderCatalogo.Append("<td>&nbsp;&nbsp;" + dr["ORIGEN"].ToString() + "</td>");
                strBuilderCatalogo.Append("<td>&nbsp;&nbsp;" + dr["CREADOR"].ToString() + "</td>");
                strBuilderCatalogo.Append("</tr>" + (char)13);
            }

            dr.Close();
            dr.Dispose();

            strBuilderCatalogo.Append("</table>");
            return strBuilderCatalogo.ToString();
        }
        protected string Borrar(string strID)
        {
            string sResul = "";
            string strId = "";
            try
            {
                oConn = GESTAR.Capa_Negocio.Conexion.Abrir();
                tr = GESTAR.Capa_Negocio.Conexion.AbrirTransaccion(oConn);
            }
            catch (Exception ex)
            {
                if (oConn.State == ConnectionState.Open) GESTAR.Capa_Negocio.Conexion.Cerrar(oConn);
                sResul = "N@@" + Errores.mostrarError("Error al abrir la conexión", ex);
                return sResul;
            }

            try
            {
                string[] aID = Regex.Split(strID, ",");

                for (int j = 0; j < aID.Length; j++)
                {
                    strId = aID[j];
                    ENTRADA.Delete(tr, short.Parse(aID[j]));
                }

                GESTAR.Capa_Negocio.Conexion.CommitTransaccion(tr);
                sResul = "";
            }
            catch (Exception ex)
            {
                GESTAR.Capa_Negocio.Conexion.CerrarTransaccion(tr);
                sResul = "N@@" + Errores.mostrarError("Error al borrar la entrada " + strId, ex);
            }
            finally
            {
                GESTAR.Capa_Negocio.Conexion.Cerrar(oConn);
            }
            return sResul;

        }					
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
            InitializeComponent();
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{   
			//this.imgbtnGrabar.Click += new System.Web.UI.ImageClickEventHandler(this.imgbtnGrabar_Click);

		}
		#endregion


	}
}
