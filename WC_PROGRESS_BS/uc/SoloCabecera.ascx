<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SoloCabecera.ascx.cs" Inherits="uc_SoloCabecera" %>
<!-- Bootstrap -->
<link href="css/bootstrap.min.css" rel="stylesheet">
<!--Icon Fonts FontAwsome-->
<link href="css/main.css" rel="Stylesheet" />



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
        <img id="logoProgress" src="imagenes/logoProgress2.png" class="img-responsive" />
    </div>
    <!-- Agrupar los enlaces de navegación, los formularios y cualquier
       otro elemento que se pueda ocultar al minimizar la barra -->
    <div id="divmenu" class="collapse navbar-collapse navbar-ex1-collapse">      
        <ul class="nav navbar-nav navbar-right">
            <li>
                <img id="logoIbermatica" src="imagenes/Logo_Ibermatica.png" class="img-responsive" />
            </li>
        </ul>
    </div>
</nav>
<br />

