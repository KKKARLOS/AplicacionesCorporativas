using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IB.Progress.Shared;

namespace IB.Progress.BLL
{
    public class GestionarIncorporaciones : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("9999c6df-614d-4b22-b255-136bb1e25d46");
        private bool disposed = false;

        #endregion

        #region Constructor

        public GestionarIncorporaciones()
            : base()
        {
            //OpenDbConn();
        }

        public GestionarIncorporaciones(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas

       
        public int RechazarIncorporacion(List<string> listapeticiones, string MotivoRechazo)
        {
            Guid methodOwnerID = new Guid("40ac43e3-e371-4008-b51e-0c5518db7cbd");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                
                DAL.GestionarIncorporaciones cGestionarIncorporaciones = new DAL.GestionarIncorporaciones(cDblib);

                int idGestionarIncorporaciones = cGestionarIncorporaciones.RechazarIncorporacion(string.Join(",", listapeticiones), MotivoRechazo);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return idGestionarIncorporaciones;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.rollbackTransaction(methodOwnerID);

                throw new IBException(119, "Ocurrió un error al intentar rechazar la incorporación del profesional.", ex);
            }
        }



        public void AceptarIncorporacion(int t001_idficepi, List<string> listapeticiones )
        {
            Guid methodOwnerID = new Guid("c47d884a-1a96-4e58-b774-4e19693ae77a");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {

                DAL.GestionarIncorporaciones cGestionarIncorporaciones = new DAL.GestionarIncorporaciones(cDblib);

                cGestionarIncorporaciones.AceptarIncorporacion(t001_idficepi, string.Join(",", listapeticiones));
                
                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return;
                
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.rollbackTransaction(methodOwnerID);

                throw new IBException(120, "Ocurrió un error al intentar aceptar la incorporación del profesional.", ex);
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
        ~GestionarIncorporaciones()
        {
            Dispose(false);
        }

        #endregion


    }
}