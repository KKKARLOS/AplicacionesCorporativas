using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace GASVI.DAL
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
        /// en la propiedad estática Conexion.GetCadenaConexion, la abre y la devuelve.
        /// </summary>
        public static SqlConnection Abrir()
		{
			SqlConnection cn = new SqlConnection(Conexion.GetCadenaConexion);
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
            SqlTransaction tr = cn.BeginTransaction(IsolationLevel.ReadCommitted);
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

        public static string GetCadenaConexion
        {
            get { return obtenerCadenaConexion(); }
        }
        private static string obtenerCadenaConexion()
        {
            string sConn = "";
            if (HttpContext.Current.Cache.Get("CadenaConexion") == null)
            {
                if (System.Configuration.ConfigurationManager.ConnectionStrings["ENTORNO"].ToString().ToUpper() == "E")
                    sConn = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionExplotacion"].ToString();
                else
                    sConn = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionDesarrollo"].ToString();

                HttpContext.Current.Cache.Insert("CadenaConexion", sConn, null, DateTime.Now.AddHours(24), TimeSpan.Zero);
            }
            else
            {
                sConn = (string)HttpContext.Current.Cache.Get("CadenaConexion");
            }

            if (HttpContext.Current.Session["GVT_DES_EMPLEADO_ENTRADA"] != null)
            {
                //sConn = sConn.Replace("app=GASVI", "app=GASVI: " + HttpContext.Current.Session["GVT_DES_EMPLEADO_ENTRADA"].ToString());
            }
            return sConn;
        }

	}
}
