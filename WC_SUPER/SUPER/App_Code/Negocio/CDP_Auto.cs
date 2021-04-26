using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : CDP
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T476_CDP
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	07/10/2009 14:44:07	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class CDP
	{
		#region Propiedades y Atributos

		private int _t476_idcnp;
		public int t476_idcnp
		{
			get {return _t476_idcnp;}
			set { _t476_idcnp = value ;}
		}

		private string _t476_denominacion;
		public string t476_denominacion
		{
			get {return _t476_denominacion;}
			set { _t476_denominacion = value ;}
		}

		private int _t303_idnodo;
		public int t303_idnodo
		{
			get {return _t303_idnodo;}
			set { _t303_idnodo = value ;}
		}

		private int _t314_idusuario_responsable;
		public int t314_idusuario_responsable
		{
			get {return _t314_idusuario_responsable;}
			set { _t314_idusuario_responsable = value ;}
		}

		private bool _t476_activo;
		public bool t476_activo
		{
			get {return _t476_activo;}
			set { _t476_activo = value ;}
		}

		private byte _t476_orden;
		public byte t476_orden
		{
			get {return _t476_orden;}
			set { _t476_orden = value ;}
		}
		#endregion

		#region Constructor

		public CDP() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T476_CDP.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	07/10/2009 14:44:07
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Insert(SqlTransaction tr, string t476_denominacion , int t303_idnodo , int t314_idusuario_responsable , bool t476_activo , byte t476_orden)
		{
			SqlParameter[] aParam = new SqlParameter[5];
			aParam[0] = new SqlParameter("@t476_denominacion", SqlDbType.Text, 30);
			aParam[0].Value = t476_denominacion;
			aParam[1] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
			aParam[1].Value = t303_idnodo;
			aParam[2] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
			aParam[2].Value = t314_idusuario_responsable;
			aParam[3] = new SqlParameter("@t476_activo", SqlDbType.Bit, 1);
			aParam[3].Value = t476_activo;
			aParam[4] = new SqlParameter("@t476_orden", SqlDbType.TinyInt, 1);
			aParam[4].Value = t476_orden;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
				return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_CDP_I", aParam));
			else
				return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CDP_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T476_CDP.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	07/10/2009 14:44:07
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Update(SqlTransaction tr, int t476_idcnp, string t476_denominacion, int t303_idnodo, int t314_idusuario_responsable, bool t476_activo, byte t476_orden)
		{
			SqlParameter[] aParam = new SqlParameter[6];
			aParam[0] = new SqlParameter("@t476_idcnp", SqlDbType.Int, 4);
			aParam[0].Value = t476_idcnp;
			aParam[1] = new SqlParameter("@t476_denominacion", SqlDbType.Text, 30);
			aParam[1].Value = t476_denominacion;
			aParam[2] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
			aParam[2].Value = t303_idnodo;
			aParam[3] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
			aParam[3].Value = t314_idusuario_responsable;
			aParam[4] = new SqlParameter("@t476_activo", SqlDbType.Bit, 1);
			aParam[4].Value = t476_activo;
			aParam[5] = new SqlParameter("@t476_orden", SqlDbType.TinyInt, 1);
			aParam[5].Value = t476_orden;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_CDP_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CDP_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T476_CDP a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	07/10/2009 14:44:07
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t476_idcnp)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t476_idcnp", SqlDbType.Int, 4);
			aParam[0].Value = t476_idcnp;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_CDP_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CDP_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T476_CDP,
		/// y devuelve una instancia u objeto del tipo CDP
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	07/10/2009 14:44:07
		/// </history>
		/// -----------------------------------------------------------------------------
		public static CDP Select(SqlTransaction tr, int t476_idcnp) 
		{
			CDP o = new CDP();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t476_idcnp", SqlDbType.Int, 4);
			aParam[0].Value = t476_idcnp;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_CDP_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CDP_S", aParam);

			if (dr.Read())
			{
				if (dr["t476_idcnp"] != DBNull.Value)
					o.t476_idcnp = int.Parse(dr["t476_idcnp"].ToString());
				if (dr["t476_denominacion"] != DBNull.Value)
					o.t476_denominacion = (string)dr["t476_denominacion"];
				if (dr["t303_idnodo"] != DBNull.Value)
					o.t303_idnodo = int.Parse(dr["t303_idnodo"].ToString());
				if (dr["t314_idusuario_responsable"] != DBNull.Value)
					o.t314_idusuario_responsable = int.Parse(dr["t314_idusuario_responsable"].ToString());
				if (dr["t476_activo"] != DBNull.Value)
					o.t476_activo = (bool)dr["t476_activo"];
				if (dr["t476_orden"] != DBNull.Value)
					o.t476_orden = byte.Parse(dr["t476_orden"].ToString());

			}
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de CDP"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T476_CDP.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	07/10/2009 14:44:07
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader Catalogo(Nullable<int> t476_idcnp, string t476_denominacion, Nullable<int> t303_idnodo, Nullable<int> t314_idusuario_responsable, Nullable<bool> t476_activo, Nullable<byte> t476_orden, byte nOrden, byte nAscDesc)
		{
			SqlParameter[] aParam = new SqlParameter[8];
			aParam[0] = new SqlParameter("@t476_idcnp", SqlDbType.Int, 4);
			aParam[0].Value = t476_idcnp;
			aParam[1] = new SqlParameter("@t476_denominacion", SqlDbType.Text, 30);
			aParam[1].Value = t476_denominacion;
			aParam[2] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
			aParam[2].Value = t303_idnodo;
			aParam[3] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
			aParam[3].Value = t314_idusuario_responsable;
			aParam[4] = new SqlParameter("@t476_activo", SqlDbType.Bit, 1);
			aParam[4].Value = t476_activo;
			aParam[5] = new SqlParameter("@t476_orden", SqlDbType.TinyInt, 1);
			aParam[5].Value = t476_orden;

			aParam[6] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
			aParam[6].Value = nOrden;
			aParam[7] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
			aParam[7].Value = nAscDesc;

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
			return SqlHelper.ExecuteSqlDataReader("SUP_CDP_C", aParam);
		}

		#endregion
	}
}
