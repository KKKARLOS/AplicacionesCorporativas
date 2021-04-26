using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    public partial class CAMBIOESTRUCTURAPSN_AUX
    {
        #region Metodos

        public static void Insertar(SqlTransaction tr, int t305_idproyectosubnodo, Nullable<int> t303_idnodo_destino, Nullable<bool> t777_procesado, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);
            aParam[i++] = ParametroSql.add("@t303_idnodo_destino", SqlDbType.Int, 4, t303_idnodo_destino);
            aParam[i++] = ParametroSql.add("@t777_procesado", SqlDbType.Bit, 1, t777_procesado);
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CAMBIOESTRUCTURAPSN_AUX_I", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CAMBIOESTRUCTURAPSN_AUX_I", aParam);
        }

        public static int Modificar(SqlTransaction tr, int t305_idproyectosubnodo, Nullable<int> t303_idnodo_destino, Nullable<bool> t777_procesado, string t777_excepcion, int t001_idficepi, Nullable<int> t777_codigoexcepcion)
        {
            SqlParameter[] aParam = new SqlParameter[6];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);
            aParam[i++] = ParametroSql.add("@t303_idnodo_destino", SqlDbType.Int, 4, t303_idnodo_destino);
            aParam[i++] = ParametroSql.add("@t777_procesado", SqlDbType.Bit, 1, t777_procesado);
            aParam[i++] = ParametroSql.add("@t777_excepcion", SqlDbType.Text, 2147483647, t777_excepcion);
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            aParam[i++] = ParametroSql.add("@t777_codigoexcepcion", SqlDbType.Int, 4, t777_codigoexcepcion);

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CAMBIOESTRUCTURAPSN_AUX_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CAMBIOESTRUCTURAPSN_AUX_U", aParam);
        }

        public static int DeleteMyAll(SqlTransaction tr, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CAMBIOESTRUCTURAPSN_AUX_DEL", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CAMBIOESTRUCTURAPSN_AUX_DEL", aParam);
        }

        public static SqlDataReader CatalogoDestino(SqlTransaction tr, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CAMBIOESTRUCTURAPSN_AUX_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CAMBIOESTRUCTURAPSN_AUX_CAT", aParam);
        }

        #endregion
    }
}
