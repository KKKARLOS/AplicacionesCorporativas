using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : FIGURAPROYECTOSUBNODO
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T310_FIGURAPROYECTOSUBNODO
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	23/06/2008 10:00:14	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class FIGURAPROYECTOSUBNODO
	{
		#region Propiedades y Atributos

		private int _t305_idproyectosubnodo;
		public int t305_idproyectosubnodo
		{
			get {return _t305_idproyectosubnodo;}
			set { _t305_idproyectosubnodo = value ;}
		}

		private int _t314_idusuario;
		public int t314_idusuario
		{
			get {return _t314_idusuario;}
			set { _t314_idusuario = value ;}
		}

		private string _t310_figura;
		public string t310_figura
		{
			get {return _t310_figura;}
			set { _t310_figura = value ;}
		}
		#endregion

		#region Constructores

		public FIGURAPROYECTOSUBNODO() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T310_FIGURAPROYECTOSUBNODO.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	23/06/2008 10:00:14
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t305_idproyectosubnodo , int t314_idusuario , string t310_figura)
		{
			SqlParameter[] aParam = new SqlParameter[3];
			aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
			aParam[0].Value = t305_idproyectosubnodo;
			aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[1].Value = t314_idusuario;
			aParam[2] = new SqlParameter("@t310_figura", SqlDbType.Text, 1);
			aParam[2].Value = t310_figura;

			if (tr == null)
				 SqlHelper.ExecuteNonQuery("SUP_FIGURAPROYECTOSUBNODO_I", aParam);
			else
				 SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURAPROYECTOSUBNODO_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Borra los registros de la tabla T310_FIGURAPROYECTOSUBNODO en función de una foreign key.
		/// </summary>
		/// <remarks>
		/// 	Creado por [sqladmin]	23/06/2008 10:00:14
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void DeleteByT314_idusuario(SqlTransaction tr, int t314_idusuario)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
			aParam[0].Value = t314_idusuario;


			if (tr == null)
				SqlHelper.ExecuteNonQuery("SUP_FIGURAPROYECTOSUBNODO_DByT314_idusuario", aParam);
			else
				SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURAPROYECTOSUBNODO_DByT314_idusuario", aParam);
		}

		#endregion
	}
}
