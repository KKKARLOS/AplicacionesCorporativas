using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IB.Progress.Models;
using System.Web.UI.HtmlControls;
using Newtonsoft.Json;
using System.Web.Services;
using System.Web.Script.Services;

public partial class Capa_Presentacion_MisEvaluaciones_Default : System.Web.UI.Page
{
    public string filtros = String.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        IB.Progress.BLL.VALORACIONESPROGRESS valoracionesBLL = null;
        List<VALORACIONESPROGRESS.miEval> misvaloraciones = null;
        bool bCont = true;
        try
        {
            if (Request.QueryString["pt"] == "Formularios")
            {
                filtros = "var filtros =" + JsonConvert.SerializeObject(Session["filtros"]).ToString() + ";";                
            }
            else
            {
                filtros = "var filtros =''";
                Session["filtrosDeMiEquipo"] = null;
            }


            //Mostramos el rol del profesional
            if (((IB.Progress.Models.Profesional)Session["PROFESIONAL"]).T004_desrol != null) {
                divRol.Style.Add("display", "block");
                spanRol.InnerText = ((IB.Progress.Models.Profesional)Session["PROFESIONAL"]).T004_desrol;
            }
                

            valoracionesBLL = new IB.Progress.BLL.VALORACIONESPROGRESS();
            misvaloraciones = valoracionesBLL.CatMisEvaluaciones(((IB.Progress.Models.Profesional)Session["PROFESIONAL"]).t001_idficepi);
            valoracionesBLL.Dispose();

            if (Request.QueryString["FEVADO"] == "1")
            {
                if (misvaloraciones.Count >= 1)
                {

                    int nencurso = 0;
                    int idvaloracion = 0;
                    int idmodeloformulario = 0;

                    for (int i = 0; i < misvaloraciones.Count; i++)
                    {
                        if (misvaloraciones[i].estado == "CUR")
                        {
                            nencurso++;
                            idvaloracion = misvaloraciones[i].idvaloracion;
                            idmodeloformulario = misvaloraciones[i].idformulario;
                        }
                    }

                    if (nencurso == 1)
                    {
                        switch (idmodeloformulario)
                        {
                            case 1:
                                Response.Redirect(Session["strserver"]+ "Capa_Presentacion/Evaluacion/Formularios/Modelo1/Default.aspx?idval=" + IB.Progress.Shared.Utils.EncodeTo64(idvaloracion.ToString()) + "&acceso=misevaluaciones", false);
                                Context.ApplicationInstance.CompleteRequest();                                
                                break;
                            case 2:
                                Response.Redirect(Session["strserver"] + "Capa_Presentacion/Evaluacion/Formularios/Modelo2/Default.aspx?idval=" + IB.Progress.Shared.Utils.EncodeTo64(idvaloracion.ToString()) + "&acceso=misevaluaciones", false);
                                Context.ApplicationInstance.CompleteRequest();                                
                                return;                                
                        }
                    }
                                    
                }

            }
        }
        catch (IB.Progress.Shared.IBException ibex)
        {
            if (valoracionesBLL != null) valoracionesBLL.Dispose();
            bCont = false;

            string msgerr = "";
            switch (ibex.ErrorCode)
            {
                case 108:
                    msgerr = ibex.Message;
                    break;
            }
            PieMenu.sErrores = "msgerr = '" + msgerr + "';";
            //Avisar a EDA por smtp
            //SMTP.send(asunto, ibex, .......);
        }
        catch (Exception ex)
        {
            if (valoracionesBLL != null) valoracionesBLL.Dispose();
            bCont = false;
            PieMenu.sErrores = "msgerr = 'Ocurrió un error general en la aplicación.';";
            //Avisar a EDA por smtp
            //SMTP.send(asunto, ex, .......);
        }

        if (bCont)
        {
            try
            {
                
                if (misvaloraciones.Count > 0)
                {
                    foreach (VALORACIONESPROGRESS.miEval miEval in misvaloraciones)
                    {
                        //Tabla de resumen
                        HtmlTableCell htblcel0 = new HtmlTableCell();
                        htblcel0.InnerHtml = "<i class='glyphicon glyphicon-search'</i>";

                        HtmlTableCell htblcel1 = new HtmlTableCell();
                        htblcel1.InnerText = miEval.evaluador;
                        //htblcel1.Width = "55%";
                        HtmlTableCell htblcel2 = new HtmlTableCell();
                        //htblcel2.InnerText = VALORACIONESPROGRESS.getEstado(miEval.estado);
                        htblcel2.InnerText = IB.Progress.Shared.Utils.getEstado(miEval.estado);
                        //htblcel2.Width = "15%";
                        HtmlTableCell htblcel3 = new HtmlTableCell();
                        htblcel3.InnerText = miEval.fecapertura.ToShortDateString();
                        //htblcel3.Width = "15%";
                        HtmlTableCell htblcel4 = new HtmlTableCell();
                        htblcel4.InnerText = (miEval.feccierre != null) ? ((DateTime)miEval.feccierre).ToShortDateString() : "";
                        //htblcel4.Width = "15%";
                        HtmlTableRow htblrow = new HtmlTableRow();
                        htblrow.Attributes.Add("idvaloracion", miEval.idvaloracion.ToString());
                        htblrow.Attributes.Add("idformulario", miEval.idformulario.ToString());
                        htblrow.Attributes.Add("estado", miEval.estado);
                        //Las pongo no visibles para habilitar si procede desde cliente
                        htblrow.Attributes.Add("class", "hide");
                        htblrow.Cells.Add(htblcel0);
                        htblrow.Cells.Add(htblcel1);
                        htblrow.Cells.Add(htblcel2);
                        htblrow.Cells.Add(htblcel3);
                        htblrow.Cells.Add(htblcel4);
                        tbdEval.Controls.Add(htblrow);

                    }
                }
                else
                {
                    HtmlTableCell htblcel1 = new HtmlTableCell();
                    htblcel1.InnerHtml = "<b>No tienes ninguna evaluación</b>";
                    HtmlTableRow htblrow = new HtmlTableRow();
                    htblrow.Cells.Add(htblcel1);
                    tbdEval.Controls.Add(htblrow);
                    cboEstado.Items.Insert(0, new ListItem(""));
                    cboEstado.Attributes.Add("disabled", "");
                    btnAcceder.Attributes.Add("disabled", "");                    
                }

            }
            catch (Exception)
            {
                //Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script1", "msgerr = 'Ocurrió un error obteniendo los roles de base de datos';", true);
                PieMenu.sErrores = "msgerr = 'Ocurrió un error cargando los datos de mis evaluaciones.';";
            }
        }
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void GuardarFiltros(string estado)
    {

        try
        {

            HttpContext.Current.Session["filtros"] = estado;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}