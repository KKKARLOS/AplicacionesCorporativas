using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for CONSUMOIAP_PROYECTOS
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class CONSUMOIAP_PROYECTOS : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("8b7c44ab-0698-4b57-875e-1c1035c66ee5");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public CONSUMOIAP_PROYECTOS()
			: base()
        {
			//OpenDbConn();
        }
		
		public CONSUMOIAP_PROYECTOS(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas

        public List<Models.CONSUMOIAP_PROYECTOS> Catalogo(int t314_idusuario, DateTime dDesde, DateTime dHasta)
        {
            OpenDbConn();

			//DAL.CONSUMOIAP_PROYECTOS cCONSUMOIAP_PROYECTOS = new DAL.CONSUMOIAP_PROYECTOS(cDblib);
            //return cCONSUMOIAP_PROYECTOS.Catalogo(t314_idusuario, dDesde, dHasta);


            //OpenDbConn();

            DAL.CONSUMOIAP_PROYECTOS cCONSUMOIAP_PROYECTOS = new DAL.CONSUMOIAP_PROYECTOS(cDblib);
            List<Models.CONSUMOIAP_PROYECTOS> oLista = cCONSUMOIAP_PROYECTOS.Catalogo(t314_idusuario, dDesde, dHasta);


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
        ~CONSUMOIAP_PROYECTOS()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
