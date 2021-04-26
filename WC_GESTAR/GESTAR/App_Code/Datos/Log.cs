using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Data;
//Para usar ArraList
using System.Collections;
using GESTAR.Capa_Negocio;

namespace GESTAR.Capa_Datos
{
    public class Log
    {
        /// <summary>
        /// inserta en la tabla ZZZMIKEL_LOG
        /// </summary>
        /// <param name="sTexto"></param>
        public static void Insertar(string sTexto)
        {
            if (sTexto.Length > 500)
                sTexto = sTexto.Substring(0, 500);
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@texto", SqlDbType.VarChar, 500, sTexto);

            SqlHelper.ExecuteNonQuery("ZZZ_MIKEL_SUP_LOG_I", aParam);
        }
    }
}
