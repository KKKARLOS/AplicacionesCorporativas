using System;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
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

        /// <summary>
        /// 
        /// Crea una nueva conexión a partir de la cadena de conexión que se recoge
        /// en la propiedad estática Utilidades.CadenaConexion, la abre y la devuelve.
        /// </summary>
        public static SqlConnection Abrir()
		{
			SqlConnection cn = new SqlConnection(Utilidades.CadenaConexion);
			cn.Open();
			return cn;
		}

        /// <summary>
        /// 
        /// Recoge como parámetro una SqlConnection, crea una transacción, la
        /// abre y la devuelve.
        /// </summary>
        public static SqlTransaction AbrirTransaccion(SqlConnection cn)
        {
            SqlTransaction tr = cn.BeginTransaction(IsolationLevel.ReadUncommitted);
            SqlHelper.SetContextInfo(cn, tr);
            return tr;
            //return cn.BeginTransaction(IsolationLevel.ReadUncommitted);
        }

        public static SqlTransaction AbrirTransaccionSerializable(SqlConnection cn)
        {
            SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable);
            SqlHelper.SetContextInfo(cn, tr);
            return tr;
            //return cn.BeginTransaction(IsolationLevel.Serializable);
        }
        /// <summary>
        /// 
        /// Realiza la Commit de la transacción que recibe como parámetro.
        /// </summary>
		public static void CommitTransaccion(SqlTransaction tr)
		{
			tr.Commit();
            tr.Dispose();
		}
        /// <summary>
        /// 
        /// Realiza la Rollback de la transacción que recibe como parámetro.
        /// </summary>
		public static void CerrarTransaccion(SqlTransaction tr)
		{
            try
            {
                tr.Rollback();
                tr.Dispose();
            }
            catch (Exception)
            {
                //Si hubiera algún SqlDataReader abierto, no se puede cerrar la 
                //transacción y se produce un error.
            }
        }
        /// <summary>
        /// 
        /// Cierra y destruye la SqlConnection que recibe como parámetro.
        /// </summary>
		public static void Cerrar(SqlConnection cn)
		{
            if (cn != null)
            {
                cn.Close();
                cn.Dispose();
            }
        }


	}
}
