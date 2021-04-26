using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

using IB.Progress.DAL;
using IB.Progress.Models;
using IB.Progress.Shared;

/// <summary>
/// Summary description for TRAMITACIONCAMBIOROL
/// </summary>
namespace IB.Progress.BLL
{
    public class DesgloseRol : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("7993c49a-3b7a-461b-93ea-4682501bf048");
        private bool disposed = false;

        #endregion

        #region Constructor

        public DesgloseRol()
            : base()
        {
            //OpenDbConn();
        }

        public DesgloseRol(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas

        public List<Models.DesgloseRol> catalogoDesgloseRol(int t001_idficepi, int parentesco)
        {
            OpenDbConn();

            DAL.DesgloseRol cDesgloseRol = new DAL.DesgloseRol(cDblib);

            return cDesgloseRol.catalogoDesgloseRol(t001_idficepi, parentesco);
        }


        #endregion

        #region Conexion base de datos y dispose
        private void OpenDbConn()
        {
            if (cDblib == null)
                cDblib = new IB.sqldblib.SqlServerSP(IB.Progress.Shared.Database.GetConStr(), classOwnerID);
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
        ~DesgloseRol()
        {
            Dispose(false);
        }

        #endregion


    }

}
