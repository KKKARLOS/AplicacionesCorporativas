using System;
using System.Collections.Generic;
using IB.SUPER.APP.DAL;
using IB.SUPER.APP.Models;

/// <summary>
/// Summary description for MOTIVOOCFA
/// </summary>
namespace IB.SUPER.APP.BLL
{
    public class MOTIVOOCFA : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("BB24FBAA-2637-49DD-8751-B9A392F8EE92");
        private bool disposed = false;

        #endregion

        #region Constructor

        public MOTIVOOCFA()
            : base()
        {
            //OpenDbConn();
        }

        public MOTIVOOCFA(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas

        public List<Models.MOTIVOOCFA> Catalogo(Models.MOTIVOOCFA oMOTIVOOCFAFilter)
        {
            OpenDbConn();

            DAL.MOTIVOOCFA cMOTIVOOCFA = new DAL.MOTIVOOCFA(cDblib);
            return cMOTIVOOCFA.Catalogo(oMOTIVOOCFAFilter);

        }

        public int Insert(Models.MOTIVOOCFA oMOTIVOOCFA)
        {
            Guid methodOwnerID = new Guid("b70fc40f-b61f-408b-94d1-fa4a138331dc");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.MOTIVOOCFA cMOTIVOOCFA = new DAL.MOTIVOOCFA(cDblib);

                int idMOTIVOOCFA = cMOTIVOOCFA.Insert(oMOTIVOOCFA);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return idMOTIVOOCFA;
            }
            catch (Exception ex)
            {
                //rollback
                //if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.rollbackTransaction(methodOwnerID);
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.rollbackTransaction(methodOwnerID);
                throw ex;
            }
        }

        public int Update(Models.MOTIVOOCFA oMOTIVOOCFA)
        {
            Guid methodOwnerID = new Guid("c04ee672-fb30-4243-b204-a8a7d2139680");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.MOTIVOOCFA cMOTIVOOCFA = new DAL.MOTIVOOCFA(cDblib);

                int result = cMOTIVOOCFA.Update(oMOTIVOOCFA);

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

        public int Delete(Int32 t840_idmotivo)
        {
            Guid methodOwnerID = new Guid("8f85a6b2-6e01-415d-845e-61f82f2a1e7e");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {

                DAL.MOTIVOOCFA cMOTIVOOCFA = new DAL.MOTIVOOCFA(cDblib);

                int result = cMOTIVOOCFA.Delete(t840_idmotivo);

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
        public void BorrarMotivos(List<Models.MOTIVOOCFA> lista)
        {
            Guid methodOwnerID = new Guid("26EEFA7A-37A4-49A1-8B2D-E34B35316327");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.MOTIVOOCFA cMotivo = new DAL.MOTIVOOCFA(cDblib);
                foreach (Models.MOTIVOOCFA elem in lista)
                {
                    cMotivo.Delete(elem.t840_idmotivo);
                }
                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

            }
            catch (Exception ex)
            {//rollback
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }
        }

        public Models.MOTIVOOCFA Select(Int32 t840_idmotivo)
        {
            OpenDbConn();

            DAL.MOTIVOOCFA cMOTIVOOCFA = new DAL.MOTIVOOCFA(cDblib);
            return cMOTIVOOCFA.Select(t840_idmotivo);
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
        ~MOTIVOOCFA()
        {
            Dispose(false);
        }

        #endregion


    }

}
