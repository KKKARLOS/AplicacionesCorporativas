using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : SUPERNODO2
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T392_SUPERNODO2
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	07/10/2009 13:00:55	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class SUPERNODO2
	{
		#region Propiedades y Atributos

		private int _t392_idsupernodo2;
		public int t392_idsupernodo2
		{
			get {return _t392_idsupernodo2;}
			set { _t392_idsupernodo2 = value ;}
		}

		private string _t392_denominacion;
		public string t392_denominacion
		{
			get {return _t392_denominacion;}
			set { _t392_denominacion = value ;}
		}

		private bool _t392_estado;
		public bool t392_estado
		{
			get {return _t392_estado;}
			set { _t392_estado = value ;}
		}

		private int _t393_idsupernodo3;
		public int t393_idsupernodo3
		{
			get {return _t393_idsupernodo3;}
			set { _t393_idsupernodo3 = value ;}
		}

		private int _t392_orden;
		public int t392_orden
		{
			get {return _t392_orden;}
			set { _t392_orden = value ;}
		}

		private int _t314_idusuario_responsable;
		public int t314_idusuario_responsable
		{
			get {return _t314_idusuario_responsable;}
			set { _t314_idusuario_responsable = value ;}
		}

		private string _t392_denominacion_CSN2P;
		public string t392_denominacion_CSN2P
		{
			get {return _t392_denominacion_CSN2P;}
			set { _t392_denominacion_CSN2P = value ;}
		}

		private bool _t392_obligatorio_CSN2P;
		public bool t392_obligatorio_CSN2P
		{
			get {return _t392_obligatorio_CSN2P;}
			set { _t392_obligatorio_CSN2P = value ;}
		}

        public bool activoqeq { get; set; }
		#endregion

		#region Constructor

		public SUPERNODO2() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T392_SUPERNODO2.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	07/10/2009 13:00:55
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Insert(SqlTransaction tr, string t392_denominacion , bool t392_estado , int t393_idsupernodo3 , 
                                 int t392_orden , int t314_idusuario_responsable , string t392_denominacion_CSN2P , 
                                 bool t392_obligatorio_CSN2P, bool activoqeq)
		{
			SqlParameter[] aParam = new SqlParameter[8];
			aParam[0] = new SqlParameter("@t392_denominacion", SqlDbType.Text, 50);
			aParam[0].Value = t392_denominacion;
			aParam[1] = new SqlParameter("@t392_estado", SqlDbType.Bit, 1);
			aParam[1].Value = t392_estado;
			aParam[2] = new SqlParameter("@t393_idsupernodo3", SqlDbType.Int, 4);
			aParam[2].Value = t393_idsupernodo3;
			aParam[3] = new SqlParameter("@t392_orden", SqlDbType.Int, 4);
			aParam[3].Value = t392_orden;
			aParam[4] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
			aParam[4].Value = t314_idusuario_responsable;
			aParam[5] = new SqlParameter("@t392_denominacion_CSN2P", SqlDbType.Text, 15);
			aParam[5].Value = t392_denominacion_CSN2P;
			aParam[6] = new SqlParameter("@t392_obligatorio_CSN2P", SqlDbType.Bit, 1);
			aParam[6].Value = t392_obligatorio_CSN2P;
            aParam[7] = new SqlParameter("@activoqeq", SqlDbType.Bit, 1);
            aParam[7].Value = activoqeq;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
				return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_SUPERNODO2_I", aParam));
			else
				return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_SUPERNODO2_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T392_SUPERNODO2.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	07/10/2009 13:00:55
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Update(SqlTransaction tr, int t392_idsupernodo2, string t392_denominacion, bool t392_estado, 
                                 int t393_idsupernodo3, int t392_orden, int t314_idusuario_responsable,
                                 string t392_denominacion_CSN2P, bool t392_obligatorio_CSN2P, bool activoqeq)
		{
			SqlParameter[] aParam = new SqlParameter[9];
			aParam[0] = new SqlParameter("@t392_idsupernodo2", SqlDbType.Int, 4);
			aParam[0].Value = t392_idsupernodo2;
			aParam[1] = new SqlParameter("@t392_denominacion", SqlDbType.Text, 50);
			aParam[1].Value = t392_denominacion;
			aParam[2] = new SqlParameter("@t392_estado", SqlDbType.Bit, 1);
			aParam[2].Value = t392_estado;
			aParam[3] = new SqlParameter("@t393_idsupernodo3", SqlDbType.Int, 4);
			aParam[3].Value = t393_idsupernodo3;
			aParam[4] = new SqlParameter("@t392_orden", SqlDbType.Int, 4);
			aParam[4].Value = t392_orden;
			aParam[5] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
			aParam[5].Value = t314_idusuario_responsable;
			aParam[6] = new SqlParameter("@t392_denominacion_CSN2P", SqlDbType.Text, 15);
			aParam[6].Value = t392_denominacion_CSN2P;
			aParam[7] = new SqlParameter("@t392_obligatorio_CSN2P", SqlDbType.Bit, 1);
			aParam[7].Value = t392_obligatorio_CSN2P;
            aParam[8] = new SqlParameter("@activoqeq", SqlDbType.Bit, 1);
            aParam[8].Value = activoqeq;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_SUPERNODO2_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_SUPERNODO2_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T392_SUPERNODO2 a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	07/10/2009 13:00:55
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t392_idsupernodo2)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t392_idsupernodo2", SqlDbType.Int, 4);
			aParam[0].Value = t392_idsupernodo2;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_SUPERNODO2_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_SUPERNODO2_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T392_SUPERNODO2,
		/// y devuelve una instancia u objeto del tipo SUPERNODO2
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	07/10/2009 13:00:55
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SUPERNODO2 Select(SqlTransaction tr, int t392_idsupernodo2) 
		{
			SUPERNODO2 o = new SUPERNODO2();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t392_idsupernodo2", SqlDbType.Int, 4);
			aParam[0].Value = t392_idsupernodo2;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_SUPERNODO2_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_SUPERNODO2_S", aParam);

			if (dr.Read())
			{
				if (dr["t392_idsupernodo2"] != DBNull.Value)
					o.t392_idsupernodo2 = int.Parse(dr["t392_idsupernodo2"].ToString());
				if (dr["t392_denominacion"] != DBNull.Value)
					o.t392_denominacion = (string)dr["t392_denominacion"];
				if (dr["t392_estado"] != DBNull.Value)
					o.t392_estado = (bool)dr["t392_estado"];
				if (dr["t393_idsupernodo3"] != DBNull.Value)
					o.t393_idsupernodo3 = int.Parse(dr["t393_idsupernodo3"].ToString());
				if (dr["t392_orden"] != DBNull.Value)
					o.t392_orden = int.Parse(dr["t392_orden"].ToString());
				if (dr["t314_idusuario_responsable"] != DBNull.Value)
					o.t314_idusuario_responsable = int.Parse(dr["t314_idusuario_responsable"].ToString());
				if (dr["t392_denominacion_CSN2P"] != DBNull.Value)
					o.t392_denominacion_CSN2P = (string)dr["t392_denominacion_CSN2P"];
				if (dr["t392_obligatorio_CSN2P"] != DBNull.Value)
					o.t392_obligatorio_CSN2P = (bool)dr["t392_obligatorio_CSN2P"];

			}
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de SUPERNODO2"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T392_SUPERNODO2.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	07/10/2009 13:00:55
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader Catalogo(Nullable<int> t392_idsupernodo2, string t392_denominacion, Nullable<bool> t392_estado, Nullable<int> t393_idsupernodo3, Nullable<int> t392_orden, Nullable<int> t314_idusuario_responsable, string t392_denominacion_CSN2P, Nullable<bool> t392_obligatorio_CSN2P, byte nOrden, byte nAscDesc)
		{
			SqlParameter[] aParam = new SqlParameter[10];
			aParam[0] = new SqlParameter("@t392_idsupernodo2", SqlDbType.Int, 4);
			aParam[0].Value = t392_idsupernodo2;
			aParam[1] = new SqlParameter("@t392_denominacion", SqlDbType.Text, 50);
			aParam[1].Value = t392_denominacion;
			aParam[2] = new SqlParameter("@t392_estado", SqlDbType.Bit, 1);
			aParam[2].Value = t392_estado;
			aParam[3] = new SqlParameter("@t393_idsupernodo3", SqlDbType.Int, 4);
			aParam[3].Value = t393_idsupernodo3;
			aParam[4] = new SqlParameter("@t392_orden", SqlDbType.Int, 4);
			aParam[4].Value = t392_orden;
			aParam[5] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
			aParam[5].Value = t314_idusuario_responsable;
			aParam[6] = new SqlParameter("@t392_denominacion_CSN2P", SqlDbType.Text, 15);
			aParam[6].Value = t392_denominacion_CSN2P;
			aParam[7] = new SqlParameter("@t392_obligatorio_CSN2P", SqlDbType.Bit, 1);
			aParam[7].Value = t392_obligatorio_CSN2P;

			aParam[8] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
			aParam[8].Value = nOrden;
			aParam[9] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
			aParam[9].Value = nAscDesc;

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
			return SqlHelper.ExecuteSqlDataReader("SUP_SUPERNODO2_C", aParam);
		}

		#endregion
	}
}
