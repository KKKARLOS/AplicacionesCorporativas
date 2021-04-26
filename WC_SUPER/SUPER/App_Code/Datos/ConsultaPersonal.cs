using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using SUPER.Capa_Datos;
using System.Data;
namespace SUPER.DAL
{
    public class ConsultaPersonal
    {
        public ConsultaPersonal()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        public static SqlDataReader GetByClaveWS(SqlTransaction tr, string t472_clavews)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t472_clavews", SqlDbType.VarChar, 20, t472_clavews)
            };
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CONSULTAPERSONAL_S3", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CONSULTAPERSONAL_S3", aParam);
        }
    }
}
