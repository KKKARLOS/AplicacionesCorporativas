using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using GESTAR.Capa_Datos;

namespace GESTAR.Capa_Negocio
{
	/// <summary>
	/// Descripci�n breve de Idiomas.
	/// </summary>
	public class AccesoAplicaciones
	{

		public AccesoAplicaciones()
		{
		//
		// TODO: agregar aqu� la l�gica del constructor
		//
		}
        public static SqlDataReader Leer()
		{
			SqlDataReader drAccesoAplicaciones = SqlHelper.ExecuteSqlDataReader(Utilidades.Conexion(), 
				"P034_ACCESOAPLICACION", 6);

			return drAccesoAplicaciones;
		}

	}
}
