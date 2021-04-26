using System;
using System.Collections.Generic;


using IB.SUPER.ADM.SIC.DAL;
using IB.SUPER.ADM.SIC.Models;

/// <summary>
/// Summary description for UnidadPreventa
/// </summary>
namespace IB.SUPER.ADM.SIC.BLL
{
    public class EstructuraPreventa : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("40125173-a185-4724-8cdd-3f850f4bbb11");
        private bool disposed = false;

        #endregion

        #region Constructor

        public EstructuraPreventa()
            : base()
        {
            //OpenDbConn();
        }

        public EstructuraPreventa(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas

        public List<Models.EstructuraPreventa> Arbol(bool bMostrarInactivos)
        {
            OpenDbConn();

            DAL.EstructuraPreventa cEstructura = new DAL.EstructuraPreventa(cDblib);
            return cEstructura.Arbol(bMostrarInactivos);
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
        ~EstructuraPreventa()
        {
            Dispose(false);
        }

        #endregion


    }

}
