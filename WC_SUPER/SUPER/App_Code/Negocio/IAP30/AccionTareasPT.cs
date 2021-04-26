using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for AccionTareasPT
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class AccionTareasPT : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("4690064a-bf70-40e8-8a3c-f8cb7e990587");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public AccionTareasPT()
			: base()
        {
			//OpenDbConn();
        }
		
		public AccionTareasPT(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas		        
        public List<Models.AccionTareasPT> Catalogo(Int32 idAccion)
        {
            OpenDbConn();

            DAL.AccionTareasPT cAccionTareas = new DAL.AccionTareasPT(cDblib);
            List<Models.AccionTareasPT> oLista = cAccionTareas.Catalogo(idAccion);
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
        ~AccionTareasPT()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
