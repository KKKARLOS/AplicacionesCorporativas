using System;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

/// <summary>
/// Descripción breve de FICEPIFORANEO
/// </summary>
namespace SUPER.DAL
{
    public class FICEPIFORANEO
    {
        public FICEPIFORANEO()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        public static void Updatear(SqlTransaction tr, int t001_idficepi, Nullable<int> t001_idficepi_promotor,
                                    Nullable<DateTime> t080_facep, Nullable<DateTime> t080_fultacc, string t080_passw,
                                    string t080_pregunta, string t080_respuesta)
        {
            SqlParameter[] aParam = new SqlParameter[7];
            int i = 0;

            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            aParam[i++] = ParametroSql.add("@t001_idficepi_promotor", SqlDbType.Int, 4, t001_idficepi_promotor);
            aParam[i++] = ParametroSql.add("@t080_facep", SqlDbType.SmallDateTime, 10, t080_facep);
            aParam[i++] = ParametroSql.add("@t080_fultacc", SqlDbType.SmallDateTime, 10, t080_fultacc);
            aParam[i++] = ParametroSql.add("@t080_passw", SqlDbType.Text, 50, t080_passw);
            aParam[i++] = ParametroSql.add("@t080_pregunta", SqlDbType.Text, 100, t080_pregunta);
            aParam[i++] = ParametroSql.add("@t080_respuesta", SqlDbType.Text, 50, t080_respuesta.ToUpper());

            if (tr == null)
                SqlHelper.ExecuteScalar("SUP_FICEPIFORANEO_UPD", aParam);
            else
                SqlHelper.ExecuteScalarTransaccion(tr, "SUP_FICEPIFORANEO_UPD", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T080_FICEPIFORANEO.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	29/05/2013 11:40:35
        /// </history>
        /// -----------------------------------------------------------------------------
        public static void InsertarFicepiForaneo(SqlTransaction tr, int t001_idficepi, int t001_idficepi_promotor, string t080_passw)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            aParam[i++] = ParametroSql.add("@t001_idficepi_promotor", SqlDbType.Int, 4, t001_idficepi_promotor);
            aParam[i++] = ParametroSql.add("@t080_passw", SqlDbType.Text, 50, t080_passw);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_FICEPIFORANEO_INS", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FICEPIFORANEO_INS", aParam);
        }
        /// <summary>
        /// Dado un idFicepi obtiene datos de T080_FICEPIFORANEO
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t001_idficepi"></param>
        /// <returns></returns>
        public static SqlDataReader GetDatos(SqlTransaction tr, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_FICEPIFORANEO_SEL", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_FICEPIFORANEO_SEL", aParam);
        }
        /// <summary>
        /// Incrementa el nº de intentos de acceso erróneos
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t001_idficepi"></param>
        public static void RegistrarAccesoFallido(SqlTransaction tr, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_FICEPIFORANEO_FALLO", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FICEPIFORANEO_FALLO", aParam);
        }
        public static void RegistrarAccesoCorrecto(SqlTransaction tr, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_FICEPIFORANEO_OK", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_FICEPIFORANEO_OK", aParam);
        }

    }
}
