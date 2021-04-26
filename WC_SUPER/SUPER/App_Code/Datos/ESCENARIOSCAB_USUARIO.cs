using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace SUPER.Capa_Datos
{
    public partial class ESCENARIOSCAB_USUARIO
    {
        #region Metodos

        public static int InsertarUsuario(SqlTransaction tr, int t789_idescenario, int t314_idusuario, DateTime t731_falta, Nullable<DateTime> t731_fbaja, Nullable<int> t333_idperfilproy)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t789_idescenario", SqlDbType.Int, 4, t789_idescenario);
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);
            aParam[i++] = ParametroSql.add("@t731_falta", SqlDbType.SmallDateTime, 4, t731_falta);
            aParam[i++] = ParametroSql.add("@t731_fbaja", SqlDbType.SmallDateTime, 4, t731_fbaja);
            aParam[i++] = ParametroSql.add("@t333_idperfilproy", SqlDbType.Int, 4, t333_idperfilproy);

            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_ESCENARIOSCAB_USUARIO_INS", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_ESCENARIOSCAB_USUARIO_INS", aParam));
        }

        public static SqlDataReader ObtenerUsuariosEscenario(SqlTransaction tr, int t789_idescenario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t789_idescenario", SqlDbType.Int, 4, t789_idescenario);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_ESCENARIOSCAB_USUARIO_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_ESCENARIOSCAB_USUARIO_CAT", aParam);
        }

        //public static void BorrarMesesEscenario(SqlTransaction tr, string sMeses)
        //{
        //    SqlParameter[] aParam = new SqlParameter[1];
        //    int i = 0;
        //    aParam[i++] = ParametroSql.add("@sMeses", SqlDbType.VarChar, 8000, sMeses);

        //    if (tr == null)
        //        SqlHelper.ExecuteNonQuery("SUP_ESCENARIOMES_DELCAT", aParam);
        //    else
        //        SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ESCENARIOMES_DELCAT", aParam);
        //}


        #endregion
    }
}
