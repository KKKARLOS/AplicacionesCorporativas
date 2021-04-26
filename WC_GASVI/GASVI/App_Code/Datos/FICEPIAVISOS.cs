using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace GASVI.DAL
{
	/// -----------------------------------------------------------------------------
	/// Project	 : GASVI
	/// Class	 : FICEPIAVISOS
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T775_FICEPIAVISOSGASVI
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	28/04/2009 11:29:20	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class FICEPIAVISOS
	{

		#region Metodos

        public static SqlDataReader ObtenerAvisos(int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[0].Value = t001_idficepi;

            return SqlHelper.ExecuteSqlDataReader("GVT_FICEPIAVISOS_SByT001_idficepi", aParam);
        }
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T775_FICEPIAVISOSGASVI.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	28/04/2009 11:29:20
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t774_idaviso , int t001_idficepi)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t774_idaviso", SqlDbType.Int, 4);
			aParam[0].Value = t774_idaviso;
			aParam[1] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
			aParam[1].Value = t001_idficepi;

			if (tr == null)
				 SqlHelper.ExecuteNonQuery("GVT_FICEPIAVISOS_I", aParam);
			else
				 SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_FICEPIAVISOS_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T775_FICEPIAVISOSGASVI a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	28/04/2009 11:29:20
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t774_idaviso, int t001_idficepi)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t774_idaviso", SqlDbType.Int, 4);
			aParam[0].Value = t774_idaviso;
			aParam[1] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
			aParam[1].Value = t001_idficepi;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("GVT_FICEPIAVISOS_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_FICEPIAVISOS_D", aParam);
		}



		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Selecciona los registros de la tabla T775_FICEPIAVISOSGASVI en función de una foreign key.
		/// </summary>
		/// <returns>DataSet</returns>
		/// <history>
		/// 	Creado por [sqladmin]	28/04/2009 11:29:20
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader SelectByt774_idaviso(SqlTransaction tr, int t774_idaviso) 
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t774_idaviso", SqlDbType.Int, 4);
			aParam[0].Value = t774_idaviso;


			if (tr == null)
				return SqlHelper.ExecuteSqlDataReader("GVT_FICEPIAVISOS_SByt774_idaviso", aParam);
			else
				return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_FICEPIAVISOS_SByt774_idaviso", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Selecciona los registros de la tabla T775_FICEPIAVISOSGASVI en función de una foreign key.
		/// </summary>
		/// <returns>DataSet</returns>
		/// <history>
		/// 	Creado por [sqladmin]	28/04/2009 11:29:20
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader SelectByT001_idficepi(SqlTransaction tr, int t001_idficepi) 
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
			aParam[0].Value = t001_idficepi;


			if (tr == null)
				return SqlHelper.ExecuteSqlDataReader("GVT_FICEPIAVISOS_SByT001_idficepi", aParam);
			else
				return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_FICEPIAVISOS_SByT001_idficepi", aParam);
		}
        public static int CountByFicepi(SqlTransaction tr, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[0].Value = t001_idficepi;


            if (tr == null)
                return System.Convert.ToInt32(SqlHelper.ExecuteScalar("GVT_FICEPIAVISOS_CountByT001_idficepi", aParam));
            else
                return System.Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "GVT_FICEPIAVISOS_CountByT001_idficepi", aParam));
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta en la tabla T775_FICEPIAVISOSGASVI todos los profesionales activos.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	10/09/2009 11:29:20
        /// </history>
        /// -----------------------------------------------------------------------------
        //public static void InsertarTodos(SqlTransaction tr, int t774_idaviso)
        //{
        //    SqlParameter[] aParam = new SqlParameter[1];
        //    aParam[0] = new SqlParameter("@t774_idaviso", SqlDbType.Int, 4);
        //    aParam[0].Value = t774_idaviso;

        //    if (tr == null)
        //        SqlHelper.ExecuteNonQuery("GVT_FICEPIAVISOS_TODOS_I", aParam);
        //    else
        //        SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_FICEPIAVISOS_TODOS_I", aParam);
        //}
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Borra de la tabla T775_FICEPIAVISOSGASVI todos los profesionales
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	10/09/2009 11:29:20
        /// </history>
        /// -----------------------------------------------------------------------------
        //public static void BorrarTodos(SqlTransaction tr, int t774_idaviso)
        //{
        //    SqlParameter[] aParam = new SqlParameter[1];
        //    aParam[0] = new SqlParameter("@t774_idaviso", SqlDbType.Int, 4);
        //    aParam[0].Value = t774_idaviso;

        //    if (tr == null)
        //        SqlHelper.ExecuteNonQuery("GVT_FICEPIAVISOS_TODOS_D", aParam);
        //    else
        //        SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_FICEPIAVISOS_TODOS_D", aParam);
        //}

		#endregion
	}
}
