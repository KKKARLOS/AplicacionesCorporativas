using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : SUPERNODO3
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T393_SUPERNODO3
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	07/10/2009 13:00:55	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class SUPERNODO3
	{
		#region Propiedades y Atributos

		private int _t393_idsupernodo3;
		public int t393_idsupernodo3
		{
			get {return _t393_idsupernodo3;}
			set { _t393_idsupernodo3 = value ;}
		}

		private string _t393_denominacion;
		public string t393_denominacion
		{
			get {return _t393_denominacion;}
			set { _t393_denominacion = value ;}
		}

		private int _t394_idsupernodo4;
		public int t394_idsupernodo4
		{
			get {return _t394_idsupernodo4;}
			set { _t394_idsupernodo4 = value ;}
		}

		private bool _t393_estado;
		public bool t393_estado
		{
			get {return _t393_estado;}
			set { _t393_estado = value ;}
		}

		private int _t393_orden;
		public int t393_orden
		{
			get {return _t393_orden;}
			set { _t393_orden = value ;}
		}

		private int _t314_idusuario_responsable;
		public int t314_idusuario_responsable
		{
			get {return _t314_idusuario_responsable;}
			set { _t314_idusuario_responsable = value ;}
		}

		private string _t393_denominacion_CSN3P;
		public string t393_denominacion_CSN3P
		{
			get {return _t393_denominacion_CSN3P;}
			set { _t393_denominacion_CSN3P = value ;}
		}

		private bool _t393_obligatorio_CSN3P;
		public bool t393_obligatorio_CSN3P
		{
			get {return _t393_obligatorio_CSN3P;}
			set { _t393_obligatorio_CSN3P = value ;}
		}

        public bool activoqeq { get; set; }
		#endregion

		#region Constructor

		public SUPERNODO3() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T393_SUPERNODO3.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	07/10/2009 13:00:55
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Insert(SqlTransaction tr, string t393_denominacion , int t394_idsupernodo4 , bool t393_estado , 
                                 int t393_orden , int t314_idusuario_responsable , string t393_denominacion_CSN3P ,
                                 bool t393_obligatorio_CSN3P, bool activoqeq)
		{
			SqlParameter[] aParam = new SqlParameter[8];
			aParam[0] = new SqlParameter("@t393_denominacion", SqlDbType.Text, 50);
			aParam[0].Value = t393_denominacion;
			aParam[1] = new SqlParameter("@t394_idsupernodo4", SqlDbType.Int, 4);
			aParam[1].Value = t394_idsupernodo4;
			aParam[2] = new SqlParameter("@t393_estado", SqlDbType.Bit, 1);
			aParam[2].Value = t393_estado;
			aParam[3] = new SqlParameter("@t393_orden", SqlDbType.Int, 4);
			aParam[3].Value = t393_orden;
			aParam[4] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
			aParam[4].Value = t314_idusuario_responsable;
			aParam[5] = new SqlParameter("@t393_denominacion_CSN3P", SqlDbType.Text, 15);
			aParam[5].Value = t393_denominacion_CSN3P;
			aParam[6] = new SqlParameter("@t393_obligatorio_CSN3P", SqlDbType.Bit, 1);
			aParam[6].Value = t393_obligatorio_CSN3P;
            aParam[7] = new SqlParameter("@activoqeq", SqlDbType.Bit, 1);
            aParam[7].Value = activoqeq;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
				return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_SUPERNODO3_I", aParam));
			else
				return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_SUPERNODO3_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T393_SUPERNODO3.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	07/10/2009 13:00:55
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Update(SqlTransaction tr, int t393_idsupernodo3, string t393_denominacion, int t394_idsupernodo4, 
                                 bool t393_estado, int t393_orden, int t314_idusuario_responsable, string t393_denominacion_CSN3P, 
                                 bool t393_obligatorio_CSN3P, bool activoqeq)
		{
			SqlParameter[] aParam = new SqlParameter[9];
			aParam[0] = new SqlParameter("@t393_idsupernodo3", SqlDbType.Int, 4);
			aParam[0].Value = t393_idsupernodo3;
			aParam[1] = new SqlParameter("@t393_denominacion", SqlDbType.Text, 50);
			aParam[1].Value = t393_denominacion;
			aParam[2] = new SqlParameter("@t394_idsupernodo4", SqlDbType.Int, 4);
			aParam[2].Value = t394_idsupernodo4;
			aParam[3] = new SqlParameter("@t393_estado", SqlDbType.Bit, 1);
			aParam[3].Value = t393_estado;
			aParam[4] = new SqlParameter("@t393_orden", SqlDbType.Int, 4);
			aParam[4].Value = t393_orden;
			aParam[5] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
			aParam[5].Value = t314_idusuario_responsable;
			aParam[6] = new SqlParameter("@t393_denominacion_CSN3P", SqlDbType.Text, 15);
			aParam[6].Value = t393_denominacion_CSN3P;
			aParam[7] = new SqlParameter("@t393_obligatorio_CSN3P", SqlDbType.Bit, 1);
			aParam[7].Value = t393_obligatorio_CSN3P;
            aParam[8] = new SqlParameter("@activoqeq", SqlDbType.Bit, 1);
            aParam[8].Value = activoqeq;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_SUPERNODO3_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_SUPERNODO3_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T393_SUPERNODO3 a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	07/10/2009 13:00:55
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t393_idsupernodo3)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t393_idsupernodo3", SqlDbType.Int, 4);
			aParam[0].Value = t393_idsupernodo3;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_SUPERNODO3_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_SUPERNODO3_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T393_SUPERNODO3,
		/// y devuelve una instancia u objeto del tipo SUPERNODO3
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	07/10/2009 13:00:55
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SUPERNODO3 Select(SqlTransaction tr, int t393_idsupernodo3) 
		{
			SUPERNODO3 o = new SUPERNODO3();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t393_idsupernodo3", SqlDbType.Int, 4);
			aParam[0].Value = t393_idsupernodo3;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_SUPERNODO3_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_SUPERNODO3_S", aParam);

			if (dr.Read())
			{
				if (dr["t393_idsupernodo3"] != DBNull.Value)
					o.t393_idsupernodo3 = int.Parse(dr["t393_idsupernodo3"].ToString());
				if (dr["t393_denominacion"] != DBNull.Value)
					o.t393_denominacion = (string)dr["t393_denominacion"];
				if (dr["t394_idsupernodo4"] != DBNull.Value)
					o.t394_idsupernodo4 = int.Parse(dr["t394_idsupernodo4"].ToString());
				if (dr["t393_estado"] != DBNull.Value)
					o.t393_estado = (bool)dr["t393_estado"];
				if (dr["t393_orden"] != DBNull.Value)
					o.t393_orden = int.Parse(dr["t393_orden"].ToString());
				if (dr["t314_idusuario_responsable"] != DBNull.Value)
					o.t314_idusuario_responsable = int.Parse(dr["t314_idusuario_responsable"].ToString());
				if (dr["t393_denominacion_CSN3P"] != DBNull.Value)
					o.t393_denominacion_CSN3P = (string)dr["t393_denominacion_CSN3P"];
				if (dr["t393_obligatorio_CSN3P"] != DBNull.Value)
					o.t393_obligatorio_CSN3P = (bool)dr["t393_obligatorio_CSN3P"];

			}
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de SUPERNODO3"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T393_SUPERNODO3.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	07/10/2009 13:00:55
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader Catalogo(Nullable<int> t393_idsupernodo3, string t393_denominacion, Nullable<int> t394_idsupernodo4, Nullable<bool> t393_estado, Nullable<int> t393_orden, Nullable<int> t314_idusuario_responsable, string t393_denominacion_CSN3P, Nullable<bool> t393_obligatorio_CSN3P, byte nOrden, byte nAscDesc)
		{
			SqlParameter[] aParam = new SqlParameter[10];
			aParam[0] = new SqlParameter("@t393_idsupernodo3", SqlDbType.Int, 4);
			aParam[0].Value = t393_idsupernodo3;
			aParam[1] = new SqlParameter("@t393_denominacion", SqlDbType.Text, 50);
			aParam[1].Value = t393_denominacion;
			aParam[2] = new SqlParameter("@t394_idsupernodo4", SqlDbType.Int, 4);
			aParam[2].Value = t394_idsupernodo4;
			aParam[3] = new SqlParameter("@t393_estado", SqlDbType.Bit, 1);
			aParam[3].Value = t393_estado;
			aParam[4] = new SqlParameter("@t393_orden", SqlDbType.Int, 4);
			aParam[4].Value = t393_orden;
			aParam[5] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
			aParam[5].Value = t314_idusuario_responsable;
			aParam[6] = new SqlParameter("@t393_denominacion_CSN3P", SqlDbType.Text, 15);
			aParam[6].Value = t393_denominacion_CSN3P;
			aParam[7] = new SqlParameter("@t393_obligatorio_CSN3P", SqlDbType.Bit, 1);
			aParam[7].Value = t393_obligatorio_CSN3P;

			aParam[8] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
			aParam[8].Value = nOrden;
			aParam[9] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
			aParam[9].Value = nAscDesc;

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
			return SqlHelper.ExecuteSqlDataReader("SUP_SUPERNODO3_C", aParam);
		}

		#endregion
	}
}
