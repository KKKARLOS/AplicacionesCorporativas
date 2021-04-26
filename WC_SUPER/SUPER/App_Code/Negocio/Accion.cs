using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : ACCION
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: t383_ACCION
	/// </summary>
	/// <history>
	/// 	Creado por [DOARHUMI]	16/11/2007 11:32:29	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class ACCION
	{

		#region Propiedades y Atributos

		private DateTime _t383_fcreacion;
		public DateTime t383_fcreacion
		{
			get {return _t383_fcreacion;}
			set { _t383_fcreacion = value ;}
		}

		private int _t382_idasunto;
		public int t382_idasunto
		{
			get {return _t382_idasunto;}
			set { _t382_idasunto = value ;}
		}

		private string _t383_alerta;
		public string t383_alerta
		{
			get {return _t383_alerta;}
			set { _t383_alerta = value ;}
		}

        private byte _t383_avance;
        public byte t383_avance
		{
			get {return _t383_avance;}
			set { _t383_avance = value ;}
		}

		private string _t383_desaccion;
		public string t383_desaccion
		{
			get {return _t383_desaccion;}
			set { _t383_desaccion = value ;}
		}

		private string _t383_desaccionlong;
		public string t383_desaccionlong
		{
			get {return _t383_desaccionlong;}
			set { _t383_desaccionlong = value ;}
		}

		private string _t383_dpto;
		public string t383_dpto
		{
			get {return _t383_dpto;}
			set { _t383_dpto = value ;}
		}

		private DateTime _t383_ffin;
		public DateTime t383_ffin
		{
			get {return _t383_ffin;}
			set { _t383_ffin = value ;}
		}

		private DateTime _t383_flimite;
		public DateTime t383_flimite
		{
			get {return _t383_flimite;}
			set { _t383_flimite = value ;}
		}

		private int _t383_idaccion;
		public int t383_idaccion
		{
			get {return _t383_idaccion;}
			set { _t383_idaccion = value ;}
		}

		private string _t383_obs;
		public string t383_obs
		{
			get {return _t383_obs;}
			set { _t383_obs = value ;}
		}
		#endregion

		#region Constructores

		public ACCION() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla t383_ACCION
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	16/11/2007 11:32:29
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, DateTime t383_fcreacion, int t382_idasunto, string t383_alerta, byte t383_avance, string t383_desaccion, string t383_desaccionlong, string t383_dpto, Nullable<DateTime> t383_ffin, Nullable<DateTime> t383_flimite, string t383_obs)
		{
			SqlParameter[] aParam = new SqlParameter[10];
			aParam[0] = new SqlParameter("@t383_fcreacion", SqlDbType.SmallDateTime, 4);
			aParam[0].Value = t383_fcreacion;
			aParam[1] = new SqlParameter("@t382_idasunto", SqlDbType.Int, 4);
			aParam[1].Value = t382_idasunto;
			aParam[2] = new SqlParameter("@t383_alerta", SqlDbType.Text, 2147483647);
			aParam[2].Value = t383_alerta;
			aParam[3] = new SqlParameter("@t383_avance", SqlDbType.TinyInt, 1);
			aParam[3].Value = t383_avance;
			aParam[4] = new SqlParameter("@t383_desaccion", SqlDbType.Text, 50);
			aParam[4].Value = t383_desaccion;
			aParam[5] = new SqlParameter("@t383_desaccionlong", SqlDbType.Text, 2147483647);
			aParam[5].Value = t383_desaccionlong;
			aParam[6] = new SqlParameter("@t383_dpto", SqlDbType.Text, 2147483647);
			aParam[6].Value = t383_dpto;
			aParam[7] = new SqlParameter("@t383_ffin", SqlDbType.SmallDateTime, 4);
			aParam[7].Value = t383_ffin;
			aParam[8] = new SqlParameter("@t383_flimite", SqlDbType.SmallDateTime, 4);
			aParam[8].Value = t383_flimite;
			aParam[9] = new SqlParameter("@t383_obs", SqlDbType.Text, 2147483647);
			aParam[9].Value = t383_obs;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_ACCION_I", aParam));
			else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_ACCION_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla t383_ACCION
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	16/11/2007 11:32:29
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, string t383_alerta, byte t383_avance, string t383_desaccion, string t383_desaccionlong, string t383_dpto, Nullable<DateTime> t383_ffin, Nullable<DateTime> t383_flimite, int t383_idaccion, string t383_obs)
		{
			SqlParameter[] aParam = new SqlParameter[9];
			aParam[0] = new SqlParameter("@t383_alerta", SqlDbType.Text, 2147483647);
			aParam[0].Value = t383_alerta;
			aParam[1] = new SqlParameter("@t383_avance", SqlDbType.TinyInt, 1);
			aParam[1].Value = t383_avance;
			aParam[2] = new SqlParameter("@t383_desaccion", SqlDbType.Text, 50);
			aParam[2].Value = t383_desaccion;
			aParam[3] = new SqlParameter("@t383_desaccionlong", SqlDbType.Text, 2147483647);
			aParam[3].Value = t383_desaccionlong;
			aParam[4] = new SqlParameter("@t383_dpto", SqlDbType.Text, 2147483647);
			aParam[4].Value = t383_dpto;
			aParam[5] = new SqlParameter("@t383_ffin", SqlDbType.SmallDateTime, 4);
			aParam[5].Value = t383_ffin;
			aParam[6] = new SqlParameter("@t383_flimite", SqlDbType.SmallDateTime, 4);
			aParam[6].Value = t383_flimite;
			aParam[7] = new SqlParameter("@t383_idaccion", SqlDbType.Int, 4);
			aParam[7].Value = t383_idaccion;
			aParam[8] = new SqlParameter("@t383_obs", SqlDbType.Text, 2147483647);
			aParam[8].Value = t383_obs;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_ACCION_U", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ACCION_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla t383_ACCION a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	16/11/2007 11:32:29
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t383_idaccion)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t383_idaccion", SqlDbType.Int, 4);
			aParam[0].Value = t383_idaccion;

			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_ACCION_D", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ACCION_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla t383_ACCION,
		/// y devuelve una instancia u objeto del tipo ACCION
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	16/11/2007 11:32:29
		/// </history>
		/// -----------------------------------------------------------------------------
		public static ACCION Select(SqlTransaction tr, int t383_idaccion) 
		{
			ACCION o = new ACCION();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t383_idaccion", SqlDbType.Int, 4);
			aParam[0].Value = t383_idaccion;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_ACCION_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_ACCION_S", aParam);

			if (dr.Read())
			{
				if (dr["t383_fcreacion"] != DBNull.Value)
					o.t383_fcreacion = (DateTime)dr["t383_fcreacion"];
				if (dr["t382_idasunto"] != DBNull.Value)
					o.t382_idasunto = (int)dr["t382_idasunto"];
				if (dr["t383_alerta"] != DBNull.Value)
					o.t383_alerta = (string)dr["t383_alerta"];
				if (dr["t383_avance"] != DBNull.Value)
                    o.t383_avance = byte.Parse(dr["t383_avance"].ToString());
				if (dr["t383_desaccion"] != DBNull.Value)
					o.t383_desaccion = (string)dr["t383_desaccion"];
				if (dr["t383_desaccionlong"] != DBNull.Value)
					o.t383_desaccionlong = (string)dr["t383_desaccionlong"];
				if (dr["t383_dpto"] != DBNull.Value)
					o.t383_dpto = (string)dr["t383_dpto"];
				if (dr["t383_ffin"] != DBNull.Value)
					o.t383_ffin = (DateTime)dr["t383_ffin"];
				if (dr["t383_flimite"] != DBNull.Value)
					o.t383_flimite = (DateTime)dr["t383_flimite"];
				if (dr["t383_idaccion"] != DBNull.Value)
					o.t383_idaccion = (int)dr["t383_idaccion"];
				if (dr["t383_obs"] != DBNull.Value)
					o.t383_obs = (string)dr["t383_obs"];

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
		/// Selecciona los registros de la tabla t383_ACCION en función de una foreign key.
		/// </summary>
		/// <returns>DataSet</returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	16/11/2007 11:32:29
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader SelectByt382_idasunto(SqlTransaction tr, int t382_idasunto) 
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t382_idasunto", SqlDbType.Int, 4);
			aParam[0].Value = t382_idasunto;

			if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_ACCION_SByt382_idasunto", aParam);
			else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_ACCION_SByt382_idasunto", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla t383_ACCION
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	16/11/2007 11:32:29
		/// </history>
		/// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo(Nullable<DateTime> t383_fcreacion, Nullable<int> t382_idasunto, Nullable<byte> t383_avance, string t383_desaccion, Nullable<DateTime> t383_ffin, Nullable<DateTime> t383_flimite, Nullable<int> t383_idaccion, byte nOrden, byte nAscDesc)
		{
			SqlParameter[] aParam = new SqlParameter[9];
			aParam[0] = new SqlParameter("@t383_fcreacion", SqlDbType.SmallDateTime, 4);
			aParam[0].Value = t383_fcreacion;
			aParam[1] = new SqlParameter("@t382_idasunto", SqlDbType.Int, 4);
			aParam[1].Value = t382_idasunto;
			aParam[2] = new SqlParameter("@t383_avance", SqlDbType.TinyInt, 1);
			aParam[2].Value = t383_avance;
			aParam[3] = new SqlParameter("@t383_desaccion", SqlDbType.Text, 50);
            if (t383_desaccion == "") aParam[3].Value = null;
            else aParam[3].Value = t383_desaccion;
			aParam[4] = new SqlParameter("@t383_ffin", SqlDbType.SmallDateTime, 4);
			aParam[4].Value = t383_ffin;
			aParam[5] = new SqlParameter("@t383_flimite", SqlDbType.SmallDateTime, 4);
			aParam[5].Value = t383_flimite;
			aParam[6] = new SqlParameter("@t383_idaccion", SqlDbType.Int, 4);
			aParam[6].Value = t383_idaccion;

			aParam[7] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
			aParam[7].Value = nOrden;
			aParam[8] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
			aParam[8].Value = nAscDesc;

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_ACCION_C", aParam);
		}

		#endregion
	}
}
