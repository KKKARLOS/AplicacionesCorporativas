using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for Tarea
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class Tarea : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("a32a6f1c-86d9-4683-83c2-54de10bc7a27");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public Tarea()
			: base()
        {
			//OpenDbConn();
        }
		
		public Tarea(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas		        
        public List<Models.Tarea> Catalogo(Int32 t305_idproyectosubnodo)
        {
            OpenDbConn();

            DAL.Tarea cTareas = new DAL.Tarea(cDblib);
            List<Models.Tarea> oLista = cTareas.Catalogo(t305_idproyectosubnodo);
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
        ~Tarea()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
