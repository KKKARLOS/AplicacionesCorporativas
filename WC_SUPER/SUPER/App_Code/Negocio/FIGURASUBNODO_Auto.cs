using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : FIGURASUBNODO
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T309_FIGURASUBNODO
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	04/12/2008 18:00:22	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class FIGURASUBNODO
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

		private string _t309_figura;
		public string t309_figura
		{
			get {return _t309_figura;}
			set { _t309_figura = value ;}
		}
		#endregion

		#region Constructor

		public FIGURASUBNODO() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T309_FIGURASUBNODO.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	04/12/2008 18:00:22
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t304_idsubnodo , int t314_idusuario , string t309_figura)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t304_idsubnodo", SqlDbType.Int, 4);
			aParam[0].Value = t304_idsubnodo;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t309_figura", SqlDbType.Text, 1);
			aParam[2].Value = t309_figura;

			if (tr == null)
				 SqlHelper.ExecuteNonQuery("SUP_FIGURASUBNODO_I", aParam);
			else
				 SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURASUBNODO_I", aParam);
		}

		#endregion
	}
}
