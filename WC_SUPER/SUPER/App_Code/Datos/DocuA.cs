using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using SUPER.Capa_Datos;
using System.Data;
namespace SUPER.DAL
{
    public class DocuA
    {
        public DocuA()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de documentos de las actividades de un proyectosubnodo
        /// </summary>
        /// <history>
        /// 	Creado por [DOARHUMI]	14/04/2014 13:15:25
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader ListaPSN(SqlTransaction tr, int t305_idproyectosubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo)
            };
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_DOCUA_PSN_C", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_DOCUA_PSN_C", aParam);
        }
        public static int UpdateIdDoc(SqlTransaction tr, int t365_iddocua, long t2_iddocumento)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t365_iddocua", SqlDbType.Int, 4, t365_iddocua),
                ParametroSql.add("@t2_iddocumento", SqlDbType.BigInt, 8, t2_iddocumento)
            };
            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_DOCUA_DOC_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DOCUA_DOC_U", aParam);
        }
    }
}
