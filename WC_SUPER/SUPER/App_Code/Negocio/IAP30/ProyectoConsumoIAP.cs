using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for ProyectoConsumoIAP
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class ProyectoConsumoIAP : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("f76f58bc-9c69-4f7d-adcd-c3e79a7b8e5d");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public ProyectoConsumoIAP()
			: base()
        {
			//OpenDbConn();
        }
		
		public ProyectoConsumoIAP(sqldblib.SqlServerSP extcDblib)
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
        ~ProyectoConsumoIAP()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
