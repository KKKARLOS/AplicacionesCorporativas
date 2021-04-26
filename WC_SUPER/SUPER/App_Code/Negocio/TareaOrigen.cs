using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : TAREAORIGEN
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: t353_TAREAORIGEN
	/// </summary>
	/// <history>
	/// 	Creado por [DOARHUMI]	23/11/2007 11:14:50	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class TAREAORIGEN
	{
		#region Propiedades y Atributos

		private short _t353_idorigen;
		public short t353_idorigen
		{
			get {return _t353_idorigen;}
			set { _t353_idorigen = value ;}
		}

		private string _t353_desorigen;
		public string t353_desorigen
		{
			get {return _t353_desorigen;}
			set { _t353_desorigen = value ;}
		}

		private string _t353_email;
		public string t353_email
		{
			get {return _t353_email;}
			set { _t353_email = value ;}
		}

		private short _t303_idnodo;
		public short t303_idnodo
		{
			get {return _t303_idnodo;}
			set { _t303_idnodo = value ;}
		}

        private bool _t353_notificable;
        public bool t353_notificable
        {
            get { return _t353_notificable; }
            set { _t353_notificable = value; }
        }
        private bool _t353_estado;
        public bool t353_estado
        {
            get { return _t353_estado; }
            set { _t353_estado = value; }
        }
        #endregion

		#region Constructores

		public TAREAORIGEN() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla t353_TAREAORIGEN.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	23/11/2007 11:14:50
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, string t353_desorigen, string t353_email, short t303_idnodo, bool t353_notificable, bool t353_estado)
		{
			SqlParameter[] aParam = new SqlParameter[5];
			aParam[0] = new SqlParameter("@t353_desorigen", SqlDbType.Text, 25);
			aParam[0].Value = t353_desorigen;
			aParam[1] = new SqlParameter("@t353_email", SqlDbType.Text, 50);
            if (t353_email != "") aParam[1].Value = t353_email;
			aParam[2] = new SqlParameter("@t303_idnodo", SqlDbType.SmallInt, 2);
			aParam[2].Value = t303_idnodo;
            aParam[3] = new SqlParameter("@t353_notificable", SqlDbType.Bit, 1);
            aParam[3].Value = t353_notificable;
            aParam[4] = new SqlParameter("@t353_estado", SqlDbType.Bit, 1);
            aParam[4].Value = t353_estado;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_TAREAORIGEN_I", aParam));
			else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_TAREAORIGEN_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla t353_TAREAORIGEN.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	23/11/2007 11:14:50
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, short t353_idorigen, string t353_desorigen, string t353_email, short t303_idnodo, bool t353_notificable, bool t353_estado)
		{
			SqlParameter[] aParam = new SqlParameter[6];
			aParam[0] = new SqlParameter("@t353_idorigen", SqlDbType.SmallInt, 2);
			aParam[0].Value = t353_idorigen;
			aParam[1] = new SqlParameter("@t353_desorigen", SqlDbType.Text, 25);
			aParam[1].Value = t353_desorigen;
			aParam[2] = new SqlParameter("@t353_email", SqlDbType.Text, 50);
			if (t353_email != "") aParam[2].Value = t353_email;
			aParam[3] = new SqlParameter("@t303_idnodo", SqlDbType.SmallInt, 2);
			aParam[3].Value = t303_idnodo;
            aParam[4] = new SqlParameter("@t353_notificable", SqlDbType.Bit, 1);
            aParam[4].Value = t353_notificable;
            aParam[5] = new SqlParameter("@t353_estado", SqlDbType.Bit, 1);
            aParam[5].Value = t353_estado;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_TAREAORIGEN_U", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_TAREAORIGEN_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla t353_TAREAORIGEN a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	23/11/2007 11:14:50
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, short t353_idorigen)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t353_idorigen", SqlDbType.SmallInt, 2);
			aParam[0].Value = t353_idorigen;

			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_TAREAORIGEN_D", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_TAREAORIGEN_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla t353_TAREAORIGEN,
		/// y devuelve una instancia u objeto del tipo TAREAORIGEN
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	23/11/2007 11:14:50
		/// </history>
		/// -----------------------------------------------------------------------------
		public static TAREAORIGEN Select(SqlTransaction tr, short t353_idorigen) 
		{
			TAREAORIGEN o = new TAREAORIGEN();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t353_idorigen", SqlDbType.SmallInt, 2);
			aParam[0].Value = t353_idorigen;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_TAREAORIGEN_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_TAREAORIGEN_S", aParam);

			if (dr.Read())
			{
				if (dr["t353_idorigen"] != DBNull.Value)
                    o.t353_idorigen = short.Parse(dr["t353_idorigen"].ToString());
				if (dr["t353_desorigen"] != DBNull.Value)
					o.t353_desorigen = (string)dr["t353_desorigen"];
				if (dr["t353_email"] != DBNull.Value)
					o.t353_email = (string)dr["t353_email"];
				if (dr["t303_idnodo"] != DBNull.Value)
					o.t303_idnodo = short.Parse(dr["t303_idnodo"].ToString());
                if (dr["t353_notificable"] != DBNull.Value)
                    o.t353_notificable = (bool)dr["t353_notificable"];
                if (dr["t353_estado"] != DBNull.Value)
                    o.t353_estado = (bool)dr["t353_estado"];
			}
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de TAREAORIGEN"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Selecciona los registros de la tabla t353_TAREAORIGEN en función de una foreign key.
        /// </summary>
        /// <returns>DataSet</returns>
        /// <history>
        /// 	Creado por [DOARHUMI]	23/11/2007 11:14:50
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader SelectByt303_idnodo(SqlTransaction tr, short t303_idnodo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.SmallInt, 2);
            aParam[0].Value = t303_idnodo;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_TAREAORIGEN_SByt303_idnodo", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_TAREAORIGEN_SByt303_idnodo", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Selecciona los registros de la tabla t353_TAREAORIGEN en función de una foreign key y que estén activos.
        /// </summary>
        /// <returns>DataSet</returns>
        /// <history>
        /// 	Creado por [doarhumi]	20/03/2007 13:14:50
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader SelectActivas(SqlTransaction tr, short t303_idnodo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.SmallInt, 2);
            aParam[0].Value = t303_idnodo;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_TAREAORIGEN_Activas", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_TAREAORIGEN_Activas", aParam);
        }

		#endregion
	}
}
