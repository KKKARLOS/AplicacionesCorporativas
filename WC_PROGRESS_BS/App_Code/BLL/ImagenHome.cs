using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using IB.Progress.Shared;
using IB.Progress.DAL;
using IB.Progress.Models;
using System.Data;
using System.Data.SqlClient;


namespace IB.Progress.BLL
{
    public class ImagenHome : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("aaf468e6-665c-4723-82cd-ad16ace8f801");
        private bool disposed = false;

        #endregion

        #region Constructor

        public ImagenHome()
            : base()
        {
            //OpenDbConn();
        }

        public ImagenHome(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas

        public void subirImagen(byte [] pic)
        {
            OpenDbConn();

            IB.Progress.DAL.ImagenHome oImagenHome = new DAL.ImagenHome(cDblib);
            oImagenHome.Update(pic);

        }

        /// <summary>
        /// Obtiene la imagen de la home
        /// </summary>
        public byte[] obtenerImagen()
        {
            OpenDbConn();

            IB.Progress.DAL.ImagenHome cImagenHome = new DAL.ImagenHome(cDblib);
            return cImagenHome.Select();
        }

        #endregion

        #region Conexion base de datos y dispose
        private void OpenDbConn()
        {
            if (cDblib == null)
                cDblib = new IB.sqldblib.SqlServerSP(Database.GetConStr(), classOwnerID);
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
        ~ImagenHome()
        {
            Dispose(false);
        }

        #endregion


    }

}
