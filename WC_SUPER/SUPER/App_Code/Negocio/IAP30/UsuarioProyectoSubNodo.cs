using System;
using System.Collections;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for UsuarioProyectoSubNodo
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class UsuarioProyectoSubNodo : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("c58089bb-91bf-4ca3-8ec3-ece6f545e7a1");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public UsuarioProyectoSubNodo()
			: base()
        {
			//OpenDbConn();
        }
		
		public UsuarioProyectoSubNodo(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas

        public Models.UsuarioProyectoSubNodo Select(int t305_idproyectosubnodo, int t314_idusuario)
        {
            OpenDbConn();

            DAL.UsuarioProyectoSubNodo cUserPSN = new DAL.UsuarioProyectoSubNodo(cDblib);
            return cUserPSN.Select(t305_idproyectosubnodo, t314_idusuario);
        }

        public Hashtable ObtenerFechasAsociacionPSN(int t314_idusuario)
        {
            Hashtable htFechasPSN = new Hashtable();
            OpenDbConn();

            DAL.UsuarioProyectoSubNodo cConsulta = new DAL.UsuarioProyectoSubNodo(cDblib);
            List<Models.UsuarioProyectoSubNodo> lst = cConsulta.CatalogoProyectosGasvi(t314_idusuario);
            foreach (Models.UsuarioProyectoSubNodo oFecPsn in lst)
            {
                htFechasPSN.Add(oFecPsn.t305_idproyectosubnodo, new DateTime?[] {
                                                                (DateTime?)oFecPsn.t330_falta,
                                                                oFecPsn.t330_fbaja
                                                                });
            }
            return htFechasPSN;
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
        ~UsuarioProyectoSubNodo()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
