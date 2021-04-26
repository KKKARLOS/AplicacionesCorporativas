using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using CR2I.Capa_Datos;

namespace CR2I.Capa_Negocio
{
	/// <summary>
	/// Descripción breve de Idiomas.
	/// </summary>
	public class AccesoAplicaciones
	{

		public AccesoAplicaciones()
		{
		//
		// TODO: agregar aquí la lógica del constructor
		//
		}
		public SqlDataReader Leer()
		{
			SqlDataReader drAccesoAplicaciones = SqlHelper.ExecuteSqlDataReader(Utilidades.CadenaConexion, 
				"P034_ACCESOAPLICACION", 8);

			return drAccesoAplicaciones;
		}

	}
}
