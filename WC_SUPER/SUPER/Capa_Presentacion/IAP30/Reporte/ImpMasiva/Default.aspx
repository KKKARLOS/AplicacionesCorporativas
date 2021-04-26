<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Reporte_ImpMasiva_Default" %>

<%@ Register Src="~/Capa_Presentacion/bsUserControls/Menu/Menu.ascx" TagPrefix="cb1" TagName="Menu" %>
<%@ Register Src="~/Capa_Presentacion/bsUserControls/Head.ascx" TagPrefix="cb1" TagName="Head" %>


<!DOCTYPE html>

<html xml:lang="es" lang="es" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <cb1:Head runat="server" id="Head" />
    <title>::: IAP 3.0 - Imputación masiva :::</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
</head>

<body data-codigopantalla="100">
    <script>
        var opciones = { delay: 1, hide:1 }
        IB.procesando.opciones(opciones);
        IB.procesando.mostrar();
    </script>
    <cb1:Menu runat="server" id="Menu" />
    <link rel="stylesheet" href="<% =Session["strServer"].ToString() %>css/jquery-ui.min.css"/>

    <div class="container-fluid ocultable">
         <h1 class="sr-only">::: SUPER ::: - Imputación masiva</h1>
         <!--<div class="row">
            <div class="col-xs-12">
                <div id="divGuia" class="pull-right">
                    <button id="btnGuia" type="button" class="btn btn-default">
                        <span class="fa fa-info-circle" style="vertical-align: middle;"></span>
                        <span>Guía</span>
                    </button>
                </div>
            </div>
        </div>-->
         
        <div class="ibox-content blockquote blockquote-info">    
            <form class="form-horizontal collapsed in"  role="form" runat="server" accept-charset="iso-8859-15">                              
                <%--<div class="ibox-title">
                    <span class="text-primary">Tipo imputación</span>
                    <h2 class="sr-only">Tipo imputación</h2>
                    <div class="ibox-tools">    
                         <a class="collapse-link">                    
                            <i data-toggle="collapse" data-target="#tipoImputacion" class="chevron_toggleable fa fa-chevron-up pull-right"></i>
                        </a>                       
                    </div>
                </div>--%>
                <a class="collapse-link">
                    <div class="ibox-title ibox-title_toggleable" role="link" aria-expanded="true" data-toggle="collapse" data-target="#tipoImputacion">
                        <span class="text-primary">Tipo imputación</span>
                        <h2 class="sr-only">Tipo imputación</h2>
                        <div class="ibox-tools">
                            <i class="fa fa-chevron-up pull-right"></i>
                        </div>
                    </div>
                </a>
                <div id="tipoImputacion" class="collapsed in sinPadding">
                    <div class="ibox-content ">

                        <div class="form-group">
                            <div class="col-xs-12" id="selImputacion">
                                <div class="radio">
                                    <label class="lblTipoImpu control-label">
                                        <input id="radio1" type="radio" name="optradio" value="1" checked="checked" />Imputar horas/día estándar a una tarea, desde el día siguiente al último día reportado hasta una fecha determinada.</label>
                                </div>
                                <div class="radio">
                                    <label class="lblTipoImpu control-label">
                                        <input id="radio2" type="radio" name="optradio" value="2" />Imputar horas/día estándar a una tarea en un intervalo de fechas.</label>
                                </div>
                                <div class="radio">
                                    <label class="lblTipoImpu control-label">
                                        <input id="radio3" type="radio" name="optradio" value="3" />Imputar x horas a una tarea en un intervalo de fechas.</label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <%--<div class="ibox-title">
                    <span class="text-primary">Datos generales</span>
                    <h2 class="sr-only">Datos generales</h2>
                    <div class="ibox-tools">
                        <a class="collapse-link">                    
                            <i data-toggle="collapse" data-target="#general" class="chevron_toggleable fa fa-chevron-up pull-right"></i>
                        </a>
                    </div>
                </div>--%>
                <a class="collapse-link">
                    <div class="ibox-title ibox-title_toggleable" role="link" aria-expanded="true" data-toggle="collapse" data-target="#general">
                        <span class="text-primary">Datos generales</span>
                        <h2 class="sr-only">Datos generales</h2>
                        <div class="ibox-tools">
                            <i class="fa fa-chevron-up pull-right"></i>
                        </div>
                    </div>
                </a>
                <div id="general" class="collapsed in sinPadding">
                    <div class="ibox-content">
                        <div class="form-group">
                            <div class="col-xs-12 col-md-6">
                                <div class="form-group">
                                    <label for="txtProy" class="col-xs-12 col-md-3 control-label fk-label">Proyecto</label>
                                    <div class="col-xs-12 col-md-9">
                                        <input id="txtProy" name="" type="text" class="form-control input-md" value="" disabled="disabled" aria-readonly="true" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="txtProyTec" class="col-xs-12 col-md-3 control-label fk-label">Proyecto técnico</label>
                                    <div class="col-xs-12 col-md-9">
                                        <input id="txtProyTec" name="" type="text" class="form-control input-md" value="" disabled="disabled" aria-readonly="true" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="txtFase" class="col-xs-12 col-md-3 control-label fk-label">Fase</label>
                                    <div class="col-xs-12 col-md-9">
                                        <input id="txtFase" name="" type="text" class="form-control input-md" value="" disabled="disabled" aria-readonly="true" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="txtAct" class="col-xs-12 col-md-3 control-label fk-label">Actividad</label>
                                    <div class="col-xs-12 col-md-9">
                                        <input id="txtAct" name="" type="text" class="form-control input-md" value="" disabled="disabled" aria-readonly="true" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-xs-12 col-md-3">
                                        <label id="lblTarea" role="link" title="Selección de tarea" class=" control-label fk-label txtLinkU" for="datosTarea">Nº Tarea</label><span class="text-danger"> *</span>
                                    </div>
                                    <label for="desTarea" class="fk-label sr-only">Descripción Tarea</label>
                                    <div id="datosTarea" class="col-xs-12 col-md-9">

                                        <input id="idTarea" style="width: 90px;" aria-required="true" aria-label="Identificador de tarea" name="textinput" type="text" class="form-control input-md text-right" value="" />
                                        <input id="desTarea" name="" type="text" class="form-control input-md" value="" disabled="disabled" aria-readonly="true" />

                                    </div>
                                </div>
                                <br class="xs-visible" />
                            </div>
                            <div class="col-xs-12 col-md-4">

                                <div class="form-group">

                                    <label for="txtUltRep" class="col-xs-12 col-md-6 control-label fk-label .">Último día reportado</label>
                                    <div class="col-xs-7 col-md-6">
                                        <input id="txtUltRep" name="" type="text" class="form-control input-md" value="" disabled="disabled" aria-readonly="true" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="SelModo" class="col-xs-12 col-md-6 control-label fk-label">Modo</label>
                                    <div class="col-xs-7 col-md-6">
                                        <select id="SelModo" name="slMotivo" class="form-control" disabled="disabled">
                                            <option value="1">Sustitución</option>
                                            <option value="2">Acumulación</option>
                                        </select>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label for="txtFIni" class="col-xs-12 col-md-6 control-label fk-label">Fecha inicio imputación<span id="txtFIniDanger" aria-hidden="true" class="text-danger hidden"> *</span></label>

                                    <div class="col-xs-7 col-md-6">
                                        <input title="Formato fecha: dd/mm/aaaa" aria-required="false" placeholder="dd/mm/aaaa" id="txtFIni" name="" type="text" class="form-control input-md txtFecha calendar-off" value="" disabled="disabled" aria-readonly="true" />
                                        
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="txtFFin" class="col-xs-12 col-md-6 control-label fk-label">Fecha fin imputación<span class="text-danger"> *</span></label>
                                    <div class="col-xs-7 col-md-6">
                                        <input title="Formato fecha: dd/mm/aaaa" aria-required="true" id="txtFFin" name="" type="text" class="form-control input-md txtFecha calendar-off" value="" placeholder="dd/mm/aaaa" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-12 col-md-2">
                                <div class="checkbox">
                                    <label for="chkFestivos" class="control-label fk-label">
                                        <input id="chkFestivos" type="checkbox" disabled />
                                        Permite imputar en festivos 
                                    </label>
                                </div>
                                <br />
                                <div class="form-group">
                                    <div class="col-xs-9 col-sm-12 col-md-12">
                                        <fieldset class="fieldset">
                                            <h3 class="sr-only">Facturabilidad</h3>
                                            <legend class="fieldset">
                                                <div class="checkbox">
                                                    <label for="chkFacturable" class="control-label fk-label">
                                                        <input id="chkFacturable" type="checkbox" disabled />
                                                        Facturable
                                                    </label>
                                                </div>
                                            </legend>
                                            <div class="disabled">
                                                <input disabled="disabled" aria-readonly="true" id="txtModoFact" name="" type="text" class="form-control input-md" value="" />
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                    
                <%--<div class="ibox-title">                             
                    <span class="text-primary">Imputación</span>  
                    <h2 class="sr-only">Imputación</h2>
                    <div class="ibox-tools">    
                         <a class="collapse-link">                    
                            <i data-toggle="collapse" data-target="#imputacion" class="chevron_toggleable fa fa-chevron-up pull-right"></i>
                        </a>                       
                    </div>                        
                    <!--Limpiamos los floats-->
                    <div class="clearfix"></div>
                </div>--%>
                <a class="collapse-link">
                    <div class="ibox-title ibox-title_toggleable" role="link" aria-expanded="true" data-toggle="collapse" data-target="#imputacion">
                        <span class="text-primary">Imputación</span>
                        <h2 class="sr-only">Imputación</h2>
                        <div class="ibox-tools">
                            <i class="fa fa-chevron-up pull-right"></i>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                </a>
                <div id="imputacion" class="with-border ui-icon-folder-collapsed in sinPadding">
                    <div class="ibox-content">
                        <div class="form-group">
                            <div class="col-xs-12 col-md-4">
                                <div class="form-group">
                                    <label for="txtHoras" class="col-xs-12 col-md-4 control-label fk-label">Horas<span id="txtHorasDanger" class="text-danger hidden"> *</span></label>
                                    <div class="col-xs-5 col-sm-3 col-md-4">
                                        <input title="Formato hora: hh,mm" aria-required="true" placeholder="0,00" id="txtHoras" type="text" name="" class="form-control input-md text-right" value="" disabled="disabled" aria-readonly="true" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-12 col-md-8">
                                <div class="form-group">
                                    <label for="txtComent" class="col-xs-12 col-md-2 control-label fk-label text-left">Comentario</label>
                                    <div class="col-xs-12 col-md-10">
                                        <textarea class="form-control txtArea" id="txtComent" name="" rows="3" maxlength="7500"></textarea>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <%--<div class="ibox-title">                             
                    <span class="text-primary">Datos IAP referentes al técnico</span>   
                    <h2 class="sr-only">Datos IAP referentes al técnico</h2>   
                    <div class="ibox-tools">    
                         <a class="collapse-link">                    
                            <i data-toggle="collapse" data-target="#datosIAP" class="chevron_toggleable fa fa-chevron-up pull-right"></i>
                        </a>                       
                    </div>                   
                    <!--Limpiamos los floats-->
                    <div class="clearfix"></div>
                </div>--%>
                <a class="collapse-link">
                    <div class="ibox-title ibox-title_toggleable" role="link" aria-expanded="true" data-toggle="collapse" data-target="#datosIAP">
                        <span class="text-primary">Datos IAP referentes al técnico</span>
                        <h2 class="sr-only">Datos IAP referentes al técnico</h2>
                        <div class="ibox-tools">
                            <i class="fa fa-chevron-up pull-right"></i>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                </a>
                <div id="datosIAP" class="collapsed in sinPadding">
                    <div class="ibox-content">
                        <div class="form-group">
                            <div class="col-xs-12 col-sm-4 col-md-4">
                                <div class="form-group">
                                    <label for="PConsumo" class="col-xs-12 col-sm-5 col-md-6 control-label fk-label">Primer consumo</label>
                                    <div class="col-xs-7 col-sm-7 col-md-6">
                                        <input disabled="disabled" aria-readonly="true" id="PConsumo" name="" type="text" class="form-control input-md" value="" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-4 col-md-3">
                                <div class="form-group">
                                    <label for="PConsumido" class="col-xs-12 col-sm-7 col-md-7 control-label fk-label">Consumido(horas)</label>
                                    <div class="col-xs-5 col-sm-5 col-md-5">
                                        <input disabled="disabled" aria-readonly="true" id="PConsumido" name="" type="text" class="form-control input-md text-right" value="" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-4 col-md-4">
                                <div class="form-group">
                                    <label for="pEstimado" class="col-xs-12 col-sm-7 col-md-7 control-label fk-label">
                                        <abbr title="Pendiente de estimar (horas)">Pte. estimado (horas)</abbr></label>
                                    <div class="col-xs-5 col-sm-5 col-md-4">
                                        <input disabled="disabled" aria-readonly="true" id="pEstimado" name="" type="text" class="form-control input-md text-right" value="" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="col-xs-12 col-sm-4 col-md-4">
                                <div class="form-group">
                                    <label for="UConsumo" class="col-xs-12 col-sm-5 col-md-6 control-label fk-label">Último consumo</label>
                                    <div class="col-xs-7 col-sm-7 col-md-6">
                                        <input disabled="disabled" aria-readonly="true" id="UConsumo" name="" type="text" class="form-control input-md" value="" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-4 col-md-3">
                                <div class="form-group">
                                    <label for="UConsumido" class="col-xs-12 col-sm-7 col-md-7 control-label fk-label">Consumido(jornadas)</label>
                                    <div class="col-xs-5 col-sm-5 col-md-5">
                                        <input disabled="disabled" aria-readonly="true" id="UConsumido" name="" type="text" class="form-control input-md text-right" value="" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-4 col-md-4">
                                <div class="form-group">
                                    <label for="ATeorico" class="col-xs-12 col-sm-7 col-md-7 control-label fk-label">Avance teórico</label>
                                    <div class="col-xs-5 col-sm-5 col-md-4 input-group">
                                        <input disabled="disabled" aria-readonly="true" id="ATeorico" name="" type="text" class="form-control input-md text-right" value="" />
                                        <span class="input-group-addon">%</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                    <%--<div class="ibox-title">                             
                    <span class="text-primary">Indicaciones</span>    
                     <h2 class="sr-only">Indicaciones</h2>                       
                    <div class="ibox-tools">    
                         <a class="collapse-link">                    
                            <i data-toggle="collapse" data-target="#indicaciones" class="chevron_toggleable fa fa-chevron-up pull-right"></i>
                        </a>                       
                    </div>
                    <div class="clearfix"></div>
                </div>--%>
                <a class="collapse-link">
                    <div class="ibox-title ibox-title_toggleable" role="link" aria-expanded="true" data-toggle="collapse" data-target="#indicaciones">
                        <span class="text-primary">Indicaciones</span>
                        <h2 class="sr-only">Indicaciones</h2>
                        <div class="ibox-tools">
                            <i class="fa fa-chevron-up pull-right"></i>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                </a>
                <div id="indicaciones" class="collapsed in sinPadding">
                    <div class="ibox-content">
                        <div class="form-group">
                            <div class="col-xs-12 col-sm-6">
                                <fieldset class="fieldset" id="irFieldset">
                                    <h3 class="sr-only">Indicaciones del responsable</h3>
                                    <legend class="fieldset"><span>Del responsable</span></legend>
                                    <div class="form-group">
                                        <label for="txtTotPrev" class="col-xs-12 col-md-5 control-label fk-label">Total previsto (horas)</label>
                                        <div class="col-xs-6 col-md-3">
                                            <input id="txtTotPrev" name="" type="text" class="form-control input-md text-right" value="" disabled="disabled" aria-readonly="true" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="txtFFinPrev" class="col-xs-12 col-md-5 control-label fk-label">Fecha fin prevista</label>
                                        <div class="col-xs-8 col-md-4">
                                            <input id="txtFFinPrev" name="" type="text" class="form-control input-md" value="" disabled="disabled" aria-readonly="true" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="txtParti" class="col-xs-12 col-md-5 control-label fk-label">Particulares</label>
                                        <div class="col-xs-12 col-md-7">
                                            <textarea class="form-control txtArea" id="txtParti" name="" rows="3" disabled="disabled" aria-readonly="true"></textarea>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="txtColec" class="col-xs-12 col-md-5 control-label fk-label">Colectivas</label>
                                        <div class="col-xs-12 col-md-7">
                                            <textarea class="form-control txtArea" id="txtColec" name="" rows="3" disabled="disabled" aria-readonly="true"></textarea>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                            <div class="col-xs-12 col-sm-6">
                                <fieldset class="fieldset" id="ctFieldset">
                                    <h3 class="sr-only">Comentarios del técnico</h3>
                                    <legend class="fieldset"><span>Del técnico</span></legend>
                                    <div class="form-group">
                                        <label for="txtTotEst" class="col-xs-12 col-md-5 control-label fk-label">Total estimado (horas)</label>
                                        <div class="col-xs-6 col-md-3">
                                            <input id="txtTotEst" name="" type="text" class="form-control input-md text-right" value="" title="Formato hora: hh,mm" placeholder="0,00" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="txtFEst" class="col-xs-12  col-md-5 control-label fk-label">Fecha de finalización estimada</label>
                                        <div class="col-xs-8 col-md-4">
                                            <input id="txtFEst" name="" type="text" class="form-control input-md txtFecha calendar-off" value="" title="Formato fecha: dd/mm/aaaa" placeholder="dd/mm/aaaa" />
                                        </div>
                                    </div>
                                    <div class="checkbox">

                                        <label for="chkFinalizado" class="col-xs-12 col-md-5 control-label fk-label">
                                            <input type="checkbox" id="chkFinalizado" />Trabajo finalizado
                                        
                                        </label>

                                    </div>
                                    <br />
                                    <div class="form-group">
                                        <label for="txtObsv" class="col-xs-12 col-md-5 control-label fk-label">Observaciones</label>
                                        <div class="col-xs-12 col-md-7">
                                            <textarea class="form-control txtArea" id="txtObsv" name="" rows="3"></textarea>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div id="pieTarea" class="pull-right" style="padding-right: 20px;">
                        <div id="divGrabar">
                            <button id="btnGrabar" type="button" role="button" class="btn btn-primary">
                                <span>Grabar</span>
                            </button>
                        </div>
                    </div>
                </div>                    
           </form>
        </div>
    </div>
    <br />
    <div class="fk_ayudaTarea"></div>
    <input type="hidden" name="RegJorNoCompleta" id="hdnRegJorNoCompleta" value="1" runat="server" />
    <input type="hidden" name="ImputarFestivos" id="hdnImputarFestivos" value="1" runat="server" />
    <input type="hidden" name="ImputarFestivos" id="txtFIniH" value="1" runat="server" />
    <input type="hidden" name="hdnfechaInicioImpPermitida" id="hdnfechaInicioImpPermitida" value="1" runat="server" />
    <input type="hidden" name="hdnfechaFinImpPermitida" id="hdnfechaFinImpPermitida" value="1" runat="server" />
</body>
</html>

<script src="js/view.js?20170605_01"></script>
<script src="js/app.js?20170726_01"></script>
<script src="<% =Session["strServer"].ToString() %>Capa_Presentacion/IAP30/scripts/JavaScript.js" type="text/javascript"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/jquery-ui.min.js"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/moment.locale.min.js"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/string.js"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/accounting.min.js"></script>
<script src="<% =Session["strServer"].ToString() %>plugins/IB/buscatarea.js?20170517_01"></script>


