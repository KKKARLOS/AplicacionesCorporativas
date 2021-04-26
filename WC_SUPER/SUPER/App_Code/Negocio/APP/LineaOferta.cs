using System;
using System.Collections.Generic;

using IB.SUPER.APP.DAL;
using IB.SUPER.APP.Models;

/// <summary>
/// Summary description for LineaOferta
/// </summary>
namespace IB.SUPER.APP.BLL
{
    public class LineaOferta : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("1F2794C0-3832-49C4-8888-2CFEA2F3C7AD");
        private bool disposed = false;

        #endregion

        #region Constructor

        public LineaOferta()
            : base()
        {
            //OpenDbConn();
        }

        public LineaOferta(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas
        public List<Models.LineaOferta> Catalogo(bool bMostrarInactivos)
        {
            OpenDbConn();

            DAL.LineaOferta cLineaOferta = new DAL.LineaOferta(cDblib);
            return cLineaOferta.Catalogo(bMostrarInactivos);

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
        ~LineaOferta()
        {
            Dispose(false);
        }

        #endregion
    }
}