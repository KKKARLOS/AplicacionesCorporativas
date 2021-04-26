using System;
using System.Collections.Generic;
using System.Web;
using IB.SUPER.SIC.DAL;
using IB.SUPER.SIC.Models;

/// <summary>
/// Summary description for SolicitudPreventa
/// </summary>
namespace IB.SUPER.SIC.BLL
{
    public class SolicitudPreventa : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("54916d65-596a-41a4-b292-4c3beddeaf17");
        private bool disposed = false;

        #endregion

        #region Constructor

        public SolicitudPreventa()
            : base()
        {
            //OpenDbConn();
        }

        public SolicitudPreventa(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas



        public int Insert(Models.SolicitudPreventa oSolicitudPreventa)
        {
            Guid methodOwnerID = new Guid("8fbbab16-8b11-41ea-b4bc-bef98a9e13bd");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.SolicitudPreventa cSolicitudPreventa = new DAL.SolicitudPreventa(cDblib);

                oSolicitudPreventa.t001_idficepi_promotor = int.Parse(HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"].ToString());

                int idSolicitudPreventa = cSolicitudPreventa.Insert(oSolicitudPreventa);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return idSolicitudPreventa;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }
        }

        /// <summary>
        /// Actualiza la denominación de una solicitud origen SUPER
        /// </summary>
        /// <param name="oSolicitudPreventa"></param>
        /// <returns></returns>
        public void UpdateDenominacion(int ta206_idsolicitudpreventa, string ta206_denominacion, string ta206_estado, string motivoAnulacion)
        {
            Guid methodOwnerID = new Guid("51BA3E44-56B6-4CF5-B98A-DF11DDFC1BB2");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.SolicitudPreventa cSolicitudPreventa = new DAL.SolicitudPreventa(cDblib);

                cSolicitudPreventa.UpdateDenominacion(ta206_idsolicitudpreventa, ta206_denominacion, ta206_estado, motivoAnulacion, (int)HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"]);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }
        }




        public void EliminarSolicitud(int ta206_idsolicitudpreventa)
        {
            Guid methodOwnerID = new Guid("fe908c00-38c2-40c5-90de-376e8adec76d");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.SolicitudPreventa cSolicitudPreventa = new DAL.SolicitudPreventa(cDblib);

                cSolicitudPreventa.EliminarSolicitud(ta206_idsolicitudpreventa);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }
        }

        /// <summary>
        /// Obtiene los datos de cabecera de una solicitud preventa. Para la llamada a la pantalla desde el botón preventa del CRM
        /// </summary>
        /// <param name="id">ta206_idoportunidad / ta206_idpartida</param>
        /// <param name="tipo">O=Oportunidad | P=Partida</param>
        /// <returns></returns>
        public Models.SolicitudPreventa Select(int? ta206_iditemorigen, string ta206_itemorigen)
        {
            OpenDbConn();

            DAL.SolicitudPreventa cSolicitudPreventa = new DAL.SolicitudPreventa(cDblib);
            return cSolicitudPreventa.Select(ta206_iditemorigen, ta206_itemorigen);
        }

        public Models.SolicitudPreventa btnAccionesSegunEstadoSolicitud (int? ta206_iditemorigen, string ta206_itemorigen)
        {
            OpenDbConn();

            DAL.SolicitudPreventa cSolicitudPreventa = new DAL.SolicitudPreventa(cDblib);
            return cSolicitudPreventa.btnAccionesSegunEstadoSolicitud(ta206_iditemorigen, ta206_itemorigen);
        }

        /// <summary>
        /// Obtiene la información de item del CRM (Oportunidad, Extensión, Partida)
        /// </summary>
        /// <param name="ta206_iditemorigen">id hermes</param>
        /// <param name="ta206_itemorigen">O P E</param>
        /// <returns></returns>
        public Models.ItemCRM SelectOrigen(int ta206_iditemorigen, string ta206_itemorigen)
        {

            OpenDbConn();

            DAL.SolicitudPreventa cSolicitudPreventa = new DAL.SolicitudPreventa(cDblib);
            Models.ItemCRM oItem = cSolicitudPreventa.SelectOrigen(ta206_iditemorigen, ta206_itemorigen);

            if (oItem.cod_comercial != null && oItem.cod_comercial.Trim().Length > 0)
            {
                DAL.Usuario cUsuario = new DAL.Usuario(cDblib);
                oItem.comercial = cUsuario.obtenerNombreComercial(oItem.cod_comercial);
            }

            if (oItem.gestorProduccion != null && oItem.gestorProduccion.Trim().Length > 0)
            {
                DAL.Usuario cUsuario = new DAL.Usuario(cDblib);
                oItem.gestorProduccion_nombre = cUsuario.obtenerNombreComercial(oItem.gestorProduccion);
            }

            //Formatear (sólo se usa para pintar en pantalla)
            if (oItem.importe != null) oItem.importe = double.Parse(oItem.importe).ToString("#,##0.00") + " " + oItem.moneda;
            if(oItem.rentabilidad != null) oItem.rentabilidad = double.Parse(oItem.rentabilidad).ToString("##0.00") + " %";
            if (oItem.exito != null) oItem.exito = double.Parse(oItem.exito).ToString("##0.00") + " %";
            if(oItem.fechaCierre != null) oItem.fechaCierre = oItem.fechaCierre.Substring(0, 10);
            if (oItem.fechaLimitePresentacion != null) oItem.fechaLimitePresentacion = oItem.fechaLimitePresentacion.Substring(0, 10);
            if (oItem.duracionProyecto != null && oItem.duracionProyecto.Trim().Length > 0) oItem.duracionProyecto += " meses";

            if (oItem.contratacionPrevista != null) oItem.contratacionPrevista = double.Parse(oItem.contratacionPrevista).ToString("#,##0.00") + " " + oItem.moneda;
            if (oItem.costePrevisto != null) oItem.costePrevisto = double.Parse(oItem.costePrevisto).ToString("#,##0.00") + " " + oItem.moneda;
            if (oItem.resultado != null) oItem.resultado = oItem.resultado.ToString();
            if (oItem.fechaInicio != null) oItem.fechaInicio = oItem.fechaInicio.Substring(0, 10);
            if (oItem.fechaFin != null) oItem.fechaFin = oItem.fechaFin.Substring(0, 10);
            
            return oItem;

        }


        /// <summary>
        /// Obtiene los datos de cabecera de una solicitud preventa por id de solicitud
        /// </summary>
        /// <returns></returns>
        public Models.SolicitudPreventa SelectById(int ta206_idsolicitudpreventa)
        {
            OpenDbConn();

            Models.SolicitudPreventa oSP;

            DAL.SolicitudPreventa cSolicitudPreventa = new DAL.SolicitudPreventa(cDblib);
            oSP = cSolicitudPreventa.SelectById(ta206_idsolicitudpreventa);

            return oSP;
        }

        public Models.SolicitudPreventa getSolicitudbyAccion(int idaccion)
        {
            OpenDbConn();

            Models.SolicitudPreventa oSP;

            DAL.SolicitudPreventa cSolicitudPreventa = new DAL.SolicitudPreventa(cDblib);
            oSP = cSolicitudPreventa.getSolicitudbyAccion(idaccion);

            return oSP;
        }

        public Models.SolicitudPreventa getSolicitudbyAccion2(int idaccion)
        {
            OpenDbConn();

            Models.SolicitudPreventa oSP;

            DAL.SolicitudPreventa cSolicitudPreventa = new DAL.SolicitudPreventa(cDblib);
            oSP = cSolicitudPreventa.getSolicitudbyAccion2(idaccion);

            return oSP;
        }

        public  List<Models.AccionLider> GetLideresSolicitud(int ta206_iditemorigen, string ta206_itemorigen, int? ta204_idaccionpreventa, int ta201_idsubareapreventa )
        {
            OpenDbConn();

            Models.SolicitudPreventa oSP;

            DAL.SolicitudPreventa cSolicitudPreventa = new DAL.SolicitudPreventa(cDblib);
            return cSolicitudPreventa.GetLideresSolicitud(ta206_iditemorigen, ta206_itemorigen, ta204_idaccionpreventa, ta201_idsubareapreventa );

        }

        public List<Models.SolicCatSuper> CatalogoSolicitudesSUPER(string origenMenu, Models.SolicCatSuperRF rf){
        
           OpenDbConn();

            Models.SolicitudPreventa oSP;

            bool admin = origenMenu == "ADM" ? true : false;

            DAL.SolicitudPreventa cSolicitudPreventa = new DAL.SolicitudPreventa(cDblib);
            return cSolicitudPreventa.CatalogoSolicitudesSUPER(admin, (int)HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"], rf );
        
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
        ~SolicitudPreventa()
        {
            Dispose(false);
        }

        #endregion


    }

}
