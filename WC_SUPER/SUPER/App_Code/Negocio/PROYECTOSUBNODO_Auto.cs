using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : PROYECTOSUBNODO
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T305_PROYECTOSUBNODO
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	18/02/2010 16:06:36	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class PROYECTOSUBNODO
	{
		#region Propiedades y Atributos

		private int _t305_idproyectosubnodo;
		public int t305_idproyectosubnodo
		{
			get {return _t305_idproyectosubnodo;}
			set { _t305_idproyectosubnodo = value ;}
		}

		private int _t301_idproyecto;
		public int t301_idproyecto
		{
			get {return _t301_idproyecto;}
			set { _t301_idproyecto = value ;}
		}

		private int _t304_idsubnodo;
		public int t304_idsubnodo
		{
			get {return _t304_idsubnodo;}
			set { _t304_idsubnodo = value ;}
		}

		private bool _t305_finalizado;
		public bool t305_finalizado
		{
			get {return _t305_finalizado;}
			set { _t305_finalizado = value ;}
		}

		private string _t305_cualidad;
		public string t305_cualidad
		{
			get {return _t305_cualidad;}
			set { _t305_cualidad = value ;}
		}

		private bool _t305_heredanodo;
		public bool t305_heredanodo
		{
			get {return _t305_heredanodo;}
			set { _t305_heredanodo = value ;}
		}

		private int _t314_idusuario_responsable;
		public int t314_idusuario_responsable
		{
			get {return _t314_idusuario_responsable;}
			set { _t314_idusuario_responsable = value ;}
		}

		private string _t305_seudonimo;
		public string t305_seudonimo
		{
			get {return _t305_seudonimo;}
			set { _t305_seudonimo = value ;}
		}

		private string _t305_accesobitacora_iap;
		public string t305_accesobitacora_iap
		{
			get {return _t305_accesobitacora_iap;}
			set { _t305_accesobitacora_iap = value ;}
		}

		private string _t305_accesobitacora_pst;
		public string t305_accesobitacora_pst
		{
			get {return _t305_accesobitacora_pst;}
			set { _t305_accesobitacora_pst = value ;}
		}

		private bool _t305_imputablegasvi;
		public bool t305_imputablegasvi
		{
			get {return _t305_imputablegasvi;}
			set { _t305_imputablegasvi = value ;}
		}

		private bool _t305_admiterecursospst;
		public bool t305_admiterecursospst
		{
			get {return _t305_admiterecursospst;}
			set { _t305_admiterecursospst = value ;}
		}

		private bool _t305_avisoresponsablepst;
		public bool t305_avisoresponsablepst
		{
			get {return _t305_avisoresponsablepst;}
			set { _t305_avisoresponsablepst = value ;}
		}

		private bool _t305_avisorecursopst;
		public bool t305_avisorecursopst
		{
			get {return _t305_avisorecursopst;}
			set { _t305_avisorecursopst = value ;}
		}

		private bool _t305_avisofigura;
		public bool t305_avisofigura
		{
			get {return _t305_avisofigura;}
			set { _t305_avisofigura = value ;}
		}

		private string _t305_modificaciones;
		public string t305_modificaciones
		{
			get {return _t305_modificaciones;}
			set { _t305_modificaciones = value ;}
		}

		private string _t305_observaciones;
		public string t305_observaciones
		{
			get {return _t305_observaciones;}
			set { _t305_observaciones = value ;}
		}

		private string _t305_observacionesadm;
		public string t305_observacionesadm
		{
			get {return _t305_observacionesadm;}
			set { _t305_observacionesadm = value ;}
		}

		private int? _t476_idcnp;
		public int? t476_idcnp
		{
			get {return _t476_idcnp;}
			set { _t476_idcnp = value ;}
		}

		private int? _t485_idcsn1p;
		public int? t485_idcsn1p
		{
			get {return _t485_idcsn1p;}
			set { _t485_idcsn1p = value ;}
		}

		private int? _t487_idcsn2p;
		public int? t487_idcsn2p
		{
			get {return _t487_idcsn2p;}
			set { _t487_idcsn2p = value ;}
		}

		private int? _t489_idcsn3p;
		public int? t489_idcsn3p
		{
			get {return _t489_idcsn3p;}
			set { _t489_idcsn3p = value ;}
		}

		private int? _t491_idcsn4p;
		public int? t491_idcsn4p
		{
			get {return _t491_idcsn4p;}
			set { _t491_idcsn4p = value ;}
		}

		private int? _t001_ficepi_visador;
		public int? t001_ficepi_visador
		{
			get {return _t001_ficepi_visador;}
			set { _t001_ficepi_visador = value ;}
		}

        private int? _t001_idficepi_visadorcv;
        public int? t001_idficepi_visadorcv
		{
            get { return _t001_idficepi_visadorcv; }
            set { _t001_idficepi_visadorcv = value; }
		}

		private bool _t305_supervisor_visador;
		public bool t305_supervisor_visador
		{
			get {return _t305_supervisor_visador;}
			set { _t305_supervisor_visador = value ;}
		}

		private byte _t305_importaciongasvi;
		public byte t305_importaciongasvi
		{
			get {return _t305_importaciongasvi;}
			set { _t305_importaciongasvi = value ;}
		}

        private string _t422_idmoneda;
        public string t422_idmoneda
        {
            get { return _t422_idmoneda; }
            set { _t422_idmoneda = value; }
        }

        private bool _t305_opd;
        public bool t305_opd
        {
            get { return _t305_opd; }
            set { _t305_opd = value; }
        }

        private int? _t001_idficepi_interlocutor;
        public int? t001_idficepi_interlocutor
		{
            get { return _t001_idficepi_interlocutor; }
            set { _t001_idficepi_interlocutor = value; }
		}
        
        public int? t001_idficepi_interlocalertasocfa {get; set; }
        #endregion

        #region Constructor

        public PROYECTOSUBNODO() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T305_PROYECTOSUBNODO.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	18/02/2010 16:06:36
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Insert(SqlTransaction tr, int t301_idproyecto , int t304_idsubnodo , bool t305_finalizado , string t305_cualidad , bool t305_heredanodo , int t314_idusuario_responsable , string t305_seudonimo , string t305_accesobitacora_iap , string t305_accesobitacora_pst , bool t305_imputablegasvi , bool t305_admiterecursospst , bool t305_avisoresponsablepst , bool t305_avisorecursopst , bool t305_avisofigura , string t305_modificaciones , string t305_observaciones , string t305_observacionesadm , Nullable<int> t476_idcnp , Nullable<int> t485_idcsn1p , Nullable<int> t487_idcsn2p , Nullable<int> t489_idcsn3p , Nullable<int> t491_idcsn4p , Nullable<int> t001_ficepi_visador , bool t305_supervisor_visador , byte t305_importaciongasvi)
		{
			SqlParameter[] aParam = new SqlParameter[25];
			aParam[0] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
			aParam[0].Value = t301_idproyecto;
			aParam[1] = new SqlParameter("@t304_idsubnodo", SqlDbType.Int, 4);
			aParam[1].Value = t304_idsubnodo;
			aParam[2] = new SqlParameter("@t305_finalizado", SqlDbType.Bit, 1);
			aParam[2].Value = t305_finalizado;
			aParam[3] = new SqlParameter("@t305_cualidad", SqlDbType.Text, 1);
			aParam[3].Value = t305_cualidad;
			aParam[4] = new SqlParameter("@t305_heredanodo", SqlDbType.Bit, 1);
			aParam[4].Value = t305_heredanodo;
			aParam[5] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
			aParam[5].Value = t314_idusuario_responsable;
			aParam[6] = new SqlParameter("@t305_seudonimo", SqlDbType.Text, 70);
			aParam[6].Value = t305_seudonimo;
			aParam[7] = new SqlParameter("@t305_accesobitacora_iap", SqlDbType.Text, 1);
			aParam[7].Value = t305_accesobitacora_iap;
			aParam[8] = new SqlParameter("@t305_accesobitacora_pst", SqlDbType.Text, 1);
			aParam[8].Value = t305_accesobitacora_pst;
			aParam[9] = new SqlParameter("@t305_imputablegasvi", SqlDbType.Bit, 1);
			aParam[9].Value = t305_imputablegasvi;
			aParam[10] = new SqlParameter("@t305_admiterecursospst", SqlDbType.Bit, 1);
			aParam[10].Value = t305_admiterecursospst;
			aParam[11] = new SqlParameter("@t305_avisoresponsablepst", SqlDbType.Bit, 1);
			aParam[11].Value = t305_avisoresponsablepst;
			aParam[12] = new SqlParameter("@t305_avisorecursopst", SqlDbType.Bit, 1);
			aParam[12].Value = t305_avisorecursopst;
			aParam[13] = new SqlParameter("@t305_avisofigura", SqlDbType.Bit, 1);
			aParam[13].Value = t305_avisofigura;
			aParam[14] = new SqlParameter("@t305_modificaciones", SqlDbType.Text, 2147483647);
			aParam[14].Value = t305_modificaciones;
			aParam[15] = new SqlParameter("@t305_observaciones", SqlDbType.Text, 2147483647);
			aParam[15].Value = t305_observaciones;
			aParam[16] = new SqlParameter("@t305_observacionesadm", SqlDbType.Text, 2147483647);
			aParam[16].Value = t305_observacionesadm;
			aParam[17] = new SqlParameter("@t476_idcnp", SqlDbType.Int, 4);
			aParam[17].Value = t476_idcnp;
			aParam[18] = new SqlParameter("@t485_idcsn1p", SqlDbType.Int, 4);
			aParam[18].Value = t485_idcsn1p;
			aParam[19] = new SqlParameter("@t487_idcsn2p", SqlDbType.Int, 4);
			aParam[19].Value = t487_idcsn2p;
			aParam[20] = new SqlParameter("@t489_idcsn3p", SqlDbType.Int, 4);
			aParam[20].Value = t489_idcsn3p;
			aParam[21] = new SqlParameter("@t491_idcsn4p", SqlDbType.Int, 4);
			aParam[21].Value = t491_idcsn4p;
			aParam[22] = new SqlParameter("@t001_ficepi_visador", SqlDbType.Int, 4);
			aParam[22].Value = t001_ficepi_visador;
			aParam[23] = new SqlParameter("@t305_supervisor_visador", SqlDbType.Bit, 1);
			aParam[23].Value = t305_supervisor_visador;
			aParam[24] = new SqlParameter("@t305_importaciongasvi", SqlDbType.TinyInt, 1);
			aParam[24].Value = t305_importaciongasvi;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
				return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_PROYECTOSUBNODO_I", aParam));
			else
				return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_PROYECTOSUBNODO_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T305_PROYECTOSUBNODO.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	18/02/2010 16:06:36
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Update(SqlTransaction tr, int t305_idproyectosubnodo, int t301_idproyecto, int t304_idsubnodo, bool t305_finalizado, string t305_cualidad, bool t305_heredanodo, int t314_idusuario_responsable, string t305_seudonimo, string t305_accesobitacora_iap, string t305_accesobitacora_pst, bool t305_imputablegasvi, bool t305_admiterecursospst, bool t305_avisoresponsablepst, bool t305_avisorecursopst, bool t305_avisofigura, string t305_modificaciones, string t305_observaciones, string t305_observacionesadm, Nullable<int> t476_idcnp, Nullable<int> t485_idcsn1p, Nullable<int> t487_idcsn2p, Nullable<int> t489_idcsn3p, Nullable<int> t491_idcsn4p, Nullable<int> t001_ficepi_visador, bool t305_supervisor_visador, byte t305_importaciongasvi)
		{
			SqlParameter[] aParam = new SqlParameter[26];
			aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
			aParam[0].Value = t305_idproyectosubnodo;
			aParam[1] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
			aParam[1].Value = t301_idproyecto;
			aParam[2] = new SqlParameter("@t304_idsubnodo", SqlDbType.Int, 4);
			aParam[2].Value = t304_idsubnodo;
			aParam[3] = new SqlParameter("@t305_finalizado", SqlDbType.Bit, 1);
			aParam[3].Value = t305_finalizado;
			aParam[4] = new SqlParameter("@t305_cualidad", SqlDbType.Text, 1);
			aParam[4].Value = t305_cualidad;
			aParam[5] = new SqlParameter("@t305_heredanodo", SqlDbType.Bit, 1);
			aParam[5].Value = t305_heredanodo;
			aParam[6] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
			aParam[6].Value = t314_idusuario_responsable;
			aParam[7] = new SqlParameter("@t305_seudonimo", SqlDbType.Text, 70);
			aParam[7].Value = t305_seudonimo;
			aParam[8] = new SqlParameter("@t305_accesobitacora_iap", SqlDbType.Text, 1);
			aParam[8].Value = t305_accesobitacora_iap;
			aParam[9] = new SqlParameter("@t305_accesobitacora_pst", SqlDbType.Text, 1);
			aParam[9].Value = t305_accesobitacora_pst;
			aParam[10] = new SqlParameter("@t305_imputablegasvi", SqlDbType.Bit, 1);
			aParam[10].Value = t305_imputablegasvi;
			aParam[11] = new SqlParameter("@t305_admiterecursospst", SqlDbType.Bit, 1);
			aParam[11].Value = t305_admiterecursospst;
			aParam[12] = new SqlParameter("@t305_avisoresponsablepst", SqlDbType.Bit, 1);
			aParam[12].Value = t305_avisoresponsablepst;
			aParam[13] = new SqlParameter("@t305_avisorecursopst", SqlDbType.Bit, 1);
			aParam[13].Value = t305_avisorecursopst;
			aParam[14] = new SqlParameter("@t305_avisofigura", SqlDbType.Bit, 1);
			aParam[14].Value = t305_avisofigura;
			aParam[15] = new SqlParameter("@t305_modificaciones", SqlDbType.Text, 2147483647);
			aParam[15].Value = t305_modificaciones;
			aParam[16] = new SqlParameter("@t305_observaciones", SqlDbType.Text, 2147483647);
			aParam[16].Value = t305_observaciones;
			aParam[17] = new SqlParameter("@t305_observacionesadm", SqlDbType.Text, 2147483647);
			aParam[17].Value = t305_observacionesadm;
			aParam[18] = new SqlParameter("@t476_idcnp", SqlDbType.Int, 4);
			aParam[18].Value = t476_idcnp;
			aParam[19] = new SqlParameter("@t485_idcsn1p", SqlDbType.Int, 4);
			aParam[19].Value = t485_idcsn1p;
			aParam[20] = new SqlParameter("@t487_idcsn2p", SqlDbType.Int, 4);
			aParam[20].Value = t487_idcsn2p;
			aParam[21] = new SqlParameter("@t489_idcsn3p", SqlDbType.Int, 4);
			aParam[21].Value = t489_idcsn3p;
			aParam[22] = new SqlParameter("@t491_idcsn4p", SqlDbType.Int, 4);
			aParam[22].Value = t491_idcsn4p;
			aParam[23] = new SqlParameter("@t001_ficepi_visador", SqlDbType.Int, 4);
			aParam[23].Value = t001_ficepi_visador;
			aParam[24] = new SqlParameter("@t305_supervisor_visador", SqlDbType.Bit, 1);
			aParam[24].Value = t305_supervisor_visador;
			aParam[25] = new SqlParameter("@t305_importaciongasvi", SqlDbType.TinyInt, 1);
			aParam[25].Value = t305_importaciongasvi;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_PROYECTOSUBNODO_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PROYECTOSUBNODO_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T305_PROYECTOSUBNODO a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	18/02/2010 16:06:36
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t305_idproyectosubnodo)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
			aParam[0].Value = t305_idproyectosubnodo;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_PROYECTOSUBNODO_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PROYECTOSUBNODO_D", aParam);
		}

		#endregion
	}
}
