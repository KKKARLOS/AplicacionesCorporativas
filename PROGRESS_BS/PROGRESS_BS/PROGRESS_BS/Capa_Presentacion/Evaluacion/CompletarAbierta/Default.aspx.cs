using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Capa_Presentacion_Evaluacion_CompletarAbierta_Default : System.Web.UI.Page
{

    public string nombre;
    protected void Page_Load(object sender, EventArgs e)
    {
        IB.Progress.BLL.MIEQUIPO miequipoBLL = null;
        List<IB.Progress.Models.MIEQUIPO.profPendEval> profpendeval = null;
        bool bCont = true;
        try
        {
            nombre = "var nombre ='" + ((IB.Progress.Models.Profesional)Session["PROFESIONAL"]).T001_nombre.ToString() + "';";
            miequipoBLL = new IB.Progress.BLL.MIEQUIPO();
            profpendeval = miequipoBLL.CatEvalPend(((IB.Progress.Models.Profesional)Session["PROFESIONAL"]).t001_idficepi);
            miequipoBLL.Dispose();            
        }
        catch (IB.Progress.Shared.IBException ibex)
        {
            if (miequipoBLL != null) miequipoBLL.Dispose();
            bCont = false;

            string msgerr = "";
            switch (ibex.ErrorCode)
            {
                case 100:
                case 101:
                    msgerr = ibex.Message;
                    break;
            }
            PieMenu.sErrores = "msgerr = '" + msgerr + "';";
            //Avisar a EDA por smtp
            //SMTP.send(asunto, ibex, .......);
        }
        catch (Exception ex)
        {
            if (miequipoBLL != null) miequipoBLL.Dispose();
            bCont = false;
            PieMenu.sErrores = "msgerr = 'Ocurrió un error general en la aplicación.';";
            //Avisar a EDA por smtp
            //SMTP.send(asunto, ex, .......);
        }

        if (bCont)
        {
            try
            {
                foreach (IB.Progress.Models.MIEQUIPO.profPendEval prof in profpendeval)
                {
                    //Tabla de resumen
                    HtmlTableCell htblcel0 = new HtmlTableCell();
                    htblcel0.InnerHtml = "<i class='glyphicon glyphicon-search'</i>";
                    HtmlTableCell htblcel1 = new HtmlTableCell();
                    htblcel1.InnerText = prof.prof;
                    HtmlTableCell htblcel2 = new HtmlTableCell();
                    htblcel2.InnerText = prof.fecapertura.ToShortDateString();
                    HtmlTableRow htblrow = new HtmlTableRow();
                    htblrow.Attributes.Add("idvaloracion", prof.idvaloracion.ToString());
                    htblrow.Attributes.Add("idformulario", prof.idformulario.ToString());
                    htblrow.Attributes.Add("correoprofesional", prof.correoprofesional.ToString());
                    htblrow.Attributes.Add("nombreprofesional", prof.nombreprofesional.ToString());
                    htblrow.Attributes.Add("fechaapertura", prof.fecapertura.ToShortDateString());
                    htblrow.Cells.Add(htblcel0);
                    htblrow.Cells.Add(htblcel1);
                    htblrow.Cells.Add(htblcel2);
                    tbdProf.Controls.Add(htblrow);
                }
            }
            catch (Exception)
            {
                //Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script1", "msgerr = 'Ocurrió un error obteniendo los roles de base de datos';", true);
                PieMenu.sErrores = "msgerr = 'Ocurrió un error cargando los datos de las evaluaciones pendientes.';";
            }
        }
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void Delete(int t930_idvaloracion, bool checkbox, string correoprofesional, string nombreevaluado, string fechaapertura)
    {
        try
        {

            IB.Progress.BLL.VALORACIONESPROGRESS cValoraciones = new IB.Progress.BLL.VALORACIONESPROGRESS();
            cValoraciones.Delete(t930_idvaloracion);
            cValoraciones.Dispose();

            if (checkbox && correoprofesional != "") {
                Correo.Enviar("Correo Progress", nombreevaluado + ", " +  ((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).nombrelargo.ToString() + " ha eliminado la evaluación que te abrió el " + fechaapertura, correoprofesional);
            }
                
        }
        catch (Exception ex)
        {
            IB.Progress.Shared.Smtp.SendSMTP("Error al borrar la evaluación", ex.Message);
        }
    }

}