using System;
using System.Collections.Generic;
using IB.SUPER.APP.DAL;
using IB.SUPER.APP.Models;

/// <summary>
/// Summary description for PRIOALERTAS
/// </summary>
namespace IB.SUPER.APP.BLL
{
    public class PRIOALERTAS : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("9dbaf74a-bf8d-430e-b1cf-274130761664");
        private bool disposed = false;

        #endregion

        #region Constructor

        public PRIOALERTAS()
            : base()
        {
            //OpenDbConn();
        }

        public PRIOALERTAS(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas

        public List<Models.PRIOALERTAS> Catalogo()
        {
            OpenDbConn();

            DAL.PRIOALERTAS cPRIOALERTAS = new DAL.PRIOALERTAS(cDblib);
            return cPRIOALERTAS.Catalogo();

        }

        public int Insert(Models.PRIOALERTAS oPRIOALERTAS)
        {
            Guid methodOwnerID = new Guid("afb9ebac-943a-4752-a77c-b7d83c7eee80");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.PRIOALERTAS cPRIOALERTAS = new DAL.PRIOALERTAS(cDblib);

                int idPRIOALERTAS = cPRIOALERTAS.Insert(oPRIOALERTAS);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return idPRIOALERTAS;
            }
            catch (Exception ex)
            {
                //rollback
                //if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.rollbackTransaction(methodOwnerID);
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.rollbackTransaction(methodOwnerID);
                throw ex;
            }
        }

        public int Update(Models.PRIOALERTAS oPRIOALERTAS)
        {
            Guid methodOwnerID = new Guid("1efd6df0-8ebe-42e3-a1e1-ef71b020dc68");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.PRIOALERTAS cPRIOALERTAS = new DAL.PRIOALERTAS(cDblib);

                int result = cPRIOALERTAS.Update(oPRIOALERTAS);

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
        ~PRIOALERTAS()
        {
            Dispose(false);
        }

        #endregion


    }

}
