using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
    /// Project	 : SUPER
	/// Class	 : ACCION_T
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T601_ACCIONT
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	09/02/2010 11:32:29	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class ACCION_T 
	{

		#region Propiedades y Atributos

		private DateTime _t601_fcreacion;
		public DateTime t601_fcreacion
		{
			get {return _t601_fcreacion;}
			set { _t601_fcreacion = value ;}
		}

		private int _t600_idasunto;
		public int t600_idasunto
		{
			get {return _t600_idasunto;}
			set { _t600_idasunto = value ;}
		}

		private string _t601_alerta;
		public string t601_alerta
		{
			get {return _t601_alerta;}
			set { _t601_alerta = value ;}
		}

		private byte _t601_avance;
        public byte t601_avance
		{
			get {return _t601_avance;}
			set { _t601_avance = value ;}
		}

		private string _t601_desaccion;
		public string t601_desaccion
		{
			get {return _t601_desaccion;}
			set { _t601_desaccion = value ;}
		}

		private string _t601_desaccionlong;
		public string t601_desaccionlong
		{
			get {return _t601_desaccionlong;}
			set { _t601_desaccionlong = value ;}
		}

		private string _t601_dpto;
		public string t601_dpto
		{
			get {return _t601_dpto;}
			set { _t601_dpto = value ;}
		}

		private DateTime _t601_ffin;
		public DateTime t601_ffin
		{
			get {return _t601_ffin;}
			set { _t601_ffin = value ;}
		}

		private DateTime _t601_flimite;
		public DateTime t601_flimite
		{
			get {return _t601_flimite;}
			set { _t601_flimite = value ;}
		}

		private int _t601_idaccion;
		public int t601_idaccion
		{
			get {return _t601_idaccion;}
			set { _t601_idaccion = value ;}
		}

		private string _t601_obs;
		public string t601_obs
		{
			get {return _t601_obs;}
			set { _t601_obs = value ;}
		}
		#endregion

		#region Constructores

		public ACCION_T() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla t601_ACCIONPT.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 11:32:29
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, DateTime t601_fcreacion, int t600_idasunto, string t601_alerta, byte t601_avance, string t601_desaccion, string t601_desaccionlong, string t601_dpto, Nullable<DateTime> t601_ffin, Nullable<DateTime> t601_flimite, string t601_obs)
		{
			SqlParameter[] aParam = new SqlParameter[10];
			aParam[0] = new SqlParameter("@t601_fcreacion", SqlDbType.SmallDateTime, 4);
			aParam[0].Value = t601_fcreacion;
			aParam[1] = new SqlParameter("@t600_idasunto", SqlDbType.Int, 4);
			aParam[1].Value = t600_idasunto;
			aParam[2] = new SqlParameter("@t601_alerta", SqlDbType.Text, 2147483647);
			aParam[2].Value = t601_alerta;
			aParam[3] = new SqlParameter("@t601_avance", SqlDbType.TinyInt, 1);
			aParam[3].Value = t601_avance;
			aParam[4] = new SqlParameter("@t601_desaccion", SqlDbType.Text, 50);
			aParam[4].Value = t601_desaccion;
			aParam[5] = new SqlParameter("@t601_desaccionlong", SqlDbType.Text, 2147483647);
			aParam[5].Value = t601_desaccionlong;
			aParam[6] = new SqlParameter("@t601_dpto", SqlDbType.Text, 2147483647);
			aParam[6].Value = t601_dpto;
			aParam[7] = new SqlParameter("@t601_ffin", SqlDbType.SmallDateTime, 4);
			aParam[7].Value = t601_ffin;
			aParam[8] = new SqlParameter("@t601_flimite", SqlDbType.SmallDateTime, 4);
			aParam[8].Value = t601_flimite;
			aParam[9] = new SqlParameter("@t601_obs", SqlDbType.Text, 2147483647);
			aParam[9].Value = t601_obs;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_ACCION_T_I", aParam));
			else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_ACCION_T_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla t601_ACCIONPT.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 11:32:29
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, string t601_alerta, byte t601_avance, string t601_desaccion, string t601_desaccionlong, string t601_dpto, Nullable<DateTime> t601_ffin, Nullable<DateTime> t601_flimite, int t601_idaccion, string t601_obs)
		{
			SqlParameter[] aParam = new SqlParameter[9];
			aParam[0] = new SqlParameter("@t601_alerta", SqlDbType.Text, 2147483647);
			aParam[0].Value = t601_alerta;
			aParam[1] = new SqlParameter("@t601_avance", SqlDbType.TinyInt, 1);
			aParam[1].Value = t601_avance;
			aParam[2] = new SqlParameter("@t601_desaccion", SqlDbType.Text, 50);
			aParam[2].Value = t601_desaccion;
			aParam[3] = new SqlParameter("@t601_desaccionlong", SqlDbType.Text, 2147483647);
			aParam[3].Value = t601_desaccionlong;
			aParam[4] = new SqlParameter("@t601_dpto", SqlDbType.Text, 2147483647);
			aParam[4].Value = t601_dpto;
			aParam[5] = new SqlParameter("@t601_ffin", SqlDbType.SmallDateTime, 4);
			aParam[5].Value = t601_ffin;
			aParam[6] = new SqlParameter("@t601_flimite", SqlDbType.SmallDateTime, 4);
			aParam[6].Value = t601_flimite;
			aParam[7] = new SqlParameter("@t601_idaccion", SqlDbType.Int, 4);
			aParam[7].Value = t601_idaccion;
			aParam[8] = new SqlParameter("@t601_obs", SqlDbType.Text, 2147483647);
			aParam[8].Value = t601_obs;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_ACCION_T_U", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ACCION_T_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla t601_ACCION a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 11:32:29
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t601_idaccion)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t601_idaccion", SqlDbType.Int, 4);
			aParam[0].Value = t601_idaccion;

			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_ACCION_T_D", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ACCION_T_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla t601_ACCION,
		/// y devuelve una instancia u objeto del tipo ACCION
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 11:32:29
		/// </history>
		/// -----------------------------------------------------------------------------
		public static ACCION_T Select(SqlTransaction tr, int t601_idaccion) 
		{
			ACCION_T o = new ACCION_T();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t601_idaccion", SqlDbType.Int, 4);
			aParam[0].Value = t601_idaccion;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_ACCION_T_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_ACCION_T_S", aParam);

			if (dr.Read())
			{
				if (dr["t601_fcreacion"] != DBNull.Value)
					o.t601_fcreacion = (DateTime)dr["t601_fcreacion"];
				if (dr["t600_idasunto"] != DBNull.Value)
					o.t600_idasunto = (int)dr["t600_idasunto"];
				if (dr["t601_alerta"] != DBNull.Value)
					o.t601_alerta = (string)dr["t601_alerta"];
				if (dr["t601_avance"] != DBNull.Value)
                    o.t601_avance = (byte)dr["t601_avance"];
				if (dr["t601_desaccion"] != DBNull.Value)
					o.t601_desaccion = (string)dr["t601_desaccion"];
				if (dr["t601_desaccionlong"] != DBNull.Value)
					o.t601_desaccionlong = (string)dr["t601_desaccionlong"];
				if (dr["t601_dpto"] != DBNull.Value)
					o.t601_dpto = (string)dr["t601_dpto"];
				if (dr["t601_ffin"] != DBNull.Value)
					o.t601_ffin = (DateTime)dr["t601_ffin"];
				if (dr["t601_flimite"] != DBNull.Value)
					o.t601_flimite = (DateTime)dr["t601_flimite"];
				if (dr["t601_idaccion"] != DBNull.Value)
					o.t601_idaccion = (int)dr["t601_idaccion"];
				if (dr["t601_obs"] != DBNull.Value)
					o.t601_obs = (string)dr["t601_obs"];

			}
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de ACCION"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Selecciona los registros de la tabla t601_ACCION en función de una foreign key.
		/// </summary>
		/// <returns>DataSet</returns>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 11:32:29
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader SelectByt600_idasunto(SqlTransaction tr, int t600_idasunto) 
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t600_idasunto", SqlDbType.Int, 4);
			aParam[0].Value = t600_idasunto;

			if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_ACCION_T_SByT600_idasunto", aParam);
			else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_ACCION_T_SByT600_idasunto", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla t601_ACCIONPT.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 11:32:29
		/// </history>
		/// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo(Nullable<DateTime> t601_fcreacion, Nullable<int> t600_idasunto, Nullable<byte> t601_avance, string t601_desaccion, Nullable<DateTime> t601_ffin, Nullable<DateTime> t601_flimite, Nullable<int> t601_idaccion, byte nOrden, byte nAscDesc)
		{
			SqlParameter[] aParam = new SqlParameter[9];
			aParam[0] = new SqlParameter("@t601_fcreacion", SqlDbType.SmallDateTime, 4);
			aParam[0].Value = t601_fcreacion;
			aParam[1] = new SqlParameter("@t600_idasunto", SqlDbType.Int, 4);
			aParam[1].Value = t600_idasunto;
			aParam[2] = new SqlParameter("@t601_avance", SqlDbType.TinyInt, 1);
			aParam[2].Value = t601_avance;
			aParam[3] = new SqlParameter("@t601_desaccion", SqlDbType.Text, 50);
            if (t601_desaccion == "") aParam[3].Value = null;
            else aParam[3].Value = t601_desaccion;
			aParam[4] = new SqlParameter("@t601_ffin", SqlDbType.SmallDateTime, 4);
			aParam[4].Value = t601_ffin;
			aParam[5] = new SqlParameter("@t601_flimite", SqlDbType.SmallDateTime, 4);
			aParam[5].Value = t601_flimite;
			aParam[6] = new SqlParameter("@t601_idaccion", SqlDbType.Int, 4);
			aParam[6].Value = t601_idaccion;

			aParam[7] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
			aParam[7].Value = nOrden;
			aParam[8] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
			aParam[8].Value = nAscDesc;

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_ACCION_T_C", aParam);
		}

		#endregion
	}
}
