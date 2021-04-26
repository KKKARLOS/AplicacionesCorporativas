using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for Horizontal
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class Horizontal : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("ddfd40a0-aa18-434c-933c-2e125ded15bd");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public Horizontal()
			: base()
        {
			//OpenDbConn();
        }
		
		public Horizontal(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas
		
		internal List<Models.Horizontal> Catalogo()
        {
            OpenDbConn();

			DAL.Horizontal cHorizontal = new DAL.Horizontal(cDblib);
            return cHorizontal.Catalogo(null, "", 2, 0);

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
        ~Horizontal()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
