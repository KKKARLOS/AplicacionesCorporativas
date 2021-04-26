using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : TIPOASUNTO
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T384_TIPOASUNTO
	/// </summary>
	/// <history>
	/// 	Creado por [DOARHUMI]	15/11/2007 11:13:52	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class TIPOASUNTO
	{
		#region Propiedades y Atributos

		private string _t384_destipo;
		public string t384_destipo
		{
			get {return _t384_destipo;}
			set { _t384_destipo = value ;}
		}

		private int _t384_idtipo;
		public int t384_idtipo
		{
			get {return _t384_idtipo;}
			set { _t384_idtipo = value ;}
		}
        private byte _t384_orden;
        public byte t384_orden
        {
            get { return _t384_orden; }
            set { _t384_orden = value; }
        }
        #endregion

		#region Constructores

		public TIPOASUNTO() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T384_TIPOASUNTO.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	15/11/2007 11:13:52
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, string t384_destipo, byte t384_orden)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t384_destipo", SqlDbType.Text, 50);
			aParam[0].Value = t384_destipo;
            aParam[1] = new SqlParameter("@t384_orden", SqlDbType.TinyInt, 1);
            aParam[1].Value = t384_orden;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_TIPOASUNTO_I", aParam));
			else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_TIPOASUNTO_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T384_TIPOASUNTO.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	15/11/2007 11:13:52
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, string t384_destipo, int t384_idtipo, byte t384_orden)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t384_destipo", SqlDbType.Text, 50);
			aParam[0].Value = t384_destipo;
			aParam[1] = new SqlParameter("@t384_idtipo", SqlDbType.Int, 4);
			aParam[1].Value = t384_idtipo;
            aParam[2] = new SqlParameter("@t384_orden", SqlDbType.TinyInt, 1);
            aParam[2].Value = t384_orden;

            // Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_TIPOASUNTO_U", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_TIPOASUNTO_U", aParam);
    		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T384_TIPOASUNTO a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	15/11/2007 11:13:52
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t384_idtipo)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t384_idtipo", SqlDbType.Int, 4);
			aParam[0].Value = t384_idtipo;

			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_TIPOASUNTO_D", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_TIPOASUNTO_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T384_TIPOASUNTO,
		/// y devuelve una instancia u objeto del tipo TIPOASUNTO
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	15/11/2007 11:13:52
		/// </history>
		/// -----------------------------------------------------------------------------
		public static TIPOASUNTO Select(SqlTransaction tr, int t384_idtipo) 
		{
			TIPOASUNTO o = new TIPOASUNTO();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t384_idtipo", SqlDbType.Int, 4);
			aParam[0].Value = t384_idtipo;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_TIPOASUNTO_S", aParam);
			else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_TIPOASUNTO_S", aParam);

			if (dr.Read())
			{
				if (dr["t384_destipo"] != DBNull.Value)
					o.t384_destipo = (string)dr["t384_destipo"];
				if (dr["t384_idtipo"] != DBNull.Value)
					o.t384_idtipo = (int)dr["t384_idtipo"];
                if (dr["t384_orden"] != DBNull.Value)
                    o.t384_orden = (byte)dr["t384_orden"];
            }
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de TIPOASUNTO"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T384_TIPOASUNTO.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	15/11/2007 11:13:52
		/// </history>
		/// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo(string t384_destipo, Nullable<int> t384_idtipo, Nullable<byte> t384_orden, 
                                             byte nOrden, byte nAscDesc)
		{
			SqlParameter[] aParam = new SqlParameter[5];
			aParam[0] = new SqlParameter("@t384_destipo", SqlDbType.Text, 50);
			aParam[0].Value = t384_destipo;
			aParam[1] = new SqlParameter("@t384_idtipo", SqlDbType.Int, 4);
			aParam[1].Value = t384_idtipo;
            aParam[2] = new SqlParameter("@t384_orden", SqlDbType.TinyInt, 1);
            aParam[2].Value = t384_orden;
            
            aParam[3] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
			aParam[3].Value = nOrden;
			aParam[4] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
			aParam[4].Value = nAscDesc;

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_TIPOASUNTO_C", aParam);
		}

		#endregion
	}
}
