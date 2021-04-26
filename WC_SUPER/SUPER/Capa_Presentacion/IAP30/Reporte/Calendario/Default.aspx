<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Reporte_Calendario_Default" %>

<%@ Register Src="~/Capa_Presentacion/bsUserControls/Menu/Menu.ascx" TagPrefix="cb1" TagName="Menu" %>
<%@ Register Src="~/Capa_Presentacion/bsUserControls/Head.ascx" TagPrefix="cb1" TagName="Head" %>
<%@ Import Namespace="IB.SUPER.Shared" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    <cb1:Head runat="server" id="Head" />  
    <link href="plugins/MenuLateral/BootSideMenu.css" rel="stylesheet"/>
	<link href="plugins/CalendarioAnual/bic_calendar.css" rel="stylesheet"/>	
	<link href="plugins/CalendarioIAP/monthly.css" rel="stylesheet"/>   
     <title>::: SUPER ::: - Calendario de días reportados</title>    

</head>

<body>    
     <script>
         var opciones = { delay: 1 }
         IB.procesando.opciones(opciones);
         IB.procesando.mostrar();
    </script>
    <cb1:Menu runat="server" id="Menu" />
    <form id="frmDatos" runat="server"> </form>
   <div class="container ocultable" id="container">       

            <h1 class="sr-only">::: SUPER ::: - Calendario de días reportados</h1>
            <div id="slideImp" class="list-group" style="display:none;" >				
                <div class="panel panel-primary" >	
                    <div class="panel-heading">
						<h2 id="mes">::: SUPER ::: - Imputaciones a tareas de </h2>
					</div>							
					<div class="panel-body" id="divTareasMov">			
                        
                        <div class="row" tabindex="0">
							<span class="col-xs-12"> - Último día reportado: <strong id="lblFUIMov"></strong></span>								    
							<span class="col-xs-12"> - Último mes cerrado: <strong id="lblUMCMov"><%=Fechas.AnnomesAFechaDescLarga((int)Session["UMC_IAP"])%></strong></span>
						</div>    
                         <div class="botoneraInferior">
                            <div class="btn-group btn-group-xs botonesApertura" role="group">
                                <button id="nivel1Mov" data-level="1" aria-label="Nivel 1" title="Proyectos económicos" type="button" class="btn btn-default btn-transparente">
                                    <span class="fa fa-square fa-square-1 nivelVerde barra1"></span>
                                </button>
                                <button id="nivel2Mov" data-level="2" aria-label="Nivel 2" title="Proyectos técnicos" type="button" class="btn btn-default btn-transparente">
                                    <span class="fa fa-square fa-square-2 nivelGris barra2"></span>
                                </button>
                                <button id="nivel3Mov" data-level="3" aria-label="Nivel 3" title="Tareas" type="button" class="btn btn-default btn-transparente">
                                    <span class="fa fa-square fa-square-3 nivelGris barra3"></span>
                                </button>
                                <button id="nivel4Mov" data-level="4" aria-label="Nivel 4" title="Consumos" type="button" class="btn btn-default btn-transparente">
                                    <span class="fa fa-square fa-square-4 nivelGris barra4"></span>
                                </button>
                            </div>
                            <span title="Despliegue completo de la línea seleccionada" id="icoBombMov" tabindex="0" role="button" aria-label="Despliegue completo de la línea seleccionada" class="fa fa-bomb fa-1-5x txtLink"></span>
                    
                         </div>      
                        <div class="infoTareasMes">              
                         <div class="table-responsive">
                             <div class="div-tableMov">
                                <h2 class="sr-only">Tabla de resultados</h2>
                                <table id="tablaCabeceraMov" class="table table-bordered table-no-border table-hover table-condensed" summary="Tabla de cabecera">
                                    <thead class="">
                                        <tr>
                                            <th id="ET" class="bg-primary columna1">Estructura técnica / Fecha consumo</th>
                                            <th id="H"  class="bg-primary text-right columna2">Horas</th>                    
                                        </tr>
                                    </thead>
                                </table>
                             </div>
                            <div id="contenedorMov" class="div-table-content" style="max-height: 500px;overflow-y: auto; margin-top:10px;">
                                <h2 class="sr-only">Tabla de contenido</h2>                                
                                <table id="tblDatosMov" class="table table-no-border table-hover table-condensed" summary="Tabla de contenido">
                                    <tbody id="bodyTablaMov" class="bodyTabla">
                                    </tbody>
                                </table>
                            </div>
                            
                            <table id="tablaPieMov" class="table table-bordered table-no-border table-hover table-condensed" summary="Tabla de pie">
                                <tfoot class="">
                                    <tr  data-level="0" class="pieTabla bg-info" data-id="pieTabla" data-parent="">
                                        <td class="celdaTxtTotal columna1"><span>Nº horas imputadas:</span><span class="sr-only">Totales</span></td>
                                        <td class="celdaTotal PieHorasImputadas columna2"></td>
                                    </tr>
                                    <tr  data-level="0" class="pieTabla bg-info PieFilaHorasAImputar" data-id="pieTabla" data-parent="">
                                        <td class="celdaTxtTotal columna1"><span>Nº horas a imputar según calendario:</span><span class="sr-only">Totales</span></td>
                                        <td class="celdaTotal PieHorasAImputar columna2"></td>
                                    </tr>
                                </tfoot>
                            </table>
                           
                        </div>
                        </div>	
					</div>
                </div>
               </div>
			<div class="row">
				<div class="col-xs-12 col-md-7">
                    <div class="calendario">							
						<table class="monthly" id="cal" tabindex="0" title="Calendario mensual de días reportados"></table>
					</div>
                    
                    <div class="leyendas row">
                    <div class="divLeyendasDias">	
                        <span class="col-xs-12 col-sm-2 text-left" style="font-weight:bold">Días:</span>
                        <span  class="col-xs-12 col-sm-10">
                            <span style="white-space:nowrap"><span class="fa fa-circle" style="color: #23527c;"></span><span > Registrados</span></span>
                            <span style="white-space:nowrap"><span class="fa fa-circle" style="color: #067607;"></span><span> Sin dato&nbsp;</span></span>
                            <span style="white-space:nowrap"><span class="fa fa-circle" style="color: #9C27B0;"></span><span> Futuros</span></span>
                            <span style="white-space:nowrap"><span class="fa fa-circle" style="color: #bd2806;"></span><span> Festivos</span></span>
                            <span style="white-space:nowrap"><span class="fa fa-circle" style="color: #9e5e01;"></span><span> Vacaciones</span></span> 
                        </span>                                          
                    </div>	
                    <div class="divLeyendasHoras hidden-xs">	                        
                        <span class="col-xs-12 col-sm-2 text-left" style="font-weight:bold">Horas:</span>
                        <span  class="col-xs-12 col-sm-10">
                            <span style="white-space:nowrap"><span class="fa fa-circle" style="color: #23527c;"></span><span> Registradas</span></span>
                            <span style="white-space:nowrap"><span class="fa fa-circle" style="color: #000000;"></span><span> Estándar</span></span>  
                            <span style="white-space:nowrap"><span class="fa fa-circle" style="color: #9e5e01;"></span><span > Planificadas</span></span>                        
                       </span>                                        
                    </div>	
                    </div>								
				</div>
				<div class="col-xs-12 col-md-5">      
                    <!--<div class="ibox-content blockquote blockquote-info">-->
                    <div class="panel-group">
                        <div class="ibox-title hidden-xs hidden-sm">
                            <div class="row hidden-xs hidden-sm text-center">
                                <div>
                                    <h2 class="sr-only">Opciones de pantalla</h2>
                                    <div class="col-xs-4">
							            <div id="anual" title="Detalle horario del calendario" class="divIconos icono link">		
                                                <span class="fa fa-calendar fa-5x"></span><br/>
                                                <span class="txtIcono spDesCalendario" role="link"  tabindex="0" aria-label="<%=Session["DESCALENDARIO_IAP"]%>"><%=Session["DESCALENDARIO_IAP"]%></span>
						                </div>
                                    </div>
						            <div class="col-xs-4">
							            <div id="divProf" title="Cambio de profesional" class="divIconos icono link">		
                                                <span id="icoProf" class="fa fa-user fa-5x"></span><br/>
                                                <span class="txtIcono" role="link" aria-label="Cambio de profesional" tabindex="0" id="spProfesional"><%=Session["DES_EMPLEADO_IAP"]%></span>
							            </div>
						            </div>
						            <div class="col-xs-4">
							            <div id="agenda" title="Agenda" class="divIconos icono link">						
								                <span id="icoAgenda" class="fa fa-book fa-5x"></span><br/>
									            <span role="link" class="txtIcono" aria-label="Agenda" tabindex="0">Agenda</span>																	
							            </div>
						            </div>	
                                </div>
                            </div>
                            <!--Limpiamos los floats-->
                            <div class="clearfix"></div>
                        </div>    
                      <div class="ibox-content" id="tareasMensuales">     

                         <div class="row" tabindex="0">
							<span class="col-xs-12 col-lg-6"> - Último día reportado: <strong id="lblFUI" ><%=Session["FEC_ULT_IMPUTACION"]%></strong></span>								    
							<span class="col-xs-12 col-lg-6"> - Último mes cerrado: <strong id="lblUMC"><%=Fechas.AnnomesAFechaDescLarga((int)Session["UMC_IAP"])%></strong></span>
						 </div>
                          <div class="col-xs-12 botoneraInferior">
                            <div class="btn-group btn-group-xs botonesApertura" role="group">
                                <button id="nivel1" data-level="1" aria-label="Nivel 1" title="Proyectos económicos" type="button" class="btn btn-default btn-transparente">
                                    <span class="fa fa-square fa-square-1 nivelVerde barra1"></span>
                                </button>
                                <button id="nivel2" data-level="2" aria-label="Nivel 2" title="Proyectos técnicos" type="button" class="btn btn-default btn-transparente">
                                    <span class="fa fa-square fa-square-2 nivelGris barra2"></span>
                                </button>
                                <button id="nivel3" data-level="3" aria-label="Nivel 3" title="Tareas" type="button" class="btn btn-default btn-transparente">
                                    <span class="fa fa-square fa-square-3 nivelGris barra3"></span>
                                </button>
                                <button id="nivel4" data-level="4" aria-label="Nivel 4" title="Consumos" type="button" class="btn btn-default btn-transparente">
                                    <span class="fa fa-square fa-square-4 nivelGris barra4"></span>
                                </button>
                            </div>
                            <span id="icoBomb" title="Despliegue completo de la línea seleccionada" tabindex="0" role="button" aria-label="Despliegue completo de la línea seleccionada" class="fa fa-bomb fa-1-5x txtLink"></span>
                    
                         </div>
                         <div class="row infoTareasMes">
                            <div class="table-responsive col-md-12">
                                <div class="div-table">
                                    <h2 class="sr-only">Tabla de resultados</h2>
                                    <table id="tablaCabecera" class="table table-bordered table-no-border table-hover table-condensed" summary="Tabla de cabecera">
                                        <thead class="">
                                            <tr>
                                                <th id="ET" class="bg-primary columna1">Estructura técnica / Fecha consumo</th>
                                                <th id="H"  class="bg-primary text-right columna2">Horas</th>                    
                                            </tr>
                                        </thead>
                                    </table>
                                </div>
                                <div id="contenedor" class="div-table-content" style="max-height: 300px;overflow-y: auto; margin-top:10px;">
                                    <h2 class="sr-only">Tabla de contenido</h2>                                
                                    <table id="tblDatos" class="table table-no-border table-hover table-condensed" summary="Tabla de contenido">
                                        <tbody id="bodyTabla" class="bodyTabla">
                                        </tbody>
                                    </table>
                                </div>
                                <div class="div-table">
                                    <table id="tablaPie" class="table table-bordered table-no-border table-hover table-condensed" summary="Tabla de pie">
                                        <tfoot class="">
                                            <tr  data-level="0" class="pieTabla bg-info" data-id="pieTabla" data-parent="">
                                                <td class="celdaTxtTotal columna1"><span>Nº horas imputadas:</span><span class="sr-only">Totales</span></td>
                                                <td class="celdaTotal PieHorasImputadas columna2"></td>
                                            </tr>
                                            <tr  data-level="0" class="pieTabla bg-info PieFilaHorasAImputar" data-id="pieTabla" data-parent="">
                                                <td class="celdaTxtTotal columna1"><span>Nº horas a imputar según calendario:</span><span class="sr-only">Totales</span></td>
                                                <td class="celdaTotal PieHorasAImputar columna2"></td>
                                            </tr>
                                        </tfoot>
                                    </table>
                                </div>
                            </div>
                        </div>	
                     </div>
                    </div>
                    <!--</div>		-->					
				</div>						
			</div>	
            		
			<div id="pieCalendario" class="hidden-md hidden-lg">
                    <div id="divBtnAnual" class="col-xs-4 col-pie no-padding text-center">
                        <button id="btnAnual" type="button" class="boton">
                           <span class="fa fa-calendar fa-3x fa-blanco" style="vertical-align: middle;"></span>
                            <br />
                            <span class="spDesCalendario" style="vertical-align:-webkit-baseline-middle;"><%=Session["DESCALENDARIO_IAP"]%></span>
                        </button>
                    </div>
                    <div id="divBtnUser" class="col-xs-5 col-pie no-padding text-center">
                        <button id="btnUser" type="button" class="boton">
                            <span class="fa fa-user fa-3x fa-blanco" style="vertical-align: middle;"></span>
                            <br />                          
                            <span id="spProfesionalMov" style="vertical-align:-webkit-baseline-middle;"><%=Session["DES_EMPLEADO_IAP"]%></span>
                            
                        </button>
                    </div>
                    <div id="divBtnAgenda" class="col-xs-3 col-pie no-padding text-center">
                        <button id="btnAgenda" type="button" class="boton">
                            <span class="fa fa-book fa-3x fa-blanco" style="vertical-align: middle;"></span>
                            <br />
                            <span style="vertical-align:-webkit-baseline-middle;">Agenda</span>
                        </button>
                    </div>                
             </div>
           
        </div>
      						
		<div id="indicator"></div>     
		<!-- Modal de calendario anual -->
        <div class="modal fade" id="calendarioAnual" role="dialog" tabindex="-1" title=":::SUPER::: - Detalle horario del calendario">
            <div class="modal-dialog modal-lg" role="dialog">
                <div class="modal-content">
                    <div class="modal-header bg-primary">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar"><span aria-hidden="true">&times;</span></button>
                        <h1 class="modal-title" id="tituloCalendarioAnual">::: SUPER ::: - Detalle horario del calendario</h1>
                    </div>
                    <div class="modal-body" id="contenidoCalendarioAnual">
						<div class="row row-modal">
                             <div class="col-md-12" tabindex="0" title="Calendario anual">
								<span class="tituloCalendario spDesCalendario"><%=Session["DESCALENDARIO_IAP"]%></span>
								<br/>
                              
								<div id="calendari_lateral1"></div>
							</div>
						</div>     
                        
                    </div>
                    <div class="modal-footer">
                       <b><button id="btnCancelar" class="btn btn-default" data-dismiss="modal">Cerrar</button></b>                    
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>       

        <div class="buscadorUsuario" id="buscProf"></div>
    </body>
    
</html>

<script src="<% =Session["strServer"].ToString() %>Capa_Presentacion/IAP30/scripts/JavaScript.js" type="text/javascript"></script>
<script src="js/View.js?20170927_01"></script>
<script src="js/app.js?20170831_01"></script>
<script src="js/javascript.js"></script>
<script src="plugins/CalendarioIAP/monthly.js?20180115_01"></script>		
<script src="plugins/MenuLateral/BootSideMenu.js"></script>
<script src="plugins/CalendarioAnual/bic_calendar.js"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/moment.locale.min.js"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/accounting.min.js"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/stringbuilder.js"></script>
<script src="<% =Session["strServer"].ToString() %>plugins/BuscadorPersonas/BuscadorPersonas.js?20170524"></script>
    
