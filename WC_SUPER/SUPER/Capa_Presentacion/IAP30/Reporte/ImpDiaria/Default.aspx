<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Reporte_ImpDiaria_Default" %>

<%@ Register Src="~/Capa_Presentacion/bsUserControls/Menu/Menu.ascx" TagPrefix="cb1" TagName="Menu" %>
<%@ Register Src="~/Capa_Presentacion/bsUserControls/Head.ascx" TagPrefix="cb1" TagName="Head" %>



<!DOCTYPE html>

<html lang="es" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">    
    <cb1:Head runat="server" id="Head" />
    <title>::: SUPER ::: - Imputación diaria</title>
    <link rel="Stylesheet" href="<% =Session["strServer"].ToString() %>css/jquery-ui.min.css"/>
</head>

<body data-codigopantalla="120">     
    <script>
        var opciones = { delay: 1 }
        IB.procesando.opciones(opciones);
        IB.procesando.mostrar();
    </script>
    <cb1:Menu runat="server" id="Menu" />

        
        <form id="form1" runat="server"></form>
        <div class="container-fluid ocultable">
            <h1 class="sr-only">::: SUPER ::: - Imputación diaria</h1>
            <div id="cambioProf"  title="Imputación en nombre de otro profesional" runat="server" style="margin-bottom:5px;"><span id="icoProf" class="fa fa-user fa-2x"></span> <span class="txtIcono" role="link" aria-label="Imputación en nombre de otro profesional" tabindex="0" id="spProfesional"><%=Session["DES_EMPLEADO_IAP"]%></span></div>
           
            <div id="botonera" class="ibox-title-principal ibox-title hidden-xs hidden-sm">
                    <div class="hidden-xs hidden-sm text-center">
                        <input type="checkbox" id="chkMostrarBotonera" />
                        <label for="chkMostrarBotonera">
                            <i class="fa"></i>
                        </label>
                        <div>
                            <h2 class="sr-only">Opciones de pantalla</h2>
                            <ul class="listaBotones">
                                <li>                                    
                                    <span class="fa-stack botonDeshabilitado" title="Comentario" id="btnComentario">
                                        <i class="fa fa-comment-o fa-stack-2x"></i>
                                    </span>
                                    <span class="txtclass botonDeshabilitado">Comentario</span>
                                </li>
                                <li>
                                    <span class="fa-stack botonDeshabilitado" title="Detalle de tarea" id="btnTarea">
                                        <i class="fa fa-file-text-o fa-stack-2x"></i>
                                    </span>
                                    <span class="txtclass botonDeshabilitado">Tarea</span>
                                </li>
                                <li>
                                    <span class="fa-stack botonDeshabilitado" title="Imputar jornada" id="btnJornada">
                                        <i class="fa fa-clock-o fa-stack-2x"></i>
                                    </span>
                                    <span class="txtclass botonDeshabilitado">Jornada</span>
                                </li>
                                <li>
                                    <span class="fa-stack botonDeshabilitado" title="Imputar semana completa" id="btnSemana">
                                        <i class="fa fa-calendar fa-stack-2x"></i>
                                    </span>
                                    <span class="txtclass botonDeshabilitado">Semana</span>
                                </li>
                                <li>
                                    <span class="fa-stack botonDeshabilitado" title="Grabar" id="btnGrabar">
                                        <i class="fa fa-floppy-o fa-stack-2x"></i>
                                    </span>
                                    <span class="txtclass botonDeshabilitado">Grabar</span>
                                </li>
                                <li>
                                    <span class="fa-stack botonDeshabilitado" title="Grabar e ir a la semana siguiente" id="btnGrabarSig">
                                        <i class="fa fa-floppy-o fa-stack-2x"></i>
                                        <i class="fa fa-arrow-circle-right fa-stack-1x overlay-upper-right"></i>
                                    </span>
                                    <%--<span class="txtclass"><abbr title="Grabar siguiente">Grabar sig.</abbr></span>--%>
                                    <span class="txtclass botonDeshabilitado">Grabar sig.</span>
                                </li>
                                <li>
                                    <span class="fa-stack botonDeshabilitado" title="Grabar y regresar" id="btnGrabarRegresar">
                                        <i class="fa fa-floppy-o fa-stack-2x"></i>
                                        <i class="fa fa-arrow-circle-left fa-stack-1x overlay-upper-right"></i>
                                    </span>
                                    <%--<span class="txtclass"><abbr title="Grabar y regresar">Grabar...</abbr></span>--%>
                                    <span class="txtclass botonDeshabilitado">Grabar...</span>
                                </li>                                
                                <li>
                                    <span class="fa-stack boton" title="Regresar" id="btnSalir" tabindex="0" role="link">
                                        <i class="fa fa-share-square-o-rev fa-stack-2x"></i>
                                    </span>
                                    <span class="txtclass">Regresar</span>
                                </li>
                            </ul>
                        </div>
                    </div>
                    <!--Limpiamos los floats-->
                    <div class="clearfix"></div>
                </div>
            <div class="ibox-content blockquote blockquote-info">
            

            <div class="panel-group">
                
                
                <div id="tipoImputacion" class="ibox-content">                    
                    <div class="row">
                        <h2 class="sr-only">Opciones de navegación</h2>
                        <div id="botoneraInferior" class="col-xs-12 col-md-3">
                            <div id="botonesApertura" class="btn-group btn-group-xs" role="group" aria-label="Botones de apertura de niveles del arbol de imputaciones">
                                <button id="nivel1" data-level="1" aria-label="nivel 1" title="Proyectos económicos" type="button" class="btn btn-default btn-blanco">
                                    <span id="barra1" class="fa fa-square fa-square-2 nivelVerde"></span>
                                </button>
                                <button id="nivel2" data-level="2" aria-label="nivel 2" title="Proyectos técnicos" type="button" class="btn btn-default btn-blanco">
                                    <span id="barra2" class="fa fa-square fa-square-3 nivelGris"></span>
                                </button>
                                <button id="nivel3" data-level="3" aria-label="nivel 3" title="Tareas" type="button" class="btn btn-default btn-blanco">
                                    <span id="barra3" class="fa fa-square fa-square-4 nivelGris"></span>
                                </button>
                            </div>
                            <span id="icoBomb" title="Despliegue completo de la línea seleccionada" data-level="3" role="button" aria-label="Desplegar los niveles inferiores de la fila seleccionada." class="fa fa-bomb fa-1-5x link" tabindex="0"></span>
                            <!--<span id="btnBitacora" role="link" title="Abrir bitácora" class="fa fa-anchor fa-2x link" tabindex="0"></span>-->
                            <img id="btnBitacora" class="fa-anchor"/>
                            <span id="btnGasviLite" runat="server" role="link" title="Abrir GASVI" class="fa fa-plane fa-2x link hidden-md hidden-lg"></span>                            
                        </div>
                        <div class="col-xs-12 col-md-6 text-center text-nowrap" style="margin-bottom: 5px;">
                            <span id="semAnt" class="fa fa-arrow-left fa-2x" ></span>
                            <span class="txtSemana"></span>
                            <span class="txtSemanaMini"></span>
                            <span id="semSig" class="fa fa-arrow-right fa-2x" ></span>
                            <div id="divTraza" runat="server" visible="false" style="float: right">
                                <div style="text-align: right;"><span>Tiempo de servidor: </span><span id="ContadorServ"></span></div>
                                <div><span>Tiempo de renderizado: </span><span id="ContadorRender"></span></div>
                            </div>
                        </div>                        
                        <div class="col-md-3" style="margin-bottom: 5px; padding-right: 0px;">
                            <button id="btnGasvi" runat="server" type="button" title="Abrir GASVI" role="link" class="btn btn-primary visible-md visible-lg">
                                <span class="fa fa-plane-btn fa-1x"></span>
                                <span>Crear solicitud GASVI</span>
                            </button>
                        </div>
                    </div>

                    <%--tabla hasta el 26/09/2016--%>
                    <div class="row">
                        <div class="table-responsive" role="grid" aria-readonly="true">
                            <div class="div-table">
                                <table id="tablaCabecera" role="presentation" class="table table-bordered table-no-border table-hover table-condensed" summary="Tabla de cabecera">
                                    <thead class="cabeceraFija">
                                        <tr id="headTabla" role="row">
                                            <th id="DEN" class="cabecera bg-primary" role="columnheader">
                                                <div>                                                    
                                                    <i id="btnBusqueda" class="fa fa-search" aria-hidden="true"></i> Denominación
                                                    <span id="divBusqueda"><i id="btnBuscarSiguiente" class="fa fa-search-plus" aria-hidden="true"></i></span>
                                                </div>
                                             </th>
                                            <th id="L" class="cabecera bg-primary" role="columnheader">
                                                <abbr title="">L. </abbr>
                                            </th>
                                            <th id="M" class="cabecera bg-primary" role="columnheader">
                                                <abbr title="">M. </abbr>
                                            </th>
                                            <th id="X" class="cabecera bg-primary" role="columnheader">
                                                <abbr title="">X. </abbr>
                                            </th>
                                            <th id="J" class="cabecera bg-primary" role="columnheader">
                                                <abbr title="">J. </abbr>
                                            </th>
                                            <th id="V" class="cabecera bg-primary" role="columnheader">
                                                <abbr title="">V. </abbr>
                                            </th>
                                            <th id="S" class="cabecera bg-primary" role="columnheader">
                                                <abbr title="">S. </abbr>
                                            </th>
                                            <th id="D" class="cabecera bg-primary" role="columnheader">
                                                <abbr title="">D. </abbr>
                                            </th>
                                            <th id="OTC" class="cabecera bg-primary" role="columnheader">
                                                <abbr title="Órdenes de trabajo codificadas" >OTC</abbr></th>
                                            <th id="OTL" class="cabecera bg-primary" role="columnheader">
                                                <abbr title="Órdenes de trabajo libres" >OTL</abbr></th>
                                            <th id="ETE" class="cabecera bg-primary" role="columnheader">
                                                <abbr title="Esfuerzo total estimado">E.T.E.</abbr></th>
                                            <th id="FFE" class="cabecera bg-primary" role="columnheader">
                                                <abbr title="Fecha de fin estimada">F.F.E.</abbr></th>
                                            <th id="F" class="cabecera bg-primary" role="columnheader">
                                                <abbr title="Tarea finalizada">Fin</abbr></th>
                                            <th id="AET" class="cabecera bg-primary" role="columnheader">
                                                <abbr title="Esfuerzo acumulado total">E.A.T.</abbr></th>
                                            <th id="EP" class="cabecera bg-primary" role="columnheader">
                                                <abbr title="Esfuerzo pendiente según estimación">E.P.</abbr></th>
                                        </tr>
                                    </thead>
                                </table>
                            </div>

                            <div id="contenedor" class="div-table-content">
                                <h2 class="sr-only">Tabla de imputaciones</h2>
                                <table id="tabla" role="presentation" class="table table-bordered table-no-border table-hover table-condensed" summary="Tabla de imputaciones">                                    
                                    <tbody id="bodyTabla" class="cabeceraFija">                                        
                                    </tbody>                                    
                                </table>
                            </div>
                            <div class="div-table">
                                <table id="tablaPie" role="presentation" class="table table-bordered table-no-border table-hover table-condensed" summary="Tabla de pie">
                                    <tfoot class="cabeceraFija">
                                        <tr id="pieTabla" role="row" data-level="0" class="bg-info" data-id="pieTabla" data-parent="">
                                            <td role="gridcell" headers="DEN" class="PieDEN celdaTotal"><span>&nbsp;</span><span class="sr-only">Totales</span></td>
                                            <td role="gridcell" headers="L" class="PieL celdaTotal diasTotal"><span></span></td>
                                            <td role="gridcell" headers="M" class="PieM celdaTotal diasTotal"><span></span></td>
                                            <td role="gridcell" headers="X" class="PieX celdaTotal diasTotal"><span></span></td>
                                            <td role="gridcell" headers="J" class="PieJ celdaTotal diasTotal"><span></span></td>
                                            <td role="gridcell" headers="V" class="PieV celdaTotal diasTotal"><span></span></td>
                                            <td role="gridcell" headers="S" class="PieS celdaTotal diasTotal"><span></span></td>
                                            <td role="gridcell" headers="D" class="PieD celdaTotal diasTotal"><span></span></td>
                                            <td role="gridcell" headers="OTC" class="PieOTC celdaTotal"><span></span></td>
                                            <td role="gridcell" headers="OTL" class="PieOTL celdaTotal"><span></span></td>
                                            <td role="gridcell" headers="ETE" class="PieETE celdaTotal"><span></span></td>
                                            <td role="gridcell" headers="FFE" class="PieFFE celdaTotal"><span></span></td>
                                            <td role="gridcell" headers="F" class="PieF celdaTotal"><span></span></td>
                                            <td role="gridcell" headers="AET" class="PieAET celdaTotal"><span></span></td>
                                            <td role="gridcell" headers="EP" class="PieEP celdaTotal"><span></span></td>
                                        </tr>                                       
                                    </tfoot>
                                </table>                            
                            </div>
                            <div id="leyendas" aria-hidden="true" class="leyenda">
                                <span class="fa fa-diamond fa-diamond-verde"></span><span> Abierto </span>
                                <span class="fa fa-diamond fa-diamond-rojo"></span><span> Cerrado </span>
                                <input class="form-control vacaciones txtLeyenda" disabled="disabled" /><span> Vacaciones </span> 
                                <input class="form-control noLaborable txtLeyenda" disabled="disabled" /><span> Festivos </span>
                                <input class="form-control fueraProyecto txtLeyenda" disabled="disabled" /><span> Fuera de proyecto </span>
                                <input class="form-control fueraVigencia txtLeyenda" disabled="disabled" /><span> Fuera de vigencia </span>
                                <input class="form-control comentario txtLeyenda" disabled="disabled" /><span> Comentarios </span>
                                <span class="obligaest spanLeyenda">Estimación obligatoria</span>
                                <span class="cerrada spanLeyenda">Cerrada</span>
                                <span class="finalizada spanLeyenda">Finalizada</span>
                                <%--<span class="paralizada spanLeyenda">Paralizada</span>--%>
                                <%--<span class="pendiente spanLeyenda">Pendiente</span>--%>
                            </div>
                        </div>
                    </div>                    

                </div>

                <div id="pieTarea" class="hidden-md hidden-lg">
                    <div class="col-xs-2 col-xs-offset-1 col-pie no-padding">
                        <button id="btnTareaLite" type="button" class="botonDeshabilitado" title="Detalle de tarea">
                            <span class="fa fa-file-text-o fa-2x fa-blanco" style="vertical-align: middle;"></span>
                            <br/>
                            <span>Tarea</span>
                        </button>
                    </div>
                    <div class="col-xs-2 col-pie no-padding">
                        <button id="btnJornadaLite" type="button" class="botonDeshabilitado" title="Imputar jornada">
                            <span class="fa fa-clock-o fa-2x fa-blanco" style="vertical-align: middle;"></span>
                            <br/>
                            <span>Jornada</span>
                        </button>
                    </div>
                    <div class="col-xs-2 col-pie no-padding">
                        <button id="btnSemanaLite" type="button" class="botonDeshabilitado" title="Imputar jornada completa">
                            <span class="fa fa-calendar fa-2x fa-blanco" style="vertical-align: middle;"></span>
                            <br/>
                            <span>Semana</span>
                        </button>
                    </div>
                    <div class="col-xs-2 col-pie no-padding">
                        <button id="btnGrabarLite" type="button" class="botonDeshabilitado" title="Grabar">
                            <span class="fa fa-floppy-o fa-2x fa-blanco" style="vertical-align: middle;"></span>
                            <br/>
                            <span>Grabar</span>
                        </button>
                    </div>
                    <div class="col-xs-2 col-pie no-padding">
                        <button id="btnSalirLite" type="button" class="boton" title="Regresar">
                            <span class="fa fa-share-square-o-rev fa-2x fa-blanco" style="vertical-align: middle;"></span>
                            <br/>
                            <span>Regresar</span>
                        </button>
                    </div>
                </div>
                <%--Div oculto para que el scroll horizontal de la tabla quede por encima de la barra fija de botones--%>
                <div id="pieTareaOculto" class="hidden-md hidden-lg">                    
                </div>
            </div>
            </div>
        </div>

        <!-- Modal de comentario -->
        <div class="modal fade" id="modalComentario" role="dialog" tabindex="-1" title=":::SUPER:::">
            <div class="modal-dialog modal-md" role="dialog">
                <div class="modal-content">
                    <div class="modal-header bg-primary">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar"><span aria-hidden="true">&times;</span></button>
                        <h1 class="modal-title" id="tituloModalComentario">:::SUPER:::</h1>
                    </div>
                    <div class="modal-body">
                        <div class="row row-modal">
                            <div class="col-xs-12">
                                <%-- La cantidad máxima de caracteres permitida es 7500 --%>
                                <textarea class="form-control txtArea" id="txtComentario" name="" rows="10"></textarea>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <b><button id="btnAceptarComentario" class="btn btn-primary" data-dismiss="modal">Aceptar</button></b>                    
                        <b><button id="btnCancelarComentario" class="btn btn-default" data-dismiss="modal">Cancelar</button></b>                    
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div> 

    <!-- Modal de Búsqueda -->
        <div class="modal fade" id="modalBusqueda" role="dialog" tabindex="-1" title=":::SUPER:::">
            <div class="modal-dialog modal-md" role="dialog">
                <div class="modal-content">
                    <div class="modal-header bg-primary">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar"><span aria-hidden="true">&times;</span></button>
                        <h1 class="modal-title" id="tituloModalBusqueda">:::SUPER::: Buscar (Introduce la cadena de búsqueda y pulsa Intro)</h1>
                    </div>
                    <div class="modal-body">
                        <div class="row row-modal">
                            <div class="col-xs-12">
                                <%-- La cantidad máxima de caracteres permitida es 7500 --%>
                                <input type="text" class="form-control" id="txtSearch" />
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">                
                        <b><button id="btnCancelarBuscar" class="btn btn-default" data-dismiss="modal">Cancelar</button></b>                    
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>

     <!-- Modal de detalle de tarea -->
        <div class="modal fade ocultable2" id="modalDetalleTarea" role="dialog" tabindex="-1" title=":::SUPER::: - Detalle de tarea">
            <div class="modal-dialog modal-lg" role="dialog">
                <div class="modal-content">
                    <div class="modal-header bg-primary">
                        <button id="btnCierreModalTarea" type="button" class="close" aria-label="Cerrar"><span aria-hidden="true">&times;</span></button>
                        <h1 class="modal-title">::: SUPER ::: - Detalle de tarea</h1>
                    </div>
                    <div class="modal-body">
                        <ul id="tabs" class="nav nav-tabs" data-tabs="tabs" role="tablist">
                            <li class="active"><a href="#general" title="General" data-toggle="tab" role="tab" aria-expanded="true">General</a></li>
                            <li><a href="#docu" id="pestanaDocu" title="Documentación" data-toggle="tab" role="tab" aria-expanded="false">Documentación</a></li>
                            <li><a href="#notas" data-toggle="tab" title="Notas" role="tab" aria-expanded="false">Notas</a></li>
                        </ul>            
                        <div id="my-tab-content" class="tab-content">
                            <div class="tab-pane active" id="general"> 
                                <div class="ibox-content">
                                <form class="form-horizontal collapsed in"  role="form" accept-charset="iso-8859-15">                              
                                    <div class="ibox-title-first ibox-title">
                                        <span class="text-primary">Datos generales</span>                                        
                                      </div>
                                    <div id="datosGenerales" class="ibox-content collapsed in">   
                                        <div class="form-group">                                
                                            <div class="col-xs-12 col-md-6">
                                                <div class="form-group">
                                                    <label for="txtProy" class="col-xs-12 col-md-4 control-label fk-label">Proyecto</label>
                                                    <div class="col-xs-12 col-md-8">
                                                        <input id="txtProy" name="" type="text" class="form-control input-md" value="" disabled="disabled" aria-readonly="true" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="txtProyTec" class="col-xs-12 col-md-4 control-label fk-label">Proyecto técnico</label>
                                                    <div class="col-xs-12 col-md-8">
                                                        <input id="txtProyTec" name="" type="text" class="form-control input-md" value="" disabled="disabled" aria-readonly="true"/>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12 col-md-6">
                                                <div class="form-group">
                                                    <label for="txtFase" class="col-xs-12 col-md-4 control-label fk-label">Fase</label>
                                                    <div class="col-xs-12 col-md-8">
                                                        <input id="txtFase" name="" type="text" class="form-control input-md" value="" disabled="disabled" aria-readonly="true"/>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="txtAct" class="col-xs-12 col-md-4 control-label fk-label">Actividad</label>
                                                    <div class="col-xs-12 col-md-8">
                                                        <input id="txtAct" name="" type="text" class="form-control input-md" value="" disabled="disabled" aria-readonly="true"/>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12">
                                                <div class="form-group">
                                                    <label id="lblTarea"  class="col-xs-12 col-md-2 control-label fk-label" for="datosTarea">Identificador tarea</label>
                                                    <label for="desTarea" class="fk-label sr-only">Descripción Tarea</label>
                                                    <div id="datosTarea">
                                                        <div class="col-xs-4 col-md-2">
                                                            <input id="idTarea" aria-label="Identificador de tarea" name="textinput" type="text" class="form-control input-md text-right" value="" disabled="disabled" aria-readonly="true"/>
                                                        </div>
                                                        <div class="col-xs-8 col-md-8">
                                                            <input id="desTarea" name="" type="text" class="form-control input-md" value="" disabled="disabled" aria-readonly="true"/>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-xs-12">
                                                <div class="form-group">
                                                    <label for="txtDesc" class="col-xs-12 control-label fk-label text-left">Descripción</label>
                                                    <div class="col-xs-12">
                                                        <textarea class="form-control txtArea" id="txtDesc" name="" rows="3" disabled="disabled" aria-readonly="true"></textarea>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="ibox-title">                             
                                            <span class="text-primary">Datos IAP referentes al técnico</span> 
                                            <!--Limpiamos los floats-->
                                            <div class="clearfix"></div>
                                        </div>
                                    <div id="datosIAP" class="ibox-content collapsed in">  
                                            <div class="form-group">
                                                <div class="col-xs-12 col-md-4">
                                                    <div class="form-group">
                                                        <label for="PConsumo" class="col-xs-12 col-md-6 control-label fk-label">Primer consumo</label>
                                                        <div class="col-xs-8 col-md-6">
                                                            <input disabled="disabled" aria-readonly="true" id="PConsumo" name="" type="text" class="form-control input-md" value="" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-xs-12 col-md-4">
                                                    <div class="form-group">
                                                        <label for="PConsumido" class="col-xs-12 col-md-6 control-label fk-label">Consumido(horas)</label>
                                                        <div class="col-xs-8 col-md-5">
                                                            <input disabled="disabled" aria-readonly="true" id="PConsumido" name="" type="text" class="form-control input-md text-right" value="" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-xs-12 col-md-4">
                                                    <div class="form-group">
                                                        <label for="pEstimado" class="col-xs-12 col-md-7 control-label fk-label"><abbr title="Pendiente de estimar (horas)">Pte. estimado (horas)</abbr></label>
                                                        <div class="col-xs-8 col-md-5">
                                                            <input disabled="disabled" aria-readonly="true" id="pEstimado" name="" type="text" class="form-control input-md text-right" value="" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-xs-12 col-md-4">
                                                    <div class="form-group">
                                                        <label for="UConsumo" class="col-xs-12 col-md-6 control-label fk-label">Último consumo</label>
                                                        <div class="col-xs-8 col-md-6">
                                                            <input disabled="disabled" aria-readonly="true" id="UConsumo" name="" type="text" class="form-control input-md" value="" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-xs-12 col-md-4">
                                                    <div class="form-group">     
                                                        <label for="UConsumido" class="col-xs-12 col-md-6 control-label fk-label">Consumido(jornadas)</label>
                                                        <div class="col-xs-8 col-md-5">
                                                            <input disabled="disabled" aria-readonly="true" id="UConsumido" name="" type="text" class="form-control input-md text-right" value="" />
                                                        </div>
                                                    </div>
                                                </div>
                                                 <div class="col-xs-12 col-md-4">
                                                    <div class="form-group">    
                                                        <label for="ATeorico" class="col-xs-12 col-md-6 control-label fk-label">Avance teórico</label>
                                                        <div class="col-xs-8 col-md-6 input-group">
                                                            <input disabled="disabled" aria-readonly="true" id="ATeorico" name="" type="text" class="form-control input-md text-right" value="" />
                                                            <span class="input-group-addon">%</span>
                                                        </div>
                                                    </div>
                                                 </div> 
                                            </div>
                                        </div>
                                    <div class="ibox-title">                             
                                            <span class="text-primary">Indicaciones</span>     
                                            <div class="clearfix"></div>
                                        </div>
                                    <div id="indicaciones" class="ibox-content collapsed in">  
                                    <div class="form-group">
                                        <div class="col-xs-12 col-sm-6">
                                            <fieldset id="irFieldset" class="fieldset">                              
                                                <h3 class="sr-only">Indicaciones del responsable</h3>
                                                <legend class="fieldset"><span>Del responsable</span></legend>
                                                 <div class="form-group">
                                                    
                                                    <label for="txtTotPrev" class="col-xs-12 col-md-8 control-label fk-label">Total previsto (horas)</label>
                                                    <div class="col-xs-10 col-md-4">
                                                        <input id="txtTotPrev" name="" type="text" class="form-control input-md text-right" value="" disabled="disabled" aria-readonly="true"/>
                                                    </div>
                                                   
                                                 </div>
                                                 <div class="form-group">
                                                    
                                                    <label for="txtFFinPrev" class="col-xs-12 col-md-7 control-label fk-label">Fecha fin prevista</label>
                                                    <div class="col-xs-10 col-md-5">
                                                        <input id="txtFFinPrev" name="" type="text" class="form-control input-md" value="" disabled="disabled" aria-readonly="true"/>
                                                    </div>
                                                   
                                                </div>
                                                <div class="form-group">
                                                    <label for="txtParti" class="col-xs-12 col-md-3 control-label fk-label">Particulares</label>
                                                    <div class="col-xs-12 col-md-9">
                                                        <textarea class="form-control txtArea" id="txtParti" name="" rows="3" disabled="disabled" aria-readonly="true"></textarea>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="txtColec" class="col-xs-12 col-md-3 control-label fk-label">Colectivas</label>
                                                    <div class="col-xs-12 col-md-9">
                                                        <textarea class="form-control txtArea" id="txtColec" name="" rows="3" disabled="disabled" aria-readonly="true"></textarea>
                                                    </div>
                                                </div>
                                            </fieldset>
                                        </div>
                                        <div class="col-xs-12 col-sm-6">
                                            <fieldset id="ctFieldset" class="fieldset">
                                                <h3 class="sr-only">Comentarios del técnico</h3>
                                                <legend class="fieldset"><span>Del técnico</span></legend>
                                                <div class="form-group">
                                                    <label for="txtTotEst" class="col-xs-12 col-md-8 control-label fk-label">Total estimado (horas)</label>
                                                    <div class="col-xs-10 col-md-4">
                                                        <input id="txtTotEst" name="" type="text" class="form-control input-md text-right" value="" title="Formato hora: hh,mm" placeholder="0,00"/>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="txtFEst" class="col-xs-12 col-md-7 control-label fk-label">Fecha de finalización estimada</label>
                                                    <div class="col-xs-10 col-md-5">
                                                        <input id="txtFEst" name="" type="text" class="form-control input-md txtFechaTarea" value="" title="Formato fecha: dd/mm/aaaa" placeholder="dd/mm/aaaa"/>
                                                    </div>
                                                </div>
                                                <div class="checkbox">                                        
                                                    <label for="chkFinalizado" class="col-xs-12 col-md-5 control-label fk-label">
                                                        <input type="checkbox" id="chkFinalizado"/>Trabajo finalizado                                        
                                                    </label>                                       
                                                </div>
                                                <br/>                                                
                                                <div class="form-group">
                                                    <label for="txtObsv" class="col-xs-12 col-md-3 control-label fk-label">Observaciones</label>
                                                    <div class="col-xs-12 col-md-9">
                                                        <textarea class="form-control txtArea" id="txtObsv" name="" rows="3"></textarea>
                                                    </div>
                                                </div>
                                            </fieldset>
                                        </div>
                                    </div>
                                </div>
                                </form>
                                </div>
                            </div>
                            <div class="tab-pane" id="docu">
                                <div class="ibox-content">
                                <form class="form-horizontal"  role="form" >  
                                    <div id="documentos" class="ibox-content collapsed in">                          
                                        <div class="row" id="datosDoc">
                                        </div>                       
                                    </div>
                                </form>
                                </div>
                             </div>
                             <div class="tab-pane" id="notas">
                                    <div class="ibox-content">
                                    <form class="form-horizontal collapsed in"  role="form" >                             
                       
                                          <div id="nots" class="ibox-content collapsed in">
                            
                                                <h2 class="sr-only">Notas</h2>
                                                <div class="form-group">                            
                                                    <label for="txtInv" class="col-xs-12 control-label fk-label text-left no-padding">Investigación/Detección</label>
                                                    <div class="col-xs-12 no-padding">
                                                        <textarea id="txtInv" class="form-control txtArea" name="" rows="3"></textarea>
                                                    </div>
                                                </div>
                                               <div class="form-group">
                                                   <label for="txtAcc" class="col-xs-12 control-label fk-label text-left no-padding">Acciones/Modificaciones</label>
                                                    <div class="col-xs-12 no-padding">
                                                        <textarea id="txtAcc" class="form-control txtArea" name="" rows="3"></textarea>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label for="txtPru" class="col-xs-12 control-label fk-label text-left no-padding">Pruebas</label>
                                                    <div class="col-xs-12 no-padding">
                                                        <textarea id="txtPru" class="form-control txtArea" name="" rows="3"></textarea>
                                                    </div>
                                                </div>
                                               <div class="form-group">
                                                   <label for="txtPaso" class="col-xs-12 control-label fk-label text-left no-padding">Pasos a producción</label>
                                                    <div class="col-xs-12 no-padding">
                                                        <textarea id="txtPaso" class="form-control txtArea" name="" rows="3"></textarea>
                                                    </div>
                                                </div>
                                        </div>
                                    </form>
                                </div>
                          </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <b><button id="btnGrabarTarea" class="btn btn-primary" disabled='disabled' data-dismiss="modal">Aceptar</button></b>                    
                        <b><button id="btnCancelarTarea" class="btn btn-default">Cancelar</button></b>                    
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div> 

        <!-- Modal de confirmación -->
        <div class='modal fade' id='bsconfirm1'>
            <div class='modal-dialog'>
                <div class='modal-content'>
                    <div class='modal-header btn-primary'>
                        <button type='button' class='close' data-dismiss='modal' aria-hidden='true'>&times;</button>
                        <h4 class='modal-title'></h4>
                    </div>
                    <div class='modal-body' style='min-height: 100px'>Durante días en los que has elegido vacaciones, has imputado consumos en proyectos que no son de vacaciones.<br />
                            Si estás conforme con lo que has hecho, pulsa "Sí" para continuar con la grabación.<br />
                            En caso contrario, pulsa "No" y modifica las imputaciones.</div>
                    <div class='modal-footer clear'>
                        <button type='button' class='btn btn-primary fk_btnsi' data-dismiss='modal'>Sí</button>
                        <button type='button' class='btn btn-default fk_btnno' data-dismiss='modal'>No</button>
                    </div>
                </div>
            </div>
        </div>


</body>

</html>

<script src="<% =Session["strServer"].ToString() %>Capa_Presentacion/IAP30/scripts/JavaScript.js?20170317_01" type="text/javascript"></script>
<script src="<% =Session["strServer"].ToString() %>plugins/jquery-scrollstop/jquery.scrollstop.min.js"></script>
<script src="<% =Session["strServer"].ToString() %>plugins/withinviewport/withinviewport.js"></script>
<script src="<% =Session["strServer"].ToString() %>plugins/withinviewport/jquery.withinviewport.js"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/jquery-ui.min.js"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/moment.locale.min.js"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/string.js"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/accounting.min.js" type="text/javascript" ></script>
<script src="<% =Session["strServer"].ToString() %>plugins/jQueryMaskPlugin/jquery.mask.min.js" type="text/javascript" ></script>
<script src="js/modelDia.js"></script>
<script src="js/View.js?v=20180226"></script>
<script src="js/viewTarea.js?20170322_01"></script>
<script src="js/appTarea.js?20170322_01"></script>
<script src="js/app.js?v=20180226"></script>
<script src="../../Documentos/modelsDocumento.js"></script>
<script src="../../Documentos/modelsDocumentoList.js"></script>
<script src="../../Documentos/viewDocumentos.js"></script>
<script src="../../Documentos/appDocumentos.js"></script>


