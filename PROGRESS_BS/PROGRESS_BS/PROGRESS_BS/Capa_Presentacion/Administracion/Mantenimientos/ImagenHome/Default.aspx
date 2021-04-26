<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Administracion_Mantenimientos_ImagenHome_Default" %>

<%@ Register Src="~/uc/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="cb1" %>
<%@ Register Src="~/uc/PieMenu.ascx" TagName="PieMenu" TagPrefix="cb1" %>
<%@ Register Src="~/uc/uc_Session.ascx" TagName="mensajeSession" TagPrefix="uc3" %>
<%@ Register Src="~/uc/HeaderMeta.ascx" TagPrefix="cb1" TagName="HeaderMeta" %>


<!DOCTYPE html>
<html>
<head>
    <cb1:HeaderMeta runat="server" ID="HeaderMeta" />
    <title>Imagen Home</title>
    <link href="<% =Session["strServer"].ToString() %>js/plugins/fileinput/fileinput.min.css" rel="Stylesheet" />
    <style>
        .containerNoticias {
            background-color: white;
            width: 750px;
            height: 435px;
        }

    </style>
</head>

<body>
    <uc3:mensajeSession ID="MensajeSession" runat="server" />
    <cb1:CabeceraMenu runat="server" ID="CabeceraMenu"></cb1:CabeceraMenu>

    <br />
    <br class="hidden-xs" />
    <br class="hidden-xs" />

    <div class="container">
        <!--
        <div class="row">
            <div class="col-xs-8 col-md-4" style="border:1px solid">uno</div>
            <div class="col-xs-1 col-md-4" style="border:1px solid">dos</div>
            <div class="col-xs-3 col-md-4" style="border:1px solid">tres</div>
        </div>
-->
        <div class="row">
            <center><div>
                 <div class="row containerNoticias">
                        <img id="imghome" src="" runat="server" class="containerNoticias"/>
                 </div>
                
            </div>
                </center>

        </div>

        <br />
        <div class="row">
            <center>
            <div>
                <button id="btnSelectImage" type="button" class="btn btn-primary" style="margin-right:5px">Seleccionar nueva imagen</button>
                <button id="btnGrabar" type="button" class="btn btn-primary" style="margin-left:5px">Grabar la imagen para la Home</button>
                
            </div>
                </center>
        </div>
    </div>

    <div class="modal fade" id="modal-selectimage">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <h4 class="modal-title">Seleccionar imagen</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-xs-10 col-xs-offset-1">
                            <input id="imageupload" name="imageupload" type="file" class="file-loading" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <span class="pull-left">Se recomienda una imagen de 750 x 435 píxeles </span>
                    <button id="btnCancelarModal" type="button" style="margin-left: 7px" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>


<cb1:PieMenu runat="server" ID="PieMenu"></cb1:PieMenu>
<script src="<% =Session["strServer"].ToString() %>js/plugins/fileinput/fileinput.min.js"></script>
<script src="<% =Session["strServer"].ToString() %>js/plugins/fileinput/fileinput_locale_es.js"></script>

    </body>
</html>


