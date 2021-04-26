using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	public partial class GRUPONAT
	{
		#region Metodos

        public static SqlDataReader CatalogoPorTipologia(int t320_idtipologiaproy)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t320_idtipologiaproy", SqlDbType.Int, 4);
            aParam[0].Value = t320_idtipologiaproy;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_GRUPONAT_CBy_TIPOLOGIA", aParam);
        }

        public static SqlDataReader CatalogoArbolPorTipologia(int t320_idtipologiaproy, Nullable<bool> t323_coste)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t320_idtipologiaproy", SqlDbType.Int, 4);
            aParam[0].Value = t320_idtipologiaproy;
            aParam[1] = new SqlParameter("@t323_coste", SqlDbType.Bit, 1);
            aParam[1].Value = t323_coste;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_GRUPONAT_ARBOL", aParam);
        }
        
		#endregion
	}
}
