using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using GESTAR.Capa_Datos;

namespace GESTAR.Capa_Negocio
{
	/// <summary>
	/// Descripción breve de Comportamientos.
	/// </summary>
	public class Errores
	{
		public static string mostrarError(string strDescripcion,System.Exception objError)
		{
			string strMensaje = strDescripcion +"\n\n";
			switch (objError.GetType().ToString())
			{
				case "System.Data.SqlClient.SqlException":
					System.Data.SqlClient.SqlException nuevoError = (System.Data.SqlClient.SqlException)objError;
				switch (nuevoError.Number)
				{
//					case 547:
//						strMensaje += "Denegado. El sistema ha detectado elementos relacionados con el registro seleccionado.";
//						break;
					case 2627:
						strMensaje += "Denegado. El sistema ha detectado que intenta insertar un elemento duplicado.";
						break;
					default:
						strMensaje += "Error: "+ nuevoError.Message;
						break;
				}
					break;
				default:
					strMensaje += "Error: "+ objError.Message;
					break;
			}
			strMensaje = strMensaje.Replace("\n",@"\n");
			strMensaje = strMensaje.Replace("\r",@"\n");

			return strMensaje;
		}
	}
}
