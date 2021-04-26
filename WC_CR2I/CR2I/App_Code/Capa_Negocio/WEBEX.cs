using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using CR2I.Capa_Datos;

namespace CR2I.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : CR2I
	/// Class	 : WEBEX
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T139_WEBEX
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	11/05/2009 13:59:49	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class WEBEX
	{
		#region Propiedades y Atributos

		private int _T139_IDRESERVA;
		public int T139_IDRESERVA
		{
			get {return _T139_IDRESERVA;}
			set { _T139_IDRESERVA = value ;}
		}

		private string _T139_MOTIVO;
		public string T139_MOTIVO
		{
			get {return _T139_MOTIVO;}
			set { _T139_MOTIVO = value ;}
		}

		private DateTime _T139_FECHORAINI;
		public DateTime T139_FECHORAINI
		{
			get {return _T139_FECHORAINI;}
			set { _T139_FECHORAINI = value ;}
		}

		private DateTime _T139_FECHORAFIN;
		public DateTime T139_FECHORAFIN
		{
			get {return _T139_FECHORAFIN;}
			set { _T139_FECHORAFIN = value ;}
		}

		private int _T001_IDFICEPI_INTERESADO;
		public int T001_IDFICEPI_INTERESADO
		{
			get {return _T001_IDFICEPI_INTERESADO;}
			set { _T001_IDFICEPI_INTERESADO = value ;}
		}

		private DateTime _T139_FECMODIF;
		public DateTime T139_FECMODIF
		{
			get {return _T139_FECMODIF;}
			set { _T139_FECMODIF = value ;}
		}

		private string _T139_ASUNTO;
		public string T139_ASUNTO
		{
			get {return _T139_ASUNTO;}
			set { _T139_ASUNTO = value ;}
		}

		private string _T139_OBSERVACIONES;
		public string T139_OBSERVACIONES
		{
			get {return _T139_OBSERVACIONES;}
			set { _T139_OBSERVACIONES = value ;}
		}

		private string _T139_PRIVADO;
		public string T139_PRIVADO
		{
			get {return _T139_PRIVADO;}
			set { _T139_PRIVADO = value ;}
		}

		private string _T139_CORREOEXT;
		public string T139_CORREOEXT
		{
			get {return _T139_CORREOEXT;}
			set { _T139_CORREOEXT = value ;}
		}

        private int _T001_IDFICEPI_SOLICITANTE;
        public int T001_IDFICEPI_SOLICITANTE
        {
            get { return _T001_IDFICEPI_SOLICITANTE; }
            set { _T001_IDFICEPI_SOLICITANTE = value; }
        }

		private string _sProfesional;
        public string sProfesional
		{
            get { return _sProfesional; }
            set { _sProfesional = value; }
		}

        private string _CIP_INTERESADO;
        public string CIP_INTERESADO
		{
            get { return _CIP_INTERESADO; }
            set { _CIP_INTERESADO = value; }
		}

		#endregion

		#region Constructor

		public WEBEX() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

        public static int Insertar(SqlTransaction tr, string T139_MOTIVO, DateTime T139_FECHORAINI, DateTime T139_FECHORAFIN, int T001_IDFICEPI, DateTime T139_FECMODIF, string T139_ASUNTO, string T139_OBSERVACIONES, string T139_PRIVADO, string T139_CORREOEXT)
		{
			if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar(Utilidades.CadenaConexion, 
                    "CR2_WEBEXI", T139_MOTIVO, T139_FECHORAINI, T139_FECHORAFIN, T001_IDFICEPI, T139_FECMODIF, T139_ASUNTO, T139_OBSERVACIONES, T139_PRIVADO, T139_CORREOEXT));
			else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "CR2_WEBEXI", T139_MOTIVO, T139_FECHORAINI, T139_FECHORAFIN, T001_IDFICEPI, T139_FECMODIF, T139_ASUNTO, T139_OBSERVACIONES, T139_PRIVADO, T139_CORREOEXT));
		}

        public static int Actualizar(SqlTransaction tr, int T139_IDRESERVA, string T139_MOTIVO, DateTime T139_FECHORAINI, DateTime T139_FECHORAFIN, int T001_IDFICEPI, DateTime T139_FECMODIF, string T139_ASUNTO, string T139_OBSERVACIONES, string T139_PRIVADO, string T139_CORREOEXT)
		{
			if (tr == null)
                return SqlHelper.ExecuteNonQuery(Utilidades.CadenaConexion, 
                    "CR2_WEBEXU", T139_IDRESERVA, T139_MOTIVO, T139_FECHORAINI, 
                    T139_FECHORAFIN, T001_IDFICEPI, T139_FECMODIF, T139_ASUNTO, T139_OBSERVACIONES, T139_PRIVADO, T139_CORREOEXT);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "CR2_WEBEXU", T139_IDRESERVA, T139_MOTIVO, T139_FECHORAINI,
                    T139_FECHORAFIN, T001_IDFICEPI, T139_FECMODIF, T139_ASUNTO, T139_OBSERVACIONES, T139_PRIVADO, T139_CORREOEXT);
		}

        public static int Eliminar(SqlTransaction tr, int T139_IDRESERVA)
		{
			if (tr == null)
                return SqlHelper.ExecuteNonQuery(Utilidades.CadenaConexion, 
                    "CR2_WEBEXD", T139_IDRESERVA);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "CR2_WEBEXD", T139_IDRESERVA);
		}

        public static WEBEX Obtener(SqlTransaction tr, int T139_IDRESERVA) 
		{
			WEBEX o = new WEBEX();

			SqlDataReader dr;
			if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader(Utilidades.CadenaConexion, 
                    "CR2_WEBEXS", T139_IDRESERVA);
			else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "CR2_WEBEXS", T139_IDRESERVA);

			if (dr.Read())
			{
				if (dr["T139_IDRESERVA"] != DBNull.Value)
					o.T139_IDRESERVA = int.Parse(dr["T139_IDRESERVA"].ToString());
				if (dr["T139_MOTIVO"] != DBNull.Value)
					o.T139_MOTIVO = (string)dr["T139_MOTIVO"];
				if (dr["T139_FECHORAINI"] != DBNull.Value)
					o.T139_FECHORAINI = (DateTime)dr["T139_FECHORAINI"];
				if (dr["T139_FECHORAFIN"] != DBNull.Value)
					o.T139_FECHORAFIN = (DateTime)dr["T139_FECHORAFIN"];
                if (dr["IDFICEPI_INTERESADO"] != DBNull.Value)
                    o.T001_IDFICEPI_INTERESADO = int.Parse(dr["IDFICEPI_INTERESADO"].ToString());
                if (dr["CIP_INTERESADO"] != DBNull.Value)
                    o.CIP_INTERESADO = (string)dr["CIP_INTERESADO"];
                if (dr["PROFESIONAL"] != DBNull.Value)
                    o.sProfesional = (string)dr["PROFESIONAL"];
				if (dr["T139_FECMODIF"] != DBNull.Value)
					o.T139_FECMODIF = (DateTime)dr["T139_FECMODIF"];
                if (dr["IDFICEPI_SOLICITANTE"] != DBNull.Value)
                    o.T001_IDFICEPI_SOLICITANTE = int.Parse(dr["IDFICEPI_SOLICITANTE"].ToString());
				if (dr["T139_ASUNTO"] != DBNull.Value)
					o.T139_ASUNTO = (string)dr["T139_ASUNTO"];
				if (dr["T139_OBSERVACIONES"] != DBNull.Value)
					o.T139_OBSERVACIONES = (string)dr["T139_OBSERVACIONES"];
				if (dr["T139_PRIVADO"] != DBNull.Value)
					o.T139_PRIVADO = (string)dr["T139_PRIVADO"];
				if (dr["T139_CORREOEXT"] != DBNull.Value)
					o.T139_CORREOEXT = (string)dr["T139_CORREOEXT"];

			}
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de WEBEX"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

        public DataSet ObtenerReservasWebex(int T001_IDFICEPI, DateTime T139_FECHORAINI, DateTime T139_FECHORAFIN)
        {
            return SqlHelper.ExecuteDataset(Utilidades.CadenaConexion,
                "CR2_WEBEX_CAT", T001_IDFICEPI, T139_FECHORAINI, T139_FECHORAFIN);
        }

        public static bool ObtenerDisponibilidad(SqlTransaction tr, int T001_IDFICEPI, DateTime T139_FECHORAINI, DateTime T139_FECHORAFIN, int T139_IDRESERVA)
        {
            return (Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "CR2_DISPONIBILIDADWEBEX", T001_IDFICEPI, T139_FECHORAINI, T139_FECHORAFIN, T139_IDRESERVA))>0)?false:true;
        }

		#endregion
	}
}
