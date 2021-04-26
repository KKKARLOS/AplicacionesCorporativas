using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for TareaBitacora
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class TareaBitacora : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("a7f7e3d6-0601-4140-bbfd-ffdbd3db2ba2");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public TareaBitacora()
			: base()
        {
			//OpenDbConn();
        }
		
		public TareaBitacora(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas		        
        public List<Models.TareaBitacora> Catalogo(int idTarea)
        {
            OpenDbConn();

            DAL.TareaBitacora cTareas = new DAL.TareaBitacora(cDblib);
            return cTareas.Catalogo(idTarea);
        }

        public Models.TareaBitacora Select(int idTarea)
        {
            OpenDbConn();

            DAL.TareaBitacora cTarea = new DAL.TareaBitacora(cDblib);
            return cTarea.Select(idTarea);
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
        ~TareaBitacora()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
