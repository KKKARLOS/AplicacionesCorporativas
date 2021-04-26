using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for NodoTarea
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class NodoTarea : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("5ec65860-2458-4125-91e4-c09a39a8b566");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public NodoTarea()
			: base()
        {
			//OpenDbConn();
        }
		
		public NodoTarea(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas		        
        public Models.NodoTarea Select(int t332_idtarea)
        {
            OpenDbConn();

            DAL.NodoTarea cNodoTarea = new DAL.NodoTarea(cDblib);
            return cNodoTarea.Select(t332_idtarea);
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
        ~NodoTarea()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
