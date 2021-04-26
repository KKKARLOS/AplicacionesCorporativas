using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.DAL
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : PROYECTOCVT
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T301_PROYECTO
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	30/07/2012 12:27:05	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class PROYECTOCVT
    {
        #region Propiedades y Atributos

        #endregion

        #region Metodos

        /// <summary>
        /// Obtiene la lista de Reponsable, Delegados, Colaboradores Y JEFES DE PROYECTO de un proyecto (INCLUYENDO LAS FIGURAS VIRTUALES)
        /// </summary>
        public static SqlDataReader GetValidadores(SqlTransaction tr, int t301_idproyecto)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
            aParam[0].Value = t301_idproyecto;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_EXPPROF_VALIDADORES_C", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_EXPPROF_VALIDADORES_C", aParam);

        }
        /// <summary>
        /// Obtiene la lista de Reponsable, Delegados, Colaboradores Y JEFES DE PROYECTO de todos los proyectos de una
        /// experiencia profesional (INCLUYENDO LAS FIGURAS VIRTUALES)
        /// </summary>
        public static SqlDataReader GetValidadoresExperiencia(SqlTransaction tr, int t808_idexpprof)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t808_idexpprof", SqlDbType.Int, 4);
            aParam[0].Value = t808_idexpprof;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_EXPPROF_VALIDADORES_EXP_C", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_EXPPROF_VALIDADORES_EXP_C", aParam);

        }

        #endregion
    }
}
