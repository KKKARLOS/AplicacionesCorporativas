using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.DAL
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : EXPFICEPIPERFIL
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T813_EXPFICEPIPERFIL
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	28/08/2012 10:28:59	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class EXPFICEPIPERFIL
	{
		#region Propiedades y Atributos

		private int _t813_idexpficepiperfil;
		public int t813_idexpficepiperfil
		{
			get {return _t813_idexpficepiperfil;}
			set { _t813_idexpficepiperfil = value ;}
		}

		private DateTime? _t813_finicio;
		public DateTime? t813_finicio
		{
			get {return _t813_finicio;}
			set { _t813_finicio = value ;}
		}

		private DateTime? _t813_ffin;
		public DateTime? t813_ffin
		{
			get {return _t813_ffin;}
			set { _t813_ffin = value ;}
		}

		private string _t813_funcion;
		public string t813_funcion
		{
			get {return _t813_funcion;}
			set { _t813_funcion = value ;}
		}

		private string _t813_observa;
		public string t813_observa
		{
			get {return _t813_observa;}
			set { _t813_observa = value ;}
		}

		private string _t839_idestado;
		public string t839_idestado
		{
            get { return _t839_idestado; }
            set { _t839_idestado = value; }
		}

		private string _t838_motivort;
		public string t838_motivort
		{
			get {return _t838_motivort;}
			set { _t838_motivort = value ;}
		}

		private DateTime _t813_fechau;
		public DateTime t813_fechau
		{
			get {return _t813_fechau;}
			set { _t813_fechau = value ;}
		}

		private int _t035_idcodperfil;
		public int t035_idcodperfil
		{
			get {return _t035_idcodperfil;}
			set { _t035_idcodperfil = value ;}
		}

		private int _t812_idexpprofficepi;
		public int t812_idexpprofficepi
		{
			get {return _t812_idexpprofficepi;}
			set { _t812_idexpprofficepi = value ;}
		}

		private int _t001_idficepiu;
		public int t001_idficepiu
		{
			get {return _t001_idficepiu;}
			set { _t001_idficepiu = value ;}
		}
        private string _dPrimerConsumo;
        public string dPrimerConsumo
        {
            get { return _dPrimerConsumo; }
            set { _dPrimerConsumo = value; }
        }
        private string _dUltimoConsumo;
        public string dUltimoConsumo
        {
            get { return _dUltimoConsumo; }
            set { _dUltimoConsumo = value; }
        }
        private string _esfuerzoenjor;
        public string esfuerzoenjor
        {
            get { return _esfuerzoenjor; }
            set { _esfuerzoenjor = value; }
        }
        private short _t020_idcodidioma;
        public short t020_idcodidioma
        {
            get { return _t020_idcodidioma; }
            set { _t020_idcodidioma = value; }
        }

        #endregion

		#region Constructor

		public EXPFICEPIPERFIL() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T813_EXPFICEPIPERFIL.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	28/08/2012 10:28:59
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Insert(SqlTransaction tr, Nullable<DateTime> t813_finicio , Nullable<DateTime> t813_ffin , string t813_funcion , 
                                string t813_observa , string t839_idestado , string t838_motivort , DateTime t813_fechau , int t035_idcodperfil ,
                                int t812_idexpprofficepi, int t001_idficepiu, Nullable<int> profCvExc, Nullable<int> respCvExc,
                                short t020_idcodidioma, int t819_idplantillacvt)
		{
			SqlParameter[] aParam = new SqlParameter[14];
			int i = 0;
			aParam[i++] = ParametroSql.add("@t813_finicio", SqlDbType.SmallDateTime, 4, t813_finicio);
			aParam[i++] = ParametroSql.add("@t813_ffin", SqlDbType.SmallDateTime, 4, t813_ffin);
			aParam[i++] = ParametroSql.add("@t813_funcion", SqlDbType.Text, 2147483647, t813_funcion);
			aParam[i++] = ParametroSql.add("@t813_observa", SqlDbType.Text, 2147483647, t813_observa);
            aParam[i++] = ParametroSql.add("@t839_idestado", SqlDbType.Text, 1, t839_idestado);
            aParam[i++] = ParametroSql.add("@t838_motivort", SqlDbType.Text, 2147483647, t838_motivort);
			aParam[i++] = ParametroSql.add("@t813_fechau", SqlDbType.SmallDateTime, 4, t813_fechau);
            aParam[i++] = ParametroSql.add("@t035_idcodperfil", SqlDbType.Int, 4, (t035_idcodperfil == -1) ? null : (int?)t035_idcodperfil);
			aParam[i++] = ParametroSql.add("@t812_idexpprofficepi", SqlDbType.Int, 4, t812_idexpprofficepi);
			aParam[i++] = ParametroSql.add("@t001_idficepiu", SqlDbType.Int, 4, t001_idficepiu);
            aParam[i++] = ParametroSql.add("@profCvExc", SqlDbType.Int, 4, profCvExc);
            aParam[i++] = ParametroSql.add("@respCvExc", SqlDbType.Int, 4, respCvExc);
            aParam[i++] = ParametroSql.add("@t020_idcodidioma", SqlDbType.Int, 2, t020_idcodidioma);
            aParam[i++] = ParametroSql.add("@t819_idplantillacvt", SqlDbType.Int, 4, t819_idplantillacvt);

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
				return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_CVT_EXPFICEPIPERFIL_I", aParam));
			else
				return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CVT_EXPFICEPIPERFIL_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T813_EXPFICEPIPERFIL.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	28/08/2012 10:28:59
		/// </history>
		/// -----------------------------------------------------------------------------
        //public static int Update(SqlTransaction tr, int t813_idexpficepiperfil, DateTime t813_finicio, Nullable<DateTime> t813_ffin, string t813_funcion, string t813_observa, string t839_idestado, string t813_motivort, DateTime t813_fechau, int t035_idcodperfil, int t812_idexpprofficepi, int t001_idficepiu)
        //{
        //    SqlParameter[] aParam = new SqlParameter[11];
        //    int i = 0;
        //    aParam[i++] = ParametroSql.add("@t813_idexpficepiperfil", SqlDbType.Int, 4, t813_idexpficepiperfil);
        //    aParam[i++] = ParametroSql.add("@t813_finicio", SqlDbType.SmallDateTime, 4, t813_finicio);
        //    aParam[i++] = ParametroSql.add("@t813_ffin", SqlDbType.SmallDateTime, 4, t813_ffin);
        //    aParam[i++] = ParametroSql.add("@t813_funcion", SqlDbType.Text, 2147483647, t813_funcion);
        //    aParam[i++] = ParametroSql.add("@t813_observa", SqlDbType.Text, 2147483647, t813_observa);
        //    aParam[i++] = ParametroSql.add("@t839_idestado", SqlDbType.Text, 1, t839_idestado);
        //    aParam[i++] = ParametroSql.add("@t813_motivort", SqlDbType.Text, 2147483647, t813_motivort);
        //    aParam[i++] = ParametroSql.add("@t813_fechau", SqlDbType.SmallDateTime, 4, t813_fechau);
        //    aParam[i++] = ParametroSql.add("@t035_idcodperfil", SqlDbType.Int, 4, t035_idcodperfil);
        //    aParam[i++] = ParametroSql.add("@t812_idexpprofficepi", SqlDbType.Int, 4, t812_idexpprofficepi);
        //    aParam[i++] = ParametroSql.add("@t001_idficepiu", SqlDbType.Int, 4, t001_idficepiu);

        //    // Ejecuta la query y devuelve el numero de registros modificados.
        //    if (tr == null)
        //        return SqlHelper.ExecuteNonQuery("SUP_CVT_EXPFICEPIPERFIL_U", aParam);
        //    else
        //        return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_EXPFICEPIPERFIL_U", aParam);
        //}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T813_EXPFICEPIPERFIL a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	28/08/2012 10:28:59
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t813_idexpficepiperfil)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			int i = 0;
			aParam[i++] = ParametroSql.add("@t813_idexpficepiperfil", SqlDbType.Int, 4, t813_idexpficepiperfil);

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_CVT_EXPFICEPIPERFIL_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_EXPFICEPIPERFIL_D", aParam);
		}


        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Update de la tabla T812_EXPPROFFICEPI, desasignando la plantilla que tuviera asignado 
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	28/08/2012 10:28:59
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int DesasignarPlantilla(SqlTransaction tr, int t812_idexpprofficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t812_idexpprofficepi", SqlDbType.Int, 4, t812_idexpprofficepi);

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CVT_EXPPROFFICEPI_SINPLANT", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_EXPPROFFICEPI_SINPLANT", aParam);
        }


		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T813_EXPFICEPIPERFIL,
		/// y devuelve una instancia u objeto del tipo EXPFICEPIPERFIL
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	28/08/2012 10:28:59
		/// </history>
		/// -----------------------------------------------------------------------------
		public static EXPFICEPIPERFIL Select(SqlTransaction tr, int t813_idexpficepiperfil) 
		{
			EXPFICEPIPERFIL o = new EXPFICEPIPERFIL();

			SqlParameter[] aParam = new SqlParameter[1];
			int i = 0;
			aParam[i++] = ParametroSql.add("@t813_idexpficepiperfil", SqlDbType.Int, 4, t813_idexpficepiperfil);

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_CVT_EXPFICEPIPERFIL_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_EXPFICEPIPERFIL_S", aParam);

			if (dr.Read())
			{
				if (dr["t813_idexpficepiperfil"] != DBNull.Value)
					o.t813_idexpficepiperfil = int.Parse(dr["t813_idexpficepiperfil"].ToString());
				if (dr["t813_finicio"] != DBNull.Value)
					o.t813_finicio = (DateTime)dr["t813_finicio"];
				if (dr["t813_ffin"] != DBNull.Value)
					o.t813_ffin = (DateTime)dr["t813_ffin"];
				if (dr["t813_funcion"] != DBNull.Value)
					o.t813_funcion = (string)dr["t813_funcion"];
				if (dr["t813_observa"] != DBNull.Value)
					o.t813_observa = (string)dr["t813_observa"];
				if (dr["t839_idestado"] != DBNull.Value)
                    o.t839_idestado = (string)dr["t839_idestado"];
				if (dr["t838_motivort"] != DBNull.Value)
					o.t838_motivort = (string)dr["t838_motivort"];
				if (dr["t813_fechau"] != DBNull.Value)
					o.t813_fechau = (DateTime)dr["t813_fechau"];
                if (dr["t035_idcodperfil"] != DBNull.Value)
                    o.t035_idcodperfil = int.Parse(dr["t035_idcodperfil"].ToString());
                else
                    o.t035_idcodperfil = -1;
				if (dr["t812_idexpprofficepi"] != DBNull.Value)
					o.t812_idexpprofficepi = int.Parse(dr["t812_idexpprofficepi"].ToString());
				if (dr["t001_idficepiu"] != DBNull.Value)
					o.t001_idficepiu = int.Parse(dr["t001_idficepiu"].ToString());
                if (dr["t020_idcodidioma"] != DBNull.Value)
                    o.t020_idcodidioma = short.Parse(dr["t020_idcodidioma"].ToString());
            }
            else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de EXPFICEPIPERFIL"));
			}
			dr.Close();
			dr.Dispose();

			return o;
		}

		#endregion
	}
}
