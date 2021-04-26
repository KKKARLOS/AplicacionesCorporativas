using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for TipoAsuntoCat
/// </summary>
namespace IB.SUPER.IAP30.BLL
{
    public class TipoAsuntoCat : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("9704DC08-874E-4799-BFE6-DB976767292A");
        private bool disposed = false;

        #endregion

        #region Constructor

        public TipoAsuntoCat()
            : base()
        {
            //OpenDbConn();
        }

        public TipoAsuntoCat(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas
        public List<Models.TipoAsuntoCat> Catalogo(Models.TipoAsuntoCat oFiltro)
        {
            OpenDbConn();
            DAL.TipoAsuntoCat cTipoAsunto = new DAL.TipoAsuntoCat(cDblib);
            List<Models.TipoAsuntoCat> oLista = cTipoAsunto.Catalogo(oFiltro);
            return oLista;
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
        ~TipoAsuntoCat()
        {
            Dispose(false);
        }

        #endregion


    }

}
