using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : ACCESOAPLI
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T000_ACCESOAPLI
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	17/12/2007 9:29:22	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class ACCESOAPLI
	{

		#region Propiedades y Atributos

		private byte _T000_CODIGO;
		public byte T000_CODIGO
		{
			get {return _T000_CODIGO;}
			set { _T000_CODIGO = value ;}
		}

		private string _T000_DESCRI;
		public string T000_DESCRI
		{
			get {return _T000_DESCRI;}
			set { _T000_DESCRI = value ;}
		}

		private bool _T000_ESTADO;
		public bool T000_ESTADO
		{
			get {return _T000_ESTADO;}
			set { _T000_ESTADO = value ;}
		}

		private string _T000_MOTIVO;
		public string T000_MOTIVO
		{
			get {return _T000_MOTIVO;}
			set { _T000_MOTIVO = value ;}
		}

		private bool _T000_NOVEDADES;
		public bool T000_NOVEDADES
		{
			get {return _T000_NOVEDADES;}
			set { _T000_NOVEDADES = value ;}
		}

		private short _T000_ORDEN;
		public short T000_ORDEN
		{
			get {return _T000_ORDEN;}
			set { _T000_ORDEN = value ;}
		}

        private bool _t000_bbdd;
        public bool t000_bbdd
        {
            get { return _t000_bbdd; }
            set { _t000_bbdd = value; }
        }

		#endregion

		#region Constructores

		public ACCESOAPLI() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T000_ACCESOAPLI.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	17/12/2007 9:29:22
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Insert(SqlTransaction tr, string T000_DESCRI , bool T000_ESTADO , string T000_MOTIVO , bool T000_NOVEDADES ,
                                 short T000_ORDEN, bool t000_bbdd)
		{
			SqlParameter[] aParam = new SqlParameter[6];
			aParam[0] = new SqlParameter("@T000_DESCRI", SqlDbType.Text, 20);
			aParam[0].Value = T000_DESCRI;
			aParam[1] = new SqlParameter("@T000_ESTADO", SqlDbType.Bit, 1);
			aParam[1].Value = T000_ESTADO;
			aParam[2] = new SqlParameter("@T000_MOTIVO", SqlDbType.Text, 250);
			aParam[2].Value = T000_MOTIVO;
			aParam[3] = new SqlParameter("@T000_NOVEDADES", SqlDbType.Bit, 1);
			aParam[3].Value = T000_NOVEDADES;
			aParam[4] = new SqlParameter("@T000_ORDEN", SqlDbType.SmallInt, 2);
			aParam[4].Value = T000_ORDEN;
            aParam[5] = new SqlParameter("@t000_bbdd", SqlDbType.Bit, 1);
            aParam[5].Value = t000_bbdd;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_ACCESOAPLI_I", aParam));
			else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_ACCESOAPLI_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T000_ACCESOAPLI.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	17/12/2007 9:29:22
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, byte T000_CODIGO, string T000_DESCRI, bool T000_ESTADO, string T000_MOTIVO,
                                 Nullable<bool> T000_NOVEDADES, Nullable<short> T000_ORDEN, Nullable<bool> t000_bbdd)
		{
			SqlParameter[] aParam = new SqlParameter[7];
			aParam[0] = new SqlParameter("@T000_CODIGO", SqlDbType.TinyInt, 1);
			aParam[0].Value = T000_CODIGO;
			aParam[1] = new SqlParameter("@T000_DESCRI", SqlDbType.Text, 20);
			aParam[1].Value = T000_DESCRI;
			aParam[2] = new SqlParameter("@T000_ESTADO", SqlDbType.Bit, 1);
			aParam[2].Value = T000_ESTADO;
			aParam[3] = new SqlParameter("@T000_MOTIVO", SqlDbType.Text, 250);
			aParam[3].Value = T000_MOTIVO;
			aParam[4] = new SqlParameter("@T000_NOVEDADES", SqlDbType.Bit, 1);
			aParam[4].Value = T000_NOVEDADES;
			aParam[5] = new SqlParameter("@T000_ORDEN", SqlDbType.SmallInt, 2);
			aParam[5].Value = T000_ORDEN;
            aParam[6] = new SqlParameter("@t000_bbdd", SqlDbType.Bit, 1);
            aParam[6].Value = t000_bbdd;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_ACCESOAPLI_U", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ACCESOAPLI_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T000_ACCESOAPLI a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	17/12/2007 9:29:22
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, byte T000_CODIGO)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@T000_CODIGO", SqlDbType.TinyInt, 1);
			aParam[0].Value = T000_CODIGO;

			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_ACCESOAPLI_D", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ACCESOAPLI_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T000_ACCESOAPLI,
		/// y devuelve una instancia u objeto del tipo ACCESOAPLI
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	17/12/2007 9:29:22
		/// </history>
		/// -----------------------------------------------------------------------------
		public static ACCESOAPLI Select(SqlTransaction tr, byte T000_CODIGO) 
		{
			ACCESOAPLI o = new ACCESOAPLI();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@T000_CODIGO", SqlDbType.TinyInt, 1);
			aParam[0].Value = T000_CODIGO;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_ACCESOAPLI_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_ACCESOAPLI_S", aParam);

			if (dr.Read())
			{
				if (dr["T000_CODIGO"] != DBNull.Value)
					o.T000_CODIGO = byte.Parse(dr["T000_CODIGO"].ToString());
				if (dr["T000_DESCRI"] != DBNull.Value)
					o.T000_DESCRI = (string)dr["T000_DESCRI"];
				if (dr["T000_ESTADO"] != DBNull.Value)
					o.T000_ESTADO = (bool)dr["T000_ESTADO"];
				if (dr["T000_MOTIVO"] != DBNull.Value)
					o.T000_MOTIVO = (string)dr["T000_MOTIVO"];
				if (dr["T000_NOVEDADES"] != DBNull.Value)
					o.T000_NOVEDADES = (bool)dr["T000_NOVEDADES"];
				if (dr["T000_ORDEN"] != DBNull.Value)
					o.T000_ORDEN = (short)dr["T000_ORDEN"];
                if (dr["t000_bbdd"] != DBNull.Value)
                    o.t000_bbdd = (bool)dr["t000_bbdd"];
            }
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de ACCESOAPLI"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T000_ACCESOAPLI.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	17/12/2007 9:29:22
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader Catalogo(Nullable<byte> T000_CODIGO, string T000_DESCRI, Nullable<bool> T000_ESTADO, string T000_MOTIVO,
                                             Nullable<bool> T000_NOVEDADES, Nullable<short> T000_ORDEN, Nullable<bool> t000_bbdd, 
                                             byte nOrden, byte nAscDesc)
		{
			SqlParameter[] aParam = new SqlParameter[9];
			aParam[0] = new SqlParameter("@T000_CODIGO", SqlDbType.TinyInt, 1);
			aParam[0].Value = T000_CODIGO;
			aParam[1] = new SqlParameter("@T000_DESCRI", SqlDbType.Text, 20);
			aParam[1].Value = T000_DESCRI;
			aParam[2] = new SqlParameter("@T000_ESTADO", SqlDbType.Bit, 1);
			aParam[2].Value = T000_ESTADO;
			aParam[3] = new SqlParameter("@T000_MOTIVO", SqlDbType.Text, 250);
			aParam[3].Value = T000_MOTIVO;
			aParam[4] = new SqlParameter("@T000_NOVEDADES", SqlDbType.Bit, 1);
			aParam[4].Value = T000_NOVEDADES;
			aParam[5] = new SqlParameter("@T000_ORDEN", SqlDbType.SmallInt, 2);
			aParam[5].Value = T000_ORDEN;
            aParam[6] = new SqlParameter("@t000_bbdd", SqlDbType.Bit, 1);
            aParam[6].Value = t000_bbdd;

			aParam[7] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
			aParam[7].Value = nOrden;
			aParam[8] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
			aParam[8].Value = nAscDesc;

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_ACCESOAPLI_C", aParam);
		}

		#endregion
	}
}
