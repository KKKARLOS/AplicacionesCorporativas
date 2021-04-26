using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : FIGURAPSN_SN2
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T678_FIGURAPSN_SN2
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	26/09/2011 15:59:32	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class FIGURAPSN_SN2
	{
		#region Propiedades y Atributos

		private int _t392_idsupernodo2;
		public int t392_idsupernodo2
		{
			get {return _t392_idsupernodo2;}
			set { _t392_idsupernodo2 = value ;}
		}

		private int _t314_idusuario;
		public int t314_idusuario
		{
			get {return _t314_idusuario;}
			set { _t314_idusuario = value ;}
		}

		private string _t678_figura;
		public string t678_figura
		{
			get {return _t678_figura;}
			set { _t678_figura = value ;}
		}
		#endregion

		#region Constructor

		public FIGURAPSN_SN2() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T678_FIGURAPSN_SN2.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	26/09/2011 15:59:32
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t392_idsupernodo2 , int t314_idusuario , string t678_figura)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t392_idsupernodo2", SqlDbType.Int, 4);
			aParam[0].Value = t392_idsupernodo2;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t678_figura", SqlDbType.Text, 1);
			aParam[2].Value = t678_figura;

			if (tr == null)
				 SqlHelper.ExecuteNonQuery("SUP_FIGURAPSN_SN2_I", aParam);
			else
				 SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURAPSN_SN2_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T678_FIGURAPSN_SN2 a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	26/09/2011 15:59:32
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t392_idsupernodo2, int t314_idusuario, string t678_figura)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t392_idsupernodo2", SqlDbType.Int, 4);
			aParam[0].Value = t392_idsupernodo2;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t678_figura", SqlDbType.Text, 1);
			aParam[2].Value = t678_figura;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_FIGURAPSN_SN2_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURAPSN_SN2_D", aParam);
		}

		#endregion
	}
}
