using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : NODO
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T303_NODO
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	25/02/2010 15:08:09	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class NODO
	{
		#region Propiedades y Atributos

		private int _t303_idnodo;
		public int t303_idnodo
		{
			get {return _t303_idnodo;}
			set { _t303_idnodo = value ;}
		}

		private string _t303_denominacion;
		public string t303_denominacion
		{
			get {return _t303_denominacion;}
			set { _t303_denominacion = value ;}
		}

        private string _t303_denabreviada;
        public string t303_denabreviada
		{
            get { return _t303_denabreviada; }
            set { _t303_denabreviada = value; }
		}
        
		private bool _t303_masdeungf;
		public bool t303_masdeungf
		{
			get {return _t303_masdeungf;}
			set { _t303_masdeungf = value ;}
		}

		private int _t391_idsupernodo1;
		public int t391_idsupernodo1
		{
			get {return _t391_idsupernodo1;}
			set { _t391_idsupernodo1 = value ;}
		}

		private int _t313_idempresa;
		public int t313_idempresa
		{
			get {return _t313_idempresa;}
			set { _t313_idempresa = value ;}
		}

		private bool _t303_cierreECOestandar;
		public bool t303_cierreECOestandar
		{
			get {return _t303_cierreECOestandar;}
			set { _t303_cierreECOestandar = value ;}
		}

		private int _t303_ultcierreeco;
		public int t303_ultcierreeco
		{
			get {return _t303_ultcierreeco;}
			set { _t303_ultcierreeco = value ;}
		}

		private bool _t303_estado;
		public bool t303_estado
		{
			get {return _t303_estado;}
			set { _t303_estado = value ;}
		}

		private string _t303_modelocostes;
		public string t303_modelocostes
		{
			get {return _t303_modelocostes;}
			set { _t303_modelocostes = value ;}
		}

		private string _t303_modelotarifas;
		public string t303_modelotarifas
		{
			get {return _t303_modelotarifas;}
			set { _t303_modelotarifas = value ;}
		}

		private bool _t303_generareplica;
		public bool t303_generareplica
		{
			get {return _t303_generareplica;}
			set { _t303_generareplica = value ;}
		}

		private byte _t303_porctolerancia;
		public byte t303_porctolerancia
		{
			get {return _t303_porctolerancia;}
			set { _t303_porctolerancia = value ;}
		}

		private int _t314_idusuario_responsable;
		public int t314_idusuario_responsable
		{
			get {return _t314_idusuario_responsable;}
			set { _t314_idusuario_responsable = value ;}
		}

		private int _t303_orden;
		public int t303_orden
		{
			get {return _t303_orden;}
			set { _t303_orden = value ;}
		}

		private bool _t303_representativo;
		public bool t303_representativo
		{
			get {return _t303_representativo;}
			set { _t303_representativo = value ;}
		}

		private string _t303_interfacehermes;
		public string t303_interfacehermes
		{
			get {return _t303_interfacehermes;}
			set { _t303_interfacehermes = value ;}
		}

		private bool _t303_defectocalcularGF;
		public bool t303_defectocalcularGF
		{
			get {return _t303_defectocalcularGF;}
			set { _t303_defectocalcularGF = value ;}
		}

		private bool _t303_defectomailiap;
		public bool t303_defectomailiap
		{
			get {return _t303_defectomailiap;}
			set { _t303_defectomailiap = value ;}
		}

		private bool _t303_cierreIAPestandar;
		public bool t303_cierreIAPestandar
		{
			get {return _t303_cierreIAPestandar;}
			set { _t303_cierreIAPestandar = value ;}
		}

		private int _t303_ultcierreIAP;
		public int t303_ultcierreIAP
		{
			get {return _t303_ultcierreIAP;}
			set { _t303_ultcierreIAP = value ;}
		}

		private bool _t303_compcontprod;
		public bool t303_compcontprod
		{
			get {return _t303_compcontprod;}
			set { _t303_compcontprod = value ;}
		}

		private bool _t303_defectoPIG;
		public bool t303_defectoPIG
		{
			get {return _t303_defectoPIG;}
			set { _t303_defectoPIG = value ;}
		}

		private float _t303_margencesionprof;
		public float t303_margencesionprof
		{
			get {return _t303_margencesionprof;}
			set { _t303_margencesionprof = value ;}
		}

		private float _t303_interesGF;
		public float t303_interesGF
		{
			get {return _t303_interesGF;}
			set { _t303_interesGF = value ;}
		}

		private string _t303_denominacion_CNP;
		public string t303_denominacion_CNP
		{
			get {return _t303_denominacion_CNP;}
			set { _t303_denominacion_CNP = value ;}
		}

		private bool _t303_obligatorio_CNP;
		public bool t303_obligatorio_CNP
		{
			get {return _t303_obligatorio_CNP;}
			set { _t303_obligatorio_CNP = value ;}
		}

		private string _t303_asignarperfiles;
		public string t303_asignarperfiles
		{
			get {return _t303_asignarperfiles;}
			set { _t303_asignarperfiles = value ;}
		}

		private bool _t303_desglose;
		public bool t303_desglose
		{
			get {return _t303_desglose;}
			set { _t303_desglose = value ;}
		}

		private bool _t303_pgrcg;
		public bool t303_pgrcg
		{
			get {return _t303_pgrcg;}
			set { _t303_pgrcg = value ;}
		}

		private bool _t303_controlhuecos;
		public bool t303_controlhuecos
		{
			get {return _t303_controlhuecos;}
			set { _t303_controlhuecos = value ;}
		}

		private bool _t303_tipolinterna;
		public bool t303_tipolinterna
		{
			get {return _t303_tipolinterna;}
			set { _t303_tipolinterna = value ;}
		}

		private bool _t303_tipolespecial;
		public bool t303_tipolespecial
		{
			get {return _t303_tipolespecial;}
			set { _t303_tipolespecial = value ;}
		}

        private bool _t303_tipolproductivaSC;
        public bool t303_tipolproductivaSC
        {
            get { return _t303_tipolproductivaSC; }
            set { _t303_tipolproductivaSC = value; }
        }

        private bool _t303_defectoadmiterecursospst;
        public bool t303_defectoadmiterecursospst
        {
            get { return _t303_defectoadmiterecursospst; }
            set { _t303_defectoadmiterecursospst = value; }
        }

        private string _t621_idovsap;
        public string t621_idovsap
        {
            get { return _t621_idovsap; }
            set { _t621_idovsap = value; }
        }

        private bool _t303_msa;
        public bool t303_msa
        {
            get { return _t303_msa; }
            set { _t303_msa = value; }
        }

        private string _t422_idmoneda;
        public string t422_idmoneda
        {
            get { return _t422_idmoneda; }
            set { _t422_idmoneda = value; }
        }

        public bool activoqeq { get; set; }
        public bool instrumental { get; set; }

        #endregion

        #region Constructor

        public NODO() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T303_NODO.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	25/02/2010 15:08:09
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, string t303_denominacion, bool t303_masdeungf, int t391_idsupernodo1, int t313_idempresa, 
                    bool t303_cierreECOestandar, int t303_ultcierreeco, bool t303_estado, string t303_modelocostes, string t303_modelotarifas, 
                    bool t303_generareplica, byte t303_porctolerancia, int t314_idusuario_responsable, int t303_orden, bool t303_representativo,
                    string t303_interfacehermes, bool t303_defectocalcularGF, bool t303_defectomailiap, bool t303_cierreIAPestandar, 
                    int t303_ultcierreIAP, bool t303_compcontprod, bool t303_defectoPIG, float t303_margencesionprof, float t303_interesGF, 
                    string t303_denominacion_CNP, bool t303_obligatorio_CNP, string t303_asignarperfiles, bool t303_desglose, bool t303_pgrcg, 
                    bool t303_controlhuecos, bool t303_tipolinterna, bool t303_tipolespecial, bool t303_tipolproductivaSC, 
                    bool t303_defectoadmiterecursospst, string t621_idovsap, bool t303_msa, string t303_denabreviada, bool t303_noalertas,
                    bool t303_cualificacionCVT, string t422_idmoneda, int t055_idcalifOCFA, Nullable<int> ta212_idorganizacioncomercial,
                    bool activoqeq, bool bInstrumental)
		{
			SqlParameter[] aParam = new SqlParameter[43];
			aParam[0] = new SqlParameter("@t303_denominacion", SqlDbType.Text, 50);
			aParam[0].Value = t303_denominacion;
			aParam[1] = new SqlParameter("@t303_masdeungf", SqlDbType.Bit, 1);
			aParam[1].Value = t303_masdeungf;
			aParam[2] = new SqlParameter("@t391_idsupernodo1", SqlDbType.Int, 4);
			aParam[2].Value = t391_idsupernodo1;
			aParam[3] = new SqlParameter("@t313_idempresa", SqlDbType.Int, 4);
			aParam[3].Value = t313_idempresa;
			aParam[4] = new SqlParameter("@t303_cierreECOestandar", SqlDbType.Bit, 1);
			aParam[4].Value = t303_cierreECOestandar;
			aParam[5] = new SqlParameter("@t303_ultcierreeco", SqlDbType.Int, 4);
			aParam[5].Value = t303_ultcierreeco;
			aParam[6] = new SqlParameter("@t303_estado", SqlDbType.Bit, 1);
			aParam[6].Value = t303_estado;
			aParam[7] = new SqlParameter("@t303_modelocostes", SqlDbType.Text, 1);
			aParam[7].Value = t303_modelocostes;
			aParam[8] = new SqlParameter("@t303_modelotarifas", SqlDbType.Text, 1);
			aParam[8].Value = t303_modelotarifas;
			aParam[9] = new SqlParameter("@t303_generareplica", SqlDbType.Bit, 1);
			aParam[9].Value = t303_generareplica;
			aParam[10] = new SqlParameter("@t303_porctolerancia", SqlDbType.TinyInt, 1);
			aParam[10].Value = t303_porctolerancia;
			aParam[11] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
			aParam[11].Value = t314_idusuario_responsable;
			aParam[12] = new SqlParameter("@t303_orden", SqlDbType.Int, 4);
			aParam[12].Value = t303_orden;
			aParam[13] = new SqlParameter("@t303_representativo", SqlDbType.Bit, 1);
			aParam[13].Value = t303_representativo;
			aParam[14] = new SqlParameter("@t303_interfacehermes", SqlDbType.Text, 1);
			aParam[14].Value = t303_interfacehermes;
			aParam[15] = new SqlParameter("@t303_defectocalcularGF", SqlDbType.Bit, 1);
			aParam[15].Value = t303_defectocalcularGF;
			aParam[16] = new SqlParameter("@t303_defectomailiap", SqlDbType.Bit, 1);
			aParam[16].Value = t303_defectomailiap;
			aParam[17] = new SqlParameter("@t303_cierreIAPestandar", SqlDbType.Bit, 1);
			aParam[17].Value = t303_cierreIAPestandar;
			aParam[18] = new SqlParameter("@t303_ultcierreIAP", SqlDbType.Int, 4);
			aParam[18].Value = t303_ultcierreIAP;
			aParam[19] = new SqlParameter("@t303_compcontprod", SqlDbType.Bit, 1);
			aParam[19].Value = t303_compcontprod;
			aParam[20] = new SqlParameter("@t303_defectoPIG", SqlDbType.Bit, 1);
			aParam[20].Value = t303_defectoPIG;
			aParam[21] = new SqlParameter("@t303_margencesionprof", SqlDbType.Real, 4);
			aParam[21].Value = t303_margencesionprof;
			aParam[22] = new SqlParameter("@t303_interesGF", SqlDbType.Real, 4);
			aParam[22].Value = t303_interesGF;
			aParam[23] = new SqlParameter("@t303_denominacion_CNP", SqlDbType.Text, 15);
			aParam[23].Value = t303_denominacion_CNP;
			aParam[24] = new SqlParameter("@t303_obligatorio_CNP", SqlDbType.Bit, 1);
			aParam[24].Value = t303_obligatorio_CNP;
			aParam[25] = new SqlParameter("@t303_asignarperfiles", SqlDbType.Text, 1);
			aParam[25].Value = t303_asignarperfiles;
			aParam[26] = new SqlParameter("@t303_desglose", SqlDbType.Bit, 1);
			aParam[26].Value = t303_desglose;
			aParam[27] = new SqlParameter("@t303_pgrcg", SqlDbType.Bit, 1);
			aParam[27].Value = t303_pgrcg;
			aParam[28] = new SqlParameter("@t303_controlhuecos", SqlDbType.Bit, 1);
			aParam[28].Value = t303_controlhuecos;
			aParam[29] = new SqlParameter("@t303_tipolinterna", SqlDbType.Bit, 1);
			aParam[29].Value = t303_tipolinterna;
			aParam[30] = new SqlParameter("@t303_tipolespecial", SqlDbType.Bit, 1);
			aParam[30].Value = t303_tipolespecial;
            aParam[31] = new SqlParameter("@t303_tipolproductivaSC", SqlDbType.Bit, 1);
            aParam[31].Value = t303_tipolproductivaSC;
            aParam[32] = new SqlParameter("@t303_defectoadmiterecursospst", SqlDbType.Bit, 1);
            aParam[32].Value = t303_defectoadmiterecursospst;
            aParam[33] = new SqlParameter("@t621_idovsap", SqlDbType.VarChar, 4);
            aParam[33].Value = t621_idovsap;
            aParam[34] = new SqlParameter("@t303_msa", SqlDbType.Bit, 1);
            aParam[34].Value = t303_msa;
            aParam[35] = new SqlParameter("@t303_denabreviada", SqlDbType.VarChar, 15);
            aParam[35].Value = t303_denabreviada;
            aParam[36] = new SqlParameter("@t303_noalertas", SqlDbType.Bit, 1);
            aParam[36].Value = t303_noalertas;
            aParam[37] = new SqlParameter("@t303_cualificacionCVT", SqlDbType.Bit, 1);
            aParam[37].Value = t303_cualificacionCVT;
            aParam[38] = new SqlParameter("@t422_idmoneda", SqlDbType.Text, 5);
            aParam[38].Value = t422_idmoneda;
            aParam[39] = new SqlParameter("@t055_idcalifOCFA", SqlDbType.Int, 4);
            aParam[39].Value = t055_idcalifOCFA;
            aParam[40] = new SqlParameter("@ta212_idorganizacioncomercial", SqlDbType.Int, 4);
            aParam[40].Value = ta212_idorganizacioncomercial;
            aParam[41] = new SqlParameter("@activoqeq", SqlDbType.Bit, 1);
            aParam[41].Value = activoqeq;
            aParam[42] = new SqlParameter("@t303_soloinstrumental", SqlDbType.Bit, 1);
            aParam[42].Value = bInstrumental;

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
				return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_NODO_I", aParam));
			else
				return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_NODO_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T303_NODO.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	25/02/2010 15:08:09
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t303_idnodo, string t303_denominacion, bool t303_masdeungf, int t391_idsupernodo1, 
                    int t313_idempresa, bool t303_cierreECOestandar, int t303_ultcierreeco, bool t303_estado, string t303_modelocostes, 
                    string t303_modelotarifas, bool t303_generareplica, byte t303_porctolerancia, int t314_idusuario_responsable, 
                    int t303_orden, bool t303_representativo, string t303_interfacehermes, bool t303_defectocalcularGF, 
                    bool t303_defectomailiap, bool t303_cierreIAPestandar, int t303_ultcierreIAP, bool t303_compcontprod, bool t303_defectoPIG, 
                    float t303_margencesionprof, float t303_interesGF, string t303_denominacion_CNP, bool t303_obligatorio_CNP, 
                    string t303_asignarperfiles, bool t303_desglose, bool t303_pgrcg, bool t303_controlhuecos, bool t303_tipolinterna, 
                    bool t303_tipolespecial, bool t303_tipolproductivaSC, bool t303_defectoadmiterecursospst, string t621_idovsap,
                    bool t303_msa, string t303_denabreviada, bool t303_noalertas, bool t303_cualificacionCVT, string t422_idmoneda,
                    int t055_idcalifOCFA, Nullable<int> ta212_idorganizacioncomercial, bool activoqeq, bool bInstrumental)
		{
			SqlParameter[] aParam = new SqlParameter[44];
			aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
			aParam[0].Value = t303_idnodo;
			aParam[1] = new SqlParameter("@t303_denominacion", SqlDbType.Text, 50);
			aParam[1].Value = t303_denominacion;
			aParam[2] = new SqlParameter("@t303_masdeungf", SqlDbType.Bit, 1);
			aParam[2].Value = t303_masdeungf;
			aParam[3] = new SqlParameter("@t391_idsupernodo1", SqlDbType.Int, 4);
			aParam[3].Value = t391_idsupernodo1;
			aParam[4] = new SqlParameter("@t313_idempresa", SqlDbType.Int, 4);
			aParam[4].Value = t313_idempresa;
			aParam[5] = new SqlParameter("@t303_cierreECOestandar", SqlDbType.Bit, 1);
			aParam[5].Value = t303_cierreECOestandar;
			aParam[6] = new SqlParameter("@t303_ultcierreeco", SqlDbType.Int, 4);
			aParam[6].Value = t303_ultcierreeco;
			aParam[7] = new SqlParameter("@t303_estado", SqlDbType.Bit, 1);
			aParam[7].Value = t303_estado;
			aParam[8] = new SqlParameter("@t303_modelocostes", SqlDbType.Text, 1);
			aParam[8].Value = t303_modelocostes;
			aParam[9] = new SqlParameter("@t303_modelotarifas", SqlDbType.Text, 1);
			aParam[9].Value = t303_modelotarifas;
			aParam[10] = new SqlParameter("@t303_generareplica", SqlDbType.Bit, 1);
			aParam[10].Value = t303_generareplica;
			aParam[11] = new SqlParameter("@t303_porctolerancia", SqlDbType.TinyInt, 1);
			aParam[11].Value = t303_porctolerancia;
			aParam[12] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
			aParam[12].Value = t314_idusuario_responsable;
			aParam[13] = new SqlParameter("@t303_orden", SqlDbType.Int, 4);
			aParam[13].Value = t303_orden;
			aParam[14] = new SqlParameter("@t303_representativo", SqlDbType.Bit, 1);
			aParam[14].Value = t303_representativo;
			aParam[15] = new SqlParameter("@t303_interfacehermes", SqlDbType.Text, 1);
			aParam[15].Value = t303_interfacehermes;
			aParam[16] = new SqlParameter("@t303_defectocalcularGF", SqlDbType.Bit, 1);
			aParam[16].Value = t303_defectocalcularGF;
			aParam[17] = new SqlParameter("@t303_defectomailiap", SqlDbType.Bit, 1);
			aParam[17].Value = t303_defectomailiap;
			aParam[18] = new SqlParameter("@t303_cierreIAPestandar", SqlDbType.Bit, 1);
			aParam[18].Value = t303_cierreIAPestandar;
			aParam[19] = new SqlParameter("@t303_ultcierreIAP", SqlDbType.Int, 4);
			aParam[19].Value = t303_ultcierreIAP;
			aParam[20] = new SqlParameter("@t303_compcontprod", SqlDbType.Bit, 1);
			aParam[20].Value = t303_compcontprod;
			aParam[21] = new SqlParameter("@t303_defectoPIG", SqlDbType.Bit, 1);
			aParam[21].Value = t303_defectoPIG;
			aParam[22] = new SqlParameter("@t303_margencesionprof", SqlDbType.Real, 4);
			aParam[22].Value = t303_margencesionprof;
			aParam[23] = new SqlParameter("@t303_interesGF", SqlDbType.Real, 4);
			aParam[23].Value = t303_interesGF;
			aParam[24] = new SqlParameter("@t303_denominacion_CNP", SqlDbType.Text, 15);
			aParam[24].Value = t303_denominacion_CNP;
			aParam[25] = new SqlParameter("@t303_obligatorio_CNP", SqlDbType.Bit, 1);
			aParam[25].Value = t303_obligatorio_CNP;
			aParam[26] = new SqlParameter("@t303_asignarperfiles", SqlDbType.Text, 1);
			aParam[26].Value = t303_asignarperfiles;
			aParam[27] = new SqlParameter("@t303_desglose", SqlDbType.Bit, 1);
			aParam[27].Value = t303_desglose;
			aParam[28] = new SqlParameter("@t303_pgrcg", SqlDbType.Bit, 1);
			aParam[28].Value = t303_pgrcg;
			aParam[29] = new SqlParameter("@t303_controlhuecos", SqlDbType.Bit, 1);
			aParam[29].Value = t303_controlhuecos;
			aParam[30] = new SqlParameter("@t303_tipolinterna", SqlDbType.Bit, 1);
			aParam[30].Value = t303_tipolinterna;
			aParam[31] = new SqlParameter("@t303_tipolespecial", SqlDbType.Bit, 1);
			aParam[31].Value = t303_tipolespecial;
			aParam[32] = new SqlParameter("@t303_tipolproductivaSC", SqlDbType.Bit, 1);
			aParam[32].Value = t303_tipolproductivaSC;
            aParam[33] = new SqlParameter("@t303_defectoadmiterecursospst", SqlDbType.Bit, 1);
            aParam[33].Value = t303_defectoadmiterecursospst;
            aParam[34] = new SqlParameter("@t621_idovsap", SqlDbType.VarChar, 4);
            aParam[34].Value = t621_idovsap;
            aParam[35] = new SqlParameter("@t303_msa", SqlDbType.Bit, 1);
            aParam[35].Value = t303_msa;
            aParam[36] = new SqlParameter("@t303_denabreviada", SqlDbType.VarChar, 15);
            aParam[36].Value = t303_denabreviada;
            aParam[37] = new SqlParameter("@t303_noalertas", SqlDbType.Bit, 1);
            aParam[37].Value = t303_noalertas;
            aParam[38] = new SqlParameter("@t303_cualificacionCVT", SqlDbType.Bit, 1);
            aParam[38].Value = t303_cualificacionCVT;
            aParam[39] = new SqlParameter("@t422_idmoneda", SqlDbType.Text, 5);
            aParam[39].Value = t422_idmoneda;
            aParam[40] = new SqlParameter("@t055_idcalifOCFA", SqlDbType.Int, 4);
            aParam[40].Value = t055_idcalifOCFA;
            aParam[41] = new SqlParameter("@ta212_idorganizacioncomercial", SqlDbType.Int, 4);
            aParam[41].Value = ta212_idorganizacioncomercial;
            aParam[42] = new SqlParameter("@activoqeq", SqlDbType.Bit, 1);
            aParam[42].Value = activoqeq;
            aParam[43] = new SqlParameter("@t303_soloinstrumental", SqlDbType.Bit, 1);
            aParam[43].Value = bInstrumental;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_NODO_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_NODO_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T303_NODO a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	25/02/2010 15:08:09
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t303_idnodo)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
			aParam[0].Value = t303_idnodo;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_NODO_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_NODO_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T303_NODO,
		/// y devuelve una instancia u objeto del tipo NODO
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	25/02/2010 15:08:09
		/// </history>
		/// -----------------------------------------------------------------------------
		public static NODO Select(SqlTransaction tr, int t303_idnodo) 
		{
			NODO o = new NODO();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
			aParam[0].Value = t303_idnodo;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_NODO_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_NODO_S", aParam);

			if (dr.Read())
			{
				if (dr["t303_idnodo"] != DBNull.Value)
					o.t303_idnodo = int.Parse(dr["t303_idnodo"].ToString());
				if (dr["t303_denominacion"] != DBNull.Value)
					o.t303_denominacion = (string)dr["t303_denominacion"];
                if (dr["t303_denabreviada"] != DBNull.Value)
                    o.t303_denabreviada = (string)dr["t303_denabreviada"];
                if (dr["t303_masdeungf"] != DBNull.Value)
					o.t303_masdeungf = (bool)dr["t303_masdeungf"];
				if (dr["t391_idsupernodo1"] != DBNull.Value)
					o.t391_idsupernodo1 = int.Parse(dr["t391_idsupernodo1"].ToString());
				if (dr["t313_idempresa"] != DBNull.Value)
					o.t313_idempresa = int.Parse(dr["t313_idempresa"].ToString());
				if (dr["t303_cierreECOestandar"] != DBNull.Value)
					o.t303_cierreECOestandar = (bool)dr["t303_cierreECOestandar"];
				if (dr["t303_ultcierreeco"] != DBNull.Value)
					o.t303_ultcierreeco = int.Parse(dr["t303_ultcierreeco"].ToString());
				if (dr["t303_estado"] != DBNull.Value)
					o.t303_estado = (bool)dr["t303_estado"];
				if (dr["t303_modelocostes"] != DBNull.Value)
					o.t303_modelocostes = (string)dr["t303_modelocostes"];
				if (dr["t303_modelotarifas"] != DBNull.Value)
					o.t303_modelotarifas = (string)dr["t303_modelotarifas"];
				if (dr["t303_generareplica"] != DBNull.Value)
					o.t303_generareplica = (bool)dr["t303_generareplica"];
				if (dr["t303_porctolerancia"] != DBNull.Value)
					o.t303_porctolerancia = byte.Parse(dr["t303_porctolerancia"].ToString());
				if (dr["t314_idusuario_responsable"] != DBNull.Value)
					o.t314_idusuario_responsable = int.Parse(dr["t314_idusuario_responsable"].ToString());
				if (dr["t303_orden"] != DBNull.Value)
					o.t303_orden = int.Parse(dr["t303_orden"].ToString());
				if (dr["t303_representativo"] != DBNull.Value)
					o.t303_representativo = (bool)dr["t303_representativo"];
				if (dr["t303_interfacehermes"] != DBNull.Value)
					o.t303_interfacehermes = (string)dr["t303_interfacehermes"];
				if (dr["t303_defectocalcularGF"] != DBNull.Value)
					o.t303_defectocalcularGF = (bool)dr["t303_defectocalcularGF"];
				if (dr["t303_defectomailiap"] != DBNull.Value)
					o.t303_defectomailiap = (bool)dr["t303_defectomailiap"];
				if (dr["t303_cierreIAPestandar"] != DBNull.Value)
					o.t303_cierreIAPestandar = (bool)dr["t303_cierreIAPestandar"];
				if (dr["t303_ultcierreIAP"] != DBNull.Value)
					o.t303_ultcierreIAP = int.Parse(dr["t303_ultcierreIAP"].ToString());
				if (dr["t303_compcontprod"] != DBNull.Value)
					o.t303_compcontprod = (bool)dr["t303_compcontprod"];
				if (dr["t303_defectoPIG"] != DBNull.Value)
					o.t303_defectoPIG = (bool)dr["t303_defectoPIG"];
				if (dr["t303_margencesionprof"] != DBNull.Value)
					o.t303_margencesionprof = float.Parse(dr["t303_margencesionprof"].ToString());
				if (dr["t303_interesGF"] != DBNull.Value)
					o.t303_interesGF = float.Parse(dr["t303_interesGF"].ToString());
				if (dr["t303_denominacion_CNP"] != DBNull.Value)
					o.t303_denominacion_CNP = (string)dr["t303_denominacion_CNP"];
				if (dr["t303_obligatorio_CNP"] != DBNull.Value)
					o.t303_obligatorio_CNP = (bool)dr["t303_obligatorio_CNP"];
				if (dr["t303_asignarperfiles"] != DBNull.Value)
					o.t303_asignarperfiles = (string)dr["t303_asignarperfiles"];
				if (dr["t303_desglose"] != DBNull.Value)
					o.t303_desglose = (bool)dr["t303_desglose"];
				if (dr["t303_pgrcg"] != DBNull.Value)
					o.t303_pgrcg = (bool)dr["t303_pgrcg"];
				if (dr["t303_controlhuecos"] != DBNull.Value)
					o.t303_controlhuecos = (bool)dr["t303_controlhuecos"];
				if (dr["t303_tipolinterna"] != DBNull.Value)
					o.t303_tipolinterna = (bool)dr["t303_tipolinterna"];
				if (dr["t303_tipolespecial"] != DBNull.Value)
					o.t303_tipolespecial = (bool)dr["t303_tipolespecial"];
				if (dr["t303_tipolproductivaSC"] != DBNull.Value)
					o.t303_tipolproductivaSC = (bool)dr["t303_tipolproductivaSC"];
                if (dr["t303_defectoadmiterecursospst"] != DBNull.Value)
                    o.t303_defectoadmiterecursospst = (bool)dr["t303_defectoadmiterecursospst"];
                if (dr["t621_idovsap"] != DBNull.Value)
                    o.t621_idovsap = (string)dr["t621_idovsap"];
                if (dr["t422_idmoneda"] != DBNull.Value)
                    o.t422_idmoneda = (string)dr["t422_idmoneda"];
            }
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de NODO"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		#endregion
	}
}
