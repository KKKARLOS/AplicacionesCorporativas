using System;
using System.Collections.Generic;


using IB.SUPER.SIC.DAL;
using IB.SUPER.SIC.Models;

/// <summary>
/// Summary description for EjecutorTareaPreventa
/// </summary>
namespace IB.SUPER.SIC.BLL
{
    public class EjecutorTareaPreventa : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("a510ed34-e8f8-43b9-91ca-c37b13a43de7");
        private bool disposed = false;

        #endregion

        #region Constructor

        public EjecutorTareaPreventa()
            : base()
        {
            //OpenDbConn();
        }

        public EjecutorTareaPreventa(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas

        internal List<Models.EjecutorTareaPreventa> Catalogo(Models.EjecutorTareaPreventa oEjecutorTareaPreventaFilter)
        {
            OpenDbConn();

            DAL.EjecutorTareaPreventa cEjecutorTareaPreventa = new DAL.EjecutorTareaPreventa(cDblib);
            return cEjecutorTareaPreventa.Catalogo(oEjecutorTareaPreventaFilter);

        }

        internal int Insert(Models.EjecutorTareaPreventa oEjecutorTareaPreventa)
        {
            Guid methodOwnerID = new Guid("521885ca-75bf-471c-af33-813d4b5dc803");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.EjecutorTareaPreventa cEjecutorTareaPreventa = new DAL.EjecutorTareaPreventa(cDblib);

                int idEjecutorTareaPreventa = cEjecutorTareaPreventa.Insert(oEjecutorTareaPreventa);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return idEjecutorTareaPreventa;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }
        }

        internal int Update(Models.EjecutorTareaPreventa oEjecutorTareaPreventa)
        {
            Guid methodOwnerID = new Guid("2b6e2ba1-fe0f-4bff-a3ae-d03595b16f5b");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.EjecutorTareaPreventa cEjecutorTareaPreventa = new DAL.EjecutorTareaPreventa(cDblib);

                int result = cEjecutorTareaPreventa.Update(oEjecutorTareaPreventa);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return result;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }
        }

        internal int Delete(Int32 ta207_idtareapreventa, Int32 t001_idficepi_ejecutor)
        {
            Guid methodOwnerID = new Guid("1207c024-c823-4158-8b84-9dc7c4bce1fe");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {

                DAL.EjecutorTareaPreventa cEjecutorTareaPreventa = new DAL.EjecutorTareaPreventa(cDblib);

                int result = cEjecutorTareaPreventa.Delete(ta207_idtareapreventa, t001_idficepi_ejecutor);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return result;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }
        }

        internal Models.EjecutorTareaPreventa Select(Int32 ta207_idtareapreventa, Int32 t001_idficepi_ejecutor)
        {
            OpenDbConn();

            DAL.EjecutorTareaPreventa cEjecutorTareaPreventa = new DAL.EjecutorTareaPreventa(cDblib);
            return cEjecutorTareaPreventa.Select(ta207_idtareapreventa, t001_idficepi_ejecutor);
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
        ~EjecutorTareaPreventa()
        {
            Dispose(false);
        }

        #endregion


    }

}
