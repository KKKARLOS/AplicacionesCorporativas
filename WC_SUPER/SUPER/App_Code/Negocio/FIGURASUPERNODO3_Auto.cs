using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : FIGURASUPERNODO3
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T397_FIGURASUPERNODO3
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	10/06/2008 12:52:14	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class FIGURASUPERNODO3
	{
		#region Propiedades y Atributos

		private int _t393_idsupernodo3;
		public int t393_idsupernodo3
		{
			get {return _t393_idsupernodo3;}
			set { _t393_idsupernodo3 = value ;}
		}

		private int _t314_idusuario;
		public int t314_idusuario
		{
			get {return _t314_idusuario;}
			set { _t314_idusuario = value ;}
		}

		private string _t397_figura;
		public string t397_figura
		{
			get {return _t397_figura;}
			set { _t397_figura = value ;}
		}
		#endregion

		#region Constructores

		public FIGURASUPERNODO3() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T397_FIGURASUPERNODO3.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	10/06/2008 12:52:14
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t393_idsupernodo3 , int t314_idusuario , string t397_figura)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t393_idsupernodo3", SqlDbType.Int, 4);
			aParam[0].Value = t393_idsupernodo3;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t397_figura", SqlDbType.Text, 1);
			aParam[2].Value = t397_figura;

			if (tr == null)
				 SqlHelper.ExecuteNonQuery("SUP_FIGURASUPERNODO3_I", aParam);
			else
				 SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURASUPERNODO3_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T397_FIGURASUPERNODO3.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	10/06/2008 12:52:14
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Update(SqlTransaction tr, int t393_idsupernodo3, int t314_idusuario, string t397_figura)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t393_idsupernodo3", SqlDbType.Int, 4);
			aParam[0].Value = t393_idsupernodo3;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t397_figura", SqlDbType.Text, 1);
			aParam[2].Value = t397_figura;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_FIGURASUPERNODO3_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURASUPERNODO3_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T397_FIGURASUPERNODO3,
		/// y devuelve una instancia u objeto del tipo FIGURASUPERNODO3
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	10/06/2008 12:52:14
		/// </history>
		/// -----------------------------------------------------------------------------
		public static FIGURASUPERNODO3 Select(SqlTransaction tr, int t393_idsupernodo3, int t314_idusuario, string t397_figura) 
		{
			FIGURASUPERNODO3 o = new FIGURASUPERNODO3();

			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t393_idsupernodo3", SqlDbType.Int, 4);
			aParam[0].Value = t393_idsupernodo3;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t397_figura", SqlDbType.Text, 1);
			aParam[2].Value = t397_figura;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_FIGURASUPERNODO3_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_FIGURASUPERNODO3_S", aParam);

			if (dr.Read())
			{
				if (dr["t393_idsupernodo3"] != DBNull.Value)
					o.t393_idsupernodo3 = int.Parse(dr["t393_idsupernodo3"].ToString());
				if (dr["t314_idusuario"] != DBNull.Value)
					o.t314_idusuario = int.Parse(dr["t314_idusuario"].ToString());
				if (dr["t397_figura"] != DBNull.Value)
					o.t397_figura = (string)dr["t397_figura"];

			}
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de FIGURASUPERNODO3"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T397_FIGURASUPERNODO3.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	10/06/2008 12:52:14
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader Catalogo(Nullable<int> t393_idsupernodo3, Nullable<int> t314_idusuario, string t397_figura, byte nOrden, byte nAscDesc)
		{
			SqlParameter[] aParam = new SqlParameter[5];
			aParam[0] = new SqlParameter("@t393_idsupernodo3", SqlDbType.Int, 4);
			aParam[0].Value = t393_idsupernodo3;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t397_figura", SqlDbType.Text, 1);
			aParam[2].Value = t397_figura;

			aParam[3] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
			aParam[3].Value = nOrden;
			aParam[4] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
			aParam[4].Value = nAscDesc;

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
			return SqlHelper.ExecuteSqlDataReader("SUP_FIGURASUPERNODO3_C", aParam);
		}

		#endregion
	}
}
