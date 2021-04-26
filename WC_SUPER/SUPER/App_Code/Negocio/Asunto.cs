using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
//using System.Web;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : ASUNTO
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T382_ASUNTO
	/// </summary>
	/// <history>
	/// 	Creado por [DOARHUMI]	15/11/2007 15:01:06	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class ASUNTO
	{
		#region Propiedades y Atributos

		private short _t303_idnodo;
		public short t303_idnodo
		{
			get {return _t303_idnodo;}
			set { _t303_idnodo = value ;}
		}

        private int _t301_idproyecto;
        public int t301_idproyecto
        {
            get { return _t301_idproyecto; }
            set { _t301_idproyecto = value; }
        }

        private int _t305_idproyectosubnodo;
        public int t305_idproyectosubnodo
        {
            get { return _t305_idproyectosubnodo; }
            set { _t305_idproyectosubnodo = value; }
        }

        private string _t382_alerta;
		public string t382_alerta
		{
			get {return _t382_alerta;}
			set { _t382_alerta = value ;}
		}

		private string _t382_desasunto;
		public string t382_desasunto
		{
			get {return _t382_desasunto;}
			set { _t382_desasunto = value ;}
		}

		private string _t382_desasuntolong;
		public string t382_desasuntolong
		{
			get {return _t382_desasuntolong;}
			set { _t382_desasuntolong = value ;}
		}

		private string _t382_dpto;
		public string t382_dpto
		{
			get {return _t382_dpto;}
			set { _t382_dpto = value ;}
		}

        private byte _t382_estado;
        public byte t382_estado
        {
            get { return _t382_estado; }
            set { _t382_estado = value; }
        }
        private string _Estado;
        public string Estado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }

        private double _t382_etp;
        public double t382_etp
		{
			get {return _t382_etp;}
			set { _t382_etp = value ;}
		}

        private double _t382_etr;
        public double t382_etr
		{
			get {return _t382_etr;}
			set { _t382_etr = value ;}
		}

		private DateTime _t382_fcreacion;
		public DateTime t382_fcreacion
		{
			get {return _t382_fcreacion;}
			set { _t382_fcreacion = value ;}
		}

		private DateTime _t382_ffin;
		public DateTime t382_ffin
		{
			get {return _t382_ffin;}
			set { _t382_ffin = value ;}
		}

		private DateTime _t382_flimite;
		public DateTime t382_flimite
		{
			get {return _t382_flimite;}
			set { _t382_flimite = value ;}
		}

		private DateTime _t382_fnotificacion;
		public DateTime t382_fnotificacion
		{
			get {return _t382_fnotificacion;}
			set { _t382_fnotificacion = value ;}
		}

		private int _t382_idasunto;
		public int t382_idasunto
		{
			get {return _t382_idasunto;}
			set { _t382_idasunto = value ;}
		}

		private string _t382_notificador;
		public string t382_notificador
		{
			get {return _t382_notificador;}
			set { _t382_notificador = value ;}
		}

		private string _t382_obs;
		public string t382_obs
		{
			get {return _t382_obs;}
			set { _t382_obs = value ;}
		}

        private byte _t382_prioridad;
        public byte t382_prioridad
        {
            get { return _t382_prioridad; }
            set { _t382_prioridad = value; }
        }
        private string _Prioridad;
        public string Prioridad
        {
            get { return _Prioridad; }
            set { _Prioridad = value; }
        }

		private string _t382_refexterna;
		public string t382_refexterna
		{
			get {return _t382_refexterna;}
			set { _t382_refexterna = value ;}
		}

        private int _t382_registrador;
        public int t382_registrador
        {
            get { return _t382_registrador; }
            set { _t382_registrador = value; }
        }
        private string _Registrador;
        public string Registrador
        {
            get { return _Registrador; }
            set { _Registrador = value; }
        }

		private int _t382_responsable;
		public int t382_responsable
		{
			get {return _t382_responsable;}
			set { _t382_responsable = value ;}
		}
        private string _Responsable;
        public string Responsable
        {
            get { return _Responsable; }
            set { _Responsable = value; }
        }

        private byte _t382_severidad;
        public byte t382_severidad
        {
            get { return _t382_severidad; }
            set { _t382_severidad = value; }
        }
        private string _Severidad;
        public string Severidad
        {
            get { return _Severidad; }
            set { _Severidad = value; }
        }

		private string _t382_sistema;
		public string t382_sistema
		{
			get {return _t382_sistema;}
			set { _t382_sistema = value ;}
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

		public ASUNTO() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T382_ASUNTO.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	15/11/2007 15:01:06
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, int t305_idproyectosubnodo, string t382_alerta, string t382_desasunto, 
                                string t382_desasuntolong, string t382_dpto, byte t382_estado, double t382_etp, double t382_etr, 
                                Nullable<DateTime> t382_ffin, Nullable<DateTime> t382_flimite, Nullable<DateTime> t382_fnotificacion, 
                                string t382_notificador, string t382_obs, byte t382_prioridad, string t382_refexterna, int t382_registrador, 
                                int t382_responsable, byte t382_severidad, string t382_sistema, int t384_idtipo)
		{
			SqlParameter[] aParam = new SqlParameter[20];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;
			aParam[1] = new SqlParameter("@t382_alerta", SqlDbType.Text, 2147483647);
			aParam[1].Value = t382_alerta;
			aParam[2] = new SqlParameter("@t382_desasunto", SqlDbType.Text, 50);
			aParam[2].Value = t382_desasunto;
			aParam[3] = new SqlParameter("@t382_desasuntolong", SqlDbType.Text, 2147483647);
			aParam[3].Value = t382_desasuntolong;
			aParam[4] = new SqlParameter("@t382_dpto", SqlDbType.Text, 2147483647);
			aParam[4].Value = t382_dpto;
			aParam[5] = new SqlParameter("@t382_estado", SqlDbType.TinyInt, 1);
			aParam[5].Value = t382_estado;
            aParam[6] = new SqlParameter("@t382_etp", SqlDbType.Float, 8);
			aParam[6].Value = t382_etp;
            aParam[7] = new SqlParameter("@t382_etr", SqlDbType.Float, 8);
			aParam[7].Value = t382_etr;
			aParam[8] = new SqlParameter("@t382_ffin", SqlDbType.SmallDateTime, 4);
			aParam[8].Value = t382_ffin;
			aParam[9] = new SqlParameter("@t382_flimite", SqlDbType.SmallDateTime, 4);
			aParam[9].Value = t382_flimite;
			aParam[10] = new SqlParameter("@t382_fnotificacion", SqlDbType.SmallDateTime, 4);
			aParam[10].Value = t382_fnotificacion;
			aParam[11] = new SqlParameter("@t382_notificador", SqlDbType.Text, 50);
			aParam[11].Value = t382_notificador;
			aParam[12] = new SqlParameter("@t382_obs", SqlDbType.Text, 2147483647);
			aParam[12].Value = t382_obs;
			aParam[13] = new SqlParameter("@t382_prioridad", SqlDbType.TinyInt, 1);
			aParam[13].Value = t382_prioridad;
			aParam[14] = new SqlParameter("@t382_refexterna", SqlDbType.Text, 50);
			aParam[14].Value = t382_refexterna;
			aParam[15] = new SqlParameter("@t382_registrador", SqlDbType.Int, 4);
			aParam[15].Value = t382_registrador;
			aParam[16] = new SqlParameter("@t382_responsable", SqlDbType.Int, 4);
			aParam[16].Value = t382_responsable;
			aParam[17] = new SqlParameter("@t382_severidad", SqlDbType.TinyInt, 1);
			aParam[17].Value = t382_severidad;
			aParam[18] = new SqlParameter("@t382_sistema", SqlDbType.Text, 50);
			aParam[18].Value = t382_sistema;
			aParam[19] = new SqlParameter("@t384_idtipo", SqlDbType.Int, 4);
			aParam[19].Value = t384_idtipo;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_ASUNTO_I", aParam));
			else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_ASUNTO_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T382_ASUNTO.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	15/11/2007 15:01:06
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, string t382_alerta,
            string t382_desasunto,
            string t382_desasuntolong,
            string t382_dpto,
            byte t382_estado,
            double t382_etp,
            double t382_etr,
            Nullable<DateTime> t382_ffin,
            Nullable<DateTime> t382_flimite,
            DateTime t382_fnotificacion,
            int t382_idasunto,
            string t382_notificador,
            string t382_obs,
            byte t382_prioridad,
            string t382_refexterna,
            int t382_responsable,
            byte t382_severidad,
            string t382_sistema,
            int t384_idtipo)
        {
			SqlParameter[] aParam = new SqlParameter[19];
			aParam[0] = new SqlParameter("@t382_alerta", SqlDbType.Text, 2147483647);
			aParam[0].Value = t382_alerta;
			aParam[1] = new SqlParameter("@t382_desasunto", SqlDbType.Text, 50);
			aParam[1].Value = t382_desasunto;
			aParam[2] = new SqlParameter("@t382_desasuntolong", SqlDbType.Text, 2147483647);
			aParam[2].Value = t382_desasuntolong;
			aParam[3] = new SqlParameter("@t382_dpto", SqlDbType.Text, 2147483647);
			aParam[3].Value = t382_dpto;
			aParam[4] = new SqlParameter("@t382_estado", SqlDbType.TinyInt, 1);
			aParam[4].Value = t382_estado;
            aParam[5] = new SqlParameter("@t382_etp", SqlDbType.Float, 8);
			aParam[5].Value = t382_etp;
            aParam[6] = new SqlParameter("@t382_etr", SqlDbType.Float, 8);
			aParam[6].Value = t382_etr;
			aParam[7] = new SqlParameter("@t382_ffin", SqlDbType.SmallDateTime, 4);
			aParam[7].Value = t382_ffin;
			aParam[8] = new SqlParameter("@t382_flimite", SqlDbType.SmallDateTime, 4);
			aParam[8].Value = t382_flimite;
			aParam[9] = new SqlParameter("@t382_fnotificacion", SqlDbType.SmallDateTime, 4);
			aParam[9].Value = t382_fnotificacion;
			aParam[10] = new SqlParameter("@t382_idasunto", SqlDbType.Int, 4);
			aParam[10].Value = t382_idasunto;
			aParam[11] = new SqlParameter("@t382_notificador", SqlDbType.Text, 50);
			aParam[11].Value = t382_notificador;
			aParam[12] = new SqlParameter("@t382_obs", SqlDbType.Text, 2147483647);
			aParam[12].Value = t382_obs;
			aParam[13] = new SqlParameter("@t382_prioridad", SqlDbType.TinyInt, 1);
			aParam[13].Value = t382_prioridad;
			aParam[14] = new SqlParameter("@t382_refexterna", SqlDbType.Text, 50);
			aParam[14].Value = t382_refexterna;
            aParam[15] = new SqlParameter("@t382_responsable", SqlDbType.Int, 4);
            aParam[15].Value = t382_responsable;
            aParam[16] = new SqlParameter("@t382_severidad", SqlDbType.TinyInt, 1);
			aParam[16].Value = t382_severidad;
			aParam[17] = new SqlParameter("@t382_sistema", SqlDbType.Text, 50);
			aParam[17].Value = t382_sistema;
			aParam[18] = new SqlParameter("@t384_idtipo", SqlDbType.Int, 4);
			aParam[18].Value = t384_idtipo;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_ASUNTO_U", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ASUNTO_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T382_ASUNTO a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	15/11/2007 15:01:06
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t382_idasunto)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t382_idasunto", SqlDbType.Int, 4);
			aParam[0].Value = t382_idasunto;

			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_ASUNTO_D", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ASUNTO_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T382_ASUNTO,
		/// y devuelve una instancia u objeto del tipo ASUNTO
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	15/11/2007 15:01:06
		/// </history>
		/// -----------------------------------------------------------------------------
		public static ASUNTO Select(SqlTransaction tr, int t382_idasunto) 
		{
			ASUNTO o = new ASUNTO();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t382_idasunto", SqlDbType.Int, 4);
			aParam[0].Value = t382_idasunto;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_ASUNTO_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_ASUNTO_S", aParam);

			if (dr.Read())
			{
				if (dr["t303_idnodo"] != DBNull.Value)
					o.t303_idnodo = short.Parse(dr["t303_idnodo"].ToString());
                if (dr["t301_idproyecto"] != DBNull.Value)
                    o.t301_idproyecto = (int)dr["t301_idproyecto"];
                if (dr["t305_idproyectosubnodo"] != DBNull.Value)
                    o.t305_idproyectosubnodo = (int)dr["t305_idproyectosubnodo"];
                if (dr["t382_alerta"] != DBNull.Value)
					o.t382_alerta = (string)dr["t382_alerta"];
				if (dr["t382_desasunto"] != DBNull.Value)
					o.t382_desasunto = (string)dr["t382_desasunto"];
				if (dr["t382_desasuntolong"] != DBNull.Value)
					o.t382_desasuntolong = (string)dr["t382_desasuntolong"];
				if (dr["t382_dpto"] != DBNull.Value)
					o.t382_dpto = (string)dr["t382_dpto"];
				if (dr["t382_estado"] != DBNull.Value)
					o.t382_estado = byte.Parse(dr["t382_estado"].ToString());
                if (dr["Estado"] != DBNull.Value)
                    o.Estado = (string)dr["Estado"];
                if (dr["t382_etp"] != DBNull.Value)
					o.t382_etp = (double)dr["t382_etp"];
				if (dr["t382_etr"] != DBNull.Value)
                    o.t382_etr = (double)dr["t382_etr"];
				if (dr["t382_fcreacion"] != DBNull.Value)
					o.t382_fcreacion = (DateTime)dr["t382_fcreacion"];
				if (dr["t382_ffin"] != DBNull.Value)
					o.t382_ffin = (DateTime)dr["t382_ffin"];
				if (dr["t382_flimite"] != DBNull.Value)
					o.t382_flimite = (DateTime)dr["t382_flimite"];
				if (dr["t382_fnotificacion"] != DBNull.Value)
					o.t382_fnotificacion = (DateTime)dr["t382_fnotificacion"];
				if (dr["t382_idasunto"] != DBNull.Value)
					o.t382_idasunto = (int)dr["t382_idasunto"];
				if (dr["t382_notificador"] != DBNull.Value)
					o.t382_notificador = (string)dr["t382_notificador"];
				if (dr["t382_obs"] != DBNull.Value)
					o.t382_obs = (string)dr["t382_obs"];
				if (dr["t382_prioridad"] != DBNull.Value)
					o.t382_prioridad = byte.Parse(dr["t382_prioridad"].ToString());
                if (dr["Prioridad"] != DBNull.Value)
                    o.Prioridad = (string)dr["Prioridad"];
                if (dr["t382_refexterna"] != DBNull.Value)
                    o.t382_refexterna = (string)dr["t382_refexterna"];
                if (dr["t382_registrador"] != DBNull.Value)
					o.t382_registrador = (int)dr["t382_registrador"];
                if (dr["Registrador"] != DBNull.Value)
                    o.Registrador = (string)dr["Registrador"];
                if (dr["t382_responsable"] != DBNull.Value)
					o.t382_responsable = (int)dr["t382_responsable"];
                if (dr["Responsable"] != DBNull.Value)
                    o.Responsable = (string)dr["Responsable"];
                if (dr["t382_severidad"] != DBNull.Value)
					o.t382_severidad = byte.Parse(dr["t382_severidad"].ToString());
                if (dr["Severidad"] != DBNull.Value)
                    o.Severidad = (string)dr["Severidad"];
                if (dr["t382_sistema"] != DBNull.Value)
					o.t382_sistema = (string)dr["t382_sistema"];
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
		/// Obtiene un catálogo de registros de la tabla T382_ASUNTO.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	15/11/2007 15:01:06
		/// </history>
		/// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo(int t305_idproyectosubnodo, string t382_desasunto, Nullable<byte> t382_estado, Nullable<double> t382_etp, Nullable<double> t382_etr, Nullable<DateTime> t382_fcreacion, Nullable<DateTime> t382_ffin, Nullable<DateTime> t382_flimite, Nullable<DateTime> t382_fnotificacion, Nullable<int> t382_idasunto, string t382_notificador, Nullable<byte> t382_prioridad, string t382_refexterna, Nullable<int> t382_registrador, Nullable<int> t382_responsable, Nullable<byte> t382_severidad, string t382_sistema, Nullable<int> t384_idtipo, byte nOrden, byte nAscDesc)
		{
            string sProcAlm;
            SqlParameter[] aParam = new SqlParameter[20];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;
			aParam[1] = new SqlParameter("@t382_desasunto", SqlDbType.Text, 50);
			aParam[1].Value = t382_desasunto;
			aParam[2] = new SqlParameter("@t382_estado", SqlDbType.TinyInt, 1);
            if (t382_estado == 0) aParam[2].Value = null;
			else aParam[2].Value = t382_estado;
            aParam[3] = new SqlParameter("@t382_etp", SqlDbType.Float, 8);
			aParam[3].Value = t382_etp;
            aParam[4] = new SqlParameter("@t382_etr", SqlDbType.Float, 8);
			aParam[4].Value = t382_etr;
			aParam[5] = new SqlParameter("@t382_fcreacion", SqlDbType.SmallDateTime, 4);
			aParam[5].Value = t382_fcreacion;
			aParam[6] = new SqlParameter("@t382_ffin", SqlDbType.SmallDateTime, 4);
			aParam[6].Value = t382_ffin;
			aParam[7] = new SqlParameter("@t382_flimite", SqlDbType.SmallDateTime, 4);
			aParam[7].Value = t382_flimite;
			aParam[8] = new SqlParameter("@t382_fnotificacion", SqlDbType.SmallDateTime, 4);
			aParam[8].Value = t382_fnotificacion;
			aParam[9] = new SqlParameter("@t382_idasunto", SqlDbType.Int, 4);
			aParam[9].Value = t382_idasunto;
			aParam[10] = new SqlParameter("@t382_notificador", SqlDbType.Text, 50);
			aParam[10].Value = t382_notificador;
			aParam[11] = new SqlParameter("@t382_prioridad", SqlDbType.TinyInt, 1);
			aParam[11].Value = t382_prioridad;
			aParam[12] = new SqlParameter("@t382_refexterna", SqlDbType.Text, 50);
			aParam[12].Value = t382_refexterna;
			aParam[13] = new SqlParameter("@t382_registrador", SqlDbType.Int, 4);
			aParam[13].Value = t382_registrador;
			aParam[14] = new SqlParameter("@t382_responsable", SqlDbType.Int, 4);
			aParam[14].Value = t382_responsable;
			aParam[15] = new SqlParameter("@t382_severidad", SqlDbType.TinyInt, 1);
			aParam[15].Value = t382_severidad;
			aParam[16] = new SqlParameter("@t382_sistema", SqlDbType.Text, 50);
			aParam[16].Value = t382_sistema;
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
                    if (nAscDesc == 0) sProcAlm = "SUP_ASUNTO_C_ESTADO1";
                    else sProcAlm = "SUP_ASUNTO_C_ESTADO2";
                    break;
                case 13:
                    if (nAscDesc == 0) sProcAlm = "SUP_ASUNTO_C_PRIORIDAD1";
                    else sProcAlm = "SUP_ASUNTO_C_PRIORIDAD2";
                    break;
                case 17:
                    if (nAscDesc == 0) sProcAlm = "SUP_ASUNTO_C_SEVERIDAD1";
                    else sProcAlm = "SUP_ASUNTO_C_SEVERIDAD2";
                    break;
                default:
                    sProcAlm="SUP_ASUNTO_C";
                    break;
            }
			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader(sProcAlm, aParam);
		}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla T382_ASUNTO y T383_ACCION para la pantalla de consulta de Bitacora.
        /// </summary>
        /// <history>
        /// 	Creado por [DOARHUMI]	15/11/2007 15:01:06
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo2(int t305_idproyectosubnodo, string t382_desasunto, 
                                            Nullable<byte> t382_estado,
                                            string t382_ffinD, string t382_ffinH,
                                            string t382_flimiteD, string t382_flimiteH,
                                            string t382_fnotificacionD, string t382_fnotificacionH, 
                                            Nullable<byte> t382_prioridad, 
                                            Nullable<byte> t382_severidad,
                                            Nullable<int> t384_idtipo, byte nOrden, byte nAscDesc, string sAcciones)
        {
            string sProcAlm;
            if (t382_ffinD == "") t382_ffinD = "01/01/1950";
            if (t382_flimiteD == "") t382_flimiteD = "01/01/1950";
            if (t382_fnotificacionD == "") t382_fnotificacionD = "01/01/1950";
            if (t382_ffinH == "") t382_ffinH = "31/12/2050";
            if (t382_flimiteH == "") t382_flimiteH = "31/12/2050";
            if (t382_fnotificacionH == "") t382_fnotificacionH = "31/12/2050";

            SqlParameter[] aParam = new SqlParameter[14];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;
            aParam[1] = new SqlParameter("@t382_desasunto", SqlDbType.Text, 50);
            aParam[1].Value = t382_desasunto;
            aParam[2] = new SqlParameter("@t382_estado", SqlDbType.TinyInt, 1);
            if (t382_estado == 0) aParam[2].Value = null;
            else aParam[2].Value = t382_estado;

            aParam[3] = new SqlParameter("@ffinD", SqlDbType.VarChar, 10);
            aParam[3].Value = t382_ffinD;
            aParam[4] = new SqlParameter("@ffinH", SqlDbType.VarChar, 10);
            aParam[4].Value = t382_ffinH;

            aParam[5] = new SqlParameter("@flimiteD", SqlDbType.VarChar, 10);
            aParam[5].Value = t382_flimiteD;
            aParam[6] = new SqlParameter("@flimiteH", SqlDbType.VarChar, 10);
            aParam[6].Value = t382_flimiteH;

            aParam[7] = new SqlParameter("@fnotificacionD", SqlDbType.VarChar, 10);
            aParam[7].Value = t382_fnotificacionD;
            aParam[8] = new SqlParameter("@fnotificacionH", SqlDbType.VarChar, 10);
            aParam[8].Value = t382_fnotificacionH;

            aParam[9] = new SqlParameter("@t382_prioridad", SqlDbType.TinyInt, 1);
            if (t382_prioridad == 0) aParam[9].Value = null;
            else aParam[9].Value = t382_prioridad;

            aParam[10] = new SqlParameter("@t382_severidad", SqlDbType.TinyInt, 1);
            if (t382_severidad == 0) aParam[10].Value = null;
            else aParam[10].Value = t382_severidad;

            aParam[11] = new SqlParameter("@t384_idtipo", SqlDbType.Int, 4);
            if (t384_idtipo == 0) aParam[11].Value = null;
            else aParam[11].Value = t384_idtipo;

            aParam[12] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
            aParam[12].Value = nOrden;
            aParam[13] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
            aParam[13].Value = nAscDesc;

            if (sAcciones == "N")
                sProcAlm = "SUP_BIT_AS";
            else
                sProcAlm = "SUP_BIT_ASyAC";

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader(sProcAlm, aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene las bitacoras de los proyectos indicados 
		///				Incluye tanbién las bitacoras asociadas a los proyectos técnicos y tareas de los proyectos seleccionados
        /// </summary>
        /// <history>
        /// 	Creado por [DOARHUMI]	15/11/2007 15:01:06
        /// </history>
        /// -----------------------------------------------------------------------------
        /// public static SqlDataReader Masivo(DateTime dtFechaFesde, DateTime dtFechaHasta, string slProyectos)
        public static SqlDataReader Masivo(string slProyectos)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@sProyectos", SqlDbType.VarChar, 8000);
            aParam[0].Value = slProyectos;
            //No hace falta discriminar pues en la lista de proyectos ya se ha aplicado el filtro de cuales son accesibles por el usuario
            return SqlHelper.ExecuteSqlDataReader("SUP_BITACORA_MASIVO", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene las bitacoras de los proyectos técnicos y tareas de un proyecto
        /// Si el usuario es solo RTPT se sacaran solo aquellos PTs de los que es responsable
        /// </summary>
        /// <history>
        /// 	Creado por [DOARHUMI]	15/11/2007 15:01:06
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader MasivoPT(int idPSN, int idUser, bool bSoloRTPT)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = idPSN;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = idUser;
            aParam[2] = new SqlParameter("@bSoloRTPT", SqlDbType.Bit, 1);
            aParam[2].Value = bSoloRTPT;

            return SqlHelper.ExecuteSqlDataReader("SUP_BITACORA_MASIVO_PT", aParam);
        }

        public static string GetResponsable(int t382_idasunto)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t382_idasunto", SqlDbType.Int, 4, t382_idasunto)
            };
            object res = SqlHelper.ExecuteScalar("SUP_ASUNTO_RESPONSABLE", aParam);
            if (res == null)
                return "";
            else
                return res.ToString();
        }
        #endregion
	}
}
