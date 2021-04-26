using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Data.SqlClient;

namespace GASVI.DAL
{
    public partial class BonoTransporte
    {
        public static SqlDataReader ObtenerBonos(string t655_estado)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t655_estado", SqlDbType.Char, 1, t655_estado);
            // Ejecuta la query y devuelve un string con el resultado
            return SqlHelper.ExecuteSqlDataReader("GVT_BONO_TRANSPORTE_CAT", aParam);
        }

        public static SqlDataReader ObtenerOficinas()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            // Ejecuta la query y devuelve un string con el resultado
            return SqlHelper.ExecuteSqlDataReader("GVT_OFICINAS_CAT", aParam);
        }
        
        public static SqlDataReader SelectImportes(int t655_idBono)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t655_idBono", SqlDbType.Int, 4, t655_idBono);
            
            // Ejecuta la query y devuelve un string con el resultado
            return SqlHelper.ExecuteSqlDataReader("GVT_BONO_IMPORTE_SEL", aParam);
        }

        public static SqlDataReader SelectBonoOficinas(int t655_idBono)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t655_idBono", SqlDbType.Int, 4, t655_idBono);

            // Ejecuta la query y devuelve un string con el resultado
            return SqlHelper.ExecuteSqlDataReader("GVT_BONO_OFICINA_SEL", aParam);
        }

        public static int DeleteBonoTransporte(SqlTransaction tr, int t655_idBono)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t655_idBono", SqlDbType.Int, 4, t655_idBono);

            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteNonQuery("GVT_BONO_TRANSPORTE_DEL", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_BONO_TRANSPORTE_DEL", aParam));
        }

        public static int InsertBonoTransporte(SqlTransaction tr, string t655_denominacion, string t655_estado, string t655_descripcion, string t422_idmoneda)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t655_denominacion", SqlDbType.Text, 16, t655_denominacion); 
            aParam[i++] = ParametroSql.add("@t655_estado", SqlDbType.Char, 1, t655_estado);
            aParam[i++] = ParametroSql.add("@t655_descripcion", SqlDbType.VarChar, 25, t655_descripcion);
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);

            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("GVT_BONO_TRANSPORTE_INS", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "GVT_BONO_TRANSPORTE_INS", aParam));
        }

        public static void UpdateBonoTransporte(SqlTransaction tr, int t655_idBono, string t655_denominacion, string t655_estado, string t655_descripcion, string t422_idmoneda)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t655_idBono", SqlDbType.Int, 4, t655_idBono);
            aParam[i++] = ParametroSql.add("@t655_denominacion", SqlDbType.VarChar, 25, t655_denominacion);
            aParam[i++] = ParametroSql.add("@t655_estado", SqlDbType.Char, 1, t655_estado);
            aParam[i++] = ParametroSql.add("@t655_descripcion", SqlDbType.VarChar, 100, t655_descripcion);
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);


            if (tr == null)
                SqlHelper.ExecuteNonQuery("GVT_BONO_TRANSPORTE_UPD", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_BONO_TRANSPORTE_UPD", aParam);
        }

        public static void DeleteBonoOficina(SqlTransaction tr, int t657_idBonoOficina)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t657_idBonoOficina", SqlDbType.SmallInt, 2, t657_idBonoOficina);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("GVT_BONO_OFICINA_DEL", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_BONO_OFICINA_DEL", aParam);
        }

        public static int InsertBonoOficina(SqlTransaction tr, int t655_idBono, int t010_idoficina,
                                                int t657_desde, int t657_hasta)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t655_idBono", SqlDbType.Int, 4, t655_idBono);
            aParam[i++] = ParametroSql.add("@t010_idoficina", SqlDbType.SmallInt, 2, t010_idoficina);
            aParam[i++] = ParametroSql.add("@t657_desde", SqlDbType.Int, 4, t657_desde);
            aParam[i++] = ParametroSql.add("@t657_hasta", SqlDbType.Int, 4, t657_hasta);

            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("GVT_BONO_OFICINA_INS", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "GVT_BONO_OFICINA_INS", aParam));
        }

        public static void UpdateBonoOficina(SqlTransaction tr, int t655_idBono, int t657_idBonoOficina, int t010_idoficina,
                                                int t657_desde, int t657_hasta)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t657_idBonoOficina", SqlDbType.SmallInt, 2, t657_idBonoOficina);
            aParam[i++] = ParametroSql.add("@t655_idBono", SqlDbType.Int, 4, t655_idBono);
            aParam[i++] = ParametroSql.add("@t010_idoficina", SqlDbType.SmallInt, 2, t010_idoficina);
            aParam[i++] = ParametroSql.add("@t657_desde", SqlDbType.Int, 4, t657_desde);
            aParam[i++] = ParametroSql.add("@t657_hasta", SqlDbType.Int, 4, t657_hasta);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("GVT_BONO_OFICINA_UPD", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_BONO_OFICINA_UPD", aParam);
        }

        public static void DeleteBonoImporte(SqlTransaction tr, int t656_idImporte)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t656_idImporte", SqlDbType.SmallInt, 2, t656_idImporte);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("GVT_BONO_IMPORTE_DEL", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_BONO_IMPORTE_DEL", aParam);
        }

        public static int InsertBonoImporte(SqlTransaction tr, int t655_idBono, decimal t656_importe,
                                                int t656_desde, int t656_hasta)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t655_idBono", SqlDbType.Int, 4, t655_idBono);
            aParam[i++] = ParametroSql.add("@t656_importe", SqlDbType.SmallMoney, 4, t656_importe);
            aParam[i++] = ParametroSql.add("@t656_desde", SqlDbType.Int, 4, t656_desde);
            aParam[i++] = ParametroSql.add("@t656_hasta", SqlDbType.Int, 4, t656_hasta);

            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("GVT_BONO_IMPORTE_INS", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "GVT_BONO_IMPORTE_INS", aParam));
        }

        public static void UpdateBonoImporte(SqlTransaction tr, int t655_idBono, int t656_idImporte, decimal t656_importe,
                                                int t656_desde, int t656_hasta)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t656_idImporte", SqlDbType.SmallInt, 2, t656_idImporte);
            aParam[i++] = ParametroSql.add("@t655_idBono", SqlDbType.Int, 4, t655_idBono);
            aParam[i++] = ParametroSql.add("@t656_importe", SqlDbType.SmallMoney, 4, t656_importe);
            aParam[i++] = ParametroSql.add("@t656_desde", SqlDbType.Int, 4, t656_desde);
            aParam[i++] = ParametroSql.add("@t656_hasta", SqlDbType.Int, 4, t656_hasta);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("GVT_BONO_IMPORTE_UPD", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_BONO_IMPORTE_UPD", aParam);
        }

        public static int FechasImporte(SqlTransaction tr,int t656_idImporte, int t655_idBono, int t656_desde, int t656_hasta)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t656_idImporte", SqlDbType.SmallInt, 2, t656_idImporte);
            aParam[i++] = ParametroSql.add("@t655_idBono", SqlDbType.Int, 4, t655_idBono);
            aParam[i++] = ParametroSql.add("@t656_desde", SqlDbType.Int, 4, t656_desde);
            aParam[i++] = ParametroSql.add("@t656_hasta", SqlDbType.Int, 4, t656_hasta);

            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("GVT_FECHAS_BONO_IMPORTE", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "GVT_FECHAS_BONO_IMPORTE", aParam)); 
        }

        public static int FechasOficinas(SqlTransaction tr, int t657_idBonoOficina, int t655_idBono, int t657_desde, int t657_hasta, int t010_idoficina)
        {

            SqlParameter[] aParam = new SqlParameter[5];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t657_idBonoOficina", SqlDbType.SmallInt, 2, t657_idBonoOficina);
            aParam[i++] = ParametroSql.add("@t655_idBono", SqlDbType.Int, 4, t655_idBono);
            aParam[i++] = ParametroSql.add("@t657_desde", SqlDbType.Int, 4, t657_desde);
            aParam[i++] = ParametroSql.add("@t657_hasta", SqlDbType.Int, 4, t657_hasta);
            aParam[i++] = ParametroSql.add("@t010_idoficina", SqlDbType.SmallInt, 2, t010_idoficina);

            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("GVT_FECHAS_BONO_OFICINA", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "GVT_FECHAS_BONO_OFICINA", aParam));
        }


        ///////////////////// Gasto Viaje Bono Transporte/////////////////////////////

        public static SqlDataReader CatalogoBonosUsuarioProyecto(int t314_idusuario, int fecha)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);
            aParam[i++] = ParametroSql.add("@fecha", SqlDbType.Int, 4, fecha);

            // Ejecuta la query y devuelve un string con el resultado
            return SqlHelper.ExecuteSqlDataReader("GVT_BONO_CAT_USUPROYEC", aParam);
        }

        public static SqlDataReader CatalogoCabeceraGVBono(int t314_idusuario, int t420_idreferencia, int t420_anomesbono)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);
            aParam[i++] = ParametroSql.add("@t420_idreferencia", SqlDbType.Int, 4, t420_idreferencia);
            aParam[i++] = ParametroSql.add("@t420_anomesbono", SqlDbType.Int, 4, t420_anomesbono);

            // Ejecuta la query y devuelve un string con el resultado
            return SqlHelper.ExecuteSqlDataReader("GVT_CABECERAGV_BONO_CAT", aParam);
        }

        public static int InsertCabeceraGVBono(SqlTransaction tr, string t420_concepto, int t001_idficepi_solicitante,
                                                int t314_idusuario_interesado, int t305_idproyectosubnodo, string t420_comentarionota,
                                                string t420_anotaciones, int t313_idempresa, int t655_idBono, float t420_importe,
                                                int t420_anomesbono, int t007_idterrfis, string t422_idmoneda, short t010_idoficina)
        {
            SqlParameter[] aParam = new SqlParameter[13];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t420_concepto", SqlDbType.VarChar, 50, t420_concepto);
            aParam[i++] = ParametroSql.add("@t001_idficepi_solicitante", SqlDbType.Int, 4, t001_idficepi_solicitante);
            aParam[i++] = ParametroSql.add("@t314_idusuario_interesado", SqlDbType.Int, 4, t314_idusuario_interesado);
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);
            aParam[i++] = ParametroSql.add("@t420_comentarionota", SqlDbType.Text, 16, t420_comentarionota);
            aParam[i++] = ParametroSql.add("@t420_anotaciones", SqlDbType.Text, 16, t420_anotaciones);
            aParam[i++] = ParametroSql.add("@t313_idempresa", SqlDbType.Int, 4, t313_idempresa);
            aParam[i++] = ParametroSql.add("@t655_idBono", SqlDbType.Int, 4, t655_idBono);
            aParam[i++] = ParametroSql.add("@t420_importe", SqlDbType.SmallMoney, 4, t420_importe);
            aParam[i++] = ParametroSql.add("@t420_anomesbono", SqlDbType.Int, 4, t420_anomesbono);
            aParam[i++] = ParametroSql.add("@t007_idterrfis", SqlDbType.Int, 4, t007_idterrfis);
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);
            aParam[i++] = ParametroSql.add("@t010_idoficina", SqlDbType.SmallInt, 2, t010_idoficina);

            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("GVT_CABECERAGV_BONO_INS", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "GVT_CABECERAGV_BONO_INS", aParam));
        }

        public static void UpdateCabeceraGVBono(SqlTransaction tr, int t420_idreferencia, string t420_concepto, int t001_idficepi_solicitante,
                                                int t314_idusuario_interesado, int t305_idproyectosubnodo, string t420_comentarionota,
                                                string t420_anotaciones, int t313_idempresa, int t655_idBono, float t420_importe,
                                                int t420_anomesbono, int t007_idterrfis, string t422_idmoneda, short t010_idoficina)
        {
            SqlParameter[] aParam = new SqlParameter[14];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t420_idreferencia", SqlDbType.Int, 4, t420_idreferencia);
            aParam[i++] = ParametroSql.add("@t420_concepto", SqlDbType.VarChar, 50, t420_concepto);
            aParam[i++] = ParametroSql.add("@t001_idficepi_solicitante", SqlDbType.Int, 4, t001_idficepi_solicitante);
            aParam[i++] = ParametroSql.add("@t314_idusuario_interesado", SqlDbType.Int, 4, t314_idusuario_interesado);
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);
            aParam[i++] = ParametroSql.add("@t420_comentarionota", SqlDbType.Text, 16, t420_comentarionota);
            aParam[i++] = ParametroSql.add("@t420_anotaciones", SqlDbType.Text, 16, t420_anotaciones);
            aParam[i++] = ParametroSql.add("@t313_idempresa", SqlDbType.Int, 4, t313_idempresa);
            aParam[i++] = ParametroSql.add("@t655_idBono", SqlDbType.Int, 4, t655_idBono);
            aParam[i++] = ParametroSql.add("@t420_importe", SqlDbType.SmallMoney, 4, t420_importe);
            aParam[i++] = ParametroSql.add("@t420_anomesbono", SqlDbType.Int, 4, t420_anomesbono);
            aParam[i++] = ParametroSql.add("@t007_idterrfis", SqlDbType.Int, 4, t007_idterrfis);
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);
            aParam[i++] = ParametroSql.add("@t010_idoficina", SqlDbType.SmallInt, 2, t010_idoficina);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("GVT_CABECERAGV_BONO_UPD", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_CABECERAGV_BONO_UPD", aParam);
        }
	}
}