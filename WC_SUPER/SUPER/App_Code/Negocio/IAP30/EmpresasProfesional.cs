using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for EmpresasProfesional
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class EmpresasProfesional : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("cbe00fce-b7c6-49d1-a42d-ba7de25591c2");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public EmpresasProfesional()
			: base()
        {
			//OpenDbConn();
        }
		
		public EmpresasProfesional(sqldblib.SqlServerSP extcDblib)
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
        ~EmpresasProfesional()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
