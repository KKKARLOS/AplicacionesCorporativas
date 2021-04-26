using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : COSTENIVEL
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T380_COSTENIVEL
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	09/05/2008 9:20:41	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class COSTENIVEL
	{

		#region Propiedades y Atributos

		private int _t380_idNivelConsumo;
		public int t380_idNivelConsumo
		{
			get {return _t380_idNivelConsumo;}
			set { _t380_idNivelConsumo = value ;}
		}

		private int _t303_idnodo;
		public int t303_idnodo
		{
			get {return _t303_idnodo;}
			set { _t303_idnodo = value ;}
		}

		private string _t380_denominacion;
		public string t380_denominacion
		{
			get {return _t380_denominacion;}
			set { _t380_denominacion = value ;}
		}

        private decimal _t380_costenivelH;
        public decimal t380_costenivelH
		{
			get {return _t380_costenivelH;}
			set { _t380_costenivelH = value ;}
		}

        private decimal _t380_costenivelJ;
        public decimal t380_costenivelJ
		{
			get {return _t380_costenivelJ;}
			set { _t380_costenivelJ = value ;}
		}

		private byte _t380_orden;
		public byte t380_orden
		{
			get {return _t380_orden;}
			set { _t380_orden = value ;}
		}
		#endregion


		#region Constructores

		public COSTENIVEL() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion


		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T380_COSTENIVEL.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	09/05/2008 9:20:41
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, int t303_idnodo, string t380_denominacion, decimal t380_costenivelH, decimal t380_costenivelJ, byte t380_orden)
		{
			SqlParameter[] aParam = new SqlParameter[5];
			aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
			aParam[0].Value = t303_idnodo;
			aParam[1] = new SqlParameter("@t380_denominacion", SqlDbType.Text, 30);
			aParam[1].Value = t380_denominacion;
			aParam[2] = new SqlParameter("@t380_costenivelH", SqlDbType.Money, 8);
			aParam[2].Value = t380_costenivelH;
			aParam[3] = new SqlParameter("@t380_costenivelJ", SqlDbType.Money, 8);
			aParam[3].Value = t380_costenivelJ;
			aParam[4] = new SqlParameter("@t380_orden", SqlDbType.TinyInt, 1);
			aParam[4].Value = t380_orden;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_COSTENIVEL_I", aParam));
			else
				return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_COSTENIVEL_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T380_COSTENIVEL.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	09/05/2008 9:20:41
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t380_idNivelConsumo, int t303_idnodo, string t380_denominacion, decimal t380_costenivelH, decimal t380_costenivelJ, byte t380_orden)
		{
			SqlParameter[] aParam = new SqlParameter[6];
			aParam[0] = new SqlParameter("@t380_idNivelConsumo", SqlDbType.Int, 4);
			aParam[0].Value = t380_idNivelConsumo;
			aParam[1] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
			aParam[1].Value = t303_idnodo;
			aParam[2] = new SqlParameter("@t380_denominacion", SqlDbType.Text, 30);
			aParam[2].Value = t380_denominacion;
			aParam[3] = new SqlParameter("@t380_costenivelH", SqlDbType.Money, 8);
			aParam[3].Value = t380_costenivelH;
			aParam[4] = new SqlParameter("@t380_costenivelJ", SqlDbType.Money, 8);
			aParam[4].Value = t380_costenivelJ;
			aParam[5] = new SqlParameter("@t380_orden", SqlDbType.TinyInt, 1);
			aParam[5].Value = t380_orden;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_COSTENIVEL_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_COSTENIVEL_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T380_COSTENIVEL a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	09/05/2008 9:20:41
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t380_idNivelConsumo)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t380_idNivelConsumo", SqlDbType.Int, 4);
			aParam[0].Value = t380_idNivelConsumo;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_COSTENIVEL_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_COSTENIVEL_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T380_COSTENIVEL.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	09/05/2008 9:20:41
		/// </history>
		/// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo(Nullable<int> t380_idNivelConsumo, Nullable<int> t303_idnodo, string t380_denominacion, Nullable<decimal> t380_costenivelH, Nullable<decimal> t380_costenivelJ, Nullable<byte> t380_orden, byte nOrden, byte nAscDesc)
		{
			SqlParameter[] aParam = new SqlParameter[8];
			aParam[0] = new SqlParameter("@t380_idNivelConsumo", SqlDbType.Int, 4);
			aParam[0].Value = t380_idNivelConsumo;
			aParam[1] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
			aParam[1].Value = t303_idnodo;
			aParam[2] = new SqlParameter("@t380_denominacion", SqlDbType.Text, 30);
			aParam[2].Value = t380_denominacion;
			aParam[3] = new SqlParameter("@t380_costenivelH", SqlDbType.Money, 8);
			aParam[3].Value = t380_costenivelH;
			aParam[4] = new SqlParameter("@t380_costenivelJ", SqlDbType.Money, 8);
			aParam[4].Value = t380_costenivelJ;
			aParam[5] = new SqlParameter("@t380_orden", SqlDbType.TinyInt, 1);
			aParam[5].Value = t380_orden;

			aParam[6] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
			aParam[6].Value = nOrden;
			aParam[7] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
			aParam[7].Value = nAscDesc;

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
			return SqlHelper.ExecuteSqlDataReader("SUP_COSTENIVEL_C", aParam);
		}

        public static SqlDataReader CatalogoNivelesNodo_By_PSN(SqlTransaction tr, int t305_idproyectosubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_COSTENIVEL_NODO_By_PSN", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_COSTENIVEL_NODO_By_PSN", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Selecciona los registros de la tabla T380_COSTENIVEL en función de una foreign key.
        /// </summary>
        /// <returns>DataSet</returns>
        /// <history>
        /// 	Creado por [sqladmin]	04/12/2009 8:58:59
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader SelectByT303_idnodo(SqlTransaction tr, int t303_idnodo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[0].Value = t303_idnodo;


            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_COSTENIVEL_SByT303_idnodo", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_COSTENIVEL_SByT303_idnodo", aParam);
        }
		#endregion
	}
}
