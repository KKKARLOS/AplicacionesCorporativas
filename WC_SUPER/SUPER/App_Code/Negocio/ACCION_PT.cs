using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
    /// Project	 : SUPER
	/// Class	 : ACCION_PT
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T410_ACCIONPT
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	28/01/2008 11:32:29	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class ACCION_PT 
	{

		#region Propiedades y Atributos

		private DateTime _t410_fcreacion;
		public DateTime t410_fcreacion
		{
			get {return _t410_fcreacion;}
			set { _t410_fcreacion = value ;}
		}

		private int _t409_idasunto;
		public int t409_idasunto
		{
			get {return _t409_idasunto;}
			set { _t409_idasunto = value ;}
		}

		private string _t410_alerta;
		public string t410_alerta
		{
			get {return _t410_alerta;}
			set { _t410_alerta = value ;}
		}

		private byte _t410_avance;
        public byte t410_avance
		{
			get {return _t410_avance;}
			set { _t410_avance = value ;}
		}

		private string _t410_desaccion;
		public string t410_desaccion
		{
			get {return _t410_desaccion;}
			set { _t410_desaccion = value ;}
		}

		private string _t410_desaccionlong;
		public string t410_desaccionlong
		{
			get {return _t410_desaccionlong;}
			set { _t410_desaccionlong = value ;}
		}

		private string _t410_dpto;
		public string t410_dpto
		{
			get {return _t410_dpto;}
			set { _t410_dpto = value ;}
		}

		private DateTime _t410_ffin;
		public DateTime t410_ffin
		{
			get {return _t410_ffin;}
			set { _t410_ffin = value ;}
		}

		private DateTime _t410_flimite;
		public DateTime t410_flimite
		{
			get {return _t410_flimite;}
			set { _t410_flimite = value ;}
		}

		private int _t410_idaccion;
		public int t410_idaccion
		{
			get {return _t410_idaccion;}
			set { _t410_idaccion = value ;}
		}

		private string _t410_obs;
		public string t410_obs
		{
			get {return _t410_obs;}
			set { _t410_obs = value ;}
		}
		#endregion

		#region Constructores

		public ACCION_PT() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla t410_ACCIONPT.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 11:32:29
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, DateTime t410_fcreacion, int t409_idasunto, string t410_alerta, byte t410_avance, string t410_desaccion, string t410_desaccionlong, string t410_dpto, Nullable<DateTime> t410_ffin, Nullable<DateTime> t410_flimite, string t410_obs)
		{
			SqlParameter[] aParam = new SqlParameter[10];
			aParam[0] = new SqlParameter("@t410_fcreacion", SqlDbType.SmallDateTime, 4);
			aParam[0].Value = t410_fcreacion;
			aParam[1] = new SqlParameter("@t409_idasunto", SqlDbType.Int, 4);
			aParam[1].Value = t409_idasunto;
			aParam[2] = new SqlParameter("@t410_alerta", SqlDbType.Text, 2147483647);
			aParam[2].Value = t410_alerta;
			aParam[3] = new SqlParameter("@t410_avance", SqlDbType.TinyInt, 1);
			aParam[3].Value = t410_avance;
			aParam[4] = new SqlParameter("@t410_desaccion", SqlDbType.Text, 50);
			aParam[4].Value = t410_desaccion;
			aParam[5] = new SqlParameter("@t410_desaccionlong", SqlDbType.Text, 2147483647);
			aParam[5].Value = t410_desaccionlong;
			aParam[6] = new SqlParameter("@t410_dpto", SqlDbType.Text, 2147483647);
			aParam[6].Value = t410_dpto;
			aParam[7] = new SqlParameter("@t410_ffin", SqlDbType.SmallDateTime, 4);
			aParam[7].Value = t410_ffin;
			aParam[8] = new SqlParameter("@t410_flimite", SqlDbType.SmallDateTime, 4);
			aParam[8].Value = t410_flimite;
			aParam[9] = new SqlParameter("@t410_obs", SqlDbType.Text, 2147483647);
			aParam[9].Value = t410_obs;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_ACCION_PT_I", aParam));
			else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_ACCION_PT_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla t410_ACCIONPT.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 11:32:29
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, string t410_alerta, byte t410_avance, string t410_desaccion, string t410_desaccionlong, string t410_dpto, Nullable<DateTime> t410_ffin, Nullable<DateTime> t410_flimite, int t410_idaccion, string t410_obs)
		{
			SqlParameter[] aParam = new SqlParameter[9];
			aParam[0] = new SqlParameter("@t410_alerta", SqlDbType.Text, 2147483647);
			aParam[0].Value = t410_alerta;
			aParam[1] = new SqlParameter("@t410_avance", SqlDbType.TinyInt, 1);
			aParam[1].Value = t410_avance;
			aParam[2] = new SqlParameter("@t410_desaccion", SqlDbType.Text, 50);
			aParam[2].Value = t410_desaccion;
			aParam[3] = new SqlParameter("@t410_desaccionlong", SqlDbType.Text, 2147483647);
			aParam[3].Value = t410_desaccionlong;
			aParam[4] = new SqlParameter("@t410_dpto", SqlDbType.Text, 2147483647);
			aParam[4].Value = t410_dpto;
			aParam[5] = new SqlParameter("@t410_ffin", SqlDbType.SmallDateTime, 4);
			aParam[5].Value = t410_ffin;
			aParam[6] = new SqlParameter("@t410_flimite", SqlDbType.SmallDateTime, 4);
			aParam[6].Value = t410_flimite;
			aParam[7] = new SqlParameter("@t410_idaccion", SqlDbType.Int, 4);
			aParam[7].Value = t410_idaccion;
			aParam[8] = new SqlParameter("@t410_obs", SqlDbType.Text, 2147483647);
			aParam[8].Value = t410_obs;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_ACCION_PT_U", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ACCION_PT_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla t410_ACCION a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 11:32:29
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t410_idaccion)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t410_idaccion", SqlDbType.Int, 4);
			aParam[0].Value = t410_idaccion;

			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_ACCION_PT_D", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ACCION_PT_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla t410_ACCION,
		/// y devuelve una instancia u objeto del tipo ACCION
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 11:32:29
		/// </history>
		/// -----------------------------------------------------------------------------
		public static ACCION_PT Select(SqlTransaction tr, int t410_idaccion) 
		{
			ACCION_PT o = new ACCION_PT();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t410_idaccion", SqlDbType.Int, 4);
			aParam[0].Value = t410_idaccion;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_ACCION_PT_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_ACCION_PT_S", aParam);

			if (dr.Read())
			{
				if (dr["t410_fcreacion"] != DBNull.Value)
					o.t410_fcreacion = (DateTime)dr["t410_fcreacion"];
				if (dr["t409_idasunto"] != DBNull.Value)
					o.t409_idasunto = (int)dr["t409_idasunto"];
				if (dr["t410_alerta"] != DBNull.Value)
					o.t410_alerta = (string)dr["t410_alerta"];
				if (dr["t410_avance"] != DBNull.Value)
                    o.t410_avance = (byte)dr["t410_avance"];
				if (dr["t410_desaccion"] != DBNull.Value)
					o.t410_desaccion = (string)dr["t410_desaccion"];
				if (dr["t410_desaccionlong"] != DBNull.Value)
					o.t410_desaccionlong = (string)dr["t410_desaccionlong"];
				if (dr["t410_dpto"] != DBNull.Value)
					o.t410_dpto = (string)dr["t410_dpto"];
				if (dr["t410_ffin"] != DBNull.Value)
					o.t410_ffin = (DateTime)dr["t410_ffin"];
				if (dr["t410_flimite"] != DBNull.Value)
					o.t410_flimite = (DateTime)dr["t410_flimite"];
				if (dr["t410_idaccion"] != DBNull.Value)
					o.t410_idaccion = (int)dr["t410_idaccion"];
				if (dr["t410_obs"] != DBNull.Value)
					o.t410_obs = (string)dr["t410_obs"];

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
		/// Selecciona los registros de la tabla t410_ACCION en función de una foreign key.
		/// </summary>
		/// <returns>DataSet</returns>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 11:32:29
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader SelectByt409_idasunto(SqlTransaction tr, int t409_idasunto) 
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t409_idasunto", SqlDbType.Int, 4);
			aParam[0].Value = t409_idasunto;

			if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_ACCION_PT_SByT409_idasunto", aParam);
			else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_ACCION_PT_SByT409_idasunto", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla t410_ACCIONPT.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 11:32:29
		/// </history>
		/// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo(Nullable<DateTime> t410_fcreacion, Nullable<int> t409_idasunto, Nullable<byte> t410_avance, string t410_desaccion, Nullable<DateTime> t410_ffin, Nullable<DateTime> t410_flimite, Nullable<int> t410_idaccion, byte nOrden, byte nAscDesc)
		{
			SqlParameter[] aParam = new SqlParameter[9];
			aParam[0] = new SqlParameter("@t410_fcreacion", SqlDbType.SmallDateTime, 4);
			aParam[0].Value = t410_fcreacion;
			aParam[1] = new SqlParameter("@t409_idasunto", SqlDbType.Int, 4);
			aParam[1].Value = t409_idasunto;
			aParam[2] = new SqlParameter("@t410_avance", SqlDbType.TinyInt, 1);
			aParam[2].Value = t410_avance;
			aParam[3] = new SqlParameter("@t410_desaccion", SqlDbType.Text, 50);
            if (t410_desaccion == "") aParam[3].Value = null;
            else aParam[3].Value = t410_desaccion;
			aParam[4] = new SqlParameter("@t410_ffin", SqlDbType.SmallDateTime, 4);
			aParam[4].Value = t410_ffin;
			aParam[5] = new SqlParameter("@t410_flimite", SqlDbType.SmallDateTime, 4);
			aParam[5].Value = t410_flimite;
			aParam[6] = new SqlParameter("@t410_idaccion", SqlDbType.Int, 4);
			aParam[6].Value = t410_idaccion;

			aParam[7] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
			aParam[7].Value = nOrden;
			aParam[8] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
			aParam[8].Value = nAscDesc;

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_ACCION_PT_C", aParam);
		}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de tareas del proyecto técnico
        /// </summary>
        /// <history>
        /// 	Creado por [doarhumi]	31/01/2008 11:32:29
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader CatalogoTareasPT(int iNumPT)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nPT", SqlDbType.Int);
            aParam[0].Value = iNumPT;

            return SqlHelper.ExecuteSqlDataReader("SUP_TAREACATA3", aParam);
        }

		#endregion
	}
}
