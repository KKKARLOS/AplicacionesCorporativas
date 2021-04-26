using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for FiguraNodo
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class FiguraNodo : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("6692a77a-dbf3-4378-9348-6309679f86a1");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public FiguraNodo()
			: base()
        {
			//OpenDbConn();
        }
		
		public FiguraNodo(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas		        
        public List<Models.FiguraNodo> Catalogo(Models.FiguraNodo mCriterios)
        {
            OpenDbConn();

            DAL.FiguraNodo cConsulta = new DAL.FiguraNodo(cDblib);
            return cConsulta.Catalogo(mCriterios);
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
        ~FiguraNodo()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
