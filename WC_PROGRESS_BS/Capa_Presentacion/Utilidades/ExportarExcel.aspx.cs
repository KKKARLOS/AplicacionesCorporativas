using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;

public partial class Capa_Presentacion_Utilidades_ExportarExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string sExtension = ".xlsx";
        string sFilename = "";
        byte[] oExcel = null;
        string pantalla = Request.QueryString["pantalla"];
        DataSet ds = new DataSet();

        Response.ClearContent();
        Response.ClearHeaders();
        Response.Buffer = true;


        svcEXCEL.IsvcEXCELClient osvcEXCEL = new svcEXCEL.IsvcEXCELClient();
        try
        {

            switch (pantalla)
            {
                //Exportación del equipo desde la pantalla de desglose de rol
                case "desgloserol":

                    //recuperar parametros del querystring
                    int idficepi = int.Parse(Request.QueryString["idficepi"]);
                    int parentesco = int.Parse(Request.QueryString["parentesco"]);
                    string idficepitext = Request.QueryString["idficepitext"];
                    string parentescotext = Request.QueryString["parentescotext"];
                    int numprofesionales = int.Parse(Request.QueryString["numprofesionales"]);

                    sFilename = "desglose por rol" + sExtension;

                    IB.Progress.BLL.DesgloseRol dRol = null;
                    try
                    {
                        //Datatable de cabecera
                        DataTable dtH = new DataTable("HEAD-Excel");
                        DataColumn dtc = new DataColumn("clave");
                        dtH.Columns.Add(dtc);
                        dtc = new DataColumn("valor");
                        dtH.Columns.Add(dtc);
                        DataRow oRow = dtH.NewRow();
                        oRow[0] = "title";
                        oRow[1] = "Profesionales por rol";
                        dtH.Rows.Add(oRow);

                        oRow = dtH.NewRow();
                        oRow[0] = "Evaluador";
                        oRow[1] = idficepitext;
                        dtH.Rows.Add(oRow);

                        oRow = dtH.NewRow();
                        oRow[0] = "Nivel de dependencia";
                        oRow[1] = parentescotext;
                        dtH.Rows.Add(oRow);

                        oRow = dtH.NewRow();
                        oRow[0] = "Total profesionales";
                        oRow[1] = numprofesionales;
                        dtH.Rows.Add(oRow);

                       
                        //Datatable de cuerpo
                        List<IB.Progress.Models.DesgloseRol> desgloseRol = null;
                        dRol = new IB.Progress.BLL.DesgloseRol();
                        desgloseRol = dRol.catalogoDesgloseRol(idficepi, parentesco);
                        dRol.Dispose();

                        //eliminar las filas con profesional = "";
                        desgloseRol = (from o in desgloseRol
                                       where o.Profesional.Length > 0
                                       select o).ToList<IB.Progress.Models.DesgloseRol>();

                        //convertir la lista a datatable
                        DataTable dtbody = desgloseRol.CopyGenericToDataTable<IB.Progress.Models.DesgloseRol>();
                        dtbody.TableName = "BODY-Excel";

                        //Eliminar las columnas no necesarias
                        ArrayList arl = new ArrayList();
                        foreach (DataColumn c in dtbody.Columns)
                        {
                            if (c.ColumnName.ToLower().Trim() != "desrol" && c.ColumnName.ToLower().Trim() != "profesional")
                                arl.Add(c);

                        }
                        foreach (DataColumn dc in arl)
                        {
                            dtbody.Columns.Remove(dc.ColumnName);
                        }

                        //renombar columnas
                        dtbody.Columns["desrol"].ColumnName = "Rol";
                        dtbody.Columns["profesional"].ColumnName = "Profesional";

                        //reordenar columnas
                        dtbody.Columns["Rol"].SetOrdinal(0);
                        dtbody.Columns["Profesional"].SetOrdinal(1);


                        //Agregar al dataset
                        ds.Tables.Add(dtH);
                        ds.Tables.Add(dtbody);
                        

                        //exportar
                        oExcel = osvcEXCEL.getExcelFromDataSet(ds, sFilename);

                    
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (dRol != null) dRol.Dispose();
                    }
                    break;

                case "solicitudesaprobadas":

                    //recuperar parametros del querystring
                    int idficepiSolicitudesaprobadas = int.Parse(Request.QueryString["idficepi"]);

                    IB.Progress.BLL.TramitacionCambioRol dTramitacion = null;
                    try
                    {
                        //Datatable de cabecera
                        DataTable dtH = new DataTable("HEAD-Excel");
                        DataColumn dtc = new DataColumn("clave");
                        dtH.Columns.Add(dtc);
                        dtc = new DataColumn("valor");
                        dtH.Columns.Add(dtc);
                        DataRow oRow = dtH.NewRow();
                        oRow[0] = "title";
                        oRow[1] = "Solicitudes aprobadas";
                        dtH.Rows.Add(oRow);
                        
                        //Datatable de cuerpo
                        List<IB.Progress.Models.TramitacionCambioRol> tramit = null;
                        dTramitacion = new IB.Progress.BLL.TramitacionCambioRol();
                        tramit = dTramitacion.getSolicitudesSegunEstado(Convert.ToChar("P"), idficepiSolicitudesaprobadas );
                        dTramitacion.Dispose();
                        
                        ////convertir la lista a datatable
                        DataTable dtbody = tramit.CopyGenericToDataTable<IB.Progress.Models.TramitacionCambioRol>();
                        dtbody.TableName = "BODY-Excel";


                        //Eliminar las columnas no necesarias
                        ArrayList arl = new ArrayList();
                        foreach (DataColumn c in dtbody.Columns)
                        {
                            if (c.ColumnName.ToLower().Trim() != "nombre_interesado" && c.ColumnName.ToLower().Trim() != "nombre_promotor"
                                 && c.ColumnName.ToLower().Trim() != "t940_desrolactual" && c.ColumnName.ToLower().Trim() != "t940_desrolpropuesto"
                                 && c.ColumnName.ToLower().Trim() != "t940_motivopropuesto" && c.ColumnName.ToLower().Trim() != "t940_fechaproposicion"
                                 && c.ColumnName.ToLower().Trim() != "aprobador" && c.ColumnName.ToLower().Trim() != "t940_fecharesolucion")
                                arl.Add(c);

                        }
                        foreach (DataColumn dc in arl)
                        {
                            dtbody.Columns.Remove(dc.ColumnName);
                        }

                        
                        dtbody.Columns["nombre_interesado"].ColumnName = "Profesional";
                        dtbody.Columns["nombre_promotor"].ColumnName = "Evaluador";
                        dtbody.Columns["t940_desrolActual"].ColumnName = "Rol actual";
                        dtbody.Columns["t940_desrolPropuesto"].ColumnName = "Rol propuesto";
                        dtbody.Columns["t940_motivopropuesto"].ColumnName = "Motivo";
                        dtbody.Columns["t940_fechaproposicion"].ColumnName = "Proposición";
                        dtbody.Columns["aprobador"].ColumnName = "Aprobador";
                        dtbody.Columns["t940_fecharesolucion"].ColumnName = "Aprobación";

                        //Reordenar columnas
                        dtbody.Columns["Profesional"].SetOrdinal(0);
                        dtbody.Columns["Evaluador"].SetOrdinal(1);
                        dtbody.Columns["Rol actual"].SetOrdinal(2);
                        dtbody.Columns["Rol propuesto"].SetOrdinal(3);
                        dtbody.Columns["Motivo"].SetOrdinal(4);
                        dtbody.Columns["Proposición"].SetOrdinal(5);
                        dtbody.Columns["Aprobador"].SetOrdinal(6);
                        dtbody.Columns["Aprobación"].SetOrdinal(7);

                        //Agregar al dataset
                        ds.Tables.Add(dtH);
                        ds.Tables.Add(dtbody);

                        //exportar
                        oExcel = osvcEXCEL.getExcelFromDataSet(ds, sFilename);

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    finally
                    {
                        if (dTramitacion != null) dTramitacion.Dispose();
                    }
                    break;
                    
                case "formaciondemandada":
                    
                    //recuperar parametros del querystring
                    int idficepiFormacionDemandada = int.Parse(Request.QueryString["idficepi"]);                    
                    int desde = int.Parse(Request.QueryString["desde"]);
                    int hasta = int.Parse(Request.QueryString["hasta"]);
                    string selmesinitext = Request.QueryString["mesinitext"];
                    string selanoinitext = Request.QueryString["anoinitext"];
                    string selmesfintext = Request.QueryString["mesfintext"];
                    string selanofintext = Request.QueryString["anofintext"];
                    int numevaluadosFormacion = int.Parse(Request.QueryString["numevaluaciones"]);

                    Nullable<short> colectivo = short.Parse(Request.QueryString["colectivo"]);
                    string colectivotext = Request.QueryString["colectivotext"];

                    sFilename = "Formación demandada" + sExtension;
                    IB.Progress.BLL.FormacionDemandada dFormacion = null;
                    try
                    {                        
                        //Datatable de cabecera
                        DataTable dtH = new DataTable("HEAD-Excel");
                        DataColumn dtc = new DataColumn("clave");
                        dtH.Columns.Add(dtc);
                        dtc = new DataColumn("valor");
                        dtH.Columns.Add(dtc);
                        DataRow oRow = dtH.NewRow();
                        oRow[0] = "title";
                        oRow[1] = "Formación demandada";
                        dtH.Rows.Add(oRow);

                        oRow = dtH.NewRow();
                        oRow[0] = "Desde";
                        oRow[1] = selmesinitext + " " + selanoinitext;
                        dtH.Rows.Add(oRow);

                        oRow = dtH.NewRow();
                        oRow[0] = "Hasta";
                        oRow[1] = selmesfintext + " " + selanofintext;
                        dtH.Rows.Add(oRow);

                        oRow = dtH.NewRow();
                        oRow[0] = "Colectivo";
                        oRow[1] = colectivotext;
                        dtH.Rows.Add(oRow);

                        oRow = dtH.NewRow();
                        oRow[0] = "Total evaluaciones";
                        oRow[1] = numevaluadosFormacion;
                        dtH.Rows.Add(oRow);

                        //Datatable de cuerpo
                        IB.Progress.Models.FormacionDemandada formacionDemandada = null;
                        dFormacion = new IB.Progress.BLL.FormacionDemandada();
                        formacionDemandada = dFormacion.catFormacionDemandada(idficepiFormacionDemandada, desde, hasta, colectivo);
                        dFormacion.Dispose();

                        ////convertir la lista a datatable
                        DataTable dtbody = formacionDemandada.FormacionDemandadaS1.CopyGenericToDataTable<IB.Progress.Models.FormacionDemandadaSelect1>();
                        dtbody.TableName = "BODY-Excel";

                        ////Eliminar las columnas no necesarias      
                        dtbody.Columns.Remove("t930_idvaloracion");
                        dtbody.Columns.Remove("idformulario");

                        //Agregar al dataset
                        ds.Tables.Add(dtH);
                        ds.Tables.Add(dtbody);

                        //exportar
                        oExcel = osvcEXCEL.getExcelFromDataSet(ds, sFilename);



                    }
                    catch (Exception ex)
                    {                        
                        throw ex;
                    }

                    finally{
                        if (dFormacion!= null) dFormacion.Dispose();
                    }
                    break;


                case "arboldependencias":

                    //recuperar parametros del querystring
                    int idficepiArbol = int.Parse(Request.QueryString["idficepi"]);                    
                    string idficepitextArbol = Request.QueryString["idficepitext"];
                    int idficepievaluador = int.Parse(Request.QueryString["idficepievaluador"]);
                    string evaluadortext = Request.QueryString["evaluadortext"];
                    string rolevaluador = Request.QueryString["rolevaluador"];
                    int numevaluadosArbol = int.Parse(Request.QueryString["numevaluados"]);

                    sFilename = "Árbol de dependencias" + sExtension;

                    IB.Progress.BLL.ArbolDependencias dArbol = null;

                    try
                    {
                        //Datatable de cabecera
                        DataTable dtH = new DataTable("HEAD-Excel");
                        DataColumn dtc = new DataColumn("clave");
                        dtH.Columns.Add(dtc);
                        dtc = new DataColumn("valor");
                        dtH.Columns.Add(dtc);
                        DataRow oRow = dtH.NewRow();
                        oRow[0] = "title";
                        oRow[1] = "Árbol de dependencias";
                        dtH.Rows.Add(oRow);

                        oRow = dtH.NewRow();
                        oRow[0] = "Evaluador/a";
                        oRow[1] = evaluadortext;
                        dtH.Rows.Add(oRow);

                        oRow = dtH.NewRow();
                        oRow[0] = "Rol del evaluador/a";
                        oRow[1] = rolevaluador;
                        dtH.Rows.Add(oRow);

                        oRow = dtH.NewRow();
                        oRow[0] = "Total evaluados/as";
                        oRow[1] = numevaluadosArbol;
                        dtH.Rows.Add(oRow);

                        //Datatable cuerpo
                        List<IB.Progress.Models.ArbolDependencias> arbolDependencias = null;
                        dArbol = new IB.Progress.BLL.ArbolDependencias();
                        arbolDependencias = dArbol.catalogoArbolDependencias(idficepievaluador);
                        dArbol.Dispose();

                      
                        //eliminar las filas con profesional = "";
                        arbolDependencias = (from o in arbolDependencias                                       
                                       select o).ToList<IB.Progress.Models.ArbolDependencias>();

                        //convertir la lista a datatable
                        DataTable dtbody = arbolDependencias.CopyGenericToDataTable<IB.Progress.Models.ArbolDependencias>();
                        dtbody.TableName = "BODY-Excel";

                        //Eliminar las columnas no necesarias
                        ArrayList arl = new ArrayList();
                        foreach (DataColumn c in dtbody.Columns)
                        {
                            if (c.ColumnName.ToLower().Trim() != "evaluado" && c.ColumnName.ToLower().Trim() != "roldelevaluado")                                
                                arl.Add(c);
                        }
                        foreach (DataColumn dc in arl)
                        {
                            dtbody.Columns.Remove(dc.ColumnName);
                        }

                        //dtbody.Rows.Remove([dtbody.Rows[0])];
                        dtbody.Rows[0].Delete();

                        //renombar columnas
                        dtbody.Columns["evaluado"].ColumnName = "Evaluado/a";
                        dtbody.Columns["roldelevaluado"].ColumnName = "Rol del evaluado/a";

                        
                        //Agregar al dataset
                        ds.Tables.Add(dtH);
                        ds.Tables.Add(dtbody);

                        

                        //exportar
                        oExcel = osvcEXCEL.getExcelFromDataSet(ds, sFilename);
                       
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally {
                        if (dArbol != null) dArbol.Dispose();
                    }

                    break;



                case "arboldependenciasALL":

                    //recuperar parametros del querystring
                    int idficepiArbolALL = int.Parse(Request.QueryString["idficepi"]);
                    string idficepitextArbolALL = Request.QueryString["idficepitext"];
                    int idficepievaluadorALL = int.Parse(Request.QueryString["idficepievaluador"]);
                    string evaluadortextALL = Request.QueryString["evaluadortext"];
                    string rolevaluadorALL = Request.QueryString["rolevaluador"];
                    int numevaluadosArbolALL = int.Parse(Request.QueryString["numevaluados"]);

                    sFilename = "Árbol de dependencias" + sExtension;

                    IB.Progress.BLL.ArbolDependencias dArbolALL = null;

                    try
                    {
                        //Datatable de cabecera
                        DataTable dtH = new DataTable("HEAD-Excel");
                        DataColumn dtc = new DataColumn("clave");
                        dtH.Columns.Add(dtc);
                        dtc = new DataColumn("valor");
                        dtH.Columns.Add(dtc);
                        DataRow oRow = dtH.NewRow();
                        oRow[0] = "title";
                        oRow[1] = "Árbol de dependencias";
                        dtH.Rows.Add(oRow);

                        //Datatable cuerpo
                        List<IB.Progress.Models.ArbolDependencias> arbolDependencias = null;
                        dArbol = new IB.Progress.BLL.ArbolDependencias();
                        arbolDependencias = dArbol.catalogoArbolDependenciasALL(idficepievaluadorALL);
                        dArbol.Dispose();


                        //eliminar las filas con profesional = "";
                        //arbolDependencias = (from o in arbolDependencias
                        //                     select o).ToList<IB.Progress.Models.ArbolDependencias>();

                        //convertir la lista a datatable
                        DataTable dtbody = arbolDependencias.CopyGenericToDataTable<IB.Progress.Models.ArbolDependencias>();
                        dtbody.TableName = "BODY-Excel";

                        //Eliminar las columnas no necesarias
                        ArrayList arl = new ArrayList();
                        foreach (DataColumn c in dtbody.Columns)
                        {
                            if (c.ColumnName.ToLower().Trim() != "evaluado" && c.ColumnName.ToLower().Trim() != "roldelevaluado" && c.ColumnName.ToLower().Trim() != "evaluador" && c.ColumnName.ToLower().Trim() != "roldelevaluador")
                                arl.Add(c);
                        }
                        foreach (DataColumn dc in arl)
                        {
                            dtbody.Columns.Remove(dc.ColumnName);
                        }

                        //renombar columnas
                        dtbody.Columns["evaluado"].ColumnName = "Evaluado/a";
                        dtbody.Columns["evaluador"].ColumnName = "Evaluador/a";
                        dtbody.Columns["Roldelevaluador"].ColumnName = "Rol del evaluador/a";
                        dtbody.Columns["Roldelevaluado"].ColumnName = "Rol del evaluado/a";
                        

                        //reordenar columnas
                        dtbody.Columns["Evaluador/a"].SetOrdinal(0);
                        dtbody.Columns["Rol del evaluador/a"].SetOrdinal(1);
                        dtbody.Columns["Evaluado/a"].SetOrdinal(2);
                        dtbody.Columns["Rol del evaluado/a"].SetOrdinal(3);


                        //Agregar al dataset
                        ds.Tables.Add(dtH);
                        ds.Tables.Add(dtbody);

                        //exportar
                        oExcel = osvcEXCEL.getExcelFromDataSet(ds, sFilename);

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (dArbolALL != null) dArbolALL.Dispose();
                    }

                    break;

            }


        }
        catch (Exception ex)
        {
            this.hdnError.Value = "Error al exportar a excel.<br /><br />: " + ex.Message;
        }
        finally
        {
            if (osvcEXCEL != null && osvcEXCEL.State != System.ServiceModel.CommunicationState.Closed)
            {
                if (osvcEXCEL.State != System.ServiceModel.CommunicationState.Faulted) osvcEXCEL.Close();
                else osvcEXCEL.Abort();
            }
        }

        //Devolver el excel
        if (Response.IsClientConnected && hdnError.Value == "")
        {
            Response.AddHeader("Content-Disposition", "attachment; filename=\"" + sFilename + "\"");
            Response.BinaryWrite(oExcel);
            Response.ContentType = "application/vnd.ms-excel";
            //Para que funcione en Chrome
            if (System.Web.HttpContext.Current.Request.Browser.Browser.ToString() == "Chrome") Response.AddHeader("Content-Length", "999999999999");
            Response.Flush();
            Response.Close();
            Response.End();
            System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        
    }
}




