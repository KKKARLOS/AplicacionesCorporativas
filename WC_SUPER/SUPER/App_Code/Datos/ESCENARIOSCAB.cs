using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SUPER.Capa_Datos
{/// <summary>
    /// Descripción breve de ESCENARIOSCAB
    /// </summary>
    public class ESCENARIOSCAB
    {
        #region Metodos

        public static DataSet ObtenerDatosEscenario_old(SqlTransaction tr, int t789_idescenario, string t422_idmoneda)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t789_idescenario", SqlDbType.Int, 4, t789_idescenario);
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);

            if (tr == null)
                return SqlHelper.ExecuteDataset("SUP_ESCENARIO_GETDATOS_old", aParam);
            else
                return SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_ESCENARIO_GETDATOS_old", aParam);
        }
        public static DataSet ObtenerDatosEscenario(SqlTransaction tr, int t789_idescenario, string t422_idmoneda)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t789_idescenario", SqlDbType.Int, 4, t789_idescenario);
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);

            if (tr == null)
                return SqlHelper.ExecuteDataset("SUP_ESCENARIO_GETDATOS", aParam);
            else
                return SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_ESCENARIO_GETDATOS", aParam);
        }

        public static int Insertar(SqlTransaction tr, 
                                    string t789_denominacion, 
                                    int t001_idficepi_creador,
                                    Nullable<int> t305_idproyectosubnodo, 
                                    Nullable<int> t306_idcontrato,
                                    Nullable<byte> t316_idmodalidad, 
                                    string t789_modelocoste,
                                    string t789_modelotarif)
        {
            SqlParameter[] aParam = new SqlParameter[7];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t789_denominacion", SqlDbType.VarChar, 50, t789_denominacion);
            aParam[i++] = ParametroSql.add("@t001_idficepi_creador", SqlDbType.Int, 4, t001_idficepi_creador);
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);
            aParam[i++] = ParametroSql.add("@t306_idcontrato", SqlDbType.Int, 4, t306_idcontrato);
            aParam[i++] = ParametroSql.add("@t316_idmodalidad", SqlDbType.Int, 4, t316_idmodalidad);
            aParam[i++] = ParametroSql.add("@t789_modelocoste", SqlDbType.Char, 1, t789_modelocoste);
            aParam[i++] = ParametroSql.add("@t789_modelotarif", SqlDbType.Char, 1, t789_modelotarif);

            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_ESCENARIOCAB_INS", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_ESCENARIOCAB_INS", aParam));
        }
        

        public static SqlDataReader Obtener(SqlTransaction tr, int t789_idescenario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t789_idescenario", SqlDbType.Int, 4, t789_idescenario);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_ESCENARIO_SEL", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_ESCENARIO_SEL", aParam);
        }

        #endregion
    }
}
