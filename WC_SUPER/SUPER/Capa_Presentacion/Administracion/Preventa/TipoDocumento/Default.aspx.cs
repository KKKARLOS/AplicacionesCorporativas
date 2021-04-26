using System;
using System.Web.UI;
using System.Text;
using System.Text.RegularExpressions;
using EO.Web;
using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;

public partial class Capa_Presentacion_Administracion_Preventa_TipoDocumento_Default : System.Web.UI.Page, ICallbackEventHandler
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

            Master.TituloPagina = "Tipos de documento de preventa";
            Master.bFuncionesLocales = true;
            if (!Page.IsPostBack)
            {
                Master.FuncionesJavaScript.Add("Javascript/draganddrop.js");
                IB.SUPER.ADM.SIC.BLL.TipoDocumento oTipoDocumento = new IB.SUPER.ADM.SIC.BLL.TipoDocumento();
                try
                {

                    List<IB.SUPER.ADM.SIC.Models.TipoDocumento> oLista = oTipoDocumento.Catalogo();
                    strTablaHTML = getHtmlMantenimiento(oLista);
                }
                catch (Exception ex)
                {
                    Master.sErrores = Errores.mostrarError("Error al obtener los datos", ex);
                }
                finally
                {
                    oTipoDocumento.Dispose();
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
        IB.SUPER.ADM.SIC.BLL.TipoDocumento oTipoDocumento = null;
        try
        {
            oTipoDocumento = new IB.SUPER.ADM.SIC.BLL.TipoDocumento();
            sResul += getHtmlMantenimiento(oTipoDocumento.Grabar(strFunciones)) + "@#@";


        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al actualizar los tipos de documento de preventa.", ex, false);
        }
        finally
        {
            oTipoDocumento.Dispose();
        }
        return sResul;
    }
    protected string getHtmlMantenimiento(List<IB.SUPER.ADM.SIC.Models.TipoDocumento> oLista)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            sb.Append("<table id='tblDatos' class='texto' style='width: 510px;' mantenimiento='1'>");
            sb.Append("<colgroup><col style='width:15px;' /><col style='width:15px;' /><col style='width:405px;' /><col style='width:65px;' /></colgroup>");
            sb.Append("<tbody id='tBodyTipoDocumento'>");
            foreach (IB.SUPER.ADM.SIC.Models.TipoDocumento oElem in oLista)
            {
                sb.Append("<tr id='" + oElem.ta211_idtipodocumento + "'");
                sb.Append(" bd='' onclick='mm(event)' style='height:20px;'>");
                sb.Append("<td></td>");//Imagen para el tipo de acción de BBDD
                sb.Append("<td><img src='../../../../images/imgMoveRow.gif' title='Pinchar y arrastrar para ordenar' ondragstart='return false;' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;cursor:row-resize'></td>");//Imagen para la ordenaciòn
               sb.Append("<td><input type='text' id='txtDesc' class='txtL' style='width:400px' value='" + oElem.ta211_denominacion + "' maxlength='50' onKeyUp='fm(event)'></td>");

                sb.Append("<td><input type='checkbox' style='width:13px;height:13px;' name='chkActiva' id='chkActiva' class='check' onclick='fm(event)' ");
                if (oElem.ta211_estadoactiva) sb.Append("checked=true");
                sb.Append("></td>");

                sb.Append("</tr>");
            }
            sb.Append("</tbody>");
            sb.Append("</table>");
        }
        catch (Exception e) { throw new Exception(e.Message); }

        return sb.ToString();
    }


}