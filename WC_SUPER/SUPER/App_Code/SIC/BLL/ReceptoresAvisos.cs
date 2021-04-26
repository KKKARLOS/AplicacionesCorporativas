using System;
using System.Collections.Generic;


using IB.SUPER.SIC.DAL;
using IB.SUPER.SIC.Models;

/// <summary>
/// Summary description for ReceptoresAvisos
/// </summary>
namespace IB.SUPER.SIC.BLL
{
    public class ReceptoresAvisos : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("B3585121-98B7-4D06-8399-17A14FDAD6A6");
        private bool disposed = false;

        #endregion

        #region Constructor

        public ReceptoresAvisos()
            : base()
        {
            //OpenDbConn();
        }

        public ReceptoresAvisos(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas

        public List<Models.ReceptoresAvisos> Catalogo()
        {
            OpenDbConn();

            DAL.ReceptoresAvisos cReceptoresAvisos = new DAL.ReceptoresAvisos(cDblib);
            return cReceptoresAvisos.Catalogo();

        }

        public int Update(Models.ReceptoresAvisos oReceptoresAvisos)
        {
            Guid methodOwnerID = new Guid("D8198EC4-92C4-49ED-9054-2C550CE39AFD");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.ReceptoresAvisos cReceptoresAvisos = new DAL.ReceptoresAvisos(cDblib);

                int result = cReceptoresAvisos.Update(oReceptoresAvisos);

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

        public void GrabarLista(List<Models.ReceptoresAvisos> Lista)
        {
            bool bConTransaccion = false;
            Guid methodOwnerID = new Guid("2E4127AD-00E3-40CD-B964-551B92CA8ABF");
            OpenDbConn();
            if (cDblib.Transaction.ownerID.Equals(new Guid()))
                bConTransaccion = true;
            if (bConTransaccion)
                cDblib.beginTransaction(methodOwnerID);

            DAL.ReceptoresAvisos bReceptor = new DAL.ReceptoresAvisos(cDblib);
            try
            {
                foreach (Models.ReceptoresAvisos oElem in Lista)
                {
                    bReceptor.Update(oElem);
                }
                if (bConTransaccion)
                    cDblib.commitTransaction(methodOwnerID);
            }
            catch (Exception ex)
            {
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID))
                    cDblib.rollbackTransaction(methodOwnerID);
                throw new Exception(ex.Message);
            }
            finally
            {
                //bReceptor.Dispose();
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
        ~ReceptoresAvisos()
        {
            Dispose(false);
        }

        #endregion


    }

}
