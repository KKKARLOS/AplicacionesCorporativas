using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using SUPER.Capa_Datos;

namespace SUPER.DAL
{
    /// <summary>
    /// Descripción breve de PROVINCIA
    /// </summary>
    public class PROVINCIA
    {
        #region Metodos

        public static int UpdateZona(SqlTransaction tr, int t173_idprovincia, int t482_idzona)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t173_idprovincia", SqlDbType.Int, 4);
            aParam[0].Value = t173_idprovincia;
            aParam[1] = new SqlParameter("@t482_idzona", SqlDbType.Int, 4);
            aParam[1].Value = t482_idzona;

            // Ejecuta la query y devuelve el numero de registros modificados.

            if (tr == null)
                return SUPER.Capa_Datos.SqlHelper.ExecuteNonQuery("SUP_PROVZONA_U", aParam);
            else
                return SUPER.Capa_Datos.SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PROVZONA_U", aParam);
        }
        public static int ActivarDesactivar(SqlTransaction tr, int t173_idprovincia, bool t173_activo)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t173_idprovincia", SqlDbType.Int, 4, t173_idprovincia),
                ParametroSql.add("@t173_activo", SqlDbType.Bit, 1, t173_activo)
            };
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_PROV_ACTI_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PROV_ACTI_U", aParam);
        }
        public static SqlDataReader CatalogoDenominacion(string t484_denominacion, string sTipoBusqueda, Nullable<int> t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t173_denominacion", SqlDbType.Text, 50);
            aParam[0].Value = t484_denominacion;
            aParam[1] = new SqlParameter("@sTipoBusq", SqlDbType.Char, 1);
            aParam[1].Value = sTipoBusqueda;
            aParam[2] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[2].Value = t314_idusuario;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteSqlDataReader("SUP_PROVINCIA_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_PROVINCIA_CAT_USU", aParam);
        }
        #endregion
    }
}