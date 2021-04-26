using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using GEMO.DAL;

namespace GEMO.BLL
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
        public static string mostrarError(string strDescripcion, System.Exception objError)
        {
            string strMensaje = strDescripcion + "\n\n";
            switch (objError.GetType().ToString())
            {
                case "System.Data.SqlClient.SqlException":
                    System.Data.SqlClient.SqlException nuevoError = (System.Data.SqlClient.SqlException)objError;
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

            return strMensaje;
        }
        public static string mostrarError(string strDescripcion, System.Exception objError, bool bReintentar)
        {
            string strMensaje = strDescripcion + "\n\n";
            switch (objError.GetType().ToString())
            {
                case "System.Data.SqlClient.SqlException":
                    System.Data.SqlClient.SqlException nuevoError = (System.Data.SqlClient.SqlException)objError;
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
                            strMensaje += "Denegado. El sistema ha detectado que intenta insertar un elemento duplicado.@@" + nuevoError.Number.ToString();
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
    }


}

