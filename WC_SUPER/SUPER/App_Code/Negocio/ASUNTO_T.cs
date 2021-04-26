using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
    /// Project	 : SUPER
	/// Class	 : ASUNTO_T
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T600_ASUNTOT
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	28/01/2008 15:01:06	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class ASUNTO_T
	{

		#region Propiedades y Atributos

		private int _t332_idtarea;
		public int t332_idtarea
		{
			get {return _t332_idtarea;}
			set { _t332_idtarea = value ;}
		}

		private string _t600_alerta;
		public string t600_alerta
		{
			get {return _t600_alerta;}
			set { _t600_alerta = value ;}
		}

		private string _t600_desasunto;
		public string t600_desasunto
		{
			get {return _t600_desasunto;}
			set { _t600_desasunto = value ;}
		}

		private string _t600_desasuntolong;
		public string t600_desasuntolong
		{
			get {return _t600_desasuntolong;}
			set { _t600_desasuntolong = value ;}
		}

		private string _t600_dpto;
		public string t600_dpto
		{
			get {return _t600_dpto;}
			set { _t600_dpto = value ;}
		}

        private byte _t600_estado;
        public byte t600_estado
        {
            get { return _t600_estado; }
            set { _t600_estado = value; }
        }
        private string _Estado;
        public string Estado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }

        private double _t600_etp;
        public double t600_etp
		{
			get {return _t600_etp;}
			set { _t600_etp = value ;}
		}

        private double _t600_etr;
        public double t600_etr
		{
			get {return _t600_etr;}
			set { _t600_etr = value ;}
		}

		private DateTime _t600_fcreacion;
		public DateTime t600_fcreacion
		{
			get {return _t600_fcreacion;}
			set { _t600_fcreacion = value ;}
		}

		private DateTime _t600_ffin;
		public DateTime t600_ffin
		{
			get {return _t600_ffin;}
			set { _t600_ffin = value ;}
		}

		private DateTime _t600_flimite;
		public DateTime t600_flimite
		{
			get {return _t600_flimite;}
			set { _t600_flimite = value ;}
		}

		private DateTime _t600_fnotificacion;
		public DateTime t600_fnotificacion
		{
			get {return _t600_fnotificacion;}
			set { _t600_fnotificacion = value ;}
		}

		private int _t600_idasunto;
		public int t600_idasunto
		{
			get {return _t600_idasunto;}
			set { _t600_idasunto = value ;}
		}

		private string _t600_notificador;
		public string t600_notificador
		{
			get {return _t600_notificador;}
			set { _t600_notificador = value ;}
		}

		private string _t600_obs;
		public string t600_obs
		{
			get {return _t600_obs;}
			set { _t600_obs = value ;}
		}

        private byte _t600_prioridad;
        public byte t600_prioridad
        {
            get { return _t600_prioridad; }
            set { _t600_prioridad = value; }
        }
        private string _Prioridad;
        public string Prioridad
        {
            get { return _Prioridad; }
            set { _Prioridad = value; }
        }

		private string _t600_refexterna;
		public string t600_refexterna
		{
			get {return _t600_refexterna;}
			set { _t600_refexterna = value ;}
		}

        private int _t600_registrador;
        public int t600_registrador
        {
            get { return _t600_registrador; }
            set { _t600_registrador = value; }
        }
        private string _Registrador;
        public string Registrador
        {
            get { return _Registrador; }
            set { _Registrador = value; }
        }

		private int _t600_responsable;
		public int t600_responsable
		{
			get {return _t600_responsable;}
			set { _t600_responsable = value ;}
		}
        private string _Responsable;
        public string Responsable
        {
            get { return _Responsable; }
            set { _Responsable = value; }
        }

        private byte _t600_severidad;
        public byte t600_severidad
        {
            get { return _t600_severidad; }
            set { _t600_severidad = value; }
        }
        private string _Severidad;
        public string Severidad
        {
            get { return _Severidad; }
            set { _Severidad = value; }
        }

		private string _t600_sistema;
		public string t600_sistema
		{
			get {return _t600_sistema;}
			set { _t600_sistema = value ;}
		}

        private int _t384_idtipo;
        public int t384_idtipo
        {
            get { return _t384_idtipo; }
            set { _t384_idtipo = value; }
        }
        private string _Tipo;
        public string Tipo
        {
            get { return _Tipo; }
            set { _Tipo = value; }
        }
        #endregion

		#region Constructores

		public ASUNTO_T() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T600_ASUNTOT.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 15:01:06
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, int t332_idtarea, string t600_alerta, string t600_desasunto, string t600_desasuntolong, string t600_dpto, byte t600_estado, double t600_etp, double t600_etr, Nullable<DateTime> t600_ffin, Nullable<DateTime> t600_flimite, Nullable<DateTime> t600_fnotificacion, string t600_notificador, string t600_obs, byte t600_prioridad, string t600_refexterna, int t600_registrador, int t600_responsable, byte t600_severidad, string t600_sistema, int t384_idtipo)
		{
			SqlParameter[] aParam = new SqlParameter[20];
            aParam[0] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
            aParam[0].Value = t332_idtarea;
			aParam[1] = new SqlParameter("@t600_alerta", SqlDbType.Text, 2147483647);
			aParam[1].Value = t600_alerta;
			aParam[2] = new SqlParameter("@t600_desasunto", SqlDbType.Text, 50);
			aParam[2].Value = t600_desasunto;
			aParam[3] = new SqlParameter("@t600_desasuntolong", SqlDbType.Text, 2147483647);
			aParam[3].Value = t600_desasuntolong;
			aParam[4] = new SqlParameter("@t600_dpto", SqlDbType.Text, 2147483647);
			aParam[4].Value = t600_dpto;
			aParam[5] = new SqlParameter("@t600_estado", SqlDbType.TinyInt, 1);
			aParam[5].Value = t600_estado;
            aParam[6] = new SqlParameter("@t600_etp", SqlDbType.Float, 8);
			aParam[6].Value = t600_etp;
            aParam[7] = new SqlParameter("@t600_etr", SqlDbType.Float, 8);
			aParam[7].Value = t600_etr;
			aParam[8] = new SqlParameter("@t600_ffin", SqlDbType.SmallDateTime, 4);
			aParam[8].Value = t600_ffin;
			aParam[9] = new SqlParameter("@t600_flimite", SqlDbType.SmallDateTime, 4);
			aParam[9].Value = t600_flimite;
			aParam[10] = new SqlParameter("@t600_fnotificacion", SqlDbType.SmallDateTime, 4);
			aParam[10].Value = t600_fnotificacion;
			aParam[11] = new SqlParameter("@t600_notificador", SqlDbType.Text, 50);
			aParam[11].Value = t600_notificador;
			aParam[12] = new SqlParameter("@t600_obs", SqlDbType.Text, 2147483647);
			aParam[12].Value = t600_obs;
			aParam[13] = new SqlParameter("@t600_prioridad", SqlDbType.TinyInt, 1);
			aParam[13].Value = t600_prioridad;
			aParam[14] = new SqlParameter("@t600_refexterna", SqlDbType.Text, 50);
			aParam[14].Value = t600_refexterna;
			aParam[15] = new SqlParameter("@t600_registrador", SqlDbType.Int, 4);
			aParam[15].Value = t600_registrador;
			aParam[16] = new SqlParameter("@t600_responsable", SqlDbType.Int, 4);
			aParam[16].Value = t600_responsable;
			aParam[17] = new SqlParameter("@t600_severidad", SqlDbType.TinyInt, 1);
			aParam[17].Value = t600_severidad;
			aParam[18] = new SqlParameter("@t600_sistema", SqlDbType.Text, 50);
			aParam[18].Value = t600_sistema;
			aParam[19] = new SqlParameter("@t384_idtipo", SqlDbType.Int, 4);
            aParam[19].Value = t384_idtipo;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_ASUNTO_T_I", aParam));
			else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_ASUNTO_T_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T600_ASUNTOT.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 15:01:06
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t332_idtarea,
            string t600_alerta,
            string t600_desasunto,
            string t600_desasuntolong,
            string t600_dpto,
            byte t600_estado,
            double t600_etp,
            double t600_etr,
            Nullable<DateTime> t600_ffin,
            Nullable<DateTime> t600_flimite,
            DateTime t600_fnotificacion,
            int t600_idasunto,
            string t600_notificador,
            string t600_obs,
            byte t600_prioridad,
            string t600_refexterna,
            int t600_responsable,
            byte t600_severidad,
            string t600_sistema,
            int t384_idtipo)
        {
			SqlParameter[] aParam = new SqlParameter[20];
            aParam[0] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
            aParam[0].Value = t332_idtarea;
			aParam[1] = new SqlParameter("@t600_alerta", SqlDbType.Text, 2147483647);
			aParam[1].Value = t600_alerta;
			aParam[2] = new SqlParameter("@t600_desasunto", SqlDbType.Text, 50);
			aParam[2].Value = t600_desasunto;
			aParam[3] = new SqlParameter("@t600_desasuntolong", SqlDbType.Text, 2147483647);
			aParam[3].Value = t600_desasuntolong;
			aParam[4] = new SqlParameter("@t600_dpto", SqlDbType.Text, 2147483647);
			aParam[4].Value = t600_dpto;
			aParam[5] = new SqlParameter("@t600_estado", SqlDbType.TinyInt, 1);
			aParam[5].Value = t600_estado;
            aParam[6] = new SqlParameter("@t600_etp", SqlDbType.Float, 8);
			aParam[6].Value = t600_etp;
            aParam[7] = new SqlParameter("@t600_etr", SqlDbType.Float, 8);
			aParam[7].Value = t600_etr;
			aParam[8] = new SqlParameter("@t600_ffin", SqlDbType.SmallDateTime, 4);
			aParam[8].Value = t600_ffin;
			aParam[9] = new SqlParameter("@t600_flimite", SqlDbType.SmallDateTime, 4);
			aParam[9].Value = t600_flimite;
			aParam[10] = new SqlParameter("@t600_fnotificacion", SqlDbType.SmallDateTime, 4);
			aParam[10].Value = t600_fnotificacion;
			aParam[11] = new SqlParameter("@t600_idasunto", SqlDbType.Int, 4);
			aParam[11].Value = t600_idasunto;
			aParam[12] = new SqlParameter("@t600_notificador", SqlDbType.Text, 50);
			aParam[12].Value = t600_notificador;
			aParam[13] = new SqlParameter("@t600_obs", SqlDbType.Text, 2147483647);
			aParam[13].Value = t600_obs;
			aParam[14] = new SqlParameter("@t600_prioridad", SqlDbType.TinyInt, 1);
			aParam[14].Value = t600_prioridad;
			aParam[15] = new SqlParameter("@t600_refexterna", SqlDbType.Text, 50);
			aParam[15].Value = t600_refexterna;
            aParam[16] = new SqlParameter("@t600_responsable", SqlDbType.Int, 4);
            aParam[16].Value = t600_responsable;
            aParam[17] = new SqlParameter("@t600_severidad", SqlDbType.TinyInt, 1);
			aParam[17].Value = t600_severidad;
			aParam[18] = new SqlParameter("@t600_sistema", SqlDbType.Text, 50);
			aParam[18].Value = t600_sistema;
			aParam[19] = new SqlParameter("@t384_idtipo", SqlDbType.Int, 4);
			aParam[19].Value = t384_idtipo;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_ASUNTO_T_U", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ASUNTO_T_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T600_ASUNTOT a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 15:01:06
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t600_idasunto)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t600_idasunto", SqlDbType.Int, 4);
			aParam[0].Value = t600_idasunto;

			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_ASUNTO_T_D", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ASUNTO_T_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T600_ASUNTOT,
		/// y devuelve una instancia u objeto del tipo ASUNTO
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 15:01:06
		/// </history>
		/// -----------------------------------------------------------------------------
		public static ASUNTO_T Select(SqlTransaction tr, int t600_idasunto) 
		{
			ASUNTO_T o = new ASUNTO_T();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t600_idasunto", SqlDbType.Int, 4);
			aParam[0].Value = t600_idasunto;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_ASUNTO_T_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_ASUNTO_T_S", aParam);

			if (dr.Read())
			{
				if (dr["t332_idtarea"] != DBNull.Value)
                    o.t332_idtarea = (int)dr["t332_idtarea"];
				if (dr["t600_alerta"] != DBNull.Value)
					o.t600_alerta = (string)dr["t600_alerta"];
				if (dr["t600_desasunto"] != DBNull.Value)
					o.t600_desasunto = (string)dr["t600_desasunto"];
				if (dr["t600_desasuntolong"] != DBNull.Value)
					o.t600_desasuntolong = (string)dr["t600_desasuntolong"];
				if (dr["t600_dpto"] != DBNull.Value)
					o.t600_dpto = (string)dr["t600_dpto"];
				if (dr["t600_estado"] != DBNull.Value)
					o.t600_estado = byte.Parse(dr["t600_estado"].ToString());
                if (dr["Estado"] != DBNull.Value)
                    o.Estado = (string)dr["Estado"];
                if (dr["t600_etp"] != DBNull.Value)
					o.t600_etp = (double)dr["t600_etp"];
				if (dr["t600_etr"] != DBNull.Value)
                    o.t600_etr = (double)dr["t600_etr"];
				if (dr["t600_fcreacion"] != DBNull.Value)
					o.t600_fcreacion = (DateTime)dr["t600_fcreacion"];
				if (dr["t600_ffin"] != DBNull.Value)
					o.t600_ffin = (DateTime)dr["t600_ffin"];
				if (dr["t600_flimite"] != DBNull.Value)
					o.t600_flimite = (DateTime)dr["t600_flimite"];
				if (dr["t600_fnotificacion"] != DBNull.Value)
					o.t600_fnotificacion = (DateTime)dr["t600_fnotificacion"];
				if (dr["t600_idasunto"] != DBNull.Value)
					o.t600_idasunto = (int)dr["t600_idasunto"];
				if (dr["t600_notificador"] != DBNull.Value)
					o.t600_notificador = (string)dr["t600_notificador"];
				if (dr["t600_obs"] != DBNull.Value)
					o.t600_obs = (string)dr["t600_obs"];
				if (dr["t600_prioridad"] != DBNull.Value)
					o.t600_prioridad = byte.Parse(dr["t600_prioridad"].ToString());
                if (dr["Prioridad"] != DBNull.Value)
                    o.Prioridad = (string)dr["Prioridad"];
                if (dr["t600_refexterna"] != DBNull.Value)
                    o.t600_refexterna = (string)dr["t600_refexterna"];
                if (dr["t600_registrador"] != DBNull.Value)
					o.t600_registrador = (int)dr["t600_registrador"];
                if (dr["Registrador"] != DBNull.Value)
                    o.Registrador = (string)dr["Registrador"];
                if (dr["t600_responsable"] != DBNull.Value)
					o.t600_responsable = (int)dr["t600_responsable"];
                if (dr["Responsable"] != DBNull.Value)
                    o.Responsable = (string)dr["Responsable"];
                if (dr["t600_severidad"] != DBNull.Value)
					o.t600_severidad = byte.Parse(dr["t600_severidad"].ToString());
                if (dr["Severidad"] != DBNull.Value)
                    o.Severidad = (string)dr["Severidad"];
                if (dr["t600_sistema"] != DBNull.Value)
					o.t600_sistema = (string)dr["t600_sistema"];
				if (dr["t384_idtipo"] != DBNull.Value)
					o.t384_idtipo = (int)dr["t384_idtipo"];
                if (dr["Tipo"] != DBNull.Value)
                    o.Tipo = (string)dr["Tipo"];

			}
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de ASUNTO"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T600_ASUNTOT.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 15:01:06
		/// </history>
		/// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo(Nullable<int> t332_idtarea, string t600_desasunto, Nullable<byte> t600_estado, Nullable<double> t600_etp, Nullable<double> t600_etr, Nullable<DateTime> t600_fcreacion, Nullable<DateTime> t600_ffin, Nullable<DateTime> t600_flimite, Nullable<DateTime> t600_fnotificacion, Nullable<int> t600_idasunto, string t600_notificador, Nullable<byte> t600_prioridad, string t600_refexterna, Nullable<int> t600_registrador, Nullable<int> t600_responsable, Nullable<byte> t600_severidad, string t600_sistema, Nullable<int> t384_idtipo, byte nOrden, byte nAscDesc)
		{
            string sProcAlm;
            SqlParameter[] aParam = new SqlParameter[20];
            aParam[0] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
            aParam[0].Value = t332_idtarea;
			aParam[1] = new SqlParameter("@t600_desasunto", SqlDbType.Text, 50);
			aParam[1].Value = t600_desasunto;
			aParam[2] = new SqlParameter("@t600_estado", SqlDbType.TinyInt, 1);
            if (t600_estado == 0) aParam[2].Value = null;
			else aParam[2].Value = t600_estado;
            aParam[3] = new SqlParameter("@t600_etp", SqlDbType.Float, 8);
			aParam[3].Value = t600_etp;
            aParam[4] = new SqlParameter("@t600_etr", SqlDbType.Float, 8);
			aParam[4].Value = t600_etr;
			aParam[5] = new SqlParameter("@t600_fcreacion", SqlDbType.SmallDateTime, 4);
			aParam[5].Value = t600_fcreacion;
			aParam[6] = new SqlParameter("@t600_ffin", SqlDbType.SmallDateTime, 4);
			aParam[6].Value = t600_ffin;
			aParam[7] = new SqlParameter("@t600_flimite", SqlDbType.SmallDateTime, 4);
			aParam[7].Value = t600_flimite;
			aParam[8] = new SqlParameter("@t600_fnotificacion", SqlDbType.SmallDateTime, 4);
			aParam[8].Value = t600_fnotificacion;
			aParam[9] = new SqlParameter("@t600_idasunto", SqlDbType.Int, 4);
			aParam[9].Value = t600_idasunto;
			aParam[10] = new SqlParameter("@t600_notificador", SqlDbType.Text, 50);
			aParam[10].Value = t600_notificador;
			aParam[11] = new SqlParameter("@t600_prioridad", SqlDbType.TinyInt, 1);
			aParam[11].Value = t600_prioridad;
			aParam[12] = new SqlParameter("@t600_refexterna", SqlDbType.Text, 50);
			aParam[12].Value = t600_refexterna;
			aParam[13] = new SqlParameter("@t600_registrador", SqlDbType.Int, 4);
			aParam[13].Value = t600_registrador;
			aParam[14] = new SqlParameter("@t600_responsable", SqlDbType.Int, 4);
			aParam[14].Value = t600_responsable;
			aParam[15] = new SqlParameter("@t600_severidad", SqlDbType.TinyInt, 1);
			aParam[15].Value = t600_severidad;
			aParam[16] = new SqlParameter("@t600_sistema", SqlDbType.Text, 50);
			aParam[16].Value = t600_sistema;
			aParam[17] = new SqlParameter("@t384_idtipo", SqlDbType.Int, 4);
            if (t384_idtipo == 0) aParam[17].Value = null;
			else aParam[17].Value = t384_idtipo;

			aParam[18] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
			aParam[18].Value = nOrden;
			aParam[19] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
			aParam[19].Value = nAscDesc;

            switch (nOrden)
            {
                case 5:
                    if (nAscDesc == 0) sProcAlm = "SUP_ASUNTO_T_C_ESTADO1";
                    else sProcAlm = "SUP_ASUNTO_T_C_ESTADO2";
                    break;
                case 13:
                    if (nAscDesc == 0) sProcAlm = "SUP_ASUNTO_T_C_PRIORIDAD1";
                    else sProcAlm = "SUP_ASUNTO_T_C_PRIORIDAD2";
                    break;
                case 17:
                    if (nAscDesc == 0) sProcAlm = "SUP_ASUNTO_T_C_SEVERIDAD1";
                    else sProcAlm = "SUP_ASUNTO_T_C_SEVERIDAD2";
                    break;
                default:
                    sProcAlm="SUP_ASUNTO_T_C";
                    break;
            }
			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader(sProcAlm, aParam);
		}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla T600_ASUNTOT y T601_ACCIONT para la pantalla de consulta de Bitacora.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	28/01/2008 15:01:06
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo2(Nullable<int> t332_idtarea, string t600_desasunto, 
                                            Nullable<byte> t600_estado,
                                            string t600_ffinD, string t600_ffinH,
                                            string t600_flimiteD, string t600_flimiteH,
                                            string t600_fnotificacionD, string t600_fnotificacionH, 
                                            Nullable<byte> t600_prioridad, 
                                            Nullable<byte> t600_severidad,
                                            Nullable<int> t384_idtipo, byte nOrden, byte nAscDesc, string sAcciones)
        {
            string sProcAlm;
            if (t600_ffinD == "") t600_ffinD = "01/01/1950";
            if (t600_flimiteD == "") t600_flimiteD = "01/01/1950";
            if (t600_fnotificacionD == "") t600_fnotificacionD = "01/01/1950";
            if (t600_ffinH == "") t600_ffinH = "31/12/2050";
            if (t600_flimiteH == "") t600_flimiteH = "31/12/2050";
            if (t600_fnotificacionH == "") t600_fnotificacionH = "31/12/2050";

            SqlParameter[] aParam = new SqlParameter[14];
            aParam[0] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
            aParam[0].Value = t332_idtarea;
            aParam[1] = new SqlParameter("@t600_desasunto", SqlDbType.Text, 50);
            aParam[1].Value = t600_desasunto;
            aParam[2] = new SqlParameter("@t600_estado", SqlDbType.TinyInt, 1);
            if (t600_estado == 0) aParam[2].Value = null;
            else aParam[2].Value = t600_estado;

            aParam[3] = new SqlParameter("@ffinD", SqlDbType.VarChar, 10);
            aParam[3].Value = t600_ffinD;
            aParam[4] = new SqlParameter("@ffinH", SqlDbType.VarChar, 10);
            aParam[4].Value = t600_ffinH;

            aParam[5] = new SqlParameter("@flimiteD", SqlDbType.VarChar, 10);
            aParam[5].Value = t600_flimiteD;
            aParam[6] = new SqlParameter("@flimiteH", SqlDbType.VarChar, 10);
            aParam[6].Value = t600_flimiteH;

            aParam[7] = new SqlParameter("@fnotificacionD", SqlDbType.VarChar, 10);
            aParam[7].Value = t600_fnotificacionD;
            aParam[8] = new SqlParameter("@fnotificacionH", SqlDbType.VarChar, 10);
            aParam[8].Value = t600_fnotificacionH;

            aParam[9] = new SqlParameter("@t600_prioridad", SqlDbType.TinyInt, 1);
            if (t600_prioridad == 0) aParam[9].Value = null;
            else aParam[9].Value = t600_prioridad;

            aParam[10] = new SqlParameter("@t600_severidad", SqlDbType.TinyInt, 1);
            if (t600_severidad == 0) aParam[10].Value = null;
            else aParam[10].Value = t600_severidad;

            aParam[11] = new SqlParameter("@t384_idtipo", SqlDbType.Int, 4);
            if (t384_idtipo == 0) aParam[11].Value = null;
            else aParam[11].Value = t384_idtipo;

            aParam[12] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
            aParam[12].Value = nOrden;
            aParam[13] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
            aParam[13].Value = nAscDesc;

            if (sAcciones == "N")
                sProcAlm = "SUP_BIT_AS_T";
            else
                sProcAlm = "SUP_BIT_AS_TyAC";

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader(sProcAlm, aParam);
        }
        #endregion
        #region Pruebas
        /*
        public static SqlDataReader Duplicados()
        {
            string sProcAlm = "ZZZ_MIK_BIT1";

            SqlParameter[] aParam = new SqlParameter[0];
            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader(sProcAlm, aParam);
        }
        public static SqlDataReader Duplicados(int t332_idtarea, string t600_desasunto, int iTipo)
        {
            string sProcAlm = "ZZZ_MIK_BIT2";

            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
            aParam[0].Value = t332_idtarea;
            aParam[1] = new SqlParameter("@t600_desasunto", SqlDbType.Text, 50);
            aParam[1].Value = t600_desasunto;
            aParam[2] = new SqlParameter("@t384_idtipo", SqlDbType.Int, 4);
            aParam[2].Value = iTipo;
            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader(sProcAlm, aParam);
        }
        public static bool TieneAcciones(int nIdAsunto)
        {
            bool bTieneAcciones = false;
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t600_idasunto", SqlDbType.Int, 4);
            aParam[0].Value = nIdAsunto;

            int returnValue;
            returnValue = System.Convert.ToInt32(SqlHelper.ExecuteScalar("ZZZ_MIKEL_TIENEACCIONES", aParam));

            if (returnValue > 0)
                bTieneAcciones = true;

            return bTieneAcciones;
        }
         * */
        #endregion
    }
}
