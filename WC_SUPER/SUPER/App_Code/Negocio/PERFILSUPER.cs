using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : PERFILSUPER
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T347_PERFILSUPER
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	05/03/2008 15:28:11	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class PERFILSUPER
    {
        #region Metodos

        public static SqlDataReader CatalogoPerfilesNodo_By_PSN(SqlTransaction tr, int t305_idproyectosubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_PERFILSUPER_NODO_By_PSN", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PERFILSUPER_NODO_By_PSN", aParam);
        }
        
        public static SqlDataReader CatalogoPerfilesCliente_By_PSN(SqlTransaction tr, int t305_idproyectosubnodo, Nullable<bool> t347_estado)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);
            aParam[i++] = ParametroSql.add("@t347_estado", SqlDbType.Bit, 1, t347_estado);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_PERFILSUPER_CLIENTE_By_PSN", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PERFILSUPER_CLIENTE_By_PSN", aParam);
        }

        #endregion
    }
}
