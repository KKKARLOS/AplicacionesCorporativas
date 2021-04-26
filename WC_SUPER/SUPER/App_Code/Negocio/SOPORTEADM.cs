using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{

    /// <summary>
    /// Summary description for SOPORTEADM
    /// </summary>
    public partial class SOPORTEADM
    {
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla T645_SOPORTEADM.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	15/02/2010 16:17:14
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_SOPORTEADM_CAT", aParam);
        }
    }
}
