using System;
using System.Collections.Generic;

using SUPERANTIGUO = SUPER;
using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

/// <summary>
/// Summary description for ProyectoTecnicoBitacora
/// </summary>
namespace IB.SUPER.IAP30.BLL
{
    public class ProyectoTecnicoBitacora : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("1d365ea1-9c4a-4dd1-aad1-156ca8ad07dc");
        private bool disposed = false;

        #endregion

        #region Constructor

        public ProyectoTecnicoBitacora()
            : base()
        {
            //OpenDbConn();
        }

        public ProyectoTecnicoBitacora(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas		        
        public List<Models.ProyectoTecnico> Catalogo(int idPSN, int idUser)
        {
            bool bAdmin = false;
            if (SUPERANTIGUO.Capa_Negocio.Utilidades.EsAdminProduccion())
                bAdmin = true;

            OpenDbConn();

            DAL.ProyectoTecnicoBitacora cPTs = new DAL.ProyectoTecnicoBitacora(cDblib);
            return cPTs.Catalogo(idPSN, idUser, bAdmin);
        }

        public Models.ProyectoTecnicoBitacora Select(int idPT)
        {
            OpenDbConn();

            DAL.ProyectoTecnicoBitacora cProy = new DAL.ProyectoTecnicoBitacora(cDblib);
            return cProy.Select(idPT);
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
        ~ProyectoTecnicoBitacora()
        {
            Dispose(false);
        }

        #endregion


    }

}
