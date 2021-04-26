using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : PERFILSUPER
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T347_PERFILSUPER
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	05/03/2008 15:28:11	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class PERFILSUPER
	{
		#region Propiedades y Atributos

		private int _t347_idperfilsuper;
		public int t347_idperfilsuper
		{
			get {return _t347_idperfilsuper;}
			set { _t347_idperfilsuper = value ;}
		}

		private string _t347_denominacion;
		public string t347_denominacion
		{
			get {return _t347_denominacion;}
			set { _t347_denominacion = value ;}
		}

		private decimal _t347_imptarifahor;
        public decimal t347_imptarifahor
		{
			get {return _t347_imptarifahor;}
			set { _t347_imptarifahor = value ;}
		}

        private decimal _t347_imptarifajor;
        public decimal t347_imptarifajor
		{
			get {return _t347_imptarifajor;}
			set { _t347_imptarifajor = value ;}
		}

		private int _t303_idnodo;
		public int t303_idnodo
		{
			get {return _t303_idnodo;}
			set { _t303_idnodo = value ;}
		}

		private int _t302_idcliente;
		public int t302_idcliente
		{
			get {return _t302_idcliente;}
			set { _t302_idcliente = value ;}
		}

		private short _t347_orden;
		public short t347_orden
		{
			get {return _t347_orden;}
			set { _t347_orden = value ;}
		}
		#endregion

		#region Constructores

		public PERFILSUPER() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T347_PERFILSUPER.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	05/03/2008 15:28:11
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, string t347_denominacion, decimal t347_imptarifahor, decimal t347_imptarifajor, Nullable<int> t303_idnodo, Nullable<int> t302_idcliente, short t347_orden, bool t347_estado)
		{
			SqlParameter[] aParam = new SqlParameter[7];
			aParam[0] = new SqlParameter("@t347_denominacion", SqlDbType.Text, 30);
			aParam[0].Value = t347_denominacion;
			aParam[1] = new SqlParameter("@t347_imptarifahor", SqlDbType.Money, 8);
			aParam[1].Value = t347_imptarifahor;
			aParam[2] = new SqlParameter("@t347_imptarifajor", SqlDbType.Money, 8);
			aParam[2].Value = t347_imptarifajor;
			aParam[3] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
			aParam[3].Value = t303_idnodo;
			aParam[4] = new SqlParameter("@t302_idcliente", SqlDbType.Int, 4);
			aParam[4].Value = t302_idcliente;
			aParam[5] = new SqlParameter("@t347_orden", SqlDbType.SmallInt, 2);
			aParam[5].Value = t347_orden;
            aParam[6] = new SqlParameter("@t347_estado", SqlDbType.Bit);
            aParam[6].Value = t347_estado;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_PERFILSUPER_I", aParam));
			else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_PERFILSUPER_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T347_PERFILSUPER.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	05/03/2008 15:28:11
		/// </history>
		/// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t347_idperfilsuper, string t347_denominacion, decimal t347_imptarifahor, decimal t347_imptarifajor, Nullable<int> t303_idnodo, Nullable<int> t302_idcliente, short t347_orden, bool t347_estado)
		{
			SqlParameter[] aParam = new SqlParameter[8];
			aParam[0] = new SqlParameter("@t347_idperfilsuper", SqlDbType.Int, 4);
			aParam[0].Value = t347_idperfilsuper;
			aParam[1] = new SqlParameter("@t347_denominacion", SqlDbType.Text, 30);
			aParam[1].Value = t347_denominacion;
			aParam[2] = new SqlParameter("@t347_imptarifahor", SqlDbType.Money, 8);
			aParam[2].Value = t347_imptarifahor;
			aParam[3] = new SqlParameter("@t347_imptarifajor", SqlDbType.Money, 8);
			aParam[3].Value = t347_imptarifajor;
			aParam[4] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
			aParam[4].Value = t303_idnodo;
			aParam[5] = new SqlParameter("@t302_idcliente", SqlDbType.Int, 4);
			aParam[5].Value = t302_idcliente;
			aParam[6] = new SqlParameter("@t347_orden", SqlDbType.SmallInt, 2);
			aParam[6].Value = t347_orden;
            aParam[7] = new SqlParameter("@t347_estado", SqlDbType.Bit);
            aParam[7].Value = t347_estado;
			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_PERFILSUPER_U", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PERFILSUPER_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T347_PERFILSUPER a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	05/03/2008 15:28:11
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t347_idperfilsuper)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t347_idperfilsuper", SqlDbType.Int, 4);
			aParam[0].Value = t347_idperfilsuper;

			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_PERFILSUPER_D", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PERFILSUPER_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Selecciona los registros de la tabla T347_PERFILSUPER en función de una foreign key.
		/// </summary>
		/// <returns>DataSet</returns>
		/// <history>
		/// 	Creado por [sqladmin]	05/03/2008 15:28:11
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader SelectByT302_idcliente(SqlTransaction tr, Nullable<int> t302_idcliente, Nullable<bool> t347_estado ) 
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t302_idcliente", SqlDbType.Int, 4);
			aParam[0].Value = t302_idcliente;
            aParam[1] = new SqlParameter("@t347_estado", SqlDbType.Bit, 1);
            aParam[1].Value = t347_estado;
			if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_PERFILSUPER_SByT302_idcliente", aParam);
			else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PERFILSUPER_SByT302_idcliente", aParam);
		}

        ///// -----------------------------------------------------------------------------
        ///// <summary>
        ///// Selecciona los registros de la tabla T347_PERFILSUPER en función de una foreign key.
        ///// </summary>
        ///// <returns>DataSet</returns>
        ///// <history>
        ///// 	Creado por [sqladmin]	05/03/2008 15:28:11
        ///// </history>
        ///// -----------------------------------------------------------------------------
        public static SqlDataReader SelectByT303_idnodo(SqlTransaction tr, Nullable<int> t303_idnodo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[0].Value = t303_idnodo;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_PERFILSUPER_SByT303_idnodo", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PERFILSUPER_SByT303_idnodo", aParam);
        }

		#endregion
	}
}
