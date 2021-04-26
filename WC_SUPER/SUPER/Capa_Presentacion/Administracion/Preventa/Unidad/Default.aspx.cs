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

using System.Text.RegularExpressions;
using System.Text;
using SUPER.Capa_Negocio;
using SUPER.BLL;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores = "";
    public SqlConnection oConn;
    public SqlTransaction tr;

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
                // Leer Empresa

                if (Request.QueryString["unidad"] != "")
                {
                    txtCodigo.Text = Utilidades.decodpar(Request.QueryString["unidad"].ToString());
                    short idUnidad = short.Parse(txtCodigo.Text);
                    if (idUnidad <= 0)
                    {
                        txtDenominacion.ReadOnly = true;
                    }
                    IB.SUPER.ADM.SIC.Models.UnidadPreventa oElem = new IB.SUPER.ADM.SIC.Models.UnidadPreventa();
                    IB.SUPER.ADM.SIC.BLL.UnidadPreventa oUnidad = new IB.SUPER.ADM.SIC.BLL.UnidadPreventa();

                    oElem = oUnidad.Select(idUnidad);
                    //PreventaUnidad oUnidad = PreventaUnidad.Select(null, short.Parse(txtCodigo.Text));
                    if (oElem.ta199_denominacion == "")
                        throw (new Exception("Unidad de preventa no existente."));
                    txtDenominacion.Text = oElem.ta199_denominacion;

                    if (oElem.ta199_estadoactiva) chkActiva.Checked = true;
                    else chkActiva.Checked = false;

                    oUnidad.Dispose();
                }
            }
            catch (Exception ex)
            {
                sErrores += SUPER.Capa_Negocio.Errores.mostrarError("Error al obtener los datos de la unidad de preventa", ex);
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
                sResultado += Grabar(aArgs[1],			                             // id Unidad
                                    SUPER.Capa_Negocio.Utilidades.unescape(aArgs[2]),// Denominanción Empresa
                                    aArgs[3]                                         //Activa
                                    );

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

    private string Grabar(string sCodigo,
                           string sDenominacion,
                           string sActiva
        )
    {
        string sResul = "";
        short nID = -1;
        bool bActiva=false;
        if (sActiva == "1") bActiva = true;
        IB.SUPER.ADM.SIC.Models.UnidadPreventa oUnidad = new IB.SUPER.ADM.SIC.Models.UnidadPreventa();
        IB.SUPER.ADM.SIC.Models.UnidadPreventa oUnidadD = new IB.SUPER.ADM.SIC.Models.UnidadPreventa();
        IB.SUPER.ADM.SIC.BLL.UnidadPreventa oElem = new IB.SUPER.ADM.SIC.BLL.UnidadPreventa();
        try
        {
            oUnidad.ta199_denominacion = sDenominacion;
            oUnidad.ta199_estadoactiva = bActiva;
            #region Datos Generales

            oUnidadD = oElem.SelectPorDenominacion(oUnidad.ta199_denominacion);
            if (sCodigo == "")
            {
                //nID = PreventaUnidad.Insert(tr,sDenominacion,bActiva);
                if (oUnidadD != null) return "AVISO@#@Ya existe una unidad con la misma denominación";
                nID = oElem.Insert(oUnidad);
            }
            else //update
            {
                nID = short.Parse(sCodigo);
                if (oUnidadD != null && nID != oUnidadD.ta199_idunidadpreventa) return "AVISO@#@Ya existe una unidad con la misma denominación";
                oUnidad.ta199_idunidadpreventa = nID;
                //PreventaUnidad.Update(tr, nID, sDenominacion, bActiva);
                oElem.Update(oUnidad);
            }
            #endregion

            sResul = "OK@#@" + nID.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos de la unidad de preventa", ex);
        }
        finally
        {
            oElem.Dispose();
        }
        return sResul;
    }
}
