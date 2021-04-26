using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for DESPLAZAMIENTO
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class DESPLAZAMIENTO : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("6aa262b4-0162-4e40-b92f-5305fc091aae");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public DESPLAZAMIENTO()
			: base()
        {
			//OpenDbConn();
        }
		
		public DESPLAZAMIENTO(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas
        public List<Models.DESPLAZAMIENTO> Catalogo(int t314_idusuario, DateTime fec_desde, DateTime fec_hasta, int t420_idreferencia)
        {
            OpenDbConn();

            DAL.DESPLAZAMIENTO cDESPLAZAMIENTO = new DAL.DESPLAZAMIENTO(cDblib);
            return cDESPLAZAMIENTO.Catalogo(t314_idusuario, fec_desde, fec_hasta, t420_idreferencia);

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
        ~DESPLAZAMIENTO()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
