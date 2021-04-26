<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Administracion_Mantenimientos_ModeloFormulario_Default" %>

<%@ Register Src="~/uc/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="cb1" %>
<%@ Register Src="~/uc/PieMenu.ascx" TagName="PieMenu" TagPrefix="cb1" %>
<%@ Register Src="~/uc/uc_Session.ascx" TagName="mensajeSession" TagPrefix="uc3" %>
<%@ Register Src="~/uc/HeaderMeta.ascx" TagPrefix="cb1" TagName="HeaderMeta" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <cb1:HeaderMeta runat="server" ID="HeaderMeta" />
    <title></title>
</head>
<body data-codigopantalla="414">
    <uc3:mensajeSession ID="MensajeSession" runat="server" />
    <cb1:CabeceraMenu runat="server" ID="CabeceraMenu"></cb1:CabeceraMenu>

    <br />
    <br class="hidden-xs" />
    <br class="hidden-xs" />

    <div class="container">
        <div class="row">
            <div class="col-xs-10 col-xs-offset-1">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">Relación entre colectivo Progress y formulario
                    </div>
                    <div class="panel-body">
                        <table id="tableEval" class="table table-hover tablesorter">
                            <thead>
                                <tr>
                                    <th><span></span>Colectivo</th>
                                    <th><span></span>Modelo formulario</th>
                                </tr>
                            </thead>
                            <tbody id="tbdColectivoFormulario">
                             
                            </tbody>
                        </table>

                    </div>
                </div>
            </div>
        </div>
    </div>

</body>
</html>
<cb1:PieMenu runat="server" ID="PieMenu"></cb1:PieMenu>
