using System;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;

public partial class Reconexion : System.Web.UI.Page, ICallbackEventHandler
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
                if (Session["ua"] != null)
                {//Si es un Forastero, no debe tener acceso a establecer la contraseña de acceso a servicios web
                    this.Label2.Visible = false;
                }
                ObtenerMonedas();

                Recurso objRec = new Recurso();
                bool bIdentificado = objRec.ObtenerRecurso(Session["IDRED_ENTRADA"].ToString(), (int?)int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString()));

                if (bIdentificado)
                {
                    cboMensaje.SelectedValue = objRec.t314_nsegmb.ToString();
                    cboGASVI.SelectedValue = objRec.t314_importaciongasvi.ToString();
                    cboCorreos.SelectedValue = (objRec.t314_recibirmails) ? "1" : "0";
                    cboPeriodificacion.SelectedValue = (objRec.t314_defectoperiodificacion) ? "1" : "0";
                    cboMultiVentana.SelectedValue = (objRec.t314_multiventana) ? "1" : "0";
                    cboBotCalendario.SelectedValue = objRec.t001_botonfecha.ToString();

                    cboCARRUSEL1024.SelectedValue = (objRec.t314_carrusel1024) ? "1" : "2";
                    cboAVANCE1024.SelectedValue = (objRec.t314_avance1024) ? "1" : "2";
                    cboRESUMEN1024.SelectedValue = (objRec.t314_resumen1024) ? "1" : "2";
                    cboDATOSRES1024.SelectedValue = (objRec.t314_datosres1024) ? "1" : "2";
                    cboFICHAECO1024.SelectedValue = (objRec.t314_fichaeco1024) ? "1" : "2";
                    cboSEGRENTA1024.SelectedValue = (objRec.t314_segrenta1024) ? "1" : "2";
                    cboAVANTEC1024.SelectedValue = (objRec.t314_avantec1024) ? "1" : "2";
                    cboESTRUCT1024.SelectedValue = (objRec.t314_estruct1024) ? "1" : "2";
                    cboFOTOPST1024.SelectedValue = (objRec.t314_fotopst1024) ? "1" : "2";
                    cboPLANT1024.SelectedValue = (objRec.t314_plant1024) ? "1" : "2";
                    cboCONST1024.SelectedValue = (objRec.t314_const1024) ? "1" : "2";
                    cboIAPFACT1024.SelectedValue = (objRec.t314_iapfact1024) ? "1" : "2";
                    cboIAPDIARIO1024.SelectedValue = (objRec.t314_iapdiario1024) ? "1" : "2";
                    cboCUADROMANDO1024.SelectedValue = (objRec.t314_cuadromando1024) ? "1" : "2";
                    cboBaseCalculo.SelectedValue = (objRec.t314_calculoonline) ? "1" : "0";
                    cboObtenerEstructura.SelectedValue = (objRec.t314_cargaestructura) ? "1" : "0";
                    cboMonedaVDC.SelectedValue = objRec.t422_idmoneda_vdc.Trim();
                    cboMonedaVDP.SelectedValue = (objRec.t422_idmoneda_vdp==null)? "0":objRec.t422_idmoneda_vdp.Trim();
                    cboDiamante.SelectedValue = (objRec.bDiamanteMovil) ? "1" : "0";
                }
            }
            catch (Exception ex)
            {
                Master.sErrores += Errores.mostrarError("Error al obtener los datos de usuario.", ex);
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
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case "setResPantalla":
                sResultado += setResPantalla(aArgs[1], aArgs[2]);
                break;
            case "setMensaje":
                sResultado += setMensaje(aArgs[1]);
                break;
            case "setGASVI":
                sResultado += setGASVI(aArgs[1]);
                break;
            case "setCorreos":
                sResultado += setCorreos(aArgs[1]);
                break;
            case "setPeriodificacion":
                sResultado += setPeriodificacion(aArgs[1]);
                break;
            case "setMultiVentana":
                sResultado += setMultiVentana(aArgs[1]);
                break;
            case "setBotCalendario":
                sResultado += setBotCalendario(aArgs[1]);
                break;
            case "setResultadoOnline":
                sResultado += setResultadoOnline(aArgs[1]);
                break;
            case "setObtenerEstructura":
                sResultado += setObtenerEstructura(aArgs[1]);
                break;
            case "setMonedaVDP":
                sResultado += setMonedaVDP(aArgs[1]);
                break;
            case "setMonedaVDC":
                sResultado += setMonedaVDC(aArgs[1]);
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

    public string setResPantalla(string sPantalla, string nResolucion)
    {
        try
        {
            int nPantalla = -1;
            switch (sPantalla)
            {
                case "TODAS":
                    nPantalla = -1;
                    Session["CARRUSEL1024"] = (nResolucion == "1") ? true : false;
                    Session["AVANCE1024"] = (nResolucion == "1") ? true : false;
                    Session["RESUMEN1024"] = (nResolucion == "1") ? true : false;
                    Session["DATOSRES1024"] = (nResolucion == "1") ? true : false;
                    Session["FICHAECO1024"] = (nResolucion == "1") ? true : false;
                    Session["SEGRENTA1024"] = (nResolucion == "1") ? true : false;
                    Session["AVANTEC1024"] = (nResolucion == "1") ? true : false;
                    Session["ESTRUCT1024"] = (nResolucion == "1") ? true : false;
                    Session["FOTOPST1024"] = (nResolucion == "1") ? true : false;
                    Session["PLANT1024"] = (nResolucion == "1") ? true : false;
                    Session["CONST1024"] = (nResolucion == "1") ? true : false;
                    Session["IAPFACT1024"] = (nResolucion == "1") ? true : false;
                    Session["IAPDIARIO1024"] = (nResolucion == "1") ? true : false;
                    Session["CUADROMANDO1024"] = (nResolucion == "1") ? true : false;
                    break;
                case "CARRUSEL1024":
                    nPantalla = 1;
                    Session["CARRUSEL1024"] = (nResolucion == "1") ? true : false;
                    break;
                case "AVANCE1024":
                    nPantalla = 2;
                    Session["AVANCE1024"] = (nResolucion == "1") ? true : false;
                    break;
                case "RESUMEN1024":
                    nPantalla = 3;
                    Session["RESUMEN1024"] = (nResolucion == "1") ? true : false;
                    break;
                case "DATOSRES1024":
                    nPantalla = 4;
                    Session["DATOSRES1024"] = (nResolucion == "1") ? true : false;
                    break;
                case "FICHAECO1024":
                    nPantalla = 5;
                    Session["FICHAECO1024"] = (nResolucion == "1") ? true : false;
                    break;
                case "SEGRENTA1024":
                    nPantalla = 6;
                    Session["SEGRENTA1024"] = (nResolucion == "1") ? true : false;
                    break;
                case "AVANTEC1024":
                    nPantalla = 7;
                    Session["AVANTEC1024"] = (nResolucion == "1") ? true : false;
                    break;
                case "ESTRUCT1024":
                    nPantalla = 8;
                    Session["ESTRUCT1024"] = (nResolucion == "1") ? true : false;
                    break;
                case "FOTOPST1024":
                    nPantalla = 9;
                    Session["FOTOPST1024"] = (nResolucion == "1") ? true : false;
                    break;
                case "PLANT1024":
                    nPantalla = 10;
                    Session["PLANT1024"] = (nResolucion == "1") ? true : false;
                    break;
                case "CONST1024":
                    nPantalla = 11;
                    Session["CONST1024"] = (nResolucion == "1") ? true : false;
                    break;
                case "IAPFACT1024":
                    nPantalla = 12;
                    Session["IAPFACT1024"] = (nResolucion == "1") ? true : false;
                    break;
                case "IAPDIARIO1024":
                    nPantalla = 13;
                    Session["IAPDIARIO1024"] = (nResolucion == "1") ? true : false;
                    break;
                case "CUADROMANDO1024":
                    nPantalla = 14;
                    Session["CUADROMANDO1024"] = (nResolucion == "1") ? true : false;
                    break;
            }

            USUARIO.UpdateResolucion(nPantalla, (int)Session["NUM_EMPLEADO_ENTRADA"], (nResolucion == "1") ? true : false);

            return "OK@#@";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al establecer la resolución", ex);
        }
    }
    public string setResultadoOnline(string nOpcion)
    {
        try
        {
            USUARIO.UpdateBaseCalculo((int)Session["NUM_EMPLEADO_ENTRADA"], (nOpcion == "1") ? true : false);
            Session["CALCULOONLINE"] = (nOpcion == "1") ? true : false;

            return "OK@#@";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al configurar la importación de GASVI.", ex);
        }
    }
    public string setObtenerEstructura(string nOpcion)
    {
        try
        {
            USUARIO.UpdateObtencionEstructura((int)Session["NUM_EMPLEADO_ENTRADA"], (nOpcion == "1") ? true : false);
            Session["CARGAESTRUCTURA"] = (nOpcion == "1") ? true : false;

            return "OK@#@";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al configurar la importación de GASVI.", ex);
        }
    }

    public string setMensaje(string nSegundos)
    {
        try
        {
            USUARIO.UpdateMensajeBienvenida((int)Session["NUM_EMPLEADO_ENTRADA"], int.Parse(nSegundos));
            if (int.Parse(nSegundos) > 0)
                Session["MostrarMensajeBienvenida"] = true;
            else
                Session["MostrarMensajeBienvenida"] = false;

            return "OK@#@";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al establecer el mensaje de bienvenida.", ex);
        }
    }
    public string setGASVI(string nOpcion)
    {
        try
        {
            USUARIO.UpdateImportacionGasvi((int)Session["NUM_EMPLEADO_ENTRADA"], byte.Parse(nOpcion));
            Session["IMPORTACIONGASVI"] = byte.Parse(nOpcion);

            return "OK@#@";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al configurar la importación de GASVI.", ex);
        }
    }
    public string setCorreos(string nOpcion)
    {
        try
        {
            USUARIO.UpdateCorreosInformativos((int)Session["NUM_EMPLEADO_ENTRADA"], (nOpcion == "1") ? true : false);
            Session["RECIBIRMAILS"] = (nOpcion == "1") ? true : false;

            return "OK@#@";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al activar el mensaje de bienvenida.", ex);
        }
    }
    public string setPeriodificacion(string nOpcion)
    {
        try
        {
            USUARIO.UpdatePeriodificacionProyectos((int)Session["NUM_EMPLEADO_ENTRADA"], (nOpcion == "1") ? true : false);

            return "OK@#@";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al configurar la periodificación de proyectos.", ex);
        }
    }
    public string setMultiVentana(string nOpcion)
    {
        try
        {
            USUARIO.UpdateMultiVentana((int)Session["NUM_EMPLEADO_ENTRADA"], (nOpcion == "1") ? true : false);

            return "OK@#@";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al configurar la periodificación de proyectos.", ex);
        }
    }
    public string setBotCalendario(string nOpcion)
    {
        try
        {
            Recurso.SetBotonCalendario(null, (int)Session["IDFICEPI_ENTRADA"], nOpcion);
            Session["BTN_FECHA"] = nOpcion;

            return "OK@#@";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al configurar el acceso a calendario.", ex);
        }
    }
    public void ObtenerMonedas()
    {
        SqlDataReader dr = MONEDA.ObtenerMonedasVDP();
        cboMonedaVDP.AppendDataBoundItems = true;
        cboMonedaVDP.DataValueField = "t422_idmoneda";
        cboMonedaVDP.DataTextField = "t422_denominacion";
        cboMonedaVDP.DataSource = dr;
        cboMonedaVDP.DataBind();
        dr.Close();
        dr.Dispose();

        SqlDataReader dr1 = MONEDA.ObtenerMonedasVDC();
        cboMonedaVDC.AppendDataBoundItems = true;
        cboMonedaVDC.DataValueField = "t422_idmoneda";
        cboMonedaVDC.DataTextField = "t422_denominacion";
        cboMonedaVDC.DataSource = dr1;
        cboMonedaVDC.DataBind();
        dr1.Close();
        dr1.Dispose();
    }
    public string setMonedaVDP(string sValor)
    {
        try
        {
            USUARIO.UpdateMonedaVDP((int)Session["NUM_EMPLEADO_ENTRADA"], (sValor == "") ? null : sValor);

            Session["MONEDA_VDP"] = (sValor == "") ? null : sValor;
            Session["DENOMINACION_VDP"] = (sValor == "") ? null : MONEDA.getDenominacionImportes(Session["MONEDA_VDP"].ToString());

            return "OK@#@";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al establecer la moneda por defecto para datos de proyecto.", ex);
        }
    }
    public string setMonedaVDC(string sValor)
    {
        try
        {
            USUARIO.UpdateMonedaVDC((int)Session["NUM_EMPLEADO_ENTRADA"], sValor);

            Session["MONEDA_VDC"] = sValor;
            Session["DENOMINACION_VDC"] = MONEDA.getDenominacionImportes(Session["MONEDA_VDC"].ToString());

            return "OK@#@";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al establecer la moneda por defecto en la que ser mostrarán los datos consolidados de proyectos.", ex);
        }
    }
}
