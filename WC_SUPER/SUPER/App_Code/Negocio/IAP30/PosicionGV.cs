using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for PosicionGV
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class PosicionGV : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("2550728e-050a-4fd2-a627-4311c1bb0961");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public PosicionGV()
			: base()
        {
			//OpenDbConn();
        }
		
		public PosicionGV(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas		        
        public bool EsDesplazamientoECOenVUP(int t615_iddesplazamiento)
        {
            bool bRes = false;
            OpenDbConn();

            DAL.PosicionGV cConsulta = new DAL.PosicionGV(cDblib);
            bRes = cConsulta.EsDesplazamientoECOenVUP(t615_iddesplazamiento);

            return bRes;
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
        ~PosicionGV()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
