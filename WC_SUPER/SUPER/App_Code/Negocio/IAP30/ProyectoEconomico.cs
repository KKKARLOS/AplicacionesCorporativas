using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for ProyectoEconomico
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class ProyectoEconomico : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("acdc2ce7-9f55-4a3d-a7f0-4fff7f416281");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public ProyectoEconomico()
			: base()
        {
			//OpenDbConn();
        }
		
		public ProyectoEconomico(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas		        
        public Models.ProyectoEconomico Select(int t301_idproyecto)
        {
            OpenDbConn();

            DAL.ProyectoEconomico cEstadoPE = new DAL.ProyectoEconomico(cDblib);
            return cEstadoPE.Select(t301_idproyecto);
        }
        public List<Models.ProyectoEconomico> Catalogo(bool esAdmin, Nullable<int> idNodo, string sEstado, string sCategoria,
                                                        Nullable<int> idCliente, Nullable<int> idResponsable, Nullable<int> numPE, string sDesPE,
                                                        string sTipoBusqueda, string sCualidad, Nullable<int> nContrato, Nullable<int> nHorizontal,
                                                        Nullable<int> nCNP, Nullable<int> nCSN1P, Nullable<int> nCSN2P, Nullable<int> nCSN3P,
                                                        Nullable<int> nCSN4P, bool bMostrarJ, bool bSoloFacturables, int nUsuario, bool bMostrarBitacoricos, 
                                                        Nullable<int> nNaturaleza, Nullable<int> nModeloContratacion)
        {
            OpenDbConn();

            DAL.ProyectoEconomico cProyectoEconomico = new DAL.ProyectoEconomico(cDblib);

            if (esAdmin) {

                return cProyectoEconomico.CatalogoAdmin(idNodo, sEstado, sCategoria, idCliente, idResponsable, numPE, sDesPE,
                                                        sTipoBusqueda, sCualidad, nContrato, nHorizontal, nCNP, nCSN1P, nCSN2P, nCSN3P,
                                                        nCSN4P, bMostrarJ, bSoloFacturables, nNaturaleza, nModeloContratacion);

            } else {

                return cProyectoEconomico.CatalogoModuloTec(idNodo, sEstado, sCategoria, idCliente, idResponsable, numPE, sDesPE,
                                                         sTipoBusqueda, sCualidad, nContrato, nHorizontal, nCNP, nCSN1P, nCSN2P, nCSN3P,
                                                        nCSN4P, nUsuario, bMostrarBitacoricos, nNaturaleza, nModeloContratacion);

            }
            

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
        ~ProyectoEconomico()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
