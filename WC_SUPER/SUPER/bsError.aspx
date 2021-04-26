<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bsError.aspx.cs" Inherits="bsError" %>

<!DOCTYPE html>

<html>
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
    <link href="Capa_Presentacion/bsUserControls/Menu/css/StyleSheet.css" rel="stylesheet" />
    <link href="./css/SUPER.css" rel="Stylesheet" />

    <title>Error</title>
</head>

<body>
    <form id="form1" runat="server">
     <div class="container">

           <nav class="navbar navbar-inverse navbar-fixed-top" role="navigation">
            <div id="cabecera" class="row row-cabecera">

                <div class="navbar-header">
                    <a class="pull-left" href="Default.aspx">
                        <img alt="SUPER" id="logoSuper" src="../Capa_Presentacion/bsImages/imgLogoTrans.png" class="img-responsive" />
                    </a>

                    <img id="imgDiamante" class="pull-left" src="../Capa_Presentacion/bsImages/imgDiamante.gif" />
                    <span id="textoSuper" class="pull-left">el GESTOR de de negocio</span>

                </div>
                <div class="hidden-xs nav navbar-nav navbar-right collapse navbar-collapse navbar-ex1-collapse">
                    <img id="logoIbermatica" alt="Ibermática" src="../Capa_Presentacion/bsImages/Logo_Ibermatica.png" class="img-responsive" />
                </div>
            </div>

        </nav>
        <hr />

        <div class="row well">
            <!--Error de aplicación-->
            <div id="Error" runat="server">
                <i class="fa fa-exclamation-triangle fa-5x pull-right text-danger"></i>
                <h3>Se ha producido un error en la aplicación.</h3>

                <br />
                <p>
                    Inténtelo de nuevo en unos momentos y si el problema persiste póngase en contacto con el CAU.
                </p>
            </div>           
        </div>
    </div>
    </form>
</body>
</html>
