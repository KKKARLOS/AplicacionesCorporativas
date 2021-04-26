using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace SUPER.Capa_Datos
{
    /// <summary>
    /// Descripción breve de USUARIOSIAUTO
    /// </summary>
    public class USUARIOSIAUTO
    {
        #region Metodos


        public static SqlDataReader ObtenerProfesionales(SqlTransaction tr)
        {
            SqlParameter[] aParam = new SqlParameter[0];

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_USUARIOSIAUTO_C", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_USUARIOSIAUTO_C", aParam);
        }
        public static void Update(SqlTransaction tr, int t314_idusuario, int t332_idtarea, float t143_horas_L,
            float t143_horas_M,float t143_horas_X,float t143_horas_J,float t143_horas_V,float t143_horas_S,
            float t143_horas_D, bool t143_avisomail
            )
        {
            SqlParameter[] aParam = new SqlParameter[10];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);
            aParam[i++] = ParametroSql.add("@t332_idtarea", SqlDbType.Int, 4, t332_idtarea);
            aParam[i++] = ParametroSql.add("@t143_horas_L", SqlDbType.Real, 4, t143_horas_L);
            aParam[i++] = ParametroSql.add("@t143_horas_M", SqlDbType.Real, 4, t143_horas_M);
            aParam[i++] = ParametroSql.add("@t143_horas_X", SqlDbType.Real, 4, t143_horas_X);
            aParam[i++] = ParametroSql.add("@t143_horas_J", SqlDbType.Real, 4, t143_horas_J);
            aParam[i++] = ParametroSql.add("@t143_horas_V", SqlDbType.Real, 4, t143_horas_V);
            aParam[i++] = ParametroSql.add("@t143_horas_S", SqlDbType.Real, 4, t143_horas_S);
            aParam[i++] = ParametroSql.add("@t143_horas_D", SqlDbType.Real, 4, t143_horas_D);
            aParam[i++] = ParametroSql.add("@t143_avisomail ", SqlDbType.Bit, 1, t143_avisomail);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_USUARIOSIAUTO_U", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_USUARIOSIAUTO_U", aParam);
        }

        public static void Insert(SqlTransaction tr, int t314_idusuario, int t332_idtarea, float t143_horas_L,
            float t143_horas_M, float t143_horas_X, float t143_horas_J, float t143_horas_V, float t143_horas_S,
            float t143_horas_D, bool t143_avisomail
            )
        {
            SqlParameter[] aParam = new SqlParameter[10];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);
            aParam[i++] = ParametroSql.add("@t332_idtarea", SqlDbType.Int, 4, t332_idtarea);
            aParam[i++] = ParametroSql.add("@t143_horas_L", SqlDbType.Real, 4, t143_horas_L);
            aParam[i++] = ParametroSql.add("@t143_horas_M", SqlDbType.Real, 4, t143_horas_M);
            aParam[i++] = ParametroSql.add("@t143_horas_X", SqlDbType.Real, 4, t143_horas_X);
            aParam[i++] = ParametroSql.add("@t143_horas_J", SqlDbType.Real, 4, t143_horas_J);
            aParam[i++] = ParametroSql.add("@t143_horas_V", SqlDbType.Real, 4, t143_horas_V);
            aParam[i++] = ParametroSql.add("@t143_horas_S", SqlDbType.Real, 4, t143_horas_S);
            aParam[i++] = ParametroSql.add("@t143_horas_D", SqlDbType.Real, 4, t143_horas_D);
            aParam[i++] = ParametroSql.add("@t143_avisomail ", SqlDbType.Bit, 1, t143_avisomail);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_USUARIOSIAUTO_I", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_USUARIOSIAUTO_I", aParam);
        }

        public static int Delete(SqlTransaction tr, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_USUARIOSIAUTO_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_USUARIOSIAUTO_D", aParam);
        }
        #endregion
    }
}