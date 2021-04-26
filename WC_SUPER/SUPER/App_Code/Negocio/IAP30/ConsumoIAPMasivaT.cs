using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for ConsumoIAPMasivaT
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class ConsumoIAPMasivaT : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("7cc37271-ad95-49d9-b30b-bc6783ad886c");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public ConsumoIAPMasivaT()
			: base()
        {
			//OpenDbConn();
        }
		
		public ConsumoIAPMasivaT(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas

        public List<Models.ConsumoIAPMasivaT> Catalogo(Int32 nUsuario, Int32 nPT, DateTime ultimoMesCerrado, Nullable<DateTime> fechaInicio, Nullable<DateTime> fechaFin)
        {
            OpenDbConn();

            DAL.ConsumoIAPMasivaT cConsumoIAPMasivaT = new DAL.ConsumoIAPMasivaT(cDblib);
            return cConsumoIAPMasivaT.Catalogo(nUsuario, nPT, ultimoMesCerrado, fechaInicio, fechaFin);

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
        ~ConsumoIAPMasivaT()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
