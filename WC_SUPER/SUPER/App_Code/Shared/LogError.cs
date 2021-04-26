using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Configuration;


namespace IB.SUPER.Shared
{
    public class LogError
    {

        /// <summary>
        ///envia por SMTP un error
        /// </summary>
        /// <param name="msg">descripción de error</param>
        /// <param name="number">Código de error</param>
        public static void LogearError(string msg, int number)
        {
            LogearError(msg, number, "");
        }

        /// <summary>
        ///envia por SMTP un error
        /// </summary>
        /// <param name="msg">descripción de error</param>
        /// <param name="number">Código de error</param>
        public static void LogearError(string msg, Exception ex)
        {
            LogearError(msg, 0, "", ex);
        }

        /// <summary>
        /// envia por SMTP un error
        /// </summary>
        /// <param name="msg">descripción de error</param>
        /// <param name="number">Código de error</param>
        /// <param name="msg2">Información adicional del error</param>
        public static void LogearError(string msg, int number, string msg2)
        {
            LogearError(msg, number, msg2, null);
        }

        /// <summary>
        /// envia por SMTP un error
        /// </summary>
        /// <param name="msg">descripción de error</param>
        /// <param name="number">Código de error</param>
        /// <param name="msg2">Información adicional del error</param>
        /// <param name="ex">Excepcion</param>
        public static void LogearError(string msg, int number, string msg2, Exception ex)
        {
            //Si no hay sesión, no envío correo
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            string modulo = getModuloError(HttpContext.Current.Request.Path); 

            sb.Append("Se ha producido un error en SUPER: \n\n");
            sb.Append("Página: " + HttpContext.Current.Request.Path + "\n");
            sb.Append("ErrorCode: " + number + "\n");
            sb.Append("ErrorMessage: " + msg);
            if (msg2.Length > 0) sb.Append(" " + msg2);
            sb.Append("\n\n");

            if (HttpContext.Current.Session != null)
            {
                if (HttpContext.Current.Session["DES_EMPLEADO_ENTRADA"] != null)
                    sb.Append("Profesional: " + HttpContext.Current.Session["DES_EMPLEADO_ENTRADA"].ToString() + "\n");
                if (HttpContext.Current.Session["IDRED"] != null)
                    sb.Append("Código de Red: " + HttpContext.Current.Session["IDRED"].ToString() + "\n");
            }

            sb.Append("\n");

            if (ex != null) sb.Append("Exception: " + ex.Message + "\n");
            if (ex != null && ex.InnerException != null) sb.Append("InnerException:" + ex.InnerException.Message + "\n");
            if (ex != null) sb.Append("StackTrace:" + ex.StackTrace.ToString() + "\n");

            //Enviar email a EDA 
            Smtp.SendSMTP("Error en SUPER" + modulo, sb.ToString());

        }

        public static void LogearErrorJS(string file, string line, string msg)
        {

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string modulo = getModuloError(file);

                sb.Append("Se ha producido un error en SUPER: \n\n");
                sb.Append("Página: " + file + "\n");
                sb.Append("Línea: " + line + "\n");
                sb.Append("ErrorMessage: " + msg);
                sb.Append("\n\n");

                if (HttpContext.Current.Session != null)
                {
                    if (HttpContext.Current.Session["DES_EMPLEADO_ENTRADA"] != null)
                        sb.Append("Profesional: " + HttpContext.Current.Session["DES_EMPLEADO_ENTRADA"].ToString() + "\n");
                    if (HttpContext.Current.Session["IDRED"] != null)
                        sb.Append("Código de Red: " + HttpContext.Current.Session["IDRED"].ToString() + "\n");
                }

                sb.Append("\n");

                //Enviar email a EDA 
                Smtp.SendSMTP("Error en SUPER"+ modulo, sb.ToString());
        }

        private static string getModuloError(string path)
        {
            string modulo = "";  
            int indice = 0;
            Boolean encontrado = false;
            string[] partesPath;

            try
            {
                partesPath = path.Split('/');

                foreach (string parte in partesPath)
                {
                    indice++;
                    if (parte == "Capa_Presentacion")
                    {
                        encontrado = true;
                        break;
                    }
                }
                if (encontrado) modulo = "-" + partesPath[indice++];                

            }
            catch (Exception)
            {
            }

            return modulo;
        }

    }
}