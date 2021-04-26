using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : FIGURASUPERNODO4
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T398_FIGURASUPERNODO4
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	10/06/2008 12:52:14	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class FIGURASUPERNODO4
	{

		#region Propiedades y Atributos

		private int _t394_idsupernodo4;
		public int t394_idsupernodo4
		{
			get {return _t394_idsupernodo4;}
			set { _t394_idsupernodo4 = value ;}
		}

		private int _t314_idusuario;
		public int t314_idusuario
		{
			get {return _t314_idusuario;}
			set { _t314_idusuario = value ;}
		}

		private string _t398_figura;
		public string t398_figura
		{
			get {return _t398_figura;}
			set { _t398_figura = value ;}
		}
		#endregion

		#region Constructores

		public FIGURASUPERNODO4() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T398_FIGURASUPERNODO4.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	10/06/2008 12:52:14
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t394_idsupernodo4 , int t314_idusuario , string t398_figura)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t394_idsupernodo4", SqlDbType.Int, 4);
			aParam[0].Value = t394_idsupernodo4;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t398_figura", SqlDbType.Text, 1);
			aParam[2].Value = t398_figura;

			if (tr == null)
				 SqlHelper.ExecuteNonQuery("SUP_FIGURASUPERNODO4_I", aParam);
			else
				 SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURASUPERNODO4_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T398_FIGURASUPERNODO4.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	10/06/2008 12:52:14
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Update(SqlTransaction tr, int t394_idsupernodo4, int t314_idusuario, string t398_figura)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t394_idsupernodo4", SqlDbType.Int, 4);
			aParam[0].Value = t394_idsupernodo4;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t398_figura", SqlDbType.Text, 1);
			aParam[2].Value = t398_figura;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_FIGURASUPERNODO4_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURASUPERNODO4_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T398_FIGURASUPERNODO4,
		/// y devuelve una instancia u objeto del tipo FIGURASUPERNODO4
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	10/06/2008 12:52:14
		/// </history>
		/// -----------------------------------------------------------------------------
		public static FIGURASUPERNODO4 Select(SqlTransaction tr, int t394_idsupernodo4, int t314_idusuario, string t398_figura) 
		{
			FIGURASUPERNODO4 o = new FIGURASUPERNODO4();

			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t394_idsupernodo4", SqlDbType.Int, 4);
			aParam[0].Value = t394_idsupernodo4;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t398_figura", SqlDbType.Text, 1);
			aParam[2].Value = t398_figura;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_FIGURASUPERNODO4_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_FIGURASUPERNODO4_S", aParam);

			if (dr.Read())
			{
				if (dr["t394_idsupernodo4"] != DBNull.Value)
					o.t394_idsupernodo4 = int.Parse(dr["t394_idsupernodo4"].ToString());
				if (dr["t314_idusuario"] != DBNull.Value)
					o.t314_idusuario = int.Parse(dr["t314_idusuario"].ToString());
				if (dr["t398_figura"] != DBNull.Value)
					o.t398_figura = (string)dr["t398_figura"];

			}
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de FIGURASUPERNODO4"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T398_FIGURASUPERNODO4.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	10/06/2008 12:52:14
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader Catalogo(Nullable<int> t394_idsupernodo4, Nullable<int> t314_idusuario, string t398_figura, byte nOrden, byte nAscDesc)
		{
			SqlParameter[] aParam = new SqlParameter[5];
			aParam[0] = new SqlParameter("@t394_idsupernodo4", SqlDbType.Int, 4);
			aParam[0].Value = t394_idsupernodo4;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t398_figura", SqlDbType.Text, 1);
			aParam[2].Value = t398_figura;

			aParam[3] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
			aParam[3].Value = nOrden;
			aParam[4] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
			aParam[4].Value = nAscDesc;

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
			return SqlHelper.ExecuteSqlDataReader("SUP_FIGURASUPERNODO4_C", aParam);
		}

		#endregion
	}
}
