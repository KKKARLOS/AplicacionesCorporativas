using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using GASVI.BLL;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.TituloPagina = "Configuración personal";
            Master.bFuncionesLocales = true;

            try
            {
                int idEmpresaDefecto = int.Parse(Session["GVT_IDEMPRESADEFECTO"].ToString());
                obtenerMotivos();
                obtenerMonedas();
                obtenerEmpresas(idEmpresaDefecto);
                cboAviso.SelectedValue = ((bool)Session["GVT_AVISOCAMBIOESTADO"]) ? "1" : "0";
                cboMotivo.SelectedValue = (Session["GVT_MOTIVODEFECTO"] != null) ? Session["GVT_MOTIVODEFECTO"].ToString() : "";
                cboEmpresa.SelectedValue = idEmpresaDefecto.ToString();
            }
            catch (Exception ex)
            {
                Master.sErrores += Errores.mostrarError("Error al obtener los datos de la configuración.", ex);
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
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["GVT_IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("setMoneda"):
                try
                {
                    Configuracion.setMoneda((int)Session["GVT_IDFICEPI_ENTRADA"], aArgs[1]);
                    Session["GVT_MONEDADEFECTO"] = aArgs[1];
                    sResultado += "OK@#@";
                }
                catch (Exception ex)
                {
                    sResultado += "Error@#@" + Errores.mostrarError("Error al grabar el cambio en la moneda por defecto.", ex);
                }
                break;
            case ("setAviso"):
                try
                {
                    bool bAviso = (aArgs[1] == "1") ? true : false;
                    Configuracion.setAviso((int)Session["GVT_IDFICEPI_ENTRADA"], bAviso);
                    Session["GVT_AVISOCAMBIOESTADO"] = bAviso;
                    sResultado += "OK@#@";
                }
                catch (Exception ex)
                {
                    sResultado += "Error@#@" + Errores.mostrarError("Error al grabar el cambio en el aviso de cambio de estado.", ex);
                }
                break;
            case ("setMotivo"):
                try
                {
                    Configuracion.setMotivo((int)Session["GVT_IDFICEPI_ENTRADA"], aArgs[1]);
                    if (aArgs[1] != "") Session["GVT_MOTIVODEFECTO"] = byte.Parse(aArgs[1]);
                    else Session["GVT_MOTIVODEFECTO"] = null;
                    sResultado += "OK@#@";
                }
                catch (Exception ex)
                {
                    sResultado += "Error@#@" + Errores.mostrarError("Error al grabar el cambio en el motivo por defecto.", ex);
                }
                break;
            case ("setEmpresa"):
                try
                {
                    Configuracion.setEmpresa((int)Session["GVT_IDFICEPI_ENTRADA"], int.Parse(aArgs[1]));
                    Session["GVT_IDEMPRESADEFECTO"] = aArgs[1];
                    Session["GVT_EMPRESADEFECTO"] = aArgs[2];
                    sResultado += "OK@#@";
                }
                catch (Exception ex)
                {
                    sResultado += "Error@#@" + Errores.mostrarError("Error al grabar el cambio en la empresa por defecto.", ex);
                }
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

    public void obtenerMotivos()
    {
        List<ElementoLista> oLista = MOTIVO.ObtenerMotivosActivos();
        ListItem oLI = null;
        oLI = new ListItem("", "");
        cboMotivo.Items.Add(oLI);
        foreach (ElementoLista oMotivo in oLista)
        {
            oLI = new ListItem(oMotivo.sDenominacion, oMotivo.sValor);
            cboMotivo.Items.Add(oLI);
        }
    }

    public void obtenerMonedas()
    {
        List<ElementoLista> oLista = MONEDA.ObtenerMonedas(true);
        ListItem oLI = null;
        foreach (ElementoLista oMoneda in oLista)
        {
            oLI = new ListItem(oMoneda.sDenominacion, oMoneda.sValor);
            if (oMoneda.sValor == Session["GVT_MONEDADEFECTO"].ToString()) oLI.Selected = true;
            cboMoneda.Items.Add(oLI);
        }
    }

    public void obtenerEmpresas(int idEmpresaDefecto)
    {
        ArrayList aEmpresas = Profesional.ObtenerEmpresasTerritorios(int.Parse(Session["GVT_USUARIOSUPER"].ToString()));

        ListItem oLI = null;
        //Si no hay empresa por defecto añado opción vacía
        if (idEmpresaDefecto==0)
        {
            oLI = new ListItem("", "0");
            cboEmpresa.Items.Add(oLI);
        }
        for (int i = 0; i < aEmpresas.Count; i++)
        {
            oLI = new ListItem(((string[])aEmpresas[i])[1], ((string[])aEmpresas[i])[0]);
            cboEmpresa.Items.Add(oLI);
        }
    }
}
