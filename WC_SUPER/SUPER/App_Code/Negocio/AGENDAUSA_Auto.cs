using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : AGENDAUSA
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T641_AGENDAUSA
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	17/01/2011 11:39:48	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class AGENDAUSA
	{
		#region Propiedades y Atributos

		private int _t641_idagendausa;
		public int t641_idagendausa
		{
			get {return _t641_idagendausa;}
			set { _t641_idagendausa = value ;}
		}

		private int _t301_idproyecto;
		public int t301_idproyecto
		{
			get {return _t301_idproyecto;}
			set { _t301_idproyecto = value ;}
		}

		private int _t641_anomes;
		public int t641_anomes
		{
			get {return _t641_anomes;}
			set { _t641_anomes = value ;}
		}

		private string _t641_consumos;
		public string t641_consumos
		{
			get {return _t641_consumos;}
			set { _t641_consumos = value ;}
		}

		private string _t641_produccion;
		public string t641_produccion
		{
			get {return _t641_produccion;}
			set { _t641_produccion = value ;}
		}

		private string _t641_facturacion;
		public string t641_facturacion
		{
			get {return _t641_facturacion;}
			set { _t641_facturacion = value ;}
		}

		private string _t641_otros;
		public string t641_otros
		{
			get {return _t641_otros;}
			set { _t641_otros = value ;}
		}
		#endregion

		#region Constructor

		public AGENDAUSA() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T641_AGENDAUSA.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	17/01/2011 11:39:48
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Insert(SqlTransaction tr, int t301_idproyecto , int t641_anomes , string t641_consumos , string t641_produccion , string t641_facturacion , string t641_otros)
		{
			SqlParameter[] aParam = new SqlParameter[6];
			aParam[0] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
			aParam[0].Value = t301_idproyecto;
			aParam[1] = new SqlParameter("@t641_anomes", SqlDbType.Int, 4);
			aParam[1].Value = t641_anomes;
			aParam[2] = new SqlParameter("@t641_consumos", SqlDbType.Text, 2147483647);
			aParam[2].Value = t641_consumos;
			aParam[3] = new SqlParameter("@t641_produccion", SqlDbType.Text, 2147483647);
			aParam[3].Value = t641_produccion;
			aParam[4] = new SqlParameter("@t641_facturacion", SqlDbType.Text, 2147483647);
			aParam[4].Value = t641_facturacion;
			aParam[5] = new SqlParameter("@t641_otros", SqlDbType.Text, 2147483647);
			aParam[5].Value = t641_otros;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
				return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_AGENDAUSA_I", aParam));
			else
				return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_AGENDAUSA_I", aParam));
		}


		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T641_AGENDAUSA a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	17/01/2011 11:39:48
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t641_idagendausa)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t641_idagendausa", SqlDbType.Int, 4);
			aParam[0].Value = t641_idagendausa;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_AGENDAUSA_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_AGENDAUSA_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T641_AGENDAUSA.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	17/01/2011 11:39:48
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader Catalogo(Nullable<int> t641_idagendausa, Nullable<int> t301_idproyecto, Nullable<int> t641_anomes, string t641_consumos, string t641_produccion, string t641_facturacion, string t641_otros, byte nOrden, byte nAscDesc)
		{
			SqlParameter[] aParam = new SqlParameter[9];
			aParam[0] = new SqlParameter("@t641_idagendausa", SqlDbType.Int, 4);
			aParam[0].Value = t641_idagendausa;
			aParam[1] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
			aParam[1].Value = t301_idproyecto;
			aParam[2] = new SqlParameter("@t641_anomes", SqlDbType.Int, 4);
			aParam[2].Value = t641_anomes;
			aParam[3] = new SqlParameter("@t641_consumos", SqlDbType.Text, 2147483647);
			aParam[3].Value = t641_consumos;
			aParam[4] = new SqlParameter("@t641_produccion", SqlDbType.Text, 2147483647);
			aParam[4].Value = t641_produccion;
			aParam[5] = new SqlParameter("@t641_facturacion", SqlDbType.Text, 2147483647);
			aParam[5].Value = t641_facturacion;
			aParam[6] = new SqlParameter("@t641_otros", SqlDbType.Text, 2147483647);
			aParam[6].Value = t641_otros;

			aParam[7] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
			aParam[7].Value = nOrden;
			aParam[8] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
			aParam[8].Value = nAscDesc;

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
			return SqlHelper.ExecuteSqlDataReader("SUP_AGENDAUSA_C", aParam);
		}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un registro de la tabla T641_AGENDAUSA,
        /// y devuelve una instancia u objeto del tipo AGENDAUSA
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	17/01/2011 12:47:58
        /// </history>
        /// -----------------------------------------------------------------------------
        public static AGENDAUSA Select(SqlTransaction tr, int t641_idagendausa)
        {
            AGENDAUSA o = new AGENDAUSA();

            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t641_idagendausa", SqlDbType.Int, 4);
            aParam[0].Value = t641_idagendausa;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_AGENDAUSA_S", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_AGENDAUSA_S", aParam);

            if (dr.Read())
            {
                if (dr["t641_idagendausa"] != DBNull.Value)
                    o.t641_idagendausa = int.Parse(dr["t641_idagendausa"].ToString());
                if (dr["t301_idproyecto"] != DBNull.Value)
                    o.t301_idproyecto = int.Parse(dr["t301_idproyecto"].ToString());
                if (dr["t641_anomes"] != DBNull.Value)
                    o.t641_anomes = int.Parse(dr["t641_anomes"].ToString());
                if (dr["t641_consumos"] != DBNull.Value)
                    o.t641_consumos = (string)dr["t641_consumos"];
                if (dr["t641_produccion"] != DBNull.Value)
                    o.t641_produccion = (string)dr["t641_produccion"];
                if (dr["t641_facturacion"] != DBNull.Value)
                    o.t641_facturacion = (string)dr["t641_facturacion"];
                if (dr["t641_otros"] != DBNull.Value)
                    o.t641_otros = (string)dr["t641_otros"];

            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de AGENDAUSA"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }
		#endregion
	}
}
