using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace GEMO.DAL
{
	/// -----------------------------------------------------------------------------
	/// Project	 : GEMO
	/// Class	 : REASIGLINEA
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T713_REASIGLINEA
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	29/04/2011 11:32:34	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class REASIGLINEA
	{
		#region Propiedades y Atributos

		private int _t708_idlinea;
		public int t708_idlinea
		{
			get {return _t708_idlinea;}
			set { _t708_idlinea = value ;}
		}

		private int _t001_idficepi;
		public int t001_idficepi
		{
			get {return _t001_idficepi;}
			set { _t001_idficepi = value ;}
		}

		private int _t001_responsable;
		public int t001_responsable
		{
			get {return _t001_responsable;}
			set { _t001_responsable = value ;}
		}

		private bool? _t713_procesado;
		public bool? t713_procesado
		{
			get {return _t713_procesado;}
			set { _t713_procesado = value ;}
		}

		private string _t713_excepcion;
		public string t713_excepcion
		{
			get {return _t713_excepcion;}
			set { _t713_excepcion = value ;}
		}
		#endregion

		#region Constructor

		public REASIGLINEA() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T713_REASIGLINEA.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	29/04/2011 11:32:34
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t708_idlinea , int t001_idficepi , int t001_responsable , Nullable<bool> t713_procesado , string t713_excepcion)
		{
			SqlParameter[] aParam = new SqlParameter[5];
			aParam[0] = new SqlParameter("@t708_idlinea", SqlDbType.Int, 4);
			aParam[0].Value = t708_idlinea;
			aParam[1] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
			aParam[1].Value = t001_idficepi;
			aParam[2] = new SqlParameter("@t001_responsable", SqlDbType.Int, 4);
			aParam[2].Value = t001_responsable;
			aParam[3] = new SqlParameter("@t713_procesado", SqlDbType.Bit, 1);
			aParam[3].Value = t713_procesado;
			aParam[4] = new SqlParameter("@t713_excepcion", SqlDbType.Text, 2147483647);
			aParam[4].Value = t713_excepcion;

			if (tr == null)
				 SqlHelper.ExecuteNonQuery("GEM_REASIGLINEA_I", aParam);
			else
				 SqlHelper.ExecuteNonQueryTransaccion(tr, "GEM_REASIGLINEA_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T713_REASIGLINEA.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	29/04/2011 11:32:34
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Update(SqlTransaction tr, int t708_idlinea, int t001_idficepi, int t001_responsable, Nullable<bool> t713_procesado, string t713_excepcion)
		{
			SqlParameter[] aParam = new SqlParameter[5];
			aParam[0] = new SqlParameter("@t708_idlinea", SqlDbType.Int, 4);
			aParam[0].Value = t708_idlinea;
			aParam[1] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
			aParam[1].Value = t001_idficepi;
			aParam[2] = new SqlParameter("@t001_responsable", SqlDbType.Int, 4);
			aParam[2].Value = t001_responsable;
			aParam[3] = new SqlParameter("@t713_procesado", SqlDbType.Bit, 1);
			aParam[3].Value = t713_procesado;
			aParam[4] = new SqlParameter("@t713_excepcion", SqlDbType.Text, 2147483647);
			aParam[4].Value = t713_excepcion;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("GEM_REASIGLINEA_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "GEM_REASIGLINEA_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T713_REASIGLINEA a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	29/04/2011 11:32:34
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t001_idficepi)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
			aParam[0].Value = t001_idficepi;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("GEM_REASIGLINEA_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "GEM_REASIGLINEA_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T713_REASIGLINEA,
		/// y devuelve una instancia u objeto del tipo REASIGLINEA
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	29/04/2011 11:32:34
		/// </history>
		/// -----------------------------------------------------------------------------
		public static REASIGLINEA Select(SqlTransaction tr, int t708_idlinea, int t001_idficepi) 
		{
			REASIGLINEA o = new REASIGLINEA();

			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t708_idlinea", SqlDbType.Int, 4);
			aParam[0].Value = t708_idlinea;
			aParam[1] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
			aParam[1].Value = t001_idficepi;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("GEM_REASIGLINEA_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GEM_REASIGLINEA_S", aParam);

			if (dr.Read())
			{
				if (dr["t708_idlinea"] != DBNull.Value)
					o.t708_idlinea = int.Parse(dr["t708_idlinea"].ToString());
				if (dr["t001_idficepi"] != DBNull.Value)
					o.t001_idficepi = int.Parse(dr["t001_idficepi"].ToString());
				if (dr["t001_responsable"] != DBNull.Value)
					o.t001_responsable = int.Parse(dr["t001_responsable"].ToString());
				if (dr["t713_procesado"] != DBNull.Value)
					o.t713_procesado = (bool)dr["t713_procesado"];
				if (dr["t713_excepcion"] != DBNull.Value)
					o.t713_excepcion = (string)dr["t713_excepcion"];

			}
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de REASIGLINEA"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}
        public static SqlDataReader CatalogoDestinoResp(SqlTransaction tr, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[0].Value = t001_idficepi;


            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("GEM_REASIGNACIONLIN_RES_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GEM_REASIGNACIONLIN_RES_CAT", aParam);
        }
		#endregion
	}
}
