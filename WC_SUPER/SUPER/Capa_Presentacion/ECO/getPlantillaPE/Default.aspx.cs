using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;
using System.Text;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string gsDesglose="N", gsTipo = "E", strTablaHTMLTarea = "", strTablaHTMLPlantilla = "", strTablaHTMLHitos = "";
    public string sErrores="";
    public SqlConnection oConn;
    public SqlTransaction tr;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            //Para que se muestre la botonera, únicamente hay que indicar el número de botonera
            //y crear el manejador de eventos para la misma.
            if (!Page.IsPostBack)
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

                    string sAmbito = Request.QueryString["sAmb"];
                    if (sAmbito == null)
                        sAmbito = "";
                    string sAux = Request.QueryString["sTipo"];
                    if (sAux != null)
                        gsTipo = sAux;
                    sAux = Request.QueryString["sDesg"];
                    if (sAux != null)
                        gsDesglose = sAux;
                    getPlantillasPE(gsTipo, gsDesglose, sAmbito);
                }
                catch (Exception ex)
                {
                    sErrores += Errores.mostrarError("Error al obtener el catálogo de plantillas empresariales", ex);
                }
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
            case ("tareasPlant"):
                sResultado += ObtenerEstructura(int.Parse(aArgs[1]));
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
    private void getPlantillasPE(string sTipo, string sDesglose, string sAmbito)
    {
        StringBuilder sb = new StringBuilder();
        int idNodo = -1, idUser=-1;
        if (sAmbito != "E")
        {
            if (Request.QueryString["idNodo"] != null)
                idNodo = int.Parse(Request.QueryString["idNodo"].ToString());
            idUser = int.Parse(Session["IDFICEPI_PC_ACTUAL"].ToString());
        }
        //SqlDataReader dr = PlantProy.CatalogoPlantillasPE(int.Parse(Request.QueryString["idNodo"].ToString()),
        //                                                  (int)Session["UsuarioActual"], sTipo);
        SqlDataReader dr = PlantProy.CatalogoPlantillasPE(idNodo, idUser, sTipo);

        sb.Append("<table id='tblDatos' class='texto MA' style='width:420px'>");
        sb.Append("<colgroup><col style='width:30px;' /><col style='width:390px;' /></colgroup>");
        sb.Append("</tbody>");
        while (dr.Read())
        {
            sb.Append("<tr id='" + dr["t338_idplantilla"].ToString() +"' style='height:20px;'");
            if (sDesglose=="T")
                sb.Append(" ondblclick='cargarPlantilla();' ");
            else
                sb.Append(" ondblclick='aceptarClick(this.rowIndex);' ");
            sb.Append(" onclick='ms(this);mostrarPlantilla(this.id);'>");
            sb.Append("<td style='padding-left:3px'>");
            switch (dr["t338_ambito"].ToString())
            {
                case "D": sb.Append("<img src='../../../images/imgIconoDepartamental.gif' style='width:16px;height:16px;border:0px' />"); break;
                case "P": sb.Append("<img src='../../../images/imgIconoPersonal.gif' style='width:16px;height:16px;border:0px' />"); break;
                case "E": sb.Append("<img src='../../../images/imgIconoEmpresarial.gif' style='width:16px;height:16px;border:0px' />"); break;
            }
            sb.Append("</td><td>"+ dr["t338_denominacion"].ToString() + "</td></tr>");
        }
        dr.Close(); 
        dr.Dispose();
        sb.Append("</tbody>");
        sb.Append("</table>");

        this.strTablaHTMLPlantilla = sb.ToString();
    }

    private string ObtenerEstructura_Old(int iPlant)
    {/* Devuelve una lista del catalogo de tareas de la plantilla que se pasa por parámetro
      * mas una lista de los hitos de cumplimiento discontinuo
  * En sTipoPlant nos indica si es un Proyecto Tecnico o un Proyecto Economico
  */
        StringBuilder sb = new StringBuilder();
        StringBuilder slHitos = new StringBuilder();
        string sPlant;
        //string sDesTipo = "", sDesc, sTipo, sTarea, sOrden, sMargen, sPlant,sFact;
        try
        {
            sPlant = iPlant.ToString();
            SqlDataReader dr = PlantTarea.Catalogo(iPlant);
            while (dr.Read())
            {
                //sb.Append(sPlant + "##" + sTarea + "##" + sDesTipo + "##" + sOrden + "##" + sMargen + "##" + sDesc + "///");
                sb.Append(sPlant);
                sb.Append("##");
                sb.Append(dr["t339_iditems"].ToString());
                sb.Append("##");
                sb.Append(dr["Tipo"].ToString());
                sb.Append("##");
                sb.Append(dr["orden"].ToString());
                sb.Append("##");
                sb.Append(dr["margen"].ToString());
                sb.Append("##");
                sb.Append(dr["Nombre"].ToString());
                sb.Append("##");
                sb.Append(((bool)dr["t339_facturable"])? "T":"F");
                sb.Append("///");

            }
            dr.Close(); dr.Dispose();
            SqlDataReader drH = PlantTarea.CatalogoHitos(iPlant);
            while (drH.Read())
            {
                //sb.Append(sPlant + "##" + sHito + "##" + sDesTipo + "##" + sOrden + "##" + sMargen + "##" + sDesc + "///");
                slHitos.Append(sPlant);
                slHitos.Append("##");
                slHitos.Append(drH["t369_idHito"].ToString());
                slHitos.Append("##HC##");
                slHitos.Append(drH["t369_orden"].ToString());
                slHitos.Append("##0##");
                slHitos.Append(drH["t369_deshito"].ToString());
                slHitos.Append("///");

            }
            drH.Close(); drH.Dispose();
            return "OK@#@" + sb.ToString() + "@#@" + slHitos;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los elementos de la plantilla", ex);
        }
    }
    private string ObtenerEstructura(int iPlant)
    {/* Devuelve una lista del catalogo de tareas de la plantilla que se pasa por parámetro
      * mas una lista de los hitos de cumplimiento discontinuo
  */
        StringBuilder strBuilder = new StringBuilder();
        StringBuilder slHitos = new StringBuilder();
        string sDesc, sTipo, sTarea, sOrden, sMargen, sResul = "", sPlant, sFact;//sDesTipo = ""
        bool bFacturable;
        try
        {
            sPlant = iPlant.ToString();
            SqlDataReader dr = PlantTarea.Catalogo(iPlant);
            while (dr.Read())
            {
                sTipo = dr["Tipo"].ToString();
                //switch (sTipo)
                //{
                //    case "P": sDesTipo = "P.T."; break;
                //    case "F": sDesTipo = "FASE"; break;
                //    case "A": sDesTipo = "ACTI."; break;
                //    case "T": sDesTipo = "TAREA"; break;
                //    case "H": sDesTipo = "HITO"; break;
                //}
                sDesc = dr["Nombre"].ToString();
                sTarea = dr["t339_iditems"].ToString();
                sOrden = dr["orden"].ToString();
                sMargen = dr["margen"].ToString();
                bFacturable = bool.Parse(dr["t339_facturable"].ToString());
                if (bFacturable) sFact = "T";
                else sFact = "F";

                //strBuilder.Append(sPlant + "##" + sTarea + "##" + sDesTipo + "##" + sOrden + "##" + sMargen + "##" + sDesc + "///");
                strBuilder.Append(sPlant);
                strBuilder.Append("##");
                strBuilder.Append(sTarea);
                strBuilder.Append("##");
                //strBuilder.Append(sDesTipo);
                strBuilder.Append(sTipo);
                strBuilder.Append("##");
                strBuilder.Append(sOrden);
                strBuilder.Append("##");
                strBuilder.Append(sMargen);
                strBuilder.Append("##");
                strBuilder.Append(sDesc);
                strBuilder.Append("##");
                strBuilder.Append(sFact);
                strBuilder.Append("///");

            }
            dr.Close(); dr.Dispose();
            SqlDataReader drH = PlantTarea.CatalogoHitos(iPlant);
            while (drH.Read())
            {
                sDesc = drH["t369_deshito"].ToString();
                sTarea = drH["t369_idHito"].ToString();
                sOrden = drH["t369_orden"].ToString();

                //strBuilder.Append(sPlant + "##" + sHito + "##" + sDesTipo + "##" + sOrden + "##" + sMargen + "##" + sDesc + "///");
                slHitos.Append(sPlant);
                slHitos.Append("##");
                slHitos.Append(sTarea);
                slHitos.Append("##HC##");
                slHitos.Append(sOrden);
                slHitos.Append("##0##");
                slHitos.Append(sDesc);
                slHitos.Append("///");

            }
            drH.Close(); drH.Dispose();
            sResul = "OK@#@" + strBuilder.ToString() + "@#@" + slHitos;
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener los elementos de la plantilla", ex);
        }
        return sResul;
    }

}
