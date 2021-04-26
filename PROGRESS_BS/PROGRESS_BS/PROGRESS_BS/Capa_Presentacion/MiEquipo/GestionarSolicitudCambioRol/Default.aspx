<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_MiEquipo_GestionarSolicitudCambioRol_Default" %>

<%@ Register  Src="~/uc/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="cb1" %>
<%@ Register  Src="~/uc/PieMenu.ascx" TagName="PieMenu" TagPrefix="cb1" %>
<%@ Register  Src="~/uc/uc_Session.ascx" TagName="mensajeSession" TagPrefix="uc3" %>
<%@ Register Src="~/uc/HeaderMeta.ascx" TagPrefix="cb1" TagName="HeaderMeta" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <cb1:HeaderMeta runat="server" ID="HeaderMeta" />
<title>Gestión de solicitudes de cambio de rol</title>
    <style>
        
        #tblSolicitudesRol tr {
            cursor: pointer;
        }

        .table > tbody > tr.active > td {
            background-color: #D8D8D8 !important;
            color: #000;
        }

        .modal button {
            margin-right: 10px;
        }

        #tblRoles tr td {
            border:none;
            margin-bottom:0;            
        }
        .W130 {
            width: 130px;
        }

    </style>
</head>
<body data-codigopantalla="301">
    <uc3:mensajeSession ID="MensajeSession" runat="server" />
    <cb1:CabeceraMenu runat="server" ID="CabeceraMenu"></cb1:CabeceraMenu>
    <br />
    <br class="hidden-xs" />
    <br class="hidden-xs" />

    <!--Modal aceptar propuesta-->
    <div class="modal fade" id="modal-aceptarPropuesta" data-backdrop="static" data-keyboard ="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <h4 class="modal-title">Aceptar la propuesta de cambio de rol</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-xs-10 col-xs-offset-1">                            
                            <h5 id="txtAceptarPropuesta" class="cajaH5"></h5>                            
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-10 col-xs-offset-1">                                                              
                            <table id="tblRoles" class="table">
                                <tr>
                                    <td class="W130"><b><span>Rol actual:</span></b></td>
                                    <td> <span id="txtRolActual"></span></td>
                                </tr>
                                <tr>
                                    <td class="W130"><b><span>Rol propuesto:</span></b></td>
                                    <td><span id="txtRolPropuesto"></span></td>
                                </tr>
                            </table>                                                                                                               
                        </div>
                    </div>
                </div>
                <div class="modal-footer">                    
                    <b><button class="btn btn-default pull-right" data-dismiss="modal">Cancelar</button></b>
                    <b><button id="btnAceptar" class="btn btn-primary pull-right">Confirmar aceptación</button></b>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!--FIN Modal aceptar propuesta-->
    
    <!--Modal NO aceptar propuesta-->
    <div class="modal fade" id="modal-NOaceptarPropuesta" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <h4 class="modal-title">Rechazar la propuesta de cambio de rol</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-xs-10 col-xs-offset-1">                            
                            <h5 id="txtNoAceptarPropuesta" class="cajaH5"></h5>
                        </div>
                    </div>

                    <div class="row">
                         <div class="col-xs-12">
                            <div>Motivo</div>
                            <textarea id="txtMotivo" style="width: 570px; height: 100px"></textarea>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">                    
                    <b><button onclick="limpiar();" class="btn btn-default pull-right" data-dismiss="modal">Cancelar</button></b>
                    <b><button id="btnNOaceptar" class="btn btn-primary pull-right">Rechazar cambio de rol</button></b>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!--FIN Modal NO aceptar propuesta-->

    <div class="container">
        <div class="divMiEquipo">
            <div class="row">
                <div class="col-xs-12">
                    <div class="table-responsive">
                        <table class="table table-striped table-hover header-fixed">
                            <thead class="bck-ibermatica">
                                <tr>
                                    <th>Profesional</th>
                                    <th>Rol actual</th>
                                    <th>Rol propuesto</th>                                    
                                    <th>&nbsp;</th>
                                </tr>
                            </thead>
                            <tbody runat="server" id="tblSolicitudesRol">
                            </tbody>
                        </table>
                    </div>
                    <div id="divpietablaSolicitudes" style="display:none">                                                                        
                        <i class="glyphicon glyphicon-comment text-primary"></i>
                        <span>Motivo</span>
                        <div class="pull-right">
                            <button  id="btnAceptarPropuesta" type="button" class="btn btn-primary">Aceptar propuesta</button>
                            <button id="btnNoAceptarPropuesta" type="button" class="btn btn-primary" style="margin-left:7px;">Rechazar propuesta</button>
                        </div>
                        
                    </div>

                </div>
            </div>
        </div>
    </div>
</body>
</html>
<cb1:PieMenu runat="server" ID="PieMenu"></cb1:PieMenu>
<script type="text/javascript" src="../../../js/moment.locale.min.js"></script>