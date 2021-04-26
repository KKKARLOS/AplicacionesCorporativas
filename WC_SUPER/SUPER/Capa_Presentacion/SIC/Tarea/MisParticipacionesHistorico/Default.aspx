<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_SIC_Tarea_MisParticipacionesHistorico_Default" %>

<%@ Register Src="~/Capa_Presentacion/bsUserControls/Head.ascx" TagPrefix="uc1" TagName="Head" %>
<%@ Register Src="~/Capa_Presentacion/bsUserControls/Menu/Menu.ascx" TagPrefix="uc1" TagName="Menu" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <uc1:Head runat="server" ID="Head" />
    <title>Mis participaciones</title>
    <link href="../../../../css/jquery-ui.min.css" rel="stylesheet" />
    <link href="../../../../plugins/datatables/jquery.dataTables.min.css" rel="stylesheet" />   
</head>
<body>
    <uc1:Menu runat="server" ID="Menu" />

    <form class="form-horizontal" id="frmAcciones" runat="server">

        <div class="container-fluid">

            <div class="row">

                <div class="ibox-title">
                    <h5 class="text-primary inline">Mis participaciones en tareas</h5>
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
                                        <label class="col-sm-2 control-label" for="cmbEstado">Estado tarea</label>
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

                                    <div class="form-group">
                                        <label class="col-sm-2 control-label" for="cmbOrigen">Origen</label>
                                        <div class="col-sm-6">
                                            <select id="cmbOrigen" class="form-control input-sm  fk_filter">
                                                <option value="">Selecciona...</option>
                                                <option value="O">Oportunidad</option>
                                                <option value="E">Extensión</option>
                                                <option value="P">Objetivo</option>
                                                <option value="S">SUPER</option>
                                            </select>
                                        </div>
                                    </div>



                                </div>
                                <!-- fin columna 1 --->

                                <!-- columna 2 --->
                                <div class="col-sm-6">
                                 
                                    <div class="form-group">
                                        <label class="col-sm-4 control-label text-right" for="txtFFinDesde">Fin previsto desde</label>
                                        <div class="col-sm-3">
                                            <input type="text" id="txtFFinDesde" class="form-control text-center fk_filter" />
                                        </div>
                                        <label class="col-sm-2 control-label text-right" for="txtFFinHasta">Hasta</label>
                                        <div class="col-sm-3">
                                            <input type="text" id="txtFFinHasta" class="form-control text-center fk_filter" />
                                        </div>
                                    </div>

                                </div>

                                <div class="col-sm-4 col-xs-offset-1">
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label text-right" for="cmbEstadoParticipacion">Estado participación</label>
                                        <div class="col-sm-4">
                                            <select id="cmbEstadoParticipacion" class="form-control input-sm fk_filter">
                                                <option value="">Selecciona...</option>
                                                <option value="A">En curso</option>
                                                <option value="F">Finalizada</option>
                                                <option value="X">Anulada</option>
                                                <option value="C">Cerrada</option>
                                            </select>
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
                                            <a id="lnkAyudaItemOrigen" class="underline control-label">ID Origen</a>
                                        </div>
                                        <div class="col-sm-10 row">
                                            <div class="input-group">
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

                            </div>
                            <!-- FIN FILA 3 -->

                            <!-- FILA 3 -->
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <div class="col-sm-2">
                                            <a id="lnkAyudaPromotor" class="underline control-label">Promotor</a>
                                        </div>
                                        <div class="col-sm-10 row">
                                            <div class="input-group">
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

                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <div class="col-sm-2 text-right">
                                            <a id="lnkAyudaComercial" class="underline control-label ">Comercial</a>
                                        </div>
                                        <div class="col-sm-10">
                                            <div class="input-group">
                                                <input type="text" id="txtComercial" class="form-control  fk_filter" readonly="readonly" />
                                                <span class="input-group-btn">
                                                    <button class="btn btn-default btn-inline-input" type="button" id="btnClearComercial">
                                                        <i class="fa fa-trash-o" aria-hidden="true"></i>
                                                    </button>
                                                </span>
                                            </div>

                                        </div>
                                    </div>
                                </div>

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
                            <!-- FIN FILA 4 -->

                            <!-- FILA 5 -->
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
                            <!-- FIN FILA 5 -->

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
                        <table id="tblTareas" class="table table-bordered table-condensed" >
                            <thead class="well text-primary cabeceraTabla">
                                <tr>
                                    <th class="text-center">Ref.</th>
                                    <th class="text-center">Tarea</th>
                                    <th class="text-center">Estado tarea</th>
                                    <th class="text-center">Acción preventa</th>
                                    <th title="Oportunidad, extensión, objetivo o solicitud preventa interna para la que se ha solicitado la acción preventa"  class="text-center">Origen</th>                                    
                                    <th class="text-center">Cuenta</th>                                    
                                    <th class="text-center>">Solicitante de la acción</th>
                                    <th class="text-center">Área</th>
                                    <th class="text-center">Subárea</th>
                                    <th class="text-center">Líder</th>                                                                        
                                    <th class="text-center">Creación</th>
                                    <th class="text-center">Fin previsto</th>
                                    <th class="text-center">Fin./Anul.</th>                                                                                                                                                                                    
                                    <%--<th class="text-center">Unidad</th>--%>                                                                        
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
    <script src="../../../../scripts/moment.locale.min.js"></script>
    <script src="../../../../scripts/accounting.min.js"></script>
    <script src="../../../../scripts/autoNumeric.js"></script>

    <script src="../../../../plugins/IB/ayuda.js"></script>
    <script src="../../../../plugins/IB/ayudamulti.js"></script>
    <script src="../../../../plugins/IB/buscaprof.js"></script>
    <script src="../../../../plugins/IB/buscaprofmulti.js"></script>
    <script src="../../plugins/IB/ayudasubareasficepi.js"></script>

    <script src="js/view.js?v=20180404"></script>
    <script src="js/app.js"></script>


</body>
</html>
