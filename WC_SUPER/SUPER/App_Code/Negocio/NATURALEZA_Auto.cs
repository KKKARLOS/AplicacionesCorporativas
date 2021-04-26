using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : NATURALEZA
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T323_NATURALEZA
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	09/12/2009 13:14:30	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class NATURALEZA
	{
		#region Propiedades y Atributos

		private int _t323_idnaturaleza;
		public int t323_idnaturaleza
		{
			get {return _t323_idnaturaleza;}
			set { _t323_idnaturaleza = value ;}
		}

		private string _t323_denominacion;
		public string t323_denominacion
		{
			get {return _t323_denominacion;}
			set { _t323_denominacion = value ;}
		}

		private int _t322_idsubgruponat;
		public int t322_idsubgruponat
		{
			get {return _t322_idsubgruponat;}
			set { _t322_idsubgruponat = value ;}
		}

		private bool _t323_regfes;
		public bool t323_regfes
		{
			get {return _t323_regfes;}
			set { _t323_regfes = value ;}
		}

		private bool _t323_regjornocompleta;
		public bool t323_regjornocompleta
		{
			get {return _t323_regjornocompleta;}
			set { _t323_regjornocompleta = value ;}
		}

		private bool _t323_coste;
		public bool t323_coste
		{
			get {return _t323_coste;}
			set { _t323_coste = value ;}
		}

		private int? _t338_idplantilla;
		public int? t338_idplantilla
		{
			get {return _t338_idplantilla;}
			set { _t338_idplantilla = value ;}
		}

		private int _t323_orden;
		public int t323_orden
		{
			get {return _t323_orden;}
			set { _t323_orden = value ;}
		}

		private byte _t323_mesesvigenciaPIG;
		public byte t323_mesesvigenciaPIG
		{
			get {return _t323_mesesvigenciaPIG;}
			set { _t323_mesesvigenciaPIG = value ;}
		}

        private bool _t323_estado;
        public bool t323_estado
        {
            get { return _t323_estado; }
            set { _t323_estado = value; }
        }
        private bool _t323_pasaaSAP;
        public bool t323_pasaaSAP
        {
            get { return _t323_pasaaSAP; }
            set { _t323_pasaaSAP = value; }
        }
        #endregion

		#region Constructor

		public NATURALEZA() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T323_NATURALEZA.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	09/12/2009 13:14:30
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Insert(SqlTransaction tr, string t323_denominacion , int t322_idsubgruponat , bool t323_regfes , 
                                 bool t323_regjornocompleta , bool t323_coste , Nullable<int> t338_idplantilla ,
                                 int t323_orden, byte t323_mesesvigenciaPIG, bool t323_estado, bool t323_pasaaSAP)
		{
			SqlParameter[] aParam = new SqlParameter[10];
			aParam[0] = new SqlParameter("@t323_denominacion", SqlDbType.Text, 50);
			aParam[0].Value = t323_denominacion;
			aParam[1] = new SqlParameter("@t322_idsubgruponat", SqlDbType.Int, 4);
			aParam[1].Value = t322_idsubgruponat;
			aParam[2] = new SqlParameter("@t323_regfes", SqlDbType.Bit, 1);
			aParam[2].Value = t323_regfes;
			aParam[3] = new SqlParameter("@t323_regjornocompleta", SqlDbType.Bit, 1);
			aParam[3].Value = t323_regjornocompleta;
			aParam[4] = new SqlParameter("@t323_coste", SqlDbType.Bit, 1);
			aParam[4].Value = t323_coste;
			aParam[5] = new SqlParameter("@t338_idplantilla", SqlDbType.Int, 4);
			aParam[5].Value = t338_idplantilla;
			aParam[6] = new SqlParameter("@t323_orden", SqlDbType.Int, 4);
			aParam[6].Value = t323_orden;
			aParam[7] = new SqlParameter("@t323_mesesvigenciaPIG", SqlDbType.TinyInt, 1);
			aParam[7].Value = t323_mesesvigenciaPIG;
            aParam[8] = new SqlParameter("@t323_estado", SqlDbType.Bit, 1);
            aParam[8].Value = t323_estado;
            aParam[9] = new SqlParameter("@t323_pasaaSAP", SqlDbType.Bit, 1);
            aParam[9].Value = t323_pasaaSAP;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
				return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_NATURALEZA_I", aParam));
			else
				return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_NATURALEZA_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T323_NATURALEZA.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	09/12/2009 13:14:30
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Update(SqlTransaction tr, int t323_idnaturaleza, string t323_denominacion, int t322_idsubgruponat,
                                 bool t323_regfes, bool t323_regjornocompleta, bool t323_coste, Nullable<int> t338_idplantilla,
                                 int t323_orden, byte t323_mesesvigenciaPIG, bool t323_estado, bool t323_pasaaSAP)
		{
			SqlParameter[] aParam = new SqlParameter[11];
			aParam[0] = new SqlParameter("@t323_idnaturaleza", SqlDbType.Int, 4);
			aParam[0].Value = t323_idnaturaleza;
			aParam[1] = new SqlParameter("@t323_denominacion", SqlDbType.Text, 50);
			aParam[1].Value = t323_denominacion;
			aParam[2] = new SqlParameter("@t322_idsubgruponat", SqlDbType.Int, 4);
			aParam[2].Value = t322_idsubgruponat;
			aParam[3] = new SqlParameter("@t323_regfes", SqlDbType.Bit, 1);
			aParam[3].Value = t323_regfes;
			aParam[4] = new SqlParameter("@t323_regjornocompleta", SqlDbType.Bit, 1);
			aParam[4].Value = t323_regjornocompleta;
			aParam[5] = new SqlParameter("@t323_coste", SqlDbType.Bit, 1);
			aParam[5].Value = t323_coste;
			aParam[6] = new SqlParameter("@t338_idplantilla", SqlDbType.Int, 4);
			aParam[6].Value = t338_idplantilla;
			aParam[7] = new SqlParameter("@t323_orden", SqlDbType.Int, 4);
			aParam[7].Value = t323_orden;
			aParam[8] = new SqlParameter("@t323_mesesvigenciaPIG", SqlDbType.TinyInt, 1);
			aParam[8].Value = t323_mesesvigenciaPIG;
            aParam[9] = new SqlParameter("@t323_estado", SqlDbType.Bit, 1);
            aParam[9].Value = t323_estado;
            aParam[10] = new SqlParameter("@t323_pasaaSAP", SqlDbType.Bit, 1);
            aParam[10].Value = t323_pasaaSAP;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_NATURALEZA_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_NATURALEZA_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T323_NATURALEZA a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	09/12/2009 13:14:30
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t323_idnaturaleza)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t323_idnaturaleza", SqlDbType.Int, 4);
			aParam[0].Value = t323_idnaturaleza;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_NATURALEZA_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_NATURALEZA_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T323_NATURALEZA,
		/// y devuelve una instancia u objeto del tipo NATURALEZA
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	09/12/2009 13:14:30
		/// </history>
		/// -----------------------------------------------------------------------------
		public static NATURALEZA Select(SqlTransaction tr, int t323_idnaturaleza) 
		{
			NATURALEZA o = new NATURALEZA();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t323_idnaturaleza", SqlDbType.Int, 4);
			aParam[0].Value = t323_idnaturaleza;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_NATURALEZA_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_NATURALEZA_S", aParam);

			if (dr.Read())
			{
				if (dr["t323_idnaturaleza"] != DBNull.Value)
					o.t323_idnaturaleza = int.Parse(dr["t323_idnaturaleza"].ToString());
				if (dr["t323_denominacion"] != DBNull.Value)
					o.t323_denominacion = (string)dr["t323_denominacion"];
				if (dr["t322_idsubgruponat"] != DBNull.Value)
					o.t322_idsubgruponat = int.Parse(dr["t322_idsubgruponat"].ToString());
				if (dr["t323_regfes"] != DBNull.Value)
					o.t323_regfes = (bool)dr["t323_regfes"];
				if (dr["t323_regjornocompleta"] != DBNull.Value)
					o.t323_regjornocompleta = (bool)dr["t323_regjornocompleta"];
				if (dr["t323_coste"] != DBNull.Value)
					o.t323_coste = (bool)dr["t323_coste"];
				if (dr["t338_idplantilla"] != DBNull.Value)
					o.t338_idplantilla = int.Parse(dr["t338_idplantilla"].ToString());
				if (dr["t323_orden"] != DBNull.Value)
					o.t323_orden = int.Parse(dr["t323_orden"].ToString());
				if (dr["t323_mesesvigenciaPIG"] != DBNull.Value)
					o.t323_mesesvigenciaPIG = byte.Parse(dr["t323_mesesvigenciaPIG"].ToString());
				if (dr["t323_estado"] != DBNull.Value)
					o.t323_estado = (bool)dr["t323_estado"];
                if (dr["t323_pasaaSAP"] != DBNull.Value)
                    o.t323_pasaaSAP = (bool)dr["t323_pasaaSAP"];

			}
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de NATURALEZA"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Selecciona los registros de la tabla T323_NATURALEZA en función de una foreign key.
		/// </summary>
		/// <returns>DataSet</returns>
		/// <history>
		/// 	Creado por [sqladmin]	09/12/2009 13:14:30
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader SelectByT338_idplantilla(SqlTransaction tr, Nullable<int> t338_idplantilla) 
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t338_idplantilla", SqlDbType.Int, 4);
			aParam[0].Value = t338_idplantilla;


			if (tr == null)
				return SqlHelper.ExecuteSqlDataReader("SUP_NATURALEZA_SByT338_idplantilla", aParam);
			else
				return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_NATURALEZA_SByT338_idplantilla", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Selecciona los registros de la tabla T323_NATURALEZA en función de una foreign key.
		/// </summary>
		/// <returns>DataSet</returns>
		/// <history>
		/// 	Creado por [sqladmin]	09/12/2009 13:14:30
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader SelectByT322_idsubgruponat(SqlTransaction tr, int t322_idsubgruponat) 
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t322_idsubgruponat", SqlDbType.Int, 4);
			aParam[0].Value = t322_idsubgruponat;


			if (tr == null)
				return SqlHelper.ExecuteSqlDataReader("SUP_NATURALEZA_SByT322_idsubgruponat", aParam);
			else
				return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_NATURALEZA_SByT322_idsubgruponat", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T323_NATURALEZA.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	09/12/2009 13:14:30
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader Catalogo(Nullable<int> t323_idnaturaleza, string t323_denominacion, Nullable<int> t322_idsubgruponat, Nullable<bool> t323_regfes, Nullable<bool> t323_regjornocompleta, Nullable<bool> t323_coste, Nullable<int> t338_idplantilla, Nullable<int> t323_orden, Nullable<byte> t323_mesesvigenciaPIG, Nullable<bool> t323_estado, byte nOrden, byte nAscDesc)
		{
			SqlParameter[] aParam = new SqlParameter[12];
			aParam[0] = new SqlParameter("@t323_idnaturaleza", SqlDbType.Int, 4);
			aParam[0].Value = t323_idnaturaleza;
			aParam[1] = new SqlParameter("@t323_denominacion", SqlDbType.Text, 50);
			aParam[1].Value = t323_denominacion;
			aParam[2] = new SqlParameter("@t322_idsubgruponat", SqlDbType.Int, 4);
			aParam[2].Value = t322_idsubgruponat;
			aParam[3] = new SqlParameter("@t323_regfes", SqlDbType.Bit, 1);
			aParam[3].Value = t323_regfes;
			aParam[4] = new SqlParameter("@t323_regjornocompleta", SqlDbType.Bit, 1);
			aParam[4].Value = t323_regjornocompleta;
			aParam[5] = new SqlParameter("@t323_coste", SqlDbType.Bit, 1);
			aParam[5].Value = t323_coste;
			aParam[6] = new SqlParameter("@t338_idplantilla", SqlDbType.Int, 4);
			aParam[6].Value = t338_idplantilla;
			aParam[7] = new SqlParameter("@t323_orden", SqlDbType.Int, 4);
			aParam[7].Value = t323_orden;
			aParam[8] = new SqlParameter("@t323_mesesvigenciaPIG", SqlDbType.TinyInt, 1);
			aParam[8].Value = t323_mesesvigenciaPIG;
			aParam[9] = new SqlParameter("@t323_estado", SqlDbType.Bit, 1);
			aParam[9].Value = t323_estado;

			aParam[10] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
			aParam[10].Value = nOrden;
			aParam[11] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
			aParam[11].Value = nAscDesc;

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
			return SqlHelper.ExecuteSqlDataReader("SUP_NATURALEZA_C", aParam);
		}

		#endregion
	}
}
