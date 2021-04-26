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
	public class Comportamientos
	{
		public Comportamientos()
		{
			//
			// TODO: agregar aquí la lógica del constructor
			//
		}
		public SqlDataReader Leer(int intComportamiento , System.Int16 rt_code)
		{
			SqlDataReader drComportamiento = SqlHelper.ExecuteSqlDataReader(Utilidades.Conexion(), 
				"GESTAR_COMPORTAMIENTO", intComportamiento , rt_code );
			return drComportamiento;
		}
	}
}
