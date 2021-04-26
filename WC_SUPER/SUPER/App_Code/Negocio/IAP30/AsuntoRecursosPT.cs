using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for AsuntoRecursosPT
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class AsuntoRecursosPT : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("6a71e300-a4ff-4d5d-96c9-fa023b290959");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public AsuntoRecursosPT()
			: base()
        {
			//OpenDbConn();
        }
		
		public AsuntoRecursosPT(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        public List<Models.AsuntoRecursosPT> Catalogo(Models.AsuntoRecursosPT oAsuntosRecursos)
        {
            OpenDbConn();

            DAL.AsuntoRecursosPT cAsuntoRecursos = new DAL.AsuntoRecursosPT(cDblib);
            List<Models.AsuntoRecursosPT> oLista = cAsuntoRecursos.Catalogo(oAsuntosRecursos);
            return oLista;
        }
		
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
        ~AsuntoRecursosPT()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
