using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

/// <summary>
/// Summary description for FIGURAPSN_CONTRATO_POOL
/// </summary>
namespace SUPER.Capa_Negocio
{
    public partial class FIGURAPSN_CONTRATO_POOL
    {
        public static SqlDataReader CatalogoFiguras()
        {
            SqlParameter[] aParam = new SqlParameter[0];

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_FIGURAPSN_CONTRATO_POOL_CF", aParam);
        }
        public static int DeleteUsuario(SqlTransaction tr, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FIGURAPSN_CONTRATO_POOL_DEL", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURAPSN_CONTRATO_POOL_DEL", aParam);
        }
    }
}