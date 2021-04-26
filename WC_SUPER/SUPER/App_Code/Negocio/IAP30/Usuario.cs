using System;
using System.Collections;
using System.Collections.Generic;
using SUPERANTIGUO = SUPER;
using IB.SUPER.Shared;
using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for Usuario
/// </summary>
namespace IB.SUPER.IAP30.BLL
{
    public class Usuario : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("76e66199-5b14-49e7-b651-69145d2a276c");
        private bool disposed = false;

        #endregion

        #region Constructor

        public Usuario()
            : base()
        {
            //OpenDbConn();
        }

        public Usuario(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas
        public Models.Usuario ObtenerRecurso(string IDRED, int? t314_idusuario)
        {
            OpenDbConn();

            try
            {
                DAL.Usuario cUsuario = new DAL.Usuario(cDblib);
                return cUsuario.ObtenerRecurso(IDRED, t314_idusuario);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Models.Usuario ObtenerRecursoReducido(int t314_idusuario)
        {
            OpenDbConn();

            try
            {
                DAL.Usuario cUsuario = new DAL.Usuario(cDblib);
                return cUsuario.ObtenerRecursoReducido(t314_idusuario);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Models.Usuario GetFechaUltImputacion(int t314_idusuario)
        {
            OpenDbConn();

            try
            {
                DAL.Usuario cUsuario = new DAL.Usuario(cDblib);
                return cUsuario.GetFecUltImputacion(t314_idusuario);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public Models.Usuario Grabar(int idCalendario, int idUser, int idUserEntrada, string sCodRed, int iUMC_IAP,
                             string sNombreEmpleado, string sNombreEmpleadoEntrada,
                             bool bJornadaReducida, double nHorasRed, DateTime? dDesdeRed, DateTime? dHastaRed,
                             int nTarea, int nOpcion, DateTime dUDR, DateTime dDesde, DateTime dHasta,
                             int nModo, bool bFestivos, bool bFinalizado, double nHoras, string obsImputacion,
                             string obsTecnico, double nETE, DateTime? dFFE, bool bObligaest, int nPSN)
        {
            bool bErrorControlado = false, bAvisadoCLE = false;
            string sMsg = "", sRes = "Grabación correcta.";
            //int iImputacionGrabada = 0;
            ArrayList aListCorreo = new ArrayList();
            ArrayList aListCorreoCLE = new ArrayList();

            Hashtable htTareasSuperanCLE = new Hashtable();                        

            Guid methodOwnerID = new Guid("a0491cb0-f8b0-40bf-bf29-9073bd561995");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            //BLL.Usuario bUsuario = new BLL.Usuario(cDblib);
            Models.Usuario cUsuario = new Models.Usuario();

            BLL.TareaPSP bTareaPSP = new BLL.TareaPSP(cDblib);
            Models.TareaPSP oTarea = new Models.TareaPSP();

            BLL.TareaIAPS bTareaIAP = new BLL.TareaIAPS(cDblib);
            Models.TareaIAPS oTareaIAP = new Models.TareaIAPS();

            BLL.Calendario bCalendario = new BLL.Calendario(cDblib);
            Models.Calendario oCal = new Models.Calendario();

            BLL.DesgloseCalendario bDesgloseCalendario = new BLL.DesgloseCalendario(cDblib);
            List<Models.DesgloseCalendario> lstDiasCal = new List<Models.DesgloseCalendario>();

            BLL.UsuarioProyectoSubNodo bUsuarioPSN = new BLL.UsuarioProyectoSubNodo(cDblib);
            Models.UsuarioProyectoSubNodo oUPSN = new Models.UsuarioProyectoSubNodo();

            BLL.ConsumoIAP bConsumoIAP = new BLL.ConsumoIAP(cDblib);
            Models.ConsumoIAP oConsumoIAP = new Models.ConsumoIAP();

            BLL.EstimacionIAP bEst = new BLL.EstimacionIAP(cDblib);

            try
            {
                cUsuario = ObtenerRecurso(sCodRed, idUser);
                oCal = bCalendario.getCalendario(idCalendario, dDesde.Year);

                int nDifDias = IB.SUPER.Shared.Fechas.DateDiff("day", dDesde, dHasta);

                #region Sustitución, si procede, de los datos existentes, por lo que se elimina lo que hubiera imputado en el rango de fechas indicado.
                if (nModo == 1)
                {//Modo sustitucion -> Borra todas las imputaciones en el rango de fechas
                    BLL.ConsumoIAP bConsumo = new BLL.ConsumoIAP(cDblib);
                    bConsumo.DeleteRango(idUser, dDesde, dHasta);
                    //DAL.ConsumoIAP cConsumoIAP = new DAL.ConsumoIAP(cDblib);
                    //int result = cConsumoIAP.DeleteRango(idUser, dDesde, dHasta);
                }
                else
                {//Modo acumulación -> Borra todas las imputaciones a una tarea determinada en el rango de fechas
                    BLL.ConsumoIAP bConsumo = new BLL.ConsumoIAP(cDblib);
                    bConsumo.DeleteTareaRango(idUser, nTarea, dDesde, dHasta);
                    //DAL.ConsumoIAP cConsumoIAP = new DAL.ConsumoIAP(cDblib);
                    //int result = cConsumoIAP.DeleteTareaRango(idUser, nTarea, dDesde, dHasta);
                }
                #endregion

                #region Obtención de datos relacionados con la tarea
                oTarea = bTareaPSP.Select(nTarea);

                //Obtener los datos de la tarea a la que se va a imputar.
                oTareaIAP = bTareaIAP.Select(nTarea);

                //Obtención de las horas estándar y festivos del rango de fechas.
                //Calendario oCalendario = obtenerDatosHorarios(tr, dDesde, dHasta);
                lstDiasCal = bDesgloseCalendario.ObtenerHorasRango(idCalendario, dDesde, dHasta);

                //Obtener las fechas de inicio y final de la asociación del recurso al proyecto.
                //USUARIOPROYECTOSUBNODO oUPSN = USUARIOPROYECTOSUBNODO.Select(tr, nPSN, (int)Session["UsuarioActual"]);
                oUPSN = bUsuarioPSN.Select(nPSN, idUser);

                DateTime dAltaProy = oUPSN.t330_falta;
                //DateTime dBajaProy = (oUPSN.t330_fbaja.HasValue) ? oUPSN.t330_fbaja : null;
                DateTime? dBajaProy = null;
                if (oUPSN.t330_fbaja != null)
                    dBajaProy = oUPSN.t330_fbaja;

                if (dAltaProy == DateTime.Parse("01/01/1900"))
                {
                    //bErrorControlado = true;
                    //throw (new ValidationException("¡Denegado!<br />No existe fecha de alta en el proyecto."));
                    throw (new Exception("¡Denegado!<br />No existe fecha de alta en el proyecto."));
                    //sMsg += "\nNo existe fecha de alta en el proyecto.";
                }
                #endregion
                #region Control mes cerrado IAP
                if (IB.SUPER.Shared.Fechas.FechaAAnnomes(dDesde) <= iUMC_IAP)
                {
                    //bErrorControlado = true;
                    //throw (new ValidationException("¡Denegado!<br />La fecha de imputación (" + dDesde.ToShortDateString() + ") pertenece a un mes IAP cerrado. Último mes cerrado IAP (" + Fechas.AnnomesAFechaDescLarga((int)HttpContext.Current.Session["UMC_IAP"]) + ")."));
                    throw (new Exception("¡Denegado!<br />La fecha de imputación (" + dDesde.ToShortDateString() + ") pertenece a un mes IAP cerrado. Último mes cerrado IAP (" + IB.SUPER.Shared.Fechas.AnnomesAFechaDescLarga(iUMC_IAP) + ")."));
                }
                #endregion
                #region Control de fechas
                if ((dDesde < dAltaProy) || (dBajaProy != null && dHasta > dBajaProy))
                {
                    //bErrorControlado = true;
                    //throw (new ValidationException("¡Denegado!<br />El periodo de imputación seleccionado se encuentra en parte o totalmente fuera de su asignación al proyecto."));
                    throw (new Exception("¡Denegado!<br />El periodo de imputación seleccionado se encuentra en parte o totalmente fuera de su asignación al proyecto."));
                }
                if ((dDesde < oTarea.t332_fiv) || (oTarea.t332_ffv != null && dHasta > oTarea.t332_ffv))
                {
                    //bErrorControlado = true;
                    //throw (new ValidationException("¡Denegado!<br />El periodo de imputación seleccionado se encuentra en parte o totalmente fuera del periodo de vigencia la tarea."));
                    throw (new Exception("¡Denegado!<br />El periodo de imputación seleccionado se encuentra en parte o totalmente fuera del periodo de vigencia la tarea."));
                }
                #endregion

                #region Control de huecos
                //if (tipoImp != "1")
                //{//El control de huecos se hace desde cliente
                //    if ((bool)HttpContext.Current.Session["CONTROLHUECOS"])
                //    {
                //        ///Controlar si entre el último día imputado (f_ult_imputac) y el primer día de imputación hay días laborables.
                //        if (existenHuecos(oCal, dDesde))
                //        {
                //            //bErrorControlado = true;
                //            throw (new ValidationException("¡Denegado!<br />Se ha detectado que entre el último día reportado y la fecha inicio imputación existen huecos."));
                //            //sMsg += "\nSe ha detectado que entre el último día reportado y la fecha inicio imputación existen huecos.";
                //        }
                //    }
                //}
                #endregion

                #region Imputación de las horas indicadas en cada uno de los días del intervalo
                bool bFestAux = false;
                DateTime dDiaAux;
                float nHorasDia = 0, fHorasAcumuladas = (float)oTareaIAP.nConsumidoHoras;
                double nJornadas = 0;
                bool ultimaImputacion = false;
                for (int i = 0; i <= nDifDias; i++)
                {
                    if (i == nDifDias) ultimaImputacion = true;
                    bFestAux = false;
                    dDiaAux = dDesde.AddDays(i);

                    #region Control día laborable y no festivo
                    foreach (Models.DesgloseCalendario oDia in lstDiasCal)
                    {
                        if (oDia.t067_dia == dDiaAux)
                        {
                            nHorasDia = oDia.t067_horas;
                            if (nOpcion == 1 || nOpcion == 2 || nHorasDia == 0) nJornadas = 1;
                            else nJornadas = nHoras / nHorasDia;
                            //Festivo
                            if (oDia.t067_festivo == 1)
                            {
                                bFestAux = true;
                                break;
                            }
                            //No laborable
                            switch (oDia.t067_dia.DayOfWeek)
                            {
                                case DayOfWeek.Monday:
                                    if (oCal.t066_semlabL == 0) bFestAux = true;
                                    break;
                                case DayOfWeek.Tuesday:
                                    if (oCal.t066_semlabM == 0) bFestAux = true;
                                    break;
                                case DayOfWeek.Wednesday:
                                    if (oCal.t066_semlabX == 0) bFestAux = true;
                                    break;
                                case DayOfWeek.Thursday:
                                    if (oCal.t066_semlabJ == 0) bFestAux = true;
                                    break;
                                case DayOfWeek.Friday:
                                    if (oCal.t066_semlabV == 0) bFestAux = true;
                                    break;
                                case DayOfWeek.Saturday:
                                    if (oCal.t066_semlabS == 0) bFestAux = true;
                                    break;
                                case DayOfWeek.Sunday:
                                    if (oCal.t066_semlabD == 0) bFestAux = true;
                                    break;
                            }
                            //if (bFestAux) 
                            break;
                        }
                    }
                    if (!bFestAux)
                    {
                        ///Control de jornada reducida.
                        if (bJornadaReducida)
                        {
                            if (dDiaAux >= dDesdeRed && dDiaAux <= dHastaRed)
                            {
                                nHorasDia = float.Parse(nHorasRed.ToString());
                                if (nOpcion == 3)
                                    nJornadas = nHoras / nHorasRed;
                                else
                                    nJornadas = 1;
                            }
                        }
                        #region Comprobacion de horas laborables
                        if (nHorasDia == 0)
                        {
                            bErrorControlado = true;
                            sMsg += "<br />Hay un error en el calendario. El número de horas laborables es cero para el día " + dDiaAux.ToShortDateString();
                            //throw (new ValidationException(sMsg));
                            throw (new Exception(sMsg));
                        }
                        if (nJornadas == 0)
                        {
                            bErrorControlado = true;
                            sMsg += "<br />Hay un error en el calendario. El número de horas laborables es cero para el día " + dDiaAux.ToShortDateString();
                            //throw (new ValidationException(sMsg));
                            throw (new Exception(sMsg));
                        }
                        #endregion
                    }
                    #endregion
                    #region Imputar
                    if (nOpcion == 1 || nOpcion == 2)
                    {//En estas opciones el modo es siempre sustitución
                        #region Imputar jornada
                        //Ahora, si el día es laborable y no festivo, insert de las horas estándar.
                        if (!bFestAux)
                        {
                            #region Controlar CLE de la tarea
                            fHorasAcumuladas += nHorasDia;

                            if (oTarea.t332_cle > 0 && fHorasAcumuladas > oTarea.t332_cle)
                            {
                                sMsg = bTareaPSP.ControlLimiteEsfuerzos(nTarea, nHorasDia, dDiaAux, htTareasSuperanCLE);                                
                                if (sMsg != "")
                                {
                                    bErrorControlado = true;
                                    throw (new Exception(sMsg));
                                }
                            }

                            #endregion
                            #region Imputar
                            oConsumoIAP.t332_idtarea = nTarea;
                            oConsumoIAP.t314_idusuario = idUser;
                            oConsumoIAP.t314_idusuario_modif = idUserEntrada;
                            oConsumoIAP.t337_comentario = obsImputacion;
                            oConsumoIAP.t337_esfuerzo = nHorasDia;
                            oConsumoIAP.t337_esfuerzoenjor = nJornadas;
                            oConsumoIAP.t337_fecha = dDiaAux;
                            oConsumoIAP.t337_fecmodif = DateTime.Now;
                            //La propia Insert está también comprobando su puede grabar en función del CLE
                            bConsumoIAP.Insert(oConsumoIAP);
                            //DAL.ConsumoIAP cConsumoIAP = new DAL.ConsumoIAP(cDblib);
                            //cConsumoIAP.Insert(oConsumoIAP);

                            //if (iImputacionGrabada == 1) {
                                ControlTraspasoIAP(idUser, idUserEntrada, sNombreEmpleadoEntrada, nTarea, dDiaAux, nHorasDia, sNombreEmpleado, aListCorreo);
                            //}
                            //else
                            //{//No ha podido grabar porque excede el CLE y es bloqueante
                            //    if (!bAvisadoCLE)
                            //    {
                            //        sMsg += "<br />Se ha sobrepasado el límite de horas máximo permitido ";
                            //        sMsg += "para la tarea '" + oTarea.t332_idtarea.ToString() + " " + oTarea.t332_destarea + "'.<br />En la fecha de imputación (" + dDiaAux.ToShortDateString() + "), el exceso es ya de " + double.Parse((fHorasAcumuladas - oTarea.t332_cle).ToString()).ToString("N") + " horas. ";
                            //        sMsg += "<br />Para poder imputar más horas a dicha tarea, ponte en contacto con el responsable de la misma.";
                            //        bErrorControlado = true;
                            //        bAvisadoCLE = true;
                            //    }
                            //}
                            
                            #endregion
                        }
                        #endregion
                    }
                    else //nOpcion == 3
                    {
                        #region Imputar x horas
                        if (nModo == 1) //Modo sustitución (ya se ha borrado lo que hubiera).
                        {
                            #region Insertar imputacion
                            if (bFestivos || (!bFestivos && !bFestAux))
                            {
                                #region Controlar CLE de la tarea
                                fHorasAcumuladas += (float)nHoras;

                                if (oTarea.t332_cle > 0 && fHorasAcumuladas > oTarea.t332_cle)
                                {
                                    sMsg = bTareaPSP.ControlLimiteEsfuerzos(nTarea, nHoras, dDiaAux, htTareasSuperanCLE);
                                    bAvisadoCLE = true;
                                    if (sMsg != "")
                                    {
                                        bErrorControlado = true;
                                        throw (new Exception(sMsg));
                                    }
                                }
                                                                
                                #endregion
                                #region Imputar
                                oConsumoIAP.t332_idtarea = nTarea;
                                oConsumoIAP.t314_idusuario = idUser;
                                oConsumoIAP.t314_idusuario_modif = idUserEntrada;
                                oConsumoIAP.t337_comentario = obsImputacion;
                                oConsumoIAP.t337_esfuerzo = (float)nHoras;
                                oConsumoIAP.t337_esfuerzoenjor = nJornadas;
                                oConsumoIAP.t337_fecha = dDiaAux;
                                oConsumoIAP.t337_fecmodif = DateTime.Now;

                                bConsumoIAP.Insert(oConsumoIAP);
                                //DAL.ConsumoIAP cConsumoIAP = new DAL.ConsumoIAP(cDblib);
                                //cConsumoIAP.Insert(oConsumoIAP);

                                //if (iImputacionGrabada == 1)
                                //{
                                    ControlTraspasoIAP(idUser, idUserEntrada, sNombreEmpleadoEntrada, nTarea, dDiaAux, (float)nHoras, sNombreEmpleado, aListCorreo);
                                //}
                                //else
                                //{
                                //    if (!bAvisadoCLE)
                                //    {
                                //        sMsg += "<br />Se ha sobrepasado el límite de horas máximo permitido ";
                                //        sMsg += "para la tarea '" + oTarea.t332_idtarea.ToString() + " " + oTarea.t332_destarea + "'.<br />En la fecha de imputación (" + dDiaAux.ToShortDateString() + ") ya el exceso es de " + double.Parse((oTareaIAP.nConsumidoHoras + nHoras - oTarea.t332_cle).ToString()).ToString("N") + " horas. ";
                                //        sMsg += "<br />Para poder imputar más horas a dicha tarea, pongase en contacto con el responsable de la misma.";
                                //        bErrorControlado = true;
                                //        bAvisadoCLE = true;
                                //    }
                                //}
                                #endregion
                            }
                            #endregion
                        }
                        else //Modo acumulación
                        {
                            #region Acumular imputación
                            if (bFestivos || (!bFestivos && !bFestAux))
                            {
                                //Obtener el sumatorio de las imputaciones en otras tareas (en la actual ya las hemos borrado).
                                Models.ConsumoIAP oConsumoIAP_Aux = new Models.ConsumoIAP();
                                oConsumoIAP_Aux = bConsumoIAP.SelectFecha(idUser, dDiaAux);

                                double nImpDia = oConsumoIAP_Aux.t337_esfuerzo;  //Consumos totales del día de otras tareas.
                                //double nImpDiaTarea = oConsumo.nHorasDiaTarea;   //Consumos de la tarea en el día.

                                double nTotalHoras = nHoras + nImpDia;// +nImpDiaTarea;
                                double nTotalTarea = nHoras;// +nImpDiaTarea;
                                if (nHorasDia == 0) nJornadas = 1;
                                else nJornadas = nTotalTarea / nHorasDia;
                                if (nTotalHoras > 24)
                                {
                                    bErrorControlado = true;
                                    //throw (new Exception("Las imputaciones del día " + dDiaAux.ToShortDateString() + " superan las 24h."));
                                    sMsg += "<br />Las imputaciones del día " + dDiaAux.ToShortDateString() + " superan las 24h.";
                                }
                                ///Delete e insert. No se hace una update, porque puede que no haya consumo que actualizar.
                                bConsumoIAP.Delete(nTarea, idUser, dDiaAux);
                                //DAL.ConsumoIAP cConsumoIAP = new DAL.ConsumoIAP(cDblib);
                                //int result = cConsumoIAP.Delete(nTarea, idUser, dDiaAux);

                                #region Controlar CLE de la tarea
                                fHorasAcumuladas += (float)nHoras;

                                if (oTarea.t332_cle > 0 && fHorasAcumuladas > oTarea.t332_cle)
                                {
                                    sMsg = bTareaPSP.ControlLimiteEsfuerzos(nTarea, nHorasDia, dDiaAux, htTareasSuperanCLE);
                                    
                                    if (sMsg != "")
                                    {                                        
                                        bErrorControlado = true;
                                        throw (new Exception(sMsg));
                                    }
                                }
                                
                                #endregion
                                #region Imputar
                                oConsumoIAP.t332_idtarea = nTarea;
                                oConsumoIAP.t314_idusuario = idUser;
                                oConsumoIAP.t314_idusuario_modif = idUserEntrada;
                                oConsumoIAP.t337_comentario = obsImputacion;
                                oConsumoIAP.t337_esfuerzo = (float)nHoras;
                                oConsumoIAP.t337_esfuerzoenjor = nJornadas;
                                oConsumoIAP.t337_fecha = dDiaAux;
                                oConsumoIAP.t337_fecmodif = DateTime.Now;

                                bConsumoIAP.Insert(oConsumoIAP);
                                //DAL.ConsumoIAP cConsumoIAP = new DAL.ConsumoIAP(cDblib);
                                //iImputacionGrabada = cConsumoIAP.Insert(oConsumoIAP);
                                //cConsumoIAP.Insert(oConsumoIAP);
                                //if (iImputacionGrabada == 1)
                                    ControlTraspasoIAP(idUser, idUserEntrada, sNombreEmpleadoEntrada, nTarea, dDiaAux, (float)nHoras, sNombreEmpleado, aListCorreo);
                                //else
                                //{
                                //    if (!bAvisadoCLE)
                                //    {
                                //        sMsg += "<br />Se ha sobrepasado el límite de horas máximo permitido ";
                                //        sMsg += "para la tarea '" + oTarea.t332_idtarea.ToString() + " " + oTarea.t332_destarea + "'. En la fecha de imputación (" + dDiaAux.ToShortDateString() + ") ya el exceso es de " + double.Parse((oTareaIAP.nConsumidoHoras + nHoras - oTarea.t332_cle).ToString()).ToString("N") + " horas. ";
                                //        sMsg += "Para poder imputar más horas a dicha tarea, pongase en contacto con el responsable de la misma.";
                                //        bErrorControlado = true;
                                //        bAvisadoCLE = true;
                                //    }
                                //}
                                #endregion
                            }
                            #endregion
                        }
                        #endregion
                    }
                    #endregion
                }
                #endregion

                #region Actualización de estimaciones y finalización.
                if (bObligaest)
                {
                    double nHorasTotales = 0;
                    DateTime dFecMax = DateTime.Parse("01/01/1900");
                    Models.ConsumoIAP oConsMax = new Models.ConsumoIAP();

                    oConsMax = bConsumoIAP.SelectAcumulados(idUser, nTarea);
                    //DAL.ConsumoIAP cConsumoIAP = new DAL.ConsumoIAP(cDblib);
                    //oConsMax = cConsumoIAP.SelectAcumulados(idUser, nTarea);


                    nHorasTotales = oConsMax.t337_esfuerzo;
                    dFecMax = oConsMax.t337_fecha;

                    if (nHorasTotales > nETE)
                    {
                        //sRes += "<br />Se han imputado más horas de las estimadas, por lo que se ha actualizado dicha estimación.";
                        nETE = nHorasTotales; //Para actualizar la estimación.
                    }
                    if (dFecMax > dFFE)
                    {
                        //sRes += "<br />Se ha realizado alguna imputación en una fecha posterior a la estimada, por lo que se ha actualizado dicha estimación.";
                        dFFE = dFecMax; //Para actualizar la estimación.
                    }
                }
                Models.EstimacionIAP oEst = new Models.EstimacionIAP();
                oEst.t314_idusuario = idUser;
                oEst.t332_idtarea = nTarea;
                oEst.t336_ffe = dFFE;
                oEst.t336_ete = nETE;
                oEst.t336_comentario = obsTecnico;
                oEst.t336_completado = bFinalizado;

                //BLL.EstimacionIAP bEst = new BLL.EstimacionIAP();
                bEst.Update(oEst);
                //DAL.EstimacionIAP cEstimacionIAP = new DAL.EstimacionIAP(cDblib);
                //cEstimacionIAP.Update(oEst);

                //if (bFinalizado)
                //{
                //    Models.TareaRecursos oTarRec = new Models.TareaRecursos();
                //    oTarRec.t314_idusuario = idUser;
                //    oTarRec.t332_idtarea = nTarea;
                //    oTarRec.t336_completado = bFinalizado;
                //    BLL.TareaRecursos bTarRec = new BLL.TareaRecursos();
                //    //TareaRecurso.FinalizarLaborEnTarea(tr, idUser, nTarea, bFinalizado);
                //    bTarRec.SetFinalizacion(oTarRec);
                //}
                #endregion                

                if (bErrorControlado)
                {
                    throw new Exception("¡Denegado!<br />" + sMsg);
                }

                cUsuario.aListCorreo = aListCorreo;

                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                try
                {
                    if (htTareasSuperanCLE.Count > 0)
                    {
                        foreach (int idTarea in htTareasSuperanCLE.Keys)
                        {
                            aListCorreoCLE = (ArrayList)((Models.TareaCLE)htTareasSuperanCLE[idTarea]).destinatariosMail;
                            if (aListCorreoCLE.Count > 0) SUPERANTIGUO.Capa_Negocio.Correo.EnviarCorreos(aListCorreoCLE);
                        }

                    }

                }
                catch (Exception ex)
                {
                    IB.SUPER.Shared.LogError.LogearError("Error al enviar el mail de control de límite de esfuerzo", ex);
                }

                return cUsuario;

            }
            

            catch (Exception ex)
            {

                //rollback
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.rollbackTransaction(methodOwnerID);

                //throw ex;
                if (bErrorControlado)
                    sRes = sMsg;
                else
                    sRes = ex.Message;// System.Uri.EscapeDataString(ex.Message);
                throw new Exception(sRes);
            }
            finally
            {
                //bUsuario.Dispose();
                bTareaPSP.Dispose();
                bTareaIAP.Dispose();
                bCalendario.Dispose();
                bDesgloseCalendario.Dispose();
                bUsuarioPSN.Dispose();
                bConsumoIAP.Dispose();
                bEst.Dispose();
            }
        }
        private static void ControlTraspasoIAP(int idUser, int idUserEntrada, string sNomProfEntrada,
                                               int t332_idtarea, DateTime dDia, float nHoras, string sNombreProfesional,
                                               ArrayList aListCorreo)
        {
            BLL.TareaCTIAP bTarea = new BLL.TareaCTIAP();

            try 
            {
                List<Models.TareaCTIAP> lstDestinatarios = new List<Models.TareaCTIAP>();

                lstDestinatarios = bTarea.Catalogo(t332_idtarea, dDia);

                //drT = TAREAPSP.flContolTraspasoIAP(tr, nTarea, dDiaAux);
                foreach (Models.TareaCTIAP oDest in lstDestinatarios)
                {
                    GenerarCorreoTraspasoIAP(idUser, idUserEntrada, sNomProfEntrada,
                                             sNombreProfesional, oDest.MAIL,
                                             oDest.t301_idproyecto.ToString("#,###") + " " + oDest.t301_denominacion,
                                             oDest.t331_despt, oDest.t334_desfase, oDest.t335_desactividad,
                                             t332_idtarea.ToString("#,###") + " " + oDest.t332_destarea,
                                             dDia.ToString(), nHoras.ToString("N"),
                                             aListCorreo);
                }
            }
            catch (Exception ex) 
            {
                IB.SUPER.Shared.LogError.LogearError("Error en control traspaso IAP", ex);
                throw ex;
            }
            finally 
            {
                bTarea.Dispose();
            }            
        }
        private static void GenerarCorreoTraspasoIAP(int idUser, int idUserEntrada, string sNomProfEntrada,
                                                     string sProfesional, string sTO, string sProy, string sProyTec, string sFase, string sActiv,
                                                     string sTarea, string sFecha, string sConsumo,
                                                     ArrayList aListCorreo)
        {
            string sAsunto = "", sTexto = "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            try
            {
                sAsunto = "Imputación en IAP a tarea con el traspaso de dedicaciones al módulo económico ya realizado.";

                sb.Append("<BR>SUPER te informa de que se ha producido una imputación de consumo a tarea en IAP estando el traspaso de dedicaciones al módulo económico realizado.");
                if (idUserEntrada != idUser)
                    sb.Append("<BR>La imputación ha sido realizada por " + sNomProfEntrada + "<BR><BR>");
                sb.Append("<label style='width:120px'>Profesional: </label><b>" + sProfesional + "</b><br>");
                sb.Append("<label style='width:120px'>Proyecto económico: </label><b>" + sProy + "</b><br>");
                sb.Append("<label style='width:120px'>Proyecto Técnico: </label>" + sProyTec + "<br>");
                if (sFase != "") sb.Append("<label style='width:120px'>Fase: </label>" + sFase + "<br>");
                if (sActiv != "") sb.Append("<label style='width:120px'>Actividad: </label>" + sActiv + "<br>");
                sb.Append("<label style='width:120px'>Tarea: </label>" + sTarea + "<br>");
                sb.Append("<label style='width:120px'>Fecha: </label>" + sFecha.Substring(0, 10) + "<br>");
                sb.Append("<label style='width:120px'>Dedicación: </label>" + sConsumo + "<br><br>");
                sTexto = sb.ToString();

                string[] aMail = { sAsunto, sTexto, sTO };
                aListCorreo.Add(aMail);

            }
            catch (Exception ex)
            {
                //sResul = "Error@#@" + Errores.mostrarError("Error al enviar correo de imputación IAP a tarea con traspaso IAP ya realizado.", ex);
                IB.SUPER.Shared.LogError.LogearError("Error al enviar correo de imputación IAP a tarea con traspaso IAP ya realizado.\n" + sTexto, ex);
            }
        }

        public int grabarConsumos(int nUsuario, int nUserEntrada, string sNombreEmpleado, string sNombreEmpleadoEntrada, int nUMC_IAP, List<Models.ConsumoIAP> consumos)
        {

            int result = 0;
            System.Text.StringBuilder sbEx = new System.Text.StringBuilder();
            ArrayList aListCorreo = new ArrayList();
            ArrayList aListCorreoCLE = new ArrayList();

            Hashtable htTareasSuperanCLE = new Hashtable();            

            Guid methodOwnerID = new Guid("beb02bf0-bbf9-4908-b2e0-704b269b5603");            

            OpenDbConn();            

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            BLL.TareaPSP TareaPSPBLL = new BLL.TareaPSP(cDblib);
            BLL.ConsumoIAP ConsumoIAPBLL = new BLL.ConsumoIAP(cDblib);
            BLL.EstimacionIAP EstimacionIAPBLL = new BLL.EstimacionIAP(cDblib);
            //DAL.ConsumoIAP cConsumoIAP;
            //DAL.EstimacionIAP cEstimacionIAP;

            try
            {
                Models.ConsumoIAP oConsumoIAP = new Models.ConsumoIAP();
                Models.EstimacionIAP oEstimacionIAP = new Models.EstimacionIAP();

                foreach (Models.ConsumoIAP consumo in consumos)
                {

                    //Se comprueba que el tipo de consumo es correcto
                    #region Control tipo consumo
                    if (consumo.tipo != "T" && consumo.tipo != "E")
                    {
                        sbEx.Append("<br /> Tipo de consumo no válido.");
                    }
                    #endregion

                    //Se comprueba que no se haya cerrado el mes
                    #region Control mes cerrado IAP                    
                    if (consumo.tipo == "T" && IB.SUPER.Shared.Fechas.FechaAAnnomes(consumo.t337_fecha) <= nUMC_IAP)
                    {
                        sbEx.Append("<br />Operación denegada. La fecha de imputación (" + consumo.t337_fecha.ToShortDateString() + ") pertenece a un mes IAP cerrado. Último mes cerrado IAP (" + IB.SUPER.Shared.Fechas.AnnomesAFechaDescLarga(nUMC_IAP) + ").");
                    }
                    #endregion

                    //Se comprueba que no se impute 0 horas cuando sea Insert o Update
                    #region Control horas/jornadas imputación
                    if (consumo.tipo == "T")
                    {
                        if (consumo.accion != "D")
                        {
                            if (consumo.t337_esfuerzo == 0)
                            {
                                sbEx.Append("<br />Operación denegada. No se permite imputar cero horas.");
                            }

                            if (consumo.t337_esfuerzoenjor == 0)
                            {
                                sbEx.Append("<br />Operación denegada. No se permite imputar cero jornadas .");
                            }
                        }   
   
                        if (consumo.accion == "I")
                        {
                            //Control de límite de esfuerzo
                            sbEx.Append(TareaPSPBLL.ControlLimiteEsfuerzos(consumo.t332_idtarea, consumo.t337_esfuerzo, consumo.t337_fecha, htTareasSuperanCLE));
                        }

                        if (consumo.accion == "U")
                        {
                            oConsumoIAP = null;
                            //Control de límite de esfuerzo(si es un update se controla si la diferencia supera el límite de esfuerzo)
                            oConsumoIAP = ConsumoIAPBLL.Select(consumo.t332_idtarea, nUsuario, consumo.t337_fecha);
                            
                            double nHoras = consumo.t337_esfuerzo - oConsumoIAP.t337_esfuerzo;

                            sbEx.Append(TareaPSPBLL.ControlLimiteEsfuerzos(consumo.t332_idtarea, nHoras, consumo.t337_fecha, htTareasSuperanCLE));
                        }
                    }
                    #endregion

                    //Se acumulan los posibles errores para mostrarlos todos de una vez
                    if (sbEx.ToString() != "")
                    {
                        //throw (new Exception(sbEx.ToString()));
                        throw new ValidationException(sbEx.ToString());
                    }

                    //Acciones posibles. Las estimaciones(E) solo pueden ser U
                    #region Acciones
                    switch (consumo.accion)
                    {
                        case "I":     
                            oConsumoIAP = new Models.ConsumoIAP();

                            oConsumoIAP.t332_idtarea = consumo.t332_idtarea;
                            oConsumoIAP.t314_idusuario = nUsuario;
                            oConsumoIAP.t337_fecha = consumo.t337_fecha;
                            oConsumoIAP.t337_esfuerzo = consumo.t337_esfuerzo;
                            oConsumoIAP.t337_esfuerzoenjor = consumo.t337_esfuerzoenjor;
                            oConsumoIAP.t337_comentario = consumo.t337_comentario;
                            oConsumoIAP.t337_fecmodif = DateTime.Now;
                            oConsumoIAP.t314_idusuario_modif = nUserEntrada;

                            ConsumoIAPBLL.Insert(oConsumoIAP);
                            //cConsumoIAP = new DAL.ConsumoIAP(cDblib);
                            //result = cConsumoIAP.Insert(oConsumoIAP);

                            ControlTraspasoIAP(nUsuario, nUserEntrada, sNombreEmpleadoEntrada, consumo.t332_idtarea, consumo.t337_fecha, consumo.t337_esfuerzo, sNombreEmpleado, aListCorreo);                            
                            break;
                        case "U":
                            if (consumo.tipo == "T")
                            {
                                oConsumoIAP = new Models.ConsumoIAP();

                                oConsumoIAP.t332_idtarea = consumo.t332_idtarea;
                                oConsumoIAP.t314_idusuario = nUsuario;
                                oConsumoIAP.t337_fecha = consumo.t337_fecha;
                                oConsumoIAP.t337_esfuerzo = consumo.t337_esfuerzo;
                                oConsumoIAP.t337_esfuerzoenjor = consumo.t337_esfuerzoenjor;
                                oConsumoIAP.t337_comentario = consumo.t337_comentario;
                                oConsumoIAP.t337_fecmodif = DateTime.Now;
                                oConsumoIAP.t314_idusuario_modif = nUserEntrada;

                                result = ConsumoIAPBLL.Update(oConsumoIAP);
                                //cConsumoIAP = new DAL.ConsumoIAP(cDblib);
                                //result = cConsumoIAP.Update(oConsumoIAP);

                                ControlTraspasoIAP(nUsuario, nUserEntrada, sNombreEmpleadoEntrada, consumo.t332_idtarea, consumo.t337_fecha, consumo.t337_esfuerzo, sNombreEmpleado, aListCorreo);                            
                            } else {

                                oEstimacionIAP = new Models.EstimacionIAP();

                                oEstimacionIAP.t314_idusuario = nUsuario;
                                oEstimacionIAP.t332_idtarea = consumo.t332_idtarea;
                                oEstimacionIAP.t336_ffe = consumo.ffe;
                                oEstimacionIAP.t336_ete = consumo.ete;
                                oEstimacionIAP.t336_comentario = consumo.t337_comentario;
                                oEstimacionIAP.t336_completado = consumo.fin;

                                result = EstimacionIAPBLL.Update(oEstimacionIAP);
                                //cEstimacionIAP = new DAL.EstimacionIAP(cDblib);
                                //result = cEstimacionIAP.Update(oEstimacionIAP);

                            }
                            break;
                        case "D":
                            result = ConsumoIAPBLL.Delete(consumo.t332_idtarea, nUsuario, consumo.t337_fecha);
                            //cConsumoIAP = new DAL.ConsumoIAP(cDblib);
                            //result = cConsumoIAP.Delete(consumo.t332_idtarea, nUsuario, consumo.t337_fecha);
                            break;
                        default:
                            throw (new Exception("<br />Acción no permitida sobre el consumo o la estimación."));
                    }
                    #endregion

                }

                try
                {
                    if (htTareasSuperanCLE.Count > 0)
                    {
                        foreach (int idTarea in htTareasSuperanCLE.Keys)
                        {
                            aListCorreoCLE = (ArrayList)((Models.TareaCLE)htTareasSuperanCLE[idTarea]).destinatariosMail;
                            if (aListCorreoCLE.Count > 0) SUPERANTIGUO.Capa_Negocio.Correo.EnviarCorreos(aListCorreoCLE);
                        }
                           
                    }
                    
                }
                catch (Exception ex)
                {
                    IB.SUPER.Shared.LogError.LogearError("Error al enviar el mail de control de límite de esfuerzo", ex);
                }

                try
                {
                    if (aListCorreo.Count > 0)
                        SUPERANTIGUO.Capa_Negocio.Correo.EnviarCorreos(aListCorreo);                        
                }
                catch (Exception ex)
                {                 
                    IB.SUPER.Shared.LogError.LogearError("Error al enviar el mail a los responsables del proyecto", ex);
                }

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return result;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }
            finally
            {
                TareaPSPBLL.Dispose();
                ConsumoIAPBLL.Dispose();
                EstimacionIAPBLL.Dispose();

            }
                                   
        }        

        #endregion

        #region Conexion base de datos y dispose
        private void OpenDbConn()
        {
            if (cDblib == null)
                cDblib = new IB.sqldblib.SqlServerSP(Shared.Database.GetConStr(), classOwnerID);
        }
        private void AttachDbConn(sqldblib.SqlServerSP extcDblib)
        {
            cDblib = extcDblib;
        }
        private void Dispose(bool disposing)
        {
            if (!this.disposed && disposing) if (cDblib != null && cDblib.OwnerID.Equals(classOwnerID)) cDblib.Dispose();
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~Usuario()
        {
            Dispose(false);
        }

        #endregion


    }

}
