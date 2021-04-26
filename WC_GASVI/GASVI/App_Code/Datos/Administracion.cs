using System;
using System.Data;
//using System.Web;
using System.Data.SqlClient;
using GASVI.BLL;

namespace GASVI.DAL
{
    public partial class Administracion
    {
        public static SqlDataReader CatalogoConsultas(short t674_estado)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t674_estado", SqlDbType.Bit, 1, t674_estado);

            return SqlHelper.ExecuteSqlDataReader("GVT_CONSULTAADMGASVI", aParam);
        }

        public static int GetNumConsultas()
        {
            SqlParameter[] aParam = new SqlParameter[0];

            return Convert.ToInt32(SqlHelper.ExecuteScalar("GVT_CONSULTAADMGASVI_COUNT", aParam));
        }

        public static SqlDataReader SelectByIdconsulta(SqlTransaction tr, int t674_idconsulta)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t674_idconsulta", SqlDbType.Int, 4, t674_idconsulta);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("GVT_PARAMETROCONSULTAADMGASVI_SByIdconsulta", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_PARAMETROCONSULTAADMGASVI_SByIdconsulta", aParam);
        }
        public static SqlDataReader Catalogo(SqlTransaction tr, bool bActivos)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@bActivos", SqlDbType.Bit, 1, bActivos);


            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("GVT_PARAMETROCONSULTAADM_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GVT_PARAMETROCONSULTAADM_CAT", aParam);
        }

        public static DataSet EjecutarConsulta(string sProdAlm, object[] aParametros)
        {
            return SqlHelper.ExecuteDataset(Utilidades.CadenaConexion, sProdAlm, 300, aParametros);
        }

        public static void UpdateConsulta(SqlTransaction tr, int t674_idconsulta, string t674_denominacion,
                                short t674_estado, string t674_descripcion)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t674_idconsulta", SqlDbType.Int, 4, t674_idconsulta);
            aParam[i++] = ParametroSql.add("@t674_denominacion", SqlDbType.VarChar, 50, t674_denominacion);
            aParam[i++] = ParametroSql.add("@t674_estado", SqlDbType.Bit, 1, t674_estado);
            aParam[i++] = ParametroSql.add("@t674_descripcion", SqlDbType.Text, 16, t674_descripcion);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("GVT_CONSULTAADMGASVI_UPD", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "GVT_CONSULTAADMGASVI_UPD", aParam);
        }


	}
}