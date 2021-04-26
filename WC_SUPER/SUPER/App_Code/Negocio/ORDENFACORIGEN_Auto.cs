using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : ORDENFACORIGEN
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T619_ORDENFACORIGEN
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	02/11/2010 12:09:32	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class ORDENFACORIGEN
	{
		#region Propiedades y Atributos

		private int _t619_idordenfac;
		public int t619_idordenfac
		{
			get {return _t619_idordenfac;}
			set { _t619_idordenfac = value ;}
		}

		private int _t305_idproyectosubnodo;
		public int t305_idproyectosubnodo
		{
			get {return _t305_idproyectosubnodo;}
			set { _t305_idproyectosubnodo = value ;}
		}

		private int _t302_idcliente_solici;
		public int t302_idcliente_solici
		{
			get {return _t302_idcliente_solici;}
			set { _t302_idcliente_solici = value ;}
		}

		private int _t302_idcliente_respago;
		public int t302_idcliente_respago
		{
			get {return _t302_idcliente_respago;}
			set { _t302_idcliente_respago = value ;}
		}

		private int _t302_idcliente_destfact;
		public int t302_idcliente_destfact
		{
			get {return _t302_idcliente_destfact;}
			set { _t302_idcliente_destfact = value ;}
		}

		private string _t619_condicionpago;
		public string t619_condicionpago
		{
			get {return _t619_condicionpago;}
			set { _t619_condicionpago = value ;}
		}

		private string _t619_viapago;
		public string t619_viapago
		{
			get {return _t619_viapago;}
			set { _t619_viapago = value ;}
		}

		private string _t619_refcliente;
		public string t619_refcliente
		{
			get {return _t619_refcliente;}
			set { _t619_refcliente = value ;}
		}

		private DateTime _t619_fprevemifact;
		public DateTime t619_fprevemifact
		{
			get {return _t619_fprevemifact;}
			set { _t619_fprevemifact = value ;}
		}

		private string _t619_moneda;
		public string t619_moneda
		{
			get {return _t619_moneda;}
			set { _t619_moneda = value ;}
		}

		private int? _t619_idagrupacion;
		public int? t619_idagrupacion
		{
			get {return _t619_idagrupacion;}
			set { _t619_idagrupacion = value ;}
		}

		private string _t619_observacionespool;
		public string t619_observacionespool
		{
			get {return _t619_observacionespool;}
			set { _t619_observacionespool = value ;}
		}

		private string _t619_comentario;
		public string t619_comentario
		{
			get {return _t619_comentario;}
			set { _t619_comentario = value ;}
		}

		private string _t621_idovsap;
		public string t621_idovsap
		{
			get {return _t621_idovsap;}
			set { _t621_idovsap = value ;}
		}

		private float _t619_dto_porcen;
		public float t619_dto_porcen
		{
			get {return _t619_dto_porcen;}
			set { _t619_dto_porcen = value ;}
		}

		private decimal _t619_dto_importe;
		public decimal t619_dto_importe
		{
			get {return _t619_dto_importe;}
			set { _t619_dto_importe = value ;}
		}

		private DateTime? _t619_fdiferida;
		public DateTime? t619_fdiferida
		{
			get {return _t619_fdiferida;}
			set { _t619_fdiferida = value ;}
		}

        private int _t314_idusuario_respcomercial;
        public int t314_idusuario_respcomercial
        {
            get { return _t314_idusuario_respcomercial; }
            set { _t314_idusuario_respcomercial = value; }
        }

        private bool _t619_ivaincluido;
        public bool t619_ivaincluido
        {
            get { return _t619_ivaincluido; }
            set { _t619_ivaincluido = value; }
        }
        public string t619_infotramit { get; set; }
		#endregion

		#region Constructor

		public ORDENFACORIGEN() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

	}
}
