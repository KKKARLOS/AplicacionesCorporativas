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
//using Microsoft.Web.UI.WebControls;
using EO.Web;
using CR2I.Capa_Negocio;
using System.Text.RegularExpressions;

public partial class Plantilla : System.Web.UI.MasterPage
{
    #region Atributos

    protected string _Title = "";
    protected int _nBotonera = 0;
    protected int _nWidthBoton = 100;
    protected string _sErrores = "";
    protected bool _bFuncionesLocales = false;
    protected ArrayList _funcionesJavaScript = new ArrayList();
    protected ArrayList _ficherosCSS = new ArrayList();
    protected int _nResolucion = 1024;
    public ToolBar Botonera = new ToolBar();
    protected bool _bContienePestanas = false;
    protected string _botonesOpcionOn = "";
    protected string _botonesOpcionOff = "";
    protected int comportamiento = 0;
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
    public int Comportamiento
    {
        get { return this.comportamiento; }
        set { this.comportamiento = value; }
    }

    #endregion

    public Plantilla()
    {
    }

    //protected void Page_PreInit(object sender, EventArgs e)
    //{
    //    // This is necessary because Safari and Chrome browsers don't display the Menu control correctly.
    //    // All webpages displaying an ASP.NET menu control must inherit this class.
    //    if (Request.ServerVariables["http_user_agent"].IndexOf("Safari", StringComparison.CurrentCultureIgnoreCase) != -1)
    //        Page.ClientTarget = "uplevel";
    //} 

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            if (Session["CR2I_IDRED"] == null)
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
                    <title> ::: CR2I ::: - " + this.TituloPagina + @"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</title>
                    <meta http-equiv='X-UA-Compatible' content='IE=8'/> 
                    <meta http-equiv='Expires' content='0'/>
                    <meta http-equiv='Pragma'  content='no-cache'/>
                    <meta http-equiv='Cache-Control' content ='no-cache'/>
                    <meta http-equiv='Content-Type' content='text/html; charset=iso-8859-15'/>
                    <script language='JavaScript' type='text/Javascript' src='" + Session["strServer"] + @"Javascript/Botonera.js'></script>
                    <script language='JavaScript' type='text/Javascript' src='" + Session["strServer"] + @"Javascript/jquery-1.7.1/jquery-1.7.1.min.js'></script>
                    <script language='JavaScript' type='text/Javascript' src='" + Session["strServer"] + @"Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js'></script>
                    <script language='JavaScript' type='text/Javascript' src='" + Session["strServer"] + @"Javascript/funciones.js'></script>
                    <script language='JavaScript' type='text/Javascript' src='" + Session["strServer"] + @"Javascript/funcionesTablas.js'></script>
                    <script language='JavaScript' type='text/Javascript' src='" + Session["strServer"] + @"Javascript/master.js'></script>
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
                strUrl += "/Functions/funciones.js";

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
//            if (this.nBotonera > 0 || this.sbotonesOpcionOn != "")
//            {
//                this.HeaderMaster.Controls.Add(new LiteralControl(@"
//                    <?import namespace='TBNS' implementation='" + Session["strServer"].ToString() + @"webctrl_client/1_0/toolbar.htc'>")
//                );
//            }
//            if (bContienePestanas)
//            {
//                this.HeaderMaster.Controls.Add(new LiteralControl(@"
//                    <?import namespace='TSNS' implementation='"+ Session["strServer"].ToString() + @"webctrl_client/1_0/tabstrip.htc'>
//                    <?import namespace='MPNS' implementation='"+ Session["strServer"].ToString() + @"webctrl_client/1_0/multipage.htc'>")
//                );
//            }
//            if (bContieneTreeview)
//            {
//                this.HeaderMaster.Controls.Add(new LiteralControl(@"
//                    <?import namespace='TVNS' implementation='" + Session["strServer"].ToString() + @"webctrl_client/1_0/treeview.htc'>")
//                );
//            }
            this.hdnErrores.Text = this.sErrores;
            if (this.nBotonera == 0)
                this.nBotonera = this.comportamiento;
            if (this.nBotonera > 0 || this.sbotonesOpcionOn != "") CargarBotonera();
        }
    }

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
