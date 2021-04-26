using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for ProyectoTecnico
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class ProyectoTecnico : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("4547cff9-5e34-427f-9825-6a11b983cf99");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public ProyectoTecnico()
			: base()
        {
			//OpenDbConn();
        }
		
		public ProyectoTecnico(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas		        
        public Models.ProyectoTecnico Select(int t331_idpt)
        {
            OpenDbConn();

            DAL.ProyectoTecnico cPT = new DAL.ProyectoTecnico(cDblib);
            return cPT.Select(t331_idpt);
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
        ~ProyectoTecnico()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
