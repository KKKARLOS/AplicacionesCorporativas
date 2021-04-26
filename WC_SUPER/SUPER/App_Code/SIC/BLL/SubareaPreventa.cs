using System;
using System.Collections.Generic;


using IB.SUPER.SIC.DAL;
using IB.SUPER.SIC.Models;
using System.Data;
using System.Web;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for SubareaPreventa
/// </summary>
namespace IB.SUPER.SIC.BLL
{
    public class SubareaPreventa : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("dfafc174-df32-4509-9a51-2b2c33c956b2");
        private bool disposed = false;

        #endregion

        #region Constructor

        public SubareaPreventa()
            : base()
        {
            //OpenDbConn();
        }

        public SubareaPreventa(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas



        internal Models.SubareaPreventa Select(Int32 ta201_idsubareapreventa)
        {
            OpenDbConn();

            DAL.SubareaPreventa cSubareaPreventa = new DAL.SubareaPreventa(cDblib);
            return cSubareaPreventa.Select(ta201_idsubareapreventa);
        }

        /// <summary>
        /// Obteniene la lista de areas para el combo de ayuda de subareas filtrado por unidad
        /// </summary>
        public List<IB.SUPER.APP.Models.KeyValue> ObtenerAreasDeUnidad(BLL.Ayudas.enumAyuda enumlst, int ta199_idunidadpreventa, bool admin) {


            OpenDbConn();

            DAL.SubareaPreventa cSubareaPreventa = new DAL.SubareaPreventa(cDblib);

            return cSubareaPreventa.ObtenerAreasDeUnidad(enumlst, (int)System.Web.HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"], ta199_idunidadpreventa, admin);
        }

        /// <summary>
        /// Obteniene la lista de subareas para la ayuda de subareas filtrado por area
        /// </summary>
        public List<IB.SUPER.APP.Models.KeyValue> ObtenerSubareasDeArea(BLL.Ayudas.enumAyuda enumlst, int ta200_idareapreventa, bool admin)
        {
            OpenDbConn();

            DAL.SubareaPreventa cSubareaPreventa = new DAL.SubareaPreventa(cDblib);

            return cSubareaPreventa.ObtenerSubareasDeArea(enumlst, (int)System.Web.HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"], ta200_idareapreventa, admin);
        }


        public List<Models.SubareaPreventa> getSubAreasByFicepi(Int32 ta200_idareapreventa, Int32 t001_idficepi, bool actuocomoadministrador)
        {
            OpenDbConn();

            DAL.SubareaPreventa cSubareaPreventa = new DAL.SubareaPreventa(cDblib);
            return cSubareaPreventa.getSubAreasByFicepi(ta200_idareapreventa, t001_idficepi, actuocomoadministrador);
        }

        public Models.SubareaPreventa getSubAreaSel(Int32 ta201_idsubareapreventa)
        {
            OpenDbConn();

            DAL.SubareaPreventa cAreaPreventa = new DAL.SubareaPreventa(cDblib);
            return cAreaPreventa.Select(ta201_idsubareapreventa);
        }

        public List<Models.SubareaPreventa> getFiguras_SubArea(Int32 ta201_idsubareapreventa)
        {
            OpenDbConn();

            DAL.SubareaPreventa cSubAreaPreventa = new DAL.SubareaPreventa(cDblib);
            return cSubAreaPreventa.getFiguras_SubArea(ta201_idsubareapreventa);
        }

        public int grabarSubArea(Models.SubareaPreventa oSubAreaPreventa, List<Models.FiguraSubareaPreventa> lstFigurasArea)
        {
            Guid methodOwnerID = new Guid("91fe2269-b98f-4376-bcdb-06a3e750c744");
            int idSubAreaPreventa = 0;
            int nFiguras = 0;
            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {

                DAL.SubareaPreventa cSubAreaPreventa = new DAL.SubareaPreventa(cDblib);

                DataTable dtFiguras = new DataTable();
                dtFiguras.Columns.Add(new DataColumn("col_1", typeof(int)));                
                dtFiguras.Columns.Add(new DataColumn("col_2", typeof(char)));


                //Recorremos la lista
                foreach (Models.FiguraSubareaPreventa oFiguras in lstFigurasArea)
                {
                    DataRow row = dtFiguras.NewRow();                    
                    row["col_1"] = oFiguras.t001_idficepi;
                    row["col_2"] = oFiguras.ta203_figura;

                    dtFiguras.Rows.Add(row);
                }

                idSubAreaPreventa = cSubAreaPreventa.grabarSubArea(oSubAreaPreventa);

                nFiguras = cSubAreaPreventa.grabarFigurasSubArea(oSubAreaPreventa.ta201_idsubareapreventa, dtFiguras);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return idSubAreaPreventa;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }
        }

        public void grabar(List<Models.SubareaPreventa> lstSubareas)
        {
            Guid methodOwnerID = new Guid("8bf259c7-135e-4a96-8703-9da7e697a68d");
            
            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.SubareaPreventa cSubAreaPreventa = new DAL.SubareaPreventa(cDblib);
               
                string cadenaFicepis = "";
                int ta201_idsubareapreventa = 0;

                for (int i = 0; i < lstSubareas.Count; i++)
                {
                    DataTable dtTablappl = null;
                    dtTablappl = new DataTable();
                    dtTablappl.Columns.Add(new DataColumn("col_1", typeof(int)));       

                    ta201_idsubareapreventa = lstSubareas[i].ta201_idsubareapreventa;      
                    cadenaFicepis = lstSubareas[i].profesionales;
                    //Recorremos la lista
                    string[] ficepis = Regex.Split(cadenaFicepis, "@#@");
                    

                    //tratamos los profesionales
                    for (int j = 0; j < ficepis.Length; j++)
			        {
                        if (ficepis[j] != "")
                        {                            
                            DataRow row = dtTablappl.NewRow();
                            row["col_1"] = int.Parse(ficepis[j]);                            
                            dtTablappl.Rows.Add(row);
                        }
                            
			        }

                    cSubAreaPreventa.grabarppldesubarea(ta201_idsubareapreventa, dtTablappl);
                    //dtTablappl.Clear();              
                }
                
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

        public int Delete(Int32 ta201_idsubareapreventa)
        {
            Guid methodOwnerID = new Guid("584034d9-404c-495e-bb43-a35c6471e9a7");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {

                DAL.SubareaPreventa cSubareaPreventa = new DAL.SubareaPreventa(cDblib);

                int result = cSubareaPreventa.Delete(ta201_idsubareapreventa);

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
        ~SubareaPreventa()
        {
            Dispose(false);
        }

        #endregion


    }

}
