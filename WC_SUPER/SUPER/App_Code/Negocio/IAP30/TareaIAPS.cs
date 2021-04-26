using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for TareaIAPS
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class TareaIAPS : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("372985b7-288b-4acf-9a3f-06270925e338");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public TareaIAPS()
			: base()
        {
			//OpenDbConn();
        }
		
		public TareaIAPS(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas	        

        public Models.TareaIAPS Select(int t332_idtarea)
        {
            OpenDbConn();

            DAL.TareaIAPS cTareaIAPS = new DAL.TareaIAPS(cDblib);
            return cTareaIAPS.Select(t332_idtarea);
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
        ~TareaIAPS()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
