using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;

using IB.SUPER.SIC.DAL;
using IB.SUPER.SIC.Models;
using System.Data;
using System.Collections;

/// <summary>
/// Summary description for ProyectoTecnico
/// </summary>
namespace IB.SUPER.SIC.BLL
{
    public class ProyectoTecnico : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("9CA69700-5E6B-4D9A-AF94-A7A3DC402B71");
        private bool disposed = false;

        #endregion

        #region Constructor

        public ProyectoTecnico()
            : base()
        {
            //OpenDbConn();
        }

        public ProyectoTecnico(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas

        public Models.ProyectoTecnico Select(Int32 t331_idpt)
        {
            OpenDbConn();

            DAL.ProyectoTecnico cPT = new DAL.ProyectoTecnico(cDblib);
            return cPT.Select(t331_idpt);
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
        ~ProyectoTecnico()
        {
            Dispose(false);
        }

        #endregion
    }
}