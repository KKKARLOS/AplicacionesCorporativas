using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : CONEXIONES
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T459_CONEXIONES
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	25/02/2010 15:10:54	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class CONEXIONES
	{
		#region Propiedades y Atributos

		private DateTime _t459_fecconect;
		public DateTime t459_fecconect
		{
			get {return _t459_fecconect;}
			set { _t459_fecconect = value ;}
		}

		private int _t314_idusuario_conect;
		public int t314_idusuario_conect
		{
			get {return _t314_idusuario_conect;}
			set { _t314_idusuario_conect = value ;}
		}

		private int? _t314_idusuario_usurpa;
		public int? t314_idusuario_usurpa
		{
			get {return _t314_idusuario_usurpa;}
			set { _t314_idusuario_usurpa = value ;}
		}
		#endregion

		#region Constructor

		public CONEXIONES() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T459_CONEXIONES.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	25/02/2010 15:10:54
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, 
                DateTime t459_fecconect, 
                int t314_idusuario_conect, 
                Nullable<int> t314_idusuario_usurpa,
                string t459_navegador,
                string t459_version)
		{
            SqlParameter[] aParam = new SqlParameter[5];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t459_fecconect", SqlDbType.SmallDateTime, 4, t459_fecconect);
            aParam[i++] = ParametroSql.add("@t314_idusuario_conect", SqlDbType.Int, 4, t314_idusuario_conect);
            aParam[i++] = ParametroSql.add("@t314_idusuario_usurpa", SqlDbType.Int, 4, t314_idusuario_usurpa);
            aParam[i++] = ParametroSql.add("@t459_navegador", SqlDbType.VarChar, 20, t459_navegador);
            aParam[i++] = ParametroSql.add("@t459_version", SqlDbType.VarChar, 20, t459_version);

			if (tr == null)
				 SqlHelper.ExecuteNonQuery("SUP_CONEXIONES_I", aParam);
			else
				 SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CONEXIONES_I", aParam);
		}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Selecciona las conexiones realizadas propias
        /// </summary>
        /// <returns>DataSet</returns>
        /// <history>
        /// 	Creado por [sqladmin]	25/02/2010 15:10:54
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader SelectPropias(SqlTransaction tr, int t001_idficepi, DateTime dtPrimer, DateTime dtUltimo)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[0].Value = t001_idficepi;
            aParam[1] = new SqlParameter("@desde", SqlDbType.SmallDateTime, 4);
            aParam[1].Value = dtPrimer;
            aParam[2] = new SqlParameter("@hasta", SqlDbType.SmallDateTime, 4);
            aParam[2].Value = dtUltimo;


            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CONEXIONES_PROPIAS", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CONEXIONES_PROPIAS", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Selecciona las conexiones realizadas en su nombre
        /// </summary>
        /// <returns>DataSet</returns>
        /// <history>
        /// 	Creado por [sqladmin]	25/02/2010 15:10:54
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader SelectAjenas(SqlTransaction tr, int nSuplantado, DateTime dtPrimer, DateTime dtUltimo)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@nSuplantado", SqlDbType.Int, 4);
            aParam[0].Value = nSuplantado;
            aParam[1] = new SqlParameter("@desde", SqlDbType.SmallDateTime, 4);
            aParam[1].Value = dtPrimer;
            aParam[2] = new SqlParameter("@hasta", SqlDbType.SmallDateTime, 4);
            aParam[2].Value = dtUltimo;


            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CONEXIONES_AJENAS", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CONEXIONES_AJENAS", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Agrupa el nº de conexiones realizadas ajenas por meses (EN NOMBRE DEL SUPLANTADO)
        /// </summary>
        /// <returns>DataSet</returns>
        /// <history>
        /// 	Creado por [sqladmin]	25/02/2010 15:10:54
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader SelectAjenasMes(SqlTransaction tr, int nSuplantado, DateTime dtPrimer, DateTime dtUltimo)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@nSuplantado", SqlDbType.Int, 4);
            aParam[0].Value = nSuplantado;
            aParam[1] = new SqlParameter("@desde", SqlDbType.SmallDateTime, 4);
            aParam[1].Value = dtPrimer;
            aParam[2] = new SqlParameter("@hasta", SqlDbType.SmallDateTime, 4);
            aParam[2].Value = dtUltimo;


            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CONEXIONES_AJENASxMES", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CONEXIONES_AJENASxMES", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Agrupa el nº de conexiones realizadas propias por meses
        /// </summary>
        /// <returns>DataSet</returns>
        /// <history>
        /// 	Creado por [sqladmin]	25/02/2010 15:10:54
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader SelectPropiasMes(SqlTransaction tr, int t001_idficepi, DateTime dtPrimer, DateTime dtUltimo)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[0].Value = t001_idficepi;
            aParam[1] = new SqlParameter("@desde", SqlDbType.SmallDateTime, 4);
            aParam[1].Value = dtPrimer;
            aParam[2] = new SqlParameter("@hasta", SqlDbType.SmallDateTime, 4);
            aParam[2].Value = dtUltimo;


            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CONEXIONES_PROPIASxMES", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CONEXIONES_PROPIASxMES", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Agrupa el nº de conexiones realizadas propias y ajenas por meses para el gráfico de actividad
        /// </summary>
        /// <returns>DataSet</returns>
        /// <history>
        /// 	Creado por [sqladmin]	08/01/2013 15:10:54
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader SelectGraficoMes(SqlTransaction tr, int t001_idficepi, int nSuplantado, DateTime dtPrimer, DateTime dtUltimo)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[0].Value = t001_idficepi;
            aParam[1] = new SqlParameter("@nSuplantado", SqlDbType.Int, 4);
            aParam[1].Value = nSuplantado;
            aParam[2] = new SqlParameter("@desde", SqlDbType.SmallDateTime, 4);
            aParam[2].Value = dtPrimer;
            aParam[3] = new SqlParameter("@hasta", SqlDbType.SmallDateTime, 4);
            aParam[3].Value = dtUltimo;


            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CONEXIONES_GRAFICO", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CONEXIONES_GRAFICO", aParam);
        }

		#endregion
	}
}
