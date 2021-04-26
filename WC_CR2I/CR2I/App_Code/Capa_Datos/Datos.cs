using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using CR2I.Capa_Negocio;

namespace CR2I.Capa_Datos
{
	/// <summary>
	/// Datos es una clase con un método estático "abrirConexion"
	/// que devuelve una conexión abierta, que apunta al servidor 
	/// indicado en el web.config con el atributo "cadenaConexion"
	/// </summary>
	public class Datos
	{
		public static SqlConnection abrirConexion
		{
			get
			{
				SqlConnection conexion = new SqlConnection(Utilidades.CadenaConexion);
				conexion.Open();
				return conexion;
			}
		}
	}
}

