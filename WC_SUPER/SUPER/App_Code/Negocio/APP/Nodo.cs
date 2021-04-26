using System;
using System.Collections.Generic;


using IB.SUPER.APP.DAL;
using IB.SUPER.APP.Models;

/// <summary>
/// Summary description for Nodo
/// </summary>
namespace IB.SUPER.APP.BLL
{
    public class Nodo : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("1933B3A6-C251-43F8-BF92-E42D67E5CFC8");
        private bool disposed = false;

        #endregion

        #region Constructor

        public Nodo()
            : base()
        {
            //OpenDbConn();
        }

        public Nodo(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas

        public List<Models.NodoBasico> Catalogo()
        {
            OpenDbConn();

            DAL.Nodo cNodo = new DAL.Nodo(cDblib);
            return cNodo.Catalogo();

        }
        public List<Models.NodoBasico> CatalogoNoHermes()
        {
            OpenDbConn();

            DAL.Nodo cNodo = new DAL.Nodo(cDblib);
            return cNodo.CatalogoNoHermes();

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
        ~Nodo()
        {
            Dispose(false);
        }

        #endregion


    }

}
