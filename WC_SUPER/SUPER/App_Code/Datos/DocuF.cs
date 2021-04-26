using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using SUPER.Capa_Datos;
using System.Data;
namespace SUPER.DAL
{
    /// <summary>
    /// Descripción breve de DocuF
    /// </summary>
    public class DocuF
    {
        public DocuF()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de documentos de las fases de un proyectosubnodo
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
                return SqlHelper.ExecuteSqlDataReader("SUP_DOCUF_PSN_C", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_DOCUF_PSN_C", aParam);
        }
        public static int UpdateIdDoc(SqlTransaction tr, int t364_iddocuf, long t2_iddocumento)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t364_iddocuf", SqlDbType.Int, 4, t364_iddocuf),
                ParametroSql.add("@t2_iddocumento", SqlDbType.BigInt, 8, t2_iddocumento)
            };
            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_DOCUF_DOC_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DOCUF_DOC_U", aParam);
        }
    }
}
