using System;
using System.Collections.Generic;
using System.Data;
using IB.SUPER.ADM.SIC.DAL;
using IB.SUPER.ADM.SIC.Models;

/// <summary>
/// Summary description for TipoTareaPreventa
/// </summary>
namespace IB.SUPER.ADM.SIC.BLL
{
    public class TipoTareaPreventa : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("d256e19c-56ac-4151-868a-966a1e0fe288");
        private bool disposed = false;

        #endregion

        #region Constructor

        public TipoTareaPreventa()
            : base()
        {
            //OpenDbConn();
        }

        public TipoTareaPreventa(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas

        public List<Models.TipoTareaPreventa> Catalogo()
        {
            OpenDbConn();

            DAL.TipoTareaPreventa cTipoTareaPreventa = new DAL.TipoTareaPreventa(cDblib);
            return cTipoTareaPreventa.Catalogo();

        }

        internal List<Models.TipoTareaPreventa> GrabarListaTareas(List<Models.TipoTareaPreventa> lstTiposTareaPreventa)
        {
            Guid methodOwnerID = new Guid("f672bc44-f3fc-4102-8b50-2d3452b19198");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try

            {
                //Datatable de tipos de tareas preventa
                DataTable dtTipoTareas = new DataTable();
                dtTipoTareas.Columns.Add(new DataColumn("ta219_idtipotareapreventa", typeof(int)));
                dtTipoTareas.Columns.Add(new DataColumn("ta219_denominacion", typeof(string)));
                dtTipoTareas.Columns.Add(new DataColumn("ta219_orden", typeof(int)));
                dtTipoTareas.Columns.Add(new DataColumn("ta219_activa", typeof(bool)));

                foreach (Models.TipoTareaPreventa oTipoTarea in lstTiposTareaPreventa)
                {
                    DataRow rowAccion = dtTipoTareas.NewRow();
                    rowAccion["ta219_idtipotareapreventa"] = oTipoTarea.ta219_idtipotareapreventa;
                    rowAccion["ta219_denominacion"] = oTipoTarea.ta219_denominacion;
                    rowAccion["ta219_orden"] = oTipoTarea.ta219_orden;
                    rowAccion["ta219_activa"] = oTipoTarea.ta219_estadoactiva;
                    dtTipoTareas.Rows.Add(rowAccion);
                }


                DAL.TipoTareaPreventa cTipoTareaPreventa = new DAL.TipoTareaPreventa(cDblib);

                List <Models.TipoTareaPreventa> lstTipoTareaPreventa = cTipoTareaPreventa.GrabarTareas(dtTipoTareas);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return lstTiposTareaPreventa;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }
        }
       
        internal Models.TipoTareaPreventa Select(Int16 ta219_idtipotareapreventa)
        {
            OpenDbConn();

            DAL.TipoTareaPreventa cTipoTareaPreventa = new DAL.TipoTareaPreventa(cDblib);
            return cTipoTareaPreventa.Select(ta219_idtipotareapreventa);
        }

        public List<Models.TipoTareaPreventa> Grabar(string sCadena)
        {
            //string sDesc = "";
            Models.TipoTareaPreventa cTarea;
            List<Models.TipoTareaPreventa> lstTipoTareasPreventa = new List<Models.TipoTareaPreventa>();
            bool bConTransaccion = false;
            int orden = 0;
            Guid methodOwnerID = new Guid("5562DB82-40CE-4F2E-9FE4-4261A276A6A0");
            OpenDbConn();
            if (cDblib.Transaction.ownerID.Equals(new Guid()))
                bConTransaccion = true;
            if (bConTransaccion)
                cDblib.beginTransaction(methodOwnerID);

            try
            {
                string[] aFun = System.Text.RegularExpressions.Regex.Split(sCadena, "///");
                bool bActiva = false;

                foreach (string oFun in aFun)
                {
                    string[] aValores = System.Text.RegularExpressions.Regex.Split(oFun, "##");
                    //0. Opcion BD. "I", "U", "D"
                    //1. ID Cualificador
                    //2. Descripcion
                    //3. Partida
                    //4. ON
                    //5. SUPER
                    //sDesc = Utilidades.unescape(aValores[2]);

                    if (aValores[3] == "1") bActiva = true; else bActiva = false;
                    //sDesc = aValores[2];

                    if(aValores[0] != "D")
                    {
                        cTarea = new Models.TipoTareaPreventa();
                        cTarea.ta219_idtipotareapreventa = short.Parse(aValores[1]);
                        cTarea.ta219_denominacion = aValores[2];
                        cTarea.ta219_orden = orden++;
                        cTarea.ta219_estadoactiva = bActiva;

                        lstTipoTareasPreventa.Add(cTarea);

                    }                    

                    /*switch (aValores[0])
                    {
                        case "I":
                            this.Insert(cAccion);
                            break;
                        case "U":
                            cAccion.ta205_idtipoaccionpreventa = short.Parse(aValores[1]);
                            this.Update(cAccion);
                            break;
                        case "D":
                            this.Delete(short.Parse(aValores[1]));
                            break;
                    }*/
                }

               List<Models.TipoTareaPreventa>  lstCatalogoResultado = this.GrabarListaTareas(lstTipoTareasPreventa); 

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return lstCatalogoResultado;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID))
                    cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }finally
            {
                this.Dispose();
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
        ~TipoTareaPreventa()
        {
            Dispose(false);
        }

        #endregion


    }

}
