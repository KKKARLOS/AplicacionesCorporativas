using System;
using System.Collections.Generic;
using System.Data;
using IB.SUPER.ADM.SIC.DAL;
using IB.SUPER.ADM.SIC.Models;

/// <summary>
/// Summary description for TipoAccionPreventa
/// </summary>
namespace IB.SUPER.ADM.SIC.BLL
{
    public class TipoAccionPreventa : IDisposable
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("d256e19c-56ac-4151-868a-966a1e0fe288");
        private bool disposed = false;

        #endregion

        #region Constructor

        public TipoAccionPreventa()
            : base()
        {
            //OpenDbConn();
        }

        public TipoAccionPreventa(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas

        public List<Models.TipoAccionPreventa> Catalogo()
        {
            OpenDbConn();

            DAL.TipoAccionPreventa cTipoAccionPreventa = new DAL.TipoAccionPreventa(cDblib);
            return cTipoAccionPreventa.Catalogo();

        }

        internal List<Models.TipoAccionPreventa> GrabarListaAcciones(List<Models.TipoAccionPreventa> lstTiposAccionPreventa)
        {
            Guid methodOwnerID = new Guid("f672bc44-f3fc-4102-8b50-2d3452b19198");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try

            {
                //Datatable de tipos de acciones preventa
                DataTable dtTipoAcciones = new DataTable();
                dtTipoAcciones.Columns.Add(new DataColumn("ta205_idtipoaccionpreventa", typeof(int)));
                dtTipoAcciones.Columns.Add(new DataColumn("ta205_denominacion", typeof(string)));
                dtTipoAcciones.Columns.Add(new DataColumn("ta205_origen_on", typeof(bool)));
                dtTipoAcciones.Columns.Add(new DataColumn("ta205_origen_partida", typeof(bool)));
                dtTipoAcciones.Columns.Add(new DataColumn("ta205_origen_super", typeof(bool)));
                dtTipoAcciones.Columns.Add(new DataColumn("ta205_unicaxaccion", typeof(bool)));
                dtTipoAcciones.Columns.Add(new DataColumn("ta205_orden", typeof(int)));
                dtTipoAcciones.Columns.Add(new DataColumn("ta205_activa", typeof(bool)));

                DataColumn colPlazo = new DataColumn("ta205_plazominreq", typeof(byte));
                colPlazo.AllowDBNull = true;
                dtTipoAcciones.Columns.Add(colPlazo);

                foreach (Models.TipoAccionPreventa oTipoAccion in lstTiposAccionPreventa)
                {
                    DataRow rowAccion = dtTipoAcciones.NewRow();
                    rowAccion["ta205_idtipoaccionpreventa"] = oTipoAccion.ta205_idtipoaccionpreventa;
                    rowAccion["ta205_denominacion"] = oTipoAccion.ta205_denominacion;
                    rowAccion["ta205_origen_on"] = oTipoAccion.ta205_origen_on;
                    rowAccion["ta205_origen_partida"] = oTipoAccion.ta205_origen_partida;
                    rowAccion["ta205_origen_super"] = oTipoAccion.ta205_origen_super;
                    rowAccion["ta205_unicaxaccion"] = oTipoAccion.ta205_unicaxaccion;
                    rowAccion["ta205_orden"] = oTipoAccion.ta205_orden;
                    rowAccion["ta205_activa"] = oTipoAccion.ta205_estadoactiva;
                    if (oTipoAccion.ta205_plazominreq != null)
                        rowAccion["ta205_plazominreq"] = oTipoAccion.ta205_plazominreq;
                    dtTipoAcciones.Rows.Add(rowAccion);
                }


                DAL.TipoAccionPreventa cTipoAccionPreventa = new DAL.TipoAccionPreventa(cDblib);

                List <Models.TipoAccionPreventa> lstTipoAccionPreventa = cTipoAccionPreventa.GrabarAcciones(dtTipoAcciones);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return lstTipoAccionPreventa;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }
        }

       /* internal int Insert(Models.TipoAccionPreventa oTipoAccionPreventa)
        {
            Guid methodOwnerID = new Guid("f672bc44-f3fc-4102-8b50-2d3452b19198");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.TipoAccionPreventa cTipoAccionPreventa = new DAL.TipoAccionPreventa(cDblib);

                int idTipoAccionPreventa = cTipoAccionPreventa.Insert(oTipoAccionPreventa);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return idTipoAccionPreventa;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }
        }

        internal int Update(Models.TipoAccionPreventa oTipoAccionPreventa)
        {
            Guid methodOwnerID = new Guid("e0d6bdd5-2033-48cd-bb3d-80c7524647c4");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.TipoAccionPreventa cTipoAccionPreventa = new DAL.TipoAccionPreventa(cDblib);

                int result = cTipoAccionPreventa.Update(oTipoAccionPreventa);

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

        internal int Delete(Int16 ta205_idtipoaccionpreventa)
        {
            Guid methodOwnerID = new Guid("324a1f0b-bd9a-4614-abf1-6735001c79fe");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {

                DAL.TipoAccionPreventa cTipoAccionPreventa = new DAL.TipoAccionPreventa(cDblib);

                int result = cTipoAccionPreventa.Delete(ta205_idtipoaccionpreventa);

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
        internal Models.TipoAccionPreventa Select(Int16 ta205_idtipoaccionpreventa)
        {
            OpenDbConn();

            DAL.TipoAccionPreventa cTipoAccionPreventa = new DAL.TipoAccionPreventa(cDblib);
            return cTipoAccionPreventa.Select(ta205_idtipoaccionpreventa);
        }

        public List<Models.TipoAccionPreventa> Grabar(string sCadena)
        {
            //string sDesc = "";
            Models.TipoAccionPreventa cAccion;
            List<Models.TipoAccionPreventa> lstTipoAccionesPreventa = new List<Models.TipoAccionPreventa>();
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
                bool bPartida = false, bON = false, bSP = false, bUA = false, bActiva = false;
                byte? nPMR= null;

                foreach (string oFun in aFun)
                {
                    string[] aValores = System.Text.RegularExpressions.Regex.Split(oFun, "##");
                    //0. Opcion BD. "I", "U", "D"
                    //1. ID Cualificador
                    //2. Descripcion
                    //3. Partida
                    //4. ON
                    //5. SUPER
                    //6. Unica por acción
                    //7. Activa
                    //8. Plazo mínimo requerido
                    //sDesc = Utilidades.unescape(aValores[2]);

                    if (aValores[3] == "1") bPartida = true; else bPartida = false;
                    if (aValores[4] == "1") bON = true; else bON = false;
                    if (aValores[5] == "1") bSP = true; else bSP = false;
                    if (aValores[6] == "1") bUA = true; else bUA = false;
                    if (aValores[7] == "1") bActiva = true; else bActiva = false;
                    //sDesc = aValores[2];
                    if (aValores[8] != "")
                        nPMR = byte.Parse(aValores[8]);
                    else
                        nPMR = null;

                    if (aValores[0] != "D")
                    {
                        cAccion = new Models.TipoAccionPreventa();
                        cAccion.ta205_idtipoaccionpreventa = short.Parse(aValores[1]);
                        cAccion.ta205_denominacion = aValores[2];
                        cAccion.ta205_origen_on = bON;
                        cAccion.ta205_origen_partida = bPartida;
                        cAccion.ta205_origen_super = bSP;
                        cAccion.ta205_unicaxaccion = bUA;
                        cAccion.ta205_orden = orden++;
                        cAccion.ta205_estadoactiva = bActiva;
                        cAccion.ta205_plazominreq = nPMR;

                        lstTipoAccionesPreventa.Add(cAccion);

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

               List<Models.TipoAccionPreventa>  lstCatalogoResultado = this.GrabarListaAcciones(lstTipoAccionesPreventa); 

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
        ~TipoAccionPreventa()
        {
            Dispose(false);
        }

        #endregion


    }

}
