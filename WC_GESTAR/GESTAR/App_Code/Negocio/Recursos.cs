using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using GESTAR.Capa_Datos;

namespace GESTAR.Capa_Negocio
{
	/// <summary>
	/// Descripción breve de Recursos.
	/// </summary>
	public class Recursos
	{
		public Recursos()
		{
			//
			// TODO: agregar aquí la lógica del constructor
			//
		}

        public static SqlDataReader ObtenerUsuario(string IDRED, System.Int16 rt_code)
		{
			SqlDataReader drFICLOGIN = SqlHelper.ExecuteSqlDataReader(Utilidades.Conexion(), 
				"FIC_LOGIN", IDRED , rt_code );
			return drFICLOGIN;
		}


        public static SqlDataReader LeerRecursos(string strEstado, string strLetra, int intColumna, int intOrden)
		{
			SqlDataReader drRecursos;
			drRecursos = SqlHelper.ExecuteSqlDataReader(Utilidades.Conexion(), 
					"P017_CATALOGOAVAN",strEstado,strLetra,intColumna, intOrden );
			return drRecursos;
		}
        public static SqlDataReader CargarRecursos(string strApellido1, string strApellido2, string strNombre, int intColumna, int intOrden)
        {
            SqlDataReader drRecursos;
            drRecursos = SqlHelper.ExecuteSqlDataReader(Utilidades.Conexion(),
                    "GESTAR_RECURSOS", strApellido1, strApellido2, strNombre, intColumna, intOrden);
            return drRecursos;
        }
	}
}