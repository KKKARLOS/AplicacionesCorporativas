using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for ConsumoIAPDia
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class ConsumoIAPDia : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("afb7f6d6-d4e0-4a0e-9dff-587dd5a3045a");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public ConsumoIAPDia()
			: base()
        {
			//OpenDbConn();
        }
		
		public ConsumoIAPDia(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas
        public Models.ConsumoIAPDia Select(int idUser, DateTime dtFecha, int idTarea)
        {
            OpenDbConn();

            try
            {
                DAL.ConsumoIAPDia cConsumo = new DAL.ConsumoIAPDia(cDblib);
                return cConsumo.Select(idUser, dtFecha, idTarea);
            }

            catch (Exception ex)
            {
                throw ex;
            }
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
        ~ConsumoIAPDia()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
