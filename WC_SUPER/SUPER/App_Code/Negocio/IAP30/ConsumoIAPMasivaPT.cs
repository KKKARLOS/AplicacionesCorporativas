using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for ConsumoIAPMasivaPT
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class ConsumoIAPMasivaPT : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("09b2a8cb-f784-4241-a8a2-ffa0825cdb69");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public ConsumoIAPMasivaPT()
			: base()
        {
			//OpenDbConn();
        }
		
		public ConsumoIAPMasivaPT(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas

        public List<Models.ConsumoIAPMasivaPT> Catalogo(Int32 nUsuario, Int32 nPSN, DateTime ultimoMesCerrado, Nullable<DateTime> fechaInicio, Nullable<DateTime> fechaFin)
        {
            OpenDbConn();

            DAL.ConsumoIAPMasivaPT cConsumoIAPMasivaPT = new DAL.ConsumoIAPMasivaPT(cDblib);
            return cConsumoIAPMasivaPT.Catalogo(nUsuario, nPSN, ultimoMesCerrado, fechaInicio, fechaFin);

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
        ~ConsumoIAPMasivaPT()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
