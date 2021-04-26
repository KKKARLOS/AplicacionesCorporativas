using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace SUPER.Capa_Datos
{
    public partial class ESCENARIOPRODUCPROF
    {
        #region Metodos

        public static void ActualizarInsertarUsuario(SqlTransaction tr, Nullable<int> t798_id, int t795_idescenariomes, int t314_idusuario, int t333_idperfilproy, double t798_unidades, string t798_comentario)
        {
            SqlParameter[] aParam = new SqlParameter[6];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t798_id", SqlDbType.Int, 4, t798_id);
            aParam[i++] = ParametroSql.add("@t795_idescenariomes", SqlDbType.Int, 4, t795_idescenariomes);
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);
            aParam[i++] = ParametroSql.add("@t333_idperfilproy", SqlDbType.Int, 4, t333_idperfilproy);
            aParam[i++] = ParametroSql.add("@t798_unidades", SqlDbType.Float, 8, t798_unidades);
            aParam[i++] = ParametroSql.add("@t798_comentario", SqlDbType.VarChar, 500, t798_comentario);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_ESCENARIOPRODUCPROF_UPDINS", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ESCENARIOPRODUCPROF_UPDINS", aParam);
        }

        public static void Eliminar(SqlTransaction tr, int t798_id)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t798_id", SqlDbType.Int, 4, t798_id);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_ESCENARIOPRODUCPROF_DEL", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ESCENARIOPRODUCPROF_DEL", aParam);
        }

        #endregion
    }
}

