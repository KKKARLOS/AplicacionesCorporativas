using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace GASVI.DAL
{
    /// <summary>
    /// Summary description for POSICIONAPARCADA_NMPGV
    /// </summary>
    public partial class POSICIONAPARCADA_NMPGV
    {
        public static SqlDataReader CatalogoGastos(SqlTransaction tr, int t663_idreferencia)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t663_idreferencia", SqlDbType.Int, 4, t663_idreferencia);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("GVT_POSICIONAPARCADA_NMPGV_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_POSICIONAPARCADA_NMPGV_CAT", aParam);
        }

        public static void AparcarPosicion(SqlTransaction tr,
                            int t663_idreferencia,
                            Nullable<DateTime> t664_fechadesde,
                            Nullable<DateTime> t664_fechahasta,
                            string t664_destino,
                            string t664_comentariopos,
                            byte t664_ncdieta,
                            byte t664_nmdieta,
                            byte t664_nedieta,
                            byte t664_nadieta,
                            short t664_nkms,
                            Nullable<int> t615_iddesplazamiento,
                            decimal t664_peajepark,
                            decimal t664_comida,
                            decimal t664_transporte,
                            decimal t664_hotel,
                            Nullable<int> t305_idproyectosubnodo
                            )
        {
            SqlParameter[] aParam = new SqlParameter[16];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t663_idreferencia", SqlDbType.Int, 4, t663_idreferencia);
            aParam[i++] = ParametroSql.add("@t664_fechadesde", SqlDbType.SmallDateTime, 4, t664_fechadesde);
            aParam[i++] = ParametroSql.add("@t664_fechahasta", SqlDbType.SmallDateTime, 4, t664_fechahasta);
            aParam[i++] = ParametroSql.add("@t664_destino", SqlDbType.VarChar, 50, t664_destino);
            aParam[i++] = ParametroSql.add("@t664_comentariopos", SqlDbType.Text, 16, t664_comentariopos);
            aParam[i++] = ParametroSql.add("@t664_ncdieta", SqlDbType.TinyInt, 1, t664_ncdieta);
            aParam[i++] = ParametroSql.add("@t664_nmdieta", SqlDbType.TinyInt, 1, t664_nmdieta);
            aParam[i++] = ParametroSql.add("@t664_nedieta", SqlDbType.TinyInt, 1, t664_nedieta);
            aParam[i++] = ParametroSql.add("@t664_nadieta", SqlDbType.TinyInt, 1, t664_nadieta);
            aParam[i++] = ParametroSql.add("@t664_nkms", SqlDbType.SmallInt, 2, t664_nkms);
            aParam[i++] = ParametroSql.add("@t615_iddesplazamiento", SqlDbType.Int, 4, t615_iddesplazamiento);
            aParam[i++] = ParametroSql.add("@t664_peajepark", SqlDbType.Money, 8, t664_peajepark);
            aParam[i++] = ParametroSql.add("@t664_comida", SqlDbType.Money, 8, t664_comida);
            aParam[i++] = ParametroSql.add("@t664_transporte", SqlDbType.Money, 8, t664_transporte);
            aParam[i++] = ParametroSql.add("@t664_hotel", SqlDbType.Money, 8, t664_hotel);
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                SqlHelper.ExecuteNonQuery("GVT_POSICIONAPARCADA_NMPGV_INS", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_POSICIONAPARCADA_NMPGV_INS", aParam);
        }


        public static void DeleteByT663_idreferencia(SqlTransaction tr, int t663_idreferencia)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t663_idreferencia", SqlDbType.Int, 4, t663_idreferencia);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("GVT_POSICIONAPARCADA_NMPGV_DByT663_idreferencia", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_POSICIONAPARCADA_NMPGV_DByT663_idreferencia", aParam);
        }
    }
}