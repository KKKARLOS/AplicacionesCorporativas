using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for AsuntoCat
/// </summary>
namespace IB.SUPER.IAP30.BLL
{
    public class AsuntoCat : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("26E0A128-0B2C-46EC-865E-72C173B4673A");
        private bool disposed = false;

        #endregion

        #region Constructor

        public AsuntoCat()
            : base()
        {
            //OpenDbConn();
        }

        public AsuntoCat(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas
        public List<Models.AsuntoCat> Catalogo(int idPSN, Nullable<int> idTipoAsunto, Nullable<byte> idEstado)
        {
            OpenDbConn();

            DAL.AsuntoCat cConsulta = new DAL.AsuntoCat(cDblib);
            return cConsulta.Catalogo(idPSN, idTipoAsunto, idEstado);
        }

        public void BorrarAsuntos(List<Models.AsuntoCat> lista)
        {
            Guid methodOwnerID = new Guid("24F47F9E-3364-41FF-A033-51DCB288D780");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.AsuntoCat cAsunto = new DAL.AsuntoCat(cDblib);
                foreach (Models.AsuntoCat asunto in lista)
                {
                    cAsunto.Borrar(asunto.idAsunto);
                }
                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

            }
            catch (Exception ex)
            {//rollback
                if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.rollbackTransaction(methodOwnerID);

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
        ~AsuntoCat()
        {
            Dispose(false);
        }

        #endregion


    }

}
