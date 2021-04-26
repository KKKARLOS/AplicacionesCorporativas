using System;
using System.Collections.Generic;

using IB.SUPER.APP.DAL;
using IB.SUPER.APP.Models;
using SUPER.Capa_Negocio;

namespace IB.SUPER.APP.BLL
{
    public class ProyectoEconomico
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("11928D55-09CC-4C77-BF01-9B191B396979");
        private bool disposed = false;

        #endregion

        #region Constructor

        public ProyectoEconomico()
            : base()
        {
            //OpenDbConn();
        }

        public ProyectoEconomico(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas

        public int Insert(Models.ProyectoEconomico oPE)
        {
            Guid methodOwnerID = new Guid("C2397722-CAD0-404A-B255-2D5D98F80347");
            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.ProyectoEconomico cPE = new DAL.ProyectoEconomico(cDblib);

                int idPE = cPE.GenerarProyecto(oPE);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return idPE;
            }
            catch (Exception ex)
            {
                //rollback
                //if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.rollbackTransaction(methodOwnerID);
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.rollbackTransaction(methodOwnerID);
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
        ~ProyectoEconomico()
        {
            Dispose(false);
        }

        #endregion

    }
}