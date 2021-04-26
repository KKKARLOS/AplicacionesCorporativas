using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : FIGURAPSN_SN4
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T680_FIGURAPSN_SN4
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	26/09/2011 15:59:32	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class FIGURAPSN_SN4
	{
		#region Propiedades y Atributos

		private int _t394_idsupernodo4;
		public int t394_idsupernodo4
		{
			get {return _t394_idsupernodo4;}
			set { _t394_idsupernodo4 = value ;}
		}

		private int _t314_idusuario;
		public int t314_idusuario
		{
			get {return _t314_idusuario;}
			set { _t314_idusuario = value ;}
		}

		private string _t680_figura;
		public string t680_figura
		{
			get {return _t680_figura;}
			set { _t680_figura = value ;}
		}
		#endregion

		#region Constructor

		public FIGURAPSN_SN4() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T680_FIGURAPSN_SN4.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	26/09/2011 15:59:32
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t394_idsupernodo4 , int t314_idusuario , string t680_figura)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t394_idsupernodo4", SqlDbType.Int, 4);
			aParam[0].Value = t394_idsupernodo4;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t680_figura", SqlDbType.Text, 1);
			aParam[2].Value = t680_figura;

			if (tr == null)
				 SqlHelper.ExecuteNonQuery("SUP_FIGURAPSN_SN4_I", aParam);
			else
				 SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURAPSN_SN4_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T680_FIGURAPSN_SN4 a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	26/09/2011 15:59:32
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t394_idsupernodo4, int t314_idusuario, string t680_figura)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t394_idsupernodo4", SqlDbType.Int, 4);
			aParam[0].Value = t394_idsupernodo4;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t680_figura", SqlDbType.Text, 1);
			aParam[2].Value = t680_figura;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_FIGURAPSN_SN4_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURAPSN_SN4_D", aParam);
		}

		#endregion
	}
}
