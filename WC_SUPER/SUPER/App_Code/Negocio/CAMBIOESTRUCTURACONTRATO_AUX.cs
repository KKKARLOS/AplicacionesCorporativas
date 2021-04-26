using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    public partial class CAMBIOESTRUCTURACONTRATO_AUX
    {
        #region Metodos

        public static void Insertar(SqlTransaction tr, int t001_idficepi, int t306_idcontrato,
                                    string t778_arrastraproy, Nullable<int> t303_idnodo_origen, Nullable<int> t303_idnodo_destino,
                                    string t778_arrastra_gestor, Nullable<int> t314_idusuario_gestorprod_origen, Nullable<int> t314_idusuario_gestorprod_destino,
                                    string t778_arrastra_cliente, Nullable<int> t302_idcliente_origen, Nullable<int> t302_idcliente_destino,
                                    Nullable<int> t314_idusuario_responsable_origen, Nullable<int> t314_idusuario_responsable_destino,
                                    Nullable<int> t314_idusuario_comercialhermes_origen, Nullable<int> t314_idusuario_comercialhermes_destino,
                                    Nullable<bool> t778_procesado)
        {
            //SqlParameter[] aParam = new SqlParameter[5];
            //int i = 0;
            //aParam[i++] = ParametroSql.add("@t306_idcontrato", SqlDbType.Int, 4, t306_idcontrato);
            //aParam[i++] = ParametroSql.add("@t303_idnodo_destino", SqlDbType.Int, 4, t303_idnodo_destino);
            //aParam[i++] = ParametroSql.add("@t778_arrastraproy", SqlDbType.Text, 1, t778_arrastraproy);
            //aParam[i++] = ParametroSql.add("@t778_procesado", SqlDbType.Bit, 1, t778_procesado);
            //aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi),
                ParametroSql.add("@t306_idcontrato", SqlDbType.Int, 4, t306_idcontrato),
                ParametroSql.add("@t778_arrastraproy", SqlDbType.Char, 1, t778_arrastraproy),
                ParametroSql.add("@t303_idnodo_origen", SqlDbType.Int, 4, t303_idnodo_origen),
                ParametroSql.add("@t303_idnodo_destino", SqlDbType.Int, 4, t303_idnodo_destino),
                ParametroSql.add("@t778_arrastra_gestor", SqlDbType.Char, 1, t778_arrastra_gestor),
                ParametroSql.add("@t314_idusuario_gestorprod_origen", SqlDbType.Int, 4, t314_idusuario_gestorprod_origen),
                ParametroSql.add("@t314_idusuario_gestorprod_destino", SqlDbType.Int, 4, t314_idusuario_gestorprod_destino),
                ParametroSql.add("@t778_arrastra_cliente", SqlDbType.Char, 1, t778_arrastra_cliente),
                ParametroSql.add("@t302_idcliente_origen", SqlDbType.Int, 4, t302_idcliente_origen),
                ParametroSql.add("@t302_idcliente_destino", SqlDbType.Int, 4, t302_idcliente_destino),
                ParametroSql.add("@t314_idusuario_responsable_origen", SqlDbType.Int, 4, t314_idusuario_responsable_origen),
                ParametroSql.add("@t314_idusuario_responsable_destino", SqlDbType.Int, 4, t314_idusuario_responsable_destino),
                ParametroSql.add("@t314_idusuario_comercialhermes_origen", SqlDbType.Int, 4, t314_idusuario_comercialhermes_origen),
                ParametroSql.add("@t314_idusuario_comercialhermes_destino", SqlDbType.Int, 4, t314_idusuario_comercialhermes_destino),
                ParametroSql.add("@t778_procesado", SqlDbType.Bit, 1, t778_procesado)
            };

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CAMBIOESTRUCTURACONTRATO_AUX_I", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CAMBIOESTRUCTURACONTRATO_AUX_I", aParam);
        }

        public static int Modificar(SqlTransaction tr, int t001_idficepi, int t306_idcontrato,
                                    //string t778_arrastraproy, Nullable<int> t303_idnodo_origen, Nullable<int> t303_idnodo_destino,
                                    //string t778_arrastra_gestor, Nullable<int> t314_idusuario_gestorprod_origen, Nullable<int> t314_idusuario_gestorprod_destino,
                                    //string t778_arrastra_cliente, Nullable<int> t302_idcliente_origen, Nullable<int> t302_idcliente_destino,
                                    //Nullable<int> t314_idusuario_responsable_origen, Nullable<int> t314_idusuario_responsable_destino,
                                    //Nullable<int> t314_idusuario_comercialhermes_origen, Nullable<int> t314_idusuario_comercialhermes_destino,
                                    Nullable<bool> t778_procesado, Nullable<int> t778_codigoexcepcion, string t778_excepcion)
        {
            //SqlParameter[] aParam = new SqlParameter[7];
            //int i = 0;
            //aParam[i++] = ParametroSql.add("@t306_idcontrato", SqlDbType.Int, 4, t306_idcontrato);
            //aParam[i++] = ParametroSql.add("@t303_idnodo_destino", SqlDbType.Int, 4, t303_idnodo_destino);
            //aParam[i++] = ParametroSql.add("@t778_arrastraproy", SqlDbType.Text, 1, t778_arrastraproy);
            //aParam[i++] = ParametroSql.add("@t778_procesado", SqlDbType.Bit, 1, t778_procesado);
            //aParam[i++] = ParametroSql.add("@t778_excepcion", SqlDbType.Text, 2147483647, t778_excepcion);
            //aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            //aParam[i++] = ParametroSql.add("@t778_codigoexcepcion", SqlDbType.Int, 4, t778_codigoexcepcion);
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi),
                ParametroSql.add("@t306_idcontrato", SqlDbType.Int, 4, t306_idcontrato),
                ParametroSql.add("@t778_procesado", SqlDbType.Bit, 1, t778_procesado),
                ParametroSql.add("@t778_codigoexcepcion", SqlDbType.Int, 4, t778_codigoexcepcion),
                ParametroSql.add("@t778_excepcion", SqlDbType.Text, 2147483647, t778_excepcion)
            };

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CAMBIOESTRUCTURACONTRATO_AUX_U", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CAMBIOESTRUCTURACONTRATO_AUX_U", aParam);
        }

        public static int DeleteMyAll(SqlTransaction tr, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CAMBIOESTRUCTURACONTRATO_AUX_DEL", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CAMBIOESTRUCTURACONTRATO_AUX_DEL", aParam);
        }

        public static SqlDataReader CatalogoDestino(SqlTransaction tr, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CAMBIOESTRUCTURACONTRATO_AUX_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CAMBIOESTRUCTURACONTRATO_AUX_CAT", aParam);
        }

        #endregion
    }
}
