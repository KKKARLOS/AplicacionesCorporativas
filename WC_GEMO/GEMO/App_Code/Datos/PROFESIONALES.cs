using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for PROFESIONALES
/// </summary>
namespace GEMO.DAL
{
    public class PROFESIONALES
    {
        public static SqlDataReader ObtenerProfesionales(string t001_apellido1, string t001_apellido2, string t001_nombre, int iOpcion)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t001_apellido1", SqlDbType.VarChar, 50);
            aParam[0].Value = t001_apellido1;
            aParam[1] = new SqlParameter("@t001_apellido2", SqlDbType.VarChar, 50);
            aParam[1].Value = t001_apellido2;
            aParam[2] = new SqlParameter("@t001_nombre", SqlDbType.VarChar, 50);
            aParam[2].Value = t001_nombre;

            if (iOpcion==0)
                return SqlHelper.ExecuteSqlDataReader("GEM_CONSULTA_PROF", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("GEM_CONSULTA_PROF_RES", aParam);
        }
        public static SqlDataReader ObtenerProfesionalesMedios(string t001_apellido1, string t001_apellido2, string t001_nombre)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t001_apellido1", SqlDbType.VarChar, 50);
            aParam[0].Value = t001_apellido1;
            aParam[1] = new SqlParameter("@t001_apellido2", SqlDbType.VarChar, 50);
            aParam[1].Value = t001_apellido2;
            aParam[2] = new SqlParameter("@t001_nombre", SqlDbType.VarChar, 50);
            aParam[2].Value = t001_nombre;

            return SqlHelper.ExecuteSqlDataReader("GEM_CONSULTA_PROF_MED", aParam);
        }

        public static SqlDataReader ObtenerDatosLogin(string t001_codred)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@sCodRed", SqlDbType.VarChar, 15);
            aParam[0].Value = t001_codred;

            return SqlHelper.ExecuteSqlDataReader("GEM_LOGIN", aParam);
        }
        public static bool bPerteneceDIS(SqlTransaction tr, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[0].Value = t001_idficepi;

            if (tr == null)
                return (System.Convert.ToInt32(SqlHelper.ExecuteScalar("FIC_PROFESIONAL_ESDIS", aParam)) > 0) ? true : false;
            else
                return (System.Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "FIC_PROFESIONAL_ESDIS", aParam)) > 0) ? true : false;
        }
        public static SqlDataReader Responsables(string t001_apellido1, string t001_apellido2, string t001_nombre, bool bMostrarBajas)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@sAp1", SqlDbType.VarChar, 50);
            aParam[0].Value = t001_apellido1;
            aParam[1] = new SqlParameter("@sAp2", SqlDbType.VarChar, 50);
            aParam[1].Value = t001_apellido2;
            aParam[2] = new SqlParameter("@sNombre", SqlDbType.VarChar, 50);
            aParam[2].Value = t001_nombre;
            aParam[3] = new SqlParameter("@bMostrarBajas", SqlDbType.Bit, 1);
            aParam[3].Value = @bMostrarBajas;

            return SqlHelper.ExecuteSqlDataReader("GEM_CONSULTA_RESP", aParam);
        }
        public static SqlDataReader Beneficiarios(string t001_apellido1, string t001_apellido2, string t001_nombre, bool bMostrarBajas)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@sAp1", SqlDbType.VarChar, 50);
            aParam[0].Value = t001_apellido1;
            aParam[1] = new SqlParameter("@sAp2", SqlDbType.VarChar, 50);
            aParam[1].Value = t001_apellido2;
            aParam[2] = new SqlParameter("@sNombre", SqlDbType.VarChar, 50);
            aParam[2].Value = t001_nombre;
            aParam[3] = new SqlParameter("@bMostrarBajas", SqlDbType.Bit, 1);
            aParam[3].Value = @bMostrarBajas;

            return SqlHelper.ExecuteSqlDataReader("GEM_CONSULTA_BEN", aParam);
        }
    }
}