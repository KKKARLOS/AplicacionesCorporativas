using System;
using System.Collections;
using System.Collections.Generic;
//using SUPERANTIGUO = SUPER;

using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for Usuario
/// </summary>
namespace IB.SUPER.IAP30.BLL
{
    public class NotaGASVI : IDisposable
    {
        #region Variables privadas
        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("9524177A-EB1A-4583-8589-693D2B592371");
        private bool disposed = false;

        #endregion
        #region Constructor
        public NotaGASVI()
            : base()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }
        public NotaGASVI(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
        #endregion

        #region Funciones públicas
        public int grabar(Models.CabeceraGV cabecera, List<Models.LineaGV> lineas)
        {
            int idReferencia;
            bool bConTransaccion = false;
            Guid methodOwnerID = new Guid("5590F1B4-7073-4B5F-A4DB-9F301648D151");
            //DOARHUMI 08/01/2018 Comprobaciones sobre las posiciones
            AnnoGasto oAnno = new AnnoGasto();
            UsuarioProyectoSubNodo oPSN = new UsuarioProyectoSubNodo();
            PosicionGV oECO = new PosicionGV();

            OpenDbConn();
            if (cDblib.Transaction.ownerID.Equals(new Guid()))
                bConTransaccion = true;
            if (bConTransaccion)
                cDblib.beginTransaction(methodOwnerID);
            try
            {
                //DOARHUMI 08/01/2018 Comprobaciones sobre las posiciones
                Hashtable htAnnoGasto = oAnno.ObtenerHTAnnoGasto();
                Hashtable htUsuarioPSN = oPSN.ObtenerFechasAsociacionPSN(cabecera.t314_idusuario_interesado);

                if (cabecera.t423_idmotivo == 1) //Si motivo proyecto
                {
                    if ((DateTime?[])htUsuarioPSN[cabecera.t305_idproyectosubnodo] == null)
                    {
                        throw (new Exception("Tramitación denegada.\n\nEl beneficiario no se encuentra asociado al proyecto."));
                    }
                    else
                    {
                    }
                }

                DAL.CabeceraGV oCab = new DAL.CabeceraGV(cDblib);
                idReferencia = oCab.Insert(cabecera);

                //Si el beneficiario es autorresponsable, comprobaciones de motivos, excepciones, etc, para pasar la nota a aprobada.
                //En realidad, por ser motivo=Proyecto no se va a autoaprobar sino que quedara en manos del profesional indicado en el proyecto
                //pero lo dejo por si algun dia cambia el criterio, y asi no tendremos que acordarnos de tocar este codigo
                if (cabecera.autoResponsable)
                {
                    oCab.GestionarAutorresponsabilidad(idReferencia);
                }


                foreach (Models.LineaGV oLinea in lineas)
                {
                    //DOARHUMI 08/01/2018 Comprobaciones sobre las posiciones
                    DateTime oAuxiliar = oLinea.desde;
                    DateTime oDesde = oLinea.desde;
                    DateTime oHasta = oLinea.hasta;
                    DateTime oDesdePSN;
                    DateTime? oHastaPSN = null;

                    oLinea.idCabecera = idReferencia;

                    do {
                        #region Comprobaciones de fechas
                        if ((DateTime[])htAnnoGasto[oAuxiliar.Year] == null)
                        {
                            throw (new Exception("Tramitación denegada.\n\nNo se pueden tramitar gastos correspondientes al año " + oAuxiliar.Year.ToString()));
                        }
                        else
                        {
                            if (oAuxiliar < ((DateTime[])htAnnoGasto[oAuxiliar.Year])[0])
                            {
                                throw (new Exception("Tramitación denegada.\n\nHasta el día " + ((DateTime[])htAnnoGasto[oAuxiliar.Year])[0].ToShortDateString() + " no se permite tramitar gastos correspondientes al año " + oAuxiliar.Year.ToString()));
                            }
                            else if (oAuxiliar > ((DateTime[])htAnnoGasto[oAuxiliar.Year])[1])
                            {
                                throw (new Exception("Tramitación denegada.\n\nSe ha superado la fecha límite establecida para tramitar gastos correspondientes al año " + oAuxiliar.Year.ToString() + " (" + ((DateTime[])htAnnoGasto[oAuxiliar.Year])[1].ToShortDateString() + ")"));
                            }
                        }
                        if (cabecera.t423_idmotivo == 1) //Si motivo proyecto
                        {
                            oDesdePSN = (DateTime)((DateTime?[])htUsuarioPSN[cabecera.t305_idproyectosubnodo])[0];
                            oHastaPSN = ((DateTime?[])htUsuarioPSN[cabecera.t305_idproyectosubnodo])[1];

                            if (oAuxiliar < oDesdePSN || (oHastaPSN != null && oAuxiliar > oHastaPSN))
                            {
                                throw (new Exception("Tramitación denegada.\n\nEl rango de fechas de la imputación (" + oDesde.ToShortDateString() + " - " + oHasta.ToShortDateString() + ") se encuentra fuera del rango de fechas de asociación al proyecto (" + oDesdePSN.ToShortDateString() + " - " + ((oHastaPSN != null) ? ((DateTime)oHastaPSN).ToShortDateString() : "") + ")"));
                            }
                        }
                        #endregion
                        #region  Comprobar que si se indica un desplazamiento ECO, se corresponda con un VUP
                        if (oLinea.idECO.ToString() != "")
                        {
                            if (!oECO.EsDesplazamientoECOenVUP((int)oLinea.idECO))
                            {
                                throw (new Exception("Tramitación denegada.\n\nEl desplazamiento correspondiente a las fechas (" + oLinea.desde.ToShortDateString() + " - " + oLinea.hasta.ToShortDateString() + ") con destino \"" + oLinea.destino + "\" se ha realizado en un coche de flota, por lo que no procede asociarlo a una solicitud GASVI."));
                            }
                        }
                        #endregion
                        oAuxiliar = oAuxiliar.AddDays(1);
                    } while (oAuxiliar <= oHasta);

                    DAL.LineaGV objLinea = new DAL.LineaGV(cDblib);
                    objLinea.Insert(oLinea);
                }

                if (bConTransaccion)
                    cDblib.commitTransaction(methodOwnerID);

                return idReferencia;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID))
                    cDblib.rollbackTransaction(methodOwnerID);
                throw new Exception(ex.Message);
            }
            finally
            {
                //nota.Dispose();
                oAnno.Dispose();
                oPSN.Dispose();
                oECO.Dispose();
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
        ~NotaGASVI()
        {
            Dispose(false);
        }

        #endregion
    }
}