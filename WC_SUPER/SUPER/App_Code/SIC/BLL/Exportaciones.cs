using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Web;
using IB.SUPER.SIC.DAL;
using IB.SUPER.SIC.Models;


/// <summary>
/// Descripción breve de Exportaciones
/// </summary>
namespace IB.SUPER.SIC.BLL
{
    public class Exportaciones
    {
        #region Variables privadas

        private sqldblib.SqlServerSP cDblib = null;
        private Guid classOwnerID = new Guid("E072893F-6C5F-42F0-B540-BFD8E468A1B8");
        private bool disposed = false;

        #endregion

        #region Constructor

        public Exportaciones()
            : base()
        {
            //OpenDbConn();
        }

        public Exportaciones(sqldblib.SqlServerSP extcDblib)
            : base()
        {
            AttachDbConn(extcDblib);
        }

        #endregion

        #region Funciones públicas

        public byte[] ExportarExcel(string exportid, string origenMenu, string filters)
        {
            //El 15/06/2018 María nos pide que quitemos las lineas de cabecera del Excel para facilitar la creación de tablas dinámicas
            byte[] bytearr = null;
            DataTable dtBody = null;
            DataTable dtHead = null;
            string filename = "";
            bool admin = origenMenu == "ADM" ? true : false;
            Hashtable ht = Shared.Utils.ParseQuerystringFilters(filters);
            

            OpenDbConn();

            DAL.Exportaciones cExport = new DAL.Exportaciones(cDblib);

            switch (exportid)
            {
                #region Acciones
                case "acciones":
                    //Obtener datatable de bbdd
                    Models.ExportAccionesFilter o = new Models.ExportAccionesFilter();
                    if (ht["estado"] != null) o.estado = ht["estado"].ToString();
                    if (ht["itemorigen"] != null) o.itemorigen = ht["itemorigen"].ToString();
                    if (ht["iditemorigen"] != null) o.iditemorigen = ht["iditemorigen"].ToString();
                    if (ht["importeDesde"] != null) o.importeDesde = ht["importeDesde"].ToString();
                    if (ht["importeHasta"] != null) o.importeHasta = ht["importeHasta"].ToString();
                    if (ht["ffinDesde"] != null) o.ffinDesde = ht["ffinDesde"].ToString();
                    if (ht["ffinHasta"] != null) o.ffinHasta = ht["ffinHasta"].ToString();
                    if (ht["promotor"] != null) o.promotor = ht["promotor"].ToString();
                    if (ht["comercial"] != null) o.comercial = ht["comercial"].ToString();
                    if (ht["lideres"] != null) o.lideres = obtenerListaIds(ht["lideres"].ToString());
                    if (ht["clientes"] != null) o.clientes = obtenerListaIds(ht["clientes"].ToString());
                    if (ht["acciones"] != null) o.acciones = obtenerListaIds(ht["acciones"].ToString());
                    if (ht["unidades"] != null) o.unidades = obtenerListaIds(ht["unidades"].ToString());
                    if (ht["areas"] != null) o.areas = obtenerListaIds(ht["areas"].ToString());
                    if (ht["subareas"] != null) o.subareas = obtenerListaIds(ht["subareas"].ToString());

                    dtBody = cExport.Acciones(admin, (int)HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"], o);
                                       
                    //Si no hay filas devolver null, 
                    if (dtBody.Rows.Count == 0) return bytearr;

                    //dtBody.TableName = "BODY-export";
                    dtBody.TableName = "Acciones";
                    //dtHead = ObtenerDatatableHEAD("HEAD-export", "Acciones preventa");

                    filename = "acciones.xlsx";

                    break;
                #endregion
                #region accionesMiAmbito
                //case "accionesMiAmbito":
                //    //Obtener datatable de bbdd
                //    Models.ExportAccionesFilter oAV = new Models.ExportAccionesFilter();
                //    if (ht["estado"] != null) oAV.estado = ht["estado"].ToString();
                //    //if (ht["itemorigen"] != null) oAV.itemorigen = ht["itemorigen"].ToString();
                //    //if (ht["iditemorigen"] != null) oAV.iditemorigen = ht["iditemorigen"].ToString();
                //    if (ht["importeDesde"] != null) oAV.importeDesde = ht["importeDesde"].ToString();
                //    if (ht["importeHasta"] != null) oAV.importeHasta = ht["importeHasta"].ToString();
                //    if (ht["ffinDesde"] != null) oAV.ffinDesde = ht["ffinDesde"].ToString();
                //    if (ht["ffinHasta"] != null) oAV.ffinHasta = ht["ffinHasta"].ToString();
                //    if (ht["promotor"] != null) oAV.promotor = ht["promotor"].ToString();
                //    if (ht["comercial"] != null) oAV.comercial = ht["comercial"].ToString();
                //    if (ht["lideres"] != null) oAV.lideres = obtenerListaIds(ht["lideres"].ToString());
                //    if (ht["clientes"] != null) oAV.clientes = obtenerListaIds(ht["clientes"].ToString());
                //    if (ht["acciones"] != null) oAV.acciones = obtenerListaIds(ht["acciones"].ToString());
                //    if (ht["unidades"] != null) oAV.unidades = obtenerListaIds(ht["unidades"].ToString());
                //    if (ht["areas"] != null) oAV.areas = obtenerListaIds(ht["areas"].ToString());
                //    if (ht["subareas"] != null) oAV.subareas = obtenerListaIds(ht["subareas"].ToString());

                //    dtBody = cExport.AccionesMiVision(admin, (int)HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"], oAV);

                //    //Eliminar las columnas no necesarias
                //    ArrayList arl = new ArrayList();
                //    foreach (DataColumn c in dtBody.Columns)
                //    {
                //        if (c.ColumnName.ToLower().Trim() != "ta204_idaccionpreventa"
                //            && c.ColumnName.ToLower().Trim() != "tipoAccion"
                //            && c.ColumnName.ToLower().Trim() != "ta206_iditemorigen"
                //            && c.ColumnName.ToLower().Trim() != "importe" && c.ColumnName.ToLower().Trim() != "den_cuenta"
                //            && c.ColumnName.ToLower().Trim() != "promotor" && c.ColumnName.ToLower().Trim() != "comercial"
                //            && c.ColumnName.ToLower().Trim() != "den_unidadcomercial" && c.ColumnName.ToLower().Trim() != "areaPreventa"
                //            && c.ColumnName.ToLower().Trim() != "subareaPreventa" && c.ColumnName.ToLower().Trim() != "lider"
                //            && c.ColumnName.ToLower().Trim() != "estadoorigen" && c.ColumnName.ToLower().Trim() != "ta204_fechacreacion"
                //            && c.ColumnName.ToLower().Trim() != "ta204_fechafinestipulada" && c.ColumnName.ToLower().Trim() != "ta204_fechafinreal"
                //            )
                //        {
                //            arl.Add(c);
                //        }

                //    }
                //    foreach (DataColumn dc in arl)
                //    {
                //        dtBody.Columns.Remove(dc.ColumnName);
                //    }

                //    //Renombar columnas
                //    dtBody.Columns["ta204_idaccionpreventa"].ColumnName = "Referencia";
                //    //dtBody.Columns["tipoAccion"].ColumnName = "Acción preventa";
                //    dtBody.Columns["importe"].ColumnName = "Importe";


                //    //Si no hay filas devolver null, 
                //    if (dtBody.Rows.Count == 0) return bytearr;

                //    dtBody.TableName = "BODY-export";
                //    dtHead = ObtenerDatatableHEAD("HEAD-export", "Consulta y acceso a las acciones preventa bajo mi ámbito de visión");

                //    filename = "accionesMiVision.xlsx";

                //    break;
                #endregion
                #region Tareas
                case "tareas":
                    //Obtener datatable de bbdd
                    Models.ExportTareasFilter p = new Models.ExportTareasFilter();
                    if (ht["estado"] != null) p.estado = ht["estado"].ToString();
                    if (ht["estado_tarea"] != null) p.estado_tarea = ht["estado_tarea"].ToString();
                    if (ht["itemorigen"] != null) p.itemorigen = ht["itemorigen"].ToString();
                    if (ht["iditemorigen"] != null) p.iditemorigen = ht["iditemorigen"].ToString();
                    if (ht["importeDesde"] != null) p.importeDesde = ht["importeDesde"].ToString();
                    if (ht["importeHasta"] != null) p.importeHasta = ht["importeHasta"].ToString();
                    if (ht["ffinDesde"] != null) p.ffinDesde = ht["ffinDesde"].ToString();
                    if (ht["ffinHasta"] != null) p.ffinHasta = ht["ffinHasta"].ToString();
                    if (ht["ffinDesde_tarea"] != null) p.ffinDesde_tarea = ht["ffinDesde_tarea"].ToString();
                    if (ht["ffinHasta_tarea"] != null) p.ffinHasta_tarea = ht["ffinHasta_tarea"].ToString();
                    if (ht["promotor"] != null) p.promotor = ht["promotor"].ToString();
                    if (ht["comercial"] != null) p.comercial = ht["comercial"].ToString();
                    if (ht["lideres"] != null) p.lideres = obtenerListaIds(ht["lideres"].ToString());
                    if (ht["clientes"] != null) p.clientes = obtenerListaIds(ht["clientes"].ToString());
                    if (ht["acciones"] != null) p.acciones = obtenerListaIds(ht["acciones"].ToString());
                    if (ht["unidades"] != null) p.unidades = obtenerListaIds(ht["unidades"].ToString());
                    if (ht["areas"] != null) p.areas = obtenerListaIds(ht["areas"].ToString());
                    if (ht["subareas"] != null) p.subareas = obtenerListaIds(ht["subareas"].ToString());

                    dtBody = cExport.Tareas(admin, (int)HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"], p);

                    //Si no hay filas devolver null, 
                    if (dtBody.Rows.Count == 0) return bytearr;

                    //dtBody.TableName = "BODY-export";
                    dtBody.TableName = "Tareas";
                    //dtHead = ObtenerDatatableHEAD("HEAD-export", "Tareas preventa");

                    filename = "tareas.xlsx";

                    break;
                #endregion
                #region Acciones y Tareas
                case "accionestareas":
                    //Obtener datatable de bbdd
                    Models.ExportTareasFilter p2 = new Models.ExportTareasFilter();
                    if (ht["estado"] != null) p2.estado = ht["estado"].ToString();
                    if (ht["estado_tarea"] != null) p2.estado_tarea = ht["estado_tarea"].ToString();
                    if (ht["itemorigen"] != null) p2.itemorigen = ht["itemorigen"].ToString();
                    if (ht["iditemorigen"] != null) p2.iditemorigen = ht["iditemorigen"].ToString();
                    if (ht["importeDesde"] != null) p2.importeDesde = ht["importeDesde"].ToString();
                    if (ht["importeHasta"] != null) p2.importeHasta = ht["importeHasta"].ToString();
                    if (ht["ffinDesde"] != null) p2.ffinDesde = ht["ffinDesde"].ToString();
                    if (ht["ffinHasta"] != null) p2.ffinHasta = ht["ffinHasta"].ToString();
                    if (ht["ffinDesde_tarea"] != null) p2.ffinDesde_tarea = ht["ffinDesde_tarea"].ToString();
                    if (ht["ffinHasta_tarea"] != null) p2.ffinHasta_tarea = ht["ffinHasta_tarea"].ToString();
                    if (ht["promotor"] != null) p2.promotor = ht["promotor"].ToString();
                    if (ht["comercial"] != null) p2.comercial = ht["comercial"].ToString();
                    if (ht["lideres"] != null) p2.lideres = obtenerListaIds(ht["lideres"].ToString());
                    if (ht["clientes"] != null) p2.clientes = obtenerListaIds(ht["clientes"].ToString());
                    if (ht["acciones"] != null) p2.acciones = obtenerListaIds(ht["acciones"].ToString());
                    if (ht["unidades"] != null) p2.unidades = obtenerListaIds(ht["unidades"].ToString());
                    if (ht["areas"] != null) p2.areas = obtenerListaIds(ht["areas"].ToString());
                    if (ht["subareas"] != null) p2.subareas = obtenerListaIds(ht["subareas"].ToString());

                    dtBody = cExport.AccionesTareas(admin, (int)HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"], p2);

                    //Si no hay filas devolver null, 
                    if (dtBody.Rows.Count == 0) return bytearr;

                    //dtBody.TableName = "BODY-export";
                    dtBody.TableName = "AccionesTareas";
                    //dtHead = ObtenerDatatableHEAD("HEAD-export", "Acciones y tareas preventa");

                    filename = "accionestareas.xlsx";

                    break;
                #endregion

                #region cargatrabajo
                case "cargatrabajo":
                    //Obtener datatable de bbdd
                    Models.ExportCargaTrabajoFilter q = new Models.ExportCargaTrabajoFilter();
                    if (ht["estado"] != null) q.estado = ht["estado"].ToString();
                    if (ht["estado_tarea"] != null) q.estado_tarea = ht["estado_tarea"].ToString();
                    if (ht["ffinDesde"] != null) q.ffinDesde = ht["ffinDesde"].ToString();
                    if (ht["ffinHasta"] != null) q.ffinHasta = ht["ffinHasta"].ToString();
                    if (ht["ffinDesde_tarea"] != null) q.ffinDesde_tarea = ht["ffinDesde_tarea"].ToString();
                    if (ht["ffinHasta_tarea"] != null) q.ffinHasta_tarea = ht["ffinHasta_tarea"].ToString();
                    if (ht["lideres"] != null) q.lideres = obtenerListaIds(ht["lideres"].ToString());
                    if (ht["unidades"] != null) q.unidades = obtenerListaIds(ht["unidades"].ToString());
                    if (ht["areas"] != null) q.areas = obtenerListaIds(ht["areas"].ToString());
                    if (ht["subareas"] != null) q.subareas = obtenerListaIds(ht["subareas"].ToString());

                    dtBody = cExport.CargaTrabajo(admin, (int)HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"], q);

                    //Si no hay filas devolver null, 
                    if (dtBody.Rows.Count == 0) return bytearr;

                    //dtBody.TableName = "BODY-export";
                    dtBody.TableName = "CargaTrabajo";
                    //dtHead = ObtenerDatatableHEAD("HEAD-export", "Carga de trabajo");

                    filename = "cargatrabajo.xlsx";

                    break;
                #endregion

                default:
                    return bytearr;
            }
            

            DataSet ds = new DataSet();
            //ds.Tables.Add(dtHead);
            ds.Tables.Add(dtBody);

            //Llamada al ibservioffice para obtener el excel
            svcEXCEL.IsvcEXCELClient osvcEXCEL = null;
            try
            {
                osvcEXCEL = new svcEXCEL.IsvcEXCELClient();
                return osvcEXCEL.getExcelFromDataSet(ds, filename);
            }
            catch (FaultException<svcEXCEL.IBOfficeException> cex)
            {
                throw new Exception(cex.Detail.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (osvcEXCEL != null && osvcEXCEL.State != System.ServiceModel.CommunicationState.Closed)
                {
                    if (osvcEXCEL.State != System.ServiceModel.CommunicationState.Faulted) osvcEXCEL.Close();
                    else osvcEXCEL.Abort();
                }
            }
        }
        #endregion

        #region Funciones privadas
        private string[] obtenerListaIds(string s) {

            string[] arr = s.Split(';');

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].Contains("#")) arr[i] = arr[i].Substring(0, arr[i].IndexOf('#'));
            }

            return arr;
        }

        private string[] obtenerListaDescs(string s)
        {

            string[] arr = s.Split(';');

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].Contains("#")) arr[i] = arr[i].Substring(arr[i].IndexOf('#') + 1);
            }

            return arr;
        }

        private DataTable ObtenerDatatableHEAD(string datatableName, string tituloExcel)
        {

            DataTable dtHead = new DataTable();
            dtHead.TableName = datatableName;
            DataColumn dtc = new DataColumn("clave");
            dtHead.Columns.Add(dtc);
            dtc = new DataColumn("valor");
            dtHead.Columns.Add(dtc);

            DataRow oRow = dtHead.NewRow();
            oRow[0] = "title";
            oRow[1] = tituloExcel;
            dtHead.Rows.InsertAt(oRow, 0);

            return dtHead;


        }
        
        
        //}
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
        ~Exportaciones()
        {
            Dispose(false);
        }

        #endregion

    }
}