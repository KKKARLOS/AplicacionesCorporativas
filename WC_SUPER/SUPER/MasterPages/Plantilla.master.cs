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
//
using EO.Web;
using SUPER.Capa_Negocio;
using System.Text.RegularExpressions;

public partial class Plantilla : System.Web.UI.MasterPage
{
    #region Atributos

    protected string _Title = "";
    protected int _nBotonera = 0;
    protected int _nPantallaAcceso = 0;
    protected int _nWidthBoton = 100;
    protected string _sErrores = "";
    protected bool _bFuncionesLocales = false;
    protected ArrayList _funcionesJavaScript = new ArrayList();
    protected bool _bEstilosLocales = false;
    protected ArrayList _ficherosCSS = new ArrayList();
    protected int _nResolucion = 1024;
    public ToolBar Botonera = new ToolBar();
    protected bool _bContienePestanas = false;
    protected string _botonesOpcionOn = "";
    protected string _botonesOpcionOff = "";
    //Por defecto indicamos que es una pantalla de Procción/Comercialización así sólo tendremos que indicar el módulo 
    //en las pantallas de CVT
    protected string _modulo = "PC";

    #endregion

    #region Propiedades públicas
    
    public string TituloPagina
    {
        get { return this._Title; }
        set { this._Title = value; }
    }
    public int nBotonera
    {
        get { return this._nBotonera; }
        set { this._nBotonera = value; }
    }
    public int nPantallaAcceso
    {
        get { return this._nPantallaAcceso; }
        set { this._nPantallaAcceso = value; }
    }
    public int nWidthBoton
    {
        get { return this._nWidthBoton; }
        set { this._nWidthBoton = value; }
    }
    public string sErrores
    {
        get { return _sErrores; }
        set { _sErrores = value; }
    }
    public bool bFuncionesLocales
    {
        get { return _bFuncionesLocales; }
        set { _bFuncionesLocales = value; }
    }
    public ArrayList FuncionesJavaScript
    {
        get { return this._funcionesJavaScript; }
        set { this._funcionesJavaScript = value; }
    }
    public bool bEstilosLocales
    {
        get { return _bEstilosLocales; }
        set { _bEstilosLocales = value; }
    }
    public ArrayList FicherosCSS
    {
        get { return this._ficherosCSS; }
        set { this._ficherosCSS = value; }
    }
    public int nResolucion
    {
        get { return this._nResolucion; }
        set { this._nResolucion = value; }
    }
    public bool bContienePestanas
    {
        get { return _bContienePestanas; }
        set { _bContienePestanas = value; }
    }
    public string sbotonesOpcionOn
    {
        get { return _botonesOpcionOn; }
        set { _botonesOpcionOn = value; }
    }
    public string sbotonesOpcionOff
    {
        get { return _botonesOpcionOff; }
        set { _botonesOpcionOff = value; }
    }
    public string Modulo
    {
        get { return _modulo; }
        set { _modulo = value; }
    }

    #endregion

    public Plantilla()
    {
    }

    //Put this piece of code in your Master page code file:
    // Adding this override so that the asp:Menu control renders properly in Safari and Chrome
    protected override void AddedControl(System.Web.UI.Control control, int index)
    {
        if (!Page.IsCallback)
        {
            if (Request.ServerVariables["http_user_agent"].IndexOf("Safari", StringComparison.CurrentCultureIgnoreCase) != -1)
            {
                if (this.Page.ClientTarget != "uplevel")
                    this.Page.ClientTarget = "uplevel";
            }
            base.AddedControl(control, index);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Session["VERSIONAPP"] = "old";
            if (Session["IDRED"] == null)
            {
                try
                {
                    Response.Redirect("~/SesionCaducada.aspx", true);
                }
                catch (System.Threading.ThreadAbortException) { }
            }
            Response.CacheControl = "no-cache";// HttpCacheability.NoCache;            

            //Estas llamadas a los ficheros javascript deben ir aquí para que se "pinten" antes de
            //las funciones locales y el fichero init válido sea el de las funciones locales.
            ///
            /// El poner el siguiente control en la posicion cero se hace porque la metaetiqueta referente
            /// a la compatibilidad con IE8 debe estar antes que cualquier .css, ya que en caso contrario
            /// Internet Explorer no le hace caso y las páginas pueden verse con otro Modo de Compatibilidad
            /// diferente a IE8 en algunos navegadores Internet Explorer.
            /// 
            string sTicks = DateTime.Now.Ticks.ToString(); //Andoni 07/01/2015: Para evitar que Chrome cachee los archivos, le pongo un valor diferente al parámetro, de forma que el navegador no tire de la versión cacheada (al tener una url diferente).
            this.HeaderMaster.Controls.AddAt(0, new LiteralControl(@"
                    <title> ::: SUPER 3.0.9 ::: - " + this.TituloPagina + @"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</title>
                    <meta http-equiv='Content-Type' content='text/html; charset=iso-8859-15'/>
                    <meta http-equiv='Expires' content='0'/>
                    <meta http-equiv='Pragma'  content='no-cache'/>
                    <meta http-equiv='Cache-Control' content ='no-cache'/>
                    <meta http-equiv='X-UA-Compatible' content='IE=8'/> 
                    <link rel='icon' href='" + Session["strServer"] + @"favicon.ico'>
                    <script language='JavaScript' type='text/Javascript' src='" + Session["strServer"] + @"Javascript/Botonera.js?v=" + sTicks + @"'></script>
                    <script language='JavaScript' type='text/Javascript' src='" + Session["strServer"] + @"Javascript/jquery-1.7.1/jquery-1.7.1.min.js?v=" + sTicks + @"'></script>
                    <script language='JavaScript' type='text/Javascript' src='" + Session["strServer"] + @"Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js?v=" + sTicks + @"'></script>
                    <script language='JavaScript' type='text/Javascript' src='" + Session["strServer"] + @"Javascript/funciones.js?v=" + sTicks + @"'></script>
                    <script language='JavaScript' type='text/Javascript' src='" + Session["strServer"] + @"Javascript/funcionesTablas.js?v=" + sTicks + @"'></script>
                    <script language='JavaScript' type='text/Javascript' src='" + Session["strServer"] + @"Javascript/master.js?v=" + sTicks + @"'></script>
			    ")
            );

            foreach (string aElemento in FuncionesJavaScript)
            {
                this.HeaderMaster.Controls.Add(new LiteralControl(@"
				    <script language='JavaScript' type='text/Javascript' src='" + Session["strServer"] + aElemento + @"'></script>")
                );
            }
            if (bFuncionesLocales)
            {
                int nPos = this.Request.Path.LastIndexOf("/");
                string strUrl = this.Request.Path.Substring(0, nPos);
                //strUrl += "/Functions/funciones.js";
                strUrl += "/Functions/funciones.js?v=" + sTicks;

                this.HeaderMaster.Controls.Add(new LiteralControl(@"
					<script language='JavaScript' type='text/Javascript' src='" + strUrl + @"'></script>")
                );
            }
            foreach (string aElemento in FicherosCSS)
            {
                this.HeaderMaster.Controls.Add(new LiteralControl(@"
					<link rel='stylesheet' href='" + Session["strServer"] + aElemento + @"' type='text/css'>")
                );
            }
            if (bEstilosLocales)
            {
                int nPos = this.Request.Path.LastIndexOf("/");
                string strUrl = this.Request.Path.Substring(0, nPos);
                strUrl += "/css/estilos.css";

                this.HeaderMaster.Controls.Add(new LiteralControl(@"
					<link rel='stylesheet' href='" + strUrl + @"' type='text/css'>")
                );
            } 

            InsertarHistoria();

            this.hdnErrores.Text = this.sErrores;

            if (this.nBotonera > 0 || this.sbotonesOpcionOn != "") CargarBotonera();
            else CargarBotoneraVacia(); //17/12/2012: Se crea la botonera vacía porque sin botonera, en Safari, el tecleo de las cajas de texto se eterniza (no tenemos la explicación de por qué)

            if (this.nPantallaAcceso > 0) Utilidades.RegistrarAcceso(this.nPantallaAcceso);

            this.lblSession.Text = "La sesión caducará en " + Session.Timeout.ToString() + " min.";

            EstablecerReconexion();
        }
    }
    /// <summary>
    /// Si estoy reconectado y cargo una nueva página tengo que verificar si tengo permiso de reconexión en el módulo al que pertenece
    /// la página. Si tengo permiso hay que indicar el nombre del reconectado, sino hay que quitarlo
    /// </summary>
    private void EstablecerReconexion()
    {
        if (Session["IDFICEPI_PC_ACTUAL"].ToString() != Session["IDFICEPI_ENTRADA"].ToString() ||
            Session["IDFICEPI_CVT_ACTUAL"].ToString() != Session["IDFICEPI_ENTRADA"].ToString())
        {//Hay reconexión
            switch (this.Modulo)
            {
                case "PC"://Producción y comercial
                    if (Session["IDFICEPI_PC_ACTUAL"].ToString() != Session["IDFICEPI_PC_RECONECTADO"].ToString())
                    {//No tengo permiso de reconexión
                        PonerNombreReconectado("");
                    }
                    break;
                case "CVT":
                    if (Session["IDFICEPI_CVT_ACTUAL"].ToString() != Session["IDFICEPI_CVT_RECONECTADO"].ToString())
                    {//No tengo permiso de reconexión
                        PonerNombreReconectado("");
                    }
                    break;
            }
        }
    }
    public void InsertarHistoria()
    {
        if (!Page.IsPostBack){
            //HistorialNavegacion.Insertar(this.Request.Path);
            string strParam = this.Request.Params.ToString();

            int intParam = strParam.IndexOf("&ASP");
            if (intParam != -1) strParam = strParam.Substring(0, intParam);
            else strParam = "";

            if (strParam.Length > 0)
                HistorialNavegacion.Insertar(this.Request.Path + "?" + strParam);
            else
                HistorialNavegacion.Insertar(this.Request.Path);

            if (HistorialNavegacion.Contador() <= 1)
            {
                //Deshabilitar el bóton de "Regresar" en caso de estar en la primera página.
                ToolBar objToolbar = (ToolBar)this.CPHB.FindControl("Botonera");
                if (objToolbar != null)
                {
                    for (int i = 0; i < objToolbar.Items.Count; i++)
                    {
                        if (objToolbar.Items[i].CommandName.ToLower() == "regresar")
                        {
                            objToolbar.Items[i].Disabled = true;
                            break;
                        }
                    }
                }
            }
        }

    }// Fin de InsertarHistoria()

    public void CargarBotonera()
    {
        CBotonera objBot = new CBotonera();
        ToolBar objToolbar = this.Botonera;
        objToolbar.ID = "Botonera";

        objBot.CargarBotoneraEO(objToolbar, this.nBotonera, this.nWidthBoton, this.sbotonesOpcionOn, this.sbotonesOpcionOff);

        Panel objPanelBarraBotones = new Panel();
        objPanelBarraBotones.ID = "BarraBotones";
        objPanelBarraBotones.BackImageUrl = "~/Images/imgBG.gif";

        objPanelBarraBotones.Controls.Add(objToolbar);
        this.CPHB.Controls.Add(objPanelBarraBotones);
    }
    public void CargarBotoneraVacia()
    {
        CBotonera objBot = new CBotonera();
        ToolBar objToolbar = this.Botonera;
        objToolbar.ID = "Botonera";

        //objBot.CargarBotoneraEO(objToolbar, this.nBotonera, this.nWidthBoton, this.sbotonesOpcionOn, this.sbotonesOpcionOff);

        Panel objPanelBarraBotones = new Panel();
        objPanelBarraBotones.ID = "BarraBotones";
        objPanelBarraBotones.Style.Add("display", "none");
        //objPanelBarraBotones.BackImageUrl = "~/Images/imgBG.gif";

        objPanelBarraBotones.Controls.Add(objToolbar);
        this.CPHB.Controls.Add(objPanelBarraBotones);
    }

    public void PonerNombreReconectado(string sNombre)
    {
        bool bPoner = false;
        if (sNombre == "" || Session["DES_EMPLEADO"].ToString() != Session["DES_EMPLEADO_ENTRADA"].ToString())
            bPoner = true;
        if (bPoner)
        {
            System.Web.UI.Control ctrl = this.Cabecera1.FindControl("lblProfReconectado");
            ((Label)ctrl).Text = sNombre;// Session["DES_EMPLEADO"].ToString();
        }
    }
}
