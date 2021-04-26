using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for Moneda
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class Moneda : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("6eb1694d-40b9-45d8-8838-dbfc61df13a7");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public Moneda()
			: base()
        {
			//OpenDbConn();
        }
		
		public Moneda(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas
        public List<Models.Moneda> Catalogo()
        {
            OpenDbConn();
            DAL.Moneda cMoneda = new DAL.Moneda(cDblib);
            List<Models.Moneda> oLista = cMoneda.Catalogo();
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
        ~Moneda()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
