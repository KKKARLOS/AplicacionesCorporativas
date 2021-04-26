<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Reporte_BitacoraPT_Default" %>
<%@ Register Src="~/Capa_Presentacion/bsUserControls/Menu/Menu.ascx" TagPrefix="cb1" TagName="Menu" %>
<%@ Register Src="~/Capa_Presentacion/bsUserControls/Head.ascx" TagPrefix="cb1" TagName="Head" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    <cb1:Head runat="server" id="Head" />      
    <link href="<% =Session["strServer"].ToString() %>Capa_Presentacion/IAP30/Reporte/Bitacora/css/StyleSheet.css" rel="stylesheet"/>
    <title>::: IAP 3.0 ::: Bitácora de proyecto técnico</title>  
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="Stylesheet" href="<% =Session["strServer"].ToString() %>css/jquery-ui.min.css"/>
    <link rel="Stylesheet" href="<% =Session["strServer"].ToString() %>plugins/datatables/jquery.dataTables.min.css"/>
    <link rel="Stylesheet" href="<% =Session["strServer"].ToString() %>plugins/datatables/Buttons/buttons.dataTables.min.css"/>

</head>
<body>
    <script>
        var opciones = { delay: 1 }
        IB.procesando.opciones(opciones);
        IB.procesando.mostrar();
    </script>

    <cb1:Menu runat="server" id="Menu" />
    <form id="frmDatos" class="form-horizontal" runat="server"></form>
    <div class="container-fluid">
        <h1 class="hiddenStructure">::: IAP 3.0 ::: Bitácora de proyecto técnico</h1>
	    <br />
        <div class="ibox-content blockquote blockquote-info" >
            <div class="row">
                <div class="ibox-title">
			        <div class="panel-group">
                       <form class="form-horizontal">                    
                            <div class="form-group row">      
                                <div class="col-xs-3 col-sm-2 col-lg-1">
                                    <label for="idProyecto" class="control-label inline">Proyecto </label>                            
                                     <span class="fa fa-diamond fa-diamond-verde inline"></span>
                                </div>       
                                <div class="col-xs-4 col-sm-2 col-lg-2">
                                    <input id="idProyecto" name="idProyecto" class="form-control input-md" type="text" readonly="readonly" aria-readonly="true" value="" runat="server"/>
                                </div>
                                <div class="col-xs-12 col-md-8">
                                    <input id="desProyecto" name="desProyecto" class="form-control input-md" type="text" readonly="readonly" aria-readonly="true" value="" title="Descripción del proyecto" runat="server"/>      
                                </div>
                            </div>
                           <br class="visible-xs" />
                            <div class="form-group row">	
                	            <div class="col-xs-3 col-sm-2 col-lg-1">
                                    <label for="desProyectoT" class="control-label inline">P.Técnico</label>
                                </div>
                                <div class="col-xs-4 col-sm-2 col-lg-2">
                                    <input id="idPT" name="idPT" class="form-control input-md" type="text" readonly="readonly" aria-readonly="true" value="" runat="server"/>
                                </div>
                                <div class="col-xs-12 col-md-8">
                                     <input id="desProyectoT" name="desProyectoT" class="form-control input-md" type="text" readonly="readonly" aria-readonly="true" runat="server"/>
                                </div>  
                            </div>                     
                            <br class="visible-xs" />
                            <div class="form-group row">		
                                 <div class="col-xs-12 col-md-12">
						            <fieldset id="fieldsetAsunto" class="fieldset">
                                        <h2 class="hiddenStructure">información sobre asuntos</h2>           
							            <legend class="fieldset"><span class="text-nowrap">Asunto</span></legend>
                                        <div class="form-group">
                                              <label for="tipo" class="col-xs-12 col-md-1 control-label">Tipo</label>  
                                              <div class="col-md-5">
                                                <select id="cboTipoAsunto" name="tipo" class="form-control"></select>  	
                                             </div>
                                            <div class="col-md-1"></div>
                                             <label for="estado" class="col-xs-12 col-md-1 control-label">Estado</label>  
                                             <div class="col-md-3">
                                                <select id="cboEstado" name="estado" class="form-control">
                                                   <option selected="selected" value="-1">TODOS</option>
                                                   <option value="1">Registrado</option>
                                                   <option value="2">Asignado</option>
                                                   <option value="3">Resuelto</option>
                                                   <option value="4">Verificado</option>
                                                   <option value="5">Aprobado</option>
                                                   <option value="6">Reabierto</option>
                                                </select>  	
                                             </div>
                                            <div class="col-md-1"></div>
                                        </div>
                               
                                         <div class="row">
                                             <div class="col-xs-12">	
                                                 <div class="table-responsive">
                                                    <table id="tablaAsunto" class="table table-bordered table-condensed" title="Asuntos relacionados">
                                                        <thead>    
                                                            <tr>
                                                                <th data-type="String" class="bg-primary ">Denominación</th>
                                                                <th data-type="String" class="bg-primary ">Tipo</th>
                                                                <th data-type="String" class="bg-primary ">Severidad</th>
                                                                <th data-type="String" class="bg-primary ">Prioridad</th>
                                                                <th data-type="DateTime" class="bg-primary ">Límite</th>
                                                                <th data-type="DateTime" class="bg-primary ">Notificación</th>
                                                                <th data-type="String" class="bg-primary ">Estado</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody id="bodyAsuntos"></tbody>
                                                    </table>
                                                   </div>
                                            </div>
                                        </div>
                                        <div id="botonesAsunto">
                                            <div id="btngoAsunto" class="botonTabla" style="margin-right:10px;">      
                                                <span id="goAsunto" role="link" aria-label="Acceso al detalle de asunto" class="fa fa-arrow-right" title="Acceso al detalle de asunto"></span>
                                            </div>
                                            <div id="btnAnadirAsunto" class="botonTabla btnModif">      
                                                <span id="nuevoAsunto" role="link" aria-label="Crear nuevo asunto" class="fa fa-plus" style="color: green"></span>
                                            </div>
                                            <div class="botonTabla btnModif" style="margin-left: 10px;">                      
                                                <span id="eliminarAsunto" role="link" aria-label="Eliminar asunto" class="fa fa-trash" style="color: red"></span>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
				            </div>
                             <div class="row">
                                 <div class="col-xs-12 col-md-6">
						            <fieldset id="fieldsetAccion" class="fieldset">
                                        <h2 class="hiddenStructure">Información sobre acciones</h2>     
							            <legend class="fieldset"><span class="text-nowrap">Acción</span></legend>                            
                                         <div class="row">
                                             <div class="col-xs-12">	
                                                 <div class="table-responsive">
                                                    <table id="tablaAccion" class="table table-bordered table-condensed" title="Acciones relacionadas">
                                                        <thead>
                                                            <tr>
                                                                <th data-type="String" class="bg-primary" style="width: 55%">Denominación</th>
                                                                <th data-type="DateTime" class="bg-primary" style="width: 15%">Limite</th>
                                                                <th data-type="Double" class="bg-primary" style="width: 15%">Avance</th>
                                                                <th data-type="DateTime" class="bg-primary" style="width: 15%">Finalización</th>
                                                            </tr>                                                            
                                                        </thead>
                                                        <tbody id="bodyAcciones">
                                                        </tbody>
                                                    </table>
                                                   </div>
                                            </div>
                                        </div>
                                        <div id="botonesAccion">
                                            <div id="btngoAccion" class="botonTabla" style="margin-right:10px;">      
                                                <span id="goAccion" role="link" aria-label="Acceso al detalle de acción" class="fa fa-arrow-right" title="Acceso al detalle de acción"></span>
                                            </div>
                                            <div id="btnAnadirAccion" class="botonTabla btnModif">      
                                                <span id="nuevaAccion" role="link" aria-label="Crear nueva acción" class="fa fa-plus" style="color: green"></span>
                                            </div>
                                            <div class="botonTabla btnModif" style="margin-left: 10px;">                      
                                                <span id="eliminarAccion" role="link" aria-label="Eliminar acción" class="fa fa-trash" style="color: red"></span>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                                 <div class="col-xs-12 col-md-6">
						            <fieldset id="fieldsetTecnico" class="fieldset">
                                        <h2 class="hiddenStructure">Información sobre tareas</h2>     
							            <legend class="fieldset"><span class="text-nowrap">Bitácoras de tareas</span></legend>                            
                                         <div class="row">
                                             <div class="col-xs-12">	
                                                 <div class="table-responsive">
                                                    <table id="tablaT" class="table" title="tareas relacionadas">
                                                        <thead>
                                                            <tr>
                                                                <th data-type="String" class="cabecera centrado" style="width: 100%">Denominación</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody id="bodyT"></tbody>
                                                    </table>
                                                   </div>
                                            </div>

                                            <div class="botonTabla pull-right" style="margin-right:20px;">                      
                                                <span id="accesoT" role="link" aria-label="Acceso a bitácora de tarea" 
                                                    class="fa fa-arrow-right" title="Acceso a bitácora de tarea"></span>
                                            </div>

                                         </div>                            
                                    </fieldset>                        
                                 </div>
				            </div>
                            <div class="row">
                                 <div id="pieTarea" class="pull-right">                             
                                    <div class="col-xs-12 col-pie">
                                        <button id="btnSalir" type="button" class="btn btn-default btn-default-lite">
                                            <span class="fa fa-share-square-o-rev fa-2x fa-blanco" style="vertical-align: middle;"></span>
                                            <span>Volver</span>
                                        </button>
                                    </div>            
                                </div>   
                            </div>        
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>       
</body>
</html>
<script src="<% =Session["strServer"].ToString() %>Capa_Presentacion/IAP30/scripts/JavaScript.js" type="text/javascript"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/jquery-ui.min.js"></script>
<script src="<% =Session["strServer"].ToString() %>plugins/datatables/jquery.dataTables.min.js"></script>
<script src="<% =Session["strServer"].ToString() %>plugins/datatables/Buttons/dataTables.buttons.min.js"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/moment.locale.min.js"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/accounting.min.js" type="text/javascript" ></script>
<script src="<% =Session["strServer"].ToString() %>scripts/stringbuilder.js" type="text/javascript" ></script>
<script src="<% =Session["strServer"].ToString() %>scripts/exportaciones.js" type="text/javascript" ></script>
<script src="js/view.js?20170502_01" type="text/javascript"></script>
<script src="js/app.js?20170502_01" type="text/javascript"></script>

