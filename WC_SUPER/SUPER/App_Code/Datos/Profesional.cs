using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using SUPER.Capa_Datos;

/// <summary>
/// Descripción breve de Profesional
/// </summary>
namespace SUPER.DAL
{

    public class Profesional
    {
        public Profesional()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        public static SqlDataReader ObtenerFotoConversacion(int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_FOTOCONVERSACION_S", aParam);
        }

        /// <summary>
        /// Obtiene el nombre completo de un profesional en formato nombre ape1 ape2
        /// </summary>
        /// <param name="t001_idficepi"></param>
        public static string GetNombre(int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            string sRes = "";
            object obj = SqlHelper.ExecuteScalar("FIC_NOMBRE_S", aParam);
            if (obj != null)
                sRes = obj.ToString();
            return sRes;
        }

        /// <summary>
        /// Obtiene el nombre completo de un profesional en formato ape1 ape2, nombre
        /// </summary>
        /// <param name="t001_idficepi"></param>
        public static string GetNombreSuper(int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            string sRes = "";
            object obj = SqlHelper.ExecuteScalar("SUP_PROFESIONAL_NOMBRE_S", aParam);
            if (obj != null)
                sRes = obj.ToString();
            return sRes;
        }

        /// <summary>
        /// Obtiene el código de red de un profesional. En caso de ser vacío devuelve su email
        /// </summary>
        /// <param name="t001_idficepi"></param>
        public static string GetCuentaCorreo(int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi)
            };
            string sRes = "";
            object obj = SqlHelper.ExecuteScalar("SUP_GET_CTACORREO", aParam);
            if (obj != null)
                sRes = obj.ToString();
            return sRes;
        }

        public static SqlDataReader CatalogoProfesionales(SqlTransaction tr, string nombre, string apellido1, string apellido2, Nullable<int> t001_cvexclusion)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            int i = 0;
            aParam[i++] = ParametroSql.add("@nombre", SqlDbType.VarChar, 20, nombre);
            aParam[i++] = ParametroSql.add("@apellido1", SqlDbType.VarChar, 25, apellido1);
            aParam[i++] = ParametroSql.add("@apellido2", SqlDbType.VarChar, 25, apellido2);
            aParam[i++] = ParametroSql.add("@t001_cvexclusion", SqlDbType.Int, 4, t001_cvexclusion);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_PROFESIONAL_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_PROFESIONAL_CAT", aParam);
        }

        public static void Update(SqlTransaction tr, int t001_idficepi, Nullable<int> t001_cvexclusion)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            aParam[i++] = ParametroSql.add("@t001_cvexclusion", SqlDbType.Int, 4, t001_cvexclusion);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CVT_PROFESIONAL_UPD", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_PROFESIONAL_UPD", aParam);
        }

        public static SqlDataReader GetProyectos(SqlTransaction tr, int t001_idficepi, string sEstadosProy)
        {
            SqlParameter[] aParam = new SqlParameter[]{
				ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi),
				ParametroSql.add("@sEstadosProy", SqlDbType.VarChar, 10, sEstadosProy)
            };

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_GETPROYECTOS_PROFESIONAL", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_GETPROYECTOS_PROFESIONAL", aParam);
        }

        #region Perfiles de mercado

        public static SqlDataReader Catalogo(SqlTransaction tr, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[]{
				ParametroSql.add("@t001_idficepi_supervisor_aux", SqlDbType.Int, 4, t001_idficepi)
            };

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_PROFPERFILMERCADO_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_PROFPERFILMERCADO_CAT", aParam);
        }

        public static SqlDataReader ComboPerfilesMercado()
        {
            SqlParameter[] aParam = new SqlParameter[0];
            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_PERFILMERCADO_CAT", aParam);
        }

        public static void GrabarPerfilMercado(SqlTransaction tr, int t001_idficepi, int t035_idcodperfil)
        {
            SqlParameter[] aParam = new SqlParameter[]{
				ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi),
                ParametroSql.add("@t035_idcodperfil", SqlDbType.Int, 4, t035_idcodperfil)
            };

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CVT_PROFPERFILMERCADO_UPD", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_PROFPERFILMERCADO_UPD", aParam);
        }

        #endregion

        public static DataSet ObtenerControlAbsentismo(SqlTransaction tr, int annomes, string sCentros, string sEvaluadores, string sPSN)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@annomes", SqlDbType.Int, 4, annomes),
                ParametroSql.add("@sCentros", SqlDbType.VarChar, 8000, sCentros),
                ParametroSql.add("@sEvaluadores", SqlDbType.VarChar, 8000, sEvaluadores),
                ParametroSql.add("@sPSN", SqlDbType.VarChar, 8000, sPSN)
            };

            if (tr == null)
                return SqlHelper.ExecuteDataset("SUP_CONTROL_ABSENTISMO_CAT", aParam);
            else
                return SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_CONTROL_ABSENTISMO_CAT", aParam);
        }
        public static int EsBaja(int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                    ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi)
                };

            int iRes = 1;
            object obj = SqlHelper.ExecuteScalar("SUP_PROFESIONAL_ESBAJA", aParam);
            if (obj != null)
                iRes = int.Parse(obj.ToString());
            return iRes;
        }


    }

}