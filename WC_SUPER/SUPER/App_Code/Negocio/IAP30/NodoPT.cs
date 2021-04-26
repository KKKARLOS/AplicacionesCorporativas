using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for NodoPT
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class NodoPT : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("61269de9-02ff-433e-aefe-6dbd66a149f4");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public NodoPT()
			: base()
        {
			//OpenDbConn();
        }
		
		public NodoPT(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas		        
        public Models.NodoPT Select(int t331_idpt)
        {
            OpenDbConn();

            DAL.NodoPT cNodoPT = new DAL.NodoPT(cDblib);
            return cNodoPT.Select(t331_idpt);
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
        ~NodoPT()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
