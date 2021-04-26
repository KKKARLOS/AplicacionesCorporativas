using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : FIGURAPSN_NODO
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T643_FIGURAPSN_NODO
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	14/01/2011 12:57:40	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class FIGURAPSN_NODO
	{
		#region Propiedades y Atributos

		private int _t303_idnodo;
		public int t303_idnodo
		{
			get {return _t303_idnodo;}
			set { _t303_idnodo = value ;}
		}

		private int _t314_idusuario;
		public int t314_idusuario
		{
			get {return _t314_idusuario;}
			set { _t314_idusuario = value ;}
		}

		private string _t643_figura;
		public string t643_figura
		{
			get {return _t643_figura;}
			set { _t643_figura = value ;}
		}
		#endregion

		#region Constructor

		public FIGURAPSN_NODO() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T643_FIGURAPSN_NODO.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	14/01/2011 12:57:40
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t303_idnodo , int t314_idusuario , string t643_figura)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
			aParam[0].Value = t303_idnodo;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t643_figura", SqlDbType.Text, 1);
			aParam[2].Value = t643_figura;

			if (tr == null)
				 SqlHelper.ExecuteNonQuery("SUP_FIGURAPSN_NODO_I", aParam);
			else
				 SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURAPSN_NODO_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T643_FIGURAPSN_NODO a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	14/01/2011 12:57:40
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t303_idnodo, int t314_idusuario, string t643_figura)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
			aParam[0].Value = t303_idnodo;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t643_figura", SqlDbType.Text, 1);
			aParam[2].Value = t643_figura;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_FIGURAPSN_NODO_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURAPSN_NODO_D", aParam);
		}

		#endregion
	}
}
