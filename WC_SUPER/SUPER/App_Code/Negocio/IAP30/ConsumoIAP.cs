using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for ConsumoIAP
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class ConsumoIAP : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("b5b2c2a0-f673-4bb3-9dfd-ca87fe611420");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public ConsumoIAP()
			: base()
        {
			//OpenDbConn();
        }
		
		public ConsumoIAP(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas

        public int DeleteRango(int t314_idusuario, DateTime dDesde, DateTime dHasta)
        {
            Guid methodOwnerID = new Guid("0ddf5a5b-b78d-42fa-b054-3b051f8c127d");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {

                DAL.ConsumoIAP cConsumoIAP = new DAL.ConsumoIAP(cDblib);

                int result = cConsumoIAP.DeleteRango(t314_idusuario, dDesde, dHasta);

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
        public int DeleteTareaRango(int t314_idusuario, int t332_idtarea, DateTime dDesde, DateTime dHasta)
        {
            Guid methodOwnerID = new Guid("0ddf5a5b-b78d-42fa-b054-3b051f8c127d");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {

                DAL.ConsumoIAP cConsumoIAP = new DAL.ConsumoIAP(cDblib);

                int result = cConsumoIAP.DeleteTareaRango(t314_idusuario, t332_idtarea, dDesde, dHasta);

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

        public Models.ConsumoIAP SelectFecha(int t314_idusuario, DateTime t337_fecha)
        {
            OpenDbConn();

            DAL.ConsumoIAP cConsumoIAP = new DAL.ConsumoIAP(cDblib);
            return cConsumoIAP.SelectFecha(t314_idusuario, t337_fecha);
        }
        /// <summary>
        /// Obtiene el sumatorio de lo imputado por el usuario en la tarea y la máxima fecha de imputación
        /// </summary>
        /// <param name="t314_idusuario"></param>
        /// <param name="t332_idtarea"></param>
        /// <returns></returns>
        public Models.ConsumoIAP SelectAcumulados(int t314_idusuario, int t332_idtarea)
        {
            OpenDbConn();

            DAL.ConsumoIAP cConsumoIAP = new DAL.ConsumoIAP(cDblib);
            return cConsumoIAP.SelectAcumulados(t314_idusuario, t332_idtarea);
        }

        public int Insert(Models.ConsumoIAP oConsumoIAP)
        {
            Guid methodOwnerID = new Guid("9d3f6f3d-e2ab-42e4-b737-03070e71ae9d");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.ConsumoIAP cConsumoIAP = new DAL.ConsumoIAP(cDblib);

                int idConsumoIAP = cConsumoIAP.Insert(oConsumoIAP);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return idConsumoIAP;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }
        }

        internal int Update(Models.ConsumoIAP oConsumoIAP)
        {
            Guid methodOwnerID = new Guid("3571ea4a-2b1a-45bf-b67c-4e903185410d");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.ConsumoIAP cConsumoIAP = new DAL.ConsumoIAP(cDblib);

                int result = cConsumoIAP.Update(oConsumoIAP);

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

        public int Delete(int t332_idtarea, int t314_idusuario, DateTime t337_fecha)
        {
            Guid methodOwnerID = new Guid("0ddf5a5b-b78d-42fa-b054-3b051f8c127d");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {

                DAL.ConsumoIAP cConsumoIAP = new DAL.ConsumoIAP(cDblib);

                int result = cConsumoIAP.Delete(t332_idtarea, t314_idusuario, t337_fecha);

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

        public Models.ConsumoIAP Select(Int32 idtarea, Int32 idusuario, DateTime fecha)
        {
            OpenDbConn();

            DAL.ConsumoIAP cConsumoIAP = new DAL.ConsumoIAP(cDblib);
            return cConsumoIAP.Select(idtarea, idusuario, fecha);
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
        ~ConsumoIAP()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
