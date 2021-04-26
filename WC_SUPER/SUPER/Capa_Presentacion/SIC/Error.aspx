<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="Capa_Presentacion_SIC_Error" %>

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
    
    <!-- Bootstrap -->
    <link href="../../css/bootstrap.min.css" rel="stylesheet" />

    <!--Icon Fonts FontAwsome-->
    <link href="../../css/font-awesome.min.css" rel="stylesheet" />
    <link href="../../Capa_Presentacion/bsUserControls/Menu/css/StyleSheet.css" rel="stylesheet" />
    <link href="../../css/SUPER.css" rel="Stylesheet" />

    <title>::: PREVENTA :::</title>


</head>

<hr />

<body>
    <nav class="navbar navbar-inverse navbar-fixed-top" role="navigation">
        <div id="cabecera" class="row row-cabecera">

            <div class="navbar-header">
                <a class="pull-left">
                    <img alt="Preventa" id="logoPreventa" src="../../Capa_Presentacion/bsImages/imgLogoPreventa.png" class="img-responsive">
                </a>

            </div>

            <div class="hidden-xs nav navbar-nav navbar-right collapse navbar-collapse navbar-ex1-collapse">
                <img id="logoIbermatica" alt="Ibermática" src="../../Capa_Presentacion/bsImages/Logo_Ibermatica.png" class="img-responsive">
            </div>
       </div>

    </nav>
    <form id="form1" runat="server">
        <div class="container">
            <div class="well">
                <i class="fa fa-clock-o fa-5x pull-right text-danger"></i>
                <h3>
                    <asp:Literal ID="ltrH3" runat="server"></asp:Literal></h3>
                <h4>
                    <asp:Literal ID="ltrH4" runat="server"></asp:Literal></h4>
            </div>
        </div>
    </form>
</body>
</html>
