using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for AsuntoRecursos
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class AsuntoRecursos : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("f28f6052-b2a0-4c32-a034-6957421e46a9");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public AsuntoRecursos()
			: base()
        {
			//OpenDbConn();
        }
		
		public AsuntoRecursos(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas		        
        public List<Models.AsuntoRecursos> Catalogo(Models.AsuntoRecursos oAsuntosRecursos)
        {
            OpenDbConn();

            DAL.AsuntoRecursos cAsuntoRecursos = new DAL.AsuntoRecursos(cDblib);
            List<Models.AsuntoRecursos> oLista = cAsuntoRecursos.Catalogo(oAsuntosRecursos);
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
        ~AsuntoRecursos()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
