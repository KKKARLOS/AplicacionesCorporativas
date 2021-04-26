using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : ORGVENTASSAP
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T621_ORGVENTASSAP
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	03/11/2010 16:47:25	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class ORGVENTASSAP
	{
		#region Constructor

		public ORGVENTASSAP() {
			//En el constructor vacio, se inicializan los atributos
			//con los valores predeterminados según el tipo de dato.
		}

		#endregion

		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T621_ORGVENTASSAP.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	03/11/2010 16:47:25
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void Insert(SqlTransaction tr, string t621_idovsap)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t621_idovsap", SqlDbType.Text, 4);
			aParam[0].Value = t621_idovsap;

			if (tr == null)
				 SqlHelper.ExecuteNonQuery("SUP_ORGVENTASSAP_I", aParam);
			else
				 SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ORGVENTASSAP_I", aParam);
		}

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Elimina un registro de la tabla T621_ORGVENTASSAP a traves de la primary key.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	03/11/2010 16:47:25
		/// </history>
		/// -----------------------------------------------------------------------------
		public static int Delete(SqlTransaction tr, string t621_idovsap)
		{
			SqlParameter[] aParam = new SqlParameter[1];
			aParam[0] = new SqlParameter("@t621_idovsap", SqlDbType.Text, 4);
			aParam[0].Value = t621_idovsap;

			if (tr == null)
				return SqlHelper.ExecuteNonQuery("SUP_ORGVENTASSAP_D", aParam);
			else
				return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ORGVENTASSAP_D", aParam);
		}

		#endregion
	}
}
