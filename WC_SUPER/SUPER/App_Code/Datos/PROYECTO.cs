using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.DAL
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : PROYECTO
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T301_PROYECTO
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	30/07/2012 12:27:05	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class PROYECTO
    {
        #region Propiedades y Atributos

        #endregion

        #region Metodos

        /// <summary>
        /// Obtención de proyectos para modificación del estado del proyecto económico
        /// </summary>

        public static SqlDataReader Obtener(SqlTransaction tr, string t301_estado, int t301_annoPIG, string sProyectos, string sNaturalezas)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t301_estado", SqlDbType.Char, 1, t301_estado);
            aParam[i++] = ParametroSql.add("@t301_annoPIG", SqlDbType.Int, 4, t301_annoPIG);
            aParam[i++] = ParametroSql.add("@sProyectos", SqlDbType.VarChar, 8000, sProyectos);
            aParam[i++] = ParametroSql.add("@sNaturalezas", SqlDbType.VarChar, 8000, sNaturalezas);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_GETPROYECTOSESTADO_ADMIN", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_GETPROYECTOSESTADO_ADMIN", aParam);

        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza el estado de un registro de la tabla T301_PROYECTO.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	26/11/2009 9:02:50
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t301_idproyecto, string t301_estado)
        {
            SqlParameter[] aParam = new SqlParameter[2];

            int i = 0;
            aParam[i++] = ParametroSql.add("@t301_idproyecto", SqlDbType.Int, 4, t301_idproyecto);
            aParam[i++] = ParametroSql.add("@t301_estado", SqlDbType.Char, 1, t301_estado);

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_PROYECTO_ESTADO_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_PROYECTO_ESTADO_U", aParam);
        }
        /// <summary>
        /// Actualiza todas las tareas del proyecto que no estén Finalizadas, Cerradas o Anuladas
        /// ETPR = sumatorio consumos IAP
        /// FFPR = fecha último consumo IAP
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="nIdPE"></param>

        public static void CierreTecnico(SqlTransaction tr, int nIdPE)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t301_idproyecto", SqlDbType.Int, 4, nIdPE)
            };
            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CIERRETECNICO", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CIERRETECNICO", aParam);
        }


        public static SqlDataReader GetProyectosSubnodoCualidad(SqlTransaction tr, int nIdPE, string slCualidades)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t301_idproyecto", SqlDbType.Int, 4, nIdPE);
            aParam[i++] = ParametroSql.add("@sCualidades", SqlDbType.VarChar, 10, slCualidades);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_GETPROYECTOSSUBNODO_CUALIDAD", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_GETPROYECTOSSUBNODO_CUALIDAD", aParam);

        }

        #endregion
    }
}
