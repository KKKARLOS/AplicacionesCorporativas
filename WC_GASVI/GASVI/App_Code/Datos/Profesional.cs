using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Data.SqlClient;
//using System.Web.Security; //para gestion de roles

namespace GASVI.DAL
{
	public partial class Profesional
    {
        public static SqlDataReader ObtenerDatosLogin(string t001_codred)
		{
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_codred", SqlDbType.VarChar, 15, t001_codred);

            return SqlHelper.ExecuteSqlDataReader("GVT_LOGIN", aParam);
		}
        public static bool bPerteneceDIS(SqlTransaction tr, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            if (tr == null)
                return (System.Convert.ToInt32(SqlHelper.ExecuteScalar("FIC_PROFESIONAL_ESDIS", aParam)) > 0) ? true : false;
            else
                return (System.Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "FIC_PROFESIONAL_ESDIS", aParam)) > 0) ? true : false;
        }
        public static bool bPermiteBono(SqlTransaction tr, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            if (tr == null)
                return (System.Convert.ToInt32(SqlHelper.ExecuteScalar("GVT_COMPROBAR_BT", aParam)) > 0) ? true : false;
            else
                return (System.Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "GVT_COMPROBAR_BT", aParam)) > 0) ? true : false;
        }

        public static bool bPermitePago(SqlTransaction tr, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            if (tr == null)
                return (System.Convert.ToInt32(SqlHelper.ExecuteScalar("GVT_COMPROBAR_AC", aParam)) > 0) ? true : false;
            else
                return (System.Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "GVT_COMPROBAR_AC", aParam)) > 0) ? true : false;
        }

        public static bool bNotasPendientes(SqlTransaction tr, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            if (tr == null)
                return (System.Convert.ToInt32(SqlHelper.ExecuteScalar("GVT_NOTASPENDIENTES_COUNT", aParam)) > 0) ? true : false;
            else
                return (System.Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "GVT_NOTASPENDIENTES_COUNT", aParam)) > 0) ? true : false;
        }
        
        public static int nNotasPendientes(SqlTransaction tr, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            if (tr == null)
                return System.Convert.ToInt32(SqlHelper.ExecuteScalar("GVT_NOTASPENDIENTES_COUNT", aParam));
            else
                return System.Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "GVT_NOTASPENDIENTES_COUNT", aParam));
        }
        public static int[] nDesgloseNotasPendientes(SqlTransaction tr, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            int[] nReturn = new int[3];
            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("GVT_NOTASPENDIENTES_COUNT", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_NOTASPENDIENTES_COUNT", aParam);

            if (dr.Read())
            {
                nReturn[0] = (int)dr["notas_pendientes"];
                nReturn[1] = (int)dr["notas_pendientes_aprobar"];
                nReturn[2] = (int)dr["notas_pendientes_aceptar"];
            }
            dr.Close();
            dr.Dispose();

            return nReturn;
        }

        public static int nNotasVisadas(SqlTransaction tr, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            if (tr == null)
                return System.Convert.ToInt32(SqlHelper.ExecuteScalar("GVT_NOTASVISADAS_COUNT", aParam));
            else
                return System.Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "GVT_NOTASVISADAS_COUNT", aParam));
        }
        public static int[] nDesgloseNotasVisadas(SqlTransaction tr, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            int[] nReturn = new int[3];
            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("GVT_NOTASVISADAS_COUNT", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_NOTASVISADAS_COUNT", aParam);

            if (dr.Read())
            {
                nReturn[0] = (int)dr["notas_ambito"];
                nReturn[1] = (int)dr["notas_aprobacion"];
                nReturn[2] = (int)dr["notas_aceptacion"];
            }
            dr.Close();
            dr.Dispose();

            return nReturn;
        }
        public static int[] nDesgloseNotasVisadas(SqlTransaction tr, int t001_idficepi, Nullable<DateTime> dtDesde, Nullable<DateTime> dtHasta)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            aParam[i++] = ParametroSql.add("@dtDesde", SqlDbType.DateTime, 4, dtDesde);
            aParam[i++] = ParametroSql.add("@dtHasta", SqlDbType.DateTime, 4, dtHasta);

            int[] nReturn = new int[2];
            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("GVT_NOTASVISADAS_FECHAS_COUNT", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_NOTASVISADAS_FECHAS_COUNT", aParam);

            if (dr.Read())
            {
                nReturn[0] = (int)dr["notas_aprobacion"];
                nReturn[1] = (int)dr["notas_aceptacion"];
            }
            dr.Close();
            dr.Dispose();

            return nReturn;
        }

        public static SqlDataReader ObtenerCatalogo(SqlTransaction tr, string strApellido1, string strApellido2, string strNombre)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            int i = 0;
            aParam[i++] = ParametroSql.add("@sApellido1", SqlDbType.VarChar, 50, strApellido1);
            aParam[i++] = ParametroSql.add("@sApellido2", SqlDbType.VarChar, 50, strApellido2);
            aParam[i++] = ParametroSql.add("@sNombre", SqlDbType.VarChar, 50, strNombre);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("GASVI_PROFESIONALES", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GASVI_PROFESIONALES", aParam);
        }

        public static bool bPerteneceVariasEmpresas(SqlTransaction tr, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);

            int nEmpresas = 0;
            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("GVT_EMPRESASPROFESIONAL", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_EMPRESASPROFESIONAL", aParam);

            while (dr.Read())
            {
                nEmpresas++;
            }
            dr.Close();
            dr.Dispose();

            return (nEmpresas > 1) ? true : false;
        }

        public static SqlDataReader ObtenerEmpresasTerritorios(SqlTransaction tr, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("GVT_EMPRESASPROFESIONAL", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_EMPRESASPROFESIONAL", aParam);
        }
        public static SqlDataReader ObtenerEmpresasTerritoriosProfesional(SqlTransaction tr, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("GVT_EMPRESASPROFESIONAL2", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_EMPRESASPROFESIONAL2", aParam);
        }

        public static SqlDataReader ObtenerBeneficiariosConsulta(SqlTransaction tr, string strApellido1, string strApellido2, string strNombre, bool bMostrarBajas)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            int i = 0;
            aParam[i++] = ParametroSql.add("@sApellido1", SqlDbType.VarChar, 50, strApellido1);
            aParam[i++] = ParametroSql.add("@sApellido2", SqlDbType.VarChar, 50, strApellido2);
            aParam[i++] = ParametroSql.add("@sNombre", SqlDbType.VarChar, 50, strNombre);
            aParam[i++] = ParametroSql.add("@bMostrarBajas", SqlDbType.Bit, 1, bMostrarBajas);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("GVT_GETBENEFICIARIO_CONS", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_GETBENEFICIARIO_CONS", aParam);
        }

        public static string ObtenerPagadores(SqlTransaction tr)
        {
            SqlParameter[] aParam = new SqlParameter[0];

            if (tr == null)
                return SqlHelper.ExecuteScalar("GVT_GETPAGADORES", aParam).ToString();
            else
                return SqlHelper.ExecuteScalarTransaccion(tr, "GVT_GETPAGADORES", aParam).ToString();
        }

        //public static SqlDataReader ObtenerEmpresasProfesional(SqlTransaction tr, string t001_codred)
        //{
        //    SqlParameter[] aParam = new SqlParameter[1];
        //    int i = 0;
        //    aParam[i++] = ParametroSql.add("@t001_codred", SqlDbType.VarChar, 15, t001_codred);

        //    if (tr == null)
        //        return SqlHelper.ExecuteSqlDataReader("GVT_EMPRESAS_PROFESIONAL", aParam);
        //    else
        //        return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_EMPRESAS_PROFESIONAL", aParam);
        //}

        public static SqlDataReader GetDatosEmpresa(SqlTransaction tr, int t313_idempresa)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t313_idempresa", SqlDbType.Int, 4, t313_idempresa);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("GVT_EMPRESA_S", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_EMPRESA_S", aParam);
        }
        
    }
}