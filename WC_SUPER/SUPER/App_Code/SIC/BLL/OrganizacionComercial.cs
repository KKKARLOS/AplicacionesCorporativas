using System;
using System.Collections.Generic;


using IB.SUPER.SIC.DAL;
using IB.SUPER.SIC.Models;

/// <summary>
/// Summary description for OrganizacionComercial
/// </summary>
namespace IB.SUPER.SIC.BLL
{
    public class OrganizacionComercial : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("4dc006a4-88d8-410c-819f-b61dbae7868a");
        private bool disposed = false;

        #endregion

        #region Constructor

        public OrganizacionComercial()
            : base()
        {
            //OpenDbConn();
        }

        public OrganizacionComercial(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas

        public List<Models.OrganizacionComercial> Catalogo(bool bSoloActivos)
        {
            OpenDbConn();

            bool? ta212_activa=null;
            if (bSoloActivos) ta212_activa = true;

            DAL.OrganizacionComercial cOrganizacionComercial = new DAL.OrganizacionComercial(cDblib);
            return cOrganizacionComercial.Catalogo(ta212_activa);

        }

        internal int Insert(Models.OrganizacionComercial oOrganizacionComercial)
        {
            Guid methodOwnerID = new Guid("37d0282a-d3d0-41cc-bb8a-f683e97daceb");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.OrganizacionComercial cOrganizacionComercial = new DAL.OrganizacionComercial(cDblib);

                int idOrganizacionComercial = cOrganizacionComercial.Insert(oOrganizacionComercial);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return idOrganizacionComercial;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }
        }

        internal int Update(Models.OrganizacionComercial oOrganizacionComercial)
        {
            Guid methodOwnerID = new Guid("37ecb396-efd3-4acc-9eba-65d38bc92217");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.OrganizacionComercial cOrganizacionComercial = new DAL.OrganizacionComercial(cDblib);

                int result = cOrganizacionComercial.Update(oOrganizacionComercial);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return result;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }
        }

        internal int Delete(Int32 ta212_idOrganizacionComercial)
        {
            Guid methodOwnerID = new Guid("6dc63cd8-3016-4146-9538-c08725a02366");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {

                DAL.OrganizacionComercial cOrganizacionComercial = new DAL.OrganizacionComercial(cDblib);

                int result = cOrganizacionComercial.Delete(ta212_idOrganizacionComercial);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return result;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }
        }

        internal Models.OrganizacionComercial Select(Int32 ta212_idOrganizacionComercial)
        {
            OpenDbConn();

            DAL.OrganizacionComercial cOrganizacionComercial = new DAL.OrganizacionComercial(cDblib);
            return cOrganizacionComercial.Select(ta212_idOrganizacionComercial);
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
        ~OrganizacionComercial()
        {
            Dispose(false);
        }

        #endregion


    }

}
