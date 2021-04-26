<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_SIC_Solicitud_CatalogoSUPER_Default" %>

<%@ Register Src="~/Capa_Presentacion/bsUserControls/Head.ascx" TagPrefix="uc1" TagName="Head" %>
<%@ Register Src="~/Capa_Presentacion/bsUserControls/Menu/Menu.ascx" TagPrefix="uc1" TagName="Menu" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <uc1:Head runat="server" ID="Head" />
    <title>Catálogo de solicitudes preventa</title>
    <link href="../../../../css/jquery-ui.min.css" rel="stylesheet" />
    <link href="../../../../plugins/datatables/jquery.dataTables.min.css" rel="stylesheet" />

</head>
<body>
    <uc1:Menu runat="server" ID="Menu" />

    <form class="form-horizontal" id="frmAcciones" runat="server">

        <div class="container-fluid">

            <div class="row">

                <div class="ibox-title">
                    <h5 class="text-primary inline">Catálogo de solicitudes preventa</h5>
                    <h5 class="pull-right"><a class="underline" href ="../../Guias/H_Crear_solicitudes_y_acciones.pdf" target="_blank">Guía</a></h5>
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
                                        <label class="col-sm-2 control-label" for="cmbEstado">Estado solicitud</label>
                                        <div class="col-sm-4">
                                            <select id="cmbEstadoSolicitud" class="form-control input-sm fk_filter">
                                                <option value="">Selecciona...</option>
                                                <option value="A" selected="selected">Abierta</option>
                                                <option value="F">Finalizada</option>
                                                <option value="X">Anulada</option>
                                                <option value="C">Cerrada</option>
                                            </select>
                                        </div>
                                    </div>

                                     <div class="form-group">
                                        <label class="col-sm-2 control-label" for="cmbEstado">Estado acción</label>
                                        <div class="col-sm-4">
                                            <select id="cmbEstadoAccion" class="form-control input-sm fk_filter">
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
                                        <div class="col-sm-4">
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

                                    <div id="divImportes" class="form-group" style="visibility:hidden">
                                        <label id="lblImporteDesde" class="col-sm-4 control-label text-right" for="txtImporteDesde"></label>
                                        <div class="col-sm-3">
                                            <input type="text" id="txtImporteDesde" class="form-control text-right  fk_filter" />
                                        </div>
                                        <label class="col-sm-2 control-label text-right" for="txtImporteHasta">Hasta (EUR)</label>
                                        <div class="col-sm-3">
                                            <input type="text" id="txtImporteHasta" class="form-control text-right fk_filter " />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-4 control-label text-right" for="txtFFinDesde">Fin requerido de acciones desde</label>
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
                                            <a id="lnkAyudaItemOrigen" class="underline control-label"></a>
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
                                            <a id="lnkAyudaPromotor" class="underline control-label"></a>
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
                                    <div  class="form-group">
                                        <div class="col-sm-3 text-right">
                                            <a id="lnkAyudaComercial" class="underline control-label ">Solicitante de acciones</a>
                                        </div>
                                        <div class="col-sm-9">
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
                                                <a id="lnkAyudaAccion" class="underline control-label">Tipo de acción</a>
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
                    <div class="row">
                        <div id="divBotones" class="col-xs-12">                            
                            <button type="button" id="btnAddSolicitud" class="btn btn-primary bottom">Añadir solicitud</button>
                            <button type="button" id="btnAddAccion" class="btn btn-primary bottom ">Añadir acción para la solicitud seleccionada</button>
                            <button type="button" id="btnDelSolicitud" class="btn btn-primary bottom hide">Eliminar solicitud</button>
                        </div>
                    </div>
                    <div class="table-responsive" style="overflow-y: hidden">
                        <table id="tblSolicitudes" class="table table-bordered table-condensed">
                            <thead class="well text-primary">
                                <tr>
                                    <th class="text-center">Ref.</th>
                                    <th class="text-center">Solicitud preventa &nbsp;&nbsp;&nbsp;(Total acciones/Acciones abiertas)</th>
                                    <th class="text-center">Área</th>                                    
                                    <th class="text-center">Promotor</th>
                                    <th class="text-center">Cuenta</th>
                                    <th class="text-center">Estado</th>
                                    <th class="text-center">Creación</th>                                    
                                </tr>
                            </thead>
                        </table>

                    </div>

                    

                    <div class="clearfix"></div>

                </div>


                <!-- Tabla de acciones de una solicitud-->
                <div class="ibox-content  blockquote blockquote-info">
                      <div class="table-responsive" style="overflow-y: hidden">
                        <table id="tblAcciones" class="table table-bordered table-condensed" >
                            <thead class="well text-primary">
                                <tr>
                                    <th  class="text-center">Ref.</th>
                                    <th  class="text-center">Acción preventa &nbsp;&nbsp;&nbsp;TT/TA</th>  
                                    <th  class="text-center">Subárea</th>
                                    <th  class="text-center">Importe</th>                                    
                                    <th  class="text-center">Solicitante</th>                                    
                                    <th  class="text-center">Org. Comercial</th>                                    
                                    <th  class="text-center">Líder</th>
                                    <th  class="text-center">Estado</th>                                                                                                            
                                    <th  class="text-center">Creación</th>
                                    <th  class="text-center">Fin requerido</th>
                                    <th  class="text-center">Fin./Anul.</th>
                                </tr>
                            </thead>
                        </table>

                    </div>
                    </div>







            </div>
        </div>

        <!-- modal alta/edicion solicitud -->
        <div class="modal fade" id="modal_solicitud"  data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header btn-primary">
                        <h4 class="modal-title">Alta de solicitud</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-group">
                                    <div class="col-sm-2">
                                        <label class="control-label" for="txtDenominacion_AS">Denominación<span class="text-danger">&nbsp;*</span></label>
                                    </div>
                                    <div class="col-sm-10">
                                        <input type="text" id="txtDenominacion_AS" class="form-control" required="required" />
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="row" id="row_estructura_AS">
                            <div class="col-sm-12">
                                <div class="form-group">
                                    <fieldset class="fieldset fieldset-auto-width">
                                        <legend class="fieldset"><span class="text-nowrap">Estructura</span></legend>
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label" for="cmbUnidad_AS">Unidad<span class="text-danger">&nbsp;*</span></label>
                                            <div class="col-sm-8">
                                                <select id="cmbUnidad_AS" class="form-control input-sm" required></select>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label" for="cmbArea_AS">Área<span class="text-danger">&nbsp;*</span></label>
                                            <div class="col-sm-8">
                                                <select id="cmbArea_AS" class="form-control input-sm" required></select>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <b>
                            <button type="button" id="btnFinalizar" class="btn btn-primary bottom pull-left hide">Finalizar solicitud</button>
                            <button type="button" id="btnAnular" class="btn btn-primary bottom pull-left hide">Anular solicitud</button>
                            <button type="button" id="btnGrabar_AS" class="btn btn-primary">Grabar</button>
                            <button type="button" id="btnCancelar_AS" data-dismiss="modal" class="btn btn-default">Cancelar</button>

                        </b>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>



        <div class="modal fade" id="modal-finAnul"  data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 id="h4Title" class="modal-title"></h4>
                    </div>
                    <div class="modal-body">
                        <span id="spanFinAnul"></span><hr />
                        <div id="divMotivo" class="hide">
                            <label>Motivo de anulación</label><span class="text-danger">&nbsp;*</span><br />
                            <textarea maxlength="250" id="txtMotivo" style="width:90%" rows="4"></textarea><br />
                            <span id="numCaracteres">250 caracteres disponibles</span>
                        </div>                        
                    </div>
                    <div class="modal-footer">
                        <button id="btnFinAnul" type="button" class="btn btn-primary">Aceptar</button>
                        <button id="btnCancelFinAnul" type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>                        
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="modal-finAnul_conf"  data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title"></h4>
                    </div>
                    <div class="modal-body">
                        
                    </div>
                    <div class="modal-footer">
                        <button id="btnAceptarFinAnul" type="button" class="btn btn-primary">Aceptar</button>
                        <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>                        
                    </div>
                </div>
            </div>
        </div>


        <div class="modal fade" id="modal-EliminarSolicitud"  data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title">Eliminación de solicitud</h4>
                    </div>
                    <div class="modal-body">
                        Esta acción eliminará la solicitud. Si estás conforme pulsa 'Aceptar', en caso contrario pulsa 'Cancelar';
                    </div>
                    <div class="modal-footer">
                        <button id="btnEliminarSolicitud" type="button" class="btn btn-primary">Aceptar</button>
                        <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>                        
                    </div>
                </div>
            </div>
        </div>


    </form>

    <div class="modal fade" id="modal-motivoAnulacionSolicitud">
	<div class="modal-dialog">
		<div class="modal-content">
			<div class="modal-header">
				<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
				<h4 class="modal-title">Motivo de anulación de solicitud</h4>
			</div>
			<div class="modal-body">
				<textarea id="txtMotivoAnulacionSolicitud" disabled="disabled" style="width:100%" rows="5"></textarea>
			</div>
			<div class="modal-footer">								
                <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
			</div>
		</div>
	</div>
</div>

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

    <script src="js/view.js?v=20180315"></script>
    <script src="js/app.js?v=20180315"></script>


</body>
</html>
