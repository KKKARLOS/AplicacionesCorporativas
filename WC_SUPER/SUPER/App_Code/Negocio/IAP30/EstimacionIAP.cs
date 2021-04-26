using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for EstimacionIAP
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class EstimacionIAP : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("8ecd140e-fb13-4129-ae0f-d5c4a7e5e8a3");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public EstimacionIAP()
			: base()
        {
			//OpenDbConn();
        }
		
		public EstimacionIAP(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas		        

        public int Update(Models.EstimacionIAP oEstimacionIAP)
        {
            Guid methodOwnerID = new Guid("aec8a2f1-6edf-4fe9-a1cc-a2323ba1d7e3");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.EstimacionIAP cEstimacionIAP = new DAL.EstimacionIAP(cDblib);

                int result = cEstimacionIAP.Update(oEstimacionIAP);

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
        ~EstimacionIAP()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
