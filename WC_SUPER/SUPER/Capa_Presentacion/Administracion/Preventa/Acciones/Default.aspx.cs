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

using System.Text;
using System.Text.RegularExpressions;
using EO.Web;
using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;
//using IB.SUPER.SIC.BLL;
using IB.SUPER.ADM.SIC.BLL;

public partial class Capa_Presentacion_Administracion_Preventa_Acciones_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHTML, sErrores;
    public SqlConnection oConn;
    public SqlTransaction tr;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            //Para que se muestre la botonera, únicamente hay que indicar el número de botonera
            //y crear el manejador de eventos para la misma.
            Master.nBotonera = 34;// 49;
            Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);

            Master.TituloPagina = "Acciones de preventa";
            Master.bFuncionesLocales = true;
            if (!Page.IsPostBack)
            {
                Master.FuncionesJavaScript.Add("Javascript/draganddrop.js");
                IB.SUPER.ADM.SIC.BLL.TipoAccionPreventa oAccion = new IB.SUPER.ADM.SIC.BLL.TipoAccionPreventa();
                try
                {              
                          
                    List<IB.SUPER.ADM.SIC.Models.TipoAccionPreventa> oLista = oAccion.Catalogo();
                    strTablaHTML = getHtmlMantenimiento(oLista);
                }
                catch (Exception ex)
                {
                    Master.sErrores = Errores.mostrarError("Error al obtener los datos", ex);
                }
                finally
                {
                    oAccion.Dispose();
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

    protected void Botonera_Click(object sender, EO.Web.ToolBarEventArgs e)
    {
        switch (e.Item.CommandName.ToLower())
        {
            case "regresar":
                try
                {
                    Response.Redirect(HistorialNavegacion.Leer(), true);
                }
                catch (System.Threading.ThreadAbortException) { }
                break;
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
            case ("grabar"):
                sResultado += Grabar(aArgs[1]);
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

    protected string Grabar(string strFunciones)
    {
        string sResul = "OK@#@";
        IB.SUPER.ADM.SIC.BLL.TipoAccionPreventa oAccion = null;
        try
        {
            oAccion = new IB.SUPER.ADM.SIC.BLL.TipoAccionPreventa();
            sResul+= getHtmlMantenimiento(oAccion.Grabar(strFunciones)) + "@#@";


        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al actualizar los tipos de acciones de preventa.", ex, false);
        }
        finally
        {
            oAccion.Dispose();
        }
        return sResul;
    }
    protected string getHtmlMantenimiento(List<IB.SUPER.ADM.SIC.Models.TipoAccionPreventa> oLista)
    {
        StringBuilder sb = new StringBuilder();        

        try
        {
            sb.Append("<table id='tblDatos' class='texto' style='width: 875px;' mantenimiento='1' border='0'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:20px;' /><col style='width:365px;' /><col style='width:60px;' /><col style='width:70px;' /><col style='width:70px;' /><col style='width:110px;' /><col style='width:60px;' /><col style='width:80px;' /></colgroup>");
            sb.Append("<tbody id='tBodyAcciones'>");
            foreach (IB.SUPER.ADM.SIC.Models.TipoAccionPreventa oElem in oLista)
            {
                              

                sb.Append("<tr id='" + oElem.ta205_idtipoaccionpreventa + "' pmr='" + oElem.ta205_plazominreq.ToString() + "'");
                sb.Append(" bd='' onclick='mm(event)' style='height:20px;'>");
                sb.Append("<td></td>");//Imagen para el tipo de acción de BBDD
                sb.Append("<td><img src='../../../../images/imgMoveRow.gif' title='Pinchar y arrastrar para ordenar' ondragstart='return false;' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;cursor:row-resize'></td>");//Imagen para la ordenaciòn
                //sb.Append("<td style='padding-left:5px;'><input type='text' id='txtDesc' class='txtL' style='width:480px' value='" + oElem.ta205_denominacion + "' maxlength='50' onKeyUp='fm(event)'></td>");
                sb.Append("<td style='padding-left:3px;'><input type='text' id='txtDesc' class='txtL' style='width:360px' value='" + oElem.ta205_denominacion + "' maxlength='50' onKeyUp='fm(event)'></td>");
               
                sb.Append("<td><input type='checkbox' style='width:13px;height:13px;' name='chkON' id='chkON' class='check' onclick='fm(event)' ");
                if (oElem.ta205_origen_on) sb.Append("checked=true");
                sb.Append("></td>");

                sb.Append("<td><input type='checkbox' style='width:13px;height:13px;' name='chkPartida' id='chkPartida' class='check' onclick='fm(event)' ");
                if (oElem.ta205_origen_partida) sb.Append("checked=true");
                sb.Append("></td>");

                sb.Append("<td><input type='checkbox' style='width:13px;height:13px;' name='chkSP' id='chkSP' class='check' onclick='fm(event)' ");
                if (oElem.ta205_origen_super) sb.Append("checked=true");
                sb.Append("></td>");

                sb.Append("<td style='padding-left:30px;'><input type='checkbox' style='width:13px;height:13px;' name='chkUA' id='chkUA' class='check' onclick='fm(event)' ");
                if (oElem.ta205_unicaxaccion) sb.Append("checked=true");
                sb.Append("></td>");

                sb.Append("<td style='padding-left:10px;'><input type='checkbox' style='width:13px;height:13px;' name='chkActiva' id='chkActiva' class='check' onclick='fm(event)' ");
                if (oElem.ta205_estadoactiva) sb.Append("checked=true");
                sb.Append("></td>");

                sb.Append("<td></td>");

                sb.Append("</tr>");
            }
            sb.Append("</tbody>");
            sb.Append("</table>");
        }
        catch (Exception e) { throw new Exception(e.Message); }
       
        return sb.ToString();
    }


}