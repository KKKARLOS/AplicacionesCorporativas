using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : CLASEECO
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T329_CLASEECO
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	28/10/2009 8:42:07	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class CLASEECO
	{
		#region Propiedades y Atributos

		private int _t329_idclaseeco;
		public int t329_idclaseeco
		{
			get {return _t329_idclaseeco;}
			set { _t329_idclaseeco = value ;}
		}

		private string _t329_denominacion;
		public string t329_denominacion
		{
			get {return _t329_denominacion;}
			set { _t329_denominacion = value ;}
		}

		private bool _t329_estado;
		public bool t329_estado
		{
			get {return _t329_estado;}
			set { _t329_estado = value ;}
		}

		private bool _t329_presentablesoloAdm;
		public bool t329_presentablesoloAdm
		{
			get {return _t329_presentablesoloAdm;}
			set { _t329_presentablesoloAdm = value ;}
		}

		private string _t329_necesidad;
		public string t329_necesidad
		{
			get {return _t329_necesidad;}
			set { _t329_necesidad = value ;}
		}

		private short _t329_orden;
		public short t329_orden
		{
			get {return _t329_orden;}
			set { _t329_orden = value ;}
		}

		private bool _t329_disparareplica;
		public bool t329_disparareplica
		{
			get {return _t329_disparareplica;}
			set { _t329_disparareplica = value ;}
		}

		private byte _t328_idconceptoeco;
		public byte t328_idconceptoeco
		{
			get {return _t328_idconceptoeco;}
			set { _t328_idconceptoeco = value ;}
		}

		private bool _t329_noborrable;
		public bool t329_noborrable
		{
			get {return _t329_noborrable;}
			set { _t329_noborrable = value ;}
		}

		private string _t329_decalajeborrado;
        public string t329_decalajeborrado
		{
			get {return _t329_decalajeborrado;}
			set { _t329_decalajeborrado = value ;}
		}

		private bool _t329_calculoGF;
		public bool t329_calculoGF
		{
			get {return _t329_calculoGF;}
			set { _t329_calculoGF = value ;}
		}

		private bool _t329_visiblecarruselC;
		public bool t329_visiblecarruselC
		{
			get {return _t329_visiblecarruselC;}
			set { _t329_visiblecarruselC = value ;}
		}

		private bool _t329_visiblecarruselJ;
		public bool t329_visiblecarruselJ
		{
			get {return _t329_visiblecarruselJ;}
			set { _t329_visiblecarruselJ = value ;}
		}

        private bool _t329_visiblecarruselP;
        public bool t329_visiblecarruselP
        {
            get { return _t329_visiblecarruselP; }
            set { _t329_visiblecarruselP = value; }
        }
        private bool _t329_clonable;
        public bool t329_clonable
        {
            get { return _t329_clonable; }
            set { _t329_clonable = value; }
        }
        //private bool _t329_factura;
        //public bool t329_factura
        //{
        //    get { return _t329_factura; }
        //    set { _t329_factura = value; }
        //}
        #endregion

		#region Constructor

		public CLASEECO() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T329_CLASEECO.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	28/10/2009 8:42:07
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, string t329_denominacion, bool t329_estado, bool t329_presentablesoloAdm, 
                                string t329_necesidad, short t329_orden, bool t329_disparareplica, byte t328_idconceptoeco, bool t329_noborrable, 
                                string t329_decalajeborrado, bool t329_calculoGF, bool t329_visiblecarruselC, bool t329_visiblecarruselJ,
                                bool t329_visiblecarruselP, bool t329_clonable)//, bool t329_factura
		{
			SqlParameter[] aParam = new SqlParameter[14];
			aParam[0] = new SqlParameter("@t329_denominacion", SqlDbType.Text, 50);
			aParam[0].Value = t329_denominacion;
			aParam[1] = new SqlParameter("@t329_estado", SqlDbType.Bit, 1);
			aParam[1].Value = t329_estado;
			aParam[2] = new SqlParameter("@t329_presentablesoloAdm", SqlDbType.Bit, 1);
			aParam[2].Value = t329_presentablesoloAdm;
			aParam[3] = new SqlParameter("@t329_necesidad", SqlDbType.Char, 1);
			aParam[3].Value = t329_necesidad;
			aParam[4] = new SqlParameter("@t329_orden", SqlDbType.SmallInt, 2);
			aParam[4].Value = t329_orden;
			aParam[5] = new SqlParameter("@t329_disparareplica", SqlDbType.Bit, 1);
			aParam[5].Value = t329_disparareplica;
			aParam[6] = new SqlParameter("@t328_idconceptoeco", SqlDbType.TinyInt, 1);
			aParam[6].Value = t328_idconceptoeco;
			aParam[7] = new SqlParameter("@t329_noborrable", SqlDbType.Bit, 1);
			aParam[7].Value = t329_noborrable;
			aParam[8] = new SqlParameter("@t329_decalajeborrado", SqlDbType.Char, 1);
			aParam[8].Value = t329_decalajeborrado;
			aParam[9] = new SqlParameter("@t329_calculoGF", SqlDbType.Bit, 1);
			aParam[9].Value = t329_calculoGF;
			aParam[10] = new SqlParameter("@t329_visiblecarruselC", SqlDbType.Bit, 1);
			aParam[10].Value = t329_visiblecarruselC;
			aParam[11] = new SqlParameter("@t329_visiblecarruselJ", SqlDbType.Bit, 1);
			aParam[11].Value = t329_visiblecarruselJ;
			aParam[12] = new SqlParameter("@t329_visiblecarruselP", SqlDbType.Bit, 1);
			aParam[12].Value = t329_visiblecarruselP;
            aParam[13] = new SqlParameter("@t329_clonable", SqlDbType.Bit, 1);
            aParam[13].Value = t329_clonable;
            //aParam[14] = new SqlParameter("@t329_factura", SqlDbType.Bit, 1);
            //aParam[14].Value = t329_factura;
            
			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
				return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_CLASEECO_I", aParam));
			else
				return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CLASEECO_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T329_CLASEECO.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	28/10/2009 8:42:07
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t329_idclaseeco, string t329_denominacion, bool t329_estado, bool t329_presentablesoloAdm, 
                                string t329_necesidad, short t329_orden, bool t329_disparareplica, byte t328_idconceptoeco, bool t329_noborrable, 
                                string t329_decalajeborrado, bool t329_calculoGF, bool t329_visiblecarruselC, bool t329_visiblecarruselJ,
                                bool t329_visiblecarruselP, bool t329_clonable)//, bool t329_factura
		{
			SqlParameter[] aParam = new SqlParameter[15];
			aParam[0] = new SqlParameter("@t329_idclaseeco", SqlDbType.Int, 4);
			aParam[0].Value = t329_idclaseeco;
			aParam[1] = new SqlParameter("@t329_denominacion", SqlDbType.Text, 50);
			aParam[1].Value = t329_denominacion;
			aParam[2] = new SqlParameter("@t329_estado", SqlDbType.Bit, 1);
			aParam[2].Value = t329_estado;
			aParam[3] = new SqlParameter("@t329_presentablesoloAdm", SqlDbType.Bit, 1);
			aParam[3].Value = t329_presentablesoloAdm;
			aParam[4] = new SqlParameter("@t329_necesidad", SqlDbType.Char, 1);
			aParam[4].Value = t329_necesidad;
			aParam[5] = new SqlParameter("@t329_orden", SqlDbType.SmallInt, 2);
			aParam[5].Value = t329_orden;
			aParam[6] = new SqlParameter("@t329_disparareplica", SqlDbType.Bit, 1);
			aParam[6].Value = t329_disparareplica;
			aParam[7] = new SqlParameter("@t328_idconceptoeco", SqlDbType.TinyInt, 1);
			aParam[7].Value = t328_idconceptoeco;
			aParam[8] = new SqlParameter("@t329_noborrable", SqlDbType.Bit, 1);
			aParam[8].Value = t329_noborrable;
			aParam[9] = new SqlParameter("@t329_decalajeborrado", SqlDbType.Char, 1);
			aParam[9].Value = t329_decalajeborrado;
			aParam[10] = new SqlParameter("@t329_calculoGF", SqlDbType.Bit, 1);
			aParam[10].Value = t329_calculoGF;
			aParam[11] = new SqlParameter("@t329_visiblecarruselC", SqlDbType.Bit, 1);
			aParam[11].Value = t329_visiblecarruselC;
			aParam[12] = new SqlParameter("@t329_visiblecarruselJ", SqlDbType.Bit, 1);
			aParam[12].Value = t329_visiblecarruselJ;
			aParam[13] = new SqlParameter("@t329_visiblecarruselP", SqlDbType.Bit, 1);
			aParam[13].Value = t329_visiblecarruselP;
            aParam[14] = new SqlParameter("@t329_clonable", SqlDbType.Bit, 1);
            aParam[14].Value = t329_clonable;
            //aParam[15] = new SqlParameter("@t329_factura", SqlDbType.Bit, 1);
            //aParam[15].Value = t329_factura;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_CLASEECO_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CLASEECO_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T329_CLASEECO a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	28/10/2009 8:42:07
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t329_idclaseeco)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t329_idclaseeco", SqlDbType.Int, 4);
			aParam[0].Value = t329_idclaseeco;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_CLASEECO_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CLASEECO_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Selecciona los registros de la tabla T329_CLASEECO en función de una foreign key.
		/// </summary>
		/// <returns>DataSet</returns>
		/// <history>
		/// 	Creado por [sqladmin]	28/10/2009 8:42:07
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader SelectByT328_idconceptoeco(SqlTransaction tr, byte t328_idconceptoeco) 
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t328_idconceptoeco", SqlDbType.TinyInt, 1);
			aParam[0].Value = t328_idconceptoeco;


			if (tr == null)
				return SqlHelper.ExecuteSqlDataReader("SUP_CLASEECO_SByT328_idconceptoeco", aParam);
			else
				return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CLASEECO_SByT328_idconceptoeco", aParam);
		}

		#endregion
	}
}
