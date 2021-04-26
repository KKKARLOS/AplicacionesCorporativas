using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for Contrato
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class Contrato : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("f6dd5f00-068e-4596-ab84-a7fab90be953");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public Contrato()
			: base()
        {
			//OpenDbConn();
        }
		
		public Contrato(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas
		
		public List<Models.Contrato> Catalogo(int t314_idusuario, bool bMostrarTodos, Nullable<int> t306_idcontrato, string t377_denominacion, string sTipoBusq, Nullable<int> t302_idcliente, bool Admin)
        {
            OpenDbConn();

			DAL.Contrato cContrato = new DAL.Contrato(cDblib);
            
            if (Admin) {
                return cContrato.CatalogoADM(t314_idusuario, null, bMostrarTodos, t306_idcontrato, t377_denominacion, sTipoBusq, t302_idcliente);
            } else {
                return cContrato.CatalogoUsu(t314_idusuario, null, bMostrarTodos, t306_idcontrato, t377_denominacion, sTipoBusq, t302_idcliente);
            }
            

        }



        public Models.Contrato ObtenerExtensionPadre(int t306_idcontrato)
        {
			OpenDbConn();
			
            DAL.Contrato cContrato = new DAL.Contrato(cDblib);
            return cContrato.ObtenerExtensionPadre(t306_idcontrato);
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
        ~Contrato()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
