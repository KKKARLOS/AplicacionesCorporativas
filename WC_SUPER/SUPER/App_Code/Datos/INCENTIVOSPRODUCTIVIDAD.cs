using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
//using System.Collections;
using SUPER.Capa_Negocio;

namespace SUPER.Capa_Datos
{
    public class INCENTIVOSPRODUCTIVIDAD
    {
        public static SqlDataReader ObtenerIncentivos()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            int i = 0;
            //aParam[i++] = ParametroSql.add("@entorno", SqlDbType.Char, 1, Utilidades.Entorno);

            //return SqlHelper.ExecuteSqlDataReader("SUP_INCENTIVOS_GETIBERPER", aParam);
            return SqlHelper.ExecuteSqlDataReader("SUP_INCENTIVOS_GETIBERPER_DATOS", aParam);
        }

        public static SqlDataReader ObtenerUsuarios(int idIberper)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@idIberper", SqlDbType.Int, 4, idIberper);

            return SqlHelper.ExecuteSqlDataReader("SUP_INCENTIVOS_GETUSUARIO", aParam);
        }

        public static void Registrar(SqlTransaction tr, int t726_idincentivo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t726_idincentivo", SqlDbType.Int, 4, t726_idincentivo);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_INCENTIVOS_REGISTRAR", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_INCENTIVOS_REGISTRAR", aParam);
        }

        public static DataSet ObtenerInstanciasProyecto(SqlTransaction tr, int t301_idproyecto, int t314_idusuario, int anomes)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t301_idproyecto", SqlDbType.Int, 4, t301_idproyecto);
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);
            aParam[i++] = ParametroSql.add("@anomes", SqlDbType.Int, 4, anomes);

            if (tr == null)
                return SqlHelper.ExecuteDataset("SUP_INCENTIVOS_GETINSTANCIASPROY", aParam);
            else
                return SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_INCENTIVOS_GETINSTANCIASPROY", aParam);
        }

        public static void Insertar(SqlTransaction tr, string slIncentivos)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@tbl", SqlDbType.Structured, SUPER.Capa_Negocio.Utilidades.GetDataTableFromListCod(slIncentivos,",")),
            };

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_PUT_INCENTIVOS", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PUT_INCENTIVOS", aParam);
        }
    }
}