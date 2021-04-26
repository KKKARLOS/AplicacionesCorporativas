using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace SUPER.Capa_Datos
{
	public partial class ESCENARIOCONSPERMES
	{
		#region Metodos

        public static void ActualizarInsertarUsuario(SqlTransaction tr, Nullable<int> t794_id, int t795_idescenariomes, 
                                        int t731_idescenariousuario, double @t794_unidades, decimal t794_costeunitario, string t794_comentario)
        {
            SqlParameter[] aParam = new SqlParameter[6];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t794_id", SqlDbType.Int, 4, t794_id);
            aParam[i++] = ParametroSql.add("@t795_idescenariomes", SqlDbType.Int, 4, t795_idescenariomes);
            aParam[i++] = ParametroSql.add("@t731_idescenariousuario", SqlDbType.Int, 4, t731_idescenariousuario);
            aParam[i++] = ParametroSql.add("@t794_unidades", SqlDbType.Float, 8, @t794_unidades);
            aParam[i++] = ParametroSql.add("@t794_costeunitario", SqlDbType.Money, 8, t794_costeunitario);
            aParam[i++] = ParametroSql.add("@t794_comentario", SqlDbType.VarChar, 500, t794_comentario);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_ESCENARIOCONSPERMES_UPDINS", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ESCENARIOCONSPERMES_UPDINS", aParam);
        }

        public static void Eliminar(SqlTransaction tr, int t794_id)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t794_id", SqlDbType.Int, 4, t794_id);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_ESCENARIOCONSPERMES_DEL", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ESCENARIOCONSPERMES_DEL", aParam);
        }
        
		#endregion
	}
}

