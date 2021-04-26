using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
    /// Project	 : SUPER
	/// Class	 : ASUNTORECURSOS_PT
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: t413_ASUNTORECURSOS
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	28/01/2008 9:57:53	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class ASUNTORECURSOS_PT
	{
		#region Propiedades y Atributos

		private int _t314_idusuario;
		public int t314_idusuario
		{
			get {return _t314_idusuario;}
			set { _t314_idusuario = value ;}
		}

		private int _t409_idasunto;
		public int t409_idasunto
		{
			get {return _t409_idasunto;}
			set { _t409_idasunto = value ;}
		}

		private bool _t413_notificar;
		public bool t413_notificar
		{
			get {return _t413_notificar;}
			set { _t413_notificar = value ;}
		}
		#endregion

		#region Constructores

		public ASUNTORECURSOS_PT() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla t413_ASUNTORECURSOSPT.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 9:57:53
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t314_idusuario , int t409_idasunto , bool t413_notificar)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[0].Value = t314_idusuario;
			aParam[1] = new SqlParameter("@t409_idasunto", SqlDbType.Int, 4);
			aParam[1].Value = t409_idasunto;
			aParam[2] = new SqlParameter("@t413_notificar", SqlDbType.Bit, 1);
			aParam[2].Value = t413_notificar;

			if (tr == null)
				SqlHelper.ExecuteNonQuery("SUP_ASUNTORECURSOS_PT_I_SNE", aParam);
			else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ASUNTORECURSOS_PT_I_SNE", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Actualiza un registro de la tabla t413_ASUNTORECURSOSPT.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 9:57:53
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Update(SqlTransaction tr, int t314_idusuario, int t409_idasunto, bool t413_notificar)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[0].Value = t314_idusuario;
			aParam[1] = new SqlParameter("@t409_idasunto", SqlDbType.Int, 4);
			aParam[1].Value = t409_idasunto;
			aParam[2] = new SqlParameter("@t413_notificar", SqlDbType.Bit, 1);
			aParam[2].Value = t413_notificar;

			// Ejecuta la query y devuelve el numero de registros modificados.
			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_ASUNTORECURSOS_PT_U", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ASUNTORECURSOS_PT_U", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla t413_ASUNTORECURSOS a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 9:57:53
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t409_idasunto, int t314_idusuario)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t409_idasunto", SqlDbType.Int, 4);
			aParam[0].Value = t409_idasunto;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;

			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_ASUNTORECURSOS_PT_D", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ASUNTORECURSOS_PT_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Selecciona los registros de la tabla t413_ASUNTORECURSOSPT en función de una foreign key.
		/// </summary>
		/// <returns>DataSet</returns>
		/// <history>
		/// 	Creado por [sqladmin]	28/01/2008 9:57:53
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader SelectByt409_idasunto(SqlTransaction tr, int t409_idasunto) 
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t409_idasunto", SqlDbType.Int, 4);
			aParam[0].Value = t409_idasunto;

			if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_ASUNTORECURSOS_PT_SByT409_idasunto", aParam);
			else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_ASUNTORECURSOS_PT_SByT409_idasunto", aParam);
		}

		#endregion
	}
}
