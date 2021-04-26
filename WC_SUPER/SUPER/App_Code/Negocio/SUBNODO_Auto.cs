using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : SUBNODO
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T304_SUBNODO
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	10/06/2008 12:52:13	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class SUBNODO
	{
		#region Propiedades y Atributos

		private int _t304_idsubnodo;
		public int t304_idsubnodo
		{
			get {return _t304_idsubnodo;}
			set { _t304_idsubnodo = value ;}
		}

		private string _t304_denominacion;
		public string t304_denominacion
		{
			get {return _t304_denominacion;}
			set { _t304_denominacion = value ;}
		}

		private int _t303_idnodo;
		public int t303_idnodo
		{
			get {return _t303_idnodo;}
			set { _t303_idnodo = value ;}
		}

		private int _t304_orden;
		public int t304_orden
		{
			get {return _t304_orden;}
			set { _t304_orden = value ;}
		}

		private bool _t304_estado;
		public bool t304_estado
		{
			get {return _t304_estado;}
			set { _t304_estado = value ;}
		}

		private byte _t304_maniobra;
        public byte t304_maniobra
		{
			get {return _t304_maniobra;}
			set { _t304_maniobra = value ;}
		}

		private int _t314_idusuario_responsable;
		public int t314_idusuario_responsable
		{
			get {return _t314_idusuario_responsable;}
			set { _t314_idusuario_responsable = value ;}
		}
        private int? _t313_idempresa;
        public int? t313_idempresa
        {
            get { return _t313_idempresa; }
            set { _t313_idempresa = value; }
        }
        private string _t313_denominacion;
        public string t313_denominacion
        {
            get { return _t313_denominacion; }
            set { _t313_denominacion = value; }
        }
		#endregion

		#region Constructores

		public SUBNODO() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T304_SUBNODO.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	10/06/2008 12:52:13
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, string t304_denominacion, int t303_idnodo, int t304_orden, bool t304_estado, byte t304_maniobra, int t314_idusuario_responsable, Nullable<int> t313_idempresa)
		{
			SqlParameter[] aParam = new SqlParameter[7];
			aParam[0] = new SqlParameter("@t304_denominacion", SqlDbType.Text, 50);
			aParam[0].Value = t304_denominacion;
			aParam[1] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
			aParam[1].Value = t303_idnodo;
			aParam[2] = new SqlParameter("@t304_orden", SqlDbType.Int, 4);
			aParam[2].Value = t304_orden;
			aParam[3] = new SqlParameter("@t304_estado", SqlDbType.Bit, 1);
			aParam[3].Value = t304_estado;
			aParam[4] = new SqlParameter("@t304_maniobra", SqlDbType.TinyInt, 1);
			aParam[4].Value = t304_maniobra;
			aParam[5] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
			aParam[5].Value = t314_idusuario_responsable;
            aParam[6] = new SqlParameter("@t313_idempresa", SqlDbType.Int, 4);
            aParam[6].Value = t313_idempresa;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
				return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_SUBNODO_I", aParam));
			else
				return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_SUBNODO_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T304_SUBNODO.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	10/06/2008 12:52:13
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t304_idsubnodo, string t304_denominacion, int t303_idnodo, int t304_orden, bool t304_estado, int t314_idusuario_responsable, Nullable<int> t313_idempresa)
		{
			SqlParameter[] aParam = new SqlParameter[7];
			aParam[0] = new SqlParameter("@t304_idsubnodo", SqlDbType.Int, 4);
			aParam[0].Value = t304_idsubnodo;
			aParam[1] = new SqlParameter("@t304_denominacion", SqlDbType.Text, 50);
			aParam[1].Value = t304_denominacion;
			aParam[2] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
			aParam[2].Value = t303_idnodo;
			aParam[3] = new SqlParameter("@t304_orden", SqlDbType.Int, 4);
			aParam[3].Value = t304_orden;
			aParam[4] = new SqlParameter("@t304_estado", SqlDbType.Bit, 1);
			aParam[4].Value = t304_estado;
			aParam[5] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
			aParam[5].Value = t314_idusuario_responsable;
            aParam[6] = new SqlParameter("@t313_idempresa", SqlDbType.Int, 4);
            aParam[6].Value = t313_idempresa;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_SUBNODO_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_SUBNODO_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T304_SUBNODO a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	10/06/2008 12:52:13
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t304_idsubnodo)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t304_idsubnodo", SqlDbType.Int, 4);
			aParam[0].Value = t304_idsubnodo;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_SUBNODO_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_SUBNODO_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T304_SUBNODO,
		/// y devuelve una instancia u objeto del tipo SUBNODO
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	10/06/2008 12:52:13
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SUBNODO Select(SqlTransaction tr, int t304_idsubnodo) 
		{
			SUBNODO o = new SUBNODO();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t304_idsubnodo", SqlDbType.Int, 4);
			aParam[0].Value = t304_idsubnodo;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_SUBNODO_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_SUBNODO_S", aParam);

			if (dr.Read())
			{
				if (dr["t304_idsubnodo"] != DBNull.Value)
					o.t304_idsubnodo = int.Parse(dr["t304_idsubnodo"].ToString());
				if (dr["t304_denominacion"] != DBNull.Value)
					o.t304_denominacion = (string)dr["t304_denominacion"];
				if (dr["t303_idnodo"] != DBNull.Value)
					o.t303_idnodo = int.Parse(dr["t303_idnodo"].ToString());
				if (dr["t304_orden"] != DBNull.Value)
					o.t304_orden = int.Parse(dr["t304_orden"].ToString());
				if (dr["t304_estado"] != DBNull.Value)
					o.t304_estado = (bool)dr["t304_estado"];
				if (dr["t304_maniobra"] != DBNull.Value)
                    o.t304_maniobra = byte.Parse(dr["t304_maniobra"].ToString());
				if (dr["t314_idusuario_responsable"] != DBNull.Value)
					o.t314_idusuario_responsable = int.Parse(dr["t314_idusuario_responsable"].ToString());
                if (dr["t313_idempresa"] != DBNull.Value)
                    o.t313_idempresa = int.Parse(dr["t313_idempresa"].ToString());
                if (dr["t313_denominacion"] != DBNull.Value)
                    o.t313_denominacion = (string)dr["t313_denominacion"];  
			}
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de SUBNODO"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}


		#endregion
	}
}
