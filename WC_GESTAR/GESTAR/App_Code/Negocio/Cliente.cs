using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using GESTAR.Capa_Datos;

namespace GESTAR.Capa_Negocio
{
	/// <summary>
	/// Descripción breve de CR.
	/// </summary>
	public class Cliente
	{
		public Cliente()
		{
			//
			// TODO: agregar aquí la lógica del constructor
			//
		}
		public static SqlDataReader Catalogo(string sDenominacion, string sTipo)
		{
			SqlDataReader drCliente= SqlHelper.ExecuteSqlDataReader(Utilidades.Conexion(),
                "GESTAR_CLIENTE", sDenominacion, sTipo);
			return drCliente;
		}
        public static SqlDataReader Catalogo(int iArea)
        {
            SqlDataReader drCliente = SqlHelper.ExecuteSqlDataReader(Utilidades.Conexion(),
                "GESTAR_CLIENTE_TEXT", iArea);
            return drCliente;
        }
	}
}
