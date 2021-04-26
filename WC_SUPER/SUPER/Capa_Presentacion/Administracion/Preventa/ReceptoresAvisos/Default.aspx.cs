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

using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;
//using System.Xml;
using System.IO;

public partial class Capa_Presentacion_Administracion_Preventa_ReceptoresAvisos_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHtml, sErrores;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.nBotonera = 9;
            Master.TituloPagina = "Mantenimiento de receptores de avisos de preventa";
            Master.bFuncionesLocales = true;
            if (!Page.IsPostBack)
            {
                try
                {
                    Catalogo();
                }
                catch (Exception ex)
                {
                    Master.sErrores = Errores.mostrarError("Error al obtener los datos", ex);
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
            case ("grabar"):
                sResultado += Grabar(aArgs[1]);
                break;
            case ("catalogo"):
                sResultado += Catalogo();
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
    private string Catalogo()
    {
        StringBuilder sb = new StringBuilder();
        string sRes = "";

        sb.Append("<table id='tblDatos' class='texto' style='width:500px; text-align:left;' mantenimiento='1' border='0'>");
        sb.Append("<colgroup><col style='width:20px;' /><col style='width:430px;'><col style='width:50px;'></colgroup>");
        sb.Append("<tbody>");

        IB.SUPER.SIC.BLL.ReceptoresAvisos oP = new IB.SUPER.SIC.BLL.ReceptoresAvisos();
        try
        {
            List<IB.SUPER.SIC.Models.ReceptoresAvisos> oLista = oP.Catalogo();
            foreach (IB.SUPER.SIC.Models.ReceptoresAvisos oElem in oLista)
            {
                sb.Append("<tr bd='' id='" + oElem.t001_idficepi.ToString() + "' style='height:20px'>");
                sb.Append("<td><img src='../../../../../images/imgFN.gif' /></td>");
                sb.Append("<td onmouseover='TTip(event)' style='width:430px;'><span class='NBR W420'>" + oElem.denProfesional + "</span></td>");
                sb.Append("<td><input type='checkbox' class='check MANO' style='width:15px;margin-left:15px;' onclick='setFila(this.parentElement.parentElement)' ");
                if(oElem.t399_avisopreventa)
                    sb.Append(" checked='true' /></td>");
                else
                    sb.Append(" /></td>");
                sb.Append("</tr>");
            }
            sb.Append("</tbody>");
            sb.Append("</table>");
            strTablaHtml = sb.ToString();

            sRes = "OK@#@" + strTablaHtml;
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al cargar la lista de receptores de avisos de preventa", ex);
            sRes = "ERROR@#@" + ex.Message;
            this.hdnMensajeError.Text = ex.Message;
        }
        finally
        {
            oP.Dispose();

        }
        return sRes;
    }
    protected string Grabar(string strDatos)
    {
        string sResul = "";
        IB.SUPER.SIC.BLL.ReceptoresAvisos oLista = new IB.SUPER.SIC.BLL.ReceptoresAvisos();
        try
        {
            #region Genero lista de objetos
            List<IB.SUPER.SIC.Models.ReceptoresAvisos> Catalogo = new List<IB.SUPER.SIC.Models.ReceptoresAvisos>();
            string[] aClase = Regex.Split(strDatos, "///");
            foreach (string oClase in aClase)
            {
                if (oClase == "") continue;
                string[] aValores = Regex.Split(oClase, "##");
                //0. ID Profesional
                //3. avisar

                IB.SUPER.SIC.Models.ReceptoresAvisos oElem = new IB.SUPER.SIC.Models.ReceptoresAvisos();
                oElem.bd = "U";
                oElem.t001_idficepi = int.Parse(aValores[0]);
                if (aValores[1]=="0")
                    oElem.t399_avisopreventa = false;
                else
                    oElem.t399_avisopreventa = true;

                Catalogo.Add(oElem);
            }
            #endregion

            oLista.GrabarLista(Catalogo);

            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar.", ex, false);
        }
        finally
        {
            oLista.Dispose();
        }
        return sResul;
    }

}