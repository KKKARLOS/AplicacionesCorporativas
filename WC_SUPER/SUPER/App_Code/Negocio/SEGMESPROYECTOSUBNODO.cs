using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
	public partial class SEGMESPROYECTOSUBNODO
	{
        #region Propiedades y Atributos

        private string _t301_modelocoste;
        public string t301_modelocoste
        {
            get { return _t301_modelocoste; }
            set { _t301_modelocoste = value; }
        }
        private string _t301_modelotarif;
        public string t301_modelotarif
        {
            get { return _t301_modelotarif; }
            set { _t301_modelotarif = value; }
        }

        #endregion

		#region Metodos

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T325_SEGMESPROYECTOSUBNODO.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	20/04/2009 16:03:19
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Insert(SqlTransaction tr, int t305_idproyectosubnodo, int t325_anomes, string t325_estado, decimal t325_avanceprod, decimal t325_gastosfinancieros, bool t325_traspasoIAP, decimal t325_prodperiod, decimal t325_consperiod)
        {
            SqlParameter[] aParam = new SqlParameter[8];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;
            aParam[1] = new SqlParameter("@t325_anomes", SqlDbType.Int, 4);
            aParam[1].Value = t325_anomes;
            aParam[2] = new SqlParameter("@t325_estado", SqlDbType.Text, 1);
            aParam[2].Value = t325_estado;
            aParam[3] = new SqlParameter("@t325_avanceprod", SqlDbType.Money, 8);
            aParam[3].Value = t325_avanceprod;
            aParam[4] = new SqlParameter("@t325_gastosfinancieros", SqlDbType.Money, 8);
            aParam[4].Value = t325_gastosfinancieros;
            aParam[5] = new SqlParameter("@t325_traspasoIAP", SqlDbType.Bit, 1);
            aParam[5].Value = t325_traspasoIAP;
            aParam[6] = new SqlParameter("@t325_prodperiod", SqlDbType.Money, 8);
            aParam[6].Value = t325_prodperiod;
            aParam[7] = new SqlParameter("@t325_consperiod", SqlDbType.Money, 8);
            aParam[7].Value = t325_consperiod;

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_SEGMESPROYECTOSUBNODO_I", aParam));
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Elimina un registro de la tabla T325_SEGMESPROYECTOSUBNODO a traves de la primary key.
        /// </summary>
        /// <history>
        /// 	Creado por [sqladmin]	20/04/2009 16:03:19
        /// </history>
        /// -----------------------------------------------------------------------------
        public static int Delete(SqlTransaction tr, int t325_idsegmesproy)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t325_idsegmesproy", SqlDbType.Int, 4);
            aParam[0].Value = t325_idsegmesproy;


            if (tr == null)
                return SqlHelper.ExecuteNonQuery("SUP_SEGMESPROYECTOSUBNODO_D", aParam);
            else
                return SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_SEGMESPROYECTOSUBNODO_D", aParam);
        }

        public static int InsertSiNoExiste(SqlTransaction tr, int t305_idproyectosubnodo, int t325_anomes)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;
            aParam[1] = new SqlParameter("@t325_anomes", SqlDbType.Int, 4);
            aParam[1].Value = t325_anomes;
            //aParam[2] = new SqlParameter("@t325_estado", SqlDbType.Text, 1);
            //aParam[2].Value = t325_estado;
            //aParam[3] = new SqlParameter("@t325_avanceprod", SqlDbType.Money, 8);
            //aParam[3].Value = t325_avanceprod;

            // Ejecuta la query y devuelve el valor del nuevo Identity.
            return Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_SEGMESPROYECTOSUBNODO_I_SNE", aParam));
        }

        public static int Cerrar(SqlTransaction tr, int t325_idsegmesproy)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t325_idsegmesproy", SqlDbType.Int, 4);
            aParam[0].Value = t325_idsegmesproy;

            return Convert.ToInt32(SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CIERREMESPROYECTO", aParam));
        }
        public static int CerrarMesADM(SqlTransaction tr, int t305_idproyectosubnodo, int t325_anomes)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;
            aParam[1] = new SqlParameter("@t325_anomes", SqlDbType.Int, 4);
            aParam[1].Value = t325_anomes;

            return Convert.ToInt32(SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CIERREMESPROYECTO_ADM", aParam));
        }
        public static int UpdateEstado(SqlTransaction tr, int t325_idsegmesproy, string sEstado)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t325_idsegmesproy", SqlDbType.Int, 4);
            aParam[0].Value = t325_idsegmesproy;
            aParam[1] = new SqlParameter("@t325_estado", SqlDbType.Text, 1);
            aParam[1].Value = sEstado;

            return Convert.ToInt32(SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_SEGMESPROYECTOSUBNODO_U_ESTADO", aParam));
        }

        public static int UpdateAvanceProduccion(SqlTransaction tr, int t325_idsegmesproy, decimal t325_avanceprod)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t325_idsegmesproy", SqlDbType.Int, 4);
            aParam[0].Value = t325_idsegmesproy;
            aParam[1] = new SqlParameter("@t325_avanceprod", SqlDbType.Money, 8);
            aParam[1].Value = t325_avanceprod;

            return Convert.ToInt32(SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_SEGMESPROYECTOSUBNODO_UAVAN", aParam));
        }
        public static int UpdateGastosFinancieros(SqlTransaction tr, int t325_idsegmesproy, decimal t325_gastosfinancieros)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t325_idsegmesproy", SqlDbType.Int, 4);
            aParam[0].Value = t325_idsegmesproy;
            aParam[1] = new SqlParameter("@t325_gastosfinancieros", SqlDbType.Money, 8);
            aParam[1].Value = t325_gastosfinancieros;

            return Convert.ToInt32(SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_SEGMESPROYECTOSUBNODO_UGF", aParam));
        }
        public static int UpdateGastosFinancierosByPSNAnomes(SqlTransaction tr, int t305_idproyectosubnodo, int t325_anomes, decimal t325_gastosfinancieros)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;
            aParam[1] = new SqlParameter("@t325_anomes", SqlDbType.Int, 4);
            aParam[1].Value = t325_anomes;
            aParam[2] = new SqlParameter("@t325_gastosfinancieros", SqlDbType.Money, 8);
            aParam[2].Value = t325_gastosfinancieros;

            return Convert.ToInt32(SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_SEGMESPROYECTOSUBNODO_UGF_ByPSNAnomes", aParam));
        }
        public static int UpdateProduccionPeriodificacion(SqlTransaction tr, int t325_idsegmesproy, decimal t325_prodperiod)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t325_idsegmesproy", SqlDbType.Int, 4);
            aParam[0].Value = t325_idsegmesproy;
            aParam[1] = new SqlParameter("@t325_prodperiod", SqlDbType.Money, 8);
            aParam[1].Value = t325_prodperiod;

            return Convert.ToInt32(SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_SEGMESPROYECTOSUBNODO_UPP", aParam));
        }
        public static int UpdateConsumoPeriodificacion(SqlTransaction tr, int t325_idsegmesproy, decimal t325_consperiod)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t325_idsegmesproy", SqlDbType.Int, 4);
            aParam[0].Value = t325_idsegmesproy;
            aParam[1] = new SqlParameter("@t325_consperiod", SqlDbType.Money, 8);
            aParam[1].Value = t325_consperiod;

            return Convert.ToInt32(SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_SEGMESPROYECTOSUBNODO_UCP", aParam));
        }
        public static int UpdateProduccionConsumoPeriodificacion(SqlTransaction tr, int t325_idsegmesproy, decimal t325_prodperiod, decimal t325_consperiod)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t325_idsegmesproy", SqlDbType.Int, 4);
            aParam[0].Value = t325_idsegmesproy;
            aParam[1] = new SqlParameter("@t325_prodperiod", SqlDbType.Money, 8);
            aParam[1].Value = t325_prodperiod;
            aParam[2] = new SqlParameter("@t325_consperiod", SqlDbType.Money, 8);
            aParam[2].Value = t325_consperiod;

            return Convert.ToInt32(SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_SEGMESPROYECTOSUBNODO_UPCP", aParam));
        }
        
        public static int UpdateTraspasoIAP(SqlTransaction tr, int t325_idsegmesproy, bool t325_traspasoIAP)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t325_idsegmesproy", SqlDbType.Int, 4);
            aParam[0].Value = t325_idsegmesproy;
            aParam[1] = new SqlParameter("@t325_traspasoIAP", SqlDbType.Bit, 1);
            aParam[1].Value = t325_traspasoIAP;

            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteNonQuery("SUP_SEGMESPROYECTOSUBNODO_UTRAS", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_SEGMESPROYECTOSUBNODO_UTRAS", aParam));
        }

        public static int ExisteSegMesProy(SqlTransaction tr, int t305_idproyectosubnodo, int t325_anomes)
        {
            int nSegMesProyResp = 0;

            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nPSN", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;
            aParam[1] = new SqlParameter("@anomes", SqlDbType.Int, 4);
            aParam[1].Value = t325_anomes;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_EXISTESEGMESPROY", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_EXISTESEGMESPROY", aParam);


            if (dr.Read())
            {
                nSegMesProyResp = (int)dr["t325_idsegmesproy"];
            }

            dr.Close();
            dr.Dispose();

            return nSegMesProyResp;
        }
        public static bool ExisteSegMesProyByID(SqlTransaction tr, int t325_idsegmesproy)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t325_idsegmesproy", SqlDbType.Int, 4);
            aParam[0].Value = t325_idsegmesproy;

            return (Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_EXISTESEGMESPROYByID", aParam))==0)?false:true;
        }

        public static void GenerarMes(SqlTransaction tr, int nProyecto)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nProyecto", SqlDbType.Int, 4);
            aParam[0].Value = nProyecto;

            SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_GENERARMESESENREPLICAS", aParam);
        }
        public static void GenerarMesEnTransaccion(SqlTransaction tr, int nProyecto)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@nProyecto", SqlDbType.Int, 4);
            aParam[0].Value = nProyecto;

            SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_GENERARMESESENREPLICAS_INTR", aParam);
        }
         
        public static SqlDataReader DatosProyectoTecnico(int t305_idproyectosubnodo, DateTime dMSC, int idRecurso, string sEsRtpt, string t422_idmoneda, string sNivelPresupuesto)
        {
            SqlParameter[] aParam = new SqlParameter[5];

            SqlDataReader dr = null;

            int i = 0;
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);
            aParam[i++] = ParametroSql.add("@dMSC", SqlDbType.DateTime, 8, dMSC);
            aParam[i++] = ParametroSql.add("@nRecurso", SqlDbType.Int, 4, idRecurso);
            aParam[i++] = ParametroSql.add("@sEsRtpt", SqlDbType.Char, 2, sEsRtpt);
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);

            switch (sNivelPresupuesto)
            {
                case "P":
                    dr = SqlHelper.ExecuteSqlDataReader("SUP_SEGUIMIENTOPT_PT", aParam);
                    break;
                case "F":
                    dr = SqlHelper.ExecuteSqlDataReader("SUP_SEGUIMIENTOPT_F", aParam);
                    break;
                case "A":
                    dr = SqlHelper.ExecuteSqlDataReader("SUP_SEGUIMIENTOPT_A", aParam);
                    break;
                case "T":
                    dr = SqlHelper.ExecuteSqlDataReader("SUP_SEGUIMIENTOPT_T", aParam);
                    break;
            }

            return dr;
        }
         
        public static SqlDataReader DatosProyectoEconomico(int nEmpleado, DateTime dMSC, int nProyecto, string t422_idmoneda, string sNivelPresupuesto)
        {

            SqlDataReader dr = null;

            SqlParameter[] aParam = new SqlParameter[4];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, nEmpleado);
            aParam[i++] = ParametroSql.add("@dMSC", SqlDbType.DateTime, 8, dMSC);
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, nProyecto);
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);

            switch (sNivelPresupuesto)
            {
                case "P":
                    dr = SqlHelper.ExecuteSqlDataReader("SUP_SEGUIMIENTOPE_PT", aParam);
                    break;
                case "F":
                    dr = SqlHelper.ExecuteSqlDataReader("SUP_SEGUIMIENTOPE_F", aParam);
                    break;
                case "A":
                    dr = SqlHelper.ExecuteSqlDataReader("SUP_SEGUIMIENTOPE_A", aParam);
                    break;
                case "T":
                    dr = SqlHelper.ExecuteSqlDataReader("SUP_SEGUIMIENTOPE_T", aParam);
                    break;
            }

            return dr;            
        }
        /// <summary>
        /// 
        /// Obtiene los datos de seguimiento, a nivel de fases + actividades + tareas.
        /// </summary>
        public static SqlDataReader DatosFaseActivTareas(int nPT, DateTime dMSC, string t422_idmoneda, string sNivelPresupuesto)
        {
            SqlDataReader dr = null;
            SqlParameter[] aParam = new SqlParameter[3];
            int i = 0;
            aParam[i++] = ParametroSql.add("@nPT", SqlDbType.Int, 4, nPT);
            aParam[i++] = ParametroSql.add("@dMSC", SqlDbType.DateTime, 8, dMSC);
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);
            switch (sNivelPresupuesto)
            {
                case "P":
                    dr = SqlHelper.ExecuteSqlDataReader("SUP_SEGUIMIENTOT2_PT", aParam);
                    break;
                case "F":
                    dr = SqlHelper.ExecuteSqlDataReader("SUP_SEGUIMIENTOT2_F", aParam);
                    break;
                case "A":
                    dr = SqlHelper.ExecuteSqlDataReader("SUP_SEGUIMIENTOT2_A", aParam);
                    break;
                case "T":
                    dr = SqlHelper.ExecuteSqlDataReader("SUP_SEGUIMIENTOT2_T", aParam);
                    break;
            }

            return dr;
            //return SqlHelper.ExecuteSqlDataReader("SUP_SEGUIMIENTOT2", aParam);
        }
        /// <summary>
        /// 
        /// Obtiene los datos de seguimiento, a nivel de tareas.
        /// </summary>
        //public static SqlDataReader DatosTarea(int nProyecto, int nPT, DateTime dMSC)
        //{
        //    SqlParameter[] aParam = new SqlParameter[3];
        //    aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
        //    aParam[1] = new SqlParameter("@t331_idpt", SqlDbType.Int, 4);
        //    aParam[2] = new SqlParameter("@dMSC", SqlDbType.DateTime, 8);

        //    aParam[0].Value = nProyecto;
        //    aParam[1].Value = nPT;
        //    aParam[2].Value = dMSC;

        //    return SqlHelper.ExecuteSqlDataReader("SUP_SEGUIMIENTOT", aParam);
        //}
        /// <summary>
        /// 
        /// Obtiene los datos de seguimiento, a nivel de profesionales.
        /// </summary>
        //public static SqlDataReader DatosProfesional(int nTarea, DateTime dMSC)
        //{
        //    SqlParameter[] aParam = new SqlParameter[2];
        //    aParam[0] = new SqlParameter("@num_tarea", SqlDbType.Int, 4);
        //    aParam[1] = new SqlParameter("@dMSC", SqlDbType.DateTime, 8);

        //    aParam[0].Value = nTarea;
        //    aParam[1].Value = dMSC;

        //    return SqlHelper.ExecuteSqlDataReader("SUP_SEGUIMIENTOP", aParam);
        //}


        public static DataSet ObtenerSeguimientoProyectoAEDS(int nPSN, DateTime dIni, DateTime dFin, string t422_idmoneda, int codUsuario, string sNivelPresupuesto)
        {
            DataSet dataSet = null;

            SqlParameter[] aParam = new SqlParameter[6];
            aParam[0] = new SqlParameter("@nPSN", SqlDbType.Int, 4);
            aParam[0].Value = nPSN;
            aParam[1] = new SqlParameter("@InicioMes", SqlDbType.DateTime, 8);
            aParam[1].Value = dIni;
            aParam[2] = new SqlParameter("@FinMes", SqlDbType.DateTime, 8);
            aParam[2].Value = dFin;
            aParam[3] = new SqlParameter("@t422_idmoneda", SqlDbType.VarChar, 5);
            aParam[3].Value = t422_idmoneda;
            aParam[4] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[4].Value = codUsuario;
            aParam[5] = new SqlParameter("@tprofasignados", SqlDbType.Int, 4);
            aParam[5].Value = 0;

            switch (sNivelPresupuesto)
            {
                case "P":
                    dataSet = SqlHelper.ExecuteDataset("SUP_SEGUIMIENTO_AE_PT", aParam);
                    break;
                case "F":
                    dataSet = SqlHelper.ExecuteDataset("SUP_SEGUIMIENTO_AE_F", aParam);
                    break;
                case "A":
                    dataSet = SqlHelper.ExecuteDataset("SUP_SEGUIMIENTO_AE_A", aParam);
                    break;
                case "T":
                    dataSet = SqlHelper.ExecuteDataset("SUP_SEGUIMIENTO_AE_T", aParam);
                    break;
            }

            return dataSet;
            //return SqlHelper.ExecuteDataset("SUP_SEGUIMIENTO_AE", aParam);            
        }
        public static DataSet ObtenerSeguimientoProyectoAEDS_TOT(int nPSN, DateTime dIni, DateTime dFin, string t422_idmoneda, int codUsuario, string sNivelPresupuesto)
        {

            DataSet dataSet = null;

            SqlParameter[] aParam = new SqlParameter[6];
            aParam[0] = new SqlParameter("@nPSN", SqlDbType.Int, 4);
            aParam[0].Value = nPSN;
            aParam[1] = new SqlParameter("@InicioMes", SqlDbType.DateTime, 8);
            aParam[1].Value = dIni;
            aParam[2] = new SqlParameter("@FinMes", SqlDbType.DateTime, 8);
            aParam[2].Value = dFin;
            aParam[3] = new SqlParameter("@t422_idmoneda", SqlDbType.VarChar, 5);
            aParam[3].Value = t422_idmoneda;
            aParam[4] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[4].Value = codUsuario;
            aParam[5] = new SqlParameter("@tprofasignados", SqlDbType.Int, 4);
            aParam[5].Value = 1;

            switch (sNivelPresupuesto)
            {
                case "P":
                    dataSet = SqlHelper.ExecuteDataset("SUP_SEGUIMIENTO_AE_PT", aParam);
                    break;
                case "F":
                    dataSet = SqlHelper.ExecuteDataset("SUP_SEGUIMIENTO_AE_F", aParam);
                    break;
                case "A":
                    dataSet = SqlHelper.ExecuteDataset("SUP_SEGUIMIENTO_AE_A", aParam);
                    break;
                case "T":
                    dataSet = SqlHelper.ExecuteDataset("SUP_SEGUIMIENTO_AE_T", aParam);
                    break;
            }

            return dataSet;
            //return SqlHelper.ExecuteDataset("SUP_SEGUIMIENTO_AE_TOT", aParam);
        }

        public static string InsertarSegMesProy(string nIdProySubNodo, string sDesde, string sHasta)
        {
            string sResul = "";//, sEstadoMes = "";
            int nPrimerMesInsertado = 0;
            StringBuilder sb = new StringBuilder();
            SqlConnection oConn = null;
            SqlTransaction tr = null;

            #region abrir conexión y transacción
            try
            {
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccionSerializable(oConn);
            }
            catch (Exception ex)
            {
                if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
                sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
                return sResul;
            }
            #endregion
            try
            {
                DateTime dDesde = Fechas.AnnomesAFecha(int.Parse(sDesde));
                DateTime dHasta = Fechas.AnnomesAFecha(int.Parse(sHasta));
                while (Fechas.DateDiff("month", dDesde, dHasta) >= 0)
                {
                    //sEstadoMes = SEGMESPROYECTOSUBNODO.EstadoMesACrear(tr, int.Parse(nIdProySubNodo), Fechas.FechaAAnnomes(dDesde));
                    int nAux = SEGMESPROYECTOSUBNODO.InsertSiNoExiste(tr, int.Parse(nIdProySubNodo), Fechas.FechaAAnnomes(dDesde));//, sEstadoMes, 0);
                    if (nAux != 0 && nPrimerMesInsertado == 0) nPrimerMesInsertado = Fechas.FechaAAnnomes(dDesde);
                    dDesde = dDesde.AddMonths(1);
                }

                Conexion.CommitTransaccion(tr);
                sResul = "OK@#@" + nPrimerMesInsertado.ToString();
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                sResul = "Error@#@" + Errores.mostrarError("Error al insertar meses", ex);
            }
            finally
            {
                Conexion.Cerrar(oConn);
            }
            return sResul;
        }
        //public static string EstadoMesACrear(SqlTransaction tr, int t305_idproyectosubnodo, int t325_anomes)
        //{
        //    string sResul = "C";
        //    PROYECTOSUBNODO oPSN = PROYECTOSUBNODO.Obtener(tr, t305_idproyectosubnodo);
        //    if (t325_anomes <= oPSN.t303_ultcierreeco)
        //    {
        //        sResul = "C";
        //        //}else if (SEGMESPROYECTOSUBNODO.HayMesesCerradosPosteriores(tr, t305_idproyectosubnodo, t325_anomes)){
        //        //    sResul = "C";
        //    }
        //    else
        //    {
        //        sResul = "A";
        //    }
        //    return sResul;
        //}
        public static string EstadoMesACrear(SqlTransaction tr, int t305_idproyectosubnodo, int nAnomes)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;
            aParam[1] = new SqlParameter("@nAnomes", SqlDbType.Int, 4);
            aParam[1].Value = nAnomes;

            if (tr == null)
                return SqlHelper.ExecuteScalar("SUP_ESTADOMESACREAR", aParam).ToString();
            else
                return SqlHelper.ExecuteScalarTransaccion(tr, "SUP_ESTADOMESACREAR", aParam).ToString();
        }
        public static bool HayMesesCerradosPosteriores(SqlTransaction tr, int t305_idproyectosubnodo, int t325_anomes)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;
            aParam[1] = new SqlParameter("@t325_anomes", SqlDbType.Int, 4);
            aParam[1].Value = t325_anomes;

            if (tr == null)
                return (Convert.ToInt32(SqlHelper.ExecuteScalar("SUP_HAYMESESCERRADOSPOSTERIORES", aParam)) == 0) ? false : true;
            else
                return (Convert.ToInt32(SqlHelper.ExecuteScalarTransaccion(tr, "SUP_HAYMESESCERRADOSPOSTERIORES", aParam)) == 0) ? false : true;
        }

        /// <summary>
        /// 
        /// Obtiene el id de SEGMESPROYECTOSUBNODO a partir de un anomes y un id de proyecto
        /// </summary>
        public static int getId(int sAnomes, int it301_idproyecto)
        {
            int nSegMesProyResp = 0;

            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nIdProy", SqlDbType.Int, 4);
            aParam[0].Value = it301_idproyecto;
            aParam[1] = new SqlParameter("@anomes", SqlDbType.Int, 4);
            aParam[1].Value = sAnomes;

            //SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("SUP_EXISTESEGMESREP", aParam);
            SqlDataReader dr = SqlHelper.ExecuteSqlDataReader("SUP_SEGMES_GETID", aParam);

            if (dr.Read())
            {
                nSegMesProyResp = (int)dr["t325_idsegmesproy"];
            }

            dr.Close();
            dr.Dispose();

            return nSegMesProyResp;
        }

        /// <summary>
        /// ¡¡ IMPORTANTE !!
        /// Si se van a obtener los datos económicos del mes (avance, consumos periodificados, etc) es
        /// necesario indicar la moneda en la que se quieren visualizar esos datos.
        /// </summary>
        public static SEGMESPROYECTOSUBNODO Obtener(SqlTransaction tr, int t325_idsegmesproy, string t422_idmoneda)
        {
            SEGMESPROYECTOSUBNODO o = new SEGMESPROYECTOSUBNODO();

            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t325_idsegmesproy", SqlDbType.Int, 4, t325_idsegmesproy);
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_SEGMESPROYECTOSUBNODO_SD", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_SEGMESPROYECTOSUBNODO_SD", aParam);

            if (dr.Read())
            {
                if (dr["t325_idsegmesproy"] != DBNull.Value)
                    o.t325_idsegmesproy = int.Parse(dr["t325_idsegmesproy"].ToString());
                if (dr["t305_idproyectosubnodo"] != DBNull.Value)
                    o.t305_idproyectosubnodo = int.Parse(dr["t305_idproyectosubnodo"].ToString());
                if (dr["t325_anomes"] != DBNull.Value)
                    o.t325_anomes = int.Parse(dr["t325_anomes"].ToString());
                if (dr["t325_estado"] != DBNull.Value)
                    o.t325_estado = (string)dr["t325_estado"];
                if (dr["t325_avanceprod"] != DBNull.Value)
                    o.t325_avanceprod = decimal.Parse(dr["t325_avanceprod"].ToString());
                if (dr["t325_gastosfinancieros"] != DBNull.Value)
                    o.t325_gastosfinancieros = decimal.Parse(dr["t325_gastosfinancieros"].ToString());
                if (dr["t325_traspasoIAP"] != DBNull.Value)
                    o.t325_traspasoIAP = (bool)dr["t325_traspasoIAP"];
                if (dr["t325_prodperiod"] != DBNull.Value)
                    o.t325_prodperiod = decimal.Parse(dr["t325_prodperiod"].ToString());
                if (dr["t325_consperiod"] != DBNull.Value)
                    o.t325_consperiod = decimal.Parse(dr["t325_consperiod"].ToString());
                if (dr["t301_modelocoste"] != DBNull.Value)
                    o.t301_modelocoste = (string)dr["t301_modelocoste"];
                if (dr["t301_modelotarif"] != DBNull.Value)
                    o.t301_modelotarif = (string)dr["t301_modelotarif"];
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de SEGMESPROYECTOSUBNODO"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }
        public static SEGMESPROYECTOSUBNODO Obtener(SqlTransaction tr, int t305_idproyectosubnodo, int t325_anomes, string t422_idmoneda)
        {
            SEGMESPROYECTOSUBNODO o = new SEGMESPROYECTOSUBNODO();

            SqlParameter[] aParam = new SqlParameter[3];
            int i = 0;

            aParam[i++] = ParametroSql.add("@nPSN", SqlDbType.Int, 4, t305_idproyectosubnodo);
            aParam[i++] = ParametroSql.add("@anomes", SqlDbType.Int, 4, t325_anomes);
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_SEGMESPROY_PSN", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_SEGMESPROY_PSN", aParam);

            if (dr.Read())
            {
                if (dr["t325_idsegmesproy"] != DBNull.Value)
                    o.t325_idsegmesproy = int.Parse(dr["t325_idsegmesproy"].ToString());
                if (dr["t305_idproyectosubnodo"] != DBNull.Value)
                    o.t305_idproyectosubnodo = int.Parse(dr["t305_idproyectosubnodo"].ToString());
                if (dr["t325_anomes"] != DBNull.Value)
                    o.t325_anomes = int.Parse(dr["t325_anomes"].ToString());
                if (dr["t325_estado"] != DBNull.Value)
                    o.t325_estado = (string)dr["t325_estado"];
                if (dr["t325_avanceprod"] != DBNull.Value)
                    o.t325_avanceprod = decimal.Parse(dr["t325_avanceprod"].ToString());
                if (dr["t325_gastosfinancieros"] != DBNull.Value)
                    o.t325_gastosfinancieros = decimal.Parse(dr["t325_gastosfinancieros"].ToString());
                if (dr["t325_traspasoIAP"] != DBNull.Value)
                    o.t325_traspasoIAP = (bool)dr["t325_traspasoIAP"];
                if (dr["t325_prodperiod"] != DBNull.Value)
                    o.t325_prodperiod = decimal.Parse(dr["t325_prodperiod"].ToString());
                if (dr["t325_consperiod"] != DBNull.Value)
                    o.t325_consperiod = decimal.Parse(dr["t325_consperiod"].ToString());
                if (dr["t301_modelocoste"] != DBNull.Value)
                    o.t301_modelocoste = (string)dr["t301_modelocoste"];
                if (dr["t301_modelotarif"] != DBNull.Value)
                    o.t301_modelotarif = (string)dr["t301_modelotarif"];
            }
            else
            {
                throw (new NullReferenceException("No se ha obtenido ningun dato de SEGMESPROYECTOSUBNODO"));
            }

            dr.Close();
            dr.Dispose();

            return o;
        }        
        public static SqlDataReader ObtenerAjuste(SqlTransaction tr, int t325_idsegmesproy)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t325_idsegmesproy", SqlDbType.Int, 4);
            aParam[0].Value = t325_idsegmesproy;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_GETAJUSTEPROY", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_GETAJUSTEPROY", aParam);
        }
        public static void GenerarMesesEnReplicas()
        {
            SqlConnection oConn = Conexion.Abrir();
            SqlTransaction tr = Conexion.AbrirTransaccionSerializable(oConn);

            try{
                SqlParameter[] aParam = new SqlParameter[0];
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_GENERARMESESENREPLICAS", aParam);
                Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                Conexion.CerrarTransaccion(tr);
                SUPER.DAL.Log.Insertar("Negocio.SEGMESPROYECTOSUBNODO.GenerarMesesEnReplicas: " + ex.Message);
                //throw (new Exception("Error al generar meses en réplicas.", ex.InnerException));
                //Para no perder el tipo de error y poder capturarlo en el método llamante
                throw;
            }
            finally
            {
                Conexion.Cerrar(oConn);
            }
        }

        public static DataSet ObtenerMesesReferenciaParaClonado(SqlTransaction tr, int t305_idproyectosubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;

            return SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_GETMESESREF_CLONADO", aParam);
        }
        public static SqlDataReader ObtenerMesesAbiertosParaBorrado(SqlTransaction tr, int t305_idproyectosubnodo, string t422_idmoneda)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_MESESBORRABLES", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_MESESBORRABLES", aParam);
        }
        public static SqlDataReader ObtenerMesesParaClonado(SqlTransaction tr, int t305_idproyectosubnodo, string t422_idmoneda)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_MESESCLONABLES", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_MESESCLONABLES", aParam);
        }
        public static int BorrarMesesAbiertos(SqlTransaction tr, string sIdsegmesproys)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@sIdsegmesproys", SqlDbType.VarChar, 4000);
            aParam[0].Value = sIdsegmesproys;

            if (tr == null)
                return Convert.ToInt32(SqlHelper.ExecuteNonQuery("SUP_BORRARMESESABIERTOS", aParam));
            else
                return Convert.ToInt32(SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_BORRARMESESABIERTOS", aParam));
        }

        public static void ClonarMes(SqlTransaction tr, 
            int t325_idsegmesproy_origen, 
            int t325_idsegmesproy_destino, 
            string t329_idclaseeco_clonable, 
            bool bConsumosPersonas,
            bool bConsumosNivel,
            bool bProduccionProfesional,
            bool bProduccionPerfil,
            bool bAvance,
            bool bPeriodificacionC,
            bool bPeriodificacionP,
            bool bAdmin
            )
        {
            SqlParameter[] aParam = new SqlParameter[11];
            aParam[0] = new SqlParameter("@t325_idsegmesproy_origen", SqlDbType.Int, 4);
            aParam[0].Value = t325_idsegmesproy_origen;
            aParam[1] = new SqlParameter("@t325_idsegmesproy_destino", SqlDbType.Int, 4);
            aParam[1].Value = t325_idsegmesproy_destino;
            aParam[2] = new SqlParameter("@t329_idclaseeco_clonable", SqlDbType.VarChar, 1000);
            aParam[2].Value = t329_idclaseeco_clonable;
            aParam[3] = new SqlParameter("@bConsumosPersonas", SqlDbType.Bit, 1);
            aParam[3].Value = bConsumosPersonas;
            aParam[4] = new SqlParameter("@bConsumosNivel", SqlDbType.Bit, 1);
            aParam[4].Value = bConsumosNivel;
            aParam[5] = new SqlParameter("@bProduccionProfesional", SqlDbType.Bit, 1);
            aParam[5].Value = bProduccionProfesional;
            aParam[6] = new SqlParameter("@bProduccionPerfil", SqlDbType.Bit, 1);
            aParam[6].Value = bProduccionPerfil;
            aParam[7] = new SqlParameter("@bAvance", SqlDbType.Bit, 1);
            aParam[7].Value = bAvance;
            aParam[8] = new SqlParameter("@bPeriodificacionC", SqlDbType.Bit, 1);
            aParam[8].Value = bPeriodificacionC;
            aParam[9] = new SqlParameter("@bPeriodificacionP", SqlDbType.Bit, 1);
            aParam[9].Value = bPeriodificacionP;
            aParam[10] = new SqlParameter("@bEsAdmin", SqlDbType.Bit, 1);
            aParam[10].Value = bAdmin;

            SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CLONARDATOSMES", aParam);
        }
        public static void BorrarDatosMes(SqlTransaction tr,
            int t325_anomes_desde,
            int t325_anomes_hasta, 
            string sResponsables, 
            string sSubnodos, 
            string sPSN,
            string t329_idclaseeco_borrable,
            bool bConsumosPersonas,
            bool bConsumosNivel,
            bool bProduccionProfesional,
            bool bProduccionPerfil,
            bool bAvance,
            bool bPeriodificacionC,
            bool bPeriodificacionP,
            bool bDeCirculante,
            bool bIncMesesCerrados
            )
        {
            SqlParameter[] aParam = new SqlParameter[15];
            aParam[0] = new SqlParameter("@t325_anomes_desde", SqlDbType.Int, 4);
            aParam[0].Value = t325_anomes_desde;
            aParam[1] = new SqlParameter("@t325_anomes_hasta", SqlDbType.Int, 4);
            aParam[1].Value = t325_anomes_hasta;
            aParam[2] = new SqlParameter("@sResponsables", SqlDbType.VarChar, 8000);
            aParam[2].Value = sResponsables;
            aParam[3] = new SqlParameter("@sSubnodos", SqlDbType.VarChar, 8000);
            aParam[3].Value = sSubnodos;
            aParam[4] = new SqlParameter("@sPSN", SqlDbType.VarChar, 8000);
            aParam[4].Value = sPSN;
            aParam[5] = new SqlParameter("@t329_idclaseeco_borrable", SqlDbType.VarChar, 1000);
            aParam[5].Value = t329_idclaseeco_borrable;
            aParam[6] = new SqlParameter("@bConsumosPersonas", SqlDbType.Bit, 1);
            aParam[6].Value = bConsumosPersonas;
            aParam[7] = new SqlParameter("@bConsumosNivel", SqlDbType.Bit, 1);
            aParam[7].Value = bConsumosNivel;
            aParam[8] = new SqlParameter("@bProduccionProfesional", SqlDbType.Bit, 1);
            aParam[8].Value = bProduccionProfesional;
            aParam[9] = new SqlParameter("@bProduccionPerfil", SqlDbType.Bit, 1);
            aParam[9].Value = bProduccionPerfil;
            aParam[10] = new SqlParameter("@bAvance", SqlDbType.Bit, 1);
            aParam[10].Value = bAvance;
            aParam[11] = new SqlParameter("@bPeriodificacionC", SqlDbType.Bit, 1);
            aParam[11].Value = bPeriodificacionC;
            aParam[12] = new SqlParameter("@bPeriodificacionP", SqlDbType.Bit, 1);
            aParam[12].Value = bPeriodificacionP;
            aParam[13] = new SqlParameter("@bDeCirculante", SqlDbType.Bit, 1);
            aParam[13].Value = bDeCirculante;
            aParam[14] = new SqlParameter("@bIncMesesCerrados", SqlDbType.Bit, 1);
            aParam[14].Value = bIncMesesCerrados;
            
            SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_BORRARDATOSPSN", aParam);
        }

        public static void GenerarDialogosDeAlertas(string sIdSegMesProy, bool b_ModoComprobacion)
        {
            SUPER.Capa_Datos.SEGMESPROYECTOSUBNODO.GenerarDialogosDeAlertas(sIdSegMesProy, b_ModoComprobacion);
        }
        
        public static string ObtenerDialogosDeAlertas_old(string s_idsegmesproy, bool b_ModoComprobacion)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.Append("<table id='tblDatos' class='texto' style='WIDTH: 800px;'>");
                sb.Append("<colgroup>");
                sb.Append("<col style='width:60px' />");
                sb.Append("<col style='width:340px' />");
                sb.Append("<col style='width:380px' />");
                sb.Append("<col style='width:20px' />");
                sb.Append("</colgroup>");
                sb.Append("<tbody>");

                int i = 0;
                SqlDataReader dr = SUPER.Capa_Datos.SEGMESPROYECTOSUBNODO.ObtenerDialogosDeAlertas(null, s_idsegmesproy, b_ModoComprobacion);

                while (dr.Read())
                {
                    sb.Append("<tr style='height:20px' ");
                    sb.Append("onclick='ms(this)' ");
                    sb.Append("id='" + i.ToString() + "' ");
                    sb.Append("idSegMes='" + dr["t325_idsegmesproy"].ToString() + "' ");
                    sb.Append("idPSN='" + dr["t305_idproyectosubnodo"].ToString() + "' ");
                    sb.Append("responsable=\"" + Utilidades.escape(dr["Responsable"].ToString()) + "\" ");
                    sb.Append("nodo=\"" + Utilidades.escape(dr["t303_denominacion"].ToString()) + "\" ");
                    sb.Append("cliente=\"" + Utilidades.escape(dr["t302_denominacion"].ToString()) + "\" ");
                    sb.Append("idAlerta='" + dr["t820_idalerta"].ToString() + "' ");
                    sb.Append("idMoneda='" + dr["t422_idmoneda"].ToString() + "' ");
                    sb.Append(">");

                    sb.Append("<td style='text-align:right; padding-right:5px;'>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + "</td>");
                    sb.Append("<td onmouseover='TTip(event)'><nobr class='NBR W330'>" + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "</nobr></td>");
                    sb.Append("<td onmouseover='TTip(event)'><nobr class='NBR W380'>" + dr["t820_denominacion"].ToString() + "</nobr></td>");
                    sb.Append("<td><img src='../../../../Images/imgInforme.png' style='cursor:pointer;' onclick='getInforme(this);' title='Obtener informe económico de los datos que generan la alerta' /></td>");
                    sb.Append("</tr>");
                    i++;
                }
                dr.Close();
                dr.Dispose();
                sb.Append("</tbody>");
                sb.Append("</table>");

                return "OK@#@" + sb.ToString();
            }
            catch (Exception ex)
            {
                return "Error@#@"+ Errores.mostrarError("Error al obtener las alertas.", ex);
            }
        }

                public static string ObtenerDialogosDeAlertas(string s_idsegmesproy, bool b_ModoComprobacion)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.Append("<table id='tblDatos' class='texto' style='WIDTH: 800px;'>");
                sb.Append("<colgroup>");
                sb.Append("<col style='width:60px' />");
                sb.Append("<col style='width:340px' />");
                sb.Append("<col style='width:380px' />");
                sb.Append("<col style='width:20px' />");
                sb.Append("</colgroup>");
                sb.Append("<tbody>");

                int i = 0;
                DataSet ds = null;
                if (HttpContext.Current.Session["DS_ALERTASCIERRE"] != null)
                {
                    ds = (DataSet)HttpContext.Current.Session["DS_ALERTASCIERRE"];
                }else{
                    ds = SUPER.Capa_Datos.SEGMESPROYECTOSUBNODO.ObtenerDialogosDeAlertasDS(null, s_idsegmesproy, b_ModoComprobacion);
                    HttpContext.Current.Session["DS_ALERTASCIERRE"] = ds;
                }
                //SqlDataReader dr = SUPER.Capa_Datos.SEGMESPROYECTOSUBNODO.ObtenerDialogosDeAlertas(null, s_idsegmesproy, b_ModoComprobacion);

                //while (dr.Read())
                foreach (DataRow oFila in ds.Tables[0].Rows)
                {
                    sb.Append("<tr style='height:20px' ");
                    sb.Append("onclick='ms(this)' ");
                    sb.Append("id='" + i.ToString() + "' ");
                    sb.Append("idSegMes='" + oFila["t325_idsegmesproy"].ToString() + "' ");
                    sb.Append("idPSN='" + oFila["t305_idproyectosubnodo"].ToString() + "' ");
                    sb.Append("responsable=\"" + Utilidades.escape(oFila["Responsable"].ToString()) + "\" ");
                    sb.Append("nodo=\"" + Utilidades.escape(oFila["t303_denominacion"].ToString()) + "\" ");
                    sb.Append("cliente=\"" + Utilidades.escape(oFila["t302_denominacion"].ToString()) + "\" ");
                    sb.Append("idAlerta='" + oFila["t820_idalerta"].ToString() + "' ");
                    sb.Append("idMoneda='" + oFila["t422_idmoneda"].ToString() + "' ");
                    sb.Append(">");

                    sb.Append("<td style='text-align:right; padding-right:5px;'>" + int.Parse(oFila["t301_idproyecto"].ToString()).ToString("#,###") + "</td>");
                    sb.Append("<td onmouseover='TTip(event)'><nobr class='NBR W330'>" + oFila["t301_denominacion"].ToString().Replace((char)34, (char)39) + "</nobr></td>");
                    sb.Append("<td onmouseover='TTip(event)'><nobr class='NBR W380'>" + oFila["t820_denominacion"].ToString() + "</nobr></td>");
                    sb.Append("<td><img src='../../../../Images/imgInforme.png' style='cursor:pointer;' onclick='getInforme(this);' title='Obtener informe económico de los datos que generan la alerta' /></td>");
                    sb.Append("</tr>");
                    i++;
                }
                //dr.Close();
                //dr.Dispose();
                sb.Append("</tbody>");
                sb.Append("</table>");

                return "OK@#@" + sb.ToString();
            }
            catch (Exception ex)
            {
                return "Error@#@"+ Errores.mostrarError("Error al obtener las alertas.", ex);
            }
        }

        public static string ObtenerInformeDeAlerta(int t325_idsegmesproy,
                            string t422_idmoneda,
                            byte t820_idalerta)
        {
            StringBuilder sb = new StringBuilder();
            string sResultadoM1 = "", sResultadoM2 = "", sAcumuladoM1 = "", sAcumuladoM2 = "", sMensaje = "";

            try
            {
                sb.Append(@"<table id='tblDatos' style='width:800px;'>
                    <colgroup>
                    <col style='width:100px;' />
                    <col style='width:300px;' />
                    <col style='width:100px;' />
                    <col style='width:100px;' />
                    <col style='width:100px;' />
                    <col style='width:100px;' />
                    </colgroup>");
                sb.Append("<tbody>");

                int i = 0;
                DataSet ds = SUPER.Capa_Datos.SEGMESPROYECTOSUBNODO.ObtenerInformeDeAlerta(null, t325_idsegmesproy, t422_idmoneda, t820_idalerta);
                string sClass = "FA";

                foreach (DataRow oFila in ds.Tables[0].Rows)
                {
                    sb.Append("<tr class='"+ sClass +"' ");

                    sb.Append(">");
                    switch (i)
                    {
                        case 0:
                            sResultadoM1 = (oFila["anomes_anterior"] == DBNull.Value)? "": Fechas.AnnomesAFechaDescLarga((int)oFila["anomes_anterior"]);
                            sResultadoM2 = (oFila["anomes"] == DBNull.Value) ? "" : Fechas.AnnomesAFechaDescLarga((int)oFila["anomes"]);
                            sAcumuladoM1 = (oFila["anomes_anterior"] == DBNull.Value) ? "" : Fechas.AnnomesAFechaDescLarga((int)oFila["anomes_anterior"]);
                            sAcumuladoM2 = (oFila["anomes"] == DBNull.Value) ? "" : Fechas.AnnomesAFechaDescLarga((int)oFila["anomes"]);
                            sb.Append("<td rowspan='4' style='text-align:center'>Análisis rentabilidad</td>"); 
                            break;
                        case 4: sb.Append("<td rowspan='3' style='text-align:center'>Análisis financiero</td>"); break;
                        case 7: sb.Append("<td rowspan='2' style='text-align:center'>Equilibrio entre avance económico y técnico</td>"); break;
                        case 9: sb.Append("<td style='text-align:center'>Análisis plantilla</td>"); break;
                    }
                    sb.Append("<td style='text-align:left'>" + oFila["t454_literal"].ToString() + "</td>");
                    sb.Append("<td>" + decimal.Parse(oFila["resultado_mes_anterior"].ToString()).ToString("#,###") + "</td>");
                    sb.Append("<td>" + decimal.Parse(oFila["resultado_mes_cierre"].ToString()).ToString("#,###") + "</td>");
                    sb.Append("<td>" + decimal.Parse(oFila["acumulado_mes_anterior"].ToString()).ToString("#,###") + "</td>");
                    sb.Append("<td>" + decimal.Parse(oFila["acumulado_mes_cierre"].ToString()).ToString("#,###") + "</td>");
                    sb.Append("</tr>");

                    switch (i)
                    {
                        case 3: sClass = "FB"; break;
                        case 6: sClass = "FA"; break;
                        case 8: sClass = "FB"; break;
                    }

                    i++;
                }

                sMensaje = ds.Tables[1].Rows[0]["t820_txtdialogo2"].ToString();

                ds.Dispose();
                sb.Append("</tbody>");
                sb.Append("</table>");

                return "OK@#@" + sb.ToString() + "@#@" + sResultadoM1 + "@#@" + sResultadoM2 + "@#@" + sAcumuladoM1 + "@#@" + sAcumuladoM2 + "@#@" + sMensaje;
            }
            catch (Exception ex)
            {
                return "Error@#@"+ Errores.mostrarError("Error al obtener el informe de la alerta.", ex);
            }
        }
		#endregion
	}
}
