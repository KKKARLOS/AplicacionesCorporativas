<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Reporte_Agenda_Default" %>

<%@ Register Src="~/Capa_Presentacion/bsUserControls/Menu/Menu.ascx" TagPrefix="cb1" TagName="Menu" %>
<%@ Register Src="~/Capa_Presentacion/bsUserControls/Head.ascx" TagPrefix="cb1" TagName="Head" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    <cb1:Head runat="server" id="Head" />
    <link href="plugins/FullCalendar/fullcalendar.css" rel="stylesheet"/>
    <link rel="stylesheet" type="text/css" href="<% =Session["strServer"].ToString() %>plugins/daterangepicker/daterangepicker.css?20170503_01" />
    <title>::: SUPER ::: - Agenda</title>        
</head>
 
<body data-codigopantalla="140">    
    <script>
        var opciones = { delay: 1 }
        IB.procesando.opciones(opciones);
        IB.procesando.mostrar();
    </script>
    <cb1:Menu runat="server" id="Menu" />    
   <div class="container-fluid ocultable" id="container">    
        <form id="frmDatos" runat="server"></form>
            <h1 class="sr-only">::: SUPER ::: - Agenda</h1>
			 <div class="ibox-content blockquote blockquote-info">
			    <div class="panel-group">		
				    <div class="row">					
					    <div class="col-xs-12">							
						    <div id="calendar"></div>
					    </div>		                        
                        <div class="hidden-xs" >
                            <div class="pull-right" style="padding-right: 15px;">
                                <br />
                                <button id="btnVolver" type="button" class="btn btn-primary btnVolver">
                                    <span>Volver</span>
                                </button>
                            </div>
                        </div>   
				    </div>
			   </div>
            </div>
            <div id="pieAgenda" class="visible-xs">
                <div class="col-xs-2 col-xs-offset-5 col-pie no-padding pull-center">
                    <button id="btnSalirLite" type="button" class="boton btnVolver" title="Regresar">
                        <span class="fa fa-share-square-o-rev fa-2x fa-blanco" style="vertical-align: middle;"></span>
                        <br/>
                        <span>Regresar</span>
                    </button>                                   
                </div>   
            </div> 
            
		</div>
		
		<!-- Modal de creación de evento -->
        <div class="modal fade" id="detalleAgenda" role="dialog" tabindex="-1" title=":::SUPER::: - Detalle de agenda">
            <div class="modal-dialog modal-lg" role="dialog">
                <div class="modal-content ocultable2">
                    <div class="modal-header bg-primary">
                        <button id="btnCierreModalEvento" type="button" class="close" aria-label="Cerrar"><span aria-hidden="true">&times;</span></button>
                        <h1 class="modal-title">:::SUPER::: - Detalle de agenda</h1>
                    </div>
                    <div class="modal-body detalleAgenda" aria-disabled="true">                       
                            <form class="form-horizontal collapsed in"  role="form" accept-charset="iso-8859-15">                              
                                <div class="ibox-title ibox-title">
                                    <span class="text-primary">Datos generales</span>                                        
                                </div>
                                <div id="datosGenerales" class="ibox-content collapsed in">   
                                    <div class="form-group">            
                                        <input id="idEventoCal" class="form-control  input-md" name="" type="hidden" value=""/>                     
                                        <div class="col-xs-12 col-md-6">
                                            <div class="form-group">
                                                <label for="prof" class="col-xs-12 col-md-4 control-label">Profesional</label>    									    
									            <div class="col-xs-12 col-md-8" >	
										            <input id="prof" class="form-control  input-md" name="prof" type="text" readonly="readonly" aria-readonly="true" value=""/> 
									            </div>                                            
                                            </div>
                                            <div class="form-group">
                                                 <label for="prom" class="col-xs-12 col-md-4 control-label">Promotor</label>  
									            <div class="col-xs-12 col-md-8" >	
										            <input id="prom" class="form-control input-md" name="prom" type="text" readonly="readonly" aria-readonly="true" value=""/> 
									            </div>
                                            </div>
                                            <div class="form-group">
                                                 <div class="col-xs-12 col-md-4">
                                                    <label for="asunto" class=" control-label">Asunto</label><span id="dangerAsunto" class="text-danger"> *</span>  
                                                 </div>
									            <div class="col-xs-12 col-md-8" >	
										            <input class="form-control input-md" name="asunto" type="text" id="asunto" value="" maxlength="50"/>
									            </div>
                                            </div>
                                        </div>
                                        <div class="col-xs-12 col-md-6">                                           
                                            <div class="form-group">                                                
                                                <label  for="motivo" class="col-xs-12 control-label fk-label text-left" >Motivo</label>	
									            <div class="col-xs-12" >	
										            <textarea id="motivo" name="motivo" class="form-control txtArea" rows="3"></textarea>     
									            </div>
                                            </div>
                                        </div>
                                        <div class="col-xs-12">
                                            <div class="form-group">
                                                <div class="col-xs-12 col-md-2">
                                                    <label id="lblTarea" title="Selección de tarea"  role="link" class="control-label fk-label txtLinkU" for="datosTarea">Tarea</label><span id="dangerTarea" class="text-danger"> *</span>
                                                </div>
                                                <label for="desTarea" class="fk-label sr-only">Descripción Tarea</label>
                                                <div id="datosTarea">
                                                    <div class="col-xs-4 col-sm-3 col-md-2">
                                                        <input id="idTarea" aria-label="Identificador de tarea" name="textinput" type="text" class="form-control input-md text-right" value=""/>
                                                    </div>
                                                    <div class="col-xs-12 col-sm-9 col-md-8">
                                                        <input id="desTarea" name="" type="text" class="form-control input-md" value="" disabled="disabled" aria-readonly="true"/>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="ibox-title ibox-title">
                                    <span class="text-primary">Rango temporal</span>                                        
                                </div>
                                <div id="rangoTemporal" class="ibox-content collapsed in">   
                                    <div class="form-group"> 
                                        <div class="col-xs-12">
                                            <div class="form-group">
                                                <label id="lblRangoFechas" class="col-xs-12 col-sm-4 col-md-3 control-label fk-label" for="lblRangoFechas">Selección de rango</label>                                            
								                <div class="col-xs-12 col-sm-8 col-md-5">
									                <input name="txtRangoFechas" type="text" id="txtRangoFechas" title="Formato fecha: dd/mm/aaaa - dd/mm/aaaa" placeholder="dd/mm/aaaa HH:MM - dd/mm/aaaa HH:MM" class="form-control input-md txtFecha"/>
								                </div>
                                            </div>
                                        </div>                                       
                                    </div>
                                    <!--<div class="form-group">  
                                        <div class="col-xs-12 col-md-3">
                                            <div class="form-group">
                                                <label for="fechaInicio" class="col-xs-6 col-sm-3 col-md-5 control-label fk-label">Fecha Inicio</label>  
									            <div class="col-xs-6 col-sm-3 col-md-7">
                                                     <input title="Formato fecha: dd/mm/aaaa" id="fechaInicio" name="" type="text" class="form-control input-md txtFecha calendar-off" value="" placeholder="dd/mm/aaaa" />
									            </div>
                                             </div>   
                                        </div>
                                         <div class="col-xs-12 col-md-3">
                                            <div class="form-group">
                                                <label for="horaInicio" class="col-xs-6 col-sm-3 col-md-6 control-label fk-label">Hora Inicio</label>  
									            <div class="col-xs-6 col-sm-3 col-md-6">	
										           <input title="Formato hora: hh:mm" placeholder="00:00" id="horaInicio" type="text" name="" class="form-control input-md text-right" value="" />
									            </div>
                                             </div>   
                                        </div>
                                        <div class="col-xs-12 col-md-3">
                                            <div class="form-group">
                                                <label for="fechaFin" class="col-xs-6 col-sm-3 col-md-5 control-label fk-label">Fecha Fin</label>  
									            <div class="col-xs-6 col-sm-3 col-md-7">
                                                     <input title="Formato fecha: dd/mm/aaaa" id="fechaFin" name="" type="text" class="form-control input-md txtFecha calendar-off" value="" placeholder="dd/mm/aaaa" />
									            </div>
                                             </div>   
                                        </div>
                                         <div class="col-xs-12 col-md-3">
                                            <div class="form-group">
                                                <label for="horaFin" class="col-xs-6 col-sm-3 col-md-6 control-label fk-label">Hora Fin</label>  
									            <div class="col-xs-6 col-sm-3 col-md-6">	
										           <input title="Formato hora: hh:mm" placeholder="00:00" id="horaFin" type="text" name="" class="form-control input-md text-right" value="" />
									            </div>
                                             </div>   
                                        </div>                                       
                                    </div>-->
                                     <div class="form-group">                                         
                                        <div class="col-xs-12">
                                            <div class="form-group">
                                                <label id="lblRepeticion" class="col-xs-12 col-sm-4 col-md-3 control-label fk-label" for="lblRangoFechas">Días de repetición</label>                                            
								                <div class="col-xs-12 col-sm-8 col-md-7">
									                <fieldset  class="fieldset">
                                                        <h3 class="sr-only">Días de repetición</h3>                                               
                                            
                                                        <label class="checkbox-inline" for="checkboxes-0" title="Lunes">
                                                            <input type="checkbox" name="checkboxes" id="checkboxes-0" value="0"/>
                                                            Lun
                                                        </label>
                                                        <label class="checkbox-inline" for="checkboxes-1" title="Martes">
                                                            <input type="checkbox" name="checkboxes" id="checkboxes-1" value="1"/>
                                                            Mar
                                                        </label>
                                                        <label class="checkbox-inline" for="checkboxes-2" title="Miércoles">
                                                            <input type="checkbox" name="checkboxes" id="checkboxes-2" value="2"/>
                                                            Mié
                                                        </label>
                                                        <label class="checkbox-inline" for="checkboxes-3" title="Jueves">
                                                            <input type="checkbox" name="checkboxes" id="checkboxes-3" value="3"/>
                                                            Jue
                                                        </label>
                                                            <label class="checkbox-inline" for="checkboxes-4" title="Viernes">
                                                            <input type="checkbox" name="checkboxes" id="checkboxes-4" value="4"/>
                                                            Vie
                                                        </label>
                                                            <label class="checkbox-inline" for="checkboxes-5" title="Sábado">
                                                            <input type="checkbox" name="checkboxes" id="checkboxes-5" value="5"/>
                                                            Sab
                                                        </label>
                                                            <label class="checkbox-inline" for="checkboxes-6" title="Domingo">
                                                            <input type="checkbox" name="checkboxes" id="checkboxes-6" value="6"/>
                                                            Dom
                                                        </label>
                                                    </fieldset>	
								                </div>
                                            </div>
                                        </div>  
                                        <!--<div class="col-xs-12 col-md-8">
                                            <fieldset  class="fieldset">
                                                <h3 class="sr-only">Días de repetición</h3>
                                                <legend class="fieldset"><span>Días de repetición</span></legend>
                                            
                                                <label class="checkbox-inline" for="checkboxes-0" title="Lunes">
                                                    <input type="checkbox" name="checkboxes" id="checkboxes-0" value="0"/>
                                                    Lun
                                                </label>
                                                <label class="checkbox-inline" for="checkboxes-1" title="Martes">
                                                    <input type="checkbox" name="checkboxes" id="checkboxes-1" value="1"/>
                                                    Mar
                                                </label>
                                                <label class="checkbox-inline" for="checkboxes-2" title="Miércoles">
                                                    <input type="checkbox" name="checkboxes" id="checkboxes-2" value="2"/>
                                                    Mié
                                                </label>
                                                <label class="checkbox-inline" for="checkboxes-3" title="Jueves">
                                                    <input type="checkbox" name="checkboxes" id="checkboxes-3" value="3"/>
                                                    Jue
                                                </label>
                                                    <label class="checkbox-inline" for="checkboxes-4" title="Viernes">
                                                    <input type="checkbox" name="checkboxes" id="checkboxes-4" value="4"/>
                                                    Vie
                                                </label>
                                                    <label class="checkbox-inline" for="checkboxes-5" title="Sábado">
                                                    <input type="checkbox" name="checkboxes" id="checkboxes-5" value="5"/>
                                                    Sab
                                                </label>
                                                    <label class="checkbox-inline" for="checkboxes-6" title="Domingo">
                                                    <input type="checkbox" name="checkboxes" id="checkboxes-6" value="6"/>
                                                    Dom
                                                </label>
                                            </fieldset>	
									    </div> -->   
								    </div>
                                </div>
                                <div class="ibox-title ibox-title">
                                    <h2 class="sr-only">Indicaciones que haga el profesional sobre el evento a programar</h2>  
                                    <span class="text-primary">Indicaciones</span>                                        
                                </div>
                                <div id="indicaciones" class="ibox-content collapsed in">   
                                    <div class="form-group">  
                                        <div class="col-xs-12 col-md-6">
                                                <label for="observaciones" class="col-xs-12 col-md-6 legend control-label">Observaciones</label>
                                                <textarea id="observaciones" name="observaciones" class="form-control txtArea" rows="5"></textarea>  
							            </div>

                                        <div class="col-xs-12 col-md-6">  
                                                <label for="privado" class="col-xs-12 col-md-6 legend control-label">Privado</label>
                                                <textarea id="privado" name="privado" class="form-control txtArea" rows="5"></textarea> 
                                        </div>
                                    </div>
                                </div>
                                <div class="ibox-title">   
									<h2 class="sr-only">Profesionales asignados al evento</h2>  
									<span class="text-primary">Otros profesionales</span> 
									<!--Limpiamos los floats-->
									<div class="clearfix"></div>
								</div>
								<div id="datosAsignadoProf" class="ibox-content collapsed in">	
                                    <div class="form-group"> 
										<div class="form-group">
											<div id="lisProfesionales" class="text-left col-xs-12 col-sm-6 col-md-6">
												<div class="form-group criterios">
													<div class="btn-group col-xs-12 col-md-1 no-padding-right-md">
														<a id="btnMarcarDesmarcarOrigen" class="selector" data-toggle="tooltip" title="Seleccionar todos los profesionales">
															<i class="glyphicon glyphicon-unchecked"></i>
														</a>
													</div>
													<div class="col-xs-12 col-sm-7 col-md-4 no-padding-right-md">
														<label for="txtApellido1" class="sr-only">Buscador de profesional por Apellido1</label>
														<input id="txtApellido1" type="text" name="" class="form-control input-md inputCriterios" placeholder="Apellido1" />
													</div>
													<div class="col-xs-12 col-sm-7 col-md-4 no-padding-right-md">
														<label for="txtApellido2" class="sr-only">Buscador de profesional por Apellido2</label>
														<input id="txtApellido2" type="text" name="" class="form-control input-md inputCriterios" placeholder="Apellido2" />
													</div>
													<div class="col-xs-12 col-sm-7 col-md-3 no-padding-right-md">
														<label for="txtNombre" class="sr-only">Buscador de profesional por Nombre</label>
														<input id="txtNombre" type="text" name="" class="form-control input-md inputCriterios" placeholder="Nombre" />
													</div>
												</div>
                                                <div class="form-group">
                                                    <div class="col-xs-12 no-padding-right-md">
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
													<button id="btnSeleccionar" class="btn btn-default btn-sm move-right col-xs-offset-4 col-xs-2 col-sm-8 col-md-6" style="margin-top:130px">
														<span class="sr-only">Seleccionar profesional</span>
														<span class="glyphicon glyphicon-chevron-right hidden-xs"></span>
                                                        <span class="glyphicon glyphicon-chevron-down visible-xs"></span>
													</button>
												</div>
											</div>
											<div id="lisProfesionales2" class="col-xs-12 col-sm-5 col-md-5">
												<div class="form-group">
													<div class="btn-group col-xs-1 col-md-1 no-padding-right-md">
														<a id="btnMarcarDesmarcarDestino" class="selector" data-toggle="tooltip" title="Seleccionar todos los profesionales">
															<i class="glyphicon glyphicon-unchecked"></i>
														</a>
													</div>
													<div class="col-xs-10 col-sm-11 col-md-11 no-padding-right-md">
														<label for="buscAsignados" class="sr-only">Buscador de asignados</label>
														<input id="buscAsignados" type="text" name="" class="form-control input-md" placeholder="Buscar" />
													</div>
												</div>
                                                <div class="form-group">
                                                    <div class="col-xs-12  no-padding-right-md ">
														<table id="tblProfesionalesSel" class="table header-fixed table-striped table-hover">
															<thead id="tabCabeceraSel">
																<tr>
																	<th class='bg-primary'>Asignados</th>
																</tr>
															</thead>  
															<tbody id="tbodyProfesionalesSel">
															</tbody>
														</table>
                                                    </div>
                                                </div>
												<div class="form-group">
													
													<div class="pull-right">
													<span class="sr-only">Eliminar profesional</span>
														<button id="btnEliminarSeleccionados" class="btn-link underline">Eliminar</button>
													</div>
												</div>								                        
											</div>
										</div>																					
                                    </div>
                                </div>
                            </form>
                                           
                    </div>
                    <div class="modal-footer">
                       <b><button id="btnAceptar" class="btn btn-primary">Aceptar</button></b>  
                       <b><button id="btnCancelar" class="btn btn-default">Cancelar</button></b>     
					   					   
                    </div>
                </div>          
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
       <!-- Modal de comentario -->
        <div class="modal fade" id="modalComentario" role="dialog" tabindex="-1" title=":::SUPER:::">
            <div class="modal-dialog modal-md" role="dialog">
                <div class="modal-content">
                    <div class="modal-header bg-primary">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar"><span aria-hidden="true">&times;</span></button>
                        <h1 class="modal-title" id="tituloModalComentario">:::SUPER::: - Motivo de eliminación</h1>
                    </div>
                    <div class="modal-body">
                        <div class="row row-modal">
                            <input type="hidden" id="idEventoEliminar" />
                            <input type="hidden" id="idEventoAgenda" />
                            <div class="col-xs-12">
                                <%-- La cantidad máxima de caracteres permitida es 370 --%>
                                <textarea maxlength="370" class="form-control txtArea" id="txtComentario" name="" rows="3"></textarea>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <b><button id="btnAceptarComentario"  class="btn btn-primary">Aceptar</button></b>                    
                        <b><button id="btnCancelarComentario" class="btn btn-default" data-dismiss="modal">Cancelar</button></b>                    
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div> 
        <div class="fk_ayudaTarea"></div>
  
</body>
</html>
<script src="<% =Session["strServer"].ToString() %>Capa_Presentacion/IAP30/scripts/JavaScript.js" type="text/javascript"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/moment.locale.min.js"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/accounting.min.js"></script>
<script type="text/javascript" src="<% =Session["strServer"].ToString() %>plugins/daterangepicker/daterangepicker.js?20170503_01"></script>
<script src="plugins/FullCalendar/fullcalendar.js"></script>
<script src="plugins/FullCalendar/lang-all.js"></script>
<script src="js/view.js?20170517_01"></script>
<script src="js/app.js?20170517_01"></script>
<script src="<% =Session["strServer"].ToString() %>plugins/IB/buscatarea.js?20170517_01"></script>


 