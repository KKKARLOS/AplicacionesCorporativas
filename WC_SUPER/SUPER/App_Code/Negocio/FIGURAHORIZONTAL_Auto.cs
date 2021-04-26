using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : FIGURAHORIZONTAL
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T312_FIGURAHORIZONTAL
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	19/12/2007 15:07:29	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class FIGURAHORIZONTAL
	{
		#region Propiedades y Atributos

		private int _t307_idhorizontal;
		public int t307_idhorizontal
		{
			get {return _t307_idhorizontal;}
			set { _t307_idhorizontal = value ;}
		}

		private int _t314_idusuario;
		public int t314_idusuario
		{
			get {return _t314_idusuario;}
			set { _t314_idusuario = value ;}
		}

		private string _t312_figura;
		public string t312_figura
		{
			get {return _t312_figura;}
			set { _t312_figura = value ;}
		}
		#endregion

		#region Constructores

		public FIGURAHORIZONTAL() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T312_FIGURAHORIZONTAL.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	19/12/2007 15:07:29
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t307_idhorizontal , int t314_idusuario , string t312_figura)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t307_idhorizontal", SqlDbType.Int, 4);
			aParam[0].Value = t307_idhorizontal;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t312_figura", SqlDbType.Text, 1);
			aParam[2].Value = t312_figura;

			if (tr == null)
				SqlHelper.ExecuteNonQuery("SUP_FIGURAHORIZONTAL_I", aParam);
			else
				SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURAHORIZONTAL_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T312_FIGURAHORIZONTAL.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	19/12/2007 15:07:29
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Update(SqlTransaction tr, int t307_idhorizontal, int t314_idusuario, string t312_figura)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t307_idhorizontal", SqlDbType.Int, 4);
			aParam[0].Value = t307_idhorizontal;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t312_figura", SqlDbType.Text, 1);
			aParam[2].Value = t312_figura;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FIGURAHORIZONTAL_U", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURAHORIZONTAL_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T312_FIGURAHORIZONTAL a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	19/12/2007 15:07:29
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t307_idhorizontal, int t314_idusuario)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t307_idhorizontal", SqlDbType.Int, 4);
			aParam[0].Value = t307_idhorizontal;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;

			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FIGURAHORIZONTAL_D", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURAHORIZONTAL_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T312_FIGURAHORIZONTAL,
		/// y devuelve una instancia u objeto del tipo FIGURAHORIZONTAL
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	19/12/2007 15:07:29
		/// </history>
		/// -----------------------------------------------------------------------------
		public static FIGURAHORIZONTAL Select(SqlTransaction tr, int t307_idhorizontal, int t314_idusuario) 
		{
			FIGURAHORIZONTAL o = new FIGURAHORIZONTAL();

			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t307_idhorizontal", SqlDbType.Int, 4);
			aParam[0].Value = t307_idhorizontal;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_FIGURAHORIZONTAL_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_FIGURAHORIZONTAL_S", aParam);

			if (dr.Read())
			{
				if (dr["t307_idhorizontal"] != DBNull.Value)
					o.t307_idhorizontal = int.Parse(dr["t307_idhorizontal"].ToString());
				if (dr["t314_idusuario"] != DBNull.Value)
					o.t314_idusuario = int.Parse(dr["t314_idusuario"].ToString());
				if (dr["t312_figura"] != DBNull.Value)
					o.t312_figura = (string)dr["t312_figura"];

			}
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de FIGURAHORIZONTAL"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T312_FIGURAHORIZONTAL.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	19/12/2007 15:07:29
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader Catalogo(Nullable<int> t307_idhorizontal, Nullable<int> t314_idusuario, string t312_figura, byte nOrden, byte nAscDesc)
		{
			SqlParameter[] aParam = new SqlParameter[5];
			aParam[0] = new SqlParameter("@t307_idhorizontal", SqlDbType.Int, 4);
			aParam[0].Value = t307_idhorizontal;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t312_figura", SqlDbType.Text, 1);
			aParam[2].Value = t312_figura;

			aParam[3] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
			aParam[3].Value = nOrden;
			aParam[4] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
			aParam[4].Value = nAscDesc;

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_FIGURAHORIZONTAL_C", aParam);
		}

		#endregion
	}
}
