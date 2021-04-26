<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Consultas_Facturabilidad_Default" %>
<%@ Register Src="~/Capa_Presentacion/bsUserControls/Menu/Menu.ascx" TagPrefix="cb1" TagName="Menu" %>
<%@ Register Src="~/Capa_Presentacion/bsUserControls/Head.ascx" TagPrefix="cb1" TagName="Head" %>
<!DOCTYPE html>
<html lang="es" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">    
    <cb1:Head runat="server" id="Head" />
    <title>::: SUPER ::: - Consulta de Facturabilidad</title>
    <link rel="stylesheet" type="text/css" href="<% =Session["strServer"].ToString() %>plugins/daterangepicker/daterangepicker.css?20170503_01" />
    <link rel="Stylesheet" href="<% =Session["strServer"].ToString() %>plugins/datatables/jquery.dataTables.min.css"/>
    <link rel="Stylesheet" href="<% =Session["strServer"].ToString() %>plugins/datatables/Buttons/buttons.dataTables.min.css"/>
    <link rel='stylesheet' href="<% =Session["strServer"].ToString() %>plugins/BuscadorPersonas/BuscadorPersonas.css" />
</head>
<body data-codigopantalla="110">   
    <script>
        var opciones = {delay: 1}
        IB.procesando.opciones(opciones);
        IB.procesando.mostrar();
    </script>
    <cb1:Menu runat="server" id="Menu" />
    <form id="frmDatos" runat="server"></form>
    <div class="container-fluid">
        <div class="ibox-content blockquote blockquote-info">
        <div class="panel-group">
            <div class="ibox-title">
        <h1 class="sr-only">::: SUPER ::: - Consulta de Facturabilidad</h1>
        
        <%--<br class="hidden-xs" />--%>
        <div class="row">
            <!--<div class="divIconos icono col-xs-9 col-sm-8 col-md-7">-->
                <!--<div class="text-center pull-right">-->
					<div id="divProf" title="Selección de profesional" class="link text-center col-xs-12 col-sm-offset-3 col-sm-6" role="link" aria-label="Cambio de profesional" tabindex="0" >						
						<span id="icoProf" class="fa fa-user fa-2x"></span>
                        <span class="txtProfesional">Profesional:</span>
                        <%--<img alt="Empleado externo" id="imagenProf" class="img-responsive" style="float:left;"/>--%>
						<span class="txtProfesional" id="spProfesional"><%=Session["DES_EMPLEADO_IAP"]%></span>
					</div>
                <!--</div>-->
            <!--</div>-->
            <!--
            <div class="col-xs-3 col-sm-4 col-md-5 no-padding-right">
                <div id="divGuia" class="pull-right">
                    <button id="btnGuia" type="button" class="btn btn-default">
                        <span class="fa fa-info-circle" style="vertical-align: middle;"></span>
                        <span>Guía</span>
                    </button>
                </div>
            </div>
            -->
        </div>


            <div id="frmDatos2" class="form-group">
                <h2 class="hiddenStructure">Criterios de búsqueda</h2>
                <div class="col-xs-12 col-sm-offset-2 col-sm-10 col-md-offset-3 col-md-9 no-padding">
                    <label id="lblRango" class="col-xs-12 col-sm-4 col-md-3 control-label fk-label" for="txtRango">Periodo de selección</label>
                    <div class="col-xs-8 col-sm-5 col-md-4 col-lg-3">
                        <input title="Formato fecha: dd/mm/aaaa - dd/mm/aaaa" placeholder="dd/mm/aaaa - dd/mm/aaaa" id="txtRango" type="text" class="form-control input-md" value="" />
                    </div>
                </div>
                <%--<div class="col-xs-12 col-sm-4 col-md-3 col-lg-2 no-padding">
                    <label id="lblDesde" for="txtDesde" class="col-xs-3 col-sm-4 col-lg-3 control-label fk-label">Desde</label>
                    <div class="col-xs-5 col-sm-7 col-lg-9 ">
                        <input title="Formato fecha: dd/mm/aaaa" placeholder="dd/mm/aaaa" id="txtDesde" name="" type="text" class="form-control input-md calendar-off"" value="" />
                    </div>
                </div>
                <div class="col-xs-12 col-sm-4 col-md-3 col-lg-2 no-padding">
                    <label id="lblHasta" for="txtHasta" class="col-xs-3 col-sm-4 col-lg-3 control-label fk-label">Hasta</label>
                    <div class="col-xs-5 col-sm-7 col-lg-9">
                        <input title="Formato fecha: dd/mm/aaaa" placeholder="dd/mm/aaaa" id="txtHasta" name="" type="text" class="form-control input-md calendar-off"" value="" />
                    </div>
                </div>--%>
            </div>

                <div class="row ocultaProcesando">
                    <div id="contenedor-tabla">
                        <h2 class="hiddenStructure">Resultados por proyecto económico</h2>
                        <table id="tabla" class="display texto-tabla cell-border" width="100%" summary="Tabla de resultados por proyecto económico">
                            <thead>
                                <%--<tr>
                            <th colspan="3">&nbsp;</th>
                            <th colspan="2" class="colTabla2 text-center">Planificación</th>
                            <th colspan="4" class="colTabla text-center">Periodo</th>
                            <th colspan="4" class="colTabla1 text-center">Inicio proyecto - > Fin periodo</th>
                        </tr>--%>
                                <tr id="cabProy">
                                    <th id="PE" data-type="String" class="bg-primary">Proyecto económico</th>
                                    <th id="T" data-type="String" class="bg-primary">Tarea</th>
                                    <th id="F" data-type="String" class="bg-primary"><abbr title="Facturabilidad">Fa.</abbr></th>
                                    <th id="HP" data-type="Double" class="bg-primary"><abbr title="Horas planificadas para la tarea">H. Pl.</abbr></th>
                                    <th id="HPR" data-type="Double" class="bg-primary"><abbr title="Horas previstas para el profesional en la tarea">H. Pr. P.</abbr></th>
                                    <th id="PHA" data-type="Double" class="bg-primary"><abbr title="Horas planificadas en la agenda por el profesional para la tarea dentro del periodo">H. Agen.</abbr></th>
                                    <th id="PHPRF" data-type="Double" class="bg-primary"><abbr title="Horas imputadas por el profesional a la tarea dentro del periodo">H. Prof.</abbr></th>
                                    <th id="PHO" data-type="Double" class="bg-primary"><abbr title="Horas imputadas por otros profesionales a la tarea dentro del periodo">H. Otros</abbr></th>
                                    <th id="PPT" data-type="Double" class="bg-primary"><abbr title="Total de horas imputadas a la tarea dentro del periodo">Total</abbr></th>
                                    <th id="IFHA" data-type="Double" class="bg-primary"><abbr title="Horas planificadas en la agenda por el profesional para la tarea desde el inicio del proyecto hasta el fin del periodo">H. Agen.F</abbr></th>
                                    <th id="IFHPRF" data-type="Double" class="bg-primary"><abbr title="Horas imputadas por el profesional a la tarea desde el inicio del proyecto hasta el fin del periodo">H. ProfF.</abbr></th>
                                    <th id="IFHO" data-type="Double" class="bg-primary"><abbr title="Horas imputadas por otros profesionales a la tarea desde el inicio del proyecto hasta el fin del periodo">H. OtrosF</abbr></th>
                                    <th id="IFPT" data-type="Double" class="bg-primary"><abbr title="Total de horas imputadas a la tarea desde el inicio del proyecto hasta el fin del periodo">TotalF</abbr></th>
                                </tr>
                            </thead>
                            <tbody id="bodyTabla"></tbody>
                            <tfoot>
                                <tr>
                                    <td class="bg-info"></td>
                                    <td class="bg-info">Totales:</td>
                                    <td class="bg-info"></td>
                                    <td class="bg-info text-right"></td>
                                    <td class="bg-info text-right"></td>
                                    <td class="bg-info text-right"></td>
                                    <td class="bg-info text-right"></td>
                                    <td class="bg-info text-right"></td>
                                    <td class="bg-info text-right"></td>
                                    <td class="bg-info text-right"></td>
                                    <td class="bg-info text-right"></td>
                                    <td class="bg-info text-right"></td>
                                    <td class="bg-info text-right"></td>
                                    <!-- class="pie bg-info text-center"-->
                                </tr>
                            </tfoot>
                        </table>
                    </div>
                </div>
                <br />
                <div class="row ocultaProcesando">
                    <div class="col-xs-12 col-md-8 no-padding">
                        <h2 class="hiddenStructure">Resultados por tipología de proyecto</h2>
                        <table id="tabla2" class="display texto-tabla cell-border" width="100%">
                            <thead>
                                <%--<tr>
                                    <th colspan="2">
                                        <span class="text-nowrap">
                                            <img alt="Icono de tarea facturable" src="../../../../Images/imgIcoMonedas.gif" />
                                            Tarea facturable
                                        </span>
                                        <span class="text-nowrap">
                                            <img alt="Icono de tarea no facturable" src="../../../../Images/imgIcoMonedasOff.gif" />
                                            Tarea no facturable
                                        </span>
                                    </th>
                                    <th class="text-center colTabla2" colspan="2">
                                        <span class="text-nowrap">Imputaciones registradas</span>
                                    </th>
                                </tr>--%>
                                <tr id="cabTipologia">
                                    <th id="TP" data-type="String" class="bg-primary">Tipología de proyecto</th>
                                    <th id="NP" data-type="String" class="bg-primary">Naturaleza de producción</th>
                                    <th id="TF" data-type="Double" class="bg-primary">
                                        <%--<img alt="Icono de tarea facturable" border="0" src="../../../../Images/imgIcoMonedas.gif" />--%>
                                        Facturable
                                    </th>
                                    <th id="TNF" data-type="Double" class="bg-primary">
                                        <%--<img alt="Icono de tarea no facturable" border="0" src="../../../../Images/imgIcoMonedasOff.gif" />--%>
                                        No facturable
                                    </th>
                                </tr>
                            </thead>
                            <tbody id="bodyTabla2">
                            </tbody>
                            <tfoot>
                                <tr>
                                    <td class="pie bg-info"></td>
                                    <td class="pie bg-info">Total horas:</td>
                                    <td class="pie bg-info text-right"></td>
                                    <td class="pie bg-info text-right"></td>
                                </tr>

                            </tfoot>
                        </table>
                        <div>
                            <span class="text-nowrap control-label">
                                <img alt="Icono de tarea facturable" src="../../../../Images/imgIcoMonedas.gif" />
                                Tarea facturable
                            </span>
                            <span class="text-nowrap control-label">
                                <img alt="Icono de tarea no facturable" src="../../../../Images/imgIcoMonedasOff.gif" />
                                Tarea no facturable
                            </span>
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-offset-2 col-sm-8 col-md-offset-0 col-md-4">
                        <!--grafico -->
                        <canvas id="myChart"></canvas>
                    </div>
                </div>
        </div>
        </div>
        </div>
        </div>

    <div class="buscadorUsuario" id="buscProf"></div>

</body>

</html>
<script src="<% =Session["strServer"].ToString() %>Capa_Presentacion/IAP30/scripts/JavaScript.js" type="text/javascript"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/moment.locale.min.js"></script>
<script type="text/javascript" src="<% =Session["strServer"].ToString() %>plugins/daterangepicker/daterangepicker.js?20170503_01"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/accounting.min.js" type="text/javascript" ></script>
<script src="<% =Session["strServer"].ToString() %>scripts/stringbuilder.js" type="text/javascript" ></script>
<script src="js/view.js?20170503_01" type="text/javascript"></script>
<script src="js/app.js" type="text/javascript"></script>
<script src="<% =Session["strServer"].ToString() %>plugins/Chart/Chart.min.js" type="text/javascript" ></script>
<script src="<% =Session["strServer"].ToString() %>plugins/datatables/jquery.dataTables.min.js"></script>
<script src="<% =Session["strServer"].ToString() %>plugins/datatables/Buttons/dataTables.buttons.min.js"></script>
<script src="<% =Session["strServer"].ToString() %>plugins/BuscadorPersonas/BuscadorPersonas.js"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/exportaciones.js" type="text/javascript" ></script>
