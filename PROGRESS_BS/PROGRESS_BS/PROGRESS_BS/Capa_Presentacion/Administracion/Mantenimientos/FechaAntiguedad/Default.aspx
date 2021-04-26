<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Administracion_Mantenimientos_FechaAntiguedad_Default" %>

<%@ Register Src="~/uc/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="cb1" %>
<%@ Register Src="~/uc/PieMenu.ascx" TagName="PieMenu" TagPrefix="cb1" %>
<%@ Register Src="~/uc/uc_Session.ascx" TagName="mensajeSession" TagPrefix="uc3" %>
<%@ Register Src="~/uc/HeaderMeta.ascx" TagPrefix="cb1" TagName="HeaderMeta" %>


<!DOCTYPE html>
<html>
<head>
    <cb1:HeaderMeta runat="server" ID="HeaderMeta" />
    <title>Imagen Home</title>
    <link rel="Stylesheet" href="../../../../js/plugins/jQueryUI/jquery-ui.min.css" />
    <style>

        .container {
            width: 970px !important;
        }
        .ui-datepicker-calendar {
            display: block;
        }

        .calendar-off table.ui-datepicker-calendar {
            display: none !important;
        }

        .ui-widget-header {
            border: 1px solid rgb(80,132,159);
            background: rgb(80,132,159);
            color: #666;
        }

        #txtFantiguedad {
            width: 100px;
            padding-left:10px;
            cursor:pointer;
        }

        @media (min-width: 1200px) {
            .container {
                width: 1170px !important;
            }
        }
    </style>
    <script>
        <%=defectoAntiguedad%>         
    </script>
</head>

<body data-codigopantalla="411">
    <uc3:mensajeSession ID="MensajeSession" runat="server" />
    <cb1:CabeceraMenu runat="server" ID="CabeceraMenu"></cb1:CabeceraMenu>

    <br />
    <br class="hidden-xs" />
    <br class="hidden-xs" />

    <div class="container">

        <div class="row">
            <div id="divtxtFantiguedad" runat="server" class="fk-ocultar">
                <span>Antigüedad de referencia (defecto)</span>
                <input type="text" runat="server" id="txtFantiguedad" name="to" readonly="readonly"  />
            </div>


        </div>


    </div>

</body>
</html>

<cb1:PieMenu runat="server" ID="PieMenu"></cb1:PieMenu>
