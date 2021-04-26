using System;
using System.Collections.Generic;

using IB.SUPER.APP.DAL;
using IB.SUPER.APP.Models;

/// <summary>
/// Summary description for Contrato
/// </summary>
namespace IB.SUPER.APP.BLL
{
    public class Contrato : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("5343E4EC-C38C-496C-A28A-06E6702E3FC4");
        private bool disposed = false;

        #endregion

        #region Constructor

        public Contrato()
            : base()
        {
            //OpenDbConn();
        }

        public Contrato(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas

        public bool Existe(int t306_idcontrato)
        {
            bool bRes = false;

            OpenDbConn();

            DAL.Contrato cContrato = new DAL.Contrato(cDblib);
            List<Models.Contrato> lst = cContrato.Catalogo(t306_idcontrato);
            if (lst.Count > 0)
                bRes = true;

            return bRes;
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
        ~Contrato()
        {
            Dispose(false);
        }

        #endregion
    }
}