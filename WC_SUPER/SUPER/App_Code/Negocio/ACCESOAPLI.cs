using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : ACCESOAPLI
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T000_ACCESOAPLI
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	17/12/2007 9:29:22	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class ACCESOAPLI
    {
        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene de la tabla T000_ACCESOAPLI si el correo SUPER está activado o no.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	11/10/2011 9:29:22
        /// </history>
        /// -----------------------------------------------------------------------------
        public static bool CorreoActivado(SqlTransaction tr, byte T000_CODIGO)
        {
            bool bRes = false;

            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@T000_CODIGO", SqlDbType.TinyInt, 1);
            aParam[0].Value = T000_CODIGO;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_ACCESOAPLI_S", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_ACCESOAPLI_S", aParam);

            if (dr.Read())
            {
                if (dr["t000_enviocorreos"] != DBNull.Value)
                    bRes = (bool)dr["t000_enviocorreos"];
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de ACCESOAPLI"));
            }

            dr.Close();
            dr.Dispose();

            return bRes;
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza un registro de la tabla T000_ACCESOAPLI.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	17/12/2007 9:29:22
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int SetEstadoCorreo(SqlTransaction tr, byte T000_CODIGO, bool t000_enviocorreos)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@T000_CODIGO", SqlDbType.TinyInt, 1);
            aParam[0].Value = T000_CODIGO;
            aParam[1] = new SqlParameter("@t000_enviocorreos", SqlDbType.Bit, 1);
            aParam[1].Value = t000_enviocorreos;

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_ACCESOAPLI_CORREO_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_ACCESOAPLI_CORREO_U", aParam);
        }


        #endregion
    }
}
