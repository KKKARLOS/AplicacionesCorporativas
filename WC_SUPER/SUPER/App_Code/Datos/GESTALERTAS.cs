using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.DAL
{
    public partial class GESTALERTAS
    {
        #region Propiedades y Atributos

        #endregion

        #region Metodos

        public static DataSet ObtenerGestoresAlertas(SqlTransaction tr)
        {
            SqlParameter[] aParam = new SqlParameter[0];
            //int i = 0;
            //aParam[i++] = ParametroSql.add("@", SqlDbType.Int, 4, );

            if (tr == null)
                return SqlHelper.ExecuteDataset("SUP_GESTALERTAS_CAT", aParam);
            else
                return SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_GESTALERTAS_CAT", aParam);
        }

        public static void Insertar(SqlTransaction tr, int t392_idsupernodo2, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t392_idsupernodo2", SqlDbType.Int, 4, t392_idsupernodo2);
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_GESTALERTAS_INS", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_GESTALERTAS_INS", aParam);
        }
        public static void Eliminar(SqlTransaction tr, int t392_idsupernodo2, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t392_idsupernodo2", SqlDbType.Int, 4, t392_idsupernodo2);
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_GESTALERTAS_DEL", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_GESTALERTAS_DEL", aParam);
        }

        public static SqlDataReader ObtenerCatalogoGestores(SqlTransaction tr)
        {
            SqlParameter[] aParam = new SqlParameter[0];

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_DIALOGOALERTAS_CATGESTORES", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_DIALOGOALERTAS_CATGESTORES", aParam);
        }

        #endregion
    }
}
