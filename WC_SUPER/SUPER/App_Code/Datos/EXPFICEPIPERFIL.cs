using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.DAL
{

    /// <summary>
    /// Descripción breve de EXPFICEPIPERFIL
    /// </summary>
    public partial class EXPFICEPIPERFIL
    {
        public static SqlDataReader Catalogo(int t001_idficepi, int t808_idexpprof)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            aParam[i++] = ParametroSql.add("@t808_idexpprof", SqlDbType.Int, 4, t808_idexpprof);
            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_EXPFICEPIPERFIL_C2", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza un registro de la tabla T813_EXPFICEPIPERFIL.
        /// Solo si el estado actual es el mismo que el estado que tenía cuando se le ha llamado al procedimiento
        /// El motivo de rechazo se guarda em la tabla de cronología T838_EXPFICEPIPERFILCRONO
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int t813_idexpficepiperfil, Nullable<DateTime> t813_finicio, Nullable<DateTime> t813_ffin,
                                string t813_funcion, string t813_observa, string t839_idestado, string t838_motivort, DateTime t813_fechau,
                                int t035_idcodperfil,  int t001_idficepiu, string t839_idestado_ant, Nullable<int> profCvExc,
                                Nullable<int> respCvExc, short t020_idcodidioma)
        {
            SqlParameter[] aParam = new SqlParameter[14];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t813_idexpficepiperfil", SqlDbType.Int, 4, t813_idexpficepiperfil);
            aParam[i++] = ParametroSql.add("@t813_finicio", SqlDbType.SmallDateTime, 4, t813_finicio);
            aParam[i++] = ParametroSql.add("@t813_ffin", SqlDbType.SmallDateTime, 4, t813_ffin);
            aParam[i++] = ParametroSql.add("@t813_funcion", SqlDbType.Text, 2147483647, t813_funcion);
            aParam[i++] = ParametroSql.add("@t813_observa", SqlDbType.Text, 2147483647, t813_observa);
            aParam[i++] = ParametroSql.add("@t839_idestado", SqlDbType.Text, 1, t839_idestado);
            aParam[i++] = ParametroSql.add("@t838_motivort", SqlDbType.Text, 500, t838_motivort);
            aParam[i++] = ParametroSql.add("@t813_fechau", SqlDbType.SmallDateTime, 4, t813_fechau);
            aParam[i++] = ParametroSql.add("@t035_idcodperfil", SqlDbType.Int, 4, (t035_idcodperfil == -1) ? null : (int?)t035_idcodperfil);
            //aParam[i++] = ParametroSql.add("@t812_idexpprofficepi", SqlDbType.Int, 4, t812_idexpprofficepi);
            aParam[i++] = ParametroSql.add("@t001_idficepiu", SqlDbType.Int, 4, t001_idficepiu);
            aParam[i++] = ParametroSql.add("@t839_idestado_ant", SqlDbType.Text, 1, t839_idestado_ant);
            aParam[i++] = ParametroSql.add("@profCvExc", SqlDbType.Int, 4, profCvExc);
            aParam[i++] = ParametroSql.add("@respCvExc", SqlDbType.Int, 4, respCvExc);
            aParam[i++] = ParametroSql.add("@t020_idcodidioma", SqlDbType.Int, 2, t020_idcodidioma);

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CVT_EXPFICEPIPERFIL_U2", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_EXPFICEPIPERFIL_U2", aParam);
        }

        public static SqlDataReader ObtenerConsumosEconomicos(int t001_idficepi, int t808_idexpprof)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            aParam[i++] = ParametroSql.add("@t808_idexpprof", SqlDbType.Int, 4, t808_idexpprof);

            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_EXPFICEPIPERFIL_CONS", aParam);
        }

        //public static int PedirBorrado(SqlTransaction tr, int t813_idexpficepiperfil, int t001_idficepi_petbor, string t813_motivo_petbor)
        //{
        //    SqlParameter[] aParam = new SqlParameter[4];
        //    int i = 0;
        //    aParam[i++] = ParametroSql.add("@t813_idexpficepiperfil", SqlDbType.Int, 4, t813_idexpficepiperfil);
        //    aParam[i++] = ParametroSql.add("@t001_idficepi_petbor", SqlDbType.Int, 4, t001_idficepi_petbor);
        //    aParam[i++] = ParametroSql.add("@t813_motivo_petbor", SqlDbType.Text, 500, t813_motivo_petbor);

        //    if (tr == null)
        //        return SqlHelper.ExecuteNonQuery("SUP_CVT_EXPFICEPIPERFIL_PD", aParam);
        //    else
        //        return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_EXPFICEPIPERFIL_PD", aParam);
        //}
    }
}