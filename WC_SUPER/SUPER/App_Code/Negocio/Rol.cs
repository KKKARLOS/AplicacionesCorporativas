using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : Rol
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T004_ROL
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	26/02/2008 11:08:05	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class ROL
    {
        #region Metodos

        public static SqlDataReader Catalogo()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_ROL_CAT", aParam);
        }
        #endregion
    }
}
