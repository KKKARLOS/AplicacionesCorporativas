using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using SUPER.Capa_Datos;
using System.Data;
/// <summary>
/// Descripción breve de ORDENFAC
/// </summary>
/// 
namespace SUPER.DAL
{
    public class ORDENFAC
    {
        public ORDENFAC()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        /// <summary>
        /// Inserta una orden de facturación como copia de una existente y devuelve su código
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t610_idordenfac"></param>
        /// <param name="t314_idusuario"></param>
        /// <returns></returns>
        public static int Duplicar(SqlTransaction tr, int t610_idordenfac, int t314_idusuario, bool bSuperAdm)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t610_idordenfac", SqlDbType.Int, 4, t610_idordenfac),
                ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario),
                ParametroSql.add("@bSuperAdm", SqlDbType.Bit, 1, bSuperAdm)
            };
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_DUPLICARORDENFAC", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_DUPLICARORDENFAC", aParam));
        }
        /// <summary>
        /// Copia registros de documentos manteniendo el id de atenea
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t610_idordenfac"></param>
        /// <param name="t314_idusuario"></param>
        /// <param name="bSuperAdm"></param>
        /// <returns></returns>
        public static int MantenerDocs(SqlTransaction tr, int t610_idordenfac, int t314_idusuario, int iNewOrdenFac)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t610_idordenfac", SqlDbType.Int, 4, t610_idordenfac),
                ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario),
                ParametroSql.add("@t610_idordenfac_new", SqlDbType.Int, 4, iNewOrdenFac)
            };
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_DUPLICARORDENFAC_MANTDOC", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_DUPLICARORDENFAC_MANTDOC", aParam));
        }
        /// <summary>
        /// Indica si existe algún documento en la lista de Ordenes de facturación
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="slOrdenes"></param>
        /// <returns></returns>
        public static bool HayDocs(SqlTransaction tr, string slOrdenes)
        {
            bool bHayDocs = false;
            SqlDataReader dr;
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@slOrdenes", SqlDbType.VarChar, 4000, slOrdenes)
            };
            if (tr == null)
                dr= SqlHelper.ExecuteSqlDataReader("SUP_DOCUOF_CAT3", aParam);
            else
                dr= SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_DOCUOF_CAT3", aParam);
            if (dr.Read())
                bHayDocs = true;
            dr.Close();
            dr.Dispose();

            return bHayDocs;
        }
        public static int CrearDesdePlantilla(SqlTransaction tr, int t629_idplantillaof, int t314_idusuario, bool bSuperAdm)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t629_idplantillaof", SqlDbType.Int, 4, t629_idplantillaof),
                ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario),
                ParametroSql.add("@bSuperAdm", SqlDbType.Bit, 1, bSuperAdm)
            };
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_ORDENFAC_ByPlantilla", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_ORDENFAC_ByPlantilla", aParam));
        }
        public static int NumDocs(SqlTransaction tr, int t610_idordenfac)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t610_idordenfac", SqlDbType.Int, 4, t610_idordenfac)
            };
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_DOCUOF_COUNT", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_DOCUOF_COUNT", aParam));
        }

    }

}
