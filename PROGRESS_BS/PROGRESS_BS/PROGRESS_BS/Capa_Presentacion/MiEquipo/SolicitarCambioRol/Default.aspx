<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_MiEquipo_SolicitarCambioRol_Default" %>

<%@ Register  Src="~/uc/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="cb1" %>
<%@ Register  Src="~/uc/PieMenu.ascx" TagName="PieMenu" TagPrefix="cb1" %>
<%@ Register  Src="~/uc/uc_Session.ascx" TagName="mensajeSession" TagPrefix="uc3" %>
<%@ Register Src="~/uc/HeaderMeta.ascx" TagPrefix="cb1" TagName="HeaderMeta" %>


<!DOCTYPE html>

<!DOCTYPE html>
<html>
<head>
    <cb1:HeaderMeta runat="server" ID="HeaderMeta" />
    <title>Solicitud de cambio de rol</title>
    <script type="text/javascript" src="js/models.js"></script>
  
</head>
<body data-codigopantalla="300">
    <uc3:mensajeSession ID="MensajeSession" runat="server" />
    <cb1:CabeceraMenu runat="server" ID="CabeceraMenu"></cb1:CabeceraMenu>
    <br />
    <br class="hidden-xs" />
    <br class="hidden-xs" />
    <div class="container">
        <div class="divMiEquipo">
            <div class="row">
                <div class="col-xs-12">
                    <div class="table-responsive">
                        <table id="tablaSolicitudRol" class="table header-fixed">
                            <thead class="bck-ibermatica">
                                <tr>

                                    <th class="fontSizeCabeceras">Profesional</th>
                                    <th class="fontSizeCabeceras">Rol actual</th>
                                    <th class="fontSizeCabeceras">Rol propuesto</th>
                                    <th></th>
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
                       <%-- <i class="glyphicon glyphicon-user"></i>
                        <span>Solicitada mediación</span>--%>
                        <i class="glyphicon glyphicon-comment text-primary"></i>
                        <span>Motivo</span>
                        <div class="pull-right">
                            <button  id="btnModalTramitar" type="button" class="btn btn-primary">Identificar rol a proponer</button>
                            <button id="btnModalAnular" type="button" class="btn btn-primary">Anular solicitud</button>
                        </div>
                        
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- MODAL Solicitud de cambio de rol -->
    
    <div class="modal fade" id="modal-solicitar">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <h4 class="modal-title">Solicitud de cambio de rol</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-xs-3">
                            <span>Profesional</span>
                        </div>
                        <div class="col-xs-9">
                            <input type="text" disabled/>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-3">
                            <span>Rol actual</span>
                        </div>
                        <div class="col-xs-9">
                            <input id="inputRolActual" type="text" disabled/>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-3">
                            <span>Rol propuesto</span>
                        </div>
                        <div class="col-xs-9">
                            <select id="selRolPropuesto"></select>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-3">
                            <span>Motivo </span><br />                            
                        </div>
                        <div class="col-xs-9">
                            <textarea id="txtMotivo" maxlength="750" rows="4"></textarea>
                            <br />
                            <span id="numCaracteres"></span>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">                    
                    <b><button class="btn btn-default pull-right" data-dismiss="modal">Cancelar</button></b>
                    <b><button id="btnTramitar" class="btn btn-primary pull-right">Tramitar solicitud</button></b>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->

    <!-- MODAL Anular solicitud -->
    
    <div class="modal fade" id="modal-no-motivo">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <h4 class="modal-title">Confirmación de tramitación de cambio de rol</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-xs-10 col-xs-offset-1">
                            <span>Has tramitado la solicitud sin aportar un motivo. Pulsa "Cancelar" para poder anotarlo, o "Aceptar" para continuar tal y como está.</span>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <b><button class="btn btn-default pull-right" data-dismiss="modal">Cancelar</button></b>
                    <b><button id="btnAceptar" class="btn btn-primary pull-right">Aceptar</button></b>                    
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->

    <!-- MODAL Anular solicitud -->
    
    <div class="modal fade" id="modal-anular">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <h4 class="modal-title">Anular solicitud</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-xs-10 col-xs-offset-1">
                            <span>Motivo de anulación</span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-10 col-xs-offset-1">
                            <textarea id="txtMotivoAnulacion" rows="4"></textarea>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">                    
                    <b><button class="btn btn-default pull-right" data-dismiss="modal">Cancelar</button></b>
                    <b><button id="btnAnular" class="btn btn-primary pull-right">Confirmar anulación</button></b>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->
</body>
</html>
<cb1:PieMenu runat="server" ID="PieMenu"></cb1:PieMenu>
