using System;
using System.Collections.Generic;
using IB.SUPER.APP.DAL;
using IB.SUPER.APP.Models;

/// <summary>
/// Summary description for SALDOSSAP
/// </summary>
namespace IB.SUPER.APP.BLL
{
    public class SALDOSSAP : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("81a6f7a1-3d49-4172-8dae-58d4942cf6b1");
        private bool disposed = false;

        #endregion

        #region Constructor

        public SALDOSSAP()
            : base()
        {
            //OpenDbConn();
        }

        public SALDOSSAP(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas

        public List<Models.SALDOSSAP> Catalogo()
        {
            OpenDbConn();

            DAL.SALDOSSAP cSALDOSSAP = new DAL.SALDOSSAP(cDblib);
            return cSALDOSSAP.Catalogo();

        }

        public int Insert(Models.SALDOSSAP oSALDOSSAP)
        {
            Guid methodOwnerID = new Guid("00e023c4-b023-40c9-a0a9-35babef8c832");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.SALDOSSAP cSALDOSSAP = new DAL.SALDOSSAP(cDblib);

                int idSALDOSSAP = cSALDOSSAP.Insert(oSALDOSSAP);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return idSALDOSSAP;
            }
            catch (Exception ex)
            {
                //rollback
                //if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.rollbackTransaction(methodOwnerID);
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.rollbackTransaction(methodOwnerID);
                throw ex;
            }
        }

        public int Delete()
        {
            Guid methodOwnerID = new Guid("b25cd191-51df-4f80-9499-fc9369f14716");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {

                DAL.SALDOSSAP cSALDOSSAP = new DAL.SALDOSSAP(cDblib);

                int result = cSALDOSSAP.Delete();

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return result;
            }
            catch (Exception ex)
            {
                //rollback
                //if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.rollbackTransaction(methodOwnerID);
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.rollbackTransaction(methodOwnerID);
                throw ex;
            }
        }

        public int Pasar_a_SUPER(int iFecha)
        {
            Guid methodOwnerID = new Guid("43960C35-E254-4CCD-8A18-4245BB1B9EB5");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {

                DAL.SALDOSSAP cSALDOSSAP = new DAL.SALDOSSAP(cDblib);

                int result = cSALDOSSAP.Pasar_a_SUPER(iFecha);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return result;
            }
            catch (Exception ex)
            {
                //rollback
                //if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.rollbackTransaction(methodOwnerID);
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.rollbackTransaction(methodOwnerID);
                throw ex;
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
        ~SALDOSSAP()
        {
            Dispose(false);
        }

        #endregion


    }

}
