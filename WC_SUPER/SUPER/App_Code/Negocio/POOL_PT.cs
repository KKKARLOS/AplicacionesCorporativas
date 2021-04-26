using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : POOL_PT
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T402_POOL_PT
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	27/12/2007 9:49:54	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class POOL_PT
	{
		#region Propiedades y Atributos

		private int _t331_idpt;
		public int t331_idpt
		{
			get {return _t331_idpt;}
			set { _t331_idpt = value ;}
		}

		private int _t314_idusuario;
		public int t314_idusuario
		{
			get {return _t314_idusuario;}
			set { _t314_idusuario = value ;}
		}
		#endregion

		#region Constructores

		public POOL_PT() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T402_POOL_PT.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	27/12/2007 9:49:54
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t331_idpt , int t314_idusuario)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
			aParam[0].Value = t331_idpt;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;

			if (tr == null)
				SqlHelper.ExecuteNonQuery("SUP_POOL_PT_I_SNE", aParam);
			else
				SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_POOL_PT_I_SNE", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T402_POOL_PT a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	27/12/2007 9:49:54
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t331_idpt, int t314_idusuario)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
			aParam[0].Value = t331_idpt;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;

			if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_POOL_PT_D", aParam);
			else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_POOL_PT_D", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T402_POOL_PT.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	27/12/2007 9:49:54
		/// </history>
		/// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo(int t331_idpt, bool bMostrarBajas)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
			aParam[0].Value = t331_idpt;
            aParam[1] = new SqlParameter("@bMostrarBajas", SqlDbType.Bit, 1);
            aParam[1].Value = bMostrarBajas;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_POOL_PT_C", aParam);
		}

		#endregion
	}
}
