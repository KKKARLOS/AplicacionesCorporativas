using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : ESPACIOACUERDO
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T638_ESPACIOACUERDO
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	20/12/2010 15:44:09	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class ESPACIOACUERDO
	{
		#region Propiedades y Atributos

        private int _t638_idAcuerdo;
        public int t638_idAcuerdo
        {
            get { return _t638_idAcuerdo; }
            set { _t638_idAcuerdo = value; }
        }

        private int _t301_idproyecto;
        public int t301_idproyecto
        {
            get { return _t301_idproyecto; }
            set { _t301_idproyecto = value; }
        }

        private bool _t638_tipoIAP;
		public bool t638_tipoIAP
		{
			get {return _t638_tipoIAP;}
			set { _t638_tipoIAP = value ;}
		}

		private bool _t638_tiporesproy;
		public bool t638_tiporesproy
		{
			get {return _t638_tiporesproy;}
			set { _t638_tiporesproy = value ;}
		}

		private bool _t638_tipocliente;
		public bool t638_tipocliente
		{
			get {return _t638_tipocliente;}
			set { _t638_tipocliente = value ;}
		}

		private bool _t638_tipoimpfijo;
		public bool t638_tipoimpfijo
		{
			get {return _t638_tipoimpfijo;}
			set { _t638_tipoimpfijo = value ;}
		}

		private bool _t638_tipootras;
		public bool t638_tipootras
		{
			get {return _t638_tipootras;}
			set { _t638_tipootras = value ;}
		}

		private string _t638_textootras;
		public string t638_textootras
		{
			get {return _t638_textootras;}
			set { _t638_textootras = value ;}
		}

		private string _t638_periodicidad;
		public string t638_periodicidad
		{
			get {return _t638_periodicidad;}
			set { _t638_periodicidad = value ;}
		}

		private string _t638_aconsiderar;
		public string t638_aconsiderar
		{
			get {return _t638_aconsiderar;}
			set { _t638_aconsiderar = value ;}
		}

		private bool _t638_conciliacion;
		public bool t638_conciliacion
		{
			get {return _t638_conciliacion;}
			set { _t638_conciliacion = value ;}
		}

		private string _t638_tipoconciliacion;
		public string t638_tipoconciliacion
		{
			get {return _t638_tipoconciliacion;}
			set { _t638_tipoconciliacion = value ;}
		}

		private string _t638_contacto;
		public string t638_contacto
		{
			get {return _t638_contacto;}
			set { _t638_contacto = value ;}
		}

		private int? _t314_idusuario_findatos;
		public int? t314_idusuario_findatos
		{
			get {return _t314_idusuario_findatos;}
			set { _t314_idusuario_findatos = value ;}
		}

        private int? _t314_idusuario_aceptacion;
        public int? t314_idusuario_aceptacion
        {
            get { return _t314_idusuario_aceptacion; }
            set { _t314_idusuario_aceptacion = value; }
        }

        private int? _t314_idusuario_denegacion;
        public int? t314_idusuario_denegacion
        {
            get { return _t314_idusuario_denegacion; }
            set { _t314_idusuario_denegacion = value; }
        }

        private DateTime _t638_ffin;
        public DateTime t638_ffin
        {
            get { return _t638_ffin; }
            set { _t638_ffin = value; }
        }

        private DateTime _t638_facept;
        public DateTime t638_facept
        {
            get { return _t638_facept; }
            set { _t638_facept = value; }
        }

        private DateTime _t638_deneg;
        public DateTime t638_deneg
        {
            get { return _t638_deneg; }
            set { _t638_deneg = value; }
        }

        private int _t638_facturaSA;
        public int t638_facturaSA
        {
            get { return _t638_facturaSA; }
            set { _t638_facturaSA = value; }
        }

        #endregion

		#region Constructor

		public ESPACIOACUERDO() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T638_ESPACIOACUERDO.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	20/12/2010 15:44:09
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Insert(SqlTransaction tr, int t301_idproyecto , bool t638_tipoIAP , bool t638_tiporesproy , bool t638_tipocliente , 
                                bool t638_tipoimpfijo , bool t638_tipootras , string t638_textootras , string t638_periodicidad , 
                                string t638_aconsiderar , bool t638_conciliacion , string t638_tipoconciliacion , string t638_contacto , 
                                Nullable<int> t314_idusuario_findatos , Nullable<int> t314_idusuario_aceptacion,
                                Nullable<DateTime> t638_ffin, Nullable<DateTime> t638_facept, bool t638_facturaSA,
                                Nullable<int> t314_idusuario_denegacion, Nullable<DateTime> t638_fdeneg)
		{
			SqlParameter[] aParam = new SqlParameter[19];
			aParam[0] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
			aParam[0].Value = t301_idproyecto;
			aParam[1] = new SqlParameter("@t638_tipoIAP", SqlDbType.Bit, 1);
			aParam[1].Value = t638_tipoIAP;
			aParam[2] = new SqlParameter("@t638_tiporesproy", SqlDbType.Bit, 1);
			aParam[2].Value = t638_tiporesproy;
			aParam[3] = new SqlParameter("@t638_tipocliente", SqlDbType.Bit, 1);
			aParam[3].Value = t638_tipocliente;
			aParam[4] = new SqlParameter("@t638_tipoimpfijo", SqlDbType.Bit, 1);
			aParam[4].Value = t638_tipoimpfijo;
			aParam[5] = new SqlParameter("@t638_tipootras", SqlDbType.Bit, 1);
			aParam[5].Value = t638_tipootras;
			aParam[6] = new SqlParameter("@t638_textootras", SqlDbType.Text, 2147483647);
			aParam[6].Value = t638_textootras;
			aParam[7] = new SqlParameter("@t638_periodicidad", SqlDbType.Text, 50);
			aParam[7].Value = t638_periodicidad;
			aParam[8] = new SqlParameter("@t638_aconsiderar", SqlDbType.Text, 2147483647);
			aParam[8].Value = t638_aconsiderar;
			aParam[9] = new SqlParameter("@t638_conciliacion", SqlDbType.Bit, 1);
			aParam[9].Value = t638_conciliacion;
			aParam[10] = new SqlParameter("@t638_tipoconciliacion", SqlDbType.Char, 1);
			aParam[10].Value = t638_tipoconciliacion;
			aParam[11] = new SqlParameter("@t638_contacto", SqlDbType.Text, 250);
			aParam[11].Value = t638_contacto;
            aParam[12] = new SqlParameter("@t314_idusuario_findatos", SqlDbType.Int, 4);
            aParam[12].Value = t314_idusuario_findatos;
            aParam[13] = new SqlParameter("@t638_ffin", SqlDbType.SmallDateTime, 4);
            aParam[13].Value = t638_ffin;
            aParam[14] = new SqlParameter("@t314_idusuario_aceptacion", SqlDbType.Int, 4);
            aParam[14].Value = t314_idusuario_aceptacion;
            aParam[15] = new SqlParameter("@t638_facept", SqlDbType.SmallDateTime, 4);
            aParam[15].Value = t638_facept;
            aParam[16] = new SqlParameter("@t638_facturaSA", SqlDbType.Bit, 1);
            aParam[16].Value = t638_facturaSA;
            aParam[17] = new SqlParameter("@t314_idusuario_denegacion", SqlDbType.Int, 4);
            aParam[17].Value = t314_idusuario_denegacion;
            aParam[18] = new SqlParameter("@t638_fdeneg", SqlDbType.SmallDateTime, 4);
            aParam[18].Value = t638_fdeneg;

			if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_ESPACIOACUERDO_I", aParam));
			else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_ESPACIOACUERDO_I", aParam));
		}

        #endregion
	}
}
