using System;
using System.Data;
using System.Configuration;
using System.Text;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ESTADO
/// </summary>
/// 
namespace GEMO.DAL
{
    public class ESTADO
    {
        public static SqlDataReader Catalogo(string sTipoBusqueda, string t710_denominacion, Nullable<int> t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t710_denominacion", SqlDbType.Text, 50);
            aParam[0].Value = t710_denominacion;
            aParam[1] = new SqlParameter("@sTipoBusq", SqlDbType.Char, 1);
            aParam[1].Value = sTipoBusqueda;

            //aParam[2] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            //aParam[2].Value = t314_idusuario;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            //if (HttpContext.Current.Session["AdminActual"].ToString() != "")
            return SqlHelper.ExecuteSqlDataReader("GEM_ESTADO_CAT", aParam);
            //else
            //    return SqlHelper.ExecuteSqlDataReader("SUP_SEGMENTO_CAT_USU", aParam);
        }
        public static SqlDataReader Catalogo2()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            return SqlHelper.ExecuteSqlDataReader("GEM_ESTADO_CAT2", aParam);
            //else
            //    return SqlHelper.ExecuteSqlDataReader("SUP_SEGMENTO_CAT_USU", aParam);
        }
    }
}