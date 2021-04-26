using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

using IB.Progress.DAL;
using IB.Progress.Models;
using IB.Progress.Shared;

/// <summary>
/// Summary description for GestionCambioResponsable
/// </summary>
namespace IB.Progress.BLL
{
    public class GestionCambioResponsable : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("e8a96aef-2555-465f-9ba9-f9757ed66a44");
        private bool disposed = false;

        #endregion

        #region Constructor

        public GestionCambioResponsable()
            : base()
        {
            //OpenDbConn();
        }

        public GestionCambioResponsable(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas

        public List<Models.GestionCambioResponsable> CatalogoCambioResponsable(Nullable<int> estado, string apellido1, string apellido2, string nombre )
        {
            OpenDbConn();

            DAL.GestionCambioResponsable cProfesional = new DAL.GestionCambioResponsable(cDblib);

            return cProfesional.CatalogoCambioResponsable(estado, apellido1, apellido2, nombre);
        }


        public int GestionAnulacion(int idpeticion, int idficefin)
        {
            
            OpenDbConn();

            try
            {

                DAL.GestionCambioResponsable cCambioResponsable = new DAL.GestionCambioResponsable(cDblib);
                //Estado 5. Anulado
                int idCambioResponsable = cCambioResponsable.GestionAnulacionAsignacion(idpeticion, idficefin, 5, null);

                return idCambioResponsable;
            }
            catch (Exception ex)
            {
               
                throw new IBException(105, "No se ha podido gestionar la anulación", ex);
            }
        }

        public int GestionAsignacion(int idpeticion, int idficefin, int idrespdestino, int idinteresado)
        {

            OpenDbConn();

            try
            {

                DAL.GestionCambioResponsable cCambioResponsable = new DAL.GestionCambioResponsable(cDblib);
                //Estado 4. Impuesto por RRHH
                int idCambioResponsable = cCambioResponsable.GestionAnulacionAsignacion(idpeticion, idficefin, 4, idrespdestino);
                int idOtroResponsable = cCambioResponsable.CambioEvalprogress(idinteresado, idrespdestino);
                return idCambioResponsable;
            }
            catch (Exception ex)
            {

                throw new IBException(105, "No se ha podido gestionar la asignación", ex);
            }
        }



        public void GestionCambioResponsableUPD(Nullable<int> idpeticion,  int idficepi_interesado, Nullable<int> idficepidestino, int idficepifin)
        {
            Guid methodOwnerID = new Guid("e02a5adf-afea-44bb-9583-88cef9393b37");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {

                DAL.GestionCambioResponsable cCambioResponsable = new DAL.GestionCambioResponsable(cDblib);

                //Estado 4. Impuesto por RRHH
                if (idpeticion != 0)
                {
                    int idCambioResponsable = cCambioResponsable.GestionAnulacionAsignacion(idpeticion, idficepifin, 4, idficepidestino);
                    int idOtroResponsable = cCambioResponsable.CambioEvalprogress(idficepi_interesado, idficepidestino);
                }

                else {
                    int idOtroResponsableSinPeticion = cCambioResponsable.CambioEvalprogress(idficepi_interesado, idficepidestino);
                }
                

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.rollbackTransaction(methodOwnerID);

                throw new IBException(105, "No se ha podido gestionar el cambio de responsable", ex);
            }
        }


        #endregion

        #region Conexion base de datos y dispose
        private void OpenDbConn()
        {
            if (cDblib == null)
                cDblib = new IB.sqldblib.SqlServerSP(IB.Progress.Shared.Database.GetConStr(), classOwnerID);
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
        ~GestionCambioResponsable()
        {
            Dispose(false);
        }

        #endregion


    }

}
