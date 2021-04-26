using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
//using SUPER.Capa_Negocio;

namespace SUPER.Capa_Datos
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : MOTIVO
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T423_MOTIVO
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	28/02/2011 9:19:14	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class MOTIVO
	{
		#region Metodos

		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Obtiene un catálogo de registros de la tabla T423_MOTIVO.
		/// </summary>
		/// <history>
		/// 	Creado por [sqladmin]	28/02/2011 9:19:14
		/// </history>
		/// -----------------------------------------------------------------------------
		public static SqlDataReader Catalogo(Nullable<byte> t423_idmotivo, string t423_denominacion, Nullable<bool> t423_estado, Nullable<int> t423_cuenta, byte nOrden, byte nAscDesc)
		{
			SqlParameter[] aParam = new SqlParameter[6];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t423_idmotivo", SqlDbType.TinyInt, 1 , t423_idmotivo);
            aParam[i++] = ParametroSql.add("@t423_denominacion", SqlDbType.Text, 50, t423_denominacion);
            aParam[i++] = ParametroSql.add("@t423_estado", SqlDbType.Bit, 1, t423_estado);
            aParam[i++] = ParametroSql.add("@t423_cuenta", SqlDbType.Int, 4, t423_cuenta);
            aParam[i++] = ParametroSql.add("@nOrden", SqlDbType.TinyInt, 1, nOrden);
            aParam[i++] = ParametroSql.add("@nAscDesc", SqlDbType.TinyInt, 1, nAscDesc);

			// Ejecuta la query y devuelve un SqlDataReader con el resultado.
			return SqlHelper.ExecuteSqlDataReader("GVT_MOTIVO_C", aParam);
		}

		#endregion
	}
}
