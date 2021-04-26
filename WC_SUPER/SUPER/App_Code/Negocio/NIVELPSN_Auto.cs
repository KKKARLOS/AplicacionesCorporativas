using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : NIVELPSN
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T442_NIVELPSN
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	22/10/2009 12:31:09	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class NIVELPSN
	{
		#region Propiedades y Atributos

		private int _t442_idnivel;
		public int t442_idnivel
		{
			get {return _t442_idnivel;}
			set { _t442_idnivel = value ;}
		}

		private string _t442_denominacion;
		public string t442_denominacion
		{
			get {return _t442_denominacion;}
			set { _t442_denominacion = value ;}
		}

		private int _t305_idproyectosubnodo;
		public int t305_idproyectosubnodo
		{
			get {return _t305_idproyectosubnodo;}
			set { _t305_idproyectosubnodo = value ;}
		}

		private decimal _t442_impnivel;
		public decimal t442_impnivel
		{
			get {return _t442_impnivel;}
			set { _t442_impnivel = value ;}
		}

		private bool _t442_estado;
		public bool t442_estado
		{
			get {return _t442_estado;}
			set { _t442_estado = value ;}
		}

		private byte _t442_orden;
		public byte t442_orden
		{
			get {return _t442_orden;}
			set { _t442_orden = value ;}
		}
		#endregion

		#region Constructor

		public NIVELPSN() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T442_NIVELPSN.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	22/10/2009 12:31:09
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Insert(SqlTransaction tr, string t442_denominacion , int t305_idproyectosubnodo , decimal t442_impnivel , bool t442_estado , byte t442_orden)
		{
			SqlParameter[] aParam = new SqlParameter[5];
			aParam[0] = new SqlParameter("@t442_denominacion", SqlDbType.Text, 30);
			aParam[0].Value = t442_denominacion;
			aParam[1] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
			aParam[1].Value = t305_idproyectosubnodo;
			aParam[2] = new SqlParameter("@t442_impnivel", SqlDbType.Money, 8);
			aParam[2].Value = t442_impnivel;
			aParam[3] = new SqlParameter("@t442_estado", SqlDbType.Bit, 1);
			aParam[3].Value = t442_estado;
			aParam[4] = new SqlParameter("@t442_orden", SqlDbType.TinyInt, 1);
			aParam[4].Value = t442_orden;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
				return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_NIVELPSN_I", aParam));
			else
				return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_NIVELPSN_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T442_NIVELPSN.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	22/10/2009 12:31:09
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Update(SqlTransaction tr, int t442_idnivel, string t442_denominacion, int t305_idproyectosubnodo, decimal t442_impnivel, bool t442_estado, byte t442_orden)
		{
			SqlParameter[] aParam = new SqlParameter[6];
			aParam[0] = new SqlParameter("@t442_idnivel", SqlDbType.Int, 4);
			aParam[0].Value = t442_idnivel;
			aParam[1] = new SqlParameter("@t442_denominacion", SqlDbType.Text, 30);
			aParam[1].Value = t442_denominacion;
			aParam[2] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
			aParam[2].Value = t305_idproyectosubnodo;
			aParam[3] = new SqlParameter("@t442_impnivel", SqlDbType.Money, 8);
			aParam[3].Value = t442_impnivel;
			aParam[4] = new SqlParameter("@t442_estado", SqlDbType.Bit, 1);
			aParam[4].Value = t442_estado;
			aParam[5] = new SqlParameter("@t442_orden", SqlDbType.TinyInt, 1);
			aParam[5].Value = t442_orden;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_NIVELPSN_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_NIVELPSN_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T442_NIVELPSN a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	22/10/2009 12:31:09
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t442_idnivel)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t442_idnivel", SqlDbType.Int, 4);
			aParam[0].Value = t442_idnivel;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_NIVELPSN_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_NIVELPSN_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Selecciona los registros de la tabla T442_NIVELPSN en función de una foreign key.
		/// </summary>
		/// <returns>DataSet</returns>
		/// <history>
		/// 	Creado por [sqladmin]	22/10/2009 12:31:09
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader SelectByT305_idproyectosubnodo(SqlTransaction tr, int t305_idproyectosubnodo) 
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
			aParam[0].Value = t305_idproyectosubnodo;


			if (tr == null)
				return SqlHelper.ExecuteSqlDataReader("SUP_NIVELPSN_SByT305_idproyectosubnodo", aParam);
			else
				return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_NIVELPSN_SByT305_idproyectosubnodo", aParam);
		}

        /// <summary>
        /// Verfica si es posible borrar un nivel
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t442_idnivel"></param>
        /// <returns></returns>
        public static int ComprobarBorrado(SqlTransaction tr, int t442_idnivel)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t442_idnivel", SqlDbType.Int, 4, t442_idnivel)
            };
            if (tr == null)
                return (int)SqlHelper.ExecuteScalar("SUP_NIVEL_D_consulta", aParam);
            else
                return (int)SqlHelper.ExecuteScalarTransaccion(tr, "SUP_NIVEL_D_consulta", aParam);
        }
        #endregion
	}
}
