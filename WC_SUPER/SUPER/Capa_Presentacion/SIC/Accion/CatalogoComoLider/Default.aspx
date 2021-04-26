<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_SIC_Accion_CatalogoComoLider_Default" %>

<%@ Register Src="~/Capa_Presentacion/bsUserControls/Head.ascx" TagPrefix="uc1" TagName="Head" %>
<%@ Register Src="~/Capa_Presentacion/bsUserControls/Menu/Menu.ascx" TagPrefix="uc1" TagName="Menu" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <uc1:Head runat="server" ID="Head" />
    <title>Mis acciones como líder</title>
    <link href="../../../../css/jquery-ui.min.css" rel="stylesheet" />
    <link href="../../../../plugins/datatables/jquery.dataTables.min.css" rel="stylesheet" />


</head>
<body>
    <uc1:Menu runat="server" ID="Menu" />

    <form class="form-horizontal" id="frmAcciones" runat="server">

        <div class="container-fluid">


            <div class="row">
                <div class="col-xs-12">
                    <div class="panel with-nav-tabs panel-default">
                        <div class="panel-heading" style="padding-top:0">
                            <ul id="ulTabs" class="nav nav-tabs">
                                <li id="liTareasAccion" class="active"><a id="tabTareasAccion" href="#tab1default" data-toggle="tab">Acciones preventa abiertas y sus tareas asociadas</a></li>
                                <li id="liTareasActivasParticipante"><a id="tabTareasActivasParticipante" href="#tab2default" data-toggle="tab">Mis tareas preventa en curso</a></li>
                            </ul>
                        </div>
                        <div class="panel-body" style="padding-top: 0">
                            <div class="tab-content">
                                <div class="tab-pane fade in active" id="tab1default">
                                    <div class="row">

                                        <div class="ibox-title">
                                            <h5 class="text-primary inline">Mis acciones preventa como líder abiertas</h5>
                                        </div>
                                        <div class="ibox-content  blockquote blockquote-info" style="padding-top:0">
                                            <div class="table-responsive" style="overflow-y: hidden">
                                                <table id="tblAcciones" class="table table-bordered table-condensed">
                                                    <thead class="well text-primary">
                                                        <tr>
                                                            <th class="text-center">Ref.</th>
                                                            <th class="text-center">Acción preventa &nbsp;&nbsp;&nbsp;(TT/TA)</th>
                                                            <th title="Oportunidad, extensión, objetivo o solicitud preventa interna para la que se ha solicitado la acción preventa" class="text-center">Origen</th>
                                                            <th class="text-center">Importe</th>
                                                            <th class="text-center">Cuenta</th>
                                                            <th class="text-center">Solicitante</th>
                                                            <th class="text-center">Org. Comercial</th>
                                                            <%--<th class="text-center">Unidad</th>--%>
                                                            <th class="text-center">Área</th>
                                                            <th class="text-center">Subárea</th>                                                            
                                                            <th class="text-center">Creación</th>
                                                            <th class="text-center">Fin requerido</th>
                                                            
                                                            
                                                        </tr>
                                                    </thead>
                                                </table>

                                            </div>

                                            <div class="clearfix"></div>

                                        </div>

                                    </div>
                                    <br />

                                    <div class="row">
                                        <div class="ibox-title">
                                            <h5 class="text-primary">Tareas asociadas a la acción preventa seleccionada</h5>
                                        </div>

                                        <div class="col-xs-8 left hide">
                                            <label class="radio-inline" for="radios-0">
                                                <input type="radio" checked="checked" name="radios" id="radios-0" value="1" />
                                                Asociadas a la acción
                                            </label>
                                            <label class="radio-inline" for="radios-1">
                                                <input type="radio" name="radios" id="radios-1" value="2" />
                                                Activas como participante
                                            </label>
                                        </div>

                                        <div id="divTareas" class="ibox-content  blockquote blockquote-info" style="padding-top: 0">
                                            <div class="col-xs-12 table-responsive">
                                                <table id="tblTareas" class="table table-bordered table-condensed">
                                                    <thead class="well text-primary">
                                                        <tr>
                                                            <th class="text-center">Ref.</th>
                                                            <th class="text-center">Tarea</th>
                                                            <th class="text-center">Participantes</th>
                                                            <th class="text-center">Estado tarea</th>                                                                                                                                                                                    
                                                            <th class="text-center">Creación</th>
                                                            <th class="text-center">Fin previsto</th>
                                                            <th class="text-center">Fin./Anul.</th>
                                                            
                                                        </tr>
                                                    </thead>
                                                </table>



                                            </div>

                                            <div class="clearfix"></div>
                                        </div>

                                    </div>



                                </div>

                                <!-- todo meter campos nuevos-->
                                <div class="tab-pane fade" id="tab2default">
                                    <div id="divTareasParticipante" class="ibox-content  blockquote blockquote-info">
                                        <div class="col-xs-12">
                                            <table id="tblTareasParticipante" class="table table-bordered table-condensed">
                                                <thead class="well text-primary">
                                                    <tr>
                                                        <th class="text-center">Ref.</th>
                                                        <th class="text-center">Tarea</th>                                                        
                                                        <th class="text-center">Participantes</th>
                                                        <th class="text-center">Acción preventa</th>

                                                        <th title="Oportunidad, extensión, objetivo o solicitud preventa interna para la que se ha solicitado la acción preventa" class="text-center">Origen</th>
                                                        <th class="text-center">Cuenta</th>
                                                        <th class="text-center">Solicitante de la acción</th>
                                                        <th class="text-center">Área</th>
                                                        <th class="text-center">Subárea</th>
                                                        <th class="text-center">Líder</th>
                                                        <th class="text-center">Creación</th>
                                                        <th class="text-center">Fin previsto</th>                                                                                                                
                                                    </tr>
                                                </thead>
                                            </table>
                                        </div>

                                        <div class="clearfix"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>








        </div>
    </form>

    <script src="../../../../plugins/datatables/jquery.dataTables.min.js"></script>
    <script src="../../../../plugins/datatables/dataTables.fixedColumns.min.js"></script>
    <script src="../../../../scripts/moment.locale.min.js"></script>
    <script src="../../../../scripts/accounting.min.js"></script>
    <script src="js/view.js?v=20180315"></script>
    <script src="js/app.js?v=20180315"></script>

</body>
</html>
