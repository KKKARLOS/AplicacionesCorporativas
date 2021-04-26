using System;
using System.Collections.Generic;
using IB.SUPER.IAP30.DAL;
using IB.SUPER.IAP30.Models;

namespace IB.SUPER.IAP30.BLL
{

    /// <summary>
    /// Descripción breve de Bitacora
    /// </summary>
    public class Bitacora
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("BC66BCE5-1142-4871-AF7B-95F8D5F700D5");
        private bool disposed = false;

        #endregion

        #region Constructor

        public Bitacora()
			: base()
        {
            //OpenDbConn();
        }

        public Bitacora(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas		        
        public List<Models.Bitacora> Catalogo(int idPSN,bool acciones,string Denominacion, 
                                            Nullable<int> TipoAsunto, Nullable<int> Estado, Nullable<int> Severidad, Nullable<int> Prioridad,
                                            Nullable<DateTime> dNotif, Nullable<DateTime> hNotif, Nullable<DateTime> dLimite, Nullable<DateTime> hLimite, Nullable<DateTime> dFin, Nullable<DateTime> hFin)
        {
            OpenDbConn();

            DAL.Bitacora cTareas = new DAL.Bitacora(cDblib);
            return cTareas.Catalogo(idPSN, acciones, Denominacion, TipoAsunto, Estado, Severidad, Prioridad, dNotif, hNotif, dLimite, hLimite, dFin, hFin);
        }

        public List<Models.Bitacora> CatalogoPT(int idPT, bool acciones, string Denominacion,
                                            Nullable<int> TipoAsunto, Nullable<int> Estado, Nullable<int> Severidad, Nullable<int> Prioridad,
                                            Nullable<DateTime> dNotif, Nullable<DateTime> hNotif, Nullable<DateTime> dLimite, Nullable<DateTime> hLimite, Nullable<DateTime> dFin, Nullable<DateTime> hFin)
        {
            OpenDbConn();

            DAL.Bitacora cTareas = new DAL.Bitacora(cDblib);
            return cTareas.CatalogoPT(idPT, acciones, Denominacion, TipoAsunto, Estado, Severidad, Prioridad, dNotif, hNotif, dLimite, hLimite, dFin, hFin);
        }

        public List<Models.Bitacora> CatalogoTareas(int idTarea, bool acciones, string Denominacion,
                                            Nullable<int> TipoAsunto, Nullable<int> Estado, Nullable<int> Severidad, Nullable<int> Prioridad,
                                            Nullable<DateTime> dNotif, Nullable<DateTime> hNotif, Nullable<DateTime> dLimite, Nullable<DateTime> hLimite, Nullable<DateTime> dFin, Nullable<DateTime> hFin)
        {
            OpenDbConn();

            DAL.Bitacora cTareas = new DAL.Bitacora(cDblib);
            return cTareas.CatalogoTareas(idTarea, acciones, Denominacion, TipoAsunto, Estado, Severidad, Prioridad, dNotif, hNotif, dLimite, hLimite, dFin, hFin);
        }


        public List<Models.ProyectoNota> Proyectos(int t314_idusuario)
        {
            OpenDbConn();

            DAL.Bitacora cProyecto = new DAL.Bitacora(cDblib);
            return cProyecto.Proyectos(t314_idusuario);

        }

        /// <summary>
        /// Da la lista de proyectos económicos que tengan proyectos técnicos con bitácora accesibles para un usuarios
        /// </summary>
        /// <param name="t314_idusuario"></param>
        /// <returns></returns>
        public List<Models.ProyectoNota> ProyectosPT(int t314_idusuario)
        {
            OpenDbConn();

            DAL.Bitacora cProyecto = new DAL.Bitacora(cDblib);
            return cProyecto.ProyectosPT(t314_idusuario);

        }

        public List<IB.SUPER.APP.Models.NodoBasico> ProyectosTecnicos(int t314_idusuario, int t305_idproyectosubnodo)
        {
            OpenDbConn();

            DAL.Bitacora cProyecto = new DAL.Bitacora(cDblib);
            return cProyecto.ProyectosTecnicos(t314_idusuario, t305_idproyectosubnodo);

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
        ~Bitacora()
        {
            Dispose(false);
        }

        #endregion
    }
}