using System;
using System.Collections.Generic;
using System.Web;
using IB.SUPER.SIC.DAL;
using IB.SUPER.SIC.Models;

/// <summary>
/// Summary description for DocumentacionPreventa
/// </summary>
namespace IB.SUPER.SIC.BLL
{
    public class DocumentacionPreventa : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("59360737-0183-4716-b90f-066d8abfce73");
        private bool disposed = false;

        #endregion

        #region variables publicas
        public enum enumOrigenEdicion : byte
        {
            tareapreventa = 1,
            accionpreventa = 2,
            tareasaccionpreventa = 3,
            acciontareapreventa = 4
        }
        #endregion

        #region Constructor

        public DocumentacionPreventa()
            : base()
        {
            //OpenDbConn();
        }

        public DocumentacionPreventa(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas

        internal string ObtenerEstadoOrigenEdicion(enumOrigenEdicion enumProp, int idpropietario)
        {

            OpenDbConn();

            int? ta207_idtareapreventa = null;
            int? ta204_idaccionpreventa = null;

            switch (enumProp)
            {
                case enumOrigenEdicion.accionpreventa:
                    ta204_idaccionpreventa = idpropietario;
                    break;
                case enumOrigenEdicion.tareapreventa:
                    ta207_idtareapreventa = idpropietario;
                    break;
            }

            if (ta204_idaccionpreventa != null)
            {
                DAL.AccionPreventa cTP = new DAL.AccionPreventa(cDblib);
                return cTP.EstadoAccion((int)ta204_idaccionpreventa);

            }
            else if (ta207_idtareapreventa != null)
            {
                DAL.TareaPreventa cTP = new DAL.TareaPreventa(cDblib);
                return cTP.EstadoTarea((int)ta207_idtareapreventa);
            }


            return "X"; //default (más restrictivo)
        
        }

        internal List<Models.DocumentacionPreventa> Catalogo(enumOrigenEdicion enumProp, int idpropietario)
        {
            OpenDbConn();

            int? ta207_idtareapreventa = null;
            int? ta204_idaccionpreventa = null;

            switch (enumProp)
            {
                case enumOrigenEdicion.accionpreventa:
                    ta204_idaccionpreventa = idpropietario;
                    break;
                case enumOrigenEdicion.tareapreventa:
                    ta207_idtareapreventa = idpropietario;
                    break;
                case enumOrigenEdicion.tareasaccionpreventa:
                    ta204_idaccionpreventa = idpropietario;
                    break;
                case enumOrigenEdicion.acciontareapreventa:
                    ta207_idtareapreventa = idpropietario;

                    break;

            }

            DAL.DocumentacionPreventa cDocumentacionPreventa = new DAL.DocumentacionPreventa(cDblib);

            return cDocumentacionPreventa.Catalogo(ta204_idaccionpreventa, ta207_idtareapreventa, enumProp);

        }

        internal List<Models.DocumentacionPreventa> CatalogoGUID(Guid GUID)
        {
            OpenDbConn();

            DAL.DocumentacionPreventa cDocumentacionPreventa = new DAL.DocumentacionPreventa(cDblib);

            return cDocumentacionPreventa.CatalogoGUID(GUID);

        }

        internal int Insert(Models.DocumentacionPreventa oDocumentacionPreventa)
        {
            Guid methodOwnerID = new Guid("823b88ca-b989-4191-916e-a9676cfb36ec");

            OpenDbConn();

            //validaciones --> comprobar que la tarea ó acción se encuentran en estado "A"
            if (oDocumentacionPreventa.ta204_idaccionpreventa != null) {
                DAL.AccionPreventa cTP = new DAL.AccionPreventa(cDblib);
                if (cTP.EstadoAccion((int)oDocumentacionPreventa.ta204_idaccionpreventa) != "A") {
                    throw new Shared.ValidationException("La acción se encuentra en un estado en el que no se permite añadir nuevos documentos.");
                }
            }
            else if (oDocumentacionPreventa.ta207_idtareapreventa != null) {
                DAL.TareaPreventa cTP = new DAL.TareaPreventa(cDblib);
                if (cTP.EstadoTarea((int)oDocumentacionPreventa.ta207_idtareapreventa) != "A") {
                    throw new Shared.ValidationException("La tarea se encuentra en un estado en el que no se permite añadir nuevos documentos.");
                }
            }

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.DocumentacionPreventa cDocumentacionPreventa = new DAL.DocumentacionPreventa(cDblib);

                oDocumentacionPreventa.t001_idficepi_autor = int.Parse(HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"].ToString());
                oDocumentacionPreventa.ta210_fechamod = DateTime.Now;
                if (oDocumentacionPreventa.ta210_kbytes == 0) oDocumentacionPreventa.ta210_kbytes = 1;

                int idDocumentacionPreventa = cDocumentacionPreventa.Insert(oDocumentacionPreventa);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return idDocumentacionPreventa;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }
        }

        internal int Update(Models.DocumentacionPreventa oDocumentacionPreventa)
        {
            Guid methodOwnerID = new Guid("9b21a5dc-10b3-4eb8-9a51-b5013f20173b");

            OpenDbConn();

            //validaciones --> comprobar que la tarea ó acción se encuentran en estado "A"
            if (oDocumentacionPreventa.ta204_idaccionpreventa != null)
            {
                DAL.AccionPreventa cTP = new DAL.AccionPreventa(cDblib);
                if (cTP.EstadoAccion((int)oDocumentacionPreventa.ta204_idaccionpreventa) != "A")
                {
                    throw new Shared.ValidationException("La acción se encuentra en un estado en el que no se permite actualizar los documentos.");
                }
            }
            else if (oDocumentacionPreventa.ta207_idtareapreventa != null)
            {
                //Si la edición del doc de tarea se realiza desde la pantalla de detalle de acción --> no validar
                if (oDocumentacionPreventa.origenEdicion == "tareapreventa")
                {
                    DAL.TareaPreventa cTP = new DAL.TareaPreventa(cDblib);
                    if (cTP.EstadoTarea((int)oDocumentacionPreventa.ta207_idtareapreventa) != "A")
                    {
                        throw new Shared.ValidationException("La tarea se encuentra en un estado en el que no se permite actualizar los documentos.");
                    }
                }
            }

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.DocumentacionPreventa cDocumentacionPreventa = new DAL.DocumentacionPreventa(cDblib);

                

                //La fecha de modificacion y el autor se actualiza solo si cambia el fichero
                if (oDocumentacionPreventa.fileupdated)
                {
                    oDocumentacionPreventa.ta210_fechamod = DateTime.Now;
                    oDocumentacionPreventa.t001_idficepi_autor = int.Parse(HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"].ToString());
                }
                else
                    oDocumentacionPreventa.ta210_fechamod = null;

                if (oDocumentacionPreventa.ta210_kbytes == 0) oDocumentacionPreventa.ta210_kbytes = 1;

                int result = cDocumentacionPreventa.Update(oDocumentacionPreventa);

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
        }

        internal int Delete(Int32 ta210_iddocupreventa)
        {
            Guid methodOwnerID = new Guid("dfddaf15-37b7-4a5b-8eb7-48a7c10e51d0");

            OpenDbConn();

            //validaciones --> comprobar que la tarea ó acción se encuentran en estado "A"

            Models.DocumentacionPreventa oDocumentacionPreventa = this.Select(ta210_iddocupreventa);

            if (oDocumentacionPreventa.ta204_idaccionpreventa != null)
            {
                DAL.AccionPreventa cTP = new DAL.AccionPreventa(cDblib);
                if (cTP.EstadoAccion((int)oDocumentacionPreventa.ta204_idaccionpreventa) != "A")
                {
                    throw new Shared.ValidationException("La acción se encuentra en un estado en el que no se permite eliminar documentos.");
                }
            }
            else if (oDocumentacionPreventa.ta207_idtareapreventa != null)
            {
                //Si el borrado del doc de tarea se realiza desde la pantalla de detalle de acción --> no validar
                if (oDocumentacionPreventa.origenEdicion == "tareapreventa")
                {
                    DAL.TareaPreventa cTP = new DAL.TareaPreventa(cDblib);
                    if (cTP.EstadoTarea((int)oDocumentacionPreventa.ta207_idtareapreventa) != "A")
                    {
                        throw new Shared.ValidationException("La tarea se encuentra en un estado en el que no se permite eliminar documentos.");
                    }
                }
            }

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {

                DAL.DocumentacionPreventa cDocumentacionPreventa = new DAL.DocumentacionPreventa(cDblib);

                int result = cDocumentacionPreventa.Delete(ta210_iddocupreventa);

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
        }

        internal Models.DocumentacionPreventa Select(Int32 ta210_iddocupreventa)
        {
            OpenDbConn();

            DAL.DocumentacionPreventa cDocumentacionPreventa = new DAL.DocumentacionPreventa(cDblib);
            return cDocumentacionPreventa.Select(ta210_iddocupreventa);
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
        ~DocumentacionPreventa()
        {
            Dispose(false);
        }

        #endregion


    }

}
