 <ul id="tabs" class="nav nav-tabs" data-tabs="tabs">
    <li class="active"><a href="#general" data-toggle="tab">General</a></li>
    <li><a href="#crono" data-toggle="tab">Cronología</a></li>         
    <li><a href="#docu" data-toggle="tab">Documentación</a></li>                       
</ul>  
    <br />                       
    <div id="my-tab-content" class="tab-content">
    <div class="tab-pane active" id="general">                           
			<div class="col-xs-12">			   
            <fieldset class="fieldset ">                                
                <legend class="fieldset"><span>Asunto</span></legend>	                                     
                <div class="row">	
                    <div class="col-xs-12 col-md-1">							
					    <label for="numeroAsunto">Número</label> 
                    </div>
                    <div class="col-xs-12 col-md-1">                                      
					    <input id="numeroAsunto" class="txtCampo" type="text"/>									 
				    </div>
                    <div class="col-xs-12 col-md-2">	
                         <label for="denominacion">Denominación</label>    
                    </div>
                    <div class="col-xs-12 col-md-8">	
					    <input id="denominacion" class="txtCampo" type="text"/>
				    </div>
                </div>
                <div class="row">	
                    <div class="col-xs-12 col-md-1">							
					    <label for="registrador">Registrador</label>   
                    </div>
                    <div class="col-xs-12 col-md-4">		
					    <input id="registrador" class="txtCampo" type="text"/>									 
				    </div>
                    <div class="col-xs-12 col-md-1">							
					    <label for="creacion">Creación</label>  
                    </div>  
                     <div class="col-xs-12 col-md-1">							
					    <input id="creacion" class="txtCampo" type="text"/>
                    </div>  
                    <div class="col-xs-12 col-md-1">							
					   <label for="responsable">Responsable</label>    
                    </div>  
					<div class="col-xs-12 col-md-4">							
					   <input id="responsable" class="txtCampo" type="text"/>
                    </div>     
                </div>
                <div class="row">	
                    <div class="col-xs-12 col-md-1">							
					    <label for="notificador">Notificador</label>   
                    </div>
                    <div class="col-xs-12 col-md-4">		
					    <input id="notificador" class="txtCampo" type="text"/>									 
				    </div>
                    <div class="col-xs-6 col-md-1">							
					    <label for="notificacion">Notificación</label>  
                    </div>  
                     <div class="col-xs-6 col-md-1">							
					    <input id="notificacion" class="txtCampo" type="text"/>
                    </div>  
                    <div class="col-xs-6 col-md-1">							
					   <label for="limite">Límite</label>    
                    </div>  
					<div class="col-xs-6 col-md-1">							
					   <input id="limite" class="txtCampo" type="text"/>
                    </div>   
                     <div class="col-xs-6 col-md-1">							
					   <label for="finalizacion">Finalización</label>    
                    </div>  
					<div class="col-xs-6 col-md-2">							
					   <input id="finalizacion" class="txtCampo" type="text"/>
                    </div>     
                </div>
                 <div class="row">	
                    <div class="col-xs-12 col-md-1">							
					    <label for="refExterna">Ref.Externa</label>   
                    </div>
                    <div class="col-xs-12 col-md-4">		
					    <input id="refExterna" class="txtCampo" type="text"/>									 
				    </div>
                    <div class="col-xs-12 col-md-3">							
					    <label for="esfuerzoPlanificado">Esfuerzo planificado(horas)</label>  
                    </div>  
                     <div class="col-xs-12 col-md-1">							
					    <input id="esfuerzoPlanificado" class="txtCampo" type="text"/>
                    </div>  
                    <div class="col-xs-12 col-md-2">							
					   <label for="esfuerzoHoras">Esfuerzo real(horas)</label>    
                    </div>  
					<div class="col-xs-12 col-md-1">							
					   <input id="esfuerzoHoras" class="txtCampo" type="text"/>
                    </div>   
                       
                </div>
                <div class="row">	
                    <div class="col-xs-12 col-md-1">							
					    <label for="severidad">Severidad</label>   
                    </div>
                    <div class="col-xs-12 col-md-1">		
					    <select id="severidad">
                            <option>
                                Normal
                            </option>
                        </select> 								 
				    </div>
                    <div class="col-xs-12 col-md-1">							
					    <label for="prioridad">Prioridad</label>   
                    </div>
                    <div class="col-xs-12 col-md-1">		
					    <select id="prioridad">
                            <option>
                                Media
                            </option>
                        </select> 								 
				    </div>
                    <div class="col-xs-12 col-md-1">							
					    <label for="tipo">Tipo</label>   
                    </div>
                    <div class="col-xs-12 col-md-4">		
					    <select id="tipo" class="txtCampo">
                            <option>
                                Seguimiento
                            </option>
                        </select> 								 
				    </div>  
                    <div class="col-xs-12 col-md-1">							
					    <label for="estado">Estado</label>   
                    </div>
                    <div class="col-xs-12 col-md-1">		
					    <select id="estado">
                            <option>
                                Registrado
                            </option>
                        </select> 								 
				    </div>  
                </div>
                 <div class="row">	
                    <div class="col-xs-12 col-md-2">							
					    <label for="sistemaAfectado">Sistema afectado</label>   
                    </div>
                    <div class="col-xs-12 col-md-4">		
					    <input id="sistemaAfectado" class="txtCampo" type="text"/>									 
				    </div>
                </div>
                <div class="row">	
                    <div class="col-xs-12 col-md-6">							
					    <label for="descripcion">Descripción</label>   
                        <br />		
					    <textarea id="descripcion" class="txtCampo" rows="3"/>									 
				    </div>
                    <div class="col-xs-12 col-md-6">							
					    <label for="observaciones">Observaciones</label>   
                        <br />
					    <textarea id="observaciones" class="txtCampo" rows="3"/>									 
				    </div>
                </div>
            </fieldset>
                <fieldset class="fieldset">
					<legend class="fieldset"><span class="text-nowrap">Asignado a</span></legend>
					<div class="row">	
                        <div class="col-xs-12 col-md-6">							
					        <label for="departamento">Departamento/Grupo</label>   
                            <br />
                            <textarea id="departamento" class="txtCampo" rows="3"/>	
                        </div>
                       
                        <div class="col-xs-12 col-md-6">							
					        <label for="notificaciones">Notificar a(e-mail separados por ;)</label>   
                            <br/>	
					        <textarea id="notificaciones" class="txtCampo" rows="3"/>									 
				        </div>
                    </div>
					<div id="divProfesionales">
						<div class="row">
							<div id="lisProfesionales" class="text-left dual-list list-left col-xs-5">
								<span><b>Profesionales</b></span>
								<div class="marco">
									<div class="row">
										<div class="btn-group col-xs-2">
											<a class="btn btn-default selector" data-toggle="tooltip" title="Seleccionar todos los profesionales">
												<i class="glyphicon glyphicon-unchecked"></i>
											</a>
										</div>
										<div class="col-xs-10 input-group">
											<input type="text" name="SearchDualList" class="form-control" placeholder="Buscar" />
										</div>
									</div>
									<ul id="ulProfesionales1" class="list-group">
									<li class="list-group-item" value="10083">ABAD GARCIA, Maria Isabel</li><li class="list-group-item" value="8809">ABALIA MACIAS, Felix Angel</li><li class="list-group-item" value="14148">ABARRATEGI URIBARRI, Gorka</li><li class="list-group-item" value="10968">ABAUNZA ZEARSOLO, Xabier</li></ul>
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
							<div id="lisProfesionales2" class="dual-list list-right col-xs-5">
								<span><b>Asignados</b></span>
								<div class="marco">
									<div class="row">
										<div class="btn-group col-xs-2">
											<a class="btn btn-default selector" data-toggle="tooltip" title="Seleccionar todos los profesionales">
												<i class="glyphicon glyphicon-unchecked"></i>
											</a>
										</div>
										<div class="col-xs-10 input-group">
											<input type="text" name="SearchDualList" class="form-control" placeholder="Buscar" />
										</div>
									</div>
									<ul id="ulProfesionales2" class="list-group">
									<li class="list-group-item" value="8706">ABAL TIRAPU, Iban</li></ul>
								</div>
							</div>
						</div>
					</div>
                </fieldset>
            </div>                   
                             
        </div>                         
        <div class="tab-pane" id="crono">
            <fieldset class="fieldset">
                <div class="row">
                    <span>Investigación/Detección</span>
                    <textarea class="txtArea" rows="3"></textarea>
                </div>
                <div class="row">
                    <span>Acciones/Modificaciones</span>
                    <textarea class="txtArea" rows="3"></textarea>
                </div>
                <div class="row">
                    <span>Pruebas</span>
                    <textarea class="txtArea" rows="3"></textarea>
                </div>
                <div class="row">
                    <span>Pasos a producción</span>
                    <textarea class="txtArea" rows="3"></textarea>
                </div>
            </fieldset>
        </div>
        <div class="tab-pane" id="docu">
            <fieldset id="fieldsetDocu" class="fieldset">
                <div class="row">
                    <div class="table-responsive">
                        <table id="tablaDocu" class="table stacktable">
                            <tbody>
                                <tr>
                                    <th class="cabecera" style="width:25%">Descripción</th>
                                    <th class="cabecera" style="width:25%">Archivo</th>
                                    <th class="cabecera" style="width:25%">Link</th>
                                    <th class="cabecera" style="width:25%">Autor</th>
                                </tr>
                                <tr class="linea">
                                    <td class="celdaHoras"><span>Prueba bbbbbbbbbbbbbbb</span></td>
                                    <td class="celdaHoras"><span>prueba.txt</span></td>
                                    <td class="celdaHoras"><span>prueba</span></td>
                                    <td class="celdaHoras"><span>prueba</span></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div id="botonesDocu" class="hidden-xs">
                    <div id="btnAnadir" class="botonTabla">      
                        <a href="#">                              
                            <span class="fa fa-plus" style="color: green"></span>
                        </a>
                    </div>
                    <div class="botonTabla" style="margin-left: 10px;">                      
                        <a href="#">
                            <span class="fa fa-trash" style="color: red"></span>
                        </a>
                    </div>
                </div>
            </fieldset>
        </div>
    </div> 

    <br/>    
