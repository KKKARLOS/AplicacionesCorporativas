using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for AccionTareas
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class AccionTareas : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("db063ae5-e00e-4539-9169-88227d8f6442");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public AccionTareas()
			: base()
        {
			//OpenDbConn();
        }
		
		public AccionTareas(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas		        
        public List<Models.AccionTareas> Catalogo(Int32 idAccion)
        {
            OpenDbConn();

            DAL.AccionTareas cAccionTareas = new DAL.AccionTareas(cDblib);
            List<Models.AccionTareas> oLista = cAccionTareas.Catalogo(idAccion);
            return oLista;
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
        ~AccionTareas()
        {
            Dispose(false);
        }
		
        #endregion
    
    }
}
