<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Consultas_Imputaciones_Default" %>

<%@ Register Src="~/Capa_Presentacion/bsUserControls/Menu/Menu.ascx" TagPrefix="cb1" TagName="Menu" %>
<%@ Register Src="~/Capa_Presentacion/bsUserControls/Head.ascx" TagPrefix="cb1" TagName="Head" %>

<!DOCTYPE html>

<html lang="es" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <cb1:Head runat="server" id="Head" />
    <title>::: SUPER ::: - Consulta de imputaciones</title>
</head>
<body data-codigopantalla="130">
    <script>
        var opciones = { delay: 1 }
        IB.procesando.opciones(opciones);
        IB.procesando.mostrar();
    </script>
    <cb1:Menu runat="server" id="Menu" />
    <link rel="stylesheet" type="text/css" href="<% =Session["strServer"].ToString() %>plugins/daterangepicker/daterangepicker.css?20170503_01" />
    <link rel="Stylesheet" href="<% =Session["strServer"].ToString() %>plugins/datatables/jquery.dataTables.min.css"/>
    <link rel="Stylesheet" href="<% =Session["strServer"].ToString() %>plugins/datatables/Buttons/buttons.dataTables.min.css"/>
    <form id="frmDatos" runat="server"> 
     <input type="hidden" id="FORMATO" name="ctl00$CPHC$FORMATO" value="PDF" />
     <input type="hidden" id="hdnUsuario" name="ctl00$CPHC$hdnUsuario"  value="" />
     <input type="hidden" id="hdnAnoMesDesde" name="ctl00$CPHC$hdnAnoMesDesde" value="" />
     <input type="hidden" id="hdnAnoMesHasta" name="ctl00$CPHC$hdnAnoMesHasta" value="" />
    </form>
    <div class="container-fluid ocultable">
        <div class="ibox-content blockquote blockquote-info">
            <div class="panel-group">
                <div class="ibox-title">
                    <h1 class="sr-only">::: SUPER ::: - Consulta de imputaciones</h1>

                    <div class="row">
                        <fieldset class="fieldset">
                            <h2 class="sr-only">Criterios de selección</h2>
                            <legend class="fieldset"><span>Criterios de selección</span></legend>
                            <%--<form class="form-horizontal">--%>
                                    <div class="form-group">                                        
                                        <%--<div class="col-xs-12 col-sm-4 col-md-3 col-lg-2 margin-bottom-xs-sm-md no-padding">
                                            <label id="lblDesde" for="txtDesde" class="col-xs-3 col-sm-4 col-lg-3 control-label fk-label">Desde</label>
                                            <div class="clearfix visible-xs"></div>
                                            <div class="col-xs-9 col-sm-8 col-lg-9 no-padding-right input-group">
                                                <span class="input-group-addon"><i class="fa fa-calendar" aria-hidden="true"></i></span>
                                                <input title="Formato fecha: dd/mm/aaaa" placeholder="dd/mm/aaaa" id="txtDesde" name="" type="text" class="form-control input-md calendar-off" value="" runat="server"/>
                                            </div>
                                        </div>
                                        
                                        <div class="col-xs-12 col-sm-4 col-md-3 col-lg-2 margin-bottom-xs-sm-md no-padding">
								            <label id="lblHasta" class="col-xs-3 col-sm-4 col-lg-3 control-label fk-label" for="txtHasta">Hasta</label>
                                            <div class="clearfix visible-xs"></div>
								            <div class="col-xs-9 col-sm-8 col-lg-9 no-padding-right input-group">
                                                <span class="input-group-addon"><i class="fa fa-calendar" aria-hidden="true"></i></span>
									            <input title="Formato fecha: dd/mm/aaaa" placeholder="dd/mm/aaaa" id="txtHasta" type="text" class="form-control input-md calendar-off" value="" runat="server"/>
								            </div>
                                        </div>--%>

                                        <div class="col-xs-12 col-sm-12 col-lg-4 margin-bottom-xs-sm-md no-padding">
								            <label id="lblRango" class="col-xs-12 col-sm-4 col-md-3 col-lg-5 control-label fk-label" for="txtRango">Periodo de selección</label>                                            
								            <div class="col-xs-9 col-sm-4 col-md-3 col-lg-7">
									            <input name="txtRango" type="text" id="txtRango" title="Formato fecha: dd/mm/aaaa - dd/mm/aaaa" placeholder="dd/mm/aaaa" class="form-control input-md"/>
								            </div>
                                        </div>

                                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-8 no-padding">
                                            <label class="col-xs-12 col-sm-1 col-md-1 col-lg-2 control-label fk-label" for="cboProyecto">Proyecto</label>
                                            <div class="col-xs-12 col-sm-11 col-md-11 col-lg-10">
                                                <select id="cboProyecto" name="cboProyecto" class="form-control" runat="server">
                                                    <option>Todos</option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                <%--</form>--%>
                        </fieldset>
                    </div>
                    <div class="row">
                        <div class="col-xs-7 col-sm-3 col-lg-2 margin-bottom-xs-sm-md no-padding">
                            <div id="niveles" class="btn-group btn-group-xs" role="group">
                                <button id="nivel1" data-level="1" aria-label="Nivel 1" title="Proyectos económicos" type="button" class="btn btn-default btn-blanco">
                                    <span id="barra1" class="fa fa-square fa-square-1 nivelVerde"></span>
                                </button>
                                <button id="nivel2" data-level="2" aria-label="Nivel 2" title="Proyectos técnicos" type="button" class="btn btn-default btn-blanco">
                                    <span id="barra2" class="fa fa-square fa-square-2 nivelGris"></span>
                                </button>
                                <button id="nivel3" data-level="3" aria-label="Nivel 3" title="Tareas" type="button" class="btn btn-default btn-blanco">
                                    <span id="barra3" class="fa fa-square fa-square-3 nivelGris"></span>
                                </button>
                                <button id="nivel4" data-level="4" aria-label="Nivel 4" title="Consumos" type="button" class="btn btn-default btn-blanco">
                                    <span id="barra4" class="fa fa-square fa-square-4 nivelGris"></span>
                                </button>
                            </div>
                            <span id="icoBomb" title="Despliegue completo de la línea seleccionada" tabindex="0" role="button" aria-label="Despliegue completo de la linea seleccionada" class="fa fa-bomb fa-1-5x txtLink"></span>
                        </div>
                        <a id="btnExportExcel" title="Exportar a EXCEL" class="dt-button pull-right" tabindex="0" role="link" aria-label="Exportar a excel el contenido de la tabla" aria-controls="tabla2" href="#">
                            <i class="fa fa-file-excel-o"></i>
                            <span>EXCEL</span>
                        </a>
                    </div>
                    <div class="row">
                        <div class="table-responsive">
                            <h2 class="sr-only">Tabla de resultados</h2>
                            <table id="tablaCabecera" class="table table-bordered table-no-border table-hover table-condensed" summary="Tabla de cabecera">
                                <thead class="cabeceraFija">
                                    <tr>
                                        <th data-type="String" id="ET" class="bg-primary">Estructura técnica / Fecha consumo / Comentarios</th>
                                        <th data-type="Double" id="H"  class="bg-primary text-center">Horas</th>
                                        <th data-type="Double" id="J"  class="bg-primary text-center">Jornadas</th>                            
                                    </tr>
                                </thead>
                            </table>
                            <div id="contenedor" class="div-table-content">
                                <h2 class="sr-only">Tabla de contenido</h2>                                
                                <table id="tblDatos" class="table cell-border table-hover table-condensed" summary="Tabla de contenido">
                                    <tbody id="bodyTabla" class="cabeceraFija">
                                    </tbody>
                                </table>
                            </div>
                            <table id="tablaPie" class="table table-bordered table-no-border table-hover table-condensed" summary="Tabla de pie">
                                <tfoot class="cabeceraFija">
                                    <tr id="pieTabla" data-level="0" class="bg-info" data-id="pieTabla" data-parent="">
                                        <td class="bg-info celdaTxtTotal" id="PieDEN"><span>Totales</span><span class="sr-only">Totales</span></td>
                                        <td class="bg-info celdaTotal " id="PieHoras"></td>
                                        <td class="bg-info celdaTotal" id="PieJornadas"></td>
                                    </tr>
                                </tfoot>
                            </table>
                        </div>
                    </div>                    
            </div>
            </div>
        </div>
    </div>
    <div id="indicator"></div>  
</body>

</html>
<script src="<% =Session["strServer"].ToString() %>scripts/moment.locale.min.js"></script>
<script type="text/javascript" src="<% =Session["strServer"].ToString() %>plugins/daterangepicker/daterangepicker.js?20170503_01"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/accounting.min.js"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/string.js"></script>
<script src="<% =Session["strServer"].ToString() %>Capa_Presentacion/IAP30/scripts/JavaScript.js" type="text/javascript"></script>
<script src="<% =Session["strServer"].ToString() %>plugins/datatables/jquery.dataTables.min.js"></script>
<script src="<% =Session["strServer"].ToString() %>plugins/datatables/Buttons/dataTables.buttons.min.js"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/exportaciones.js" type="text/javascript" ></script>
<script src="js/View.js?20170517_01"></script>
<script src="js/app.js?20170717_01"></script>