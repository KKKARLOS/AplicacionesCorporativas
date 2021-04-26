using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : TARIFAPROY
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T333_PERFILPROY
	/// </summary>
	/// <history>
	/// 	Creado por [DOARHUMI]	20/11/2007 10:28:45	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class TARIFAPROY
	{
		#region Propiedades y Atributos

		private int _t333_idperfilproy;
		public int t333_idperfilproy
		{
			get {return _t333_idperfilproy;}
			set { _t333_idperfilproy = value ;}
		}

		private int _t301_idproyecto;
		public int t301_idproyecto
		{
			get {return _t301_idproyecto;}
			set { _t301_idproyecto = value ;}
		}

		private string _t333_denominacion;
		public string t333_denominacion
		{
			get {return _t333_denominacion;}
			set { _t333_denominacion = value ;}
		}

        private decimal _t333_imptarifa;
        public decimal t333_imptarifa
		{
			get {return _t333_imptarifa;}
			set { _t333_imptarifa = value ;}
		}
		#endregion

		#region Constructores

		public TARIFAPROY() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T333_PERFILPROY.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 10:28:45
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, int t301_idproyecto, string t333_denominacion, decimal t333_imptarifa)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
			aParam[0].Value = t301_idproyecto;
			aParam[1] = new SqlParameter("@t333_denominacion", SqlDbType.Text, 25);
			aParam[1].Value = t333_denominacion;
			aParam[2] = new SqlParameter("@t333_imptarifa", SqlDbType.Real, 4);
			aParam[2].Value = t333_imptarifa;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_TARIFAPROY_I", aParam));
			else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_TARIFAPROY_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T333_PERFILPROY.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 10:28:45
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t333_idperfilproy, int t301_idproyecto, string t333_denominacion, decimal t333_imptarifa)
		{
			SqlParameter[] aParam = new SqlParameter[4];
			aParam[0] = new SqlParameter("@t333_idperfilproy", SqlDbType.Int, 4);
			aParam[0].Value = t333_idperfilproy;
			aParam[1] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
			aParam[1].Value = t301_idproyecto;
			aParam[2] = new SqlParameter("@t333_denominacion", SqlDbType.Text, 25);
			aParam[2].Value = t333_denominacion;
			aParam[3] = new SqlParameter("@t333_imptarifa", SqlDbType.Real, 4);
			aParam[3].Value = t333_imptarifa;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_TARIFAPROY_U", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_TARIFAPROY_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T333_PERFILPROY a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 10:28:45
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Delete(SqlTransaction tr, int t333_idperfilproy)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t333_idperfilproy", SqlDbType.Int, 4);
			aParam[0].Value = t333_idperfilproy;

			if (tr == null)
				SqlHelper.ExecuteNonQuery("SUP_TARIFAPROY_D", aParam);
			else
				SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_TARIFAPROY_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un registro de la tabla T333_PERFILPROY,
		/// y devuelve una instancia u objeto del tipo TARIFAPROY
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 10:28:45
		/// </history>
		/// -----------------------------------------------------------------------------
		public static TARIFAPROY Select(int t333_idperfilproy) 
		{
			TARIFAPROY o = new TARIFAPROY();

			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t333_idperfilproy", SqlDbType.Int, 4);
			aParam[0].Value = t333_idperfilproy;

			SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("SUP_TARIFAPROY_S", aParam);

			if (dr.Read())
			{
				if (dr["t333_idperfilproy"] != DBNull.Value)
					o.t333_idperfilproy = (int)dr["t333_idperfilproy"];
				if (dr["t301_idproyecto"] != DBNull.Value)
					o.t301_idproyecto = (int)dr["t301_idproyecto"];
				if (dr["t333_denominacion"] != DBNull.Value)
					o.t333_denominacion = (string)dr["t333_denominacion"];
				if (dr["t333_imptarifa"] != DBNull.Value)
                    o.t333_imptarifa = decimal.Parse(dr["t333_imptarifa"].ToString());

            }
			else
			{
				throw (new NullReferenceException("No se ha obtenido ningun dato de TARIFAPROY"));
			}

            dr.Close();
            dr.Dispose();
            return o;
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Selecciona los registros de la tabla T333_PERFILPROY en función de una foreign key.
		/// </summary>
		/// <returns>DataSet</returns>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 10:28:45
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader  SelectByt301_idproyecto(int t301_idproyecto) 
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
			aParam[0].Value = t301_idproyecto;

            return SqlHelper.ExecuteSqlDataReader("SUP_TARIFAPROY_SByt301_idproyecto", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T333_PERFILPROY.
		/// </summary>
		/// <history>
		/// 	Creado por [DOARHUMI]	20/11/2007 10:28:45
		/// </history>
		/// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo(int t333_idperfilproy, int t301_idproyecto, string t333_denominacion, decimal t333_imptarifa, byte nOrden, byte nAscDes)
		{
			SqlParameter[] aParam = new SqlParameter[6];
			aParam[0] = new SqlParameter("@t333_idperfilproy", SqlDbType.Int, 4);
			aParam[0].Value = t333_idperfilproy;
			aParam[1] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
			aParam[1].Value = t301_idproyecto;
			aParam[2] = new SqlParameter("@t333_denominacion", SqlDbType.Text, 25);
			aParam[2].Value = t333_denominacion;
			aParam[3] = new SqlParameter("@t333_imptarifa", SqlDbType.Real, 4);
			aParam[3].Value = t333_imptarifa;

			aParam[4] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
			aParam[4].Value = nOrden;
			aParam[5] = new SqlParameter("@nAscDes", SqlDbType.TinyInt, 1);
			aParam[5].Value = nAscDes;

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_TARIFAPROY_C", aParam);
		}

		#endregion
	}
}
