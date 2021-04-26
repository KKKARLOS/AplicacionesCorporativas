using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for AccionRecursos
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class AccionRecursos : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("16acdd7f-298b-4c5b-8fe4-a2500c84687e");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public AccionRecursos()
			: base()
        {
			//OpenDbConn();
        }
		
		public AccionRecursos(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas		       
        public List<Models.AccionRecursos> Catalogo(Models.AccionRecursos oAccionRecursos)
        {
            OpenDbConn();

            DAL.AccionRecursos cAccionRecursos = new DAL.AccionRecursos(cDblib);
            List<Models.AccionRecursos> oLista = cAccionRecursos.Catalogo(oAccionRecursos);
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
        ~AccionRecursos()
        {
            Dispose(false);
        }
		
        #endregion
   
    }
}
