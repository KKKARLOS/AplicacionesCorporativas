using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;


namespace SUPER.Capa_Presentacion.UserControls
{
	/// <summary>
	///		Descripción breve de Avisos.
	/// </summary>
    public partial class Avisos : System.Web.UI.UserControl//, ICallbackEventHandler
	{
        //private string _callbackResultado = null;
        public string strTablaHTML = "";
        public SqlConnection oConn;
        public SqlTransaction tr;
        protected string strUrl;
		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!Page.IsCallback)
            {
                this.strUrl = Session["strServer"].ToString() + "Images/";
                strTablaHTML = ObtenerAvisos();
            }
            //string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            //string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
        //public string GetCallbackResult()
        //{
        //    //Se envía el resultado al cliente.
        //    return _callbackResultado;
        //}
        //public void RaiseCallbackEvent(string eventArg)
        //{
        //    string sResultado = "";
        //    //1º Si hubiera argumentos, se recogen y tratan.
        //    //string MisArg = eventArg;
        //    string[] aArgs = Regex.Split(eventArg, "@#@");
        //    sResultado = aArgs[0] + @"@#@";

        //    //2º Aquí realizaríamos el acceso a BD, etc,...
        //    switch (aArgs[0])
        //    {
        //        case ("eliminar"):
        //            sResultado += EliminarAviso(aArgs[1]);
        //            break;
        //    }

        //    //3º Damos contenido a la variable que se envía de vuelta al cliente.
        //    _callbackResultado = sResultado;
        //}
        private string ObtenerAvisos()
        {
            StringBuilder sb = new StringBuilder();
            string sAfect;
            //sb.Append(" aProy = new Array();\n");
            sb.Append("<table id='tblDatos'>");
            //SqlDataReader dr = USUARIOAVISOS.SelectByT314_idusuario(null, int.Parse(Session["UsuarioActual"].ToString()));
            //Victor 17/06/2010: mostrar los avisos del usuario de entrada
            SqlDataReader dr = USUARIOAVISOS.SelectByT314_idusuario(null, (int)Session["NUM_EMPLEADO_ENTRADA"]);
            //int i = 0;
            while (dr.Read())
            {
                //Al haber saltos de línea no puedo usar un array por lo que cargo una tabla oculta
                //sb.Append("\taProy[" + i.ToString() + "] = {" +
                //                "titulo:\"" + Utilidades.escape(dr["t448_titulo"].ToString()) + "\"," +
                //                "texto:\"" + Utilidades.escape(dr["t448_texto"].ToString()) + "\"};\n");
                //i++;
                sAfect = "";
                sb.Append("<tr id='" + dr["t448_idaviso"].ToString() + "' titulo='" + dr["t448_titulo"].ToString() + "'");
                sb.Append(" texto='" + dr["t448_texto"].ToString() + "' afect='");
                if ((bool)dr["t448_IAP"]) sAfect += "IAP, ";
                if ((bool)dr["t448_PGE"]) sAfect += "PGE, ";
                if ((bool)dr["t448_PST"]) sAfect += "PST, ";
                if (sAfect.Length > 0)
                    sAfect = sAfect.Substring(0, sAfect.Length - 2);
                else
                    sAfect = "NINGUNO";
                sb.Append(sAfect);
                sb.Append("'><td></td></tr>");
            }
            sb.Append("</table>");
            dr.Close();
            dr.Dispose();
            return sb.ToString();
        }
        private string EliminarAviso(string sIdAviso)
        {
            string sResul = "OK@#@";
            #region abrir conexión y transacción
            try
            {
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccion(oConn);
            }
            catch (Exception ex)
            {
                if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
                sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
                return sResul;
            }
            #endregion

            try
            {
                if (sIdAviso != "")
                {
                    //USUARIOAVISOS.Delete(tr, int.Parse(sIdAviso), int.Parse(Session["UsuarioActual"].ToString()));
                    //int iNumAvisos = USUARIOAVISOS.CountByUsuario(tr, int.Parse(Session["UsuarioActual"].ToString()));
                    //Victor 17/06/2010: mostrar los avisos del usuario de entrada
                    USUARIOAVISOS.Delete(tr, int.Parse(sIdAviso), (int)Session["NUM_EMPLEADO_ENTRADA"]);
                    int iNumAvisos = USUARIOAVISOS.CountByUsuario(tr, (int)Session["NUM_EMPLEADO_ENTRADA"]);
                    if (iNumAvisos == 0)
                        Session["HAYAVISOS"] = "0";
                }
                Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = "Error@#@" + Errores.mostrarError("Error al eliminar el aviso " + sIdAviso, ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
            }
            return sResul;
        }

		#region Código generado por el Diseñador de Web Forms
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: llamada requerida por el Diseñador de Web Forms ASP.NET.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Método necesario para admitir el Diseñador. No se puede modificar
		///		el contenido del método con el editor de código.
		/// </summary>
		private void InitializeComponent()
		{
			
		}
		#endregion
	}
}
