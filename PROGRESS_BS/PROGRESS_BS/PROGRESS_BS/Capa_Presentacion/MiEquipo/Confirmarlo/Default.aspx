<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_MiEquipo_Confirmarlo_Default" %>

<%@ Register  Src="~/uc/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="cb1" %>
<%@ Register  Src="~/uc/PieMenu.ascx" TagName="PieMenu" TagPrefix="cb1" %>
<%@ Register  Src="~/uc/uc_Session.ascx" TagName="mensajeSession" TagPrefix="uc3" %>
<%@ Register Src="~/uc/HeaderMeta.ascx" TagPrefix="cb1" TagName="HeaderMeta" %>


<!DOCTYPE html>

<!DOCTYPE html>
<html>
<head>
    <cb1:HeaderMeta runat="server" ID="HeaderMeta" />
    <title>Confirmación de equipo</title>
</head>
<body data-codigopantalla="200">
    <uc3:mensajeSession ID="MensajeSession" runat="server" />
    <cb1:CabeceraMenu runat="server" ID="CabeceraMenu"></cb1:CabeceraMenu>
    <br />
    <br class="hidden-xs" />
    <br class="hidden-xs" />
    <div class="container">
        <div class="divMiEquipo">
            <div class="row">
                <div class="col-xs-12 well">
                    <div class="row">
                        <div class="col-xs-6">
                            <span>Última confirmación de equipo</span>
                            <input readonly="readonly" type="text" class="form-control" runat="server" id="dateConfirm" />
                        </div>
                        <div class="col-xs-6">
                            <div class="pull-right" runat="server" id="divEntradas">
                                <span class="glyphicon glyphicon-info-sign"></span>
                                <a href="../GestIncorporaciones/Default.aspx">Gestiona las incorporaciones pendientes a tu equipo</a>
                            </div>
                        </div>
                    </div>
                    <div class="table-responsive">
                        <table id="tablaConfirmarlo" class="table header-fixed  table-striped table-hover">
                            <thead class="bck-ibermatica">
                                <tr>                                    
                                    <th><span style="margin-right:15px;">Profesional  </span>(<span id="spanNumero"></span> miembros)</th>
                                    <th>Rol</th>
                                </tr>
                            </thead>
                            <tbody runat="server" id="idtbody">
                            </tbody>
                        </table>
                    </div>
                    <div id="divSalTramite">
                        <span class="glyphicon glyphicon-new-window"></span>
                        <span>Salida tramitada</span>
                        <i class="fa fa-compress"></i>
                        <span>Salida rechazada</span>
                        <%--<i class="glyphicon glyphicon-user"></i>
                        <span>Solicitada mediación</span>--%>
                        <button type="button" class="btn btn-primary pull-right">Confirmar equipo</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
<cb1:PieMenu runat="server" ID="PieMenu"></cb1:PieMenu>
