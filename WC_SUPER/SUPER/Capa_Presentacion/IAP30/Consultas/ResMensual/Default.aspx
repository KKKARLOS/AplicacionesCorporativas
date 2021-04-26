<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Consultas_ResMensual_Default" %>

<%@ Register Src="~/Capa_Presentacion/bsUserControls/Menu/Menu.ascx" TagPrefix="cb1" TagName="Menu" %>
<%@ Register Src="~/Capa_Presentacion/bsUserControls/Head.ascx" TagPrefix="cb1" TagName="Head" %>


<!DOCTYPE html>

<html lang="es" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <cb1:Head runat="server" id="Head" />
    <title>::: SUPER ::: - Consulta de Resumen mensual</title>
    <link rel="Stylesheet" href="<% =Session["strServer"].ToString() %>css/jquery-ui.min.css"/>
    <link rel="Stylesheet" href="<% =Session["strServer"].ToString() %>plugins/datatables/jquery.dataTables.min.css"/>
    <link rel="Stylesheet" href="<% =Session["strServer"].ToString() %>plugins/datatables/Buttons/buttons.dataTables.min.css"/>
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
    <form id="frmDatos" runat="server">
     <input type="hidden" id="FORMATO" name="ctl00$CPHC$FORMATO" value="PDF" />
     <input type="hidden" id="hdnIDS" name="ctl00$CPHC$hdnIDS"  value="" />
     <input type="hidden" id="hdnAnoMesDesde" name="ctl00$CPHC$hdnAnoMesDesde" value="" />
     <input type="hidden" id="hdnAnoMesHasta" name="ctl00$CPHC$hdnAnoMesHasta" value="" />

<div class="container-fluid ocultable">
    <div class="ibox-content blockquote blockquote-info">
        <div class="panel-group">
            <div class="ibox-title">
                    <h1 class="sr-only">::: SUPER ::: - Consulta de Resumen mensual</h1>
                    <div class="row">
                        <div class="col-xs-12">
                            <fieldset class="fieldset">
                                <h2 class="sr-only">Periodo</h2>
                                <legend class="fieldset"><span tabindex="0" role="link" id="btnPeriodo">Periodo</span></legend>
                                <form class="form-horizontal">
                                    <div class="col-xs-12 col-md-5 col-lg-4 criterios">
                                        <div  class="form-group">
                                            <select id="selOpc" aria-label="Seleccionar periodo">
                                                <option value="0">Seleccionar periodo...</option>
                                                <option value="1">Año completo a seleccionar</option>
                                                <option value="2">Mes completo a seleccionar</option>
                                                <option value="3">Desde enero a mes a seleccionar</option>
<%--                                            <option value="4">Desde principio a mes a seleccionar</option>
                                                <option value="5">Desde principio a final</option>--%>
                                            </select>
                                            <%--<div id="divFlechas" style="display: inline; margin-left: 5px;" class="no-padding datepicker-container">
                                                <i id="flechaIzda" style="margin-top:8px" tabindex="0" role="button" aria-label="Retroceder elemento seleccionado en el periodo" class="fa fa-arrow-circle-left fa-1_6x"></i>
                                                <i id="flechaDcha" tabindex="0" role="button" aria-label="Avanzar elemento seleccionado en el periodo" class="fa fa-arrow-circle-right fa-1_6x"></i>
                                            </div>--%>
                                        </div>
                                    </div>
                                    <div id="grpTxtInicio" class="form-group col-xs-12 col-sm-5 col-md-3 no-padding criterios">
                                        <label id="lblInicio" for="txtInicio" class="col-xs-3 col-md-3 control-label fk-label">Inicio</label>
                                        <div class="col-xs-8 col-md-9 col-lg-8">
                                            <input id="txtInicio" name="txtInicio" type="text" runat="server" class="form-control input-md date-picker calendar-off" placeholder="mm/yyyy" value="" />
                                        </div>
                                    </div>
                                    <div id="grpTxtFin" class="form-group col-xs-12 col-sm-5 col-md-3 no-padding criterios">
                                        <label id="lblFin" for="txtFin" class="col-xs-3 col-md-3 control-label fk-label">Fin</label>
                                        <div class="col-xs-8 col-md-9 col-lg-8">
                                            <input id="txtFin" name="txtFin" type="text" runat="server" class="form-control input-md date-picker calendar-off" value="" />
                                        </div>
                                    </div>
                                    <div id="grpAno" class="form-group col-xs-12 col-sm-10 col-md-6 no-padding criterios">
                                        <label id="lblAno" for="txtFin" class="col-xs-3 col-sm-2 col-md-3 col-lg-2 control-label fk-label">Año</label>
                                        <div class="col-xs-5 col-sm-2 col-md-3 col-lg-2">
                                            <input id="txtAno" name="txtAno" type="text" runat="server" class="form-control input-md date-picker calendar-off" value="" />
                                        </div>
                                    </div>
                                    <div id="grpAnoMes" class="form-group col-xs-12 col-sm-10 col-md-6 no-padding criterios">
                                        <label id="lblAnoMes" for="txtFin" class="col-xs-3 col-sm-2 col-md-3 col-lg-2 control-label fk-label"">Mes</label>
                                        <div class="col-xs-8 col-sm-4 col-md-5 col-lg-4">
                                            <input id="txtAnoMes" name="txtAnoMes" type="text" runat="server" class="form-control input-md date-picker calendar-off" value="" />
                                        </div>
                                    </div>
                                    <div id="grpMes" class="form-group col-xs-12 col-sm-10 col-md-6 no-padding criterios">
                                        <label id="lblMes" for="txtFin" class="col-xs-3 col-sm-2 col-md-3 col-lg-2 control-label fk-label"">Mes</label>
                                        <div class="col-xs-8 col-sm-3 col-md-4 col-lg-3">
                                            <input id="txtMes" name="txtMes" type="text" runat="server" class="form-control input-md date-picker calendar-off" value="" />
                                        </div>
                                    </div>
                                    <div id="Imprimir" title="Exportar a PDF" class="form-group col-xs-12 col-sm-1 col-lg-2">
                                        <a class="dt-button" tabindex="0" aria-controls="tabla2" href="#">
                                            <i class="fa fa-file-pdf-o"></i>
                                            <span>PDF</span>
                                        </a>
                                    </div>
                                    <%--<div id="Imprimir" class="form-group col-xs-12 col-sm-1 no-padding text-center">
                                        <i class="fa fa-file-pdf-o" aria-hidden="true"></i>
                                        <span tabindex="0" role="link" class="txtLinkU" id="btnImpu">PDF</span>
                                    </div>--%>

                                </form>
                            </fieldset>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 col-sm-6">
                            <fieldset class="fieldset">
                                <h2 class="sr-only">Totales mensuales</h2>
                                <legend class="fieldset">Totales mensuales</legend>
                                <div class="">
                                    <table id="tablaTotal" class="display texto-tabla cell-border" width="100%">
                                        <thead>
                                            <tr>
                                                <th id="MA" class="cabecera bg-primary text-left">Mes/Año</th>
                                                <th id="H" class="cabecera bg-primary text-right">Horas</th>
                                            </tr>
                                        </thead>
                                        <tbody id="bodyTablaTotal">                                                                           
                                        </tbody>
                                        <tfoot></tfoot>
                                    </table>
                                </div>
                            </fieldset>                
                        </div>     
                        <div class="col-xs-12 col-sm-6">
                            <fieldset class="fieldset">
                                <h2 class="sr-only">Detalle</h2>
                                <legend class="fieldset">Detalle</legend>
                                <div class="">
                                    <table id="tablaDetalle" class="display texto-tabla cell-border" width="100%">
                                        <thead>
                                            <tr>
                                                <th id="F" class="cabecera bg-primary text-left">Fecha</th>
                                                <th id="D" class="cabecera bg-primary text-left">Día</th>
                                                <th id="HR" class="cabecera bg-primary text-right">Horas</th>
                                            </tr>
                                        </thead>
                                        <tbody id="bodyTablaDetalle">                                                                          
                                        </tbody>
                                        <tfoot></tfoot>
                                    </table>
                                </div>
                            </fieldset>                
                        </div>                   
                    </div>
            </div>
        </div>
    </div>
</div>
<div id="indicator"></div>  
</form> 
</body>

</html>
<script src="<% =Session["strServer"].ToString() %>scripts/ssrs.js?v=23/04/2018"></script>
<script src="<% =Session["strServer"].ToString() %>Javascript/funciones.js"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/jquery-ui.min.js"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/moment.locale.min.js"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/accounting.min.js"></script>
<script src="<% =Session["strServer"].ToString() %>Capa_Presentacion/IAP30/scripts/JavaScript.js" type="text/javascript"></script>
<script src="<% =Session["strServer"].ToString() %>plugins/datatables/jquery.dataTables.min.js"></script>
<script src="<% =Session["strServer"].ToString() %>plugins/datatables/Buttons/dataTables.buttons.min.js"></script>

<script src="js/View.js"></script>
<script src="js/app.js"></script>
<script src="js/JavaScript.js" type="text/javascript"></script>
