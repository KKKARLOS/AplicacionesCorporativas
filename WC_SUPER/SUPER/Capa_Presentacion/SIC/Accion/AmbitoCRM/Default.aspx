<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_SIC_Accion_AmbitoCRM_Default" %>

<%@ Register Src="~/Capa_Presentacion/bsUserControls/Head.ascx" TagPrefix="uc1" TagName="Head" %>
<%@ Register Src="~/Capa_Presentacion/bsUserControls/cabeceraPreventa.ascx" TagPrefix="uc1" TagName="cabeceraPreventa" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <uc1:Head runat="server" ID="Head" />
    <title>Ambito de visión de preventa</title>
    <link href="../../../../css/jquery-ui.min.css" rel="stylesheet" />
    <link href="../../../../plugins/datatables/jquery.dataTables.min.css" rel="stylesheet" />

</head>
<body>
    <uc1:cabeceraPreventa runat="server" ID="cabeceraPreventa" />

    <form class="form-horizontal" id="frmAcciones" runat="server">

        <div class="container-fluid">

            <div class="row">

                <div class="ibox-title">
                    <h5 class="text-primary inline">Catálogo de acciones preventa solicitadas</h5>
                </div>

                <div class="panel-group">
                    <div class="panel panel-default">

                        <div class="panel-heading">
                            <a id="linkFiltros" class="underline" data-toggle="collapse" data-target="#divFiltros">Replegar filtros de búsqueda</a>
                            <span style="margin-left: 6px; margin-right: 6px">|</span>
                            <a class="underline" id="lnkLimpiarFiltros">Resetear filtros</a>
                        </div>

                        <div id="divFiltros" class="panel-body panel-collapse collapse in">
                            <!-- FILA 1 -->
                            <div class="row">

                                <!-- columna 1 --->
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label" for="cmbEstado">Estado</label>
                                        <div class="col-sm-6">
                                            <select id="cmbEstado" class="form-control input-sm fk_filter">
                                                <option value="">Selecciona...</option>
                                                <option value="A" selected="selected">Abierta</option>
                                                <option value="F">Finalizada</option>
                                                <option value="X">Anulada</option>
                                                <option value="C">Cerrada</option>
                                            </select>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label title="Oportunidad, extensión u objetivo" class="col-sm-2 control-label" for="cmbOrigen">ON/Ext/Obj</label>
                                        <div class="col-sm-6">
                                            <select id="cmbOrigen" class="form-control input-sm  fk_filter">
                                                <option value="">Selecciona...</option>
                                                <option value="O">Oportunidad</option>
                                                <option value="E">Extensión</option>
                                                <option value="P">Objetivo</option>
                                            </select>
                                        </div>
                                    </div>


                                </div>
                                <!-- fin columna 1 --->

                                <!-- columna 2 --->
                                <div class="col-sm-6">

                                    <div class="form-group">
                                        <label class="col-sm-4 control-label text-right" for="txtImporteDesde">Importe desde (EUR)</label>
                                        <div class="col-sm-3">
                                            <input type="text" id="txtImporteDesde" class="form-control text-right  fk_filter" />
                                        </div>
                                        <label class="col-sm-2 control-label text-right" for="txtImporteHasta">Hasta (EUR)</label>
                                        <div class="col-sm-3">
                                            <input type="text" id="txtImporteHasta" class="form-control text-right fk_filter " />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-4 control-label text-right" for="txtFFinDesde">Fin requerido desde</label>
                                        <div class="col-sm-3">
                                            <input type="text" id="txtFFinDesde" class="form-control text-center fk_filter" />
                                        </div>
                                        <label class="col-sm-2 control-label text-right" for="txtFFinHasta">Hasta</label>
                                        <div class="col-sm-3">
                                            <input type="text" id="txtFFinHasta" class="form-control text-center fk_filter" />
                                        </div>
                                    </div>

                                </div>
                                <!-- fin columna 2 --->


                            </div>
                            <!-- FIN FILA 1 -->

                            <!-- FILA 2 -->
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <div class="col-sm-2">
                                            <a id="lnkAyudaItemOrigen" class="underline control-label">Origen</a>
                                        </div>
                                        <div class="col-sm-10">
                                            <div class="input-group row">
                                                <input type="text" id="txtItemOrigen" class="form-control fk_filter" readonly="readonly" />
                                                <span class="input-group-btn">
                                                    <button class="btn btn-default btn-inline-input" type="button" id="btnClearItemOrigen">
                                                        <i class="fa fa-trash-o" aria-hidden="true"></i>
                                                    </button>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <div class="col-sm-2 text-right">
                                            <a id="lnkAyudaPromotor" class="underline control-label">Solicitante</a>
                                        </div>
                                        <div class="col-sm-10">
                                            <div class="input-group ">
                                                <input type="text" id="txtPromotor" class="form-control  fk_filter" readonly="readonly" />
                                                <span class="input-group-btn">
                                                    <button class="btn btn-default btn-inline-input" type="button" id="btnClearPromotor">
                                                        <i class="fa fa-trash-o" aria-hidden="true"></i>
                                                    </button>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <!-- FIN FILA 2 -->

                            <!-- FILA 3 -->
                            <div class="row">

                                <!-- columna 1 --->
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <fieldset class="fieldset fieldset-auto-width">
                                            <legend class="fieldset">
                                                <a id="lnkAyudaLider" class="underline control-label">Líder</a>
                                            </legend>
                                            <div class="fieldset-container-scroll">
                                                <ul class="list-group fk_filter" id="lstLideres">
                                                </ul>
                                            </div>

                                        </fieldset>
                                    </div>
                                </div>
                                <!-- fin columna 1 --->

                                <!-- columna 2 --->
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <fieldset class="fieldset fieldset-auto-width">
                                            <legend class="fieldset">
                                                <a id="lnkAyudaCliente" class="underline control-label">Cliente</a>
                                            </legend>
                                            <div class="fieldset-container-scroll">
                                                <ul class="list-group fk_filter" id="lstClientes">
                                                </ul>
                                            </div>

                                        </fieldset>
                                    </div>
                                </div>
                                <!-- fin columna 2 --->


                                <!-- columna 3 --->
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <fieldset class="fieldset fieldset-auto-width">
                                            <legend class="fieldset">
                                                <a id="lnkAyudaAccion" class="underline control-label">Acción</a>
                                            </legend>
                                            <div class="fieldset-container-scroll">
                                                <ul class="list-group fk_filter" id="lstAcciones">
                                                </ul>
                                            </div>

                                        </fieldset>
                                    </div>
                                </div>
                                <!-- fin columna 3 --->

                            </div>
                            <!-- FIN FILA 3 -->

                            <!-- FILA 4 -->
                            <div class="row">

                                <!-- columna 1 --->
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <fieldset class="fieldset fieldset-auto-width">
                                            <legend class="fieldset">
                                                <a id="lnkAyudaUnidad" class="underline control-label">Unidad</a>
                                            </legend>
                                            <div class="fieldset-container-scroll">
                                                <ul class="list-group fk_filter" id="lstUnidades">
                                                </ul>
                                            </div>

                                        </fieldset>
                                    </div>
                                </div>
                                <!-- fin columna 1 --->


                                <!-- columna 2 --->
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <fieldset class="fieldset fieldset-auto-width">
                                            <legend class="fieldset">
                                                <a id="lnkAyudaArea" class="underline control-label">Área</a>
                                            </legend>
                                            <div class="fieldset-container-scroll">
                                                <ul class="list-group fk_filter" id="lstAreas">
                                                </ul>
                                            </div>

                                        </fieldset>
                                    </div>
                                </div>
                                <!-- fin columna 2 --->

                                <!-- columna 3 --->
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <fieldset class="fieldset fieldset-auto-width">
                                            <legend class="fieldset">
                                                <a id="lnkAyudaSubarea" class="underline control-label">Subárea</a>
                                            </legend>
                                            <div class="fieldset-container-scroll">
                                                <ul class="list-group fk_filter" id="lstSubareas">
                                                </ul>
                                            </div>

                                        </fieldset>
                                    </div>
                                </div>
                                <!-- fin columna 3 --->


                            </div>
                            <!-- FIN FILA 4 -->

                            <!-- consultar -->
                            <div class="row">
                                <div class="col-xs-12">
                                    <button type="button" id="btnConsultar" class="btn btn-primary bottom pull-right">Obtener</button>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>

                <div class="clearfix"></div>


                <div class="ibox-content  blockquote blockquote-info">
                    <div class="table-responsive" style="overflow-y: hidden">
                        <table id="tblAcciones" class="table table-bordered table-condensed" >
                            <thead class="well text-primary">
                                <tr>
                                    <th data-type="Double" class="text-center">Ref.</th>
                                    <th data-type="String" class="text-center">Acción preventa</th>
                                    <th data-type="String" title="Oportunidad, extensión u objetivo" class="text-center">ON/Ext/Obj</th>
                                    <th data-type="String" class="text-center">Importe</th>
                                    <th data-type="String" class="text-center">Cuenta</th>
                                    <th data-type="String" class="text-center">Unidad</th>
                                    <th data-type="String" class="text-center">Área</th>
                                    <th data-type="String" class="text-center">Subárea</th>
                                    <th data-type="String" class="text-center">Líder</th>
                                    <th data-type="String" class="text-center">Solicitante</th>
                                    <th data-type="String" class="text-center">Estado</th>                                                                                                            
                                    <th data-type="DateTime" class="text-center">Creación</th>
                                    <th data-type="DateTime" class="text-center">Fin requerido</th>
                                    <th data-type="DateTime" class="text-center">Fin./Anul.</th>
                                </tr>
                            </thead>
                        </table>

                    </div>

                    <div class="clearfix"></div>

                </div>
            </div>
        </div>
    </form>

    <div id="divAyudaItemOrigen"></div>
    <div id="divAyudaPromotor"></div>
    <div id="divAyudaComercial"></div>
    <div id="divAyudaLideres"></div>
    <div id="divAyudaClientes"></div>
    <div id="divAyudaAcciones"></div>
    <div id="divAyudaUnidades"></div>
    <div id="divAyudaAreas"></div>
    <div id="divAyudaSubareas"></div>

    <script src="../../../../scripts/jquery-ui.min.js"></script>
    <script src="../../../../scripts/datepicker_es.js"></script>
    <script src="../../../../plugins/datatables/jquery.dataTables.min.js"></script>
    <script src="../../../../plugins/datatables/dataTables.fixedColumns.min.js"></script>
    <script src="../../../../plugins/datatables/Buttons/dataTables.buttons.min.js"></script>
    <script src="../../Scripts/Exportaciones.js" type="text/javascript" ></script>
    
    <script src="../../../../scripts/moment.locale.min.js"></script>
    <script src="../../../../scripts/accounting.min.js"></script>
    <script src="../../../../scripts/autoNumeric.js"></script>

    <script src="../../../../plugins/IB/ayuda.js"></script>
    <script src="../../../../plugins/IB/ayudamulti.js"></script>
    <script src="../../../../plugins/IB/buscaprof.js"></script>
    <script src="../../../../plugins/IB/buscaprofmulti.js"></script>
    <script src="../../plugins/IB/ayudasubareasficepi.js"></script>

    <script src="js/view.js?v=20180315"></script>
    <script src="js/app.js?v=20180315"></script>


</body>
</html>