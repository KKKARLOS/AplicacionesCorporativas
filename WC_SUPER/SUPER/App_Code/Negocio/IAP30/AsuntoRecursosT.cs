using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for AsuntoRecursosT
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class AsuntoRecursosT : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("0b0deadc-2dc1-41f1-8b68-764e207b6479");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public AsuntoRecursosT()
			: base()
        {
			//OpenDbConn();
        }
		
		public AsuntoRecursosT(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas		        
        public List<Models.AsuntoRecursosT> Catalogo(Models.AsuntoRecursosT oAsuntosRecursos)
        {
            OpenDbConn();

            DAL.AsuntoRecursosT cAsuntoRecursos = new DAL.AsuntoRecursosT(cDblib);
            List<Models.AsuntoRecursosT> oLista = cAsuntoRecursos.Catalogo(oAsuntosRecursos);
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
        ~AsuntoRecursosT()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
