using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using IB.Progress.Shared;
using IB.Progress.DAL;
using IB.Progress.Models;
using System.Data;

/// <summary>
/// Summary description for ROLIB
/// </summary>
namespace IB.Progress.BLL
{
    public class Colectivo : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("FEF84D50-0009-4423-BAE5-A9522EFA6D36");
        private bool disposed = false;

        #endregion

        #region Constructor

        public Colectivo()
            : base()
        {
            //OpenDbConn();
        }

        public Colectivo(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas

        public List<Models.Colectivo> Catalogo()
        {
            OpenDbConn();

            DAL.Colectivo cColectivo = new DAL.Colectivo(cDblib);
            return cColectivo.Catalogo();

        }

        #endregion

        #region Conexion base de datos y dispose
        private void OpenDbConn()
        {
            if (cDblib == null)
                cDblib = new IB.sqldblib.SqlServerSP(Database.GetConStr(), classOwnerID);
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
        ~Colectivo()
        {
            Dispose(false);
        }

        #endregion


    }

}
