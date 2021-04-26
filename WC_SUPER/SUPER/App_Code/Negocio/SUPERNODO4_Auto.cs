using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : SUPERNODO4
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T394_SUPERNODO4
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	07/10/2009 13:00:55	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class SUPERNODO4
	{
		#region Propiedades y Atributos

		private int _t394_idsupernodo4;
		public int t394_idsupernodo4
		{
			get {return _t394_idsupernodo4;}
			set { _t394_idsupernodo4 = value ;}
		}

		private string _t394_denominacion;
		public string t394_denominacion
		{
			get {return _t394_denominacion;}
			set { _t394_denominacion = value ;}
		}

		private bool _t394_estado;
		public bool t394_estado
		{
			get {return _t394_estado;}
			set { _t394_estado = value ;}
		}

		private int _t394_orden;
		public int t394_orden
		{
			get {return _t394_orden;}
			set { _t394_orden = value ;}
		}

		private int _t314_idusuario_responsable;
		public int t314_idusuario_responsable
		{
			get {return _t314_idusuario_responsable;}
			set { _t314_idusuario_responsable = value ;}
		}

		private string _t394_denominacion_CSN4P;
		public string t394_denominacion_CSN4P
		{
			get {return _t394_denominacion_CSN4P;}
			set { _t394_denominacion_CSN4P = value ;}
		}

		private bool _t394_obligatorio_CSN4P;
		public bool t394_obligatorio_CSN4P
		{
			get {return _t394_obligatorio_CSN4P;}
			set { _t394_obligatorio_CSN4P = value ;}
		}

        public bool activoqeq { get; set; }
		#endregion

		#region Constructor

		public SUPERNODO4() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T394_SUPERNODO4.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	07/10/2009 13:00:55
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Insert(SqlTransaction tr, string t394_denominacion , bool t394_estado , int t394_orden , 
                                 int t314_idusuario_responsable , string t394_denominacion_CSN4P , bool t394_obligatorio_CSN4P
                                , bool activoqeq)
		{
			SqlParameter[] aParam = new SqlParameter[7];
			aParam[0] = new SqlParameter("@t394_denominacion", SqlDbType.Text, 50);
			aParam[0].Value = t394_denominacion;
			aParam[1] = new SqlParameter("@t394_estado", SqlDbType.Bit, 1);
			aParam[1].Value = t394_estado;
			aParam[2] = new SqlParameter("@t394_orden", SqlDbType.Int, 4);
			aParam[2].Value = t394_orden;
			aParam[3] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
			aParam[3].Value = t314_idusuario_responsable;
			aParam[4] = new SqlParameter("@t394_denominacion_CSN4P", SqlDbType.Text, 15);
			aParam[4].Value = t394_denominacion_CSN4P;
            aParam[5] = new SqlParameter("@t394_obligatorio_CSN4P", SqlDbType.Bit, 1);
            aParam[5].Value = t394_obligatorio_CSN4P;
            aParam[6] = new SqlParameter("@activoqeq", SqlDbType.Bit, 1);
            aParam[6].Value = activoqeq;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
				return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_SUPERNODO4_I", aParam));
			else
				return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_SUPERNODO4_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T394_SUPERNODO4.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	07/10/2009 13:00:55
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Update(SqlTransaction tr, int t394_idsupernodo4, string t394_denominacion, bool t394_estado, int t394_orden, 
                                 int t314_idusuario_responsable, string t394_denominacion_CSN4P, bool t394_obligatorio_CSN4P,
                                 bool activoqeq)
		{
			SqlParameter[] aParam = new SqlParameter[8];
			aParam[0] = new SqlParameter("@t394_idsupernodo4", SqlDbType.Int, 4);
			aParam[0].Value = t394_idsupernodo4;
			aParam[1] = new SqlParameter("@t394_denominacion", SqlDbType.Text, 50);
			aParam[1].Value = t394_denominacion;
			aParam[2] = new SqlParameter("@t394_estado", SqlDbType.Bit, 1);
			aParam[2].Value = t394_estado;
			aParam[3] = new SqlParameter("@t394_orden", SqlDbType.Int, 4);
			aParam[3].Value = t394_orden;
			aParam[4] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
			aParam[4].Value = t314_idusuario_responsable;
			aParam[5] = new SqlParameter("@t394_denominacion_CSN4P", SqlDbType.Text, 15);
			aParam[5].Value = t394_denominacion_CSN4P;
			aParam[6] = new SqlParameter("@t394_obligatorio_CSN4P", SqlDbType.Bit, 1);
			aParam[6].Value = t394_obligatorio_CSN4P;
            aParam[7] = new SqlParameter("@activoqeq", SqlDbType.Bit, 1);
            aParam[7].Value = activoqeq;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_SUPERNODO4_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_SUPERNODO4_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T394_SUPERNODO4 a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	07/10/2009 13:00:55
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t394_idsupernodo4)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t394_idsupernodo4", SqlDbType.Int, 4);
			aParam[0].Value = t394_idsupernodo4;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_SUPERNODO4_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_SUPERNODO4_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T394_SUPERNODO4,
		/// y devuelve una instancia u objeto del tipo SUPERNODO4
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	07/10/2009 13:00:55
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SUPERNODO4 Select(SqlTransaction tr, int t394_idsupernodo4) 
		{
			SUPERNODO4 o = new SUPERNODO4();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t394_idsupernodo4", SqlDbType.Int, 4);
			aParam[0].Value = t394_idsupernodo4;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_SUPERNODO4_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_SUPERNODO4_S", aParam);

			if (dr.Read())
			{
				if (dr["t394_idsupernodo4"] != DBNull.Value)
					o.t394_idsupernodo4 = int.Parse(dr["t394_idsupernodo4"].ToString());
				if (dr["t394_denominacion"] != DBNull.Value)
					o.t394_denominacion = (string)dr["t394_denominacion"];
				if (dr["t394_estado"] != DBNull.Value)
					o.t394_estado = (bool)dr["t394_estado"];
				if (dr["t394_orden"] != DBNull.Value)
					o.t394_orden = int.Parse(dr["t394_orden"].ToString());
				if (dr["t314_idusuario_responsable"] != DBNull.Value)
					o.t314_idusuario_responsable = int.Parse(dr["t314_idusuario_responsable"].ToString());
				if (dr["t394_denominacion_CSN4P"] != DBNull.Value)
					o.t394_denominacion_CSN4P = (string)dr["t394_denominacion_CSN4P"];
				if (dr["t394_obligatorio_CSN4P"] != DBNull.Value)
					o.t394_obligatorio_CSN4P = (bool)dr["t394_obligatorio_CSN4P"];

			}
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de SUPERNODO4"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T394_SUPERNODO4.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	07/10/2009 13:00:55
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader Catalogo(Nullable<int> t394_idsupernodo4, string t394_denominacion, Nullable<bool> t394_estado, Nullable<int> t394_orden, Nullable<int> t314_idusuario_responsable, string t394_denominacion_CSN4P, Nullable<bool> t394_obligatorio_CSN4P, byte nOrden, byte nAscDesc)
		{
			SqlParameter[] aParam = new SqlParameter[9];
			aParam[0] = new SqlParameter("@t394_idsupernodo4", SqlDbType.Int, 4);
			aParam[0].Value = t394_idsupernodo4;
			aParam[1] = new SqlParameter("@t394_denominacion", SqlDbType.Text, 50);
			aParam[1].Value = t394_denominacion;
			aParam[2] = new SqlParameter("@t394_estado", SqlDbType.Bit, 1);
			aParam[2].Value = t394_estado;
			aParam[3] = new SqlParameter("@t394_orden", SqlDbType.Int, 4);
			aParam[3].Value = t394_orden;
			aParam[4] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
			aParam[4].Value = t314_idusuario_responsable;
			aParam[5] = new SqlParameter("@t394_denominacion_CSN4P", SqlDbType.Text, 15);
			aParam[5].Value = t394_denominacion_CSN4P;
			aParam[6] = new SqlParameter("@t394_obligatorio_CSN4P", SqlDbType.Bit, 1);
			aParam[6].Value = t394_obligatorio_CSN4P;

			aParam[7] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
			aParam[7].Value = nOrden;
			aParam[8] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
			aParam[8].Value = nAscDesc;

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
			return SqlHelper.ExecuteSqlDataReader("SUP_SUPERNODO4_C", aParam);
		}

		#endregion
	}
}
