<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Consultas_ParteAct_Default" %>

<%@ Register Src="~/Capa_Presentacion/bsUserControls/Menu/Menu.ascx" TagPrefix="cb1" TagName="Menu" %>
<%@ Register Src="~/Capa_Presentacion/bsUserControls/Head.ascx" TagPrefix="cb1" TagName="Head" %>

<!DOCTYPE html>

<html lang="es" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <cb1:Head runat="server" id="Head" />
    <title>::: SUPER ::: - Consulta de Partes de actividad</title>    
    <link rel="stylesheet" type="text/css" href="<% =Session["strServer"].ToString() %>plugins/daterangepicker/daterangepicker.css?20170503_01" />
    <link rel="Stylesheet" href="<% =Session["strServer"].ToString() %>plugins/datatables/jquery.dataTables.min.css"/>
    <link rel="Stylesheet" href="<% =Session["strServer"].ToString() %>plugins/datatables/Buttons/buttons.dataTables.min.css"/>
    <link type="text/css" href="<% =Session["strServer"].ToString() %>plugins/datatables/Checkboxes/dataTables.checkboxes.css" rel="stylesheet" />   
</head>

<body>
    <script>
        var opciones = { delay: 1 }
        IB.procesando.opciones(opciones);
        IB.procesando.mostrar();
        //SSRS
        var servidorSSRS ="<%=Session["ServidorSSRS"]%>";
        //SSRS
    </script>
    <cb1:Menu runat="server" id="Menu" />    
        
    <%--<form id="form1" runat="server"></form>--%>
    <form id="frmDatos" runat="server">
     <input type="hidden" id="FORMATO" name="ctl00$CPHC$FORMATO" value="PDF" />
     <input type="hidden" id="hdnConsumos" name="ctl00$CPHC$hdnConsumos"  value="" />
    </form>
    <div class="container-fluid ocultable">
        <div class="ibox-content blockquote blockquote-info">
        <div class="panel-group">
            <div class="ibox-title">
        <h1 class="sr-only">::: SUPER ::: - Consulta de Partes de actividad</h1>

        <div class="row">
            <h2 class="sr-only">Criterios de selección</h2>
            <div class="col-xs-12 col-sm-4">
                <fieldset class="fieldset">
                    <legend class="fieldset"><span>Profesional </span></legend>
                    <div tabindex="0" id="divProf" aria-label="Seleccción de profesional" class="col-xs-12 no-padding text-center">
                        <span><%=Session["DES_EMPLEADO_IAP"]%></span>
                    </div>
                </fieldset>
            </div>
            <div class="col-xs-12 col-sm-4">
                <fieldset class="fieldset">
                    <legend class="fieldset"><span>Tareas</span></legend>
                    <div class="col-xs-6 col-sm-12 col-md-6 checkbox no-margin">
                        <label>
                            <input id="chkFacturables" type="checkbox" class="fk_chk fk_filtro" checked="checked"/>Facturables
                        </label>
                    </div>
                    <div class="col-xs-6 col-sm-12 col-md-6 checkbox no-margin text-nowrap">
                        <label>
                            <input id="chkNoFacturables" type="checkbox" class="fk_chk fk_filtro" checked="checked"/>No facturables
                        </label>
                    </div>
                </fieldset>
            </div>
            <div class="col-xs-12 col-sm-4">
                <fieldset class="fieldset">
                    <legend class="fieldset"><span>Periodo</span></legend>
                        <div class="form-group">
                            <div class="col-xs-12 col-md-10 col-md-offset-1 col-lg-8 col-lg-offset-2">
                                <input title="Formato fecha: dd/mm/aaaa - dd/mm/aaaa" placeholder="dd/mm/aaaa - dd/mm/aaaa" id="txtRango" type="text" class="form-control input-md fk_filtro" value="" />
                            </div>
                        </div>
                </fieldset>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12 col-sm-4">
                <fieldset class="fieldset">
                    <legend class="fieldset"><span role="heading" aria-level="3"><span tabindex="0" role="link" class="underline btn-link" title="Seleccción de proyectos" aria-label="Seleccción de proyectos" id="btnProyecto">Proyecto</span><span id="btnEliminarProyectos" tabindex="0" role="button" title="Eliminar la selección de proyectos actual" aria-label="Eliminar la selección de proyectos actual" class="fa fa-trash-o fa-1x"></span></span></legend>
                    <div class="col-xs-12 no-padding">
                        <ul id="listaProyectos" class="no-padding fk_lista fk_filtro"></ul>
                            <%--<li data-psn ="123165">P1</li>--%>                        
                    </div>
                </fieldset>
            </div>
            <div class="col-xs-12 col-sm-4">
                <fieldset class="fieldset">
                    <legend class="fieldset"><span role="heading" aria-level="3"><span tabindex="0" role="link" class="underline btn-link" title="Seleccción de cliente" aria-label="Seleccción de cliente" id="btnCliente">Cliente</span><span id="btnEliminarClientes" tabindex="0" role="button" title="Eliminar la selección de clientes actual" aria-label="Eliminar la selección de clientes actual" class="fa fa-trash-o fa-1x"></span></span></legend>
                    <div class="col-xs-12 no-padding">
                        <ul id="listaClientes" class="no-padding fk_lista fk_filtro"></ul>
                            <%--<li data-id ="123165">P1</li>--%> 
                    </div>
                </fieldset>
            </div>
            <div class="col-xs-12 col-sm-4">
                <fieldset class="fieldset">
                    <legend class="fieldset"><span>Modelo de parte</span><a id="btnExportar" class="dt-button" tabindex="0" aria-controls="tabla2" href="#">
                            <i class="fa fa-file-pdf-o"></i>
                            <span>PDF</span>
                        </a></legend>
                    <div class="col-xs-12 no-padding">
                        <div class="radio no-margin no-padding">
                            <label class="no-padding">
                                <label>
                                    <input type="radio" name="optradio" checked="checked" value="0"/><i class="fa fa-file-o" aria-hidden="true"></i> Un impreso por cliente, profesional y fecha</label>
                            </label>
                        </div>
                    </div>
                    <br class="visible-xs" />
                    <div class="col-xs-12 no-padding">
                        <div class="radio no-margin no-padding">
                            <label class="no-padding">
                                <label>
                                    <input type="radio" name="optradio" value="1"/><i class="fa fa-files-o" aria-hidden="true"></i> Un impreso por cliente y profesional</label>
                            </label>
                        </div>
                    </div>
                    <%--<div class="text-right">
                        <a id="btnExportar" class="dt-button" tabindex="0" aria-controls="tabla2" href="#">
                            <i class="fa fa-file-pdf-o"></i>
                            <span>PDF</span>
                        </a>
                    </div>--%>
                </fieldset>
            </div>
        </div>


        <div class="row margin-bottom">
            <div id="divBotones">
                <button id="btnObtener" type="button" class="btn btn-primary">
                    <span>Obtener</span>
                </button>
                <%--<button id="btnExportar" type="button" class="btn btn-default">
                    <span>Exportar</span>
                </button>--%>                
            </div>
        </div>
        <div class="row">
            <div id="contenedor-tabla">
                <h2 class="sr-only">Tabla de resultados de partes de actividad</h2>
                <table summary="Tabla de resultados de partes de actividad" id="tablaResultados" class="table table-bordered table-no-border display">
                    <thead>
                        <tr>                            
                            <th id="hCh" class="cabecera bg-primary"></th>
                            <th id="T" class="cabecera bg-primary">Tarea</th>
                            <th id="P" class="cabecera bg-primary">Profesional</th>
                            <th id="FC" class="cabecera bg-primary">Fecha</th>
                            <th id="H" class="cabecera bg-primary">Horas</th>
                            <th id="C" class="cabecera bg-primary">Cliente</th>
                            <th id="F" class="cabecera bg-primary"><abbr title="Facturable">F</abbr></th>
                            <th id="MF" class="cabecera bg-primary"><abbr title="Modo de facturación">Modo fact.</abbr></th>
                        </tr>
                    </thead>
                    <tbody id="bodyTabla">                        
                    </tbody>
                </table>
            </div>                
        </div>
        </div>
        </div>
        </div>
    </div>
    <br />

    <!-- Modal de selección de proyecto -->
    <div class="modal fade ocultable2" id="modal-Proyecto" role="dialog" tabindex="-1" title="::SUPER::: - Selección de proyectos">
        <div class="modal-dialog modal-lg" role="dialog">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar"><span aria-hidden="true">&times;</span></button>
                    <h2 class="modal-title">:::SUPER::: - Selección de proyectos</h2>
                </div>
                <div class="modal-body">
                    <form class="form-horizontal">
                        <div class="form-group">
                            <div class="col-xs-12 col-md-6 no-padding margin-bottom-xs-sm">                                
                                <label id="lblproyecto" class="col-xs-12 col-sm-3 control-label fk-label" for="idProyecto">Proyecto</label>
                                <label for="desProyecto" class="fk-label sr-only">Descripción proyecto</label>
                                <div class="col-xs-12 col-sm-9 no-padding">
                                    <div class="col-xs-4 col-sm-3 col-md-4">
                                        <input id="txtIdProyecto" maxlength="6" name="textinput" type="text" class="form-control txtNum input-md fk_filtro_proy fk_filtro_proy_input fk_filtro_proy_proy" value="" />
                                    </div>
                                    <div id="divTxtDesProyecto" class="col-xs-8 col-sm-9 col-md-8">
                                        <input id="txtDesProyecto" name="" type="text" class="form-control input-md fk_filtro_proy fk_filtro_proy_input fk_filtro_proy_proy" value="" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-12 col-md-6">                                
                                <%--<div class="col-xs-2 col-sm-1 col-md-2 no-padding" title="Tipo de búsqueda">--%>
                                <div class="col-xs-3 col-md-3 no-padding margin-bottom-xs-sm" title="Tipo de búsqueda">
                                    <select id="selBusquedaProyectos" name="selBusquedaProyectos" class="form-control fk_filtro_proy fk_filtro_proy_sel">                                        
                                        <option value="C" selected="selected">Contiene</option>
                                        <option value="I">Inicia con</option>
                                    </select>
                                </div>
                                <%--<div class="col-xs-offset-2 col-xs-8 col-sm-offset-0 col-sm-11 col-md-10 no-padding-right">--%>
                                <div class="col-xs-12 col-md-9 no-padding">
                                    <label for="selCualidad" class="col-xs-12 col-sm-2 col-md-3 control-label fk-label no-padding-xs-sm" id="txtCua">Cualidad</label>
                                    <div class="col-xs-12 col-sm-10 col-md-9 no-padding">
                                        <select id="selCualidad" name="selCualidad" class="form-control fk_filtro_proy fk_filtro_proy_sel">
                                            <option value="" selected="selected">Todas</option>
                                            <option value="C">Contratante</option>
                                            <option value="P">Replicado con gestión</option>
                                        </select>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div id="CRAdmin" runat="server" class="col-xs-12 col-md-6 margin-bottom-xs-sm">
                                <label for="txtCR" role="link" class="underline btn-link col-xs-12 col-sm-3 col-md-3 control-label fk-label no-padding"><span id="lblCRAdmin">C.R. </span></label>
                                <div class="col-xs-12 col-sm-9 col-md-9 no-padding-xs-sm">
                                    <div class="input-group no-padding">
                                        <input id="txtCR" name="" type="text" class="form-control input-md fk_filtro_proy fk_filtro_proy_input" value="" disabled="disabled" />
                                        <span class="input-group-btn">
                                            <button class="btn btn-default btn-inline-input" type="button" id="btnEliminarCR" aria-label="Eliminar la selección de C.R. actual"">
                                                <i class="fa fa-trash-o" aria-hidden="true"></i>
                                            </button>
                                        </span>
                                    </div>
                                </div>                                
                            </div>
                            <div id="CR" runat="server" class="col-xs-12 col-md-6 margin-bottom-xs-sm">
                                <label for="txtCR" role="link" class="col-xs-12 col-sm-3 col-md-3 control-label fk-label no-padding" id="lblCR">C.R. </label>
                                <div class="col-xs-12 col-sm-9 col-md-9 no-padding-xs-sm">
                                    <select id="txtCR" name="txtCR" class="form-control fk_filtro_proy fk_filtro_proy_sel">
                                    </select>
                                </div>                                
                            </div>
                            <div class="col-xs-12 col-sm-6 col-md-3 margin-bottom-xs no-padding-sm">
                                <label for="selEstado" class="col-xs-12 col-sm-4 control-label fk-label no-padding-xs-sm" >Estado</label>
                                <div class="col-xs-12 col-sm-8 no-padding">
                                    <select id="selEstado" name="selEstado" class="form-control fk_filtro_proy">
                                        <option value="">Todos</option>
                                        <option value="A" selected="selected">Abierto</option>
                                        <option value="C">Cerrado</option>
                                        <option value="H">Histórico</option>
                                        <option value="P">Presupuestado</option>
                                    </select>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6 col-md-3">
                                <label for="selCategoria" class="col-xs-12 col-sm-6 control-label fk-label no-padding-xs-sm" id="lblCat">Categoría</label>
                                <div class="col-xs-12 col-sm-6 no-padding">
                                    <select id="selCategoria" name="selCategoria" class="form-control fk_filtro_proy fk_filtro_proy_sel">
                                        <option value="" selected="selected">Todas</option>
                                        <option value="P">Producto</option>
                                        <option value="S">Servicio</option>
                                    </select>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-12 col-md-6 margin-bottom-xs-sm">
                                <label for="txtClienteProyectos" role="link" data-origen="P" class="underline btn-link col-xs-12 col-sm-3 control-label fk-label no-padding"><span id="lblClienteProyectos">Cliente </span></label>
                                <div class="col-xs-12 col-sm-9 no-padding-xs-sm">                                    
                                    <div class="input-group no-padding">
                                        <input id="txtClienteProyectos" name="" type="text" class="form-control input-md fk_filtro_proy fk_filtro_proy_input" value="" disabled="disabled"/>                                    
                                        <span class="input-group-btn">
                                            <button class="btn btn-default btn-inline-input" type="button" id="btnEliminarClienteProyectos" aria-label="Eliminar la selección de cliente actual">
                                                <i class="fa fa-trash-o" aria-hidden="true"></i>
                                            </button>
                                        </span>
                                    </div>
                                </div>                                
                            </div>
                            <div class="col-xs-12 col-md-6 no-padding">
                                <label role="link" class="underline btn-link col-xs-12 col-sm-3 control-label fk-label no-padding-right-xs" for="idContrato"><span id="lblContrato">Contrato </span></label>
                                <label for="desContrato" class="fk-label sr-only">Descripción Contrato</label>
                                <div class="col-xs-12 col-sm-9 no-padding">
                                    <div class="col-xs-4 col-sm-3 col-md-4">
                                        <input id="txtIdContrato" name="textinput" type="text" class="form-control input-md fk_filtro_proy fk_filtro_proy_input" value="" disabled="disabled" />
                                    </div>
                                    <div id="divTxtDesContrato" class="col-xs-8 col-sm-9 col-md-8">                                        
                                        <div class="input-group no-padding">
                                            <input id="txtDesContrato" name="" type="text" class="form-control input-md fk_filtro_proy fk_filtro_proy_input" value="" disabled="disabled" />
                                            <span class="input-group-btn">
                                                <button class="btn btn-default btn-inline-input" type="button" id="btnEliminarContrato" aria-label="Eliminar la selección de contrato actual">
                                                    <i class="fa fa-trash-o" aria-hidden="true"></i>
                                                </button>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-12 col-md-6 margin-bottom-xs-sm">                                   
                                <label for="txtResp" role="link" class="underline btn-link col-xs-12 col-sm-3 control-label fk-label no-padding"><span id="lblResp">Responsable </span></label>
                                <div class="col-xs-12 col-sm-9 no-padding-xs-sm">                                    
                                    <div class="input-group no-padding">
                                        <input id="txtResponsable" name="" type="text" class="form-control input-md fk_filtro_proy fk_filtro_proy_input" value="" disabled="disabled" />
                                        <span class="input-group-btn">
                                            <button class="btn btn-default btn-inline-input" type="button" id="btnEliminarResp" aria-label="Eliminar la selección de responsable actual">
                                                <i class="fa fa-trash-o" aria-hidden="true"></i>
                                            </button>
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-12 col-md-6 no-padding-sm">                                                                
                                <label for="txtHor" role="link" class="underline btn-link col-xs-12 col-sm-3 control-label fk-label no-padding-xs-sm"><span id="lblHor">Horizontal </span></label>
                                <div class="col-xs-12 col-sm-9 no-padding-xs-sm">                                    
                                    <div class="input-group no-padding">
                                        <input id="txtHorizontal" name="" type="text" class="form-control input-md fk_filtro_proy fk_filtro_proy_input" value="" disabled="disabled"/>                                    
                                        <span class="input-group-btn">
                                            <button class="btn btn-default btn-inline-input" type="button" id="btnEliminarHor" aria-label="Eliminar la selección de horizontal actual">
                                                <i class="fa fa-trash-o" aria-hidden="true"></i>
                                            </button>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-12 col-md-6 margin-bottom-xs-sm">                                      
                                <label for="txtQn" class="fk_Cualif col-xs-12 col-sm-3 control-label fk-label no-padding"><span id="lblQn">Cualif. Qn </span></label>
                                <div class="col-xs-12 col-sm-9 no-padding-xs-sm">                                    
                                    <div class="input-group no-padding">
                                        <input id="txtQn" name="" type="text" class="form-control input-md fk_filtro_proy fk_filtro_proy_input" value="" disabled="disabled" />                                    
                                        <span class="input-group-btn">
                                            <button class="btn btn-default btn-inline-input" type="button" id="btnEliminarQn" aria-label="Eliminar la selección de Qn actual">
                                                <i class="fa fa-trash-o" aria-hidden="true"></i>
                                            </button>
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-12 col-md-6 no-padding-sm">
                                <label for="txtQ1" class="fk_Cualif col-xs-12 col-sm-3 control-label fk-label no-padding-xs-sm"><span id="lblQ1">Cualif. Q1 </span></label>
                                <div class="col-xs-12 col-sm-9 no-padding-xs-sm">                                    
                                    <div class="input-group no-padding">
                                        <input id="txtQ1" name="" type="text" class="form-control input-md fk_filtro_proy fk_filtro_proy_input" value="" disabled="disabled" />                                    
                                        <span class="input-group-btn">
                                            <button class="btn btn-default btn-inline-input" type="button" id="btnEliminarQ1" aria-label="Eliminar la selección de cualificador Q1 actual">
                                                <i class="fa fa-trash-o" aria-hidden="true"></i>
                                            </button>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-12 col-md-6 margin-bottom-xs-sm">                                                          
                                <label for="txtQ2" class="fk_Cualif col-xs-12 col-sm-3 control-label fk-label no-padding"><span id="lblQ2">Cualif. Q2 </span></label>
                                <div class="col-xs-12 col-sm-9 no-padding-xs-sm">                                    
                                    <div class="input-group no-padding">
                                        <input id="txtQ2" name="" type="text" class="form-control input-md fk_filtro_proy fk_filtro_proy_input" value="" disabled="disabled" />                                    
                                        <span class="input-group-btn">
                                            <button class="btn btn-default btn-inline-input" type="button" id="btnEliminarQ2" aria-label="Eliminar la selección de Q2 actual">
                                                <i class="fa fa-trash-o" aria-hidden="true"></i>
                                            </button>
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <div id="grpQ3" class="col-xs-12 col-md-6 no-padding-sm">                                
                                <label for="txtQ3" class="fk_Cualif col-xs-12 col-sm-3 control-label fk-label no-padding-xs-sm"><span id="lblQ3">Cualif. Q3 </span></label>
                                <div class="col-xs-12 col-sm-9 no-padding-xs-sm">                                    
                                    <div class="input-group no-padding">
                                        <input id="txtQ3" name="" type="text" class="form-control input-md fk_filtro_proy fk_filtro_proy_input" value="" disabled="disabled" />                                    
                                        <span class="input-group-btn">
                                            <button class="btn btn-default btn-inline-input" type="button" id="btnEliminarQ3" aria-label="Eliminar la selección de cualificador Q3 actual">
                                                <i class="fa fa-trash-o" aria-hidden="true"></i>
                                            </button>
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <div id="grpQ4" class="col-xs-12 col-md-6 margin-bottom-xs-sm"">                                    
                                <label for="txtQ4" class="fk_Cualif col-xs-12 col-sm-3 control-label fk-label no-padding"><span id="lblQ4">Cualif. Q4 </span></label>
                                <div class="col-xs-12 col-sm-9 no-padding-xs-sm">                                    
                                    <div class="input-group no-padding">
                                        <input id="txtQ4" name="" type="text" class="form-control input-md fk_filtro_proy fk_filtro_proy_input" value="" disabled="disabled" />                                    
                                        <span class="input-group-btn">
                                            <button class="btn btn-default btn-inline-input" type="button" id="btnEliminarQ4" aria-label="Eliminar la selección de Q4 actual">
                                                <i class="fa fa-trash-o" aria-hidden="true"></i>
                                            </button>
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-12 col-md-6 margin-bottom-xs-sm"">                                
                                 <label for="selModCon" class="col-xs-12 col-sm-3 control-label fk-label no-padding text-nowrap" id="txtModCon">Modelo Contrat.</label>
                                    <div class="col-xs-12 col-sm-9 no-padding">
                                        <select id="selModCon" name="selModCon" class="form-control fk_filtro_proy fk_filtro_proy_sel">
                                        </select>
                                    </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-12 col-md-3 col-md-offset-6" title="Obtiene la información auntomáticamente al cambiar el valor de algún criterio de selección">
                                    <i class="fa fa-cog fa-1-5x" aria-hidden="true">&nbsp;</i>Búsqueda automática <input id="chkAuto" title="Obtiene la información auntomáticamente al cambiar el valor de algún criterio de selección" type="checkbox" />
                                </div>
                            <div id="divObtenerProyectos" class="col-xs-12 col-md-3 margin-bottom">
                                <button id="btnObtenerProyectos" type="button" class="btn btn-primary">
                                    <span>Obtener</span>
                                </button>                               
                            </div>
                        </div>

                        
                    </form>
                    <div id="divProyectos">
                        <div class="row">
                            <div id="proyectos-lisProyectos" class="text-left dual-list list-left col-xs-12 col-sm-5">
                                <h4>Proyectos</h4>
                                <div class="marco no-padding">
                                    <%--<div class="row">
                                        <div class="btn-group col-xs-2">
                                            <a class="btn btn-default selector" data-toggle="popover" title="" data-content="Marcar/desmarcar todo" data-placement="top">
                                                <i class="glyphicon glyphicon-unchecked"></i>
                                            </a>
                                        </div>

                                    </div>--%>
                                    <table id="tablaProyectos" class="table table-bordered table-no-border display nowrap" cellspacing="0" width="100%">
                                        <thead>
                                            <tr>
                                                <th id="IcoProy" class="cabecera bg-primary text-left"></th>
                                                <th id="NumProy" class="cabecera bg-primary text-left">Nº</th>
                                                <th id="DesProy" class="cabecera bg-primary text-left">Proyecto</th>
                                            </tr>
                                        </thead>
                                        <tbody id="bodyTablaProyectos">
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <div id="proyectos-Acciones" class="list-arrows btnacciones col-xs-12 col-sm-2 text-center MT100">
                                <div class="row col-xs-3 col-xs-offset-4 col-sm-offset-0 col-sm-12">
                                    <button class="btn btn-default btn-sm move-right">
                                        <span class="glyphicon glyphicon-chevron-right hidden-xs"></span>
                                        <span class="glyphicon glyphicon-chevron-down visible-xs"></span>
                                    </button>
                                </div>
                            </div>
                            <div id="proyectos-lisSeleccionados" class="dual-list list-right col-xs-12 col-sm-5">
                                <h4>Proyectos seleccionados</h4>
                                <div class="marco no-padding">
                                    <%--<div class="row">
                                        <div class="btn-group col-xs-2">
                                            <a class="btn btn-default selector" data-toggle="popover" title="" data-content="Marcar/desmarcar todo" data-placement="top">
                                                <i class="glyphicon glyphicon-unchecked"></i>
                                            </a>
                                        </div>
                                    </div>--%>
                                    <table id="tablaProyectosSel" class="table table-bordered table-no-border display nowrap" width="100%">
                                        <thead>
                                            <tr>
                                                <%--<th colspan="3" id="DesProySel" class="cabecera bg-primary text-left">Proyectos seleccionados</th>--%>
                                                <th id="IcoProy2" class="cabecera bg-primary text-left"></th>
                                                <th id="NumProy2" class="cabecera bg-primary text-left">Nº</th>
                                                <th id="DesProy2" class="cabecera bg-primary text-left">Proyecto</th>
                                            </tr>
                                        </thead>
                                        <tbody id="bodyTablaProyectosSel">
                                        </tbody>
                                    </table>
                                    <!--Eliminar proyectos-->
                                    <div class="btnacciones pull-right">
                                        <button id="proyectos-btnEliminarSeleccionados" class="move-left btn-link underline">Eliminar</button>
                                    </div>
                                </div>
                            </div>                           
                        </div>
                    </div>
                    <div id="leyendas" class="leyenda">
                        <span class="text-nowrap"><span class="fa fa-ticket fa-rotate-90"></span> Producto </span>
                        <span class="text-nowrap"><span class="fa fa-user"></span> Servicio </span>
                        <span class="text-nowrap"><span class="fa fa-copyright fk_verde"></span> Contratante </span>
                        <span class="text-nowrap"><span class="fa fa-registered"></span> Replicado </span>
                        <span class="text-nowrap"><span class="fa fa-registered fk_verde"></span> Replicado con gestión propia </span>
                        <span class="text-nowrap"><span class="fa fa-diamond fa-diamond-verde"></span> Abierto </span>
                        <span class="text-nowrap"><span class="fa fa-diamond fa-diamond-rojo"></span> Cerrado </span>
                        <span class="text-nowrap"><span class="fa fa-diamond fa-diamond-gris"></span> Histórico </span>
                        <span class="text-nowrap"><span class="fa fa-diamond fa-diamond-amarillo"></span> Presupuestado </span>
                    </div>
                </div>
                <div class="modal-footer">
                    <b><button id="btnAceptarProyecto" class="btn btn-primary" data-dismiss="modal">Aceptar</button></b>
                    <b><button id="btnCancelarProyecto" class="btn btn-default" data-dismiss="modal">Cancelar</button></b>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <!-- Modal de selección de contrato en la selección de proyecto -->
    <div class="modal fade ocultable3" id="modal-Contrato" role="dialog" tabindex="-1" title=":::SUPER::: - Selección de contrato">
        <div class="modal-dialog modal-lg" role="dialog">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar"><span aria-hidden="true">&times;</span></button>
                    <h1 class="modal-title">:::SUPER::: - Selección de contrato</h1>
                </div>
                <div class="modal-body">     
                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-xs-12 col-md-8 margin-bottom no-padding">
                                <label class="col-xs-12 col-sm-3 fk-label" for="idContratoM">Contrato</label>
                                <div class="col-xs-12 col-sm-9 no-padding">
                                    <div class="col-xs-4 col-sm-3 col-md-4">
                                        <input id="idContratoM" maxlength="6" name="textinput" type="text" class="form-control txtNum input-md" value="" />
                                    </div>
                                    <div class="col-xs-8 col-sm-9 col-md-8">
                                        <input id="desContratoM" name="" aria-label="Descripción del contrato" type="text" class="form-control input-md" value="" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-4 col-sm-3 col-md-2 margin-bottom" title="Tipo de búsqueda">
                                    <select id="selBusquedaContratos" name="selBusquedaContratos" class="form-control">
                                        <option value="I">Inicia con</option>
                                        <option value="C" selected="selected">Contiene</option>
                                    </select>
                                </div>
                            <div id="divChkContratos" class="col-xs-6 col-md-2 checkbox margin-bottom">
                                <label><input id="chkMostrarTodosContratos" type="checkbox" checked="checked" />Mostrar todos</label>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-12 col-md-8 margin-bottom no-padding">                                   
                                <label for="txtClienteContratos" role="link" data-origen="C" class="underline btn-link col-xs-12 col-sm-3 fk-label"><span id="lblClienteContratos">Cliente </span></label>
                                <div class="col-xs-12 col-sm-9">                                    
                                    <div class="input-group no-padding">
                                        <input id="txtClienteContratos" name="" type="text" class="form-control input-md" value="" disabled="disabled"/>
                                        <span class="input-group-btn">
                                            <button class="btn btn-default btn-inline-input" type="button" id="btnEliminarClienteContratos" aria-label="Eliminar la selección de cliente actual">
                                                <i class="fa fa-trash-o" aria-hidden="true"></i>
                                            </button>
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-12 col-md-4 margin-bottom text-centert-xs-sm">                                
                                <b>
                                    <button id="btnObtenerContratoProyectos" class="btn btn-primary">Obtener</button></b>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-xs-12">
                                <table id="tablaContratos" class="table table-bordered table-no-border display" width="100%">
                                    <thead>
                                        <tr>
                                            <th colspan="3"></th>
                                            <th colspan="2" class="bg-primary text-center">Producto</th>
                                            <th colspan="2" class="bg-primary text-center">Servicio</th>
                                            <th colspan="2"></th>
                                        </tr>
                                        <tr>
                                            <th id="O" class="bg-primary">Oportunidad / Extensión</th>
                                            <th id="CT" class="bg-primary text-center">Contrato</th>
                                            <th id="IE" class="bg-primary text-center">Extension</th>
                                            <th id="CP" class="bg-primary text-center">Contratado</th>
                                            <th id="PP" class="bg-primary text-center">Pendiente</th>
                                            <th id="CS" class="bg-primary text-center">Contratado</th>
                                            <th id="PS" class="bg-primary text-center">Pendiente</th>
                                            <th id="CL" class="bg-primary text-center">Cliente</th>
                                        </tr>
                                    </thead>
                                    <tbody id="bodyTablaContratos">
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <b><button id="btnAceptarContrato" class="btn btn-primary" data-dismiss="modal">Aceptar</button></b>
                    <b><button id="btnCancelarContrato" class="btn btn-default" data-dismiss="modal">Cancelar</button></b>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

     <!-- Modal de selección de CR en la selección de proyecto -->
    <div class="modal fade" id="modal-CR" role="dialog" tabindex="-1" title=":::SUPER::: - Selección de C.R.">
        <div class="modal-dialog modal-md" role="dialog">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar"><span aria-hidden="true">&times;</span></button>
                    <h1 class="modal-title">:::SUPER::: - Selección de C.R.</h1>
                </div>
                <div class="modal-body">
                    <form class="form-horizontal">
                        <div class="form-group">
                            <div class="col-xs-12">
                            <table id="tablaCR" class="table table-bordered table-no-border display" cellspacing="0" width="100%">
                                <thead>
                                    <tr>
                                        <th id="IdNodo" class="cabecera bg-primary text-left">Nº</th>
                                        <th id="DenNodo" class="cabecera bg-primary text-left">Denominación</th>
                                    </tr>
                                </thead>
                                <tbody id="bodyTablaCR">
                                </tbody>
                            </table>
                            </div>
                        </div>
                    </form>
                </div>              
                <div class="modal-footer">
                    <b><button id="btnAceptarCR" class="btn btn-primary" data-dismiss="modal">Aceptar</button></b>
                    <b><button id="btnCancelarCR" class="btn btn-default" data-dismiss="modal">Cancelar</button></b>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <div class="fk_ayudaCliente"></div>
    <div class="fk_ayudaClienteMulti"></div>
    <div class="buscadorResp" id="buscProf"></div>
    <div class="fk_ayudaCatalogoBasico"></div>
    </body>

</html>
    <script src="<% =Session["strServer"].ToString() %>scripts/ssrs.js?v=23/04/2018"></script>
    <script src="<% =Session["strServer"].ToString() %>Javascript/funciones.js"></script>

<script src="<% =Session["strServer"].ToString() %>Capa_Presentacion/IAP30/scripts/JavaScript.js" type="text/javascript"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/moment.locale.min.js"></script>
<script type="text/javascript" src="<% =Session["strServer"].ToString() %>plugins/daterangepicker/daterangepicker.js?20170503_01"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/string.js"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/accounting.min.js" type="text/javascript" ></script>
<%--<script src="js/JavaScript.js" type="text/javascript"></script>--%>
<script src="<% =Session["strServer"].ToString() %>plugins/datatables/jquery.dataTables.min.js"></script>
<script src="<% =Session["strServer"].ToString() %>plugins/datatables/Buttons/dataTables.buttons.min.js"></script>
<script type="text/javascript" src="<% =Session["strServer"].ToString() %>plugins/datatables/Checkboxes/dataTables.checkboxes.min.js"></script>
<script src="<% =Session["strServer"].ToString() %>plugins/IB/buscaclientemulti.js"></script>
<script src="<% =Session["strServer"].ToString() %>plugins/IB/buscacliente.js"></script>
<script src="<% =Session["strServer"].ToString() %>plugins/BuscadorPersonas/BuscadorPersonas.js"></script>
<script src="<% =Session["strServer"].ToString() %>plugins/IB/buscacatalogobasico.js"></script>

<script src="js/view.js?20170517_01" type="text/javascript"></script>
<script src="js/app.js?20170313_01" type="text/javascript"></script>



