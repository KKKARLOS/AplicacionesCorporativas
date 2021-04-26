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
    /// Descripción breve de DOCSOLICITUD
    /// </summary>
    public class DOCSOLICITUD
    {
        public DOCSOLICITUD()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        /// <summary>
        /// Obtiene el catálogo de documentos asociados a una solicitud
        /// </summary>
        /// <param name="t696_id"></param>
        /// <returns></returns>
        public static SqlDataReader Catalogo(int t696_id)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t696_id", SqlDbType.Int, 4);
            aParam[0].Value = t696_id;

            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_SOLICITUDDOC_CAT", aParam);
        }
        /// <summary>
        /// Obtiene el catalogo de documentos asociados a una solicitud que todavía no se ha grabado
        /// </summary>
        /// <param name="t624_usuticks"></param>
        /// <returns></returns>
        public static SqlDataReader CatalogoByUsuTicks(string t697_usuticks)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t697_usuticks", SqlDbType.VarChar, 50, t697_usuticks)
            };
            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_SOLICITUDDOC_ByUsuTicks_CAT", aParam);
        }
        public static SqlDataReader Select(SqlTransaction tr, int t697_iddoc)//, bool bTraerArchivo)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t697_iddoc", SqlDbType.Int, 4, t697_iddoc)
            };

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_CVT_SOLICITUDDOC_O", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_SOLICITUDDOC_O", aParam);

            return dr;
        }
        public static int Insert(SqlTransaction tr, Nullable<int> t696_id, string t697_descripcion, string t697_nombrearchivo,
                                 Nullable<long> idContentServer, int t001_idficepi_autor, string t697_usuticks)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t696_id", SqlDbType.Int, 4, t696_id),
                ParametroSql.add("@t697_descripcion", SqlDbType.VarChar, 50, t697_descripcion),
                ParametroSql.add("@t697_nombrearchivo", SqlDbType.VarChar, 250, t697_nombrearchivo),
                ParametroSql.add("@t2_iddocumento", SqlDbType.BigInt, 8, idContentServer),
                ParametroSql.add("@t001_idficepi_autor", SqlDbType.Int, 4, t001_idficepi_autor),
                ParametroSql.add("@t697_usuticks", SqlDbType.VarChar, 50, t697_usuticks)
            };
            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_CVT_SOLICITUDDOC_I", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CVT_SOLICITUDDOC_I", aParam));
        }

        public static int Update(SqlTransaction tr, int t696_id, int t697_iddoc, string t697_descripcion, string t697_nombrearchivo,
                                 Nullable<long> idContentServer, int t001_idficepi_modif)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t696_id", SqlDbType.Int, 4, t696_id),
                ParametroSql.add("@t697_iddoc", SqlDbType.Int, 4, t697_iddoc),
                ParametroSql.add("@t697_descripcion", SqlDbType.VarChar, 50, t697_descripcion),
                ParametroSql.add("@t697_nombrearchivo", SqlDbType.VarChar, 250, t697_nombrearchivo),
                ParametroSql.add("@t2_iddocumento", SqlDbType.BigInt, 8, idContentServer),
                ParametroSql.add("@t001_idficepi_modif", SqlDbType.Int, 4, t001_idficepi_modif)
            };
            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CVT_SOLICITUDDOC_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_SOLICITUDDOC_U", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina un registro de la tabla T697_DOC_SOLICITUD a traves de la primary key.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	18/02/2014 10:14:49
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Delete(SqlTransaction tr, int t697_iddoc)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t697_iddoc", SqlDbType.Int, 4, t697_iddoc),
            };
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CVT_SOLICITUDDOC_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_SOLICITUDDOC_D", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina un registro de la tabla T697_DOC_SOLICITUD a traves del UsuTick.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	18/02/2014 10:14:49
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int DeleteByUsuTicks(SqlTransaction tr, string t697_usuticks)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t697_usuticks", SqlDbType.VarChar, 50, t697_usuticks),
            };
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CVT_SOLICITUDDOC_D2", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_SOLICITUDDOC_D2", aParam);
        }
    }
}