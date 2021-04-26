using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using IB.Progress.Shared;
using IB.Progress.DAL;
using IB.Progress.Models;
using System.Data;


namespace IB.Progress.BLL
{
    public class BulaAvisos : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("681dc135-affb-4c9c-a00b-4da640dd4185");
        private bool disposed = false;

        #endregion

        #region Constructor

        public BulaAvisos()
            : base()
        {
            //OpenDbConn();
        }

        public BulaAvisos(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas

        
        public void Catalogo()
        {
            OpenDbConn();

            DAL.BulaAvisos cComunidadProgress = new DAL.BulaAvisos(cDblib);
            //return cComunidadProgress.Catalogo();

        }


        public Models.ComunidadProgress catalogo()
        {
            OpenDbConn();

            DAL.BulaAvisos cBulaAvisos = new DAL.BulaAvisos(cDblib);

            return cBulaAvisos.Catalogo();
        }


      

        public int Update(int contenedor, List<short> lista)
        {
            Guid methodOwnerID = new Guid("5956b2ad-8ce0-4688-bf8e-8d7427f1de7a");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {

                DAL.BulaAvisos cBulaAvisos = new DAL.BulaAvisos(cDblib);

                int result = cBulaAvisos.Update(contenedor, string.Join(",", lista));

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return result;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.rollbackTransaction(methodOwnerID);

                throw new IBException(107, "Ocurrió un error al actualizar pantalla mantenimiento de bula de avisos.", ex);
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
        ~BulaAvisos()
        {
            Dispose(false);
        }

        #endregion


    }

}
