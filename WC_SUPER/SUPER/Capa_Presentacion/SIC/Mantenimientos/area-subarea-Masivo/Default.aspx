<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_SIC_Mantenimientos_area_subarea_Masivo_Default" %>

<%@ Register Src="~/Capa_Presentacion/bsUserControls/Head.ascx" TagPrefix="uc1" TagName="Head" %>
<%@ Register Src="~/Capa_Presentacion/bsUserControls/Menu/Menu.ascx" TagPrefix="uc1" TagName="Menu" %>

<!DOCTYPE html>

<html>
<head>
    <uc1:Head runat="server" ID="Head" />
    <title>Mantenimiento Áreas y Subáreas</title>

    <link href="../../../../plugins/datatables/jquery.dataTables.min.css" rel="stylesheet" />   
</head>


<body>
    <uc1:Menu runat="server" ID="Menu" />
   
        <section>
            <div class="container-fluid">              
                <div class="row">
                    <div class="ibox-title">
                        <h5 class="text-primary">Pool de posibles líderes de Subárea</h5>
                        <h5 class="pull-right"><a class="underline" href ="../../Guias/Mantenimiento_pool_lideres.pdf" target="_blank">Guía</a></h5>
                    </div>
                    <div class="">
                        <div class="col-xs-9">
                            <table id="tblAreaSubarea" class="table table-bordered table-condensed">
                                <thead class="well text-primary">
                                    <tr>                                       
                                        <th class="text-center">Área</th>
                                        <th class="text-center">Subárea</th>                                               
                                        <th class="text-center"><input class="fk_selTodos" type="checkbox" /></th>
                                        <th class="text-center">Pool</th>                                                                                                                        
                                    </tr>
                                </thead>
                            </table>

                            <br />
                            <div class="pull-left">
                                <button id="btnGrabar" class="btn btn-primary">Grabar</button>
                                
                            </div>
                            <div class="pull-right">
                                  <button id="btnAgregar" class="btn btn-primary">Agregar</button>
                                 <button id="btnReemplazar" class="btn btn-primary">Sustituir</button>
                                 <button id="btnEliminar" class="btn btn-primary">Eliminar</button>
                            </div>
                        </div>
                        
                         <div class="col-xs-3">
                             <ul id="ulPool" class="list-group"></ul>                          
                                                         
                             <div>
                                <span id="spanSeleccionarProf" class="underline">Seleccionar profesionales</span><br />                                          
                                <span id="spanCopiarProf" class="underline">Seleccionar profesionales por copia</span>
                             </div>

                        </div>

                         <div class="clearfix"></div>

                    </div>

                </div>
            </div>

        </section>


    
	<div class="modal fade" id="modal-copiarProf">
		<div class="modal-dialog">
			<div class="modal-content">
				<div class="modal-header">
					<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
					<h4 class="modal-title">Copiar profesionales</h4>
				</div>
				<div class="modal-body">
					 <table id="tblCopiarProf" class="table table-bordered table-condensed">
                                <thead class="well text-primary">
                                    <tr>                                       
                                        <th class="text-center">Área</th>
                                        <th class="text-center">Subárea</th>                                                                                                                                                                      
                                    </tr>
                                </thead>

                         <tbody id="tbdCopiarProf"></tbody>

                         
                        </table>
				</div>
				<div class="modal-footer">					
					<button id="btnAceptarModalCopiarProf" type="button" class="btn btn-primary">Aceptar</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
				</div>
			</div>
		</div>
	</div>


    <!--Ayuda multiSelección-->    
    <div id="divAyudaProfesionales"></div>        
    <script src="../../../../plugins/IB/buscaprofmulti.js"></script>

    <script src="../../../../plugins/datatables/jquery.dataTables.min.js"></script>    
    <script src="js/view.js"></script>    
    <script src="js/app.js"></script>    
    </body>

    </html>



