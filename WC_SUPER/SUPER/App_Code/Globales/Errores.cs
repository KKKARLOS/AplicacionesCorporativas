using System;
using System.Web;
using System.Web.SessionState;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Net.Mail;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// <summary>
	/// Errores es una clase con un método estático "mostrarError"
	/// que devuelve un string con la descripción del error. Recibe dos
	/// parámetros. 1- Un título del error. 2- El objeto error atrapado
	/// en el try catch. Dependiendo del tipo de error, devolvera una
	/// descripción genérica o más personalizada.
	/// </summary>
	public class Errores
	{
        //public static string mostrarError(string strDescripcion, System.Exception objError)
        //{
        //    string strMensaje = strDescripcion + "\n\n";
        //    switch (objError.GetType().ToString())
        //    {
        //        case "System.Data.SqlClient.SqlException":
        //            System.Data.SqlClient.SqlException nuevoError = (System.Data.SqlClient.SqlException)objError;
        //            switch (nuevoError.Number)
        //            {
        //                case 17:
        //                    strMensaje += "El servidor SQL no existe o se ha denegado el acceso.";
        //                    break;
        //                case 547:
        //                    //strMensaje += "Conflicto de integridad referencial \npor parte del objeto "+ nuevoError.Procedure;
        //                    //strMensaje += "Denegado. El sistema ha detectado un problema de integridad referencial en alguno de los elementos relacionados con el registro seleccionado.";
        //                    strMensaje += "Denegado. El sistema ha detectado un problema de integridad referencial en alguno de los elementos relacionados con el registro seleccionado.@#@" + nuevoError.Number.ToString();
        //                    break;
        //                case 2627:
        //                case 2601:
        //                    //strMensaje += "Conflicto de registro duplicado \npor parte del objeto "+ nuevoError.Procedure;
        //                    //2601: Conflicto de indice con unique.
        //                    strMensaje += "Denegado. El sistema ha detectado que intenta insertar un elemento duplicado.@#@" + nuevoError.Number.ToString();
        //                    break;
        //                case 1505:
        //                    strMensaje += "Denegado. El sistema ha detectado un problema con un elemento duplicado y un índice único.@#@" + nuevoError.Number.ToString();
        //                    break;
        //                case 1205: /* Deadlock Victim */
        //                    strMensaje += "Se ha producido un acceso concurrente a un mismo recurso, por lo que el sistema le ha excluido del proceso. Por favor, inténtelo de nuevo. Si el problema persiste, comuníquelo al CAU. Disculpe las molestias.@#@" + nuevoError.Number.ToString();
        //                    break;
        //                default:
        //                    strMensaje += "Error: " + nuevoError.Message;
        //                    break;
        //            }
        //            break;
        //        default:
        //            strMensaje += "Error: " + objError.Message;
        //            break;
        //    }
        //    strMensaje += "\n\nVuelva a intentarlo y, si persiste el problema, notifique la incidencia al CAU.\n\nDisculpe las molestias.";
        //    strMensaje = strMensaje.Replace("\n", @"\n");
        //    strMensaje = strMensaje.Replace("\r", @"\n");

        //    return strMensaje;
        //}
        public static string mostrarError(string strDescripcion, System.Exception objError)
        {
            SUPER.DAL.Log.Insertar("mostrarError. Inicio");
            int iError = 0;
            //string strMensaje = strDescripcion + "\n\n";
            string strMensaje = strDescripcion + (char)10 + (char)10;
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
                            strMensaje += "Se ha producido un acceso concurrente a un mismo recurso, por lo que el sistema le ha excluido del proceso.";
                            break;
                        case -2:
                            strMensaje += "Se ha producido un acceso concurrente a un mismo recurso, por lo que el sistema ha dado un error de tiempo de espera.";
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
            strMensaje = strMensaje + (char)10 + (char)10 + "Vuelve a intentarlo y, si persiste el problema, notifica la incidencia al CAU." + (char)10 + (char)10 + "Disculpa las molestias.";


            //strMensaje += "\n\nVuelva a intentarlo y, si persiste el problema, notifique la incidencia al CAU.\n\nDisculpe las molestias.";
            //strMensaje = strMensaje.Replace("\n", @"\n");
            //strMensaje = strMensaje.Replace("\r", @"\n");
            strMensaje += "@#@" + iError.ToString();

            //EnviarErrorEDA(strDescripcion+ "\n\n " + "Código de error: "+ iError.ToString(), objError);
            EnviarErrorEDA(strDescripcion + (char)10 + (char)10 + "Código de error: " + iError.ToString(), objError);
            
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
                            strMensaje += "Se ha producido un acceso concurrente a un mismo recurso, por lo que el sistema le ha excluido del proceso.";
                            break;
                        case -2:
                            strMensaje += "Se ha producido un acceso concurrente a un mismo recurso, por lo que el sistema ha dado un error de tiempo de espera.";
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
                strMensaje += "\n\nVuelve a intentarlo y, si persiste el problema, notifica la incidencia al CAU.\n\nDisculpa las molestias.";
            }
            strMensaje = strMensaje.Replace("\n", @"\n");
            strMensaje = strMensaje.Replace("\r", @"\n");
            strMensaje += "@#@" + iError.ToString();

            EnviarErrorEDA(strDescripcion, objError);

            return strMensaje;
        }
        /*
       public static string mostrarError(string strDescripcion)
       {
           string strMensaje = strDescripcion + "\n\n";
           strMensaje += "\n\nVuelva a intentarlo y, si persiste el problema, notifique la incidencia al CAU.\n\nDisculpe las molestias.";
           strMensaje = strMensaje.Replace("\n", @"\n");
           strMensaje = strMensaje.Replace("\r", @"\n");

           return strMensaje;
       }
       */
        //Versión mejor y más sencilla que la de arriba ya que luego no necesitas hacer nada en javascript pues te distingue los saltos de línea

        public static string mostrarError(string strDescripcion)
        {
            string strMensaje = strDescripcion + (char)10 + (char)10;
            strMensaje += " " + (char)10 + (char)10 + "Vuelve a intentarlo y, si persiste el problema, notifica la incidencia al CAU." + (char)10 + (char)10 + "Disculpa las molestias.";
            return strMensaje;
        }

        public static string mostrarErrorAjax(string strDescripcion, System.Exception objError)
        {
            int iError = 0;
            string strMensaje = strDescripcion + (char)10 + (char)10;
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
                            strMensaje += "Se ha producido un acceso concurrente a un mismo recurso, por lo que el sistema le ha excluido del proceso.";
                            break;
                        case -2:
                            strMensaje += "Se ha producido un acceso concurrente a un mismo recurso, por lo que el sistema ha dado un error de tiempo de espera.";
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
            strMensaje = strMensaje + (char)10 + (char)10 + "Vuelve a intentarlo y, si persiste el problema, notifica la incidencia al CAU." + (char)10 + (char)10 + "Disculpa las molestias.";
            strMensaje += "@#@" + iError.ToString();

            //EnviarErrorEDA(strDescripcion, objError);

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
        public static string CampoResponsableIntegridad(System.Exception objError)
        {
            string sCampo = "";
            if (((object)objError).GetType().Name != "SqlException") return "";
            switch (((SqlException)objError).Number)
            {
                case 547:
                    int nPosicionColumn = objError.Message.IndexOf("column '",0);
                    int nLongitudColumn = objError.Message.IndexOf("'", nPosicionColumn+8);
                    sCampo = objError.Message.Substring(nPosicionColumn + 8, nLongitudColumn - nPosicionColumn + 8);
                    //return sCampo;
                    break;
                default:
                    //return "";
                    break;
            }

            return sCampo;
        }

        private static void EnviarErrorEDA(string strDescripcion, System.Exception objError)
        {
            //SUPER.DAL.Log.Insertar("EnviarErrorEDA. Inicio");
            if (objError.Message == "File does not exist."
                || objError.Message == "Thread was being aborted."
                || objError.Message == "El archivo no existe."
                || objError.Message == "Subproceso anulado."
                || objError.Message == "La ruta de acceso 'OPTIONS' no está permitida."
                )
            {
                return;
            }
            //SUPER.DAL.Log.Insertar("EnviarErrorEDA. Antes Rte");
            string sRteError = RteMailError();
            string sToError = DestMailError();
            MailMessage objMail = new MailMessage();

            //objMail.From = new MailAddress("EDA@ibermatica.com", "EDA desde SUPER");
            //objMail.To.Add(new MailAddress("EDA@ibermatica.com", "EDA"));
            objMail.From = new MailAddress(sRteError, "EDA desde SUPER");
            objMail.To.Add(new MailAddress(sToError, "EDA"));
            objMail.IsBodyHtml = true;
            objMail.Subject = "Error en SUPER (desde la clase de errores)";

            try
            {
            //Gets the ASPX Page Name that caused the Error and 
            //the UserHost Address where the Error occured.
                if (HttpContext.Current.Session != null)
            objMail.Body = "Se ha producido un error en la página: " + HttpContext.Current.Request.Path + "<br>" +
                "en la dirección IP: " + HttpContext.Current.Request.UserHostAddress + "<br><br>";
                else
                    objMail.Body = "Se ha producido un error";

                if (HttpContext.Current.Session != null && HttpContext.Current.Session["APELLIDO1"] != null)
            {
                objMail.Body += "Usuario: " + HttpContext.Current.Session["APELLIDO1"].ToString() + " ";
                objMail.Body += HttpContext.Current.Session["APELLIDO2"].ToString() + ", ";
                objMail.Body += HttpContext.Current.Session["NOMBRE"].ToString() + "<br><br>";
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
            }
            catch
            {
                objMail.Body = "Se ha producido un error";
            }
            SmtpClient myClient = new SmtpClient(ConfigurationManager.AppSettings["ServidorSMTP"]);
            try
            {
                //SUPER.DAL.Log.Insertar("EnviarErrorEDA. 3");
                if (ConfigurationManager.ConnectionStrings["ENTORNO"].ToString() == "E" && 
                    ConfigurationManager.AppSettings["ENTORNO"].ToString() == "E")
                {
                    //SUPER.DAL.Log.Insertar("EnviarErrorEDA. 4");
                    myClient.Send(objMail);
                    //SUPER.DAL.Log.Insertar("EnviarErrorEDA. 5");
                }
            }
            catch (Exception)
            {
                //SUPER.DAL.Log.Insertar("EnviarErrorEDA. Error");
            }
        }

        /// <summary>
        /// Obtiene el buzón destinatario de correos de error
        /// </summary>
        /// <returns></returns>
        public static string DestMailError()
        {
            string sToError = "AMS-DIS-ERRAPP@ibermatica.com";
            try
            {
                sToError = ConfigurationManager.AppSettings["SMTP_to"].ToString();
                if (ConfigurationManager.ConnectionStrings["ENTORNO"].ToString() == "D")
                    sToError = ConfigurationManager.AppSettings["SMTP_to_DES"].ToString();

                if (sToError == "") sToError = "AMS-DIS-ERRAPP@ibermatica.com";
            }
            catch
            {
                sToError = "AMS-DIS-ERRAPP@ibermatica.com";
            }
            return sToError;
        }
        /// <summary>
        /// Obtiene el buzón remitente de correos de error
        /// </summary>
        /// <returns></returns>
        public static string RteMailError()
        {
            string sRte = "EDA@ibermatica.com";
            try
            {
                sRte = ConfigurationManager.AppSettings["SMTP_from"].ToString();
                if (sRte == "") sRte = "EDA@ibermatica.com";
            }
            catch
            {
                sRte = "EDA@ibermatica.com";
            }
            return sRte;
        }

    }
}

