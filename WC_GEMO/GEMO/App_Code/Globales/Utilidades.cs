using System;
using System.Web;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using Microsoft.JScript;
using System.Web.UI.WebControls;
namespace GEMO.BLL
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
        public static string CadenaConexionFic
        {
            get { return obtenerCadenaConexionFichero(); }
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

            if (HttpContext.Current.Session["DES_EMPLEADO_ENTRADA"] != null)
            {
                //sConn = sConn.Replace("app=GEMO", "app=GEMO: " + HttpContext.Current.Session["DES_EMPLEADO_ENTRADA"].ToString());
            }
            return sConn;
        }
        private static string obtenerCadenaConexionFichero()
        {
            string sConn = "";
            if (HttpContext.Current.Cache.Get("CadenaConexionFic") == null)
            {
                if (System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString().ToUpper() == "E")
                    sConn = System.Configuration.ConfigurationManager.AppSettings["ConexionFichExplotacion"];
                else
                    sConn = System.Configuration.ConfigurationManager.AppSettings["ConexionFichDesarrollo"];

                HttpContext.Current.Cache.Insert("CadenaConexionFic", sConn, null, DateTime.Now.AddHours(24), TimeSpan.Zero);
            }
            else
            {
                sConn = (string)HttpContext.Current.Cache.Get("CadenaConexionFic");
            }

            if (HttpContext.Current.Session["DES_EMPLEADO_ENTRADA"] != null)
            {
                //sConn = sConn.Replace("app=PROGRESS", "app=PROGRESS: " + HttpContext.Current.Session["DES_EMPLEADO_ENTRADA"].ToString());
            }
            return sConn;
        }
        public static string TamanoArchivo(int nBytes)
        {
            string sResul = "";
            if (nBytes > 0)
            {
                if (nBytes > 1048576) //1MB
                {
                    double nMbs = (double)nBytes / 1048576;
                    sResul = "&nbsp;&nbsp;&nbsp;(" + nMbs.ToString("N") + " Mb.)";
                }
                else if (nBytes > 1024) //1KB
                {
                    double nKbs = (double)nBytes / 1024;
                    sResul = "&nbsp;&nbsp;&nbsp;(" + nKbs.ToString("N") + " Kb.)";
                }
                else
                {
                    sResul = "&nbsp;&nbsp;&nbsp;(" + nBytes.ToString() + " bytes)";
                }
            }
            return sResul;
        }

        public static bool isNumeric(object value)
        {
            try
            {
                double d = System.Double.Parse(value.ToString(), System.Globalization.NumberStyles.Any);
                return true;
            }
            catch (System.FormatException)
            {
                return false;
            }
        }
        public static bool isDate(object value)
        {
            try
            {
                if (value.ToString() == "") return false;

                DateTime d = System.Convert.ToDateTime(value.ToString());
                return true;
            }
            catch (System.FormatException)
            {
                return false;
            }
        }
        public static string decodpar(string sCadena)
        {
            return GlobalObject.unescape(Encoding.ASCII.GetString(System.Convert.FromBase64String(sCadena)));
        }

        static public string EncodeTo64(string toEncode)
        {
            return System.Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode));
        }

        static public string DecodeFrom64(string encodedData)
        {
            return System.Text.ASCIIEncoding.ASCII.GetString(System.Convert.FromBase64String(encodedData));
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

        public static string CadenaParaTooltipExtendido(string sCadena)
        {
            return sCadena.Replace(((char)91).ToString(), "&#91;&#91;").Replace(((char)93).ToString(), "&#93;&#93;").Replace(((char)60).ToString(), "&#60;").Replace(((char)62).ToString(), "&#62;").Replace(((char)34).ToString(), "&#34;").Replace(((char)39).ToString(), "&#39;");//Se duplican los corchetes porque luego la función del boxover.js convierte las dobles en simples.
        }
        /* Los profesionales tienen en T001_FICEPI.t001_botonfecha un parámetro que indica el tipo de funcionalidad
          * que desea para las cajas de texto donde se manejan fechas.
          * En función de esa configuración caunado se entra a una pantalla que contiene fechas, desde el Page_Load
          * se llama a esta función para cada uno de los campos fecha que existan para establecer su functionalidad
          * sTipo="I" -> la caja no es editable y el control calendario se muestra con click izquierdo
          * sTipo="D" -> la caja es editable y el control calendario se muestra con click derecho
          * */
        public static void SetEventosFecha(object oFecha)
        {
            TextBox txtFecha = (TextBox)oFecha;
            if (HttpContext.Current.Session["BTN_FECHA"].ToString() == "D")
            {
                txtFecha.Attributes.Add("onfocus", "focoFecha(event);");
                txtFecha.Attributes.Add("onmousedown", "mc1(event);");
                txtFecha.Attributes.Remove("onclick");
                txtFecha.Attributes.Remove("readonly");
            }
            else
            {
                txtFecha.Attributes.Add("readonly", "readonly");
                txtFecha.Attributes.Add("onclick", "mc(event)");
                txtFecha.Attributes.Remove("onfocus");
                txtFecha.Attributes.Remove("onmousedown");
            }
        }
    }
}
