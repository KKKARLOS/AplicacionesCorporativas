using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for ConsumoAgendaDia
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class ConsumoAgendaDia : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("3703fc30-62f1-4eb6-bfb2-bbc93868a912");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public ConsumoAgendaDia()
			: base()
        {
			//OpenDbConn();
        }
		
		public ConsumoAgendaDia(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas		        

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
        ~ConsumoAgendaDia()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
