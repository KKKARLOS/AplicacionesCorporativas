using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;
//Para el HttpContext
using System.Web;

/// <summary>
/// Summary description for FIGURAPSN_RESPONSABLE
/// </summary>
namespace SUPER.Capa_Negocio
{
    public partial class FIGURAPSN_RESPONSABLE
    {
        public static SqlDataReader CatalogoFiguras(int nIdUser)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nIdUser", SqlDbType.Int, 4);
            aParam[0].Value = nIdUser;
            //aParam[1] = new SqlParameter("@bForaneos", SqlDbType.Bit, 1);
            //aParam[1].Value = (bool)HttpContext.Current.Session["FORANEOS"];

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_FIGURAPSN_RESPONSABLE_CF", aParam);
        }
        public static int DeleteUsuario(SqlTransaction tr, int t314_idusuario_responsable, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t314_idusuario_responsable", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario_responsable;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FIGURAPSN_RESPONSABLE_DEL", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURAPSN_RESPONSABLE_DEL", aParam);
        }
    }
}
