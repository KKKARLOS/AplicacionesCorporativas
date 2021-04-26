using IB.SUPER.Shared;
using SUPER.Capa_Negocio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;

/// <summary>
/// Summary description for PlanifAgendaCat
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class PlanifAgendaCat : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("80561336-2931-4bc5-9396-583045e72de8");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public PlanifAgendaCat()
			: base()
        {
			//OpenDbConn();
        }
		
		public PlanifAgendaCat(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas	

        public List<Models.PlanifAgendaCat> CatalogoEventos(Int32 idficepi, DateTime fechaInicio, DateTime fechaFin)
        {
            OpenDbConn();

            DAL.PlanifAgendaCat cPlanifAgendaCat = new DAL.PlanifAgendaCat(cDblib);
            return cPlanifAgendaCat.Catalogo(idficepi, fechaInicio, fechaFin);

        }

        public Models.PlanifAgendaCat getDetalleEvento(Int32 idEvento)
        {
            OpenDbConn();

            DAL.PlanifAgendaCat cPlanifAgendaCat = new DAL.PlanifAgendaCat(cDblib);
            return cPlanifAgendaCat.Select(idEvento);

        }

        public string grabarEventoProfAsignados(Models.PlanifAgendaCat evento, string[] otrosProfesionales, bool confirmarBorrado)
        {
            int idEvento = 0;
            string result = "";
            string sOtrosProfNoCita = "";

            ArrayList aListCorreo = new ArrayList();

            bool bDias = false;
            bool[] bDia = new bool[7];
            
            bool bConTransaccion = false;
            Guid methodOwnerID = new Guid("C9762B5E-8973-4DFC-81A6-C233F3503749");

            OpenDbConn();
            if (cDblib.Transaction.ownerID.Equals(new Guid()))
                bConTransaccion = true;
            if (bConTransaccion)
                cDblib.beginTransaction(methodOwnerID);

            try
                {
                    int idEventoProf = 0;
                    ArrayList profesionalesNoCita = new ArrayList();
                    int idFicepiProf = 0;
                    string codRedProf = "";
                    Models.ValidarTareaAgenda accesoATarea = new Models.ValidarTareaAgenda();
                    DAL.PlanifAgendaCat planifAgendaCatDAL = new DAL.PlanifAgendaCat(cDblib);
                    DAL.PromotoresAgendaCat promotoresAgendaCatDAL = new DAL.PromotoresAgendaCat(cDblib);
                    DAL.ValidarTareaAgenda validarTareaAgendaDAL = new DAL.ValidarTareaAgenda(cDblib);
    
       
                    Models.PlanifAgendaCat newEvento;
                    foreach (string oProf in otrosProfesionales)
                    {
                        string[] aProf = Regex.Split(oProf, "//");
                        idFicepiProf = int.Parse(aProf[0]);
                        codRedProf = aProf[1];
                        //si la cita es a tarea, comprobar que el profesional tiene acceso a la tarea.
                        if (evento.IdTarea != null)
                        {
                            accesoATarea = validarTareaAgendaDAL.Select(idFicepiProf, (int)evento.IdTarea);
                            if (accesoATarea == null)
                            {
                                profesionalesNoCita.Add(idFicepiProf);
                                if (sOtrosProfNoCita == "") sOtrosProfNoCita = idFicepiProf.ToString();
                                else sOtrosProfNoCita += "," + idFicepiProf.ToString();
                                continue;
                            }
                        }

                        #region Control días de la semana

                        bDia = evento.DiasSemana;
                        if (Array.Exists(bDia, element => element == true))
                        {
                            bDias = true;
                        }
                        #endregion

                        if (bDias)
                        {
                            #region Una reserva cada día para el rango horario indicado
                            newEvento = new Models.PlanifAgendaCat();
                            newEvento = duplicarEvento(evento);
                            newEvento.Privado = ""; //El campo privado que ha escrito el interesado no se inserta a los otros profesionales.
                            newEvento.Idficepi = idFicepiProf; //Se asigna el idficepi del profesional que se está tratando

                            int nDiff = IB.SUPER.Shared.Fechas.DateDiff("day", evento.StartTime, evento.EndTime);
                            for (int b = 0; b <= nDiff; b++)
                            {
                                //comprobar que el día a grabar está entre los días seleccionados
                                System.DateTime dAux = evento.StartTime.AddDays(b);
                                switch (dAux.DayOfWeek)
                                {
                                    case System.DayOfWeek.Monday: if (bDia[0] == false) continue; break;
                                    case System.DayOfWeek.Tuesday: if (bDia[1] == false) continue; break;
                                    case System.DayOfWeek.Wednesday: if (bDia[2] == false) continue; break;
                                    case System.DayOfWeek.Thursday: if (bDia[3] == false) continue; break;
                                    case System.DayOfWeek.Friday: if (bDia[4] == false) continue; break;
                                    case System.DayOfWeek.Saturday: if (bDia[5] == false) continue; break;
                                    case System.DayOfWeek.Sunday: if (bDia[6] == false) continue; break;
                                }

                                //Si llega aquí es que hay que grabar los datos de la reserva para ese día.
                                DateTime dFechaHoraFin = IB.SUPER.Shared.Fechas.crearDateTime(dAux.ToShortDateString(), evento.EndTime.ToShortTimeString());

                                //Antes de realizar la reserva, comprobar la disponibilidad;
                                if (planifAgendaCatDAL.getDisponibilidad(newEvento.Idficepi, dAux, dFechaHoraFin, 0))
                                {
                                    //En caso de tener disponibilidad se inserta el evento en BBDD
                                    // y si hay necesidad de enviar correo, se crea el correo y se añade a la lista de correos
                                    newEvento.StartTime = dAux;
                                    newEvento.EndTime = dFechaHoraFin;
                                    idEventoProf = planifAgendaCatDAL.Insert(newEvento);
                                    newEvento.ID = idEventoProf;
                                    aListCorreo.Add(crearCorreo(newEvento, codRedProf));
                                }
                                else
                                {                     
                                    profesionalesNoCita.Add(idFicepiProf);
                                    if (sOtrosProfNoCita == "") sOtrosProfNoCita = newEvento.Idficepi.ToString();
                                    else sOtrosProfNoCita += "," + newEvento.Idficepi.ToString();
                                    break;                                                                         
                                }

                            #endregion
                            }//Fin de bucle de días
                            
                        }
                        else
                        {
                            #region Una sola reserva para el rango desde la fecha de inicio a la de fin
                            //Antes de realizar la reserva, comprobar la disponibilidad;
                            newEvento = new Models.PlanifAgendaCat();
                            newEvento = duplicarEvento(evento);
                            newEvento.Privado = ""; //El campo privado que ha escrito el interesado no se inserta a los otros profesionales.
                            newEvento.Idficepi = idFicepiProf; //Se asigna el idficepi del profesional que se está tratando

                            if (planifAgendaCatDAL.getDisponibilidad(newEvento.Idficepi, evento.StartTime, evento.EndTime, 0))
                            {
                                //En caso de tener disponibilidad se inserta el evento en BBDD
                                // y si hay necesidad de enviar correo, se crea el correo y se añade a la lista de correos
                                idEventoProf = planifAgendaCatDAL.Insert(newEvento);
                                newEvento.ID = idEventoProf;
                                aListCorreo.Add(crearCorreo(newEvento, codRedProf));
                                
                            }
                            else
                            {
                                profesionalesNoCita.Add(idFicepiProf);
                                if (sOtrosProfNoCita == "") sOtrosProfNoCita = newEvento.Idficepi.ToString();
                                else sOtrosProfNoCita += "," + newEvento.Idficepi.ToString();      
                            }
                            
                            #endregion
                        }//Fin de If (bDias)

                    }//Fin de bucle de profesionales



                    if (sOtrosProfNoCita != "")
                    {
                        throw new ValidationException("");
                    }

                    if (bConTransaccion) cDblib.commitTransaction(methodOwnerID);
                }

                catch (ValidationException ex)
                {
                    //rollback
                    if (cDblib.Transaction.ownerID.Equals(new Guid()))
                        cDblib.rollbackTransaction(methodOwnerID);

                    if (sOtrosProfNoCita != "") return idEvento + "//" + sOtrosProfNoCita;
                    throw ex;

                }

                catch (Exception ex)
                {
                    //rollback
                    if (cDblib.Transaction.ownerID.Equals(new Guid()))
                        cDblib.rollbackTransaction(methodOwnerID);

                    throw new Exception("La planificación para el profesional ha sido realizada con éxito. Sin embargo, se ha producido un error en la programación de las citas de los profesionales asignados: " +  ex.Message);
            
                }  
        
                finally
                {
                    
                }

                try
                {
                    if (aListCorreo.Count > 0)
                       Correo.EnviarCorreosCita(aListCorreo);
                }
                catch (Exception ex)
                {
                    //sResul = "Error@#@" + Errores.mostrarError("Error al enviar el mail a los responsables del proyecto", ex);
                    IB.SUPER.Shared.LogError.LogearError("Error al enviar los mails de convocatoria:", ex);
                }

                return "OK";

        }


        public int grabarEvento(Models.PlanifAgendaCat evento, bool confirmarBorrado)
        {

            bool bEnviarEmail = false;
            int idEvento = 0;
            bool errorControlado = false;

            ArrayList aListCorreo = new ArrayList();

            bool bDias = false;
            bool[] bDia = new bool[7];

            Models.PlanifAgendaCat oEvento;
            
            bool bConTransaccion = false;
            Guid methodOwnerID = new Guid("F35A931C-50C1-4FAA-B3AB-3819DE6E0C79");

            OpenDbConn();
            if (cDblib.Transaction.ownerID.Equals(new Guid()))
                bConTransaccion = true;
            if (bConTransaccion)
                cDblib.beginTransaction(methodOwnerID);

            try

            {
                DAL.PlanifAgendaCat planifAgendaCatDAL = new DAL.PlanifAgendaCat(cDblib);
                DAL.PromotoresAgendaCat promotoresAgendaCatDAL = new DAL.PromotoresAgendaCat(cDblib);                        
       
                evento.Idficepi = int.Parse(HttpContext.Current.Session["IDFICEPI_IAP"].ToString());
                evento.IdficepiMod = int.Parse(HttpContext.Current.Session["IDFICEPI_IAP"].ToString());            

                //En caso de que el evento ya exista, hay que comprobar los datos que han sido modificados para
                //ver si es necesario enviar mail
                if (evento.ID != 0)
                {
                    #region Update del evento
                    oEvento = planifAgendaCatDAL.Select(evento.ID);

                    //comprobación de si hay que comunicar el cambio
                    if (evento.StartTime != oEvento.StartTime
                        || evento.EndTime != oEvento.EndTime
                        || (evento.IdTarea != -1 && evento.IdTarea != oEvento.IdTarea))
                    {
                        bEnviarEmail = true;
                    }

                    //Se comprueba si el evento tiene dsponibilidad o no
                    if (planifAgendaCatDAL.getDisponibilidad(evento.Idficepi, evento.StartTime, evento.EndTime, evento.ID))
                    {
                        //En caso de tener disponibilidad se actualiza el evento en BBDD
                        // y si hay necesidad de enviar correo, se crea el correo y se añade a la lista de correos
                        if (bEnviarEmail) evento.IdficepiMod = int.Parse(HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString());
                        idEvento = planifAgendaCatDAL.Update(evento);
                        if (bEnviarEmail) aListCorreo.Add(crearCorreoMod(evento, oEvento, null));
                    }
                    //Si no tiene disponibilidad se manda un mensaje de error
                    else
                    {
                        throw new ValidationException("Cita denegada por solapamiento. Revisa el mapa de citas.");
                    }

                    #endregion
                }
                //El evento no existe, se debe hacer un Insert en BBDD
                else
                {
                    #region insert del evento

                    #region Control días de la semana

                    bDia = evento.DiasSemana;
                    if (Array.Exists(bDia, element => element == true))
                    {
                        bDias = true;
                    }
                    #endregion
                    if (bDias)
                    {

                        #region Una reserva cada día para el rango horario indicado
                        Models.PlanifAgendaCat newEvento = new Models.PlanifAgendaCat();
                        newEvento = duplicarEvento(evento);

                        int nDiff = IB.SUPER.Shared.Fechas.DateDiff("day", evento.StartTime, evento.EndTime);
                        for (int b = 0; b <= nDiff; b++)
                        {
                            //comprobar que el día a grabar está entre los días seleccionados
                            System.DateTime dAux = evento.StartTime.AddDays(b);
                            switch (dAux.DayOfWeek)
                            {
                                case System.DayOfWeek.Monday: if (bDia[0] == false) continue; break;
                                case System.DayOfWeek.Tuesday: if (bDia[1] == false) continue; break;
                                case System.DayOfWeek.Wednesday: if (bDia[2] == false) continue; break;
                                case System.DayOfWeek.Thursday: if (bDia[3] == false) continue; break;
                                case System.DayOfWeek.Friday: if (bDia[4] == false) continue; break;
                                case System.DayOfWeek.Saturday: if (bDia[5] == false) continue; break;
                                case System.DayOfWeek.Sunday: if (bDia[6] == false) continue; break;
                            }

                            //Si llega aquí es que hay que grabar los datos de la reserva para ese día.
                            DateTime dFechaHoraFin = IB.SUPER.Shared.Fechas.crearDateTime(dAux.ToShortDateString(), evento.EndTime.ToShortTimeString());

                            //Antes de realizar la reserva, comprobar la disponibilidad;
                            if (planifAgendaCatDAL.getDisponibilidad(evento.Idficepi, dAux, dFechaHoraFin, 0))
                            {
                                //En caso de tener disponibilidad se inserta el evento en BBDD
                                // y si hay necesidad de enviar correo, se crea el correo y se añade a la lista de correos
                                newEvento.StartTime = dAux;
                                newEvento.EndTime = dFechaHoraFin;
                                newEvento.IdficepiMod = int.Parse(HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString());
                                idEvento = planifAgendaCatDAL.Insert(newEvento);
                                newEvento.ID = idEvento;
                                aListCorreo.Add(crearCorreo(newEvento, null));
                            }
                            else
                            {

                                if (evento.Idficepi != (int)HttpContext.Current.Session["IDFICEPI_ENTRADA"])
                                {
                                    throw new Exception("Cita denegada por solapamiento. Revisa el mapa de citas.");
                                }

                                if (!confirmarBorrado)
                                {
                                    Models.PromotoresAgendaCat oPromotoresAgendaCat = new Models.PromotoresAgendaCat();
                                    List<Models.PromotoresAgendaCat> listaPromotoresEvento = new List<Models.PromotoresAgendaCat>();
                                    oPromotoresAgendaCat.t458_idPlanif = 0;
                                    oPromotoresAgendaCat.t458_fechoraini = dAux;
                                    oPromotoresAgendaCat.t458_fechorafin = dFechaHoraFin;
                                    oPromotoresAgendaCat.t001_idficepi = evento.Idficepi;
                                    listaPromotoresEvento = promotoresAgendaCatDAL.Catalogo(oPromotoresAgendaCat);

                                    foreach (Models.PromotoresAgendaCat oProm in listaPromotoresEvento)
                                    {
                                        if (oProm.t001_idficepi_mod == evento.Idficepi)
                                        {
                                            throw new Exception("Cita denegada por solapamiento. Revisa el mapa de citas.");
                                        }
                                    }
                                    errorControlado = true;
                                    throw new Exception("");
                                }
                                else
                                {
                                    //Hay que borrar las citas solapadas.
                                    Models.PromotoresAgendaCat oPromotoresAgendaCat = new Models.PromotoresAgendaCat();
                                    List<Models.PromotoresAgendaCat> listaPromotoresEvento = new List<Models.PromotoresAgendaCat>();
                                    oPromotoresAgendaCat.t458_idPlanif = 0;
                                    oPromotoresAgendaCat.t458_fechoraini = dAux;
                                    oPromotoresAgendaCat.t458_fechorafin = dFechaHoraFin;
                                    oPromotoresAgendaCat.t001_idficepi = evento.Idficepi;
                                    listaPromotoresEvento = promotoresAgendaCatDAL.Catalogo(oPromotoresAgendaCat);

                                    Models.PlanifAgendaCat eventoEliminar;

                                    foreach (Models.PromotoresAgendaCat oProm in listaPromotoresEvento)
                                    {
                                        eventoEliminar = new Models.PlanifAgendaCat();
                                        eventoEliminar.ID = oProm.t458_idPlanif;
                                        eventoEliminar.IdficepiMod = oProm.t001_idficepi_mod;
                                        eventoEliminar.StartTime = oProm.t458_fechoraini;
                                        eventoEliminar.EndTime = oProm.t458_fechorafin;
                                        eventoEliminar.Motivo = oProm.Motivo;
                                        eventoEliminar.Profesional = oProm.Profesional;
                                        eventoEliminar.CodRedPromotor = oProm.t001_codred_promotor;
                                        planifAgendaCatDAL.Delete(eventoEliminar.ID);
                                        aListCorreo.Add(crearCorreoEliminacionEventoPorSolapamiento(eventoEliminar));

                                    }
                                    //Una vez eliminados los eventos que generan los solapamientos se procede a insertar el evento
                                    newEvento.StartTime = dAux;
                                    newEvento.EndTime = dFechaHoraFin;
                                    newEvento.IdficepiMod = int.Parse(HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString());
                                    newEvento.ID = planifAgendaCatDAL.Insert(newEvento);
                                    aListCorreo.Add(crearCorreo(newEvento, null));
                                }

                            }
                        }
                        #endregion

                        if (idEvento == 0)
                        {
                            throw new ValidationException("Las fechas y/o los días de repetición seleccionados no son correctos");
                        }
                    }
                    

                    else
                    {
                        #region Una sola reserva para el rango desde la fecha de inicio a la de fin
                        //Antes de realizar la reserva, comprobar la disponibilidad;
                        if (planifAgendaCatDAL.getDisponibilidad(evento.Idficepi, evento.StartTime, evento.EndTime, 0))
                        {
                            //En caso de tener disponibilidad se inserta el evento en BBDD
                            // y si hay necesidad de enviar correo, se crea el correo y se añade a la lista de correos
                            evento.IdficepiMod = int.Parse(HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString());
                            idEvento = planifAgendaCatDAL.Insert(evento);
                            evento.ID = idEvento;
                            aListCorreo.Add(crearCorreo(evento, null));
                        }
                        else
                        {
                            if (evento.Idficepi != (int)HttpContext.Current.Session["IDFICEPI_ENTRADA"])
                            {
                                throw new Exception("Cita denegada por solapamiento. Revisa el mapa de citas.");
                            }

                            if (!confirmarBorrado)
                            {
                                Models.PromotoresAgendaCat oPromotoresAgendaCat = new Models.PromotoresAgendaCat();
                                List<Models.PromotoresAgendaCat> listaPromotoresEvento = new List<Models.PromotoresAgendaCat>();
                                oPromotoresAgendaCat.t458_idPlanif = 0;
                                oPromotoresAgendaCat.t458_fechoraini = evento.StartTime;
                                oPromotoresAgendaCat.t458_fechorafin = evento.EndTime;
                                oPromotoresAgendaCat.t001_idficepi = evento.Idficepi;
                                listaPromotoresEvento = promotoresAgendaCatDAL.Catalogo(oPromotoresAgendaCat);

                                foreach (Models.PromotoresAgendaCat oProm in listaPromotoresEvento)
                                {
                                    if (oProm.t001_idficepi_mod == evento.Idficepi)
                                    {
                                        break;
                                    }
                                }
                                errorControlado = true;
                                throw new Exception("");
                            }
                            //En caso de que el usuario haya decidido eliminar las citas que solapan la cita actual, se procede al borrado
                            else
                            {
                                //Hay que borrar las citas solapadas.
                                Models.PromotoresAgendaCat oPromotoresAgendaCat = new Models.PromotoresAgendaCat();
                                List<Models.PromotoresAgendaCat> listaPromotoresEvento = new List<Models.PromotoresAgendaCat>();
                                oPromotoresAgendaCat.t458_idPlanif = 0;
                                oPromotoresAgendaCat.t458_fechoraini = evento.StartTime;
                                oPromotoresAgendaCat.t458_fechorafin = evento.EndTime;
                                oPromotoresAgendaCat.t001_idficepi = evento.Idficepi;
                                listaPromotoresEvento = promotoresAgendaCatDAL.Catalogo(oPromotoresAgendaCat);

                                Models.PlanifAgendaCat eventoEliminar;

                                foreach (Models.PromotoresAgendaCat oProm in listaPromotoresEvento)
                                {
                                    eventoEliminar = new Models.PlanifAgendaCat();
                                    eventoEliminar.ID = oProm.t458_idPlanif;
                                    eventoEliminar.IdficepiMod = oProm.t001_idficepi_mod;
                                    eventoEliminar.StartTime = oProm.t458_fechoraini;
                                    eventoEliminar.EndTime = oProm.t458_fechorafin;
                                    eventoEliminar.Motivo = oProm.Motivo;
                                    eventoEliminar.Profesional = oProm.Profesional;
                                    eventoEliminar.CodRedPromotor = oProm.t001_codred_promotor;
                                    planifAgendaCatDAL.Delete(eventoEliminar.ID);
                                    aListCorreo.Add(crearCorreoEliminacionEventoPorSolapamiento(eventoEliminar));


                                }
                                //Una vez eliminados los eventos que generan los solapamientos se procede a insertar el evento
                                evento.IdficepiMod = int.Parse(HttpContext.Current.Session["IDFICEPI_ENTRADA"].ToString());
                                idEvento = planifAgendaCatDAL.Insert(evento);
                                evento.ID = idEvento;
                                aListCorreo.Add(crearCorreo(evento, null));
                            }

                        }
                        #endregion
                    }                    
                    #endregion                    
                }
                if (bConTransaccion) cDblib.commitTransaction(methodOwnerID);
            }

            catch (ValidationException ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(new Guid()))
                    cDblib.rollbackTransaction(methodOwnerID);

                throw ex;

            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(new Guid()))
                    cDblib.rollbackTransaction(methodOwnerID);

                if (!errorControlado) throw ex;
                return -1;

            }
            finally
            {
                
            }

            try
            {
                if (aListCorreo.Count > 0)
                    Correo.EnviarCorreosCita(aListCorreo);
            }
            catch (Exception ex)
            {
                //sResul = "Error@#@" + Errores.mostrarError("Error al enviar el mail a los responsables del proyecto", ex);
                IB.SUPER.Shared.LogError.LogearError("Error al enviar los mails de convocatoria:", ex);
            }

            return idEvento;
        }

        private static Models.PlanifAgendaCat duplicarEvento(Models.PlanifAgendaCat evento)
        {
            Models.PlanifAgendaCat nuevoEvento = new Models.PlanifAgendaCat();
            nuevoEvento.IdficepiMod = evento.IdficepiMod;
            nuevoEvento.Idficepi = evento.Idficepi;
            nuevoEvento.FechaMod = evento.FechaMod;
            nuevoEvento.StartTime = evento.StartTime;
            nuevoEvento.EndTime = evento.EndTime;
            nuevoEvento.IdTarea = evento.IdTarea;
            nuevoEvento.DesTarea = evento.DesTarea;
            nuevoEvento.Asunto = evento.Asunto;
            nuevoEvento.Motivo = evento.Motivo;
            nuevoEvento.Privado = evento.Privado;
            nuevoEvento.Observaciones = evento.Observaciones;

            return nuevoEvento;
        }


        private static string[] crearCorreoMod(Models.PlanifAgendaCat evento, Models.PlanifAgendaCat oEvento, string codRed)
        {
            #region Creación de correo
            string sMotivoCita = "";
            string sAsuntoCita = "";
            string sTexto = "";
            string sTextoInt = "";
            string sTO = "";
            string sFecIni;
            string sFecFin;
            string sFichero = "";

            var sAsuntoMail = (evento.Asunto == "") ? evento.DesTarea : evento.Asunto;
            sAsuntoCita = "(Agenda SUPER) " + Utilidades.unescape(sAsuntoMail);

            sFecIni = evento.StartTime.ToString();
            if (sFecIni.Length == 19) sFecIni = sFecIni.Substring(0, 16);
            else sFecIni = sFecIni.Substring(0, 15);
            sFecFin = evento.EndTime.ToString();
            if (sFecFin.Length == 19) sFecFin = sFecFin.Substring(0, 16);
            else sFecFin = sFecFin.Substring(0, 15);

            sTexto = @"La cita: <br><br>
				<b><span style='width:40px'>Inicio:</span></b> " + oEvento.StartTime + @"<br>
				<b><span style='width:40px'>Fin:</span></b> " + oEvento.EndTime + @"
				<br><br><b>Motivo de la cita:</b> " + oEvento.Motivo + @"
				<br><br><br>Ha sido modificada por " + HttpContext.Current.Session["DES_EMPLEADO_ENTRADA"].ToString() + @".<br><br>
				La nueva cita es: <br><br>
				<b><span style='width:40px'>Inicio:</span></b> " + sFecIni + @"<br>
				<b><span style='width:40px'>Fin:</span></b> " + sFecFin + @"
				<br><br><b>Motivo de la cita:</b> " + Utilidades.unescape(evento.Motivo).Replace(((char)10).ToString(), "<br>") + @"<br><br><br>";

            sTextoInt = sTexto + @"<span style='color:blue'>Si deseas registrar la reserva en tu calendario Outlook, abre el fichero adjunto (extensión .ics) y a continuación pulsa el botón 'Guardar y cerrar'. En el caso de que ya tuvieras alguna anotación registrada en tu agenda, motivada por alguna notificación anterior de esta misma reserva, deberás proceder a su modificación o eliminación de forma manual.
				<br>(Opción sólo válida para accesos desde las oficinas conectadas a la red interna de Ibermática)</span>";

            sMotivoCita = "Motivo de la cita:=0D=0A" + evento.Motivo;
            if (codRed != null) sTO = codRed.ToString();
            else sTO = HttpContext.Current.Session["IDRED_IAP"].ToString();
            sFichero = "";
            try
            {
                sFichero = crearCitaOutlook(evento.ID, sAsuntoCita, sMotivoCita, evento.StartTime, evento.EndTime);
            }
            catch { }
            string[] aMail = { sAsuntoCita, sTextoInt, sTO, sFichero };

            return aMail;

            #endregion
        }

        private static string[] crearCorreo(Models.PlanifAgendaCat evento, string codRed)
        {
            #region Creación de correo
            string sMotivoCita = "";
            string sAsuntoCita = "";
            string sTexto = "";
            string sTextoInt = "";
            string sTO = "";
            string sFecIni;
            string sFecFin;
            string sFichero = "";

            var sAsuntoMail = (evento.Asunto == "") ? evento.DesTarea : evento.Asunto;
            sAsuntoCita = "(Agenda SUPER) " + Utilidades.unescape(sAsuntoMail);

            sFecIni = evento.StartTime.ToString();
            if (sFecIni.Length == 19) sFecIni = sFecIni.Substring(0, 16);
            else sFecIni = sFecIni.Substring(0, 15);
            sFecFin = evento.EndTime.ToString();
            if (sFecFin.Length == 19) sFecFin = sFecFin.Substring(0, 16);
            else sFecFin = sFecFin.Substring(0, 15);

            sTexto = @"Cita: <br><br>
                                <b>Promotor:</b> " + HttpContext.Current.Session["DES_EMPLEADO_ENTRADA"].ToString() + @"<br>
								<b><span style='width:40px'>Inicio:</span></b> " + sFecIni + @"<br>
								<b><span style='width:40px'>Fin:</span></b> " + sFecFin + @"
								<br><br><b>Motivo de la cita:</b> " + Utilidades.unescape(evento.Motivo).Replace(((char)10).ToString(), "<br>") + @"<br><br><br><br>";

            sTextoInt = sTexto + @"<span style='color:blue'>Si deseas registrar la reserva en tu calendario Outlook, abre el fichero adjunto (extensión .ics) y a continuación pulsa el botón 'Guardar y cerrar'.
								<br>(Opción sólo válida para accesos desde las oficinas conectadas a la red interna de Ibermática)</span>";

            sMotivoCita = "Motivo de la cita:=0D=0A" + evento.Motivo;
            sFichero = "";
            if (codRed != null) sTO = codRed.ToString();
            else sTO = HttpContext.Current.Session["IDRED_IAP"].ToString();
            try
            {
                sFichero = crearCitaOutlook(evento.ID, sAsuntoCita, sMotivoCita, evento.StartTime, evento.EndTime);
            }
            catch { }
            string[] aMail = { sAsuntoCita, sTextoInt, sTO, sFichero };

            return aMail;

            #endregion
        }

        private static string[] crearCorreoEliminacionEvento(Models.PlanifAgendaCat evento, string codRed)
        {
            #region Creación de correo
            string sAsuntoCita = "";
            string sTexto = "";
            string sTextoInt = "";
            string sTO = "";
            string sFecIni;
            string sFecFin;
            string sFichero = "";

            var sAsuntoMail = (evento.Asunto == "") ? evento.DesTarea : evento.Asunto;
            sAsuntoCita = "(Agenda SUPER) " + Utilidades.unescape(sAsuntoMail);

            sFecIni = evento.StartTime.ToString();
            if (sFecIni.Length == 19) sFecIni = sFecIni.Substring(0, 16);
            else sFecIni = sFecIni.Substring(0, 15);
            sFecFin = evento.EndTime.ToString();
            if (sFecFin.Length == 19) sFecFin = sFecFin.Substring(0, 16);
            else sFecFin = sFecFin.Substring(0, 15);

            sTexto = @"La cita para: <br><br>
                        <b>Profesional:</b> " + HttpContext.Current.Session["DES_EMPLEADO_IAP"].ToString() + @"<br>
						<b><span style='width:40px'>Inicio:</span></b> " + sFecIni + @" <br>
						<b><span style='width:40px'>Fin:</span></b> " + sFecFin + @"
						<br><br><b>Motivo de la cita:</b> " + Utilidades.unescape(evento.Motivo).Replace(((char)10).ToString(), "<br>") + @"<br><br>
						Ha sido eliminada por " + HttpContext.Current.Session["DES_EMPLEADO_ENTRADA"].ToString() + @".<br><br>
						<br><br><b>Motivo de eliminación:</b> " + Utilidades.unescape(evento.MotivoEliminacion).Replace(((char)10).ToString(), "<br>") + @"<br><br><br>";

            sTextoInt = sTexto + @"<span style='color:blue'>En el caso de que tuvieras alguna anotación registrada en tu agenda, motivada por alguna notificación de esta misma cita, deberás proceder a su eliminación de forma manual.</span>";
            if (codRed != null) sTO = codRed.ToString();
            else sTO = HttpContext.Current.Session["IDRED_IAP"].ToString();
            string[] aMail = { sAsuntoCita, sTextoInt, sTO, sFichero };

            return aMail;

            #endregion
        }

        private static string[] crearCorreoEliminacionEventoPorSolapamiento(Models.PlanifAgendaCat evento)
        {
            #region Creación de correo
            string sAsuntoCita = "";
            string sTexto = "";
            string sTextoInt = "";
            string sTO = "";
            string sFecIni;
            string sFecFin;
            string sFichero = "";

            var sAsuntoMail = (evento.Asunto == "") ? evento.DesTarea : evento.Asunto;
            sAsuntoCita = "(Agenda SUPER) Eliminación cita agenda.";
            sFecIni = evento.StartTime.ToString();
            if (sFecIni.Length == 19) sFecIni = sFecIni.Substring(0, 16);
            else sFecIni = sFecIni.Substring(0, 15);
            sFecFin = evento.EndTime.ToString();
            if (sFecFin.Length == 19) sFecFin = sFecFin.Substring(0, 16);
            else sFecFin = sFecFin.Substring(0, 15);

            sTexto = @"La cita para: <br><br>
                            <b>Profesional:</b> " + evento.Profesional + @"<br>
						    <b><span style='width:40px'>Inicio:</span></b> " + sFecIni + @" <br>
						    <b><span style='width:40px'>Fin:</span></b> " + sFecFin + @"
						    <br><br><b>Motivo de la cita:</b> " + (evento.Motivo)+ "<br>" + @"<br><br>
						    Ha sido eliminada debido a solapamiento de una nueva cita creada por " + HttpContext.Current.Session["DES_EMPLEADO_ENTRADA"].ToString() + @".<br><br>";

            sTextoInt = sTexto + @"<span style='color:blue'>En el caso de que tuvieras alguna anotación registrada en tu agenda, motivada por alguna notificación de esta misma cita, deberás proceder a su eliminación de forma manual.</span>";
            sTO = evento.CodRedPromotor;
            string[] aMail = { sAsuntoCita, sTextoInt, sTO, sFichero };

            return aMail;

            #endregion
        }

        private static string crearCitaOutlook(int nReserva, string sAsunto, string sMotivo, DateTime dFecHoraIni, DateTime dFecHoraFin)
        {
            string sFichero = IB.SUPER.Shared.Fechas.calendarioOutlook(System.DateTime.Now.AddHours(1), true) + ".ics";
            string sDesde = IB.SUPER.Shared.Fechas.calendarioOutlook(dFecHoraIni, false);
            string sHasta = IB.SUPER.Shared.Fechas.calendarioOutlook(dFecHoraFin, false);

            sMotivo = sMotivo.Replace(((char)10).ToString(), "=0D=0A");
            sMotivo = sMotivo.Replace(((char)13).ToString(), "");

            StreamWriter fp;
            string sPath = "";

            try
            {
                fp = File.CreateText(HttpContext.Current.Request.PhysicalApplicationPath + "Upload\\" + sFichero);
                //				fp.WriteLine(this.txtMotivo.Text);
                string sContenido = @"
BEGIN:VCALENDAR
VERSION:2.0
METHOD:PUBLISH
BEGIN:VEVENT
SUMMARY;ENCODING=QUOTED-PRINTABLE:" + sAsunto + @"
DESCRIPTION;ENCODING=QUOTED-PRINTABLE:" + sMotivo + @"
DTSTAMP:" + sDesde + @"
DTSTART:" + sDesde + @"
DTEND:" + sHasta + @"
PRIORITY:0
STATUS:CONFIRMED
UID:" + "SUPERAGENDA" + nReserva.ToString() + @"
BEGIN:VALARM
TRIGGER;VALUE=DURATION:-PT10M
ACTION:DISPLAY
DESCRIPTION:Event reminder
END:VALARM
END:VEVENT
END:VCALENDAR
					";
                fp.WriteLine(sContenido);
                fp.Close();
                sPath = HttpContext.Current.Request.PhysicalApplicationPath + "Upload\\" + sFichero;
            }
            catch (Exception err)
            {
                string s = "File Creation failed. Reason is as follows " + err.ToString();
                if (HttpContext.Current.Session["UsuarioActual"].ToString() == "2340")
                {
                    StreamWriter fp2;
                    fp2 = File.CreateText(HttpContext.Current.Request.PhysicalApplicationPath + "Upload\\Errores.txt");
                    fp2.WriteLine(s);
                    fp2.Close();
                }
            }
            return sPath;
        }



        public bool getDisponibilidad(int t001_idficepi, DateTime t458_fechoraini, DateTime t458_fechorafin, int t458_idPlanif)
        {
            OpenDbConn();

            DAL.PlanifAgendaCat cPlanifAgendaCat = new DAL.PlanifAgendaCat(cDblib);
            return cPlanifAgendaCat.getDisponibilidad(t001_idficepi, t458_fechoraini, t458_fechorafin, t458_idPlanif);

        }

        

        public int insertEvento(Models.PlanifAgendaCat evento)
        {
            OpenDbConn();

            DAL.PlanifAgendaCat cPlanifAgendaCat = new DAL.PlanifAgendaCat(cDblib);
            int result = cPlanifAgendaCat.Insert(evento);
            return result;
        }

        public int updateEvento(Models.PlanifAgendaCat evento)
        {
            OpenDbConn();

            DAL.PlanifAgendaCat cPlanifAgendaCat = new DAL.PlanifAgendaCat(cDblib);
            int result = cPlanifAgendaCat.Update(evento);
            return result;
        }

        public int eliminarEvento(Models.PlanifAgendaCat evento, bool enviarMail, bool solapamiento)
        {
            OpenDbConn();
            int result = 0;
            ArrayList aListCorreo = new ArrayList();

            try
            {

                DAL.PlanifAgendaCat cPlanifAgendaCat = new DAL.PlanifAgendaCat(cDblib);
                result = cPlanifAgendaCat.Delete(evento.ID);
                if (enviarMail)
                {
                    if (!solapamiento)
                    {
                        aListCorreo.Add(crearCorreoEliminacionEvento(evento, evento.CodRedProfesional));
                        if (evento.CodRedProfesional != evento.CodRedPromotor) aListCorreo.Add(crearCorreoEliminacionEvento(evento, evento.CodRedPromotor));

                    }
                    else
                    {
                        aListCorreo.Add(crearCorreoEliminacionEventoPorSolapamiento(evento));
                    }

                }           
            }catch(Exception ex){
                IB.SUPER.Shared.LogError.LogearError("Error al elminar el evento", ex);
            }           

            try
            {
                if (aListCorreo.Count > 0)
                    Correo.EnviarCorreosCita(aListCorreo);
            }
            catch (Exception ex)
            {
                IB.SUPER.Shared.LogError.LogearError("Error al enviar los mails de convocatoria:", ex);
            }

            return result;
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
        ~PlanifAgendaCat()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
