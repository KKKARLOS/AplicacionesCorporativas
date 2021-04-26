using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.DAL
{

    /// <summary>
    /// Descripción breve de PerfilExper
    /// </summary>
    public class PerfilExper
    {
        public PerfilExper()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        public static SqlDataReader GetPerfilesExpPer(SqlTransaction tr, byte tipo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@tipo", SqlDbType.Bit, 4, tipo);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_PERFILESEXPPER_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_PERFILESEXPPER_CAT", aParam);

        }
        public static SqlDataReader GetPerfilesConsultas(SqlTransaction tr)
        {
            SqlParameter[] aParam = new SqlParameter[0];

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("[SUP_CVT_CONSULTA_PERFIL_EXP]", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "[SUP_CVT_CONSULTA_PERFIL_EXP]", aParam);
        }

        public static SqlDataReader catalogo(SqlTransaction tr)
        {
            SqlParameter[] aParam = new SqlParameter[0];

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_PERFILEXPER_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_PERFILEXPER_CAT", aParam);

        }

        public static void Delete(SqlTransaction tr, int T035_CODPERFIL)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t035_codperfil", SqlDbType.Int, 4, T035_CODPERFIL);


            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CVT_PERFILEXPER_DEL", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_PERFILEXPER_DEL", aParam);
        }

        public static int Insert(SqlTransaction tr, string T035_DESCRIPCION, string T035_ABREVIADO, int T035_RH, int T035_NIVEL)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t035_descripcion", SqlDbType.VarChar, 50, T035_DESCRIPCION);
            aParam[i++] = ParametroSql.add("@t035_abreviado", SqlDbType.VarChar, 3, T035_ABREVIADO.ToUpper());
            aParam[i++] = ParametroSql.add("@t035_rh", SqlDbType.Int, 1, T035_RH);
            aParam[i++] = ParametroSql.add("@t035_nivel", SqlDbType.Int, 1, T035_NIVEL);

            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_CVT_PERFILEXPER_INS", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CVT_PERFILEXPER_INS", aParam));
        }

        public static void Update(SqlTransaction tr, int T035_CODPERFIL, string T035_DESCRIPCION, string T035_ABREVIADO, int T035_RH, int T035_NIVEL)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t035_codperfil", SqlDbType.Int, 4, T035_CODPERFIL);
            aParam[i++] = ParametroSql.add("@t035_descripcion", SqlDbType.VarChar, 50, T035_DESCRIPCION);
            aParam[i++] = ParametroSql.add("@t035_abreviado", SqlDbType.VarChar, 3, T035_ABREVIADO.ToUpper());
            aParam[i++] = ParametroSql.add("@t035_rh", SqlDbType.Int, 1, T035_RH);
            aParam[i++] = ParametroSql.add("@t035_nivel", SqlDbType.Int, 1, T035_NIVEL);


            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CVT_PERFILEXPER_UPD", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_PERFILEXPER_UPD", aParam);
        }

    }
}