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
	public class Proveedor
	{
		public Proveedor()
		{
			//
			// TODO: agregar aquí la lógica del constructor
			//
		}
        public static SqlDataReader Catalogo(string sDenominacion, string sTipo)
        {
            SqlDataReader drProveedor = SqlHelper.ExecuteSqlDataReader(Utilidades.Conexion(),
                "GESTAR_PROVEEDOR", sDenominacion, sTipo);
            return drProveedor;
        }
        public static SqlDataReader Catalogo(int iArea)
        {
            SqlDataReader drProveedor = SqlHelper.ExecuteSqlDataReader(Utilidades.Conexion(),
                "GESTAR_PROVEEDOR_TEXT",iArea);
            return drProveedor;
        }
	}
}
