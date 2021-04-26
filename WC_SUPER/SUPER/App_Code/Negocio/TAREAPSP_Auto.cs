using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : TAREAPSP
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: t332_TAREAPSP
	/// </summary>
	/// <history>
	/// 	Creado por [DOARHUMI]	21/11/2007 12:15:32	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class TAREAPSP
	{
		#region Propiedades y Atributos

		private int _t332_idtarea;
		public int t332_idtarea
		{
			get {return _t332_idtarea;}
			set { _t332_idtarea = value ;}
		}

		private string _t332_destarea;
		public string t332_destarea
		{
			get {return _t332_destarea;}
			set { _t332_destarea = value ;}
		}

		private string _t332_destarealong;
		public string t332_destarealong
		{
			get {return _t332_destarealong;}
			set { _t332_destarealong = value ;}
		}

		private int _t331_idpt;
		public int t331_idpt
		{
			get {return _t331_idpt;}
			set { _t331_idpt = value ;}
		}

		private int _t335_idactividad;
		public int t335_idactividad
		{
			get {return _t335_idactividad;}
			set { _t335_idactividad = value ;}
		}

        private int _t314_idusuario_promotor;
        public int t314_idusuario_promotor
		{
            get { return _t314_idusuario_promotor; }
            set { _t314_idusuario_promotor = value; }
		}

        private int _t314_idusuario_ultmodif;
        public int t314_idusuario_ultmodif
		{
            get { return _t314_idusuario_ultmodif; }
            set { _t314_idusuario_ultmodif = value; }
		}

		private DateTime _t332_falta;
		public DateTime t332_falta
		{
			get {return _t332_falta;}
			set { _t332_falta = value ;}
		}

		private DateTime _t332_fultmodif;
		public DateTime t332_fultmodif
		{
			get {return _t332_fultmodif;}
			set { _t332_fultmodif = value ;}
		}

		private DateTime? _t332_fiv;
		public DateTime? t332_fiv
		{
			get {return _t332_fiv;}
			set { _t332_fiv = value ;}
		}

		private DateTime? _t332_ffv;
		public DateTime? t332_ffv
		{
			get {return _t332_ffv;}
			set { _t332_ffv = value ;}
		}

		private byte _t332_estado;
		public byte t332_estado
		{
			get {return _t332_estado;}
			set { _t332_estado = value ;}
		}

		private DateTime? _t332_fipl;
		public DateTime? t332_fipl
		{
			get {return _t332_fipl;}
			set { _t332_fipl = value ;}
		}

		private DateTime? _t332_ffpl;
		public DateTime? t332_ffpl
		{
			get {return _t332_ffpl;}
			set { _t332_ffpl = value ;}
		}

        private double _t332_etpl;
        public double t332_etpl
		{
			get {return _t332_etpl;}
			set { _t332_etpl = value ;}
		}

		private DateTime? _t332_ffpr;
		public DateTime? t332_ffpr
		{
			get {return _t332_ffpr;}
			set { _t332_ffpr = value ;}
		}

		private double _t332_etpr;
		public double t332_etpr
		{
			get {return _t332_etpr;}
			set { _t332_etpr = value ;}
		}

		private double _t332_cle;
		public double t332_cle
		{
			get {return _t332_cle;}
			set { _t332_cle = value ;}
		}

		private string _t332_tipocle;
		public string t332_tipocle
		{
			get {return _t332_tipocle;}
			set { _t332_tipocle = value ;}
		}

		private short _t332_orden;
		public short t332_orden
		{
			get {return _t332_orden;}
			set { _t332_orden = value ;}
		}

		private bool _t332_facturable;
		public bool t332_facturable
		{
			get {return _t332_facturable;}
			set { _t332_facturable = value ;}
		}

        private decimal _t332_presupuesto;
        public decimal t332_presupuesto
		{
			get {return _t332_presupuesto;}
			set { _t332_presupuesto = value ;}
		}

		private short _t353_idorigen;
		public short t353_idorigen
		{
			get {return _t353_idorigen;}
			set { _t353_idorigen = value ;}
		}

		private string _t332_otl;
		public string t332_otl
		{
			get {return _t332_otl;}
			set { _t332_otl = value ;}
		}

		private string _t332_incidencia;
		public string t332_incidencia
		{
			get {return _t332_incidencia;}
			set { _t332_incidencia = value ;}
		}

		private string _t332_observaciones;
		public string t332_observaciones
		{
			get {return _t332_observaciones;}
			set { _t332_observaciones = value ;}
		}

		private bool _t332_notificable;
		public bool t332_notificable
		{
			get {return _t332_notificable;}
			set { _t332_notificable = value ;}
		}

		private string _t332_notas1;
		public string t332_notas1
		{
			get {return _t332_notas1;}
			set { _t332_notas1 = value ;}
		}

		private string _t332_notas2;
		public string t332_notas2
		{
			get {return _t332_notas2;}
			set { _t332_notas2 = value ;}
		}

		private string _t332_notas3;
		public string t332_notas3
		{
			get {return _t332_notas3;}
			set { _t332_notas3 = value ;}
		}

		private string _t332_notas4;
		public string t332_notas4
		{
			get {return _t332_notas4;}
			set { _t332_notas4 = value ;}
		}

		private double _t332_avance;
		public double t332_avance
		{
			get {return _t332_avance;}
			set { _t332_avance = value ;}
		}

		private bool _t332_avanceauto;
		public bool t332_avanceauto
		{
			get {return _t332_avanceauto;}
			set { _t332_avanceauto = value ;}
		}

        private int _t314_idusuario_fin;
        public int t314_idusuario_fin
        {
            get { return _t314_idusuario_fin; }
            set { _t314_idusuario_fin = value; }
        }
        private DateTime? _t332_ffin;
        public DateTime? t332_ffin
        {
            get { return _t332_ffin; }
            set { _t332_ffin = value; }
        }

        private int _t314_idusuario_cierre;
        public int t314_idusuario_cierre
        {
            get { return _t314_idusuario_cierre; }
            set { _t314_idusuario_cierre = value; }
        }
        private DateTime? _t332_fcierre;
        public DateTime? t332_fcierre
        {
            get { return _t332_fcierre; }
            set { _t332_fcierre = value; }
        }

        private bool _t332_impiap;
        public bool t332_impiap
        {
            get { return _t332_impiap; }
            set { _t332_impiap = value; }
        }

        private bool _t332_notasiap;
        public bool t332_notasiap
        {
            get { return _t332_notasiap; }
            set { _t332_notasiap = value; }
        }

        private bool _t332_heredanodo;
        public bool t332_heredanodo
        {
            get { return _t332_heredanodo; }
            set { _t332_heredanodo = value; }
        }
        private bool _t332_heredaproyeco;
        public bool t332_heredaproyeco
        {
            get { return _t332_heredaproyeco; }
            set { _t332_heredaproyeco = value; }
        }

        private string _t332_mensaje;
        public string t332_mensaje
        {
            get { return _t332_mensaje; }
            set { _t332_mensaje = value; }
        }
        private bool _t332_notif_prof;
        public bool t332_notif_prof
        {
            get { return _t332_notif_prof; }
            set { _t332_notif_prof = value; }
        }
        private string _t332_acceso_bitacora_iap;
        public string t332_acceso_bitacora_iap
        {
            get { return _t332_acceso_bitacora_iap; }
            set { _t332_acceso_bitacora_iap = value; }
        }

        private int _t324_idmodofact;
        public int t324_idmodofact
        {
            get { return _t324_idmodofact; }
            set { _t324_idmodofact = value; }
        }
        
        #endregion

		#region Constructores

		public TAREAPSP() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla t332_TAREAPSP
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	21/11/2007 12:15:32
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, string t332_destarea, string t332_destarealong, Nullable<int> t331_idpt,
                                Nullable<int> t335_idactividad, int t314_idusuario_promotor, Nullable<int> t314_idusuario_ultmodif, DateTime t332_falta, 
                                Nullable<DateTime> t332_fultmodif, Nullable<DateTime> t332_fiv, Nullable<DateTime> t332_ffv, byte t332_estado,
                                Nullable<DateTime> t332_fipl, Nullable<DateTime> t332_ffpl, Nullable<double> t332_etpl, 
                                Nullable<DateTime> t332_ffpr, Nullable<double> t332_etpr, Nullable<int> t346_idpst, Nullable<double> t332_cle,
                                string t332_tipocle, short t332_orden, bool t332_facturable, decimal t332_presupuesto, 
                                Nullable<short> t353_idorigen, string t332_otl, string t332_incidencia, string t332_observaciones, 
                                bool t332_notificable, string t332_notas1, string t332_notas2, string t332_notas3, string t332_notas4, 
                                Nullable<double> t332_avance, bool t332_avanceauto, bool t332_impiap, bool t332_notasiap,
                                bool t332_heredanodo, bool t332_heredaproyeco, string t332_mensaje, bool t332_notif_prof, string t332_acceso_bitacora_iap,
                                Nullable<int> t324_idmodofact, bool bHorasComplementarias)
		{
			SqlParameter[] aParam = new SqlParameter[42];
			aParam[0] = new SqlParameter("@t332_destarea", SqlDbType.Text, 100);
			aParam[0].Value = t332_destarea;
			aParam[1] = new SqlParameter("@t332_destarealong", SqlDbType.Text, 2147483647);
			aParam[1].Value = t332_destarealong;
			aParam[2] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
			aParam[2].Value = t331_idpt;
			aParam[3] = new SqlParameter("@t335_idactividad", SqlDbType.Int, 4);
            if (t335_idactividad <= 0) t335_idactividad = null;
			aParam[3].Value = t335_idactividad;
            aParam[4] = new SqlParameter("@t314_idusuario_promotor", SqlDbType.Int, 4);
            aParam[4].Value = t314_idusuario_promotor;
            aParam[5] = new SqlParameter("@t314_idusuario_ultmodif", SqlDbType.Int, 4);
            aParam[5].Value = t314_idusuario_ultmodif;
            aParam[6] = new SqlParameter("@t332_falta", SqlDbType.SmallDateTime, 4);
			aParam[6].Value = t332_falta;
			aParam[7] = new SqlParameter("@t332_fultmodif", SqlDbType.SmallDateTime, 4);
			aParam[7].Value = t332_fultmodif;
			aParam[8] = new SqlParameter("@t332_fiv", SqlDbType.SmallDateTime, 4);
			aParam[8].Value = t332_fiv;
			aParam[9] = new SqlParameter("@t332_ffv", SqlDbType.SmallDateTime, 4);
			aParam[9].Value = t332_ffv;
			aParam[10] = new SqlParameter("@t332_estado", SqlDbType.TinyInt, 1);
			aParam[10].Value = t332_estado;
			aParam[11] = new SqlParameter("@t332_fipl", SqlDbType.SmallDateTime, 4);
			aParam[11].Value = t332_fipl;
			aParam[12] = new SqlParameter("@t332_ffpl", SqlDbType.SmallDateTime, 4);
			aParam[12].Value = t332_ffpl;
			aParam[13] = new SqlParameter("@t332_etpl", SqlDbType.Float, 8);
			aParam[13].Value = t332_etpl;
			aParam[14] = new SqlParameter("@t332_ffpr", SqlDbType.SmallDateTime, 4);
			aParam[14].Value = t332_ffpr;
			aParam[15] = new SqlParameter("@t332_etpr", SqlDbType.Float, 8);
			aParam[15].Value = t332_etpr;
			aParam[16] = new SqlParameter("@t346_idpst", SqlDbType.Int, 4);
			aParam[16].Value = t346_idpst;
			aParam[17] = new SqlParameter("@t332_cle", SqlDbType.Float, 8);
			aParam[17].Value = t332_cle;
			aParam[18] = new SqlParameter("@t332_tipocle", SqlDbType.Text, 1);
			aParam[18].Value = t332_tipocle;
			aParam[19] = new SqlParameter("@t332_orden", SqlDbType.SmallInt, 2);
			aParam[19].Value = t332_orden;
			aParam[20] = new SqlParameter("@t332_facturable", SqlDbType.Bit, 1);
			aParam[20].Value = t332_facturable;
			aParam[21] = new SqlParameter("@t332_presupuesto", SqlDbType.Money, 8);
			aParam[21].Value = t332_presupuesto;
			aParam[22] = new SqlParameter("@t353_idorigen", SqlDbType.SmallInt, 2);
			aParam[22].Value = t353_idorigen;
			aParam[23] = new SqlParameter("@t332_otl", SqlDbType.Text, 25);
			aParam[23].Value = t332_otl;
			aParam[24] = new SqlParameter("@t332_incidencia", SqlDbType.Text, 25);
			aParam[24].Value = t332_incidencia;
			aParam[25] = new SqlParameter("@t332_observaciones", SqlDbType.Text, 2147483647);
			aParam[25].Value = t332_observaciones;
			aParam[26] = new SqlParameter("@t332_notificable", SqlDbType.Bit, 1);
			aParam[26].Value = t332_notificable;
			aParam[27] = new SqlParameter("@t332_notas1", SqlDbType.Text, 2147483647);
			aParam[27].Value = t332_notas1;
			aParam[28] = new SqlParameter("@t332_notas2", SqlDbType.Text, 2147483647);
			aParam[28].Value = t332_notas2;
			aParam[29] = new SqlParameter("@t332_notas3", SqlDbType.Text, 2147483647);
			aParam[29].Value = t332_notas3;
			aParam[30] = new SqlParameter("@t332_notas4", SqlDbType.Text, 2147483647);
			aParam[30].Value = t332_notas4;
			aParam[31] = new SqlParameter("@t332_avance", SqlDbType.Float, 8);
			aParam[31].Value = t332_avance;
			aParam[32] = new SqlParameter("@t332_avanceauto", SqlDbType.Bit, 1);
			aParam[32].Value = t332_avanceauto;
            aParam[33] = new SqlParameter("@t332_impiap", SqlDbType.Bit, 1);
            aParam[33].Value = t332_impiap;
            aParam[34] = new SqlParameter("@t332_notasiap", SqlDbType.Bit, 1);
            aParam[34].Value = t332_notasiap;
            aParam[35] = new SqlParameter("@t332_heredanodo", SqlDbType.Bit, 1);
            aParam[35].Value = t332_heredanodo;
            aParam[36] = new SqlParameter("@t332_heredaproyeco", SqlDbType.Bit, 1);
            aParam[36].Value = t332_heredaproyeco;
            aParam[37] = new SqlParameter("@t332_mensaje", SqlDbType.Text, 2147483647);
            aParam[37].Value = t332_mensaje;
            aParam[38] = new SqlParameter("@t332_notif_prof", SqlDbType.Bit, 1);
            aParam[38].Value = t332_notif_prof;
            aParam[39] = new SqlParameter("@t332_acceso_iap", SqlDbType.Text, 1);
            aParam[39].Value = t332_acceso_bitacora_iap;
            aParam[40] = new SqlParameter("@t324_idmodofact", SqlDbType.Int, 4);
            aParam[40].Value = t324_idmodofact;
            aParam[41] = new SqlParameter("@t332_horascomplementarias", SqlDbType.Bit, 1);
            aParam[41].Value = bHorasComplementarias;

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_TAREAPSP_I", aParam));
			else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_TAREAPSP_I", aParam));

        }
/*
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla t332_TAREAPSP
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	21/11/2007 12:15:32
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t332_idtarea, string t332_destarea, string t332_destarealong, Nullable<int> t331_idpt,
                                Nullable<int> t335_idactividad, int t314_idusuario_promotor, Nullable<int> t314_idusuario_ultmodif, DateTime t332_falta, 
                                Nullable<DateTime> t332_fultmodif, Nullable<DateTime> t332_fiv, Nullable<DateTime> t332_ffv, byte t332_estado, 
                                Nullable<DateTime> t332_fipl, Nullable<DateTime> t332_ffpl, Nullable<double> t332_etpl, 
                                Nullable<DateTime> t332_ffpr, Nullable<double> t332_etpr, Nullable<int> t346_idpst, Nullable<double> t332_cle,
                                string t332_tipocle, short t332_orden, bool t332_facturable, decimal t332_presupuesto, 
                                Nullable<short> t353_idorigen, string t332_otl, string t332_incidencia, string t332_observaciones, 
                                bool t332_notificable, string t332_notas1, string t332_notas2, string t332_notas3, string t332_notas4,
                                Nullable<double> t332_avance, bool t332_avanceauto, Nullable<int> t314_idusuario_fin,
                                Nullable<DateTime> t332_ffin, Nullable<int> t314_idusuario_cierre, Nullable<DateTime> t332_fcierre, bool t332_impiap,
                                bool t332_notasiap, bool t332_heredanodo, bool t332_heredaproyeco, string t332_mensaje, string t332_acceso_bitacora_iap,
                                Nullable<int> t324_idmodofact)
		{
			SqlParameter[] aParam = new SqlParameter[45];
			aParam[0] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
			aParam[0].Value = t332_idtarea;
			aParam[1] = new SqlParameter("@t332_destarea", SqlDbType.Text, 50);
			aParam[1].Value = t332_destarea;
			aParam[2] = new SqlParameter("@t332_destarealong", SqlDbType.Text, 2147483647);
			aParam[2].Value = t332_destarealong;
			aParam[3] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
			aParam[3].Value = t331_idpt;
			aParam[4] = new SqlParameter("@t335_idactividad", SqlDbType.Int, 4);
            //if (t335_idactividad <= 0) t335_idactividad = null;
			aParam[4].Value = t335_idactividad;
            aParam[5] = new SqlParameter("@t314_idusuario_promotor", SqlDbType.Int, 4);
            aParam[5].Value = t314_idusuario_promotor;
            aParam[6] = new SqlParameter("@t314_idusuario_ultmodif", SqlDbType.Int, 4);
            aParam[6].Value = t314_idusuario_ultmodif;
			aParam[7] = new SqlParameter("@t332_falta", SqlDbType.SmallDateTime, 4);
			aParam[7].Value = t332_falta;
			aParam[8] = new SqlParameter("@t332_fultmodif", SqlDbType.SmallDateTime, 4);
			aParam[8].Value = t332_fultmodif;
			aParam[9] = new SqlParameter("@t332_fiv", SqlDbType.SmallDateTime, 4);
			aParam[9].Value = t332_fiv;
			aParam[10] = new SqlParameter("@t332_ffv", SqlDbType.SmallDateTime, 4);
			aParam[10].Value = t332_ffv;
			aParam[11] = new SqlParameter("@t332_estado", SqlDbType.TinyInt, 1);
			aParam[11].Value = t332_estado;
			aParam[12] = new SqlParameter("@t332_fipl", SqlDbType.SmallDateTime, 4);
			aParam[12].Value = t332_fipl;
			aParam[13] = new SqlParameter("@t332_ffpl", SqlDbType.SmallDateTime, 4);
			aParam[13].Value = t332_ffpl;
			aParam[14] = new SqlParameter("@t332_etpl", SqlDbType.Float, 8);
			aParam[14].Value = t332_etpl;
			aParam[15] = new SqlParameter("@t332_ffpr", SqlDbType.SmallDateTime, 4);
			aParam[15].Value = t332_ffpr;
			aParam[16] = new SqlParameter("@t332_etpr", SqlDbType.Float, 8);
			aParam[16].Value = t332_etpr;
			aParam[17] = new SqlParameter("@t346_idpst", SqlDbType.Int, 4);
			aParam[17].Value = t346_idpst;
			aParam[18] = new SqlParameter("@t332_cle", SqlDbType.Float, 8);
			aParam[18].Value = t332_cle;
			aParam[19] = new SqlParameter("@t332_tipocle", SqlDbType.Text, 1);
			aParam[19].Value = t332_tipocle;
			aParam[20] = new SqlParameter("@t332_orden", SqlDbType.SmallInt, 2);
			aParam[20].Value = t332_orden;
			aParam[21] = new SqlParameter("@t332_facturable", SqlDbType.Bit, 1);
			aParam[21].Value = t332_facturable;
			aParam[22] = new SqlParameter("@t332_presupuesto", SqlDbType.Money, 8);
			aParam[22].Value = t332_presupuesto;
			aParam[23] = new SqlParameter("@t353_idorigen", SqlDbType.SmallInt, 2);
			aParam[23].Value = t353_idorigen;
			aParam[24] = new SqlParameter("@t332_otl", SqlDbType.Text, 25);
			aParam[24].Value = t332_otl;
			aParam[25] = new SqlParameter("@t332_incidencia", SqlDbType.Text, 25);
			aParam[25].Value = t332_incidencia;
			aParam[26] = new SqlParameter("@t332_observaciones", SqlDbType.Text, 2147483647);
			aParam[26].Value = t332_observaciones;
			aParam[27] = new SqlParameter("@t332_notificable", SqlDbType.Bit, 1);
			aParam[27].Value = t332_notificable;
			aParam[28] = new SqlParameter("@t332_notas1", SqlDbType.Text, 2147483647);
			aParam[28].Value = t332_notas1;
			aParam[29] = new SqlParameter("@t332_notas2", SqlDbType.Text, 2147483647);
			aParam[29].Value = t332_notas2;
			aParam[30] = new SqlParameter("@t332_notas3", SqlDbType.Text, 2147483647);
			aParam[30].Value = t332_notas3;
			aParam[31] = new SqlParameter("@t332_notas4", SqlDbType.Text, 2147483647);
			aParam[31].Value = t332_notas4;
			aParam[32] = new SqlParameter("@t332_avance", SqlDbType.Float, 8);
			aParam[32].Value = t332_avance;
			aParam[33] = new SqlParameter("@t332_avanceauto", SqlDbType.Bit, 1);
			aParam[33].Value = t332_avanceauto;
            aParam[34] = new SqlParameter("@t314_idusuario_fin", SqlDbType.Int, 4);
            aParam[34].Value = t314_idusuario_fin;
            aParam[35] = new SqlParameter("@t332_ffin", SqlDbType.SmallDateTime, 4);
            aParam[35].Value = t332_ffin;
            aParam[36] = new SqlParameter("@t314_idusuario_cierre", SqlDbType.Int, 4);
            aParam[36].Value = t314_idusuario_cierre;
            aParam[37] = new SqlParameter("@t332_fcierre", SqlDbType.SmallDateTime, 4);
            aParam[37].Value = t332_fcierre;
            aParam[38] = new SqlParameter("@t332_impiap", SqlDbType.Bit, 1);
            aParam[38].Value = t332_impiap;
            aParam[39] = new SqlParameter("@t332_notasiap", SqlDbType.Bit, 1);
            aParam[39].Value = t332_notasiap;
            aParam[40] = new SqlParameter("@t332_heredanodo", SqlDbType.Bit, 1);
            aParam[40].Value = t332_heredanodo;
            aParam[41] = new SqlParameter("@t332_heredaproyeco", SqlDbType.Bit, 1);
            aParam[41].Value = t332_heredaproyeco;
            aParam[42] = new SqlParameter("@t332_mensaje", SqlDbType.Text, 2147483647);
            aParam[42].Value = t332_mensaje;
            aParam[43] = new SqlParameter("@t332_acceso_iap", SqlDbType.Text, 1);
            aParam[43].Value = t332_acceso_bitacora_iap;
            aParam[44] = new SqlParameter("@t324_idmodofact", SqlDbType.Int, 4);
            aParam[44].Value = t324_idmodofact;
            
			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_TAREAPSP_U", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_TAREAPSP_U", aParam);
		}
*/
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla t332_TAREAPSP a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	21/11/2007 12:15:32
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t332_idtarea)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
			aParam[0].Value = t332_idtarea;

			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_TAREAPSP_D", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_TAREAPSP_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla t332_TAREAPSP,
		/// y devuelve una instancia u objeto del tipo TAREAPSP
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	21/11/2007 12:15:32
		/// </history>
		/// -----------------------------------------------------------------------------
		public static TAREAPSP Select(SqlTransaction tr, int t332_idtarea) 
		{
			TAREAPSP o = new TAREAPSP();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
			aParam[0].Value = t332_idtarea;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_TAREAPSP_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_TAREAPSP_S", aParam);

			if (dr.Read())
			{
				if (dr["t332_idtarea"] != DBNull.Value)
					o.t332_idtarea = (int)dr["t332_idtarea"];
				if (dr["t332_destarea"] != DBNull.Value)
					o.t332_destarea = (string)dr["t332_destarea"];
				if (dr["t332_destarealong"] != DBNull.Value)
					o.t332_destarealong = (string)dr["t332_destarealong"];
                if (dr["t331_idpt"] != DBNull.Value)
                    o.t331_idpt = (int)dr["t331_idpt"];
                if (dr["t334_idfase"] != DBNull.Value)
                    o.t334_idfase = (int)dr["t334_idfase"];
                if (dr["t335_idactividad"] != DBNull.Value)
					o.t335_idactividad = (int)dr["t335_idactividad"];
                if (dr["t314_idusuario_promotor"] != DBNull.Value)
                    o.t314_idusuario_promotor = (int)dr["t314_idusuario_promotor"];
                if (dr["t314_idusuario_ultmodif"] != DBNull.Value)
                    o.t314_idusuario_ultmodif = (int)dr["t314_idusuario_ultmodif"];
				if (dr["t332_falta"] != DBNull.Value)
					o.t332_falta = (DateTime)dr["t332_falta"];
				if (dr["t332_fultmodif"] != DBNull.Value)
					o.t332_fultmodif = (DateTime)dr["t332_fultmodif"];
				if (dr["t332_fiv"] != DBNull.Value)
					o.t332_fiv = (DateTime)dr["t332_fiv"];
				if (dr["t332_ffv"] != DBNull.Value)
					o.t332_ffv = (DateTime)dr["t332_ffv"];
				if (dr["t332_estado"] != DBNull.Value)
					o.t332_estado = byte.Parse(dr["t332_estado"].ToString());
				if (dr["t332_fipl"] != DBNull.Value)
					o.t332_fipl = (DateTime)dr["t332_fipl"];
				if (dr["t332_ffpl"] != DBNull.Value)
					o.t332_ffpl = (DateTime)dr["t332_ffpl"];
				if (dr["t332_etpl"] != DBNull.Value)
					o.t332_etpl = double.Parse(dr["t332_etpl"].ToString());
				if (dr["t332_ffpr"] != DBNull.Value)
					o.t332_ffpr = (DateTime)dr["t332_ffpr"];
				if (dr["t332_etpr"] != DBNull.Value)
					o.t332_etpr = (double)dr["t332_etpr"];
				if (dr["t346_idpst"] != DBNull.Value)
					o.t346_idpst = (int)dr["t346_idpst"];
				if (dr["t332_cle"] != DBNull.Value)
                    o.t332_cle = double.Parse(dr["t332_cle"].ToString());
				if (dr["t332_tipocle"] != DBNull.Value)
					o.t332_tipocle = (string)dr["t332_tipocle"];
				if (dr["t332_orden"] != DBNull.Value)
                    o.t332_orden = short.Parse(dr["t332_orden"].ToString());
				if (dr["t332_facturable"] != DBNull.Value)
					o.t332_facturable = (bool)dr["t332_facturable"];
				if (dr["t332_presupuesto"] != DBNull.Value)
                    o.t332_presupuesto = decimal.Parse(dr["t332_presupuesto"].ToString());
				if (dr["t353_idorigen"] != DBNull.Value)
					o.t353_idorigen = short.Parse(dr["t353_idorigen"].ToString());
				if (dr["t332_otl"] != DBNull.Value)
					o.t332_otl = (string)dr["t332_otl"];
				if (dr["t332_incidencia"] != DBNull.Value)
					o.t332_incidencia = (string)dr["t332_incidencia"];
				if (dr["t332_observaciones"] != DBNull.Value)
					o.t332_observaciones = (string)dr["t332_observaciones"];
				if (dr["t332_notificable"] != DBNull.Value)
					o.t332_notificable = (bool)dr["t332_notificable"];
				if (dr["t332_notas1"] != DBNull.Value)
					o.t332_notas1 = (string)dr["t332_notas1"];
				if (dr["t332_notas2"] != DBNull.Value)
					o.t332_notas2 = (string)dr["t332_notas2"];
				if (dr["t332_notas3"] != DBNull.Value)
					o.t332_notas3 = (string)dr["t332_notas3"];
				if (dr["t332_notas4"] != DBNull.Value)
					o.t332_notas4 = (string)dr["t332_notas4"];
				if (dr["t332_avance"] != DBNull.Value)
					o.t332_avance = (double)dr["t332_avance"];
				if (dr["t332_avanceauto"] != DBNull.Value)
					o.t332_avanceauto = (bool)dr["t332_avanceauto"];
                if (dr["t314_idusuario_fin"] != DBNull.Value)
                    o.t314_idusuario_fin = (int)dr["t314_idusuario_fin"];
                if (dr["t332_ffin"] != DBNull.Value)
                    o.t332_ffin = (DateTime)dr["t332_ffin"];
                if (dr["t314_idusuario_cierre"] != DBNull.Value)
                    o.t314_idusuario_cierre = (int)dr["t314_idusuario_cierre"];
                if (dr["t332_fcierre"] != DBNull.Value)
                    o.t332_fcierre = (DateTime)dr["t332_fcierre"];
                if (dr["t332_impiap"] != DBNull.Value)
                    o.t332_impiap = (bool)dr["t332_impiap"];
                if (dr["t332_notasiap"] != DBNull.Value)
                    o.t332_notasiap = (bool)dr["t332_notasiap"];
                if (dr["t332_heredanodo"] != DBNull.Value)
                    o.t332_heredanodo = (bool)dr["t332_heredanodo"];
                if (dr["t332_heredaproyeco"] != DBNull.Value)
                    o.t332_heredaproyeco = (bool)dr["t332_heredaproyeco"];
                if (dr["t332_mensaje"] != DBNull.Value)
                    o.t332_mensaje = (string)dr["t332_mensaje"];
                if (dr["t332_acceso_iap"] != DBNull.Value)
                    o.t332_acceso_bitacora_iap = (string)dr["t332_acceso_iap"];
                if (dr["t324_idmodofact"] != DBNull.Value)
                    o.t324_idmodofact = (int)dr["t324_idmodofact"];
            }
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de TAREAPSP"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Selecciona los registros de la tabla t332_TAREAPSP en función de una foreign key.
		/// </summary>
		/// <returns>DataSet</returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	21/11/2007 12:15:32
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader SelectByt335_idactividad(SqlTransaction tr, Nullable<int> t335_idactividad) 
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t335_idactividad", SqlDbType.Int, 4);
			aParam[0].Value = t335_idactividad;

			if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_TAREAPSP_SByt335_idactividad", aParam);
			else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_TAREAPSP_SByt335_idactividad", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Selecciona los registros de la tabla t332_TAREAPSP en función de una foreign key.
		/// </summary>
		/// <returns>DataSet</returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	21/11/2007 12:15:32
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader SelectByt331_idpt(SqlTransaction tr, Nullable<int> t331_idpt) 
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
			aParam[0].Value = t331_idpt;

			if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_TAREAPSP_SByt331_idpt", aParam);
			else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_TAREAPSP_SByt331_idpt", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Borra los registros de la tabla t332_TAREAPSP en función de una foreign key.
		/// </summary>
		/// <remarks>
		/// 	Creado por [DOARHUMI]	21/11/2007 12:15:32
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void DeleteByt335_idactividad(SqlTransaction tr, Nullable<int> t335_idactividad)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t335_idactividad", SqlDbType.Int, 4);
			aParam[0].Value = t335_idactividad;

			if (tr == null)
				SqlHelper.ExecuteNonQuery("SUP_TAREAPSP_DByt335_idactividad", aParam);
			else
				SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_TAREAPSP_DByt335_idactividad", aParam);
		}

		#endregion
	}
}
