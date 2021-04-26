<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Administracion_Reconexion_Default" %>

<%@ Register Src="~/uc/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="cb1" %>
<%@ Register Src="~/uc/PieMenu.ascx" TagName="PieMenu" TagPrefix="cb1" %>
<%@ Register Src="~/uc/uc_Session.ascx" TagName="mensajeSession" TagPrefix="uc3" %>
<%@ Register Src="~/uc/HeaderMeta.ascx" TagPrefix="cb1" TagName="HeaderMeta" %>


<!DOCTYPE html>

<html>
<head>
    <cb1:HeaderMeta runat="server" ID="HeaderMeta" />
    <title>Reconexión</title>
    <link href="../../../js/plugins/tablesorter/css/style.css" rel="stylesheet" type="text/css" />

    <script>
        var strServer = "<% =Session["strServer"].ToString() %>";       
    </script>

 
</head>
<body>
    <uc3:mensajeSession ID="MensajeSession" runat="server" />
    <cb1:CabeceraMenu runat="server" ID="CabeceraMenu"></cb1:CabeceraMenu>

    <br class="hidden-xs" />
    <br class="hidden-xs" />

    <div class="container">

        <div class="row">
            <div class="col-xs-10 col-xs-offset-1 col-lg-8 col-lg-offset-2">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">Reconexión</h3>
                    </div>
                    <div class="panel-body">
                           <div id="txtBusqueda" class="">
                        <div class="pull-left">
                             <label for="lblApellido1">Apellido 1º</label><br />
                                <input id="inputApellido1" type="text" />
                        </div>
                        <div class="pull-left" style="margin-left: 5%">
                             <label for="lblApellido2">Apellido 2º</label><br />
                                <input id="inputApellido2" type="text" />
                        </div>
                        <div class="pull-left" style="margin-left: 5%">
                            <label for="lblNombre">Nombre</label><br />
                            <input id="txtNombre" type="text" />
                        </div>
                        <div class="pull-left">
                            <button id="btnObtener" class="btn btn-primary btn-xs hide">Obtener</button>
                        </div>
                    </div>
                    
                    

                    <div class="row">
                        <div class="col-xs-12" style="margin-top:10px">
                            <ul class="list-group" runat="server" id="lisEvaluadores">
                            </ul>
                        </div>
                    </div>

                        <div class="row">
                            <div class="col-xs-12">

                                <b>
                                    <button id="btnAceptar" class="btn btn-primary pull-right">Aceptar</button></b>
                            </div>
                        </div>

                </div>

                    
                </div>
            </div>
        </div>



    </div>


</body>
</html>
<cb1:PieMenu runat="server" ID="PieMenu"></cb1:PieMenu>
<script type="text/javascript" src="../../../js/plugins/tablesorter/jquery.tablesorter.min.js"></script>






