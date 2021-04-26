using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : VAE
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T340_VAE
	/// </summary>
	/// <history>
	/// 	Creado por [DOARHUMI]	20/11/2007 13:19:23	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class VAE
	{
		#region Propiedades y Atributos

		private int _t340_idvae;
		public int t340_idvae
		{
			get {return _t340_idvae;}
			set { _t340_idvae = value ;}
		}

		private string _t340_valor;
		public string t340_valor
		{
			get {return _t340_valor;}
			set { _t340_valor = value ;}
		}

		private bool _t340_estado;
		public bool t340_estado
		{
			get {return _t340_estado;}
			set { _t340_estado = value ;}
		}

		private int _t341_idae;
		public int t341_idae
		{
			get {return _t341_idae;}
			set { _t341_idae = value ;}
		}

        private int _t340_orden;
        public int t340_orden
		{
			get {return _t340_orden;}
			set { _t340_orden = value ;}
		}
		#endregion

		#region Constructores

		public VAE() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T340_VAE.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 13:19:23
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Insert(SqlTransaction tr, string t340_valor , bool t340_estado , int t341_idae , int t340_orden)
		{
			SqlParameter[] aParam = new SqlParameter[4];
			aParam[0] = new SqlParameter("@t340_valor", SqlDbType.VarChar, 25);
			aParam[0].Value = t340_valor;
			aParam[1] = new SqlParameter("@t340_estado", SqlDbType.Bit, 1);
			aParam[1].Value = t340_estado;
			aParam[2] = new SqlParameter("@t341_idae", SqlDbType.Int, 4);
			aParam[2].Value = t341_idae;
			aParam[3] = new SqlParameter("@t340_orden", SqlDbType.Int, 4);
			aParam[3].Value = t340_orden;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_VAE_I", aParam));
			else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_VAE_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T340_VAE.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 13:19:23
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t340_idvae, string t340_valor, bool t340_estado, int t341_idae, int t340_orden)
		{
			SqlParameter[] aParam = new SqlParameter[5];
			aParam[0] = new SqlParameter("@t340_idvae", SqlDbType.Int, 4);
			aParam[0].Value = t340_idvae;
			aParam[1] = new SqlParameter("@t340_valor", SqlDbType.Text, 25);
			aParam[1].Value = t340_valor;
			aParam[2] = new SqlParameter("@t340_estado", SqlDbType.Bit, 1);
			aParam[2].Value = t340_estado;
			aParam[3] = new SqlParameter("@t341_idae", SqlDbType.Int, 4);
			aParam[3].Value = t341_idae;
            aParam[4] = new SqlParameter("@t340_orden", SqlDbType.Int, 4);
			aParam[4].Value = t340_orden;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_VAE_U", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_VAE_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T340_VAE a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 13:19:23
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t340_idvae)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t340_idvae", SqlDbType.Int, 4);
			aParam[0].Value = t340_idvae;

			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_VAE_D", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_VAE_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T340_VAE,
		/// y devuelve una instancia u objeto del tipo VAE
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 13:19:23
		/// </history>
		/// -----------------------------------------------------------------------------
		public static VAE Select(int t340_idvae) 
		{
			VAE o = new VAE();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t340_idvae", SqlDbType.Int, 4);
			aParam[0].Value = t340_idvae;

			SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("SUP_VAE_S", aParam);

			if (dr.Read())
			{
				if (dr["t340_idvae"] != DBNull.Value)
					o.t340_idvae = (int)dr["t340_idvae"];
				if (dr["t340_valor"] != DBNull.Value)
					o.t340_valor = (string)dr["t340_valor"];
				if (dr["t340_estado"] != DBNull.Value)
					o.t340_estado = (bool)dr["t340_estado"];
				if (dr["t341_idae"] != DBNull.Value)
					o.t341_idae = (int)dr["t341_idae"];
				if (dr["t340_orden"] != DBNull.Value)
					o.t340_orden = (int)dr["t340_orden"];
            
            }
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de VAE"));
			}

            dr.Close();
            dr.Dispose();
            
            return o;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Selecciona los registros de la tabla T340_VAE en función de una foreign key.
		/// </summary>
		/// <returns>DataSet</returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 13:19:23
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader  SelectByt341_idae(int t341_idae) 
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t341_idae", SqlDbType.Int, 4);
			aParam[0].Value = t341_idae;

            return SqlHelper.ExecuteSqlDataReader("PSP_VAE_SByt341_idae", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T340_VAE.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 13:19:23
		/// </history>
		/// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo(Nullable<int> t340_idvae, string t340_valor, Nullable<bool> t340_estado, Nullable<int> t341_idae, Nullable<int> t340_orden, byte nOrden, byte nAscDesc)
		{
			SqlParameter[] aParam = new SqlParameter[7];
			aParam[0] = new SqlParameter("@t340_idvae", SqlDbType.Int, 4);
			aParam[0].Value = t340_idvae;
			aParam[1] = new SqlParameter("@t340_valor", SqlDbType.Text, 25);
			aParam[1].Value = t340_valor;
			aParam[2] = new SqlParameter("@t340_estado", SqlDbType.Bit, 1);
			aParam[2].Value = t340_estado;
			aParam[3] = new SqlParameter("@t341_idae", SqlDbType.Int, 4);
			aParam[3].Value = t341_idae;
            aParam[4] = new SqlParameter("@t340_orden", SqlDbType.Int, 4);
			aParam[4].Value = t340_orden;

			aParam[5] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
			aParam[5].Value = nOrden;
			aParam[6] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
			aParam[6].Value = nAscDesc;

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_VAE_C", aParam);
		}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla T340_VAE, en función de un código
        /// de une, y lo devuelve ordenado por "order" y "valor" (nombre).
        /// </summary>
        /// <history>
        /// 	Creado por [DOARHUMI]	27/09/2006 17:04:24
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader CatalogoByUne(int t303_idnodo, string sAmbito)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 2);
            aParam[0].Value = t303_idnodo;
            aParam[1] = new SqlParameter("@sAmbito", SqlDbType.Char, 1);
            aParam[1].Value = sAmbito;
			aParam[2] = new SqlParameter("@t340_estado", SqlDbType.Bit, 1);
			aParam[2].Value = true;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_VAE_SByCod_une", aParam);
        }
        public static SqlDataReader CatalogoByUne(int t303_idnodo, string sAmbito, Nullable<bool> t340_estado)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 2);
            aParam[0].Value = t303_idnodo;
            aParam[1] = new SqlParameter("@sAmbito", SqlDbType.Char, 1);
            aParam[1].Value = sAmbito;
			aParam[2] = new SqlParameter("@t340_estado", SqlDbType.Bit, 1);
			aParam[2].Value = t340_estado;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_VAE_SByCod_une", aParam);
        }
        #endregion
	}
}
