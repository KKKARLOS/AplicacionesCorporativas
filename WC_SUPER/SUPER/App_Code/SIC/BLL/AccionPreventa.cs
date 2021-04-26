using System;
using System.Collections.Generic;
using System.Web;
using IB.SUPER.SIC.DAL;
using IB.SUPER.SIC.Models;

/// <summary>
/// Summary description for AccionPreventa
/// </summary>
namespace IB.SUPER.SIC.BLL
{
    public class AccionPreventa : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("a33e5adf-5268-4d7b-8052-01f47049e7e9");
        private bool disposed = false;

        #endregion

        #region Constructor

        public AccionPreventa()
            : base()
        {
            //OpenDbConn();
        }

        public AccionPreventa(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas


        public List<Models.AccionPreventa> CatalogoLider()
        {

            OpenDbConn();

            DAL.AccionPreventa cAccionPreventa = new DAL.AccionPreventa(cDblib);
            return cAccionPreventa.CatalogoLíder((int)HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"]);

        }

        public Models.AccionPreventa numAcciones()
        {

            OpenDbConn();

            DAL.AccionPreventa cAccionPreventa = new DAL.AccionPreventa(cDblib);
            return cAccionPreventa.numAcciones((int)HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"]);

        }

        public int Insert(Models.AccionPreventa oAccionPreventa, Models.SolicitudPreventa oSP, Guid guidprovisional)
        {
            Guid methodOwnerID = new Guid("c520a70a-6315-488b-a802-07b679db76e3");

            BLL.Listas cListas = null;
            BLL.SolicitudPreventa cSolicitudPreventa = null;
            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.AccionPreventa cAccionPreventa = new DAL.AccionPreventa(cDblib);
                cSolicitudPreventa = new BLL.SolicitudPreventa(cDblib);

                //grabar siempre la solicitud para acciones de origen CRM. Si existe devuelve el mismo id
                if (oSP.ta206_itemorigen == "O" || oSP.ta206_itemorigen == "P" || oSP.ta206_itemorigen == "E")
                {
                    oSP.ta200_idareapreventa = null;
                    oAccionPreventa.ta206_idsolicitudpreventa = cSolicitudPreventa.Insert(oSP);
                }
                else
                { //itemorigen = "S"
                    oSP.ta206_iditemorigen = oSP.ta206_idsolicitudpreventa;
                    oAccionPreventa.ta206_idsolicitudpreventa = oSP.ta206_idsolicitudpreventa;
                }



                //Validacion: Comprobación de tipo de acción no duplicable para otra acción de la misma solicitud.
                cListas = new BLL.Listas(cDblib);
                List<Models.TipoAccionPreventa> lst = cListas.GetListTipoAccionFiltrada(oSP.ta206_itemorigen, (int)oSP.ta206_iditemorigen);
                if (!lst.Contains(new Models.TipoAccionPreventa(oAccionPreventa.ta205_idtipoaccionpreventa)))
                    throw new IB.SUPER.Shared.ValidationException("La acción seleccionada está considerada como única por solicitud. Al ya existir otra solicitud con la misma acción, no se permite realizar la grabación.");
               
                
                oAccionPreventa.t001_idficepi_promotor = int.Parse(HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"].ToString());
                
                if (oAccionPreventa.ta201_idsubareapreventa == 0)  //Subarea == comercial, lider es el usuario conectado (el comercial)
                    oAccionPreventa.t001_idficepi_lider = oAccionPreventa.t001_idficepi_promotor;

                int idAccionPreventa = cAccionPreventa.Insert(oAccionPreventa, guidprovisional, (int)HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"]);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return idAccionPreventa;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }

            finally {
                if (cListas != null) cListas.Dispose();
                if(cSolicitudPreventa != null) cSolicitudPreventa.Dispose();
            }
        }

        public int Update(Models.AccionPreventa oAccionPreventa)
        {
            Guid methodOwnerID = new Guid("1c38edd7-67c4-4c5c-8cc2-544a064419a3");
            
            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.AccionPreventa cAccionPreventa = new DAL.AccionPreventa(cDblib);

                int result = cAccionPreventa.Update(oAccionPreventa, (int)HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"]);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return result;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }
        }

        public Models.AccionPreventa Select(Int32 ta204_idaccionpreventa)
        {
            OpenDbConn();

            DAL.AccionPreventa cAccionPreventa = new DAL.AccionPreventa(cDblib);
            return cAccionPreventa.Select(ta204_idaccionpreventa);
        }

        public Models.AccionPreventa accionPreventa_catTareas(Int32 ta204_idaccionpreventa)
        {
            OpenDbConn();

            DAL.AccionPreventa cAccionPreventa = new DAL.AccionPreventa(cDblib);
            return cAccionPreventa.accionPreventa_catTareas(ta204_idaccionpreventa, (int)HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"]);
        }

        public int AutoasignarLider(int ta204_idaccionpreventa)
        {
            Guid methodOwnerID = new Guid("1c38edd7-67c4-4c5c-8cc2-544a064419a3");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.AccionPreventa cAccionPreventa = new DAL.AccionPreventa(cDblib);

                int result = cAccionPreventa.AsignarLider(ta204_idaccionpreventa, int.Parse(HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"].ToString()));

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                //return result;
                return 0;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }
        }

        public List<Models.AccionPreventaCAT> Catalogo(Models.AccionCatRequestFilter rf) {

            OpenDbConn();

            DAL.AccionPreventa cAccionPreventa = new DAL.AccionPreventa(cDblib);
            return cAccionPreventa.Catalogo(rf);
        }
        
        public List<Models.AccionPreventaCAT> CatalogoPosibleLider() {

            OpenDbConn();

            DAL.AccionPreventa cAccionPreventa = new DAL.AccionPreventa(cDblib);
            return cAccionPreventa.CatalogoPosibleLider((int)HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"]);
        
        }

        public List<Models.AccionPreventaCAT> CatalogoMisAccionescomoLider()
        {

            OpenDbConn();

            DAL.AccionPreventa cAccionPreventa = new DAL.AccionPreventa(cDblib);
            return cAccionPreventa.CatalogoMisAccionescomoLider((int)HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"]);

        }

        public List<Models.AccionPreventaCAT> CatalogoPdteLider(bool? filter)
        {

            OpenDbConn();

            DAL.AccionPreventa cAccionPreventa = new DAL.AccionPreventa(cDblib);
            return cAccionPreventa.CatalogoPdteLider((int)HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"], filter);

        }

        public List<Models.AccionPreventaCAT> CatalogoPdteAsignarLider()
        {

            OpenDbConn();

            DAL.AccionPreventa cAccionPreventa = new DAL.AccionPreventa(cDblib);
            return cAccionPreventa.CatalogoPdteAsignarLider((int)HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"]);

        }

        public List<Models.AccionCatFigAreaSubarea> CatalogoFigAreaSubarea(string origenMenu, AccionCatFigAreaSubareaFilter rf)
        {
            OpenDbConn();

            bool admin = origenMenu == "ADM" ? true : false;

            DAL.AccionPreventa cAccionPreventa = new DAL.AccionPreventa(cDblib);
            return cAccionPreventa.CatalogoFigAreaSubarea(admin, (int)HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"], rf);
        }

        public List<Models.AccionCatAmbitoCRM> CatalogoAmbitoCRM(AccionCatAmbitoCRMFilter rf)
        {
            OpenDbConn();

            DAL.AccionPreventa cAccionPreventa = new DAL.AccionPreventa(cDblib);
            return cAccionPreventa.CatalogoAmbitoCRM((int)HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"], rf);
        }

        public List<Models.AccionPreventa> CatalogoImputaciones(string ta206_itemorigen, int ta206_iditemorigen)
        {

            OpenDbConn();

            DAL.AccionPreventa cAccionPreventa = new DAL.AccionPreventa(cDblib);
            return cAccionPreventa.CatalogoImputaciones(ta206_itemorigen, ta206_iditemorigen);
        }

        public List<Models.AccionPreventa> CatalogoAccionesBySolicitud(int ta206_idsolicitudpreventa)
        {

            OpenDbConn();

            DAL.AccionPreventa cAccionPreventa = new DAL.AccionPreventa(cDblib);
            return cAccionPreventa.CatalogoAccionesBySolicitud(ta206_idsolicitudpreventa, (int)HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"]);
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
        ~AccionPreventa()
        {
            Dispose(false);
        }

        #endregion


    }

}
