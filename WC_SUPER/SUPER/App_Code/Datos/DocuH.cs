using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using SUPER.Capa_Datos;
using System.Data;
namespace SUPER.DAL
{
    public class DocuH
    {
        public DocuH()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de documentos de los hitos de un proyectosubnodo
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
                return SqlHelper.ExecuteSqlDataReader("SUP_DOCUH_PSN_C", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_DOCUH_PSN_C", aParam);
        }
        public static int UpdateIdDoc(SqlTransaction tr, int t366_iddocuh, long t2_iddocumento)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t366_iddocuh", SqlDbType.Int, 4, t366_iddocuh),
                ParametroSql.add("@t2_iddocumento", SqlDbType.BigInt, 8, t2_iddocumento)
            };
            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_DOCUH_DOC_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DOCUH_DOC_U", aParam);
        }
    }
}