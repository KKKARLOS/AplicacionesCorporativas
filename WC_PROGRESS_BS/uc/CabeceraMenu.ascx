<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CabeceraMenu.ascx.cs" Inherits="uc_CabeceraMenu" %>


<!-- Bootstrap -->

<link href="<% =Session["strServer"].ToString() %>css/bootstrap.min.css" rel="stylesheet" />
<!--Icon Fonts FontAwsome-->
<link href="<% =Session["strServer"].ToString() %>css/font-awesome.min.css" rel="stylesheet" />
<link href="<% =Session["strServer"].ToString() %>css/main.css" rel="Stylesheet" />

<link href="<% =Session["strServer"].ToString() %>uc/Menu/css/sm-core-css.css" rel="stylesheet" />
<link href="<% =Session["strServer"].ToString() %>uc/Menu/css/sm-clean.css" rel="stylesheet" />


<!-- css propio de la página-->
<link href="css/StyleSheet.css" rel="stylesheet" />

<!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
<!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
<!--[if lt IE 9]>
    <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
    <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->


<style>
    .navbar-inverse .navbar-nav > .open > a, .navbar-inverse .navbar-nav > .open > a:hover, .navbar-inverse .navbar-nav > .open > a:focus {
        background-color: #428bca;
    }

</style>


<label ID="lblReconectado" style="color:#E2E0E0;font-size:0.9em; position:absolute;top:0;left:0; z-index:1111111111; font-weight:normal;" visible="false" runat="server" Text=""></label>                


<nav class="navbar navbar-inverse navbar-fixed-top" style="background-color:#428bca" role="navigation">
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
        <a href="<% =Session["strServer"].ToString() %>Capa_Presentacion/Home/Home.aspx">
            <img id="logoProgress" src="<% =Session["strServer"].ToString() %>imagenes/logoProgress2.png" class="img-responsive" />
        </a>
    </div>

    <!-- Agrupar los enlaces de navegación, los formularios y cualquier
       otro elemento que se pueda ocultar al minimizar la barra -->
    <div id="divmenu" class="collapse navbar-collapse navbar-ex1-collapse">
        <ul class="nav navbar-nav fixed-menu" id="ulPrincipal" runat="server"></ul>

        <ul class="nav navbar-nav navbar-right">
            <li>
                <img id="logoIbermatica" src="<% =Session["strServer"].ToString() %>imagenes/Logo_Ibermatica.png" class="img-responsive" />
            </li>
        </ul>

    </div>



</nav>


<nav class="fixedBar">
    <div class="pull-left">
        <asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" />
        <asp:SiteMapPath ID="SiteMap1" runat="server"></asp:SiteMapPath>
    </div>
    
    
    <div class="pull-right" style="margin-right:10px";>
        <label ID="lblSession" style="font-size:0.9em; font-weight:normal;" runat="server" Text=""></label>                
    </div>

</nav>

<br />

<script src="<% =Session["strServer"].ToString() %>uc/Menu/js/jquery.smartmenus.js"></script>


<script>
    var strServer = "<%=Session["strServer"].ToString() %>";
    var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos. 

</script>

<iframe name="iFrmSession" id="iFrmSession" src="<%=Session["strServer"].ToString() %>uc/ControlSesion.aspx" style="visibility:hidden;width:1px;height:1px"></iframe>
<script>
   
    $("#CabeceraMenu_ulPrincipal li[data-name='Guía pantalla actual (F10)'] a span").on("click", function () {        
        obtenerDocumentoAyuda($(document).find("body").attr("data-codigopantalla"));
    })


    $("#CabeceraMenu_ulPrincipal li[data-name='Guía completa'] a").on("click", function () {        
        $(this).attr("target", "_blank");
    })

    $("body").on("keydown", function () {        
        if (event.keyCode == 121) {
            obtenerDocumentoAyuda($(document).find("body").attr("data-codigopantalla"));
        }
    })
    
    //Controlamos la posición de los submenus dependiendo del espacio disponible en pantalla.
    $(".dropdown-submenu").hover(function(){                        
        var posicionOcupado = $(this).offset().left + $(this).width();
        var espacioDisponible = $(window).width() - posicionOcupado - 30;//Le restamos 30 píxeles para tener un poco de margen

        if ($(this).find(">ul").width() >= espacioDisponible){                
            $(this).find(">ul").css("right", "auto").css("left", -$(this).find(">ul").width());
        }       
    })
 
    function actualizarSession() {
        try {
            if (window.frames["iFrmSession"].nIDTimeMin==undefined) return;
            if (window.frames["iFrmSession"].nIDTimeMin==null) return;
            //Método al que solo se accede desde la ventana principal
            window.frames["iFrmSession"].clearTimeout(window.frames["iFrmSession"].nIDTimeMin);
            window.frames["iFrmSession"].clearTimeout(window.frames["iFrmSession"].nIDTimeSeg);
            window.frames["iFrmSession"].nSession = intSession + 1;
            window.frames["iFrmSession"].restaSession();
            window.top.CerrarMensajeSession();
        } catch (e) {
            alertNew("danger", "Error al actualizar la caducidad de sesión");
        }
    }
    function ReiniciarSession() {
        try {
            document.getElementById("iFrmSession").src = strServer + "uc/ControlSesion.aspx";
            document.getElementById("CabeceraMenu_lblSession").innerText = "La sesión caducará en " + intSession + " min.";
        } catch (e) {
            alertNew("danger", "Error al reiniciar la sesión.");
        }
    }

</script>