using System;
using System.Collections.Generic;


namespace IB.SUPER.Negocio
{
    public class bsUsuario : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("807c4eda-0f3c-470f-ac01-25e5880aa8d8");
        private bool disposed = false;

        #endregion

        #region Constructor

        public bsUsuario()
            : base()
        {
            //OpenDbConn();
        }

        

        public bsUsuario(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas

        public List<Models.bsUsuario> Catalogo(Nullable<Int32> t314_idusuario, Nullable<Int32> t001_idficepi, Nullable<Int32> t001_idficepi_pc)
        {
            OpenDbConn();

            DAL.bsUsuario cUsuario = new DAL.bsUsuario(cDblib);
            return cUsuario.CatalogoAccionesPendientes(t314_idusuario, t001_idficepi, t001_idficepi_pc);

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
        ~bsUsuario()
        {
            Dispose(false);
        }

        #endregion


    }

}
