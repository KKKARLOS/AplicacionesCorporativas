using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : PLANTILLAORDENFAC
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T629_PLANTILLAORDENFAC
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	18/11/2010 10:31:34	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class PLANTILLAORDENFAC
	{
		#region Propiedades y Atributos

		private int _t629_idplantillaof;
		public int t629_idplantillaof
		{
			get {return _t629_idplantillaof;}
			set { _t629_idplantillaof = value ;}
		}

		private string _t629_denominacion;
		public string t629_denominacion
		{
			get {return _t629_denominacion;}
			set { _t629_denominacion = value ;}
		}

		private string _t629_estado;
		public string t629_estado
		{
			get {return _t629_estado;}
			set { _t629_estado = value ;}
		}

		private int _t314_idusuario;
		public int t314_idusuario
		{
			get {return _t314_idusuario;}
			set { _t314_idusuario = value ;}
		}

		private int? _t305_idproyectosubnodo;
		public int? t305_idproyectosubnodo
		{
			get {return _t305_idproyectosubnodo;}
			set { _t305_idproyectosubnodo = value ;}
		}

		private int? _t302_idcliente_solici;
		public int? t302_idcliente_solici
		{
			get {return _t302_idcliente_solici;}
			set { _t302_idcliente_solici = value ;}
		}

		private int? _t302_idcliente_respago;
		public int? t302_idcliente_respago
		{
			get {return _t302_idcliente_respago;}
			set { _t302_idcliente_respago = value ;}
		}

		private int? _t302_idcliente_destfact;
		public int? t302_idcliente_destfact
		{
			get {return _t302_idcliente_destfact;}
			set { _t302_idcliente_destfact = value ;}
		}

		private string _t629_condicionpago;
		public string t629_condicionpago
		{
			get {return _t629_condicionpago;}
			set { _t629_condicionpago = value ;}
		}

		private string _t629_viapago;
		public string t629_viapago
		{
			get {return _t629_viapago;}
			set { _t629_viapago = value ;}
		}

		private string _t629_refcliente;
		public string t629_refcliente
		{
			get {return _t629_refcliente;}
			set { _t629_refcliente = value ;}
		}

		private DateTime? _t629_fprevemifact;
		public DateTime? t629_fprevemifact
		{
			get {return _t629_fprevemifact;}
			set { _t629_fprevemifact = value ;}
		}

		private string _t629_moneda;
		public string t629_moneda
		{
			get {return _t629_moneda;}
			set { _t629_moneda = value ;}
		}

		private int? _t622_idagrupacion;
		public int? t622_idagrupacion
		{
			get {return _t622_idagrupacion;}
			set { _t622_idagrupacion = value ;}
		}

		private string _t629_observacionespool;
		public string t629_observacionespool
		{
			get {return _t629_observacionespool;}
			set { _t629_observacionespool = value ;}
		}

		private string _t629_comentario;
		public string t629_comentario
		{
			get {return _t629_comentario;}
			set { _t629_comentario = value ;}
		}

		private string _t621_idovsap;
		public string t621_idovsap
		{
			get {return _t621_idovsap;}
			set { _t621_idovsap = value ;}
		}

		private float _t629_dto_porcen;
		public float t629_dto_porcen
		{
			get {return _t629_dto_porcen;}
			set { _t629_dto_porcen = value ;}
		}

		private decimal _t629_dto_importe;
		public decimal t629_dto_importe
		{
			get {return _t629_dto_importe;}
			set { _t629_dto_importe = value ;}
		}

		private bool _t629_ivaincluido;
		public bool t629_ivaincluido
		{
			get {return _t629_ivaincluido;}
			set { _t629_ivaincluido = value ;}
		}

        private string _t629_observacionesplan;
        public string t629_observacionesplan
        {
            get { return _t629_observacionesplan; }
            set { _t629_observacionesplan = value; }
        }

        private string _t629_textocabecera;
        public string t629_textocabecera
        {
            get { return _t629_textocabecera; }
            set { _t629_textocabecera = value; }
        }
        public bool t302_efactur { get; set; }

        #endregion

		#region Constructor

		public PLANTILLAORDENFAC() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T629_PLANTILLAORDENFAC.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	18/11/2010 10:31:34
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t629_idplantillaof, string t629_denominacion, string t629_estado, Nullable<int> t305_idproyectosubnodo, Nullable<int> t302_idcliente_solici, Nullable<int> t302_idcliente_respago, Nullable<int> t302_idcliente_destfact, string t629_condicionpago, string t629_viapago, string t629_refcliente, Nullable<DateTime> t629_fprevemifact, string t629_moneda, Nullable<int> t622_idagrupacion, string t629_observacionespool, string t629_comentario, string t621_idovsap, float t629_dto_porcen, decimal t629_dto_importe, bool t629_ivaincluido, string t629_observacionesplan, string t629_textocabecera)//, int t314_idusuario
		{
			SqlParameter[] aParam = new SqlParameter[21];
			aParam[0] = new SqlParameter("@t629_idplantillaof", SqlDbType.Int, 4);
			aParam[0].Value = t629_idplantillaof;
			aParam[1] = new SqlParameter("@t629_denominacion", SqlDbType.Text, 50);
			aParam[1].Value = t629_denominacion;
			aParam[2] = new SqlParameter("@t629_estado", SqlDbType.Text, 1);
			aParam[2].Value = t629_estado;
            //aParam[3] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            //aParam[3].Value = t314_idusuario;
			aParam[3] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
			aParam[3].Value = t305_idproyectosubnodo;
			aParam[4] = new SqlParameter("@t302_idcliente_solici", SqlDbType.Int, 4);
			aParam[4].Value = t302_idcliente_solici;
			aParam[5] = new SqlParameter("@t302_idcliente_respago", SqlDbType.Int, 4);
			aParam[5].Value = t302_idcliente_respago;
			aParam[6] = new SqlParameter("@t302_idcliente_destfact", SqlDbType.Int, 4);
			aParam[6].Value = t302_idcliente_destfact;
			aParam[7] = new SqlParameter("@t629_condicionpago", SqlDbType.Text, 4);
			aParam[7].Value = t629_condicionpago;
			aParam[8] = new SqlParameter("@t629_viapago", SqlDbType.Text, 1);
			aParam[8].Value = t629_viapago;
			aParam[9] = new SqlParameter("@t629_refcliente", SqlDbType.Text, 35);
			aParam[9].Value = t629_refcliente;
			aParam[10] = new SqlParameter("@t629_fprevemifact", SqlDbType.SmallDateTime, 4);
			aParam[10].Value = t629_fprevemifact;
			aParam[11] = new SqlParameter("@t629_moneda", SqlDbType.Text, 5);
			aParam[11].Value = t629_moneda;
			aParam[12] = new SqlParameter("@t622_idagrupacion", SqlDbType.Int, 4);
			aParam[12].Value = t622_idagrupacion;
			aParam[13] = new SqlParameter("@t629_observacionespool", SqlDbType.Text, 2147483647);
			aParam[13].Value = t629_observacionespool;
			aParam[14] = new SqlParameter("@t629_comentario", SqlDbType.Text, 2147483647);
			aParam[14].Value = t629_comentario;
			aParam[15] = new SqlParameter("@t621_idovsap", SqlDbType.Text, 4);
			aParam[15].Value = t621_idovsap;
			aParam[16] = new SqlParameter("@t629_dto_porcen", SqlDbType.Real, 4);
			aParam[16].Value = t629_dto_porcen;
			aParam[17] = new SqlParameter("@t629_dto_importe", SqlDbType.Money, 8);
			aParam[17].Value = t629_dto_importe;
			aParam[18] = new SqlParameter("@t629_ivaincluido", SqlDbType.Bit, 1);
			aParam[18].Value = t629_ivaincluido;
            aParam[19] = new SqlParameter("@t629_observacionesplan", SqlDbType.Text, 2147483647);
            aParam[19].Value = t629_observacionesplan;
            aParam[20] = new SqlParameter("@t629_textocabecera", SqlDbType.Text, 2147483647);
            aParam[20].Value = t629_textocabecera;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_PLANTILLAORDENFAC_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PLANTILLAORDENFAC_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T629_PLANTILLAORDENFAC a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	18/11/2010 10:31:34
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t629_idplantillaof)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t629_idplantillaof", SqlDbType.Int, 4);
			aParam[0].Value = t629_idplantillaof;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_PLANTILLAORDENFAC_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PLANTILLAORDENFAC_D", aParam);
		}


		#endregion
	}
}
