using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : ORDENFAC
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T610_ORDENFAC
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	06/10/2010 13:20:03	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class ORDENFAC
	{
		#region Propiedades y Atributos

		private int _t610_idordenfac;
		public int t610_idordenfac
		{
			get {return _t610_idordenfac;}
			set { _t610_idordenfac = value ;}
		}

		private DateTime _t610_fcreacion;
		public DateTime t610_fcreacion
		{
			get {return _t610_fcreacion;}
			set { _t610_fcreacion = value ;}
		}

		private string _t610_estado;
		public string t610_estado
		{
			get {return _t610_estado;}
			set { _t610_estado = value ;}
		}

		private DateTime? _t610_ftramitada;
		public DateTime? t610_ftramitada
		{
			get {return _t610_ftramitada;}
			set { _t610_ftramitada = value ;}
		}

		private DateTime? _t610_ftraspasada;
		public DateTime? t610_ftraspasada
		{
			get {return _t610_ftraspasada;}
			set { _t610_ftraspasada = value ;}
		}

		private DateTime? _t610_fenviada;
		public DateTime? t610_fenviada
		{
			get {return _t610_fenviada;}
			set { _t610_fenviada = value ;}
		}

		private DateTime? _t610_frecogida;
		public DateTime? t610_frecogida
		{
			get {return _t610_frecogida;}
			set { _t610_frecogida = value ;}
		}

		private bool _t610_semaforo;
		public bool t610_semaforo
		{
			get {return _t610_semaforo;}
			set { _t610_semaforo = value ;}
		}

		private int _t305_idproyectosubnodo;
		public int t305_idproyectosubnodo
		{
			get {return _t305_idproyectosubnodo;}
			set { _t305_idproyectosubnodo = value ;}
		}

        private int? _t302_idcliente_solici;
        public int? t302_idcliente_solici
        {
            get { return _t302_idcliente_solici; }
            set { _t302_idcliente_solici = value; }
        }

        private int? _t302_idcliente_respago;
        public int? t302_idcliente_respago
        {
            get { return _t302_idcliente_respago; }
            set { _t302_idcliente_respago = value; }
        }

        private int? _t302_idcliente_destfact;
        public int? t302_idcliente_destfact
		{
			get {return _t302_idcliente_destfact;}
			set { _t302_idcliente_destfact = value ;}
		}

		private string _t610_condicionpago;
		public string t610_condicionpago
		{
			get {return _t610_condicionpago;}
			set { _t610_condicionpago = value ;}
		}

		private string _t610_viapago;
		public string t610_viapago
		{
			get {return _t610_viapago;}
			set { _t610_viapago = value ;}
		}

		private string _t610_refcliente;
		public string t610_refcliente
		{
			get {return _t610_refcliente;}
			set { _t610_refcliente = value ;}
		}

        private DateTime? _t610_fprevemifact;
        public DateTime? t610_fprevemifact
		{
			get {return _t610_fprevemifact;}
			set { _t610_fprevemifact = value ;}
		}

		private string _t610_moneda;
		public string t610_moneda
		{
			get {return _t610_moneda;}
			set { _t610_moneda = value ;}
		}

		private int? _t622_idagrupacion;
		public int? t622_idagrupacion
		{
			get {return _t622_idagrupacion;}
			set { _t622_idagrupacion = value ;}
		}

		private string _t610_observacionespool;
		public string t610_observacionespool
		{
			get {return _t610_observacionespool;}
			set { _t610_observacionespool = value ;}
		}

		private string _t610_comentario;
		public string t610_comentario
		{
			get {return _t610_comentario;}
			set { _t610_comentario = value ;}
		}

        private string _t610_dvsap;
		public string t610_dvsap
		{
            get { return _t610_dvsap; }
            set { _t610_dvsap = value; }
		}

        private string _t621_idovsap;
        public string t621_idovsap
		{
            get { return _t621_idovsap; }
            set { _t621_idovsap = value; }
		}

		private float _t610_dto_porcen;
		public float t610_dto_porcen
		{
			get {return _t610_dto_porcen;}
			set { _t610_dto_porcen = value ;}
		}

		private decimal _t610_dto_importe;
		public decimal t610_dto_importe
		{
			get {return _t610_dto_importe;}
			set { _t610_dto_importe = value ;}
		}

		private DateTime? _t610_fdiferida;
		public DateTime? t610_fdiferida
		{
			get {return _t610_fdiferida;}
			set { _t610_fdiferida = value ;}
		}

        private int _t314_idusuario_respcomercial;
        public int t314_idusuario_respcomercial
        {
            get { return _t314_idusuario_respcomercial; }
            set { _t314_idusuario_respcomercial = value; }
        }

        private bool _t610_ivaincluido;
        public bool t610_ivaincluido
        {
            get { return _t610_ivaincluido; }
            set { _t610_ivaincluido = value; }
        }

        private string _t610_textocabecera;
        public string t610_textocabecera
		{
            get { return _t610_textocabecera; }
            set { _t610_textocabecera = value; }
		}

        private string _t610_observacionesplan;
        public string t610_observacionesplan
        {
            get { return _t610_observacionesplan; }
            set { _t610_observacionesplan = value; }
        }

        public string t610_infotramit { get; set; }
        public string denComercial { get; set; }
        public int numDocs { get; set; }

        #endregion

        #region Constructor

        public ORDENFAC() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos


        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina un registro de la tabla T610_ORDENFAC a traves de la primary key.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	06/10/2010 15:21:52
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Delete(SqlTransaction tr, int t610_idordenfac)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t610_idordenfac", SqlDbType.Int, 4);
            aParam[0].Value = t610_idordenfac;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_ORDENFAC_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ORDENFAC_D", aParam);
        }

		#endregion
	}
}
