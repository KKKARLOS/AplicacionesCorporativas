using System;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// <summary>
	/// Descripci�n breve de Comportamientos.
	/// </summary>
	public class Conexion
	{
		public Conexion()
		{
			//
			// TODO: agregar aqu� la l�gica del constructor
			//
		}

        /// <summary>
        /// 
        /// Crea una nueva conexi�n a partir de la cadena de conexi�n que se recoge
        /// en la propiedad est�tica Utilidades.CadenaConexion, la abre y la devuelve.
        /// </summary>
        public static SqlConnection Abrir()
		{
			SqlConnection cn = new SqlConnection(Utilidades.CadenaConexion);
			cn.Open();
			return cn;
		}

        /// <summary>
        /// 
        /// Recoge como par�metro una SqlConnection, crea una transacci�n, la
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
        /// Realiza la Commit de la transacci�n que recibe como par�metro.
        /// </summary>
		public static void CommitTransaccion(SqlTransaction tr)
		{
			tr.Commit();
            tr.Dispose();
		}
        /// <summary>
        /// 
        /// Realiza la Rollback de la transacci�n que recibe como par�metro.
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
                //Si hubiera alg�n SqlDataReader abierto, no se puede cerrar la 
                //transacci�n y se produce un error.
            }
        }
        /// <summary>
        /// 
        /// Cierra y destruye la SqlConnection que recibe como par�metro.
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
