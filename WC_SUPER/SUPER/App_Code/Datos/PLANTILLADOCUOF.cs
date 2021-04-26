using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using SUPER.Capa_Datos;
using System.Data;
/// <summary>
/// Descripción breve de PLANTILLADOCUOF
/// </summary>
namespace SUPER.DAL
{
    public class PLANTILLADOCUOF
    {
        public PLANTILLADOCUOF()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        public static SqlDataReader Catalogo(SqlTransaction tr, int t629_idplantillaof)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t629_idplantillaof", SqlDbType.Int, 4, t629_idplantillaof)
            };
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_PLANTILLADOCUOF_CAT2", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PLANTILLADOCUOF_CAT2", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T631_PLANTILLADOCUOF.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [DOARHUMI]	04/03/2014 10:14:49
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, Nullable<int> t629_idplantillaof, string t624_descripcion, string t624_nombrearchivo,
                                 long t2_iddocumento, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t629_idplantillaof", SqlDbType.Int, 4, t629_idplantillaof),
                ParametroSql.add("@t631_descripcion", SqlDbType.VarChar, 50, t624_descripcion),
                ParametroSql.add("@t631_nombrearchivo", SqlDbType.VarChar, 250, t624_nombrearchivo),
                ParametroSql.add("@t2_iddocumento", SqlDbType.BigInt, 8, t2_iddocumento),
                ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario)
            };
            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_PLANTILLADOCUOF_I2", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_PLANTILLADOCUOF_I2", aParam));
        }
    }
}
