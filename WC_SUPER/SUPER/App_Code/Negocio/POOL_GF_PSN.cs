using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    public partial class POOL_GF_PSN
    {
        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Selecciona los registros de la tabla T432_POOL_GF_PSN en función de una foreign key.
        /// </summary>
        /// <returns>DataSet</returns>
        /// <history>
        /// 	Creado por [sqladmin]	04/07/2008 12:25:54
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader CatalogoGFdePSN(SqlTransaction tr, int t305_idproyectosubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nPSN", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_GETPOOLGF_PSN", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_GETPOOLGF_PSN", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T432_POOL_GF_PSN.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	04/07/2008 12:25:54
        /// </history>
        /// -----------------------------------------------------------------------------
        public static void InsertarSiNoExiste(SqlTransaction tr, int t305_idproyectosubnodo, int t342_idgrupro)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;
            aParam[1] = new SqlParameter("@t342_idgrupro", SqlDbType.Int, 4);
            aParam[1].Value = t342_idgrupro;

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_POOL_GF_PSN_ICOUNT", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_POOL_GF_PSN_ICOUNT", aParam);
        }

        #endregion
    }
}
