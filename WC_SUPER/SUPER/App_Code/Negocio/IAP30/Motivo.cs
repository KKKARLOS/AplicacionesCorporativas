using System;
using System.Collections.Generic;
using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for Motivo
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class Motivo : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("627d4237-57a9-432d-82e6-3aada2bb27f6");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public Motivo()
			: base()
        {
			//OpenDbConn();
        }
		
		public Motivo(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas

        public List<Models.Motivo> Catalogo()
        {
            OpenDbConn();
            DAL.Motivo cMotivo = new DAL.Motivo(cDblib);
            List<Models.Motivo> oLista = cMotivo.Catalogo();
            return oLista;
        }		        

        public Models.Motivo Select(byte t423_idmotivo)
        {
            OpenDbConn();

            DAL.Motivo cMotivo = new DAL.Motivo(cDblib);
            return cMotivo.Select(t423_idmotivo);
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
        ~Motivo()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
