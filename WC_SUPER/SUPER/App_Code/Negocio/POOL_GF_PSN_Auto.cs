using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : POOL_GF_PSN
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T432_POOL_GF_PSN
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	04/07/2008 12:25:54	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class POOL_GF_PSN
	{
		#region Propiedades y Atributos

		private int _t305_idproyectosubnodo;
		public int t305_idproyectosubnodo
		{
			get {return _t305_idproyectosubnodo;}
			set { _t305_idproyectosubnodo = value ;}
		}

		private int _t342_idgrupro;
		public int t342_idgrupro
		{
			get {return _t342_idgrupro;}
			set { _t342_idgrupro = value ;}
		}
		#endregion

		#region Constructor

		public POOL_GF_PSN() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos


		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T432_POOL_GF_PSN a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	04/07/2008 12:25:54
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t305_idproyectosubnodo, int t342_idgrupro)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
			aParam[0].Value = t305_idproyectosubnodo;
			aParam[1] = new SqlParameter("@t342_idgrupro", SqlDbType.Int, 4);
			aParam[1].Value = t342_idgrupro;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_POOL_GF_PSN_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_POOL_GF_PSN_D", aParam);
		}

		#endregion
	}
}
