using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.DAL
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : EXPPROFPROYECTO
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T818_EXPPROFPROYECTO
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	30/07/2012 12:27:05	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class EXPPROFPROYECTO
    {
        #region Propiedades y Atributos

        private int _t301_idproyecto;
        public int t301_idproyecto
        {
            get { return _t301_idproyecto; }
            set { _t301_idproyecto = value; }
        }
        private int _t808_idexpprof;
        public int t808_idexpprof
        {
            get { return _t808_idexpprof; }
            set { _t808_idexpprof = value; }
        }

        #endregion

        #region Constructor

        public EXPPROFPROYECTO()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion

        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T818_EXPPROF.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	30/07/2012 12:27:05
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, int t808_idexpprof, int t301_idproyecto)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t808_idexpprof", SqlDbType.Int, 4, t808_idexpprof);
            aParam[i++] = ParametroSql.add("@t301_idproyecto", SqlDbType.Int, 4, t301_idproyecto);

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_CVT_EXPPROFPROYECTO_I", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CVT_EXPPROFPROYECTO_I", aParam));
        }

        public static SqlDataReader CatalogoByExperiencia(SqlTransaction tr, int t808_idexpprof)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t808_idexpprof", SqlDbType.Int, 4, t808_idexpprof);

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_EXPPROFPROYECTO_CATByEXP", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_EXPPROFPROYECTO_CATByEXP", aParam);
        }

        public static int CountProyectosByExperiencia(SqlTransaction tr, int t808_idexpprof)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t808_idexpprof", SqlDbType.Int, 4, t808_idexpprof);

            DataSet ds = null;
            int nResultado = 0;
            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                ds = SqlHelper.ExecuteDataset("SUP_EXPPROFPROYECTO_CATByEXP", aParam);
            else
                ds = SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_EXPPROFPROYECTO_CATByEXP", aParam);

            nResultado = ds.Tables[0].Rows.Count;
            ds.Dispose();
            return nResultado;
        }

        #endregion
    }
}
