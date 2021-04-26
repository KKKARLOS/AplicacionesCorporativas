using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
    /// Project	 : SUPER
	/// Class	 : ASUNTO_PT
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T409_ASUNTOPT
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	28/01/2008 15:01:06	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class ASUNTO_PT
	{

		#region Propiedades y Atributos

		private int _t331_idpt;
		public int t331_idpt
		{
			get {return _t331_idpt;}
			set { _t331_idpt = value ;}
		}

		private string _t409_alerta;
		public string t409_alerta
		{
			get {return _t409_alerta;}
			set { _t409_alerta = value ;}
		}

		private string _t409_desasunto;
		public string t409_desasunto
		{
			get {return _t409_desasunto;}
			set { _t409_desasunto = value ;}
		}

		private string _t409_desasuntolong;
		public string t409_desasuntolong
		{
			get {return _t409_desasuntolong;}
			set { _t409_desasuntolong = value ;}
		}

		private string _t409_dpto;
		public string t409_dpto
		{
			get {return _t409_dpto;}
			set { _t409_dpto = value ;}
		}

        private byte _t409_estado;
        public byte t409_estado
        {
            get { return _t409_estado; }
            set { _t409_estado = value; }
        }
        private string _Estado;
        public string Estado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }

        private double _t409_etp;
        public double t409_etp
		{
			get {return _t409_etp;}
			set { _t409_etp = value ;}
		}

        private double _t409_etr;
        public double t409_etr
		{
			get {return _t409_etr;}
			set { _t409_etr = value ;}
		}

		private DateTime _t409_fcreacion;
		public DateTime t409_fcreacion
		{
			get {return _t409_fcreacion;}
			set { _t409_fcreacion = value ;}
		}

		private DateTime _t409_ffin;
		public DateTime t409_ffin
		{
			get {return _t409_ffin;}
			set { _t409_ffin = value ;}
		}

		private DateTime _t409_flimite;
		public DateTime t409_flimite
		{
			get {return _t409_flimite;}
			set { _t409_flimite = value ;}
		}

		private DateTime _t409_fnotificacion;
		public DateTime t409_fnotificacion
		{
			get {return _t409_fnotificacion;}
			set { _t409_fnotificacion = value ;}
		}

		private int _t409_idasunto;
		public int t409_idasunto
		{
			get {return _t409_idasunto;}
			set { _t409_idasunto = value ;}
		}

		private string _t409_notificador;
		public string t409_notificador
		{
			get {return _t409_notificador;}
			set { _t409_notificador = value ;}
		}

		private string _t409_obs;
		public string t409_obs
		{
			get {return _t409_obs;}
			set { _t409_obs = value ;}
		}

        private byte _t409_prioridad;
        public byte t409_prioridad
        {
            get { return _t409_prioridad; }
            set { _t409_prioridad = value; }
        }
        private string _Prioridad;
        public string Prioridad
        {
            get { return _Prioridad; }
            set { _Prioridad = value; }
        }

		private string _t409_refexterna;
		public string t409_refexterna
		{
			get {return _t409_refexterna;}
			set { _t409_refexterna = value ;}
		}

        private int _t409_registrador;
        public int t409_registrador
        {
            get { return _t409_registrador; }
            set { _t409_registrador = value; }
        }
        private string _Registrador;
        public string Registrador
        {
            get { return _Registrador; }
            set { _Registrador = value; }
        }

		private int _t409_responsable;
		public int t409_responsable
		{
			get {return _t409_responsable;}
			set { _t409_responsable = value ;}
		}
        private string _Responsable;
        public string Responsable
        {
            get { return _Responsable; }
            set { _Responsable = value; }
        }

        private byte _t409_severidad;
        public byte t409_severidad
        {
            get { return _t409_severidad; }
            set { _t409_severidad = value; }
        }
        private string _Severidad;
        public string Severidad
        {
            get { return _Severidad; }
            set { _Severidad = value; }
        }

		private string _t409_sistema;
		public string t409_sistema
		{
			get {return _t409_sistema;}
			set { _t409_sistema = value ;}
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

		public ASUNTO_PT() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T409_ASUNTOPT.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 15:01:06
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, int t331_idpt, string t409_alerta, string t409_desasunto, string t409_desasuntolong, string t409_dpto, byte t409_estado, double t409_etp, double t409_etr, Nullable<DateTime> t409_ffin, Nullable<DateTime> t409_flimite, Nullable<DateTime> t409_fnotificacion, string t409_notificador, string t409_obs, byte t409_prioridad, string t409_refexterna, int t409_registrador, int t409_responsable, byte t409_severidad, string t409_sistema, int t384_idtipo)
		{
			SqlParameter[] aParam = new SqlParameter[20];
			aParam[0] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
			aParam[0].Value = t331_idpt;
			aParam[1] = new SqlParameter("@t409_alerta", SqlDbType.Text, 2147483647);
			aParam[1].Value = t409_alerta;
			aParam[2] = new SqlParameter("@t409_desasunto", SqlDbType.Text, 50);
			aParam[2].Value = t409_desasunto;
			aParam[3] = new SqlParameter("@t409_desasuntolong", SqlDbType.Text, 2147483647);
			aParam[3].Value = t409_desasuntolong;
			aParam[4] = new SqlParameter("@t409_dpto", SqlDbType.Text, 2147483647);
			aParam[4].Value = t409_dpto;
			aParam[5] = new SqlParameter("@t409_estado", SqlDbType.TinyInt, 1);
			aParam[5].Value = t409_estado;
            aParam[6] = new SqlParameter("@t409_etp", SqlDbType.Float, 8);
			aParam[6].Value = t409_etp;
            aParam[7] = new SqlParameter("@t409_etr", SqlDbType.Float, 8);
			aParam[7].Value = t409_etr;
			aParam[8] = new SqlParameter("@t409_ffin", SqlDbType.SmallDateTime, 4);
			aParam[8].Value = t409_ffin;
			aParam[9] = new SqlParameter("@t409_flimite", SqlDbType.SmallDateTime, 4);
			aParam[9].Value = t409_flimite;
			aParam[10] = new SqlParameter("@t409_fnotificacion", SqlDbType.SmallDateTime, 4);
			aParam[10].Value = t409_fnotificacion;
			aParam[11] = new SqlParameter("@t409_notificador", SqlDbType.Text, 50);
			aParam[11].Value = t409_notificador;
			aParam[12] = new SqlParameter("@t409_obs", SqlDbType.Text, 2147483647);
			aParam[12].Value = t409_obs;
			aParam[13] = new SqlParameter("@t409_prioridad", SqlDbType.TinyInt, 1);
			aParam[13].Value = t409_prioridad;
			aParam[14] = new SqlParameter("@t409_refexterna", SqlDbType.Text, 50);
			aParam[14].Value = t409_refexterna;
			aParam[15] = new SqlParameter("@t409_registrador", SqlDbType.Int, 4);
			aParam[15].Value = t409_registrador;
			aParam[16] = new SqlParameter("@t409_responsable", SqlDbType.Int, 4);
			aParam[16].Value = t409_responsable;
			aParam[17] = new SqlParameter("@t409_severidad", SqlDbType.TinyInt, 1);
			aParam[17].Value = t409_severidad;
			aParam[18] = new SqlParameter("@t409_sistema", SqlDbType.Text, 50);
			aParam[18].Value = t409_sistema;
			aParam[19] = new SqlParameter("@t384_idtipo", SqlDbType.Int, 4);
            aParam[19].Value = t384_idtipo;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_ASUNTO_PT_I", aParam));
			else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_ASUNTO_PT_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T409_ASUNTOPT.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 15:01:06
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t331_idpt,
            string t409_alerta,
            string t409_desasunto,
            string t409_desasuntolong,
            string t409_dpto,
            byte t409_estado,
            double t409_etp,
            double t409_etr,
            Nullable<DateTime> t409_ffin,
            Nullable<DateTime> t409_flimite,
            DateTime t409_fnotificacion,
            int t409_idasunto,
            string t409_notificador,
            string t409_obs,
            byte t409_prioridad,
            string t409_refexterna,
            int t409_responsable,
            byte t409_severidad,
            string t409_sistema,
            int t384_idtipo)
        {
			SqlParameter[] aParam = new SqlParameter[20];
			aParam[0] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
			aParam[0].Value = t331_idpt;
			aParam[1] = new SqlParameter("@t409_alerta", SqlDbType.Text, 2147483647);
			aParam[1].Value = t409_alerta;
			aParam[2] = new SqlParameter("@t409_desasunto", SqlDbType.Text, 50);
			aParam[2].Value = t409_desasunto;
			aParam[3] = new SqlParameter("@t409_desasuntolong", SqlDbType.Text, 2147483647);
			aParam[3].Value = t409_desasuntolong;
			aParam[4] = new SqlParameter("@t409_dpto", SqlDbType.Text, 2147483647);
			aParam[4].Value = t409_dpto;
			aParam[5] = new SqlParameter("@t409_estado", SqlDbType.TinyInt, 1);
			aParam[5].Value = t409_estado;
            aParam[6] = new SqlParameter("@t409_etp", SqlDbType.Float, 8);
			aParam[6].Value = t409_etp;
            aParam[7] = new SqlParameter("@t409_etr", SqlDbType.Float, 8);
			aParam[7].Value = t409_etr;
			aParam[8] = new SqlParameter("@t409_ffin", SqlDbType.SmallDateTime, 4);
			aParam[8].Value = t409_ffin;
			aParam[9] = new SqlParameter("@t409_flimite", SqlDbType.SmallDateTime, 4);
			aParam[9].Value = t409_flimite;
			aParam[10] = new SqlParameter("@t409_fnotificacion", SqlDbType.SmallDateTime, 4);
			aParam[10].Value = t409_fnotificacion;
			aParam[11] = new SqlParameter("@t409_idasunto", SqlDbType.Int, 4);
			aParam[11].Value = t409_idasunto;
			aParam[12] = new SqlParameter("@t409_notificador", SqlDbType.Text, 50);
			aParam[12].Value = t409_notificador;
			aParam[13] = new SqlParameter("@t409_obs", SqlDbType.Text, 2147483647);
			aParam[13].Value = t409_obs;
			aParam[14] = new SqlParameter("@t409_prioridad", SqlDbType.TinyInt, 1);
			aParam[14].Value = t409_prioridad;
			aParam[15] = new SqlParameter("@t409_refexterna", SqlDbType.Text, 50);
			aParam[15].Value = t409_refexterna;
            aParam[16] = new SqlParameter("@t409_responsable", SqlDbType.Int, 4);
            aParam[16].Value = t409_responsable;
            aParam[17] = new SqlParameter("@t409_severidad", SqlDbType.TinyInt, 1);
			aParam[17].Value = t409_severidad;
			aParam[18] = new SqlParameter("@t409_sistema", SqlDbType.Text, 50);
			aParam[18].Value = t409_sistema;
			aParam[19] = new SqlParameter("@t384_idtipo", SqlDbType.Int, 4);
			aParam[19].Value = t384_idtipo;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_ASUNTO_PT_U", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ASUNTO_PT_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T409_ASUNTOPT a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 15:01:06
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t409_idasunto)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t409_idasunto", SqlDbType.Int, 4);
			aParam[0].Value = t409_idasunto;

			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_ASUNTO_PT_D", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ASUNTO_PT_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T409_ASUNTOPT,
		/// y devuelve una instancia u objeto del tipo ASUNTO
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 15:01:06
		/// </history>
		/// -----------------------------------------------------------------------------
		public static ASUNTO_PT Select(SqlTransaction tr, int t409_idasunto) 
		{
			ASUNTO_PT o = new ASUNTO_PT();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t409_idasunto", SqlDbType.Int, 4);
			aParam[0].Value = t409_idasunto;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_ASUNTO_PT_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_ASUNTO_PT_S", aParam);

			if (dr.Read())
			{
				if (dr["t331_idpt"] != DBNull.Value)
					o.t331_idpt = (int)dr["t331_idpt"];
				if (dr["t409_alerta"] != DBNull.Value)
					o.t409_alerta = (string)dr["t409_alerta"];
				if (dr["t409_desasunto"] != DBNull.Value)
					o.t409_desasunto = (string)dr["t409_desasunto"];
				if (dr["t409_desasuntolong"] != DBNull.Value)
					o.t409_desasuntolong = (string)dr["t409_desasuntolong"];
				if (dr["t409_dpto"] != DBNull.Value)
					o.t409_dpto = (string)dr["t409_dpto"];
				if (dr["t409_estado"] != DBNull.Value)
					o.t409_estado = byte.Parse(dr["t409_estado"].ToString());
                if (dr["Estado"] != DBNull.Value)
                    o.Estado = (string)dr["Estado"];
                if (dr["t409_etp"] != DBNull.Value)
					o.t409_etp = (double)dr["t409_etp"];
				if (dr["t409_etr"] != DBNull.Value)
                    o.t409_etr = (double)dr["t409_etr"];
				if (dr["t409_fcreacion"] != DBNull.Value)
					o.t409_fcreacion = (DateTime)dr["t409_fcreacion"];
				if (dr["t409_ffin"] != DBNull.Value)
					o.t409_ffin = (DateTime)dr["t409_ffin"];
				if (dr["t409_flimite"] != DBNull.Value)
					o.t409_flimite = (DateTime)dr["t409_flimite"];
				if (dr["t409_fnotificacion"] != DBNull.Value)
					o.t409_fnotificacion = (DateTime)dr["t409_fnotificacion"];
				if (dr["t409_idasunto"] != DBNull.Value)
					o.t409_idasunto = (int)dr["t409_idasunto"];
				if (dr["t409_notificador"] != DBNull.Value)
					o.t409_notificador = (string)dr["t409_notificador"];
				if (dr["t409_obs"] != DBNull.Value)
					o.t409_obs = (string)dr["t409_obs"];
				if (dr["t409_prioridad"] != DBNull.Value)
					o.t409_prioridad = byte.Parse(dr["t409_prioridad"].ToString());
                if (dr["Prioridad"] != DBNull.Value)
                    o.Prioridad = (string)dr["Prioridad"];
                if (dr["t409_refexterna"] != DBNull.Value)
                    o.t409_refexterna = (string)dr["t409_refexterna"];
                if (dr["t409_registrador"] != DBNull.Value)
					o.t409_registrador = (int)dr["t409_registrador"];
                if (dr["Registrador"] != DBNull.Value)
                    o.Registrador = (string)dr["Registrador"];
                if (dr["t409_responsable"] != DBNull.Value)
					o.t409_responsable = (int)dr["t409_responsable"];
                if (dr["Responsable"] != DBNull.Value)
                    o.Responsable = (string)dr["Responsable"];
                if (dr["t409_severidad"] != DBNull.Value)
					o.t409_severidad = byte.Parse(dr["t409_severidad"].ToString());
                if (dr["Severidad"] != DBNull.Value)
                    o.Severidad = (string)dr["Severidad"];
                if (dr["t409_sistema"] != DBNull.Value)
					o.t409_sistema = (string)dr["t409_sistema"];
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
		/// Obtiene un catálogo de registros de la tabla T409_ASUNTOPT.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 15:01:06
		/// </history>
		/// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo(Nullable<int> t331_idpt, string t409_desasunto, Nullable<byte> t409_estado, Nullable<double> t409_etp, Nullable<double> t409_etr, Nullable<DateTime> t409_fcreacion, Nullable<DateTime> t409_ffin, Nullable<DateTime> t409_flimite, Nullable<DateTime> t409_fnotificacion, Nullable<int> t409_idasunto, string t409_notificador, Nullable<byte> t409_prioridad, string t409_refexterna, Nullable<int> t409_registrador, Nullable<int> t409_responsable, Nullable<byte> t409_severidad, string t409_sistema, Nullable<int> t384_idtipo, byte nOrden, byte nAscDesc)
		{
            string sProcAlm;
            SqlParameter[] aParam = new SqlParameter[20];
			aParam[0] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
			aParam[0].Value = t331_idpt;
			aParam[1] = new SqlParameter("@t409_desasunto", SqlDbType.Text, 50);
			aParam[1].Value = t409_desasunto;
			aParam[2] = new SqlParameter("@t409_estado", SqlDbType.TinyInt, 1);
            if (t409_estado == 0) aParam[2].Value = null;
			else aParam[2].Value = t409_estado;
            aParam[3] = new SqlParameter("@t409_etp", SqlDbType.Float, 8);
			aParam[3].Value = t409_etp;
            aParam[4] = new SqlParameter("@t409_etr", SqlDbType.Float, 8);
			aParam[4].Value = t409_etr;
			aParam[5] = new SqlParameter("@t409_fcreacion", SqlDbType.SmallDateTime, 4);
			aParam[5].Value = t409_fcreacion;
			aParam[6] = new SqlParameter("@t409_ffin", SqlDbType.SmallDateTime, 4);
			aParam[6].Value = t409_ffin;
			aParam[7] = new SqlParameter("@t409_flimite", SqlDbType.SmallDateTime, 4);
			aParam[7].Value = t409_flimite;
			aParam[8] = new SqlParameter("@t409_fnotificacion", SqlDbType.SmallDateTime, 4);
			aParam[8].Value = t409_fnotificacion;
			aParam[9] = new SqlParameter("@t409_idasunto", SqlDbType.Int, 4);
			aParam[9].Value = t409_idasunto;
			aParam[10] = new SqlParameter("@t409_notificador", SqlDbType.Text, 50);
			aParam[10].Value = t409_notificador;
			aParam[11] = new SqlParameter("@t409_prioridad", SqlDbType.TinyInt, 1);
			aParam[11].Value = t409_prioridad;
			aParam[12] = new SqlParameter("@t409_refexterna", SqlDbType.Text, 50);
			aParam[12].Value = t409_refexterna;
			aParam[13] = new SqlParameter("@t409_registrador", SqlDbType.Int, 4);
			aParam[13].Value = t409_registrador;
			aParam[14] = new SqlParameter("@t409_responsable", SqlDbType.Int, 4);
			aParam[14].Value = t409_responsable;
			aParam[15] = new SqlParameter("@t409_severidad", SqlDbType.TinyInt, 1);
			aParam[15].Value = t409_severidad;
			aParam[16] = new SqlParameter("@t409_sistema", SqlDbType.Text, 50);
			aParam[16].Value = t409_sistema;
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
                    if (nAscDesc == 0) sProcAlm = "SUP_ASUNTO_PT_C_ESTADO1";
                    else sProcAlm = "SUP_ASUNTO_PT_C_ESTADO2";
                    break;
                case 13:
                    if (nAscDesc == 0) sProcAlm = "SUP_ASUNTO_PT_C_PRIORIDAD1";
                    else sProcAlm = "SUP_ASUNTO_PT_C_PRIORIDAD2";
                    break;
                case 17:
                    if (nAscDesc == 0) sProcAlm = "SUP_ASUNTO_PT_C_SEVERIDAD1";
                    else sProcAlm = "SUP_ASUNTO_PT_C_SEVERIDAD2";
                    break;
                default:
                    sProcAlm="SUP_ASUNTO_PT_C";
                    break;
            }
			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader(sProcAlm, aParam);
		}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla T409_ASUNTOPT y T077_ACCION para la pantalla de consulta de Bitacora.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	28/01/2008 15:01:06
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo2(Nullable<int> t331_idpt, string t409_desasunto, 
                                            Nullable<byte> t409_estado,
                                            string t409_ffinD, string t409_ffinH,
                                            string t409_flimiteD, string t409_flimiteH,
                                            string t409_fnotificacionD, string t409_fnotificacionH, 
                                            Nullable<byte> t409_prioridad, 
                                            Nullable<byte> t409_severidad,
                                            Nullable<int> t384_idtipo, byte nOrden, byte nAscDesc, string sAcciones)
        {
            string sProcAlm;
            if (t409_ffinD == "") t409_ffinD = "01/01/1950";
            if (t409_flimiteD == "") t409_flimiteD = "01/01/1950";
            if (t409_fnotificacionD == "") t409_fnotificacionD = "01/01/1950";
            if (t409_ffinH == "") t409_ffinH = "31/12/2050";
            if (t409_flimiteH == "") t409_flimiteH = "31/12/2050";
            if (t409_fnotificacionH == "") t409_fnotificacionH = "31/12/2050";

            SqlParameter[] aParam = new SqlParameter[14];
            aParam[0] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
            aParam[0].Value = t331_idpt;
            aParam[1] = new SqlParameter("@t409_desasunto", SqlDbType.Text, 50);
            aParam[1].Value = t409_desasunto;
            aParam[2] = new SqlParameter("@t409_estado", SqlDbType.TinyInt, 1);
            if (t409_estado == 0) aParam[2].Value = null;
            else aParam[2].Value = t409_estado;

            aParam[3] = new SqlParameter("@ffinD", SqlDbType.VarChar, 10);
            aParam[3].Value = t409_ffinD;
            aParam[4] = new SqlParameter("@ffinH", SqlDbType.VarChar, 10);
            aParam[4].Value = t409_ffinH;

            aParam[5] = new SqlParameter("@flimiteD", SqlDbType.VarChar, 10);
            aParam[5].Value = t409_flimiteD;
            aParam[6] = new SqlParameter("@flimiteH", SqlDbType.VarChar, 10);
            aParam[6].Value = t409_flimiteH;

            aParam[7] = new SqlParameter("@fnotificacionD", SqlDbType.VarChar, 10);
            aParam[7].Value = t409_fnotificacionD;
            aParam[8] = new SqlParameter("@fnotificacionH", SqlDbType.VarChar, 10);
            aParam[8].Value = t409_fnotificacionH;

            aParam[9] = new SqlParameter("@t409_prioridad", SqlDbType.TinyInt, 1);
            if (t409_prioridad == 0) aParam[9].Value = null;
            else aParam[9].Value = t409_prioridad;

            aParam[10] = new SqlParameter("@t409_severidad", SqlDbType.TinyInt, 1);
            if (t409_severidad == 0) aParam[10].Value = null;
            else aParam[10].Value = t409_severidad;

            aParam[11] = new SqlParameter("@t384_idtipo", SqlDbType.Int, 4);
            if (t384_idtipo == 0) aParam[11].Value = null;
            else aParam[11].Value = t384_idtipo;

            aParam[12] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
            aParam[12].Value = nOrden;
            aParam[13] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
            aParam[13].Value = nAscDesc;

            if (sAcciones == "N")
                sProcAlm = "SUP_BIT_AS_PT";
            else
                sProcAlm = "SUP_BIT_AS_PTyAC";

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader(sProcAlm, aParam);
        }
        #endregion
	}
}
