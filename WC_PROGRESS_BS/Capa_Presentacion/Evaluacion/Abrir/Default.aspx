<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Evaluacion_Abrir_Default" %>

<%@ Register Src="~/uc/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="cb1" %>
<%@ Register Src="~/uc/PieMenu.ascx" TagName="PieMenu" TagPrefix="cb1" %>
<%@ Register Src="~/uc/uc_Session.ascx" TagName="mensajeSession" TagPrefix="uc3" %>
<%@ Register Src="~/uc/HeaderMeta.ascx" TagPrefix="cb1" TagName="HeaderMeta" %>


<!DOCTYPE html>
<html>
<head>
    <cb1:HeaderMeta runat="server" ID="HeaderMeta" />
    <title>Abrir evaluación</title>
    <script type="text/javascript">
        <%=js_miequipo%>
        var strServer = "<%=Session["strServer"]%>";
    </script>
</head>

<body data-codigopantalla="102">
    <uc3:mensajeSession ID="MensajeSession" runat="server" />
    <cb1:CabeceraMenu runat="server" ID="CabeceraMenu"></cb1:CabeceraMenu>

    <br class="hidden-xs" />
    <br class="hidden-xs" />

    <!-- TIMELINE -->
    <div id="timeline" class="container">
        <div class="">
            <div class="">
                <div id="selEvaluados">
                    <img class="" src="../../../imagenes/timelinecolor.png" />
                    <p>Selección de profesionales</p>
                </div>

                <div id="confNotificaciones">
                    <img class="" src="../../../imagenes/timeline.png" />
                    <p>Configuración de notificaciones</p>
                </div>

                <div id="finProceso">
                    <img class="" src="../../../imagenes/timeline.png" />
                    <p>Finalización del proceso </p>
                </div>
            </div>
        </div>
    </div>
    <!-- FIN Timeline -->

    <br />

    <!-- Capa 1 - Selección de evaluados -->
    <div class="container" id="capaPaso1">
        <div id="divMiEquipo">
            <div class="row">
                <div class="text-left dual-list list-left col-xs-5">
                    <h4>Mi equipo</h4>
                    <div class="well padding10">
                        <div class="row">
                            <div class="btn-group col-xs-2">
                                <a class="btn btn-default selector" data-placement="top" data-toggle="popover" title="" data-content="Marcar/desmarcar todo">
                                    <i class="glyphicon glyphicon-unchecked"></i>
                                </a>
                            </div>
                            <div class="col-xs-10 input-group">
                                <input type="text" name="SearchDualList" class="form-control" placeholder="Buscar" />
                            </div>
                        </div>
                        <ul class="list-group" runat="server" id="lisMiEquipo">
                        </ul>
                    </div>



                    <div class="row">
                        <div class="text-left col-xs-12" id="divLeyendas2">
                            <i class="fa fa-file-text-o verde"></i>
                            <span>Evaluación abierta</span>
                            <i style="margin-left: 5px" class="fa fa-file-text-o azul"></i>
                            <span>Evaluación en curso</span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="text-left col-xs-12" id="divLeyendas">
                            <span class="glyphicon glyphicon-new-window"></span>
                            <span>Salida tramitada</span>
                            <i style="margin-left: 15px" class="fa fa-compress"></i>
                            <span>Salida rechazada</span>
                        </div>
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



                <div class="dual-list list-right col-xs-5">
                    <h4>Profesionales a evaluar</h4>
                    <div class="well padding10">
                        <div class="row">
                            <div class="btn-group col-xs-2">
                                <a class="btn btn-default selector" data-toggle="popover" data-placement="top" title="" data-content="Marcar/desmarcar todo">
                                    <i class="glyphicon glyphicon-unchecked"></i>
                                </a>
                            </div>
                            <div class="col-xs-10 input-group">
                                <input type="text" name="SearchDualList" class="form-control" placeholder="Buscar" />
                            </div>
                        </div>
                        <ul class="list-group" runat="server" id="lisAEvaluar">
                        </ul>
                    </div>

                    <div class="row">
                        <div id="divBotonesCapa1" class="pull-right">
                            <div class="row">
                                <div class="" style="margin-right: 10px">
                                    <b>
                                        <button id="btnSiguienteEtapa1" style="margin-left: 7px;" type="button" class="btn btn-primary"><span>Siguiente</span><i class="glyphicon glyphicon-chevron-right"></i></button></b>
                                </div>
                            </div>
                        </div>
                    </div>


                </div>



            </div>


        </div>
    </div>
    <!-- FIN Capa 1 - Selección de evaluados -->





    <div class="container hide" id="capaPaso2">
        <div class="alturacontenedor">
            <table>
                <thead>
                    <tr>
                        <th>Profesional
            <div id="th1">Profesional</div>
                        </th>
                        <th>
                            <i class="glyphicon glyphicon-unchecked" title="Seleccionar (o quitar la selección) a todos los profesionales, para enviarles un correo con el comunicado de apertura de evaluación"></i>
                            <div id="th2"><i class="glyphicon glyphicon-unchecked" title="Seleccionar (o quitar la selección) a todos los profesionales, para enviarles un correo con el comunicado de apertura de evaluación"></i></div>
                        </th>
                        <th>Texto a enviar a los profesionales con marca de envío
            <div id="th3">Texto a enviar a los profesionales con marca de envío</div>
                        </th>
                    </tr>
                </thead>
                <tbody runat="server" id="tbdProfesionales">
                </tbody>
            </table>

        </div>

        <br />
        <div class="row">
            <div id="divBotonesCapa2" class="pull-right">
                <b>
                    <button id="btnAnteriorEtapa2" type="button" class="btn btn-primary"><span><i class="glyphicon glyphicon-chevron-left"></i>Anterior</span></button></b>
                <b>
                    <button id="btnSiguienteEtapa2" style="margin-left: 7px;" type="button" class="btn btn-primary"><span>Siguiente</span><i class="glyphicon glyphicon-chevron-right"></i></button></b>
            </div>
        </div>
    </div>




    <!-- FIN Capa 2 - Configuración de envío de correos -->

    <!-- Capa 3 - Resumen del proceso -->
    <div class="container hide" id="capaPaso3">
        <div class="row">
            <div class="col-xs-12">
                <span>Pulsa el botón 'Finalizar' para que a los profesionales que has seleccionado les llegue tu mensaje de 'apertura de evaluación'. Sobre el icono <i class="fa fa-envelope fa-lg clickable"></i></span> puedes ver el texto que se enviará. Puedes modificarlo volviendo a la pantalla anterior.</span>
                    
                <br />
                <br class="hidden-xs" />

                <br class="hidden-xs" />
                <div class="col-xs-10 col-xs-offset-1">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <span style="margin-right: 20px;">Notificación</span>
                            <span>Evaluado/a</span>
                        </div>
                        <div class="panel-body">

                            <table class="table table-hover">
                                <tbody runat="server" id="tbdResumen">
                                </tbody>
                            </table>


                        </div>
                    </div>
                </div>

                <br />

                <div class="row">
                    <div class="col-xs-10 col-xs-offset-1">
                        <div id="divBotonesCapa3" class="pull-right">
                            <b>
                                <button id="btnAnteriorEtapa3" type="button" class="btn btn-primary"><span><i class="glyphicon glyphicon-chevron-left"></i>Anterior</span></button></b>
                            <b>
                                <button id="btnFinalizarEtapa3" style="margin-left: 7px;" type="button" class="btn btn-primary"><span>Finalizar</span></button></b>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <!-- FIN Capa 3 - Resumen del proceso -->

    <!-- MODAL IMPRIMIR -->

    <div class="modal fade" id="modal-imprimir">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <h4 class="modal-title">Imprimir formulario de evaluación</h4>
                </div>
                <div class="modal-body">
                    <div class="row">

                        <p>
                            Puedes imprimir los formularios en blanco de los profesionales a los que has enviado la notificación, o seleccionar otros formularios manualmente (uno a uno o de forma masiva). Pulsa el botón 'continuar' para salir de esta ventana.
                        </p>

                        <p>
                            <span id="txtAvisoIncidente" style="color: red; display: none">No se han abierto evaluaciones a todos los profesionales seleccionados, por existir evaluaciones no cerradas para ellos. 
                            </span>
                        </p>
                        <br />
                    </div>
                    <div class="row">
                        <div class="btn-group col-xs-1 col-xs-offset-2">
                            <a class="btn btn-default selector" data-toggle="popover" title="" data-placement="top" data-content="Marcar/desmarcar todo">
                                <i class="glyphicon glyphicon-unchecked"></i>
                            </a>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-8 col-xs-offset-2">
                            <ul class="list-group" runat="server" id="lisImpEvaluados">
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <b>
                        <button style="margin-left: 10px;" class="btn btn-primary pull-right" data-toggle="modal" data-dismiss="modal" data-backdrop="static" data-keyboard="false" data-target="#modal-finalizar">Continuar</button></b>
                    <b>
                        <button id="btnImprimir" class="btn btn-primary pull-right">Imprimir</button></b>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->

    <!--FIN MODAL IMPRIMIR-->

    <!-- MODAL FINALIZAR -->

    <div class="modal fade" id="modal-finalizar">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <h4 class="modal-title">Finalización del proceso</h4>
                </div>
                <div class="modal-body">
                    <p>Has finalizado el proceso de apertura de evaluaciones. Ahora, si lo deseas, puedes completarlas pulsando el botón "Completar evaluaciones abiertas". También puedes hacerlo en otra ocasión, en este caso, pulsa el botón "Cerrar".</p>
                </div>
                <div class="modal-footer">
                    <b>
                        <button class="btn btn-primary">Completar evaluaciones abiertas</button></b>
                    <b>
                        <button style="margin-left: 7px" class="btn btn-default">Cerrar</button></b>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->
    
    <!--FIN MODAL FINALIZAR-->

</body>
</html>
<cb1:PieMenu runat="server" ID="PieMenu"></cb1:PieMenu>

