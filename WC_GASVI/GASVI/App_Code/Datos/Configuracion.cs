using System;
using System.Configuration;
using System.Data;
//using System.Web;
using System.Data.SqlClient;
using System.Collections;

namespace GASVI.DAL
{
    public partial class Configuracion
    {
        public static void UpdateMoneda(SqlTransaction tr, int t001_idficepi, string t422_idmoneda)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("GVT_CONF_MONEDA_U", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_CONF_MONEDA_U", aParam);
        }

        public static void UpdateAviso(SqlTransaction tr, int t001_idficepi, bool t176_aviso)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            aParam[i++] = ParametroSql.add("@t176_aviso", SqlDbType.Bit, 1, t176_aviso);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("GVT_CONF_AVISOCAMBIOES_U", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_CONF_AVISOCAMBIOES_U", aParam);
        }

        public static void UpdateMotivo(SqlTransaction tr, int t001_idficepi, Nullable<byte> t423_idmotivo)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            aParam[i++] = ParametroSql.add("@t423_idmotivo", SqlDbType.TinyInt, 1, t423_idmotivo);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("GVT_CONF_MOTIVO_U", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_CONF_MOTIVO_U", aParam);
        }
        public static void UpdateEmpresa(SqlTransaction tr, int t001_idficepi, int t313_idempresa)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            aParam[i++] = ParametroSql.add("@t313_idempresa", SqlDbType.Int, 4, t313_idempresa);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("GVT_CONF_EMPRESA_U", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_CONF_EMPRESA_U", aParam);
        }

        public static int GetEmpresaDefecto(SqlTransaction tr, string t001_codred)
        {
            int idEmpresa = 0;
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_codred", SqlDbType.VarChar, 15, t001_codred);

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("GVT_CONF_EMPRESA_S", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_CONF_EMPRESA_S", aParam);
            
            if (dr.Read())
                idEmpresa = (int)dr[0];

            dr.Close();
            dr.Dispose();

            return idEmpresa;
        }
        public static int GetEmpresaDefecto(SqlTransaction tr, int t314_idusuario)
        {
            int idEmpresa = 0;
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("GVT_CONF_EMPRESA_S2", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_CONF_EMPRESA_S2", aParam);

            if (dr.Read())
                idEmpresa = (int)dr[0];

            dr.Close();
            dr.Dispose();

            return idEmpresa;
        }
    }
}