using System;
using System.Data;
using System.Configuration;
using System.Text;
using System.Data.SqlClient;

/// <summary>
/// Summary description for FACTURACION
/// </summary>
/// 
namespace GEMO.DAL
{
    public class FACTURACION
    {
        public static SqlDataReader Control(SqlTransaction tr, string sFileName, string sFileNamePath)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@sFileName", SqlDbType.Text, 20);
            aParam[0].Value = sFileName;

            aParam[1] = new SqlParameter("@sFileNamePath", SqlDbType.Text, 400);
            aParam[1].Value = sFileNamePath;

            return SqlHelper.ExecuteSqlDataReaderFic("GEM_CONTROL_SUBIDA", aParam);
            //return Convert.ToInt32(SqlHelper.ExecuteScalarFic("GEM_CONTROL_SUBIDA", aParam));
        }
        public static int Fichero(SqlTransaction tr, string sFileName, string sFileNamePath)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@sFileName", SqlDbType.Text, 20);
            aParam[0].Value = sFileName;


            aParam[1] = new SqlParameter("@sFileNamePath", SqlDbType.Text, 400);
            aParam[1].Value = sFileNamePath;
            //aParam[1].Value = @"\\mk18031\FACTURACION\SG100782.MDB";

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            //if (tr == null)
            return Convert.ToInt32(SqlHelper.ExecuteScalarFic("GEM_SUBIR", aParam));
            //else
            //    return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "GEM_SUBIR", aParam));
        }
        public static int Mail(SqlTransaction tr, DateTime fecha_factura)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@fecha_factura", SqlDbType.SmallDateTime, 4);
            aParam[0].Value = fecha_factura;

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            //if (tr == null)
            return Convert.ToInt32(SqlHelper.ExecuteScalarFic("GEM_COMUNICA_ENVIADO", aParam));
            //else
            //    return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "GEM_SUBIR", aParam));
        }
        public static SqlDataReader Fechas()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            return SqlHelper.ExecuteSqlDataReader("GEM_FACTURACION_FEC", aParam);
        }
        public static SqlDataReader ConsumosLineaMesPropias(long t708_numlinea, DateTime fecha_factura)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t708_numlinea", SqlDbType.BigInt, 8);
            aParam[0].Value = t708_numlinea;
            aParam[1] = new SqlParameter("@fecha_factura", SqlDbType.SmallDateTime, 4);
            aParam[1].Value = fecha_factura;

            return SqlHelper.ExecuteSqlDataReader("GEM_RESUMEN_CONSUMOS_PRO", aParam);
        }
        public static SqlDataReader ConsumosLineaMesPropiasV2(long t708_numlinea, DateTime fecha_factura)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t708_numlinea", SqlDbType.BigInt, 8);
            aParam[0].Value = t708_numlinea;
            aParam[1] = new SqlParameter("@fecha_factura", SqlDbType.SmallDateTime, 4);
            aParam[1].Value = fecha_factura;

            return SqlHelper.ExecuteSqlDataReader("GEM_RESUMEN_CONSUMOS_PRO_V2", aParam);
        }
        public static SqlDataReader ConsumosLineaMesColaboradores(int t101_idficepi, DateTime fecha_factura)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@fecha_factura", SqlDbType.SmallDateTime, 4);
            aParam[0].Value = fecha_factura;
            aParam[1] = new SqlParameter("@t101_idficepi", SqlDbType.Int, 4);
            aParam[1].Value = t101_idficepi;

            return SqlHelper.ExecuteSqlDataReader("GEM_RESUMEN_CONSUMOS_COL", aParam);
        }
        public static SqlDataReader ConsumosLineaMesColaboradoresV2(int t101_idficepi, DateTime fecha_factura)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@fecha_factura", SqlDbType.SmallDateTime, 4);
            aParam[0].Value = fecha_factura;
            aParam[1] = new SqlParameter("@t101_idficepi", SqlDbType.Int, 4);
            aParam[1].Value = t101_idficepi;

            return SqlHelper.ExecuteSqlDataReader("GEM_RESUMEN_CONSUMOS_COL_V2", aParam);
        }
    }
}