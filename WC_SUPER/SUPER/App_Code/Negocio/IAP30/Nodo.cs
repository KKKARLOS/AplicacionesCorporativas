using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for Nodo
/// </summary>
namespace IB.SUPER.IAP30.BLL
{
    public class Nodo : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("519f7700-3adf-47cd-8bc2-759df0cd8d2b");
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

        public List<Models.Nodo> Catalogo()
        {
            OpenDbConn();

            DAL.Nodo cNodo = new DAL.Nodo(cDblib);
            return cNodo.Catalogo();

        }

        public Models.Nodo Select(Int32 t303_idnodo)
        {
            OpenDbConn();

            DAL.Nodo cNodo = new DAL.Nodo(cDblib);
            return cNodo.Select(t303_idnodo);
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
