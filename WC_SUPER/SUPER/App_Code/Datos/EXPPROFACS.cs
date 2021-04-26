using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.DAL
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : EXPPROFACS
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T816_EXPPROFACS
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	30/07/2012 13:53:26	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class EXPPROFACS
    {
        #region Propiedades y Atributos

        #endregion

        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla T816_EXPPROFACS.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	30/07/2012 13:53:26
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader getAreas(SqlTransaction tr, int t808_idexpprof)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t808_idexpprof", SqlDbType.Int, 4, t808_idexpprof);
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_EXPPROFACS_C2", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_EXPPROFACS_C2", aParam);
        }

        public static void InsertEP(SqlTransaction tr, int t808_idexpprof, string sAcs)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t808_idexpprof", SqlDbType.Int, 4, t808_idexpprof);
            aParam[i++] = ParametroSql.add("@sacs", SqlDbType.VarChar, 8000, sAcs);
            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CVT_EXPPROFACS_INS", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_EXPPROFACS_INS", aParam);
        }

        #endregion
    }
}
