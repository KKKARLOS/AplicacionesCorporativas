using System;
using System.Collections.Generic;


using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for BuscadorTareasBloque
/// </summary>
namespace IB.SUPER.IAP30.BLL 
{
    public class BuscadorTareasBloque : IDisposable
    {
    	#region Variables privadas
		
		private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("570ec6ff-f636-4676-9979-898a97c6f858");
        private bool disposed = false;
		
		#endregion
		
        #region Constructor
		
        public BuscadorTareasBloque()
			: base()
        {
			//OpenDbConn();
        }
		
		public BuscadorTareasBloque(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }
		
        #endregion       

        #region Funciones públicas

        internal List<Models.BuscadorTareasBloque> Catalogo(Int32 nUsuario, DateTime ultimoMesCerrado, Nullable<DateTime> fechaInicio, Nullable<DateTime> fechaFin)
        {
            OpenDbConn();

			DAL.BuscadorTareasBloque cBuscadorTareasBloque = new DAL.BuscadorTareasBloque(cDblib);
            return cBuscadorTareasBloque.Catalogo(nUsuario, ultimoMesCerrado, fechaInicio, fechaFin);

        }

        internal List<Models.BuscadorTareasBloque> CatalogoAgenda(Int32 idFicepi)
        {
            OpenDbConn();

            DAL.BuscadorTareasBloque cBuscadorTareasBloque = new DAL.BuscadorTareasBloque(cDblib);
            return cBuscadorTareasBloque.CatalogoAgenda(idFicepi);

        }


        internal List<Models.BuscadorTareasBloque> tareasBitacoraIAP(Int32 nUsuario)
        {
            OpenDbConn();

            DAL.BuscadorTareasBloque cBuscadorTareasBloque = new DAL.BuscadorTareasBloque(cDblib);
            return cBuscadorTareasBloque.CatalogoBitacoraIAP(nUsuario);

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
        ~BuscadorTareasBloque()
        {
            Dispose(false);
        }
		
        #endregion

    
    }

}
