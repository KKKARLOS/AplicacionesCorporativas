using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using GESTAR.Capa_Datos;

namespace GESTAR.Capa_Negocio
{
	/// <summary>
	/// Descripción breve de Recursos.
	/// </summary>
    public class Deficiencias
	{
		public Deficiencias()
		{
			//
			// TODO: agregar aquí la lógica del constructor
			//
		}

        public static SqlDataReader LeerCatalogoDeficiencias(int intIDFICEPI, string strIDArea, int intColumna, int intOrden, string strSituacion, string strFechaInicio, string strFechaFin, string strAdmin, string strCoordinador, string strSolicitante, string strEspecialista, int intFiltroCoordinador)
		{
			if (strAdmin=="A") intIDFICEPI=0;

			SqlDataReader drTareas;

            if (strFechaInicio == "") strFechaInicio = "01-01-1900";
	        if (strFechaFin=="") strFechaFin= "31-12-2099";

            if (intFiltroCoordinador!=0) intIDFICEPI = intFiltroCoordinador;
			drTareas = SqlHelper.ExecuteSqlDataReader(Utilidades.Conexion(),
                    "GESTAR_CATALOGODEFICI", intIDFICEPI, int.Parse(strIDArea), intColumna, intOrden, strSituacion, strFechaInicio, strFechaFin, strCoordinador, strSolicitante, strEspecialista, intFiltroCoordinador);
			return drTareas;
		}
 
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T044_DEFICIENCIA.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	09/08/2007 10:38:39
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Insert
                (
                SqlTransaction tr, int T042_IDAREA, byte T044_ESTADO, string T044_ASUNTO, 
                string T044_DESCRIPCION, byte T044_IMPORTANCIA, byte T044_PRIORIDAD, short T044_AVANCE, 
                Nullable<DateTime> T044_FECLIMITE, Nullable<DateTime> T044_FECPACT, string T044_OBSERVACION,
                byte T044_UNIDADESTIMA, Nullable<float> T044_TIEMPOESTI, Nullable<DateTime> T044_FINIPREV,
                Nullable<DateTime> T044_FFINPREV, Nullable<float> T044_TIEMPOINVER, 
                Nullable<DateTime> T044_FECINIREAL, Nullable<DateTime> T044_FECFINREAL,
                Nullable<DateTime> T044_NOTIFICADA, Nullable<int> T044_COORDINADOR, 
                Nullable<int> T044_SOLICITANTE, string T044_CLIENTE, Nullable<short> T074_IDENTRADA, 
                string T044_CR, Nullable<int> T076_IDTIPO, Nullable<int> T077_IDALCANCE, 
                Nullable<int> T078_IDPROCESO, Nullable<int> T079_IDPRODUCTO, string T044_PROVEEDOR, 
                Nullable<int> T081_IDREQUISITO, Nullable<int> T082_CAUSA_CAT, string T044_CAUSA,
                string T044_RESULTADO, byte T044_RTDO, int T044_USUMODIF, string T044_SOLACLARA, 
                string T044_ACLARA
                )
        {
            SqlParameter[] aParam = new SqlParameter[36];
            aParam[0] = new SqlParameter("@T042_IDAREA", SqlDbType.Int, 4);
            aParam[0].Value = T042_IDAREA;
            aParam[1] = new SqlParameter("@T044_ESTADO", SqlDbType.TinyInt, 1);
            aParam[1].Value = T044_ESTADO;
            aParam[2] = new SqlParameter("@T044_ASUNTO", SqlDbType.Text, 60);
            aParam[2].Value = T044_ASUNTO;
            aParam[3] = new SqlParameter("@T044_DESCRIPCION", SqlDbType.Text, 2147483647);
            aParam[3].Value = T044_DESCRIPCION;
            aParam[4] = new SqlParameter("@T044_IMPORTANCIA", SqlDbType.TinyInt, 1);
            aParam[4].Value = T044_IMPORTANCIA;
            aParam[5] = new SqlParameter("@T044_PRIORIDAD", SqlDbType.TinyInt, 1);
            aParam[5].Value = T044_PRIORIDAD;
            aParam[6] = new SqlParameter("@T044_AVANCE", SqlDbType.SmallInt, 2);
            aParam[6].Value = T044_AVANCE;
            aParam[7] = new SqlParameter("@T044_FECLIMITE", SqlDbType.SmallDateTime, 4);
            aParam[7].Value = T044_FECLIMITE;
            aParam[8] = new SqlParameter("@T044_FECPACT", SqlDbType.SmallDateTime, 4);
            aParam[8].Value = T044_FECPACT;
            aParam[9] = new SqlParameter("@T044_OBSERVACION", SqlDbType.Text, 2147483647);
            aParam[9].Value = T044_OBSERVACION;
            aParam[10] = new SqlParameter("@T044_UNIDADESTIMA", SqlDbType.TinyInt, 1);
            aParam[10].Value = T044_UNIDADESTIMA;
            aParam[11] = new SqlParameter("@T044_TIEMPOESTI", SqlDbType.Real, 4);
            aParam[11].Value = T044_TIEMPOESTI;
            aParam[12] = new SqlParameter("@T044_FINIPREV", SqlDbType.SmallDateTime, 4);
            aParam[12].Value = T044_FINIPREV;
            aParam[13] = new SqlParameter("@T044_FFINPREV", SqlDbType.SmallDateTime, 4);
            aParam[13].Value = T044_FFINPREV;
            aParam[14] = new SqlParameter("@T044_TIEMPOINVER", SqlDbType.Real, 4);
            aParam[14].Value = T044_TIEMPOINVER;
            aParam[15] = new SqlParameter("@T044_FECINIREAL", SqlDbType.SmallDateTime, 4);
            aParam[15].Value = T044_FECINIREAL;
            aParam[16] = new SqlParameter("@T044_FECFINREAL", SqlDbType.SmallDateTime, 4);
            aParam[16].Value = T044_FECFINREAL;
            aParam[17] = new SqlParameter("@T044_NOTIFICADA", SqlDbType.SmallDateTime, 4);
            aParam[17].Value = T044_NOTIFICADA;
            aParam[18] = new SqlParameter("@T044_COORDINADOR", SqlDbType.Int, 4);
            aParam[18].Value = T044_COORDINADOR;
            aParam[19] = new SqlParameter("@T044_SOLICITANTE", SqlDbType.Int, 4);
            aParam[19].Value = T044_SOLICITANTE;
            aParam[20] = new SqlParameter("@T044_CLIENTE", SqlDbType.VarChar, 100);
            aParam[20].Value = T044_CLIENTE;
            aParam[21] = new SqlParameter("@T074_IDENTRADA", SqlDbType.SmallInt, 2);
            aParam[21].Value = T074_IDENTRADA;
            aParam[22] = new SqlParameter("@T044_CR", SqlDbType.VarChar, 40);
            aParam[22].Value = T044_CR;
            aParam[23] = new SqlParameter("@T076_IDTIPO", SqlDbType.Int, 4);
            aParam[23].Value = T076_IDTIPO;
            aParam[24] = new SqlParameter("@T077_IDALCANCE", SqlDbType.Int, 4);
            aParam[24].Value = T077_IDALCANCE;
            aParam[25] = new SqlParameter("@T078_IDPROCESO", SqlDbType.Int, 4);
            aParam[25].Value = T078_IDPROCESO;
            aParam[26] = new SqlParameter("@T079_IDPRODUCTO", SqlDbType.Int, 4);
            aParam[26].Value = T079_IDPRODUCTO;
            aParam[27] = new SqlParameter("@T044_PROVEEDOR", SqlDbType.VarChar, 100);
            aParam[27].Value = T044_PROVEEDOR;
            aParam[28] = new SqlParameter("@T081_IDREQUISITO", SqlDbType.Int, 4);
            aParam[28].Value = T081_IDREQUISITO;
            aParam[29] = new SqlParameter("@T082_CAUSA_CAT", SqlDbType.Int, 4);
            aParam[29].Value = T082_CAUSA_CAT;
            aParam[30] = new SqlParameter("@T044_CAUSA", SqlDbType.Text, 2147483647);
            aParam[30].Value = T044_CAUSA;
            aParam[31] = new SqlParameter("@T044_RESULTADO", SqlDbType.Text, 2147483647);
            aParam[31].Value = T044_RESULTADO;
            aParam[32] = new SqlParameter("@T044_RTDO", SqlDbType.TinyInt, 1);
            aParam[32].Value = T044_RTDO;
            aParam[33] = new SqlParameter("@T044_USUMODIF", SqlDbType.Int, 4);
            aParam[33].Value = T044_USUMODIF;
            aParam[34] = new SqlParameter("@T044_SOLACLARA", SqlDbType.Text, 2147483647);
            aParam[34].Value = T044_SOLACLARA;
            aParam[35] = new SqlParameter("@T044_ACLARA", SqlDbType.Text, 2147483647);
            aParam[35].Value = T044_ACLARA;


            // Ejecuta la query y devuelve el valor del nuevo Identity.
            int returnValue;
            if (tr == null)
                returnValue = Convert.ToInt32(SqlHelper.ExecuteScalar("GESTAR_DEFICIENCIA_I", aParam));
            else
                returnValue = Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "GESTAR_DEFICIENCIA_I", aParam));
            return returnValue;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza un registro de la tabla T044_DEFICIENCIA.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	09/08/2007 10:38:39
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Update
                   (
                    SqlTransaction tr, int T044_IDDEFICIENCIA, int T042_IDAREA, byte T044_ESTADO, byte T044_ESTADO_ANT, 
                    string T044_ASUNTO, string T044_DESCRIPCION, byte T044_IMPORTANCIA, byte T044_PRIORIDAD, 
                    short T044_AVANCE, Nullable<DateTime> T044_FECLIMITE, Nullable<DateTime> T044_FECPACT, 
                    string T044_OBSERVACION, byte T044_UNIDADESTIMA, Nullable<float> T044_TIEMPOESTI, 
                    Nullable<DateTime> T044_FINIPREV, Nullable<DateTime> T044_FFINPREV, 
                    Nullable<float> T044_TIEMPOINVER, Nullable<DateTime> T044_FECINIREAL, 
                    Nullable<DateTime> T044_FECFINREAL, Nullable<DateTime> T044_NOTIFICADA, 
                    Nullable<int> T044_COORDINADOR, Nullable<int> T044_SOLICITANTE, 
                    string T044_CLIENTE, Nullable<short> T074_IDENTRADA, string T044_CR, 
                    Nullable<int> T076_IDTIPO, Nullable<int> T077_IDALCANCE, Nullable<int> T078_IDPROCESO, 
                    Nullable<int> T079_IDPRODUCTO, string T044_PROVEEDOR, Nullable<int> T081_IDREQUISITO, 
                    Nullable<int> T082_CAUSA_CAT, string T044_CAUSA, string T044_RESULTADO, byte T044_RTDO,
                    string T096_MOTIVO, int T044_USUMODIF, string T044_SOLACLARA, string T044_ACLARA, string T044_PRUEBAS
                    )
        {

            SqlParameter[] aParam = new SqlParameter[40];
            aParam[0] = new SqlParameter("@T044_IDDEFICIENCIA", SqlDbType.Int, 4);
            aParam[0].Value = T044_IDDEFICIENCIA;
            aParam[1] = new SqlParameter("@T042_IDAREA", SqlDbType.Int, 4);
            aParam[1].Value = T042_IDAREA;
            aParam[2] = new SqlParameter("@T044_ESTADO", SqlDbType.TinyInt, 1);
            aParam[2].Value = T044_ESTADO;
            aParam[3] = new SqlParameter("@T044_ESTADO_ANT", SqlDbType.TinyInt, 1);
            aParam[3].Value = T044_ESTADO_ANT;
            aParam[4] = new SqlParameter("@T044_ASUNTO", SqlDbType.Text, 60);
            aParam[4].Value = T044_ASUNTO;
            aParam[5] = new SqlParameter("@T044_DESCRIPCION", SqlDbType.Text, 2147483647);
            aParam[5].Value = T044_DESCRIPCION;
            aParam[6] = new SqlParameter("@T044_IMPORTANCIA", SqlDbType.TinyInt, 1);
            aParam[6].Value = T044_IMPORTANCIA;
            aParam[7] = new SqlParameter("@T044_PRIORIDAD", SqlDbType.TinyInt, 1);
            aParam[7].Value = T044_PRIORIDAD;
            aParam[8] = new SqlParameter("@T044_AVANCE", SqlDbType.SmallInt, 2);
            aParam[8].Value = T044_AVANCE;
            aParam[9] = new SqlParameter("@T044_FECLIMITE", SqlDbType.SmallDateTime, 4);
            aParam[9].Value = T044_FECLIMITE;
            aParam[10] = new SqlParameter("@T044_FECPACT", SqlDbType.SmallDateTime, 4);
            aParam[10].Value = T044_FECPACT;
            aParam[11] = new SqlParameter("@T044_OBSERVACION", SqlDbType.Text, 2147483647);
            aParam[11].Value = T044_OBSERVACION;
            aParam[12] = new SqlParameter("@T044_UNIDADESTIMA", SqlDbType.TinyInt, 1);
            aParam[12].Value = T044_UNIDADESTIMA;
            aParam[13] = new SqlParameter("@T044_TIEMPOESTI", SqlDbType.Real, 4);
            aParam[13].Value = T044_TIEMPOESTI;
            aParam[14] = new SqlParameter("@T044_FINIPREV", SqlDbType.SmallDateTime, 4);
            aParam[14].Value = T044_FINIPREV;
            aParam[15] = new SqlParameter("@T044_FFINPREV", SqlDbType.SmallDateTime, 4);
            aParam[15].Value = T044_FFINPREV;
            aParam[16] = new SqlParameter("@T044_TIEMPOINVER", SqlDbType.Real, 4);
            aParam[16].Value = T044_TIEMPOINVER;
            aParam[17] = new SqlParameter("@T044_FECINIREAL", SqlDbType.SmallDateTime, 4);
            aParam[17].Value = T044_FECINIREAL;
            aParam[18] = new SqlParameter("@T044_FECFINREAL", SqlDbType.SmallDateTime, 4);
            aParam[18].Value = T044_FECFINREAL;
            aParam[19] = new SqlParameter("@T044_NOTIFICADA", SqlDbType.SmallDateTime, 4);
            aParam[19].Value = T044_NOTIFICADA;
            aParam[20] = new SqlParameter("@T044_COORDINADOR", SqlDbType.Int, 4);
            aParam[20].Value = T044_COORDINADOR;
            aParam[21] = new SqlParameter("@T044_SOLICITANTE", SqlDbType.Int, 4);
            aParam[21].Value = T044_SOLICITANTE;
            aParam[22] = new SqlParameter("@T044_CLIENTE", SqlDbType.VarChar, 100);
            aParam[22].Value = T044_CLIENTE;
            aParam[23] = new SqlParameter("@T074_IDENTRADA", SqlDbType.SmallInt, 2);
            aParam[23].Value = T074_IDENTRADA;
            aParam[24] = new SqlParameter("@T044_CR", SqlDbType.VarChar, 40);
            aParam[24].Value = T044_CR;
            aParam[25] = new SqlParameter("@T076_IDTIPO", SqlDbType.Int, 4);
            aParam[25].Value = T076_IDTIPO;
            aParam[26] = new SqlParameter("@T077_IDALCANCE", SqlDbType.Int, 4);
            aParam[26].Value = T077_IDALCANCE;
            aParam[27] = new SqlParameter("@T078_IDPROCESO", SqlDbType.Int, 4);
            aParam[27].Value = T078_IDPROCESO;
            aParam[28] = new SqlParameter("@T079_IDPRODUCTO", SqlDbType.Int, 4);
            aParam[28].Value = T079_IDPRODUCTO;
            aParam[29] = new SqlParameter("@T044_PROVEEDOR", SqlDbType.VarChar, 100);
            aParam[29].Value = T044_PROVEEDOR;
            aParam[30] = new SqlParameter("@T081_IDREQUISITO", SqlDbType.Int, 4);
            aParam[30].Value = T081_IDREQUISITO;
            aParam[31] = new SqlParameter("@T082_CAUSA_CAT", SqlDbType.Int, 4);
            aParam[31].Value = T082_CAUSA_CAT;
            aParam[32] = new SqlParameter("@T044_CAUSA", SqlDbType.Text, 2147483647);
            aParam[32].Value = T044_CAUSA;
            aParam[33] = new SqlParameter("@T044_RESULTADO", SqlDbType.Text, 2147483647);
            aParam[33].Value = T044_RESULTADO;
            aParam[34] = new SqlParameter("@T044_RTDO", SqlDbType.TinyInt, 1);
            aParam[34].Value = T044_RTDO;
            aParam[35] = new SqlParameter("@T096_MOTIVO", SqlDbType.Text, 2147483647);
            aParam[35].Value = T096_MOTIVO;
            aParam[36] = new SqlParameter("@T044_USUMODIF", SqlDbType.Int, 4);
            aParam[36].Value = T044_USUMODIF;
            aParam[37] = new SqlParameter("@T044_SOLACLARA", SqlDbType.Text, 2147483647);
            aParam[37].Value = T044_SOLACLARA;
            aParam[38] = new SqlParameter("@T044_ACLARA", SqlDbType.Text, 2147483647);
            aParam[38].Value = T044_ACLARA;
            aParam[39] = new SqlParameter("@T044_PRUEBAS", SqlDbType.Text, 2147483647);
            aParam[39].Value = T044_PRUEBAS;

            // Ejecuta la query y devuelve el numero de registros modificados.
            int returnValue;
            if (tr == null)
                returnValue = SqlHelper.ExecuteNonQuery("GESTAR_DEFICIENCIA_U", aParam);
            else
                returnValue = SqlHelper.ExecuteNonQueryTransaccion(tr, "GESTAR_DEFICIENCIA_U", aParam);

            return returnValue;
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Actualiza un registro de la tabla T044_DEFICIENCIA.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	09/08/2007 10:38:39
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int UpdateCoordi(SqlTransaction tr, int T044_IDDEFICIENCIA, Nullable<int> T044_COORDINADOR)
        {

            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@T044_IDDEFICIENCIA", SqlDbType.Int, 4);
            aParam[0].Value = T044_IDDEFICIENCIA;
            aParam[1] = new SqlParameter("@T044_COORDINADOR", SqlDbType.Int, 4);
            aParam[1].Value = T044_COORDINADOR;

            // Ejecuta la query y devuelve el numero de registros modificados.
            int returnValue;
            if (tr == null)
                returnValue = SqlHelper.ExecuteNonQuery("GESTAR_DEFICIENCIA_COORD", aParam);
            else
                returnValue = SqlHelper.ExecuteNonQueryTransaccion(tr, "GESTAR_DEFICIENCIA_COORD", aParam);

            return returnValue;
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina un registro de la tabla T044_DEFICIENCIA a traves de la primary key.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	09/08/2007 10:38:39
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Delete(SqlTransaction tr, int T044_IDDEFICIENCIA)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@T044_IDDEFICIENCIA", SqlDbType.Int, 4);
            aParam[0].Value = T044_IDDEFICIENCIA;

            int returnValue;
            if (tr == null)
                returnValue = SqlHelper.ExecuteNonQuery("GESTAR_DEFICIENCIA_D", aParam);
            else
                returnValue = SqlHelper.ExecuteNonQueryTransaccion(tr, "GESTAR_DEFICIENCIA_D", aParam);

            return returnValue;
        }
        public static int Abrir(SqlTransaction tr, int T044_IDDEFICIENCIA, Nullable<int> T001_IDFICEPI)
        {

            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@T044_IDDEFICIENCIA", SqlDbType.Int, 4);
            aParam[0].Value = T044_IDDEFICIENCIA;
            aParam[1] = new SqlParameter("@T001_IDFICEPI", SqlDbType.Int, 4);
            aParam[1].Value = T001_IDFICEPI;

            // Ejecuta la query y devuelve el numero de registros modificados.
            int returnValue;
            if (tr == null)
                returnValue = SqlHelper.ExecuteNonQuery("GESTAR_DEFICIENCIA_ABR", aParam);
            else
                returnValue = SqlHelper.ExecuteNonQueryTransaccion(tr, "GESTAR_DEFICIENCIA_ABR", aParam);

            return returnValue;
        }
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Obtiene un registro de la tabla T044_DEFICIENCIA,
        /// y devuelve una orden u objeto del tipo DEFICIENCIA
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	09/08/2007 10:38:39
        /// </history>
        /// -----------------------------------------------------------------------------
        public static SqlDataReader Select(SqlTransaction tr, int T044_IDDEFICIENCIA)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@T044_IDDEFICIENCIA", SqlDbType.Int, 4);
            aParam[0].Value = T044_IDDEFICIENCIA;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("GESTAR_DEFICIENCIA_S", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GESTAR_DEFICIENCIA_S", aParam);

            return dr;
        }
        public static SqlDataReader EsTecnicoDeficenciaTarea(SqlTransaction tr, int T044_IDDEFICIENCIA, int T001_IDFICEPI)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@T044_IDDEFICIENCIA", SqlDbType.Int, 4);
            aParam[0].Value = T044_IDDEFICIENCIA;
            aParam[1] = new SqlParameter("@T001_IDFICEPI", SqlDbType.Int, 4);
            aParam[1].Value = T001_IDFICEPI;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("GESTAR_DEFICIENCIA_T", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GESTAR_DEFICIENCIA_T", aParam);

            return dr;
        }
        public static SqlDataReader EspecialistasEnTareas(SqlTransaction tr, int T044_IDDEFICIENCIA)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@T044_IDDEFICIENCIA", SqlDbType.Int, 4);
            aParam[0].Value = T044_IDDEFICIENCIA;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("GESTAR_DEFICIENCIA_E", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "GESTAR_DEFICIENCIA_E", aParam);

            return dr;
        }
        public static SqlDataReader BusqAvanzaDefi
                                                (   
                                                    short shImportancia, 
                                                    short shPrioridad,
                                                    short shRtado,
                                                    int iIDArea,
                                                    int iEntrada,
                                                    int iTipo,
                                                    int iAlcance,
                                                    int iProceso,
                                                    int iProducto,
                                                    int iRequisito,
                                                    int iCausa,
                                                    int iCoordinador,
                                                    int iSolicitante,
                                                    string sFechaInicio,
                                                    string sFechaFin,
                                                    string sProveedor,
                                                    string sCliente,
                                                    string sCR,
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
                                                    string sAprobadas,
                                                    string sAnuladas,
                                                    short sOpLogica,
                                                    string sCaso,
                                                    int iIDFICEPI,
                                                    string strAdmin,
                                                    byte byteColum,
                                                    byte byteOrden
                                               )
        {
            if (strAdmin == "A") iIDFICEPI = 0;

            SqlDataReader drDeficiencias;

            if (sCaso == "A")
            {
                drDeficiencias = SqlHelper.ExecuteSqlDataReader(Utilidades.Conexion(),
                        "GESTAR_CAT_DEFICI_AVANZ_ACT", iIDFICEPI, iIDArea,
                        shImportancia, shPrioridad, shRtado, iEntrada,
                        iTipo, iAlcance, iProceso, iProducto, iRequisito,
                        iCausa, iCoordinador, iSolicitante, sProveedor, sCliente,
                        sCR, sTramitadas, PdteAclara, sAclaRta,
                        sAceptadas, sRechazadas, sEnEstudio, sPdtesDeResolucion,
                        sPdtesDeAcepPpta, sEnResolucion, sResueltas,
                        sNoAprobadas, sAprobadas, sAnuladas, byteColum, byteOrden
                        );
            }
            else
            {
                if (sFechaInicio == "") sFechaInicio = "01-01-1900";
                if (sFechaFin == "") sFechaFin = "31-12-2099";

                string sProced = (sOpLogica == 0) ? "GESTAR_CAT_DEFICI_AVANZ_CRO_OR" : "GESTAR_CAT_DEFICI_AVANZ_CRO_AND";

                drDeficiencias = SqlHelper.ExecuteSqlDataReader(Utilidades.Conexion(),
                        sProced, iIDFICEPI, iIDArea,
                        shImportancia, shPrioridad, shRtado, iEntrada,
                        iTipo, iAlcance, iProceso, iProducto, iRequisito,
                        iCausa, iCoordinador, iSolicitante, sFechaInicio,
                        sFechaFin, sProveedor, sCliente, sCR, sTramitadas, PdteAclara, sAclaRta,
                        sAceptadas, sRechazadas, sEnEstudio, sPdtesDeResolucion,
                        sPdtesDeAcepPpta, sEnResolucion, sResueltas,
                        sNoAprobadas, sAprobadas, sAnuladas, byteColum, byteOrden
                        );
            }
            return drDeficiencias;
        }
        public static SqlDataReader BusqVtoDefi
                                                (
                                                    int iIDArea,
                                                    short shOpcion,
                                                    string sFechaInicio,
                                                    string sFechaFin,
                                                    int iIDFICEPI,
                                                    string strAdmin,
                                                    byte byteColum,
                                                    byte byteOrden
                                               )
        {
            if (strAdmin == "A") iIDFICEPI = 0;

            SqlDataReader drDeficiencias;

            if (sFechaInicio == "") sFechaInicio = "01-01-1900";
            if (sFechaFin == "") sFechaFin = "31-12-2099";

            drDeficiencias = SqlHelper.ExecuteSqlDataReader(Utilidades.Conexion(),
                    "GESTAR_CAT_DEFICI_VTO", shOpcion, iIDFICEPI, iIDArea, sFechaInicio, sFechaFin, byteColum, byteOrden
                    );
            return drDeficiencias;
        }


        public static SqlDataReader ObtenerTareasParaCambios(int t044_iddeficiencia)
        {
            return SqlHelper.ExecuteSqlDataReader(Utilidades.Conexion(),
                "GESTAR_GETTAREA_CAMBIOFECHA", t044_iddeficiencia);
        }

    }
}