using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using IB.Services.Super.Globales;

namespace IB.Services.Super.DAL
{
    public class USUARIO
    {
        //public static USUARIO Select(SqlTransaction tr, int t314_idusuario)
        //{
        //    USUARIO o = new USUARIO();

        //    //SqlParameter[] aParam = new SqlParameter[1];
        //    //aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
        //    //aParam[0].Value = t314_idusuario;
        //    SqlParameter[] aParam = new SqlParameter[]{  
        //        ParametrosSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario),
        //    };

        //    SqlDataReader dr;
        //    if (tr == null)
        //        dr = SqlHelper.ExecuteSqlDataReader("SUP_PROFESIONAL_S2", aParam);
        //    else
        //        dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PROFESIONAL_S2", aParam);

        //    if (dr.Read())
        //    {
        //        o.t314_idusuario = t314_idusuario;
        //        if (dr["t001_idficepi"] != DBNull.Value)
        //            o.t001_idficepi = int.Parse(dr["t001_idficepi"].ToString());
        //        if (dr["t001_fecalta"] != DBNull.Value)
        //            o.t001_fecalta = (DateTime)dr["t001_fecalta"];
        //        if (dr["t001_fecbaja"] != DBNull.Value)
        //            o.t001_fecbaja = (DateTime)dr["t001_fecbaja"];
        //        if (dr["t314_falta"] != DBNull.Value)
        //            o.t314_falta = (DateTime)dr["t314_falta"];
        //        if (dr["t314_fbaja"] != DBNull.Value)
        //            o.t314_fbaja = (DateTime)dr["t314_fbaja"];
        //    }
        //    else
        //    {
        //        o.t314_idusuario = -1;
        //        //throw (new NullReferenceException("No se ha obtenido ningun dato de USUARIO"));
        //    }

        //    dr.Close();
        //    dr.Dispose();

        //    return o;
        //}
        public static SqlDataReader Select(SqlTransaction tr, int t314_idusuario)
        {
            //SqlParameter[] aParam = new SqlParameter[1];
            //int i = 0;
            //aParam[i++] = ParametroSql.add("@t001_codred", SqlDbType.VarChar, 12, codred);
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametrosSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario),
            };
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_PROFESIONAL_S2", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PROFESIONAL_S2", aParam);
        }
    }
}
