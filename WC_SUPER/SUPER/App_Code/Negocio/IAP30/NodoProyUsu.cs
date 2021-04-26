using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for NodoProyUsu
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class NodoProyUsu : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("56fc112d-9dc9-43b2-a897-f8ab32bd017f");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public NodoProyUsu()
			: base()
        {
			//OpenDbConn();
        }
		
		public NodoProyUsu(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas

        public List<Models.NodoProyUsu> Catalogo(int nUsuario, bool bMostrarBitacoricos, bool bSoloActivos)
        {
            OpenDbConn();

            DAL.NodoProyUsu cNodoProyUsu = new DAL.NodoProyUsu(cDblib);
            return cNodoProyUsu.Catalogo(nUsuario, bMostrarBitacoricos, bSoloActivos);

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
        ~NodoProyUsu()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
