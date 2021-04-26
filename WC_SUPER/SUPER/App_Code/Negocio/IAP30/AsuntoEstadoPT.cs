using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for AsuntoEstadoPT
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class AsuntoEstadoPT : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("0a3bf79f-b03e-4982-926a-ce6a21326407");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public AsuntoEstadoPT()
			: base()
        {
			//OpenDbConn();
        }
		
		public AsuntoEstadoPT(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas		        
        public List<Models.AsuntoEstadoPT> Catalogo(int t409_idasunto)
        {
            OpenDbConn();

            DAL.AsuntoEstadoPT cAsuntoEstado = new DAL.AsuntoEstadoPT(cDblib);
            List<Models.AsuntoEstadoPT> oLista = cAsuntoEstado.Catalogo(t409_idasunto);
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
        ~AsuntoEstadoPT()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
