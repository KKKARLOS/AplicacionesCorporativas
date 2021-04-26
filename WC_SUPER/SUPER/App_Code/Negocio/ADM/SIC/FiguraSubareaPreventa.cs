using System;
using System.Collections.Generic;


using IB.SUPER.ADM.SIC.DAL;
using IB.SUPER.ADM.SIC.Models;
//Para usar DataTable
using System.Data;

/// <summary>
/// Summary description for FiguraSubareaPreventa
/// </summary>
namespace IB.SUPER.ADM.SIC.BLL
{
    public class FiguraSubareaPreventa : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("0b025dcd-2fe2-47fd-ae44-d78c4fe6e492");
        private bool disposed = false;

        #endregion

        #region Constructor

        public FiguraSubareaPreventa()
            : base()
        {
            //OpenDbConn();
        }

        public FiguraSubareaPreventa(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas

        public List<int> ObtenerLideresSubarea(int ta201_idsubareapreventa)
        {
            OpenDbConn();

            DAL.FiguraSubareaPreventa cFiguraSubareaPreventa = new DAL.FiguraSubareaPreventa(cDblib);
            return cFiguraSubareaPreventa.ObtenerLideresSubarea(ta201_idsubareapreventa);

        }

        public List<Models.FiguraSubareaPreventa> Catalogo(Models.FiguraSubareaPreventa oFiguraSubareaPreventaFilter)
        {
            OpenDbConn();

            DAL.FiguraSubareaPreventa cFiguraSubareaPreventa = new DAL.FiguraSubareaPreventa(cDblib);
            return cFiguraSubareaPreventa.Catalogo(oFiguraSubareaPreventaFilter);

        }
        public List<Models.FiguraSubareaPreventa> Catalogo(Int32 ta201_idsubareapreventa)
        {
            OpenDbConn();

            DAL.FiguraSubareaPreventa cFiguraSubareaPreventa = new DAL.FiguraSubareaPreventa(cDblib);
            return cFiguraSubareaPreventa.Catalogo(ta201_idsubareapreventa);

        }

        //public int Insert(Models.FiguraSubareaPreventa oFiguraSubareaPreventa)
        //{
        //    Guid methodOwnerID = new Guid("9467a74f-e890-4cf2-9e12-4930dca042e1");

        //    OpenDbConn();

        //    if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

        //    try
        //    {
        //        DAL.FiguraSubareaPreventa cFiguraSubareaPreventa = new DAL.FiguraSubareaPreventa(cDblib);

        //        int idFiguraSubareaPreventa = cFiguraSubareaPreventa.Insert(oFiguraSubareaPreventa);

        //        //Finalizar transacción 
        //        if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

        //        return idFiguraSubareaPreventa;
        //    }
        //    catch (Exception ex)
        //    {
        //        //rollback
        //        if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.rollbackTransaction(methodOwnerID);

        //        throw ex;
        //    }
        //}
        /*
        public int Update(Models.FiguraSubareaPreventa oFiguraSubareaPreventa)
        {
            Guid methodOwnerID = new Guid("4dbc8b52-bdb6-4aeb-bdb6-13e1e4bddb3e");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.FiguraSubareaPreventa cFiguraSubareaPreventa = new DAL.FiguraSubareaPreventa(cDblib);

                int result = cFiguraSubareaPreventa.Update(oFiguraSubareaPreventa);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return result;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }
        }
        */

        public Models.FiguraSubareaPreventa Select(Int32 ta201_idsubareapreventa, Int32 t001_idficepi, String ta203_figura)
        {
            OpenDbConn();

            DAL.FiguraSubareaPreventa cFiguraSubareaPreventa = new DAL.FiguraSubareaPreventa(cDblib);
            return cFiguraSubareaPreventa.Select(ta201_idsubareapreventa, t001_idficepi, ta203_figura);
        }

        //public int Delete(Int32 ta201_idsubareapreventa, Int32 t001_idficepi)
        //{
        //    Guid methodOwnerID = new Guid("b6a8a7d3-00cd-461c-8224-0e14e1671b67");

        //    OpenDbConn();

        //    if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

        //    try
        //    {

        //        DAL.FiguraSubareaPreventa cFiguraSubareaPreventa = new DAL.FiguraSubareaPreventa(cDblib);

        //        int result = cFiguraSubareaPreventa.Delete(ta201_idsubareapreventa, t001_idficepi);

        //        //Finalizar transacción 
        //        if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        //rollback
        //        if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.rollbackTransaction(methodOwnerID);

        //        throw ex;
        //    }
        //}
        //public int DeleteFigura(Int32 ta201_idsubareapreventa, Int32 t001_idficepi, String ta203_figura)
        //{
        //    Guid methodOwnerID = new Guid("85eaefff-ed87-41b2-bbbc-c81469b10758");

        //    OpenDbConn();

        //    if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

        //    try
        //    {

        //        DAL.FiguraSubareaPreventa cFiguraSubareaPreventa = new DAL.FiguraSubareaPreventa(cDblib);

        //        int result = cFiguraSubareaPreventa.DeleteFigura(ta201_idsubareapreventa, t001_idficepi, ta203_figura);

        //        //Finalizar transacción 
        //        if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        //rollback
        //        if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.rollbackTransaction(methodOwnerID);

        //        throw ex;
        //    }
        //}
        public void ActualizarFiguras(Int32 ta201_idsubareapreventa, List<IB.SUPER.ADM.SIC.Models.FiguraSubareaPreventa> lstFiguras)
        {
            Guid methodOwnerID = new Guid("0F1686CA-A369-4850-B591-9F57F5129E34");
            OpenDbConn();
            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);
            DataTable dtFiguras = new DataTable();
            try
            {
                DAL.FiguraSubareaPreventa cFiguraSubareaPreventa = new DAL.FiguraSubareaPreventa(cDblib);

                #region Creo el datatable para pasarselo al proc almacenado
                //dtFiguras.Columns.Add(new DataColumn("item", typeof(int)));
                dtFiguras.Columns.Add(new DataColumn("t001_idficepi", typeof(int)));
                dtFiguras.Columns.Add(new DataColumn("figura", typeof(char)));
                foreach (IB.SUPER.ADM.SIC.Models.FiguraSubareaPreventa oFigura in lstFiguras)
                {
                    DataRow row = dtFiguras.NewRow();
                    //row["item"] = oFigura.ta201_idsubareapreventa;
                    row["t001_idficepi"] = oFigura.t001_idficepi;
                    row["figura"] = oFigura.ta203_figura;

                    dtFiguras.Rows.Add(row);
                }       
                #endregion

                cFiguraSubareaPreventa.ActualizarFiguras(ta201_idsubareapreventa, dtFiguras);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }
            finally
            {
                dtFiguras.Dispose();
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
        ~FiguraSubareaPreventa()
        {
            Dispose(false);
        }

        #endregion


    }

}
