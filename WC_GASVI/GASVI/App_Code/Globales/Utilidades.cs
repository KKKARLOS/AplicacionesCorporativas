using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
//using Microsoft.JScript;

namespace GASVI.BLL
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

            if (HttpContext.Current.Session["GVT_DES_EMPLEADO_ENTRADA"] != null)
            {
                //sConn = sConn.Replace("app=GASVI", "app=GASVI: " + HttpContext.Current.Session["GVT_DES_EMPLEADO_ENTRADA"].ToString());
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
            //return GlobalObject.unescape(Encoding.ASCII.GetString(System.Convert.FromBase64String(sCadena)));
            return System.Uri.UnescapeDataString(Encoding.ASCII.GetString(System.Convert.FromBase64String(sCadena)));
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

        public static bool EstructuraActiva(string sNivel)
        {
            bool bActiva = false;
            if (HttpContext.Current.Cache.Get("EstructuraActiva") == null)
            {
                Hashtable htNivel = new Hashtable();
                List<Estructura> oLista = Estructura.ListaGlobal();
                foreach (Estructura oEstr in oLista)
                {
                    switch (oEstr.nCodigo)
                    {
                        case 6:
                            if (oEstr.bUtilizado) htNivel.Add("SN4", true);
                            else htNivel.Add("SN4", false);
                            break;
                        case 5:
                            if (oEstr.bUtilizado) htNivel.Add("SN3", true);
                            else htNivel.Add("SN3", false);
                            break;
                        case 4:
                            if (oEstr.bUtilizado) htNivel.Add("SN2", true);
                            else htNivel.Add("SN2", false);
                            break;
                        case 3:
                            if (oEstr.bUtilizado) htNivel.Add("SN1", true);
                            else htNivel.Add("SN1", false);
                            break;
                    }
                }
                bActiva = (bool)htNivel[sNivel];
                HttpContext.Current.Cache.Insert("EstructuraActiva", htNivel, null, DateTime.Now.AddDays(1), TimeSpan.Zero);
            }
            else
            {
                Hashtable htNivel = (Hashtable)HttpContext.Current.Cache.Get("EstructuraActiva");
                bActiva = (bool)htNivel[sNivel];
            }
            return bActiva;
        }

    }
}
