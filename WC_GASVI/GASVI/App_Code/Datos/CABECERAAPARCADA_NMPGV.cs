using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace GASVI.DAL
{
    public class CABECERAAPARCADA_NMPGV
    {
        #region Metodos

        public static SqlDataReader ObtenerDatosCabecera(SqlTransaction tr, int t663_idreferencia)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t663_idreferencia", SqlDbType.Int, 4, t663_idreferencia);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("GVT_CABECERAAPARCADA_NMPGV_O", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_CABECERAAPARCADA_NMPGV_O", aParam);
        }

        public static int AparcarCabecera(SqlTransaction tr,
                string t663_concepto,
                int t001_idficepi_solicitante,
                int t314_idusuario_interesado,
                bool t663_justificantes,
                Nullable<int> t305_idproyectosubnodo,
                string t422_idmoneda,
                string t663_comentarionota,
                string t663_anotaciones,
                Nullable<int> t313_idempresa
            )
        {
            SqlParameter[] aParam = new SqlParameter[9];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t663_concepto", SqlDbType.VarChar, 50, t663_concepto);
            aParam[i++] = ParametroSql.add("@t001_idficepi_solicitante", SqlDbType.Int, 4, t001_idficepi_solicitante);
            aParam[i++] = ParametroSql.add("@t314_idusuario_interesado", SqlDbType.Int, 4, t314_idusuario_interesado);
            aParam[i++] = ParametroSql.add("@t663_justificantes", SqlDbType.Bit, 1, t663_justificantes);
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);
            aParam[i++] = ParametroSql.add("@t663_comentarionota", SqlDbType.Text, 16, t663_comentarionota);
            aParam[i++] = ParametroSql.add("@t663_anotaciones", SqlDbType.Text, 16, t663_anotaciones);
            aParam[i++] = ParametroSql.add("@t313_idempresa", SqlDbType.Int, 4, t313_idempresa);

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("GVT_CABECERAAPARCADA_NMPGV_INS", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "GVT_CABECERAAPARCADA_NMPGV_INS", aParam));

        }

        public static int ReAparcarCabecera(SqlTransaction tr,
                int t663_idreferencia,
                string t663_concepto,
                int t001_idficepi_solicitante,
                int t314_idusuario_interesado,
                bool t663_justificantes,
                Nullable<int> t305_idproyectosubnodo,
                string t422_idmoneda,
                string t663_comentarionota,
                string t663_anotaciones,
                DateTime t663_faparcada,
                Nullable<int> t313_idempresa
            )
        {
            SqlParameter[] aParam = new SqlParameter[11];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t663_idreferencia", SqlDbType.Int, 4, t663_idreferencia);
            aParam[i++] = ParametroSql.add("@t663_concepto", SqlDbType.VarChar, 50, t663_concepto);
            aParam[i++] = ParametroSql.add("@t001_idficepi_solicitante", SqlDbType.Int, 4, t001_idficepi_solicitante);
            aParam[i++] = ParametroSql.add("@t314_idusuario_interesado", SqlDbType.Int, 4, t314_idusuario_interesado);
            aParam[i++] = ParametroSql.add("@t663_justificantes", SqlDbType.Bit, 1, t663_justificantes);
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);
            aParam[i++] = ParametroSql.add("@t663_comentarionota", SqlDbType.Text, 16, t663_comentarionota);
            aParam[i++] = ParametroSql.add("@t663_anotaciones", SqlDbType.Text, 16, t663_anotaciones);
            aParam[i++] = ParametroSql.add("@t663_faparcada", SqlDbType.SmallDateTime, 4, t663_faparcada);
            aParam[i++] = ParametroSql.add("@t313_idempresa", SqlDbType.Int, 4, t313_idempresa);

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("GVT_CABECERAAPARCADA_NMPGV_UPD", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_CABECERAAPARCADA_NMPGV_UPD", aParam);

        }

        public static int EliminarNotaAparcada(SqlTransaction tr, int t663_idreferencia)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t663_idreferencia", SqlDbType.Int, 4, t663_idreferencia);

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("GVT_CABECERAAPARCADA_NMPGV_DEL", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_CABECERAAPARCADA_NMPGV_DEL", aParam);
        }


        #endregion
    }
}