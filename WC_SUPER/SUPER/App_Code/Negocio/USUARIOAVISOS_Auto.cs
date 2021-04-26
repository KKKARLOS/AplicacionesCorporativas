using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : USUARIOAVISOS
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T449_USUARIOAVISOS
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	28/04/2009 11:29:20	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class USUARIOAVISOS
	{
		#region Propiedades y Atributos

		private int _t448_idaviso;
		public int t448_idaviso
		{
			get {return _t448_idaviso;}
			set { _t448_idaviso = value ;}
		}

		private int _t314_idusuario;
		public int t314_idusuario
		{
			get {return _t314_idusuario;}
			set { _t314_idusuario = value ;}
		}
		#endregion

		#region Constructor

		public USUARIOAVISOS() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T449_USUARIOAVISOS.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	28/04/2009 11:29:20
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t448_idaviso , int t314_idusuario)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t448_idaviso", SqlDbType.Int, 4);
			aParam[0].Value = t448_idaviso;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;

			if (tr == null)
				 SqlHelper.ExecuteNonQuery("SUP_USUARIOAVISOS_I", aParam);
			else
				 SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_USUARIOAVISOS_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T449_USUARIOAVISOS.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	28/04/2009 11:29:20
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Update(SqlTransaction tr, int t448_idaviso, int t314_idusuario)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t448_idaviso", SqlDbType.Int, 4);
			aParam[0].Value = t448_idaviso;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_USUARIOAVISOS_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_USUARIOAVISOS_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T449_USUARIOAVISOS a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	28/04/2009 11:29:20
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t448_idaviso, int t314_idusuario)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t448_idaviso", SqlDbType.Int, 4);
			aParam[0].Value = t448_idaviso;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_USUARIOAVISOS_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_USUARIOAVISOS_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T449_USUARIOAVISOS,
		/// y devuelve una instancia u objeto del tipo USUARIOAVISOS
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	28/04/2009 11:29:20
		/// </history>
		/// -----------------------------------------------------------------------------
		public static USUARIOAVISOS Select(SqlTransaction tr, int t448_idaviso, int t314_idusuario) 
		{
			USUARIOAVISOS o = new USUARIOAVISOS();

			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t448_idaviso", SqlDbType.Int, 4);
			aParam[0].Value = t448_idaviso;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_USUARIOAVISOS_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_USUARIOAVISOS_S", aParam);

			if (dr.Read())
			{
				if (dr["t448_idaviso"] != DBNull.Value)
					o.t448_idaviso = int.Parse(dr["t448_idaviso"].ToString());
				if (dr["t314_idusuario"] != DBNull.Value)
					o.t314_idusuario = int.Parse(dr["t314_idusuario"].ToString());

			}
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de USUARIOAVISOS"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Selecciona los registros de la tabla T449_USUARIOAVISOS en función de una foreign key.
		/// </summary>
		/// <returns>DataSet</returns>
		/// <history>
		/// 	Creado por [sqladmin]	28/04/2009 11:29:20
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader SelectByT448_idaviso(SqlTransaction tr, int t448_idaviso) 
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t448_idaviso", SqlDbType.Int, 4);
			aParam[0].Value = t448_idaviso;


			if (tr == null)
				return SqlHelper.ExecuteSqlDataReader("SUP_USUARIOAVISOS_SByT448_idaviso", aParam);
			else
				return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_USUARIOAVISOS_SByT448_idaviso", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Selecciona los registros de la tabla T449_USUARIOAVISOS en función de una foreign key.
		/// </summary>
		/// <returns>DataSet</returns>
		/// <history>
		/// 	Creado por [sqladmin]	28/04/2009 11:29:20
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader SelectByT314_idusuario(SqlTransaction tr, int t314_idusuario) 
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[0].Value = t314_idusuario;


			if (tr == null)
				return SqlHelper.ExecuteSqlDataReader("SUP_USUARIOAVISOS_SByT314_idusuario", aParam);
			else
				return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_USUARIOAVISOS_SByT314_idusuario", aParam);
		}
        public static int CountByUsuario(SqlTransaction tr, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;


            if (tr == null)
                return System.Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_USUARIOAVISOS_CountByT314_idusuario", aParam));
            else
                return System.Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_USUARIOAVISOS_CountByT314_idusuario", aParam));
        }

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T449_USUARIOAVISOS.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	28/04/2009 11:29:20
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader Catalogo(Nullable<int> t448_idaviso, Nullable<int> t314_idusuario, byte nOrden, byte nAscDesc)
		{
			SqlParameter[] aParam = new SqlParameter[4];
			aParam[0] = new SqlParameter("@t448_idaviso", SqlDbType.Int, 4);
			aParam[0].Value = t448_idaviso;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;

			aParam[2] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
			aParam[2].Value = nOrden;
			aParam[3] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
			aParam[3].Value = nAscDesc;

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
			return SqlHelper.ExecuteSqlDataReader("SUP_USUARIOAVISOS_C", aParam);
		}

		#endregion
	}
}
