<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Reporte_Bitacora_Accion_AccionPT_Default" %>
<%@ Register Src="~/Capa_Presentacion/bsUserControls/Menu/Menu.ascx" TagPrefix="cb1" TagName="Menu" %>
<%@ Register Src="~/Capa_Presentacion/bsUserControls/Head.ascx" TagPrefix="cb1" TagName="Head" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    <cb1:Head runat="server" id="Head" /> 
    <title>::: SUPER ::: - Detalle de acción</title>     
</head>
<body>        
    <script>
        var opciones = { delay: 1 }
        IB.procesando.opciones(opciones);
        IB.procesando.mostrar();
    </script>
    <cb1:Menu runat="server" id="Menu" />    
    <link rel='stylesheet' href="<% =Session["strServer"].ToString() %>plugins/datatables/jquery.dataTables.min.css" />
    <link rel="stylesheet" href="<% =Session["strServer"].ToString() %>css/jquery-ui.min.css"/>   
    <link rel="stylesheet" href="../../../../Documentos/StyleSheet.css"/>      
    <form id="frmDatos" runat="server">
    </form>
    <div class="container-fluid ocultable">
        <div class="ibox-content blockquote blockquote-info">
            <div class="panel-group">
                <div>
                    <h1 class="sr-only">::: SUPER ::: - Creación de una acción para proyecto económico</h1>  
                    <div id="content">
                        <ul id="tabs" class="nav nav-tabs" data-tabs="tabs" role="tablist">
                            <li class="active"><a href="#general" id="pestana1" data-toggle="tab" class="txtLink leido" role="tab" aria-expanded="true">General</a></li>
                            <li><a href="#tareas" data-toggle="tab" class="txtLink noLeido" role="tab" aria-expanded="false">Tareas</a></li>         
                            <li><a href="#docu" data-toggle="tab" class="txtLink noLeido" role="tab" aria-expanded="false">Documentación</a></li>                       
                        </ul>  
                   
                        <div id="my-tab-content" class="tab-content">
                            <div class="tab-pane active" id="general">  									
								<div class="ibox-content">
									<form class="form-horizontal collapsed in"  role="form" accept-charset="iso-8859-15"> 
										<a class="collapse-link">
											<div class="ibox-title-first ibox-title ibox-title_toggleable" data-toggle="collapse" data-target="#datosAsunto">
											<span class="text-primary">Asunto</span>  
											<h2 class="hiddenStructure">Información sobre el asunto</h2>      
												<div class="ibox-tools">
													<i class="fa fa-chevron-up pull-right"></i>
												</div>   
											</div>
										</a>
										<div id="datosAsunto" class="collapsed in sinPadding">  
                                            <div class="ibox-content">  
											    <div class="form-group">  
												    <div class="form-group">
													    <label id="lblNumeroAsun" for="txtIdAsunto" class="col-xs-3 col-sm-1 col-md-1 col-lg-1 control-label">Número</label>  
													    <div class="col-xs-4 col-xs-off-5 col-sm-2 col-md-2 col-lg-2">
														    <input id="txtIdAsunto" name="txtIdAsunto" class="form-control input-md margin-bottom-xs-sm-md" type="text" readonly="true" value="" />      
													    </div>
													    <label for="txtDesAsunto" class="col-xs-12 col-md-2 col-lg-1 control-label">Denominación</label>  
													    <div class="col-xs-12 col-md-7 col-lg-8">
														    <input id="txtDesAsunto" name="txtDesAsunto" class="form-control input-md" type="text" readonly="true" value="" />      
													    </div>
												    </div>
                       										
											    </div>
                                            </div>
										</div>		
										<a class="collapse-link">
											<div class="ibox-title ibox-title_toggleable" data-toggle="collapse" data-target="#datosAccion">  
												<span class="text-primary">Acción</span> 
												<h2 class="hiddenStructure">Información sobre la acción</h2>  
												<div class="ibox-tools">
													<i class="fa fa-chevron-up pull-right"></i>
												</div> 													
												<!--Limpiamos los floats-->
												<div class="clearfix"></div>
											</div>  
										</a>	
										<div id="datosAccion" class="collapsed in sinPadding"> 
                                            <div class="ibox-content">
											    <div class="form-group">
												    <div class="form-group">
													    <label id="lblNumeroAccion" for="txtIdAccion" class="col-xs-3 col-sm-1 col-md-1 col-lg-1 control-label">Número</label>  
													    <div class="col-xs-4 col-xs-off-5 col-sm-2 col-md-2 col-lg-2">
														    <input id="txtIdAccion" name="txtIdAccion" class="form-control input-md margin-bottom-xs-sm-md" type="text" readonly="true" value="" />      
													    </div>
													    <label for="txtDesAccion" class="col-xs-12 col-md-2 col-lg-1 control-label">Denominación</label>  
													    <div class="col-xs-12 col-md-7 col-lg-8">
														    <input id="txtDesAccion" name="txtDesAccion" class="form-control input-md margin-bottom-xs-sm-md" type="text"  value="" maxlength="50" />      
													    </div>
												    </div>

												    <div class="form-group">
													    <label for="txtFechaLimite" class="col-xs-5 col-sm-1 col-md-1 col-lg-1 control-label">Límite</label>  
													    <div class="col-xs-7 col-sm-3 col-md-2 col-lg-2">
														    <input title="Formato fecha: dd/mm/aaaa" placeholder="dd/mm/aaaa" id="txtFechaLimite" name="txtFechaLimite" class="form-control input-md margin-bottom-xs-sm-md calendar-off" type="text"  value="" />      
													    </div>
										
													    <label for="txtFechaFinalizacion" class="col-xs-5 col-sm-2 col-md-2 col-lg-1 control-label">Finalización</label>  
													    <div class="col-xs-7 col-sm-3 col-md-2 col-lg-2">
														    <input title="Formato fecha: dd/mm/aaaa" placeholder="dd/mm/aaaa"  id="txtFechaFinalizacion" name="txtFechaFinalizacion" class="form-control input-md margin-bottom-xs-sm-md calendar-off" type="text"  value="" />      
													    </div> 

													    <label for="cboAvance" class="col-xs-5 col-sm-1 col-md-1 col-lg-1 control-label">Avance</label> 
													    <div class="col-xs-4 col-sm-2 col-md-1 col-lg-1">	
													       <select id="cboAvance" name="cboAvance" class="form-control margin-bottom-xs-sm-md">                                                        															
                                                                <option value="0" selected="selected">0</option>
															    <option value="10">10</option>
															    <option value="20">20</option>
															    <option value="30">30</option>
                                                                <option value="40">40</option>
                                                                <option value="50">50</option>
                                                                <option value="60">60</option>
                                                                <option value="70">70</option>
                                                                <option value="80">80</option>
                                                                <option value="90">90</option>
                                                                <option value="100">100</option>
														    </select> 						 
													    </div>    					 
												    </div>
												    <div class="form-group">
													    <div class="col-xs-12 col-md-6">  
														    <label for="txtDescripcion" class="col-xs-12 col-md-6 control-label text-left" style="margin-left:-13px">Descripción</label>
														    <textarea id="txtDescripcion" name="txtDescripcion" class="form-control txtArea" rows="3"></textarea> 
													    </div>
													    <div class="col-xs-12 col-md-6">
														    <label for="txtObservaciones" class="col-xs-12 col-md-6 control-label text-left" style="margin-left:-13px">Observaciones</label>
														    <textarea id="txtObservaciones" name="txtObservaciones" class="form-control txtArea" rows="3"></textarea>  
													    </div>
												    </div>	
                                                </div>
                                            </div>
                                        </div>                                        																
										<a class="collapse-link">
											<div class="ibox-title ibox-title_toggleable" data-toggle="collapse" data-target="#datosAsignadoProf" id="capaProfesionales">    
												<span class="text-primary">Asignado a</span> 
												<h2 class="hiddenStructure">Información sobre los profesionales asignados al asunto</h2>  
												<div class="ibox-tools">
													<i class="fa fa-chevron-up pull-right"></i>
												</div> 		
												<!--Limpiamos los floats-->
												<div class="clearfix"></div>
											</div>
										</a>	
										<div id="datosAsignadoProf" class="collapsed in sinPadding">
                                            <div class="ibox-content"> 
                                                <div class="form-group"> 										
											        <div class="form-group">
												        <div class="col-xs-12 col-md-6">
													        <label for="txtDpto" class="col-xs-12 col-md-6 legend control-label text-left" style="margin-left:-13px">Departamento/Grupo</label>
													        <textarea id="txtDpto" name="txtDpto" class="form-control txtArea" rows="3"></textarea>  
												        </div>
												        <div class="col-xs-12 col-md-6">  
													        <label for="txtAlerta" class="col-xs-12 col-md-7 legend control-label text-left" style="margin-left:-13px">Notificar a(e-mail separados por ;)</label>
													        <textarea id="txtAlerta" name="txtAlerta" class="form-control txtArea" rows="3"></textarea> 
												        </div>
											        </div>								
											        <div class="form-group">
													    <div id="lisProfesionales" class="text-left col-xs-12 col-sm-5 col-md-5">
														    <div class="form-group criterios">
															    <div id="btnMarca" class="btn-group col-xs-12 col-sm-2 col-md-2">
																    <a id="btnMarcarDesmarcarOrigen" style="margin-top:-8px;" class="btn selector margin-bottom-xs-sm no-padding" data-toggle="popover" title="" data-content="Marcar/desmarcar todo" data-placement="top">
																	    <i class="glyphicon glyphicon-unchecked"></i>
																    </a>
															    </div>
															    <div class="col-xs-12 col-sm-9 col-md-3 criterios no-padding-left ">
																    <label for="txtApellido1" class="hiddenStructure">Buscador de profesional por Apellido1</label>
																    <input id="txtApellido1" type="text" class="form-control margin-bottom-xs-sm no-padding-right" placeholder="Apellido1" />
															    </div>
															    <div class="col-xs-12 col-sm-offset-2 col-sm-9 col-md-offset-0 col-md-3 criterios no-padding-left">
																    <label for="txtApellido2" class="hiddenStructure">Buscador de profesional por Apellido2</label>
																    <input id="txtApellido2" type="text" class="form-control margin-bottom-xs-sm no-padding-right" placeholder="Apellido2" />
															    </div>                                                            
															    <div class="col-xs-12 col-sm-offset-2 col-sm-9 col-md-offset-0 col-md-3 criterios no-padding-left">
																    <label for="txtNombre" class="hiddenStructure">Buscador de profesional por Nombre</label>
																    <input id="txtNombre" type="text" class="form-control margin-bottom-xs-sm no-padding-right" placeholder="Nombre" />
															    </div>
														    </div>
                                                            <div class="form-group">
                                                                <div class="col-xs-12">
														            <table id="tblProfesionales" class="table header-fixed table-striped">
																            <thead id="tabCabecera">
																	            <tr>
																		            <th class='bg-primary'>Profesional</th>
																	            </tr>
																            </thead>                                                                
																            <tbody id="tbodyProfesionales">
															            </tbody>
														            </table>
                                                                </div>
                                                            </div>
													    </div>
													    <div class="list-arrows col-xs-12 col-sm-1 col-md-1 text-center align-verti">
														    <div class="form-group">
															    <button id="btnSeleccionar" class="btn btn-default btn-sm move-right col-xs-offset-4 col-xs-2 col-sm-8 col-md-6" style="margin-top:30px">
																    <span class="hiddenStructure">Seleccionar profesional</span>
                                                                    <span class="glyphicon glyphicon-chevron-right hidden-xs"></span>
                                                                    <span class="glyphicon glyphicon-chevron-down visible-xs"></span>
															    </button>
														    </div>
													    </div>
													    <div id="lisProfesionales2" class="col-xs-12 col-sm-6 col-md-6">
														    <div id="cabprofe" class="form-group">
															    <div class="btn-group col-xs-1">
																    <a id="btnMarcarDesmarcarDestino" style="margin-top:-8px" class="btn selector margin-bottom-xs-sm no-padding" data-toggle="popover" title="" data-content="Marcar/desmarcar todo" data-placement="top">
																	    <i class="glyphicon glyphicon-unchecked"></i>
																    </a>
															    </div>
															    <div class="col-xs-10 input-group">
																    <label for="buscAsignados" class="hiddenStructure">Buscador de asignados</label>
																    <input id="buscAsignados" type="text" class="form-control margin-bottom-xs-sm" placeholder="Buscar" />
															    </div>
														    </div>
                                                            <div class="form-group">
                                                                <div class="col-xs-12">
														            <table id="tblProfesionalesSel" class="table header-fixed table-striped table-hover">
															            <thead id="tabCabeceraSel">
																            <tr>
																	            <th class='bg-primary'>Asignados</th>
																	            <th class='bg-primary'>Notificar</th>
																            </tr>
															            </thead>  
															            <tbody id="tbodyProfesionalesSel">
															            </tbody>
														            </table>
                                                                </div>
                                                            </div>
														    <div class="form-group">
															    <div class="pull-left">
																    <button id="btnDesmarcarEliminados" class="btn-link underline" style="margin-left:15px">Desmarcar eliminados</button>
															    </div>
															    <div class="pull-right">
															    <span class="hiddenStructure">Eliminar profesional</span>
																    <button id="btnEliminarSeleccionados" class="btn-link underline">Eliminar</button>
															    </div>
														    </div>								                        
													    </div>
											        </div>																					
                                                </div>
                                            </div>
                                        </div>
									</form>
								</div>
							</div>     
                            <div class="tab-pane" id="tareas">             
                                <div class="form-group" style="margin-top:20px">
                                    <div class="form-group">
                                        <div class="table-responsive">
										    <h2 class="sr-only">Tabla de resultados</h2>
                                            <div class="div-table">
                                                <table id="tblCabeceraTareas" class="table table-bordered table-no-border table-hover table-condensed cabeceraFija" summary="Relación de tareas">
                                                    <thead>
                                                        <tr>
                                                            <th class="bg-primary">Número</th>
                                                            <th class="bg-primary">Denominación</th>
                                                            <th class="bg-primary text-right">ETPL</th>
                                                            <th class="bg-primary text-center">FIPL</th>
                                                            <th class="bg-primary text-center">FFPL</th>
                                                            <th class="bg-primary text-right">ETPR</th>
                                                            <th class="bg-primary text-center">FFPR</th>
                                                            <th class="bg-primary text-right">Consumo</th>
                                                            <th class="bg-primary text-center">%Avance</th>
                                                        </tr>
                                                    </thead>
                                                </table> 
                                            </div>
										    <div class="div-table-content">
											    <h2 class="sr-only">Tabla de contenido</h2>                                
											    <table id="tblDatosTarea" class="table cell-border table-hover table-condensed" summary="Tabla de contenido">
												    <tbody id="tbodyTareas" class="cabeceraFija">
												    </tbody>
											    </table>
										    </div>	
                                        </div>										
                                    </div>
                                    <div class="form-group">
                                        <div id="botonesTarea" class="pull-right" style="margin-right:20px">
                                            <div class="botonTabla">      
                                                <span id="btnDeshacerTarea" role="link" tabindex="0" aria-label="Deshacer acciones" class="fa fa-undo" style="color: black"></span>
                                            </div>
                                            <div class="botonTabla" style="margin-left: 10px;">      
                                                <span id="btnNuevaTarea" role="link" tabindex="0" aria-label="Asociar nueva tarea" class="fa fa-plus" style="color: green"></span>
                                            </div>
                                            <div class="botonTabla" style="margin-left: 10px;">                      
                                                <span id="btnEliminarTarea" role="link" tabindex="0" aria-label="Eliminar tarea" class="fa fa-trash" style="color: red"></span>
                                            </div>
                                        </div> 
                                    </div>
                                    <br />
                                </div>  
                            </div>
                            <div class="tab-pane" id="docu">                       
						        <form class="form-horizontal collapsed in"  role="form" >  
							        <div id="documentos" class="ibox-content collapsed in">  
								        <div class="form-group" id="datosDoc">
								        </div>
							        </div>
						        </form>
                            </div>
                        </div><br /><br />  
                        <div class="form-group">
                            <div id="pieTarea" class="pull-right">                             
                                <div class="col-pie">
                                    <button id="btnGrabar" type="button" class="btn btn-primary btn-default-lite">
                                        <span class="fa fa-share-square-o-rev fa-2x fa-blanco" style="vertical-align: middle;"></span>
                                        <span>Grabar</span>
                                    </button>
                                    <button id="btnSalir" style="margin-left:8px" type="button" class="btn btn-default btn-default-lite">
                                        <span class="fa fa-share-square-o-rev fa-2x fa-blanco" style="vertical-align: middle;"></span>
                                        <span>Volver</span>
                                    </button>
                                </div>            
                            </div>
                            <div class="clearfix"></div>   
                        </div>     
                    </div>
                </div>
            </div>
        </div>
    </div>

        <!-- Plugin de Tarea-->
<%--        <div class="fk_ayudaTarea"></div>     --%>

    <!-- Modal de Tarea -->
    <div class="modal fade" id="modalTarea">
        <div class="modal-dialog modal-lg" role="dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title">::: SUPER ::: - Selección de tareas</h4>
                </div>
                <div class="modal-body">                    
 					<div class="form-group">
                        <div class="table-responsive">
                            <h2 class="sr-only">Tabla de ayuda para la selección de tareas</h2> 
                            <div class="div-table-ayuda">                               
                                <table id="tblCabeceraTareasAyuda" class="table-bordered table-no-border table-hover table-condensed cabeceraFija" summary="Tabla de cabecera">
								    <thead>
									    <tr>
										    <th class='bg-primary'>Número</th>
										    <th class='bg-primary'>Denominación</th>
									    </tr>
								    </thead>
                                </table>
                            </div>
                            <div class="div-table-content-ayuda">
								<h2 class="sr-only">Tabla de contenido</h2> 
                                <table id="tblTareasAyuda" class="table cell-border table-hover table-condensed" summary="Tabla de contenido">        
								    <tbody id="tbodyTareasAyuda" class="cabeceraFija">
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>	
                <div class="modal-footer">
                    <b><button id="btnSeleccionarTareas" class="btn btn-primary">Aceptar</button></b>
                    <b><button id="btnCancelarTareas" class="btn btn-default" data-dismiss="modal">Cancelar</button></b>
                </div>
            </div>
        </div>
    </div>
</body>

</html>
<script src="<% =Session["strServer"].ToString() %>scripts/string.js"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/jquery-ui.min.js"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/moment.locale.min.js"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/accounting.min.js"></script>
<script src="<% =Session["strServer"].ToString() %>Capa_Presentacion/IAP30/scripts/JavaScript.js" type="text/javascript"></script>
<script src="../../../../Documentos/modelsDocumento.js"></script>
<script src="../../../../Documentos/modelsDocumentoList.js"></script>
<script src="../../../../Documentos/viewDocumentos.js"></script>
<script src="../../../../Documentos/appDocumentos.js"></script>
<script src="js/view.js?20180110"></script>
<script src="js/app.js?20170313_01"></script>
