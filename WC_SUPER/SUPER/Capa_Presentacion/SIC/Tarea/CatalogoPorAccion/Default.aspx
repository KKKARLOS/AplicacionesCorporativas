<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_SIC_Tarea_CatalogoDeAccion_Default" %>

<%@ Register Src="~/Capa_Presentacion/bsUserControls/Head.ascx" TagPrefix="uc1" TagName="Head" %>
<%@ Register Src="~/Capa_Presentacion/bsUserControls/Menu/Menu.ascx" TagPrefix="uc1" TagName="Menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <uc1:Head runat="server" ID="Head" />
    <title>Catálogo de tareas</title>
    <link href="../../../../plugins/datatables/jquery.dataTables.min.css" rel="stylesheet" />
</head>
<body id="body" runat="server">
    <uc1:Menu runat="server" ID="Menu" />

    <!-- meter paddingtop al container -->
    <form id="form1" runat="server"></form>
    <form class="form-horizontal">
        <section>
            <div class="container-fluid">

                <div class="row">
                    <div class="ibox-content  blockquote blockquote-info">
                        <div class="row">
                            <div class="col-sm-4">
                                <div class="form-group MB0">
                                    <label class="col-sm-2 control-label" for="acc-cmbTipoAccion">Acción</label>
                                    <div class="col-sm-10">
                                        <input id="txtTipoAccion" runat="server" type="text" class="form-control" value="" disabled="disabled" />
                                    </div>

                                </div>

                            </div>

                            <div class="col-sm-3">
                                <div class="form-group MB0">
                                    <label class="col-sm-5 control-label text-right"  for="acc-cmbTipoAccion">F.Fin requerida</label>
                                    <div class="col-sm-5">
                                        <input id="txtFinRequerida" runat="server" type="text" class="form-control text-center" value="" disabled="disabled" />
                                    </div>

                                </div>

                            </div>


                            <div class="col-sm-5">
                                <div class="form-group MB0">
                                    <label class="col-sm-2 control-label text-right" for="acc-cmbTipoAccion">Líder</label>
                                    <div class="col-sm-7">
                                        <input id="txtLider" runat="server" type="text" class="form-control" value="" disabled="disabled" />
                                    </div>
                                </div>
                            </div>

                        </div>

                        <br />

                        <div>
                            <div class="form-group">
                                <label class="col-sm-1 control-label" style="width:102px" for="txtOportunidad" id="lblOportunidadSolic" runat="server">Oportunidad</label>
                                <div id="inputNumOportunidadAcciones" class="col-sm-1">
                                    <input type="text" id="acc-txtOportunidadSolic" readonly="readonly" class="form-control text-right" style="width: 90px" />
                                </div>
                                <div class="col-sm-9">
                                    <input type="text" id="acc-txtDenominacionSolic" readonly="readonly" style="width:101%" class="form-control" />
                                </div>
                            </div>


                            <div id="divCliente" class="form-group MB0" runat="server">
                                <label id="acc-lblClienteSolic" class="col-sm-1 control-label" style="width:102px" for="txtCliente">Cuenta</label>
                                <div class="col-sm-10">
                                    <input type="text" id="acc-txtClienteSolic" style="width:101%" readonly="readonly" class="form-control " value="" />
                                </div>

                            </div>

                            <br />

                            <div class="col-sm-5">
                                <div class="form-group MB0">
                                    <a id="linkInformacionAdicional" runat="server" href="#" data-toggle="collapse" data-target="#divInformacionAdicional"><i class="fa fa-plus"></i>
                                        <span>Información</span></a>
                                </div>
                            </div>

                            <div class="clearfix"></div>


                            <div id="divInformacionAdicional" class="collapse">
                                <div style="padding-top: 10px">
                                    <div id="div_container_cab_OE" runat="server">
                                        <div class="col-sm-12">
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label" for="txtComercial_cab">Solicitante</label>
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

                                                <label class="col-sm-3 control-label text-right" for="txtProbabilidadExisto_cab">Probabilidad éxito</label>
                                                <div class="col-sm-2">
                                                    <input type="text" id="txtProbabilidadExisto_cab" readonly="readonly" class="form-control text-right" value="" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-12">
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label" for="txtGestorProduccion_cab">Gestor de producción</label>
                                                <div class="col-sm-5">
                                                    <input type="text" id="txtGestorProduccion_cab" readonly="readonly" class="form-control " value="" />
                                                </div>

                                                <label class="col-sm-3 text-right control-label" for="txtImporte_cab">Importe</label>
                                                <div class="col-sm-2">
                                                    <input type="text" id="txtImporte_cab" readonly="readonly" class="form-control text-right" value="" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-12">
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label" for="txtEtapaVentas_cab">Etapa de ventas</label>
                                                <div class="col-sm-5">
                                                    <input type="text" id="txtEtapaVentas_cab" readonly="readonly" class="form-control " value="" />
                                                </div>

                                                <label class="col-sm-3 control-label text-right" for="txtRentabilidad_cab">Rentabilidad</label>
                                                <div class="col-sm-2">
                                                    <input type="text" id="txtRentabilidad_cab" readonly="readonly" class="form-control text-right" value="" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-12">
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label" for="txtAreaConTecno_cab">Área conocimiento tecno.</label>
                                                <div class="col-sm-5">
                                                    <input type="text" id="txtAreaConTecno_cab" readonly="readonly" class="form-control " value="" />
                                                </div>

                                                <label class="col-sm-3 text-right control-label" for="txtDuracionProyecto_cab">Duración del proyecto</label>
                                                <div class="col-sm-2">
                                                    <input type="text" id="txtDuracionProyecto_cab" readonly="readonly" class="form-control " value="" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-12">
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label" for="txtAreaConSectorial_cab">Área conocimiento sectorial</label>
                                                <div class="col-sm-5">
                                                    <input type="text" id="txtAreaConSectorial_cab" readonly="readonly" class="form-control " value="" />
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>

                                <div class="col-xs-12 row">
                                    <%--<div id="div_container_cab_P" runat="server">
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label" for="txtOferta_cab">Oferta</label>
                                            <div class="col-sm-6">
                                                <input type="text" id="txtOferta_cab" readonly="readonly" class="form-control " value="" />
                                            </div>

                                            <label class="col-sm-2 control-label text-right" for="txtFechaInicio_cab">Fecha Inicio</label>
                                            <div class="col-sm-2">
                                                <input type="text" id="txtFechaInicio_cab" readonly="readonly" class="form-control text-center" value="" />
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-2 control-label" for="txtContataPrevista_cab">Contratación prevista</label>
                                            <div class="col-sm-3">
                                                <input type="text" id="txtContataPrevista_cab" readonly="readonly" class="form-control text-right" value="" />
                                            </div>

                                            <label class="col-sm-2 col-sm-offset-3 control-label text-right" for="txtFechaFin_cab">Fecha Fin</label>
                                            <div class="col-sm-2">
                                                <input type="text" id="txtFechaFin_cab" readonly="readonly" class="form-control text-center" value="" />
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-2 control-label" for="txtCoste_cab">Coste</label>
                                            <div class="col-sm-3">
                                                <input type="text" id="txtCoste_cab" readonly="readonly" class="form-control text-right" value="" />
                                            </div>

                                            <label class="col-sm-2 col-sm-offset-3 control-label text-right" for="txtResultado_cab">Resultado</label>
                                            <div class="col-sm-2">
                                                <input type="text" id="txtResultado_cab" readonly="readonly" class="form-control text-right" value="" />
                                            </div>
                                        </div>

                                    </div>--%>



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

                                    </div>





















                                </div>
                            </div>

                            <div class="clearfix"></div>
                        </div>




                    </div>
                </div>


                <div class="row">
                    <div class="ibox-title">
                        <h5 class="text-primary">Tareas asociadas a la acción</h5>
                    </div>
                    <div class="ibox-content  blockquote blockquote-info">
                        <div class="col-xs-12 table-responsive">
                            <table id="tblTareas" class="table table-bordered table-condensed">
                                <thead class="well text-primary">
                                    <tr>
                                        <th class="text-center">Ref.</th>
                                        <th class="text-center">Tarea</th>
                                        <th class="text-center">Participantes</th>
                                        <th class="text-center">Estado</th>
                                        <th class="text-center">Creación</th>
                                        <th class="text-center">F. Fin prevista</th>
                                        <th class="text-center">Fin./Anul.</th>
                                        
                                    </tr>
                                </thead>
                            </table>

                        </div>




                        <div class="col-xs-12 MT20">
                            <a id="btnAddTarea" runat="server" class="btn btn-primary">Añadir tarea</a>
                            <button type="button" class="btn btn-default pull-right" id="btnCerrar">Cerrar</button>
                        </div>

                        <div class="clearfix"></div>
                    </div>

                </div>



            </div>

        </section>
    </form>

    <script src="../../../../plugins/datatables/jquery.dataTables.min.js"></script>
    <script src="../../../../scripts/moment.locale.min.js"></script>
    <script src="../../../../scripts/accounting.min.js"></script>
    <script src="js/view.js?v=20180315"></script>
    <script src="js/app.js?v=20180315"></script>

</body>
</html>
