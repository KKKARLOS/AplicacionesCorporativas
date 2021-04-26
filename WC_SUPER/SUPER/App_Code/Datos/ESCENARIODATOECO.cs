using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace SUPER.Capa_Datos
{
    public partial class ESCENARIODATOECO
    {
        #region Metodos

        public static void ActualizarInsertarUsuario(SqlTransaction tr, Nullable<int> t799_id, int t795_idescenariomes, int t806_idmotivopartida, decimal t799_importe, string t799_comentario)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t799_id", SqlDbType.Int, 4, t799_id);
            aParam[i++] = ParametroSql.add("@t795_idescenariomes", SqlDbType.Int, 4, t795_idescenariomes);
            aParam[i++] = ParametroSql.add("@t806_idmotivopartida", SqlDbType.Int, 4, t806_idmotivopartida);
            aParam[i++] = ParametroSql.add("@t799_importe", SqlDbType.Money, 8, t799_importe);
            aParam[i++] = ParametroSql.add("@t799_comentario", SqlDbType.VarChar, 500, t799_comentario);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_ESCENARIODATOECO_UPDINS", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ESCENARIODATOECO_UPDINS", aParam);
        }

        public static void Eliminar(SqlTransaction tr, int t799_id)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t799_id", SqlDbType.Int, 4, t799_id);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_ESCENARIODATOECO_DEL", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ESCENARIODATOECO_DEL", aParam);
        }

        #endregion
    }
}

