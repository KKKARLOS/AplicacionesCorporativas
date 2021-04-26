using System;
using System.Collections.Generic;




namespace IB.SUPER.Negocio
{
    public class bsUsuariosavisos : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("a24002dd-ff7f-4616-861d-dd6168919bf1");
        private bool disposed = false;

        #endregion

        #region Constructor

        public bsUsuariosavisos()
            : base()
        {
            //OpenDbConn();
        }

        public bsUsuariosavisos(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas



        public List<Models.bsUsuariosAvisos> Select(Int32 t314_idusuario)
        {
            OpenDbConn();

            DAL.bsUsuariosAvisos cbsUsuariosAvisos = new DAL.bsUsuariosAvisos(cDblib);
            return cbsUsuariosAvisos.Select(t314_idusuario);
        }


        public int CountByUsuario(Int32 t314_idusuario)
        {
            OpenDbConn();

            DAL.bsUsuariosAvisos cbsUsuariosAvisos = new DAL.bsUsuariosAvisos(cDblib);
            return cbsUsuariosAvisos.CountByUsuario(t314_idusuario);
        }

        public int Delete(Int32 t448_idaviso, int t314_idusuario)
        {
            OpenDbConn();

            DAL.bsUsuariosAvisos cbsUsuariosAvisos = new DAL.bsUsuariosAvisos(cDblib);
            return cbsUsuariosAvisos.Delete(t448_idaviso, t314_idusuario);
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
        ~bsUsuariosavisos()
        {
            Dispose(false);
        }

        #endregion


    }

}
