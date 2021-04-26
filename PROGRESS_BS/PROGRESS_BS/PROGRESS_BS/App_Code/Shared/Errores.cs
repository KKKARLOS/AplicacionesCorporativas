using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Web;

namespace IB.Progress.Shared
{
    public class Errores
    {
        public static string mostrarError(string strDescripcion, System.Exception objError)
        {
            int iError = 0;
            string strMensaje = strDescripcion + "\n\n";
            switch (objError.GetType().ToString())
            {
                case "System.Data.SqlClient.SqlException":
                    System.Data.SqlClient.SqlException nuevoError = (System.Data.SqlClient.SqlException)objError;
                    iError = nuevoError.Number;
                    switch (nuevoError.Number)
                    {
                        case 17:
                            strMensaje += "El servidor SQL no existe o se ha denegado el acceso.";
                            break;
                        case 547:
                            //strMensaje += "Conflicto de integridad referencial \npor parte del objeto "+ nuevoError.Procedure;
                            //strMensaje += "Denegado. El sistema ha detectado un problema de integridad referencial en alguno de los elementos relacionados con el registro seleccionado.";
                            strMensaje += "Denegado. El sistema ha detectado un problema de integridad referencial en alguno de los elementos relacionados con el registro seleccionado.";
                            break;
                        case 2627:
                        case 2601:
                            //strMensaje += "Conflicto de registro duplicado \npor parte del objeto "+ nuevoError.Procedure;
                            //2601: Conflicto de indice con unique.
                            strMensaje += "Denegado. El sistema ha detectado que intenta insertar un elemento duplicado.";
                            break;
                        case 1505:
                            strMensaje += "Denegado. El sistema ha detectado un problema con un elemento duplicado y un índice único.";
                            break;
                        case 1205: /* Deadlock Victim */
                            strMensaje += "Se ha producido un acceso concurrente a un mismo recurso, por lo que el sistema le ha excluido del proceso. Por favor, inténtelo de nuevo. Si el problema persiste, comuníquelo al CAU. Disculpe las molestias.";
                            break;
                        default:
                            strMensaje += "Error: " + nuevoError.Message;
                            break;
                    }
                    break;
                default:
                    strMensaje += "Error: " + objError.Message;
                    break;
            }
            strMensaje += "\n\nVuelva a intentarlo y, si persiste el problema, notifique la incidencia al CAU.\n\nDisculpe las molestias.";
            strMensaje = strMensaje.Replace("\n", @"\n");
            strMensaje = strMensaje.Replace("\r", @"\n");
            strMensaje += "@#@" + iError.ToString();

            EnviarErrorEDA(strDescripcion, objError);
            return strMensaje;
        }


        public static string mostrarError(string strDescripcion, System.Exception objError, bool bReintentar)
        {
            int iError = 0;
            string strMensaje = strDescripcion + "\n\n";
            switch (objError.GetType().ToString())
            {
                case "System.Data.SqlClient.SqlException":
                    System.Data.SqlClient.SqlException nuevoError = (System.Data.SqlClient.SqlException)objError;
                    iError = nuevoError.Number;
                    switch (nuevoError.Number)
                    {
                        case 17:
                            strMensaje += "El servidor SQL no existe o se ha denegado el acceso.";
                            break;
                        case 547:
                            //strMensaje += "Conflicto de integridad referencial \npor parte del objeto "+ nuevoError.Procedure;
                            strMensaje += "Denegado. El sistema ha detectado un problema de integridad referencial en alguno de los elementos relacionados con el registro seleccionado.";
                            break;
                        case 2627:
                        case 2601:
                            //strMensaje += "Conflicto de registro duplicado \npor parte del objeto "+ nuevoError.Procedure;
                            //2601: Conflicto de indice con unique.
                            strMensaje += "Denegado. El sistema ha detectado que intenta insertar un elemento duplicado.";
                            break;
                        case 1505:
                            strMensaje += "Denegado. El sistema ha detectado un problema con un elemento duplicado y un índice único.";
                            break;
                        case 1205: /* Deadlock Victim */
                            bReintentar = false;
                            strMensaje += "Se ha producido un acceso concurrente a un mismo recurso, por lo que el sistema le ha excluido del proceso. Por favor, inténtelo de nuevo. Si el problema persiste, comuníquelo al CAU. Disculpe las molestias.";
                            break;
                        default:
                            strMensaje += "Error: " + nuevoError.Message;
                            break;
                    }
                    break;
                default:
                    strMensaje += "Error: " + objError.Message;
                    break;
            }
            if (bReintentar)
            {
                strMensaje += "\n\nVuelva a intentarlo y, si persiste el problema, notifique la incidencia al CAU.\n\nDisculpe las molestias.";
            }
            strMensaje = strMensaje.Replace("\n", @"\n");
            strMensaje = strMensaje.Replace("\r", @"\n");
            strMensaje += "@#@" + iError.ToString();

            EnviarErrorEDA(strDescripcion, objError);

            return strMensaje;
        }
        public static string mostrarError(string strDescripcion)
        {
            string strMensaje = strDescripcion + "\n\n";
            strMensaje += "\n\nVuelva a intentarlo y, si persiste el problema, notifique la incidencia al CAU.\n\nDisculpe las molestias.";
            strMensaje = strMensaje.Replace("\n", @"\n");
            strMensaje = strMensaje.Replace("\r", @"\n");

            return strMensaje;
        }
        public static bool EsErrorIntegridad(System.Exception objError)
        {
            bool bResul = false;
            if (((object)objError).GetType().Name != "SqlException") return false;
            switch (((SqlException)objError).Number)
            {
                case 547:
                case 2627:
                case 2601:
                case 1505:
                    bResul = true;
                    break;
            }

            return bResul;
        }

        private static void EnviarErrorEDA(string strDescripcion, System.Exception objError)
        {
            MailMessage objMail = new MailMessage();

            objMail.From = new MailAddress("EDA@ibermatica.com", "EDA desde PROGRESS");
            objMail.To.Add(new MailAddress("EDA@ibermatica.com", "EDA"));
            objMail.IsBodyHtml = true;
            objMail.Subject = "Error en ADMCONSERVA (desde la clase de errores)";

            //Gets the ASPX Page Name that caused the Error and 
            //the UserHost Address where the Error occured.
            objMail.Body = "Se ha producido un error en la página: " + HttpContext.Current.Request.Path + "<br>" +
                "en la dirección IP: " + HttpContext.Current.Request.UserHostAddress + "<br><br>";

            if (HttpContext.Current.Session != null)
            {
                if (HttpContext.Current.Session["PROFESIONAL"] != null)
                    objMail.Body += "Profesional: " + ((Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).nombre + " ";
                
                if (HttpContext.Current.Session["IDRED"] != null)
                    objMail.Body += "Código de Red: " + HttpContext.Current.Session["IDRED"].ToString() + "<br><br>";
            }
            //Gets the Error Message
            objMail.Body += "Mensaje de error: " + objError.Message + "<br><br>";
            objMail.Body += "Descri. de error: " + strDescripcion + "<br><br>";
            //Gets the Detailed Error Message
            objMail.Body += "<b>El detalle del error:</b>" + objError.InnerException + "<br><br>";
            objMail.Body += "<b>El origen del error:</b>" + objError.Source + "<br><br>";
            objMail.Body += "<b>El lugar del error:</b>" + objError.StackTrace.Replace(((char)10).ToString(), "<br>") + "<br><br>";
            objMail.Body += "<b>El método del error:</b>" + objError.TargetSite + "<br><br>";

            SmtpClient myClient = new SmtpClient(ConfigurationManager.AppSettings["ServidorSMTP"]);
            try
            {
                if (ConfigurationManager.ConnectionStrings["ENTORNO"].ToString() == "E")
                {
                    myClient.Send(objMail);
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
