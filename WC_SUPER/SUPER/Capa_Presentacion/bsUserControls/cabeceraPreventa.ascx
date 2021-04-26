<%@ Control Language="C#" AutoEventWireup="true" CodeFile="cabeceraPreventa.ascx.cs" Inherits="Capa_Presentacion_bsUserControls_cabeceraPreventa" %>

<style>

.row-cabecera {
    margin-right: 0px;
    margin-left: 0px;
    margin-bottom:7px;
}

.menu-fijo {
    padding: 0;
    position: fixed;   
    z-index: 1010;
    width: 100%;
}

.fixedBar {
    background-color: #f2f2f2;
    width: 100%;
    position: fixed;
    z-index: 1000;    
    padding-left: 10px;
    font-size:0.85em;
}

#navSesion {top:60px;}

.navbar-inverse .navbar-nav > li > a {
    color: #222 !important;
}

.navbar-inverse .navbar-nav > .open > a,
.navbar-inverse .navbar-nav > .open > a:hover,
.navbar-inverse .navbar-nav > .open > a:focus {
    background-color: #d9d5d5;
    color: #222;
}

.navbar-nav > li > a {
    padding-top: 10px;
    padding-bottom: 10px;
    line-height: 20px;
}


#textoSuper {
    margin-top: 20px;
    margin-left: 20px;
    color: #fff;
    font-size: 22px;    
}

#logoIbermatica {
    margin-right:20px;
    margin-top:10px;
}


</style>
<nav class="navbar navbar-inverse navbar-fixed-top"  role="navigation">
    <div id="cabecera" class="row row-cabecera">

        <div class="navbar-header">
            <a class="pull-left">
                <img alt="Preventa" id="logoPreventa" src="<%=Session["strServer"].ToString() %>Capa_Presentacion/bsImages/imgLogoPreventa.png" class="img-responsive">
            </a>
            
        </div>

        <div class="hidden-xs nav navbar-nav navbar-right collapse navbar-collapse navbar-ex1-collapse">
            <img id="logoIbermatica" alt="Ibermática" src="<%=Session["strServer"].ToString() %>Capa_Presentacion/bsImages/Logo_Ibermatica.png" class="img-responsive">            
        </div>


        <div class="pull-left hidden-xs" style="margin-left:10%">            
            <br />            
            <span style="color:white" runat="server" id="textoUsuario"></span>                
        </div>

    </div>

</nav>

<!-- tiempo de sesión-->
<nav id="navSesion" class="fixedBar hidden-xs">
    <div id="barraDerecha" class="pull-right" style="margin-right:10px;" aria-hidden="true">
        <label ID="lblSession" style="margin-right:10px;"></label>
        <%--<span id="spnUsuario" runat="server"></span>     --%>
    </div> 

</nav>


<br /><br /><br class="hidden-xs" /><br class="hidden-xs" />








