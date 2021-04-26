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
    /// Descripción breve de PAIS
    /// </summary>
    public class PAIS
    {

        #region Metodos

        public static SqlDataReader Catalogo()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            // Ejecuta la query y devuelve un SqlDataReader con el resultado. 
            return SUPER.Capa_Datos.SqlHelper.ExecuteSqlDataReader("SUP_PAIS_C", aParam);

        }
        public static SqlDataReader Provincias(Nullable<int> t172_idpais)
        {
            SqlParameter[] aParam = new SqlParameter[1];

            aParam[0] = new SqlParameter("@t172_idpais", SqlDbType.Int, 4);
            aParam[0].Value = t172_idpais;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SUPER.Capa_Datos.SqlHelper.ExecuteSqlDataReader("SUP_PROVPAIS_C", aParam);
        }
        public static SqlDataReader Arbol()
        {
            SqlParameter[] aParam = new SqlParameter[0];

            // Ejecuta la query y devuelve un SqlDataReader con el resultado. 
            return SUPER.Capa_Datos.SqlHelper.ExecuteSqlDataReader("SUP_PAIS_PROVINCIA_CAT", aParam);
        }

        public static int ActivarDesactivar(SqlTransaction tr, int t172_idpais, bool t172_activo)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t172_idpais", SqlDbType.Int, 4, t172_idpais),
                ParametroSql.add("@t172_activo", SqlDbType.Bit, 1, t172_activo)
            };
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_PAIS_ACTI_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PAIS_ACTI_U", aParam);
        }
        public static SqlDataReader CatalogoDenominacion(string t484_denominacion, string sTipoBusqueda, Nullable<int> t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t172_denominacion", SqlDbType.Text, 50);
            aParam[0].Value = t484_denominacion;
            aParam[1] = new SqlParameter("@sTipoBusq", SqlDbType.Char, 1);
            aParam[1].Value = sTipoBusqueda;
            aParam[2] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[2].Value = t314_idusuario;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteSqlDataReader("SUP_PAIS_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_PAIS_CAT_USU", aParam);
        }
        #endregion
    }
}