using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.DAL
{
/// <summary>
/// Descripción breve de CuentasCVT
/// </summary>
    public partial class CuentasCVT
    {
        #region Metodos

        public static SqlDataReader SectorSegmento(SqlTransaction tr)
        {
            SqlParameter[] aParam = new SqlParameter[0];
            
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_SECTORSEGMENTO_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_SECTORSEGMENTO_CAT", aParam);
        }

        public static SqlDataReader Catalogo(SqlTransaction tr, string t811_denominacion, Nullable<byte> t811_estado, string sTipoBusqueda)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t811_denominacion", SqlDbType.VarChar, 100, t811_denominacion);
            aParam[i++] = ParametroSql.add("@t811_estado", SqlDbType.Bit, 1, t811_estado);
            aParam[i++] = ParametroSql.add("@sTipoBusqueda", SqlDbType.Char, 1, sTipoBusqueda);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_CUENTASCVT_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_CUENTASCVT_CAT", aParam);
        }
        /// <summary>
        /// Obtiene cuentasCVT mas clientes que no figuran como cuentasCVT
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t811_denominacion"></param>
        /// <param name="t811_estado"></param>
        /// <param name="sTipoBusqueda"></param>
        /// <returns></returns>
        public static SqlDataReader CuentasMasClientes(SqlTransaction tr, string t811_denominacion, Nullable<byte> t811_estado, string sTipoBusqueda)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t811_denominacion", SqlDbType.VarChar, 100, t811_denominacion);
            aParam[i++] = ParametroSql.add("@t811_estado", SqlDbType.Bit, 1, t811_estado);
            aParam[i++] = ParametroSql.add("@sTipoBusqueda", SqlDbType.Char, 1, sTipoBusqueda);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_CUENTASyCLIENTES_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_CUENTASyCLIENTES_CAT", aParam);
        }

        public static void Delete(SqlTransaction tr, int T811_IDCUENTA)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t811_idcuenta", SqlDbType.Int, 4, T811_IDCUENTA);


            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CVT_CUENTASCVT_DEL", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_CUENTASCVT_DEL", aParam);
        }

        public static int Insert(SqlTransaction tr, string T811_DENOMINACION, Nullable<int> T484_IDSEGMENTO, byte T811_ESTADO, Nullable<int> t302_idcliente)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t811_denominacion", SqlDbType.VarChar, 100, T811_DENOMINACION.ToUpper());
            aParam[i++] = ParametroSql.add("@t484_idsegmento", SqlDbType.Int, 4, T484_IDSEGMENTO);
            aParam[i++] = ParametroSql.add("@t811_estado", SqlDbType.Bit, 1, T811_ESTADO);
            aParam[i++] = ParametroSql.add("@t302_idcliente", SqlDbType.Int, 4, t302_idcliente);

            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_CVT_CUENTASCVT_INS", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CVT_CUENTASCVT_INS", aParam));
        }

        public static void Update(SqlTransaction tr, int T811_IDCUENTA, string T811_DENOMINACION, int T484_IDSEGMENTO, byte T811_ESTADO)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t811_idcuenta", SqlDbType.Int, 4, T811_IDCUENTA);
            aParam[i++] = ParametroSql.add("@t811_denominacion", SqlDbType.VarChar, 100, T811_DENOMINACION.ToUpper());
            aParam[i++] = ParametroSql.add("@t484_idsegmento", SqlDbType.Int, 4, T484_IDSEGMENTO);
            aParam[i++] = ParametroSql.add("@t811_estado", SqlDbType.Bit, 1, T811_ESTADO);


            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CVT_CUENTASCVT_UPD", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_CUENTASCVT_UPD", aParam);
        }
        public static void UpdateSegmento(SqlTransaction tr, int t811_idcuenta, int t484_idsegmento)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t811_idcuenta", SqlDbType.Int, 4, t811_idcuenta),
                ParametroSql.add("@t484_idsegmento", SqlDbType.Int, 4, t484_idsegmento)
            };
            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CVT_CUENTASCVT_SEGMENTO_U", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_CUENTASCVT_SEGMENTO_U", aParam);
        }


        public static SqlDataReader CatalogoProv(string sDenominacion, string sTipo)
        {
            //SqlDataReader drProveedor = SqlHelper.ExecuteSqlDataReader(Utilidades.CadenaConexion,
            //    "FRM_PROVEEDOR", sDenominacion, sTipo);
            //return drProveedor;

            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@denominacion", SqlDbType.VarChar, 100, sDenominacion);
            aParam[i++] = ParametroSql.add("@tipo", SqlDbType.VarChar, 1, sTipo);

            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_PROVEEDORCURSO_CAT", aParam);
            //return SqlHelper.ExecuteSqlDataReader("FRM_PROVEEDOR", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla T811_CUENTACVT.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	05/09/2012 14:15:58
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo(Nullable<int> t811_idcuenta, string t811_denominacion, Nullable<bool> t811_estado, Nullable<int> t484_idsegmento, byte nOrden, byte nAscDesc)
        {
            SqlParameter[] aParam = new SqlParameter[6];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t811_idcuenta", SqlDbType.Int, 4, t811_idcuenta);
            aParam[i++] = ParametroSql.add("@t811_denominacion", SqlDbType.Text, 100, t811_denominacion);
            aParam[i++] = ParametroSql.add("@t811_estado", SqlDbType.Bit, 1, t811_estado);
            aParam[i++] = ParametroSql.add("@t484_idsegmento", SqlDbType.Int, 4, t484_idsegmento);

            aParam[i++] = ParametroSql.add("@nOrden", SqlDbType.TinyInt, 1, nOrden);
            aParam[i++] = ParametroSql.add("@nAscDesc", SqlDbType.TinyInt, 1, nAscDesc);

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_CUENTACVT_C", aParam);
        }

        //AUTOCOMPLETE
        public static SqlDataReader CatalogoCuentaCVT(SqlTransaction tr, string t811_denominacion)//, Nullable<int> origen
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t811_denominacion", SqlDbType.Text, 100, t811_denominacion);
            //aParam[i++] = ParametroSql.add("@origen", SqlDbType.Int, 1, origen);
            
            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_CUENTACVT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_CUENTACVT", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene Los profesionales asociados a una cuenta (cliente no Ibermática)
        /// </summary>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader ProfAsociados(SqlTransaction tr, int t811_idcuenta)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t811_idcuenta", SqlDbType.Int, 4, t811_idcuenta);

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_PROF_CLINOIB_C", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_PROF_CLINOIB_C", aParam);
        }
        public static SqlDataReader ElementosAsociadoAReasignar(SqlTransaction tr, int t811_idcuenta)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t811_idcuenta", SqlDbType.Int, 4, t811_idcuenta)
            };

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_REASIG_CUENTA_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_REASIG_CUENTA_CAT", aParam);
        }
        /// <summary>
        /// Busca en cuentas por denominación exacta
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t811_denominacion"></param>
        /// <returns>0 si no existe, en caso contrario el código de la cuenta</returns>
        public static SqlDataReader ObtenerPorNombre(SqlTransaction tr, string t811_denominacion)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t811_denominacion", SqlDbType.Text, 100);
            aParam[0].Value = t811_denominacion;
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_CUENTACVT_ByNombreExacto", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_CUENTACVT_ByNombreExacto", aParam);
        }

        public static SqlDataReader Datos(SqlTransaction tr, int t811_idcuenta)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t811_idcuenta", SqlDbType.Int, 4, t811_idcuenta)
            };
            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_CUENTACVT_S", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_CUENTACVT_S", aParam);
        }
        #endregion
    }
}





