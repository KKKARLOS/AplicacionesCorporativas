<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Reporte_Bitacora_Asunto_AsuntoPT_Default" %>
<%@ Register Src="~/Capa_Presentacion/bsUserControls/Menu/Menu.ascx" TagPrefix="cb1" TagName="Menu" %>
<%@ Register Src="~/Capa_Presentacion/bsUserControls/Head.ascx" TagPrefix="cb1" TagName="Head" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    <cb1:Head runat="server" id="Head" /> 
    <title>::: SUPER ::: - Detalle de asunto</title>     
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
    <div class="container-fluid">
        <div class="ibox-content blockquote blockquote-info">
            <div class="panel-group">
                <div>
                    <h1 class="sr-only">::: SUPER ::: - Creación de asunto para proyecto técnico</h1>  
                    <div id="content">
                        <ul id="tabs" class="nav nav-tabs" data-tabs="tabs" role="tablist">
                            <li class="active"><a href="#general" id="pestana1" data-toggle="tab" class="txtLink leido" role="tab" aria-expanded="true">General</a></li>
                            <li><a href="#crono" data-toggle="tab" class="txtLink noLeido" role="tab" aria-expanded="false">Cronología</a></li>         
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
													    <label id="lblNumero" for="txtIdAsunto" class="col-xs-3 col-sm-1 col-md-1 col-lg-1 control-label">Número</label>  
													    <div class="col-xs-4 col-xs-off-5 col-sm-2 col-md-2 col-lg-1">
														    <input id="txtIdAsunto" name="txtIdAsunto" class="form-control input-md margin-bottom-xs-sm-md no-padding-right" type="text" readonly="true" value="" />      
													    </div>
													    <label for="txtDesAsunto" class="col-xs-12 col-md-2 col-lg-1 control-label">Denominación</label>  
													    <div class="col-xs-12 col-md-7 col-lg-9">
														    <input id="txtDesAsunto" name="txtDesAsunto" class="form-control input-md" type="text"  value="" maxlength="50"/>      
													    </div>
												    </div>
												    <div class="form-group">
													    <label for="txtRegistrador" class="col-xs-12 col-md-1 control-label">Registrador</label>  
													    <div class="col-xs-12 col-md-5">
														    <input id="txtRegistrador" name="txtRegistrador" class="form-control input-md margin-bottom-xs-sm-md" type="text" readonly="true" value="" />      
													    </div>

													    <label id="lblResponsable" for="txtResponsable" class="col-xs-12 col-sm-2 col-md-1 control-label txtLinkU">Responsable</label>
													    <div class="col-xs-12 col-md-5">
														    <input id="txtResponsable" name="txtResponsable" class="form-control input-md" type="text" readonly="true" value="" />      
													    </div>
												    </div>
												    <div class="form-group">
										
													    <label for="txtNotificador" class="col-xs-12 col-md-1 control-label">Notificador</label>
													    <div class="col-xs-12 col-md-5">		
														    <input id="txtNotificador" name="txtNotificador" class="form-control input-md margin-bottom-xs-sm-md" type="text"  value="" maxlength="50"/>      					 
													    </div>
													
                                                        <label id="lblCreacion" for="txtFechaCreacion" class="col-xs-7 col-sm-2 col-md-1 col-lg-1 control-label">Creación</label>  
													    <div class="col-xs-5 col-sm-3 col-md-2 col-lg-2">
														    <input id="txtFechaCreacion" name="creacion" class="form-control input-md margin-bottom-xs-sm-md calendar-off" type="text" readonly="true" value="" />      
													    </div>
                                                    										
													    <label for="txtFechaNotificacion" class="col-xs-7 col-sm-2 col-md-1 col-lg-1 control-label">Notificación</label>  
													    <div class="col-xs-5 col-sm-3 col-md-2">
														    <input title="Formato fecha: dd/mm/aaaa" placeholder="dd/mm/aaaa" id="txtFechaNotificacion" name="txtFechaNotificacion" class="form-control input-md margin-bottom-xs-sm-md calendar-off" type="text"  value="" />      
													    </div>		
												    </div> 
												    <div class="form-group">
													    <label for="txtFechaLimite" class="col-xs-7 col-sm-2 col-md-1 col-lg-1 control-label">Límite</label>  
													    <div class="col-xs-5 col-sm-3 col-md-2 col-lg-2">
														    <input title="Formato fecha: dd/mm/aaaa" placeholder="dd/mm/aaaa" id="txtFechaLimite" name="txtFechaLimite" class="form-control input-md margin-bottom-xs-sm-md calendar-off" type="text"  value="" />      
													    </div>
										
													    <label for="txtFechaFinalizacion" class="col-xs-7 col-sm-2 col-md-1 col-lg-1 control-label">Finalización</label>  
													    <div class="col-xs-5 col-sm-3 col-md-2 col-lg-2">
														    <input title="Formato fecha: dd/mm/aaaa" placeholder="dd/mm/aaaa"  id="txtFechaFinalizacion" name="txtFechaFinalizacion" class="form-control input-md calendar-off" type="text"  value="" />      
													    </div> 

													    <label for="txtRefExt" class="col-xs-12 col-md-1 col-lg-1 control-label">Ref.Externa</label>
													    <div class="col-xs-12 col-md-5 col-lg-5">		
														    <input id="txtRefExt" name="txtRefExt" class="form-control input-md margin-bottom-xs-sm-md" type="text"  value="" maxlength="50"/>      					 
													    </div>                 
																			 
												    </div>
												    <div class="form-group">
													    <label for="cboSeveridad" class="col-xs-6 col-sm-3 col-md-1 col-lg-1 control-label">Severidad</label> 
													    <div class="col-xs-6 col-sm-3 col-md-2 col-lg-2">		
													       <select id="cboSeveridad" name="cboSeveridad" class="form-control margin-bottom-xs-sm-md">
															    <option value="1">Crítica</option>
															    <option value="2">Grave</option>
															    <option value="3" selected="selected">Normal</option>
															    <option value="4">Leve</option>
														    </select> 						 
													    </div>     
													    <label for="txtEtp" class="col-xs-8 col-sm-3 col-md-3 col-lg-2 control-label ">Esfuerzo planif. (horas)&nbsp;&nbsp;</label>  
													    <div class="col-xs-4 col-sm-3 col-md-2 col-lg-1 no-padding-left">		
														    <input id="txtEtp" name="txtEtp" class="form-control input-md margin-bottom-xs-sm-md numeroDecimal no-padding-left" maxlength="7" type="text"  value="" />      					 					
													    </div>  
										
													    <label for="txtEtr" class="col-xs-8 col-sm-3 col-md-2 col-lg-2 control-label">Esfuerzo real (horas)</label>  
													    <div class="col-xs-4 col-sm-3 col-md-2 col-lg-1 no-padding-left">			
														    <input id="txtEtr" name="txtEtr" class="form-control input-md numeroDecimal margin-bottom-xs-sm-md no-padding-left" maxlength="7" type="text"  value="" />      					 					
													    </div>
                                                                        
												    </div>
												    <div class="form-group">
													    <label for="cboPrioridad" class="col-xs-6 col-sm-1 col-md-1 col-lg-1 control-label">Prioridad</label> 	                                
													    <div class="col-xs-6 col-sm-2 col-md-3 col-lg-2">	
														    <select id="cboPrioridad" name="cboPrioridad" class="form-control margin-bottom-xs-sm-md">
															    <option value="1">Máxima</option>
															    <option value="2">Alta</option>
															    <option value="3" selected="selected">Media</option>
															    <option value="4">Baja</option>
														    </select>  	                                                                      							 
													    </div>
													    <label for="cboEstado" class="col-xs-6 col-sm-1 col-md-1 col-lg-1 control-label">Estado</label> 
													    <div class="col-xs-6 col-sm-3 col-md-2 col-lg-2">		
														    <select id="cboEstado" name="cboEstado" class="form-control  margin-bottom-xs-sm-md">
															    <option value="0" selected="selected"></option>
															    <option value="1">Registrado</option>
															    <option value="2">Asignado</option>
															    <option value="3">Resuelto</option>
															    <option value="4">Verificado</option>
															    <option value="5">Aprobado</option>
															    <option value="6">Reabierto</option>
														    </select>  							 
													    </div>  
													    <label for="cboTipo" class="col-xs-6 col-sm-1 col-md-1 col-lg-1 control-label">Tipo</label> 
													    <div class="col-xs-6 col-sm-4 col-md-4 col-lg-4">		
														    <select id="cboTipo" name="cboTipo" class="form-control  margin-bottom-xs-sm-md">
															    <option>
																    Seguimiento
															    </option>
														    </select> 								 
													    </div>  
												    </div>
												    <div class="form-group">
													    <label for="txtSistema" class="col-xs-12 col-md-1 col-lg-1 control-label text-left">Sistema</label> 
													    <div class="col-xs-12 col-md-11 col-lg-11">		
														    <input id="txtSistema" name="txtSistema" class="form-control input-md" type="text"  value="" maxlength="50"/>      					 
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
										<div id="datosAsignadoProf" class="collapsed in">	
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
                            <div class="tab-pane" id="crono">                        
                                <div class="form-group" style="margin-top:20px">
                                    <div class="table-responsive">
                                        <table id="tablaCrono" class="table" title="Cronología del asunto">
                                            <thead>
                                                <tr>
                                                    <th class="bg-primary" style="width: 25%">Estado</th>
                                                    <th class="bg-primary" style="width: 25%">Fecha</th>
                                                    <th class="bg-primary" style="width: 50%">Profesional</th>
                                                </tr>
                                            </thead>
                                            <tbody id="bodyTablaCrono">
                                            </tbody>
                                        </table>
                                    </div>
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
                        </div>                         
                        <div id="pieAsunto" class="pull-right" >
                            <div class="col-pie">
                                <button id="btnGrabar" type="button" class="btn btn-primary btn-default-lite">
                                    <span class="fa fa-share-square-o-rev fa-2x fa-blanco" style="vertical-align: middle;"></span>
                                    <span>Grabar</span>
                                </button>
                                <button id="btnSalir" type="button" class="btn btn-default btn-default-lite">
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
    <div class="buscadorUsuario" id="buscProf"></div>       
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
<script src="<% =Session["strServer"].ToString() %>plugins/BuscadorPersonas/BuscadorPersonas.js"></script>