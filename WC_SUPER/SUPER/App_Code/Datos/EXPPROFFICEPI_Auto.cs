using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.DAL
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : EXPPROFFICEPI
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T812_EXPPROFFICEPI
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	01/08/2012 12:59:16	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class EXPPROFFICEPI
	{
		#region Propiedades y Atributos

		private int _t812_idexpprofficepi;
		public int t812_idexpprofficepi
		{
			get {return _t812_idexpprofficepi;}
			set { _t812_idexpprofficepi = value ;}
		}

		private string _t812_visiblecv;
        public string t812_visiblecv
		{
			get {return _t812_visiblecv;}
			set { _t812_visiblecv = value ;}
		}

		private DateTime? _t812_finicio;
		public DateTime? t812_finicio
		{
			get {return _t812_finicio;}
			set { _t812_finicio = value ;}
		}

		private DateTime? _t812_ffin;
		public DateTime? t812_ffin
		{
			get {return _t812_ffin;}
			set { _t812_ffin = value ;}
		}

		private int _t001_idficepi;
		public int t001_idficepi
		{
			get {return _t001_idficepi;}
			set { _t001_idficepi = value ;}
		}

		private int _t808_idexpprof;
		public int t808_idexpprof
		{
			get {return _t808_idexpprof;}
			set { _t808_idexpprof = value ;}
		}

		private int? _t001_idficepi_validador;
		public int? t001_idficepi_validador
		{
			get {return _t001_idficepi_validador;}
			set { _t001_idficepi_validador = value ;}
		}

		private int? _t819_idplantillacvt;
		public int? t819_idplantillacvt
		{
			get {return _t819_idplantillacvt;}
			set { _t819_idplantillacvt = value ;}
		}

        //Validador
        private int? _idValidador;
        public int? idValidador
        {
            get { return _idValidador; }
            set { _idValidador = value; }
        }
        private string _denValidador;
        public string denValidador
        {
            get { return _denValidador; }
            set { _denValidador = value; }
        }

        #endregion

		#region Constructor

		public EXPPROFFICEPI() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T812_EXPPROFFICEPI.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	01/08/2012 12:59:16
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Insert(SqlTransaction tr, string t812_visiblecv , Nullable<DateTime> t812_finicio , 
                                 Nullable<DateTime> t812_ffin , int t001_idficepi , int t808_idexpprof , Nullable<int> t001_idficepi_validador, 
                                 Nullable<int> t819_idplantillacvt)
		{
			SqlParameter[] aParam = new SqlParameter[7];
			int i = 0;
            aParam[i++] = ParametroSql.add("@t812_visiblecv", SqlDbType.Char, 1, t812_visiblecv == "" ? null : t812_visiblecv);
			aParam[i++] = ParametroSql.add("@t812_finicio", SqlDbType.SmallDateTime, 4, t812_finicio);
			aParam[i++] = ParametroSql.add("@t812_ffin", SqlDbType.SmallDateTime, 4, t812_ffin);
			aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
			aParam[i++] = ParametroSql.add("@t808_idexpprof", SqlDbType.Int, 4, t808_idexpprof);
			aParam[i++] = ParametroSql.add("@t001_idficepi_validador", SqlDbType.Int, 4, t001_idficepi_validador);
			aParam[i++] = ParametroSql.add("@t819_idplantillacvt", SqlDbType.Int, 4, t819_idplantillacvt);

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
				return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_CVT_EXPPROFFICEPI_I", aParam));
			else
				return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CVT_EXPPROFFICEPI_I", aParam));
		}
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un registro de la tabla T812_EXPPROFFICEPI,
        /// y devuelve una instancia u objeto del tipo EXPPROFFICEPI
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	28/08/2012 11:47:27
        /// </history>
        /// -----------------------------------------------------------------------------
        public static EXPPROFFICEPI Select(SqlTransaction tr, int t812_idexpprofficepi)
        {
            EXPPROFFICEPI o = new EXPPROFFICEPI();

            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t812_idexpprofficepi", SqlDbType.Int, 4, t812_idexpprofficepi);

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_CVT_EXPPROFFICEPI_S", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_EXPPROFFICEPI_S", aParam);

            if (dr.Read())
            {
                if (dr["t812_idexpprofficepi"] != DBNull.Value)
                    o.t812_idexpprofficepi = int.Parse(dr["t812_idexpprofficepi"].ToString());
                //if (dr["t812_visiblecv"] != DBNull.Value)
                    o.t812_visiblecv = dr["t812_visiblecv"].ToString();
                if (dr["t812_finicio"] != DBNull.Value)
                    o.t812_finicio = (DateTime)dr["t812_finicio"];
                if (dr["t812_ffin"] != DBNull.Value)
                    o.t812_ffin = (DateTime)dr["t812_ffin"];
                if (dr["t001_idficepi"] != DBNull.Value)
                    o.t001_idficepi = int.Parse(dr["t001_idficepi"].ToString());
                if (dr["t808_idexpprof"] != DBNull.Value)
                    o.t808_idexpprof = int.Parse(dr["t808_idexpprof"].ToString());
                if (dr["t001_idficepi_validador"] != DBNull.Value)
                    o.t001_idficepi_validador = int.Parse(dr["t001_idficepi_validador"].ToString());
                if (dr["t819_idplantillacvt"] != DBNull.Value)
                    o.t819_idplantillacvt = int.Parse(dr["t819_idplantillacvt"].ToString());
                //Validador. Se toma el de de la experiencia del profesional, si no hay se toma su evaluador progress
                if (dr["idValidador"] != DBNull.Value)
                {
                    o.idValidador = (int)dr["idValidador"];
                    o.denValidador = dr["denValidador"].ToString();
                }
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de EXPPROFFICEPI"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }
       
		#endregion
	}
}
