using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.DAL
{
    /// <summary>
    /// Descripción breve de NodoAlertas
    /// </summary>
    public partial class NodoAlertas
    {
        //public NodoAlertas(){}
        public static void Update(SqlTransaction tr, int t826_idnodoalertas, 
                                  Nullable<int> t826_parametro1, Nullable<int> t826_parametro2, Nullable<int> t826_parametro3)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t826_idnodoalertas", SqlDbType.Int, 4, t826_idnodoalertas);
            aParam[i++] = ParametroSql.add("@t826_parametro1", SqlDbType.Int, 4, t826_parametro1);
            aParam[i++] = ParametroSql.add("@t826_parametro2", SqlDbType.Int, 4, t826_parametro2);
            aParam[i++] = ParametroSql.add("@t826_parametro3", SqlDbType.Int, 4, t826_parametro3);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_NODOALERTAS_UPD", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_NODOALERTAS_UPD", aParam);
        }

        public static SqlDataReader CatalogoByNodo(SqlTransaction tr, int t303_idnodo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t303_idnodo", SqlDbType.Int, 4, t303_idnodo);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_NODOALERTAS_C2", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_NODOALERTAS_C2", aParam);
        }
        public static void TrasladarAlertaEstructuraParam(SqlTransaction tr, byte nOpcion, byte nNivel, int nCodigo)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            int i = 0;
            aParam[i++] = ParametroSql.add("@nOpcion", SqlDbType.TinyInt, 1, nOpcion);
            aParam[i++] = ParametroSql.add("@nNivel", SqlDbType.TinyInt, 1, nNivel);
            aParam[i++] = ParametroSql.add("@nCodigo", SqlDbType.Int, 4, nCodigo);

            // Ejecuta la query y devuelve el numero de registros modificados.
            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_SETALERTA_ESTRUCTURA_PARAM", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_SETALERTA_ESTRUCTURA_PARAM", aParam);
        }
        public static int Duplicar(SqlTransaction tr, int t303_idnodoDestino)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@idNodoDes", SqlDbType.Int, 4);
            aParam[0].Value = t303_idnodoDestino;

            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_NODOALERTAS_U_COP", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_NODOALERTAS_U_COP", aParam));
        }
        public static int DeleteByNodo(SqlTransaction tr, int t303_idnodo)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t303_idnodo", SqlDbType.Int, 4, t303_idnodo)
            };
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_NODOALERTAS_D1", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_NODOALERTAS_D1", aParam));
        }
        public static int Duplicar(SqlTransaction tr, int t303_idnodoOrigen, int t303_idnodoDestino)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@idNodoOri", SqlDbType.Int, 4, t303_idnodoOrigen),
                ParametroSql.add("@idNodoDes", SqlDbType.Int, 4, t303_idnodoDestino)
            };
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_NODOALERTAS_I_COP", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_NODOALERTAS_I_COP", aParam));
        }
    }
}