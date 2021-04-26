using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for Recursos
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class Recursos : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("7e1cb7e2-2785-4b24-9bbf-905d44f19e5c");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public Recursos()
			: base()
        {
			//OpenDbConn();
        }
		
		public Recursos(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas
        
        public Models.Recursos establecerUsuarioIAP(string sCodRed, int num_empleado)
        {
            OpenDbConn();

            DAL.Recursos cRecursos = new DAL.Recursos(cDblib);
            return cRecursos.Select("",num_empleado);
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
        ~Recursos()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
