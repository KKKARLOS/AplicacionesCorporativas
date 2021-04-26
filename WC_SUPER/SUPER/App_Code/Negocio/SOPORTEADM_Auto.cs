using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : SOPORTEADM
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T645_SOPORTEADM
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	28/02/2011 17:17:27	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class SOPORTEADM
	{
		#region Propiedades y Atributos

		private int _t001_idficepi;
		public int t001_idficepi
		{
			get {return _t001_idficepi;}
			set { _t001_idficepi = value ;}
		}
		#endregion

		#region Constructor

		public SOPORTEADM() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T645_SOPORTEADM.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	28/02/2011 17:17:27
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, int t001_idficepi)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
			aParam[0].Value = t001_idficepi;

			if (tr == null)
				 SqlHelper.ExecuteNonQuery("SUP_SOPORTEADM_I", aParam);
			else
				 SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_SOPORTEADM_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T645_SOPORTEADM a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	28/02/2011 17:17:27
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, int t001_idficepi)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
			aParam[0].Value = t001_idficepi;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_SOPORTEADM_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_SOPORTEADM_D", aParam);
		}

		#endregion
	}
}
