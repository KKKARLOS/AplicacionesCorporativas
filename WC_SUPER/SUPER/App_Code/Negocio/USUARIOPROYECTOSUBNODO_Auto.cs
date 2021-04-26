using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : USUARIOPROYECTOSUBNODO
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T330_USUARIOPROYECTOSUBNODO
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	05/06/2008 11:39:23	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class USUARIOPROYECTOSUBNODO
	{
		#region Propiedades y Atributos

		private int _t305_idproyectosubnodo;
		public int t305_idproyectosubnodo
		{
			get {return _t305_idproyectosubnodo;}
			set { _t305_idproyectosubnodo = value ;}
		}

		private int _t314_idusuario;
		public int t314_idusuario
		{
			get {return _t314_idusuario;}
			set { _t314_idusuario = value ;}
		}

        private decimal _t330_costecon;
        public decimal t330_costecon
		{
			get {return _t330_costecon;}
			set { _t330_costecon = value ;}
		}

        private decimal _t330_costerep;
        public decimal t330_costerep
		{
			get {return _t330_costerep;}
			set { _t330_costerep = value ;}
		}

		private bool _t330_deriva;
		public bool t330_deriva
		{
			get {return _t330_deriva;}
			set { _t330_deriva = value ;}
		}

		private DateTime _t330_falta;
		public DateTime t330_falta
		{
			get {return _t330_falta;}
			set { _t330_falta = value ;}
		}

		private DateTime? _t330_fbaja;
		public DateTime? t330_fbaja
		{
			get {return _t330_fbaja;}
			set { _t330_fbaja = value ;}
		}

		private int? _t333_idperfilproy;
		public int? t333_idperfilproy
		{
			get {return _t333_idperfilproy;}
			set { _t333_idperfilproy = value ;}
		}
		#endregion

		#region Constructores

		public USUARIOPROYECTOSUBNODO() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T330_USUARIOPROYECTOSUBNODO a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	05/06/2008 11:39:23
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t305_idproyectosubnodo, int t314_idusuario)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
			aParam[0].Value = t305_idproyectosubnodo;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_USUARIOPROYECTOSUBNODO_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_USUARIOPROYECTOSUBNODO_D", aParam);
		}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un registro de la tabla T330_USUARIOPROYECTOSUBNODO,
        /// y devuelve una instancia u objeto del tipo USUARIOPROYECTOSUBNODO
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	24/08/2009 12:36:58
        /// </history>
        /// -----------------------------------------------------------------------------
        public static USUARIOPROYECTOSUBNODO Select(SqlTransaction tr, int t305_idproyectosubnodo, int t314_idusuario)
        {
            USUARIOPROYECTOSUBNODO o = new USUARIOPROYECTOSUBNODO();

            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_USUARIOPROYECTOSUBNODO_S", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_USUARIOPROYECTOSUBNODO_S", aParam);

            if (dr.Read())
            {
                if (dr["t305_idproyectosubnodo"] != DBNull.Value)
                    o.t305_idproyectosubnodo = int.Parse(dr["t305_idproyectosubnodo"].ToString());
                if (dr["t314_idusuario"] != DBNull.Value)
                    o.t314_idusuario = int.Parse(dr["t314_idusuario"].ToString());
                if (dr["t330_costecon"] != DBNull.Value)
                    o.t330_costecon = decimal.Parse(dr["t330_costecon"].ToString());
                if (dr["t330_costerep"] != DBNull.Value)
                    o.t330_costerep = decimal.Parse(dr["t330_costerep"].ToString());
                if (dr["t330_deriva"] != DBNull.Value)
                    o.t330_deriva = (bool)dr["t330_deriva"];
                if (dr["t330_falta"] != DBNull.Value)
                    o.t330_falta = (DateTime)dr["t330_falta"];
                if (dr["t330_fbaja"] != DBNull.Value)
                    o.t330_fbaja = (DateTime)dr["t330_fbaja"];
                if (dr["t333_idperfilproy"] != DBNull.Value)
                    o.t333_idperfilproy = int.Parse(dr["t333_idperfilproy"].ToString());
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de USUARIOPROYECTOSUBNODO"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }


		#endregion
	}
}
