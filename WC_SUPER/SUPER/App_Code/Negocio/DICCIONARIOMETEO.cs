using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : DICCIONARIOMETEO
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T497_DICCIONARIOMETEO
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	29/01/2010 13:15:59	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class DICCIONARIOMETEO
	{
		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Inserta un registro en la tabla T497_DICCIONARIOMETEO.
		/// </summary>
		/// <returns></returns>
		/// <history>
		/// 	Creado por [sqladmin]	29/01/2010 13:15:59
		/// </history>
		/// -----------------------------------------------------------------------------
		public static void InsertSiNoExiste(SqlTransaction tr, string t497_origen , string t479_resultado)
		{
			SqlParameter[] aParam = new SqlParameter[2];
			aParam[0] = new SqlParameter("@t497_origen", SqlDbType.Text, 50);
			aParam[0].Value = t497_origen;
			aParam[1] = new SqlParameter("@t479_resultado", SqlDbType.Text, 50);
			aParam[1].Value = t479_resultado;

			if (tr == null)
				 SqlHelper.ExecuteNonQuery("SUP_DICCIONARIOMETEO_ISNE", aParam);
			else
                 SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DICCIONARIOMETEO_ISNE", aParam);
		}

		public static SqlDataReader Catalogo()
		{
			SqlParameter[] aParam = new SqlParameter[0];
			return SqlHelper.ExecuteSqlDataReader("SUP_DICCIONARIOMETEO_CAT", aParam);
		}

		#endregion
	}
}
