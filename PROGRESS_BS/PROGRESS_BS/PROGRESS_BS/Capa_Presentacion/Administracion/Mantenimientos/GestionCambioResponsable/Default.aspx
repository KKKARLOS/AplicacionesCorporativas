<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Administracion_Mantenimientos_GestionarMediacion_Default" %>

<%@ Register  Src="~/uc/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="cb1" %>
<%@ Register  Src="~/uc/PieMenu.ascx" TagName="PieMenu" TagPrefix="cb1" %>
<%@ Register  Src="~/uc/uc_Session.ascx" TagName="mensajeSession" TagPrefix="uc3" %>
<%@ Register Src="~/uc/HeaderMeta.ascx" TagPrefix="cb1" TagName="HeaderMeta" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<cb1:HeaderMeta runat="server" ID="HeaderMeta" />
<title>Gestionar cambio de responsable</title>
<link href="../../../../js/plugins/tablesorter/css/style.css" rel="stylesheet" type="text/css" />
   
</head>
<body data-codigopantalla="400">
    
    <uc3:mensajeSession ID="MensajeSession" runat="server" />
    <cb1:CabeceraMenu runat="server" ID="CabeceraMenu"></cb1:CabeceraMenu>    
    <br class="hidden-xs" />
    <br class="hidden-xs" />

    
     <div class="modal fade" id="modal-gestionar">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <h4 class="modal-title">Gestión de dependencias</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-xs-8">
                            <table>
                                <tbody id="tblModalGestionar">
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-xs-8">
                            <label id="lblInteresado">Interesado</label>
                            <input id="txtInteresado" disabled style="width: 570px" type="text" />
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-xs-6">
                            <label id="lblEvalActual">Evaluador actual</label>
                            <input id="txtOrigen" disabled style="width: 270px;margin-bottom:15px;" type="text" />
                            <label id="lblMotivoOrigen">Motivo de la proposición</label>
                            <textarea id="txtMotivoOrigen" disabled="disabled" style="width:270px"></textarea><br />

                            <label id="lblFechaTramitacion">Tramitación:</label>
                            <span id="txtFechatramitacion"></span><br />
                            <label id="lblFechaRechazo" style="display:none;">Rechazo:</label>
                            <span style="display:none" id="txtFecharechazo"></span>
                            <label id="lblFechaSolMediacion" style="display:none;">Solicitud de mediación:</label>
                            <span style="display:none" id="txtFechaSolMediacion"></span>
                        </div>

                        <div class="col-xs-6">
                            <label id="lblDestino" style="visibility:hidden">Evaluador propuesto</label>
                            <input id="txtDestino" disabled style="width: 270px;margin-bottom:15px;" type="text" />
                            <label id="lblMotivoDestino" class="hide">Motivo de rechazo</label>
                            <textarea id="txtMotivoDestino" class="hide" disabled="disabled" style="width:270px"></textarea>
                        </div>
                    </div>



                   
                </div>
                <div class="modal-footer">
                        
                    <b><button id="btnSelEvaluador" class="btn btn-primary" style="margin-left:7px" data-toggle="modal" data-backdrop="static" data-keyboard="false" data-target="#modal-finalizar">Seleccionar otro evaluador</button></b>
                    <%--<b><button id="btnForzar" style="display:none; margin-left:7px" class="btn btn-primary"  data-toggle="modal" data-backdrop="static" data-keyboard="false" data-target="#modal-finalizar">Forzar asignación</button></b>--%>
                    <b><button id="btnAnular" class="btn btn-primary" style="margin-left:7px" data-toggle="modal" data-backdrop="static" data-keyboard="false" data-target="#modal-finalizar">Anular</button></b>
                    <button style="margin-left:7px"  type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>                                            
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>


    <!--MODAL EVALUADORES-->
    <div class="modal fade" id="modal-evaluadores">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <h4 class="modal-title">Seleccionar evaluador</h4>
                </div>
                <div class="modal-body">

                    <div id="txtBusqueda" class="row">
                        <div class="col-xs-3">
                             <label for="lblApellido1">Apellido 1º</label>
                                <input id="txtApellido1Modal" type="text" />
                        </div>
                        <div class="col-xs-3" style="margin-left: 8px">
                             <label for="lblApellido2">Apellido 2º</label>
                                <input id="txtApellido2Modal" type="text" />
                        </div>
                        <div class="col-xs-3" style="margin-left: 8px">
                            <label for="lblNombre">Nombre</label>
                            <input id="txtNombreModal" type="text" />
                        </div>
                        <div class="col-xs-1 col-xs-offset-1">
                            <button id="btnObtener" style="margin-top: 22px" class="btn btn-primary btn-xs">Obtener</button>
                        </div>
                    </div>


                    <br />
                    <div class="row">
                        <div class="col-xs-12">
                            <ul class="list-group" runat="server" id="lisEvaluadores">
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <b><button id="btnSeleccionar" class="btn btn-primary">Seleccionar</button></b>
                    <b><button id="btnCancelar" class="btn btn-default">Cancelar</button></b>                    
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->

    <!--FIN MODAL EVALUADORES-->


    <!--Confirmar cambio de evaludor -->
    <div class="modal fade" id="modal-ConfirmCambio" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">                    
                    <h4 class="modal-title">Confirmación de cambio de evaluador</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-xs-12">
                            <h5 id="txtProfCambio" class="cajaH5"></h5>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <button type="button" id="btnConfirmarCambio" class="btn btn-primary">Confirmar cambio</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>                    
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>


    <div class="modal fade" id="modal-Anular" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">                    
                    <h4 class="modal-title">Confirmación de anulación</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-xs-12">
                            <h5 id="txtProfAnular" class="cajaH5"></h5>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <button type="button" id="btnConfirmarAnular" class="btn btn-primary">Confirmar anulación</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>                    
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

  
    <div class="modal fade" id="modal-Forzar" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">                    
                    <h4 class="modal-title">Confirmación de asignación</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-xs-12">
                            <h5 id="txtProfForzar" class="cajaH5"></h5>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <button type="button" id="btnConfirmarForzar" class="btn btn-primary">Confirmar asignación</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>                    
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>



    <div class="container">
        <div class="divCambioResponsable">
            <div class="row">
                <div class="col-xs-12">

                     <fieldset class="fieldset">
                    <legend class="fieldset">Filtros</legend>
                   

                    <!--Contenido filtros-->
                    <div id="filtros">                      
                        <div class="row">                                                        
                            
                            <div id="divNombre">
                                <div class="col-xs-2" style="width:17.666667%;">
                                    <label for="lblApellido1">Apellido 1º</label>
                                    <input  id="txtApellido1"  type="text" />
                                </div>
                                <div class="col-xs-2" style="width:17.666667%;">
                                    <label  for="lblApellido2">Apellido 2º</label>
                                    <input  id="txtApellido2" type="text" />
                                </div>
                                <div class="col-xs-2" style="width:17.666667%;">
                                    <label  for="lblNombre">Nombre</label>
                                    <input style="margin-left:-1px" id="txtNombre" type="text" />
                                </div>

                                <div id="divEstado" style="margin-top:25px;margin-right:75px" runat="server" class="pull-right">
                                <b>Estado</b>
                                <select runat="server" id="cboEstado">      
                                    <option value=""  selected="selected"></option>                                                                        
                                    <option value="1">Tramitadas</option>
                                    <option value="3">Rechazadas</option>                                    
                                    <%--<option value="6" selected="selected">Solicitada mediación</option>                                                                        --%>
                                </select>
                            </div>

                            </div>


                            

                        </div>

                    </div>

                </fieldset>


                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">Gestión de dependencias</h3>
                    </div>
                    <div class="panel-body">
                        <table id="tablaGestCambioResponsable" class="table header-fixed">
                            <thead>
                                <tr>
                                    <th><span style="margin-left:12px">Profesional</span></th>
                                    <th><span class="margenCabecerasOrdenables">Estado</span></th>
                                    <th><span class="margenCabecerasOrdenables">Tramitación</span></th>
                                    <th><span style="margin-left:12px">Rechazo</span></th>
                                    <%--<th><span style="margin-left:11px">Sol. Mediación</span></th>--%>
                                </tr>
                            </thead>
                            <tbody runat="server" id="tblGestCambioResponsable">
                              
                            </tbody>
                        </table>

                    </div>
                    
                </div>


                    <div class="pull-right">
                        <button id="btnGestionar" class="btn btn-primary hide">Gestionar</button>
                    </div>

                <!--Detalle de la fila -->
                <div id="divDetalle" class="hide">   
                                   
                </div>

                </div>

                

            </div>

            

        </div>
    </div>
</body>
</html>
<cb1:PieMenu runat="server" ID="PieMenu"></cb1:PieMenu>
<script type="text/javascript" src="../../../../js/plugins/tablesorter/jquery.tablesorter.min.js"></script> 
<script type="text/javascript" src="../../../../js/moment.locale.min.js"></script>