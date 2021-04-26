using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace GASVI.DAL
{
    public partial class POSICIONAPARCADA_NEGV
    {
        #region Métodos

        public static SqlDataReader CatalogoGastos(SqlTransaction tr, int t660_idreferencia)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t660_idreferencia", SqlDbType.Int, 4, t660_idreferencia);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("GVT_POSICIONAPARCADA_NEGV_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_POSICIONAPARCADA_NEGV_CAT", aParam);
        }

        public static void AparcarPosicion(SqlTransaction tr,
                                    int t660_idreferencia,
                                    Nullable<DateTime> t661_fechadesde,
                                    Nullable<DateTime> t661_fechahasta,
                                    string t661_destino,
                                    string t661_comentariopos,
                                    byte t661_ncdieta,
                                    byte t661_nmdieta,
                                    byte t661_nedieta,
                                    byte t661_nadieta, 
                                    short t661_nkms,
                                    Nullable<int> t615_iddesplazamiento,
                                    decimal t661_peajepark,
                                    decimal t661_comida,
                                    decimal t661_transporte,
                                    decimal t661_hotel
                                    )
        {
            SqlParameter[] aParam = new SqlParameter[15];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t660_idreferencia", SqlDbType.Int, 4, t660_idreferencia);
            aParam[i++] = ParametroSql.add("@t661_fechadesde", SqlDbType.SmallDateTime, 4, t661_fechadesde);
            aParam[i++] = ParametroSql.add("@t661_fechahasta", SqlDbType.SmallDateTime, 4, t661_fechahasta);
            aParam[i++] = ParametroSql.add("@t661_destino", SqlDbType.VarChar, 50, t661_destino);
            aParam[i++] = ParametroSql.add("@t661_comentariopos", SqlDbType.Text, 16, t661_comentariopos);
            aParam[i++] = ParametroSql.add("@t661_ncdieta", SqlDbType.TinyInt, 1, t661_ncdieta);
            aParam[i++] = ParametroSql.add("@t661_nmdieta", SqlDbType.TinyInt, 1, t661_nmdieta);
            aParam[i++] = ParametroSql.add("@t661_nedieta", SqlDbType.TinyInt, 1, t661_nedieta);
            aParam[i++] = ParametroSql.add("@t661_nadieta", SqlDbType.TinyInt, 1, t661_nadieta);
            aParam[i++] = ParametroSql.add("@t661_nkms", SqlDbType.SmallInt, 2, t661_nkms);
            aParam[i++] = ParametroSql.add("@t615_iddesplazamiento", SqlDbType.Int, 4, t615_iddesplazamiento);
            aParam[i++] = ParametroSql.add("@t661_peajepark", SqlDbType.Money, 8, t661_peajepark);
            aParam[i++] = ParametroSql.add("@t661_comida", SqlDbType.Money, 8, t661_comida);
            aParam[i++] = ParametroSql.add("@t661_transporte", SqlDbType.Money, 8, t661_transporte);
            aParam[i++] = ParametroSql.add("@t661_hotel", SqlDbType.Money, 8, t661_hotel);

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                SqlHelper.ExecuteNonQuery("GVT_POSICIONAPARCADA_NEGV_INS", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_POSICIONAPARCADA_NEGV_INS", aParam);
        }

        public static void DeleteByT660_idreferencia(SqlTransaction tr, int t660_idreferencia)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t660_idreferencia", SqlDbType.Int, 4, t660_idreferencia);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("GVT_POSICIONAPARCADA_NEGV_DByT660_idreferencia", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_POSICIONAPARCADA_NEGV_DByT660_idreferencia", aParam);
        }

        #endregion

    }
}