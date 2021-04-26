using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace GASVI.DAL
{
    public class CABECERAAPARCADA_NEGV
    {
        #region Metodos

        public static SqlDataReader ObtenerDatosCabecera(SqlTransaction tr, int t660_idreferencia)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t660_idreferencia", SqlDbType.Int, 4, t660_idreferencia);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("GVT_CABECERAAPARCADA_NEGV_O", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_CABECERAAPARCADA_NEGV_O", aParam);
        }

        public static int AparcarCabecera(SqlTransaction tr,
                string t660_concepto,
                int t001_idficepi_solicitante,
                int t314_idusuario_interesado,
                byte t423_idmotivo,
                bool t660_justificantes,
                Nullable<int> t305_idproyectosubnodo,
                string t422_idmoneda,
                string t660_comentarionota,
                string t660_anotaciones,
                decimal t660_importeanticipo,
                Nullable<DateTime> t660_fanticipo,
                string t660_lugaranticipo,
                decimal t660_importedevolucion,
                Nullable<DateTime> t660_fdevolucion,
                string t660_lugardevolucion,
                string t660_aclaracionesanticipo,
                decimal t660_pagadotransporte,
                decimal t660_pagadohotel,
                decimal t660_pagadootros,
                string t660_aclaracionepagado,
                Nullable<int> t313_idempresa
            )
        {
            SqlParameter[] aParam = new SqlParameter[21];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t660_concepto", SqlDbType.VarChar, 50, t660_concepto);
            aParam[i++] = ParametroSql.add("@t001_idficepi_solicitante", SqlDbType.Int, 4, t001_idficepi_solicitante);
            aParam[i++] = ParametroSql.add("@t314_idusuario_interesado", SqlDbType.Int, 4, t314_idusuario_interesado);
            aParam[i++] = ParametroSql.add("@t423_idmotivo", SqlDbType.TinyInt, 1, t423_idmotivo);
            aParam[i++] = ParametroSql.add("@t660_justificantes", SqlDbType.Bit, 1, t660_justificantes);
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, (t423_idmotivo == 1) ? (int?)t305_idproyectosubnodo : null);
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);
            aParam[i++] = ParametroSql.add("@t660_comentarionota", SqlDbType.Text, 2147483647, t660_comentarionota);
            aParam[i++] = ParametroSql.add("@t660_anotaciones", SqlDbType.Text, 2147483647, t660_anotaciones);
            aParam[i++] = ParametroSql.add("@t660_importeanticipo", SqlDbType.Money, 8, t660_importeanticipo);
            aParam[i++] = ParametroSql.add("@t660_fanticipo", SqlDbType.SmallDateTime, 4, (t660_fanticipo.HasValue) ? (DateTime?)t660_fanticipo : null);
            aParam[i++] = ParametroSql.add("@t660_lugaranticipo", SqlDbType.VarChar, 50, t660_lugaranticipo);
            aParam[i++] = ParametroSql.add("@t660_importedevolucion", SqlDbType.Money, 8, t660_importedevolucion);
            aParam[i++] = ParametroSql.add("@t660_fdevolucion", SqlDbType.SmallDateTime, 4, (t660_fdevolucion.HasValue) ? (DateTime?)t660_fdevolucion : null);
            aParam[i++] = ParametroSql.add("@t660_lugardevolucion", SqlDbType.VarChar, 50, t660_lugardevolucion);
            aParam[i++] = ParametroSql.add("@t660_aclaracionesanticipo", SqlDbType.Text, 2147483647, t660_aclaracionesanticipo);
            aParam[i++] = ParametroSql.add("@t660_pagadotransporte", SqlDbType.Money, 8, t660_pagadotransporte);
            aParam[i++] = ParametroSql.add("@t660_pagadohotel", SqlDbType.Money, 8, t660_pagadohotel);
            aParam[i++] = ParametroSql.add("@t660_pagadootros", SqlDbType.Money, 8, t660_pagadootros);
            aParam[i++] = ParametroSql.add("@t660_aclaracionepagado", SqlDbType.Text, 2147483647, t660_aclaracionepagado);
            aParam[i++] = ParametroSql.add("@t313_idempresa", SqlDbType.Int, 4, t313_idempresa);

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("GVT_CABECERAAPARCADA_NEGV_INS", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "GVT_CABECERAAPARCADA_NEGV_INS", aParam));

        }

        public static int ReAparcarCabecera(SqlTransaction tr,
                int t660_idreferencia,
                string t660_concepto,
                int t001_idficepi_solicitante,
                int t314_idusuario_interesado,
                byte t423_idmotivo,
                bool t660_justificantes,
                Nullable<int> t305_idproyectosubnodo,
                string t422_idmoneda,
                string t660_comentarionota,
                string t660_anotaciones,
                decimal t660_importeanticipo,
                Nullable<DateTime> t660_fanticipo,
                string t660_lugaranticipo,
                decimal t660_importedevolucion,
                Nullable<DateTime> t660_fdevolucion,
                string t660_lugardevolucion,
                string t660_aclaracionesanticipo,
                decimal t660_pagadotransporte,
                decimal t660_pagadohotel,
                decimal t660_pagadootros,
                string t660_aclaracionepagado,
                DateTime t660_faparcada,
                Nullable<int> t313_idempresa
            )
        {
            SqlParameter[] aParam = new SqlParameter[23];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t660_idreferencia", SqlDbType.Int, 4, t660_idreferencia);
            aParam[i++] = ParametroSql.add("@t660_concepto", SqlDbType.VarChar, 50, t660_concepto);
            aParam[i++] = ParametroSql.add("@t001_idficepi_solicitante", SqlDbType.Int, 4, t001_idficepi_solicitante);
            aParam[i++] = ParametroSql.add("@t314_idusuario_interesado", SqlDbType.Int, 4, t314_idusuario_interesado);
            aParam[i++] = ParametroSql.add("@t423_idmotivo", SqlDbType.TinyInt, 1, t423_idmotivo);
            aParam[i++] = ParametroSql.add("@t660_justificantes", SqlDbType.Bit, 1, t660_justificantes);
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, (t423_idmotivo == 1) ? (int?)t305_idproyectosubnodo : null);
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);
            aParam[i++] = ParametroSql.add("@t660_comentarionota", SqlDbType.Text, 2147483647, t660_comentarionota);
            aParam[i++] = ParametroSql.add("@t660_anotaciones", SqlDbType.Text, 2147483647, t660_anotaciones);
            aParam[i++] = ParametroSql.add("@t660_importeanticipo", SqlDbType.Money, 8, t660_importeanticipo);
            aParam[i++] = ParametroSql.add("@t660_fanticipo", SqlDbType.SmallDateTime, 4, (t660_fanticipo.HasValue) ? (DateTime?)t660_fanticipo : null);
            aParam[i++] = ParametroSql.add("@t660_lugaranticipo", SqlDbType.VarChar, 50, t660_lugaranticipo);
            aParam[i++] = ParametroSql.add("@t660_importedevolucion", SqlDbType.Money, 8, t660_importedevolucion);
            aParam[i++] = ParametroSql.add("@t660_fdevolucion", SqlDbType.SmallDateTime, 4, (t660_fdevolucion.HasValue) ? (DateTime?)t660_fdevolucion : null);
            aParam[i++] = ParametroSql.add("@t660_lugardevolucion", SqlDbType.VarChar, 50, t660_lugardevolucion);
            aParam[i++] = ParametroSql.add("@t660_aclaracionesanticipo", SqlDbType.Text, 2147483647, t660_aclaracionesanticipo);
            aParam[i++] = ParametroSql.add("@t660_pagadotransporte", SqlDbType.Money, 8, t660_pagadotransporte);
            aParam[i++] = ParametroSql.add("@t660_pagadohotel", SqlDbType.Money, 8, t660_pagadohotel);
            aParam[i++] = ParametroSql.add("@t660_pagadootros", SqlDbType.Money, 8, t660_pagadootros);
            aParam[i++] = ParametroSql.add("@t660_aclaracionepagado", SqlDbType.Text, 2147483647, t660_aclaracionepagado);
            aParam[i++] = ParametroSql.add("@t660_faparcada", SqlDbType.SmallDateTime, 4, t660_faparcada);
            aParam[i++] = ParametroSql.add("@t313_idempresa", SqlDbType.Int, 4, t313_idempresa);

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("GVT_CABECERAAPARCADA_NEGV_UPD", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_CABECERAAPARCADA_NEGV_UPD", aParam);

        }

        public static int EliminarNotaAparcada(SqlTransaction tr, int t660_idreferencia)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t660_idreferencia", SqlDbType.Int, 4, t660_idreferencia);

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("GVT_CABECERAAPARCADA_NEGV_DEL", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_CABECERAAPARCADA_NEGV_DEL", aParam);
        }

        /// <summary>
        /// Comprueba si una empresa está vigente para un profesional (tabla T654_FICEPIEMPTER)
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t313_idempresa"></param>
        /// <param name="t314_idusuario"></param>
        /// <returns></returns>
        public static bool EmpresaNotaVigenteEnUsuario(SqlTransaction tr, int t313_idempresa, int t314_idusuario)
        {
            bool bRes = false;
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t313_idempresa", SqlDbType.Int, 4, t313_idempresa);
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);

            object miObj;
            if (tr == null)
                miObj= SqlHelper.ExecuteScalar("GVT_CABECERAAPARCADA_NEGV_DEL", aParam);
            else
                miObj= SqlHelper.ExecuteScalarTransaccion(tr, "GVT_CABECERAAPARCADA_NEGV_DEL", aParam);
            if (miObj.ToString() == "1")
                bRes = true;
            return bRes;
        }

        #endregion
    }
}