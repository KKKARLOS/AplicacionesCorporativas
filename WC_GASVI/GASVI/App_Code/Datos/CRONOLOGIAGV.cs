using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace GASVI.DAL
{
    public partial class CRONOLOGIAGV
    {
        #region Metodos
        public static void InsertarCorreoAceptador(SqlTransaction tr, int t420_idreferencia, int t001_idficepi, string t659_motivo)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t420_idreferencia", SqlDbType.Int, 4, t420_idreferencia);
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            aParam[i++] = ParametroSql.add("@t659_motivo", SqlDbType.VarChar, 500, t659_motivo);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("GVT_CRONOLOGIA_CORREOACEPTADOR", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_CRONOLOGIA_CORREOACEPTADOR", aParam);
        }



        #endregion
    }
}
