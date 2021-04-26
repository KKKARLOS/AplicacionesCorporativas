using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;

using IB.SUPER.SIC.DAL;
using IB.SUPER.SIC.Models;
using System.Data;
using System.Collections;

/// <summary>
/// Summary description for ProyectoSubnodo
/// </summary>
namespace IB.SUPER.SIC.BLL
{
    public class ProyectoSubnodo : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("4D6366D7-EA43-4E6E-821E-8D95946AD081");
        private bool disposed = false;

        #endregion

        #region Constructor

        public ProyectoSubnodo()
            : base()
        {
            //OpenDbConn();
        }

        public ProyectoSubnodo(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas

        public Models.ProyectoSubnodo Select(Int32 t301_idproyecto)
        {
            OpenDbConn();

            DAL.ProyectoSubnodo cPT = new DAL.ProyectoSubnodo(cDblib);
            return cPT.Select(t301_idproyecto);
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
        ~ProyectoSubnodo()
        {
            Dispose(false);
        }

        #endregion
    }
}