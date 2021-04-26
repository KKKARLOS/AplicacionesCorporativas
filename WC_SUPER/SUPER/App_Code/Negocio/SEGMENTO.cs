using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	public partial class SEGMENTO
	{
		#region Metodos

        public static SqlDataReader CatalogoDenominacion(string t484_denominacion, string sTipoBusqueda, Nullable<int> t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t484_denominacion", SqlDbType.Text, 50);
            aParam[0].Value = t484_denominacion;
            aParam[1] = new SqlParameter("@sTipoBusq", SqlDbType.Char, 1);
            aParam[1].Value = sTipoBusqueda;
            aParam[2] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[2].Value = t314_idusuario;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteSqlDataReader("SUP_SEGMENTO_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_SEGMENTO_CAT_USU", aParam);
        }
        public static SqlDataReader Catalogo(SqlTransaction tr, Nullable<int> t483_idsector)
        {
            return SUPER.DAL.SEGMENTO.Catalogo(tr, t483_idsector);
        }
    	#endregion
	}
}
