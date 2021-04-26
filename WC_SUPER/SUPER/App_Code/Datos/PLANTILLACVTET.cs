using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;

namespace SUPER.DAL
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : PLANTILLACVTET
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T814_PLANTILLACVTET
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	13/08/2012 12:27:05	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class PLANTILLACVTET
    {
        #region Propiedades y Atributos

        private int _t819_idplantillacvt;
        public int t819_idplantillacvt
        {
            get { return _t819_idplantillacvt; }
            set { _t819_idplantillacvt = value; }
        }
        private int _t036_idcodentorno;
        public int t036_idcodentorno
        {
            get { return _t036_idcodentorno; }
            set { _t036_idcodentorno = value; }
        }

        #endregion

        #region Constructor

        public PLANTILLACVTET()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion

        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T814_PLANTILLACVTET.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	13/08/2012 12:27:05	
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, int t819_idplantillacvt, int t036_idcodentorno)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t819_idplantillacvt", SqlDbType.Int, 4, t819_idplantillacvt);
            aParam[i++] = ParametroSql.add("@t036_idcodentorno", SqlDbType.Int, 4, t036_idcodentorno);

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_CVT_PLANTILLACVTET_I", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_CVT_PLANTILLACVTET_I", aParam));
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina un registro de la tabla T814_PLANTILLACVTET a traves de la primary key.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	13/08/2012 12:27:05	
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Delete(SqlTransaction tr, int t819_idplantillacvt, int t036_idcodentorno)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t819_idplantillacvt", SqlDbType.Int, 4, t819_idplantillacvt);
            aParam[i++] = ParametroSql.add("@t036_idcodentorno", SqlDbType.Int, 4, t036_idcodentorno);

            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_CVT_PLANTILLACVTET_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CVT_PLANTILLACVTET_D", aParam);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla T814_PLANTILLACVTET.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	08/08/2012 10:05:13
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader GetEntornos(SqlTransaction tr, int t819_idplantillacvt)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t819_idplantillacvt", SqlDbType.Int, 4, t819_idplantillacvt);
            if (tr == null) 
                return SqlHelper.ExecuteSqlDataReader("SUP_CVT_PLANTILLACVTET_C2", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CVT_PLANTILLACVTET_C2", aParam);
        }
        #endregion
    }
}
