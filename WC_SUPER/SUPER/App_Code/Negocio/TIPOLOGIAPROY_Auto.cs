using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : TIPOLOGIAPROY
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T320_TIPOLOGIAPROY
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	30/06/2008 9:12:53	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class TIPOLOGIAPROY
	{
		#region Propiedades y Atributos

		private byte _t320_idtipologiaproy;
		public byte t320_idtipologiaproy
		{
			get {return _t320_idtipologiaproy;}
			set { _t320_idtipologiaproy = value ;}
		}

		private string _t320_denominacion;
		public string t320_denominacion
		{
			get {return _t320_denominacion;}
			set { _t320_denominacion = value ;}
		}

		private bool _t320_facturable;
		public bool t320_facturable
		{
			get {return _t320_facturable;}
			set { _t320_facturable = value ;}
		}

		private bool _t320_interno;
		public bool t320_interno
		{
			get {return _t320_interno;}
			set { _t320_interno = value ;}
		}

		private bool _t320_especial;
		public bool t320_especial
		{
			get {return _t320_especial;}
			set { _t320_especial = value ;}
		}

		private bool _t320_requierecontrato;
		public bool t320_requierecontrato
		{
			get {return _t320_requierecontrato;}
			set { _t320_requierecontrato = value ;}
		}

		private byte _t320_orden;
		public byte t320_orden
		{
			get {return _t320_orden;}
			set { _t320_orden = value ;}
		}

        private bool _t320_creaalertas;
        public bool t320_creaalertas
        {
            get { return _t320_creaalertas; }
            set { _t320_creaalertas = value; }
        }
		#endregion

		#region Constructor

		public TIPOLOGIAPROY() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T320_TIPOLOGIAPROY.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	30/06/2008 9:12:53
		/// </history>
		/// -----------------------------------------------------------------------------
        public static byte Insert(SqlTransaction tr, string t320_denominacion, bool t320_facturable, bool t320_interno, bool t320_especial, bool t320_requierecontrato, byte t320_orden, bool t320_creaalertas)
		{
			SqlParameter[] aParam = new SqlParameter[7];
			aParam[0] = new SqlParameter("@t320_denominacion", SqlDbType.Text, 50);
			aParam[0].Value = t320_denominacion;
			aParam[1] = new SqlParameter("@t320_facturable", SqlDbType.Bit, 1);
			aParam[1].Value = t320_facturable;
			aParam[2] = new SqlParameter("@t320_interno", SqlDbType.Bit, 1);
			aParam[2].Value = t320_interno;
			aParam[3] = new SqlParameter("@t320_especial", SqlDbType.Bit, 1);
			aParam[3].Value = t320_especial;
			aParam[4] = new SqlParameter("@t320_requierecontrato", SqlDbType.Bit, 1);
			aParam[4].Value = t320_requierecontrato;
			aParam[5] = new SqlParameter("@t320_orden", SqlDbType.TinyInt, 1);
			aParam[5].Value = t320_orden;
            aParam[6] = new SqlParameter("@t320_creaalertas", SqlDbType.Bit, 1);
            aParam[6].Value = t320_creaalertas;
            
             // Ejecuta la query y devuelve el valor del nuevo Identity.
             if (tr == null)
                 return Convert.ToByte(SqlHelper.ExecuteScalar("SUP_TIPOLOGIAPROY_I", aParam));
             else
                 return Convert.ToByte(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_TIPOLOGIAPROY_I", aParam));
         }

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T320_TIPOLOGIAPROY.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	30/06/2008 9:12:53
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, byte t320_idtipologiaproy, string t320_denominacion, bool t320_facturable, bool t320_interno, bool t320_especial, bool t320_requierecontrato, byte t320_orden, bool t320_creaalertas)
		{
			SqlParameter[] aParam = new SqlParameter[8];
			aParam[0] = new SqlParameter("@t320_idtipologiaproy", SqlDbType.TinyInt, 1);
			aParam[0].Value = t320_idtipologiaproy;
			aParam[1] = new SqlParameter("@t320_denominacion", SqlDbType.Text, 50);
			aParam[1].Value = t320_denominacion;
			aParam[2] = new SqlParameter("@t320_facturable", SqlDbType.Bit, 1);
			aParam[2].Value = t320_facturable;
			aParam[3] = new SqlParameter("@t320_interno", SqlDbType.Bit, 1);
			aParam[3].Value = t320_interno;
			aParam[4] = new SqlParameter("@t320_especial", SqlDbType.Bit, 1);
			aParam[4].Value = t320_especial;
			aParam[5] = new SqlParameter("@t320_requierecontrato", SqlDbType.Bit, 1);
			aParam[5].Value = t320_requierecontrato;
			aParam[6] = new SqlParameter("@t320_orden", SqlDbType.TinyInt, 1);
			aParam[6].Value = t320_orden;
            aParam[7] = new SqlParameter("@t320_creaalertas", SqlDbType.Bit, 1);
            aParam[7].Value = t320_creaalertas;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_TIPOLOGIAPROY_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_TIPOLOGIAPROY_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T320_TIPOLOGIAPROY a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	30/06/2008 9:12:53
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, byte t320_idtipologiaproy)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t320_idtipologiaproy", SqlDbType.TinyInt, 1);
			aParam[0].Value = t320_idtipologiaproy;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_TIPOLOGIAPROY_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_TIPOLOGIAPROY_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T320_TIPOLOGIAPROY,
		/// y devuelve una instancia u objeto del tipo TIPOLOGIAPROY
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	30/06/2008 9:12:53
		/// </history>
		/// -----------------------------------------------------------------------------
		public static TIPOLOGIAPROY Select(SqlTransaction tr, byte t320_idtipologiaproy) 
		{
			TIPOLOGIAPROY o = new TIPOLOGIAPROY();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t320_idtipologiaproy", SqlDbType.TinyInt, 1);
			aParam[0].Value = t320_idtipologiaproy;

			SqlDataReader dr;
			if (tr == null)
				dr = SqlHelper.ExecuteSqlDataReader("SUP_TIPOLOGIAPROY_S", aParam);
			else
				dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_TIPOLOGIAPROY_S", aParam);

			if (dr.Read())
			{
				if (dr["t320_idtipologiaproy"] != DBNull.Value)
					o.t320_idtipologiaproy = byte.Parse(dr["t320_idtipologiaproy"].ToString());
				if (dr["t320_denominacion"] != DBNull.Value)
					o.t320_denominacion = (string)dr["t320_denominacion"];
				if (dr["t320_facturable"] != DBNull.Value)
					o.t320_facturable = (bool)dr["t320_facturable"];
				if (dr["t320_interno"] != DBNull.Value)
					o.t320_interno = (bool)dr["t320_interno"];
				if (dr["t320_especial"] != DBNull.Value)
					o.t320_especial = (bool)dr["t320_especial"];
				if (dr["t320_requierecontrato"] != DBNull.Value)
					o.t320_requierecontrato = (bool)dr["t320_requierecontrato"];
				if (dr["t320_orden"] != DBNull.Value)
					o.t320_orden = byte.Parse(dr["t320_orden"].ToString());
                if (dr["t320_creaalertas"] != DBNull.Value)
                    o.t320_creaalertas = (bool)dr["t320_creaalertas"];

			}
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de TIPOLOGIAPROY"));
			}

			dr.Close();
			dr.Dispose();

			return o;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T320_TIPOLOGIAPROY.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	30/06/2008 9:12:54
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader Catalogo(Nullable<byte> t320_idtipologiaproy, string t320_denominacion, Nullable<bool> t320_facturable, Nullable<bool> t320_interno, Nullable<bool> t320_especial, Nullable<bool> t320_requierecontrato, Nullable<byte> t320_orden, byte nOrden, byte nAscDesc)
		{
			SqlParameter[] aParam = new SqlParameter[9];
			aParam[0] = new SqlParameter("@t320_idtipologiaproy", SqlDbType.TinyInt, 1);
			aParam[0].Value = t320_idtipologiaproy;
			aParam[1] = new SqlParameter("@t320_denominacion", SqlDbType.Text, 50);
			aParam[1].Value = t320_denominacion;
			aParam[2] = new SqlParameter("@t320_facturable", SqlDbType.Bit, 1);
			aParam[2].Value = t320_facturable;
			aParam[3] = new SqlParameter("@t320_interno", SqlDbType.Bit, 1);
			aParam[3].Value = t320_interno;
			aParam[4] = new SqlParameter("@t320_especial", SqlDbType.Bit, 1);
			aParam[4].Value = t320_especial;
			aParam[5] = new SqlParameter("@t320_requierecontrato", SqlDbType.Bit, 1);
			aParam[5].Value = t320_requierecontrato;
			aParam[6] = new SqlParameter("@t320_orden", SqlDbType.TinyInt, 1);
			aParam[6].Value = t320_orden;

			aParam[7] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
			aParam[7].Value = nOrden;
			aParam[8] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
			aParam[8].Value = nAscDesc;

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
			return SqlHelper.ExecuteSqlDataReader("SUP_TIPOLOGIAPROY_C", aParam);
		}

		#endregion
	}
}
