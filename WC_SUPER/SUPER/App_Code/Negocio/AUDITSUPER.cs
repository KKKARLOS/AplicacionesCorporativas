using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : AUDITSUPER
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
	/// Clase de acceso a datos para la tabla: T499_AUDITSUPER
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	12/02/2010 13:51:54	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class AUDITSUPER
	{
		#region Metodos

        public static SqlDataReader ObtenerCatalogo(int nPantalla, string sItem, string sPares, DateTime dDesde, DateTime dHasta)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            aParam[0] = new SqlParameter("@nPantalla", SqlDbType.Int, 4);
            aParam[0].Value = nPantalla;
            aParam[1] = new SqlParameter("@sItem", SqlDbType.VarChar, 8000);
            aParam[1].Value = sItem;
            aParam[2] = new SqlParameter("@sPares", SqlDbType.VarChar, 8000);
            aParam[2].Value = sPares;
            aParam[3] = new SqlParameter("@dDesde", SqlDbType.SmallDateTime, 4);
            aParam[3].Value = dDesde;
            aParam[4] = new SqlParameter("@dHasta", SqlDbType.SmallDateTime, 4);
            aParam[4].Value = dHasta;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_AUDITSUPER_CAT", aParam);
        }
        //public static SqlDataReader ObtenerDetalle(int t499_idaudit)
        //{
        //    SqlParameter[] aParam = new SqlParameter[1];
        //    aParam[0] = new SqlParameter("@t499_idaudit", SqlDbType.Int, 4);
        //    aParam[0].Value = t499_idaudit;

        //    return SqlHelper.ExecuteSqlDataReader("SUP_AUDITSUPER_DET", aParam);
        //}
        //public static SqlDataReader ObtenerDetalle(string t499_guidaudit)
        //{
        //    Guid idGuid = new Guid(t499_guidaudit);
        //    SqlParameter[] aParam = new SqlParameter[]{
        //        ParametroSql.add("@t499_guidaudit", SqlDbType.UniqueIdentifier, 16, idGuid),
        //    };

        //    return SqlHelper.ExecuteSqlDataReader("SUP_AUDITSUPER_DET", aParam);
        //}
        public static SqlDataReader ObtenerDetalle(Int64 t499_idaudit)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t499_idaudit", SqlDbType.BigInt, 8, t499_idaudit),
            };

            return SqlHelper.ExecuteSqlDataReader("SUP_AUDITSUPER_DET", aParam);
        }

		#endregion
	}
}
