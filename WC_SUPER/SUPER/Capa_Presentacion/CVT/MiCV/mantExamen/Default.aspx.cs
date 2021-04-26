using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using SUPER.BLL;
using SUPER.Capa_Negocio;
using System.Text.RegularExpressions;

public partial class Capa_Presentacion_CVT_MiCV_mantExamen : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sTareasPendientes = "", sIDDocuAux = "";
    public SqlConnection oConn;
    public SqlTransaction tr;


    protected void Page_Load(object sender, EventArgs e)
    {
        string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
        string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

        //2º Se "registra" la función que va a acceder al servidor.
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
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

            if (!Page.IsPostBack)
            {
                sIDDocuAux = "SUPER-" + Session["IDFICEPI_CVT_ACTUAL"].ToString() + "-" + DateTime.Now.Ticks.ToString();

                Utilidades.SetEventosFecha(this.txtFechaO);
                Utilidades.SetEventosFecha(this.txtFechaC);

                this.hdnIdFicepi.Value = SUPER.Capa_Negocio.Utilidades.decodpar(Request.QueryString["iF"].ToString());
                this.hdnUserAct.Value = Session["IDFICEPI_CVT_ACTUAL"].ToString();

                //Compruebo si estoy en mi propio curriculum
                if (this.hdnIdFicepi.Value == this.hdnUserAct.Value)
                    this.hdnEsMiCV.Value = "S";

                if (Request.QueryString["c"] != null && (Request.QueryString["c"] != ""))
                    hdnIdCertificado.Value = Utilidades.decodpar(Request.QueryString["c"]);
                
                if (Request.QueryString["eA"] != null && (Request.QueryString["eA"] != ""))
                    hdnEsEncargado.Value = Utilidades.decodpar(Request.QueryString["eA"]);

                if (Request.QueryString["iE"] != null && Request.QueryString["iE"] != "")
                {
                    hdnIdExamen.Value = Utilidades.decodpar(Request.QueryString["iE"]);
                    hdnIdExamenOld.Value = hdnIdExamen.Value;
                }

                if (Request.QueryString["eE"] != null && (Request.QueryString["eE"] != ""))
                    hdnEstado.Value = Utilidades.decodpar(Request.QueryString["eE"]);
                else
                    hdnEstado.Value = "B";

                switch (hdnEstado.Value)
                {
                    case "S": //Pte. cumplimentar (origen ECV)
                    case "T": //Pte. cumplimentar (origen Validador)
                        imgEstado.ImageUrl = "~/Images/imgEstadoCVTPenCumplimentar.png";
                        break;
                    case "O": //Pte. validar (origen ECV)
                    case "P": //Pte. validar (origen Validador)
                        //imgEstado.ImageUrl = "~/Images/imgEstadoCVTPenValidar.png";
                        imgEstado.ImageUrl = "~/Images/imgSeparador.gif";
                        break;
                    case "Y": //Pseudovalidado (origen ECV)
                    case "X": //Pseudovalidado (origen Validador)
                        imgEstado.ImageUrl = "~/Images/imgEstadoCVTPseudovalidado.png";
                        break;
                    case "B": //Borrador
                        imgEstado.ImageUrl = "~/Images/imgEstadoCVTBorrador.png";
                        break;
                    case "R": //No Interesante
                        //imgEstado.ImageUrl = "~/Images/imgEstadoCVTNoInteresante.png";
                        imgEstado.ImageUrl = "~/Images/imgSeparador.gif";
                        break;
                    case "V": //Validado
                        //imgEstado.ImageUrl = "~/Images/imgEstadoCVTValidado.png";
                        imgEstado.ImageUrl = "~/Images/imgSeparador.gif";
                        break;
                }
                //Recojo si el certificado tiene examenes sin borrado pdte
                if (Request.QueryString["te"] != null && (Request.QueryString["te"] != ""))
                    this.hdnHayExamenes.Value = Utilidades.decodpar(Request.QueryString["te"]);

                if (Request.QueryString["nm"] != null && (Request.QueryString["nm"] != ""))
                    txtDenom.Value = Utilidades.decodpar(Request.QueryString["nm"]);

                if (txtDenom.Value == "")
                    this.lblBuscar.Visible = true;

                if (Request.QueryString["fO"] != null && (Request.QueryString["fO"] != ""))
                    txtFechaO.Text = Utilidades.decodpar(Request.QueryString["fO"]);

                if (Request.QueryString["fC"] != null && (Request.QueryString["fC"] != ""))
                    txtFechaC.Text = Utilidades.decodpar(Request.QueryString["fC"]);

                if (Request.QueryString["eC"] != null && (Request.QueryString["eC"] != ""))
                    hdnEntCert.Value = Utilidades.decodpar(Request.QueryString["eC"]);

                if (Request.QueryString["eT"] != null && (Request.QueryString["eT"] != ""))
                    hdnEntorno.Value = Utilidades.decodpar(Request.QueryString["eT"]);

                if (Request.QueryString["t2"] != null && (Request.QueryString["t2"] != ""))
                    this.hdnContentServer.Value = Utilidades.decodpar(Request.QueryString["t2"]);

                if (hdnEstado.Value == "S" || hdnEstado.Value == "T") //Pendiente de cumplimentar.
                {
                    //imgEstado.Attributes.Add("onmouseover", "showTTE(\"" + Utilidades.decodpar(Request.QueryString["mot"]) + "\",\"Motivo\",null,300)");
                    //imgEstado.Attributes.Add("onmouseout", "hideTTE()");
                    imgInfoEstado.Style.Add("visibility", "visible");
                    imgInfoEstado.Attributes.Add("onmouseover", "showTTE(\"" + Utilidades.decodpar(Request.QueryString["mot"]) + "\",\"Motivo\",null,300)");
                    imgInfoEstado.Attributes.Add("onmouseout", "hideTTE()");
                }
                if (hdnEstado.Value != "V")
                {
                    imgEstado.Attributes.Add("onmouseover", "showTTE(\"" + Utilidades.escape(Curvit.ToolTipEstados(hdnEstado.Value)) + "\",\"Información\",null,300)");
                    imgEstado.Attributes.Add("onmouseout", "hideTTE()");
                }

                if (Utilidades.decodpar(Request.QueryString["tD"]) == "1")
                {
                    if (Utilidades.decodpar(Request.QueryString["url"]) != "")
                        imgDownloadDoc.Attributes.Add("onclick", "verDOC2('" + Request.QueryString["url"] + "')");
                    else
                        imgDownloadDoc.Attributes.Add("onclick", "verDOC()");
                    txtNombreDocumento.Text = Utilidades.decodpar(Request.QueryString["nD"]);
                    //imgUploadDoc.Style.Add("display", "inline-block");
                    imgDownloadDoc.Style.Add("display", "inline-block");
                    //imgBorrarDoc.Style.Add("display", "inline-block");
                }
                //12/06/2018 Cualquier modificación sobre exámenes se hará a través de una nueva solicitud  
                txtNombreDocumento.Enabled = false;                

                if (hdnEsEncargado.Value == "1")
                    omitirObligParaEncargado();
                
                bool bEstaDeBaja = SUPER.BLL.Profesional.EstaDeBaja(int.Parse(hdnProf.Value));

                //Tratamiento especial en la botonera de los exámenes       
                //12/06/2018 Cualquier modificación sobre exámenes se hará a través de una nueva solicitud  
                deshabilitarUsuario();
                btnSalir.Style.Add("display", "inline-block");
                btnCancelar.Style.Add("display", "none");
                btnEnviar.Style.Add("display", "none");

               /* if (hdnEstado.Value == "V")
                {
                    deshabilitarUsuario();
                    btnSalir.Style.Add("display", "inline-block");
                    btnCancelar.Style.Add("display", "none");
                }
                else
                {
                    btnEnviar.Style.Add("display", "inline-block");
                    if (hdnEsEncargado.Value == "1")
                    {
                        if (!bEstaDeBaja) btnCumplimentar.Style.Add("display", "inline-block");
                    }
                    //else if (hdnEstado.Value == "B")
                    //{
                    //    btnAparcar.Style.Add("display", "inline-block");
                    //}
                }*/
            }

            if (User.IsInRole("DIS") || ConfigurationManager.AppSettings["MOSTRAR_SOLODIS"] == "0")
                sTareasPendientes = Curriculum.MiCVTareasPendientes(6, int.Parse(hdnIdFicepi.Value), (hdnIdExamen.Value == "") ? 0 : int.Parse(this.hdnIdExamen.Value),null);
            //Compruebo si en el historial la última acción fué enviar a cumplimentar, en cuyo caso cargo el mensaje
            //que el validador le quiere hacer llegar al profesional
            this.hdnMsgCumplimentar.Value = SUPER.BLL.Historial.GetMsgPdteValidar("T595_FICEPIEXAMENCRONO", int.Parse(hdnIdFicepi.Value),
                                                                                  (this.hdnIdExamen.Value == "") ? -1 : int.Parse(this.hdnIdExamen.Value));
        }
        catch (Exception ex)
        {
            hdnErrores.Value = Errores.mostrarError("Error al obtener los datos", ex);
        }
    }
    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "", sCad="";
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("grabar"):
                sResultado += Grabar(int.Parse(aArgs[1]), int.Parse(aArgs[2]), int.Parse(aArgs[3]),
                                     Utilidades.unescape(aArgs[4]), aArgs[5], aArgs[6], aArgs[7], aArgs[8], Utilidades.unescape(aArgs[9]));
                break;
            case ("documentos"):
                sCad = getDocumento(aArgs[1], aArgs[5]);
                if (sCad.IndexOf("Error@#@") >= 0) sResultado += sCad;
                else sResultado += "OK@#@" + sCad;
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

    public void deshabilitarUsuario()
    {
        try
        {
            omitirObligParaEncargado();
            txtDenom.Disabled = true;
            
            txtFechaO.Enabled = false;
            txtFechaC.Enabled = false;

            imgUploadDoc.Style.Add("display", "none");
            imgBorrarDoc.Style.Add("display", "none");
        }
        catch (Exception ex)
        {
            hdnErrores.Value = Errores.mostrarError("Error al deshabilitar controles", ex);
        }
    }

    public void omitirObligParaEncargado()
    {
        try
        {
            foreach (Control ctrl in Page.FindControl("formDetExamen").Controls)
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

    private string Grabar(int t001_idficepi, int idExamenOld, int idExamenNew, string sDenDoc, string sUsuTick,
                          string sFechaO, string sFechaC, string t839_idestado, string sMotivo)
    {
        string sRes = "";
        DateTime? dtFecObtencion = null;
        DateTime? dtFecCaducidad = null;

        try
        {
            if (sFechaO != "")
                dtFecObtencion = DateTime.Parse(sFechaO);
            if (sFechaC != "")
                dtFecCaducidad = DateTime.Parse(sFechaC);

            int aux = SUPER.BLL.Examen.AsignarProfesional(null, t001_idficepi, idExamenOld, idExamenNew, sDenDoc, sUsuTick,
                                                         dtFecObtencion, dtFecCaducidad, t839_idestado.ToCharArray()[0], true, sMotivo);
            sRes="OK@#@";
        }
        catch (Exception e)
        {
            sRes = "Error@#@" + e.Message;
        }
        return sRes;
    }
    private string getDocumento(string sUsuTicks, string sIdFicepi)
    {
        string sRes = "";
        try
        {
            if (Utilidades.isNumeric(sUsuTicks))
            {
                //dr = DOCUOF.Catalogo(int.Parse(st610_idordenfac));
                //sRes += dr["t624_nombrearchivo"].ToString();
                SUPER.BLL.Examen oExamen = SUPER.BLL.Examen.SelectDoc(null, int.Parse(sUsuTicks), int.Parse(sIdFicepi));
                sRes = oExamen.T591_NDOC + "@#@S@#@" + oExamen.t2_iddocumento.ToString();
            }
            else
            {
                SUPER.BLL.DocuAux oDoc = SUPER.BLL.DocuAux.GetDocumento(null, sUsuTicks);
                sRes = oDoc.t686_nombre + "@#@N@#@" + sUsuTicks;
            }

            return sRes;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener documento del examen", ex);
        }
    }
}
