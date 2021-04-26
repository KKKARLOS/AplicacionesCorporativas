using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace GASVI.DAL
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

        public static SqlDataReader ObtenerDatosCambioEstado(SqlTransaction tr, int t420_idreferencia)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t420_idreferencia", SqlDbType.Int, 4, t420_idreferencia);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("GVT_CAMBIOESTADO_SEL", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_CAMBIOESTADO_SEL", aParam);
        }


        public static int InsertarCabeceraPago(SqlTransaction tr, string t431_idestado, string t420_concepto, int t001_idficepi_solicitante,
                                            int t314_idusuario_interesado, Nullable<int> t305_idproyectosubnodo,
                                            string t422_idmoneda, string t420_comentarionota, string t420_anotaciones,decimal t420_importe,
                                            byte t423_idmotivo, Nullable<int> t666_idacuerdogv, int t313_idempresa, byte t007_idterrfis,
                                            string t175_idcc, short t010_idoficina)
        {
            SqlParameter[] aParam = new SqlParameter[15];
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
            aParam[i++] = ParametroSql.add("@t175_idcc", SqlDbType.Char, 4, t175_idcc);
            aParam[i++] = ParametroSql.add("@t010_idoficina", SqlDbType.SmallInt, 2, t010_idoficina);

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
                Nullable<int> t420_idreferencia_lote,
                string t175_idcc
            )
        {
            SqlParameter[] aParam = new SqlParameter[36];
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
            aParam[i++] = ParametroSql.add("@t175_idcc", SqlDbType.Char, 4, t175_idcc);
            
            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("GVT_CABECERAGV_INS", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "GVT_CABECERAGV_INS", aParam));
        }

        public static int ModificarCabeceraPago(SqlTransaction tr, int t420_idreferencia, string t431_idestado, string t420_concepto, int t001_idficepi_solicitante,
                                            int t314_idusuario_interesado, Nullable<int> t305_idproyectosubnodo,
                                            string t422_idmoneda, string t420_comentarionota, string t420_anotaciones, decimal t420_importe,
                                            byte t423_idmotivo, Nullable<int> t666_idacuerdogv, int t313_idempresa, byte t007_idterrfis,
                                            string t175_idcc, short t010_idoficina)
        {
            SqlParameter[] aParam = new SqlParameter[16];
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
            aParam[i++] = ParametroSql.add("@t175_idcc", SqlDbType.Char, 4, t175_idcc);
            aParam[i++] = ParametroSql.add("@t010_idoficina", SqlDbType.SmallInt, 2, t010_idoficina);

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
                short t010_idoficina,
                string t175_idcc
            )
        {
            SqlParameter[] aParam = new SqlParameter[36];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t420_idreferencia", SqlDbType.Int, 4, t420_idreferencia);
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
            aParam[i++] = ParametroSql.add("@t175_idcc", SqlDbType.Char, 4, t175_idcc);

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

        public static void UpdateCentroCoste(SqlTransaction tr, int t420_idreferencia, string t175_idcc)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t420_idreferencia", SqlDbType.Int, 4, t420_idreferencia);
            aParam[i++] = ParametroSql.add("@t175_idcc", SqlDbType.Char, 4, t175_idcc);

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

        public static DataSet Aprobar(SqlTransaction tr, string sReferencias)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@sReferencias", SqlDbType.VarChar, 8000, sReferencias);

            if (tr == null)
                return SqlHelper.ExecuteDataset("GVT_APROBAR", aParam);
            else
                return SqlHelper.ExecuteDatasetTransaccion(tr, "GVT_APROBAR", aParam);
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

        public static void Aceptar(SqlTransaction tr, string sReferenciasYDatos)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@sReferenciasYDatos", SqlDbType.VarChar, 8000, sReferenciasYDatos);

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

        public static int AnularAdm(SqlTransaction tr, int t420_idreferencia, string t659_motivo)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t420_idreferencia", SqlDbType.Int, 4, t420_idreferencia);
            aParam[i++] = ParametroSql.add("@t659_motivo", SqlDbType.VarChar, 500, t659_motivo);

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("GVT_ANULAR_ADM", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_ANULAR_ADM", aParam);
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
        public static SqlDataReader CatalogoOtrosPagos(int t314_idusuario_interesado, int t420_idreferencia)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t314_idusuario_interesado", SqlDbType.Int, 4, t314_idusuario_interesado);
            aParam[i++] = ParametroSql.add("@t420_idreferencia", SqlDbType.Int, 4, t420_idreferencia);

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

        public static SqlDataReader ObtenerMisSolicitudes(SqlTransaction tr, int t001_idficepi,
            string sEstados,
            string sMotivos,
            int nDesde,
            int nHasta,
            string t420_concepto,
            Nullable<int> t305_idproyectosubnodo,
            Nullable<int> t420_idreferencia)
        {
            SqlParameter[] aParam = new SqlParameter[8];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            aParam[i++] = ParametroSql.add("@sEstados", SqlDbType.VarChar, 30, sEstados);
            aParam[i++] = ParametroSql.add("@sMotivos", SqlDbType.VarChar, 30, sMotivos);
            aParam[i++] = ParametroSql.add("@nDesde", SqlDbType.Int, 4, nDesde);
            aParam[i++] = ParametroSql.add("@nHasta", SqlDbType.Int, 4, nHasta);
            aParam[i++] = ParametroSql.add("@t420_concepto", SqlDbType.VarChar, 50, t420_concepto);
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);
            aParam[i++] = ParametroSql.add("@t420_idreferencia", SqlDbType.Int, 4, t420_idreferencia);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("GVT_MISSOLICITUDES", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_MISSOLICITUDES", aParam);
        }

        public static SqlDataReader ObtenerSolicitudesADM(SqlTransaction tr,
            string sEstados,
            string sMotivos,
            int nDesde,
            int nHasta,
            string t420_concepto,
            Nullable<int> t305_idproyectosubnodo,
            Nullable<int> t420_idreferencia,
            Nullable<int> t001_idficepi_aprobada,
            Nullable<int> t001_idficepi_interesado,
            Nullable<int> t303_idnodo_proyecto,
            Nullable<int> t302_idcliente_proyecto
            )
        {
            SqlParameter[] aParam = new SqlParameter[11];
            int i = 0;
            aParam[i++] = ParametroSql.add("@sEstados", SqlDbType.VarChar, 30, sEstados);
            aParam[i++] = ParametroSql.add("@sMotivos", SqlDbType.VarChar, 30, sMotivos);
            aParam[i++] = ParametroSql.add("@nDesde", SqlDbType.Int, 4, nDesde);
            aParam[i++] = ParametroSql.add("@nHasta", SqlDbType.Int, 4, nHasta);
            aParam[i++] = ParametroSql.add("@t420_concepto", SqlDbType.VarChar, 50, t420_concepto);
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);
            aParam[i++] = ParametroSql.add("@t420_idreferencia", SqlDbType.Int, 4, t420_idreferencia);
            aParam[i++] = ParametroSql.add("@t001_idficepi_aprobada", SqlDbType.Int, 4, t001_idficepi_aprobada);
            aParam[i++] = ParametroSql.add("@t001_idficepi_interesado", SqlDbType.Int, 4, t001_idficepi_interesado);
            aParam[i++] = ParametroSql.add("@t303_idnodo_proyecto", SqlDbType.Int, 4, t303_idnodo_proyecto);
            aParam[i++] = ParametroSql.add("@t302_idcliente_proyecto", SqlDbType.Int, 4, t302_idcliente_proyecto);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("GVT_SOLICITUDESADM", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_SOLICITUDESADM", aParam);
        }
        public static SqlDataReader ObtenerSolicitudesAmbito(SqlTransaction tr,
            string sOpcion,
            int t001_idficepi,
            string sMotivos,
            int nDesde,
            int nHasta,
            string t420_concepto,
            Nullable<int> t305_idproyectosubnodo,
            Nullable<int> t420_idreferencia,
            Nullable<int> t001_idficepi_interesado,
            Nullable<int> t303_idnodo_proyecto,
            Nullable<int> t001_idficepi_responsable_proyecto,
            Nullable<int> t302_idcliente_proyecto)
        {
            SqlParameter[] aParam = new SqlParameter[12];
            int i = 0;
            aParam[i++] = ParametroSql.add("@sOpcion", SqlDbType.Char, 2, sOpcion);
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            aParam[i++] = ParametroSql.add("@sMotivos", SqlDbType.VarChar, 30, sMotivos);
            aParam[i++] = ParametroSql.add("@nDesde", SqlDbType.Int, 4, nDesde);
            aParam[i++] = ParametroSql.add("@nHasta", SqlDbType.Int, 4, nHasta);
            aParam[i++] = ParametroSql.add("@t420_concepto", SqlDbType.VarChar, 50, t420_concepto);
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);
            aParam[i++] = ParametroSql.add("@t420_idreferencia", SqlDbType.Int, 4, t420_idreferencia);
            aParam[i++] = ParametroSql.add("@t001_idficepi_interesado", SqlDbType.Int, 4, t001_idficepi_interesado);
            aParam[i++] = ParametroSql.add("@t303_idnodo_proyecto", SqlDbType.Int, 4, t303_idnodo_proyecto);
            aParam[i++] = ParametroSql.add("@t001_idficepi_responsable_proyecto", SqlDbType.Int, 4, t001_idficepi_responsable_proyecto);
            aParam[i++] = ParametroSql.add("@t302_idcliente_proyecto", SqlDbType.Int, 4, t302_idcliente_proyecto);

            if (tr == null)
            {
                return SqlHelper.ExecuteSqlDataReader("GVT_SOLICITUDESAMBITO", aParam);
            }
            else
            {
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_SOLICITUDESAMBITO", aParam);
            }
        }

        public static DataSet ObtenerMiAmbito(SqlTransaction tr, int t001_idficepi,
            string sMotivos,
            int nDesde,
            int nHasta)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            aParam[i++] = ParametroSql.add("@nDesde", SqlDbType.Int, 4, nDesde);
            aParam[i++] = ParametroSql.add("@nHasta", SqlDbType.Int, 4, nHasta);
            aParam[i++] = ParametroSql.add("@sMotivos", SqlDbType.VarChar, 30, sMotivos);

            if (tr == null)
                return SqlHelper.ExecuteDataset("GVT_MIAMBITO", aParam);
            else
                return SqlHelper.ExecuteDatasetTransaccion(tr, "GVT_MIAMBITO", aParam);
        }
        
        public static SqlDataReader ObtenerDireccionesCorreo(SqlTransaction tr, int t420_idreferencia)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t420_idreferencia", SqlDbType.Int, 4, t420_idreferencia);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("GVT_GETCORREOCABECERA", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_GETCORREOCABECERA", aParam);
        }

        /////Procesos////
        public static void UpdateEstado(SqlTransaction tr, int t420_idreferencia, string t431_idestado)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t420_idreferencia", SqlDbType.Int, 4, t420_idreferencia);
            aParam[i++] = ParametroSql.add("@t431_idestado", SqlDbType.Char, 1, t431_idestado);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("GVT_CABECERAGV_UPD_ESTADO", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_CABECERAGV_UPD_ESTADO", aParam);
        }

        public static SqlDataReader ObtenerAprobadores(SqlTransaction tr, int t420_idreferencia)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t420_idreferencia", SqlDbType.Int, 4, t420_idreferencia);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("GVT_GETAPROBADORES", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_GETAPROBADORES", aParam);
        }
        public static SqlDataReader ObtenerAceptadores(SqlTransaction tr, int t420_idreferencia)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t420_idreferencia", SqlDbType.Int, 4, t420_idreferencia);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("GVT_GETACEPTADORES", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_GETACEPTADORES", aParam);
        }
        public static string ObtenerAceptadoresCODRED(SqlTransaction tr, int t420_idreferencia)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t420_idreferencia", SqlDbType.Int, 4, t420_idreferencia);

            if (tr == null)
                return SqlHelper.ExecuteScalar("GVT_GETACEPTADORES_RED", aParam).ToString();
            else
                return SqlHelper.ExecuteScalarTransaccion(tr, "GVT_GETACEPTADORES_RED", aParam).ToString();
        }

        public static SqlDataReader ObtenerCentroCosteMotivo(SqlTransaction tr, int t314_idusuario_interesado, byte t423_idmotivo, Nullable<int> t305_idproyectosubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t314_idusuario_interesado", SqlDbType.Int, 4, t314_idusuario_interesado);
            aParam[i++] = ParametroSql.add("@t423_idmotivo", SqlDbType.TinyInt, 1, t423_idmotivo);
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("GVT_GETCENTROCOSTE_MOTIVO", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_GETCENTROCOSTE_MOTIVO", aParam);
        }

        #endregion
    }
}
