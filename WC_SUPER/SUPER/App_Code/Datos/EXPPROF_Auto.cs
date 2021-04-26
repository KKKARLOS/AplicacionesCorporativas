using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.DAL
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : EXPPROF
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T808_EXPPROF
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	30/07/2012 12:27:05	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class EXPPROF
	{
		#region Propiedades y Atributos

		private int _t808_idexpprof;
		public int t808_idexpprof
		{
			get {return _t808_idexpprof;}
			set { _t808_idexpprof = value ;}
		}

		private string _t808_denominacion;
		public string t808_denominacion
		{
			get {return _t808_denominacion;}
			set { _t808_denominacion = value ;}
		}

		private string _t808_descripcion;
		public string t808_descripcion
		{
			get {return _t808_descripcion;}
			set { _t808_descripcion = value ;}
		}

		private bool _t808_enibermatica;
		public bool t808_enibermatica
		{
			get {return _t808_enibermatica;}
			set { _t808_enibermatica = value ;}
		}

		private int? _t811_idcuenta_ori;
		public int? t811_idcuenta_ori
		{
			get {return _t811_idcuenta_ori;}
			set { _t811_idcuenta_ori = value ;}
		}

		private int? _t811_idcuenta_para;
		public int? t811_idcuenta_para
		{
			get {return _t811_idcuenta_para;}
			set { _t811_idcuenta_para = value ;}
		}

		private int? _t302_idcliente;
		public int? t302_idcliente
		{
			get {return _t302_idcliente;}
			set { _t302_idcliente = value ;}
		}

		private int? _t313_idempresa;
		public int? t313_idempresa
		{
			get {return _t313_idempresa;}
			set { _t313_idempresa = value ;}
		}

		#endregion

		#region Constructor

		public EXPPROF() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T808_EXPPROF.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	30/07/2012 12:27:05
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Insert(SqlTransaction tr, string t808_denominacion , string t808_descripcion , bool t808_enibermatica , Nullable<int> t811_idcuenta_ori , Nullable<int> t811_idcuenta_para , Nullable<int> t302_idcliente , Nullable<int> t313_idempresa)
		{
			SqlParameter[] aParam = new SqlParameter[7];
			int i = 0;
			aParam[i++] = ParametroSql.add("@t808_denominacion", SqlDbType.VarChar, 70, t808_denominacion);
			aParam[i++] = ParametroSql.add("@t808_descripcion", SqlDbType.Text, 2147483647, t808_descripcion);
			aParam[i++] = ParametroSql.add("@t808_enibermatica", SqlDbType.Bit, 1, t808_enibermatica);
			aParam[i++] = ParametroSql.add("@t811_idcuenta_ori", SqlDbType.Int, 4, t811_idcuenta_ori);
			aParam[i++] = ParametroSql.add("@t811_idcuenta_para", SqlDbType.Int, 4, t811_idcuenta_para);
			aParam[i++] = ParametroSql.add("@t302_idcliente", SqlDbType.Int, 4, t302_idcliente);
			aParam[i++] = ParametroSql.add("@t313_idempresa", SqlDbType.Int, 4, t313_idempresa);

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
				return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_CVT_EXPPROF_I", aParam));
			else
				return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CVT_EXPPROF_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T808_EXPPROF.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	30/07/2012 12:27:05
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Update(SqlTransaction tr, int t808_idexpprof, string t808_denominacion, string t808_descripcion, bool t808_enibermatica, Nullable<int> t811_idcuenta_ori, Nullable<int> t811_idcuenta_para, Nullable<int> t302_idcliente, Nullable<int> t313_idempresa)
		{
			SqlParameter[] aParam = new SqlParameter[8];
			int i = 0;
			aParam[i++] = ParametroSql.add("@t808_idexpprof", SqlDbType.Int, 4, t808_idexpprof);
			aParam[i++] = ParametroSql.add("@t808_denominacion", SqlDbType.VarChar, 70, t808_denominacion);
			aParam[i++] = ParametroSql.add("@t808_descripcion", SqlDbType.Text, 2147483647, t808_descripcion);
			aParam[i++] = ParametroSql.add("@t808_enibermatica", SqlDbType.Bit, 1, t808_enibermatica);
			aParam[i++] = ParametroSql.add("@t811_idcuenta_ori", SqlDbType.Int, 4, t811_idcuenta_ori);
			aParam[i++] = ParametroSql.add("@t811_idcuenta_para", SqlDbType.Int, 4, t811_idcuenta_para);
			aParam[i++] = ParametroSql.add("@t302_idcliente", SqlDbType.Int, 4, t302_idcliente);
			aParam[i++] = ParametroSql.add("@t313_idempresa", SqlDbType.Int, 4, t313_idempresa);

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_CVT_EXPPROF_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_EXPPROF_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T808_EXPPROF a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	30/07/2012 12:27:05
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t808_idexpprof)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			int i = 0;
			aParam[i++] = ParametroSql.add("@t808_idexpprof", SqlDbType.Int, 4, t808_idexpprof);

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_CVT_EXPPROF_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_EXPPROF_D", aParam);
		}

		#endregion
	}
}
