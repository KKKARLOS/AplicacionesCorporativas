<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Administracion_Estadisticas_Default" %>

<%@ Register Src="~/uc/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="cb1" %>
<%@ Register Src="~/uc/HeaderMeta.ascx" TagName="HeaderMeta" TagPrefix="cb1" %>
<%@ Register Src="~/uc/PieMenu.ascx" TagName="PieMenu" TagPrefix="cb1" %>
<%@ Register  Src="~/uc/uc_Session.ascx" TagName="mensajeSession" TagPrefix="uc3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <cb1:HeaderMeta runat="server" ID="HeaderMeta"></cb1:HeaderMeta>    
    <title></title>      
    <link rel="Stylesheet" href="../../../js/plugins/jQueryUI/jquery-ui.css"/>
    <style>
         .ui-datepicker-calendar {
            display: block;
        }

        .calendar-off table.ui-datepicker-calendar {
            display: none !important;
        }

        .ui-widget-header {       
            border: 1px solid rgb(80,132,159);
            background: rgb(80,132,159);
            color: #666;
        }

    </style>
    <!--Pasar a cliente el idficepi conectado (obtención de las estadísticas en base a él)-->
    <script>
        <%=idficepi%>
        <%=nombre%>     
        <%=sexo%> 
        <%=origen%> 
        <%=defectoAntiguedad%> 
        
    </script>
</head>

<body data-codigopantalla="104">
    <uc3:mensajeSession ID="MensajeSession" runat="server" />
    <cb1:CabeceraMenu runat="server" ID="CabeceraMenu"></cb1:CabeceraMenu>
    
    <br class="hidden-xs" />
    <br class="hidden-xs" />

    <form runat="server"></form>    
    <%--<button id="btnExcel">Excel</button>--%>

     <div class="container" style="margin-top:-5px">
        <div class="row">
            <div class="col-xs-12">
                <fieldset class="fieldset">
                    <legend class="fieldset"><span class="spanFiltros4">Filtros </span><button class="btn-xs btn-primary pull-right" id="btnRestablecer">Restablecer</button></legend>

                    <!--Contenido filtros-->
                    <div id="filtros">
                        <!--Fila 1-->
                        <div class="row">

                        <div class="col-xs-7">
                            <fieldset class="fieldset">
                                <legend class="fieldset"><span class="spanFiltros">Período</span></legend>
                                <div class="col-xs-6">
                                    <select id="selMesIni" runat="server" onchange="cargarEstadisticas()"></select>
                                    <select id="selAnoIni" runat="server" onchange="cargarEstadisticas()"></select>
                                </div>
                                <div class="col-xs-6">
                                    <select id="selMesFin" runat="server" onchange="cargarEstadisticas()"></select>
                                    <select id="selAnoFin" runat="server" onchange="cargarEstadisticas()"></select>
                                </div>
                            </fieldset>
                        </div>
                        
                            
                        <div class="col-xs-5">

                            <div id="divcboFantiguedad" runat="server" class="fk-ocultar">
                                <span>Antigüedad de referencia</span>
                                 <select id="cboAntRef" runat="server" onchange="cargarEstadisticas()">
                                    <option value="1">Más de un año</option>
                                    <option value="2">Más de dos años</option>
                                    <option value="3">Más de tres años</option>                                    
                                    <option value="4">Más de cuatro años</option>                                    
                                    <option value="5">Más de cinco años</option>                                    
                                </select>    
                            </div>

                            <div id="divtxtFantiguedad" runat="server" class="fk-ocultar">
                                <span>Antigüedad de referencia</span>
                               <input type="text" runat="server" id="txtFantiguedad" name="to" readonly="readonly" onchange="cargarEstadisticas()"/>                                              
                            </div>

                            <div class="clearfix"></div>
                              <div id="divNivel" class="pull-right">
                                <span>Nivel de dependencia</span>

                                 <select id="cboProfundizacion" runat="server" onchange="cargarEstadisticas()">
                                    <option value="1">Primer nivel</option>
                                    <option value="2">Hasta segundo nivel</option>
                                    <option value="3">Todos los niveles</option>                                    
                                </select>    
                            </div>

                            <div class="clearfix"></div>
                            <div id="divColectivo" class="pull-right">
                                <span>Colectivo</span>
                                 <select id="cboColectivo" maxlength="50" runat="server" onchange="cargarEstadisticas()">
                                     <option value="" >Todos</option>                                   
                                </select>              
                            </div>
                        </div>

                        </div>

                        <!--FIN Fila 1-->

                        <div class="row">
                             <div id="divEvaluador" runat="server" class="col-xs-6 fk-ocultar">
                                <button id="lblEvaluador" class="btn btn-link" runat="server"></button>
                                <input id="evaluador" type="text" readonly="readonly"/>
                            </div>
                        </div>

                        <div class="row">
                            <div id="divSituacion" runat="server" class="col-xs-6">
                                <span>Situación</span>
                                <select id="cboSituacion" maxlength="50" runat="server" onchange="cargarEstadisticas()">
                                    <option class="fk_fotoActual" value="">Actual</option>
                                </select>
                            </div>
                        </div>

                        <!-- FIN Filtros-->
                    </div>
                </fieldset>
            </div>
        </div>
    </div>

    <div class="container">
        <div>
            <table id="tblHeader">
                <thead>
                    <tr>
                        <th class="w200"></th>
                        <th style="width:123px"><i class="fa fa-male fa-lg"></i></th>
                        <th style="width:98px"><i class="fa fa-female fa-lg"></i></th>
                        <th style="width:103px"><i class="fa fa-male fa-lg"></i><i class="fa fa-female fa-lg"></i></th>
                        <th class="separador2"></th>

                        <th style="width:106px" class="naranja"><i class="fa fa-male fa-lg"></i></th>
                        <th style="width:99px" class="naranja"><i class="fa fa-female fa-lg"></i></th>
                        <th style="width:103px" class="naranja"><i class="fa fa-male fa-lg"></i><i class="fa fa-female fa-lg"></i></th>
                        <th></th>
                    </tr>
                 </thead>
            </table>
        </div>

        <div class="row">
            <div class="col-xs-12">
                <div id="panelProfesionales" class="panel panel-primary">
                    <div class="panel-cabeceraizquierda">
                        <h3 id="h3Profesionales" class="panel-tituloizquierda">Profesionales</h3>
                        <h3 id="h3Dependientes" class="panel-tituloizquierda">dependientes</h3>                        
                    </div>
                    <div class="panel-contenido">

                        <div class="pull-left" style="padding-left:0; padding-right:0;width:212px">
                            <table style="margin-bottom:0" class="table">
                            <tbody>
                                <tr>                                    
                                    <td><a href="#" data-toggle="popover" data-placement="top" title="Información" data-content="Profesionales dependientes del evaluador/a indicado en el filtro, que tienen evaluaciones firmadas por su evaluador/a en el período."><i class="glyphicon glyphicon-info-sign"></i></a>Evaluados</td>
                                </tr>
                                <tr>
                                    <td><a href="#" data-toggle="popover" data-placement="top" title="Información" data-content="Profesionales dependientes del evaluador/a indicado en el filtro, que no tienen evaluaciones firmadas por su evaluador/a en el período."><i class="glyphicon glyphicon-info-sign"></i></a>No evaluados</td>
                                </tr>
                                <tr>
                                    <td>Total</td>
                                </tr>
                            </tbody>

                        </table>
                        </div>
                        
                        <div class="pull-left" style="width:620px">
                            <table id="tblProfesionales" class="table">                         
                           
                        </table>
                        </div>
                        
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>

    <br />

    <div class="container">
        <div class="row">
            <div class="col-xs-12">
                <div id="panelEvaluaciones" class="panel panel-primary">
                    <div class="panel-cabeceraizquierda">
                        <h3 class="panel-tituloizquierda">Evaluaciones</h3>
                    </div>
                  <div class="panel-contenido">

                       <div class="pull-left" style="padding-left:0; padding-right:0;width:212px">
                            <table style="margin-bottom:0" class="table">
                            <tbody>
                                <tr>

                                    <td><a href="#" data-toggle="popover" data-placement="top" title="Información" data-content="Número de evaluaciones abiertas en el período indicado en el filtro por el evaluador/a seleccionado, y que todavía no han firmado."><i class="glyphicon glyphicon-info-sign"></i></a>Abiertas</td>
                                    
                                </tr>
                                <tr>
                                    <td><a href="#" data-toggle="popover" data-placement="top" title="Información" data-content="Número de evaluaciones abiertas por el evaluador/a indicado en el filtro, y que ha firmado en el período seleccionado."><i class="glyphicon glyphicon-info-sign"></i></a>En curso</td>
                                </tr>
                                <tr>
                                    <td>Cerradas</td>
                                </tr>

                                <tr>
                                    <td style="padding-left:30px !important;"><a href="#" data-toggle="popover" data-placement="top" title="Información" data-content="Número de evaluaciones abiertas por el evaluador/a indicado en el filtro, y que habiéndolas firmado en el período seleccionado, han sido firmadas por el evaluado/a."><i class="glyphicon glyphicon-info-sign"></i></a>Firmadas</td>
                                </tr>

                                <tr>
                                    <td style="padding-left:30px !important;"><a href="#" data-toggle="popover" data-placement="top" title="Información" data-content="Número de evaluaciones abiertas por el evaluador/a indicado en el filtro, y que habiéndolas firmado en el período seleccionado y transcurrido 15 días desde dicha firma, no han sido firmadas por el evaluado/a."><i class="glyphicon glyphicon-info-sign"></i></a>No firmadas</td>
                                </tr>

                                <tr>
                                    <td>Total</td>
                                </tr>


                            </tbody>

                        </table>
                        </div>



                      <div class="pull-left" style="width: 620px">
                          <table id="tblEvaluaciones" class="table">
                          </table>
                      </div>

                    </div>
                    <div class="clearfix">
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-xs-6" style="margin-top: -5px;">
                 <i class="fa fa-male fa-lg"></i><i class="fa fa-female fa-lg"></i>
                <span>Profesionales sin tener en cuenta antigüedad</span><br />
                <i class="fa fa-male fa-lg naranja"></i><i class="fa fa-female fa-lg naranja"></i>
                <span>Profesionales con antigüedad superior a la seleccionada</span>

            </div>


            <div id="divbtnFoto" class="col-xs-5 col-xs-offset-1">                
                <button id="btnExportarPDF" class="btn btn-primary pull-right">Exportar PDF</button>
                <button id="btnFoto" runat="server" style="margin-right:10px" class="btn btn-primary pull-right">Fotos</button>
                    
            </div>
        </div>
    </div>

    <!--MODAL EVALUADORES-->
    <div class="modal fade" id="modal-evaluadores">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <h4 class="modal-title">Selección de evaluador/a</h4>
                </div>
                <div class="modal-body">

                    <div id="txtBusqueda" class="row">
                        <div class="col-xs-3">
                             <label for="lblApellido1">Apellido 1º</label>
                                <input id="txtApellido1" type="text" />
                        </div>
                        <div class="col-xs-3" style="margin-left: 8px">
                             <label for="lblApellido2">Apellido 2º</label>
                                <input id="txtApellido2" type="text" />
                        </div>
                        <div class="col-xs-3" style="margin-left: 8px">
                            <label for="lblNombre">Nombre</label>
                            <input id="txtNombre" type="text" />
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

    <!-- Catálogo de fotos -->
    <div class="modal fade" id="modal-foto">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <h4 class="modal-title">Catálogo de fotos</h4>
                </div>
                <div class="row">
                    <div class="col-xs-12">
                        <div id="panelFotos" class="panel">
                            <div class="panel-body">
                                <table id="headertblFotos" class="table table-hover">
                                    <thead>
                                        <tr>
                                            <th>Nombre</th>
                                            <th>Fecha</th>
                                            <th>Autor/a</th>
                                        </tr>
                                    </thead>
                                    <tbody runat="server" id="tbdFoto">
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="modal-footer">
                    <b>
                        <button id="btnInsertarFoto" class="btn btn-primary">Sacar nueva</button></b>
                    <b>
                        <button id="btnBorrarFoto" style="margin-left:7px" class="btn btn-primary">Eliminar</button></b>
                    <b>
                        <button id="btnSalir" style="margin-left:7px" class="btn btn-default">Cerrar</button></b>
                </div>
            </div>
        </div>
    </div>
    
     <!-- Modal confirmación eliminar foto -->
    <div class="modal fade" id="modal-EliminarFoto" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">                    
                    <h4 class="modal-title">Confirmación de eliminación</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-xs-12">
                            <h5 id="txtConfirmacionEliminacion" class="cajaH5"></h5>
                        </div>                        
                    </div>

                </div>
                <div class="modal-footer">
                    <button type="button" id="btnConfirmacionEliminacion" class="btn btn-primary">Confirmar eliminación</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>




    <!-- FIN Capa 1 - Catálogo de fotos -->


    <!--MODAL Detalle Foto-->
    <div class="modal fade" id="modal-foto-i">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <h4 class="modal-title">Sacar foto</h4>
                </div>
                
                <div class="modal-body">
                    <div class="col-xs-12">
                        <label for="lblFoto">Denominación</label>
                        <input id="txtNombreFoto"  maxlength="50" type="text" placeholder="Introduce la denominación para la nueva foto" />
                    </div>
                </div>

                <br /><br /><br />

                <div class="modal-footer">
                    <b><button id="btnAceptarFoto" class="btn btn-primary">Grabar</button></b>
                    <b><button id="btnCancelarFoto" class="btn btn-default">Cancelar</button></b>                    
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->

    <!--FIN MODAL Detalle Foto-->

    
   <iframe id="ifrExportacion" name="ifrExportacion" frameborder="no" width="10px" height="1px" style="visibility:hidden" ></iframe>	   
</body>


</html>

<cb1:PieMenu runat="server" ID="PieMenu"></cb1:PieMenu>


<script type="text/javascript" src="js/models.js"></script>



