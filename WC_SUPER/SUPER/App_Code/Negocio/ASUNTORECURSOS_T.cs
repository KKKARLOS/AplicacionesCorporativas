using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
    /// Project	 : SUPER
	/// Class	 : ASUNTORECURSOS_T
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: t604_ASUNTORECURSOS
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	28/01/2008 9:57:53	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class ASUNTORECURSOS_T
	{
		#region Propiedades y Atributos

		private int _t314_idusuario;
		public int t314_idusuario
		{
			get {return _t314_idusuario;}
			set { _t314_idusuario = value ;}
		}

		private int _t600_idasunto;
		public int t600_idasunto
		{
			get {return _t600_idasunto;}
			set { _t600_idasunto = value ;}
		}

		private bool _t604_notificar;
		public bool t604_notificar
		{
			get {return _t604_notificar;}
			set { _t604_notificar = value ;}
		}
		#endregion

		#region Constructores

		public ASUNTORECURSOS_T() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla t604_ASUNTORECURSOST.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 9:57:53
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t314_idusuario , int t600_idasunto , bool t604_notificar)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[0].Value = t314_idusuario;
			aParam[1] = new SqlParameter("@t600_idasunto", SqlDbType.Int, 4);
			aParam[1].Value = t600_idasunto;
			aParam[2] = new SqlParameter("@t604_notificar", SqlDbType.Bit, 1);
			aParam[2].Value = t604_notificar;

			if (tr == null)
				SqlHelper.ExecuteNonQuery("SUP_ASUNTORECURSOS_T_I_SNE", aParam);
			else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ASUNTORECURSOS_T_I_SNE", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla t604_ASUNTORECURSOST.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 9:57:53
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Update(SqlTransaction tr, int t314_idusuario, int t600_idasunto, bool t604_notificar)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[0].Value = t314_idusuario;
			aParam[1] = new SqlParameter("@t600_idasunto", SqlDbType.Int, 4);
			aParam[1].Value = t600_idasunto;
			aParam[2] = new SqlParameter("@t604_notificar", SqlDbType.Bit, 1);
			aParam[2].Value = t604_notificar;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_ASUNTORECURSOS_T_U", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ASUNTORECURSOS_T_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla t604_ASUNTORECURSOST a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 9:57:53
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t600_idasunto, int t314_idusuario)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t600_idasunto", SqlDbType.Int, 4);
			aParam[0].Value = t600_idasunto;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;

			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_ASUNTORECURSOS_T_D", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ASUNTORECURSOS_T_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Selecciona los registros de la tabla t604_ASUNTORECURSOST en función de una foreign key.
		/// </summary>
		/// <returns>DataSet</returns>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 9:57:53
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader SelectByt600_idasunto(SqlTransaction tr, int t600_idasunto) 
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t600_idasunto", SqlDbType.Int, 4);
			aParam[0].Value = t600_idasunto;

			if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_ASUNTORECURSOS_T_SByT600_idasunto", aParam);
			else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_ASUNTORECURSOS_T_SByT600_idasunto", aParam);
		}

		#endregion
	}
}
