using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	public partial class PRODUCFACTPERF
	{
		#region Metodos

        public static void Insert(SqlTransaction tr, int t325_idsegmesproy, int t333_idperfilproy, decimal t444_imptarifamc, double t444_unidades)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@t325_idsegmesproy", SqlDbType.Int, 4);
            aParam[0].Value = t325_idsegmesproy;
            aParam[1] = new SqlParameter("@t333_idperfilproy", SqlDbType.Int, 4);
            aParam[1].Value = t333_idperfilproy;
            aParam[2] = new SqlParameter("@t444_imptarifamc", SqlDbType.Money, 8);
            aParam[2].Value = t444_imptarifamc;
            aParam[3] = new SqlParameter("@t444_unidades", SqlDbType.Float, 8);
            aParam[3].Value = t444_unidades;

            SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PRODUCFACTPERF_I", aParam);
        }
        public static void DeleteByT325_idsegmesproy(SqlTransaction tr, int t325_idsegmesproy)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t325_idsegmesproy", SqlDbType.Int, 4);
            aParam[0].Value = t325_idsegmesproy;

            SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PRODUCFACTPERF_DByT325_idsegmesproy", aParam);
        }

        public static int Delete(SqlTransaction tr, int t325_idsegmesproy, int t333_idperfilproy)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t325_idsegmesproy", SqlDbType.Int, 4);
            aParam[0].Value = t325_idsegmesproy;
            aParam[1] = new SqlParameter("@t333_idperfilproy", SqlDbType.Int, 4);
            aParam[1].Value = t333_idperfilproy;

            return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PRODUCFACTPERF_D", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Comprueba si existe produccion de un perfil en un mes
        /// </summary>
        /// -----------------------------------------------------------------------------
        //public static bool HayProduccion(SqlTransaction tr, int nIdSegMesProy, int nIdPerfilProy)
        //{
        //    bool bHayProduccion = false;

        //    SqlParameter[] aParam = new SqlParameter[2];
        //    aParam[0] = new SqlParameter("@nIdSegMesProy", SqlDbType.Int, 4);
        //    aParam[1] = new SqlParameter("@nIdPerfilProy", SqlDbType.Int, 4);

        //    aParam[0].Value = nIdSegMesProy;
        //    aParam[1].Value = nIdPerfilProy;

        //    int nResul = 0;
        //    if (tr != null)
        //        nResul = System.Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_PRODUCFACTPERF_COUNT", aParam));
        //    else
        //        nResul = System.Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_PRODUCFACTPERF_COUNT", aParam));

        //    if (nResul > 0) bHayProduccion = true;

        //    return bHayProduccion;
        //}
        public static Nullable<double> GetUnidades(SqlTransaction tr, int nIdSegMesProy, int nIdPerfilProy)
        {
            double? dUnidades = null;
            SqlDataReader dr;

            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nIdSegMesProy", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@nIdPerfilProy", SqlDbType.Int, 4);

            aParam[0].Value = nIdSegMesProy;
            aParam[1].Value = nIdPerfilProy;

            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_PRODUCFACTPERF_UNIDADES_S", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PRODUCFACTPERF_UNIDADES_S", aParam);

            if (dr.Read())
            {
                dUnidades = (double)dr[0];
            }
            dr.Close();
            dr.Dispose();
            return dUnidades;
        }
        public static void UpdateUnidades(SqlTransaction tr, int t325_idsegmesproy, int nIdPerfilProy, double t444_unidades)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t325_idsegmesproy", SqlDbType.Int, 4);
            aParam[0].Value = t325_idsegmesproy;
            aParam[1] = new SqlParameter("@t333_idperfilproy", SqlDbType.Int, 4);
            aParam[1].Value = nIdPerfilProy;
            aParam[2] = new SqlParameter("@t444_unidades", SqlDbType.Float, 8);
            aParam[2].Value = t444_unidades;

            SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PRODUCFACTPERF_UNIDADES_U", aParam);
        }


        public static DataSet CatalogoMesCerrado(int t325_idsegmesproy, string t422_idmoneda)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            int i = 0;
            aParam[i++] = ParametroSql.add("@nSegMesProy", SqlDbType.Int, 4, t325_idsegmesproy);
            aParam[i++] = ParametroSql.add("@bSA", SqlDbType.Bit, 1, SUPER.Capa_Negocio.Utilidades.EsSuperAdminProduccion());
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);


            return SqlHelper.ExecuteDataset("SUP_GETPRODUCFACTPERF_MESC", aParam);
        }
        public static DataSet CatalogoMesAbierto(int t325_idsegmesproy, string t422_idmoneda)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@nSegMesProy", SqlDbType.Int, 4, t325_idsegmesproy);
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);

            return SqlHelper.ExecuteDataset("SUP_GETPRODUCFACTPERF_MESA", aParam);
        }

		#endregion
	}
}
