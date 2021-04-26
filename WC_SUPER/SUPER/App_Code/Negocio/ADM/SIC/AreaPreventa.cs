using System;
using System.Collections.Generic;


using IB.SUPER.ADM.SIC.DAL;
using IB.SUPER.ADM.SIC.Models;

/// <summary>
/// Summary description for AreaPreventa
/// </summary>
namespace IB.SUPER.ADM.SIC.BLL
{
    public class AreaPreventa : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("2d57f08c-d3ad-482c-a575-0654b493e454");
        private bool disposed = false;

        #endregion

        #region Constructor

        public AreaPreventa()
            : base()
        {
            //OpenDbConn();
        }

        public AreaPreventa(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas

        public List<Models.AreaPreventa> Catalogo(Models.AreaPreventa oAreaPreventaFilter)
        {
            OpenDbConn();

            DAL.AreaPreventa cAreaPreventa = new DAL.AreaPreventa(cDblib);
            return cAreaPreventa.Catalogo(oAreaPreventaFilter);

        }

        public int Insert(Models.AreaPreventa oAreaPreventa)
        {
            Guid methodOwnerID = new Guid("24190a12-df18-4b0c-9987-4a8a08956858");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.AreaPreventa cAreaPreventa = new DAL.AreaPreventa(cDblib);

                int idAreaPreventa = cAreaPreventa.Insert(oAreaPreventa);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return idAreaPreventa;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }
        }

        public int Update(Models.AreaPreventa oAreaPreventa)
        {
            Guid methodOwnerID = new Guid("7459cc8f-3373-46c0-8fb6-35fec66c15a8");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.AreaPreventa cAreaPreventa = new DAL.AreaPreventa(cDblib);

                int result = cAreaPreventa.Update(oAreaPreventa);

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

        public int Delete(Int32 ta200_idareapreventa)
        {
            Guid methodOwnerID = new Guid("d2fca125-dbf5-4382-8f71-5fc61bb37228");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {

                DAL.AreaPreventa cAreaPreventa = new DAL.AreaPreventa(cDblib);

                int result = cAreaPreventa.Delete(ta200_idareapreventa);

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

        public Models.AreaPreventa Select(Int32 ta200_idareapreventa)
        {
            OpenDbConn();

            DAL.AreaPreventa cAreaPreventa = new DAL.AreaPreventa(cDblib);
            return cAreaPreventa.Select(ta200_idareapreventa);
        }

        public Models.AreaPreventa SelectPorDenominacion(string ta200_denominacion)
        {
            OpenDbConn();

            DAL.AreaPreventa cAreaPreventa = new DAL.AreaPreventa(cDblib);
            return cAreaPreventa.SelectPorDenominacion(ta200_denominacion);
        }

        public Models.AreaPreventa Select2(Int32 ta200_idareapreventa)
        {
            OpenDbConn();

            DAL.AreaPreventa cAreaPreventa = new DAL.AreaPreventa(cDblib);
            return cAreaPreventa.Select2(ta200_idareapreventa);
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
        ~AreaPreventa()
        {
            Dispose(false);
        }

        #endregion


    }

}
