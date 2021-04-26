using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for Calendario
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class Calendario : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("dfb4b728-4796-4f3c-8702-1bf4829e6b91");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public Calendario()
			: base()
        {
			//OpenDbConn();
        }
		
		public Calendario(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas

        public Models.Calendario getCalendario(Int32 idCal, Int32 nAnno)
        {
            OpenDbConn();

            DAL.Calendario cCalendario = new DAL.Calendario(cDblib);
            return cCalendario.getCalendario(idCal, nAnno);
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
        ~Calendario()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
