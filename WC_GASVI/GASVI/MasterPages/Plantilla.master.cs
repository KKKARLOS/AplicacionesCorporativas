using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using GASVI.BLL;
using EO.Web;


using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

public partial class Plantilla : System.Web.UI.MasterPage
{
    #region Atributos

    protected string _Title = "";
    protected int _nBotonera = 0;
    protected string _sErrores = "";
    protected bool _bFuncionesLocales = false;
    protected ArrayList _funcionesJavaScript = new ArrayList();
    protected ArrayList _ficherosCSS = new ArrayList();
    protected int _nResolucion = 1024;
    public ToolBar Botonera = new ToolBar();
    protected string _botonesOpcionOn = "";
    protected string _botonesOpcionOff = "";
    protected int _nWidthBoton = 100;

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

    public int nWidthBoton
    {
        get { return this._nWidthBoton; }
        set { this._nWidthBoton = value; }
    }

    #endregion

    public Plantilla()
    {
    }

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
            if (Session["GVT_IDRED"] == null)
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
            this.HeaderMaster.Controls.AddAt(0, new LiteralControl(@"
                    <title> ::: GASVI 2.0 ::: - " + this.TituloPagina + @"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</title>
                    <meta http-equiv='Content-Type' content='text/html; charset=iso-8859-15'/>
                    <meta http-equiv='Expires' content='0'>
                    <meta http-equiv='Pragma' content='no-cache'>
                    <meta http-equiv='Cache-Control' content ='no-cache'>
                    <meta http-equiv='X-UA-Compatible' content='IE=edge'/> 
                    <script language='JavaScript' type='text/Javascript' src='" + Session["GVT_strServer"] + @"Javascript/jquery-1.7.1/jquery-1.7.1.min.js'></script>
                    <script language='JavaScript' type='text/Javascript' src='" + Session["GVT_strServer"] + @"Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js'></script>
                    <script language='JavaScript' type='text/Javascript' src='" + Session["GVT_strServer"] + @"Javascript/funciones.js'></script>
                    <script language='JavaScript' type='text/Javascript' src='" + Session["GVT_strServer"] + @"Javascript/funcionesTablas.js'></script>
                    <script language='JavaScript' type='text/Javascript' src='" + Session["GVT_strServer"] + @"Javascript/master.js'></script>
                    <script language='JavaScript' type='text/Javascript' src='" + Session["GVT_strServer"] + @"Javascript/Botonera.js'></script>
			    ")
            );

            foreach (string aElemento in FuncionesJavaScript)
            {
                this.HeaderMaster.Controls.Add(new LiteralControl(@"
				    <script language='JavaScript' type='text/Javascript' src='" + Session["GVT_strServer"] + aElemento + @"'></script>")
                );
            }
            if (bFuncionesLocales)
            {
                //string sTicks = DateTime.Now.Ticks.ToString();
                int nPos = this.Request.Path.LastIndexOf("/");
                string strUrl = this.Request.Path.Substring(0, nPos);
                //strUrl += "/Functions/funciones.js?v=" + sTicks;
                strUrl += "/Functions/funciones.js?v=3";

                this.HeaderMaster.Controls.Add(new LiteralControl(@"
					<script language='JavaScript' type='text/Javascript' src='" + strUrl + @"'></script>")
                );
            }
            foreach (string aElemento in FicherosCSS)
            {
                this.HeaderMaster.Controls.Add(new LiteralControl(@"
					<link rel='stylesheet' href='" + Session["GVT_strServer"] + aElemento + @"' type='text/css' />")
                );
            }
            if (this.nBotonera > 0 || this.sbotonesOpcionOn != "")
            {
                this.HeaderMaster.Controls.Add(new LiteralControl(@"
                    <?import namespace='TBNS' implementation='" + Session["GVT_strServer"].ToString() + @"webctrl_client/1_0/toolbar.htc'>")
                );
            }
           
            InsertarHistoria();

            this.hdnErrores.Text = this.sErrores;

            if (this.nBotonera > 0 || this.sbotonesOpcionOn != "") CargarBotonera();

            this.lblSession.Text = "La sesión caducará en " + Session.Timeout.ToString() + " min.";
        }
    }

    public void InsertarHistoria()
    {
        if (!Page.IsPostBack)
        {
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



}
