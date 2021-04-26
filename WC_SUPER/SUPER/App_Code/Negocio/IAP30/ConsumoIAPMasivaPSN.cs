using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for ConsumoIAPMasivaPSN
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class ConsumoIAPMasivaPSN : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("9bdcb9f8-4d5c-4132-8035-c9e0e7af7274");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public ConsumoIAPMasivaPSN()
			: base()
        {
			//OpenDbConn();
        }
		
		public ConsumoIAPMasivaPSN(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas

        public List<Models.ConsumoIAPMasivaPSN> Catalogo(Int32 nUsuario, DateTime ultimoMesCerrado, Nullable<DateTime> fehcaInicio, Nullable<DateTime> fechaFin)
        {
            OpenDbConn();

            DAL.ConsumoIAPMasivaPSN cConsumoIAPMasivaPSN = new DAL.ConsumoIAPMasivaPSN(cDblib);
            return cConsumoIAPMasivaPSN.Catalogo(nUsuario, ultimoMesCerrado, fehcaInicio, fechaFin);

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
        ~ConsumoIAPMasivaPSN()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
