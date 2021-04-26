using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for ProyectoTecnicoIAPS
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class ProyectoTecnicoIAPS : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("04267bfe-b061-4b4c-9aac-b41a89c68d69");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public ProyectoTecnicoIAPS()
			: base()
        {
			//OpenDbConn();
        }
		
		public ProyectoTecnicoIAPS(sqldblib.SqlServerSP extcDblib)
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
        ~ProyectoTecnicoIAPS()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
