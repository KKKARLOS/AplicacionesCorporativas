using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for NodoPSN
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class NodoPSN : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("91869f98-fd6a-44c0-bc59-77e0034d7cc8");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public NodoPSN()
			: base()
        {
			//OpenDbConn();
        }
		
		public NodoPSN(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas		        
        public Models.NodoPSN Select(int t305_idproyectosubnodo)
        {
            OpenDbConn();

            DAL.NodoPSN cNodoPSN = new DAL.NodoPSN(cDblib);
            return cNodoPSN.Select(t305_idproyectosubnodo);
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
        ~NodoPSN()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
