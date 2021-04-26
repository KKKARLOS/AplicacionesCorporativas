using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
//using System.Collections;
//using SUPER.Capa_Negocio;

namespace SUPER.Capa_Datos
{
    public class CEC
    {

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla T708_LINEA.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	11/04/2011 16:23:02
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Busqueda(
                    string sDepartamentos,
                    string sCEEC,
                    string sValores
               )
        {
            SqlParameter[] aParam = new SqlParameter[3];

            aParam[0] = new SqlParameter("@sDepartamentos", SqlDbType.VarChar, 8000);
            aParam[0].Value = sDepartamentos;
            aParam[1] = new SqlParameter("@sCEEC", SqlDbType.VarChar, 8000);
            aParam[1].Value = sCEEC;
            aParam[2] = new SqlParameter("@sValores", SqlDbType.VarChar, 8000);
            aParam[2].Value = sValores;

            return SqlHelper.ExecuteSqlDataReader("SUP_CEE_VALORES_PROY", aParam);
        }


    }
}