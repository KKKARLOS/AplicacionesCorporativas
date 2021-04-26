using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : SUPERNODO1
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T391_SUPERNODO1
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	07/10/2009 13:00:55	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class SUPERNODO1
	{
		#region Propiedades y Atributos

		private int _t391_idsupernodo1;
		public int t391_idsupernodo1
		{
			get {return _t391_idsupernodo1;}
			set { _t391_idsupernodo1 = value ;}
		}

		private string _t391_denominacion;
		public string t391_denominacion
		{
			get {return _t391_denominacion;}
			set { _t391_denominacion = value ;}
		}

		private int _t392_idsupernodo2;
		public int t392_idsupernodo2
		{
			get {return _t392_idsupernodo2;}
			set { _t392_idsupernodo2 = value ;}
		}

		private bool _t391_estado;
		public bool t391_estado
		{
			get {return _t391_estado;}
			set { _t391_estado = value ;}
		}

		private int _t391_orden;
		public int t391_orden
		{
			get {return _t391_orden;}
			set { _t391_orden = value ;}
		}

		private int _t314_idusuario_responsable;
		public int t314_idusuario_responsable
		{
			get {return _t314_idusuario_responsable;}
			set { _t314_idusuario_responsable = value ;}
		}

		private string _t391_denominacion_CSN1P;
		public string t391_denominacion_CSN1P
		{
			get {return _t391_denominacion_CSN1P;}
			set { _t391_denominacion_CSN1P = value ;}
		}

		private bool _t391_obligatorio_CSN1P;
		public bool t391_obligatorio_CSN1P
		{
			get {return _t391_obligatorio_CSN1P;}
			set { _t391_obligatorio_CSN1P = value ;}
		}

        public bool activoqeq { get; set; }
		#endregion

		#region Constructor

		public SUPERNODO1() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T391_SUPERNODO1.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	07/10/2009 13:00:55
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Insert(SqlTransaction tr, string t391_denominacion , int t392_idsupernodo2 , bool t391_estado , 
                                 int t391_orden , int t314_idusuario_responsable , string t391_denominacion_CSN1P ,
                                 bool t391_obligatorio_CSN1P, bool activoqeq)
		{
			SqlParameter[] aParam = new SqlParameter[8];
			aParam[0] = new SqlParameter("@t391_denominacion", SqlDbType.Text, 50);
			aParam[0].Value = t391_denominacion;
			aParam[1] = new SqlParameter("@t392_idsupernodo2", SqlDbType.Int, 4);
			aParam[1].Value = t392_idsupernodo2;
			aParam[2] = new SqlParameter("@t391_estado", SqlDbType.Bit, 1);
			aParam[2].Value = t391_estado;
			aParam[3] = new SqlParameter("@t391_orden", SqlDbType.Int, 4);
			aParam[3].Value = t391_orden;
			aParam[4] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
			aParam[4].Value = t314_idusuario_responsable;
			aParam[5] = new SqlParameter("@t391_denominacion_CSN1P", SqlDbType.Text, 15);
			aParam[5].Value = t391_denominacion_CSN1P;
			aParam[6] = new SqlParameter("@t391_obligatorio_CSN1P", SqlDbType.Bit, 1);
			aParam[6].Value = t391_obligatorio_CSN1P;
            aParam[7] = new SqlParameter("@activoqeq", SqlDbType.Bit, 1);
            aParam[7].Value = activoqeq;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
				return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_SUPERNODO1_I", aParam));
			else
				return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_SUPERNODO1_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T391_SUPERNODO1.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	07/10/2009 13:00:55
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Update(SqlTransaction tr, int t391_idsupernodo1, string t391_denominacion, int t392_idsupernodo2, 
                                 bool t391_estado, int t391_orden, int t314_idusuario_responsable, string t391_denominacion_CSN1P,
                                 bool t391_obligatorio_CSN1P, bool activoqeq)
		{
			SqlParameter[] aParam = new SqlParameter[9];
			aParam[0] = new SqlParameter("@t391_idsupernodo1", SqlDbType.Int, 4);
			aParam[0].Value = t391_idsupernodo1;
			aParam[1] = new SqlParameter("@t391_denominacion", SqlDbType.Text, 50);
			aParam[1].Value = t391_denominacion;
			aParam[2] = new SqlParameter("@t392_idsupernodo2", SqlDbType.Int, 4);
			aParam[2].Value = t392_idsupernodo2;
			aParam[3] = new SqlParameter("@t391_estado", SqlDbType.Bit, 1);
			aParam[3].Value = t391_estado;
			aParam[4] = new SqlParameter("@t391_orden", SqlDbType.Int, 4);
			aParam[4].Value = t391_orden;
			aParam[5] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
			aParam[5].Value = t314_idusuario_responsable;
			aParam[6] = new SqlParameter("@t391_denominacion_CSN1P", SqlDbType.Text, 15);
			aParam[6].Value = t391_denominacion_CSN1P;
			aParam[7] = new SqlParameter("@t391_obligatorio_CSN1P", SqlDbType.Bit, 1);
			aParam[7].Value = t391_obligatorio_CSN1P;
            aParam[8] = new SqlParameter("@activoqeq", SqlDbType.Bit, 1);
            aParam[8].Value = activoqeq;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_SUPERNODO1_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_SUPERNODO1_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T391_SUPERNODO1 a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	07/10/2009 13:00:55
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t391_idsupernodo1)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t391_idsupernodo1", SqlDbType.Int, 4);
			aParam[0].Value = t391_idsupernodo1;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_SUPERNODO1_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_SUPERNODO1_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T391_SUPERNODO1,
		/// y devuelve una instancia u objeto del tipo SUPERNODO1
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	07/10/2009 13:00:55
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SUPERNODO1 Select(SqlTransaction tr, int t391_idsupernodo1) 
		{
			SUPERNODO1 o = new SUPERNODO1();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t391_idsupernodo1", SqlDbType.Int, 4);
			aParam[0].Value = t391_idsupernodo1;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_SUPERNODO1_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_SUPERNODO1_S", aParam);

			if (dr.Read())
			{
				if (dr["t391_idsupernodo1"] != DBNull.Value)
					o.t391_idsupernodo1 = int.Parse(dr["t391_idsupernodo1"].ToString());
				if (dr["t391_denominacion"] != DBNull.Value)
					o.t391_denominacion = (string)dr["t391_denominacion"];
				if (dr["t392_idsupernodo2"] != DBNull.Value)
					o.t392_idsupernodo2 = int.Parse(dr["t392_idsupernodo2"].ToString());
				if (dr["t391_estado"] != DBNull.Value)
					o.t391_estado = (bool)dr["t391_estado"];
				if (dr["t391_orden"] != DBNull.Value)
					o.t391_orden = int.Parse(dr["t391_orden"].ToString());
				if (dr["t314_idusuario_responsable"] != DBNull.Value)
					o.t314_idusuario_responsable = int.Parse(dr["t314_idusuario_responsable"].ToString());
				if (dr["t391_denominacion_CSN1P"] != DBNull.Value)
					o.t391_denominacion_CSN1P = (string)dr["t391_denominacion_CSN1P"];
				if (dr["t391_obligatorio_CSN1P"] != DBNull.Value)
					o.t391_obligatorio_CSN1P = (bool)dr["t391_obligatorio_CSN1P"];
			}
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de SUPERNODO1"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T391_SUPERNODO1.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	07/10/2009 13:00:55
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader Catalogo(Nullable<int> t391_idsupernodo1, string t391_denominacion, Nullable<int> t392_idsupernodo2, Nullable<bool> t391_estado, Nullable<int> t391_orden, Nullable<int> t314_idusuario_responsable, string t391_denominacion_CSN1P, Nullable<bool> t391_obligatorio_CSN1P, byte nOrden, byte nAscDesc)
		{
			SqlParameter[] aParam = new SqlParameter[10];
			aParam[0] = new SqlParameter("@t391_idsupernodo1", SqlDbType.Int, 4);
			aParam[0].Value = t391_idsupernodo1;
			aParam[1] = new SqlParameter("@t391_denominacion", SqlDbType.Text, 50);
			aParam[1].Value = t391_denominacion;
			aParam[2] = new SqlParameter("@t392_idsupernodo2", SqlDbType.Int, 4);
			aParam[2].Value = t392_idsupernodo2;
			aParam[3] = new SqlParameter("@t391_estado", SqlDbType.Bit, 1);
			aParam[3].Value = t391_estado;
			aParam[4] = new SqlParameter("@t391_orden", SqlDbType.Int, 4);
			aParam[4].Value = t391_orden;
			aParam[5] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
			aParam[5].Value = t314_idusuario_responsable;
			aParam[6] = new SqlParameter("@t391_denominacion_CSN1P", SqlDbType.Text, 15);
			aParam[6].Value = t391_denominacion_CSN1P;
			aParam[7] = new SqlParameter("@t391_obligatorio_CSN1P", SqlDbType.Bit, 1);
			aParam[7].Value = t391_obligatorio_CSN1P;

			aParam[8] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
			aParam[8].Value = nOrden;
			aParam[9] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
			aParam[9].Value = nAscDesc;

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
			return SqlHelper.ExecuteSqlDataReader("SUP_SUPERNODO1_C", aParam);
		}

		#endregion
	}
}
