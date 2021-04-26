<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Reporte_GASVI_Default" %>

<%@ Register Src="~/Capa_Presentacion/bsUserControls/Head.ascx" TagPrefix="uc1" TagName="Head" %>
<%@ Register Src="~/Capa_Presentacion/bsUserControls/Menu/Menu.ascx" TagPrefix="uc1" TagName="Menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <uc1:Head runat="server" ID="Head" />
    <link href="<% =Session["strServer"].ToString() %>plugins/SimpleCalculadorajQuery/SimpleCalculadorajQuery.css" rel="stylesheet" /> 
    <link rel="Stylesheet" href="<% =Session["strServer"].ToString() %>css/jquery-ui.min.css"/>  
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>::: SUPER ::: - GASVI</title>
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
    <uc1:Menu runat="server" ID="Menu" />

    <div class="container-fluid">
        <h1 class="sr-only">::: SUPER ::: - GASVI</h1>        
        <div class="ibox-content blockquote blockquote-info">
            <%--<div class="ibox-title">
                <span class="text-primary">Datos de entrada</span>
                <div class="ibox-tools">
                    <a class="collapse-link">
                        <i data-toggle="collapse" data-target="#filtros" class="chevron_toggleable fa fa-chevron-up pull-right"></i>
                    </a>
                </div>
            </div>--%>
            <a class="collapse-link">
                <div class="ibox-title ibox-title_toggleable" data-toggle="collapse" data-target="#filtros">
                    <span class="text-primary">Crear solicitud GASVI - Datos de entrada</span>
                    <h2 class="sr-only">Crear solicitud GASVI - Datos de entrada</h2>
                    <div class="ibox-tools">
                        <i class="fa fa-chevron-up pull-right"></i>
                    </div>
                </div>
            </a>
            <div id="filtros" class="collapsed in sinPadding">
            <form class="form-horizontal ibox-content" role="form" runat="server">
                <fieldset >                    
                    <div id="filtrosFormulario">
                        <!--Primera fila-->
                        <div class="form-group">
                            <div class="col-xs-12 col-sm-6">
                                <label id="lblBeneficiario" for="txtnomBene" class="col-xs-12 col-md-3 control-label" runat="server">Beneficiario</label>
                                <div class="col-xs-12 col-md-8">
                                    <input id="txtnomBene" name="" type="text" class="form-control input-md" value="" readonly="readonly" aria-readonly="true" runat="server" />
                                </div>
                            </div>

                            <div class="col-xs-12 col-sm-6">
                                <label for="txtRefe" class="col-xs-12 col-md-4 control-label fk-label">Referencia</label>
                                <div class="col-xs-12 col-md-8">
                                    <input id="txtRefe" name="" type="text" class="form-control input-md" readonly="readonly" aria-readonly="true"  value="" />
                                </div>
                            </div>
                        </div>


                        <!--Segunda fila-->
                        <div class="form-group">
                            <div class="col-xs-12 col-sm-6">
                                <label for="txtConcepto" class="col-xs-12 col-md-3 control-label fk-label">Concepto<span class="text-danger"> *</span></label>
                                <div class="col-xs-12 col-md-8">
                                    <input id="txtConcepto" name="txtConcepto" type="text" class="form-control input-md" value="" required="required" aria-required="true" />
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6">
                                <label for="txtEmpresa" class="col-xs-12 col-md-4 control-label fk-label">Empresa</label>
                                <div class="col-xs-12 col-md-8">
                                    <input id="txtEmpresa" name="" type="text" class="form-control input-md" value="" readonly="readonly" aria-readonly="true" runat="server" />
                                    <select id="cboEmpresa" name="cboEmpresa" class="form-control" runat="server"></select>
                                </div>
                            </div>
                        </div>

                        <!--Tercera fila-->
                        <div class="form-group">
                            <div class="col-xs-12 col-sm-6">
                                <label for="txtMotivo" class="col-xs-12 col-md-3 control-label fk-label">Motivo</label>
                                <div class="col-xs-12 col-md-8">
                                    <!--<select id="cboMotivo" name="cboMotivo" class="form-control"><option value="1">Proyecto</option></select>-->
                                    <input id="txtMotivo" name="" type="text" class="form-control input-md" value="" readonly="readonly" aria-readonly="true" runat="server" />
                                </div>
                            </div>


                            <div class="col-xs-12 col-sm-6">
                                <label for="txtOfi" class="col-xs-12 col-md-4 control-label fk-label">Oficina liquidadora</label>
                                <div class="col-xs-12 col-md-8">
                                    <input id="txtOfi" name="txtOfi" type="text" class="form-control input-md" value="" runat="server"/>
                                </div>
                            </div>
                        </div>
                        <br class="visible-xs" />

                        <!--Cuarta fila-->
                        <div class="form-group">
                            <div class="col-xs-12 col-sm-6">
                                <div class="col-xs-12 col-md-3">
                                    <label id="lblProy" title="Selección de proyecto" role="link" class="control-label fk-label btn-link txtLinkU">Proyecto</label><span class="text-danger"> *</span>
                                 </div>   
                                <div class="col-xs-12 col-md-8">
                                    <input id="txtPro" name="" type="text" class="form-control input-md" value="" aria-required="true" required="required" runat="server" />
                                </div>
                           </div>
                            <div class="col-xs-12 col-sm-6">
                                <label for="cboMoneda" class="col-xs-12 col-md-4 control-label fk-label">Moneda</label>
                                <div class="col-xs-12 col-md-8">
                                    <select id="cboMoneda" name="cboMoneda" class="form-control">
                                    </select>
                                </div>
                            </div>
                        </div>

                        <br class="visible-xs" />

                        <!-- Quinta fila -->
                        <div class="form-group">
                            <div class="col-xs-12 col-md-6">
                                <fieldset class="fieldset">
                                    <legend class="fieldset"><span class="text-nowrap">A cobrar</span></legend>
                                    <div class="col-xs-12 col-lg-6 form-group">
                                        <label id="lblSinRet" for="txtSinRet" class="col-xs-12 col-sm-7 col-md-6 control-label fk-label text-left no-padding">Sin retención</label>
                                        <div class="col-xs-12 col-sm-5 col-md-6 no-padding">
                                            <input id="txtSinRet" name="" type="text" class="form-control input-md text-right" value="0,00" readonly="readonly" aria-readonly="true" />
                                        </div>                                                                               
                                    </div>
                                    <div class="col-xs-12 col-lg-6 form-group">
                                        <label id="lblEnNomina" for="txtNomina" class="col-xs-12 col-sm-7 col-md-6 control-label fk-label text-nowrap text-left no-padding-xs-sm-md">En nómina</label>
                                        <div class="col-xs-12 col-sm-5 col-md-6 no-padding">
                                            <input id="txtNomina" name="" type="text" class="form-control input-md text-right" value="0,00" readonly="readonly" aria-readonly="true" />
                                        </div>
                                    </div>
                                </fieldset>
                                 <br class="visible-xs" />
                            </div>
                           
                            <!--Sexta fila-->
                            <div class="col-xs-12 col-md-2">
                                <fieldset class="fieldset">
                                    <legend class="fieldset">Total viaje</legend>    
                                        <div class="form-group col-xs-6 col-md-12">                         
                                            <input id="TViaje" name="" aria-label="Total viaje" type="text" class="form-control input-md text-right" value="0,00" readonly="readonly" aria-readonly="true" />
                                        </div>
                                </fieldset>
                                 <br class="visible-xs" />
                            </div>


                            <div class="col-xs-12 col-md-4">
                                <fieldset class="fieldset">
                                    <legend class="fieldset"><span>Justificantes<span class="text-danger"> *</span></span></legend>
                                    <div id="justificantes" class="col-xs-8">
                                        <div class="radio-inline">
                                            <label for="radioSi">
                                                <input type="radio" id="radioSi" name="optradio" value="1" required="required" aria-required="true" />Sí</label>
                                        </div>
                                        <div class="radio-inline">
                                            <label for="radioNo">
                                                <input type="radio" id="radioNo" name="optradio" value="0" />No</label>
                                        </div>
                                    </div>

                                    <div class="col-xs-4">
                                        <span class="fa-stack fa-lg">
                                            <i class="fa fa-file-o fa-stack-2x text-muted"></i>
                                            <i id="icoJustificante" class="fa fa-question-circle fa-stack-1x text-primary em1"></i>
                                        </span>
                                    </div>
                                </fieldset>
                            </div>


                        </div>


                        <!--Séptima fila-->
                        <div class="form-group column-vcenter">
                            <div class="col-xs-12 col-md-8">
                                <label for="txtObser" class="col-xs-12 control-label fk-label text-left">Observaciones</label>
                                <div class="col-xs-12">
                                    <textarea id="txtObser" class="form-control" name="" rows="3"></textarea>
                                </div>
                            </div>


                       
                            <div class="col-xs-12 col-md-3">
                                <!--
                                <div class="col-xs-12">
                                    <span class="fa fa-info-circle fa-1x control-label"></span>
                                    <span class="control-label">Disposiciones generales</span>
                                </div>
                                -->
                                <div id="btnAnot" class="col-xs-12">
                                    <span id="icoAnot" class="fa fa-file-o fa-2x"></span>
                                    <label class="txtLinkU"><span tabindex="0" title="Anotaciones personales" role="link">Anotaciones personales</span></label>
                                </div>                            
                            </div>

                            
                            <div class="col-xs-12 col-md-2">
                                <%--<div id="btnCalc" role="link" class="txtLinkU col-xs-12" tabindex="0" aria-label="Calculadora">
                                    <span class="fa fa-calculator fa-2x"></span>
                                </div>--%>
                            </div>
                        </div>


                    </div>

                </fieldset>
            </form>
            </div>
            <br />
             <!--Pestañas-->                
            <div class="row">
                <div class="col-xs-12">
                    <div class="">
                        <div class="ibox-title">
                            <ul id="tabs" class="nav nav-tabs" data-tabs="tabs" role="tablist">
                                <%--<ul class="nav nav-pills pull-left">--%>
                                <li class="active"><a href="#tabGastos" title="Gastos" data-toggle="tab">Gastos</a></li>
                                <li><a href="#tabAnticipos" title="Anticipos" data-toggle="tab">Anticipos</a></li>
                                <li><a href="#tabOtrosDatos" title="Otros datos" data-toggle="tab">Otros datos</a></li>
                            </ul>
                            <%--<div class="ibox-tools">
                                    <a class="collapse-link">
                                        <i data-toggle="collapse" data-target="#gastos" class="chevron_toggleable fa fa-chevron-up pull-right"></i>
                                    </a>
                                </div>--%>
                            <!--Limpiamos los floats-->
                            <div class="clearfix"></div>

                        </div>

                        <div id="gastos" class="ibox-content collapse in">
                            <div class="tab-content">
                                <!--Pestaña gastos-->
                                <div class="tab-pane fadeIn active" id="tabGastos">
                                    <div class="form-group">
                                        <div class="table-responsive">
                                            <table id="tablaGastos" class="table table-megacondensed text-center" summary="Tabla de introducción de conceptos de gasto">
                                                <thead>
                                                    <tr>
                                                        <th class="bg-primary text-center" colspan="2">FECHA</th>
                                                        <th class="bg-primary text-center" colspan="2"><span class="sr-only">Destino</span></th>
                                                        <th class="bg-primary text-center" colspan="8">SIN JUSTIFICANTE</th>
                                                        <th class="bg-primary text-center" colspan="4">CON JUSTIFICANTE</th>
                                                        <th class="bg-primary text-center"></th>
                                                    </tr>
                                                    <tr>
                                                        <th class="bg-primary text-center" colspan="2"><span class="sr-only">Fecha</span></th>
                                                        <th class="bg-primary text-center" colspan="2"><span class="sr-only">Destino</span></th>
                                                        <th class="bg-primary text-center" colspan="5">Dietas</th>
                                                        <th id="hCaution" class="bg-primary text-center" colspan="3">
                                                            <i id="imgKMSEstandares" class="txtLink fa fa-info-circle fa-lg ocultar" aria-hidden="true"></i>
                                                            Vehículo propio
                                                        </th>
                                                        <th class="bg-primary text-center" colspan="4"><span class="sr-only">Con justificante</span></th>
                                                        <th class="bg-primary text-center"><span class="sr-only">Con justificante</span></th>
                                                    </tr>
                                                    <tr>
                                                        <th id="hIni" class="bg-primary text-center">Inicio</th>
                                                        <th id="hFin" class="bg-primary text-center">Fin</th>
                                                        <th id="hDest" class="bg-primary text-center">Destino</th>
                                                        <th id="hC" class="bg-primary text-center">
                                                            <abbr title="Comentario al gasto">C</abbr></th>
                                                        <th id="hCD" class="bg-primary  text-center">
                                                            <abbr title="Número de dietas completas">C</abbr></th>
                                                        <th id="hM" class="bg-primary  text-center">
                                                            <abbr title="Número de medias dietas">M</abbr></th>
                                                        <th id="hE" class="bg-primary  text-center">
                                                            <abbr title="Número de dietas especiales">E</abbr></th>
                                                        <th id="hA" class="bg-primary  text-center">
                                                            <abbr title="Número de dietas de alojamiento">A</abbr></th>
                                                        <th id="hImpo" class="bg-primary text-center">Importe</th>
                                                        <th id="hKm" class="bg-primary text-center">
                                                            <abbr title="Kilómetro en vehículo propio">Kms.</abbr></th>
                                                        <th id="hImpoProp" class="bg-primary text-center">Importe</th>
                                                        <th id="hECO" class="bg-primary text-center">ECO</th>
                                                        <th id="hPeajes" class="bg-primary  text-center" title="Gastos de derivados de la utilización del vehículo de flota o particular, tales como peajes y aparcamientos.">Peajes
                                                        </th>
                                                        <th id="hComidas" class="bg-primary text-center" title="Gastos de manutención e invitaciones. En este último caso se precisa indicar los nombres de los invitados así como su empresa, tanto en el reverso de la factura como en el campo de observaciones de esta solicitud.">Comidas
                                                        </th>
                                                        <th id="hTransp" class="bg-primary text-center" title="Gastos de derivados de la utilización de autobuses, taxis, metro, coches de alquiler (+ gasolina),...">Transp.
                                                        </th>
                                                        <th id="hHotel" class="bg-primary text-center">Hoteles</th>
                                                        <th id="hTotal" class="bg-primary text-center">TOTAL</th>
                                                    </tr>
                                                </thead>
                                                <tbody id="bodyGastos">
                                                </tbody>
                                                <tfoot>
                                                    <tr>
                                                        <td class="pie bg-info"></td>
                                                        <td class="pie bg-info"></td>
                                                        <td class="pie bg-info" tabindex='0'>TOTAL</td>
                                                        <td class="pie bg-info"></td>
                                                        <td id="txtGSTDC" class="pie bg-info text-center" tabindex='0' aria-labelledby='hCD'></td>
                                                        <td id="txtGSTMD" class="pie bg-info text-center" tabindex='0' aria-labelledby='hM'></td>
                                                        <td id="txtGSTDE" class="pie bg-info text-center" tabindex='0' aria-labelledby='hE'></td>
                                                        <td id="txtGSTAL" class="pie bg-info text-center" tabindex='0' aria-labelledby='hA'></td>
                                                        <td id="txtGSTIDI" class="pie bg-info text-right" tabindex='0' aria-labelledby='hImpo'><span class="paddingCeldaNum"></span></td>
                                                        <td id="txtGSTKM" class="pie bg-info text-right" tabindex='0' aria-labelledby='hKm'>
                                                            <span class="paddingCeldaNum"></span>
                                                        </td>
                                                        <td id="txtGSTIKM" class="pie bg-info text-right" tabindex='0' aria-labelledby='hImpoProp'><span class="paddingCeldaNum"></span></td>
                                                        <td class="pie bg-info text-right"></td>
                                                        <td id="txtGSTPE" class="pie bg-info text-right" tabindex='0' aria-labelledby='hPeajes'><span class="paddingCeldaNum"></span></td>
                                                        <td id="txtGSTCO" class="pie bg-info text-right" tabindex='0' aria-labelledby='hComidas'><span class="paddingCeldaNum"></span></td>
                                                        <td id="txtGSTTR" class="pie bg-info text-right" tabindex='0' aria-labelledby='hTransp'><span class="paddingCeldaNum"></span></td>
                                                        <td id="txtGSTHO" class="pie bg-info text-right" tabindex='0' aria-labelledby='hHotel'><span class="paddingCeldaNum"></span></td>
                                                        <td id="txtGSTOTAL" class="pie bg-info text-right" tabindex='0' aria-labelledby='hTotal'><span class="paddingCeldaNum"></span></td>
                                                    </tr>
                                                </tfoot>
                                            </table>
                                        </div>

                                        <div id="pieTabla" class="pull-right">
                                            <h2 class="sr-only">Acciones para la tabla</h2>
                                            <ul class="list-inline">
                                                <li><span id="NFilaGasto" tabindex="0" role="button" aria-label="Añadir nueva fila de gasto" title="Añadir nueva fila de gasto" class="fa fa-plus fa-1x text-success"></span></li>
                                                <li><span id="EFilaGasto" tabindex="0" role="button" aria-label="Eliminar fila de gasto seleccionada" title="Eliminar fila de gasto seleccionada" class="fa fa-minus fa-1x text-warning"></span></li>
                                                <li><span id="DFilaGasto" tabindex="0" role="button" aria-label="Duplicar fila de gasto seleccionada" title="Duplicar fila de gasto seleccionada" class="fa fa-clone fa-1x"></span></li>
                                            </ul>
                                        </div>

                                    </div>
                                </div>

                                <!--Pestaña anticipos-->
                                <div class="tab-pane fade" id="tabAnticipos">
                                    <div class="tab-pane col-xs-12" id="anticipos">
                                        <h2 class="sr-only">Anticipos</h2>
                                        <div class="form-group">
                                            <fieldset class="fieldset">
                                                <legend class="fieldset"><span>LIQUIDACIÓN DE ANTICIPOS (Sólo si hay anticipos a liquidar)</span></legend>
                                                <div class="col-xs-12 col-sm-4">
                                                    <table id="tablaAnticipado" class="table table-bordered table-condensed" aria-label="Anticipado" summary="Tabla de liquidación de anticipos - Anticipado">
                                                        <tbody>
                                                            <tr>
                                                                <th class="bg-primary text-center">Anticipado</th>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <label for="FAnti" class="col-xs-4 control-label fk-label">Fecha</label>
                                                                    <div class="col-xs-6 col-sm-8 col-md-6">
                                                                        <input id="FAnti" name="" type="text" class="form-control input-md calendar-off" value="" />
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <label for="ImpoAnti" class="col-xs-4 control-label fk-label">Importe</label>
                                                                    <div class="col-xs-6 col-sm-8 col-md-6">
                                                                        <input id="ImpoAnti" name="" type="text" class="form-control input-md anticipo" value="" />
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <label for="OfiAnti" class="col-xs-4 control-label fk-label">Oficina</label>
                                                                    <div class="col-xs-8">
                                                                        <input id="OfiAnti" name="" type="text" class="form-control input-md" value="" />
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                                <div class="col-xs-12 col-sm-4">
                                                    <table id="tablaDevuelto" class="table table-bordered table-condensed" aria-label="Devuelto" summary="Tabla de liquidación de anticipos - Devuelto">
                                                        <tbody>
                                                            <tr>
                                                                <th class="bg-primary text-center">Devuelto</th>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <label for="FDev" class="col-xs-4 control-label fk-label">Fecha</label>
                                                                    <div class="col-xs-6 col-sm-8 col-md-6">
                                                                        <input id="FDev" name="" type="text" class="form-control input-md" value="" />
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <label for="ImpoDev" class="col-xs-4 control-label fk-label">Importe</label>
                                                                    <div class="col-xs-6 col-sm-8 col-md-6">
                                                                        <input id="ImpoDev" name="" type="text" class="form-control input-md anticipo" value="" />
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <label for="OfiDev" class="col-xs-4 control-label fk-label">Oficina</label>
                                                                    <div class="col-xs-8">
                                                                        <input id="OfiDev" name="" type="text" class="form-control input-md" value="" />
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                                <div class="col-xs-12 col-sm-4">
                                                    <table id="tablaAclaraciones" class="table table-bordered table-condensed" summary="Tabla de liquidación de anticipos - Aclaraciones">
                                                        <tbody>
                                                            <tr>
                                                                <th class="bg-primary text-center">Aclaraciones</th>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <div class="col-xs-12">
                                                                        <textarea class="form-control txtArea" id="txtAclaraciones" aria-label="Aclaraciones" name="" rows="4"></textarea>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </fieldset>
                                        </div>
                                        <div class="form-group">
                                            <fieldset class="fieldset">
                                                <legend class="fieldset"><span>GASTOS PAGADOS POR LA EMPRESA (Sólo si se va a facturar al cliente. Importes sin IVA) </span></legend>
                                                <div class="col-xs-12 col-sm-6">
                                                    <table id="tablaBilletes" class="table table-bordered table-condensed" aria-label="Billetes de agencia" summary="Tabla de gastos pagados por la empresa - Billetes de agencia">
                                                        <tbody>
                                                            <tr>
                                                                <th class="bg-primary text-center" colspan="4">Billetes de agencia</th>
                                                            </tr>
                                                            <tr>
                                                                <th class="bg-primary text-center">
                                                                    <label for="TransGas" class="fk-label">Transporte</label></th>
                                                                <th class="bg-primary text-center">
                                                                    <label for="HGas" class="fk-label">Hotel</label></th>
                                                                <th class="bg-primary text-center">
                                                                    <label for="OGas" class="fk-label">Otros</label></th>
                                                                <th class="bg-primary text-center">
                                                                    <label for="totGas" class="fk-label">Total €</label></th>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <input id="TransGas" class="gpe form-control input-md" type="text" /></td>
                                                                <td>
                                                                    <input id="HGas" class="gpe form-control input-md" type="text" /></td>
                                                                <td>
                                                                    <input id="OGas" class="gpe form-control input-md" type="text" /></td>
                                                                <td>
                                                                    <input id="totGas" class="form-control input-md" readonly='readonly' aria-readonly='true' type="text" /></td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                                <div class="col-xs-12 col-sm-6">
                                                    <table id="tablaAclaraciones2" class="table table-bordered table-condensed" summary="Tabla de gastos pagados por la empresa - Aclaraciones">
                                                        <tbody>
                                                            <tr>
                                                                <th class="bg-primary text-center" colspan="2">Aclaraciones</th>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <div class="col-xs-12">
                                                                        <textarea id="txtAclaraciones2" class="form-control txtArea" aria-label="Aclaraciones" name="" rows="4"></textarea>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </fieldset>
                                        </div>
                                    </div>
                                </div>
                                <!--FIN Pestaña anticipos-->

                                <!--Pestaña otros datos-->
                                <div class="tab-pane fade" id="tabOtrosDatos">
                                    <div class="tab-pane col-xs-12" id="otrosdatos">
                                        <h2 class="sr-only">Otros datos</h2>
                                        <div class="row">
                                            <div class="col-xs-12 col-md-5">
                                                <fieldset class="fieldset">
                                                    <legend class="fieldset"><span id="lblTerritorio" runat="server"></span></legend>
                                                    <table id="tablaTerritorio" class="table table-bordered table-condensed" summary="Tabla de importes por concepto del territorio fiscal de Gipuzkoa">
                                                        <tbody>
                                                            <tr>
                                                                <th class="bg-primary text-center vcenter" rowspan="2">Conceptos</th>
                                                                <th class="bg-primary text-center" colspan="2">Importes €</th>
                                                            </tr>
                                                            <tr>
                                                                <th class="bg-primary text-center">Convenio</th>
                                                                <th class="bg-primary text-center">Exento de retención</th>
                                                            </tr>
                                                            <tr>
                                                                <td class="text-center">Kilometraje</td>
                                                                <td id="cldKMCO" runat="server" class="text-right"></td>
                                                                <td id="cldKMEX" runat="server" class="text-right"></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="text-center">Dieta completa</td>
                                                                <td id="cldDCCO" runat="server" class="text-right"></td>
                                                                <td id="cldDCEX" runat="server" class="text-right"></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="text-center">Media dieta</td>
                                                                <td id="cldMDCO" runat="server" class="text-right"></td>
                                                                <td id="cldMDEX" runat="server" class="text-right"></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="text-center">Dieta especial</td>
                                                                <td id="cldDECO" runat="server" class="text-right"></td>
                                                                <td id="cldDEEX" runat="server" class="text-right"></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="text-center">Dieta alojamiento</td>
                                                                <td id="cldDACO" runat="server" class="text-right"></td>
                                                                <td id="cldDAEX" runat="server" class="text-right"></td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </fieldset>
                                            </div>
                                            <div class="col-xs-12 col-md-7">
                                                <fieldset class="fieldset">
                                                    <legend class="fieldset"><span>Historial</span></legend>
                                                    <table id="tablaHistorial" class="table table-bordered table-condensed" summary="Tabla de historial de hojas de gasto">
                                                        <tbody>
                                                            <tr>
                                                                <th class="bg-primary text-center">Estado</th>
                                                                <th class="bg-primary text-center">Fecha</th>
                                                                <th class="bg-primary text-center">Profesional/Motivo</th>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </fieldset>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!--FIN Pestaña otros datos-->
                            </div>
                        </div>

                    </div>
                </div>
                <div class="pull-right botonesPie">
                    <div class="col-xs-6">
                        <button id="btnTramitar" type="button" role="link" class="btn btn-primary">Tramitar</button>
                    </div>
                    <div class="col-xs-6">
                        <button id="btnVolver" type="button" role="link" class="btn btn-default">Volver</button>
                    </div>
                </div>
            </div>
            <!--FIN Pestaña gastos-->

        </div>
    </div>
    <!-- Modal de anotaciones personales -->
    <div class="modal fade" id="anotaciones" role="dialog" tabindex="-1" title="::: SUPER ::: - Anotaciones personales">
 	    <div class="modal-dialog" role="dialog">
 		    <div class="modal-content">
 			    <div class="modal-header bg-primary">
 			        <button type="button" class="close" id="closeAnota" data-dismiss="modal" aria-label="Cerrar"><span aria-hidden="true">&times;</span></button>
 				    <h4 class="modal-title">::: SUPER ::: - Anotaciones personales</h4>
 			    </div>
 			    <div class="modal-body">
 				     <form class="form-horizontal">
                        <div class="form-group">
                            <div class="col-xs-12">
                                <div class="col-xs-12">
                                    <textarea id="txtDesAnotaciones" class="form-control txtArea" name="" rows="6"></textarea>
                                </div>
                            </div>
                        </div>
                        </form>
 			    </div>
 			    <div class="modal-footer">
 				    <button id="btnAceptarAnot" class="btn btn-primary" data-dismiss="modal">Aceptar</button>
 				    <button id="btnCancelarAnot" class="btn btn-default" data-dismiss="modal">Cancelar</button>
 			    </div>
 		    </div>
 	    </div>
     </div>

     <!-- Modal de comentario de gasto -->
    <div class="modal fade" id="comentarioGasto" role="dialog" tabindex="-1" title="::: SUPER ::: - Comentario">
        <div class="modal-dialog" role="dialog">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">::: SUPER ::: - Comentario</h4>
                </div>
                <div class="modal-body">
                    <form class="form-horizontal">
                    <div class="form-group">
                        <div class="col-xs-12 no-padding">
                                <div class="col-xs-12">
                                    <textarea id="txtDesComentario" class="form-control txtArea" name="" rows="6"></textarea>
                                </div>
                        </div>
                    </div>
                    </form>
                </div>
                <div class="modal-footer">                   
                    <button id="btnAceptarComent" class="btn btn-primary" data-dismiss="modal">Aceptar</button>                   
                    <button id="btnCancelarComent" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

     <!-- Modal de calculadora -->
    <div class="modal fade" id="calculadora" role="dialog" tabindex="-1" title="::: SUPER ::: - Calculadora">
        <div class="modal-dialog modal-dialog-calc" role="dialog">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">::: SUPER ::: - Calculadora</h4>
                </div>
                <div class="modal-body">
                    <div class="row row-modal">
                        <div class="col-xs-12">
                            <div id="idCalculadora"></div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <b>
                        <button id="btnLlevarValor" class="btn btn-primary">Llevar</button></b>
                    <b>
                        <button id="btnCancelarCalc" class="btn btn-default" data-dismiss="modal">Cancelar</button></b>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <div class="fk_ayudaProyecto"></div>

    <!-- Modal de distancias kilométricas -->
    <div class="modal fade" id="distancias" role="dialog" tabindex="-1" title="::: SUPER ::: - Distancias kilométricas">
 	    <div class="modal-dialog" role="dialog" >
 		    <div class="modal-content">
 			    <div class="modal-header bg-primary">
 			        <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar"><span aria-hidden="true">&times;</span></button>
 				    <h4 class="modal-title">::: SUPER ::: - Distancias kilométricas</h4>
 			    </div>
 			    <div class="modal-body">
 				     <form class="form-horizontal">
                        <div class="form-group">
                            <div class="col-xs-12">
                                Distancia kilométrica ida y vuelta<br /><br />
                                Miramon/Zuatzu:<br />
                                Derio  190<br />Miñano   190<br />Cercas Bajas 210<br />Elgoibar 100<br />Pamplona 160<br /><br />
                                Derio:<br />Miramon/Zuatzu 190<br />Miñano   180<br />Cercas Bajas 160<br />Elgoibar 100<br />Pamplona 330<br /><br />
                                Miñano:<br />Miramon/Zuatzu    190<br />Derio    180<br />Elgoibar 105<br />Pamplona 200<br /><br />
                                Cercas Bajas:<br />Miramon/Zuatzu  210<br />
                            </div>
                        </div>
                        </form>
 			    </div>
 			    <div class="modal-footer">
                     <b><button id="btnCancelar" class="btn btn-default" data-dismiss="modal">Cerrar</button></b>                    
 			    </div>
 		    </div>
 	    </div>
    </div>

    <!-- Modal Selección de desplazamiento ECO-->
    <div class="modal fade" id="desplazamiento">
        <div class="modal-dialog modal-lg" role="dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title">::: SUPER ::: - Selección de desplazamiento ECO</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="table-responsive">
                            <h2 class="sr-only">Tabla de contenido</h2>                                
                            <table id="tblDesplaza" class="table table-bordered table-no-border table-hover table-condensed" summary="Tabla de contenido">
								<thead>
									<tr>
										<th class='bg-primary' title="Referencia ECO" style="padding-right:10px; text-align:right;">Ref.</th>
										<th class='bg-primary'>Destino</th>
										<th class='bg-primary'>Observaciones</th>
										<th class='bg-primary'>Ida</th>
										<th class='bg-primary'>Vuelta</th>
										<th class='bg-primary' style="padding-right:2px; text-align:right;">Nº usos</th>
									</tr>
								</thead>
								<tbody id="tbodyDesplaza" class="cabeceraFija">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>	
                <div class="modal-footer">
                    <b><button id="btnSeleccionarDesplazamiento" class="btn btn-primary" disabled="disabled">Seleccionar</button></b>
                    <b><button id="btnCancelaDesplazamiento" class="btn btn-default" data-dismiss="modal">Cancelar</button></b>
                </div>
            </div>
        </div>
    </div>

    <input type="hidden" name="hdnIdProyectoSubNodo" id="hdnIdProyectoSubNodo" value="" runat="server" /> 
    <input type="hidden" name="hdnAnotacionesPersonales" id="hdnAnotacionesPersonales" value="" runat="server" /> 

</body>
</html>
<script src="<% =Session["strServer"].ToString() %>Capa_Presentacion/IAP30/scripts/JavaScript.js" type="text/javascript"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/jquery-ui.min.js"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/moment.locale.min.js" type="text/javascript"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/accounting.min.js" type="text/javascript" ></script>
<script src="<% =Session["strServer"].ToString() %>scripts/string.js"></script>
<%--<script src="<% =Session["strServer"].ToString() %>scripts/string.js" type="text/javascript" ></script>--%>
<script src="<% =Session["strServer"].ToString() %>scripts/stringbuilder.js" type="text/javascript" ></script>
<script src="<% =Session["strServer"].ToString() %>plugins/SimpleCalculadorajQuery/SimpleCalculadorajQuery.js" type="text/javascript"></script>
<script src="<% =Session["strServer"].ToString() %>plugins/IB/buscaproyecto.js" type="text/javascript"></script>
<script src="js/view.js?20170313_01" type="text/javascript"></script>
<script src="js/app.js?20171019" type="text/javascript"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/ssrs.js?v=23/04/2018"></script>
