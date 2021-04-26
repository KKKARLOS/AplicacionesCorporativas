using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace SUPER.Capa_Datos
{
    /// <summary>
    /// Descripción breve de GestorSAP
    /// </summary>
    public class GestorSAP
    {
        #region Metodos


        public static SqlDataReader ObtenerProfesionales(SqlTransaction tr)
        {
            SqlParameter[] aParam = new SqlParameter[0];

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_GESTORCOBRO_C", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_GESTORCOBRO_C", aParam);
        }
        public static void Update(SqlTransaction tr, Nullable<int> t314_idusuario, string gestorcobro_sap)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@gestorcobro_sap", SqlDbType.Char, 2, gestorcobro_sap);
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_GESTORCOBRO_U", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_GESTORCOBRO_U", aParam);
        }
        #endregion
    }
}