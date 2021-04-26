using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for InforImpuMen
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class InforImpuMen : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("46c516a8-759d-4679-ba71-d9c2d2022ad1");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public InforImpuMen()
			: base()
        {
			//OpenDbConn();
        }
		
		public InforImpuMen(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas

        public List<Models.InforImpuMen> Catalogo(string sProfesionales, int nDesde, int nHasta, string sTipo)
        {
            OpenDbConn();

            DAL.InforImpuMen cInforImpuMen = new DAL.InforImpuMen(cDblib);
            return cInforImpuMen.Catalogo(sProfesionales, nDesde, nHasta, sTipo);
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
        ~InforImpuMen()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
