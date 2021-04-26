using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace SUPER.Capa_Datos
{
    /// <summary>
    /// Descripción breve de USUARIOSCATP
    /// </summary>
    public class USUARIOSCATP
    {
        #region Metodos

        public static SqlDataReader ObtenerProfesionales(SqlTransaction tr)
        {
            SqlParameter[] aParam = new SqlParameter[0];

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_USUARIOSCATP_C", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_USUARIOSCATP_C", aParam);
        }

        public static SqlDataReader LimiteExcedido(SqlTransaction tr, string sFecha)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@sFecha", SqlDbType.Char, 8, sFecha);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CONTROLUSUCATP", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CONTROLUSUCATP", aParam);
        }

        public static void Update(SqlTransaction tr, int t314_idusuario, float t144_horasmax_L,
            float t144_horasmax_M, float t144_horasmax_X, float t144_horasmax_J, float t144_horasmax_V, float t144_horasmax_S,
            float t144_horasmax_D, float t144_horasmax_SEM, float t144_horasmax_MES, bool t144_avisomail
            )
        {
            SqlParameter[] aParam = new SqlParameter[11];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);
            aParam[i++] = ParametroSql.add("@t144_horasmax_L", SqlDbType.Real, 4, t144_horasmax_L);
            aParam[i++] = ParametroSql.add("@t144_horasmax_M", SqlDbType.Real, 4, t144_horasmax_M);
            aParam[i++] = ParametroSql.add("@t144_horasmax_X", SqlDbType.Real, 4, t144_horasmax_X);
            aParam[i++] = ParametroSql.add("@t144_horasmax_J", SqlDbType.Real, 4, t144_horasmax_J);
            aParam[i++] = ParametroSql.add("@t144_horasmax_V", SqlDbType.Real, 4, t144_horasmax_V);
            aParam[i++] = ParametroSql.add("@t144_horasmax_S", SqlDbType.Real, 4, t144_horasmax_S);
            aParam[i++] = ParametroSql.add("@t144_horasmax_D", SqlDbType.Real, 4, t144_horasmax_D);
            aParam[i++] = ParametroSql.add("@t144_horasmax_SEM", SqlDbType.Real, 4, t144_horasmax_SEM);
            aParam[i++] = ParametroSql.add("@t144_horasmax_MES", SqlDbType.Real, 4, t144_horasmax_MES);
            aParam[i++] = ParametroSql.add("@t144_avisomail ", SqlDbType.Bit, 1, t144_avisomail);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_USUARIOSCATP_U", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_USUARIOSCATP_U", aParam);
        }

        public static void Insert(SqlTransaction tr, int t314_idusuario, float t144_horasmax_L,
            float t144_horasmax_M, float t144_horasmax_X, float t144_horasmax_J, float t144_horasmax_V, float t144_horasmax_S,
            float t144_horasmax_D, float t144_horasmax_SEM, float t144_horasmax_MES, bool t144_avisomail
            )
        {
            SqlParameter[] aParam = new SqlParameter[11];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);
            aParam[i++] = ParametroSql.add("@t144_horasmax_L", SqlDbType.Real, 4, t144_horasmax_L);
            aParam[i++] = ParametroSql.add("@t144_horasmax_M", SqlDbType.Real, 4, t144_horasmax_M);
            aParam[i++] = ParametroSql.add("@t144_horasmax_X", SqlDbType.Real, 4, t144_horasmax_X);
            aParam[i++] = ParametroSql.add("@t144_horasmax_J", SqlDbType.Real, 4, t144_horasmax_J);
            aParam[i++] = ParametroSql.add("@t144_horasmax_V", SqlDbType.Real, 4, t144_horasmax_V);
            aParam[i++] = ParametroSql.add("@t144_horasmax_S", SqlDbType.Real, 4, t144_horasmax_S);
            aParam[i++] = ParametroSql.add("@t144_horasmax_D", SqlDbType.Real, 4, t144_horasmax_D);
            aParam[i++] = ParametroSql.add("@t144_horasmax_SEM", SqlDbType.Real, 4, t144_horasmax_SEM);
            aParam[i++] = ParametroSql.add("@t144_horasmax_MES", SqlDbType.Real, 4, t144_horasmax_MES);
            aParam[i++] = ParametroSql.add("@t144_avisomail ", SqlDbType.Bit, 1, t144_avisomail);
            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_USUARIOSCATP_I", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_USUARIOSCATP_I", aParam);
        }

        public static int Delete(SqlTransaction tr, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_USUARIOSCATP_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_USUARIOSCATP_D", aParam);
        }
        #endregion
    }
}