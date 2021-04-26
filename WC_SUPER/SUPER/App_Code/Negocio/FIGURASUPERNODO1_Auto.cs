using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : FIGURASUPERNODO1
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T395_FIGURASUPERNODO1
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	10/06/2008 12:52:14	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class FIGURASUPERNODO1
	{
		#region Propiedades y Atributos

		private int _t391_idsupernodo1;
		public int t391_idsupernodo1
		{
			get {return _t391_idsupernodo1;}
			set { _t391_idsupernodo1 = value ;}
		}

		private int _t314_idusuario;
		public int t314_idusuario
		{
			get {return _t314_idusuario;}
			set { _t314_idusuario = value ;}
		}

		private string _t395_figura;
		public string t395_figura
		{
			get {return _t395_figura;}
			set { _t395_figura = value ;}
		}
		#endregion

		#region Constructores

		public FIGURASUPERNODO1() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T395_FIGURASUPERNODO1.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	10/06/2008 12:52:14
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t391_idsupernodo1 , int t314_idusuario , string t395_figura)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t391_idsupernodo1", SqlDbType.Int, 4);
			aParam[0].Value = t391_idsupernodo1;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t395_figura", SqlDbType.Text, 1);
			aParam[2].Value = t395_figura;

			if (tr == null)
				 SqlHelper.ExecuteNonQuery("SUP_FIGURASUPERNODO1_I", aParam);
			else
				 SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURASUPERNODO1_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T395_FIGURASUPERNODO1.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	10/06/2008 12:52:14
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Update(SqlTransaction tr, int t391_idsupernodo1, int t314_idusuario, string t395_figura)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t391_idsupernodo1", SqlDbType.Int, 4);
			aParam[0].Value = t391_idsupernodo1;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t395_figura", SqlDbType.Text, 1);
			aParam[2].Value = t395_figura;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_FIGURASUPERNODO1_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURASUPERNODO1_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T395_FIGURASUPERNODO1,
		/// y devuelve una instancia u objeto del tipo FIGURASUPERNODO1
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	10/06/2008 12:52:14
		/// </history>
		/// -----------------------------------------------------------------------------
		public static FIGURASUPERNODO1 Select(SqlTransaction tr, int t391_idsupernodo1, int t314_idusuario, string t395_figura) 
		{
			FIGURASUPERNODO1 o = new FIGURASUPERNODO1();

			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t391_idsupernodo1", SqlDbType.Int, 4);
			aParam[0].Value = t391_idsupernodo1;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t395_figura", SqlDbType.Text, 1);
			aParam[2].Value = t395_figura;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_FIGURASUPERNODO1_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_FIGURASUPERNODO1_S", aParam);

			if (dr.Read())
			{
				if (dr["t391_idsupernodo1"] != DBNull.Value)
					o.t391_idsupernodo1 = int.Parse(dr["t391_idsupernodo1"].ToString());
				if (dr["t314_idusuario"] != DBNull.Value)
					o.t314_idusuario = int.Parse(dr["t314_idusuario"].ToString());
				if (dr["t395_figura"] != DBNull.Value)
					o.t395_figura = (string)dr["t395_figura"];

			}
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de FIGURASUPERNODO1"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T395_FIGURASUPERNODO1.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	10/06/2008 12:52:14
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader Catalogo(Nullable<int> t391_idsupernodo1, Nullable<int> t314_idusuario, string t395_figura, byte nOrden, byte nAscDesc)
		{
			SqlParameter[] aParam = new SqlParameter[5];
			aParam[0] = new SqlParameter("@t391_idsupernodo1", SqlDbType.Int, 4);
			aParam[0].Value = t391_idsupernodo1;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t395_figura", SqlDbType.Text, 1);
			aParam[2].Value = t395_figura;

			aParam[3] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
			aParam[3].Value = nOrden;
			aParam[4] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
			aParam[4].Value = nAscDesc;

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
			return SqlHelper.ExecuteSqlDataReader("SUP_FIGURASUPERNODO1_C", aParam);
		}

		#endregion
	}
}
