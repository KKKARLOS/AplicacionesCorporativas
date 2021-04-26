using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using SUPER.Capa_Datos;

namespace SUPER.DAL
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : EXPPROF
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T808_EXPPROF
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	30/07/2012 12:27:05	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class EXPPROF
    {
        #region Propiedades y Atributos

        #endregion

        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla T808_EXPPROF.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	30/07/2012 12:27:05
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader DatosExpProf(SqlTransaction tr, int t808_idexpprof)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t808_idexpprof", SqlDbType.Int, 4, t808_idexpprof);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_EXPPROF_S4", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_EXPPROF_S4", aParam);
        }

        public static SqlDataReader DatosExpProf(SqlTransaction tr, int t808_idexpprof, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t808_idexpprof", SqlDbType.Int, 4, t808_idexpprof),
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi)
            };
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_EXPPROF_S5", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_EXPPROF_S5", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene los datos de una experiencia fuera de ibarmatica
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	30/07/2012 12:27:05
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader DatosExpProfFuera(SqlTransaction tr, int t808_idexpprof)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t808_idexpprof", SqlDbType.Int, 4, t808_idexpprof);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_EXPPROF_FUERA_S", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_EXPPROF_FUERA_S", aParam);
        }
        public static SqlDataReader DatosExpProfFuera(SqlTransaction tr, int t808_idexpprof, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t808_idexpprof", SqlDbType.Int, 4, t808_idexpprof),
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi)
            };
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_EXPPROF_FUERA_S2", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_EXPPROF_FUERA_S2", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla T808_EXPPROF.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	30/07/2012 12:27:05
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Datos(SqlTransaction tr, int t301_idproyecto)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t301_idproyecto", SqlDbType.Int, 4, t301_idproyecto);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_EXPPROF_S2", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_EXPPROF_S2", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene el nombre de un proyecto y su cliente
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	07/08/2012 12:27:05
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader DatosProy(SqlTransaction tr, int t301_idproyecto)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t301_idproyecto", SqlDbType.Int, 4, t301_idproyecto);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_EXPPROF_S3", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_EXPPROF_S3", aParam);
        }

        /// <summary>
        /// Obtiene un catálogo de proyectos asociados al cliente que se pasa como parametro
        /// y que tengan experiencia profesional asociada
        /// </summary>
        public static SqlDataReader GetProyectosExpProf(SqlTransaction tr, int t302_idcliente)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t302_idcliente", SqlDbType.Int, 4, t302_idcliente);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_GETPROYECTOSEXPPROF_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_GETPROYECTOSEXPPROF_CAT", aParam);

        }

        /// <summary>
        /// Obtiene el número de proyectos asociados al cliente que se pasa como parametro
        /// y que tengan experiencia profesional asociada
        /// </summary>
        public static bool bHayExpProfByCliente(SqlTransaction tr, int t302_idcliente)
        {
            SqlParameter[] aParam = new SqlParameter[1]{
                ParametroSql.add("@t302_idcliente", SqlDbType.Int, 4, t302_idcliente)
            };

            if (tr == null)
                return (Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_CVT_GETPROYECTOSEXPPROF_COUNT", aParam)) > 0)? true:false;
            else
                return (Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CVT_GETPROYECTOSEXPPROF_COUNT", aParam)) > 0)? true:false;

        }

        /// <summary>
        /// Obtiene un catálogo de experiencias profesionales en IBERMATICA asociados al profesional que se pasa como parametro
        /// </summary>
        public static SqlDataReader CatalogoEnIbermaticaByProfesional(SqlTransaction tr, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[0].Value = t001_idficepi;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_EXPPROF_ENIB_CAT2", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_EXPPROF_ENIB_CAT2", aParam);

        }

        /// <summary>
        /// Obtiene un catálogo de experiencias profesionales fuera de IBERMATICA asociados al profesional que se pasa como parametro
        /// </summary>
        public static SqlDataReader CatalogoFueraIbermaticaByProfesional(SqlTransaction tr, int t001_idficepi)
        {
            
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[0].Value = t001_idficepi;


            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_EXPPROF_FUERAIB_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_EXPPROF_FUERAIB_CAT", aParam);

        }

        public static SqlDataReader MiCVExpProfEnIbermaticaHTML(SqlTransaction tr, int t001_idficepi, int bControl, string nombrecuenta, Nullable<int> idcuenta, Nullable<int> t483_idsector, Nullable<int> t035_codperfile, string let036_idcodentorno)
        {
            SqlParameter[] aParam = new SqlParameter[7];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            aParam[i++] = ParametroSql.add("@bfiltros", SqlDbType.Int, 1, bControl);
            aParam[i++] = ParametroSql.add("@nombrecuenta", SqlDbType.VarChar, 100, nombrecuenta);
            aParam[i++] = ParametroSql.add("@idcuenta", SqlDbType.Int, 4, idcuenta);
            aParam[i++] = ParametroSql.add("@t483_idsector", SqlDbType.Int, 4, t483_idsector);
            aParam[i++] = ParametroSql.add("@t035_codperfile", SqlDbType.Int, 4, t035_codperfile);
            aParam[i++] = ParametroSql.add("@let036_idcodentorno", SqlDbType.VarChar, 8000, let036_idcodentorno);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_EXPPROFIBERRP", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_EXPPROFIBERRP", aParam);
        }

        public static SqlDataReader MiCVExpProfFueraIbermaticaHTML(SqlTransaction tr, int t001_idficepi, int bControl, string nombrecuenta, Nullable<int> idcuenta, Nullable<int> t483_idsector, Nullable<int> t035_codperfile, string let036_idcodentorno)
        {
            SqlParameter[] aParam = new SqlParameter[7];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            aParam[i++] = ParametroSql.add("@bfiltros", SqlDbType.Int, 1, bControl);
            aParam[i++] = ParametroSql.add("@nombrecuenta", SqlDbType.VarChar, 100, nombrecuenta);
            aParam[i++] = ParametroSql.add("@idcuenta", SqlDbType.Int, 4, idcuenta);
            aParam[i++] = ParametroSql.add("@t483_idsector", SqlDbType.Int, 4, t483_idsector);
            aParam[i++] = ParametroSql.add("@t035_codperfile", SqlDbType.Int, 4, t035_codperfile);
            aParam[i++] = ParametroSql.add("@let036_idcodentorno", SqlDbType.VarChar, 8000, let036_idcodentorno);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_EXPPROFFUERARP", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_EXPPROFFUERARP", aParam);

        }

        public static SqlDataReader CatalogoExpProf(SqlTransaction tr, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_EXPPROF_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_EXPPROF_CAT", aParam);
        }

        public static SqlDataReader DatosExpProfDetPerfil(SqlTransaction tr, int t808_idexpprof)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t808_idexpprof", SqlDbType.Int, 4, t808_idexpprof);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_EXPPROF_SEL", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_EXPPROF_SEL", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Reasigna clientes no Ibermatica
        /// </summary>
        /// <returns></returns>
        /// -----------------------------------------------------------------------------
        public static void Reasignar(SqlTransaction tr, int idOrigen, int idDestino)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@idOrigen", SqlDbType.Int, 4, idOrigen);
            aParam[i++] = ParametroSql.add("@idDestino", SqlDbType.Int, 4, idDestino);

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CVT_REASIGNAR_CUENTA", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_REASIGNAR_CUENTA", aParam);
        }
        /// <summary>
        /// Borra los proyectos de la experiencia de la lista de proyectos a cualificar T174_PROYECTOCUALIFICACIONCVT
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t808_idexpprof"></param>
        /// <returns></returns>
        public static int BorrarPdteCualificar(SqlTransaction tr, int t808_idexpprof)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t808_idexpprof", SqlDbType.Int, 4, t808_idexpprof);
            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CVT_PROYECTOCUALIFICACIONCVT_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_PROYECTOCUALIFICACIONCVT_D", aParam);
        }


        public static SqlDataReader ObtenerExperienciasProyectos(string sDenExperiencia, int t314_idusuario, string t301_estado, string t301_categoria, string t305_cualidad,
                                                                 string sClientes, string sResponsables, string sIDEstructura, bool bComparacionLogica, string sPSN, bool bAdministrador)
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t808_denominacion", SqlDbType.VarChar, 70, sDenExperiencia),
                ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario),
                ParametroSql.add("@t301_estado", SqlDbType.Char, 1, t301_estado),
                ParametroSql.add("@t301_categoria", SqlDbType.Char, 1, t301_categoria),
                ParametroSql.add("@t305_cualidad", SqlDbType.Char, 1, t305_cualidad),
                ParametroSql.add("@sClientes", SqlDbType.VarChar, 8000, sClientes),
                ParametroSql.add("@sResponsables", SqlDbType.VarChar, 8000, sResponsables),
                //ParametroSql.add("@sNaturalezas", SqlDbType.VarChar, 8000, sNaturalezas),
                //ParametroSql.add("@sHorizontal", SqlDbType.VarChar, 8000, sHorizontal),
                //ParametroSql.add("@sModeloContrato", SqlDbType.VarChar, 8000, sModeloContrato),
                //ParametroSql.add("@sContrato", SqlDbType.VarChar, 8000, sContrato),
                ParametroSql.add("@sIDEstructura", SqlDbType.VarChar, 8000, sIDEstructura),
                //ParametroSql.add("@sSectores", SqlDbType.VarChar, 8000, sSectores),
                //ParametroSql.add("@sSegmentos", SqlDbType.VarChar, 8000, sSegmentos),
                ParametroSql.add("@bComparacionLogica", SqlDbType.Bit, 1, bComparacionLogica),
                //ParametroSql.add("@sCNP", SqlDbType.VarChar, 8000, sCNP),
                //ParametroSql.add("@sCSN1P", SqlDbType.VarChar, 8000, sCSN1P),
                //ParametroSql.add("@sCSN2P", SqlDbType.VarChar, 8000, sCSN2P),
                //ParametroSql.add("@sCSN3P", SqlDbType.VarChar, 8000, sCSN3P),
                //ParametroSql.add("@sCSN4P", SqlDbType.VarChar, 8000, sCSN4P),
                ParametroSql.add("@sPSN", SqlDbType.VarChar, 8000, sPSN),
                ParametroSql.add("@bAdministrador", SqlDbType.Bit, 1, bAdministrador)
            };

            return SqlHelper.ExecuteSqlDataReader("SUP_GETEXPERIENCIAS_CAT", aParam);
        }


        public static SqlDataReader ObtenerCriterios(int t314_idusuario, int nDesde, int nHasta, int nFilasMax)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@nDesde", SqlDbType.Int, 4);
            aParam[1].Value = nDesde;
            aParam[2] = new SqlParameter("@nHasta", SqlDbType.Int, 4);
            aParam[2].Value = nHasta;
            aParam[3] = new SqlParameter("@nFilasMax", SqlDbType.Int, 4);
            aParam[3].Value = nFilasMax;

            return SqlHelper.ExecuteSqlDataReader("SUP_CONS_EXPERIENCIAS_CRITERIOS", aParam);
        }

        #endregion
    }
}
