using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.DAL
{
    /// <summary>
    /// Descripción breve de EntornoTecno
    /// </summary>
    public partial class EntornoTecno
    {
        #region Metodos

        /// <summary>
        /// OBTIENE LOS ENTORNOS TECNOLOGICOS INDEPENDIENTEMENTE DE SI ESTAN ASIGNADOS O NO
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t036_descripcion"></param>
        /// <param name="t036_estado"></param>
        /// <returns></returns>

        public static SqlDataReader Catalogo(SqlTransaction tr, string t036_descripcion, Nullable<byte> t036_estado, string sTipoBusqueda)
        {
            SqlParameter[] aParam = new SqlParameter[]{                
                ParametroSql.add("@t036_descripcion", SqlDbType.VarChar, 50, t036_descripcion),
                ParametroSql.add("@t036_estado", SqlDbType.Bit, 1, t036_estado),
                ParametroSql.add("@sTipoBusqueda", SqlDbType.Char, 1, sTipoBusqueda)
            };

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_ENTORNOTECNO_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_ENTORNOTECNO_CAT", aParam);
        }


        /// <summary>
        /// Obtiene los entornos tecnológicos asociados al perfil de un profesional en una experiencia profesional
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t813_idexpficepiperfil">Código de perfil del profesional en la experiencia profesional</param>
        /// <returns></returns>
        public static SqlDataReader CatalogoByProf(SqlTransaction tr, int t813_idexpficepiperfil)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t813_idexpficepiperfil", SqlDbType.Int, 4, t813_idexpficepiperfil);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_EXPFICEPIENTORNO_C2", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_EXPFICEPIENTORNO_C2", aParam);
        }

        public static SqlDataReader CatalogoPlantilla(SqlTransaction tr, int t819_idplantillacvt)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t819_idplantillacvt", SqlDbType.Int, 4, t819_idplantillacvt);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_PLANTILLAENTORNO_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_PLANTILLAENTORNO_CAT", aParam);
        }

        public static void Delete(SqlTransaction tr, int T036_IDCODENTORNO)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t036_idcodentorno", SqlDbType.Int, 4, T036_IDCODENTORNO);


            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CVT_ENTORNOTECNO_DEL", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_ENTORNOTECNO_DEL", aParam);
        }

        public static int Insert(SqlTransaction tr, string T036_DESCRIPCION, Byte T036_ESTADO)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t036_descripcion", SqlDbType.VarChar, 100, T036_DESCRIPCION.Trim().ToUpper());
            aParam[i++] = ParametroSql.add("@t036_estado", SqlDbType.Bit, 1, T036_ESTADO);

            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_CVT_ENTORNOTECNO_INS", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CVT_ENTORNOTECNO_INS", aParam));
        }

        public static void Update(SqlTransaction tr, int T036_IDCODENTORNO, string T036_DESCRIPCION, Byte T036_ESTADO)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t036_idcodentorno", SqlDbType.Int, 4, T036_IDCODENTORNO);
            aParam[i++] = ParametroSql.add("@t036_descripcion", SqlDbType.VarChar, 100, T036_DESCRIPCION.Trim().ToUpper());
            aParam[i++] = ParametroSql.add("@t036_estado", SqlDbType.Bit, 1, T036_ESTADO);


            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CVT_ENTORNOTECNO_UPD", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_ENTORNOTECNO_UPD", aParam);
        }

        public static SqlDataReader obtenerEntorno(int tipo)// 1 Todos
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;

            aParam[i++] = ParametroSql.add("@tipo", SqlDbType.Int, 4, tipo);

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_ENTORNO_CAT", aParam);
        }

        /// <summary>
        /// Obtiene los Profesionales Asociados asociados a un entorno tecnológico
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t036_idcodentorno">Código de entorno tecnológico</param>
        /// <returns></returns>
        public static SqlDataReader ProfAsociados(SqlTransaction tr, int t036_idcodentorno)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t036_idcodentorno", SqlDbType.Int, 4, t036_idcodentorno);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_PROF_ENTORNO_C", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_PROF_ENTORNO_C", aParam);
        }
        public static SqlDataReader ElementosAsociadoAReasignar(SqlTransaction tr, int t036_idcodentorno)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t036_idcodentorno", SqlDbType.Int, 4, t036_idcodentorno)
            };

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_REASIG_ENTORNO_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_REASIG_ENTORNO_CAT", aParam);
        }
        #endregion
    }
}





