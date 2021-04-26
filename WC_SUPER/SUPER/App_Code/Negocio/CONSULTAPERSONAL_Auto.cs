using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : CONSULTAPERSONAL
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T472_CONSULTAPERSONAL
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	13/01/2010 11:15:36	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class CONSULTAPERSONAL
	{
		#region Propiedades y Atributos

		private int _t472_idconsulta;
		public int t472_idconsulta
		{
			get {return _t472_idconsulta;}
			set { _t472_idconsulta = value ;}
		}

		private string _t472_denominacion;
		public string t472_denominacion
		{
			get {return _t472_denominacion;}
			set { _t472_denominacion = value ;}
		}

		private string _t472_procalm;
		public string t472_procalm
		{
			get {return _t472_procalm;}
			set { _t472_procalm = value ;}
		}

		private bool _t472_estado;
		public bool t472_estado
		{
			get {return _t472_estado;}
			set { _t472_estado = value ;}
		}

        private string _t472_descripcion;
        public string t472_descripcion
        {
            get { return _t472_descripcion; }
            set { _t472_descripcion = value; }
        }
        private string _t472_clavews;
        public string t472_clavews
        {
            get { return _t472_clavews; }
            set { _t472_clavews = value; }
        }
        #endregion 

        #region Constructor

        public CONSULTAPERSONAL() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T472_CONSULTAPERSONAL.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	13/01/2010 11:15:36
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Insert(SqlTransaction tr, string t472_denominacion, string t472_procalm, bool t472_estado,
                                 string t472_descripcion, string t472_clavews)
		{
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t472_denominacion", SqlDbType.Text, 50, t472_denominacion),
                ParametroSql.add("@t472_procalm", SqlDbType.Text, 30, t472_procalm),
                ParametroSql.add("@t472_estado", SqlDbType.Bit, 1, t472_estado),
                ParametroSql.add("@t472_descripcion", SqlDbType.Text, 2147483647, t472_descripcion),
                ParametroSql.add("@t472_clavews", SqlDbType.Text, 20, t472_clavews)
            };
            // Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
				return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_CONSULTAPERSONAL_I", aParam));
			else
				return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CONSULTAPERSONAL_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T472_CONSULTAPERSONAL.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	13/01/2010 11:15:36
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Update(SqlTransaction tr, int t472_idconsulta, string t472_denominacion, string t472_procalm, bool t472_estado,
                                 string t472_descripcion, string t472_clavews)
		{
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t472_idconsulta", SqlDbType.Int, 4, t472_idconsulta),
                ParametroSql.add("@t472_denominacion", SqlDbType.Text, 50, t472_denominacion),
                ParametroSql.add("@t472_procalm", SqlDbType.Text, 30, t472_procalm),
                ParametroSql.add("@t472_estado", SqlDbType.Bit, 1, t472_estado),
                ParametroSql.add("@t472_descripcion", SqlDbType.Text, 2147483647, t472_descripcion),
                ParametroSql.add("@t472_clavews", SqlDbType.Text, 20, t472_clavews)
            };
            // Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_CONSULTAPERSONAL_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CONSULTAPERSONAL_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T472_CONSULTAPERSONAL a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	13/01/2010 11:15:36
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t472_idconsulta)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t472_idconsulta", SqlDbType.Int, 4);
			aParam[0].Value = t472_idconsulta;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_CONSULTAPERSONAL_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CONSULTAPERSONAL_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T472_CONSULTAPERSONAL,
		/// y devuelve una instancia u objeto del tipo CONSULTAPERSONAL
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	13/01/2010 11:15:36
		/// </history>
		/// -----------------------------------------------------------------------------
		public static CONSULTAPERSONAL Select(SqlTransaction tr, int t472_idconsulta) 
		{
			CONSULTAPERSONAL o = new CONSULTAPERSONAL();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t472_idconsulta", SqlDbType.Int, 4);
			aParam[0].Value = t472_idconsulta;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_CONSULTAPERSONAL_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CONSULTAPERSONAL_S", aParam);

			if (dr.Read())
			{
				if (dr["t472_idconsulta"] != DBNull.Value)
					o.t472_idconsulta = int.Parse(dr["t472_idconsulta"].ToString());
				if (dr["t472_denominacion"] != DBNull.Value)
					o.t472_denominacion = (string)dr["t472_denominacion"];
				if (dr["t472_procalm"] != DBNull.Value)
					o.t472_procalm = (string)dr["t472_procalm"];
				if (dr["t472_estado"] != DBNull.Value)
					o.t472_estado = (bool)dr["t472_estado"];
                if (dr["t472_descripcion"] != DBNull.Value)
                    o.t472_descripcion = (string)dr["t472_descripcion"];
                if (dr["t472_clavews"] != DBNull.Value)
                    o.t472_clavews = (string)dr["t472_clavews"];

			}
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de CONSULTAPERSONAL"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		#endregion
	}
}
