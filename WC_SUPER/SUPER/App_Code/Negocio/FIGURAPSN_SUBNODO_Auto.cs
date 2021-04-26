using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : FIGURAPSN_SUBNODO
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T676_FIGURAPSN_SUBNODO
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	26/09/2011 15:59:31	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class FIGURAPSN_SUBNODO
	{
		#region Propiedades y Atributos

		private int _t304_idsubnodo;
		public int t304_idsubnodo
		{
			get {return _t304_idsubnodo;}
			set { _t304_idsubnodo = value ;}
		}

		private int _t314_idusuario;
		public int t314_idusuario
		{
			get {return _t314_idusuario;}
			set { _t314_idusuario = value ;}
		}

		private string _t676_figura;
		public string t676_figura
		{
			get {return _t676_figura;}
			set { _t676_figura = value ;}
		}
		#endregion

		#region Constructor

		public FIGURAPSN_SUBNODO() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T676_FIGURAPSN_SUBNODO.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	26/09/2011 15:59:31
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t304_idsubnodo , int t314_idusuario , string t676_figura)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t304_idsubnodo", SqlDbType.Int, 4);
			aParam[0].Value = t304_idsubnodo;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t676_figura", SqlDbType.Text, 1);
			aParam[2].Value = t676_figura;

			if (tr == null)
				 SqlHelper.ExecuteNonQuery("SUP_FIGURAPSN_SUBNODO_I", aParam);
			else
				 SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURAPSN_SUBNODO_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T676_FIGURAPSN_SUBNODO a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	26/09/2011 15:59:31
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t304_idsubnodo, int t314_idusuario, string t676_figura)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t304_idsubnodo", SqlDbType.Int, 4);
			aParam[0].Value = t304_idsubnodo;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t676_figura", SqlDbType.Text, 1);
			aParam[2].Value = t676_figura;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_FIGURAPSN_SUBNODO_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURAPSN_SUBNODO_D", aParam);
		}

		#endregion
	}
}
