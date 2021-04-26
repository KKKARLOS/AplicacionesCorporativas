using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for TareaIAPMasiva
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class TareaIAPMasiva : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("b7c19fc1-d13d-4a81-bc11-a69283236f67");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public TareaIAPMasiva()
			: base()
        {
			//OpenDbConn();
        }
		
		public TareaIAPMasiva(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas

        public Models.TareaIAPMasiva Select(Int32 nUsuario, Int32 idTarea, DateTime dUMC, DateTime? fechaInicio, DateTime? fechaFin)
        {
            OpenDbConn();

            DAL.TareaIAPMasiva cTareaIAPMasiva = new DAL.TareaIAPMasiva(cDblib);
            return cTareaIAPMasiva.Select(nUsuario, idTarea, dUMC, fechaInicio, fechaFin);
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
        ~TareaIAPMasiva()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
