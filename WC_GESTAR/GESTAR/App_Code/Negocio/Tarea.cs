using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using GESTAR.Capa_Datos;

namespace GESTAR.Capa_Negocio
{
    /// -----------------------------------------------------------------------------
    /// Project	 : GESTAR
    /// Class	 : TAREA
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Clase de acceso a datos para la tabla: T072_TAREA
    /// </summary>
    /// <history>
    /// 	Creado por [sqladmin]	03/09/2007 14:07:32	
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class TAREA
    {
        #region Constructores

        public TAREA()
        {
            //En el constructor vacio, se inicializan los atributos
            //con los valores predeterminados según el tipo de dato.
        }

        #endregion


        #region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T072_TAREA.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	03/09/2007 14:07:32
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, int T044_IDDEFICIENCIA, string T072_DENOMINACION, string T072_DESCRIPCION, Nullable<DateTime> T072_FECINIPREV, Nullable<DateTime> T072_FECFINPREV, Nullable<DateTime> T072_FECINIREAL, Nullable<DateTime> T072_FECFINREAL, string T072_CAUSA, string T072_INTERVENCION, string T072_CONSIDERACION, short T072_AVANCE, byte T072_RESULTADO, int T072_USUMODIF)
        {
            SqlParameter[] aParam = new SqlParameter[13];
            aParam[0] = new SqlParameter("@T044_IDDEFICIENCIA", SqlDbType.Int, 4);
            aParam[0].Value = T044_IDDEFICIENCIA;
            aParam[1] = new SqlParameter("@T072_DENOMINACION", SqlDbType.Text, 100);
            aParam[1].Value = T072_DENOMINACION;
            aParam[2] = new SqlParameter("@T072_DESCRIPCION", SqlDbType.Text, 2147483647);
            aParam[2].Value = T072_DESCRIPCION;
            aParam[3] = new SqlParameter("@T072_FECINIPREV", SqlDbType.SmallDateTime, 4);
            aParam[3].Value = T072_FECINIPREV;
            aParam[4] = new SqlParameter("@T072_FECFINPREV", SqlDbType.SmallDateTime, 4);
            aParam[4].Value = T072_FECFINPREV;
            aParam[5] = new SqlParameter("@T072_FECINIREAL", SqlDbType.SmallDateTime, 4);
            aParam[5].Value = T072_FECINIREAL;
            aParam[6] = new SqlParameter("@T072_FECFINREAL", SqlDbType.SmallDateTime, 4);
            aParam[6].Value = T072_FECFINREAL;
            aParam[7] = new SqlParameter("@T072_CAUSA", SqlDbType.Text, 2147483647);
            aParam[7].Value = T072_CAUSA;
            aParam[8] = new SqlParameter("@T072_INTERVENCION", SqlDbType.Text, 2147483647);
            aParam[8].Value = T072_INTERVENCION;
            aParam[9] = new SqlParameter("@T072_CONSIDERACION", SqlDbType.Text, 2147483647);
            aParam[9].Value = T072_CONSIDERACION;
            aParam[10] = new SqlParameter("@T072_AVANCE", SqlDbType.SmallInt, 2);
            aParam[10].Value = T072_AVANCE;
            aParam[11] = new SqlParameter("@T072_RESULTADO", SqlDbType.TinyInt, 1);
            aParam[11].Value = T072_RESULTADO;
            aParam[12] = new SqlParameter("@T072_USUMODIF", SqlDbType.Int, 4);
            aParam[12].Value = T072_USUMODIF;

            // Ejecuta la query y devuelve el valor del nuevo Identity.

            int returnValue;
            if (tr == null)
                returnValue = Convert.ToInt32(SqlHelper.ExecuteScalar("GESTAR_TAREA_I", aParam));
            else
                returnValue = Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "GESTAR_TAREA_I", aParam));
            return returnValue;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza un registro de la tabla T072_TAREA.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	03/09/2007 14:07:32
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Update(SqlTransaction tr, int T072_IDTAREA, int T044_IDDEFICIENCIA, string T072_DENOMINACION, string T072_DESCRIPCION, Nullable<DateTime> T072_FECINIPREV, Nullable<DateTime> T072_FECFINPREV, Nullable<DateTime> T072_FECINIREAL, Nullable<DateTime> T072_FECFINREAL, string T072_CAUSA, string T072_INTERVENCION, string T072_CONSIDERACION, short T072_AVANCE, byte T072_RESULTADO, int T072_USUMODIF)
        {
            SqlParameter[] aParam = new SqlParameter[14];
            aParam[0] = new SqlParameter("@T072_IDTAREA", SqlDbType.Int, 4);
            aParam[0].Value = T072_IDTAREA;
            aParam[1] = new SqlParameter("@T044_IDDEFICIENCIA", SqlDbType.Int, 4);
            aParam[1].Value = T044_IDDEFICIENCIA;
            aParam[2] = new SqlParameter("@T072_DENOMINACION", SqlDbType.Text, 100);
            aParam[2].Value = T072_DENOMINACION;
            aParam[3] = new SqlParameter("@T072_DESCRIPCION", SqlDbType.Text, 2147483647);
            aParam[3].Value = T072_DESCRIPCION;
            aParam[4] = new SqlParameter("@T072_FECINIPREV", SqlDbType.SmallDateTime, 4);
            aParam[4].Value = T072_FECINIPREV;
            aParam[5] = new SqlParameter("@T072_FECFINPREV", SqlDbType.SmallDateTime, 4);
            aParam[5].Value = T072_FECFINPREV;
            aParam[6] = new SqlParameter("@T072_FECINIREAL", SqlDbType.SmallDateTime, 4);
            aParam[6].Value = T072_FECINIREAL;
            aParam[7] = new SqlParameter("@T072_FECFINREAL", SqlDbType.SmallDateTime, 4);
            aParam[7].Value = T072_FECFINREAL;
            aParam[8] = new SqlParameter("@T072_CAUSA", SqlDbType.Text, 2147483647);
            aParam[8].Value = T072_CAUSA;
            aParam[9] = new SqlParameter("@T072_INTERVENCION", SqlDbType.Text, 2147483647);
            aParam[9].Value = T072_INTERVENCION;
            aParam[10] = new SqlParameter("@T072_CONSIDERACION", SqlDbType.Text, 2147483647);
            aParam[10].Value = T072_CONSIDERACION;
            aParam[11] = new SqlParameter("@T072_AVANCE", SqlDbType.SmallInt, 2);
            aParam[11].Value = T072_AVANCE;
            aParam[12] = new SqlParameter("@T072_RESULTADO", SqlDbType.TinyInt, 1);
            aParam[12].Value = T072_RESULTADO;
            aParam[13] = new SqlParameter("@T072_USUMODIF", SqlDbType.Int, 4);
            aParam[13].Value = T072_USUMODIF;

            // Ejecuta la query y devuelve el numero de registros modificados.
            int returnValue;
            if (tr == null)
                returnValue = SqlHelper.ExecuteNonQuery("GESTAR_TAREA_U", aParam);
            else
                returnValue = SqlHelper.ExecuteNonQueryTransaccion(tr, "GESTAR_TAREA_U", aParam);

            return returnValue;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina un registro de la tabla T072_TAREA a traves de la primary key.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	03/09/2007 14:07:32
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Delete(SqlTransaction tr, int T072_IDTAREA)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@T072_IDTAREA", SqlDbType.Int, 4);
            aParam[0].Value = T072_IDTAREA;

            int returnValue;
            if (tr == null)
                returnValue = SqlHelper.ExecuteNonQuery("GESTAR_TAREA_D", aParam);
            else
                returnValue = SqlHelper.ExecuteNonQueryTransaccion(tr, "GESTAR_TAREA_D", aParam);

            return returnValue;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un registro de la tabla T072_TAREA,
        /// y devuelve una instancia u objeto del tipo TAREA
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	03/09/2007 14:07:32
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Select(SqlTransaction tr, int T072_IDTAREA)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@T072_IDTAREA", SqlDbType.Int, 4);
            aParam[0].Value = T072_IDTAREA;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("GESTAR_TAREA_S", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GESTAR_TAREA_S", aParam);

            return dr;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un catálogo de registros de la tabla T072_TAREA.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	03/09/2007 14:07:32
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Catalogo(
                                    Nullable<int> T044_IDDEFICIENCIA,
                                    byte nOrden, 
                                    byte nAscDesc,
                                    Nullable<int> IDFICEPI
                                    //Nullable<byte> T072_RESULTADO, 
                                    //Nullable<DateTime> strFechaInicio, 
                                    //Nullable<DateTime> strFechaFin
                                    )
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@IDDEFICIENCIA", SqlDbType.Int, 4);
            aParam[0].Value = T044_IDDEFICIENCIA;
            aParam[1] = new SqlParameter("@INTCOLUMNA", SqlDbType.TinyInt, 1);
            aParam[1].Value = nOrden;
            aParam[2] = new SqlParameter("@INTORDEN", SqlDbType.TinyInt, 1);
            aParam[2].Value = nAscDesc;
            aParam[3] = new SqlParameter("@IDFICEPI", SqlDbType.Int, 4);
            aParam[3].Value = IDFICEPI;

            //aParam[3] = new SqlParameter("@RESULTADO", SqlDbType.TinyInt, 1);
            //aParam[3].Value = T072_RESULTADO;
            //aParam[4] = new SqlParameter("@FECHA_INICIO", SqlDbType.SmallDateTime, 4);
            //aParam[4].Value = strFechaInicio;
            //aParam[5] = new SqlParameter("@FECHA_FIN", SqlDbType.SmallDateTime, 4);
            //aParam[5].Value = strFechaFin;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("GESTAR_DEFI_TAREAS", aParam);

            return dr;
        }
        public static SqlDataReader LeerTecnicosTarea(Nullable<int> intIdTarea)
        {
            SqlParameter[] aParam = new SqlParameter[4];

            aParam[0] = new SqlParameter("@CASO", SqlDbType.Char, 1);
            aParam[0].Value = "T";
            aParam[1] = new SqlParameter("@T042_IDAREA", SqlDbType.Int, 4);
            aParam[1].Value = null;
            aParam[2] = new SqlParameter("@T072_IDTAREA", SqlDbType.Int, 4);
            aParam[2].Value = intIdTarea;
            aParam[3] = new SqlParameter("@T001_IDFICEPI", SqlDbType.Int, 4);
            aParam[3].Value = null;

            // Ejecuta la query y devuelve un SqlDataReader con el resultado.
            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("GESTAR_TECNICO", aParam);

            return dr;
        }
        public static SqlDataReader TareasPropias
                                                (
                                                    int iIDArea,
                                                    short sSituacion,
                                                    short sResultado,
                                                    string sFecha,
                                                    string sTramitadas,
                                                    string PdteAclara,
                                                    string sAclaRta,
                                                    string sAceptadas,
                                                    string sRechazadas,
                                                    string sEnEstudio,
                                                    string sPdtesDeResolucion,
                                                    string sPdtesDeAcepPpta,
                                                    string sEnResolucion,
                                                    string sResueltas,
                                                    string sNoAprobadas,
                                                    int iIDFICEPI,
                                                    string strAdmin,
                                                    byte byteColum,
                                                    byte byteOrden 
                                               )
        {
            if (strAdmin == "A") iIDFICEPI = 0;

            SqlDataReader dr;

            if (sFecha == "") sFecha = "01-01-1900";

            dr = SqlHelper.ExecuteSqlDataReader(Utilidades.Conexion(),
                    "GESTAR_CAT_TAREAS_PROPIAS", iIDFICEPI, iIDArea, sSituacion, sResultado, sFecha,
                    sTramitadas, PdteAclara, sAclaRta, sAceptadas, sRechazadas, sEnEstudio, sPdtesDeResolucion,
                    sPdtesDeAcepPpta, sEnResolucion, sResueltas, sNoAprobadas,                    
                    byteColum, byteOrden   
                    );
            return dr;
        }
        public static SqlDataReader Avanzado
                                        (
                                            int iIDArea,
                                            int iDeficiencia,
                                            int iCoordinador,
                                            int iEspecialista,
                                            short sSituacion,
                                            short sResultado,
                                            short shOpcion,
                                            string sFechaInicio,
                                            string sFechaFin,
                                            string sTramitadas,
                                            string PdteAclara,
                                            string sAclaRta,
                                            string sAceptadas,
                                            string sRechazadas,
                                            string sEnEstudio,
                                            string sPdtesDeResolucion,
                                            string sPdtesDeAcepPpta,
                                            string sEnResolucion,
                                            string sResueltas,
                                            string sNoAprobadas,
                                            int iIDFICEPI,
                                            string strAdmin,
                                            byte byteColum,
                                            byte byteOrden 
                                       )
        {
            if (strAdmin == "A") iIDFICEPI = 0;

            SqlDataReader dr;

            if (sFechaInicio == "") sFechaInicio = "01-01-1900";
            if (sFechaFin == "") sFechaFin = "31-12-2099";

            dr = SqlHelper.ExecuteSqlDataReader(Utilidades.Conexion(),
                    "GESTAR_CAT_TAREAS_AVANZADO", shOpcion,iIDFICEPI, iIDArea, iDeficiencia, iCoordinador, iEspecialista, sSituacion, sResultado, sFechaInicio, sFechaFin,
                    sTramitadas, PdteAclara, sAclaRta, sAceptadas, sRechazadas, sEnEstudio, sPdtesDeResolucion,
                    sPdtesDeAcepPpta, sEnResolucion, sResueltas, sNoAprobadas, byteColum, byteOrden  
                    );
            return dr;
        }
        public static SqlDataReader Volcar
                                        (
                                            short shOpcion,
                                            int iIDArea,
                                            int iDeficiencia,
                                            int iCoordinador,
                                            int iEspecialista,
                                            short sSituacion,
                                            short sResultado,
                                            string sFechaInicio,
                                            string sFechaFin,
                                            string sTramitadas,
                                            string PdteAclara,
                                            string sAclaRta,
                                            string sAceptadas,
                                            string sRechazadas,
                                            string sEnEstudio,
                                            string sPdtesDeResolucion,
                                            string sPdtesDeAcepPpta,
                                            string sEnResolucion,
                                            string sResueltas,
                                            string sNoAprobadas,
                                            int iIDFICEPI,
                                            string strAdmin
                                       )
        {
            if (strAdmin == "A") iIDFICEPI = 0;

            SqlDataReader dr;

            if (sFechaInicio == "") sFechaInicio = "01-01-1900";
            if (sFechaFin == "") sFechaFin = "31-12-2099";

            dr = SqlHelper.ExecuteSqlDataReader(Utilidades.Conexion(),
                    "GESTAR_CAT_TAREAS_AVANZADO_VOL", shOpcion, iIDFICEPI, iIDArea, iDeficiencia, iCoordinador, iEspecialista, sSituacion, sResultado, sFechaInicio, sFechaFin,
                    sTramitadas, PdteAclara, sAclaRta, sAceptadas, sRechazadas, sEnEstudio, sPdtesDeResolucion,
                    sPdtesDeAcepPpta, sEnResolucion, sResueltas, sNoAprobadas                    
                    );
            return dr;
        }
        #endregion
    }
}
