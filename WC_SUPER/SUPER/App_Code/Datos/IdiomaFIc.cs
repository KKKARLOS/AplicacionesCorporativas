using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SUPER.Capa_Datos
{
    /// <summary>
    /// Descripción breve de IdiomaFic
    /// </summary>
    public partial class IdiomaFic
    {
        #region Metodos

        public static SqlDataReader Detalle(int t001_idficepi,int t020_idcodidioma)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            aParam[i++] = ParametroSql.add("@t020_idcodidioma", SqlDbType.Int, 4, t020_idcodidioma);
            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_IDIOMAFIC_SEL", aParam);
        }

        public static void Insert(int t001_idficepi, int t020_idcodidioma, int? t013_lectura, int? t013_escritura, int? t013_oral)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            aParam[i++] = ParametroSql.add("@t020_idcodidioma", SqlDbType.Int, 4, t020_idcodidioma);
            aParam[i++] = ParametroSql.add("@t013_lectura", SqlDbType.Int, 4, t013_lectura);
            aParam[i++] = ParametroSql.add("@t013_escritura", SqlDbType.Int, 4, t013_escritura);
            aParam[i++] = ParametroSql.add("@t013_oral", SqlDbType.Int, 4, t013_oral);
 

            SqlHelper.ExecuteScalar("SUP_CVT_IDIOMAFIC_INS", aParam);
        }

        public static void Update(int t001_idficepi, int t020_idcodidioma, int? t013_lectura, int? t013_escritura, int? t013_oral, int idcodidioma)
        {
            SqlParameter[] aParam = new SqlParameter[6];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            aParam[i++] = ParametroSql.add("@t020_idcodidioma", SqlDbType.Int, 4, t020_idcodidioma);
            aParam[i++] = ParametroSql.add("@t013_lectura", SqlDbType.Int, 4, t013_lectura);
            aParam[i++] = ParametroSql.add("@t013_escritura", SqlDbType.Int, 4, t013_escritura);
            aParam[i++] = ParametroSql.add("@t013_oral", SqlDbType.Int, 4, t013_oral);
            aParam[i++] = ParametroSql.add("@idcodidioma", SqlDbType.Int, 4, idcodidioma);

            SqlHelper.ExecuteScalar("SUP_CVT_IDIOMAFIC_UPD", aParam);
        }

        public static int Delete(SqlTransaction tr, int t001_idficepi, short t020_idcodidioma)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            aParam[i++] = ParametroSql.add("@t020_idcodidioma", SqlDbType.SmallInt, 2, t020_idcodidioma);

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CVT_IDIOMAFIC_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_IDIOMAFIC_D", aParam);
        }

        #endregion
    }
}





