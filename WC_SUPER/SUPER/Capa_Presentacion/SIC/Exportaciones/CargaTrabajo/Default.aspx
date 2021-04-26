<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_SIC_Exportaciones_CargaTrabajo_Default" %>

<%@ Register Src="~/Capa_Presentacion/bsUserControls/Head.ascx" TagPrefix="uc1" TagName="Head" %>
<%@ Register Src="~/Capa_Presentacion/bsUserControls/Menu/Menu.ascx" TagPrefix="uc1" TagName="Menu" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <uc1:Head runat="server" ID="Head" />
    <title>Exportación de carga de trabajo</title>
    <link href="../../../../css/jquery-ui.min.css" rel="stylesheet" />

</head>
<body>
    <uc1:Menu runat="server" ID="Menu" />

    <form class="form-horizontal" id="frmAcciones" runat="server">
        <div class="container-fluid">

            <div class="row">

                <div class="ibox-title">
                    <h5 class="text-primary inline">Exportación de carga de trabajo</h5>
                </div>

                <div class="panel-group">
                    <div class="panel panel-default">

                        <div class="panel-heading">
                            <a class="underline" id="lnkLimpiarFiltros">Resetear filtros</a>
                        </div>

                        <div id="divFiltros" class="panel-body panel-collapse collapse in">
                            <!-- FILA 1 -->
                            <div class="row">
                                <fieldset class="fieldset fieldset-auto-width">
                                    <legend class="fieldset">Acción
                                    </legend>

                                    <!-- columna 1 --->
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label" for="cmbEstado">Estado</label>
                                            <div class="col-sm-6">
                                                <select id="cmbEstado" class="form-control input-sm fk_filter">
                                                    <option value="">Selecciona...</option>
                                                    <option value="A">Abierta</option>
                                                    <option value="F">Finalizada</option>
                                                    <option value="X">Anulada</option>
                                                    <option value="C">Cerrada</option>
                                                </select>
                                            </div>
                                        </div>



                                    </div>
                                    <!-- fin columna 1 --->

                                    <!-- columna 2 --->
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label text-right" for="txtFFinDesde">F.Fin estipulada desde</label>
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
                                </fieldset>

                            </div>
                            <!-- FIN FILA 1 -->

                            <br />

                            <!-- FILA 2 -->
                            <div class="row">
                                <fieldset class="fieldset fieldset-auto-width">
                                    <legend class="fieldset">Tarea
                                    </legend>

                                    <!-- columna 1 --->
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label" for="cmbEstado_tarea">Estado</label>
                                            <div class="col-sm-6">
                                                <select id="cmbEstado_tarea" class="form-control input-sm fk_filter">
                                                    <option value="">Selecciona...</option>
                                                    <option value="A">Abierta</option>
                                                    <option value="F">Finalizada</option>
                                                    <option value="X">Anulada</option>
                                                    <option value="C">Cerrada</option>
                                                </select>
                                            </div>
                                        </div>



                                    </div>
                                    <!-- fin columna 1 --->

                                    <!-- columna 2 --->
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label text-right" for="txtFFinDesde_tarea">F.Fin prevista desde</label>
                                            <div class="col-sm-3">
                                                <input type="text" id="txtFFinDesde_tarea" class="form-control text-center fk_filter" />
                                            </div>
                                            <label class="col-sm-2 control-label text-right" for="txtFFinHasta_tarea">Hasta</label>
                                            <div class="col-sm-3">
                                                <input type="text" id="txtFFinHasta_tarea" class="form-control text-center fk_filter" />
                                            </div>
                                        </div>
                                    </div>

                                    <!-- fin columna 2 --->
                                </fieldset>

                            </div>
                            <!-- FIN FILA 2 -->

                            <br />

                            <!-- FILA 3 -->
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
                            <!-- FIN FILA 3 -->

                            <!-- FILA 4 -->
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

                            </div>
                            <!-- FIN FILA 4 -->

                            <!-- consultar -->
                            <div class="row">
                                <div class="col-xs-12">
                                    <button type="button" id="btnExportar" class="btn btn-primary bottom pull-right">Exportar</button>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>

                <div class="clearfix"></div>

            </div>
        </div>

        <iframe id="iframeExportar" width="0" height="0"></iframe>
    </form>

    <div id="divAyudaLideres"></div>
    <div id="divAyudaUnidades"></div>
    <div id="divAyudaAreas"></div>
    <div id="divAyudaSubareas"></div>

    <script src="../../../../scripts/jquery-ui.min.js"></script>
    <script src="../../../../scripts/datepicker_es.js"></script>
    <script src="../../../../plugins/IB/ayudamulti.js"></script>
    <script src="../../../../plugins/IB/buscaprofmulti.js"></script>
    <script src="../../plugins/IB/ayudasubareasficepi.js"></script>

    <script src="js/view.js"></script>
    <script src="js/app.js"></script>


</body>
</html>
