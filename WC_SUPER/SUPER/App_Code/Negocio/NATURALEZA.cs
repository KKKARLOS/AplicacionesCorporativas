using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : NATURALEZA
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T323_NATURALEZA
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	16/06/2008 12:28:27	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class NATURALEZA
    {
        #region Metodos
        public static SqlDataReader CatalogoConPlantilla(int t322_idsubgruponat)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t322_idsubgruponat", SqlDbType.Int, 4);
            aParam[0].Value = t322_idsubgruponat;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_NATURALEZA_CPlant", aParam);
        }

        public static SqlDataReader NaturalezasPorTipologia(int t320_idtipologiaproy)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t320_idtipologiaproy", SqlDbType.Int, 4);
            aParam[0].Value = t320_idtipologiaproy;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_NATURALEZA_CByTipologia", aParam);
        }
        public static SqlDataReader CatalogoDenominacion(string t323_denominacion, string sTipoBusqueda, Nullable<int> t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t323_denominacion", SqlDbType.Text, 50);
            aParam[0].Value = t323_denominacion;
            aParam[1] = new SqlParameter("@sTipoBusq", SqlDbType.Char, 1);
            aParam[1].Value = sTipoBusqueda;
            aParam[2] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[2].Value = t314_idusuario;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteSqlDataReader("SUP_NATURALEZA_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_NATURALEZA_CAT_USU", aParam);
        }
        public static SqlDataReader CatalogoPIG()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_NATURALEZAIMP_CAT", aParam);
        }
        public static int UpdateDefectoVIG(SqlTransaction tr, int t323_idnaturaleza, byte t323_mesesvigenciaPIG, bool t323_replicaPIG)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t323_idnaturaleza", SqlDbType.Int, 4);
            aParam[0].Value = t323_idnaturaleza;
            aParam[1] = new SqlParameter("@t323_mesesvigenciaPIG", SqlDbType.TinyInt, 1);
            aParam[1].Value = t323_mesesvigenciaPIG;
            aParam[2] = new SqlParameter("@t323_replicaPIG", SqlDbType.Bit, 1);
            aParam[2].Value = t323_replicaPIG;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_NATURALEZA_UVIG", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_NATURALEZA_UVIG", aParam);
        }

        public static SqlDataReader GetEstructura(bool bMostrarInactivos)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@bMostrarInactivos", SqlDbType.Bit, 1);
            aParam[0].Value = bMostrarInactivos;

            return SqlHelper.ExecuteSqlDataReader("SUP_GETESTRUCTURA_NATURALEZA", aParam);
        }

        #endregion
    }
}
