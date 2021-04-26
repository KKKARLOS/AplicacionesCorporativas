using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : CATEGSUPER
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T450_CATEGSUPER
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	02/09/2009 13:17:04	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class CATEGSUPER
	{
		#region Propiedades y Atributos

		private int _t450_idcategsuper;
		public int t450_idcategsuper
		{
			get {return _t450_idcategsuper;}
			set { _t450_idcategsuper = value ;}
		}

		private string _t450_denominacion;
		public string t450_denominacion
		{
			get {return _t450_denominacion;}
			set { _t450_denominacion = value ;}
		}

		private decimal _t450_costemediohora;
		public decimal t450_costemediohora
		{
			get {return _t450_costemediohora;}
			set { _t450_costemediohora = value ;}
		}

		private decimal _t450_costemediojornada;
		public decimal t450_costemediojornada
		{
			get {return _t450_costemediojornada;}
			set { _t450_costemediojornada = value ;}
		}
		#endregion

		#region Constructor

		public CATEGSUPER() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T450_CATEGSUPER.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	02/09/2009 13:17:04
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Insert(SqlTransaction tr, string t450_denominacion , decimal t450_costemediohora , decimal t450_costemediojornada)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t450_denominacion", SqlDbType.Text, 25);
			aParam[0].Value = t450_denominacion;
			aParam[1] = new SqlParameter("@t450_costemediohora", SqlDbType.Money, 8);
			aParam[1].Value = t450_costemediohora;
			aParam[2] = new SqlParameter("@t450_costemediojornada", SqlDbType.Money, 8);
			aParam[2].Value = t450_costemediojornada;

			// Ejecuta la query y devuelve el valor del nuevo Identity.
			if (tr == null)
				return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_CATEGSUPER_I", aParam));
			else
				return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CATEGSUPER_I", aParam));
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla T450_CATEGSUPER.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	02/09/2009 13:17:04
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Update(SqlTransaction tr, int t450_idcategsuper, string t450_denominacion, decimal t450_costemediohora, decimal t450_costemediojornada)
		{
			SqlParameter[] aParam = new SqlParameter[4];
			aParam[0] = new SqlParameter("@t450_idcategsuper", SqlDbType.Int, 4);
			aParam[0].Value = t450_idcategsuper;
			aParam[1] = new SqlParameter("@t450_denominacion", SqlDbType.Text, 25);
			aParam[1].Value = t450_denominacion;
			aParam[2] = new SqlParameter("@t450_costemediohora", SqlDbType.Money, 8);
			aParam[2].Value = t450_costemediohora;
			aParam[3] = new SqlParameter("@t450_costemediojornada", SqlDbType.Money, 8);
			aParam[3].Value = t450_costemediojornada;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_CATEGSUPER_U", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CATEGSUPER_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T450_CATEGSUPER a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	02/09/2009 13:17:04
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t450_idcategsuper)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t450_idcategsuper", SqlDbType.Int, 4);
			aParam[0].Value = t450_idcategsuper;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_CATEGSUPER_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CATEGSUPER_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T450_CATEGSUPER.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	02/09/2009 13:17:04
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader Catalogo(Nullable<int> t450_idcategsuper, string t450_denominacion, Nullable<decimal> t450_costemediohora, Nullable<decimal> t450_costemediojornada, byte nOrden, byte nAscDesc)
		{
			SqlParameter[] aParam = new SqlParameter[6];
			aParam[0] = new SqlParameter("@t450_idcategsuper", SqlDbType.Int, 4);
			aParam[0].Value = t450_idcategsuper;
			aParam[1] = new SqlParameter("@t450_denominacion", SqlDbType.Text, 25);
			aParam[1].Value = t450_denominacion;
			aParam[2] = new SqlParameter("@t450_costemediohora", SqlDbType.Money, 8);
			aParam[2].Value = t450_costemediohora;
			aParam[3] = new SqlParameter("@t450_costemediojornada", SqlDbType.Money, 8);
			aParam[3].Value = t450_costemediojornada;

			aParam[4] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
			aParam[4].Value = nOrden;
			aParam[5] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
			aParam[5].Value = nAscDesc;

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
			return SqlHelper.ExecuteSqlDataReader("SUP_CATEGSUPER_C", aParam);
		}

		#endregion
	}
}
