using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	/// -----------------------------------------------------------------------------
	/// Project	 : SUPER
	/// Class	 : FIGURAPSN_SN1
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
    /// Clase de acceso a datos para la tabla: T677_FIGURAPSN_SN1
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	26/09/2011 15:59:31	
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class FIGURAPSN_SN1
	{
        public static SqlDataReader CatalogoFiguras(int t391_idsupernodo1)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t391_idsupernodo1", SqlDbType.Int, 4);
            aParam[0].Value = t391_idsupernodo1;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_FIGURAPSN_SN1_CF", aParam);
        }
        public static int DeleteUsuario(SqlTransaction tr, int t391_idsupernodo1, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t391_idsupernodo1", SqlDbType.Int, 4);
            aParam[0].Value = t391_idsupernodo1;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FIGURAPSN_SN1_DEL", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURAPSN_SN1_DEL", aParam);
        }
    }
}
