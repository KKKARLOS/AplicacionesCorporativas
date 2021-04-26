using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for ValidarTareaAgenda
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class ValidarTareaAgenda : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("bd260ef7-8acc-4251-a794-3e44e8dbbf05");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public ValidarTareaAgenda()
			: base()
        {
			//OpenDbConn();
        }
		
		public ValidarTareaAgenda(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas		 
       
        public Models.ValidarTareaAgenda Select(Int32 idFicepi, Int32 idTarea)
        {
            OpenDbConn();

            DAL.ValidarTareaAgenda cValidarTareaAgenda = new DAL.ValidarTareaAgenda(cDblib);
            return cValidarTareaAgenda.Select(idFicepi, idTarea);
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
        ~ValidarTareaAgenda()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
