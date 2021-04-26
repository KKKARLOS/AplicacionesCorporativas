using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using IB.Services.Super.Globales;

namespace IB.Services.Super.DAL
{
    public class CONSULTA
    {
        /// <summary>
        /// Acceso por clave de la consulta personalizada y usuario
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t472_clavews"></param>
        /// <param name="t314_idusuario"></param>
        /// <returns></returns>
        public static SqlDataReader Select(SqlTransaction tr, string t472_clavews, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametrosSql.add("@t472_clavews", SqlDbType.VarChar, 20, t472_clavews),
                ParametrosSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario),
            };
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CONSULTAPERSONAL_S2", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CONSULTAPERSONAL_S2", aParam);
        }
        /// <summary>
        /// Acceso por clave de la consulta personalizada 
        /// El 07/04/2013 Víctor indica que no hace falta usuario para el acceso a la consulta personalizada
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t472_clavews"></param>
        /// <param name="t314_idusuario"></param>
        /// <returns></returns>
        public static SqlDataReader Select(SqlTransaction tr, string t472_clavews)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametrosSql.add("@t472_clavews", SqlDbType.VarChar, 20, t472_clavews)
            };
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CONSULTAPERSONAL_S3", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CONSULTAPERSONAL_S3", aParam);
        }
        public static DataSet EjecutarConsultaDS(string sProdAlm, object[] aParametros)
        {
            return SqlHelper.ExecuteDatasetCP(Utilidades.CadenaConexion, sProdAlm, 300, aParametros);
        }
    }
}
