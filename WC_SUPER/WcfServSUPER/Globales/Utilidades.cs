using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace IB.Services.Super.Globales
{
    public class Utilidades
    {
        public static string CadenaConexion
        {
            get { return obtenerCadenaConexion(); }
        }
        private static string obtenerCadenaConexion()
        {
            //string sConn = "";
            //string sError = "(u)";
            //try
            //{
                //sError = "(v)";
                if (System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString().ToUpper() == "E")
                    return System.Configuration.ConfigurationManager.ConnectionStrings["ConexionExplotacion"].ToString();
                else
                    return System.Configuration.ConfigurationManager.ConnectionStrings["ConexionDesarrollo"].ToString();
            //    sError = "(y)";
            //}
            //catch (Exception e)
            //{
            //    throw (new Exception(sError + " " + e.Message));
            //}

            //return sConn;
        }

        public static string cabXml()
        {
            return @"<?xml version='1.0' encoding='utf-8' ?>";
        }
        public static string ComponerXmlErrorMsg(int errorCode, string errorDescription)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(cabXml());
            sb.Append("<Datos>");
            sb.Append("<Error>" + errorCode + "</Error>");
            sb.Append("<Message>" + errorDescription + "</Message>");
            sb.Append("</Datos>");

            return sb.ToString();
        }
        /// <summary>
        /// Para evitar un XML mal formado, sustituye 
        ///     < por &lt; 
        ///     > por &gt; 
        ///     & por &amp;
        /// </summary>
        /// <param name="sTexto"></param>
        /// <returns></returns>
        public static string textoXml(string sTexto)
        {
            sTexto = sTexto.Replace("<", "&lt;");
            sTexto = sTexto.Replace(">", "&gt;");
            sTexto = sTexto.Replace("&", "&amp;");
            return sTexto;
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
        public static bool isInteger(object value)
        {
            try
            {
                long l = System.Int64.Parse(value.ToString(), System.Globalization.NumberStyles.Any);
                return true;
            }
            catch (System.FormatException)
            {
                return false;
            }
        }
        public static bool isAnoMes(object value)
        {
            try
            {
                int iAnoMes = System.Int32.Parse(value.ToString(), System.Globalization.NumberStyles.Any);
                return ValidarAnnomes(iAnoMes);
            }
            catch (System.FormatException)
            {
                return false;
            }
        }
        public static int getEntero(string sCampo, string sNumero)
        {
            int iRes = 0;
            try
            {
                iRes = int.Parse(sNumero);
            }
            catch
            {
                throw new Exception("Parámetro " + sCampo + ". La cadena " + sNumero + " no es un entero valido");
            }
            return iRes;
        }
        public static double getDouble(string sCampo, string sNumero)
        {
            double dRes = 0;
            try
            {
                dRes = Double.Parse(sNumero);
            }
            catch
            {
                throw new Exception("Parámetro " + sCampo + ". La cadena " + sNumero + " no es un double valido");
            }
            return dRes;
        }
        public static decimal getDecimal(string sCampo, string sNumero)
        {
            Decimal dRes = 0;
            try
            {
                dRes = Decimal.Parse(sNumero);
            }
            catch
            {
                throw new Exception("Parámetro " + sCampo + ". La cadena " + sNumero + " no es un decimal valido");
            }
            return dRes;
        }
        public static short getShort(string sCampo, string sNumero)
        {
            short dRes = 0;
            try
            {
                dRes = short.Parse(sNumero);
            }
            catch
            {
                throw new Exception("Parámetro " + sCampo + ". La cadena " + sNumero + " no es un short valido");
            }
            return dRes;
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
        public static System.DateTime getDate(string sCampo, string strFecha)
        {
            System.DateTime dt = new DateTime(1900, 1, 1);
            try
            {
                if (strFecha != "")
                {
                    string[] aFec = Regex.Split(strFecha, "/");
                    dt = new DateTime(int.Parse(aFec[2]), int.Parse(aFec[1]), int.Parse(aFec[0]));
                }
            }
            catch
            {
                throw new Exception("Parámetro " + sCampo + ". La cadena " + strFecha + " no es una fecha valida en formato dd/mm/yyyy");
            }
            return dt;
        }
        public static bool ValidarAnnomes(int nAnnoMes)
        {
            if (nAnnoMes.ToString().Length != 6)
                throw (new Exception("La longitud del AnnoMes no es de seis dígitos"));
            if (nAnnoMes % 100 < 1 || nAnnoMes % 100 > 12)
                throw (new Exception("El mes no es coherente. Menor de 1 o mayor de 12."));
            if (nAnnoMes / 100 < 1900 || nAnnoMes / 100 > 2078)
                throw (new Exception("El año no es coherente. Menor de 1900 o mayor de 2078."));

            return true;
        }
        public static DateTime AnnomesAFecha(int nAnnoMes)
        {
            if (ValidarAnnomes(nAnnoMes))
                return DateTime.Parse("01/" + nAnnoMes.ToString().Substring(4, 2) + "/" + nAnnoMes.ToString().Substring(0, 4));
            else
                return DateTime.Parse("01/01/1900");
        }
    }
}
