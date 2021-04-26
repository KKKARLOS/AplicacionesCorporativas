using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;
using System.Web;

namespace SUPER.Capa_Negocio
{
    public partial class RECALCULOCOSTES
    {
        public static DataSet ValidarIberperCostes(int iAnoMes)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@MesInicio", SqlDbType.Int, 4);
            aParam[0].Value = iAnoMes;

            return SqlHelper.ExecuteDataset("SUP_RECALCOSTES_VALIDACION", aParam);
        }

        public static SqlDataReader GetCatalogo()
        {
            SqlParameter[] aParam = new SqlParameter[0];

            return SqlHelper.ExecuteSqlDataReader("SUP_RECALCULOCOSTES_CAT", aParam);
        }
        public static void Procesar(SqlTransaction tr, int MesInicio, byte nCaso)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@base", SqlDbType.VarChar, 10);
            aParam[0].Value = "IBERPER";
            aParam[1] = new SqlParameter("@MesInicio", SqlDbType.Int, 4);
            aParam[1].Value = MesInicio;
            aParam[2] = new SqlParameter("@nCaso", SqlDbType.TinyInt, 1);
            aParam[2].Value = nCaso;

            SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_RECALCULOCOSTES_ESTANDAR", 300, aParam);
        }

        public static void DeleteAll(SqlTransaction tr)
        {
            SqlParameter[] aParam = new SqlParameter[0];

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_RECALCULOCOSTES_D_ALL", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_RECALCULOCOSTES_D_ALL", aParam);
        }

        //public static void ProcesarFicepi(SqlTransaction tr, int MesInicio, byte nCaso)
        //{
        //    SqlParameter[] aParam = new SqlParameter[2];
        //    aParam[0] = new SqlParameter("@MesInicio", SqlDbType.Int, 4);
        //    aParam[0].Value = MesInicio;
        //    aParam[1] = new SqlParameter("@nCaso", SqlDbType.TinyInt, 1);
        //    aParam[1].Value = nCaso;

        //    SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_RECALCULOCOSTES_ESTANDAR_FICEPI", 300, aParam);
        //}
    }
}