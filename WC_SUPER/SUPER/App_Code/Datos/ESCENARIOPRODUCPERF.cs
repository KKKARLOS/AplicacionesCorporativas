using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace SUPER.Capa_Datos
{
    public partial class ESCENARIOPRODUCPERF
    {
        #region Metodos

        public static void ActualizarInsertarUsuario(SqlTransaction tr, Nullable<int> t797_id, int t795_idescenariomes, int t333_idperfilproy, double t797_unidades, string t797_comentario)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t797_id", SqlDbType.Int, 4, t797_id);
            aParam[i++] = ParametroSql.add("@t795_idescenariomes", SqlDbType.Int, 4, t795_idescenariomes);
            aParam[i++] = ParametroSql.add("@t333_idperfilproy", SqlDbType.Int, 4, t333_idperfilproy);
            aParam[i++] = ParametroSql.add("@t797_unidades", SqlDbType.Float, 8, t797_unidades);
            aParam[i++] = ParametroSql.add("@t797_comentario", SqlDbType.VarChar, 500, t797_comentario);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_ESCENARIOPRODUCPERF_UPDINS", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ESCENARIOPRODUCPERF_UPDINS", aParam);
        }

        public static void Eliminar(SqlTransaction tr, int t797_id)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t797_id", SqlDbType.Int, 4, t797_id);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_ESCENARIOPRODUCPERF_DEL", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ESCENARIOPRODUCPERF_DEL", aParam);
        }

        #endregion
    }
}

