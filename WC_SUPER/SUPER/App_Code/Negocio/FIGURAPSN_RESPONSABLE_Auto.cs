using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : FIGURAPSN_RESPONSABLE
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T642_FIGURAPSN_RESPONSABLE
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	14/01/2011 12:57:40	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class FIGURAPSN_RESPONSABLE
	{
		#region Propiedades y Atributos

		private int _t314_idusuario_responsable;
		public int t314_idusuario_responsable
		{
			get {return _t314_idusuario_responsable;}
			set { _t314_idusuario_responsable = value ;}
		}

		private int _t314_idusuario;
		public int t314_idusuario
		{
			get {return _t314_idusuario;}
			set { _t314_idusuario = value ;}
		}

		private string _t642_figura;
		public string t642_figura
		{
			get {return _t642_figura;}
			set { _t642_figura = value ;}
		}
		#endregion

		#region Constructor

		public FIGURAPSN_RESPONSABLE() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T642_FIGURAPSN_RESPONSABLE.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	14/01/2011 12:57:40
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t314_idusuario_responsable , int t314_idusuario , string t642_figura)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
			aParam[0].Value = t314_idusuario_responsable;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t642_figura", SqlDbType.Text, 1);
			aParam[2].Value = t642_figura;

			if (tr == null)
				 SqlHelper.ExecuteNonQuery("SUP_FIGURAPSN_RESPONSABLE_I", aParam);
			else
				 SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURAPSN_RESPONSABLE_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T642_FIGURAPSN_RESPONSABLE a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	14/01/2011 12:57:40
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t314_idusuario_responsable, int t314_idusuario, string t642_figura)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
			aParam[0].Value = t314_idusuario_responsable;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t642_figura", SqlDbType.Text, 1);
			aParam[2].Value = t642_figura;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_FIGURAPSN_RESPONSABLE_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURAPSN_RESPONSABLE_D", aParam);
		}

		#endregion
	}
}
