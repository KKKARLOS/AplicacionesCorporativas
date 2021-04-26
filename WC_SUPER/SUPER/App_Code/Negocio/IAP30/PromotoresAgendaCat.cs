using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for PromotoresAgendaCat
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class PromotoresAgendaCat : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("00c3ae66-d842-46ee-9033-c63774691e02");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public PromotoresAgendaCat()
			: base()
        {
			//OpenDbConn();
        }
		
		public PromotoresAgendaCat(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas		
        
        public List<Models.PromotoresAgendaCat> CatalogoPromotoresEvento(Models.PromotoresAgendaCat oPromotoresAgendaCat)
        {
            OpenDbConn();

            DAL.PromotoresAgendaCat cPromotoresAgendaCat = new DAL.PromotoresAgendaCat(cDblib);
            return cPromotoresAgendaCat.Catalogo(oPromotoresAgendaCat);

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
        ~PromotoresAgendaCat()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
