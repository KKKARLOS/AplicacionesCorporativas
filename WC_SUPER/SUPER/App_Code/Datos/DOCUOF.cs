using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using SUPER.Capa_Datos;
using System.Data;

/// <summary>
/// Descripción breve de DOCUOF
/// </summary>
namespace SUPER.DAL
{
    public class DOCUOF
    {
        public DOCUOF()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        public static SqlDataReader Catalogo(SqlTransaction tr, int t610_idordenfac)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t610_idordenfac", SqlDbType.Int, 4, t610_idordenfac)
            };
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_DOCUOF_CAT2", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_DOCUOF_CAT2", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T624_DOCUOF.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [DOARHUMI]	03/03/2014 10:14:49
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, Nullable<int> t610_idordenfac, string t624_descripcion, string t624_nombrearchivo,
                                 byte[] t624_archivo, long t2_iddocumento, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t610_idordenfac", SqlDbType.Int, 4, t610_idordenfac),
                ParametroSql.add("@t624_descripcion", SqlDbType.VarChar, 50, t624_descripcion),
                ParametroSql.add("@t624_nombrearchivo", SqlDbType.VarChar, 250, t624_nombrearchivo),
                ParametroSql.add("@t624_archivo", SqlDbType.Binary, 2147483647, t624_archivo),
                ParametroSql.add("@t2_iddocumento", SqlDbType.BigInt, 8, t2_iddocumento),
                ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario)
            };
            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_DOCUOF_I2", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_DOCUOF_I2", aParam));
        }
    }
}
