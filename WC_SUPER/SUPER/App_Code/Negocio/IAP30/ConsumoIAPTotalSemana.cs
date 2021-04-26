using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for ConsumoIAPTotalSemana
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class ConsumoIAPTotalSemana : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("d94c26bf-6f75-4a4b-9a9e-d3ed191c8540");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public ConsumoIAPTotalSemana()
			: base()
        {
			//OpenDbConn();
        }
		
		public ConsumoIAPTotalSemana(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas

        public Models.ConsumoIAPTotalSemana ObtenerConsumosTotalesSemanaIAP(int nEmpleado, DateTime dDesde, DateTime dHasta)
        {
            OpenDbConn();

            DAL.ConsumoIAPTotalSemana cConsumoIAPTotalSemana = new DAL.ConsumoIAPTotalSemana(cDblib);
            return cConsumoIAPTotalSemana.ObtenerConsumosTotalesSemanaIAP(nEmpleado, dDesde.Date, dHasta.Date);

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
        ~ConsumoIAPTotalSemana()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
