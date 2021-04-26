using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace SUPER.Capa_Datos
{
    public partial class ESCENARIOCONSNIVELMES
    {
        #region Metodos

        public static void ActualizarInsertarUsuario(SqlTransaction tr, Nullable<int> t796_id, int t795_idescenariomes, int t442_idnivel, double t796_unidades, string t796_comentario)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t796_id", SqlDbType.Int, 4, t796_id);
            aParam[i++] = ParametroSql.add("@t795_idescenariomes", SqlDbType.Int, 4, t795_idescenariomes);
            aParam[i++] = ParametroSql.add("@t442_idnivel", SqlDbType.Int, 4, t442_idnivel);
            aParam[i++] = ParametroSql.add("@t796_unidades", SqlDbType.Float, 8, t796_unidades);
            aParam[i++] = ParametroSql.add("@t796_comentario", SqlDbType.VarChar, 500, t796_comentario);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_ESCENARIOCONSNIVELMES_UPDINS", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ESCENARIOCONSNIVELMES_UPDINS", aParam);
        }

        public static void Eliminar(SqlTransaction tr, int t796_id)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t796_id", SqlDbType.Int, 4, t796_id);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_ESCENARIOCONSNIVELMES_DEL", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ESCENARIOCONSNIVELMES_DEL", aParam);
        }

        #endregion
    }
}

