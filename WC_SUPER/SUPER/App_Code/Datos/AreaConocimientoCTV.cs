using System;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.DAL
{

    public  class AreaConocimientoCTV
    {
        #region Metodos   
        
        public static SqlDataReader CatalogoConSec(SqlTransaction tr, Nullable<bool> bActiva)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@bActiva", SqlDbType.Bit, 1, bActiva)
            };
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_AREACONOCIMIENTOSEC_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_AREACONOCIMIENTOSEC_CAT", aParam);
        }

        public static SqlDataReader CatalogoConTecno(SqlTransaction tr, Nullable<bool> bActiva)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@bActiva", SqlDbType.Bit, 1, bActiva)
            };
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_AREACONOCIMIENTOTEC_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_AREACONOCIMIENTOTEC_CAT", aParam);
        }

        public static int InsertConSec(SqlTransaction tr, string T809_DENOMINACION, bool bActiva)
        {
            //aParam[i++] = ParametroSql.add("@T809_DENOMINACION", SqlDbType.Text, 120, T809_DENOMINACION);
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@T809_DENOMINACION", SqlDbType.Text, 50, T809_DENOMINACION.ToUpper()),
                ParametroSql.add("@bActiva", SqlDbType.Bit, 1, bActiva)
            };

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_CVT_AREACONOCIMIENTOSEC_INS", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CVT_AREACONOCIMIENTOSEC_INS", aParam));
        }

        public static int InsertConTec(SqlTransaction tr, string T810_DENOMINACION, bool bActiva)
        {
            //aParam[i++] = ParametroSql.add("@T810_DENOMINACION", SqlDbType.Text, 120, T810_DENOMINACION);
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@T810_DENOMINACION", SqlDbType.Text, 50, T810_DENOMINACION.ToUpper()),
                ParametroSql.add("@bActiva", SqlDbType.Bit, 1, bActiva)
            };

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_CVT_AREACONOCIMIENTOTEC_INS", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CVT_AREACONOCIMIENTOTEC_INS", aParam));
        }

        public static void UpdateConSec(SqlTransaction tr, int T809_IDACONSECT, string T809_DENOMINACION, bool bActiva)
        {
            //aParam[i++] = ParametroSql.add("@T809_IDACONSECT", SqlDbType.Int, 4, T809_IDACONSECT);
            //aParam[i++] = ParametroSql.add("@T809_DENOMINACION", SqlDbType.Text, 120, T809_DENOMINACION);
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@T809_IDACONSECT", SqlDbType.Int, 4, T809_IDACONSECT),
                ParametroSql.add("@T809_DENOMINACION", SqlDbType.Text, 50, T809_DENOMINACION),
                ParametroSql.add("@bActiva", SqlDbType.Bit, 1, bActiva)
            };
            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CVT_AREACONOCIMIENTOSEC_UPD", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_AREACONOCIMIENTOSEC_UPD", aParam);
        }

        public static void UpdateConTec(SqlTransaction tr, int T810_IDACONTECNO, string T810_DENOMINACION, bool bActiva)
        {
            //aParam[i++] = ParametroSql.add("@T810_IDACONTECNO", SqlDbType.Int, 4, T810_IDACONTECNO);
            //aParam[i++] = ParametroSql.add("@T810_DENOMINACION", SqlDbType.Text, 120, T810_DENOMINACION);
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@T810_IDACONTECNO", SqlDbType.Int, 4, T810_IDACONTECNO),
                ParametroSql.add("@T810_DENOMINACION", SqlDbType.Text, 50, T810_DENOMINACION),
                ParametroSql.add("@bActiva", SqlDbType.Bit, 1, bActiva)
            };
            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CVT_AREACONOCIMIENTOTEC_UPD", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_AREACONOCIMIENTOTEC_UPD", aParam);
        }

        public static void DeleteConSec(SqlTransaction tr, int T809_IDACONSECT)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@T809_IDACONSECT", SqlDbType.Int, 4);
            aParam[0].Value = T809_IDACONSECT;

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CVT_AREACONOCIMIENTOSEC_DEL", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_AREACONOCIMIENTOSEC_DEL", aParam);
        }
        public static void DeleteConTec(SqlTransaction tr, int T810_IDACONTECNO)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@T810_IDACONTECNO", SqlDbType.Int, 4);
            aParam[0].Value = T810_IDACONTECNO;

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CVT_AREACONOCIMIENTOTEC_DEL", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_AREACONOCIMIENTOTEC_DEL", aParam);
        }

        public static SqlDataReader CatalogoAreasSec(SqlTransaction tr, string conocimientos)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@conocimientos", SqlDbType.VarChar, 8000, conocimientos);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_AREASSEC_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_AREASSEC_CAT", aParam);
        }

        public static SqlDataReader CatalogoAreasTecno(SqlTransaction tr, string conocimientos)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@conocimientos", SqlDbType.VarChar, 8000, conocimientos);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_AREASTEC_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_AREASTEC_CAT", aParam);
        }

        #endregion
    }
}
