<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bsSesionCaducada.aspx.cs" Inherits="bsSesionCaducada" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head>
    <!-- Etiquetas Meta -->
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta http-equiv='Expires' content='0' />
    <meta http-equiv='Pragma' content='no-cache' />
    <meta http-equiv='Cache-Control' content='no-cache' />
    <meta name="apple-mobile-web-app-capable" content="yes" />

    <!-- Bootstrap -->
    <link href="./css/bootstrap.min.css" rel="stylesheet" />

    <!--Icon Fonts FontAwsome-->
    <link href="./css/font-awesome.min.css" rel="stylesheet" />
    <link href="./Capa_Presentacion/bsUserControls/Menu/css/StyleSheet.css" rel="stylesheet" />
    <link href="./css/SUPER.css" rel="Stylesheet" />

    <title>Sesión caducada</title>
</head>

<hr />

<body>
    <div class="container">
        <nav class="navbar navbar-inverse navbar-fixed-top" role="navigation">
            <div id="cabecera" class="row row-cabecera">

                <div class="navbar-header">
                    <a class="pull-left" href="Default.aspx">
                        <img alt="SUPER" id="logoSuper" src="./Capa_Presentacion/bsImages/imgLogoTrans.png" class="img-responsive" />
                    </a>

                    <img id="imgDiamante" class="pull-left" src="./Capa_Presentacion/bsImages/imgDiamante.gif" />
                    <span id="textoSuper" class="pull-left">el GESTOR de negocio</span>

                </div>
                <div class="hidden-xs nav navbar-nav navbar-right collapse navbar-collapse navbar-ex1-collapse">
                    <img id="logoIbermatica" alt="Ibermática" src="./Capa_Presentacion/bsImages/Logo_Ibermatica.png" class="img-responsive" />
                </div>
            </div>

        </nav>
    </div>
    <form id="form1" runat="server">
        <div class="container">
            <div class="well">
                <i class="fa fa-clock-o fa-5x pull-right text-danger"></i>
                <h3>Tu sesión ha caducado. </h3>
                <h4>Haz click <a href="Default.aspx">aquí</a> para volver a conectarte.</h4>
            </div>
        </div>
    </form>
</body>
</html>
