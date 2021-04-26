using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : ESPACIOCOMUNICACION
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T639_ESPACIOCOMUNICACION
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	05/01/2011 13:04:29	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class ESPACIOCOMUNICACION
	{
		#region Propiedades y Atributos

		private int _t639_idcomunicacion;
		public int t639_idcomunicacion
		{
			get {return _t639_idcomunicacion;}
			set { _t639_idcomunicacion = value ;}
		}

		private int _t301_idproyecto;
		public int t301_idproyecto
		{
			get {return _t301_idproyecto;}
			set { _t301_idproyecto = value ;}
		}

		private DateTime _t639_fechacom;
		public DateTime t639_fechacom
		{
			get {return _t639_fechacom;}
			set { _t639_fechacom = value ;}
		}

		private int _t314_idusuario;
		public int t314_idusuario
		{
			get {return _t314_idusuario;}
			set { _t314_idusuario = value ;}
		}

		private bool _t639_consumo;
		public bool t639_consumo
		{
			get {return _t639_consumo;}
			set { _t639_consumo = value ;}
		}

		private bool _t639_produccion;
		public bool t639_produccion
		{
			get {return _t639_produccion;}
			set { _t639_produccion = value ;}
		}

		private bool _t639_facturacion;
		public bool t639_facturacion
		{
			get {return _t639_facturacion;}
			set { _t639_facturacion = value ;}
		}

		private bool _t639_otros;
		public bool t639_otros
		{
			get {return _t639_otros;}
			set { _t639_otros = value ;}
		}

		private string _t639_descripcion;
		public string t639_descripcion
		{
			get {return _t639_descripcion;}
			set { _t639_descripcion = value ;}
		}

		private bool _t639_vigenciaproyecto;
		public bool t639_vigenciaproyecto
		{
			get {return _t639_vigenciaproyecto;}
			set { _t639_vigenciaproyecto = value ;}
		}

		private int? _t639_vigenciadesde;
		public int? t639_vigenciadesde
		{
			get {return _t639_vigenciadesde;}
			set { _t639_vigenciadesde = value ;}
		}

		private int? _t639_vigenciahasta;
		public int? t639_vigenciahasta
		{
			get {return _t639_vigenciahasta;}
			set { _t639_vigenciahasta = value ;}
		}

		private string _t639_observaciones;
		public string t639_observaciones
		{
			get {return _t639_observaciones;}
			set { _t639_observaciones = value ;}
		}
        private string _autor;
        public string autor
        {
            get { return _autor; }
            set { _autor = value; }
        }
		private string _t301_denominacion;
        public string t301_denominacion
		{
            get { return _t301_denominacion; }
            set { _t301_denominacion = value; }
		}
		#endregion

		#region Constructor

		public ESPACIOCOMUNICACION() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T639_ESPACIOCOMUNICACION.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	05/01/2011 13:04:29
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t639_idcomunicacion, int t301_idproyecto, Nullable<DateTime> t639_fechacom, int t314_idusuario, bool t639_consumo, bool t639_produccion, bool t639_facturacion, bool t639_otros, string t639_descripcion, bool t639_vigenciaproyecto, Nullable<int> t639_vigenciadesde, Nullable<int> t639_vigenciahasta, string t639_observaciones)
		{
			SqlParameter[] aParam = new SqlParameter[13];
			aParam[0] = new SqlParameter("@t639_idcomunicacion", SqlDbType.Int, 4);
			aParam[0].Value = t639_idcomunicacion;
			aParam[1] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
			aParam[1].Value = t301_idproyecto;
			aParam[2] = new SqlParameter("@t639_fechacom", SqlDbType.SmallDateTime, 4);
			aParam[2].Value = t639_fechacom;
			aParam[3] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[3].Value = t314_idusuario;
			aParam[4] = new SqlParameter("@t639_consumo", SqlDbType.Bit, 1);
			aParam[4].Value = t639_consumo;
			aParam[5] = new SqlParameter("@t639_produccion", SqlDbType.Bit, 1);
			aParam[5].Value = t639_produccion;
			aParam[6] = new SqlParameter("@t639_facturacion", SqlDbType.Bit, 1);
			aParam[6].Value = t639_facturacion;
			aParam[7] = new SqlParameter("@t639_otros", SqlDbType.Bit, 1);
			aParam[7].Value = t639_otros;
			aParam[8] = new SqlParameter("@t639_descripcion", SqlDbType.Text, 2147483647);
			aParam[8].Value = t639_descripcion;
			aParam[9] = new SqlParameter("@t639_vigenciaproyecto", SqlDbType.Bit, 1);
			aParam[9].Value = t639_vigenciaproyecto;
			aParam[10] = new SqlParameter("@t639_vigenciadesde", SqlDbType.Int, 4);
			aParam[10].Value = t639_vigenciadesde;
			aParam[11] = new SqlParameter("@t639_vigenciahasta", SqlDbType.Int, 4);
			aParam[11].Value = t639_vigenciahasta;
			aParam[12] = new SqlParameter("@t639_observaciones", SqlDbType.Text, 2147483647);
			aParam[12].Value = t639_observaciones;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_ESPACIOCOMUNICACION_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ESPACIOCOMUNICACION_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T639_ESPACIOCOMUNICACION a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	05/01/2011 13:04:29
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t639_idcomunicacion)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t639_idcomunicacion", SqlDbType.Int, 4);
			aParam[0].Value = t639_idcomunicacion;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_ESPACIOCOMUNICACION_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ESPACIOCOMUNICACION_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T639_ESPACIOCOMUNICACION,
		/// y devuelve una instancia u objeto del tipo ESPACIOCOMUNICACION
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	05/01/2011 13:04:29
		/// </history>
		/// -----------------------------------------------------------------------------
		public static ESPACIOCOMUNICACION Select(SqlTransaction tr, int t639_idcomunicacion) 
		{
			ESPACIOCOMUNICACION o = new ESPACIOCOMUNICACION();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t639_idcomunicacion", SqlDbType.Int, 4);
			aParam[0].Value = t639_idcomunicacion;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_ESPACIOCOMUNICACION_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_ESPACIOCOMUNICACION_S", aParam);

			if (dr.Read())
			{
				if (dr["t639_idcomunicacion"] != DBNull.Value)
					o.t639_idcomunicacion = int.Parse(dr["t639_idcomunicacion"].ToString());
				if (dr["t301_idproyecto"] != DBNull.Value)
					o.t301_idproyecto = int.Parse(dr["t301_idproyecto"].ToString());
				if (dr["t639_fechacom"] != DBNull.Value)
					o.t639_fechacom = (DateTime)dr["t639_fechacom"];
				if (dr["t314_idusuario"] != DBNull.Value)
					o.t314_idusuario = int.Parse(dr["t314_idusuario"].ToString());
				if (dr["t639_consumo"] != DBNull.Value)
					o.t639_consumo = (bool)dr["t639_consumo"];
				if (dr["t639_produccion"] != DBNull.Value)
					o.t639_produccion = (bool)dr["t639_produccion"];
				if (dr["t639_facturacion"] != DBNull.Value)
					o.t639_facturacion = (bool)dr["t639_facturacion"];
				if (dr["t639_otros"] != DBNull.Value)
					o.t639_otros = (bool)dr["t639_otros"];
				if (dr["t639_descripcion"] != DBNull.Value)
					o.t639_descripcion = (string)dr["t639_descripcion"];
				if (dr["t639_vigenciaproyecto"] != DBNull.Value)
					o.t639_vigenciaproyecto = (bool)dr["t639_vigenciaproyecto"];
				if (dr["t639_vigenciadesde"] != DBNull.Value)
					o.t639_vigenciadesde = int.Parse(dr["t639_vigenciadesde"].ToString());
				if (dr["t639_vigenciahasta"] != DBNull.Value)
					o.t639_vigenciahasta = int.Parse(dr["t639_vigenciahasta"].ToString());
				if (dr["t639_observaciones"] != DBNull.Value)
					o.t639_observaciones = (string)dr["t639_observaciones"];
                if (dr["autor"] != DBNull.Value)
                    o.autor = (string)dr["autor"];
                if (dr["t301_denominacion"] != DBNull.Value)
                    o.t301_denominacion = (string)dr["t301_denominacion"];

			}
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de ESPACIOCOMUNICACION"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		#endregion
	}
}
