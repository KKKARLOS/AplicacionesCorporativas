using System;
using System.Collections.Generic;

using IB.SUPER.APP.DAL;
using IB.SUPER.APP.Models;

/// <summary>
/// Summary description for FIGURAPROYECTOSUBNODO
/// </summary>
namespace IB.SUPER.APP.BLL
{
    public class FIGURAPROYECTOSUBNODO : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("bb606355-b114-4570-9821-0e31d0b8e3e3");
        private bool disposed = false;

        #endregion

        #region Constructor

        public FIGURAPROYECTOSUBNODO()
            : base()
        {
            //OpenDbConn();
        }

        public FIGURAPROYECTOSUBNODO(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas
        internal int Insert(Models.FIGURAPROYECTOSUBNODO oFIGURAPROYECTOSUBNODO)
        {
            Guid methodOwnerID = new Guid("67f93f85-b3be-49ca-acee-fecb37d251f6");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.FIGURAPROYECTOSUBNODO cFIGURAPROYECTOSUBNODO = new DAL.FIGURAPROYECTOSUBNODO(cDblib);

                int idFIGURAPROYECTOSUBNODO = cFIGURAPROYECTOSUBNODO.Insert(oFIGURAPROYECTOSUBNODO);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return idFIGURAPROYECTOSUBNODO;
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
        ~FIGURAPROYECTOSUBNODO()
        {
            Dispose(false);
        }

        #endregion


    }

}
