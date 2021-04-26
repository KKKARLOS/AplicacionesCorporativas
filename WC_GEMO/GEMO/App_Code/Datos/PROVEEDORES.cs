using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace GEMO.DAL
{
	/// -----------------------------------------------------------------------------
	/// Project	 : GEMO
    /// Class	 : PROVEEDORES
	/// 
	/// -----------------------------------------------------------------------------
	/// <summary>
    /// Clase de acceso a datos para la tabla: T063_PROVEEDORMOV
	/// </summary>
	/// <history>
	/// 	Creado por [sqladmin]	11/04/2011 16:23:02	
	/// </history>
	/// -----------------------------------------------------------------------------

        public class PROVEEDORES
        {
            #region Metodos

            public static SqlDataReader Catalogo()
            {
                SqlParameter[] aParam = new SqlParameter[0];
                return SqlHelper.ExecuteSqlDataReader("GEM_PROVEEDOR_C", aParam);
            }
            #endregion
        }    
}
