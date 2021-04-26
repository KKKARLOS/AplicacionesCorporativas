using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using IB.SUPER.IAP30.Models;
using SUPERANTIGUO = SUPER;

/// <summary>
/// Summary description for ConsumoIAPSemana
/// </summary>

namespace IB.SUPER.IAP30.DAL
{

    internal class ConsumoIAPSemana
    {
        #region variables privadas y constructor
        private sqldblib.SqlServerSP cDblib;


        private enum enumDBFields : byte
        {
            nUsuario = 1,
            dDesde = 2,
            dHasta = 3,            
            nPSN = 4,
            nPT = 5,
            nFase = 6,
            nActividad = 7,
            datatablePSN = 8,
            soloPrimerNivel = 9

        }

        internal ConsumoIAPSemana(sqldblib.SqlServerSP extcDblib)
        {
            if (extcDblib == null)
                throw new Exception("Exception en el constructor DAL. Objeto dblib no instanciado.");

            if (extcDblib.Connection.State != System.Data.ConnectionState.Open)
                throw new Exception("Exception en el constructor DAL. Se perdió la conexión a la base de datos.");

            cDblib = extcDblib;
        }

        #endregion

        #region funciones publicas     

        /// <summary>
        /// Obtiene los proyectosubnodos en los que puede imputar esfuerzos un usuario en IAP, en una semana.
        /// </summary>
        internal List<Models.ConsumoIAPSemana> ObtenerConsumosIAPSemanaPSN(int nUsuario, DateTime dDesde, DateTime dHasta)
        {
            Models.ConsumoIAPSemana oConsumoIAPSemana = null;
            List<Models.ConsumoIAPSemana> lst = new List<Models.ConsumoIAPSemana>();
            IDataReader dr = null;
            //log4net.ILog cLog = SUPERANTIGUO.BLL.Log.logger;

            try
            {

                SqlParameter[] dbparams = new SqlParameter[3] {
                    Param(enumDBFields.nUsuario, nUsuario),
                    Param(enumDBFields.dDesde, dDesde),
                    Param(enumDBFields.dHasta, dHasta),
                };

                //cLog.Debug("Llamada al procedimiento almacenado SUP_CONSUMOIAPSEMANA_PSN_IAP30 {nUusario: " + nUsuario + "  , dDesde: " + dDesde + "   , dHasta:  " + dHasta + "}");

                dr = cDblib.DataReader("SUP_CONSUMOIAPSEMANA_PSN_IAP30", dbparams);

                //cLog.Debug("Comienza a leer el dr");

                while (dr.Read())
                {
                    oConsumoIAPSemana = new Models.ConsumoIAPSemana();
                    oConsumoIAPSemana.nivel = 1;
                    oConsumoIAPSemana.tipo = "PSN";
                    oConsumoIAPSemana.t305_idproyectosubnodo = Convert.ToInt32(dr["t305_idproyectosubnodo"]);
                    oConsumoIAPSemana.t301_idproyecto = Convert.ToInt32(dr["t301_idproyecto"]);                    
                    oConsumoIAPSemana.denominacion = Convert.ToString(dr["t305_seudonimo"]);
                    oConsumoIAPSemana.t303_denominacion = Convert.ToString(dr["t303_denominacion"]);
                    oConsumoIAPSemana.t302_denominacion = Convert.ToString(dr["t302_denominacion"]);
                    if (!Convert.IsDBNull(dr["responsable"]))
                        oConsumoIAPSemana.Responsable = Convert.ToString(dr["responsable"]);
                    if (!Convert.IsDBNull(dr["t301_estado"]))
                        oConsumoIAPSemana.t301_estado = Convert.ToString(dr["t301_estado"]);
                    if (!Convert.IsDBNull(dr["t303_idnodo"]))
                        oConsumoIAPSemana.t303_idnodo = Convert.ToInt32(dr["t303_idnodo"]);
                    if (!Convert.IsDBNull(dr["t305_imputablegasvi"]))
                        oConsumoIAPSemana.t305_imputablegasvi = Convert.ToBoolean(dr["t305_imputablegasvi"]);
                    if (!Convert.IsDBNull(dr["t323_idnaturaleza"]))
                        oConsumoIAPSemana.t323_idnaturaleza = Convert.ToInt32(dr["t323_idnaturaleza"]);
                    oConsumoIAPSemana.esf_Lunes = Convert.ToDouble(dr["esf_Lunes"]);                    
                    oConsumoIAPSemana.esf_Martes = Convert.ToDouble(dr["esf_Martes"]);                    
                    oConsumoIAPSemana.esf_Miercoles = Convert.ToDouble(dr["esf_Miercoles"]);                    
                    oConsumoIAPSemana.esf_Jueves = Convert.ToDouble(dr["esf_Jueves"]);                    
                    oConsumoIAPSemana.esf_Viernes = Convert.ToDouble(dr["esf_Viernes"]);                    
                    oConsumoIAPSemana.esf_Sabado = Convert.ToDouble(dr["esf_Sabado"]);                    
                    oConsumoIAPSemana.esf_Domingo = Convert.ToDouble(dr["esf_Domingo"]);                    
                    if (!Convert.IsDBNull(dr["TotalEstimado"]))
                        oConsumoIAPSemana.TotalEstimado = Convert.ToDouble(dr["TotalEstimado"]);
                    if (!Convert.IsDBNull(dr["FinEstimado"]))
                        oConsumoIAPSemana.FinEstimado = Convert.ToDateTime(dr["FinEstimado"]);
                    oConsumoIAPSemana.EsfuerzoTotalAcumulado = Convert.ToDouble(dr["EsfuerzoTotalAcumulado"]);
                    if (!Convert.IsDBNull(dr["FechaUltimaImputacion"]))
                        oConsumoIAPSemana.fultiimp = Convert.ToDateTime(dr["FechaUltimaImputacion"]);
                    if (!Convert.IsDBNull(dr["Pendiente"]))
                        oConsumoIAPSemana.Pendiente = Convert.ToDouble(dr["Pendiente"]);                    
                    oConsumoIAPSemana.AccesoBitacora = Convert.ToString(dr["AccesoBitacora"]);

                    lst.Add(oConsumoIAPSemana);

                }

                //cLog.Debug("Termina de leer el dr");

                return lst;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dr != null)
                {
                    if (!dr.IsClosed) dr.Close();
                    dr.Dispose();
                }
            }
        }

        /// <summary>
        /// Obtiene la relación de proyectos económicos del mes de fecha desde, así como los consumos
        /// y el desglose técnico de proyectosubnodos recibidos como parámetro a nivel:
        ///	Proyectos técnicos, Fases, Actividades, Tareas asignadas al usuario
        /// </summary>
        internal List<Models.ConsumoIAPSemana> ObtenerConsumosIAPSemanaCompleto(int nUsuario, DataTable dtPSNs, DateTime dDesde, DateTime dHasta)
        {
            Models.ConsumoIAPSemana oConsumoIAPSemana = null;
            List<Models.ConsumoIAPSemana> lst = new List<Models.ConsumoIAPSemana>();
            IDataReader dr = null;
            //log4net.ILog cLog = SUPERANTIGUO.BLL.Log.logger;

            try
            {

                SqlParameter[] dbparams = new SqlParameter[4] {
                    Param(enumDBFields.nUsuario, nUsuario),
                    Param(enumDBFields.datatablePSN, dtPSNs),
                    Param(enumDBFields.dDesde, dDesde),
                    Param(enumDBFields.dHasta, dHasta),
                };

                //cLog.Debug("Llamada al procedimiento almacenado SUP_CONSUMOIAPSEMANA_COMPLETO_D_IAP30 {nUusario: " + nUsuario + "  , dDesde: " + dDesde + "   , dHasta:  " + dHasta + "}");

                dr = cDblib.DataReader("SUP_CONSUMOIAPSEMANA_COMPLETO_D_IAP30", dbparams);

                //cLog.Debug("Comienza a leer el dr");

                while (dr.Read())
                {
                    oConsumoIAPSemana = new Models.ConsumoIAPSemana();
                    if (!Convert.IsDBNull(dr["nivel"]))
                        oConsumoIAPSemana.nivel = Convert.ToInt32(dr["nivel"]);
                    oConsumoIAPSemana.tipo = Convert.ToString(dr["tipo"]);
                    oConsumoIAPSemana.t305_idproyectosubnodo = Convert.ToInt32(dr["t305_idproyectosubnodo"]);
                    oConsumoIAPSemana.t301_idproyecto = Convert.ToInt32(dr["t301_idproyecto"]);
                    if (!Convert.IsDBNull(dr["t331_idpt"]))
                        oConsumoIAPSemana.t331_idpt = Convert.ToInt32(dr["t331_idpt"]);
                    if (!Convert.IsDBNull(dr["t334_idfase"]))
                        oConsumoIAPSemana.t334_idfase = Convert.ToInt32(dr["t334_idfase"]);
                    if (!Convert.IsDBNull(dr["t335_idactividad"]))
                        oConsumoIAPSemana.t335_idactividad = Convert.ToInt32(dr["t335_idactividad"]);
                    if (!Convert.IsDBNull(dr["t332_idtarea"]))
                        oConsumoIAPSemana.t332_idtarea = Convert.ToInt32(dr["t332_idtarea"]);
                    if (!Convert.IsDBNull(dr["t332_estado"]))
                        oConsumoIAPSemana.t332_estado = Convert.ToInt32(dr["t332_estado"]);
                    oConsumoIAPSemana.denominacion = Convert.ToString(dr["denominacion"]);
                    oConsumoIAPSemana.t303_denominacion = Convert.ToString(dr["t303_denominacion"]);
                    oConsumoIAPSemana.t302_denominacion = Convert.ToString(dr["t302_denominacion"]);
                    if (!Convert.IsDBNull(dr["responsable"]))
                        oConsumoIAPSemana.Responsable = Convert.ToString(dr["responsable"]);
                    if (!Convert.IsDBNull(dr["t301_estado"]))
                        oConsumoIAPSemana.t301_estado = Convert.ToString(dr["t301_estado"]);
                    if (!Convert.IsDBNull(dr["t303_idnodo"]))
                        oConsumoIAPSemana.t303_idnodo = Convert.ToInt32(dr["t303_idnodo"]);
                    if (!Convert.IsDBNull(dr["t305_imputablegasvi"]))
                        oConsumoIAPSemana.t305_imputablegasvi = Convert.ToBoolean(dr["t305_imputablegasvi"]);
                    if (!Convert.IsDBNull(dr["t323_idnaturaleza"]))
                        oConsumoIAPSemana.t323_idnaturaleza = Convert.ToInt32(dr["t323_idnaturaleza"]);
                    oConsumoIAPSemana.esf_Lunes = Convert.ToDouble(dr["esf_Lunes"]);
                    oConsumoIAPSemana.esfJorn_Lunes = Convert.ToDouble(dr["esfJorn_Lunes"]);
                    if (!Convert.IsDBNull(dr["com_Lunes"]))
                        oConsumoIAPSemana.com_Lunes = Convert.ToString(dr["com_Lunes"]);
                    if (!Convert.IsDBNull(dr["lab_Lunes"]))
                        oConsumoIAPSemana.lab_Lunes = Convert.ToInt32(dr["lab_Lunes"]);
                    if (!Convert.IsDBNull(dr["out_Lunes"]))
                        oConsumoIAPSemana.out_Lunes = Convert.ToInt32(dr["out_Lunes"]);
                    if (!Convert.IsDBNull(dr["vig_Lunes"]))
                        oConsumoIAPSemana.vig_Lunes = Convert.ToInt32(dr["vig_Lunes"]);
                    if (!Convert.IsDBNull(dr["vac_Lunes"]))
                        oConsumoIAPSemana.vac_Lunes = Convert.ToInt32(dr["vac_Lunes"]);
                    oConsumoIAPSemana.esf_Martes = Convert.ToDouble(dr["esf_Martes"]);
                    oConsumoIAPSemana.esfJorn_Martes = Convert.ToDouble(dr["esfJorn_Martes"]);
                    if (!Convert.IsDBNull(dr["com_Martes"]))
                        oConsumoIAPSemana.com_Martes = Convert.ToString(dr["com_Martes"]);
                    if (!Convert.IsDBNull(dr["lab_Martes"]))
                        oConsumoIAPSemana.lab_Martes = Convert.ToInt32(dr["lab_Martes"]);
                    if (!Convert.IsDBNull(dr["out_Martes"]))
                        oConsumoIAPSemana.out_Martes = Convert.ToInt32(dr["out_Martes"]);
                    if (!Convert.IsDBNull(dr["vig_Martes"]))
                        oConsumoIAPSemana.vig_Martes = Convert.ToInt32(dr["vig_Martes"]);
                    if (!Convert.IsDBNull(dr["vac_Martes"]))
                        oConsumoIAPSemana.vac_Martes = Convert.ToInt32(dr["vac_Martes"]);
                    oConsumoIAPSemana.esf_Miercoles = Convert.ToDouble(dr["esf_Miercoles"]);
                    oConsumoIAPSemana.esfJorn_Miercoles = Convert.ToDouble(dr["esfJorn_Miercoles"]);
                    if (!Convert.IsDBNull(dr["com_Miercoles"]))
                        oConsumoIAPSemana.com_Miercoles = Convert.ToString(dr["com_Miercoles"]);
                    if (!Convert.IsDBNull(dr["lab_Miercoles"]))
                        oConsumoIAPSemana.lab_Miercoles = Convert.ToInt32(dr["lab_Miercoles"]);
                    if (!Convert.IsDBNull(dr["out_Miercoles"]))
                        oConsumoIAPSemana.out_Miercoles = Convert.ToInt32(dr["out_Miercoles"]);
                    if (!Convert.IsDBNull(dr["vig_Miercoles"]))
                        oConsumoIAPSemana.vig_Miercoles = Convert.ToInt32(dr["vig_Miercoles"]);
                    if (!Convert.IsDBNull(dr["vac_Miercoles"]))
                        oConsumoIAPSemana.vac_Miercoles = Convert.ToInt32(dr["vac_Miercoles"]);
                    oConsumoIAPSemana.esf_Jueves = Convert.ToDouble(dr["esf_Jueves"]);
                    oConsumoIAPSemana.esfJorn_Jueves = Convert.ToDouble(dr["esfJorn_Jueves"]);
                    if (!Convert.IsDBNull(dr["com_Jueves"]))
                        oConsumoIAPSemana.com_Jueves = Convert.ToString(dr["com_Jueves"]);
                    if (!Convert.IsDBNull(dr["lab_Jueves"]))
                        oConsumoIAPSemana.lab_Jueves = Convert.ToInt32(dr["lab_Jueves"]);
                    if (!Convert.IsDBNull(dr["out_Jueves"]))
                        oConsumoIAPSemana.out_Jueves = Convert.ToInt32(dr["out_Jueves"]);
                    if (!Convert.IsDBNull(dr["vig_Jueves"]))
                        oConsumoIAPSemana.vig_Jueves = Convert.ToInt32(dr["vig_Jueves"]);
                    if (!Convert.IsDBNull(dr["vac_Jueves"]))
                        oConsumoIAPSemana.vac_Jueves = Convert.ToInt32(dr["vac_Jueves"]);
                    oConsumoIAPSemana.esf_Viernes = Convert.ToDouble(dr["esf_Viernes"]);
                    oConsumoIAPSemana.esfJorn_Viernes = Convert.ToDouble(dr["esfJorn_Viernes"]);
                    if (!Convert.IsDBNull(dr["com_Viernes"]))
                        oConsumoIAPSemana.com_Viernes = Convert.ToString(dr["com_Viernes"]);
                    if (!Convert.IsDBNull(dr["lab_Viernes"]))
                        oConsumoIAPSemana.lab_Viernes = Convert.ToInt32(dr["lab_Viernes"]);
                    if (!Convert.IsDBNull(dr["out_Viernes"]))
                        oConsumoIAPSemana.out_Viernes = Convert.ToInt32(dr["out_Viernes"]);
                    if (!Convert.IsDBNull(dr["vig_Viernes"]))
                        oConsumoIAPSemana.vig_Viernes = Convert.ToInt32(dr["vig_Viernes"]);
                    if (!Convert.IsDBNull(dr["vac_Viernes"]))
                        oConsumoIAPSemana.vac_Viernes = Convert.ToInt32(dr["vac_Viernes"]);
                    oConsumoIAPSemana.esf_Sabado = Convert.ToDouble(dr["esf_Sabado"]);
                    oConsumoIAPSemana.esfJorn_Sabado = Convert.ToDouble(dr["esfJorn_Sabado"]);
                    if (!Convert.IsDBNull(dr["com_Sabado"]))
                        oConsumoIAPSemana.com_Sabado = Convert.ToString(dr["com_Sabado"]);
                    if (!Convert.IsDBNull(dr["lab_Sabado"]))
                        oConsumoIAPSemana.lab_Sabado = Convert.ToInt32(dr["lab_Sabado"]);
                    if (!Convert.IsDBNull(dr["out_Sabado"]))
                        oConsumoIAPSemana.out_Sabado = Convert.ToInt32(dr["out_Sabado"]);
                    if (!Convert.IsDBNull(dr["vig_Sabado"]))
                        oConsumoIAPSemana.vig_Sabado = Convert.ToInt32(dr["vig_Sabado"]);
                    if (!Convert.IsDBNull(dr["vac_Sabado"]))
                        oConsumoIAPSemana.vac_Sabado = Convert.ToInt32(dr["vac_Sabado"]);
                    oConsumoIAPSemana.esf_Domingo = Convert.ToDouble(dr["esf_Domingo"]);
                    oConsumoIAPSemana.esfJorn_Domingo = Convert.ToDouble(dr["esfJorn_Domingo"]);
                    if (!Convert.IsDBNull(dr["com_Domingo"]))
                        oConsumoIAPSemana.com_Domingo = Convert.ToString(dr["com_Domingo"]);
                    if (!Convert.IsDBNull(dr["lab_Domingo"]))
                        oConsumoIAPSemana.lab_Domingo = Convert.ToInt32(dr["lab_Domingo"]);
                    if (!Convert.IsDBNull(dr["out_Domingo"]))
                        oConsumoIAPSemana.out_Domingo = Convert.ToInt32(dr["out_Domingo"]);
                    if (!Convert.IsDBNull(dr["vig_Domingo"]))
                        oConsumoIAPSemana.vig_Domingo = Convert.ToInt32(dr["vig_Domingo"]);
                    if (!Convert.IsDBNull(dr["vac_Domingo"]))
                        oConsumoIAPSemana.vac_Domingo = Convert.ToInt32(dr["vac_Domingo"]);
                    if (!Convert.IsDBNull(dr["TotalEstimado"]))
                        oConsumoIAPSemana.TotalEstimado = Convert.ToDouble(dr["TotalEstimado"]);
                    if (!Convert.IsDBNull(dr["FinEstimado"]))
                        oConsumoIAPSemana.FinEstimado = Convert.ToDateTime(dr["FinEstimado"]);
                    oConsumoIAPSemana.EsfuerzoTotalAcumulado = Convert.ToDouble(dr["EsfuerzoTotalAcumulado"]);
                    if (!Convert.IsDBNull(dr["FechaUltimaImputacion"]))
                        oConsumoIAPSemana.fultiimp = Convert.ToDateTime(dr["FechaUltimaImputacion"]);
                    if (!Convert.IsDBNull(dr["Pendiente"]))
                        oConsumoIAPSemana.Pendiente = Convert.ToDouble(dr["Pendiente"]);
                    if (!Convert.IsDBNull(dr["t330_falta"]))
                        oConsumoIAPSemana.t330_falta = Convert.ToDateTime(dr["t330_falta"]);
                    if (!Convert.IsDBNull(dr["t330_fbaja"]))
                        oConsumoIAPSemana.t330_fbaja = Convert.ToDateTime(dr["t330_fbaja"]);                    
                    if (!Convert.IsDBNull(dr["t323_regjornocompleta"]))
                        oConsumoIAPSemana.t323_regjornocompleta = Convert.ToInt32(dr["t323_regjornocompleta"]);
                    if (!Convert.IsDBNull(dr["t323_regfes"]))
                        oConsumoIAPSemana.t323_regfes = Convert.ToInt32(dr["t323_regfes"]);
                    if (!Convert.IsDBNull(dr["t331_obligaest"]))
                        oConsumoIAPSemana.t331_obligaest = Convert.ToInt32(dr["t331_obligaest"]);
                    if (!Convert.IsDBNull(dr["HayIndicaciones"]))
                        oConsumoIAPSemana.HayIndicaciones = Convert.ToInt32(dr["HayIndicaciones"]);
                    if (!Convert.IsDBNull(dr["t336_completado"]))
                        oConsumoIAPSemana.t336_completado = Convert.ToInt32(dr["t336_completado"]);
                    if (!Convert.IsDBNull(dr["t332_impiap"]))
                        oConsumoIAPSemana.t332_impiap = Convert.ToInt32(dr["t332_impiap"]);
                    if (!Convert.IsDBNull(dr["t332_fiv"]))
                        oConsumoIAPSemana.t332_fiv = Convert.ToDateTime(dr["t332_fiv"]);
                    if (!Convert.IsDBNull(dr["t332_ffv"]))
                        oConsumoIAPSemana.t332_ffv = Convert.ToDateTime(dr["t332_ffv"]);
                    if (!Convert.IsDBNull(dr["t346_codpst"]))
                        oConsumoIAPSemana.t346_codpst = Convert.ToString(dr["t346_codpst"]);
                    if (!Convert.IsDBNull(dr["t346_despst"]))
                        oConsumoIAPSemana.t346_despst = Convert.ToString(dr["t346_despst"]);
                    if (!Convert.IsDBNull(dr["t332_otl"]))
                        oConsumoIAPSemana.t332_otl = Convert.ToString(dr["t332_otl"]);
                    if (!Convert.IsDBNull(dr["orden"]))
                        oConsumoIAPSemana.orden = Convert.ToString(dr["orden"]);
                    oConsumoIAPSemana.AccesoBitacora = Convert.ToString(dr["AccesoBitacora"]);

                    lst.Add(oConsumoIAPSemana);

                }

                //cLog.Debug("Termina de leer el dr");

                return lst;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dr != null)
                {
                    if (!dr.IsClosed) dr.Close();
                    dr.Dispose();
                }
            }
        }       

        /// <summary>
        /// Obtiene el desglose de primer nivel o de todos los niveles de n proyectosubnodo
        /// </summary>
        internal List<Models.ConsumoIAPSemana> ObtenerConsumosIAPSemanaPSN_D(int nUsuario, DataTable dtPSNs, DateTime dDesde, DateTime dHasta, int soloPrimerNivel)
        {
            Models.ConsumoIAPSemana oConsumoIAPSemana = null;
            List<Models.ConsumoIAPSemana> lst = new List<Models.ConsumoIAPSemana>();
            IDataReader dr = null;
            //log4net.ILog cLog = SUPERANTIGUO.BLL.Log.logger;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[5] {
                    Param(enumDBFields.nUsuario, nUsuario),
                    Param(enumDBFields.datatablePSN, dtPSNs),
                    Param(enumDBFields.dDesde, dDesde),
                    Param(enumDBFields.dHasta, dHasta),
                    Param(enumDBFields.soloPrimerNivel, soloPrimerNivel),                    
                };

                //cLog.Debug("Llamada al procedimiento almacenado SUP_CONSUMOIAPSEMANA_PSN_D_IAP30");

                dr = cDblib.DataReader("SUP_CONSUMOIAPSEMANA_PSN_D_IAP30", dbparams);
                
                //cLog.Debug("Comienza a leer el dr");

                while (dr.Read())
                {
                    oConsumoIAPSemana = new Models.ConsumoIAPSemana();
                    if (!Convert.IsDBNull(dr["nivel"]))
                        oConsumoIAPSemana.nivel = Convert.ToInt32(dr["nivel"]);
                    oConsumoIAPSemana.tipo = Convert.ToString(dr["tipo"]);
                    oConsumoIAPSemana.t305_idproyectosubnodo = Convert.ToInt32(dr["t305_idproyectosubnodo"]);
                    oConsumoIAPSemana.t331_idpt = Convert.ToInt32(dr["t331_idpt"]);
                    if (!Convert.IsDBNull(dr["t334_idfase"]))
                        oConsumoIAPSemana.t334_idfase = Convert.ToInt32(dr["t334_idfase"]);
                    if (!Convert.IsDBNull(dr["t335_idactividad"]))
                        oConsumoIAPSemana.t335_idactividad = Convert.ToInt32(dr["t335_idactividad"]);
                    if (!Convert.IsDBNull(dr["t332_idtarea"]))
                        oConsumoIAPSemana.t332_idtarea = Convert.ToInt32(dr["t332_idtarea"]);
                    if (!Convert.IsDBNull(dr["t332_estado"]))
                        oConsumoIAPSemana.t332_estado = Convert.ToInt32(dr["t332_estado"]);
                    oConsumoIAPSemana.denominacion = Convert.ToString(dr["denominacion"]);
                    oConsumoIAPSemana.esf_Lunes = Convert.ToDouble(dr["esf_Lunes"]);
                    oConsumoIAPSemana.com_Lunes = Convert.ToString(dr["com_Lunes"]);
                    if (!Convert.IsDBNull(dr["lab_Lunes"]))
                        oConsumoIAPSemana.lab_Lunes = Convert.ToInt32(dr["lab_Lunes"]);
                    if (!Convert.IsDBNull(dr["out_Lunes"]))
                        oConsumoIAPSemana.out_Lunes = Convert.ToInt32(dr["out_Lunes"]);
                    if (!Convert.IsDBNull(dr["vig_Lunes"]))
                        oConsumoIAPSemana.vig_Lunes = Convert.ToInt32(dr["vig_Lunes"]);
                    if (!Convert.IsDBNull(dr["vac_Lunes"]))
                        oConsumoIAPSemana.vac_Lunes = Convert.ToInt32(dr["vac_Lunes"]);
                    oConsumoIAPSemana.esf_Martes = Convert.ToDouble(dr["esf_Martes"]);
                    oConsumoIAPSemana.com_Martes = Convert.ToString(dr["com_Martes"]);
                    if (!Convert.IsDBNull(dr["lab_Martes"]))
                        oConsumoIAPSemana.lab_Martes = Convert.ToInt32(dr["lab_Martes"]);
                    if (!Convert.IsDBNull(dr["out_Martes"]))
                        oConsumoIAPSemana.out_Martes = Convert.ToInt32(dr["out_Martes"]);
                    if (!Convert.IsDBNull(dr["vig_Martes"]))
                        oConsumoIAPSemana.vig_Martes = Convert.ToInt32(dr["vig_Martes"]);
                    if (!Convert.IsDBNull(dr["vac_Martes"]))
                        oConsumoIAPSemana.vac_Martes = Convert.ToInt32(dr["vac_Martes"]);
                    oConsumoIAPSemana.esf_Miercoles = Convert.ToDouble(dr["esf_Miercoles"]);
                    oConsumoIAPSemana.com_Miercoles = Convert.ToString(dr["com_Miercoles"]);
                    if (!Convert.IsDBNull(dr["lab_Miercoles"]))
                        oConsumoIAPSemana.lab_Miercoles = Convert.ToInt32(dr["lab_Miercoles"]);
                    if (!Convert.IsDBNull(dr["out_Miercoles"]))
                        oConsumoIAPSemana.out_Miercoles = Convert.ToInt32(dr["out_Miercoles"]);
                    if (!Convert.IsDBNull(dr["vig_Miercoles"]))
                        oConsumoIAPSemana.vig_Miercoles = Convert.ToInt32(dr["vig_Miercoles"]);
                    if (!Convert.IsDBNull(dr["vac_Miercoles"]))
                        oConsumoIAPSemana.vac_Miercoles = Convert.ToInt32(dr["vac_Miercoles"]);
                    oConsumoIAPSemana.esf_Jueves = Convert.ToDouble(dr["esf_Jueves"]);
                    oConsumoIAPSemana.com_Jueves = Convert.ToString(dr["com_Jueves"]);
                    if (!Convert.IsDBNull(dr["lab_Jueves"]))
                        oConsumoIAPSemana.lab_Jueves = Convert.ToInt32(dr["lab_Jueves"]);
                    if (!Convert.IsDBNull(dr["out_Jueves"]))
                        oConsumoIAPSemana.out_Jueves = Convert.ToInt32(dr["out_Jueves"]);
                    if (!Convert.IsDBNull(dr["vig_Jueves"]))
                        oConsumoIAPSemana.vig_Jueves = Convert.ToInt32(dr["vig_Jueves"]);
                    if (!Convert.IsDBNull(dr["vac_Jueves"]))
                        oConsumoIAPSemana.vac_Jueves = Convert.ToInt32(dr["vac_Jueves"]);
                    oConsumoIAPSemana.esf_Viernes = Convert.ToDouble(dr["esf_Viernes"]);
                    oConsumoIAPSemana.com_Viernes = Convert.ToString(dr["com_Viernes"]);
                    if (!Convert.IsDBNull(dr["lab_Viernes"]))
                        oConsumoIAPSemana.lab_Viernes = Convert.ToInt32(dr["lab_Viernes"]);
                    if (!Convert.IsDBNull(dr["out_Viernes"]))
                        oConsumoIAPSemana.out_Viernes = Convert.ToInt32(dr["out_Viernes"]);
                    if (!Convert.IsDBNull(dr["vig_Viernes"]))
                        oConsumoIAPSemana.vig_Viernes = Convert.ToInt32(dr["vig_Viernes"]);
                    if (!Convert.IsDBNull(dr["vac_Viernes"]))
                        oConsumoIAPSemana.vac_Viernes = Convert.ToInt32(dr["vac_Viernes"]);
                    oConsumoIAPSemana.esf_Sabado = Convert.ToDouble(dr["esf_Sabado"]);
                    oConsumoIAPSemana.com_Sabado = Convert.ToString(dr["com_Sabado"]);
                    if (!Convert.IsDBNull(dr["lab_Sabado"]))
                        oConsumoIAPSemana.lab_Sabado = Convert.ToInt32(dr["lab_Sabado"]);
                    if (!Convert.IsDBNull(dr["out_Sabado"]))
                        oConsumoIAPSemana.out_Sabado = Convert.ToInt32(dr["out_Sabado"]);
                    if (!Convert.IsDBNull(dr["vig_Sabado"]))
                        oConsumoIAPSemana.vig_Sabado = Convert.ToInt32(dr["vig_Sabado"]);
                    if (!Convert.IsDBNull(dr["vac_Sabado"]))
                        oConsumoIAPSemana.vac_Sabado = Convert.ToInt32(dr["vac_Sabado"]);
                    oConsumoIAPSemana.esf_Domingo = Convert.ToDouble(dr["esf_Domingo"]);
                    oConsumoIAPSemana.com_Domingo = Convert.ToString(dr["com_Domingo"]);
                    if (!Convert.IsDBNull(dr["lab_Domingo"]))
                        oConsumoIAPSemana.lab_Domingo = Convert.ToInt32(dr["lab_Domingo"]);
                    if (!Convert.IsDBNull(dr["out_Domingo"]))
                        oConsumoIAPSemana.out_Domingo = Convert.ToInt32(dr["out_Domingo"]);
                    if (!Convert.IsDBNull(dr["vig_Domingo"]))
                        oConsumoIAPSemana.vig_Domingo = Convert.ToInt32(dr["vig_Domingo"]);
                    if (!Convert.IsDBNull(dr["vac_Domingo"]))
                        oConsumoIAPSemana.vac_Domingo = Convert.ToInt32(dr["vac_Domingo"]);
                    if (!Convert.IsDBNull(dr["TotalEstimado"]))
                        oConsumoIAPSemana.TotalEstimado = Convert.ToDouble(dr["TotalEstimado"]);
                    if (!Convert.IsDBNull(dr["FinEstimado"]))
                        oConsumoIAPSemana.FinEstimado = Convert.ToDateTime(dr["FinEstimado"]);
                    oConsumoIAPSemana.EsfuerzoTotalAcumulado = Convert.ToDouble(dr["EsfuerzoTotalAcumulado"]);
                    if (!Convert.IsDBNull(dr["FechaUltimaImputacion"]))
                        oConsumoIAPSemana.fultiimp = Convert.ToDateTime(dr["FechaUltimaImputacion"]);
                    if (!Convert.IsDBNull(dr["Pendiente"]))
                        oConsumoIAPSemana.Pendiente = Convert.ToDouble(dr["Pendiente"]);
                    if (!Convert.IsDBNull(dr["t330_falta"]))
                        oConsumoIAPSemana.t330_falta = Convert.ToDateTime(dr["t330_falta"]);
                    if (!Convert.IsDBNull(dr["t330_fbaja"]))
                        oConsumoIAPSemana.t330_fbaja = Convert.ToDateTime(dr["t330_fbaja"]);
                    if (!Convert.IsDBNull(dr["t323_regjornocompleta"]))
                        oConsumoIAPSemana.t323_regjornocompleta = Convert.ToInt32(dr["t323_regjornocompleta"]);
                    if (!Convert.IsDBNull(dr["t323_regfes"]))
                        oConsumoIAPSemana.t323_regfes = Convert.ToInt32(dr["t323_regfes"]);
                    if (!Convert.IsDBNull(dr["t331_obligaest"]))
                        oConsumoIAPSemana.t331_obligaest = Convert.ToInt32(dr["t331_obligaest"]);
                    if (!Convert.IsDBNull(dr["HayIndicaciones"]))
                        oConsumoIAPSemana.HayIndicaciones = Convert.ToInt32(dr["HayIndicaciones"]);
                    if (!Convert.IsDBNull(dr["t336_completado"]))
                        oConsumoIAPSemana.t336_completado = Convert.ToInt32(dr["t336_completado"]);
                    if (!Convert.IsDBNull(dr["t332_impiap"]))
                        oConsumoIAPSemana.t332_impiap = Convert.ToInt32(dr["t332_impiap"]);
                    if (!Convert.IsDBNull(dr["t332_fiv"]))
                        oConsumoIAPSemana.t332_fiv = Convert.ToDateTime(dr["t332_fiv"]);
                    if (!Convert.IsDBNull(dr["t332_ffv"]))
                        oConsumoIAPSemana.t332_ffv = Convert.ToDateTime(dr["t332_ffv"]);
                    if (!Convert.IsDBNull(dr["t346_codpst"]))
                        oConsumoIAPSemana.t346_codpst = Convert.ToString(dr["t346_codpst"]);
                    if (!Convert.IsDBNull(dr["t346_despst"]))
                        oConsumoIAPSemana.t346_despst = Convert.ToString(dr["t346_despst"]);
                    if (!Convert.IsDBNull(dr["t332_otl"]))
                        oConsumoIAPSemana.t332_otl = Convert.ToString(dr["t332_otl"]);
                    if (!Convert.IsDBNull(dr["orden"]))
                        oConsumoIAPSemana.orden = Convert.ToString(dr["orden"]);
                    oConsumoIAPSemana.AccesoBitacora = Convert.ToString(dr["AccesoBitacora"]);                                    

                    lst.Add(oConsumoIAPSemana);                    

                }
                //cLog.Debug("Termina de leer el dr");
                
                return lst;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dr != null)
                {
                    if (!dr.IsClosed) dr.Close();
                    dr.Dispose();
                }
            }
        }

        /// <summary>
        /// Obtiene el desglose de primer nivel o de todos los niveles de un Proyecto Técnico
        /// </summary>
        internal List<Models.ConsumoIAPSemana> ObtenerConsumosIAPSemanaPT_D(int nUsuario, int nPT, DateTime dDesde, DateTime dHasta, int soloPrimerNivel)
        {
            Models.ConsumoIAPSemana oConsumoIAPSemana = null;
            List<Models.ConsumoIAPSemana> lst = new List<Models.ConsumoIAPSemana>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[5] {
                    Param(enumDBFields.nUsuario, nUsuario),
                    Param(enumDBFields.nPT, nPT),
                    Param(enumDBFields.dDesde, dDesde),
                    Param(enumDBFields.dHasta, dHasta),
                    Param(enumDBFields.soloPrimerNivel, soloPrimerNivel),                    
                };

                dr = cDblib.DataReader("SUP_CONSUMOIAPSEMANA_PT_D_IAP30", dbparams);
                while (dr.Read())
                {
                    oConsumoIAPSemana = new Models.ConsumoIAPSemana();
                    if (!Convert.IsDBNull(dr["nivel"]))
                        oConsumoIAPSemana.nivel = Convert.ToInt32(dr["nivel"]);
                    oConsumoIAPSemana.tipo = Convert.ToString(dr["tipo"]);
                    oConsumoIAPSemana.t305_idproyectosubnodo = Convert.ToInt32(dr["t305_idproyectosubnodo"]);
                    oConsumoIAPSemana.t331_idpt = Convert.ToInt32(dr["t331_idpt"]);
                    if (!Convert.IsDBNull(dr["t334_idfase"]))
                        oConsumoIAPSemana.t334_idfase = Convert.ToInt32(dr["t334_idfase"]);
                    if (!Convert.IsDBNull(dr["t335_idactividad"]))
                        oConsumoIAPSemana.t335_idactividad = Convert.ToInt32(dr["t335_idactividad"]);
                    if (!Convert.IsDBNull(dr["t332_idtarea"]))
                        oConsumoIAPSemana.t332_idtarea = Convert.ToInt32(dr["t332_idtarea"]);
                    if (!Convert.IsDBNull(dr["t332_estado"]))
                        oConsumoIAPSemana.t332_estado = Convert.ToInt32(dr["t332_estado"]);
                    oConsumoIAPSemana.denominacion = Convert.ToString(dr["denominacion"]);
                    oConsumoIAPSemana.esf_Lunes = Convert.ToDouble(dr["esf_Lunes"]);
                    oConsumoIAPSemana.esfJorn_Lunes = Convert.ToDouble(dr["esfJorn_Lunes"]);

                    oConsumoIAPSemana.com_Lunes = Convert.ToString(dr["com_Lunes"]);
                    if (!Convert.IsDBNull(dr["lab_Lunes"]))
                        oConsumoIAPSemana.lab_Lunes = Convert.ToInt32(dr["lab_Lunes"]);
                    if (!Convert.IsDBNull(dr["out_Lunes"]))
                        oConsumoIAPSemana.out_Lunes = Convert.ToInt32(dr["out_Lunes"]);
                    if (!Convert.IsDBNull(dr["vig_Lunes"]))
                        oConsumoIAPSemana.vig_Lunes = Convert.ToInt32(dr["vig_Lunes"]);
                    if (!Convert.IsDBNull(dr["vac_Lunes"]))
                        oConsumoIAPSemana.vac_Lunes = Convert.ToInt32(dr["vac_Lunes"]);
                    oConsumoIAPSemana.esf_Martes = Convert.ToDouble(dr["esf_Martes"]);
                    oConsumoIAPSemana.esfJorn_Martes = Convert.ToDouble(dr["esfJorn_Martes"]);
                    oConsumoIAPSemana.com_Martes = Convert.ToString(dr["com_Martes"]);
                    if (!Convert.IsDBNull(dr["lab_Martes"]))
                        oConsumoIAPSemana.lab_Martes = Convert.ToInt32(dr["lab_Martes"]);
                    if (!Convert.IsDBNull(dr["out_Martes"]))
                        oConsumoIAPSemana.out_Martes = Convert.ToInt32(dr["out_Martes"]);
                    if (!Convert.IsDBNull(dr["vig_Martes"]))
                        oConsumoIAPSemana.vig_Martes = Convert.ToInt32(dr["vig_Martes"]);
                    if (!Convert.IsDBNull(dr["vac_Martes"]))
                        oConsumoIAPSemana.vac_Martes = Convert.ToInt32(dr["vac_Martes"]);
                    oConsumoIAPSemana.esf_Miercoles = Convert.ToDouble(dr["esf_Miercoles"]);
                    oConsumoIAPSemana.esfJorn_Miercoles = Convert.ToDouble(dr["esfJorn_Miercoles"]);
                    oConsumoIAPSemana.com_Miercoles = Convert.ToString(dr["com_Miercoles"]);
                    if (!Convert.IsDBNull(dr["lab_Miercoles"]))
                        oConsumoIAPSemana.lab_Miercoles = Convert.ToInt32(dr["lab_Miercoles"]);
                    if (!Convert.IsDBNull(dr["out_Miercoles"]))
                        oConsumoIAPSemana.out_Miercoles = Convert.ToInt32(dr["out_Miercoles"]);
                    if (!Convert.IsDBNull(dr["vig_Miercoles"]))
                        oConsumoIAPSemana.vig_Miercoles = Convert.ToInt32(dr["vig_Miercoles"]);
                    if (!Convert.IsDBNull(dr["vac_Miercoles"]))
                        oConsumoIAPSemana.vac_Miercoles = Convert.ToInt32(dr["vac_Miercoles"]);
                    oConsumoIAPSemana.esf_Jueves = Convert.ToDouble(dr["esf_Jueves"]);
                    oConsumoIAPSemana.esfJorn_Jueves = Convert.ToDouble(dr["esfJorn_Jueves"]);
                    oConsumoIAPSemana.com_Jueves = Convert.ToString(dr["com_Jueves"]);
                    if (!Convert.IsDBNull(dr["lab_Jueves"]))
                        oConsumoIAPSemana.lab_Jueves = Convert.ToInt32(dr["lab_Jueves"]);
                    if (!Convert.IsDBNull(dr["out_Jueves"]))
                        oConsumoIAPSemana.out_Jueves = Convert.ToInt32(dr["out_Jueves"]);
                    if (!Convert.IsDBNull(dr["vig_Jueves"]))
                        oConsumoIAPSemana.vig_Jueves = Convert.ToInt32(dr["vig_Jueves"]);
                    if (!Convert.IsDBNull(dr["vac_Jueves"]))
                        oConsumoIAPSemana.vac_Jueves = Convert.ToInt32(dr["vac_Jueves"]);
                    oConsumoIAPSemana.esf_Viernes = Convert.ToDouble(dr["esf_Viernes"]);
                    oConsumoIAPSemana.esfJorn_Viernes = Convert.ToDouble(dr["esfJorn_Viernes"]);
                    oConsumoIAPSemana.com_Viernes = Convert.ToString(dr["com_Viernes"]);
                    if (!Convert.IsDBNull(dr["lab_Viernes"]))
                        oConsumoIAPSemana.lab_Viernes = Convert.ToInt32(dr["lab_Viernes"]);
                    if (!Convert.IsDBNull(dr["out_Viernes"]))
                        oConsumoIAPSemana.out_Viernes = Convert.ToInt32(dr["out_Viernes"]);
                    if (!Convert.IsDBNull(dr["vig_Viernes"]))
                        oConsumoIAPSemana.vig_Viernes = Convert.ToInt32(dr["vig_Viernes"]);
                    if (!Convert.IsDBNull(dr["vac_Viernes"]))
                        oConsumoIAPSemana.vac_Viernes = Convert.ToInt32(dr["vac_Viernes"]);
                    oConsumoIAPSemana.esf_Sabado = Convert.ToDouble(dr["esf_Sabado"]);
                    oConsumoIAPSemana.esfJorn_Sabado = Convert.ToDouble(dr["esfJorn_Sabado"]);
                    oConsumoIAPSemana.com_Sabado = Convert.ToString(dr["com_Sabado"]);
                    if (!Convert.IsDBNull(dr["lab_Sabado"]))
                        oConsumoIAPSemana.lab_Sabado = Convert.ToInt32(dr["lab_Sabado"]);
                    if (!Convert.IsDBNull(dr["out_Sabado"]))
                        oConsumoIAPSemana.out_Sabado = Convert.ToInt32(dr["out_Sabado"]);
                    if (!Convert.IsDBNull(dr["vig_Sabado"]))
                        oConsumoIAPSemana.vig_Sabado = Convert.ToInt32(dr["vig_Sabado"]);
                    if (!Convert.IsDBNull(dr["vac_Sabado"]))
                        oConsumoIAPSemana.vac_Sabado = Convert.ToInt32(dr["vac_Sabado"]);
                    oConsumoIAPSemana.esf_Domingo = Convert.ToDouble(dr["esf_Domingo"]);
                    oConsumoIAPSemana.esfJorn_Domingo = Convert.ToDouble(dr["esfJorn_Domingo"]);
                    oConsumoIAPSemana.com_Domingo = Convert.ToString(dr["com_Domingo"]);
                    if (!Convert.IsDBNull(dr["lab_Domingo"]))
                        oConsumoIAPSemana.lab_Domingo = Convert.ToInt32(dr["lab_Domingo"]);
                    if (!Convert.IsDBNull(dr["out_Domingo"]))
                        oConsumoIAPSemana.out_Domingo = Convert.ToInt32(dr["out_Domingo"]);
                    if (!Convert.IsDBNull(dr["vig_Domingo"]))
                        oConsumoIAPSemana.vig_Domingo = Convert.ToInt32(dr["vig_Domingo"]);
                    if (!Convert.IsDBNull(dr["vac_Domingo"]))
                        oConsumoIAPSemana.vac_Domingo = Convert.ToInt32(dr["vac_Domingo"]);
                    if (!Convert.IsDBNull(dr["TotalEstimado"]))
                        oConsumoIAPSemana.TotalEstimado = Convert.ToDouble(dr["TotalEstimado"]);
                    if (!Convert.IsDBNull(dr["FinEstimado"]))
                        oConsumoIAPSemana.FinEstimado = Convert.ToDateTime(dr["FinEstimado"]);
                    oConsumoIAPSemana.EsfuerzoTotalAcumulado = Convert.ToDouble(dr["EsfuerzoTotalAcumulado"]);
                    if (!Convert.IsDBNull(dr["FechaUltimaImputacion"]))
                        oConsumoIAPSemana.fultiimp = Convert.ToDateTime(dr["FechaUltimaImputacion"]);
                    if (!Convert.IsDBNull(dr["Pendiente"]))
                        oConsumoIAPSemana.Pendiente = Convert.ToDouble(dr["Pendiente"]);
                    if (!Convert.IsDBNull(dr["t330_falta"]))
                        oConsumoIAPSemana.t330_falta = Convert.ToDateTime(dr["t330_falta"]);
                    if (!Convert.IsDBNull(dr["t330_fbaja"]))
                        oConsumoIAPSemana.t330_fbaja = Convert.ToDateTime(dr["t330_fbaja"]);
                    if (!Convert.IsDBNull(dr["t323_regjornocompleta"]))
                        oConsumoIAPSemana.t323_regjornocompleta = Convert.ToInt32(dr["t323_regjornocompleta"]);
                    if (!Convert.IsDBNull(dr["t323_regfes"]))
                        oConsumoIAPSemana.t323_regfes = Convert.ToInt32(dr["t323_regfes"]);
                    if (!Convert.IsDBNull(dr["t331_obligaest"]))
                        oConsumoIAPSemana.t331_obligaest = Convert.ToInt32(dr["t331_obligaest"]);
                    if (!Convert.IsDBNull(dr["HayIndicaciones"]))
                        oConsumoIAPSemana.HayIndicaciones = Convert.ToInt32(dr["HayIndicaciones"]);
                    if (!Convert.IsDBNull(dr["t336_completado"]))
                        oConsumoIAPSemana.t336_completado = Convert.ToInt32(dr["t336_completado"]);
                    if (!Convert.IsDBNull(dr["t332_impiap"]))
                        oConsumoIAPSemana.t332_impiap = Convert.ToInt32(dr["t332_impiap"]);
                    if (!Convert.IsDBNull(dr["t332_fiv"]))
                        oConsumoIAPSemana.t332_fiv = Convert.ToDateTime(dr["t332_fiv"]);
                    if (!Convert.IsDBNull(dr["t332_ffv"]))
                        oConsumoIAPSemana.t332_ffv = Convert.ToDateTime(dr["t332_ffv"]);
                    if (!Convert.IsDBNull(dr["t346_codpst"]))
                        oConsumoIAPSemana.t346_codpst = Convert.ToString(dr["t346_codpst"]);
                    if (!Convert.IsDBNull(dr["t346_despst"]))
                        oConsumoIAPSemana.t346_despst = Convert.ToString(dr["t346_despst"]);
                    if (!Convert.IsDBNull(dr["t332_otl"]))
                        oConsumoIAPSemana.t332_otl = Convert.ToString(dr["t332_otl"]);
                    if (!Convert.IsDBNull(dr["orden"]))
                        oConsumoIAPSemana.orden = Convert.ToString(dr["orden"]);
                    oConsumoIAPSemana.AccesoBitacora = Convert.ToString(dr["AccesoBitacora"]);

                    lst.Add(oConsumoIAPSemana);

                }
                return lst;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dr != null)
                {
                    if (!dr.IsClosed) dr.Close();
                    dr.Dispose();
                }
            }
        }

        /// <summary>
        /// Obtiene el desglose de primer nivel o de todos los niveles de una fase
        /// </summary>
        internal List<Models.ConsumoIAPSemana> ObtenerConsumosIAPSemanaF(int nUsuario, int nFase, DateTime dDesde, DateTime dHasta, int soloPrimerNivel)
        {
            Models.ConsumoIAPSemana oConsumoIAPSemana = null;
            List<Models.ConsumoIAPSemana> lst = new List<Models.ConsumoIAPSemana>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[5] {
                    Param(enumDBFields.nUsuario, nUsuario),
                    Param(enumDBFields.nFase, nFase),
                    Param(enumDBFields.dDesde, dDesde),
                    Param(enumDBFields.dHasta, dHasta),
                    Param(enumDBFields.soloPrimerNivel, soloPrimerNivel),                    
                };

                dr = cDblib.DataReader("SUP_CONSUMOIAPSEMANA_F_D_IAP30", dbparams);
                while (dr.Read())
                {
                    oConsumoIAPSemana = new Models.ConsumoIAPSemana();
                    if (!Convert.IsDBNull(dr["nivel"]))
                        oConsumoIAPSemana.nivel = Convert.ToInt32(dr["nivel"]);
                    oConsumoIAPSemana.tipo = Convert.ToString(dr["tipo"]);
                    oConsumoIAPSemana.t305_idproyectosubnodo = Convert.ToInt32(dr["t305_idproyectosubnodo"]);
                    oConsumoIAPSemana.t331_idpt = Convert.ToInt32(dr["t331_idpt"]);
                    if (!Convert.IsDBNull(dr["t334_idfase"]))
                        oConsumoIAPSemana.t334_idfase = Convert.ToInt32(dr["t334_idfase"]);
                    if (!Convert.IsDBNull(dr["t335_idactividad"]))
                        oConsumoIAPSemana.t335_idactividad = Convert.ToInt32(dr["t335_idactividad"]);
                    if (!Convert.IsDBNull(dr["t332_idtarea"]))
                        oConsumoIAPSemana.t332_idtarea = Convert.ToInt32(dr["t332_idtarea"]);
                    if (!Convert.IsDBNull(dr["t332_estado"]))
                        oConsumoIAPSemana.t332_estado = Convert.ToInt32(dr["t332_estado"]);
                    oConsumoIAPSemana.denominacion = Convert.ToString(dr["denominacion"]);
                    oConsumoIAPSemana.esf_Lunes = Convert.ToDouble(dr["esf_Lunes"]);
                    oConsumoIAPSemana.esfJorn_Lunes = Convert.ToDouble(dr["esfJorn_Lunes"]);
                    oConsumoIAPSemana.com_Lunes = Convert.ToString(dr["com_Lunes"]);
                    if (!Convert.IsDBNull(dr["lab_Lunes"]))
                        oConsumoIAPSemana.lab_Lunes = Convert.ToInt32(dr["lab_Lunes"]);
                    if (!Convert.IsDBNull(dr["out_Lunes"]))
                        oConsumoIAPSemana.out_Lunes = Convert.ToInt32(dr["out_Lunes"]);
                    if (!Convert.IsDBNull(dr["vig_Lunes"]))
                        oConsumoIAPSemana.vig_Lunes = Convert.ToInt32(dr["vig_Lunes"]);
                    if (!Convert.IsDBNull(dr["vac_Lunes"]))
                        oConsumoIAPSemana.vac_Lunes = Convert.ToInt32(dr["vac_Lunes"]);
                    oConsumoIAPSemana.esf_Martes = Convert.ToDouble(dr["esf_Martes"]);
                    oConsumoIAPSemana.esfJorn_Martes = Convert.ToDouble(dr["esfJorn_Martes"]);
                    oConsumoIAPSemana.com_Martes = Convert.ToString(dr["com_Martes"]);
                    if (!Convert.IsDBNull(dr["lab_Martes"]))
                        oConsumoIAPSemana.lab_Martes = Convert.ToInt32(dr["lab_Martes"]);
                    if (!Convert.IsDBNull(dr["out_Martes"]))
                        oConsumoIAPSemana.out_Martes = Convert.ToInt32(dr["out_Martes"]);
                    if (!Convert.IsDBNull(dr["vig_Martes"]))
                        oConsumoIAPSemana.vig_Martes = Convert.ToInt32(dr["vig_Martes"]);
                    if (!Convert.IsDBNull(dr["vac_Martes"]))
                        oConsumoIAPSemana.vac_Martes = Convert.ToInt32(dr["vac_Martes"]);
                    oConsumoIAPSemana.esf_Miercoles = Convert.ToDouble(dr["esf_Miercoles"]);
                    oConsumoIAPSemana.esfJorn_Miercoles = Convert.ToDouble(dr["esfJorn_Miercoles"]);
                    oConsumoIAPSemana.com_Miercoles = Convert.ToString(dr["com_Miercoles"]);
                    if (!Convert.IsDBNull(dr["lab_Miercoles"]))
                        oConsumoIAPSemana.lab_Miercoles = Convert.ToInt32(dr["lab_Miercoles"]);
                    if (!Convert.IsDBNull(dr["out_Miercoles"]))
                        oConsumoIAPSemana.out_Miercoles = Convert.ToInt32(dr["out_Miercoles"]);
                    if (!Convert.IsDBNull(dr["vig_Miercoles"]))
                        oConsumoIAPSemana.vig_Miercoles = Convert.ToInt32(dr["vig_Miercoles"]);
                    if (!Convert.IsDBNull(dr["vac_Miercoles"]))
                        oConsumoIAPSemana.vac_Miercoles = Convert.ToInt32(dr["vac_Miercoles"]);
                    oConsumoIAPSemana.esf_Jueves = Convert.ToDouble(dr["esf_Jueves"]);
                    oConsumoIAPSemana.esfJorn_Jueves = Convert.ToDouble(dr["esfJorn_Jueves"]);
                    oConsumoIAPSemana.com_Jueves = Convert.ToString(dr["com_Jueves"]);
                    if (!Convert.IsDBNull(dr["lab_Jueves"]))
                        oConsumoIAPSemana.lab_Jueves = Convert.ToInt32(dr["lab_Jueves"]);
                    if (!Convert.IsDBNull(dr["out_Jueves"]))
                        oConsumoIAPSemana.out_Jueves = Convert.ToInt32(dr["out_Jueves"]);
                    if (!Convert.IsDBNull(dr["vig_Jueves"]))
                        oConsumoIAPSemana.vig_Jueves = Convert.ToInt32(dr["vig_Jueves"]);
                    if (!Convert.IsDBNull(dr["vac_Jueves"]))
                        oConsumoIAPSemana.vac_Jueves = Convert.ToInt32(dr["vac_Jueves"]);
                    oConsumoIAPSemana.esf_Viernes = Convert.ToDouble(dr["esf_Viernes"]);
                    oConsumoIAPSemana.esfJorn_Viernes = Convert.ToDouble(dr["esfJorn_Viernes"]);
                    oConsumoIAPSemana.com_Viernes = Convert.ToString(dr["com_Viernes"]);
                    if (!Convert.IsDBNull(dr["lab_Viernes"]))
                        oConsumoIAPSemana.lab_Viernes = Convert.ToInt32(dr["lab_Viernes"]);
                    if (!Convert.IsDBNull(dr["out_Viernes"]))
                        oConsumoIAPSemana.out_Viernes = Convert.ToInt32(dr["out_Viernes"]);
                    if (!Convert.IsDBNull(dr["vig_Viernes"]))
                        oConsumoIAPSemana.vig_Viernes = Convert.ToInt32(dr["vig_Viernes"]);
                    if (!Convert.IsDBNull(dr["vac_Viernes"]))
                        oConsumoIAPSemana.vac_Viernes = Convert.ToInt32(dr["vac_Viernes"]);
                    oConsumoIAPSemana.esf_Sabado = Convert.ToDouble(dr["esf_Sabado"]);
                    oConsumoIAPSemana.esfJorn_Sabado = Convert.ToDouble(dr["esfJorn_Sabado"]);
                    oConsumoIAPSemana.com_Sabado = Convert.ToString(dr["com_Sabado"]);
                    if (!Convert.IsDBNull(dr["lab_Sabado"]))
                        oConsumoIAPSemana.lab_Sabado = Convert.ToInt32(dr["lab_Sabado"]);
                    if (!Convert.IsDBNull(dr["out_Sabado"]))
                        oConsumoIAPSemana.out_Sabado = Convert.ToInt32(dr["out_Sabado"]);
                    if (!Convert.IsDBNull(dr["vig_Sabado"]))
                        oConsumoIAPSemana.vig_Sabado = Convert.ToInt32(dr["vig_Sabado"]);
                    if (!Convert.IsDBNull(dr["vac_Sabado"]))
                        oConsumoIAPSemana.vac_Sabado = Convert.ToInt32(dr["vac_Sabado"]);
                    oConsumoIAPSemana.esf_Domingo = Convert.ToDouble(dr["esf_Domingo"]);
                    oConsumoIAPSemana.esfJorn_Domingo = Convert.ToDouble(dr["esfJorn_Domingo"]);
                    oConsumoIAPSemana.com_Domingo = Convert.ToString(dr["com_Domingo"]);
                    if (!Convert.IsDBNull(dr["lab_Domingo"]))
                        oConsumoIAPSemana.lab_Domingo = Convert.ToInt32(dr["lab_Domingo"]);
                    if (!Convert.IsDBNull(dr["out_Domingo"]))
                        oConsumoIAPSemana.out_Domingo = Convert.ToInt32(dr["out_Domingo"]);
                    if (!Convert.IsDBNull(dr["vig_Domingo"]))
                        oConsumoIAPSemana.vig_Domingo = Convert.ToInt32(dr["vig_Domingo"]);
                    if (!Convert.IsDBNull(dr["vac_Domingo"]))
                        oConsumoIAPSemana.vac_Domingo = Convert.ToInt32(dr["vac_Domingo"]);
                    if (!Convert.IsDBNull(dr["TotalEstimado"]))
                        oConsumoIAPSemana.TotalEstimado = Convert.ToDouble(dr["TotalEstimado"]);
                    if (!Convert.IsDBNull(dr["FinEstimado"]))
                        oConsumoIAPSemana.FinEstimado = Convert.ToDateTime(dr["FinEstimado"]);
                    oConsumoIAPSemana.EsfuerzoTotalAcumulado = Convert.ToDouble(dr["EsfuerzoTotalAcumulado"]);
                    if (!Convert.IsDBNull(dr["FechaUltimaImputacion"]))
                        oConsumoIAPSemana.fultiimp = Convert.ToDateTime(dr["FechaUltimaImputacion"]);
                    if (!Convert.IsDBNull(dr["Pendiente"]))
                        oConsumoIAPSemana.Pendiente = Convert.ToDouble(dr["Pendiente"]);
                    if (!Convert.IsDBNull(dr["t330_falta"]))
                        oConsumoIAPSemana.t330_falta = Convert.ToDateTime(dr["t330_falta"]);
                    if (!Convert.IsDBNull(dr["t330_fbaja"]))
                        oConsumoIAPSemana.t330_fbaja = Convert.ToDateTime(dr["t330_fbaja"]);
                    if (!Convert.IsDBNull(dr["t323_regjornocompleta"]))
                        oConsumoIAPSemana.t323_regjornocompleta = Convert.ToInt32(dr["t323_regjornocompleta"]);
                    if (!Convert.IsDBNull(dr["t323_regfes"]))
                        oConsumoIAPSemana.t323_regfes = Convert.ToInt32(dr["t323_regfes"]);
                    if (!Convert.IsDBNull(dr["t331_obligaest"]))
                        oConsumoIAPSemana.t331_obligaest = Convert.ToInt32(dr["t331_obligaest"]);
                    if (!Convert.IsDBNull(dr["HayIndicaciones"]))
                        oConsumoIAPSemana.HayIndicaciones = Convert.ToInt32(dr["HayIndicaciones"]);
                    if (!Convert.IsDBNull(dr["t336_completado"]))
                        oConsumoIAPSemana.t336_completado = Convert.ToInt32(dr["t336_completado"]);
                    if (!Convert.IsDBNull(dr["t332_impiap"]))
                        oConsumoIAPSemana.t332_impiap = Convert.ToInt32(dr["t332_impiap"]);
                    if (!Convert.IsDBNull(dr["t332_fiv"]))
                        oConsumoIAPSemana.t332_fiv = Convert.ToDateTime(dr["t332_fiv"]);
                    if (!Convert.IsDBNull(dr["t332_ffv"]))
                        oConsumoIAPSemana.t332_ffv = Convert.ToDateTime(dr["t332_ffv"]);
                    if (!Convert.IsDBNull(dr["t346_codpst"]))
                        oConsumoIAPSemana.t346_codpst = Convert.ToString(dr["t346_codpst"]);
                    if (!Convert.IsDBNull(dr["t346_despst"]))
                        oConsumoIAPSemana.t346_despst = Convert.ToString(dr["t346_despst"]);
                    if (!Convert.IsDBNull(dr["t332_otl"]))
                        oConsumoIAPSemana.t332_otl = Convert.ToString(dr["t332_otl"]);
                    if (!Convert.IsDBNull(dr["orden"]))
                        oConsumoIAPSemana.orden = Convert.ToString(dr["orden"]);
                    oConsumoIAPSemana.AccesoBitacora = Convert.ToString(dr["AccesoBitacora"]);

                    lst.Add(oConsumoIAPSemana);

                }
                return lst;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dr != null)
                {
                    if (!dr.IsClosed) dr.Close();
                    dr.Dispose();
                }
            }
        }

        /// <summary>
        /// Obtiene el desglose de primer nivel o de todos los niveles de una actividad
        /// </summary>
        internal List<Models.ConsumoIAPSemana> ObtenerConsumosIAPSemanaA(int nUsuario, int nActividad, DateTime dDesde, DateTime dHasta)
        {
            Models.ConsumoIAPSemana oConsumoIAPSemana = null;
            List<Models.ConsumoIAPSemana> lst = new List<Models.ConsumoIAPSemana>();
            IDataReader dr = null;

            try
            {
                SqlParameter[] dbparams = new SqlParameter[4] {
                    Param(enumDBFields.nUsuario, nUsuario),
                    Param(enumDBFields.nActividad, nActividad),
                    Param(enumDBFields.dDesde, dDesde),
                    Param(enumDBFields.dHasta, dHasta)                   
                };

                dr = cDblib.DataReader("SUP_CONSUMOIAPSEMANA_A_D_IAP30", dbparams);
                while (dr.Read())
                {
                    oConsumoIAPSemana = new Models.ConsumoIAPSemana();
                    if (!Convert.IsDBNull(dr["nivel"]))
                        oConsumoIAPSemana.nivel = Convert.ToInt32(dr["nivel"]);
                    oConsumoIAPSemana.tipo = Convert.ToString(dr["tipo"]);
                    oConsumoIAPSemana.t305_idproyectosubnodo = Convert.ToInt32(dr["t305_idproyectosubnodo"]);
                    oConsumoIAPSemana.t331_idpt = Convert.ToInt32(dr["t331_idpt"]);
                    if (!Convert.IsDBNull(dr["t334_idfase"]))
                        oConsumoIAPSemana.t334_idfase = Convert.ToInt32(dr["t334_idfase"]);
                    if (!Convert.IsDBNull(dr["t335_idactividad"]))
                        oConsumoIAPSemana.t335_idactividad = Convert.ToInt32(dr["t335_idactividad"]);
                    if (!Convert.IsDBNull(dr["t332_idtarea"]))
                        oConsumoIAPSemana.t332_idtarea = Convert.ToInt32(dr["t332_idtarea"]);
                    if (!Convert.IsDBNull(dr["t332_estado"]))
                        oConsumoIAPSemana.t332_estado = Convert.ToInt32(dr["t332_estado"]);
                    oConsumoIAPSemana.denominacion = Convert.ToString(dr["denominacion"]);
                    oConsumoIAPSemana.esf_Lunes = Convert.ToDouble(dr["esf_Lunes"]);
                    oConsumoIAPSemana.esfJorn_Lunes = Convert.ToDouble(dr["esfJorn_Lunes"]);
                    oConsumoIAPSemana.com_Lunes = Convert.ToString(dr["com_Lunes"]);
                    if (!Convert.IsDBNull(dr["lab_Lunes"]))
                        oConsumoIAPSemana.lab_Lunes = Convert.ToInt32(dr["lab_Lunes"]);
                    if (!Convert.IsDBNull(dr["out_Lunes"]))
                        oConsumoIAPSemana.out_Lunes = Convert.ToInt32(dr["out_Lunes"]);
                    if (!Convert.IsDBNull(dr["vig_Lunes"]))
                        oConsumoIAPSemana.vig_Lunes = Convert.ToInt32(dr["vig_Lunes"]);
                    if (!Convert.IsDBNull(dr["vac_Lunes"]))
                        oConsumoIAPSemana.vac_Lunes = Convert.ToInt32(dr["vac_Lunes"]);
                    oConsumoIAPSemana.esf_Martes = Convert.ToDouble(dr["esf_Martes"]);
                    oConsumoIAPSemana.esfJorn_Martes = Convert.ToDouble(dr["esfJorn_Martes"]);
                    oConsumoIAPSemana.com_Martes = Convert.ToString(dr["com_Martes"]);
                    if (!Convert.IsDBNull(dr["lab_Martes"]))
                        oConsumoIAPSemana.lab_Martes = Convert.ToInt32(dr["lab_Martes"]);
                    if (!Convert.IsDBNull(dr["out_Martes"]))
                        oConsumoIAPSemana.out_Martes = Convert.ToInt32(dr["out_Martes"]);
                    if (!Convert.IsDBNull(dr["vig_Martes"]))
                        oConsumoIAPSemana.vig_Martes = Convert.ToInt32(dr["vig_Martes"]);
                    if (!Convert.IsDBNull(dr["vac_Martes"]))
                        oConsumoIAPSemana.vac_Martes = Convert.ToInt32(dr["vac_Martes"]);
                    oConsumoIAPSemana.esf_Miercoles = Convert.ToDouble(dr["esf_Miercoles"]);
                    oConsumoIAPSemana.esfJorn_Miercoles = Convert.ToDouble(dr["esfJorn_Miercoles"]);
                    oConsumoIAPSemana.com_Miercoles = Convert.ToString(dr["com_Miercoles"]);
                    if (!Convert.IsDBNull(dr["lab_Miercoles"]))
                        oConsumoIAPSemana.lab_Miercoles = Convert.ToInt32(dr["lab_Miercoles"]);
                    if (!Convert.IsDBNull(dr["out_Miercoles"]))
                        oConsumoIAPSemana.out_Miercoles = Convert.ToInt32(dr["out_Miercoles"]);
                    if (!Convert.IsDBNull(dr["vig_Miercoles"]))
                        oConsumoIAPSemana.vig_Miercoles = Convert.ToInt32(dr["vig_Miercoles"]);
                    if (!Convert.IsDBNull(dr["vac_Miercoles"]))
                        oConsumoIAPSemana.vac_Miercoles = Convert.ToInt32(dr["vac_Miercoles"]);
                    oConsumoIAPSemana.esf_Jueves = Convert.ToDouble(dr["esf_Jueves"]);
                    oConsumoIAPSemana.esfJorn_Jueves = Convert.ToDouble(dr["esfJorn_Jueves"]);
                    oConsumoIAPSemana.com_Jueves = Convert.ToString(dr["com_Jueves"]);
                    if (!Convert.IsDBNull(dr["lab_Jueves"]))
                        oConsumoIAPSemana.lab_Jueves = Convert.ToInt32(dr["lab_Jueves"]);
                    if (!Convert.IsDBNull(dr["out_Jueves"]))
                        oConsumoIAPSemana.out_Jueves = Convert.ToInt32(dr["out_Jueves"]);
                    if (!Convert.IsDBNull(dr["vig_Jueves"]))
                        oConsumoIAPSemana.vig_Jueves = Convert.ToInt32(dr["vig_Jueves"]);
                    if (!Convert.IsDBNull(dr["vac_Jueves"]))
                        oConsumoIAPSemana.vac_Jueves = Convert.ToInt32(dr["vac_Jueves"]);
                    oConsumoIAPSemana.esf_Viernes = Convert.ToDouble(dr["esf_Viernes"]);
                    oConsumoIAPSemana.esfJorn_Viernes = Convert.ToDouble(dr["esfJorn_Viernes"]);
                    oConsumoIAPSemana.com_Viernes = Convert.ToString(dr["com_Viernes"]);
                    if (!Convert.IsDBNull(dr["lab_Viernes"]))
                        oConsumoIAPSemana.lab_Viernes = Convert.ToInt32(dr["lab_Viernes"]);
                    if (!Convert.IsDBNull(dr["out_Viernes"]))
                        oConsumoIAPSemana.out_Viernes = Convert.ToInt32(dr["out_Viernes"]);
                    if (!Convert.IsDBNull(dr["vig_Viernes"]))
                        oConsumoIAPSemana.vig_Viernes = Convert.ToInt32(dr["vig_Viernes"]);
                    if (!Convert.IsDBNull(dr["vac_Viernes"]))
                        oConsumoIAPSemana.vac_Viernes = Convert.ToInt32(dr["vac_Viernes"]);
                    oConsumoIAPSemana.esf_Sabado = Convert.ToDouble(dr["esf_Sabado"]);
                    oConsumoIAPSemana.esfJorn_Sabado = Convert.ToDouble(dr["esfJorn_Sabado"]);
                    oConsumoIAPSemana.com_Sabado = Convert.ToString(dr["com_Sabado"]);
                    if (!Convert.IsDBNull(dr["lab_Sabado"]))
                        oConsumoIAPSemana.lab_Sabado = Convert.ToInt32(dr["lab_Sabado"]);
                    if (!Convert.IsDBNull(dr["out_Sabado"]))
                        oConsumoIAPSemana.out_Sabado = Convert.ToInt32(dr["out_Sabado"]);
                    if (!Convert.IsDBNull(dr["vig_Sabado"]))
                        oConsumoIAPSemana.vig_Sabado = Convert.ToInt32(dr["vig_Sabado"]);
                    if (!Convert.IsDBNull(dr["vac_Sabado"]))
                        oConsumoIAPSemana.vac_Sabado = Convert.ToInt32(dr["vac_Sabado"]);
                    oConsumoIAPSemana.esf_Domingo = Convert.ToDouble(dr["esf_Domingo"]);
                    oConsumoIAPSemana.esfJorn_Domingo = Convert.ToDouble(dr["esfJorn_Domingo"]);
                    oConsumoIAPSemana.com_Domingo = Convert.ToString(dr["com_Domingo"]);
                    if (!Convert.IsDBNull(dr["lab_Domingo"]))
                        oConsumoIAPSemana.lab_Domingo = Convert.ToInt32(dr["lab_Domingo"]);
                    if (!Convert.IsDBNull(dr["out_Domingo"]))
                        oConsumoIAPSemana.out_Domingo = Convert.ToInt32(dr["out_Domingo"]);
                    if (!Convert.IsDBNull(dr["vig_Domingo"]))
                        oConsumoIAPSemana.vig_Domingo = Convert.ToInt32(dr["vig_Domingo"]);
                    if (!Convert.IsDBNull(dr["vac_Domingo"]))
                        oConsumoIAPSemana.vac_Domingo = Convert.ToInt32(dr["vac_Domingo"]);
                    if (!Convert.IsDBNull(dr["TotalEstimado"]))
                        oConsumoIAPSemana.TotalEstimado = Convert.ToDouble(dr["TotalEstimado"]);
                    if (!Convert.IsDBNull(dr["FinEstimado"]))
                        oConsumoIAPSemana.FinEstimado = Convert.ToDateTime(dr["FinEstimado"]);
                    oConsumoIAPSemana.EsfuerzoTotalAcumulado = Convert.ToDouble(dr["EsfuerzoTotalAcumulado"]);
                    if (!Convert.IsDBNull(dr["FechaUltimaImputacion"]))
                        oConsumoIAPSemana.fultiimp = Convert.ToDateTime(dr["FechaUltimaImputacion"]);
                    if (!Convert.IsDBNull(dr["Pendiente"]))
                        oConsumoIAPSemana.Pendiente = Convert.ToDouble(dr["Pendiente"]);
                    if (!Convert.IsDBNull(dr["t330_falta"]))
                        oConsumoIAPSemana.t330_falta = Convert.ToDateTime(dr["t330_falta"]);
                    if (!Convert.IsDBNull(dr["t330_fbaja"]))
                        oConsumoIAPSemana.t330_fbaja = Convert.ToDateTime(dr["t330_fbaja"]);
                    if (!Convert.IsDBNull(dr["t323_regjornocompleta"]))
                        oConsumoIAPSemana.t323_regjornocompleta = Convert.ToInt32(dr["t323_regjornocompleta"]);
                    if (!Convert.IsDBNull(dr["t323_regfes"]))
                        oConsumoIAPSemana.t323_regfes = Convert.ToInt32(dr["t323_regfes"]);
                    if (!Convert.IsDBNull(dr["t331_obligaest"]))
                        oConsumoIAPSemana.t331_obligaest = Convert.ToInt32(dr["t331_obligaest"]);
                    if (!Convert.IsDBNull(dr["HayIndicaciones"]))
                        oConsumoIAPSemana.HayIndicaciones = Convert.ToInt32(dr["HayIndicaciones"]);
                    if (!Convert.IsDBNull(dr["t336_completado"]))
                        oConsumoIAPSemana.t336_completado = Convert.ToInt32(dr["t336_completado"]);
                    if (!Convert.IsDBNull(dr["t332_impiap"]))
                        oConsumoIAPSemana.t332_impiap = Convert.ToInt32(dr["t332_impiap"]);
                    if (!Convert.IsDBNull(dr["t332_fiv"]))
                        oConsumoIAPSemana.t332_fiv = Convert.ToDateTime(dr["t332_fiv"]);
                    if (!Convert.IsDBNull(dr["t332_ffv"]))
                        oConsumoIAPSemana.t332_ffv = Convert.ToDateTime(dr["t332_ffv"]);
                    if (!Convert.IsDBNull(dr["t346_codpst"]))
                        oConsumoIAPSemana.t346_codpst = Convert.ToString(dr["t346_codpst"]);
                    if (!Convert.IsDBNull(dr["t346_despst"]))
                        oConsumoIAPSemana.t346_despst = Convert.ToString(dr["t346_despst"]);
                    if (!Convert.IsDBNull(dr["t332_otl"]))
                        oConsumoIAPSemana.t332_otl = Convert.ToString(dr["t332_otl"]);
                    if (!Convert.IsDBNull(dr["orden"]))
                        oConsumoIAPSemana.orden = Convert.ToString(dr["orden"]);
                    oConsumoIAPSemana.AccesoBitacora = Convert.ToString(dr["AccesoBitacora"]);

                    lst.Add(oConsumoIAPSemana);

                }
                return lst;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dr != null)
                {
                    if (!dr.IsClosed) dr.Close();
                    dr.Dispose();
                }
            }
        }

        #endregion

        #region funciones privadas
        private SqlParameter Param(enumDBFields dbField, object value)
        {
            SqlParameter dbParam = null;
            string paramName = null;
            SqlDbType paramType = default(SqlDbType);
            int paramSize = 0;
            ParameterDirection paramDirection = ParameterDirection.Input;

            switch (dbField)
            {
                case enumDBFields.nUsuario:
                    paramName = "@nUsuario";
                    paramType = SqlDbType.Int;
                    paramSize = 5;
                    break;                
                case enumDBFields.dDesde:
                    paramName = "@dDesde";
                    paramType = SqlDbType.DateTime;
                    paramSize = 8;
                    break;
                case enumDBFields.dHasta:
                    paramName = "@dHasta";
                    paramType = SqlDbType.DateTime;
                    paramSize = 8;
                    break;                
                case enumDBFields.nPSN:
                    paramName = "@nPSN";
                    paramType = SqlDbType.Int;
                    paramSize = 5;
                    break;
                case enumDBFields.nPT:
                    paramName = "@nPT";
                    paramType = SqlDbType.Int;
                    paramSize = 5;
                    break;
                case enumDBFields.nFase:
                    paramName = "@nFase";
                    paramType = SqlDbType.Int;
                    paramSize = 5;
                    break;
                case enumDBFields.nActividad:
                    paramName = "@nActividad";
                    paramType = SqlDbType.Int;
                    paramSize = 5;
                    break;
                case enumDBFields.datatablePSN:
                    paramName = "@TABPSN";
                    paramType = SqlDbType.Structured;
                    paramSize = 100;
                    break;
                case enumDBFields.soloPrimerNivel:
                    paramName = "@bSoloPrimerNivel";
                    paramType = SqlDbType.Bit;
					paramSize = 1;
                    break;
            }


            dbParam = cDblib.dbParameter(paramName, paramType, paramSize);
            dbParam.Direction = paramDirection;
            if (paramDirection == ParameterDirection.Input | paramDirection == ParameterDirection.InputOutput) dbParam.Value = value;

            return dbParam;

        }

        #endregion

    }

}