using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
//using System.Collections;
//using SUPER.Capa_Negocio;

namespace SUPER.Capa_Datos
{
    public class ALERTAS
    {
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza un registro de la tabla T830_PSNALERTAS.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	23/07/2012 12:36:46
        /// </history>
        /// -----------------------------------------------------------------------------
         
        public static int UpdatePSNAlertas(SqlTransaction tr, byte t820_idalerta, string sAccion)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t820_idalerta", SqlDbType.TinyInt, 1, t820_idalerta);
            aParam[i++] = ParametroSql.add("@Accion", SqlDbType.Char, 1, sAccion);

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_ALERTAS_U_PSN", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ALERTAS_U_PSN", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza un registro de la tabla T826_NODOALERTAS.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	23/07/2012 12:36:46
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int UpdateNodoAlertas(SqlTransaction tr, byte t820_idalerta, string sAccion)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t820_idalerta", SqlDbType.TinyInt, 1, t820_idalerta);
            aParam[i++] = ParametroSql.add("@Accion", SqlDbType.Char, 1, sAccion);

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_ALERTAS_U_NODO", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ALERTAS_U_NODO", aParam);
        }
        public static SqlDataReader Catalogo(SqlTransaction tr)
        {
            SqlParameter[] aParam = new SqlParameter[0];

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_ALERTAS_C", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_ALERTAS_C", aParam);
        }

        public static void EjecucionMensual(){
            SqlParameter[] aParam = new SqlParameter[0];
            SqlHelper.ExecuteNonQuery("SUP_ALERTA_GENDIALOGO_B", 600, aParam);
        }
    }
}