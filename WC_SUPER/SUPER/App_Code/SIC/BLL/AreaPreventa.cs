using System;
using System.Collections.Generic;
using IB.SUPER.SIC.DAL;
using IB.SUPER.SIC.Models;
using System.Data;
using System.Linq;

/// <summary>
/// Summary description for AreaPreventa
/// </summary>
namespace IB.SUPER.SIC.BLL
{
    public class AreaPreventa : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("2d57f08c-d3ad-482c-a575-0654b493e454");
        private bool disposed = false;

        #endregion

        #region Constructor

        public AreaPreventa()
            : base()
        {
            //OpenDbConn();
        }

        public AreaPreventa(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas

        
        public List<Models.AreaPreventa> getAreasByFicepi(int t001_idficepi, bool actuocomoadministrador)
        {
            OpenDbConn();

            DAL.AreaPreventa cAreaPreventa = new DAL.AreaPreventa(cDblib);
            return cAreaPreventa.getAreasByFicepi(t001_idficepi, actuocomoadministrador);

        }

        public List<Models.AreaPreventa> getpplporsubareaparaficepi(int t001_idficepi)
        {
            OpenDbConn();

            DAL.AreaPreventa cAreaPreventa = new DAL.AreaPreventa(cDblib);
            return cAreaPreventa.getpplporsubareaparaficepi(t001_idficepi);

        }

        public List<Models.AreaPreventa> getareassubareasppl(int t001_idficepi)
        {
            OpenDbConn();

            DAL.AreaPreventa cAreaPreventa = new DAL.AreaPreventa(cDblib);

            List<Models.AreaPreventa> lstDatos = cAreaPreventa.getareassubareasppl(t001_idficepi);
            List<Models.AreaPreventa> lstPosibleLider = cAreaPreventa.getpplporsubareaparaficepi(t001_idficepi);

            foreach (Models.AreaPreventa item in lstDatos)
            {

                for (int i = 0; i < lstPosibleLider.Count; i++)
                {
                    if (item.ta201_idsubareapreventa == lstPosibleLider[i].ta201_idsubareapreventa)
                    {
                        item.profesional += lstPosibleLider[i].t001_idficepi + "@#@" + lstPosibleLider[i].profesional +  "|";
                    }
                }
                
                
                //var lstPosibleLiderSubArea = (from p in lstPosibleLider
                //                                       where p.ta201_idsubareapreventa == item.ta201_idsubareapreventa
                //                                       //select new { p.profesional, p.t001_idficepi }).ToList();
                                                       
                //                                       select p.profesional).ToList<String>();


                
                //item.profesional = string.Join("|", lstPosibleLiderSubArea.ToArray());


               
                

            }

            return lstDatos;
        }
 
        public Models.AreaPreventa Select(Int32 ta200_idareapreventa)
        {
            OpenDbConn();

            DAL.AreaPreventa cAreaPreventa = new DAL.AreaPreventa(cDblib);
            return cAreaPreventa.Select(ta200_idareapreventa);
        }


        public List<Models.AreaPreventa> getFiguras_Area(Int32 ta200_idareapreventa)
        {
            OpenDbConn();

            DAL.AreaPreventa cAreaPreventa = new DAL.AreaPreventa(cDblib);
            return cAreaPreventa.getFiguras_Area(ta200_idareapreventa);
        }

        public int grabarArea(Models.AreaPreventa oAreaPreventa, List<Models.FiguraAreaPreventa> lstFigurasArea)
        {
            Guid methodOwnerID = new Guid("532e7c11-3ff0-4640-b531-d9a267300e9b");
            int idAreaPreventa = 0;
            int nFiguras = 0;
            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {

                DAL.AreaPreventa cAreaPreventa = new DAL.AreaPreventa(cDblib);

                DataTable dtFiguras = new DataTable();
                dtFiguras.Columns.Add(new DataColumn("col_1", typeof(int)));                
                dtFiguras.Columns.Add(new DataColumn("col_2", typeof(char)));


                //Recorremos la lista
                foreach (Models.FiguraAreaPreventa oFiguras in lstFigurasArea)
                {
                    DataRow row = dtFiguras.NewRow();                    
                    row["col_1"] = oFiguras.t001_idficepi;
                    row["col_2"] = oFiguras.ta202_figura;

                    dtFiguras.Rows.Add(row);
                }

                idAreaPreventa = cAreaPreventa.grabarArea(oAreaPreventa);

                nFiguras = cAreaPreventa.grabarFigurasArea(oAreaPreventa.ta200_idareapreventa, dtFiguras);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return idAreaPreventa;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }
        }

        public int Delete(Int32 ta200_idareapreventa)
        {
            Guid methodOwnerID = new Guid("d2fca125-dbf5-4382-8f71-5fc61bb37228");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {

                DAL.AreaPreventa cAreaPreventa = new DAL.AreaPreventa(cDblib);

                int result = cAreaPreventa.Delete(ta200_idareapreventa);

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
        ~AreaPreventa()
        {
            Dispose(false);
        }

        #endregion


    }

}
