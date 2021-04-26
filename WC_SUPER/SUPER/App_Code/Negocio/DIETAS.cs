using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : DIETA
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T401_FIGURACLIENTE
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	19/12/2007 15:07:30	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class DIETA
    {
        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene las figuras de un usuario en un proyectosubnodo
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            return SqlHelper.ExecuteSqlDataReader("SUP_DIETA_CAT", aParam);
        }
       
        #endregion
    }
}
