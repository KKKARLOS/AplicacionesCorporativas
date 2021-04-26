using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;
//Para el HttpContext
using System.Web;

/// <summary>
/// Summary description for FIGURAPSN_NODO
/// </summary>
namespace SUPER.Capa_Negocio
{
    public partial class FIGURAPSN_NODO
    {
        public static SqlDataReader CatalogoFiguras(int t303_idnodo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[0].Value = t303_idnodo;
            //aParam[1] = new SqlParameter("@bForaneos", SqlDbType.Bit, 1);
            //aParam[1].Value = (bool)HttpContext.Current.Session["FORANEOS"];

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_FIGURAPSN_NODO_CF", aParam);
        }
        public static int DeleteUsuario(SqlTransaction tr, int t303_idnodo, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[0].Value = t303_idnodo;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = t314_idusuario;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_FIGURAPSN_NODO_DEL", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FIGURAPSN_NODO_DEL", aParam);
        }
        public static int Duplicar(SqlTransaction tr, int t303_idnodoOrigen, int t303_idnodoDestino)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@idNodoOri", SqlDbType.Int, 4);
            aParam[0].Value = t303_idnodoOrigen;
            aParam[1] = new SqlParameter("@idNodoDes", SqlDbType.Int, 4);
            aParam[1].Value = t303_idnodoDestino;

            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_FIGURAPSN_NODO_I_COP", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_FIGURAPSN_NODO_I_COP", aParam));
        }
    }
}
