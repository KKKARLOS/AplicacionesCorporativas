using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using CR2I.Capa_Datos;

namespace CR2I.Capa_Negocio
{
	/// <summary>
	/// Descripción breve de Comportamientos.
	/// </summary>
	public class Conexion
	{
		public Conexion()
		{
			//
			// TODO: agregar aquí la lógica del constructor
			//
		}
		public static SqlConnection Abrir()
		{
			SqlConnection cn = new SqlConnection(Utilidades.CadenaConexion);
			cn.Open();			
			return  cn;
		}
		public static SqlTransaction AbrirTransaccion(SqlConnection cn)
		{
			SqlTransaction tr = cn.BeginTransaction();
			return  tr;
		}
		public static void CerrarTransaccion(SqlTransaction tr)
		{
            try
            {
                tr.Rollback();
            }
            catch (Exception)
            {
                //Si hubiera algún SqlDataReader abierto, no se puede cerrar la 
                //transacción y se produce un error.
            }
        }
		public static void CommitTransaccion(SqlTransaction tr)
		{
			tr.Commit();
		}
		public static void Cerrar(SqlConnection cn)
		{
			cn.Close();
			cn.Dispose();
		}


	}
}
