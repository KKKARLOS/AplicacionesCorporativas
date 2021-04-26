using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Collections.Generic;
using SUPER.Capa_Datos;

namespace SUPER.Capa_Negocio
{
    /// <summary>
    /// Summary description for Consumo
    /// </summary>
    public class Consumo
    {
        #region Atributos complementarios

        private double _nHorasDiaGlobal;
        public double nHorasDiaGlobal
        {
            get { return _nHorasDiaGlobal; }
            set { _nHorasDiaGlobal = value; }
        }
        private double _nHorasDiaTarea;
        public double nHorasDiaTarea
        {
            get { return _nHorasDiaTarea; }
            set { _nHorasDiaTarea = value; }
        }
        #endregion

        #region	Métodos públicos

        /// <summary>
        /// Obtiene los consumos economicos
        /// </summary>
        public static DataSet ObtenerConsumosEconomicos(int num_empleado, DateTime dDesde, DateTime dHasta, Nullable<int> iNodo, bool bDelNodo, 
                                                        bool bExternos, bool bOtrosNodos, bool bSoloProyAbiertos, string sModeloCoste)
        {
            SqlParameter[] aParam = new SqlParameter[9];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = num_empleado;
            aParam[1] = new SqlParameter("@dDesde", SqlDbType.DateTime, 8);
            aParam[1].Value = dDesde;
            aParam[2] = new SqlParameter("@dHasta", SqlDbType.DateTime, 8);
            aParam[2].Value = dHasta;
            aParam[3] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[3].Value = iNodo;
            aParam[4] = new SqlParameter("@bDelNodo", SqlDbType.Bit, 1);
            aParam[4].Value = bDelNodo;
            aParam[5] = new SqlParameter("@bExternos", SqlDbType.Bit, 1);
            aParam[5].Value = bExternos;
            aParam[6] = new SqlParameter("@bOtrosNodos", SqlDbType.Bit, 1);
            aParam[6].Value = bOtrosNodos;
            aParam[7] = new SqlParameter("@bSoloProyAbiertos", SqlDbType.Bit, 1);
            aParam[7].Value = bSoloProyAbiertos;
            aParam[8] = new SqlParameter("@sModeloCoste", SqlDbType.Char, 1);
            aParam[8].Value = sModeloCoste;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteDataset("SUP_CONS_CONSPERMES_ADMIN", aParam);
            else
                return SqlHelper.ExecuteDataset("SUP_CONS_CONSPERMES", aParam);
        }
        /// <summary>
        /// Obtiene los consumos economicos de una lista de profesionales
        /// </summary>
        public static SqlDataReader ObtenerConsumosEconomicosProf(int num_empleado, int nDesde, int nHasta, string slProfesionales)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = num_empleado;
            aParam[1] = new SqlParameter("@nDesde", SqlDbType.Int, 4);
            aParam[1].Value = nDesde;
            aParam[2] = new SqlParameter("@nHasta", SqlDbType.Int, 4);
            aParam[2].Value = nHasta;
            aParam[3] = new SqlParameter("@sProfesionales", SqlDbType.VarChar, 8000);
            aParam[3].Value = slProfesionales;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteSqlDataReader("SUP_CONS_CONSPERMES_PROF_ADMIN", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_CONS_CONSPERMES_PROF", aParam);
        }

        public static DataSet ObtenerJornadasReportadasDS(int annomes, int num_empleado, Nullable<int> iNodo, Nullable<int> iGF, bool bExternos, 
                                                          bool bOtrosNodos, bool bSoloProyAbiertos, bool bForaneos)
        {
            SqlParameter[] aParam = new SqlParameter[8];
            aParam[0] = new SqlParameter("@annomes", SqlDbType.Int, 4);
            aParam[0].Value = annomes;
            aParam[1] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[1].Value = num_empleado;
            aParam[2] = new SqlParameter("@t303_idnodo", SqlDbType.Int, 4);
            aParam[2].Value = iNodo;
            aParam[3] = new SqlParameter("@t342_idgrupro", SqlDbType.Int, 4);
            aParam[3].Value = iGF;
            aParam[4] = new SqlParameter("@bExternos", SqlDbType.Bit, 1);
            aParam[4].Value = bExternos;
            aParam[5] = new SqlParameter("@bOtrosNodos", SqlDbType.Bit, 1);
            aParam[5].Value = bOtrosNodos;
            aParam[6] = new SqlParameter("@bSoloProyAbiertos", SqlDbType.Bit, 1);
            aParam[6].Value = bSoloProyAbiertos;
            aParam[7] = new SqlParameter("@bForaneos", SqlDbType.Bit, 1);
            aParam[7].Value = bForaneos;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteDataset("SUP_JORNADASREPORTADAS_ADMIN", aParam);
            else
                return SqlHelper.ExecuteDataset("SUP_JORNADASREPORTADAS", aParam);
        }

        public static SqlDataReader ObtenerJornadasReportadasUsuario(int num_empleado, DateTime dDesde, Nullable<DateTime> dHasta)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = num_empleado;
            aParam[1] = new SqlParameter("@dDesde", SqlDbType.SmallDateTime, 4);
            aParam[1].Value = dDesde;
            aParam[2] = new SqlParameter("@dHasta", SqlDbType.SmallDateTime, 2);
            aParam[2].Value = dHasta;

            return SqlHelper.ExecuteSqlDataReader("SUP_JORNREPORTUSUARIO", aParam);
        }

        /// <summary>
        /// Obtiene las fechas en las que el usuario ha realizado
        /// alguna imputación de esfuerzos en IAP.
        /// </summary>
        public static SqlDataReader ObtenerFechasConsumosIAP(int nUsuario, int nAnomesCerrado)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;
            aParam[1] = new SqlParameter("@nAnomesCerrado", SqlDbType.Int, 4);
            aParam[1].Value = nAnomesCerrado;

            return SqlHelper.ExecuteSqlDataReader("SUP_FECHACONSUMOS", aParam);
        }

        /// <summary>
        /// Obtiene los esfuerzos totales imputados por un usuario en IAP, en una semana.
        /// </summary>
        public static SqlDataReader ObtenerConsumosTotalesSemanaIAP(int nUsuario, DateTime dDesde, DateTime dHasta, Nullable<DateTime> dLunes, Nullable<DateTime> dMartes, Nullable<DateTime> dMiercoles, Nullable<DateTime> dJueves, Nullable<DateTime> dViernes, Nullable<DateTime> dSabado, Nullable<DateTime> dDomingo)
        {
            SqlParameter[] aParam = new SqlParameter[10];
            aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;
            aParam[1] = new SqlParameter("@dDesde", SqlDbType.DateTime, 8);
            aParam[1].Value = dDesde;
            aParam[2] = new SqlParameter("@dHasta", SqlDbType.DateTime, 8);
            aParam[2].Value = dHasta;
            aParam[3] = new SqlParameter("@dLunes", SqlDbType.DateTime, 8);
            aParam[3].Value = dLunes;
            aParam[4] = new SqlParameter("@dMartes", SqlDbType.DateTime, 8);
            aParam[4].Value = dMartes;
            aParam[5] = new SqlParameter("@dMiercoles", SqlDbType.DateTime, 8);
            aParam[5].Value = dMiercoles;
            aParam[6] = new SqlParameter("@dJueves", SqlDbType.DateTime, 8);
            aParam[6].Value = dJueves;
            aParam[7] = new SqlParameter("@dViernes", SqlDbType.DateTime, 8);
            aParam[7].Value = dViernes;
            aParam[8] = new SqlParameter("@dSabado", SqlDbType.DateTime, 8);
            aParam[8].Value = dSabado;
            aParam[9] = new SqlParameter("@dDomingo", SqlDbType.DateTime, 8);
            aParam[9].Value = dDomingo;

            return SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOIAPTOTALSEMANA", aParam);
        }
       
        /// <summary>
        /// Obtiene los proyectos en los que puede imputar esfuerzos un usuario en IAP, en una semana.
        /// </summary>
        public static SqlDataReader ObtenerConsumosSemanaIAP_PSN(int nUsuario, DateTime dDesde, DateTime dHasta, Nullable<DateTime> dLunes, Nullable<DateTime> dMartes, Nullable<DateTime> dMiercoles, Nullable<DateTime> dJueves, Nullable<DateTime> dViernes, Nullable<DateTime> dSabado, Nullable<DateTime> dDomingo)
        {
            SqlParameter[] aParam = new SqlParameter[10];
            aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;
            aParam[1] = new SqlParameter("@dDesde", SqlDbType.DateTime, 8);
            aParam[1].Value = dDesde;
            aParam[2] = new SqlParameter("@dHasta", SqlDbType.DateTime, 8);
            aParam[2].Value = dHasta;
            aParam[3] = new SqlParameter("@dLunes", SqlDbType.DateTime, 8);
            aParam[3].Value = dLunes;
            aParam[4] = new SqlParameter("@dMartes", SqlDbType.DateTime, 8);
            aParam[4].Value = dMartes;
            aParam[5] = new SqlParameter("@dMiercoles", SqlDbType.DateTime, 8);
            aParam[5].Value = dMiercoles;
            aParam[6] = new SqlParameter("@dJueves", SqlDbType.DateTime, 8);
            aParam[6].Value = dJueves;
            aParam[7] = new SqlParameter("@dViernes", SqlDbType.DateTime, 8);
            aParam[7].Value = dViernes;
            aParam[8] = new SqlParameter("@dSabado", SqlDbType.DateTime, 8);
            aParam[8].Value = dSabado;
            aParam[9] = new SqlParameter("@dDomingo", SqlDbType.DateTime, 8);
            aParam[9].Value = dDomingo;

            return SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOIAPSEMANA_PSN", aParam);
        }
        public static SqlDataReader ObtenerConsumosSemanaIAP_PT(int nUsuario, int nPSN, DateTime dDesde, DateTime dHasta, Nullable<DateTime> dLunes, Nullable<DateTime> dMartes, Nullable<DateTime> dMiercoles, Nullable<DateTime> dJueves, Nullable<DateTime> dViernes, Nullable<DateTime> dSabado, Nullable<DateTime> dDomingo)
        {
            SqlParameter[] aParam = new SqlParameter[11];
            aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;
            aParam[1] = new SqlParameter("@nPSN", SqlDbType.Int, 4);
            aParam[1].Value = nPSN;
            aParam[2] = new SqlParameter("@dDesde", SqlDbType.DateTime, 8);
            aParam[2].Value = dDesde;
            aParam[3] = new SqlParameter("@dHasta", SqlDbType.DateTime, 8);
            aParam[3].Value = dHasta;
            aParam[4] = new SqlParameter("@dLunes", SqlDbType.DateTime, 8);
            aParam[4].Value = dLunes;
            aParam[5] = new SqlParameter("@dMartes", SqlDbType.DateTime, 8);
            aParam[5].Value = dMartes;
            aParam[6] = new SqlParameter("@dMiercoles", SqlDbType.DateTime, 8);
            aParam[6].Value = dMiercoles;
            aParam[7] = new SqlParameter("@dJueves", SqlDbType.DateTime, 8);
            aParam[7].Value = dJueves;
            aParam[8] = new SqlParameter("@dViernes", SqlDbType.DateTime, 8);
            aParam[8].Value = dViernes;
            aParam[9] = new SqlParameter("@dSabado", SqlDbType.DateTime, 8);
            aParam[9].Value = dSabado;
            aParam[10] = new SqlParameter("@dDomingo", SqlDbType.DateTime, 8);
            aParam[10].Value = dDomingo;

            return SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOIAPSEMANA_PT", aParam);
        }
        public static SqlDataReader ObtenerConsumosSemanaIAP_T(int nUsuario, int nPT, DateTime dDesde, DateTime dHasta, Nullable<DateTime> dLunes, Nullable<DateTime> dMartes, Nullable<DateTime> dMiercoles, Nullable<DateTime> dJueves, Nullable<DateTime> dViernes, Nullable<DateTime> dSabado, Nullable<DateTime> dDomingo)
        {
            SqlParameter[] aParam = new SqlParameter[11];
            aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;
            aParam[1] = new SqlParameter("@nPT", SqlDbType.Int, 4);
            aParam[1].Value = nPT;
            aParam[2] = new SqlParameter("@dDesde", SqlDbType.DateTime, 8);
            aParam[2].Value = dDesde;
            aParam[3] = new SqlParameter("@dHasta", SqlDbType.DateTime, 8);
            aParam[3].Value = dHasta;
            aParam[4] = new SqlParameter("@dLunes", SqlDbType.DateTime, 8);
            aParam[4].Value = dLunes;
            aParam[5] = new SqlParameter("@dMartes", SqlDbType.DateTime, 8);
            aParam[5].Value = dMartes;
            aParam[6] = new SqlParameter("@dMiercoles", SqlDbType.DateTime, 8);
            aParam[6].Value = dMiercoles;
            aParam[7] = new SqlParameter("@dJueves", SqlDbType.DateTime, 8);
            aParam[7].Value = dJueves;
            aParam[8] = new SqlParameter("@dViernes", SqlDbType.DateTime, 8);
            aParam[8].Value = dViernes;
            aParam[9] = new SqlParameter("@dSabado", SqlDbType.DateTime, 8);
            aParam[9].Value = dSabado;
            aParam[10] = new SqlParameter("@dDomingo", SqlDbType.DateTime, 8);
            aParam[10].Value = dDomingo;
            
            return SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOIAPSEMANA_T", aParam);
        }
        public static SqlDataReader ObtenerConsumosSemanaIAP_PT_D(int nUsuario, int nPT, DateTime dDesde, DateTime dHasta, Nullable<DateTime> dLunes, Nullable<DateTime> dMartes, Nullable<DateTime> dMiercoles, Nullable<DateTime> dJueves, Nullable<DateTime> dViernes, Nullable<DateTime> dSabado, Nullable<DateTime> dDomingo)
        {
            SqlParameter[] aParam = new SqlParameter[11];
            aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;
            aParam[1] = new SqlParameter("@nPT", SqlDbType.Int, 4);
            aParam[1].Value = nPT;
            aParam[2] = new SqlParameter("@dDesde", SqlDbType.DateTime, 8);
            aParam[2].Value = dDesde;
            aParam[3] = new SqlParameter("@dHasta", SqlDbType.DateTime, 8);
            aParam[3].Value = dHasta;
            aParam[4] = new SqlParameter("@dLunes", SqlDbType.DateTime, 8);
            aParam[4].Value = dLunes;
            aParam[5] = new SqlParameter("@dMartes", SqlDbType.DateTime, 8);
            aParam[5].Value = dMartes;
            aParam[6] = new SqlParameter("@dMiercoles", SqlDbType.DateTime, 8);
            aParam[6].Value = dMiercoles;
            aParam[7] = new SqlParameter("@dJueves", SqlDbType.DateTime, 8);
            aParam[7].Value = dJueves;
            aParam[8] = new SqlParameter("@dViernes", SqlDbType.DateTime, 8);
            aParam[8].Value = dViernes;
            aParam[9] = new SqlParameter("@dSabado", SqlDbType.DateTime, 8);
            aParam[9].Value = dSabado;
            aParam[10] = new SqlParameter("@dDomingo", SqlDbType.DateTime, 8);
            aParam[10].Value = dDomingo;

            return SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOIAPSEMANA_PT_D", aParam);
        }
        public static SqlDataReader ObtenerConsumosSemanaIAP_F(int nUsuario, int nPT, int nFase, DateTime dDesde, DateTime dHasta, Nullable<DateTime> dLunes, Nullable<DateTime> dMartes, Nullable<DateTime> dMiercoles, Nullable<DateTime> dJueves, Nullable<DateTime> dViernes, Nullable<DateTime> dSabado, Nullable<DateTime> dDomingo)
        {
            SqlParameter[] aParam = new SqlParameter[12];
            aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;
            aParam[1] = new SqlParameter("@nPT", SqlDbType.Int, 4);
            aParam[1].Value = nPT;
            aParam[2] = new SqlParameter("@nFase", SqlDbType.Int, 4);
            aParam[2].Value = nFase;
            aParam[3] = new SqlParameter("@dDesde", SqlDbType.DateTime, 8);
            aParam[3].Value = dDesde;
            aParam[4] = new SqlParameter("@dHasta", SqlDbType.DateTime, 8);
            aParam[4].Value = dHasta;
            aParam[5] = new SqlParameter("@dLunes", SqlDbType.DateTime, 8);
            aParam[5].Value = dLunes;
            aParam[6] = new SqlParameter("@dMartes", SqlDbType.DateTime, 8);
            aParam[6].Value = dMartes;
            aParam[7] = new SqlParameter("@dMiercoles", SqlDbType.DateTime, 8);
            aParam[7].Value = dMiercoles;
            aParam[8] = new SqlParameter("@dJueves", SqlDbType.DateTime, 8);
            aParam[8].Value = dJueves;
            aParam[9] = new SqlParameter("@dViernes", SqlDbType.DateTime, 8);
            aParam[9].Value = dViernes;
            aParam[10] = new SqlParameter("@dSabado", SqlDbType.DateTime, 8);
            aParam[10].Value = dSabado;
            aParam[11] = new SqlParameter("@dDomingo", SqlDbType.DateTime, 8);
            aParam[11].Value = dDomingo;

            return SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOIAPSEMANA_F", aParam);
        }
        public static SqlDataReader ObtenerConsumosSemanaIAP_A(int nUsuario, int nPT, int nActividad, DateTime dDesde, DateTime dHasta, Nullable<DateTime> dLunes, Nullable<DateTime> dMartes, Nullable<DateTime> dMiercoles, Nullable<DateTime> dJueves, Nullable<DateTime> dViernes, Nullable<DateTime> dSabado, Nullable<DateTime> dDomingo)
        {
            SqlParameter[] aParam = new SqlParameter[12];
            aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;
            aParam[1] = new SqlParameter("@nPT", SqlDbType.Int, 4);
            aParam[1].Value = nPT;
            aParam[2] = new SqlParameter("@nActividad", SqlDbType.Int, 4);
            aParam[2].Value = nActividad;
            aParam[3] = new SqlParameter("@dDesde", SqlDbType.DateTime, 8);
            aParam[3].Value = dDesde;
            aParam[4] = new SqlParameter("@dHasta", SqlDbType.DateTime, 8);
            aParam[4].Value = dHasta;
            aParam[5] = new SqlParameter("@dLunes", SqlDbType.DateTime, 8);
            aParam[5].Value = dLunes;
            aParam[6] = new SqlParameter("@dMartes", SqlDbType.DateTime, 8);
            aParam[6].Value = dMartes;
            aParam[7] = new SqlParameter("@dMiercoles", SqlDbType.DateTime, 8);
            aParam[7].Value = dMiercoles;
            aParam[8] = new SqlParameter("@dJueves", SqlDbType.DateTime, 8);
            aParam[8].Value = dJueves;
            aParam[9] = new SqlParameter("@dViernes", SqlDbType.DateTime, 8);
            aParam[9].Value = dViernes;
            aParam[10] = new SqlParameter("@dSabado", SqlDbType.DateTime, 8);
            aParam[10].Value = dSabado;
            aParam[11] = new SqlParameter("@dDomingo", SqlDbType.DateTime, 8);
            aParam[11].Value = dDomingo;

            return SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOIAPSEMANA_A", aParam);
        }
        public static SqlDataReader ObtenerTareasImpMasiva_PSN(int nUsuario, DateTime dUMC, Nullable<DateTime> dInicioImput, Nullable<DateTime> dFinImput)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;
            aParam[1] = new SqlParameter("@dUMC", SqlDbType.SmallDateTime, 4);
            aParam[1].Value = dUMC;
            aParam[2] = new SqlParameter("@dInicioImput", SqlDbType.SmallDateTime, 4);
            aParam[2].Value = dInicioImput;
            aParam[3] = new SqlParameter("@dFinImput", SqlDbType.SmallDateTime, 4);
            aParam[3].Value = dFinImput;
            return SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOIAPMASIVA_PSN", aParam);
        }

        public static SqlDataReader ObtenerTareasImpMasiva_PT(int nUsuario, int nPSN, DateTime dUMC, Nullable<DateTime> dInicioImput, Nullable<DateTime> dFinImput)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;
            aParam[1] = new SqlParameter("@nPSN", SqlDbType.Int, 4);
            aParam[1].Value = nPSN;
            aParam[2] = new SqlParameter("@dUMC", SqlDbType.SmallDateTime, 4);
            aParam[2].Value = dUMC;
            aParam[3] = new SqlParameter("@dInicioImput", SqlDbType.SmallDateTime, 4);
            aParam[3].Value = dInicioImput;
            aParam[4] = new SqlParameter("@dFinImput", SqlDbType.SmallDateTime, 4);
            aParam[4].Value = dFinImput;
            return SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOIAPMASIVA_PT", aParam);
        }

        public static SqlDataReader ObtenerTareasImpMasiva_T(int nUsuario, int nPT, DateTime dUMC, Nullable<DateTime> dInicioImput, Nullable<DateTime> dFinImput)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;
            aParam[1] = new SqlParameter("@nPT", SqlDbType.Int, 4);
            aParam[1].Value = nPT;
            aParam[2] = new SqlParameter("@dUMC", SqlDbType.SmallDateTime, 4);
            aParam[2].Value = dUMC;
            aParam[3] = new SqlParameter("@dInicioImput", SqlDbType.SmallDateTime, 4);
            aParam[3].Value = dInicioImput;
            aParam[4] = new SqlParameter("@dFinImput", SqlDbType.SmallDateTime, 4);
            aParam[4].Value = dFinImput;

            return SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOIAPMASIVA_T", aParam);
        }

        public static SqlDataReader ObtenerTarea(int nUsuario, int nTarea, DateTime dUMC)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;
            aParam[1] = new SqlParameter("@nTarea", SqlDbType.Int, 4);
            aParam[1].Value = nTarea;
            aParam[2] = new SqlParameter("@dUMC", SqlDbType.SmallDateTime, 4);
            aParam[2].Value = dUMC;

            return SqlHelper.ExecuteSqlDataReader("SUP_IAP_TAREA_C", aParam);
        }

        public static SqlDataReader ObtenerConsumosCalendarioIAP(int nUsuario, int idFicepi, int nCalendario, int nMes, int nAnno)
        {
            SqlParameter[] aParam = new SqlParameter[5];
            aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;
            aParam[1] = new SqlParameter("@idFicepi", SqlDbType.Int, 4);
            aParam[1].Value = idFicepi;
            aParam[2] = new SqlParameter("@nCalendario", SqlDbType.Int, 4);
            aParam[2].Value = nCalendario;
            aParam[3] = new SqlParameter("@nMes", SqlDbType.Int, 4);
            aParam[3].Value = nMes;
            aParam[4] = new SqlParameter("@nAnno", SqlDbType.Int, 4);
            aParam[4].Value = nAnno;

            return SqlHelper.ExecuteSqlDataReader("SUP_HORASIMPUTADASCALENDARIO", aParam);
        }
        public static SqlDataReader ObtenerConsumosFacturabilidad(int t001_idficepi, DateTime dDesde, DateTime dHasta)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@dDesde", SqlDbType.DateTime, 8);
            aParam[2] = new SqlParameter("@dHasta", SqlDbType.DateTime, 8);
            aParam[0].Value = t001_idficepi;
            aParam[1].Value = dDesde;
            aParam[2].Value = dHasta;

            return SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOIAP_FACT", aParam);
        }
        public static SqlDataReader ObtenerIndicadoresFacturabilidad(int t001_idficepi, DateTime dDesde, DateTime dHasta)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t001_idficepi", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@dDesde", SqlDbType.DateTime, 8);
            aParam[2] = new SqlParameter("@dHasta", SqlDbType.DateTime, 8);
            aParam[0].Value = t001_idficepi;
            aParam[1].Value = dDesde;
            aParam[2].Value = dHasta;

            return SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOIAPINDI_FACT", aParam);
        }

        public static SqlDataReader ObtenerProyectosImputacionesIAP(int t314_idusuario, DateTime dDesde, DateTime dHasta)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@dDesde", SqlDbType.DateTime, 8);
            aParam[1].Value = dDesde;
            aParam[2] = new SqlParameter("@dHasta", SqlDbType.DateTime, 8);
            aParam[2].Value = dHasta;

            return SqlHelper.ExecuteSqlDataReader("SUP_GETPROYECTOS_CONSUMOIAP", aParam);
        }
        public static SqlDataReader ObtenerImputacionesIAP(int t314_idusuario, Nullable<int> t305_idproyectosubnodo, DateTime dDesde, DateTime dHasta)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[1].Value = t305_idproyectosubnodo;
            aParam[2] = new SqlParameter("@dDesde", SqlDbType.DateTime, 8);
            aParam[2].Value = dDesde;
            aParam[3] = new SqlParameter("@dHasta", SqlDbType.DateTime, 8);
            aParam[3].Value = dHasta;

            return SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOS_TECNICO_IAP", aParam);
        }

        /// <summary>
        /// 
        /// Elimina los datos de las imputaciones de un recurso
        /// en un rango de fechas determinado.
        /// </summary>
        public static void EliminarRango(SqlTransaction tr, int nUsuario, DateTime dDesde, DateTime dHasta)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@dDesde", SqlDbType.SmallDateTime, 4);
            aParam[2] = new SqlParameter("@dHasta", SqlDbType.SmallDateTime, 4);

            aParam[0].Value = nUsuario;
            aParam[1].Value = dDesde;
            aParam[2].Value = dHasta;

            SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CONSUMOIAPDRANGO", aParam);
        }
        public static SqlDataReader ObtenerConsumoMaximo(SqlTransaction tr, int nUsuario, int nTarea)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@nTarea", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;
            aParam[1].Value = nTarea;

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOIAPTAREAMAXS", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CONSUMOIAPTAREAMAXS", aParam);
        }
        public void ObtenerImputacionesDia(SqlTransaction tr, int nUsuario, DateTime dDia, int nTarea)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[1] = new SqlParameter("@dDia", SqlDbType.SmallDateTime, 4);
            aParam[2] = new SqlParameter("@nTarea", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;
            aParam[1].Value = dDia;
            aParam[2].Value = nTarea;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOIAPDIA", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CONSUMOIAPDIA", aParam);

            if (dr.Read())
            {
                nHorasDiaGlobal = double.Parse(dr["horas"].ToString());
                if (dr.Read())
                {
                    nHorasDiaTarea = double.Parse(dr["horas"].ToString());
                }
            }
            dr.Close();
            dr.Dispose();
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Inserta un registro en la tabla T439_CORREOS.
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// 	Creado por [sqladmin]	22/09/2009 12:03:32
        /// </history>
        /// -----------------------------------------------------------------------------
        public static void InsertarCorreo(SqlTransaction tr, byte t439_tipo, bool t439_accion, bool t439_estado, Nullable<int> t314_idusuario,
                                  Nullable<int> t439_clave1, Nullable<int> t439_clave2, string t439_figura, Nullable<int> t301_idproyecto)
        {
            SqlParameter[] aParam = new SqlParameter[8];
            aParam[0] = new SqlParameter("@t439_tipo", SqlDbType.TinyInt, 1);
            aParam[0].Value = t439_tipo;
            aParam[1] = new SqlParameter("@t439_accion", SqlDbType.Bit, 1);
            aParam[1].Value = t439_accion;
            aParam[2] = new SqlParameter("@t439_estado", SqlDbType.Bit, 1);
            aParam[2].Value = t439_estado;
            aParam[3] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[3].Value = t314_idusuario;
            aParam[4] = new SqlParameter("@t439_clave1", SqlDbType.Int, 4);
            aParam[4].Value = t439_clave1;
            aParam[5] = new SqlParameter("@t439_clave2", SqlDbType.Int, 4);
            aParam[5].Value = t439_clave2;
            aParam[6] = new SqlParameter("@t439_figura", SqlDbType.Text, 1);
            aParam[6].Value = t439_figura;
            aParam[7] = new SqlParameter("@t301_idproyecto", SqlDbType.Int, 4);
            aParam[7].Value = t301_idproyecto;

            if (tr == null)
                SqlHelper.ExecuteNonQuery("SUP_CORREOS_I", aParam);
            else
                SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_CORREOS_I", aParam);
        }

        public static SqlDataReader ObtenerTareasImpMasivaReducida(int nUsuario, DateTime dUMC)
        {
            SqlParameter[] aParam = new SqlParameter[2];
            aParam[0] = new SqlParameter("@nUsuario", SqlDbType.Int, 4);
            aParam[0].Value = nUsuario;
            aParam[1] = new SqlParameter("@dUMC", SqlDbType.SmallDateTime, 4);
            aParam[1].Value = dUMC;

            return SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOIAPMASIVA_REDUCIDA", aParam);
        }
        public static SqlDataReader ObtenerPartesActividad(string t314_idusuario,
            string t302_idcliente,
            string t305_idproyectosubnodo,
            Nullable<bool> facturable,
            DateTime t337_fecha_desde,
            DateTime t337_fecha_hasta
            )
        {
            SqlParameter[] aParam = new SqlParameter[6];
            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.VarChar, 8000);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@t302_idcliente", SqlDbType.VarChar, 8000);
            aParam[1].Value = t302_idcliente;
            aParam[2] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.VarChar, 8000);
            aParam[2].Value = t305_idproyectosubnodo;
            aParam[3] = new SqlParameter("@facturable", SqlDbType.Bit, 1);
            aParam[3].Value = facturable;
            aParam[4] = new SqlParameter("@t337_fecha_desde", SqlDbType.SmallDateTime, 4);
            aParam[4].Value = t337_fecha_desde;
            aParam[5] = new SqlParameter("@t337_fecha_hasta", SqlDbType.SmallDateTime, 4);
            aParam[5].Value = t337_fecha_hasta;

            return SqlHelper.ExecuteSqlDataReader("SUP_PARTEACTIVIDAD_CAT_IAP", aParam);
        }
        public static SqlDataReader ObtenerPartesActividad(int t314_idusuario, string sProfesionales,
            string t302_idcliente,
            string t305_idproyectosubnodo,
            Nullable<bool> facturable,
            DateTime t337_fecha_desde,
            DateTime t337_fecha_hasta
            )
        {
            SqlParameter[] aParam = new SqlParameter[7];

            aParam[0] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[0].Value = t314_idusuario;
            aParam[1] = new SqlParameter("@Profesionales", SqlDbType.VarChar, 8000);
            aParam[1].Value = sProfesionales;
            aParam[2] = new SqlParameter("@t302_idcliente", SqlDbType.VarChar, 8000);
            aParam[2].Value = t302_idcliente;
            aParam[3] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.VarChar, 8000);
            aParam[3].Value = t305_idproyectosubnodo;
            aParam[4] = new SqlParameter("@facturable", SqlDbType.Bit, 1);
            aParam[4].Value = facturable;
            aParam[5] = new SqlParameter("@t337_fecha_desde", SqlDbType.SmallDateTime, 4);
            aParam[5].Value = t337_fecha_desde;
            aParam[6] = new SqlParameter("@t337_fecha_hasta", SqlDbType.SmallDateTime, 4);
            aParam[6].Value = t337_fecha_hasta;

            // Si se llama desde el PSP

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteSqlDataReader("SUP_PARTEACTIVIDAD_CAT_ADM", aParam);
            else
                return SqlHelper.ExecuteSqlDataReader("SUP_PARTEACTIVIDAD_CAT_USU", aParam);

        }
        public static DataSet ObtenerInformeConsProfAEDS(short nConcepto, string sCodigo, int t314_idusuario, string sDesde, string sHasta,
                                                            string sInt, string sExt, string sNivel)
        {
            SqlParameter[] aParam = new SqlParameter[8];
            aParam[0] = new SqlParameter("@Concepto", SqlDbType.SmallInt, 2);
            aParam[0].Value = nConcepto;
            aParam[1] = new SqlParameter("@Codigo", SqlDbType.VarChar, 8000);
            aParam[1].Value = sCodigo;
            aParam[2] = new SqlParameter("@t314_idusuario", SqlDbType.Int, 4);
            aParam[2].Value = t314_idusuario;
            aParam[3] = new SqlParameter("@FechaDesde", SqlDbType.VarChar, 10);
            aParam[3].Value = sDesde;
            aParam[4] = new SqlParameter("@FechaHasta", SqlDbType.VarChar, 10);
            aParam[4].Value = sHasta;
            aParam[5] = new SqlParameter("@sInt", SqlDbType.Char, 1);
            aParam[5].Value = sInt;
            aParam[6] = new SqlParameter("@sExt", SqlDbType.Char, 1);
            aParam[6].Value = sExt;
            aParam[7] = new SqlParameter("@sNivel", SqlDbType.Char, 1);
            aParam[7].Value = sNivel;

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                return SqlHelper.ExecuteDataset("SUP_PROY_DESGLOSADO_AE_ADMIN", aParam);
            else
                return SqlHelper.ExecuteDataset("SUP_PROY_DESGLOSADO_AE", aParam);
        }

        public static bool HayImputacionesIAP(SqlTransaction tr, int t305_idproyectosubnodo, DateTime dDesde, DateTime dHasta)
        {
            bool bHay = false;

            SqlParameter[] aParam = new SqlParameter[3];
            aParam[0] = new SqlParameter("@t305_idproyectosubnodo", SqlDbType.Int, 4);
            aParam[0].Value = t305_idproyectosubnodo;
            aParam[1] = new SqlParameter("@dDesde", SqlDbType.DateTime, 8);
            aParam[1].Value = dDesde;
            aParam[2] = new SqlParameter("@dHasta", SqlDbType.DateTime, 8);
            aParam[2].Value = dHasta;

            SqlDataReader dr;
            if (tr == null)
                dr = SqlHelper.ExecuteSqlDataReader("SUP_CONSUMOIAP_HAYENMES", aParam);
            else
                dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_CONSUMOIAP_HAYENMES", aParam);

            if (dr.Read())
            {
                int iNumConsumos = int.Parse(dr["iNumConsumos"].ToString());
                if (iNumConsumos > 0)
                    bHay = true;
            }
            dr.Close();
            dr.Dispose();

            return bHay;
        }

        #endregion
    }

}
