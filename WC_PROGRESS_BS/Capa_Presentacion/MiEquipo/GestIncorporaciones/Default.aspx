<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_MiEquipo_GestIncorporaciones_Default" %>

<%@ Register  Src="~/uc/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="cb1" %>
<%@ Register  Src="~/uc/PieMenu.ascx" TagName="PieMenu" TagPrefix="cb1" %>
<%@ Register  Src="~/uc/uc_Session.ascx" TagName="mensajeSession" TagPrefix="uc3" %>
<%@ Register Src="~/uc/HeaderMeta.ascx" TagPrefix="cb1" TagName="HeaderMeta" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <cb1:HeaderMeta runat="server" ID="HeaderMeta" />
    <title>Gestionar incorporaciones</title>

    <style>
        #tblIncorpEnTramite tr {
            cursor:pointer;
        }

        .table > tbody > tr.active, .table > tbody > tr.active > td {
            background-color: #D8D8D8 !important;
            color: #000;
        }

        #btnAceptarIncorporacion {
            margin-right: 10px;
        }
    </style>
</head>
<body data-codigopantalla="202">
    <uc3:mensajeSession ID="MensajeSession" runat="server" />
    <cb1:CabeceraMenu runat="server" ID="CabeceraMenu"></cb1:CabeceraMenu>
    <br />
    <br class="hidden-xs" />
    <br class="hidden-xs" />

    

    <!-- Modal Aceptar incorporación -->
    <div class="modal fade" id="modal-AceptarIncorporacion" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">                    
                    <h4 class="modal-title">Aceptar incorporación</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div style="max-height:150px; overflow:auto;" class="col-xs-12">
                            <%--<h5 id="txtAceptarIncorporacion" class="cajaH5"></h5>--%>
                              <table>
                                <tbody id="tblModalAceptarIncorporacion">
                                </tbody>
                            </table>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <button type="button" id="btnConfirmIncorporacion" class="btn btn-primary">Confirmar incorporación</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>                    
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>


     <!-- Modal Aceptar incorporación -->
    <div class="modal fade" id="modal-RechazarIncorporacion" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">                    
                    <h4 class="modal-title">Rechazar incorporación</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-xs-12" style="max-height:150px; overflow:auto; margin-bottom:15px;">
                            <h5 id="txtRechazarIncorporacion"></h5>
                             <table>
                                <tbody id="tblModalRechazarIncorporacion">
                                </tbody>
                            </table>
                        </div>
                        
                        <div class="col-xs-12">
                            <div>Motivo de rechazo</div>
                            <textarea id="txtMotivo" style="width: 570px; height: 100px"></textarea>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <button type="button" id="btnConfirmRechazo" class="btn btn-primary">Confirmar rechazo</button>
                    <button type="button" onclick="limpiar();" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <!-- FIN MODALES-->

    <div class="container">
        <div class="">
            <div class="row">
                <div id="divMiEquipo" class="col-xs-4 well">
                    <div class="row">                       
                        <div class="col-xs-6" style="margin-bottom:5px">
                            <b><span>Mi equipo</span></b>
                        </div>
                    </div>

                    <div class="table-responsive">
                        <table id="tablaMiEquipo" class="table header-fixed table-striped">
                            <thead class="bck-ibermatica" style="height:40px">
                                <tr>                                    
                                    <th class="fontSizeCabeceras">Profesional</th>
                                </tr>
                            </thead>
                            <tbody runat="server" id="lisMiEquipo">
                            </tbody>
                        </table>
                    </div>


                    <div id="divpieTablaMiEquipo">

                    </div>
                </div>


                <div class="col-xs-8  well" style="width:64%; float:right">
                    <div class="row">
                        <div class="col-xs-6" style="margin-bottom:5px">
                            <b><span>Incorporaciones en trámite</span></b>
                        </div>
                    </div>
                    <div class="table-responsive">
                        <table id="tablaIncorporaciones" class="table header-fixed table-striped table-hover">
                            <thead class="bck-ibermatica">
                                <tr>                                    
                                    <th class="fontSizeCabeceras">Profesional</th>
                                    <th class="fontSizeCabeceras">Origen</th>
                                    <th class="fontSizeCabeceras">Tramitada</th>
                                    <th></th>                                    
                                </tr>
                            </thead>
                            <tbody runat="server" id="tblIncorpEnTramite">
                            </tbody>
                        </table>
                    </div>

                    <div id="divpietablaIncorporaciones" style="display:none">
                        
                        <div class="clearfix"></div>

                        <div>                                                                                  
                            <i class="glyphicon glyphicon-comment text-primary" style="margin-left: 3px"></i>
                            <span>Comentario origen</span>
                            
                        </div>
                        <br />

                        <div>                            
                            <button type="button" id="btnRechazarIncorporacion" class="btn btn-primary pull-right">Rechazar incorporación</button>
                            <button type="button" id="btnAceptarIncorporacion" class="btn btn-primary pull-right">Aceptar incorporación</button>
                        </div>

                    </div>
                </div>


            </div>
        </div>
    </div>
</body>
</html>


<cb1:PieMenu runat="server" ID="PieMenu"></cb1:PieMenu>
