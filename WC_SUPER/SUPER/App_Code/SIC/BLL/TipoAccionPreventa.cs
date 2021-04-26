using System;
using System.Collections.Generic;


using IB.SUPER.SIC.DAL;
using IB.SUPER.SIC.Models;

/// <summary>
/// Summary description for TipoAccionPreventa
/// </summary>
namespace IB.SUPER.SIC.BLL
{
    public class TipoAccionPreventa : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("d256e19c-56ac-4151-868a-966a1e0fe288");
        private bool disposed = false;

        #endregion

        #region Constructor

        public TipoAccionPreventa()
            : base()
        {
            //OpenDbConn();
        }

        public TipoAccionPreventa(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas

        internal List<Models.TipoAccionPreventa> Catalogo(Models.TipoAccionPreventa oTipoAccionPreventaFilter)
        {
            OpenDbConn();

            DAL.TipoAccionPreventa cTipoAccionPreventa = new DAL.TipoAccionPreventa(cDblib);
            return cTipoAccionPreventa.Catalogo(oTipoAccionPreventaFilter);

        }

        internal int Insert(Models.TipoAccionPreventa oTipoAccionPreventa)
        {
            Guid methodOwnerID = new Guid("f672bc44-f3fc-4102-8b50-2d3452b19198");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.TipoAccionPreventa cTipoAccionPreventa = new DAL.TipoAccionPreventa(cDblib);

                int idTipoAccionPreventa = cTipoAccionPreventa.Insert(oTipoAccionPreventa);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return idTipoAccionPreventa;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }
        }

        internal int Update(Models.TipoAccionPreventa oTipoAccionPreventa)
        {
            Guid methodOwnerID = new Guid("e0d6bdd5-2033-48cd-bb3d-80c7524647c4");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.TipoAccionPreventa cTipoAccionPreventa = new DAL.TipoAccionPreventa(cDblib);

                int result = cTipoAccionPreventa.Update(oTipoAccionPreventa);

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

        internal int Delete(Int16 ta205_idtipoaccionpreventa)
        {
            Guid methodOwnerID = new Guid("324a1f0b-bd9a-4614-abf1-6735001c79fe");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {

                DAL.TipoAccionPreventa cTipoAccionPreventa = new DAL.TipoAccionPreventa(cDblib);

                int result = cTipoAccionPreventa.Delete(ta205_idtipoaccionpreventa);

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

        internal Models.TipoAccionPreventa Select(Int16 ta205_idtipoaccionpreventa)
        {
            OpenDbConn();

            DAL.TipoAccionPreventa cTipoAccionPreventa = new DAL.TipoAccionPreventa(cDblib);
            return cTipoAccionPreventa.Select(ta205_idtipoaccionpreventa);
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
        ~TipoAccionPreventa()
        {
            Dispose(false);
        }

        #endregion


    }

}
