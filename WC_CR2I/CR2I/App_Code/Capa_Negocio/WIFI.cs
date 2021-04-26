using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using CR2I.Capa_Datos;

namespace CR2I.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : CR2I
	/// Class	 : WIFI
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T085_WIFI
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	05/05/2010 9:37:24	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class WIFI
	{
		#region Propiedades y Atributos

		private int _t085_idreserva;
		public int t085_idreserva
		{
			get {return _t085_idreserva;}
			set { _t085_idreserva = value ;}
		}

		private int _t001_idficepi;
		public int t001_idficepi
		{
			get {return _t001_idficepi;}
			set { _t001_idficepi = value ;}
		}

		private string _t085_interesado;
		public string t085_interesado
		{
			get {return _t085_interesado;}
			set { _t085_interesado = value ;}
		}

		private string _t085_empresa;
		public string t085_empresa
		{
			get {return _t085_empresa;}
			set { _t085_empresa = value ;}
		}

		private DateTime _t085_fechoraini;
		public DateTime t085_fechoraini
		{
			get {return _t085_fechoraini;}
			set { _t085_fechoraini = value ;}
		}

		private DateTime _t085_fechorafin;
		public DateTime t085_fechorafin
		{
			get {return _t085_fechorafin;}
			set { _t085_fechorafin = value ;}
		}

		private string _t085_observaciones;
		public string t085_observaciones
		{
			get {return _t085_observaciones;}
			set { _t085_observaciones = value ;}
		}

		private byte _t085_estado;
		public byte t085_estado
		{
			get {return _t085_estado;}
			set { _t085_estado = value ;}
		}

		private string _t085_usuwifi;
		public string t085_usuwifi
		{
			get {return _t085_usuwifi;}
			set { _t085_usuwifi = value ;}
		}

		private string _t085_pwdwifi;
		public string t085_pwdwifi
		{
			get {return _t085_pwdwifi;}
			set { _t085_pwdwifi = value ;}
		}
		#endregion

		#region Constructor

		public WIFI() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T085_WIFI.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	05/05/2010 9:37:24
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Insert(SqlTransaction tr, int t001_idficepi , string t085_interesado , string t085_empresa , DateTime t085_fechoraini , DateTime t085_fechorafin , string t085_observaciones , byte t085_estado , string t085_pwdwifi)
		{
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar(Utilidades.CadenaConexion,
                    "CR2_WIFI_I", t001_idficepi, t085_interesado, t085_empresa, t085_fechoraini, t085_fechorafin, 
                    t085_observaciones, t085_estado, t085_pwdwifi));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr,
                    "CR2_WIFI_I", t001_idficepi, t085_interesado, t085_empresa, t085_fechoraini, t085_fechorafin,
                    t085_observaciones, t085_estado, t085_pwdwifi));
		}

        public static int Actualizar(SqlTransaction tr, int t085_idreserva, int t001_idficepi, string t085_interesado, string t085_empresa, DateTime t085_fechoraini, DateTime t085_fechorafin, string t085_observaciones, byte t085_estado, string t085_pwdwifi)
		{
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar(Utilidades.CadenaConexion,
                    "CR2_WIFI_U", t085_idreserva, t001_idficepi, t085_interesado, t085_empresa, t085_fechoraini, 
                    t085_fechorafin, t085_observaciones, t085_estado, t085_pwdwifi));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr,
                    "CR2_WIFI_U", t085_idreserva, t001_idficepi, t085_interesado, t085_empresa, t085_fechoraini, 
                    t085_fechorafin, t085_observaciones, t085_estado, t085_pwdwifi));
		}

        public static int Eliminar(SqlTransaction tr, int t085_idreserva)
		{
            if (tr == null)
                return SqlHelper.ExecuteNonQuery(Utilidades.CadenaConexion,
                    "CR2_WIFI_D", t085_idreserva);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "CR2_WIFI_D", t085_idreserva);

		}

		public static WIFI Obtener(SqlTransaction tr, int t085_idreserva) 
		{
			WIFI o = new WIFI();

			SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader(Utilidades.CadenaConexion,
                    "CR2_WIFI_S", t085_idreserva);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "CR2_WIFI_S", t085_idreserva);

			if (dr.Read())
			{
				if (dr["t085_idreserva"] != DBNull.Value)
					o.t085_idreserva = int.Parse(dr["t085_idreserva"].ToString());
				if (dr["t001_idficepi"] != DBNull.Value)
					o.t001_idficepi = int.Parse(dr["t001_idficepi"].ToString());
				if (dr["t085_interesado"] != DBNull.Value)
					o.t085_interesado = (string)dr["t085_interesado"];
				if (dr["t085_empresa"] != DBNull.Value)
					o.t085_empresa = (string)dr["t085_empresa"];
				if (dr["t085_fechoraini"] != DBNull.Value)
					o.t085_fechoraini = (DateTime)dr["t085_fechoraini"];
				if (dr["t085_fechorafin"] != DBNull.Value)
					o.t085_fechorafin = (DateTime)dr["t085_fechorafin"];
				if (dr["t085_observaciones"] != DBNull.Value)
					o.t085_observaciones = (string)dr["t085_observaciones"];
				if (dr["t085_estado"] != DBNull.Value)
					o.t085_estado = byte.Parse(dr["t085_estado"].ToString());
				if (dr["t085_usuwifi"] != DBNull.Value)
					o.t085_usuwifi = (string)dr["t085_usuwifi"];
				if (dr["t085_pwdwifi"] != DBNull.Value)
					o.t085_pwdwifi = (string)dr["t085_pwdwifi"];

			}
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de WIFI"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T085_WIFI.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	05/05/2010 9:37:24
		/// </history>
		/// -----------------------------------------------------------------------------
        public static SqlDataReader CatalogoWifi(int t001_idficepi, bool bSoloActivas)
		{
            return SqlHelper.ExecuteSqlDataReader(Utilidades.CadenaConexion,
                    "CR2_WIFI_CAT", t001_idficepi, bSoloActivas);
		}

		#endregion
	}
}
