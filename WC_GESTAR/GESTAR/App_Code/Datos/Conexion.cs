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
			SqlConnection cn = new SqlConnection(Utilidades.Conexion());
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
            tr.Rollback();
        }
		public static void CerrarTransaccion(SqlTransaction tr,SqlConnection cn)
		{
			tr.Rollback();
			cn.Close();
            cn.Dispose();
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
