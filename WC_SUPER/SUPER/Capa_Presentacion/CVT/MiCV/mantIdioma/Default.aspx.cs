using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SUPER.Capa_Negocio;
using SUPER.BLL;
using System.Text.RegularExpressions;

public partial class Capa_Presentacion_CVT_MiCV_mantIdioma_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    protected byte[] ArchivoEnBinario;
    public string sNombre, sIDDocuAux = "";
    protected HttpPostedFile Archivo;
    public string strTablaHTML = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                if (Session["IDRED"] == null)
                {
                    try
                    {
                        Response.Redirect("~/SesionCaducadaModal.aspx", true);
                    }
                    catch (System.Threading.ThreadAbortException) { return; }
                }

                if (!Page.IsPostBack)
                {
                    if (Request.QueryString["ficepi"] != null) 
                        hdnIdFicepi.Value = Utilidades.decodpar(Request.QueryString["ficepi"]);
                    if (Request.QueryString["esEnc"] != null) 
                        hdnEsEncargado.Value = Utilidades.decodpar(Request.QueryString["esEnc"]);
                    if (Utilidades.decodpar(Request.QueryString["idioma"]) == "-1")//Insert
                    {
                        cboIdioma.DataSource = Idioma.obtenerCboIdioma(int.Parse(hdnIdFicepi.Value.ToString()));
                        cboIdioma.DataValueField = "sValor";
                        cboIdioma.DataTextField = "sDenominacion";
                        cboIdioma.DataBind();
                        //CargarCombos();
                    }
                    else //Update
                    {
                        cboIdioma.Style.Add("display", "none");
                        txtIdioma.Style.Add("display", "inline-block");
                        if (Request.QueryString["idioma"] != null)
                        {
                            hdnIdCodIdioma.Value = Utilidades.decodpar(Request.QueryString["idioma"]);
                            this.hdnIdCodIdiomaEntrada.Value = this.hdnIdCodIdioma.Value;
                        }
                        CargarDatos(IdiomaFic.Detalle(int.Parse(hdnIdFicepi.Value.ToString()), int.Parse(hdnIdCodIdioma.Value.ToString())));

                        string[] aTablas = Regex.Split(CargarTitulos(int.Parse(hdnIdFicepi.Value.ToString()), int.Parse(hdnIdCodIdioma.Value.ToString())), "@#@");
                        if (aTablas[0] == "OK")
                            this.strTablaHTML = aTablas[1];
                        else
                            hdnErrores.Value = aTablas[1];
                    }
                    if (hdnEsEncargado.Value == "1" || Utilidades.decodpar(Request.QueryString["idioma"]) != "-1")
                        omitirObligParaEncargado();
                }
            }
            //1º Se indican (por este orden) la función a la que se va a devolver el resultado
            //   y la función que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

            //2º Se "registra" la función que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
        catch (Exception ex)
        {
            hdnErrores.Value = Errores.mostrarError("Error al cargar la pagina", ex);
        }
    }


    public void CargarDatos(IdiomaFic o)
    {
            txtIdioma.Value = o.T020_DESCRIPCION;
            hdnIdCodIdioma.Value = o.T020_IDCODIDIOMA.ToString();
            hdnOP.Value = "2";

            switch (o.T013_LECTURA)
            {
                case 1: rdbLectura_0.Checked = true; break;
                case 2: rdbLectura_1.Checked = true; break;
                case 3: rdbLectura_2.Checked = true; break;
            }
            switch (o.T013_ESCRITURA)
            {
                case 1: rdbEscritura_0.Checked = true; break;
                case 2: rdbEscritura_1.Checked = true; break;
                case 3: rdbEscritura_2.Checked = true; break;
            }
            switch (o.T013_ORAL)
            {
                case 1: rdbConversacion_0.Checked = true; break;
                case 2: rdbConversacion_1.Checked = true; break;
                case 3: rdbConversacion_2.Checked = true; break;
            }
    }

    public string CargarTitulos(int idFicepi, int idIdioma)
    {
        try
        {
            return "OK@#@" + TituloIdiomaFic.Catalogo(idFicepi, idIdioma);
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los títulos", ex);
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
        try
        {

            switch (aArgs[0])
            {
                case ("grabar"):
                    IdiomaFic.Grabar((aArgs[1] == "") ? null : (int?)int.Parse(aArgs[1]), 
                                     int.Parse(aArgs[2]), 
                                     (aArgs[3] == "") ? null : (int?)int.Parse(aArgs[3]), 
                                     (aArgs[4] == "") ? null : (int?)int.Parse(aArgs[4]), 
                                     (aArgs[5] == "") ? null : (int?)int.Parse(aArgs[5]), 
                                     int.Parse(hdnIdFicepi.Value));
                    sResultado += "OK";
                    break;
                case ("eliminartitulo"):
                    sResultado += TituloIdiomaFic.Delete(aArgs[1]).ToString();
                    break;
                case ("CargarTitulos"):
                    sResultado += CargarTitulos(int.Parse(aArgs[1]), int.Parse(aArgs[2]));
                    break;
            }
        }
        catch (Exception ex)
        {

            switch (aArgs[0])
            {
                case ("grabar"):
                    sResultado += "Error@#@" + Errores.mostrarError("Error al grabar", ex);
                    break;
                case ("eliminartitulo"):
                    sResultado += "Error@#@" + Errores.mostrarError("Error al eliminar", ex);
                    break;
            }
        }

        //3º Damos contenido a la variable que se envía de vuelta al cliente.
        _callbackResultado = sResultado;
    }

    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }

    public void omitirObligParaEncargado()
    {
        try
        {
            foreach (Control ctrl in Page.FindControl("form1").Controls)
            {//Ocultar asteriscos para el encargado de curriculums
                if (ctrl.GetType().Name == "Label")
                {
                    if (((Label)ctrl).Text == "*")
                    {
                        ((Label)ctrl).Style["display"] = "none";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            hdnErrores.Value = Errores.mostrarError("Error al omitir obligatoriedad", ex);
        }
    }
}