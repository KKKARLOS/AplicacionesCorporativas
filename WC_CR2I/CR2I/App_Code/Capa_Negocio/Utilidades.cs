using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace CR2I.Capa_Negocio
{
    /// <summary>
    /// Descripción breve .
    /// </summary>
    public class Utilidades
    {
        public Utilidades()
        {
            //
            // TODO: agregar aquí la lógica del constructor
            //
        }

        /// <summary>
        /// 
        /// Devuelve la Cadena de Conexión a la base de datos.
        /// </summary>
        public static string CadenaConexion
        {
            get { return obtenerCadenaConexion(); }
        }

        private static string obtenerCadenaConexion()
        {
            string sConn = "";
            if (HttpContext.Current.Cache.Get("CadenaConexion") == null)
            {
                if (System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString().ToUpper() == "E")
                    sConn = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionExplotacion"].ToString();
                else
                    sConn = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionDesarrollo"].ToString();

                HttpContext.Current.Cache.Insert("CadenaConexion", sConn, null, DateTime.Now.AddHours(24), TimeSpan.Zero);
            }
            else
            {
                sConn = (string)HttpContext.Current.Cache.Get("CadenaConexion");
            }

            //if (HttpContext.Current.Session["CR2I_NOMBRE"] != null)
            //{
            //    sConn = sConn.Replace("app=CR2I", "app=CR2I: " + HttpContext.Current.Session["CR2I_APELLIDO1"].ToString() + " " + HttpContext.Current.Session["CR2I_APELLIDO2"].ToString() + ", " + HttpContext.Current.Session["CR2I_NOMBRE"].ToString());
            //}
            return sConn;
        }

        public static void SetEventosFecha(object oFecha)
        {
            TextBox txtFecha = (TextBox)oFecha;
            //if (HttpContext.Current.Session["BTN_FECHA"].ToString() == "D")
            //{
            //    txtFecha.Attributes.Add("onfocus", "focoFecha_CB(event);");
            //    txtFecha.Attributes.Add("onmousedown", "mc1_CB(event);");
            //    txtFecha.Attributes.Remove("onclick");
            //    txtFecha.Attributes.Remove("readonly");
            //}
            //else
            //{
            txtFecha.Attributes.Add("readonly", "readonly");
            txtFecha.Attributes.Add("onclick", "mc(event)");
            txtFecha.Attributes.Remove("onfocus");
            txtFecha.Attributes.Remove("onmousedown");
            //}
        }

        public static string decodpar(string sCadena)
        {
            if (sCadena == null) return "";
            if (sCadena == "") return "";
            return Utilidades.unescape(Encoding.ASCII.GetString(System.Convert.FromBase64String(sCadena)));
        }

        public static string escape(string sCadena)
        {
            if (sCadena == null) return "";  //El método EscapeDataString no acepta un null como input

            int nLongMax = 32766; //Longitud máxima permitida por el método EscapeDataString
            int nBloques = sCadena.Length / nLongMax;

            string sResultado = "";
            for (int i = 0; i <= nBloques; i++)
            {
                sResultado += System.Uri.EscapeDataString(sCadena.Substring(i * nLongMax, Math.Min(sCadena.Length - (i * nLongMax), nLongMax)));
            }

            return sResultado;
            //return System.Uri.EscapeDataString(sCadena);
        }
        public static string unescape(string sCadena)
        {
            if (sCadena == null) return "";  //El método UnescapeDataString no acepta un null como input

            return System.Uri.UnescapeDataString(sCadena);
        }

    }

}
