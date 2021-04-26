using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
//using SUPER.Capa_Negocio;

namespace SUPER.Capa_Datos
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : POSICIONGV
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T421_POSICIONGV
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	16/03/2011 11:54:32	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class POSICIONGV
    {
        #region Metodos

        public static SqlDataReader CatalogoGastos(SqlTransaction tr, int t420_idreferencia)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t420_idreferencia", SqlDbType.Int, 4, t420_idreferencia);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("GVT_POSICIONGV_CAT_NE", aParam); //Catálogo Nota Estándar
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_POSICIONGV_CAT_NE", aParam); //Catálogo Nota Estándar
        }

        public static void InsertarPosicion(SqlTransaction tr,
            int t420_idreferencia,
            DateTime t421_fechadesde,
            DateTime t421_fechahasta,
            string t421_destino,
            string t421_comentariopos,
            byte t421_ncdieta,
            byte t421_nmdieta,
            byte t421_nedieta,
            byte t421_nadieta,
            short t421_nkms,
            Nullable<int> t615_iddesplazamiento,
            decimal t421_peajepark,
            decimal t421_comida,
            decimal t421_transporte,
            decimal t421_hotel
            ){
                SqlParameter[] aParam = new SqlParameter[15];
                int i = 0;
                aParam[i++] = ParametroSql.add("@t420_idreferencia", SqlDbType.Int, 4, t420_idreferencia);
                aParam[i++] = ParametroSql.add("@t421_fechadesde", SqlDbType.SmallDateTime, 4, t421_fechadesde);
                aParam[i++] = ParametroSql.add("@t421_fechahasta", SqlDbType.SmallDateTime, 4, t421_fechahasta);
                aParam[i++] = ParametroSql.add("@t421_destino", SqlDbType.VarChar, 50, t421_destino);
                aParam[i++] = ParametroSql.add("@t421_comentariopos", SqlDbType.Text, 16, t421_comentariopos);
                aParam[i++] = ParametroSql.add("@t421_ncdieta", SqlDbType.TinyInt, 1, t421_ncdieta);
                aParam[i++] = ParametroSql.add("@t421_nmdieta", SqlDbType.TinyInt, 1, t421_nmdieta);
                aParam[i++] = ParametroSql.add("@t421_nedieta", SqlDbType.TinyInt, 1, t421_nedieta);
                aParam[i++] = ParametroSql.add("@t421_nadieta", SqlDbType.TinyInt, 1, t421_nadieta);
                aParam[i++] = ParametroSql.add("@t421_nkms", SqlDbType.SmallInt, 2, t421_nkms);
                aParam[i++] = ParametroSql.add("@t615_iddesplazamiento", SqlDbType.Int, 4, t615_iddesplazamiento);
                aParam[i++] = ParametroSql.add("@t421_peajepark", SqlDbType.Money, 8, t421_peajepark);
                aParam[i++] = ParametroSql.add("@t421_comida", SqlDbType.Money, 8, t421_comida);
                aParam[i++] = ParametroSql.add("@t421_transporte", SqlDbType.Money, 8, t421_transporte);
                aParam[i++] = ParametroSql.add("@t421_hotel", SqlDbType.Money, 8, t421_hotel);

                // Ejecuta la query y devuelve el valor del nuevo Identity.
                if (tr == null)
                    SqlHelper.ExecuteNonQuery("GVT_POSICIONGV_INS", aParam);
                else
                    SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_POSICIONGV_INS", aParam);
            }

            public static void DeleteByT420_idreferencia(SqlTransaction tr, int t420_idreferencia)
            {
                SqlParameter[] aParam = new SqlParameter[1];
                int i = 0;
                aParam[i++] = ParametroSql.add("@t420_idreferencia", SqlDbType.Int, 4, t420_idreferencia);

                if (tr == null)
                    SqlHelper.ExecuteNonQuery("GVT_POSICIONGV_DByT420_idreferencia", aParam);
                else
                    SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_POSICIONGV_DByT420_idreferencia", aParam);
            }

        #endregion
    }
}
