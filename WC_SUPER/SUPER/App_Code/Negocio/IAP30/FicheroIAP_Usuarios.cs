using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for FicheroIAP_Usuarios
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class FicheroIAP_Usuarios : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("7a0a666c-39f4-4cc1-a00c-6ff3ee8f2f96");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public FicheroIAP_Usuarios()
			: base()
        {
			//OpenDbConn();
        }
		
		public FicheroIAP_Usuarios(sqldblib.SqlServerSP extcDblib)
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
        ~FicheroIAP_Usuarios()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
