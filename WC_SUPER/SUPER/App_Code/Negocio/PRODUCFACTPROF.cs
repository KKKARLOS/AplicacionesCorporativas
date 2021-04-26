using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	public partial class PRODUCFACTPROF 
	{
		#region Metodos

        public static void UpdateInsertSiNoExiste(SqlTransaction tr, int t325_idsegmesproy, int t332_idtarea, int t314_idusuario, Nullable<int> t333_idperfilproy, double t433_unidades)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            aParam[0] = new SqlParameter("@t325_idsegmesproy", SqlDbType.Int, 4);
            aParam[0].Value = t325_idsegmesproy;
            aParam[1] = new SqlParameter("@t332_idtarea", SqlDbType.Int, 4);
            aParam[1].Value = t332_idtarea;
            aParam[2] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[2].Value = t314_idusuario;
            aParam[3] = new SqlParameter("@t333_idperfilproy", SqlDbType.Int, 4);
            aParam[3].Value = t333_idperfilproy;
            aParam[4] = new SqlParameter("@t433_unidades", SqlDbType.Float, 8);
            aParam[4].Value = t433_unidades;

            SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PRODUCFACTPROF_UISNE", aParam);
        }

        public static void DeleteByT325_idsegmesproy(SqlTransaction tr, int t325_idsegmesproy)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t325_idsegmesproy", SqlDbType.Int, 4);
            aParam[0].Value = t325_idsegmesproy;

            SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PRODUCFACTPROF_DByT325_idsegmesproy", aParam);
        }

        public static DataSet ObtenerProduccionPerfilAgrupado(int t325_idsegmesproy, string t422_idmoneda)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@nSegMesProy", SqlDbType.Int, 4, t325_idsegmesproy);
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);

            return SqlHelper.ExecuteDataset("SUP_GETPRODUCFACTPERF_AGRUPADO", aParam);
        }

        public static SqlDataReader CatalogoMesCerrado(int t325_idsegmesproy, bool bConConsumos, string t422_idmoneda)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            int i = 0;
            aParam[i++] = ParametroSql.add("@nSegMesProy", SqlDbType.Int, 4, t325_idsegmesproy);
            aParam[i++] = ParametroSql.add("@bConConsumos", SqlDbType.Bit, 1, bConConsumos);
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);

            return SqlHelper.ExecuteSqlDataReader("SUP_GETPRODUCCIONPROF_MESC", aParam);
        }

        public static SqlDataReader CatalogoMesAbierto(int t325_idsegmesproy, bool bConConsumos, string t422_idmoneda)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            int i = 0;
            aParam[i++] = ParametroSql.add("@nSegMesProy", SqlDbType.Int, 4, t325_idsegmesproy);
            aParam[i++] = ParametroSql.add("@bConConsumos", SqlDbType.Bit, 1, bConConsumos);
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);

            return SqlHelper.ExecuteSqlDataReader("SUP_GETPRODUCCIONPROF_MESA", aParam);
        }
        /// <summary>
        /// Devuelve el nº de filas en T433_PRODUCFACTPROF para una tarea
        /// Se usa cuando se está intentando borrar una tarea
        /// </summary>
        public static int GetFilasTarea(SqlTransaction tr, int nTarea)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nTarea", SqlDbType.Int, 4);
            aParam[0].Value = nTarea;
            if (tr != null)
                return System.Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_PRODUCFACTPROF_COUNT_TAREA", aParam));
            else
                return System.Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_PRODUCFACTPROF_COUNT_TAREA", aParam));
        }

		#endregion
	}
}
