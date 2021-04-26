using System;
using System.Collections;
using System.Collections.Generic;
using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

namespace IB.SUPER.IAP30.BLL
{
    /// <summary>
    /// Descripción breve de UsuarioGV
    /// </summary>
    public class UsuarioGV : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("89543389-95BC-4F64-9E88-DE14BFD0CD37");
        private bool disposed = false;

        #endregion

        #region Constructor

        public UsuarioGV()
            : base()
        {
            //OpenDbConn();
        }

        public UsuarioGV(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion
        #region Funciones públicas
        public Models.UsuarioGV Select(int t314_idusuario)
        {
            OpenDbConn();

            try
            {
                DAL.UsuarioGV cUsuario = new DAL.UsuarioGV(cDblib);
                return cUsuario.Select(t314_idusuario);
            }

            catch (Exception ex)
            {
                throw ex;
            }
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
        ~UsuarioGV()
        {
            Dispose(false);
        }

        #endregion
    }
}