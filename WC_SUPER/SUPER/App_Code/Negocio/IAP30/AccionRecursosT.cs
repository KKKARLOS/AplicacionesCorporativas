using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for AccionRecursosT
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class AccionRecursosT : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("82822004-68af-47c3-a10c-dee5a7642a03");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public AccionRecursosT()
			: base()
        {
			//OpenDbConn();
        }
		
		public AccionRecursosT(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas		
        public List<Models.AccionRecursosT> Catalogo(Models.AccionRecursosT oAccionRecursos)
        {
            OpenDbConn();

            DAL.AccionRecursosT cAccionRecursos = new DAL.AccionRecursosT(cDblib);
            List<Models.AccionRecursosT> oLista = cAccionRecursos.Catalogo(oAccionRecursos);
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
        ~AccionRecursosT()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
