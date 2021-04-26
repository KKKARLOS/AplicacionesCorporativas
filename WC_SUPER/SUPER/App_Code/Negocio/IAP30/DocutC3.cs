using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for DocutC3
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class DocutC3 : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("970382ab-12d9-48e8-8156-67f48fc2875e");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public DocutC3()
			: base()
        {
			//OpenDbConn();
        }
		
		public DocutC3(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas

        public List<Models.DocutC3> Catalogo(Int32 idUsuarioAutorizado, Int32 idTarea)
        {
            OpenDbConn();

            DAL.DocutC3 cDocutC3 = new DAL.DocutC3(cDblib);
            return cDocutC3.Catalogo(idUsuarioAutorizado, idTarea);

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
        ~DocutC3()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
