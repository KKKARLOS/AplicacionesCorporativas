using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;
namespace SUPER.DAL
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
    /// Class	 : EXCLUIDOS_CTP
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
    /// Clase de acceso a datos para la tabla: T976_EXCLUIDOS_TPL
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	15/02/2010 16:17:14	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class EXCLUIDOS_CTP
	{

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
        /// Inserta un registro en la tabla T976_EXCLUIDOS_TPL.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	15/02/2010 16:17:14
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t001_idficepi)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
			aParam[0].Value = t001_idficepi;
			if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_EXCLUIDOS_CTP_I", aParam);
			else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_EXCLUIDOS_CTP_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
        /// Elimina un registro de la tabla T976_EXCLUIDOS_TPL a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	15/02/2010 16:17:14
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t001_idficepi)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
			aParam[0].Value = t001_idficepi;

			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_EXCLUIDOS_CTP_D", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_EXCLUIDOS_CTP_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
        /// Obtiene un catálogo de registros de la tabla T976_EXCLUIDOS_TPL.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	15/02/2010 16:17:14
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader Catalogo()
		{
			SqlParameter[] aParam = new SqlParameter[0];
			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_EXCLUIDOS_CTP_CAT", aParam);
		}

		#endregion
	}
}
