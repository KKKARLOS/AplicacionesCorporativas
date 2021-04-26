using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : FIGURANODO
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T308_FIGURANODO
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	10/06/2008 12:52:13	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class FIGURANODO
	{
		#region Propiedades y Atributos

		private int _t303_idnodo;
		public int t303_idnodo
		{
			get {return _t303_idnodo;}
			set { _t303_idnodo = value ;}
		}

		private int _t314_idusuario;
		public int t314_idusuario
		{
			get {return _t314_idusuario;}
			set { _t314_idusuario = value ;}
		}

		private string _t308_figura;
		public string t308_figura
		{
			get {return _t308_figura;}
			set { _t308_figura = value ;}
		}
		#endregion

		#region Constructores

		public FIGURANODO() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T308_FIGURANODO.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	10/06/2008 12:52:13
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t303_idnodo , int t314_idusuario , string t308_figura)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
			aParam[0].Value = t303_idnodo;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t308_figura", SqlDbType.Text, 1);
			aParam[2].Value = t308_figura;

			if (tr == null)
				 SqlHelper.ExecuteNonQuery("SUP_FIGURANODO_I", aParam);
			else
				 SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURANODO_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T308_FIGURANODO,
		/// y devuelve una instancia u objeto del tipo FIGURANODO
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	10/06/2008 12:52:13
		/// </history>
		/// -----------------------------------------------------------------------------
		public static FIGURANODO Select(SqlTransaction tr, int t303_idnodo, int t314_idusuario, string t308_figura) 
		{
			FIGURANODO o = new FIGURANODO();

			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
			aParam[0].Value = t303_idnodo;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t308_figura", SqlDbType.Text, 1);
			aParam[2].Value = t308_figura;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_FIGURANODO_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_FIGURANODO_S", aParam);

			if (dr.Read())
			{
				if (dr["t303_idnodo"] != DBNull.Value)
					o.t303_idnodo = int.Parse(dr["t303_idnodo"].ToString());
				if (dr["t314_idusuario"] != DBNull.Value)
					o.t314_idusuario = int.Parse(dr["t314_idusuario"].ToString());
				if (dr["t308_figura"] != DBNull.Value)
					o.t308_figura = (string)dr["t308_figura"];

			}
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de FIGURANODO"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T308_FIGURANODO.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	10/06/2008 12:52:13
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader Catalogo(Nullable<int> t303_idnodo, Nullable<int> t314_idusuario, string t308_figura, byte nOrden, byte nAscDesc)
		{
			SqlParameter[] aParam = new SqlParameter[5];
			aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
			aParam[0].Value = t303_idnodo;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t308_figura", SqlDbType.Text, 1);
			aParam[2].Value = t308_figura;

			aParam[3] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
			aParam[3].Value = nOrden;
			aParam[4] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
			aParam[4].Value = nAscDesc;

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
			return SqlHelper.ExecuteSqlDataReader("SUP_FIGURANODO_C", aParam);
		}

		#endregion
	}
}
