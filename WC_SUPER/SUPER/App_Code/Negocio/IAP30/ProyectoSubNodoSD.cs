using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for ProyectoSubNodoSD
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class ProyectoSubNodoSD : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("f7042816-27e4-4e38-a931-3217d4d827e7");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public ProyectoSubNodoSD()
			: base()
        {
			//OpenDbConn();
        }
		
		public ProyectoSubNodoSD(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas		        
        public Models.ProyectoSubNodoSD Select(int t305_idproyectosubnodo)
        {
            OpenDbConn();

            DAL.ProyectoSubNodoSD cProyectoSubNodoSD = new DAL.ProyectoSubNodoSD(cDblib);
            return cProyectoSubNodoSD.Select(t305_idproyectosubnodo);
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
        ~ProyectoSubNodoSD()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
