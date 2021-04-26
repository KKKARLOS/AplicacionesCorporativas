using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for AccionRecursosPT
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class AccionRecursosPT : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("67219e52-5b89-48c2-9570-63947d0ab0db");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public AccionRecursosPT()
			: base()
        {
			//OpenDbConn();
        }
		
		public AccionRecursosPT(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas		
        public List<Models.AccionRecursosPT> Catalogo(Models.AccionRecursosPT oAccionRecursos)
        {
            OpenDbConn();

            DAL.AccionRecursosPT cAccionRecursos = new DAL.AccionRecursosPT(cDblib);
            List<Models.AccionRecursosPT> oLista = cAccionRecursos.Catalogo(oAccionRecursos);
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
        ~AccionRecursosPT()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
