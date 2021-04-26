using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SUPER.Capa_Datos;
//Para usar ArraList
using System.Collections;
//Para usar List<>
using System.Collections.Generic;

namespace SUPER.DAL
{
    public partial class PROYECTOSUBNODO
    {
        #region Metodos

        public static SqlDataReader CatalogoPendienteCualificarCVT(SqlTransaction tr, int t314_idusuario)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_PROYECTOSACUALIFICARCVT_CAT", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PROYECTOSACUALIFICARCVT_CAT", aParam);
        }

        public static SqlDataReader ObtenerInterlocutores(SqlTransaction tr, int t305_idproyectosubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_DIALOGOALERTAS_GETINTERLOCUTOR", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_DIALOGOALERTAS_GETINTERLOCUTOR", aParam);
        }
        public static SqlDataReader ObtenerInterlocutoresOCyFA(SqlTransaction tr)
        {
            SqlParameter[] aParam = new SqlParameter[0];
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_GESTORCOBROSAP_C", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_GESTORCOBROSAP_C", aParam);
        }
        public static SqlDataReader ObtenerProduccionPAC(SqlTransaction tr, int t305_idproyectosubnodo, int t314_idusuario,
                                                        bool bAdmin, string t422_idmoneda)
        {
            SqlParameter[] aParam = new SqlParameter[4];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);
            aParam[i++] = ParametroSql.add("@bAdmin", SqlDbType.Bit, 1, bAdmin);
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_GETPROYECTOS_PRODUCCION", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_GETPROYECTOS_PRODUCCION", aParam);
        }
        
        #region Pruebas


        public static SqlDataReader ProyectoNoCualificar(SqlTransaction tr, int t001_idficepi_diceno, int t301_idproyecto, string t301_motivono_cvt)
        {
            SqlParameter[] aParam = new SqlParameter[3];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t001_idficepi_diceno", SqlDbType.Int, 4, t001_idficepi_diceno);
            aParam[i++] = ParametroSql.add("@t301_idproyecto", SqlDbType.Int, 4, t301_idproyecto);
            aParam[i++] = ParametroSql.add("@t301_motivono_cvt", SqlDbType.VarChar, 250, t301_motivono_cvt);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_PROYECTOSNOCUALIFICARCVT_UPD", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PROYECTOSNOCUALIFICARCVT_UPD", aParam);
        }

        #region BBII

        #region Pruebas Tabla dinámica
        public static SqlDataReader PruebaDatosTablaDinamica(SqlTransaction tr)
        {
            SqlParameter[] aParam = new SqlParameter[0];

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("PRU_TABLADINAMICA", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "PRU_TABLADINAMICA", aParam);
        }
        public static SqlDataReader PruebaDatosTablaDinamica2(SqlTransaction tr)
        {
            SqlParameter[] aParam = new SqlParameter[0];

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("PRU_TABLADINAMICA_2", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "PRU_TABLADINAMICA_2", aParam);
        }
        public static DataSet PruebaDatosTablaDinamicaDS(SqlTransaction tr)
        {
            SqlParameter[] aParam = new SqlParameter[0];

            if (tr == null)
                return SqlHelper.ExecuteDataset("PRU_TABLADINAMICA_2_v2", aParam);
            else
                return SqlHelper.ExecuteDatasetTransaccion(tr, "PRU_TABLADINAMICA_2_v2", aParam);
        }
        public static DataSet PruebaDatosTablaDinamicaDSV3(SqlTransaction tr)
        {
            SqlParameter[] aParam = new SqlParameter[0];

            if (tr == null)
                return SqlHelper.ExecuteDataset("PRU_TABLADINAMICA_V3", aParam);
            else
                return SqlHelper.ExecuteDatasetTransaccion(tr, "PRU_TABLADINAMICA_V3", aParam);
        }
        public static SqlDataReader PruebaDatosTablaDinamicaV4(SqlTransaction tr,
            int t314_idusuario,
            int nDesde,
            int nHasta,
            string t422_idmoneda,
            bool bNodo,
            bool bProyecto,
            bool bCliente,
            bool bResponsable,
            bool bCualidad,
            bool bNaturaleza)
        {
            SqlParameter[] aParam = new SqlParameter[10];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);
            aParam[i++] = ParametroSql.add("@nDesde", SqlDbType.Int, 4, nDesde);
            aParam[i++] = ParametroSql.add("@nHasta", SqlDbType.Int, 4, nHasta);
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);
            aParam[i++] = ParametroSql.add("@bNodo", SqlDbType.Bit, 1, bNodo);
            aParam[i++] = ParametroSql.add("@bProyecto", SqlDbType.Bit, 1, bProyecto);
            aParam[i++] = ParametroSql.add("@bCliente", SqlDbType.Bit, 1, bCliente);
            aParam[i++] = ParametroSql.add("@bResponsable", SqlDbType.Bit, 1, bResponsable);
            aParam[i++] = ParametroSql.add("@bCualidad", SqlDbType.Bit, 1, bCualidad);
            aParam[i++] = ParametroSql.add("@bNaturaleza", SqlDbType.Bit, 1, bNaturaleza);

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("PRU_TABLADINAMICA_V4", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "PRU_TABLADINAMICA_V4", aParam);
        }

        public static DataSet PruebaDatosTablaDinamicaV5(SqlTransaction tr,
            int t314_idusuario,
            int nDesde,
            int nHasta,
            string t422_idmoneda,
            bool bNodo,
            bool bProyecto,
            bool bCliente,
            bool bResponsable,
            bool bCualidad,
            bool bNaturaleza,
            string sNodo,
            string sProyecto,
            string sCliente,
            string sResponsable,
            string sCualidad,
            string sNaturaleza,
            bool bTablasAuxiliares,
            string sOrdenDimensiones)
        {
            SqlParameter[] aParam = new SqlParameter[18];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);
            aParam[i++] = ParametroSql.add("@nDesde", SqlDbType.Int, 4, nDesde);
            aParam[i++] = ParametroSql.add("@nHasta", SqlDbType.Int, 4, nHasta);
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);
            aParam[i++] = ParametroSql.add("@bNodo", SqlDbType.Bit, 1, bNodo);
            aParam[i++] = ParametroSql.add("@bProyecto", SqlDbType.Bit, 1, bProyecto);
            aParam[i++] = ParametroSql.add("@bCliente", SqlDbType.Bit, 1, bCliente);
            aParam[i++] = ParametroSql.add("@bResponsable", SqlDbType.Bit, 1, bResponsable);
            aParam[i++] = ParametroSql.add("@bCualidad", SqlDbType.Bit, 1, bCualidad);
            aParam[i++] = ParametroSql.add("@bNaturaleza", SqlDbType.Bit, 1, bNaturaleza);
            aParam[i++] = ParametroSql.add("@sNodo", SqlDbType.VarChar, 8000, sNodo);
            aParam[i++] = ParametroSql.add("@sProyecto", SqlDbType.VarChar, 8000, sProyecto);
            aParam[i++] = ParametroSql.add("@sCliente", SqlDbType.VarChar, 8000, sCliente);
            aParam[i++] = ParametroSql.add("@sResponsable", SqlDbType.VarChar, 8000, sResponsable);
            aParam[i++] = ParametroSql.add("@sCualidad", SqlDbType.VarChar, 8000, sCualidad);
            aParam[i++] = ParametroSql.add("@sNaturaleza", SqlDbType.VarChar, 8000, sNaturaleza);
            aParam[i++] = ParametroSql.add("@bTablasAuxiliares", SqlDbType.Bit, 1, bTablasAuxiliares);
            aParam[i++] = ParametroSql.add("@sOrdenDimensiones", SqlDbType.VarChar, 100, sOrdenDimensiones);

            if (tr == null)
                return SqlHelper.ExecuteDataset("PRU_TABLADINAMICA_V5", aParam);
            else
                return SqlHelper.ExecuteDatasetTransaccion(tr, "PRU_TABLADINAMICA_V5", aParam);
        }

        public static DataSet PruebaDatosTablaDinamicaV5_EM(SqlTransaction tr,
            int t314_idusuario,
            int nDesde,
            int nHasta,
            string t422_idmoneda,
            bool bNodo,
            bool bProyecto,
            bool bCliente,
            bool bResponsable,
            bool bCualidad,
            bool bNaturaleza,
            string sNodo,
            string sProyecto,
            string sCliente,
            string sResponsable,
            string sCualidad,
            string sNaturaleza,
            string sFormulas,
            bool bTablasAuxiliares,
            string sOrdenDimensiones)
        {
            SqlParameter[] aParam = new SqlParameter[19];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario);
            aParam[i++] = ParametroSql.add("@nDesde", SqlDbType.Int, 4, nDesde);
            aParam[i++] = ParametroSql.add("@nHasta", SqlDbType.Int, 4, nHasta);
            aParam[i++] = ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda);
            aParam[i++] = ParametroSql.add("@bNodo", SqlDbType.Bit, 1, bNodo);
            aParam[i++] = ParametroSql.add("@bProyecto", SqlDbType.Bit, 1, bProyecto);
            aParam[i++] = ParametroSql.add("@bCliente", SqlDbType.Bit, 1, bCliente);
            aParam[i++] = ParametroSql.add("@bResponsable", SqlDbType.Bit, 1, bResponsable);
            aParam[i++] = ParametroSql.add("@bCualidad", SqlDbType.Bit, 1, bCualidad);
            aParam[i++] = ParametroSql.add("@bNaturaleza", SqlDbType.Bit, 1, bNaturaleza);
            aParam[i++] = ParametroSql.add("@sNodo", SqlDbType.VarChar, 8000, sNodo);
            aParam[i++] = ParametroSql.add("@sProyecto", SqlDbType.VarChar, 8000, sProyecto);
            aParam[i++] = ParametroSql.add("@sCliente", SqlDbType.VarChar, 8000, sCliente);
            aParam[i++] = ParametroSql.add("@sResponsable", SqlDbType.VarChar, 8000, sResponsable);
            aParam[i++] = ParametroSql.add("@sCualidad", SqlDbType.VarChar, 8000, sCualidad);
            aParam[i++] = ParametroSql.add("@sNaturaleza", SqlDbType.VarChar, 8000, sNaturaleza);
            aParam[i++] = ParametroSql.add("@sFormulas", SqlDbType.VarChar, 8000, sFormulas);
            aParam[i++] = ParametroSql.add("@bTablasAuxiliares", SqlDbType.Bit, 1, bTablasAuxiliares);
            aParam[i++] = ParametroSql.add("@sOrdenDimensiones", SqlDbType.VarChar, 100, sOrdenDimensiones);

            if (tr == null)
                return SqlHelper.ExecuteDataset("PRU_TABLADINAMICA_V5_EM", aParam);
            else
                return SqlHelper.ExecuteDatasetTransaccion(tr, "PRU_TABLADINAMICA_V5_EM", aParam);
        }
        #endregion

        #region Análisis Económico
        public static DataSet AnalisisEconomico(SqlTransaction tr,
            int t314_idusuario,
            int nDesde,
            int nHasta,
            string t422_idmoneda,
            string sCategoria_cri,
            string sCualidad_cri,
            string sSubnodos_cri,
            string sResponsables_cri,
            string sSectores_cri,
            string sSegmentos_cri,
            string sNaturalezas_cri,
            string sClientes_cri,
            string sModeloContrato_cri,
            string sContrato_cri ,
            string sPSN_cri,
            string sComerciales_cri,
            string sSoporteAdm_cri,
            bool bSN4,
            bool bSN3,
            bool bSN2,
            bool bSN1,
            bool bNodo,
            bool bCliente,
            bool bResponsable,
            bool bComercial,
            bool bContrato,
            bool bProyecto,
            bool bModeloContrato,
            bool bNaturaleza,
            bool bSector,
            bool bSegmento,
            string sSN4,
            string sSN3,
            string sSN2,
            string sSN1,
            string sNodo,
            string sCliente,
            string sResponsable,
            string sComercial,
            string sContrato,
            string sPSN,
            string sModeloContrato,
            string sNaturaleza,
            string sSector,
            string sSegmento,
            bool bTablasAuxiliares,
            string sOrdenDimensiones,
            bool bEvolucionMensual,
            string sOrdenMagnitudes,
            int nMagnitudEvolucionMensual,
            string sTipoColumnaEvolucionMensual
        )
        {
            SqlParameter[] aParam = new SqlParameter[]{
				ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario),
				ParametroSql.add("@nDesde", SqlDbType.Int, 4, nDesde),
				ParametroSql.add("@nHasta", SqlDbType.Int, 4, nHasta),
				ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda),
				ParametroSql.add("@t301_categoria_cri", SqlDbType.Char, 1, sCategoria_cri),
				ParametroSql.add("@t305_cualidad_cri", SqlDbType.Char, 1, sCualidad_cri),
				ParametroSql.add("@sSubnodos_cri", SqlDbType.VarChar, sSubnodos_cri),
				ParametroSql.add("@sResponsables_cri", SqlDbType.VarChar, sResponsables_cri),
				ParametroSql.add("@sSectores_cri", SqlDbType.VarChar, sSectores_cri),
				ParametroSql.add("@sSegmentos_cri", SqlDbType.VarChar, sSegmentos_cri),
				ParametroSql.add("@sNaturalezas_cri", SqlDbType.VarChar, sNaturalezas_cri),
				ParametroSql.add("@sClientes_cri", SqlDbType.VarChar, sClientes_cri),
				ParametroSql.add("@sModeloContrato_cri", SqlDbType.VarChar, sModeloContrato_cri),
				ParametroSql.add("@sContrato_cri", SqlDbType.VarChar, sContrato_cri),
				ParametroSql.add("@sPSN_cri", SqlDbType.VarChar, sPSN_cri),
                ParametroSql.add("@sComerciales_cri", SqlDbType.VarChar, sComerciales_cri),
                ParametroSql.add("@sSoporteAdm_cri", SqlDbType.VarChar, sSoporteAdm_cri),
                ParametroSql.add("@bSN4", SqlDbType.Bit, 1, bSN4),
				ParametroSql.add("@bSN3", SqlDbType.Bit, 1, bSN3),
				ParametroSql.add("@bSN2", SqlDbType.Bit, 1, bSN2),
				ParametroSql.add("@bSN1", SqlDbType.Bit, 1, bSN1),
				ParametroSql.add("@bNodo", SqlDbType.Bit, 1, bNodo),
				ParametroSql.add("@bCliente", SqlDbType.Bit, 1, bCliente),
				ParametroSql.add("@bResponsable", SqlDbType.Bit, 1, bResponsable),
				ParametroSql.add("@bComercial", SqlDbType.Bit, 1, bComercial),
				ParametroSql.add("@bContrato", SqlDbType.Bit, 1, bContrato),
				ParametroSql.add("@bProyecto", SqlDbType.Bit, 1, bProyecto),
				ParametroSql.add("@bModeloContrato", SqlDbType.Bit, 1, bModeloContrato),
				ParametroSql.add("@bNaturaleza", SqlDbType.Bit, 1, bNaturaleza),
				ParametroSql.add("@bSector", SqlDbType.Bit, 1, bSector),
				ParametroSql.add("@bSegmento", SqlDbType.Bit, 1, bSegmento),
				ParametroSql.add("@sSN4", SqlDbType.VarChar, sSN4),
				ParametroSql.add("@sSN3", SqlDbType.VarChar, sSN3),
				ParametroSql.add("@sSN2", SqlDbType.VarChar, sSN2),
				ParametroSql.add("@sSN1", SqlDbType.VarChar, sSN1),
				ParametroSql.add("@sNodo", SqlDbType.VarChar, sNodo),
				ParametroSql.add("@sCliente", SqlDbType.VarChar, sCliente),
				ParametroSql.add("@sResponsable", SqlDbType.VarChar, sResponsable),
				ParametroSql.add("@sComercial", SqlDbType.VarChar, sComercial),
				ParametroSql.add("@sContrato", SqlDbType.VarChar, sContrato),
				ParametroSql.add("@sPSN", SqlDbType.VarChar, sPSN),
				ParametroSql.add("@sModeloContrato", SqlDbType.VarChar, sModeloContrato),
				ParametroSql.add("@sNaturaleza", SqlDbType.VarChar, sNaturaleza),
				ParametroSql.add("@sSector", SqlDbType.VarChar, sSector),
				ParametroSql.add("@sSegmento", SqlDbType.VarChar, sSegmento),
				ParametroSql.add("@bTablasAuxiliares", SqlDbType.Bit, 1, bTablasAuxiliares),
				ParametroSql.add("@sOrdenDimensiones", SqlDbType.VarChar, 100, sOrdenDimensiones),
				ParametroSql.add("@sOrdenMagnitudes", SqlDbType.VarChar, 100, sOrdenMagnitudes),
				ParametroSql.add("@nMagnitudEvolucionMensual", SqlDbType.Int, 4, nMagnitudEvolucionMensual),
				ParametroSql.add("@sTipoColumnaEvolucionMensual", SqlDbType.VarChar, 5, sTipoColumnaEvolucionMensual)
            };

            if (tr == null)
                return SqlHelper.ExecuteDataset(((bEvolucionMensual) ? "SUP_BBII_ECONOMICO_EM" : "SUP_BBII_ECONOMICO"), aParam);
            else
                return SqlHelper.ExecuteDatasetTransaccion(tr, ((bEvolucionMensual) ? "SUP_BBII_ECONOMICO_EM" : "SUP_BBII_ECONOMICO"), aParam);
        }

        public static DataSet AnalisisEconomicoProfundizacion(SqlTransaction tr,
            int t314_idusuario,
            int nDesde,
            int nHasta,
            string t422_idmoneda,
            string sCategoria_cri,
            string sCualidad_cri,
            string sSubnodos_cri,
            string sResponsables_cri,
            string sSectores_cri,
            string sSegmentos_cri,
            string sNaturalezas_cri,
            string sClientes_cri,
            string sModeloContrato_cri,
            string sContrato_cri,
            string sPSN_cri,
            string sComerciales_cri,
            string sSoporteAdm_cri,
            Nullable<int> nSN4,
            Nullable<int> nSN3,
            Nullable<int> nSN2,
            Nullable<int> nSN1,
            Nullable<int> nNodo,
            Nullable<int> nCliente,
            Nullable<int> nResponsable,
            Nullable<int> nComercial,
            Nullable<int> nContrato,
            Nullable<int> nPSN,
            Nullable<byte> nModeloContrato,
            Nullable<int> nNaturaleza,
            Nullable<int> nSector,
            Nullable<int> nSegmento
        )
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario),
                ParametroSql.add("@nDesde", SqlDbType.Int, 4, nDesde),
                ParametroSql.add("@nHasta", SqlDbType.Int, 4, nHasta),
                ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda),
                ParametroSql.add("@t301_categoria_cri", SqlDbType.Char, 1, sCategoria_cri),
                ParametroSql.add("@t305_cualidad_cri", SqlDbType.Char, 1, sCualidad_cri),
                ParametroSql.add("@sSubnodos_cri", SqlDbType.VarChar, 8000, sSubnodos_cri),
                ParametroSql.add("@sResponsables_cri", SqlDbType.VarChar, 8000, sResponsables_cri),
                ParametroSql.add("@sSectores_cri", SqlDbType.VarChar, 8000, sSectores_cri),
                ParametroSql.add("@sSegmentos_cri", SqlDbType.VarChar, 8000, sSegmentos_cri),
                ParametroSql.add("@sNaturalezas_cri", SqlDbType.VarChar, 8000, sNaturalezas_cri),
                ParametroSql.add("@sClientes_cri", SqlDbType.VarChar, 8000, sClientes_cri),
                ParametroSql.add("@sModeloContrato_cri", SqlDbType.VarChar, 8000, sModeloContrato_cri),
                ParametroSql.add("@sContrato_cri", SqlDbType.VarChar, 8000, sContrato_cri),
                ParametroSql.add("@sPSN_cri", SqlDbType.VarChar, 8000, sPSN_cri),
                ParametroSql.add("@sComerciales_cri", SqlDbType.VarChar, 8000, sComerciales_cri),
                ParametroSql.add("@sSoporteAdm_cri", SqlDbType.VarChar, 8000, sSoporteAdm_cri),
                ParametroSql.add("@nSN4", SqlDbType.Int, 4, nSN4),
                ParametroSql.add("@nSN3", SqlDbType.Int, 4, nSN3),
                ParametroSql.add("@nSN2", SqlDbType.Int, 4, nSN2),
                ParametroSql.add("@nSN1", SqlDbType.Int, 4, nSN1),
                ParametroSql.add("@nNodo", SqlDbType.Int, 4, nNodo),
                ParametroSql.add("@nCliente", SqlDbType.Int, 4, nCliente),
                ParametroSql.add("@nResponsable", SqlDbType.Int, 4, nResponsable),
                ParametroSql.add("@nComercial", SqlDbType.Int, 4, nComercial),
                ParametroSql.add("@nContrato", SqlDbType.Int, 4, nContrato),
                ParametroSql.add("@nPSN", SqlDbType.Int, 4, nPSN),
                ParametroSql.add("@nModeloContrato", SqlDbType.TinyInt, 1, nModeloContrato),
                ParametroSql.add("@nNaturaleza", SqlDbType.Int, 4, nNaturaleza),
                ParametroSql.add("@nSector", SqlDbType.Int, 4, nSector),
                ParametroSql.add("@nSegmento", SqlDbType.Int, 4, nSegmento)
            };

            if (tr == null)
                return SqlHelper.ExecuteDataset("SUP_BBII_ECONOMICO_PROF", aParam);
            else
                return SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_BBII_ECONOMICO_PROF", aParam);
        }

        public static SqlDataReader AnalisisEconomicoProfundizacionN2(SqlTransaction tr,
            int t305_idproyectosubnodo,
            int t325_anomes,
            int t454_idformula,
            string t422_idmoneda,
            bool bAgrupado
        )
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo),
                ParametroSql.add("@t325_anomes", SqlDbType.Int, 4, t325_anomes),
                ParametroSql.add("@t454_idformula", SqlDbType.Int, 4, t454_idformula),
                ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda),
                ParametroSql.add("@bAgrupado", SqlDbType.Bit, 1, bAgrupado)
            };

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_BBII_ECONOMICO_PROFN2", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_BBII_ECONOMICO_PROFN2", aParam);
        }


        public static SqlDataReader AnalisisEconomicoMeses(SqlTransaction tr,
            int t305_idproyectosubnodo,
            int nDesde,
            int nHasta,
            string t422_idmoneda
        )
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo),
                ParametroSql.add("@nDesde", SqlDbType.Int, 4, nDesde),
                ParametroSql.add("@nHasta", SqlDbType.Int, 4, nHasta),
                ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda)
            };

            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_BBII_ECONOMICO_MESPSN", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_BBII_ECONOMICO_MESPSN", aParam);
        }

        #endregion

        #region Análisis Financiero
        public static DataSet AnalisisFinanciero(SqlTransaction tr,
            Nullable<int> t314_idusuario,
            int t001_idficepi, 
            int anomes,
            string t422_idmoneda,
            string sCategoria_cri,
            string sCualidad_cri,
            string sSubnodos_cri,
            string sResponsables_cri,
            string sSectores_cri,
            string sSegmentos_cri,
            string sNaturalezas_cri,
            string sClientes_cri,
            string sModeloContrato_cri,
            string sContrato_cri,
            string sPSN_cri,
            string sComerciales_cri,
            string sSoporteAdm_cri,
            bool bSN4,
            bool bSN3,
            bool bSN2,
            bool bSN1,
            bool bNodo,
            bool bCliente,
            bool bResponsable,
            bool bComercial,
            bool bContrato,
            bool bProyecto,
            bool bModeloContrato,
            bool bNaturaleza,
            bool bSector,
            bool bSegmento,
            string sSN4,
            string sSN3,
            string sSN2,
            string sSN1,
            string sNodo,
            string sCliente,
            string sResponsable,
            string sComercial,
            string sContrato,
            string sPSN,
            string sModeloContrato,
            string sNaturaleza,
            string sSector,
            string sSegmento,
            bool bTablasAuxiliares,
            string sOrdenDimensiones
        )
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario),
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi),
                ParametroSql.add("@anomes", SqlDbType.Int, 4, anomes),
                ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda),
                ParametroSql.add("@t301_categoria_cri", SqlDbType.Char, 1, sCategoria_cri),
                ParametroSql.add("@t305_cualidad_cri", SqlDbType.Char, 1, sCualidad_cri),
                ParametroSql.add("@sSubnodos_cri", SqlDbType.VarChar, 8000, sSubnodos_cri),
                ParametroSql.add("@sResponsables_cri", SqlDbType.VarChar, 8000, sResponsables_cri),
                ParametroSql.add("@sSectores_cri", SqlDbType.VarChar, 8000, sSectores_cri),
                ParametroSql.add("@sSegmentos_cri", SqlDbType.VarChar, 8000, sSegmentos_cri),
                ParametroSql.add("@sNaturalezas_cri", SqlDbType.VarChar, 8000, sNaturalezas_cri),
                ParametroSql.add("@sClientes_cri", SqlDbType.VarChar, 8000, sClientes_cri),
                ParametroSql.add("@sModeloContrato_cri", SqlDbType.VarChar, 8000, sModeloContrato_cri),
                ParametroSql.add("@sContrato_cri", SqlDbType.VarChar, 8000, sContrato_cri),
                ParametroSql.add("@sPSN_cri", SqlDbType.VarChar, 8000, sPSN_cri),
                ParametroSql.add("@sComerciales_cri", SqlDbType.VarChar, 8000, sComerciales_cri),
                ParametroSql.add("@sSoporteAdm_cri", SqlDbType.VarChar, 8000, sSoporteAdm_cri),
                ParametroSql.add("@bSN4", SqlDbType.Bit, 1, bSN4),
                ParametroSql.add("@bSN3", SqlDbType.Bit, 1, bSN3),
                ParametroSql.add("@bSN2", SqlDbType.Bit, 1, bSN2),
                ParametroSql.add("@bSN1", SqlDbType.Bit, 1, bSN1),
                ParametroSql.add("@bNodo", SqlDbType.Bit, 1, bNodo),
                ParametroSql.add("@bCliente", SqlDbType.Bit, 1, bCliente),
                ParametroSql.add("@bResponsable", SqlDbType.Bit, 1, bResponsable),
                ParametroSql.add("@bComercial", SqlDbType.Bit, 1, bComercial),
                ParametroSql.add("@bContrato", SqlDbType.Bit, 1, bContrato),
                ParametroSql.add("@bProyecto", SqlDbType.Bit, 1, bProyecto),
                ParametroSql.add("@bModeloContrato", SqlDbType.Bit, 1, bModeloContrato),
                ParametroSql.add("@bNaturaleza", SqlDbType.Bit, 1, bNaturaleza),
                ParametroSql.add("@bSector", SqlDbType.Bit, 1, bSector),
                ParametroSql.add("@bSegmento", SqlDbType.Bit, 1, bSegmento),
                ParametroSql.add("@sSN4", SqlDbType.VarChar, 8000, sSN4),
                ParametroSql.add("@sSN3", SqlDbType.VarChar, 8000, sSN3),
                ParametroSql.add("@sSN2", SqlDbType.VarChar, 8000, sSN2),
                ParametroSql.add("@sSN1", SqlDbType.VarChar, 8000, sSN1),
                ParametroSql.add("@sNodo", SqlDbType.VarChar, 8000, sNodo),
                ParametroSql.add("@sCliente", SqlDbType.VarChar, 8000, sCliente),
                ParametroSql.add("@sResponsable", SqlDbType.VarChar, 8000, sResponsable),
                ParametroSql.add("@sComercial", SqlDbType.VarChar, 8000, sComercial),
                ParametroSql.add("@sContrato", SqlDbType.VarChar, 8000, sContrato),
                ParametroSql.add("@sPSN", SqlDbType.VarChar, 8000, sPSN),
                ParametroSql.add("@sModeloContrato", SqlDbType.VarChar, 8000, sModeloContrato),
                ParametroSql.add("@sNaturaleza", SqlDbType.VarChar, 8000, sNaturaleza),
                ParametroSql.add("@sSector", SqlDbType.VarChar, 8000, sSector),
                ParametroSql.add("@sSegmento", SqlDbType.VarChar, 8000, sSegmento),
                ParametroSql.add("@bTablasAuxiliares", SqlDbType.Bit, 1, bTablasAuxiliares),
                ParametroSql.add("@sOrdenDimensiones", SqlDbType.VarChar, 100, sOrdenDimensiones)
            };

            if (tr == null)
                return SqlHelper.ExecuteDataset("SUP_BBII_FINANCIERO", aParam);
            else
                return SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_BBII_FINANCIERO", aParam);
        }

        public static DataSet AnalisisFinancieroProfundizacion(SqlTransaction tr,
            Nullable<int> t314_idusuario,
            int t001_idficepi,
            int anomes,
            string t422_idmoneda,
            string sCategoria_cri,
            string sCualidad_cri,
            string sSubnodos_cri,
            string sResponsables_cri,
            string sSectores_cri,
            string sSegmentos_cri,
            string sNaturalezas_cri,
            string sClientes_cri,
            string sModeloContrato_cri,
            string sContrato_cri,
            string sPSN_cri,
            string sComerciales_cri,
            string sSoporteAdm_cri,
            Nullable<int> nSN4,
            Nullable<int> nSN3,
            Nullable<int> nSN2,
            Nullable<int> nSN1,
            Nullable<int> nNodo,
            Nullable<int> nCliente,
            Nullable<int> nResponsable,
            Nullable<int> nComercial,
            Nullable<int> nContrato,
            Nullable<int> nPSN,
            Nullable<byte> nModeloContrato,
            Nullable<int> nNaturaleza,
            Nullable<int> nSector,
            Nullable<int> nSegmento
        )
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario),
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi),
                ParametroSql.add("@anomes", SqlDbType.Int, 4, anomes),
                ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda),
                ParametroSql.add("@t301_categoria_cri", SqlDbType.Char, 1, sCategoria_cri),
                ParametroSql.add("@t305_cualidad_cri", SqlDbType.Char, 1, sCualidad_cri),
                ParametroSql.add("@sSubnodos_cri", SqlDbType.VarChar, 8000, sSubnodos_cri),
                ParametroSql.add("@sResponsables_cri", SqlDbType.VarChar, 8000, sResponsables_cri),
                ParametroSql.add("@sSectores_cri", SqlDbType.VarChar, 8000, sSectores_cri),
                ParametroSql.add("@sSegmentos_cri", SqlDbType.VarChar, 8000, sSegmentos_cri),
                ParametroSql.add("@sNaturalezas_cri", SqlDbType.VarChar, 8000, sNaturalezas_cri),
                ParametroSql.add("@sClientes_cri", SqlDbType.VarChar, 8000, sClientes_cri),
                ParametroSql.add("@sModeloContrato_cri", SqlDbType.VarChar, 8000, sModeloContrato_cri),
                ParametroSql.add("@sContrato_cri", SqlDbType.VarChar, 8000, sContrato_cri),
                ParametroSql.add("@sPSN_cri", SqlDbType.VarChar, 8000, sPSN_cri),
                ParametroSql.add("@sComerciales_cri", SqlDbType.VarChar, 8000, sComerciales_cri),
                ParametroSql.add("@sSoporteAdm_cri", SqlDbType.VarChar, 8000, sSoporteAdm_cri),
                ParametroSql.add("@nSN4", SqlDbType.Int, 4, nSN4),
                ParametroSql.add("@nSN3", SqlDbType.Int, 4, nSN3),
                ParametroSql.add("@nSN2", SqlDbType.Int, 4, nSN2),
                ParametroSql.add("@nSN1", SqlDbType.Int, 4, nSN1),
                ParametroSql.add("@nNodo", SqlDbType.Int, 4, nNodo),
                ParametroSql.add("@nCliente", SqlDbType.Int, 4, nCliente),
                ParametroSql.add("@nResponsable", SqlDbType.Int, 4, nResponsable),
                ParametroSql.add("@nComercial", SqlDbType.Int, 4, nComercial),
                ParametroSql.add("@nContrato", SqlDbType.Int, 4, nContrato),
                ParametroSql.add("@nPSN", SqlDbType.Int, 4, nPSN),
                ParametroSql.add("@nModeloContrato", SqlDbType.TinyInt, 1, nModeloContrato),
                ParametroSql.add("@nNaturaleza", SqlDbType.Int, 4, nNaturaleza),
                ParametroSql.add("@nSector", SqlDbType.Int, 4, nSector),
                ParametroSql.add("@nSegmento", SqlDbType.Int, 4, nSegmento)
            };

            if (tr == null)
                return SqlHelper.ExecuteDataset("SUP_BBII_FINANCIERO_PROF", aParam);
            else
                return SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_BBII_FINANCIERO_PROF", aParam);
        }


        #endregion

        #region Vencimiento de Facturas
        public static DataSet VencimientoFacturas(SqlTransaction tr,
            Nullable<int> t314_idusuario,
            int t001_idficepi,
            string t422_idmoneda,
            string sCategoria_cri,
            string sCualidad_cri,
            string sSubnodos_cri,
            string sResponsables_cri,
            string sSectores_cri,
            string sSegmentos_cri,
            string sNaturalezas_cri,
            string sClientes_cri,
            string sModeloContrato_cri,
            string sContrato_cri,
            string sPSN_cri,
            string sComerciales_cri,
            string sClienteFact_cri,
            string sSectorFact_cri,
            string sSegmentoFact_cri,
            string sEmpresaFact_cri,
            string sSoporteAdm_cri,
            bool bSN4,
            bool bSN3,
            bool bSN2,
            bool bSN1,
            bool bNodo,
            bool bCliente,
            bool bResponsable,
            bool bComercial,
            bool bContrato,
            bool bProyecto,
            bool bModeloContrato,
            bool bNaturaleza,
            bool bSector,
            bool bSegmento,
            bool bClienteFact,
            bool bSectorFact,
            bool bSegmentoFact,
            bool bEmpresaFact,
            string sSN4,
            string sSN3,
            string sSN2,
            string sSN1,
            string sNodo,
            string sCliente,
            string sResponsable,
            string sComercial,
            string sContrato,
            string sPSN,
            string sModeloContrato,
            string sNaturaleza,
            string sSector,
            string sSegmento,
            string sClienteFact,
            string sSectorFact,
            string sSegmentoFact,
            string sEmpresaFact,
            bool bTablasAuxiliares,
            string sOrdenDimensiones
        )
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario),
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi),
                ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda),
                ParametroSql.add("@t301_categoria_cri", SqlDbType.Char, 1, sCategoria_cri),
                ParametroSql.add("@t305_cualidad_cri", SqlDbType.Char, 1, sCualidad_cri),
                ParametroSql.add("@sSubnodos_cri", SqlDbType.VarChar, 8000, sSubnodos_cri),
                ParametroSql.add("@sResponsables_cri", SqlDbType.VarChar, 8000, sResponsables_cri),
                ParametroSql.add("@sSectores_cri", SqlDbType.VarChar, 8000, sSectores_cri),
                ParametroSql.add("@sSegmentos_cri", SqlDbType.VarChar, 8000, sSegmentos_cri),
                ParametroSql.add("@sNaturalezas_cri", SqlDbType.VarChar, 8000, sNaturalezas_cri),
                ParametroSql.add("@sClientes_cri", SqlDbType.VarChar, 8000, sClientes_cri),
                ParametroSql.add("@sModeloContrato_cri", SqlDbType.VarChar, 8000, sModeloContrato_cri),
                ParametroSql.add("@sContrato_cri", SqlDbType.VarChar, 8000, sContrato_cri),
                ParametroSql.add("@sPSN_cri", SqlDbType.VarChar, 8000, sPSN_cri),
                ParametroSql.add("@sComerciales_cri", SqlDbType.VarChar, 8000, sComerciales_cri),
                ParametroSql.add("@sClienteFact_cri", SqlDbType.VarChar, 8000, sClienteFact_cri),
                ParametroSql.add("@sSectorFact_cri", SqlDbType.VarChar, 8000, sSectorFact_cri),
                ParametroSql.add("@sSegmentoFact_cri", SqlDbType.VarChar, 8000, sSegmentoFact_cri),
                ParametroSql.add("@sEmpresaFact_cri", SqlDbType.VarChar, 8000, sEmpresaFact_cri),
                ParametroSql.add("@sSoporteAdm_cri", SqlDbType.VarChar, 8000, sSoporteAdm_cri),
                ParametroSql.add("@bSN4", SqlDbType.Bit, 1, bSN4),
                ParametroSql.add("@bSN3", SqlDbType.Bit, 1, bSN3),
                ParametroSql.add("@bSN2", SqlDbType.Bit, 1, bSN2),
                ParametroSql.add("@bSN1", SqlDbType.Bit, 1, bSN1),
                ParametroSql.add("@bNodo", SqlDbType.Bit, 1, bNodo),
                ParametroSql.add("@bCliente", SqlDbType.Bit, 1, bCliente),
                ParametroSql.add("@bResponsable", SqlDbType.Bit, 1, bResponsable),
                ParametroSql.add("@bComercial", SqlDbType.Bit, 1, bComercial),
                ParametroSql.add("@bContrato", SqlDbType.Bit, 1, bContrato),
                ParametroSql.add("@bProyecto", SqlDbType.Bit, 1, bProyecto),
                ParametroSql.add("@bModeloContrato", SqlDbType.Bit, 1, bModeloContrato),
                ParametroSql.add("@bNaturaleza", SqlDbType.Bit, 1, bNaturaleza),
                ParametroSql.add("@bSector", SqlDbType.Bit, 1, bSector),
                ParametroSql.add("@bSegmento", SqlDbType.Bit, 1, bSegmento),
                ParametroSql.add("@bClienteFact", SqlDbType.Bit, 1, bClienteFact),
                ParametroSql.add("@bSectorFact", SqlDbType.Bit, 1, bSectorFact),
                ParametroSql.add("@bSegmentoFact", SqlDbType.Bit, 1, bSegmentoFact),
                ParametroSql.add("@bEmpresaFact", SqlDbType.Bit, 1, bEmpresaFact),
                ParametroSql.add("@sSN4", SqlDbType.VarChar, 8000, sSN4),
                ParametroSql.add("@sSN3", SqlDbType.VarChar, 8000, sSN3),
                ParametroSql.add("@sSN2", SqlDbType.VarChar, 8000, sSN2),
                ParametroSql.add("@sSN1", SqlDbType.VarChar, 8000, sSN1),
                ParametroSql.add("@sNodo", SqlDbType.VarChar, 8000, sNodo),
                ParametroSql.add("@sCliente", SqlDbType.VarChar, 8000, sCliente),
                ParametroSql.add("@sResponsable", SqlDbType.VarChar, 8000, sResponsable),
                ParametroSql.add("@sComercial", SqlDbType.VarChar, 8000, sComercial),
                ParametroSql.add("@sContrato", SqlDbType.VarChar, 8000, sContrato),
                ParametroSql.add("@sPSN", SqlDbType.VarChar, 8000, sPSN),
                ParametroSql.add("@sModeloContrato", SqlDbType.VarChar, 8000, sModeloContrato),
                ParametroSql.add("@sNaturaleza", SqlDbType.VarChar, 8000, sNaturaleza),
                ParametroSql.add("@sSector", SqlDbType.VarChar, 8000, sSector),
                ParametroSql.add("@sSegmento", SqlDbType.VarChar, 8000, sSegmento),
                ParametroSql.add("@sClienteFact", SqlDbType.VarChar, 8000, sClienteFact),
                ParametroSql.add("@sSectorFact", SqlDbType.VarChar, 8000, sSectorFact),
                ParametroSql.add("@sSegmentoFact", SqlDbType.VarChar, 8000, sSegmentoFact),
                ParametroSql.add("@sEmpresaFact", SqlDbType.VarChar, 8000, sEmpresaFact),
                ParametroSql.add("@bTablasAuxiliares", SqlDbType.Bit, 1, bTablasAuxiliares),
                ParametroSql.add("@sOrdenDimensiones", SqlDbType.VarChar, 100, sOrdenDimensiones)
                };

            if (tr == null)
                return SqlHelper.ExecuteDataset("SUP_BBII_VENCIMIENTO", aParam);
            else
                return SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_BBII_VENCIMIENTO", aParam);
        }

        public static DataSet VencimientoFacturasProfundizacion(SqlTransaction tr,
            Nullable<int> t314_idusuario,
            int t001_idficepi,
            string t422_idmoneda,
            string sCategoria_cri,
            string sCualidad_cri,
            string sSubnodos_cri,
            string sResponsables_cri,
            string sSectores_cri,
            string sSegmentos_cri,
            string sNaturalezas_cri,
            string sClientes_cri,
            string sModeloContrato_cri,
            string sContrato_cri,
            string sPSN_cri,
            string sComerciales_cri,
            string sClienteFact_cri,
            string sSectorFact_cri,
            string sSegmentoFact_cri,
            string sEmpresaFact_cri,
            string sSoporteAdm_cri,
            Nullable<int> nSN4,
            Nullable<int> nSN3,
            Nullable<int> nSN2,
            Nullable<int> nSN1,
            Nullable<int> nNodo,
            Nullable<int> nCliente,
            Nullable<int> nResponsable,
            Nullable<int> nComercial,
            Nullable<int> nContrato,
            Nullable<int> nPSN,
            Nullable<byte> nModeloContrato,
            Nullable<int> nNaturaleza,
            Nullable<int> nSector,
            Nullable<int> nSegmento,
            Nullable<int> nClienteFact,
            Nullable<int> nSectorFact,
            Nullable<int> nSegmentoFact,
            Nullable<int> nEmpresaFact
        )
        {
            SqlParameter[] aParam = new SqlParameter[]{
                ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario),
                ParametroSql.add("@t001_idficepi", SqlDbType.Int, 4, t001_idficepi),
                ParametroSql.add("@t422_idmoneda", SqlDbType.VarChar, 5, t422_idmoneda),
                ParametroSql.add("@t301_categoria_cri", SqlDbType.Char, 1, sCategoria_cri),
                ParametroSql.add("@t305_cualidad_cri", SqlDbType.Char, 1, sCualidad_cri),
                ParametroSql.add("@sSubnodos_cri", SqlDbType.VarChar, 8000, sSubnodos_cri),
                ParametroSql.add("@sResponsables_cri", SqlDbType.VarChar, 8000, sResponsables_cri),
                ParametroSql.add("@sSectores_cri", SqlDbType.VarChar, 8000, sSectores_cri),
                ParametroSql.add("@sSegmentos_cri", SqlDbType.VarChar, 8000, sSegmentos_cri),
                ParametroSql.add("@sNaturalezas_cri", SqlDbType.VarChar, 8000, sNaturalezas_cri),
                ParametroSql.add("@sClientes_cri", SqlDbType.VarChar, 8000, sClientes_cri),
                ParametroSql.add("@sModeloContrato_cri", SqlDbType.VarChar, 8000, sModeloContrato_cri),
                ParametroSql.add("@sContrato_cri", SqlDbType.VarChar, 8000, sContrato_cri),
                ParametroSql.add("@sPSN_cri", SqlDbType.VarChar, 8000, sPSN_cri),
                ParametroSql.add("@sComerciales_cri", SqlDbType.VarChar, 8000, sComerciales_cri),
                ParametroSql.add("@sClienteFact_cri", SqlDbType.VarChar, 8000, sClienteFact_cri),
                ParametroSql.add("@sSectorFact_cri", SqlDbType.VarChar, 8000, sSectorFact_cri),
                ParametroSql.add("@sSegmentoFact_cri", SqlDbType.VarChar, 8000, sSegmentoFact_cri),
                ParametroSql.add("@sEmpresaFact_cri", SqlDbType.VarChar, 8000, sEmpresaFact_cri),
                ParametroSql.add("@sSoporteAdm_cri", SqlDbType.VarChar, 8000, sSoporteAdm_cri),
                ParametroSql.add("@nSN4", SqlDbType.Int, 4, nSN4),
                ParametroSql.add("@nSN3", SqlDbType.Int, 4, nSN3),
                ParametroSql.add("@nSN2", SqlDbType.Int, 4, nSN2),
                ParametroSql.add("@nSN1", SqlDbType.Int, 4, nSN1),
                ParametroSql.add("@nNodo", SqlDbType.Int, 4, nNodo),
                ParametroSql.add("@nCliente", SqlDbType.Int, 4, nCliente),
                ParametroSql.add("@nResponsable", SqlDbType.Int, 4, nResponsable),
                ParametroSql.add("@nComercial", SqlDbType.Int, 4, nComercial),
                ParametroSql.add("@nContrato", SqlDbType.Int, 4, nContrato),
                ParametroSql.add("@nPSN", SqlDbType.Int, 4, nPSN),
                ParametroSql.add("@nModeloContrato", SqlDbType.TinyInt, 1, nModeloContrato),
                ParametroSql.add("@nNaturaleza", SqlDbType.Int, 4, nNaturaleza),
                ParametroSql.add("@nSector", SqlDbType.Int, 4, nSector),
                ParametroSql.add("@nSegmento", SqlDbType.Int, 4, nSegmento),
                ParametroSql.add("@nClienteFact", SqlDbType.Int, 4, nClienteFact),
                ParametroSql.add("@nSectorFact", SqlDbType.Int, 4, nSectorFact),
                ParametroSql.add("@nSegmentoFact", SqlDbType.Int, 4, nSegmentoFact),
                ParametroSql.add("@nEmpresaFact", SqlDbType.Int, 4, nEmpresaFact)
            };

            if (tr == null)
                return SqlHelper.ExecuteDataset("SUP_BBII_VENCIMIENTO_PROF", aParam);
            else
                return SqlHelper.ExecuteDatasetTransaccion(tr, "SUP_BBII_VENCIMIENTO_PROF", aParam);
        }


        #endregion

        #endregion
        #endregion

        #region Duplicar
        /// <summary>
        /// Devuelve el tipo de visión que un usuario tiene sobre un proyecto
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t001_idficepi"></param>
        /// <param name="t301_idproyecto"></param>
        /// <param name="t174_motivono"></param>
        /// <returns>modo_lectura y motivo de visión</returns>
        public static SqlDataReader ProyectoVision(SqlTransaction tr, int t314_idusuario, int t305_idproyectosubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@nUsuario", SqlDbType.Int, 4, t314_idusuario),
                ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo)
            };
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_PROY_VISION", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PROY_VISION", aParam);
        }
        /// <summary>
        /// Copia los elementos que forman la estructura téncica de un proyecto económico
        /// Si bCopiarDocs==True mantiene el id de Atenea en los nuevos registros de documento
        /// Sino graba t2_iddocumento a null
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="iNumPSN_Origen"></param>
        /// <param name="iNumPSN_Destino"></param>
        /// <param name="iNumPE_Origen"></param>
        /// <param name="iNumPE_Destino"></param>
        /// <param name="bBitacoraPT"></param>
        /// <param name="bBitacoraTA"></param>
        /// <param name="bHitos"></param>
        /// <param name="sEstadosTarea"></param>
        /// <param name="t314_idusuario"></param>
        /// <param name="bCopiarDocs"></param>
        public static void Duplicar(SqlTransaction tr, int iNumPSN_Origen, int iNumPSN_Destino, int iNumPE_Origen, int iNumPE_Destino,
                                    bool bBitacoraPT, bool bBitacoraTA, bool bHitos, string sEstadosTarea, int t314_idusuario, bool bCopiarDocs, bool bCopiaAE)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t305_idproyectosubnodo_origen", SqlDbType.Int, 4, iNumPSN_Origen),
                ParametroSql.add("@t305_idproyectosubnodo_destino", SqlDbType.Int, 4, iNumPSN_Destino),
                ParametroSql.add("@t301_idproyecto_origen", SqlDbType.Int, 4, iNumPE_Origen),
                ParametroSql.add("@t301_idproyecto_destino", SqlDbType.Int, 4, iNumPE_Destino),
                ParametroSql.add("@Bitacora_PT", SqlDbType.Bit, 1, bBitacoraPT),
                ParametroSql.add("@Bitacora_TA", SqlDbType.Bit, 1, bBitacoraTA),
                ParametroSql.add("@Hitos", SqlDbType.Bit, 1, bHitos),
                ParametroSql.add("@EstadoTarea", SqlDbType.VarChar, 15, sEstadosTarea),
                ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario),
                ParametroSql.add("@bCopiarDocs", SqlDbType.Bit, 1, bCopiarDocs),
                ParametroSql.add("@bCopiaAtrest", SqlDbType.Bit, 1, bCopiaAE)
            };
            SqlHelper.ExecuteNonQueryTransaccion(tr, "SUP_DUPLICAR_ET", aParam);
        }
        /// <summary>
        /// Obtiene una lista de documentos de PTs de un PSN 
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t305_idproyectosubnodo"></param>
        /// <returns></returns>
        public static SqlDataReader ListaPTconDoc(SqlTransaction tr, int t305_idproyectosubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo)
            };
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_PSN_PTDOC", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PSN_PTDOC", aParam);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t305_idproyectosubnodo"></param>
        /// <returns></returns>
        public static SqlDataReader ListaAsuntoPTconDoc(SqlTransaction tr, int t305_idproyectosubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[]{  
                ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo)
            };
            if (tr == null)
                return SqlHelper.ExecuteSqlDataReader("SUP_PSN_ASUPTDOC", aParam);
            else
                return SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PSN_ASUPTDOC", aParam);
        }
        #endregion

        public static string GetNombre(int t305_idproyectosubnodo)
        {
            string sRes = "";
            SqlDataReader dr = SUPER.Capa_Negocio.PROYECTO.fgGetDatosProy(t305_idproyectosubnodo);
            if (dr.Read())
                sRes = dr["t301_idproyecto"].ToString() + "-" + dr["t301_denominacion"].ToString();
            dr.Close();
            dr.Dispose();

            return sRes;
        }
        public static byte GetModalidad(int t305_idproyectosubnodo)
        {
            byte idMod = 0;
            SqlDataReader dr = SUPER.Capa_Negocio.PROYECTO.fgGetDatosProy4(t305_idproyectosubnodo);
            if (dr.Read())
                idMod = byte.Parse(dr["ModalidadProyecto"].ToString() );
            dr.Close();
            dr.Dispose();

            return idMod;
        }
        public static string GetCodigoExternoClienteProyecto(int t305_idproyectosubnodo)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);

            object miObj= SqlHelper.ExecuteScalar("SUP_CODEXTCLIPROY_S", aParam);

            return (string)miObj;
        }
        /// <summary>
        /// Obtiene las tareas vivas de un PSN
        /// </summary>
        /// <param name="tr"></param>
        /// <param name="t305_idproyectosubnodo"></param>
        /// <param name="t314_idusuario">Restringe las tareas a las asignadas al profesional</param>
        /// <param name="bAsignadas">Indica que hay que restringir las tareas al profesional</param>
        /// <param name="bSoloActivas">Restringe las tareas a las que su asociación al profesional este activa</param>
        /// <returns></returns>
        public static List<SUPER.Capa_Negocio.TAREAPSP> GetTareasVivas(SqlTransaction tr, int t305_idproyectosubnodo, int t314_idusuario, 
                                                   bool bAsignadas, bool bSoloActivas)
        {
            #region Obterner el DataReader
            SqlDataReader dr;
            if (bAsignadas)
            {
                SqlParameter[] aParam = new SqlParameter[]{  
                    ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo),
                    ParametroSql.add("@t314_idusuario", SqlDbType.Int, 4, t314_idusuario),
                    ParametroSql.add("@bSoloActivas", SqlDbType.Bit, 1, bSoloActivas)
                };
                if (tr == null)
                    dr= SqlHelper.ExecuteSqlDataReader("SUP_TAREAS_PROFESIONAL_ByPSN", aParam);
                else
                    dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_TAREAS_PROFESIONAL_ByPSN", aParam);
            }
            else
            {
                SqlParameter[] aParam = new SqlParameter[]{  
                    ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo)
                };
                if (tr == null)
                    dr = SqlHelper.ExecuteSqlDataReader("SUP_TAREA_S1", aParam);
                else
                    dr = SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_TAREA_S1", aParam);
            }
            #endregion
            List<SUPER.Capa_Negocio.TAREAPSP> aTareas = new List<SUPER.Capa_Negocio.TAREAPSP>();
            SUPER.Capa_Negocio.TAREAPSP oTarea;

            while (dr.Read())
            {
                oTarea = new SUPER.Capa_Negocio.TAREAPSP();
                oTarea.t332_idtarea = int.Parse(dr["t332_idtarea"].ToString());
                oTarea.t332_notif_prof = (bool)dr["t332_notif_prof"];
                oTarea.t332_destarea = dr["t332_destarea"].ToString();
                oTarea.num_proyecto = int.Parse(dr["num_proyecto"].ToString());
                oTarea.nom_proyecto = dr["nom_proyecto"].ToString();
                oTarea.t331_despt = dr["t331_despt"].ToString();
                oTarea.t334_desfase = dr["t334_desfase"].ToString();
                oTarea.t335_desactividad = dr["t335_desactividad"].ToString();
                oTarea.t346_codpst = dr["t346_codpst"].ToString();
                oTarea.t346_despst = dr["t346_despst"].ToString();
                oTarea.t332_otl = dr["t332_otl"].ToString();
                oTarea.t332_incidencia = dr["t332_incidencia"].ToString();
                oTarea.t332_mensaje = dr["t332_mensaje"].ToString();

                aTareas.Add(oTarea);
            }
            dr.Close();
            dr.Dispose();

            return aTareas;
        }

        /// <summary>
        /// Dada una lista de proyectos, obtiene la lista de códigos de subnodo correspondientes
        /// </summary>        
        /// <param name="slProyecto"></param>
        /// <returns></returns>
        public static SqlDataReader ObtenerProyectosSubnodo(string slProyecto)
        {
            SqlParameter[] aParam = new SqlParameter[1];
            aParam[0] = new SqlParameter("@sProyectos", SqlDbType.VarChar, 8000);
            aParam[0].Value = slProyecto;

            return SqlHelper.ExecuteSqlDataReader("SUP_GETIDPROYECTOSSUBNODO", aParam);
        }

        public static int GetUltimoMesCerrado(SqlTransaction tr, int t305_idproyectosubnodo)
        {
            int iAnoMes = 0;
            SqlDataReader dr;
            SqlParameter[] aParam = new SqlParameter[1];
            int i = 0;
            aParam[i++] = ParametroSql.add("@t305_idproyectosubnodo", SqlDbType.Int, 4, t305_idproyectosubnodo);
            

            if (tr == null)
                dr= SqlHelper.ExecuteSqlDataReader("SUP_PSN_UMC", aParam);
            else
                dr= SqlHelper.ExecuteSqlDataReaderTransaccion(tr, "SUP_PSN_UMC", aParam);

            if (dr.Read())
                iAnoMes = int.Parse(dr["t325_anomes"].ToString());
            dr.Close();
            dr.Dispose();

            return iAnoMes;
        }


        #endregion


    }
}
