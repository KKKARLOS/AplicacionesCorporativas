using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for ModalidadContrato
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class ModalidadContrato : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("4492da51-a485-424a-8355-ef1b92c60354");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public ModalidadContrato()
			: base()
        {
			//OpenDbConn();
        }
		
		public ModalidadContrato(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas

        public List<Models.ModalidadContrato> Catalogo(Nullable<byte> t316_idmodalidad, string t316_denominacion, bool bTodos, byte nOrden, byte nAscDesc)
        {
            OpenDbConn();

            DAL.ModalidadContrato cModalidadContrato = new DAL.ModalidadContrato(cDblib);
            return cModalidadContrato.Catalogo(t316_idmodalidad, t316_denominacion, bTodos, nOrden, nAscDesc);

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
        ~ModalidadContrato()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
