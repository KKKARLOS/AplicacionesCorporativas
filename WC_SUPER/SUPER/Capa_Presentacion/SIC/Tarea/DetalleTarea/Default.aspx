<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_SIC_DetalleTarea_Default" %>

<%@ Register Src="~/Capa_Presentacion/bsUserControls/Head.ascx" TagPrefix="uc1" TagName="Head" %>
<%@ Register Src="~/Capa_Presentacion/bsUserControls/Menu/Menu.ascx" TagPrefix="uc1" TagName="Menu" %>

<!DOCTYPE html>

<html>
<head>
    <uc1:Head runat="server" ID="Head" />
    <title>Mis tareas como participante</title>
    <link href="../../../../plugins/ayudapersonas/ayudapersonas.css" rel="stylesheet" />
    <link href="../../../../plugins/datatables/jquery.dataTables.min.css" rel="stylesheet" />
    <link href="../../../../css/jquery-ui.min.css" rel="stylesheet" />
</head>


<body>
    <uc1:Menu runat="server" ID="Menu" />

    <!-- meter paddingtop al container -->
  <form id="frmTareas" class="form-horizontal" runat="server">
        <section data-route='add-tareas' data-weight="8">
            <div class="container">
                  <div class="row">                  
                    <div class="ibox-content  blockquote blockquote-info">
                        
                        <div class="form-group">
                                    <label class="col-sm-1 control-label" for="ta205_denominacion">Acción</label>
                                    <div class="col-sm-4">
                                        <input type="text" id="ta205_denominacion" runat="server" class="form-control" disabled="disabled" />
                                    </div>

                                    <label class="col-sm-1 control-label text-right" for="txtFinRequerida">Fin req.</label>
                                    <div class="col-sm-2">
                                        <input type="text" id="txtFinRequerida" runat="server" class="form-control text-center" disabled="disabled" />
                                    </div>
                                
                                    <label class="col-sm-1 control-label text-right" for="lider">Líder</label>
                                    <div class="col-sm-3">
                                        <input type="text" id="lider" runat="server" class="form-control" disabled />
                                    </div>
                        </div>


                         <div class="form-group">
                                <label class="col-sm-1 control-label" for="txtOportunidad" id="lblOportunidadSolic" runat="server">Oportunidad</label>
                                <div id="inputNumOportunidadAcciones" class="col-sm-1">
                                    <input type="text" id="acc-txtOportunidadSolic" readonly="readonly" class="form-control text-right" style="width: 90px"/>
                                </div>
                                <div class="col-sm-10">
                                    <input type="text" id="acc-txtDenominacionSolic" readonly="readonly" class="form-control" />
                                </div>
                         </div>


                        <div id="divCliente" class="form-group MB0" runat="server">
                            <label id="acc-lblClienteSolic" class="col-sm-1 control-label" for="txtCliente">Cliente</label>
                            <div class="col-sm-11">
                                <input type="text" id="acc-txtClienteSolic" readonly="readonly" class="form-control " value="" />
                            </div>

                        </div>

                        <br />

                        <div class="col-sm-5">
                            <div class="form-group MB0">
                                <a id="linkInformacionAdicional" runat="server" href="#" data-toggle="collapse" data-target="#divInformacionAdicional" ><i class="fa fa-plus"></i>
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

                               
                  <%--<div id="div_container_cab_P"  runat="server">
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
                                    <div class="col-sm-2">
                                        <input type="text" id="txtContataPrevista_cab" readonly="readonly" class="form-control text-right" value="" />
                                    </div>

                                    <label class="col-sm-2 col-sm-offset-4 control-label text-right" for="txtFechaFin_cab">Fecha Fin</label>
                                    <div class="col-sm-2">
                                        <input type="text" id="txtFechaFin_cab" readonly="readonly" class="form-control text-center" value="" />
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-sm-2 control-label" for="txtCoste_cab">Coste</label>
                                    <div class="col-sm-2">
                                        <input type="text" id="txtCoste_cab" readonly="readonly" class="form-control text-right" value="" />
                                    </div>

                                    <label class="col-sm-2 col-sm-offset-4 control-label text-right" for="txtResultado_cab">Resultado</label>
                                    <div class="col-sm-2">
                                        <input type="text" id="txtResultado_cab" readonly="readonly" class="form-control text-right" value="" />
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-sm-2 control-label" for="txtCoste_cab">Solicitante</label>
                                    <div class="col-sm-5">
                                        <input type="text" id="txtSolicitante" readonly="readonly" class="form-control text-right" value="" />
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



                <div class="row">
                    <div class="ibox-title">
                        <h5 id="txtDetalleTarea" class="text-primary">Detalle tarea</h5>
                         <!--Sello estado-->
                        <label id="lblSello" runat="server" class="col-sm-3 control-label sello"></label>
                        <h5 class="pull-right"><a class="underline" href ="../../Guias/L_Detalle_de_tarea.pdf" target="_blank">Guía</a></h5>
                    </div>

                    <div class="ibox-content  blockquote blockquote-info">
                        <div class="row">
                            <div class="col-xs-12 col-sm-12">
                                <div class="form-group">                                                                                                           
                                    <label class="col-xs-12 col-sm-1 control-label" for="txtOportunidad">Tarea<span class="fk_camposObligatorios rojoVivo hide">&nbsp;*</span></label>
                                    <div id="divNumtarea" class="col-xs-12 col-sm-1">
                                        <input id="ta207_idtareapreventa" runat="server" type="text" disabled="disabled" class="form-control text-right" style="width: 90px" />
                                    </div>
                                    <div class="col-xs-12 col-sm-7">
                                        <asp:DropDownList id="selectDenominacion" runat="server" Enabled="false"  required="required" class="form-control" />                                                                                
                                    </div>

                                    <div id="divFechaCreacion" class="col-xs-12 col-sm-3  pull-right">
                                        <label class="col-xs-12 col-sm-6 control-label text-right" for="txtOportunidad">Fecha creación</label>
                                        <div class="col-xs-12 col-sm-3">
                                            <input id="ta207_fechacreacion" style="width: 90px" runat="server" type="text" class="form-control" disabled="disabled" />
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>

                        <div class="row row">
                            <div class="col-xs-12 col-sm-12">
                                <div class="form-group">
                                    <div class="row col-xs-12 col-sm-8">
                                        <div runat="server" id="divinputDenominacion"  style="display: none">
                                            <label class="col-xs-12 col-sm-3 control-label " for="ta207_denominacion"> Denominación<span id="asteriscoDenominacion" class="fk_camposObligatorios rojoVivo hide">&nbsp;*</span></label>
                                            <div class="col-xs-12 col-sm-8" style="padding-left:15px; padding-right:15px">
                                                <input type="text" id="ta207_denominacion" maxlength="100" runat="server" required="required" disabled="disabled" class="form-control" />
                                            </div>
                                            
                                        </div>

                                    </div>
                                    
                                    <div class="col-xs-12 col-sm-4" style="margin-left:-9px">
                                        <label class="col-xs-12 col-sm-9 control-label text-right" for="txtOportunidad"> F. Finalización prevista<span class="fk_camposObligatorios rojoVivo hide">&nbsp;*</span></label>
                                        <div class="col-xs-12 col-sm-3">
                                            <input id="ta207_fechafinprevista" name="ta207_fechafinprevista" style="width: 90px" required="required" runat="server" type="text" class="form-control" disabled="disabled" />
                                        </div>

                                    </div>
                                       
                                </div>
                            </div>
                        </div>

                        
                        <div class="row">
                             <div class="col-xs-12 col-sm-5">
                                 <a id="linkDocumentacion" class="quitarEstilosEnlace"  runat="server">Documentación</a>
                                <span id="spanNumDocumentos" class="badge" runat="server"></span>
                             </div>
                              

                            <div id="divFechaFinReal" class="col-xs-12 col-sm-5  pull-right">
                                <label runat="server" id="lblta207_fechafinreal" class="col-xs-12 col-sm-9 control-label text-right" style="margin-left:-15px" for="ta207_fechafinreal"></label>
                                <div class="col-xs-12 col-sm-3">
                                    <input id="ta207_fechafinreal" style="width: 90px" runat="server" type="text" class="form-control" disabled="disabled" />
                                </div>
                            </div>

                            
                            <div class="clearfix"></div>
                            <br />

                              <div class="col-sm-8">                                
                                <div class="form-group">
                                    <label class="col-sm-12 control-label">Descripción</label>
                                    <div class="col-sm-12">
                                        <textarea class="form-control" rows="6" disabled="disabled"  id="ta207_descripcion" runat="server"></textarea><br />
                                        <label class="control-label">Observaciones</label>
                                        <textarea class="form-control" rows="5" disabled="disabled" id="ta207_observaciones" runat="server"></textarea>
                                    </div>
                                </div>                              
                            </div>



                            <div id="divOtrosParticipantees" class="col-sm-4">
                                <label id="lblParticipantes" class="underline quitarEstilosEnlace">Participantes</label>                                
                                
                                <table id="tblParticipantes" class="compact cell-border">       
                                    <thead class="bg-primary">
                                        <tr>
                                            <th>Profesional</th>
                                            <th>Participación</th>
                                        </tr>
                                    </thead>                                                              
                                   
                                </table>
                            </div>
                           
                            <div class="col-sm-4">                                
                                <div class="form-group">
                                    <br />
                                    <label class="col-sm-12 control-label">Comentarios de los participantes</label>
                                    <div class="col-sm-12">
                                        <textarea class="form-control" rows="5" disabled="disabled" id="ta207_comentario" runat="server"></textarea>
                                    </div>
                                </div>                              
                            </div>


                        </div>

                        <br />

                        
                         <div class="pull-left">                             
                            <button type="button"  class="btn btn-primary hide" id="btnFinalizarTarea">Finalizar tarea</button>
                            <button type="button"  class="btn btn-primary hide" id="btnAnularTarea">Anular tarea</button>
                            <button type="button"  class="btn btn-primary hide" id="btnFinalizarParticipacion">Finalizar mi participación</button>
                            <button type="button" class="btn btn-primary hide" id="btnAnularParticipacion">Anular mi participación</button>
                             <div id="divtextareaMotivoAnulacion" class="hide">
                                 <span>Motivo de anulación</span><br />
                                 <textarea id="textareaMotivoAnulacion" disabled="disabled" class="W500" runat="server"></textarea>
                             </div>
                        </div>

                        <div class="row pull-right">                            
                            <button type="button" class="btn btn-primary hide" id="btnGrabar">Grabar</button>
                            <button type="button" class="btn btn-default" id="btnRegresar">Cerrar</button>                            
                        </div>

                        <div class="clearfix"></div>
                    </div>
                </div>
                
            </div>
            
        </section>
    </form>



    <!-- TAREAS -->

       <div class="modal fade" id="modal-documentacion">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <h4 class="modal-title">Documentación</h4>
                </div>
                <div class="modal-body">                    
                    <div class="row">
                         <div class="col-sm-12">                               
                                <table class="table table-bordered table-megacondensed">                                      
                                       <tbody>
                                           <tr>                                               
                                               <td>Oportunidad.doc</td>
                                           </tr>
                                       </tbody>

                                   </table>
                                    
                                    <div class="pull-right">
                                        <i class="fa fa-plus text-success"></i>
                                        <i class="fa fa-minus text-danger"></i>
                                    </div>
                            </div>
                    </div>
                </div>
                <div class="modal-footer">                   
                    <b>
                        <button id="btnCancelarModalDocumentacion" data-dismiss="modal" class="btn btn-default">Cerrar</button></b>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>



    <!--Modal estado participantes-->
    <div class="modal fade" id="modal-estadoParticipantes">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <h4 class="modal-title">Participación en la tarea</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-sm-12">

                            <div class="form-group">
                                <div class="col-xs-12">
                                    <span id="spanProfesional"></span>
                                    <hr />
                                    <div class="radio">
                                        <label for="rdbParticipacionEnCurso">
                                            <input type="radio" name="rdbEstados" id="rdbParticipacionEnCurso" value="A">
                                            <span></span>
                                        </label>
                                    </div>
                                    <div class="radio">
                                        <label for="rdbParticipacionFinalizada">
                                            <input type="radio" name="rdbEstados" id="rdbParticipacionFinalizada" value="F">
                                            <span></span>
                                        </label>
                                    </div>

                                    <div class="radio">
                                        <label for="rdbParticipacionAnulada">
                                            <input type="radio" name="rdbEstados" id="rdbParticipacionAnulada" value="X">
                                            <span></span>
                                        </label>
                                    </div>
                                </div>
                            </div>



                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <b>
                        <button id="btnAceptarModalEstadoPartipacion" data-dismiss="modal" class="btn btn-primary">Aceptar</button>
                        <button id="btnCancelarModalEstadoPartipacion" data-dismiss="modal" class="btn btn-default">Cancelar</button>
                    </b>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>



      <div class="modal fade" id="modal-anularTarea">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <h4 class="modal-title">Anulación de tarea</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-sm-12">
                           <p>Has pulsado el botón 'Anular tarea'. ¿Estás conforme?"</p> 
                            <p>Para poder anular una tarea, debes introducir un motivo.</p> 
                            <label class="label-control">Motivo de anulación<span class="text-danger">&nbsp;*</span></label>
                            <textarea maxlength="250" rows="5" class="form-control" id="txtMotivoAnulacion" required="required"></textarea>                                
                            <span id="numCaracteres">250 caracteres disponibles</span>                   
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <b>
                        <button id="btnAceptar_anularTarea" data-dismiss="modal" class="btn btn-primary">Aceptar</button>
                        <button id="btnCancelar_anularTarea" data-dismiss="modal" class="btn btn-default">Cancelar</button>
                    </b>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>


    <div id="divayudapersonasmulti1"></div>
    <script src="../../../../plugins/IB/buscaprofmulti.js"></script>

    <script src="../../../../scripts/jquery-ui.min.js"></script>
    <script src="../../../../scripts/datepicker_es.js"></script>
    <script src="../../../../plugins/datatables/jquery.dataTables.min.js"></script>
    <script src="../../../../scripts/moment.locale.min.js"></script>
    <script src="../../../../scripts/string.js"></script>    
    <script src="../../Documentos/Documentos.js?v=20171128_02"></script>

    <script src="js/view.js?v=20180315"></script>
    <script src="js/app.js?v=20180315"></script>
    
    </body>

    </html>


