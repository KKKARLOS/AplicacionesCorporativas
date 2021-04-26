<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Menu.ascx.cs" Inherits="Capa_Presentacion_bsUserControls_Menu_Menu" %>

<link href="<% =Session["strServer"].ToString() %>Capa_Presentacion/bsUserControls/Menu/css/ionicons.min.css" rel="stylesheet" />
<link href="<% =Session["strServer"].ToString() %>Capa_Presentacion/bsUserControls/Menu/css/responsivemultimenu.css" rel="stylesheet" />

<link href="<% =Session["strServer"].ToString() %>Capa_Presentacion/bsUserControls/Menu/css/sm-core-css.css" rel="stylesheet" />
<link href="<% =Session["strServer"].ToString() %>Capa_Presentacion/bsUserControls/Menu/css/sm-clean.css" rel="stylesheet" />
<link href="<% =Session["strServer"].ToString() %>Capa_Presentacion/bsUserControls/Menu/css/StyleSheet.css" rel="stylesheet" />

<script>
    var EsForaneo = <%= (Session["ua"] != null)? "1":"0" %>;    
</script>
<!--Menu-->
<nav class="navbar navbar-inverse navbar-fixed-top" role="navigation">
    <div id="cabecera" class="row row-cabecera">
    <!-- El logotipo y el icono que despliega el menú se agrupan
    para mostrarlos mejor en los dispositivos móviles -->
        <div class="navbar-header">
            <button type="button" class="navbar-toggle" data-toggle="collapse"
                data-target=".navbar-ex1-collapse">
                <span class="sr-only">Desplegar navegación</span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
            </button>

            <a class="pull-left" href="<%=Session["strServer"].ToString() %>Capa_Presentacion/bsInicio/Default.aspx">
                <img alt="SUPER" id="logoSuper" src="<%=Session["strServer"].ToString() %>Capa_Presentacion/bsImages/imgLogoTrans.png" class="img-responsive">
            </a>

            <img id="imgDiamante" runat="server" class="pull-left"  />
            <span id="textoSuper" class="pull-left">el GESTOR de negocio</span>     
                                                                                           
        </div>    
        <div id="divUsuarioConectado" class="pull-left hidden-xs hidden-sm">            
            <br />
            <span runat="server" id="textoUsuarioReconectado"></span>  <br />  
            <span runat="server" id="textoUsuario"></span>                
        </div>
        
                                               
        <div class="hidden-xs nav navbar-nav navbar-right collapse navbar-collapse navbar-ex1-collapse">
            
            <img id="logoIbermatica" alt="Ibermática" src="<%=Session["strServer"].ToString() %>Capa_Presentacion/bsImages/Logo_Ibermatica.png" class="img-responsive">
            <%--<span id="textoFecha"><%=System.DateTime.Now.ToShortDateString() %></span>--%>
        </div>
    </div> 

</nav>

<div class="collapse navbar-collapse navbar-ex1-collapse menu-fijo">
    <nav id="main-nav" role="navigation">
        <ul id="ulPrincipal" tabindex="0" runat="server" class="sm sm-clean" role="presentation"></ul>
    </nav>
</div>

<!-- Migas de pan -->
<nav id="navBreadcrumbs" class="fixedBar hidden-xs" runat="server">
    <div class="pull-left ">
        <asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" />
        <asp:SiteMapPath ID="SiteMap1" runat="server"></asp:SiteMapPath>
    </div>
        
    <div id="barraDerecha" class="pull-right" aria-hidden="true">
        <label role="link" id="guia" runat="server" visible="false" class="guia txtLinkU">Guia de ayuda (F10)</label>
        <label ID="lblSession"></label>
        <%--<span id="spnUsuario" runat="server"></span>     --%>
    </div> 

</nav>

<nav id="navGuia" class="fixedBar visible-xs" runat="server">   
    <div id="barraDerechaxs" class="pull-right" aria-hidden="true">
        <label role="link" id="guiaxs" runat="server" visible="false" class="guia txtLinkU">Guia de ayuda</label>
    </div> 

</nav>

<br /><br /><br class="hidden-xs" /><br class="hidden-xs" /><br class="hidden-xs" />

<!--FIN Menu-->
<script src="<% =Session["strServer"].ToString() %>Capa_Presentacion/bsUserControls/Menu/js/jquery.smartmenus.js"></script>

<script>
    $(document).ready(function () {

        //Menu de navegación
        $('#Menu_ulPrincipal').smartmenus({
            mainMenuSubOffsetX: -1,
            mainMenuSubOffsetY: 4,
            subMenusSubOffsetX: 6,
            subMenusSubOffsetY: -6
        });

        //borramos aquellos nodos vacíos (así evitamos que la gente vea el módulo PGE y no pueda acceder)
        $("#Menu_ulPrincipal > li > a").not(".has-submenu").remove();

        //Quitamos las flechas del nivel principal en resoluciones mayores a 768
        if ($(window).width() > 768)
            $("#Menu_ulPrincipal > li > a > span:nth-child(1)").remove();

        //Para acceder a Gasvi
        $("#Menu_ulPrincipal li[id='goGasvi']").on("click", function(){
            try {
                if (EsForaneo == 1) {                    
                    IB.bsalert.fixedAlert("warning", "Error", "Acceso no autorizado.");
                    return;
                }
                var strUrl = "";
                if (document.location.protocol == "http:") strUrl = "http://gasvi.intranet.ibermatica/default.aspx";
                else strUrl = "https://extranet.ibermatica.com/gasvi/default.aspx";

                window.open(strUrl, "", "resizable=no,status=no,scrollbars=no,menubar=no,top=" + eval(screen.availHeight / 2 - 384) + ",left=" + eval(screen.availWidth / 2 - 512) + ",width=1014px,height=709px");
                var ventana = window.self;
                ventana.close();
            } catch (e) {
                IB.bserror.mostrarErrorAplicacion("Error", "Error al conectar con GASVI.");
                return false
            }
        })
        
    })
</script>
   


