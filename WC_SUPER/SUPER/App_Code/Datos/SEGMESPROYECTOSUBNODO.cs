using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
//using System.Collections;
using SUPER.Capa_Negocio;

namespace SUPER.Capa_Datos
{
    public class SEGMESPROYECTOSUBNODO
    {
        /*
         * Este método se va a ejecutar en un segundo hilo de ejecución, por lo que no va a
         * tener valor el objeto HttpContext.Current del que hacen uso los métodos habituales
         * de acceso a base de datos. Por este motivo obtenemos la cadena de conexión y se la
         * enviamos directamente al ExecuteNonQuery
         * */
        public static void GenerarDialogosDeAlertas(string s_idsegmesproy, bool b_ModoComprobacion)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@s_idsegmesproy", SqlDbType.VarChar, 8000, s_idsegmesproy);
            aParam[i++] = ParametroSql.add("@b_ModoComprobacion", SqlDbType.Bit, 1, b_ModoComprobacion);

            string sConnectionString = "";

            if (ConfigurationManager.ConnectionStrings["ENTORNO"].ConnectionString == "E")
                sConnectionString = ConfigurationManager.ConnectionStrings["ConexionExplotacion"].ConnectionString;
            else
                sConnectionString = ConfigurationManager.ConnectionStrings["ConexionDesarrollo"].ConnectionString;

            SqlHelper.ExecuteNonQuery(sConnectionString, CommandType.StoredProcedure, "SUP_ALERTA_GENDIALOGO_A", aParam);
        }

        public static SqlDataReader ObtenerDialogosDeAlertas(SqlTransaction tr, string s_idsegmesproy, bool b_ModoComprobacion)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@s_idsegmesproy", SqlDbType.VarChar, 8000, s_idsegmesproy);
            aParam[i++] = ParametroSql.add("@b_ModoComprobacion", SqlDbType.Bit, 1, b_ModoComprobacion);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_ALERTA_GENDIALOGO_A", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_ALERTA_GENDIALOGO_A", aParam);
        }
        public static DataSet ObtenerDialogosDeAlertasDS(SqlTransaction tr, string s_idsegmesproy, bool b_ModoComprobacion)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@s_idsegmesproy", SqlDbType.VarChar, 8000, s_idsegmesproy);
            aParam[i++] = ParametroSql.add("@b_ModoComprobacion", SqlDbType.Bit, 1, b_ModoComprobacion);

            if (tr == null)
                return SqlHelper.ExecuteDataset("SUP_ALERTA_GENDIALOGO_A", aParam);
            else
                return SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_ALERTA_GENDIALOGO_A", aParam);
        }
        public static DataSet ObtenerInformeDeAlerta(SqlTransaction tr, int t325_idsegmesproy,
                            string t422_idmoneda,
                            byte t820_idalerta)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t325_idsegmesproy", SqlDbType.Int, 4, t325_idsegmesproy);
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);
            aParam[i++] = ParametroSql.add("@t820_idalerta", SqlDbType.TinyInt, 1, t820_idalerta);

            if (tr == null)
                return SqlHelper.ExecuteDataset("SUP_INFORMEECOALARMA", aParam);
            else
                return SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_INFORMEECOALARMA", aParam);
        }
    }
}