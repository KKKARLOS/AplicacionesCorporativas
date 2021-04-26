using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using SUPER.Capa_Datos;
using System.Data;
//Para usar ArraList
using System.Collections;

namespace SUPER.DAL
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

        ///<summary>
        /// inserta en la tabla T425_LOG (t001_idficepi, t425_tipo)
        ///</summary>
        ///<param name="t001_idficepi"></param>
        ///<param name="t425_tipo"></param>
        public static void Insertar(int t001_idficepi, int t425_tipo)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi),
                ParametroSql.add("@t425_tipo", SqlDbType.Int, 4, t425_tipo)
            };

            SqlHelper.ExecuteNonQuery("SUP_LOG_I", aParam);
            
        }

        ///<summary>
        /// inserta en la tabla T425_LOG (t001_idficepi, t425_tipo, t425_texto)
        ///</summary>
        ///<param name="t001_idficepi"></param>
        ///<param name="t425_tipo"></param>
        ///<param name="t425_texto"></param>
        public static void Insertar(int t001_idficepi, int t425_tipo, string t425_texto)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi),
                ParametroSql.add("@t425_tipo", SqlDbType.Int, 4, t425_tipo),
                ParametroSql.add("@t425_texto", SqlDbType.VarChar, 500, t425_texto)
            };

            SqlHelper.ExecuteNonQuery("SUP_LOG_I", aParam);
        }
    }
}
