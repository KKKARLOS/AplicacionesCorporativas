using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : CONTRATO
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T306_CONTRATO
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	26/11/2009 10:28:38	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class CONTRATO
	{
		#region Propiedades y Atributos

		private int _t306_idcontrato;
		public int t306_idcontrato
		{
			get {return _t306_idcontrato;}
			set { _t306_idcontrato = value ;}
		}

		private int _t303_idnodo;
		public int t303_idnodo
		{
			get {return _t303_idnodo;}
			set { _t303_idnodo = value ;}
		}

		private int _t302_idcliente_contrato;
		public int t302_idcliente_contrato
		{
			get {return _t302_idcliente_contrato;}
			set { _t302_idcliente_contrato = value ;}
		}

		private int _t314_idusuario_responsable;
		public int t314_idusuario_responsable
		{
			get {return _t314_idusuario_responsable;}
			set { _t314_idusuario_responsable = value ;}
		}

		private int _t314_idusuario_gestorprod;
		public int t314_idusuario_gestorprod
		{
			get {return _t314_idusuario_gestorprod;}
			set { _t314_idusuario_gestorprod = value ;}
		}

        private string _Contrato;
        public string Contrato
        {
            get { return _Contrato; }
            set { _Contrato = value; }
        }

        private string _Cliente;
        public string Cliente
        {
            get { return _Cliente; }
            set { _Cliente = value; }
        }

        private string _Responsable;
        public string Responsable
        {
            get { return _Responsable; }
            set { _Responsable = value; }
        }

        private string _GestorProdu;
        public string GestorProdu
        {
            get { return _GestorProdu; }
            set { _GestorProdu = value; }
        }

        private string _Nodo;
        public string Nodo
        {
            get { return _Nodo; }
            set { _Nodo = value; }
        }

        private bool _t306_visionreplicas;
        public bool t306_visionreplicas
        {
            get { return _t306_visionreplicas; }
            set { _t306_visionreplicas = value; }
        }

        private int _t314_idusuario_ComercialHERMES;
        public int t314_idusuario_ComercialHERMES
        {
            get { return _t314_idusuario_ComercialHERMES; }
            set { _t314_idusuario_ComercialHERMES = value; }
        }

        private string _ComercialHERMES;
        public string ComercialHERMES
        {
            get { return _ComercialHERMES; }
            set { _ComercialHERMES = value; }
        }
        //Organización comercial
        public int ta212_idorganizacioncomercial { get; set; }
        public string ta212_codigoexterno { get; set; }
        public string ta212_denominacion { get; set; }
        //Nueva Línea de Oferta
        public int t195_idlineaoferta { get; set; }
        public int t195_codigoexterno { get; set; }
        public string t195_denominacion { get; set; }
        #endregion

        #region Constructor

        public CONTRATO() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T306_CONTRATO.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	26/11/2009 10:28:38
		/// </history>
		/// -----------------------------------------------------------------------------
        public static void Insert(SqlTransaction tr, int t306_idcontrato, int t303_idnodo, int t302_idcliente_contrato, int t314_idusuario_responsable, int t314_idusuario_gestorprod, bool t306_visionreplicas)
		{
			SqlParameter[] aParam = new SqlParameter[6];
			aParam[0] = new SqlParameter("@t306_idcontrato", SqlDbType.Int, 4);
			aParam[0].Value = t306_idcontrato;
			aParam[1] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
			aParam[1].Value = t303_idnodo;
			aParam[2] = new SqlParameter("@t302_idcliente_contrato", SqlDbType.Int, 4);
			aParam[2].Value = t302_idcliente_contrato;
			aParam[3] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
			aParam[3].Value = t314_idusuario_responsable;
			aParam[4] = new SqlParameter("@t314_idusuario_gestorprod", SqlDbType.Int, 4);
			aParam[4].Value = t314_idusuario_gestorprod;
            aParam[5] = new SqlParameter("@t306_visionreplicas", SqlDbType.Bit, 1);
            aParam[5].Value = t306_visionreplicas;
            
			if (tr == null)
				 SqlHelper.ExecuteNonQuery("SUP_CONTRATO_I", aParam);
			else
				 SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CONTRATO_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T306_CONTRATO.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	26/11/2009 10:28:38
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t306_idcontrato, int t303_idnodo, int t302_idcliente_contrato, 
                                int t314_idusuario_responsable, int t314_idusuario_gestorprod, int t314_idusuario_ComercialHERMES,
                                bool t306_visionreplicas)
		{
			SqlParameter[] aParam = new SqlParameter[7];
			aParam[0] = new SqlParameter("@t306_idcontrato", SqlDbType.Int, 4);
			aParam[0].Value = t306_idcontrato;
			aParam[1] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
			aParam[1].Value = t303_idnodo;
			aParam[2] = new SqlParameter("@t302_idcliente_contrato", SqlDbType.Int, 4);
			aParam[2].Value = t302_idcliente_contrato;
			aParam[3] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
			aParam[3].Value = t314_idusuario_responsable;
            aParam[4] = new SqlParameter("@t314_idusuario_gestorprod", SqlDbType.Int, 4);
            aParam[4].Value = t314_idusuario_gestorprod;
            aParam[5] = new SqlParameter("@t314_idusuario_comercialhermes", SqlDbType.Int, 4);
            aParam[5].Value = t314_idusuario_ComercialHERMES;
            aParam[6] = new SqlParameter("@t306_visionreplicas", SqlDbType.Bit, 1);
            aParam[6].Value = t306_visionreplicas;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_CONTRATO_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CONTRATO_U", aParam);
		}

        public static int Update(SqlTransaction tr, int t306_idcontrato, int t314_idusuario_gestorprod)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t306_idcontrato", SqlDbType.Int, 4);
            aParam[0].Value = t306_idcontrato;
            aParam[1] = new SqlParameter("@t314_idusuario_gestorprod", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario_gestorprod;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CONTRATO_U_GP", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CONTRATO_U_GP", aParam);
        }
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T306_CONTRATO a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	26/11/2009 10:28:38
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t306_idcontrato)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t306_idcontrato", SqlDbType.Int, 4);
			aParam[0].Value = t306_idcontrato;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_CONTRATO_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CONTRATO_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T306_CONTRATO,
		/// y devuelve una instancia u objeto del tipo CONTRATO
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	26/11/2009 10:28:38
		/// </history>
		/// -----------------------------------------------------------------------------
		public static CONTRATO Select(SqlTransaction tr, int t306_idcontrato) 
		{
			CONTRATO o = new CONTRATO();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t306_idcontrato", SqlDbType.Int, 4);
			aParam[0].Value = t306_idcontrato;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_CONTRATO_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CONTRATO_S", aParam);

			if (dr.Read())
			{
				if (dr["t306_idcontrato"] != DBNull.Value)
					o.t306_idcontrato = int.Parse(dr["t306_idcontrato"].ToString());
				if (dr["t303_idnodo"] != DBNull.Value)
					o.t303_idnodo = int.Parse(dr["t303_idnodo"].ToString());
				if (dr["t302_idcliente_contrato"] != DBNull.Value)
					o.t302_idcliente_contrato = int.Parse(dr["t302_idcliente_contrato"].ToString());
				if (dr["t314_idusuario_responsable"] != DBNull.Value)
					o.t314_idusuario_responsable = int.Parse(dr["t314_idusuario_responsable"].ToString());
				if (dr["t314_idusuario_gestorprod"] != DBNull.Value)
					o.t314_idusuario_gestorprod = int.Parse(dr["t314_idusuario_gestorprod"].ToString());
                if (dr["Contrato"] != DBNull.Value)
                    o.Contrato = (string)dr["Contrato"];
                if (dr["Cliente"] != DBNull.Value)
                    o.Cliente = (string)dr["Cliente"];
                if (dr["Responsable"] != DBNull.Value)
                    o.Responsable = (string)dr["Responsable"];
                if (dr["GestorProdu"] != DBNull.Value)
                    o.GestorProdu = (string)dr["GestorProdu"];
                if (dr["Nodo"] != DBNull.Value)
                    o.Nodo = (string)dr["Nodo"];
                if (dr["t306_visionreplicas"] != DBNull.Value)
                    o.t306_visionreplicas = (bool)dr["t306_visionreplicas"];
                if (dr["t314_idusuario_ComercialHERMES"] != DBNull.Value)
                    o.t314_idusuario_ComercialHERMES = int.Parse(dr["t314_idusuario_ComercialHERMES"].ToString());
                if (dr["Comercial"] != DBNull.Value)
                    o.ComercialHERMES = (string)dr["Comercial"];
                //Organización comercial
                if (dr["ta212_idorganizacioncomercial"] != DBNull.Value)
                    o.ta212_idorganizacioncomercial = int.Parse(dr["ta212_idorganizacioncomercial"].ToString());
                if (dr["ta212_codigoexterno"] != DBNull.Value)
                    o.ta212_codigoexterno = (string)dr["ta212_codigoexterno"];
                if (dr["ta212_denominacion"] != DBNull.Value)
                    o.ta212_denominacion = (string)dr["ta212_denominacion"];
                //Nueva Línea de Oferta
                if (dr["t195_idlineaoferta"] != DBNull.Value)
                    o.t195_idlineaoferta = int.Parse(dr["t195_idlineaoferta"].ToString());
                if (dr["t195_codigoexterno"] != DBNull.Value)
                    o.t195_codigoexterno = int.Parse(dr["t195_codigoexterno"].ToString());
                if (dr["t195_denominacion"] != DBNull.Value)
                    o.t195_denominacion = (string)dr["t195_denominacion"];
            }
            else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de CONTRATO"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		#endregion
	}
}
