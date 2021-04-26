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
using EO.Web; 
using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;
using System.Text;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            //Para que se muestre la botonera, �nicamente hay que indicar el n�mero de botonera
            //y crear el manejador de eventos para la misma.
            Master.nBotonera = 7;
            Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);
            Master.bFuncionesLocales = true;
            Master.TituloPagina = "Maestro de plantillas";
            if (!Page.IsPostBack)
            {
                try
                {
    	            this.lblNodo.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
                    this.lblNodo.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.NODO));
                    
                    this.txtOrigen.Text = Request.QueryString["sOr"];
                    string sPlantAux = Request.QueryString["nIDPlant"];
                    if (sPlantAux != null)
                    {
                        this.hdnIDPlantilla.Text = sPlantAux;
                        this.hdnIDPlantillaOriginal.Text = sPlantAux;
                        Session["IDPlant"] = sPlantAux;
                    }
                    else if (Session["IDPlant"] != null)
                    {
                        sPlantAux = Session["IDPlant"].ToString();
                        this.hdnIDPlantilla.Text = sPlantAux;
                        this.hdnIDPlantillaOriginal.Text = sPlantAux;
                    }

                    string sTipo = Request.QueryString["sTipo"];
                    if (sTipo != null) this.txtTipo.Text = sTipo;
                    else this.txtTipo.Text = "E";

                    PlantProy objPlant = new PlantProy();
                    objPlant.Obtener(int.Parse(this.hdnIDPlantilla.Text));

                    this.hndCRActual.Text = Request.QueryString["nCR"];
                    CR objCR = new CR();
                    //int iNumEmpleado = int.Parse(HttpContext.Current.Session["IDFICEPI_PC_ACTUAL"].ToString());
                    int iNumEmpleado = int.Parse(HttpContext.Current.Session["UsuarioActual"].ToString());
                    this.cboCR.DataSource = objCR.ObtenerCatalogoPlant(iNumEmpleado);
                    this.cboCR.DataTextField = "sDesCR";
                    this.cboCR.DataValueField = "nIdCR";
                    this.cboCR.DataBind();

                    ListItem Elemento = new ListItem("", "-1");
                    this.cboCR.Items.Insert(0, Elemento);

                    this.txtDesPlantilla.Text = objPlant.descripcion;
                    if (objPlant.activo) this.chkActivo.Checked = true;
                    else this.chkActivo.Checked = false;
                    this.cboCR.SelectedValue = objPlant.codune.ToString();

                    this.cboAmbito.SelectedValue = objPlant.ambito;

                    this.txtObs.Text = objPlant.obs;
                    //Establezco los posibles valores del combo de �mbito
                    plEstablecerAmbitos();
                    //Establezco la modificabilidad de la plantilla
                    this.txtModificable.Text = flPlantillaModificable(objPlant.ambito);
                    if (this.txtModificable.Text == "T")
                    {
                        if (objPlant.ambito != "D") cboCR.Enabled = false;
                        else cboCR.Enabled = true;
                        this.txtDesPlantilla.Focus();
                    }
                }
                catch (Exception ex)
                {
                    Master.sErrores = Errores.mostrarError("Error al cargar la plantilla", ex);
                }
            }

            //1� Se indican (por este orden) la funci�n a la que se va a devolver el resultado
            //   y la funci�n que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

            //2� Se "registra" la funci�n que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
    }
    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        //1� Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2� Aqu� realizar�amos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("grabar"):
                sResultado += Grabar(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7]);
                break;
            //case ("eliminar"):
            //    sResultado += EliminarPlantilla(aArgs[1]);
            //    break;
        }

        //3� Damos contenido a la variable que se env�a de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se env�a el resultado al cliente.
        return _callbackResultado;
    }
    protected void Botonera_Click(object sender, EO.Web.ToolBarEventArgs e)
    {
        int iPos;
        switch (e.Item.CommandName.ToLower())
        {
            //case "grabar":
            //    Grabar();
            //    break;
            case "regresar":
                string strUrl = HistorialNavegacion.Leer();
                //if (Request.QueryString["rCR"].ToString() != "-1")//Si hay que restaurar el CR con el que hemos llegado
                //{
                //    iPos = strUrl.IndexOf("&nCR");
                //    if (iPos == -1) strUrl += "&nCR=" + Request.QueryString["nCR"].ToString();
                //    else
                //    {
                //        strUrl = "~" + strUrl.Substring(6, iPos - 6) + "&nCR=" + Request.QueryString["nCR"].ToString();
                //    }
                //}
                //else
                //{
                //    iPos = strUrl.IndexOf("&nCR");
                //    if (iPos != -1) strUrl = "~" + strUrl.Substring(6, iPos - 6);
                //}
                if (Request.QueryString["rCR"] != null)
                {
                    iPos = strUrl.IndexOf("?sTipo=");
                    if (iPos != -1)
                    {
                        strUrl = strUrl.Substring(0, iPos + 9);
                    }
                    strUrl += "&rCR=" + Request.QueryString["rCR"].ToString();
                    strUrl += "&bE=" + Request.QueryString["bE"].ToString();
                    strUrl += "&bD=" + Request.QueryString["bD"].ToString();
                    strUrl += "&bP=" + Request.QueryString["bP"].ToString();
                }
                try
                {
                    Response.Redirect(strUrl, true);
                }
                catch (System.Threading.ThreadAbortException) { }
                break;
        }
    }
    //protected void Regresar()
    //{
    //    Response.Redirect(HistorialNavegacion.Leer(), true);
    //}
    private string Grabar(string sIdPlant, string sDesPlant, string sCodUne, string sEstado, string sAmbito, string sTipo, string sObs)
    {
        string sResul = "";
        int iPlant, iPromotor = int.Parse(Session["IDFICEPI_PC_ACTUAL"].ToString());
        //bool bInsertarPT;
        SqlConnection oConn=null;
        SqlTransaction tr=null;
        try
        {
            if (sCodUne == "") sCodUne = "-1";
            if (sIdPlant != "0")
            {
                PlantProy.Modificar(int.Parse(sIdPlant), sAmbito, Utilidades.unescape(sDesPlant), int.Parse(sEstado), iPromotor, int.Parse(sCodUne), Utilidades.unescape(sObs));
                sResul = "OK@#@" + sIdPlant;
            }
            else
            {
                //if (sAmbito=="T") bInsertarPT = true;
                //else bInsertarPT = false;
                try
                {
                    oConn = Conexion.Abrir();
                    tr = Conexion.AbrirTransaccion(oConn);
                }
                catch (Exception ex)
                {
                    sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexi�n", ex);
                    return sResul;
                }

                iPlant = PlantProy.Insertar(tr, sTipo, Utilidades.unescape(sDesPlant), int.Parse(sEstado), sAmbito, iPromotor, int.Parse(sCodUne), Utilidades.unescape(sObs));

                Conexion.CommitTransaccion(tr);

                this.hdnIDPlantilla.Text = iPlant.ToString();
                Session["IDPlant"] = iPlant.ToString();
                sResul = "OK@#@" + iPlant.ToString();
            }
        }
        catch (Exception ex)
        {
            //TextBox hdnErrores = (TextBox)Master.FindControl("hdnErrores");
            //hdnErrores.Text = Errores.mostrarError("Error al grabar el detalle del calendario", ex);
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar el maestro de la plantilla", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    //private string EliminarPlantilla(string sIDPlant)
    //{
    //    string sResul = "";
    //    try
    //    {
    //        PlantProy.Eliminar(int.Parse(sIDPlant));
    //        sResul = "OK";

    //    }
    //    catch (Exception ex)
    //    {
    //        Master.sErrores = Errores.mostrarError("Error al eliminar la plantilla", ex);
    //    }

    //    return sResul;
    //}
    private string flPlantillaModificable(string sAmbito)
    {
        //	Si la plantilla es Empresarial solo ser� modificable si el usuario conectado tiene perfil de Administrador
        //	Si la plantilla es Departamental solo ser� modificable si el usuario conectado tiene perfil de Oficina T�cnica o superior
        //	Si la plantilla es Personal siempre es modificable (se supone que un usuario solo ve las plantillas personales que son suyas)
        string sResul = "F";
        try
        {
            switch (sAmbito)
            {
                case "E"://empresarial
                    if (User.IsInRole("A")) sResul = "T";
                    break;
                case "D"://departamental
                    if (User.IsInRole("A")) sResul = "T";
                    else
                    {
                        if (User.IsInRole("RSN") || User.IsInRole("DSN") || User.IsInRole("ISN") || User.IsInRole("RN")
                            || User.IsInRole("DN") || User.IsInRole("CN") || User.IsInRole("IN") || User.IsInRole("OT"))
                        {
                            sResul = "T";
                        }
                    }
                    break;
                case "P"://privada
                    sResul = "T";
                    break;
                case ""://no tiene ambito -> vamos a crear una plantilla nueva
                    sResul = "T";
                    break;
            }
        }
        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al establecer la modificabilidad de la plantilla", ex);
        }

        return sResul;
    }
    private void plEstablecerAmbitos()
    {
        // Si entro sin c�digo de plantilla es que quiero generar una nueva plantilla
        // Si hemos entrado desde el men� ADM
        //	    Si el usuario es administrador podr� elegir entre Empresarial, Departamental y Personal
        //	    Si el usuario es directo de CR o integrante de Oficina T�cnica podr� elegir entre Departamental y Personal
        //	    En otro caso solo tendr� Personal (no editable)
        //Si hemos entrado desde el men� PST
        //	    Si el usuario es administrador, director de CR o integrante de Oficina T�cnica podr� elegir entre Departamental y Personal
        //	    En otro caso solo tendr� Personal (no editable)
        string sDesc, sCod;
        try
        {
            if (this.txtOrigen.Text == "A")//venimos de ADM
            {
                if (this.hdnIDPlantilla.Text == "0")
                {
                    this.cboAmbito.Items.Clear();
                    if (User.IsInRole("A"))
                    {
                        ListItem Elemento = new ListItem("Empresarial", "E");
                        this.cboAmbito.Items.Insert(0, Elemento);

                        ListItem Elemento2 = new ListItem("Departamental", "D");
                        this.cboAmbito.Items.Insert(1, Elemento2);

                        ListItem Elemento1 = new ListItem("Personal", "P");
                        this.cboAmbito.Items.Insert(2, Elemento1);
                        this.cboAmbito.SelectedValue = "P";
                    }
                    else
                    {
                        if (User.IsInRole("OT") || User.IsInRole("RN") || User.IsInRole("DN") || User.IsInRole("CN"))
                        {
                            ListItem Elemento = new ListItem("Departamental", "D");
                            this.cboAmbito.Items.Insert(0, Elemento);

                            ListItem Elemento1 = new ListItem("Personal", "P");
                            this.cboAmbito.Items.Insert(1, Elemento1);
                            this.cboAmbito.SelectedValue = "P";
                        }
                        else
                        {

                            ListItem Elemento = new ListItem("Personal", "P");
                            this.cboAmbito.Items.Insert(0, Elemento);
                            this.cboAmbito.SelectedValue = "P";
                        }
                    }
                }
                else
                {//Entramos desde el catalogo con una plantilla existente
                    if (User.IsInRole("A"))
                    {//Soy un mandamas, no tengo restricciones
                    }
                    else
                    {
                        if (User.IsInRole("OT") || User.IsInRole("RN") || User.IsInRole("DN") || User.IsInRole("CN"))
                        {//no puedo tocar las empresariales
                            sDesc = this.cboAmbito.SelectedItem.ToString();
                            sCod = this.cboAmbito.SelectedValue;
                            if (sCod == "E")
                            {
                                this.cboAmbito.Items.Clear();
                                ListItem Elemento = new ListItem(sDesc, sCod);
                                this.cboAmbito.Items.Insert(0, Elemento);
                                this.cboAmbito.SelectedValue = sCod;
                            }
                            else
                            {
                                this.cboAmbito.Items.Clear();
                                ListItem Elemento = new ListItem("Departamental", "D");
                                this.cboAmbito.Items.Insert(0, Elemento);
                                ListItem Elemento1 = new ListItem("Personal", "P");
                                this.cboAmbito.Items.Insert(1, Elemento1);
                                this.cboAmbito.SelectedValue = sCod;
                            }
                        }
                        else
                        {//Si soy un mindundi no puedo cambiar el �mbito
                            sDesc = this.cboAmbito.SelectedItem.ToString();
                            sCod = this.cboAmbito.SelectedValue;
                            this.cboAmbito.Items.Clear();
                            ListItem Elemento = new ListItem(sDesc, sCod);
                            this.cboAmbito.Items.Insert(0, Elemento);
                            this.cboAmbito.SelectedValue = sCod;
                        }
                    }
                }
            }
            else//venimos de PST
            {
                if (this.hdnIDPlantilla.Text == "0")
                {
                    this.cboAmbito.Items.Clear();
                    if (User.IsInRole("A") || User.IsInRole("OT") || User.IsInRole("RN") || User.IsInRole("DN") || User.IsInRole("CN"))
                        {
                            ListItem Elemento = new ListItem("Departamental", "D");
                            this.cboAmbito.Items.Insert(0, Elemento);

                            ListItem Elemento1 = new ListItem("Personal", "P");
                            this.cboAmbito.Items.Insert(1, Elemento1);
                            this.cboAmbito.SelectedValue = "P";
                        }
                    else
                    {

                        ListItem Elemento = new ListItem("Personal", "P");
                        this.cboAmbito.Items.Insert(0, Elemento);
                        this.cboAmbito.SelectedValue = "P";
                    }
                }
                else
                {//Entramos desde el catalogo con una plantilla existente
                    if (User.IsInRole("A") || User.IsInRole("OT") || User.IsInRole("RN") || User.IsInRole("DN") || User.IsInRole("CN"))
                    {//no puedo tocar las empresariales
                        sDesc = this.cboAmbito.SelectedItem.ToString();
                        sCod = this.cboAmbito.SelectedValue;
                        if (sCod == "E")
                        {
                            this.cboAmbito.Items.Clear();
                            ListItem Elemento = new ListItem(sDesc, sCod);
                            this.cboAmbito.Items.Insert(0, Elemento);
                            this.cboAmbito.SelectedValue = sCod;
                        }
                        else
                        {
                            this.cboAmbito.Items.Clear();
                            ListItem Elemento = new ListItem("Departamental", "D");
                            this.cboAmbito.Items.Insert(0, Elemento);
                            ListItem Elemento1 = new ListItem("Personal", "P");
                            this.cboAmbito.Items.Insert(1, Elemento1);
                            this.cboAmbito.SelectedValue = sCod;
                        }
                    }
                    else
                    {//Si soy un mindundi no puedo cambiar el �mbito
                        sDesc = this.cboAmbito.SelectedItem.ToString();
                        sCod = this.cboAmbito.SelectedValue;
                        this.cboAmbito.Items.Clear();
                        ListItem Elemento = new ListItem(sDesc, sCod);
                        this.cboAmbito.Items.Insert(0, Elemento);
                        this.cboAmbito.SelectedValue = sCod;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al establecer los �mbitos de plantilla accesibles", ex);
        }
    }
}
