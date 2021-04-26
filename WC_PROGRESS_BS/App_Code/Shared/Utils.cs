using System.Web;
using System.Collections;
using System;
using System.Text;

namespace IB.Progress.Shared
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
            if (sCadena == null) return "";
            if (sCadena == "") return "";
            return Utils.unescape(Encoding.ASCII.GetString(System.Convert.FromBase64String(sCadena)));
        }

        static public string EncodeTo64(string toEncode)
        {
            return System.Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode));
        }

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

        /// <summary>
        /// Parseo del querystring 
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

        public static string getEstado(string estado)
        {
            switch (estado)
            {
                case "ABI":
                    return "Abierta";

                case "CUR":
                    return "En curso";

                case "CCF":
                    return "Cerrada firmada";

                case "CSF":
                    return "Cerrada sin firmar";

                default:
                    return "";

            }
        }

    }
}