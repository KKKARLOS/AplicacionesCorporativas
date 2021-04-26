using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for AsuntoEstado
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class AsuntoEstado : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("d64a59a6-dc09-4b46-8aa5-3523af8738ba");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public AsuntoEstado()
			: base()
        {
			//OpenDbConn();
        }
		
		public AsuntoEstado(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas		
        public List<Models.AsuntoEstado> Catalogo(int t382_idasunto)
        {
            OpenDbConn();

            DAL.AsuntoEstado cAsuntoEstado = new DAL.AsuntoEstado(cDblib);
            List<Models.AsuntoEstado> oLista = cAsuntoEstado.Catalogo(t382_idasunto);
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
        ~AsuntoEstado()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
