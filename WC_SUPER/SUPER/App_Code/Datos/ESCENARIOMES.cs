using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace SUPER.Capa_Datos
{
	public partial class ESCENARIOMES
	{
		#region Metodos

        public static void InsertarMeses(SqlTransaction tr, int t789_idescenario, int anomes_min, int anomes_max)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t789_idescenario", SqlDbType.Int, 4, t789_idescenario);
            aParam[i++] = ParametroSql.add("@anomes_min", SqlDbType.Int, 4, anomes_min);
            aParam[i++] = ParametroSql.add("@anomes_max", SqlDbType.Int, 4, anomes_max);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_ESCENARIO_SETMESES", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ESCENARIO_SETMESES", aParam);
        }

        public static int InsertarMes(SqlTransaction tr, int t789_idescenario, int t795_anomes, string t795_comentario)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t789_idescenario", SqlDbType.Int, 4, t789_idescenario);
            aParam[i++] = ParametroSql.add("@t795_anomes", SqlDbType.Int, 4, t795_anomes);
            aParam[i++] = ParametroSql.add("@t795_comentario", SqlDbType.Text, 16, t795_comentario);
            
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_ESCENARIOMES_INS", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_ESCENARIOMES_INS", aParam));
        }

        public static SqlDataReader ObtenerCatalogoEscenario(SqlTransaction tr, int t789_idescenario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t789_idescenario", SqlDbType.Int, 4, t789_idescenario);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_ESCENARIOMES_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_ESCENARIOMES_CAT", aParam);
        }

        public static void ActualizarComentario(SqlTransaction tr, int t795_idescenariomes, string t795_comentario)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t795_idescenariomes", SqlDbType.Int, 4, t795_idescenariomes);
            aParam[i++] = ParametroSql.add("@t795_comentario", SqlDbType.Text, 16, t795_comentario);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_ESCENARIOMES_UPDCOM", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ESCENARIOMES_UPDCOM", aParam);
        }

        public static SqlDataReader ObtenerMesesBorrables(SqlTransaction tr, int t789_idescenario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t789_idescenario", SqlDbType.Int, 4, t789_idescenario);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_ESCENARIOMES_BORRABLE", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_ESCENARIOMES_BORRABLE", aParam);
        }

        public static void BorrarMesesEscenario(SqlTransaction tr, string sMeses)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@sMeses", SqlDbType.VarChar, 8000, sMeses);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_ESCENARIOMES_DELCAT", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ESCENARIOMES_DELCAT", aParam);
        }

        
		#endregion
	}
}
