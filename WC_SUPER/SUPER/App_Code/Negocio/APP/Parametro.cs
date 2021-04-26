using System;
using System.Collections.Generic;

using IB.SUPER.APP.DAL;
using IB.SUPER.APP.Models;

/// <summary>
/// Summary description for Nodo
/// </summary>
namespace IB.SUPER.APP.BLL
{
    public class Parametro: IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("B352AAA6-969D-4A9C-A886-FCD9DC34E322");
        private bool disposed = false;

        #endregion

        #region Constructor

        public Parametro()
            : base()
        {
            //OpenDbConn();
        }

        public Parametro(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas

        public Models.Parametro GetDatos(int idTabla)
        {
            OpenDbConn();

            DAL.Parametro cParametro = new DAL.Parametro(cDblib);
            return cParametro.GetDatos(idTabla);
        }
        public List<Models.Parametro> Catalogo(int idTabla)
        {
            OpenDbConn();

            DAL.Parametro cParametro = new DAL.Parametro(cDblib);
            return cParametro.Catalogo(idTabla);

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
        ~Parametro()
        {
            Dispose(false);
        }

        #endregion
    }
}