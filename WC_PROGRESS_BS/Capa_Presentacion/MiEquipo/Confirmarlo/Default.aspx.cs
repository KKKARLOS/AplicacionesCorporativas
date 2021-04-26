using IB.Progress.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Capa_Presentacion_MiEquipo_Confirmarlo_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        IB.Progress.BLL.MIEQUIPO miequipoBLL = null;
        IB.Progress.Models.MIEQUIPO miequipo = null;
        bool bCont = true;
        try {
            miequipoBLL = new IB.Progress.BLL.MIEQUIPO();
            miequipo = miequipoBLL.CatalogoConfirmarMiequipo(((IB.Progress.Models.Profesional)Session["PROFESIONAL"]).t001_idficepi);
            miequipoBLL.Dispose();

            if (miequipo.confirmequipo != null)
                dateConfirm.Value = miequipo.confirmequipo.Value.ToShortDateString();

            if (!miequipo.entradasentramite)
                divEntradas.Attributes.Add("class", "hide");
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
            Smtp.SendSMTP("Error en la aplicación PROGRESS", ibex.ToString());
        }
        catch (Exception ex)
        {
            if (miequipoBLL != null) miequipoBLL.Dispose();
            bCont = false;
            PieMenu.sErrores = "msgerr = 'Ocurrió un error general en la aplicación.';";
            //Avisar a EDA por smtp
            Smtp.SendSMTP("Error en la aplicación PROGRESS", ex.ToString());
        }
        if (bCont)
        {
            try
            {
                foreach (IB.Progress.Models.MIEQUIPO.profesional prof in miequipo.profesionales)
                {
                    HtmlTableCell htblcel1 = new HtmlTableCell();
                    if (prof.estado != null)
                    {
                        HtmlGenericControl aux = new HtmlGenericControl();
                        if (prof.estado == 1)
                        {
                            aux.TagName = "span";
                            aux.Attributes.Add("class", "glyphicon glyphicon-new-window");
                        }
                        else if (prof.estado == 3)
                        {
                            aux.TagName = "i";
                            aux.Attributes.Add("class", "fa fa-compress");
                        }

                        else if (prof.estado == 6)
                        {
                            aux.TagName = "i";
                            aux.Attributes.Add("class", "glyphicon glyphicon-user");
                        }
                        htblcel1.Controls.Add(aux);
                    }

                    HtmlGenericControl nombre = new HtmlGenericControl("span");
                    nombre.InnerText = prof.prof;
                    htblcel1.Controls.Add(nombre);
                    HtmlTableCell htblcel2 = new HtmlTableCell();
                    htblcel2.InnerText = prof.rol;
                    HtmlTableRow htblrow = new HtmlTableRow();
                    htblrow.Cells.Add(htblcel1);
                    htblrow.Cells.Add(htblcel2);
                    idtbody.Controls.Add(htblrow);
                }
            }
            catch (Exception ex)
            {
                //Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script1", "msgerr = 'Ocurrió un error obteniendo los roles de base de datos';", true);
                PieMenu.sErrores = "msgerr = 'Ocurrió un error cargando los datos de mi equipo';";
                Smtp.SendSMTP("Error en la aplicación PROGRESS", ex.ToString());
            }
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string confirmarEquipo()
    {
        try
        {
            IB.Progress.BLL.MIEQUIPO miequipo = new IB.Progress.BLL.MIEQUIPO();
            miequipo.Update(((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).t001_idficepi);
            miequipo.Dispose();
            return DateTime.Now.ToShortDateString();            
        }
        catch (Exception ex)
        {
            Smtp.SendSMTP("Error al confirmar el equipo", ex.ToString());
            throw ex;
        }
    }
}