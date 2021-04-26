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
/// Summary description for ROLIB
/// </summary>
namespace IB.Progress.BLL
{
    public class ComunidadProgress : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("7624175a-e819-40e0-ae22-a6386693fc92");
        private bool disposed = false;

        #endregion

        #region Constructor

        public ComunidadProgress()
            : base()
        {
            //OpenDbConn();
        }

        public ComunidadProgress(sqldblib.SqlServerSP extcDblib)
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


        public Models.ComunidadProgress catalogo()
        {
            OpenDbConn();

            DAL.ComunidadProgress cComunidad = new DAL.ComunidadProgress(cDblib);

            return cComunidad.Catalogo();
        }


        public int Update(int contenedor, List<short> lista)
        {
            Guid methodOwnerID = new Guid("5956b2ad-8ce0-4688-bf8e-8d7427f1de7a");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {

                DAL.ComunidadProgress cComunidad = new DAL.ComunidadProgress(cDblib);

                int result = cComunidad.Update(contenedor, string.Join(",", lista));

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return result;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.rollbackTransaction(methodOwnerID);

                throw new IBException(103, "Ocurrió un error al actualizar pantalla mantenimiento de comunidad Progress.", ex);
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
        ~ComunidadProgress()
        {
            Dispose(false);
        }

        #endregion


    }

}
