using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using SUPER.Capa_Datos;
using System.Data;
/// <summary>
/// Descripción breve de PLANTILLAORDENFAC
/// </summary>
namespace SUPER.DAL
{
    public class PLANTILLAORDENFAC
    {
        public PLANTILLAORDENFAC()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        /// <summary>
        /// Inserta una plantilla de orden de facturación como copia de una existente y devuelve su código
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t610_idordenfac"></param>
        /// <param name="t314_idusuario"></param>
        /// <returns></returns>
        public static int CrearDesdePlantilla(SqlTransaction tr, int t629_idplantillaof, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t629_idplantillaof", SqlDbType.Int, 4, t629_idplantillaof),
                ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario)
            };
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_PLANTILLAORDENFAC_ByPlantilla", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_PLANTILLAORDENFAC_ByPlantilla", aParam));
        }
        /// <summary>
        /// Inserta una plantilla de orden de facturación como copia de una orden de facturación y devuelve su código
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t610_idordenfac"></param>
        /// <param name="t314_idusuario"></param>
        /// <returns></returns>
        public static int CrearDesdeOrden(SqlTransaction tr, int t610_idordenfac, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t610_idordenfac", SqlDbType.Int, 4, t610_idordenfac),
                ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario)
            };
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_PLANTILLAORDENFAC_ByOrden", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_PLANTILLAORDENFAC_ByOrden", aParam));
        }
        /// <summary>
        /// Copia registros de documentos DE UNA ORDEN DE FACTURACIÓN A UNA plantilla manteniendo el id de atenea
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t610_idordenfac"></param>
        /// <param name="t314_idusuario"></param>
        /// <param name="bSuperAdm"></param>
        /// <returns></returns>
        public static int MantenerDocsOF(SqlTransaction tr, int t610_idordenfac, int t314_idusuario, int t629_idplantillaof)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t610_idordenfac", SqlDbType.Int, 4, t610_idordenfac),
                ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario),
                ParametroSql.add("@t629_idplantillaof_new", SqlDbType.Int, 4, t629_idplantillaof)
            };
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_DOCUOF_COPIAR_PLANTILLA", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_DOCUOF_COPIAR_PLANTILLA", aParam));
        }
        /// <summary>
        /// Copia registros de documentos manteniendo el id de atenea
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t610_idordenfac"></param>
        /// <param name="t314_idusuario"></param>
        /// <param name="bSuperAdm"></param>
        /// <returns></returns>
        public static int MantenerDocs(SqlTransaction tr, int t629_idplantillaof, int t314_idusuario, int iNewPlant)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t629_idplantillaof", SqlDbType.Int, 4, t629_idplantillaof),
                ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario),
                ParametroSql.add("@t629_idplantillaof_new", SqlDbType.Int, 4, iNewPlant)
            };
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_DUPLICARPLANTORDENFAC_MANTDOC", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_DUPLICARPLANTORDENFAC_MANTDOC", aParam));
        }
        /// <summary>
        /// Indica si existe algún documento en la lista de plantillas de Ordenes de facturación
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="slPlantillas"></param>
        /// <returns></returns>
        public static bool HayDocs(SqlTransaction tr, string slPlantillas)
        {
            bool bHayDocs = false;
            SqlDataReader dr;
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@slPlantillas", SqlDbType.VarChar, 4000, slPlantillas)
            };
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_PLANTILLADOCUOF_CAT3", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PLANTILLADOCUOF_CAT3", aParam);
            if (dr.Read())
                bHayDocs = true;
            dr.Close();
            dr.Dispose();

            return bHayDocs;
        }
    }
}
