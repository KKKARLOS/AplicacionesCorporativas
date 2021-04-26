using System;
using System.Collections.Generic;
using IB.SUPER.APP.DAL;
using IB.SUPER.APP.Models;

/// <summary>
/// Descripción breve de ALERTA
/// </summary>
namespace IB.SUPER.APP.BLL
{

    public class ALERTA : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("2ebac374-9faf-4f3f-a796-c9dcd35453ee");
        private bool disposed = false;

        #endregion

        #region Constructor

        public ALERTA()
            : base()
        {
            //OpenDbConn();
        }

        public ALERTA(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas
        public List<Models.ALERTA> Lista()
        {
            OpenDbConn();

            DAL.ALERTA cAlerta = new DAL.ALERTA(cDblib);
            return cAlerta.Lista();

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
        ~ALERTA()
        {
            Dispose(false);
        }

        #endregion

    }
}