using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SUPER.Capa_Datos
{
	public partial class CABECERAGV
	{
		#region Metodos

        public static SqlDataReader ObtenerDatosCabecera(SqlTransaction tr, int t420_idreferencia)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t420_idreferencia", SqlDbType.Int, 4, t420_idreferencia);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("GVT_CABECERAGV_O", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_CABECERAGV_O", aParam);
        }

        public static SqlDataReader ObtenerDatosCabeceraBono(SqlTransaction tr, int t420_idreferencia)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t420_idreferencia", SqlDbType.Int, 4, t420_idreferencia);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("GVT_CABECERAGV_BONO_SEL", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_CABECERAGV_BONO_SEL", aParam);
        }

        public static SqlDataReader ObtenerDatosCabeceraPago(SqlTransaction tr, int t420_idreferencia)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t420_idreferencia", SqlDbType.Int, 4, t420_idreferencia);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("GVT_CABECERAGV_PAGO_SEL", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_CABECERAGV_PAGO_SEL", aParam);
        }

        public static int InsertarCabeceraPago(SqlTransaction tr, string t431_idestado, string t420_concepto, int t001_idficepi_solicitante,
                                            int t314_idusuario_interesado, Nullable<int> t305_idproyectosubnodo,
                                            string t422_idmoneda, string t420_comentarionota, string t420_anotaciones,decimal t420_importe,
                                            byte t423_idmotivo, Nullable<int> t666_idacuerdogv, int t313_idempresa, byte t007_idterrfis)
        {
            SqlParameter[] aParam = new SqlParameter[13];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t431_idestado", SqlDbType.Char, 1, t431_idestado);
            aParam[i++] = ParametroSql.add("@t420_concepto", SqlDbType.VarChar, 50, t420_concepto);
            aParam[i++] = ParametroSql.add("@t001_idficepi_solicitante", SqlDbType.Int, 4, t001_idficepi_solicitante);
            aParam[i++] = ParametroSql.add("@t314_idusuario_interesado", SqlDbType.Int, 4, t314_idusuario_interesado);
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, (t423_idmotivo == 1) ? (int?)t305_idproyectosubnodo : null);
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);
            aParam[i++] = ParametroSql.add("@t420_comentarionota", SqlDbType.Text, 16, t420_comentarionota);
            aParam[i++] = ParametroSql.add("@t420_anotaciones", SqlDbType.Text, 16, t420_anotaciones);
            aParam[i++] = ParametroSql.add("@t420_importe", SqlDbType.SmallMoney, 4, t420_importe);
            aParam[i++] = ParametroSql.add("@t423_idmotivo", SqlDbType.TinyInt, 1, t423_idmotivo);
            aParam[i++] = ParametroSql.add("@t666_idacuerdogv", SqlDbType.Int, 4, t666_idacuerdogv);
            aParam[i++] = ParametroSql.add("@t313_idempresa", SqlDbType.Int, 4, t313_idempresa);
            aParam[i++] = ParametroSql.add("@t007_idterrfis", SqlDbType.TinyInt, 1, t007_idterrfis);

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("GVT_CABECERAGV_PAGO_INS", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "GVT_CABECERAGV_PAGO_INS", aParam));
        }

        public static int InsertarCabecera(SqlTransaction tr,
                string t431_idestado,
                string t420_concepto,
                int t001_idficepi_solicitante,
                int t314_idusuario_interesado,
                byte t423_idmotivo,
                bool t420_justificantes,
                Nullable<int> t305_idproyectosubnodo,
                string t422_idmoneda,
                string t420_comentarionota,
                string t420_anotaciones,
                decimal t420_importeanticipo,
                Nullable<DateTime> t420_fanticipo,
                string t420_lugaranticipo,
                decimal t420_importedevolucion,
                Nullable<DateTime> t420_fdevolucion,
                string t420_lugardevolucion,
                string t420_aclaracionesanticipo,
                decimal t420_pagadotransporte,
                decimal t420_pagadohotel,
                decimal t420_pagadootros,
                string t420_aclaracionepagado,
                int t313_idempresa,
                byte t007_idterrfis,
                decimal t420_impdico,
                decimal t420_impmdco,
                decimal t420_impalco,
                decimal t420_impkmco,
                decimal t420_impdeco,
                decimal t420_impdiex,
                decimal t420_impmdex,
                decimal t420_impalex,
                decimal t420_impkmex,
                decimal t420_impdeex,
                short t010_idoficina,
                Nullable<int> t420_idreferencia_lote
            )
        {
            SqlParameter[] aParam = new SqlParameter[35];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t431_idestado", SqlDbType.Char, 1, t431_idestado);
            aParam[i++] = ParametroSql.add("@t420_concepto", SqlDbType.VarChar, 50, t420_concepto);
            aParam[i++] = ParametroSql.add("@t001_idficepi_solicitante", SqlDbType.Int, 4, t001_idficepi_solicitante);
            aParam[i++] = ParametroSql.add("@t314_idusuario_interesado", SqlDbType.Int, 4, t314_idusuario_interesado);
            aParam[i++] = ParametroSql.add("@t423_idmotivo", SqlDbType.TinyInt, 1, t423_idmotivo);
            aParam[i++] = ParametroSql.add("@t420_justificantes", SqlDbType.Bit, 1, t420_justificantes);
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, (t423_idmotivo == 1) ? (int?)t305_idproyectosubnodo : null);
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);
            aParam[i++] = ParametroSql.add("@t420_comentarionota", SqlDbType.Text, 16, t420_comentarionota);
            aParam[i++] = ParametroSql.add("@t420_anotaciones", SqlDbType.Text, 16, t420_anotaciones);
            aParam[i++] = ParametroSql.add("@t420_importeanticipo", SqlDbType.Money, 8, t420_importeanticipo);
            aParam[i++] = ParametroSql.add("@t420_fanticipo", SqlDbType.SmallDateTime, 4, (t420_fanticipo.HasValue) ? (DateTime?)t420_fanticipo : null);
            aParam[i++] = ParametroSql.add("@t420_lugaranticipo", SqlDbType.VarChar, 50, t420_lugaranticipo);
            aParam[i++] = ParametroSql.add("@t420_importedevolucion", SqlDbType.Money, 8, t420_importedevolucion);
            aParam[i++] = ParametroSql.add("@t420_fdevolucion", SqlDbType.SmallDateTime, 4, (t420_fdevolucion.HasValue) ? (DateTime?)t420_fdevolucion : null);
            aParam[i++] = ParametroSql.add("@t420_lugardevolucion", SqlDbType.VarChar, 50, t420_lugardevolucion);
            aParam[i++] = ParametroSql.add("@t420_aclaracionesanticipo", SqlDbType.Text, 16, t420_aclaracionesanticipo);
            aParam[i++] = ParametroSql.add("@t420_pagadotransporte", SqlDbType.Money, 8, t420_pagadotransporte);
            aParam[i++] = ParametroSql.add("@t420_pagadohotel", SqlDbType.Money, 8, t420_pagadohotel);
            aParam[i++] = ParametroSql.add("@t420_pagadootros", SqlDbType.Money, 8, t420_pagadootros);
            aParam[i++] = ParametroSql.add("@t420_aclaracionepagado", SqlDbType.Text, 16, t420_aclaracionepagado);
            aParam[i++] = ParametroSql.add("@t313_idempresa", SqlDbType.Int, 4, t313_idempresa);
            aParam[i++] = ParametroSql.add("@t007_idterrfis", SqlDbType.TinyInt, 1, t007_idterrfis);
            aParam[i++] = ParametroSql.add("@t420_impdico", SqlDbType.SmallMoney, 4, t420_impdico);
            aParam[i++] = ParametroSql.add("@t420_impmdco", SqlDbType.SmallMoney, 4, t420_impmdco);
            aParam[i++] = ParametroSql.add("@t420_impalco", SqlDbType.SmallMoney, 4, t420_impalco);
            aParam[i++] = ParametroSql.add("@t420_impkmco", SqlDbType.SmallMoney, 4, t420_impkmco);
            aParam[i++] = ParametroSql.add("@t420_impdeco", SqlDbType.SmallMoney, 4, t420_impdeco);
            aParam[i++] = ParametroSql.add("@t420_impdiex", SqlDbType.SmallMoney, 4, t420_impdiex);
            aParam[i++] = ParametroSql.add("@t420_impmdex", SqlDbType.SmallMoney, 4, t420_impmdex);
            aParam[i++] = ParametroSql.add("@t420_impalex", SqlDbType.SmallMoney, 4, t420_impalex);
            aParam[i++] = ParametroSql.add("@t420_impkmex", SqlDbType.SmallMoney, 4, t420_impkmex);
            aParam[i++] = ParametroSql.add("@t420_impdeex", SqlDbType.SmallMoney, 4, t420_impdeex);
            aParam[i++] = ParametroSql.add("@t010_idoficina", SqlDbType.SmallInt, 2, t010_idoficina);
            aParam[i++] = ParametroSql.add("@t420_idreferencia_lote", SqlDbType.Int, 4, t420_idreferencia_lote);

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("GVT_CABECERAGV_INS", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "GVT_CABECERAGV_INS", aParam));
        }

        public static int ModificarCabeceraPago(SqlTransaction tr, int t420_idreferencia, string t431_idestado, string t420_concepto, int t001_idficepi_solicitante,
                                            int t314_idusuario_interesado, Nullable<int> t305_idproyectosubnodo,
                                            string t422_idmoneda, string t420_comentarionota, string t420_anotaciones, decimal t420_importe,
                                            byte t423_idmotivo, Nullable<int> t666_idacuerdogv, int t313_idempresa, byte t007_idterrfis)
        {
            SqlParameter[] aParam = new SqlParameter[14];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t420_idreferencia", SqlDbType.Int, 4, t420_idreferencia);
            aParam[i++] = ParametroSql.add("@t431_idestado", SqlDbType.Char, 1, t431_idestado);
            aParam[i++] = ParametroSql.add("@t420_concepto", SqlDbType.VarChar, 50, t420_concepto);
            aParam[i++] = ParametroSql.add("@t001_idficepi_solicitante", SqlDbType.Int, 4, t001_idficepi_solicitante);
            aParam[i++] = ParametroSql.add("@t314_idusuario_interesado", SqlDbType.Int, 4, t314_idusuario_interesado);
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, (t423_idmotivo == 1) ? (int?)t305_idproyectosubnodo : null);
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);
            aParam[i++] = ParametroSql.add("@t420_comentarionota", SqlDbType.Text, 16, t420_comentarionota);
            aParam[i++] = ParametroSql.add("@t420_anotaciones", SqlDbType.Text, 16, t420_anotaciones);
            aParam[i++] = ParametroSql.add("@t420_importe", SqlDbType.Money, 8, t420_importe);
            aParam[i++] = ParametroSql.add("@t423_idmotivo", SqlDbType.TinyInt, 1, t423_idmotivo);
            aParam[i++] = ParametroSql.add("@t666_idacuerdogv", SqlDbType.Int, 4, t666_idacuerdogv);
            aParam[i++] = ParametroSql.add("@t313_idempresa", SqlDbType.Int, 4, t313_idempresa);
            aParam[i++] = ParametroSql.add("@t007_idterrfis", SqlDbType.TinyInt, 1, t007_idterrfis);

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("GVT_CABECERAGV_PAGO_UPD", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_CABECERAGV_PAGO_UPD", aParam);

        }


        public static int ModificarCabecera(SqlTransaction tr,
                int t420_idreferencia,
                string t431_idestado,
                string t420_concepto,
                int t001_idficepi_solicitante,
                int t314_idusuario_interesado,
                byte t423_idmotivo,
                bool t420_justificantes,
                Nullable<int> t305_idproyectosubnodo,
                string t422_idmoneda,
                string t420_comentarionota,
                string t420_anotaciones,
                decimal t420_importeanticipo,
                Nullable<DateTime> t420_fanticipo,
                string t420_lugaranticipo,
                decimal t420_importedevolucion,
                Nullable<DateTime> t420_fdevolucion,
                string t420_lugardevolucion,
                string t420_aclaracionesanticipo,
                decimal t420_pagadotransporte,
                decimal t420_pagadohotel,
                decimal t420_pagadootros,
                string t420_aclaracionepagado,
                int t313_idempresa,
                byte t007_idterrfis,
                decimal t420_impdico,
                decimal t420_impmdco,
                decimal t420_impalco,
                decimal t420_impkmco,
                decimal t420_impdeco,
                decimal t420_impdiex,
                decimal t420_impmdex,
                decimal t420_impalex,
                decimal t420_impkmex,
                decimal t420_impdeex,
                short t010_idoficina
            )
        {
            SqlParameter[] aParam = new SqlParameter[35];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t420_idreferencia", SqlDbType.Int, 4, t420_idreferencia);
            aParam[1] = ParametroSql.add("@t431_idestado", SqlDbType.Char, 1, t431_idestado);
            aParam[2] = ParametroSql.add("@t420_concepto", SqlDbType.VarChar, 50, t420_concepto);
            aParam[3] = ParametroSql.add("@t001_idficepi_solicitante", SqlDbType.Int, 4, t001_idficepi_solicitante);
            aParam[4] = ParametroSql.add("@t314_idusuario_interesado", SqlDbType.Int, 4, t314_idusuario_interesado);
            aParam[5] = ParametroSql.add("@t423_idmotivo", SqlDbType.TinyInt, 1, t423_idmotivo);
            aParam[6] = ParametroSql.add("@t420_justificantes", SqlDbType.Bit, 1, t420_justificantes);
            aParam[7] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, (t423_idmotivo == 1) ? (int?)t305_idproyectosubnodo : null);
            aParam[8] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);
            aParam[9] = ParametroSql.add("@t420_comentarionota", SqlDbType.Text, 16, t420_comentarionota);
            aParam[10] = ParametroSql.add("@t420_anotaciones", SqlDbType.Text, 16, t420_anotaciones);
            aParam[11] = ParametroSql.add("@t420_importeanticipo", SqlDbType.Money, 8, t420_importeanticipo);
            aParam[12] = ParametroSql.add("@t420_fanticipo", SqlDbType.SmallDateTime, 4, (t420_fanticipo.HasValue) ? (DateTime?)t420_fanticipo : null);
            aParam[13] = ParametroSql.add("@t420_lugaranticipo", SqlDbType.VarChar, 50, t420_lugaranticipo);
            aParam[14] = ParametroSql.add("@t420_importedevolucion", SqlDbType.Money, 8, t420_importedevolucion);
            aParam[15] = ParametroSql.add("@t420_fdevolucion", SqlDbType.SmallDateTime, 4, (t420_fdevolucion.HasValue) ? (DateTime?)t420_fdevolucion : null);
            aParam[16] = ParametroSql.add("@t420_lugardevolucion", SqlDbType.VarChar, 50, t420_lugardevolucion);
            aParam[17] = ParametroSql.add("@t420_aclaracionesanticipo", SqlDbType.Text, 16, t420_aclaracionesanticipo);
            aParam[18] = ParametroSql.add("@t420_pagadotransporte", SqlDbType.Money, 8, t420_pagadotransporte);
            aParam[19] = ParametroSql.add("@t420_pagadohotel", SqlDbType.Money, 8, t420_pagadohotel);
            aParam[20] = ParametroSql.add("@t420_pagadootros", SqlDbType.Money, 8, t420_pagadootros);
            aParam[21] = ParametroSql.add("@t420_aclaracionepagado", SqlDbType.Text, 16, t420_aclaracionepagado);
            aParam[22] = ParametroSql.add("@t313_idempresa", SqlDbType.Int, 4, t313_idempresa);
            aParam[23] = ParametroSql.add("@t007_idterrfis", SqlDbType.TinyInt, 1, t007_idterrfis);
            aParam[24] = ParametroSql.add("@t420_impdico", SqlDbType.SmallMoney, 4, t420_impdico);
            aParam[25] = ParametroSql.add("@t420_impmdco", SqlDbType.SmallMoney, 4, t420_impmdco);
            aParam[26] = ParametroSql.add("@t420_impalco", SqlDbType.SmallMoney, 4, t420_impalco);
            aParam[27] = ParametroSql.add("@t420_impkmco", SqlDbType.SmallMoney, 4, t420_impkmco);
            aParam[28] = ParametroSql.add("@t420_impdeco", SqlDbType.SmallMoney, 4, t420_impdeco);
            aParam[29] = ParametroSql.add("@t420_impdiex", SqlDbType.SmallMoney, 4, t420_impdiex);
            aParam[30] = ParametroSql.add("@t420_impmdex", SqlDbType.SmallMoney, 4, t420_impmdex);
            aParam[31] = ParametroSql.add("@t420_impalex", SqlDbType.SmallMoney, 4, t420_impalex);
            aParam[32] = ParametroSql.add("@t420_impkmex", SqlDbType.SmallMoney, 4, t420_impkmex);
            aParam[33] = ParametroSql.add("@t420_impdeex", SqlDbType.SmallMoney, 4, t420_impdeex);
            aParam[34] = ParametroSql.add("@t010_idoficina", SqlDbType.SmallInt, 2, t010_idoficina);

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("GVT_CABECERAGV_UPD", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_CABECERAGV_UPD", aParam);

        }

        public static void UpdateAcuerdo(SqlTransaction tr, int t420_idreferencia, int t666_idacuerdogv)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0; 
            aParam[i++] = ParametroSql.add("@t420_idreferencia", SqlDbType.Int, 4, t420_idreferencia);
            aParam[i++] = ParametroSql.add("@t666_idacuerdogv", SqlDbType.Int, 4, t666_idacuerdogv);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("GVT_CABECERAGV_UPD_ACUERDO", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_CABECERAGV_UPD_ACUERDO", aParam);
        }
        public static void UpdateLote(SqlTransaction tr, int t420_idreferencia, int t420_idreferencia_lote)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t420_idreferencia", SqlDbType.Int, 4, t420_idreferencia);
            aParam[i++] = ParametroSql.add("@t420_idreferencia_lote", SqlDbType.Int, 4, t420_idreferencia_lote);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("GVT_CABECERAGV_UPD_LOTE", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_CABECERAGV_UPD_LOTE", aParam);
        }

        public static void UpdateCentroCoste(SqlTransaction tr, int t420_idreferencia, string t317_idcencos)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t420_idreferencia", SqlDbType.Int, 4, t420_idreferencia);
            aParam[i++] = ParametroSql.add("@t317_idcencos", SqlDbType.Char, 4, t317_idcencos);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("GVT_CABECERAGV_UPD_CC", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_CABECERAGV_UPD_CC", aParam);
        }

        public static SqlDataReader ObtenerNotasAbiertasYRecientes(SqlTransaction tr, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("GVT_NOTASABIERTASYRECIENTES", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_NOTASABIERTASYRECIENTES", aParam);
        }

        public static int RecuperarNotaEstandar(SqlTransaction tr, int t420_idreferencia)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t420_idreferencia", SqlDbType.Int, 4, t420_idreferencia);

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("GVT_RECUPERAR_GV", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_RECUPERAR_GV", aParam);
        }

        public static int RecuperarBono(SqlTransaction tr, int t420_idreferencia)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t420_idreferencia", SqlDbType.Int, 4, t420_idreferencia);

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("GVT_RECUPERAR_BONO", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_RECUPERAR_BONO", aParam);
        }

        public static int RecuperarPago(SqlTransaction tr, int t420_idreferencia)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t420_idreferencia", SqlDbType.Int, 4, t420_idreferencia);

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("GVT_RECUPERAR_PAGO", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_RECUPERAR_PAGO", aParam);
        }

        public static SqlDataReader ObtenerHistorial(SqlTransaction tr, int t420_idreferencia)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t420_idreferencia", SqlDbType.Int, 4, t420_idreferencia);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("GVT_HISTORIAL", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_HISTORIAL", aParam);
        }

        public static void GestionarAutorresponsabilidad(SqlTransaction tr, int t420_idreferencia)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t420_idreferencia", SqlDbType.Int, 4, t420_idreferencia);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("GVT_AUTORRESPONSABILIDAD_APROBAR", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_AUTORRESPONSABILIDAD_APROBAR", aParam);
        }

        public static SqlDataReader ObtenerNotasPendientesAprobar(SqlTransaction tr, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("GVT_NOTASPENDIENTES_APROBAR", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_NOTASPENDIENTES_APROBAR", aParam);
        }

        public static SqlDataReader ObtenerNotasPendientesAceptar(SqlTransaction tr, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("GVT_NOTASPENDIENTES_ACEPTAR", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_NOTASPENDIENTES_ACEPTAR", aParam);
        }

        public static void Aprobar(SqlTransaction tr, string sReferencias)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@sReferencias", SqlDbType.VarChar, 4000, sReferencias);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("GVT_APROBAR", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_APROBAR", aParam);
        }
        public static int NoAprobar(SqlTransaction tr, int t420_idreferencia, string t659_motivo)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t420_idreferencia", SqlDbType.Int, 4, t420_idreferencia);
            aParam[i++] = ParametroSql.add("@t659_motivo", SqlDbType.VarChar, 500, t659_motivo);

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("GVT_NOAPROBAR", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_NOAPROBAR", aParam);
        }

        public static void Aceptar(SqlTransaction tr, string sReferenciasYFechas)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@sReferenciasYFechas", SqlDbType.VarChar, 8000, sReferenciasYFechas);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("GVT_ACEPTAR", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_ACEPTAR", aParam);
        }
        public static int NoAceptar(SqlTransaction tr, int t420_idreferencia, string t659_motivo)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t420_idreferencia", SqlDbType.Int, 4, t420_idreferencia);
            aParam[i++] = ParametroSql.add("@t659_motivo", SqlDbType.VarChar, 500, t659_motivo);

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("GVT_NOACEPTAR", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_NOACEPTAR", aParam);
        }

        public static int Anular(SqlTransaction tr, int t420_idreferencia, string t659_motivo)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t420_idreferencia", SqlDbType.Int, 4, t420_idreferencia);
            aParam[i++] = ParametroSql.add("@t659_motivo", SqlDbType.VarChar, 500, t659_motivo);

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("GVT_ANULAR", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_ANULAR", aParam);
        }

        public static string ObtenerCentroCoste(SqlTransaction tr, int t420_idreferencia)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t420_idreferencia", SqlDbType.Int, 4, t420_idreferencia);

            if (tr == null)
                return SqlHelper.ExecuteScalar("GVT_GETCENTROCOSTE", aParam).ToString().Trim();
            else
                return SqlHelper.ExecuteScalarTransaccion(tr, "GVT_GETCENTROCOSTE", aParam).ToString().Trim();
        }

        ////////////////////////////////Nuevo Pago Concertado////////////////////////////////
        public static SqlDataReader CatalogoOtrosPagos(int t314_idusuario_interesado)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t314_idusuario_interesado", SqlDbType.Int, 4, t314_idusuario_interesado);
            // Ejecuta la query y devuelve un string con el resultado
            return SqlHelper.ExecuteSqlDataReader("GVT_CABECERAGV_PAGO_CAT", aParam);
        }

        public static SqlDataReader ObtenerNotasDeUnLote(SqlTransaction tr, int t420_idreferencia_lote)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t420_idreferencia_lote", SqlDbType.Int, 4, t420_idreferencia_lote);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("GVT_GASTOSNOTASLOTE", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_GASTOSNOTASLOTE", aParam);
        }

        #endregion
    }
}
