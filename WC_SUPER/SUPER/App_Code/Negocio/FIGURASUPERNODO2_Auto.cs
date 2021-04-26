using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : FIGURASUPERNODO2
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T396_FIGURASUPERNODO2
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	10/06/2008 12:52:14	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class FIGURASUPERNODO2
	{
		#region Propiedades y Atributos

		private int _t392_idsupernodo2;
		public int t392_idsupernodo2
		{
			get {return _t392_idsupernodo2;}
			set { _t392_idsupernodo2 = value ;}
		}

		private int _t314_idusuario;
		public int t314_idusuario
		{
			get {return _t314_idusuario;}
			set { _t314_idusuario = value ;}
		}

		private string _t396_figura;
		public string t396_figura
		{
			get {return _t396_figura;}
			set { _t396_figura = value ;}
		}
		#endregion

		#region Constructores

		public FIGURASUPERNODO2() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T396_FIGURASUPERNODO2.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	10/06/2008 12:52:14
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t392_idsupernodo2 , int t314_idusuario , string t396_figura)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t392_idsupernodo2", SqlDbType.Int, 4);
			aParam[0].Value = t392_idsupernodo2;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t396_figura", SqlDbType.Text, 1);
			aParam[2].Value = t396_figura;

			if (tr == null)
				 SqlHelper.ExecuteNonQuery("SUP_FIGURASUPERNODO2_I", aParam);
			else
				 SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURASUPERNODO2_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T396_FIGURASUPERNODO2.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	10/06/2008 12:52:14
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Update(SqlTransaction tr, int t392_idsupernodo2, int t314_idusuario, string t396_figura)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t392_idsupernodo2", SqlDbType.Int, 4);
			aParam[0].Value = t392_idsupernodo2;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t396_figura", SqlDbType.Text, 1);
			aParam[2].Value = t396_figura;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_FIGURASUPERNODO2_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURASUPERNODO2_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T396_FIGURASUPERNODO2,
		/// y devuelve una instancia u objeto del tipo FIGURASUPERNODO2
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	10/06/2008 12:52:14
		/// </history>
		/// -----------------------------------------------------------------------------
		public static FIGURASUPERNODO2 Select(SqlTransaction tr, int t392_idsupernodo2, int t314_idusuario, string t396_figura) 
		{
			FIGURASUPERNODO2 o = new FIGURASUPERNODO2();

			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t392_idsupernodo2", SqlDbType.Int, 4);
			aParam[0].Value = t392_idsupernodo2;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t396_figura", SqlDbType.Text, 1);
			aParam[2].Value = t396_figura;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_FIGURASUPERNODO2_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_FIGURASUPERNODO2_S", aParam);

			if (dr.Read())
			{
				if (dr["t392_idsupernodo2"] != DBNull.Value)
					o.t392_idsupernodo2 = int.Parse(dr["t392_idsupernodo2"].ToString());
				if (dr["t314_idusuario"] != DBNull.Value)
					o.t314_idusuario = int.Parse(dr["t314_idusuario"].ToString());
				if (dr["t396_figura"] != DBNull.Value)
					o.t396_figura = (string)dr["t396_figura"];

			}
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de FIGURASUPERNODO2"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T396_FIGURASUPERNODO2.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	10/06/2008 12:52:14
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader Catalogo(Nullable<int> t392_idsupernodo2, Nullable<int> t314_idusuario, string t396_figura, byte nOrden, byte nAscDesc)
		{
			SqlParameter[] aParam = new SqlParameter[5];
			aParam[0] = new SqlParameter("@t392_idsupernodo2", SqlDbType.Int, 4);
			aParam[0].Value = t392_idsupernodo2;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t396_figura", SqlDbType.Text, 1);
			aParam[2].Value = t396_figura;

			aParam[3] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
			aParam[3].Value = nOrden;
			aParam[4] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
			aParam[4].Value = nAscDesc;

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
			return SqlHelper.ExecuteSqlDataReader("SUP_FIGURASUPERNODO2_C", aParam);
		}

		#endregion
	}
}
