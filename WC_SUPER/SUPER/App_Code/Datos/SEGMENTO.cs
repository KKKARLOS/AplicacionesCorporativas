using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.DAL
{

    /// <summary>
    /// Descripción breve de SEGMENTO
    /// </summary>
    public class SEGMENTO
    {
        public SEGMENTO()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        /// <summary>
        /// Obtiene los sementos de un sector (o todos los segmentos si se pasa sector a null)
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t483_idsector"></param>
        /// <returns></returns>
        public static SqlDataReader Catalogo(SqlTransaction tr, Nullable<int> t483_idsector)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t483_idsector", SqlDbType.Int, 4);
            aParam[0].Value = t483_idsector;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_SEGMENTO_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_SEGMENTO_CAT", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T484_SEGMENTO
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	02/10/2009 13:57:05
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, string t484_denominacion, string t484_codigoexterno, int t483_idsector)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t484_denominacion", SqlDbType.Text, 50);
            aParam[0].Value = t484_denominacion;
            aParam[1] = new SqlParameter("@t484_codigoexterno", SqlDbType.Text, 15);
            aParam[1].Value = t484_codigoexterno;
            aParam[2] = new SqlParameter("@t483_idsector", SqlDbType.Int, 4);
            aParam[2].Value = t483_idsector;

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_SEGMENTO_I", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_SEGMENTO_I", aParam));
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza un registro de la tabla T484_SEGMENTO.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	02/10/2009 13:57:05
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t484_idsegmento, string t484_denominacion, string t484_codigoexterno, int t483_idsector)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@t484_idsegmento", SqlDbType.Int, 4);
            aParam[0].Value = t484_idsegmento;
            aParam[1] = new SqlParameter("@t484_denominacion", SqlDbType.Text, 50);
            aParam[1].Value = t484_denominacion;
            aParam[2] = new SqlParameter("@t484_codigoexterno", SqlDbType.Text, 15);
            aParam[2].Value = t484_codigoexterno;
            aParam[3] = new SqlParameter("@t483_idsector", SqlDbType.Int, 4);
            aParam[3].Value = t483_idsector;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_SEGMENTO_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_SEGMENTO_U", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina un registro de la tabla T484_SEGMENTO a traves de la primary key.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	02/10/2009 13:57:05
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Delete(SqlTransaction tr, int t484_idsegmento)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t484_idsegmento", SqlDbType.Int, 4);
            aParam[0].Value = t484_idsegmento;

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_SEGMENTO_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_SEGMENTO_D", aParam);
        }
    }
}