using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Text.RegularExpressions;

using SUPER.Capa_Negocio;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHTML = "";
    public string sErrores = "";
    public string sLectura = "false";
    public string sLecturaInsMes = "false";
    public string sModoCoste = "";
    public string sNodo = "";
    public string sMonedaProyecto = "", sMonedaImportes = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            try
            {
                if (Session["IDRED"] == null)
                {
                    try
                    {
                        Response.Redirect("~/SesionCaducadaModal.aspx", true);
                    }
                    catch (System.Threading.ThreadAbortException) { return; }
                }
                //if (!(bool)Session["FORANEOS"])
                //{
                //    this.imgForaneo.Visible = false;
                //    this.lblForaneo.Visible = false;
                //}

                sNodo = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
                if ((bool)Session["MODOLECTURA_PROYECTOSUBNODO"]) sLecturaInsMes = "true";

                cboRecursos.Items.Add(new ListItem("Todos los asignados al proyecto", "0"));
                cboRecursos.Items.Add(new ListItem("Pertenecientes al " + Estructura.getDefCorta(Estructura.sTipoElem.NODO) +" del proyecto", "1"));
                //cboRecursos.Items.Add(new ListItem("Pertenecientes a otros " + Estructura.getDefCorta(Estructura.sTipoElem.NODO) +" de la empresa", "2"));
                //cboRecursos.Items.Add(new ListItem("Pertenecientes a otras empresas grupo", "3"));
                cboRecursos.Items.Add(new ListItem("Externos", "4"));
                

                if (Request.QueryString["sCualidad"].ToString() == "J")
                {
                    cboRecursos.SelectedValue = "1";
                }
                else
                {
                    cboRecursos.SelectedValue = "0";
                }

                if (Request.QueryString["T"].ToString() == "P")
                {
                    this.Title = "Producción por dedicación de profesionales";
                }
                else
                {
                    this.Title = "Ingresos por dedicación de profesionales";
                }

                #region Monedas y denominaciones
                sMonedaProyecto = Session["MONEDA_PROYECTOSUBNODO"].ToString();
                lblMonedaProyecto.InnerText = MONEDA.getDenominacion(Session["MONEDA_PROYECTOSUBNODO"].ToString());

                if (Session["MONEDA_VDP"] == null)
                {
                    sMonedaImportes = sMonedaProyecto;
                    lblMonedaImportes.InnerText = MONEDA.getDenominacionImportes(sMonedaImportes);
                }
                else
                {
                    sMonedaImportes = Session["MONEDA_VDP"].ToString();
                    lblMonedaImportes.InnerText = MONEDA.getDenominacionImportes(Session["MONEDA_VDP"].ToString());
                }
                #endregion

                //if (User.IsInRole("DIS") || ConfigurationManager.AppSettings["MOSTRAR_SOLODIS"] == "0")
                    divMonedaImportes.Style.Add("visibility", "visible");

                string strTabla = getDatosProfesionales(Request.QueryString["nSegMesProy"], sMonedaImportes);
                string[] aTabla = Regex.Split(strTabla, "@#@");
                if (aTabla[0] != "Error") this.strTablaHTML = aTabla[1];
                else sErrores = aTabla[1];
            }
            catch (Exception ex)
            {
                this.sErrores = Errores.mostrarError("Error al obtener los datos", ex);
            }

            //1º Se indican (por este orden) la función a la que se va a devolver el resultado
            //   y la función que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

            //2º Se "registra" la función que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("getDatosProf"):
                sResultado += getDatosProfesionales(aArgs[1], aArgs[2]);
                break;
        }

        //3º Damos contenido a la variable que se envía de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }

    public string getDatosProfesionales(string sSegMesProy, string sMonedaImportes2)
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            sLectura = "true";
            SqlDataReader dr = CONSPERMES.CatalogoProdIngProfesionales(int.Parse(sSegMesProy), sMonedaImportes2);

            sb.Append("<table class=texto id=tblDatos style='width: 960px;' mantenimiento=1>");
            sb.Append("<colgroup>");
            sb.Append("<col style='width:10px;' />");
            sb.Append("<col style='width:20px;' />");
            sb.Append("<col style='width:60px;' />");
            sb.Append("<col style='width:510px;' />");
            sb.Append("<col style='width:120px;' />");
            sb.Append("<col style='width:120px;' />");
            sb.Append("<col style='width:120px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t314_idusuario"].ToString() + "' caso='" + dr["caso"].ToString() + "'");
                sb.Append(" tipo='" + dr["TipoRecurso"].ToString() + "' bd='' ");
                sb.Append("style='height:20px;'>");

                sb.Append("<td><img src='../../../images/imgFN.gif'></td>");
                sb.Append("<td>");
                //switch (dr["caso"].ToString())
                //{
                //    case "1":
                //        sb.Append("<img border='0' src='../../../Images/imgUsuP" + dr["sexo"].ToString() + ".gif' width='16px' height='16px' />");
                //        break;
                //    case "4":
                //        sb.Append("<img border='0' src='../../../Images/imgUsuE" + dr["sexo"].ToString() + ".gif' width='16px' height='16px' />");
                //        break;
                //}
                switch (dr["TipoRecurso"].ToString())
                {
                    case "I":
                        sb.Append("<img border='0' src='../../../Images/imgUsuP" + dr["sexo"].ToString() + ".gif' width='16px' height='16px' />");
                        break;
                    case "F":
                        sb.Append("<img border='0' src='../../../Images/imgUsuF" + dr["sexo"].ToString() + ".gif' width='16px' height='16px' />");
                        break;
                    default:
                        sb.Append("<img border='0' src='../../../Images/imgUsuE" + dr["sexo"].ToString() + ".gif' width='16px' height='16px' />");
                        break;
                }
                sb.Append("</td>");
                sb.Append("<td style='text-align:right; padding-right:10px;'>" + dr["t314_idusuario"].ToString() + "</td>");
                sb.Append("<td>" + dr["profesional"].ToString() + "</td>");

                sb.Append("<td style='text-align:right;'>" + decimal.Parse(dr["t378_costeunitariocon"].ToString()).ToString("N") + "</td>");
                sb.Append("<td style='text-align:right;'>" + double.Parse(dr["t378_unidades"].ToString()).ToString("N") + "</td>");
                sb.Append("<td style='text-align:right;padding-right:2px;'>" + double.Parse(dr["importe"].ToString()).ToString("N") + "</td>");
                sb.Append("</tr>");

            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            SEGMESPROYECTOSUBNODO oSegMes = SEGMESPROYECTOSUBNODO.Obtener(null, int.Parse(sSegMesProy), sMonedaImportes2);
            sModoCoste = oSegMes.t301_modelocoste;

            return "OK@#@" + sb.ToString() + "@#@" + sLectura + "@#@" + MONEDA.getDenominacionImportes(sMonedaImportes2);
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los datos de producción/ingresos", ex);
        }
    }
    private string getMesesProy(string sIDProySubnodo)
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = SEGMESPROYECTOSUBNODO.SelectByT305_idproyectosubnodo(null, int.Parse(sIDProySubnodo));

            while (dr.Read())
            {
                sb.Append(dr["t325_idsegmesproy"].ToString() + "##");
                sb.Append(dr["t325_anomes"].ToString() + "##");
                sb.Append(dr["t325_estado"].ToString() + "///");
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los meses del proyectosubnodo", ex);
        }
    }
    private string addMesesProy(string nIdProySubNodo, string sDesde, string sHasta)
    {
        return SEGMESPROYECTOSUBNODO.InsertarSegMesProy(nIdProySubNodo, sDesde, sHasta);
    }

}
