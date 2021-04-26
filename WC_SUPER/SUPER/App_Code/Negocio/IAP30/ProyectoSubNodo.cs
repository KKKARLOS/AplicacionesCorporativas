using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for ProyectoSubNodo
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class ProyectoSubNodo : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("79ab03fc-42db-4415-9944-8d76270a8b4f");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public ProyectoSubNodo()
			: base()
        {
			//OpenDbConn();
        }
		
		public ProyectoSubNodo(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas		        
        public Models.ProyectoSubNodo Select(int idPSN)
        {
            OpenDbConn();

            DAL.ProyectoSubNodo cProy = new DAL.ProyectoSubNodo(cDblib);
            return cProy.Select(idPSN);
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
        ~ProyectoSubNodo()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
