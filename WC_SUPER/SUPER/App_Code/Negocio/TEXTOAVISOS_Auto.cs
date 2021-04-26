using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : TEXTOAVISOS
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T448_TEXTOAVISOS
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	28/04/2009 11:29:19	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class TEXTOAVISOS
	{
		#region Propiedades y Atributos

		private int _t448_idaviso;
		public int t448_idaviso
		{
			get {return _t448_idaviso;}
			set { _t448_idaviso = value ;}
		}

		private string _t448_denominacion;
		public string t448_denominacion
		{
			get {return _t448_denominacion;}
			set { _t448_denominacion = value ;}
		}

		private string _t448_titulo;
		public string t448_titulo
		{
			get {return _t448_titulo;}
			set { _t448_titulo = value ;}
		}

		private string _t448_texto;
		public string t448_texto
		{
			get {return _t448_texto;}
			set { _t448_texto = value ;}
		}

		private bool _t448_IAP;
		public bool t448_IAP
		{
			get {return _t448_IAP;}
			set { _t448_IAP = value ;}
		}

		private bool _t448_PGE;
		public bool t448_PGE
		{
			get {return _t448_PGE;}
			set { _t448_PGE = value ;}
		}

		private bool _t448_PST;
		public bool t448_PST
		{
			get {return _t448_PST;}
			set { _t448_PST = value ;}
		}

		private DateTime? _t448_fiv;
		public DateTime? t448_fiv
		{
			get {return _t448_fiv;}
			set { _t448_fiv = value ;}
		}

		private DateTime? _t448_ffv;
		public DateTime? t448_ffv
		{
			get {return _t448_ffv;}
			set { _t448_ffv = value ;}
		}
		#endregion

		#region Constructor

		public TEXTOAVISOS() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T448_TEXTOAVISOS.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	28/04/2009 11:29:19
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Insert(SqlTransaction tr, string t448_denominacion , string t448_titulo , string t448_texto , bool t448_IAP , bool t448_PGE , bool t448_PST , Nullable<DateTime> t448_fiv , Nullable<DateTime> t448_ffv)
		{
			SqlParameter[] aParam = new SqlParameter[8];
			aParam[0] = new SqlParameter("@t448_denominacion", SqlDbType.Text, 50);
			aParam[0].Value = t448_denominacion;
			aParam[1] = new SqlParameter("@t448_titulo", SqlDbType.Text, 50);
			aParam[1].Value = t448_titulo;
			aParam[2] = new SqlParameter("@t448_texto", SqlDbType.Text, 2147483647);
			aParam[2].Value = t448_texto;
			aParam[3] = new SqlParameter("@t448_IAP", SqlDbType.Bit, 1);
			aParam[3].Value = t448_IAP;
			aParam[4] = new SqlParameter("@t448_PGE", SqlDbType.Bit, 1);
			aParam[4].Value = t448_PGE;
			aParam[5] = new SqlParameter("@t448_PST", SqlDbType.Bit, 1);
			aParam[5].Value = t448_PST;
			aParam[6] = new SqlParameter("@t448_fiv", SqlDbType.SmallDateTime, 4);
			aParam[6].Value = t448_fiv;
			aParam[7] = new SqlParameter("@t448_ffv", SqlDbType.SmallDateTime, 4);
			aParam[7].Value = t448_ffv;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
				return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_TEXTOAVISOS_I", aParam));
			else
				return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_TEXTOAVISOS_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T448_TEXTOAVISOS.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	28/04/2009 11:29:19
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Update(SqlTransaction tr, int t448_idaviso, string t448_denominacion, string t448_titulo, string t448_texto, bool t448_IAP, bool t448_PGE, bool t448_PST, Nullable<DateTime> t448_fiv, Nullable<DateTime> t448_ffv)
		{
			SqlParameter[] aParam = new SqlParameter[9];
			aParam[0] = new SqlParameter("@t448_idaviso", SqlDbType.Int, 4);
			aParam[0].Value = t448_idaviso;
			aParam[1] = new SqlParameter("@t448_denominacion", SqlDbType.Text, 50);
			aParam[1].Value = t448_denominacion;
			aParam[2] = new SqlParameter("@t448_titulo", SqlDbType.Text, 50);
			aParam[2].Value = t448_titulo;
			aParam[3] = new SqlParameter("@t448_texto", SqlDbType.Text, 2147483647);
			aParam[3].Value = t448_texto;
			aParam[4] = new SqlParameter("@t448_IAP", SqlDbType.Bit, 1);
			aParam[4].Value = t448_IAP;
			aParam[5] = new SqlParameter("@t448_PGE", SqlDbType.Bit, 1);
			aParam[5].Value = t448_PGE;
			aParam[6] = new SqlParameter("@t448_PST", SqlDbType.Bit, 1);
			aParam[6].Value = t448_PST;
			aParam[7] = new SqlParameter("@t448_fiv", SqlDbType.SmallDateTime, 4);
			aParam[7].Value = t448_fiv;
			aParam[8] = new SqlParameter("@t448_ffv", SqlDbType.SmallDateTime, 4);
			aParam[8].Value = t448_ffv;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_TEXTOAVISOS_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_TEXTOAVISOS_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T448_TEXTOAVISOS a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	28/04/2009 11:29:19
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t448_idaviso)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t448_idaviso", SqlDbType.Int, 4);
			aParam[0].Value = t448_idaviso;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_TEXTOAVISOS_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_TEXTOAVISOS_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T448_TEXTOAVISOS,
		/// y devuelve una instancia u objeto del tipo TEXTOAVISOS
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	28/04/2009 11:29:19
		/// </history>
		/// -----------------------------------------------------------------------------
		public static TEXTOAVISOS Select(SqlTransaction tr, int t448_idaviso) 
		{
			TEXTOAVISOS o = new TEXTOAVISOS();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t448_idaviso", SqlDbType.Int, 4);
			aParam[0].Value = t448_idaviso;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_TEXTOAVISOS_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_TEXTOAVISOS_S", aParam);

			if (dr.Read())
			{
				if (dr["t448_idaviso"] != DBNull.Value)
					o.t448_idaviso = int.Parse(dr["t448_idaviso"].ToString());
				if (dr["t448_denominacion"] != DBNull.Value)
					o.t448_denominacion = (string)dr["t448_denominacion"];
				if (dr["t448_titulo"] != DBNull.Value)
					o.t448_titulo = (string)dr["t448_titulo"];
				if (dr["t448_texto"] != DBNull.Value)
					o.t448_texto = (string)dr["t448_texto"];
				if (dr["t448_IAP"] != DBNull.Value)
					o.t448_IAP = (bool)dr["t448_IAP"];
				if (dr["t448_PGE"] != DBNull.Value)
					o.t448_PGE = (bool)dr["t448_PGE"];
				if (dr["t448_PST"] != DBNull.Value)
					o.t448_PST = (bool)dr["t448_PST"];
				if (dr["t448_fiv"] != DBNull.Value)
					o.t448_fiv = (DateTime)dr["t448_fiv"];
				if (dr["t448_ffv"] != DBNull.Value)
					o.t448_ffv = (DateTime)dr["t448_ffv"];

			}
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de TEXTOAVISOS"));
			}

			dr.Close();
			dr.Dispose();

			return o;
        }
        #region codigo comentado

        /// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T448_TEXTOAVISOS.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	28/04/2009 11:29:19
		/// </history>
		/// -----------------------------------------------------------------------------
        //public static SqlDataReader Catalogo(Nullable<int> t448_idaviso, string t448_denominacion, string t448_titulo, string t448_texto, Nullable<bool> t448_IAP, Nullable<bool> t448_PGE, Nullable<bool> t448_PST, Nullable<DateTime> t448_fiv, Nullable<DateTime> t448_ffv, byte nOrden, byte nAscDesc)
        //{
        //    SqlParameter[] aParam = new SqlParameter[11];
        //    aParam[0] = new SqlParameter("@t448_idaviso", SqlDbType.Int, 4);
        //    aParam[0].Value = t448_idaviso;
        //    aParam[1] = new SqlParameter("@t448_denominacion", SqlDbType.Text, 50);
        //    aParam[1].Value = t448_denominacion;
        //    aParam[2] = new SqlParameter("@t448_titulo", SqlDbType.Text, 50);
        //    aParam[2].Value = t448_titulo;
        //    aParam[3] = new SqlParameter("@t448_texto", SqlDbType.Text, 2147483647);
        //    aParam[3].Value = t448_texto;
        //    aParam[4] = new SqlParameter("@t448_IAP", SqlDbType.Bit, 1);
        //    aParam[4].Value = t448_IAP;
        //    aParam[5] = new SqlParameter("@t448_PGE", SqlDbType.Bit, 1);
        //    aParam[5].Value = t448_PGE;
        //    aParam[6] = new SqlParameter("@t448_PST", SqlDbType.Bit, 1);
        //    aParam[6].Value = t448_PST;
        //    aParam[7] = new SqlParameter("@t448_fiv", SqlDbType.SmallDateTime, 4);
        //    aParam[7].Value = t448_fiv;
        //    aParam[8] = new SqlParameter("@t448_ffv", SqlDbType.SmallDateTime, 4);
        //    aParam[8].Value = t448_ffv;

        //    aParam[9] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
        //    aParam[9].Value = nOrden;
        //    aParam[10] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
        //    aParam[10].Value = nAscDesc;

        //    // Ejecuta la query y devuelve un SqlDataReader con el resultado.
        //    return SqlHelper.ExecuteSqlDataReader("SUP_TEXTOAVISOS_C", aParam);
        //}

        #endregion

        #endregion
    }
}
