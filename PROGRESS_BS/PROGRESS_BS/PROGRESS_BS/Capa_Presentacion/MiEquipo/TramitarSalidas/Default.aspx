<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_MiEquipo_TramitarSalidas_Default" %>

<%@ Register  Src="~/uc/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="cb1" %>
<%@ Register  Src="~/uc/PieMenu.ascx" TagName="PieMenu" TagPrefix="cb1" %>
<%@ Register  Src="~/uc/uc_Session.ascx" TagName="mensajeSession" TagPrefix="uc3" %>
<%@ Register Src="~/uc/HeaderMeta.ascx" TagPrefix="cb1" TagName="HeaderMeta" %>


<!DOCTYPE html>
<html>
<head>
    <cb1:HeaderMeta runat="server" ID="HeaderMeta" />
    <title></title>

    <style>
      

        .table > tbody > tr.active  {
            background-color: #D8D8D8 !important;
            color: #000;
        }

        table > tbody > tr.active > td {
            background-color:inherit !important;
        }
        
        table tr {
            cursor:pointer;
        }

        #tblModalTramitar, #tblModalAnularSalida {
            max-height: 100px;
            overflow: auto;
            display: block;
            width: 570px;
        }

        #tblgetFicepi {
            max-height: 300px;
            overflow: auto;
        }

        ul > li, #lisMiEquipo tr {
            cursor: pointer;
        }

        hr {
            border-top: 1px solid #808080 !important;
        }

        .tacharUsuario {
            text-decoration : line-through;
        }

        .cajaH5 {
            border:1px solid #ccc;
            padding:10px;
            color: #666;
            border-radius:5px;
            font-size:14px;
        }

        #btnSolicitarMediacion {
            margin-right: 10px;
        }
    </style>
</head>

<body data-codigopantalla="201">
    <uc3:mensajeSession ID="MensajeSession" runat="server" />
    <cb1:CabeceraMenu runat="server" ID="CabeceraMenu"></cb1:CabeceraMenu>
    <br />
    <br class="hidden-xs" />
    <br class="hidden-xs" />


    <div class="modal fade" id="modal-tramitar">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <h4 class="modal-title">Identificación de evaluador/a destino</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-xs-8">
                            <table>
                                <tbody id="tblModalTramitar">
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-xs-8">
                            <a class="btn btn-link" style="padding: 0; text-decoration: underline;" data-toggle="modal" href="#modal-ficepi">Selección de evaluador/a destino</a>
                            <input id="txtDestinatario" disabled style="width: 570px" type="text" />
                        </div>
                    </div>

                    <br />

                    <div class="row">
                        <div class="col-xs-12">
                            <span>Comentario para evaluador/a destino</span><br />
                            <textarea id="txtComentario" maxlength="750" style="width: 570px; height: 100px"></textarea>
                            <br />
                            <span id="numCaracteres">160 caracteres disponibles</span>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <b><button id="btnConfirmarSalida" class="btn btn-primary" data-toggle="modal" data-backdrop="static" data-keyboard="false" data-target="#modal-finalizar">Confirmar salida</button></b>
                    <button type="button" style="margin-left:7px" onclick="comprobarCambios()" class="btn btn-default" data-dismiss="modal">Cancelar</button>                                            
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>





    <div class="modal fade" id="modal-ficepi" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">                    
                    <h4 class="modal-title">Selección de evaluador/a destino</h4>
                </div>
                <div class="modal-body">
                    <div id="txtBusqueda" class="row">
                        <div class="col-xs-3">
                            <span>Apellido 1</span><input id="inputApellido1" type="text" value="" />
                        </div>
                        <div class="col-xs-3" style="margin-left: 8px">
                            <span>Apellido 2</span><input id="inputApellido2" type="text" value="" />
                        </div>
                        <div class="col-xs-3" style="margin-left: 8px">
                            <span>Nombre</span><input id="txtNombre" type="text" value="" />
                        </div>
                        <div class="col-xs-1 col-xs-offset-1">
                            <button id="btnObtener" style="margin-top: 22px" class="btn btn-primary btn-xs">Obtener</button>
                        </div>
                    </div>

                    <br />

                    <div class="row">
                        <div class="col-xs-12">
                            <ul style="padding: 0" id="tblgetFicepi"></ul>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <button type="button" id="btnSeleccionar" class="btn btn-primary">Aceptar</button>
                    <button type="button" class="btn btn-default"  onclick="comprobarCambios()" data-dismiss="modal">Cancelar</button>                    
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->


    <div class="modal fade" id="modal-AnularSalida" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">                    
                    <h4 class="modal-title">Anulación de salidas en trámite</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-xs-12">
                            <%--<h5 id="txtProfAnulacion" class="cajaH5"></h5>--%>
                             <table>
                                <tbody id="tblModalAnularSalida">
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <br />

                      <div id="divMotivo" class="row" style="display:none">
                        <div class="col-xs-12">
                            <span>Motivo</span><br />
                            <textarea id="txtComentarioAnulacion" maxlength="750" style="width: 570px; height: 100px"></textarea>
                            <br />
                            <span id="numCaracteresAnulacion"></span>
                        </div>
                     </div>


                </div>
                <div class="modal-footer">
                    <button type="button" id="btnConfirmarAnulacion" class="btn btn-primary">Confirmar anulación</button>
                    <button type="button" id= "btnCancelarAnulacion" class="btn btn-default" style="margin-left:7px;" data-dismiss="modal">Cancelar</button>                    
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>


     <div class="modal fade" id="modal-mediacion" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">                    
                    <h4 class="modal-title">Solicitar mediación a RRHH</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-xs-12">
                            <h5 id="txtProfMediacion" class="cajaH5"></h5>
                        </div>
                    </div>

                     <div class="row">
                        <div class="col-xs-8">
                            <span>Motivo</span><br />
                            <textarea id="txtComentarioMediacion" maxlength="750" style="width: 570px; height: 100px"></textarea>
                            <br />
                            <span id="numCaracteresMediacion">160 caracteres disponibles</span>
                        </div>
                     </div>

                </div>
                <div class="modal-footer">
                    <button type="button" id="btnConfirmarMediacion" class="btn btn-primary">Confirmar mediación</button>
                    <button type="button" id="btnCancelarMediacion" class="btn btn-default" data-dismiss="modal">Cancelar</button>                    
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>


    <div class="container">
        
            <div class="row">
                <div class="col-xs-12">
                <div id="divMiEquipo" class="col-xs-4 well">
                    <div class="row">

                        <div id="myteam">
                            <div class="btn-group col-xs-1">
                                <a class="btn btn-default selector" data-toggle="popover"  data-placement="top" title="" data-content="Marcar/desmarcar todo">
                                    <i class="glyphicon glyphicon-unchecked"></i>
                                </a>
                            </div>

                        </div>

                        <div class="col-xs-6">
                            <b><span style="display: inline-block; margin-top: 10px; margin-left: 15px">Mi equipo</span></b>
                        </div>



                    </div>




                    <%-- <ul class="list-group" runat="server" id="lisMiEquipo">
                      
                    </ul>
                    --%>

                    <div class="table-responsive" style="margin-top: 5px;"> 
                        <table id="tblMiEquipo" class="table header-fixed table-striped">
                            <thead class="bck-ibermatica">
                                <tr>
                                    <th></th>
                                    <th class="fontSizeCabeceras">Profesional</th>
                                </tr>
                            </thead>
                            <tbody runat="server" id="lisMiEquipo">
                            </tbody>
                        </table>
                    </div>


                    <div id="divpieTablaMiEquipo">

                        <a href="#" data-toggle="popover" data-placement="top" title="Evaluación abierta" data-content="Pendiente de la firma del evaluador/a."><i class="fa fa-file-text-o verde"></i><span id="spanEvalAbierta">Evaluación abierta</span></a> 

                        <a href="#" data-toggle="popover" data-placement="top" title="Evaluación en curso" data-content="Pendiente de la firma del evaluado/a."><i class="fa fa-file-text-o azul"></i><span id="spanEvalEnCurso">Evaluación en curso</span></a> 
                          
                        <button style="margin-top:20px" id="btnTramitar" data-toggle="modal" onclick="tramitar()" type="button" class="btn btn-primary pull-right">Identificar evaluador/a destino</button>
                    </div>
                </div>


                <div class="col-xs-8 well" style="width:66%; float:right">
                    <div class="row">
                        <div class="col-xs-6">
                            <b><span style="display: inline-block; margin-top: 12px">Mi equipo con salidas en trámite</span></b>
                        </div>
                    </div>
                    <div class="table-responsive">
                        <table id="tablaSalidasEnTramite" class="table header-fixed table-striped table-hover" style="margin-top:7px">
                            <thead class="bck-ibermatica">
                                <tr>
                                    <th></th>
                                    <th class="fontSizeCabeceras">Profesional</th>
                                    <th class="fontSizeCabeceras">Evaluador/a destino</th>
                                    <th class="fontSizeCabeceras">Tramitada</th>
                                    <th></th>
                                    <th></th>
                                </tr>
                            </thead>
                            <tbody runat="server" id="tblSalidasEnTramite">
                            </tbody>
                        </table>
                    </div>

                    <div id="divpietablaSalidas" class="hide">
                      
                        <div class="clearfix"></div>

                        <div>
                            <span class="glyphicon glyphicon-new-window text-danger" style="margin-left: 3px"></span>
                            <span style="margin-right:3px">Salida tramitada</span>
                            <i class="fa fa-compress text-danger"></i>
                            <span style="margin-right:3px">Salida rechazada</span>

                           <%-- <i class="glyphicon glyphicon-user text-danger"></i>
                            <span>Solicitada mediación</span><br />--%>

                            <i class="glyphicon glyphicon-comment text-primary" style="margin-left: 3px"></i>
                            <span style="margin-right:3px">Mi comentario</span>
                            <i style="margin-left: 3px" class="glyphicon glyphicon glyphicon-comment text-danger"></i>
                            <span>Motivo rechazo</span>

                        </div>
                        <br />

                        <div id="divSalTramite">
                            <button type="button" id="btnAnularSalida" class="btn btn-primary pull-right">Anular salida</button>
                            
                            <%--<button type="button" id="btnSolicitarMediacion" class="btn btn-primary pull-right">Solicitar mediación</button>--%>
                            
                        </div>

                    </div>
                </div>

</div>
            </div>
     
    </div>



</body>
</html>
<cb1:PieMenu runat="server" ID="PieMenu"></cb1:PieMenu>

