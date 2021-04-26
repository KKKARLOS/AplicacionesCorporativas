using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
//using System.Collections;
//using SUPER.Capa_Negocio;

namespace SUPER.Capa_Datos
{
    public class USUSINAVISOVTO
    {
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T856_NOAVISOVENCIMIENTO.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	15/02/2010 16:17:14
        /// </history>
        /// -----------------------------------------------------------------------------
        public static void Insert(SqlTransaction tr, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[0].Value = t001_idficepi;

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_USUSINAVISOVTO_I", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_USUSINAVISOVTO_I", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza un registro de la tabla T856_NOAVISOVENCIMIENTO.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	15/02/2010 16:17:14
        /// </history>
        /// -----------------------------------------------------------------------------
        //public static int Update(SqlTransaction tr, int t001_idficepi)
        //{
        //    SqlParameter[] aParam = new SqlParameter[1];
        //    aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
        //    aParam[0].Value = t001_idficepi;

        //     Ejecuta la query y devuelve el numero de registros modificados.
        //    if (tr == null)
        //        return SqlHelper.ExecuteNonQuery("SUP_USUSINAVISOVTO_U", aParam);
        //    else
        //        return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_USUSINAVISOVTO_U", aParam);
        //}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina un registro de la tabla T856_NOAVISOVENCIMIENTO a traves de la primary key.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	15/02/2010 16:17:14
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Delete(SqlTransaction tr, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[0].Value = t001_idficepi;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_USUSINAVISOVTO_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_USUSINAVISOVTO_D", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla T856_NOAVISOVENCIMIENTO.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	15/02/2010 16:17:14
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_USUSINAVISOVTO_C", aParam);
        }
    }
}