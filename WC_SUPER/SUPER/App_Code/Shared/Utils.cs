using System.Web;
using System.Collections;
using System;
using System.ServiceModel;
using System.Globalization;
using System.Threading;
using System.Collections.Generic;

namespace IB.SUPER.Shared
{
    public class Utils
    {
        public Utils()
        {

        }

        /// <summary>
        /// Decodifica y unescapea una cadena
        /// </summary>
        public static string decodpar(string sCadena)
        {
            return System.Uri.UnescapeDataString(System.Text.Encoding.ASCII.GetString(System.Convert.FromBase64String(System.Uri.UnescapeDataString(sCadena))));
        }

      
        /// <summary>
        /// Codifica y escapea una cadena
        /// </summary>
        public static string codpar(string sCadena)
        {
            return System.Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(System.Uri.EscapeDataString(sCadena)));
        }

        /// <summary>
        /// Parseo del querystring escapeado
        /// </summary>
        /// <param name="qs"></param>
        /// <returns></returns>
        public static Hashtable ParseQuerystring(string qs)
        {
            if (qs.Trim().Length == 0) return new Hashtable();

            Hashtable ht = new Hashtable();

            string p = decodpar(qs);


            string[] arr = p.Split('&');
            string[] arr2;
            for (int i = 0; i < arr.Length; i++)
            {
                arr2 = arr[i].Split('=');
                ht.Add(arr2[0], arr2[1]);
            }

            return ht;

        }

        /// <summary>
        /// Parseo de los filtros
        /// </summary>
        /// <param name="qs"></param>
        /// <returns></returns>
        public static Hashtable ParseQuerystringFilters(string qs)
        {
            if (qs.Trim().Length == 0) return new Hashtable();

            Hashtable ht = new Hashtable();


            string[] arr = qs.Split('|');
            string[] arr2;
            for (int i = 0; i < arr.Length; i++)
            {
                arr2 = arr[i].Split(':');
                ht.Add(arr2[0], arr2[1]);
            }

            return ht;

        }

        public static Dictionary<string, string> ConvertQSFiltersToJson(string filters){

            if (filters.Trim().Length == 0) return new Dictionary<string, string>();

            Dictionary<string, string> dct = new Dictionary<string, string>();


            string[] arr = filters.Split('|');
            string[] arr2;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].Length > 0)
                {
                    arr2 = arr[i].Split(':');
                    dct.Add(arr2[0], arr2[1]);
                }
            }

            return dct;

        }

    
        /// <summary>
        /// Parseo del querystring escapeado
        /// </summary>
        /// <param name="qs"></param>
        /// <returns></returns>
        public static Hashtable ParseQuerystring_nocod(string qs)
        {
            if (qs.Trim().Length == 0) return new Hashtable();

            Hashtable ht = new Hashtable();

            string p = qs;


            string[] arr = p.Split('&');
            string[] arr2;
            for (int i = 0; i < arr.Length; i++)
            {
                arr2 = arr[i].Split('=');
                ht.Add(arr2[0], arr2[1]);
            }

            return ht;

        }

        public static string MsgErrorConserva(string sAccion, ConservaException cex)
        {
            string sRes = "";

            switch (sAccion)
            {
                case "W"://Está intentando grabar un archivo en el Content-Server
                    sRes = "Error al almacenar el documento.<br /><br />";
                    break;
                case "R"://Está intentando traer un archivo del Content-Server
                    sRes = "Error al descargar el documento.<br /><br />";
                    break;
            }
            if (cex.ErrorCode != 120)
            {
                sRes += cex.ErrorCode.ToString();
                switch (cex.ErrorCode)
                {
                    case 100:
                        sRes += "Debe indicar el nombre del documento.<br /><br />";
                        break;
                    case 101:
                        sRes += "El documento no puede estar vacío.<br /><br />";
                        break;
                    case 102:
                        sRes += "Se ha superado el tamaño de documento máximo permitido (";
                        sRes += System.Configuration.ConfigurationManager.AppSettings["TamMaxContentServer"] + ")<br /><br />";
                        break;
                    case 103://No se ha indicado el id del documento para leerlo del Content-Server
                        break;
                    default:
                        sRes += cex.Message;
                        break;
                }
                sRes += cex.ErrorCode.ToString();
            }
            else //error en la operación del repositorio. consultar innerexception
            {
                ConservaException innercex = (ConservaException)cex.InnerException;
                sRes += cex.Message + ". " + innercex.Message + ".<br/><br/> Error code: " + innercex.ErrorCode;

            }

            return sRes;
        }

        /// <summary>
        /// monta el mensaje para el innerexception
        /// </summary>
        public static string GetInnerExMsg(Exception ex)
        {
            if (ex.InnerException != null) return ex.InnerException.Message;
            else return "";
        }

        /// <summary>
        /// Capitaliza el valor pasado como parametro
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Capitalize(string value)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
        }

        /// Indica si el profesional actual es Aministrador o SuperAdministrador de producción
        /// </summary>
        /// <returns></returns>
        public static bool EsAdminProduccion()
        {
            bool bRes = false;
            if (HttpContext.Current.Session["ADMINISTRADOR_PC_ACTUAL"].ToString() == "A" ||
                HttpContext.Current.Session["ADMINISTRADOR_PC_ACTUAL"].ToString() == "SA")
                bRes = true;
            return bRes;
        }
        public static bool EsAdminProduccionEntrada()
        {
            bool bRes = false;
            if (HttpContext.Current.Session["ADMINISTRADOR_PC_ENTRADA"].ToString() == "A" ||
                HttpContext.Current.Session["ADMINISTRADOR_PC_ENTRADA"].ToString() == "SA")
                bRes = true;
            return bRes;
        }
        public static bool EsSuperAdminProduccion()
        {
            bool bRes = false;
            if (HttpContext.Current.Session["ADMINISTRADOR_PC_ACTUAL"].ToString() == "SA")
                bRes = true;
            return bRes;
        }
        public static int GetUserActual()
        {
            int idUser = 0;
            if (HttpContext.Current.Session["UsuarioActual"].ToString() == "0")
                idUser = int.Parse(HttpContext.Current.Session["UsuarioActual_CVT"].ToString());
            else
                idUser = int.Parse(HttpContext.Current.Session["UsuarioActual"].ToString());

            return idUser;
        }
    }


}