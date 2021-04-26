using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : Incumplimientos
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: 
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	17/12/2007 9:29:22	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class Incumplimientos
    {
        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene los incumplimientos de un profesional en un periodo dado.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	11/10/2011 9:29:22
        /// </history>
        /// -----------------------------------------------------------------------------
        public static DataSet Propios(SqlTransaction tr, int t001_idficepi, DateTime dDesde, DateTime dHasta)
        {

            SqlParameter[] aParam = new SqlParameter[3];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            aParam[i++] = ParametroSql.add("@dDesde", SqlDbType.SmallDateTime, 4, dDesde);
            aParam[i++] = ParametroSql.add("@dHasta", SqlDbType.SmallDateTime, 4, dHasta);

            if (tr == null)
                return SqlHelper.ExecuteDataset("SUP_PANTA_INCUMPLIMIENTOS_PROPIOS", aParam);
            else
                return SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_PANTA_INCUMPLIMIENTOS_PROPIOS", aParam);
        }
        public static DataSet DeMiEquipo(SqlTransaction tr, int t001_idficepi, DateTime dDesde, DateTime dHasta,  string sProfesionales, string sIDEstructura, int nTareasVencidas, bool bMiEquipoDirecto, bool bTareasFueraPlazoSinHacer)
        {

            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi),
                ParametroSql.add("@dDesde", SqlDbType.SmallDateTime, 4, dDesde),
                ParametroSql.add("@dHasta", SqlDbType.SmallDateTime, 4, dHasta),
                ParametroSql.add("@sProfesionales", SqlDbType.VarChar, 8000, sProfesionales),
                ParametroSql.add("@sIDEstructura", SqlDbType.VarChar, 8000, sIDEstructura),
                ParametroSql.add("@nTareasVencidas", SqlDbType.Int, 4, nTareasVencidas),
                ParametroSql.add("@bMiEquipoDirecto", SqlDbType.Bit, 1, bMiEquipoDirecto),
                ParametroSql.add("@bTareasFueraPlazoSinHacer", SqlDbType.Bit, 1, bTareasFueraPlazoSinHacer)
            };

            if (tr == null)
                return SqlHelper.ExecuteDataset("SUP_PANTA_INCUMPLIMIENTOS_MI_EQUIPO", aParam);
            else
                return SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_PANTA_INCUMPLIMIENTOS_MI_EQUIPO", aParam);
        }
        #endregion
    }
}
