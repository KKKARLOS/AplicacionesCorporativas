using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using IB.Progress.Shared;
using IB.Progress.DAL;
using IB.Progress.Models;
using System.Data;

/// <summary>
/// Summary description for PERFILES
/// </summary>
namespace IB.Progress.BLL
{
    public class Perfiles : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("c9c8021b-c601-4e91-a143-cad805ef2e63");
        private bool disposed = false;

        #endregion

        #region Constructor

        public Perfiles()
            : base()
        {
            //OpenDbConn();
        }

        public Perfiles(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas

        //public  List<Models.ComunidadProgress> Catalogo()
        public void Catalogo()
        {
            OpenDbConn();

            DAL.ComunidadProgress cComunidadProgress = new DAL.ComunidadProgress(cDblib);
            //return cComunidadProgress.Catalogo();

        }


        public Models.Perfiles catalogo()
        {
            OpenDbConn();

            DAL.Perfiles cPerfiles = new DAL.Perfiles(cDblib);

            return cPerfiles.Catalogo();
        }


        public int Update(int contenedor, List<short> lista)
        {
            Guid methodOwnerID = new Guid("5cd1f103-d790-49d9-a8a3-f2357c041a4a");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {

                DAL.Perfiles cPerfiles = new DAL.Perfiles(cDblib);

                int result = cPerfiles.Update(contenedor, string.Join(",", lista));

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return result;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.rollbackTransaction(methodOwnerID);

                throw new IBException(103, "Ocurrió un error al actualizar pantalla de perfiles.", ex);
            }
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
        ~Perfiles()
        {
            Dispose(false);
        }

        #endregion


    }

}
