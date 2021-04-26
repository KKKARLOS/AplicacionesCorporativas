using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for TipoAsunto
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class TipoAsunto : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("ee36aa8c-fc12-4709-8ace-fa11ad13f0e1");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public TipoAsunto()
			: base()
        {
			//OpenDbConn();
        }
		
		public TipoAsunto(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        
        #region Funciones públicas
        public List<Models.TipoAsunto> Catalogo()
        {
            OpenDbConn();
            DAL.TipoAsunto cTipoAsunto = new DAL.TipoAsunto(cDblib);
            List<Models.TipoAsunto> oLista = cTipoAsunto.Catalogo("",null,null,3,0);
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
        ~TipoAsunto()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
