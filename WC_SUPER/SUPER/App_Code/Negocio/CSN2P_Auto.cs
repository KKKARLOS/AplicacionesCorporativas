using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : CSN2P
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T487_CSN2P
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	02/10/2009 13:57:05	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class CSN2P
	{
		#region Propiedades y Atributos

		private int _t487_idcsn2p;
		public int t487_idcsn2p
		{
			get {return _t487_idcsn2p;}
			set { _t487_idcsn2p = value ;}
		}

		private string _t487_denominacion;
		public string t487_denominacion
		{
			get {return _t487_denominacion;}
			set { _t487_denominacion = value ;}
		}

		private int _t392_idsupernodo2;
		public int t392_idsupernodo2
		{
			get {return _t392_idsupernodo2;}
			set { _t392_idsupernodo2 = value ;}
		}

		private int _t314_idusuario_responsable;
		public int t314_idusuario_responsable
		{
			get {return _t314_idusuario_responsable;}
			set { _t314_idusuario_responsable = value ;}
		}

        private bool _t487_activo;
        public bool t487_activo
		{
			get {return _t487_activo;}
			set { _t487_activo = value ;}
		}

		private byte _t487_orden;
		public byte t487_orden
		{
			get {return _t487_orden;}
			set { _t487_orden = value ;}
		}
		#endregion

		#region Constructor

		public CSN2P() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T487_CSN2P.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	02/10/2009 13:57:05
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, string t487_denominacion, int t392_idsupernodo2, int t314_idusuario_responsable, bool t487_activo, byte t487_orden)
		{
			SqlParameter[] aParam = new SqlParameter[5];
			aParam[0] = new SqlParameter("@t487_denominacion", SqlDbType.Text, 30);
			aParam[0].Value = t487_denominacion;
			aParam[1] = new SqlParameter("@t392_idsupernodo2", SqlDbType.Int, 4);
			aParam[1].Value = t392_idsupernodo2;
			aParam[2] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
			aParam[2].Value = t314_idusuario_responsable;
			aParam[3] = new SqlParameter("@t487_activo", SqlDbType.Int, 4);
			aParam[3].Value = t487_activo;
			aParam[4] = new SqlParameter("@t487_orden", SqlDbType.TinyInt, 1);
			aParam[4].Value = t487_orden;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
				return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_CSN2P_I", aParam));
			else
				return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CSN2P_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T487_CSN2P.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	02/10/2009 13:57:05
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Update(SqlTransaction tr, int t487_idcsn2p, string t487_denominacion, int t392_idsupernodo2, int t314_idusuario_responsable, bool t487_activo, byte t487_orden)
		{
			SqlParameter[] aParam = new SqlParameter[6];
			aParam[0] = new SqlParameter("@t487_idcsn2p", SqlDbType.Int, 4);
			aParam[0].Value = t487_idcsn2p;
			aParam[1] = new SqlParameter("@t487_denominacion", SqlDbType.Text, 30);
			aParam[1].Value = t487_denominacion;
			aParam[2] = new SqlParameter("@t392_idsupernodo2", SqlDbType.Int, 4);
			aParam[2].Value = t392_idsupernodo2;
			aParam[3] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
			aParam[3].Value = t314_idusuario_responsable;
			aParam[4] = new SqlParameter("@t487_activo", SqlDbType.Int, 4);
			aParam[4].Value = t487_activo;
			aParam[5] = new SqlParameter("@t487_orden", SqlDbType.TinyInt, 1);
			aParam[5].Value = t487_orden;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_CSN2P_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CSN2P_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T487_CSN2P a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	02/10/2009 13:57:05
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t487_idcsn2p)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t487_idcsn2p", SqlDbType.Int, 4);
			aParam[0].Value = t487_idcsn2p;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_CSN2P_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CSN2P_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T487_CSN2P,
		/// y devuelve una instancia u objeto del tipo CSN2P
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	02/10/2009 13:57:05
		/// </history>
		/// -----------------------------------------------------------------------------
		public static CSN2P Select(SqlTransaction tr, int t487_idcsn2p) 
		{
			CSN2P o = new CSN2P();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t487_idcsn2p", SqlDbType.Int, 4);
			aParam[0].Value = t487_idcsn2p;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_CSN2P_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CSN2P_S", aParam);

			if (dr.Read())
			{
				if (dr["t487_idcsn2p"] != DBNull.Value)
					o.t487_idcsn2p = int.Parse(dr["t487_idcsn2p"].ToString());
				if (dr["t487_denominacion"] != DBNull.Value)
					o.t487_denominacion = (string)dr["t487_denominacion"];
				if (dr["t392_idsupernodo2"] != DBNull.Value)
					o.t392_idsupernodo2 = int.Parse(dr["t392_idsupernodo2"].ToString());
				if (dr["t314_idusuario_responsable"] != DBNull.Value)
					o.t314_idusuario_responsable = int.Parse(dr["t314_idusuario_responsable"].ToString());
				if (dr["t487_activo"] != DBNull.Value)
					o.t487_activo = bool.Parse(dr["t487_activo"].ToString());
				if (dr["t487_orden"] != DBNull.Value)
					o.t487_orden = byte.Parse(dr["t487_orden"].ToString());
                if (dr["responsable"] != DBNull.Value)
                    o.DesResponsable = (string)dr["responsable"];
			}
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de CSN2P"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T487_CSN2P.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	02/10/2009 13:57:05
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader Catalogo(Nullable<int> t487_idcsn2p, string t487_denominacion, Nullable<int> t392_idsupernodo2, Nullable<int> t314_idusuario_responsable, Nullable<int> t487_activo, Nullable<byte> t487_orden, byte nOrden, byte nAscDesc)
		{
			SqlParameter[] aParam = new SqlParameter[8];
			aParam[0] = new SqlParameter("@t487_idcsn2p", SqlDbType.Int, 4);
			aParam[0].Value = t487_idcsn2p;
			aParam[1] = new SqlParameter("@t487_denominacion", SqlDbType.Text, 30);
			aParam[1].Value = t487_denominacion;
			aParam[2] = new SqlParameter("@t392_idsupernodo2", SqlDbType.Int, 4);
			aParam[2].Value = t392_idsupernodo2;
			aParam[3] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
			aParam[3].Value = t314_idusuario_responsable;
			aParam[4] = new SqlParameter("@t487_activo", SqlDbType.Int, 4);
			aParam[4].Value = t487_activo;
			aParam[5] = new SqlParameter("@t487_orden", SqlDbType.TinyInt, 1);
			aParam[5].Value = t487_orden;

			aParam[6] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
			aParam[6].Value = nOrden;
			aParam[7] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
			aParam[7].Value = nAscDesc;

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
			return SqlHelper.ExecuteSqlDataReader("SUP_CSN2P_C", aParam);
		}

		#endregion
	}
}
