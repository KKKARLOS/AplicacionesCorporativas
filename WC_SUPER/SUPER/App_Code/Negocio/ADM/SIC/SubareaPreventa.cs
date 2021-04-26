using System;
using System.Collections.Generic;
using IB.SUPER.ADM.SIC.DAL;
using IB.SUPER.ADM.SIC.Models;
//Para el RegEx
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for SubareaPreventa
/// </summary>
namespace IB.SUPER.ADM.SIC.BLL
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

         public Models.SubareaPreventa SelectPorDenominacion(string t201_denominacion, Int32 ta200_idareapreventa)
        {
            OpenDbConn();

            DAL.SubareaPreventa cSubareaPreventa = new DAL.SubareaPreventa(cDblib);
            return cSubareaPreventa.SelectPorDenominacion(t201_denominacion, ta200_idareapreventa);
        }

        public Models.SubareaPreventa Select(Int32 ta201_idsubareapreventa)
        {
            OpenDbConn();

            DAL.SubareaPreventa cSubareaPreventa = new DAL.SubareaPreventa(cDblib);
            return cSubareaPreventa.Select(ta201_idsubareapreventa);
        }
        public Models.SubareaPreventa Select2(Int32 ta201_idsubareapreventa)
        {
            OpenDbConn();

            DAL.SubareaPreventa cSubareaPreventa = new DAL.SubareaPreventa(cDblib);
            return cSubareaPreventa.Select2(ta201_idsubareapreventa);
        }
        public int Insert(Models.SubareaPreventa oSubareaPreventa)
        {
            Guid methodOwnerID = new Guid("f70cdb21-9a9c-47a4-8046-4a0725a74b3d");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.SubareaPreventa cSubareaPreventa = new DAL.SubareaPreventa(cDblib);

                int idSubareaPreventa = cSubareaPreventa.Insert(oSubareaPreventa);

                //Finalizar transacción 
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.commitTransaction(methodOwnerID);

                return idSubareaPreventa;
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }
        }
        public int Update(Models.SubareaPreventa oSubareaPreventa)
        {
            Guid methodOwnerID = new Guid("9acb94e8-95f9-4ce9-9e2b-8500e4840b0e");

            OpenDbConn();

            if (cDblib.Transaction.ownerID.Equals(new Guid())) cDblib.beginTransaction(methodOwnerID);

            try
            {
                DAL.SubareaPreventa cSubareaPreventa = new DAL.SubareaPreventa(cDblib);

                int result = cSubareaPreventa.Update(oSubareaPreventa);

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
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID)) cDblib.rollbackTransaction(methodOwnerID);

                throw ex;
            }
        }

        public int Grabar(string strDatosBasicos, string strFiguras)
        {
            int nID = -1;
            string[] aDatosBasicos = null;
            IB.SUPER.ADM.SIC.Models.SubareaPreventa oSubArea = new IB.SUPER.ADM.SIC.Models.SubareaPreventa();
            IB.SUPER.ADM.SIC.Models.SubareaPreventa oSubAreaD= new IB.SUPER.ADM.SIC.Models.SubareaPreventa();
            IB.SUPER.ADM.SIC.BLL.SubareaPreventa oElem = new IB.SUPER.ADM.SIC.BLL.SubareaPreventa();
            IB.SUPER.ADM.SIC.BLL.FiguraSubareaPreventa oElemFig = new IB.SUPER.ADM.SIC.BLL.FiguraSubareaPreventa();

            #region Apertura de conexión y transacción
            bool bConTransaccion = false;
            Guid methodOwnerID = new Guid("9222133C-0A0E-4534-9ECA-543B80954936");
            OpenDbConn();
            if (cDblib.Transaction.ownerID.Equals(new Guid()))
                bConTransaccion = true;
            if (bConTransaccion)
                cDblib.beginTransaction(methodOwnerID);
            #endregion

            try
            {
                #region Datos Generales
                if (strDatosBasicos != "")//No se ha modificado nada de la pestaña general
                {
                    aDatosBasicos = Regex.Split(strDatosBasicos, "##");
                    ///aDatosBasicos[0] = ID
                    ///aDatosBasicos[1] = Denominacion
                    ///aDatosBasicos[2] = IDResponsable
                    ///aDatosBasicos[3] = Activo
                    ///aDatosBasicos[4] = IDPadre
                    ///aDatosBasicos[5] = Tipo de asignación de líder
                    oSubArea.ta200_idareapreventa = int.Parse(aDatosBasicos[4]);
                    oSubArea.ta201_denominacion = aDatosBasicos[1];
                    oSubArea.ta201_estadoactiva = (aDatosBasicos[3] == "1") ? true : false;
                    oSubArea.t001_idficepi_responsable = int.Parse(aDatosBasicos[2]);
                    oSubArea.ta201_permitirautoasignacionlider = (aDatosBasicos[5] == "1") ? true : false;

                    oSubAreaD = oElem.SelectPorDenominacion(oSubArea.ta201_denominacion, oSubArea.ta200_idareapreventa);

                    if (aDatosBasicos[0] == "") //insert
                    {
                        if (oSubAreaD != null) return nID;// throw new Exception("Ya existe un subárea con la misma denominación");
                        nID = oElem.Insert(oSubArea);
                    }
                    else //update
                    {
                        nID = int.Parse(aDatosBasicos[0]);
                        if (oSubAreaD != null && oSubAreaD.ta201_idsubareapreventa != nID) return -1;  //throw new Exception("Ya existe un subárea con la misma denominación");
                        oSubArea.ta201_idsubareapreventa = nID;
                        oElem.Update(oSubArea);
                    }
                }

                #endregion

                #region Datos Figuras
                List<IB.SUPER.ADM.SIC.Models.FiguraSubareaPreventa> lstFiguras = new List<IB.SUPER.ADM.SIC.Models.FiguraSubareaPreventa>();
                if (strFiguras != "")//No se ha modificado nada de la pestaña de Figuras
                {
                    string[] aUsuarios = Regex.Split(strFiguras, "///");
                    foreach (string oUsuario in aUsuarios)
                    {
                        if (oUsuario == "") continue;
                        string[] aFig = Regex.Split(oUsuario, "##");
                        ///aFig[0] = bd
                        ///aFig[1] = idUsuario
                        ///aFig[2] = Figuras

                        //if (aFig[0] == "D")
                        //    //PreventaSubareaFiguras.DeleteUsuario(tr, nID, int.Parse(aFig[1]));
                        //    oElemFig.Delete(nID, int.Parse(aFig[1]));
                        //else
                        //{
                        string[] aFiguras = Regex.Split(aFig[2], ",");
                        foreach (string oFigura in aFiguras)
                        {
                            if (oFigura == "") continue;
                            string[] aFig2 = Regex.Split(oFigura, "@");
                            ///aFig2[0] = bd
                            ///aFig2[1] = Figura
                            IB.SUPER.ADM.SIC.Models.FiguraSubareaPreventa oFig = new IB.SUPER.ADM.SIC.Models.FiguraSubareaPreventa();
                            //oFig.ta201_idsubareapreventa = nID;
                            oFig.t001_idficepi = int.Parse(aFig[1]);
                            oFig.ta203_figura = aFig2[1];
                            //if (aFig2[0] == "D")
                            //    oElemFig.DeleteFigura(nID, int.Parse(aFig[1]), aFig2[1]);
                            //else
                            //    oElemFig.Insert(oFig);
                            lstFiguras.Add(oFig);
                        }
                        //}
                    }
                    oElemFig.ActualizarFiguras(nID, lstFiguras);
                }
                #endregion

                if (bConTransaccion)
                    cDblib.commitTransaction(methodOwnerID);
            }
            catch (Exception ex)
            {
                //rollback
                if (cDblib.Transaction.ownerID.Equals(methodOwnerID))
                    cDblib.rollbackTransaction(methodOwnerID);

                //sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos del elemento de estructura", ex, false);
                throw new Exception("Error al grabar los datos. " + ex.Message);
            }
            finally
            {
                oElem.Dispose();
                oElemFig.Dispose();
            }
            return nID;
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
