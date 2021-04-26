using System;
using System.Collections.Generic;


using IB.SUPER.ADM.SIC.DAL;
using IB.SUPER.ADM.SIC.Models;

/// <summary>
/// Summary description for FiguraAreaPreventa
/// </summary>
namespace IB.SUPER.ADM.SIC.BLL
{
    public class FiguraAreaPreventa : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("32289fb8-95ef-4495-81b5-002e58d0064c");
        private bool disposed = false;

        #endregion

        #region Constructor

        public FiguraAreaPreventa()
            : base()
        {
            //OpenDbConn();
        }

        public FiguraAreaPreventa(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas


        public List<Models.FiguraAreaPreventa> Catalogo(Int32 ta200_idareapreventa)
        {
            OpenDbConn();

            DAL.FiguraAreaPreventa cFiguraAreaPreventa = new DAL.FiguraAreaPreventa(cDblib);
            return cFiguraAreaPreventa.Catalogo(ta200_idareapreventa);

        }
        public int Insert(Models.FiguraAreaPreventa oFiguraAreaPreventa)
        {
            Guid methodOwnerID = new Guid("f7969532-eb41-40a0-98b2-15dfb5c2d980");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.FiguraAreaPreventa cFiguraAreaPreventa = new DAL.FiguraAreaPreventa(cDblib);

                int idFiguraAreaPreventa = cFiguraAreaPreventa.Insert(oFiguraAreaPreventa);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return idFiguraAreaPreventa;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }
        }
        /*
        public int Update(Models.FiguraAreaPreventa oFiguraAreaPreventa)
        {
            Guid methodOwnerID = new Guid("b6a8a7d3-00cd-461c-8224-0e14e1671b67");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.FiguraAreaPreventa cFiguraAreaPreventa = new DAL.FiguraAreaPreventa(cDblib);

                int result = cFiguraAreaPreventa.Update(oFiguraAreaPreventa);

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
        */
        public int Delete(Int32 ta200_idareapreventa, Int32 t001_idficepi)
        {
            Guid methodOwnerID = new Guid("b7799c05-6e4b-46da-aded-27b48d4b078b");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {

                DAL.FiguraAreaPreventa cFiguraAreaPreventa = new DAL.FiguraAreaPreventa(cDblib);

                int result = cFiguraAreaPreventa.Delete(ta200_idareapreventa, t001_idficepi);

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
        public int DeleteFigura(Int32 ta200_idareapreventa, Int32 t001_idficepi, string ta202_figura)
        {
            Guid methodOwnerID = new Guid("b7799c05-6e4b-46da-aded-27b48d4b078b");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {

                DAL.FiguraAreaPreventa cFiguraAreaPreventa = new DAL.FiguraAreaPreventa(cDblib);

                int result = cFiguraAreaPreventa.DeleteFigura(ta200_idareapreventa, t001_idficepi, ta202_figura);

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

        public Models.FiguraAreaPreventa Select(Int32 ta200_idareapreventa, Int32 t001_idficepi)
        {
            OpenDbConn();

            DAL.FiguraAreaPreventa cFiguraAreaPreventa = new DAL.FiguraAreaPreventa(cDblib);
            return cFiguraAreaPreventa.Select(ta200_idareapreventa, t001_idficepi);
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
        ~FiguraAreaPreventa()
        {
            Dispose(false);
        }

        #endregion


    }

}
