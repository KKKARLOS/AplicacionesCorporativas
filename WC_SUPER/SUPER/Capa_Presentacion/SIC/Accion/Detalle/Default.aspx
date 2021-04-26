<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_SIC_Accion_Detalle_Default" %>

<%@ Register Src="~/Capa_Presentacion/bsUserControls/Head.ascx" TagPrefix="uc1" TagName="Head" %>
<%@ Register Src="~/Capa_Presentacion/bsUserControls/Menu/Menu.ascx" TagPrefix="uc1" TagName="Menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <uc1:Head runat="server" ID="Head" />
    <title>Acción preventa</title>
    <link href="../../../../css/jquery-ui.min.css" rel="stylesheet" />
</head>
<body runat="server" id="body">
    <uc1:Menu runat="server" ID="Menu" />

    <form class="form-horizontal" id="frmAcciones" runat="server">

        <div class="container">
            <div class="row">

                <div class="ibox-content blockquote blockquote-info">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group">
                                <label class="col-sm-1 control-label" for="txtIditemorigen_cab" id="lblItemorigen_cab" runat="server">Oportunidad</label>
                                <div class="col-sm-1">
                                    <input type="text" id="txtIditemorigen_cab" runat="server" readonly="readonly" class="form-control text-right" style="width: 90px;" />
                                </div>
                                <div class="col-sm-10">
                                    <input type="text" id="txtDenominacion_cab" readonly="readonly" class="form-control" value="" />
                                </div>
                            </div>
                        </div>

                        <div class="col-xs-12">
                            <div id="divOportExt" class="form-group hide" runat="server">
                                <label class="col-sm-1 control-label" for="txtlblOportExt_cab" id="lblOportExt" runat="server">Oportunidad</label>
                                <div class="col-sm-1">
                                    <input type="text" id="txtOportExt" runat="server" readonly="readonly" class="form-control text-right" style="width: 90px;" />
                                </div>
                                <div class="col-sm-10">
                                    <input type="text" id="txtdenOportExt_cab" readonly="readonly" class="form-control" value="" />
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-12">
                            <div id="div_txtCuenta_cab" class="form-group" runat="server">
                                <label class="col-sm-1 control-label" for="txtCuenta_cab">Cuenta</label>
                                <div class="col-sm-11">
                                    <input type="text" id="txtCuenta_cab" readonly="readonly" class="form-control" value="" />
                                </div>

                            </div>
                        </div>


                        <div class="col-sm-7">
                            <div class="form-group MB0">
                                <a id="linkInformacionAdicional" runat="server" href="#" data-toggle="collapse" data-target="#divInformacionAdicional"><i class="fa fa-plus"></i>
                                    <span>Información</span></a>
                            </div>
                        </div>

                        <div class="clearfix"></div>

                        <div id="divInformacionAdicional" class="collapse">
                            <div>
                                <div id="div_container_cab_OE" runat="server">
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label" for="txtComercial_cab">Comercial</label>
                                            <div class="col-sm-5">
                                                <input type="text" id="txtComercial_cab" readonly="readonly" class="form-control " value="" />
                                            </div>

                                            <label class="col-sm-3 control-label text-right" for="txtEstado_cab">Estado</label>
                                            <div class="col-sm-2">
                                                <input type="text" id="txtEstado_cab" readonly="readonly" class="form-control " value="" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label" for="txtOrganizacionComercial_cab">Organización comercial</label>
                                            <div class="col-sm-5">
                                                <input type="text" id="txtOrganizacionComercial_cab" readonly="readonly" class="form-control " value="" />
                                            </div>

                                            <label class="col-sm-3 control-label text-right" for="txtFechaCierre_cab">Fecha de cierre</label>
                                            <div class="col-sm-2">
                                                <input type="text" id="txtFechaCierre_cab" readonly="readonly" class="form-control text-center" value="" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label" for="txtCentroResponsabilidad_cab">Centro de responsabilidad</label>
                                            <div class="col-sm-5">
                                                <input type="text" id="txtCentroResponsabilidad_cab" readonly="readonly" class="form-control " value="" />
                                            </div>

                                            <label class="col-sm-3 control-label text-right" for="txtFechaCierre_cab">Límite presentación</label>
                                            <div class="col-sm-2">
                                                <input type="text" id="txtFechaLimitePresentacion_cab" readonly="readonly" class="form-control text-center" value="" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label" for="txtGestorProduccion_cab">Gestor de producción</label>
                                            <div class="col-sm-5">
                                                <input type="text" id="txtGestorProduccion_cab" readonly="readonly" class="form-control " value="" />
                                            </div>
                                            <label class="col-sm-3 control-label text-right" for="txtProbabilidadExisto_cab">Probabilidad éxito</label>
                                            <div class="col-sm-2">
                                                <input type="text" id="txtProbabilidadExisto_cab" readonly="readonly" class="form-control text-right" value="" />
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label" for="txtEtapaVentas_cab">Etapa de ventas</label>
                                            <div class="col-sm-5">
                                                <input type="text" id="txtEtapaVentas_cab" readonly="readonly" class="form-control " value="" />
                                            </div>
                                            <label class="col-sm-3 text-right control-label" for="txtImporte_cab">Importe</label>
                                            <div class="col-sm-2">
                                                <input type="text" id="txtImporte_cab" readonly="readonly" class="form-control text-right" value="" />
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label" for="txtAreaConTecno_cab">Área conocimiento tecno.</label>
                                            <div class="col-sm-5">
                                                <input type="text" id="txtAreaConTecno_cab" readonly="readonly" class="form-control " value="" />
                                            </div>
                                            <label class="col-sm-3 control-label text-right" for="txtRentabilidad_cab">Rentabilidad</label>
                                            <div class="col-sm-2">
                                                <input type="text" id="txtRentabilidad_cab" readonly="readonly" class="form-control text-right" value="" />
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label" for="txtAreaConSectorial_cab">Área conocimiento sectorial</label>
                                            <div class="col-sm-5">
                                                <input type="text" id="txtAreaConSectorial_cab" readonly="readonly" class="form-control " value="" />
                                            </div>

                                            <label class="col-sm-3 text-right control-label" for="txtDuracionProyecto_cab">Duración del proyecto</label>
                                            <div class="col-sm-2">
                                                <input type="text" id="txtDuracionProyecto_cab" readonly="readonly" class="form-control " value="" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div id="div_container_cab_P" runat="server">
                                <div class="form-group">

                                    <label class="col-sm-2 control-label" for="txtComercial_cab">Comercial</label>
                                    <div class="col-sm-5">
                                        <input type="text" id="txtComercial_cabObjetivo" readonly="readonly" class="form-control " value="" />
                                    </div>

                                    <label class="col-sm-2 control-label text-right" for="txtEstado_cab">Estado</label>
                                    <div class="col-sm-2">
                                        <input type="text" id="txtEstado_cabObj" readonly="readonly" class="form-control " value="" />
                                    </div>

                                </div>

                                <div class="form-group">

                                    <label class="col-sm-2 control-label" for="txtOferta_cab">Oferta</label>
                                    <div class="col-sm-5">
                                        <input type="text" id="txtOferta_cab" readonly="readonly" class="form-control " value="" />
                                    </div>


                                    <label class="col-sm-2 control-label text-right" for="txtFechaInicio_cab">Fecha Inicio</label>
                                    <div class="col-sm-2">
                                        <input type="text" id="txtFechaInicio_cab" readonly="readonly" class="form-control text-center" value="" />
                                    </div>

                                </div>


                                <div class="form-group">
                                    <label class="col-sm-2 control-label" for="txtContataPrevista_cab">Contratación prevista</label>
                                    <div class="col-sm-2">
                                        <input type="text" id="txtContataPrevista_cab" readonly="readonly" class="form-control text-right" value="" />
                                    </div>

                                    <label class="col-sm-2 col-sm-offset-3 control-label text-right" for="txtFechaFin_cab">Fecha fin prevista</label>
                                    <div class="col-sm-2">
                                        <input type="text" id="txtFechaFin_cab" readonly="readonly" class="form-control text-center" value="" />
                                    </div>
                                </div>

                                <div class="form-group">

                                    <label class="col-sm-2 control-label" for="txtFechaFin_cab">Org. Comercial</label>
                                    <div class="col-sm-9">
                                        <input type="text" id="txtOrgComercial_Objetivo" readonly="readonly" class="form-control" value="" />
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-sm-2 control-label tex-right" for="txtContataPrevista_cab">Tipo de negocio</label>
                                    <div class="col-sm-9">
                                        <input type="text" id="txtTipoNegocio" readonly="readonly" class="form-control" value="" />
                                    </div>
                                </div>


                                <div class="form-group">

                                    <label class="col-sm-2 control-label" for="txtFechaFin_cab">Descripción del objetivo</label>
                                    <div class="col-sm-9">
                                        <textarea rows="5" style="width: 100%" disabled="disabled" id="txtDescObjetivo"></textarea>
                                    </div>
                                </div>


                                <%-- <div class="form-group">
                                    <label class="col-sm-2 control-label" for="txtCoste_cab">Coste</label>
                                    <div class="col-sm-2">
                                        <input type="text" id="txtCoste_cab" readonly="readonly" class="form-control text-right" value="" />
                                    </div>

                                    <label class="col-sm-2 col-sm-offset-3 control-label text-right" for="txtResultado_cab">Resultado</label>
                                    <div class="col-sm-2">
                                        <input type="text" id="txtResultado_cab" readonly="readonly" class="form-control text-right" value="" />
                                    </div>
                                </div>--%>
                            </div>

                        </div>


                    </div>

                </div>
            </div>

            <div class="row" id="divAccionContainer">
                <div class="ibox-title">
                    <h5 id="txtAccionPreventa" class="text-primary">Detalle acción</h5>
                    <h5 class="pull-right"><a class="underline" href="../../Guias/K_Detalle_de_accion.pdf" target="_blank">Guía</a></h5>
                </div>

                <div class="ibox-content  blockquote blockquote-info">
                    <div class="row">
                        <div class="col-sm-7">
                            <div class="form-group">
                                <!--Sello estado-->
                                <label class="col-sm-4 hidden-xs control-label sello pull-right" id="estado"></label>
                                <label class="col-sm-2 control-label" for="cmbTipoAccion">Acción<span class="text-danger">&nbsp;*</span></label>
                                <div class="col-sm-9">
                                    <select id="cmbTipoAccion" class="form-control input-sm required " disabled="disabled" required="required"></select>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-sm-2">
                                    <a id="btnAyudaLider" href="#" class="underline control-label">Líder</a><span id="asteriscoLider" class="text-danger hide">&nbsp;*</span>
                                </div>

                                <div class="col-sm-9">
                                    <input type="text" id="txtLider" class="form-control " readonly="readonly" />
                                    <div><a id="btnOtrosLideres" href="#" class="underline control-label">Otros líderes de la solicitud</a></div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label" for="txtDenominacion">Descripción<span class="text-danger">&nbsp;*</span></label>
                                <div class="col-sm-9">
                                    <textarea class="form-control" rows="3" id="txtDenominacion" required="required" disabled="disabled"></textarea>

                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label" for="txtObservaciones">Observaciones</label>
                                <div class="col-sm-9">
                                    <textarea class="form-control" rows="3" id="txtObservaciones" disabled="disabled"></textarea>
                                </div>

                            </div>
                        </div>
                        <div class="col-sm-5">
                            <div class="form-group">
                                <fieldset class="fieldset fieldset-auto-width">
                                    <legend class="fieldset"><span class="text-nowrap">Estructura</span></legend>

                                    <!-- Maquetación de formularios-->
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label" for="cmbUnidad">Unidad<span class="text-danger">&nbsp;*</span></label>
                                        <div class="col-sm-9">
                                            <select id="cmbUnidad" class="form-control input-sm" required disabled="disabled"></select>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label" id="lblArea" for="cmbArea">Área<span class="text-danger">&nbsp;*</span></label>
                                        <div class="col-sm-9">
                                            <select id="cmbArea" class="form-control input-sm" required disabled="disabled"></select>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label" id="lblSubarea" for="cmbSubarea">Subárea<span class="text-danger">&nbsp;*</span></label>
                                        <div class="col-sm-9">
                                            <select id="cmbSubarea" class="form-control input-sm" required="required" disabled="disabled"></select>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-3 control-label" id="lblRespSubarea" for="txtRespSubarea">Resp. Subárea</label>
                                        <div class="col-sm-9">
                                            <span id="txtRespSubarea"></span>
                                        </div>
                                    </div>

                                </fieldset>
                            </div>

                            <div class="form-group">
                                <label class="col-sm-5 control-label" for="dteFC">Fecha creación</label>
                                <div class="col-sm-3 input-group date">
                                    <input type="text" class="form-control text-center " id="dteFC" disabled="disabled" />
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-sm-5 control-label" for="dteFFE">Fecha finalización requerida<span class="text-danger">&nbsp;*</span></label>
                                <div class="col-sm-3 input-group date">
                                    <input type="text" class="form-control text-center " id="dteFFE" required="required" disabled="disabled" />
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-sm-5 control-label" for="dteFFEM">Plazo mínimo requerido</label>
                                <div class="col-sm-5 control-label" id="dteFFEM">

                                </div>
                            </div>

                            <div class="form-group">
                                <label id="lbldteFFR" class="col-sm-5 control-label" for="dteFFR"></label>
                                <div class="col-sm-3 input-group date">
                                    <input type="text" class="form-control text-center " id="dteFFR" disabled="disabled" />
                                </div>
                            </div>

                            <div class="form-group col-sm-3 visible-xs">
                                <label class="col-sm-5 control-label" for="txtEstado">Estado</label>
                                <div class="col-sm-7">
                                    <input type="text" readonly="readonly" value="Abierta" class="form-control input-sm" id="estadomin" />
                                </div>
                            </div>

                            <div id="divTareasAsociadas">
                                <a id="btnTareas" href="#" class="underline">Tareas asociadas</a>
                                <span id="lblTareasCount" class="badge"></span>
                            </div>

                            <div id="divDocumentacion">
                                <a id="btnDocumentacion" href="#" class="underline">Documentación</a>
                                <span id="lblDocumentosCount" class="badge"></span>
                            </div>

                        </div>

                    </div>

                    <hr />

                    <div class="pull-left">
                        <button type="button" class="btn btn-primary" style="display: none;" id="btnFinalizarAccion">Finalizar acción</button>
                        <button type="button" class="btn btn-primary" style="display: none;" id="btnAnularAccion">Anular acción</button>
                        <button type="button" class="btn btn-primary" style="display: none;" id="btnReplicarAccion">Generar acción hermana</button>
                        <button type="button" class="btn btn-primary" style="display: none;" id="btnAutoAsignar">Autoasignarme líder</button>

                        <div id="divtextareaMotivoAnulacion" class="hide form-group">
                            <span>Motivo de anulación</span><span class="text-danger">*</span><br />
                            <textarea id="textareaMotivoAnulacion" class="W500 form-control" disabled="disabled"></textarea>
                        </div>
                    </div>
                    <div class="row pull-right">

                        <button type="button" class="btn btn-primary" style="display: none;" id="btnGrabar">Grabar</button>
                        <button type="button" class="btn btn-default" id="btnCerrar" style="margin-left: 7px">Cerrar</button>
                    </div>
                    <div class="clearfix"></div>

                </div>
            </div>
        </div>

        <div id="divAyudaLider"></div>

        <div class="modal fade" id="modal_anularAccion">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header btn-primary">
                        <h4 class="modal-title">Anular acción</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-sm-12">
                                <p>Has pulsado el botón 'Anular acción'. ¿Estás conforme?"</p>
                                <p>Para poder anular una acción, debes introducir el motivo de anulación.</p>
                                <label class="label-control">Motivo de anulación<span class="text-danger">&nbsp;*</span></label>
                                <textarea maxlength="250" rows="5" class="form-control" id="modal_txtMotivoAnulacion" required="required"></textarea>
                                <span id="numCaracteres_anularAccion">250 caracteres disponibles</span>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <b>
                            <button type="button" id="btnAceptar_anularAccion" class="btn btn-primary">Aceptar</button>
                            <button type="button" id="btnCancelar_anularAccion" data-dismiss="modal" class="btn btn-default">Cancelar</button>
                        </b>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>

        <div class="modal fade" id="modal_finalizarAccion">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header btn-primary">
                        <h4 class="modal-title">Finalizar acción</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-sm-12">
                                <p>Has pulsado el botón 'Finalizar acción'. Pulsa 'Aceptar' si estás conforme.</p>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <b>
                            <button type="button" id="btnAceptar_finalizarAccion" class="btn btn-primary">Aceptar</button>
                            <button type="button" id="btnCancelar_finalizarAccion" data-dismiss="modal" class="btn btn-default">Cancelar</button>
                        </b>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>

        <div class="modal fade" id="modal_replicarAccion">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header btn-primary">
                        <h4 class="modal-title">Generar acción hermana</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-group">
                                    <label class="col-sm-3 control-label" for="cmbTipoAccion_RA">Acción a solicitar<span class="text-danger">&nbsp;*</span></label>
                                    <div class="col-sm-8">
                                        <select id="cmbTipoAccion_RA" class="form-control input-sm required " required></select>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-sm-3 control-label" for="txtDenominacion_RA">Descripción<span class="text-danger">&nbsp;*</span></label>
                                    <div class="col-sm-8">
                                        <textarea class="form-control" rows="3" id="txtDenominacion_RA" required="required"></textarea>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-3 control-label" for="txtObservaciones_RA">Observaciones</label>
                                    <div class="col-sm-8">
                                        <textarea class="form-control" rows="8" id="txtObservaciones_RA"></textarea>
                                    </div>

                                </div>
                                <div class="form-group">
                                    <label class="col-sm-4 control-label" for="dteFFE_RA">Fecha finalización requerida<span class="text-danger">&nbsp;*</span></label>
                                    <div class="col-sm-3">
                                        <input type="text" class="form-control text-center" id="dteFFE_RA" required="required" />
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-sm-4 control-label" for="dteFFEM_RA">Fecha fin. requerida mínima</label>
                                    <div class="col-sm-5 control-label" id="dteFFEM_RA">
                                    </div>
                                </div>


                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <b>
                            <button type="button" id="btnAceptar_RA" class="btn btn-primary">Aceptar</button>
                            <button type="button" id="btnCancelar_RA" data-dismiss="modal" class="btn btn-default">Cancelar</button>
                        </b>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>

        <div class="modal fade" id="modal_otrosLideres">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header btn-primary">
                        <h4 class="modal-title">Otros líderes de la solicitud</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-sm-12">
                                Los líderes subrayados, son susceptibles de ser asignados a la acción, por pertenecer al pool de posibles líderes del subárea al que pertenece la acción. Para asignarlo automáticamente, pulsa sobre su nombre.<br />
                                Los que aparecen como no seleccionables no tienen figura de posible líder en el subárea de la acción actual.
                                <br />
                                <br />
                                <table class="table table-bordered table-condensed">
                                    <thead>
                                        <tr>
                                            <th>Acción</th>
                                            <th>Área</th>
                                            <th>Subárea</th>
                                            <th>Líder</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <b>
                            <button type="button" id="btnCancelar_modal_otrosLideres" data-dismiss="modal" class="btn btn-default">Cerrar</button>
                        </b>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>

    </form>

    <script src="../../../../scripts/jquery-ui.min.js"></script>
    <script src="../../../../scripts/datepicker_es.js"></script>
    <script src="../../Documentos/Documentos.js?v=20171128_02"></script>
    <%--<script src="../../../../plugins/IB/buscaprof.js?v=20180109_01"></script>--%>
    <script src="../../plugins/IB/buscalider.js?v=20180306_01"></script>
    <script src="../../../../scripts/moment.locale.min.js"></script>
    <script src="../../../../scripts/string.js"></script>
    <script src="../../../../scripts/array.js"></script>

    <script src="js/modelsAccion.js"></script>
    <script src="js/view.js?v=20180315"></script>
    <script src="js/app.js?v=20180405_01"></script>

</body>
</html>
