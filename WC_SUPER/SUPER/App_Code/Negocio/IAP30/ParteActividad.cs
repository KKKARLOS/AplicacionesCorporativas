using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for ParteActividad
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class ParteActividad : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("52f46102-ff65-4213-bbff-fbc7ce133d0f");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public ParteActividad()
			: base()
        {
			//OpenDbConn();
        }
		
		public ParteActividad(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas

        public List<Models.ParteActividad> Catalogo(string t314_idusuario, string idClientes, string idproyectosubnodos, Nullable<bool> facturable, DateTime dDesde, DateTime dHasta)
        {
            OpenDbConn();

			DAL.ParteActividad cParteActividad = new DAL.ParteActividad(cDblib);
            return cParteActividad.Catalogo(t314_idusuario, idClientes, idproyectosubnodos, facturable, dDesde, dHasta);

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
        ~ParteActividad()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
