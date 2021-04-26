using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : TEXTOAVISOS
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T448_TEXTOAVISOS
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	28/04/2009 11:29:19	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class TEXTOAVISOS
    {
        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla T448_TEXTOAVISOS.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	28/04/2009 11:29:19
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo2(byte nOrden, byte nAscDesc)
        {
            SqlParameter[] aParam = new SqlParameter[2];

            aParam[0] = new SqlParameter("@nOrden", SqlDbType.TinyInt, 1);
            aParam[0].Value = nOrden;
            aParam[1] = new SqlParameter("@nAscDesc", SqlDbType.TinyInt, 1);
            aParam[1].Value = nAscDesc;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_TEXTOAVISOS_C2", aParam);
        }
        public static SqlDataReader CatalogoRecursos(int nIdAviso)
        {//Obtine los recursos asociados al aviso
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nIdAviso", SqlDbType.Int, 4);
            aParam[0].Value = nIdAviso;

            return SqlHelper.ExecuteSqlDataReader("SUP_TEXTOAVISOS_RECURSOCATA", aParam);
        }

        #endregion
    }
}
