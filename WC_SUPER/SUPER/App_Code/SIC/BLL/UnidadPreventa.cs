using System;
using System.Collections.Generic;


using IB.SUPER.SIC.DAL;
using IB.SUPER.SIC.Models;

/// <summary>
/// Summary description for UnidadPreventa
/// </summary>
namespace IB.SUPER.SIC.BLL
{
    public class UnidadPreventa : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("7C9D891B-2DB3-4E04-8A87-D9E1BE30F201");
        private bool disposed = false;

        #endregion

        #region Constructor

        public UnidadPreventa()
            : base()
        {
            //OpenDbConn();
        }

        public UnidadPreventa(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas

        internal List<Models.UnidadPreventa> Catalogo(Models.UnidadPreventa oUnidadPreventaFilter)
        {
            OpenDbConn();

            DAL.UnidadPreventa cUnidadPreventa = new DAL.UnidadPreventa(cDblib);
            return cUnidadPreventa.Catalogo(oUnidadPreventaFilter);

        }

        internal int Insert(Models.UnidadPreventa oUnidadPreventa)
        {
            Guid methodOwnerID = new Guid("A680B03B-7A19-40D9-B4DC-3FEFDBCBB960");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.UnidadPreventa cUnidadPreventa = new DAL.UnidadPreventa(cDblib);

                int idUnidadPreventa = cUnidadPreventa.Insert(oUnidadPreventa);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return idUnidadPreventa;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }
        }

        internal int Update(Models.UnidadPreventa oUnidadPreventa)
        {
            Guid methodOwnerID = new Guid("4C4DD80E-6FE3-40F4-A385-CB43CFE75288");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.UnidadPreventa cUnidadPreventa = new DAL.UnidadPreventa(cDblib);

                int result = cUnidadPreventa.Update(oUnidadPreventa);

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

        public int Delete(Int16 ta199_idunidadpreventa)
        {
            Guid methodOwnerID = new Guid("291D4D11-6012-44AB-A3C8-B5616DA08B7F");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {

                DAL.UnidadPreventa cUnidadPreventa = new DAL.UnidadPreventa(cDblib);

                int result = cUnidadPreventa.Delete(ta199_idunidadpreventa);

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

        //internal Models.UnidadPreventa Select(Int16 ta199_idunidadpreventa)
        //{
        //    OpenDbConn();

        //    DAL.UnidadPreventa cUnidadPreventa = new DAL.UnidadPreventa(cDblib);
        //    return cUnidadPreventa.Select(ta199_idunidadpreventa);
        //}


        public Models.UnidadPreventa Select(Int16 ta199_idunidadpreventa)
        {
            OpenDbConn();

            DAL.UnidadPreventa cUnidadPreventa = new DAL.UnidadPreventa(cDblib);
            return cUnidadPreventa.Select(ta199_idunidadpreventa);
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
        ~UnidadPreventa()
        {
            Dispose(false);
        }

        #endregion


    }

}
