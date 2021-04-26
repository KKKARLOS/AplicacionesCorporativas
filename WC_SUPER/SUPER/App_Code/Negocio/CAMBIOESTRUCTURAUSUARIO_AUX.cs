using System;
using System.Configuration;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    public partial class CAMBIOESTRUCTURAUSUARIO_AUX
    {
        #region Metodos

        public static void Insertar(SqlTransaction tr, int t314_idusuario, Nullable<int> t303_idnodo_destino, int t776_anomes, Nullable<bool> t776_procesado, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);
            aParam[i++] = ParametroSql.add("@t303_idnodo_destino", SqlDbType.Int, 4, t303_idnodo_destino);
            aParam[i++] = ParametroSql.add("@t776_anomes", SqlDbType.Int, 4, t776_anomes);
            aParam[i++] = ParametroSql.add("@t776_procesado", SqlDbType.Bit, 1, t776_procesado);
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CAMBIOESTRUCTURAUSUARIO_AUX_I", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CAMBIOESTRUCTURAUSUARIO_AUX_I", aParam);
        }

        public static int DeleteMyAll(SqlTransaction tr, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CAMBIOESTRUCTURAUSUARIO_AUX_DEL", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CAMBIOESTRUCTURAUSUARIO_AUX_DEL", aParam);
        }

        public static int Modificar(SqlTransaction tr, int t314_idusuario, Nullable<int> t303_idnodo_destino, int t776_anomes, Nullable<bool> t776_procesado, string t776_excepcion, int t001_idficepi, Nullable<int> t776_codigoexcepcion)
        {
            SqlParameter[] aParam = new SqlParameter[7];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);
            aParam[i++] = ParametroSql.add("@t303_idnodo_destino", SqlDbType.Int, 4, t303_idnodo_destino);
            aParam[i++] = ParametroSql.add("@t776_anomes", SqlDbType.Int, 4, t776_anomes);
            aParam[i++] = ParametroSql.add("@t776_procesado", SqlDbType.Bit, 1, t776_procesado);
            aParam[i++] = ParametroSql.add("@t776_excepcion", SqlDbType.Text, 2147483647, t776_excepcion);
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            aParam[i++] = ParametroSql.add("@t776_codigoexcepcion", SqlDbType.Int, 4, t776_codigoexcepcion);
            
            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CAMBIOESTRUCTURAUSUARIO_AUX_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CAMBIOESTRUCTURAUSUARIO_AUX_U", aParam);
        }

        public static SqlDataReader CatalogoDestino(SqlTransaction tr, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CAMBIOESTRUCTURAUSUARIO_AUX_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CAMBIOESTRUCTURAUSUARIO_AUX_CAT", aParam);
        }


        #endregion
    }
}
