using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Data.SqlClient;
using SUPER.Capa_Datos;

/// <summary>
/// Summary description for Factura 
/// </summary>
public class Factura
{
    public Factura()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    //Lineas de factura
    public static SqlDataReader Lineas(SqlTransaction tr, string t376_seriefactura, int t376_numerofactura)
    {
        SqlParameter[] aParam = new SqlParameter[2];
        aParam[0] = new SqlParameter("@sSerie", SqlDbType.Text, 5);
        aParam[0].Value = t376_seriefactura;
        aParam[1] = new SqlParameter("@nFactura", SqlDbType.Int, 4);
        aParam[1].Value = t376_numerofactura;

        if (tr == null)
            return SqlHelper.ExecuteSqlDataReader("SUP_FACT_LINEAS", aParam);
        else
            return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_FACT_LINEAS", aParam);
    }
    //Lineas de factura y cobros
    public static SqlDataReader LineasYCobros(string t376_seriefactura, int t376_numerofactura)
    {
        SqlParameter[] aParam = new SqlParameter[2];
        aParam[0] = new SqlParameter("@sSerie", SqlDbType.Text, 5);
        aParam[0].Value = t376_seriefactura;
        aParam[1] = new SqlParameter("@nFactura", SqlDbType.Int, 4);
        aParam[1].Value = t376_numerofactura;

        // Ejecuta la query y devuelve un SqlDataReader con el resultado.
        return SqlHelper.ExecuteSqlDataReader("SUP_FACTyCOBROS", aParam);
    }
    //public static SqlDataReader Cobros(string t376_seriefactura, int t376_numerofactura)
    //{
    //    SqlParameter[] aParam = new SqlParameter[2];
    //    aParam[0] = new SqlParameter("@sSerie", SqlDbType.Text, 5);
    //    aParam[0].Value = t376_seriefactura;
    //    aParam[1] = new SqlParameter("@nFactura", SqlDbType.Int, 4);
    //    aParam[1].Value = t376_numerofactura;

    //    // Ejecuta la query y devuelve un SqlDataReader con el resultado.
    //    return SqlHelper.ExecuteSqlDataReader("SUP_COBROSdeFACT", aParam);
    //}
    public static SqlDataReader CobrosMes(SqlTransaction tr, string t376_seriefactura, int t376_numerofactura)
    {
        SqlParameter[] aParam = new SqlParameter[2];
        aParam[0] = new SqlParameter("@t376_seriefactura", SqlDbType.Int, 4);
        aParam[0].Value = t376_seriefactura;
        aParam[1] = new SqlParameter("@t376_numerofactura", SqlDbType.Int, 4);
        aParam[1].Value = t376_numerofactura;

        if (tr == null)
            return SqlHelper.ExecuteSqlDataReader("SUP_COBROS_FACT_MES", aParam);
        else
            return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_COBROS_FACT_MES", aParam);
    }
    //Obtiene el añomes de una factura
    public static int GetAnoMes(string t376_seriefactura, int t376_numerofactura)
    {
        SqlParameter[] aParam = new SqlParameter[2];
        aParam[0] = new SqlParameter("@t376_seriefactura", SqlDbType.Text, 5);
        aParam[0].Value = t376_seriefactura;
        aParam[1] = new SqlParameter("@t376_numerofactura", SqlDbType.Int, 4);
        aParam[1].Value = t376_numerofactura;

        object obj = SqlHelper.ExecuteScalar("SUP_ANOMESFACT_S", aParam);
        if (obj != null)
            return Convert.ToInt32(obj);
        else
            return 0;
    }
    public static string getDatoDisponibilidadFra(string sSerieNumeroFactura)
    {
        SqlParameter[] aParam = new SqlParameter[]{
            ParametroSql.add("@sSerieNumeroFactura", SqlDbType.VarChar, 10, sSerieNumeroFactura)
        };

        return SqlHelper.ExecuteScalar("SUP_DISPONIBILIDAD_FRA_SAP", aParam).ToString();
    }
}