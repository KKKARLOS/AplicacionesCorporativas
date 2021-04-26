using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : FIGURAPSN_SN1
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T677_FIGURAPSN_SN1
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	26/09/2011 15:59:32	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class FIGURAPSN_SN1
	{
		#region Propiedades y Atributos

		private int _t391_idsupernodo1;
		public int t391_idsupernodo1
		{
			get {return _t391_idsupernodo1;}
			set { _t391_idsupernodo1 = value ;}
		}

		private int _t314_idusuario;
		public int t314_idusuario
		{
			get {return _t314_idusuario;}
			set { _t314_idusuario = value ;}
		}

		private string _t677_figura;
		public string t677_figura
		{
			get {return _t677_figura;}
			set { _t677_figura = value ;}
		}
		#endregion

		#region Constructor

		public FIGURAPSN_SN1() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T677_FIGURAPSN_SN1.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	26/09/2011 15:59:32
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t391_idsupernodo1 , int t314_idusuario , string t677_figura)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t391_idsupernodo1", SqlDbType.Int, 4);
			aParam[0].Value = t391_idsupernodo1;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t677_figura", SqlDbType.Text, 1);
			aParam[2].Value = t677_figura;

			if (tr == null)
				 SqlHelper.ExecuteNonQuery("SUP_FIGURAPSN_SN1_I", aParam);
			else
				 SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURAPSN_SN1_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T677_FIGURAPSN_SN1 a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	26/09/2011 15:59:32
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t391_idsupernodo1, int t314_idusuario, string t677_figura)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t391_idsupernodo1", SqlDbType.Int, 4);
			aParam[0].Value = t391_idsupernodo1;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t677_figura", SqlDbType.Text, 1);
			aParam[2].Value = t677_figura;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_FIGURAPSN_SN1_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURAPSN_SN1_D", aParam);
		}

		#endregion
	}
}
