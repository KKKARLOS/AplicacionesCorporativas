using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using GESTAR.Capa_Datos;

namespace GESTAR.Capa_Negocio
{
    /// <summary>
    /// Descripción breve de Fechas.
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
            get { return Conexion(); }
        }


        public static string Conexion()
        {
            string sConn = "";
            if (HttpContext.Current.Cache.Get("CadenaConexion") == null)
            {
                if (System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ConnectionString.ToUpper() == "E")
                    sConn = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionExplotacion"].ToString();
                else
                    sConn = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionDesarrollo"].ToString();

                HttpContext.Current.Cache.Insert("CadenaConexion", sConn, null, DateTime.Now.AddHours(24), TimeSpan.Zero);
            }
            else
            {
                sConn = (string)HttpContext.Current.Cache.Get("CadenaConexion");
            }

//            if (HttpContext.Current.Session["NOMBRE"] != null)
//                sConn = sConn.Replace("app=GESTAR", "app=GESTAR: " + HttpContext.Current.Session["NOMBRE"].ToString());

            return sConn;
        }
        /// <summary>
        /// Devuelve una cadena con el mensaje de error en accesos fallidos al Content-Server
        /// </summary>
        /// <param name="sAccion">W->Subir archivo, R->Descargar archivo</param>
        /// <param name="cex"></param>
        /// <returns></returns>
        public static string MsgErrorConserva(string sAccion, ConservaException cex)
        {
            string sRes = "";
            bool bCAU = true;

            switch (sAccion)
            {
                case "W"://Está intentando grabar un archivo en el Content-Server
                    sRes = "Error al almacenar el documento." + (char)10 + (char)10; 
                    break;
                case "R"://Está intentando traer un archivo del Content-Server
                    sRes = "Error al descargar el documento." + (char)10 + (char)10; 
                    break;
            }
            if (cex.InnerException == null)
            {
                switch (cex.ErrorCode)
                {
                    case 100:
                        bCAU = false;//No es un problema del Content-Server sino del fichero que intenta subir/descargar el usuario
                        sRes += "Debe indicar el nombre del documento." + (char)10 + (char)10;
                        break;
                    case 101:
                        bCAU = false;
                        sRes += "El documento no puede estar vacío." + (char)10 + (char)10;
                        break;
                    case 102:
                        bCAU = false;
                        sRes += "Se ha superado el tamaño de documento máximo permitido (";
                        sRes += System.Configuration.ConfigurationManager.AppSettings["TamMaxContentServer"] + ")" + (char)10 + (char)10;
                        break;
                    case 103://No se ha indicado el id del documento para leerlo del Content-Server
                        bCAU = false;
                        break;
                    default:
                        //sRes += cex.Message;
                        break;
                }
                sRes += "Error: " + cex.ErrorCode.ToString();
            }
            else
            {
                if (cex.InnerException.GetType().Name == "ConservaException")//cex.ErrorCode == 120
                {
                    ConservaException icex = (ConservaException)cex.InnerException;
                    sRes += "Error: " + icex.ErrorCode.ToString() + (char)10 + (char)10;
                    sRes += "Descripción:  " + icex.Message;// +"Póngase en contacto con el CAU.";
                }
                else
                {
                    //sRes += cex.InnerException.Message;
                    sRes += "Error: " + cex.ErrorCode.ToString() + (char)10 + (char)10;
                    sRes += "Descripción:  " + cex.InnerException.Message;// +"Póngase en contacto con el CAU.";
                }
            }
            if (bCAU)
                sRes = sRes + (char)10 + (char)10 + "Póngase en contacto con el CAU.";

            return sRes;
        }

  /* 
   *Los profesionales tienen en T001_FICEPI.t001_botonfecha un parámetro que indica el tipo de funcionalidad
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
        public static string decodpar(string sCadena)
        {
            if (sCadena == null) return "";
            if (sCadena == "") return "";
            return Utilidades.unescape(Encoding.ASCII.GetString(System.Convert.FromBase64String(sCadena)));
        }

        //static public string EncodeTo64(string toEncode)
        //{
        //    return System.Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode));
        //}

        //static public string DecodeFrom64(string encodedData)
        //{
        //    return System.Text.ASCIIEncoding.ASCII.GetString(System.Convert.FromBase64String(encodedData));
        //}

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
    }
}
