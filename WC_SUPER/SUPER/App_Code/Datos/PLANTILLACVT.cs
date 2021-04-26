using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;
namespace SUPER.DAL
{
    /// -----------------------------------------------------------------------------
    /// Project	 : SUPER
    /// Class	 : PLANTILLACVT
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T819_PLANTILLACVT
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	08/08/2012 10:05:13	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class PLANTILLACVT
    {
        #region Propiedades y Atributos

        private int _t819_idplantillacvt;
        public int t819_idplantillacvt
        {
            get { return _t819_idplantillacvt; }
            set { _t819_idplantillacvt = value; }
        }

        private string _t819_denominacion;
        public string t819_denominacion
        {
            get { return _t819_denominacion; }
            set { _t819_denominacion = value; }
        }
        #endregion

        #region Constructor

        public PLANTILLACVT()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion

        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla T819_PLANTILLACVT.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	08/08/2012 10:05:13
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader GetPlantillas(int t808_idexpprof)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t808_idexpprof", SqlDbType.Int, 4, t808_idexpprof);
            return SUPER.Capa_Datos.SqlHelper.ExecuteSqlDataReader("SUP_CVT_PLANTILLACVT_C2", aParam);
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un registro de la tabla T819_PLANTILLACVT.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	13/08/2012 11:34:31
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Select(int t819_idplantillacvt)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t819_idplantillacvt", SqlDbType.Int, 4, t819_idplantillacvt);
            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_PLANTILLACVT_S2", aParam);
        }

        public static SqlDataReader Detalle(int t819_idplantillacvt, int t001_idficepi)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t819_idplantillacvt", SqlDbType.Int, 4, t819_idplantillacvt);
            aParam[i++] = ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi);
            
            return SqlHelper.ExecuteSqlDataReader("SUP_CVT_EXPFICEPIPLANTILLA_S", aParam);
        }


        #endregion
    }
}
