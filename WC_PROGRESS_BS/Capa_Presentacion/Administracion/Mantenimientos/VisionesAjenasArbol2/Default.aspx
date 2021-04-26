<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Administracion_Mantenimientos_VisionesAjenasArbol2_Default" %>

<%@ Register  Src="~/uc/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="cb1" %>
<%@ Register  Src="~/uc/PieMenu.ascx" TagName="PieMenu" TagPrefix="cb1" %>
<%@ Register  Src="~/uc/uc_Session.ascx" TagName="mensajeSession" TagPrefix="uc3" %>
<%@ Register Src="~/uc/HeaderMeta.ascx" TagPrefix="cb1" TagName="HeaderMeta" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <cb1:HeaderMeta runat="server" ID="HeaderMeta" />
    <title>Visiones ajenas al árbol de dependencias</title>
    <style>
         #leyenda {
            margin-left:-20px; color:#3f7abc;
        }
    </style>
</head>
<body data-codigopantalla="417">
    <uc3:mensajeSession ID="MensajeSession" runat="server" />
    <cb1:CabeceraMenu runat="server" ID="CabeceraMenu"></cb1:CabeceraMenu>    
    <br class="hidden-xs" />
    <br class="hidden-xs" />

    <form runat="server"></form>
    
    <div class="container">
        <div class="row">
            <div class="col-xs-1 col-xs-offset-1" style="margin-bottom:5px;">
                <img id="imgPlegarTodo" alt="Expandir a primer nivel" src="../../../../imagenes/nivel1.png" />
                <img id="imgExpandirTodo" alt="Expandir todo" src="../../../../imagenes/nivel2.png" />
            </div>            

            <div class="col-xs-4">
                <span id="leyenda">... puede ver las evaluaciones que ve ...</span>
            </div>         
        </div>
    </div>

    <div class="container">
        <div class="row">
            <div class="col-xs-10 header col-xs-offset-1">
                <span class="pull-left" style="margin-left: 7px">Visualizador/a agregado/a</span>
                <span id="txtHeaderVisualizados" class="pull-left hide" style="margin-left: 112px">Visualizadores/as</span>                
            </div>
        </div>



        <div class="row">
            <div class="col-xs-10 col-xs-offset-1">

                <!-- Árbol -->
                <div class="tree">
                    <ul id="ulVisionesAjeasArbol" style="padding-left: 0">
                    </ul>
                </div>
                <!-- FIN Árbol -->

            </div>
        </div>


        <div class="row">
            <div class="col-xs-10 col-xs-offset-1">
                <div>
                    <button id="addVisualizador" class="btn btn-primary">Añadir visualizador/a</button>
                    <button id="delVisualizador" class="btn btn-primary" style="margin-left: 7px">Eliminar visualizador/a seleccionado/a</button>
                    <button id="editVisualizador" class="btn btn-primary" style="margin-left: 7px">Modificar visualizador/a seleccionado/a</button>
                </div>
            </div>
        </div>
        
    </div>



     
    <div class="modal fade" id="modal-mantenimiento">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <h4 class="modal-title">Mantenimiento de visualizadores/as agregados/as</h4>
                </div>
                <div class="modal-body">
                    
                    
                    <div class="row">
                       <div id="divEvaluador" runat="server" class="col-xs-10 fk-ocultar">
                                <button id="lblVisualizadorModal" class="btn btn-link" runat="server">Visualizador agregado</button>
                                <span id="spanVisualizador" style="display:none">Visualizador agregado</span>
                                <input id="evaluador" type="text" readonly="readonly"/>
                        </div>
                    </div>
                    <hr />

                    <div class="row">
                        <div id="divtblMant" class="col-xs-12">
                            <ul id="resultadoVisualizados"> </ul>   

                        </div>
                    </div>

                    

                </div>
                <div class="modal-footer">
                    <button class="btn btn-primary pull-left" id="mntVisualizados"  data-toggle="modal">Selección de visualizadores/as</button>
                    <b><button id="btnGrabar" class="btn btn-primary" data-toggle="modal" data-backdrop="static" data-keyboard="false" data-target="#modal-finalizar">Grabar</button></b>
                    <button id="btnCancelarGrabar" type="button" style="margin-left:7px"  class="btn btn-default" data-dismiss="modal">Cancelar</button>                                            
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>



    <div class="modal fade" id="modal-confirmacion-eliminacion">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <h4 class="modal-title">Confirmación de eliminación</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-xs-10 col-xs-offset-1">
                            <span>Si pulsas "Aceptar" se eliminará el profesional visualizador/a agregado/a con todos sus visualizadores/as</span>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <b><button class="btn btn-default pull-right" style="margin-left:10px" data-dismiss="modal">Cancelar</button></b>
                    <b><button id="btnAceptarEliminacion" class="btn btn-primary pull-right">Aceptar</button></b>                    
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->

 <!--MODAL EVALUADORES-->
    <div class="modal fade" id="modal-evaluadores">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <h4 class="modal-title">Selección de visualizador/a</h4>
                </div>
                <div class="modal-body">
                    
                    <div id="txtBusqueda" class="">
                        <div class="pull-left">
                             <label for="lblApellido1">Apellido 1º</label><br />
                                <input id="inputApellido1" type="text" />
                        </div>
                        <div class="pull-left" style="margin-left: 8px">
                             <label for="lblApellido2">Apellido 2º</label><br />
                                <input id="inputApellido2" type="text" />
                        </div>
                        <div class="pull-left" style="margin-left: 8px">
                            <label for="lblNombre">Nombre</label><br />
                            <input id="txtNombre" type="text" />
                        </div>
                        <div class="pull-right">
                            <button id="btnObtener" class="btn btn-primary btn-xs hide">Obtener</button>
                        </div>
                    </div>
                       

                    <br /><br />
                    <div class="row">
                        <div class="col-xs-12">
                            <ul class="list-group" runat="server" id="lisEvaluadores">
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <b><button id="btnSeleccionar" class="btn btn-primary" style="display:none">Seleccionar</button></b>
                    <b><button id="btnCancelar" class="btn btn-default">Cancelar</button></b>                    
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->

    <!--FIN MODAL EVALUADORES-->




    <div class="modal fade" id="modal-seleccionVisualizados">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header btn-primary">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 id="Headermodal-seleccionVisualizados" class="modal-title"></h4>
            </div>
            <div class="modal-body">
                <div id="divVisualizados">
                    <div class="row">
                        <div id="divlisPersonas" class="text-left dual-list list-left col-xs-5">                                                        
                            <h4>Profesionales</h4>
                            <div class="well">
                                <div class="row">
                                    <div class="btn-group col-xs-2">
                                        <a class="btn btn-default selector" data-toggle="popover" data-content="Marcar/desmarcar todos" title="" data-placement="top" >
                                            <i class="glyphicon glyphicon-unchecked"></i>
                                        </a>
                                    </div>
                                    <div class="col-xs-10 input-group">
                                        <input type="text" name="SearchDualList" class="form-control" placeholder="Buscar" />
                                    </div>
                                </div>
                                <ul class="list-group" runat="server" id="lisPersonas">
                                </ul>
                            </div>
                        </div>
                        <div class="list-arrows col-xs-2 text-center">
                            <div class="row">
                                <button class="btn btn-default btn-sm move-right">
                                    <span class="glyphicon glyphicon-chevron-right"></span>
                                </button>
                            </div>
                            <div class="row">
                                <button class="btn btn-default btn-sm move-left">
                                    <span class="glyphicon glyphicon-chevron-left"></span>
                                </button>
                            </div>
                        </div>
                        <div id="divlisVisualizados" class="dual-list list-right col-xs-5">
                            <h4>Visualizadores/as</h4>
                            <div class="well">
                                <div class="row">
                                    <div class="btn-group col-xs-2">
                                        <a class="btn btn-default selector" data-toggle="popover" title="" data-content="Marcar/desmarcar todos" data-placement="top">
                                            <i class="glyphicon glyphicon-unchecked"></i>
                                        </a>
                                    </div>
                                    <div class="col-xs-10 input-group">
                                        <input type="text" name="SearchDualList" class="form-control" placeholder="Buscar" />
                                    </div>
                                </div>
                                <ul class="list-group" runat="server" id="lisVisualizados">
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button id="btnAceptarVisualizados" type="button" class="btn btn-primary">Aceptar</button>
                <button id="btnCancelarVisualizados" type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>                
            </div>
        </div>
    </div>
</div>


</body>
</html>
<cb1:PieMenu runat="server" ID="PieMenu"></cb1:PieMenu>

 
