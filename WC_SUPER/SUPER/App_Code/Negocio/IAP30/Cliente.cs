using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for Cliente
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class Cliente : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("063e5a4a-5e6d-4760-9638-a6468dcc07d7");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public Cliente()
			: base()
        {
			//OpenDbConn();
        }
		
		public Cliente(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas

        public List<Models.Cliente> Catalogo(string t302_denominacion, string sTipoBusqueda, bool bSoloActivos, bool bInternos)
        {
            OpenDbConn();

			DAL.Cliente cCliente = new DAL.Cliente(cDblib);
            return cCliente.Catalogo(t302_denominacion, sTipoBusqueda, bSoloActivos, bInternos, null);

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
        ~Cliente()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
