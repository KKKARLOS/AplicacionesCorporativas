using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SUPER.Capa_Datos
{
    public partial class PROYECTOGV
    {
        #region Metodos

        public static SqlDataReader ObtenerCatalogoCreacionNota(SqlTransaction tr, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("GVT_GETPROYECTOSCREARNOTA_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_GETPROYECTOSCREARNOTA_CAT", aParam);

        }

        #endregion
    }
}
