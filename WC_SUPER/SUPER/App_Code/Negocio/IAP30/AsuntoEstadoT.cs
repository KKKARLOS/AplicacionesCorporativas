using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for AsuntoEstadoT
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class AsuntoEstadoT : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("b72806cd-aafe-4f83-a917-4e26d075b0f0");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public AsuntoEstadoT()
			: base()
        {
			//OpenDbConn();
        }
		
		public AsuntoEstadoT(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas
        public List<Models.AsuntoEstadoT> Catalogo(int t600_idasunto)
        {
            OpenDbConn();

            DAL.AsuntoEstadoT cAsuntoEstado = new DAL.AsuntoEstadoT(cDblib);
            List<Models.AsuntoEstadoT> oLista = cAsuntoEstado.Catalogo(t600_idasunto);
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
        ~AsuntoEstadoT()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
