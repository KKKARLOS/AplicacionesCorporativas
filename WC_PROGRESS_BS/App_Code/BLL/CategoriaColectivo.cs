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
    public class CategoriaColectivo : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("d268f090-46af-4335-8f02-a36e6b4ee690");
        private bool disposed = false;

        #endregion

        #region Constructor

        public CategoriaColectivo()
            : base()
        {
            //OpenDbConn();
        }

        public CategoriaColectivo(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas

       

        public Models.CategoriaColectivo catalogo()
        {
            OpenDbConn();

            DAL.CategoriaColectivo cCategoriaColectivo = new DAL.CategoriaColectivo(cDblib);

            return cCategoriaColectivo.Catalogo();
        }


        public int Update(int t935_idcategoriaprofesional, int t941_idcolectivo)
        {
            //Guid methodOwnerID = new Guid("acaa197e-2313-4e2f-b9a0-5fe5afc1850b");

            OpenDbConn();

            //if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {

                DAL.CategoriaColectivo cCategoriaColectivo = new DAL.CategoriaColectivo(cDblib);

                int result = cCategoriaColectivo.Update(t935_idcategoriaprofesional, t941_idcolectivo);

                ////Finalizar transacción 
                //if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return result;
            }
            catch (Exception ex)
            {
                //rollback
                //if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.rollbackTransaction(methodOwnerID);

                throw new IBException(103, "Ocurrió un error al actualizar pantalla de Categoría/Colectivo.", ex);
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
        ~CategoriaColectivo()
        {
            Dispose(false);
        }

        #endregion


    }

}
