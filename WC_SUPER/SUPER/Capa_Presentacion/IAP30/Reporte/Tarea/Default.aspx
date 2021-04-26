<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Reporte_Tarea_Default" %>

<%@ Register Src="~/Capa_Presentacion/bsUserControls/Menu/Menu.ascx" TagPrefix="cb1" TagName="Menu" %>
<%@ Register Src="~/Capa_Presentacion/bsUserControls/Head.ascx" TagPrefix="cb1" TagName="Head" %>


<!DOCTYPE html>

<html lang="es" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <cb1:Head runat="server" id="Head" />    
    <title>::: SUPER ::: - Detalle de tarea</title>    
</head>

<body>    
    <cb1:Menu runat="server" id="Menu" />

    <link rel='stylesheet' href="<% =Session["strServer"].ToString() %>plugins/datatables/jquery.dataTables.min.css" />
    <link rel="stylesheet" href="<% =Session["strServer"].ToString() %>css/jquery-ui.min.css"/>   
    <link rel="stylesheet" href="../../Documentos/StyleSheet.css"/>    
    <div class="container ocultable">
        <h1 class="sr-only">::: SUPER ::: - Detalle de tarea</h1>
        <br /><br />
        <ul id="tabs" class="nav nav-tabs" data-tabs="tabs" role="tablist">
            <li class="active"><a href="#general" data-toggle="tab" role="tab" aria-expanded="true">General</a></li>
            <li><a href="#docu" data-toggle="tab" role="tab" aria-expanded="false">Documentación</a></li>
            <li><a href="#notas" data-toggle="tab" role="tab" aria-expanded="false">Notas</a></li>
        </ul>            
        <div id="my-tab-content" class="tab-content">
            <div class="tab-pane active" id="general"> 
                <div class="ibox-content blockquote blockquote-info">
                <form class="form-horizontal collapsed in"  role="form" runat="server" accept-charset="iso-8859-15">                              
                    <div class="ibox-title">
                        <h2 class="text-primary">Datos generales</h2>
                        <div class="ibox-tools">
                            <a class="collapse-link">                    
                                <i data-toggle="collapse" data-target="#datosGenerales" class="chevron_toggleable fa fa-chevron-up pull-right"></i>
                            </a>
                        </div>
                      </div>
                    <div id="datosGenerales" class="ibox-content collapsed in">   
                        <div class="form-group">                                
                            <div class="col-xs-12 col-md-6">
                                <div class="form-group">
                                    <label for="txtProy" class="col-xs-12 col-md-4 control-label fk-label">Proyecto</label>
                                    <div class="col-xs-12 col-md-8">
                                        <input id="txtProy" name="" type="text" class="form-control input-md" value="" disabled="disabled" aria-readonly="true" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="txtProyTec" class="col-xs-12 col-md-4 control-label fk-label">Proyecto técnico</label>
                                    <div class="col-xs-12 col-md-8">
                                        <input id="txtProyTec" name="" type="text" class="form-control input-md" value="" disabled="disabled" aria-readonly="true"/>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-12 col-md-6">
                                <div class="form-group">
                                    <label for="txtFase" class="col-xs-12 col-md-4 control-label fk-label">Fase</label>
                                    <div class="col-xs-12 col-md-8">
                                        <input id="txtFase" name="" type="text" class="form-control input-md" value="" disabled="disabled" aria-readonly="true"/>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="txtAct" class="col-xs-12 col-md-4 control-label fk-label">Actividad</label>
                                    <div class="col-xs-12 col-md-8">
                                        <input id="txtAct" name="" type="text" class="form-control input-md" value="" disabled="disabled" aria-readonly="true"/>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-12">
                                <div class="form-group">
                                    <label id="lblTarea"  class="col-xs-12 col-md-2 control-label fk-label" for="datosTarea">Identificador tarea</label>
                                    <label for="desTarea" class="fk-label sr-only">Descripción Tarea</label>
                                    <div id="datosTarea">
                                        <div class="col-xs-4 col-md-2">
                                            <input id="idTarea" aria-label="Identificador de tarea" name="textinput" type="text" class="form-control input-md" value="" disabled="disabled" aria-readonly="true"/>
                                        </div>
                                        <div class="col-xs-8 col-md-8">
                                            <input id="desTarea" name="" type="text" class="form-control input-md" value="" disabled="disabled" aria-readonly="true"/>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="txtDesc" class="col-xs-12 control-label fk-label text-left">Descripción</label>
                            <div class="col-xs-12">
                                <textarea class="form-control txtArea" id="txtDesc" name="" rows="3" disabled="disabled" aria-readonly="true"></textarea>
                            </div>
                        </div>
                    </div>
                    <div class="ibox-title">                             
                            <h2 class="text-primary">Datos IAP referentes al técnico</h2>      
                            <div class="ibox-tools">    
                                 <a class="collapse-link">                    
                                    <i data-toggle="collapse" data-target="#datosIAP" class="chevron_toggleable fa fa-chevron-up pull-right"></i>
                                </a>                       
                            </div>                   
                            <!--Limpiamos los floats-->
                            <div class="clearfix"></div>
                        </div>
                    <div id="datosIAP" class="ibox-content collapsed in">  
                            <div class="form-group">
                                <div class="col-xs-12">
                                    <div class="form-group">
                                        <div class="col-xs-12 col-sm-4">
                                            <label for="PConsumo" class="col-xs-6 col-sm-12 col-md-6 col-lg-7 control-label fk-label">Primer consumo</label>
                                            <div class="col-xs-6 col-sm-12 col-md-12 col-md-6 col-lg-5">
                                                <input disabled="disabled" aria-readonly="true" id="PConsumo" name="" type="text" class="form-control input-md" value="" />
                                            </div>
                                        </div>
                                        <div class="col-xs-12 col-sm-4">
                                            <label for="PConsumido" class="col-xs-7 col-sm-12 col-md-7 control-label fk-label">Consumido(horas)</label>
                                            <div class="col-xs-5 col-sm-12 col-md-12 col-md-5">
                                                <input disabled="disabled" aria-readonly="true" id="PConsumido" name="" type="text" class="form-control input-md" value="" />
                                            </div>
                                        </div>
                                        <div class="col-xs-12 col-sm-4">
                                            <label for="pEstimado" class="col-xs-7 col-sm-12 col-md-7 control-label fk-label"><abbr title="Pendiente de estimar (horas)">Pte. estimado (horas)</abbr></label>
                                            <div class="col-xs-5 col-sm-12 col-md-12 col-md-5">
                                                <input disabled="disabled" aria-readonly="true" id="pEstimado" name="" type="text" class="form-control input-md" value="" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-xs-12 col-sm-4">
                                            <label for="UConsumo" class="col-xs-6 col-sm-12 col-md-6 col-lg-7 control-label fk-label">Último consumo</label>
                                            <div class="col-xs-6 col-sm-12 col-md-12 col-md-6 col-lg-5">
                                                <input disabled="disabled" aria-readonly="true" id="UConsumo" name="" type="text" class="form-control input-md" value="" />
                                            </div>
                                        </div>
                                        <div class="col-xs-12 col-sm-4">
                                            <label for="UConsumido" class="col-xs-7 col-sm-12 col-md-7 control-label fk-label">Consumido(jornadas)</label>
                                            <div class="col-xs-5 col-sm-12 col-md-12 col-md-5">
                                                <input disabled="disabled" aria-readonly="true" id="UConsumido" name="" type="text" class="form-control input-md" value="" />
                                            </div>
                                        </div>
                                        <div class="col-xs-12 col-sm-4">
                                            <label for="ATeorico" class="col-xs-7 col-sm-12 col-md-7 control-label fk-label">Avance teórico</label>
                                            <div class="col-xs-5 col-sm-12 col-md-12 col-md-5 input-group">
                                                <input disabled="disabled" aria-readonly="true" id="ATeorico" name="" type="text" class="form-control input-md" value="" />
                                                <span class="input-group-addon">%</span>
                                            </div>
                                        </div>
                                    </div>
                                 </div>           
                            </div>
                         </div>
                    <div class="ibox-title">                             
                            <h2 class="text-primary">Indicaciones</h2>                        
                            <div class="ibox-tools">    
                                 <a class="collapse-link">                    
                                    <i data-toggle="collapse" data-target="#indicaciones" class="chevron_toggleable fa fa-chevron-up pull-right"></i>
                                </a>                       
                            </div>
                            <div class="clearfix"></div>
                        </div>
                    <div id="indicaciones" class="ibox-content collapsed in">  
                    <div class="form-group">
                        <div class="col-xs-12 col-sm-6">
                            <fieldset class="fieldset">                              
                                <h3 class="sr-only">Indicaciones del responsable</h3>
                                <legend class="fieldset"><span>Del responsable</span></legend>
                                 <div class="form-group">
                                    <div class="col-xs-12">
                                        <label for="txtTotPrev" class="col-xs-6 col-md-8 control-label fk-label">Total previsto (horas)</label>
                                        <div class="col-xs-6 col-md-4">
                                            <input id="txtTotPrev" name="" type="text" class="form-control input-md" value="" disabled="disabled" aria-readonly="true"/>
                                        </div>
                                    </div>
                                 </div>
                                 <div class="form-group">
                                    <div class="col-xs-12">
                                        <label for="txtFFinPrev" class="col-xs-6 col-md-8 control-label fk-label">Fecha fin prevista</label>
                                        <div class="col-xs-6 col-md-4">
                                            <input id="txtFFinPrev" name="" type="text" class="form-control input-md" value="" disabled="disabled" aria-readonly="true"/>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-xs-12">
                                        <label for="txtParti" class="col-xs-12 col-md-3 control-label fk-label">Particulares</label>
                                        <div class="col-xs-12 col-md-9">
                                            <textarea class="form-control txtArea" id="txtParti" name="" rows="3" disabled="disabled" aria-readonly="true"></textarea>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-xs-12">
                                        <label for="txtColec" class="col-xs-12 col-md-3 control-label fk-label">Colectivas</label>
                                        <div class="col-xs-12 col-md-9">
                                            <textarea class="form-control txtArea" id="txtColec" name="" rows="3" disabled="disabled" aria-readonly="true"></textarea>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <div class="col-xs-12 col-sm-6">
                            <fieldset class="fieldset">
                                <h3 class="sr-only">Comentarios del técnico</h3>
                                <legend class="fieldset"><span>Del técnico</span></legend>
                                <div class="form-group">
                                    <div class="col-xs-12">
                                        <label for="txtTotEst" class="col-xs-6 col-sm-12 col-md-8 control-label fk-label">Total estimado (horas)</label>
                                        <div class="col-xs-6 col-sm-8 col-md-4">
                                            <input id="txtTotEst" name="" type="text" class="form-control input-md" value="" title="Formato hora: hh,mm" placeholder="0,00"/>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-xs-12">
                                        <label for="txtFEst" class="col-xs-6 col-sm-12 col-md-8 control-label fk-label">Fecha de finalización estimada</label>
                                        <div class="col-xs-6 col-sm-8 col-md-4">
                                            <input id="txtFEst" name="" type="text" class="form-control input-md txtFecha date-picker calendar-off" value="" title="Formato fecha: dd/mm/aaaa" placeholder="dd/mm/aaaa"/>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-xs-12">
                                        <div class="checkbox">
                                            <label class="col-xs-6 col-sm-12 col-md-8 control-label fk-label">
                                                <input type="checkbox" id="chkFinalizado"/>Trabajo finalizado
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-xs-12">
                                        <label for="txtObsv" class="col-xs-12 col-md-4 col-lg-3 control-label fk-label">Observaciones</label>
                                        <div class="col-xs-12 col-md-8 col-lg-9">
                                            <textarea class="form-control txtArea" id="txtObsv" name="" rows="3"></textarea>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </div>
                </form>
                </div>
            </div>
            <div class="tab-pane" id="docu">
                <div class="ibox-content blockquote blockquote-info">
                <form class="form-horizontal collapsed in"  role="form" >  
                    <div id="documentos" class="ibox-content collapsed in">  
                         <!--<div class="row">
                           
                            <table id="tablaDocu" class="display">
                                <thead>
                                    <tr>
                                        <th id="t363_descripcion" class="bg-primary">Descripción</th>
                                        <th id="t363_archivo" class="bg-primary">Archivo</th>
                                        <th id="t363_weblink" class="bg-primary">Link</th>
                                        <th id="t314_idusuario_autor" class="bg-primary">Autor</th>
                                    </tr>
                                </thead>        
                                <tbody>
           
                                </tbody>
                            </table>
                        </div>-->
                        <div class="row" id="datosDoc">
                        </div>
                        <!--<div id="botonesDocu" class="hidden-xs">
                            <div tabindex="0" role="button" aria-label="Añadir documento" id="btnAnadir" class="botonDocu">
                                <span class="fa fa-plus"></span>
                            </div>
                            <div tabindex="0" role="button" aria-label="Eliminar documento" id="btnEliminar" class="botonDocu">
                                <span class="fa fa-trash"></span>
                            </div>
                        </div>-->
                </div>
                </form>
                </div>
             </div>
             <div class="tab-pane" id="notas">
                    <div class="ibox-content blockquote blockquote-info">
                    <form class="form-horizontal collapsed in"  role="form" >                             
                       
                          <div id="nots" class="ibox-content collapsed in">
                            
                                <h2 class="sr-only">Notas</h2>
                                <div class="form-group">                            
                                    <label for="txtInv" class="col-xs-12 control-label fk-label text-left no-padding">Investigación/Detección</label>
                                    <div class="col-xs-12 no-padding">
                                        <textarea id="txtInv" class="form-control txtArea" name="" rows="3"></textarea>
                                    </div>
                                </div>
                               <div class="form-group">
                                   <label for="txtAcc" class="col-xs-12 control-label fk-label text-left no-padding">Acciones/Modificaciones</label>
                                    <div class="col-xs-12 no-padding">
                                        <textarea id="txtAcc" class="form-control txtArea" name="" rows="3"></textarea>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="txtPru" class="col-xs-12 control-label fk-label text-left no-padding">Pruebas</label>
                                    <div class="col-xs-12 no-padding">
                                        <textarea id="txtPru" class="form-control txtArea" name="" rows="3"></textarea>
                                    </div>
                                </div>
                               <div class="form-group">
                                   <label for="txtPaso" class="col-xs-12 control-label fk-label text-left no-padding">Pasos a producción</label>
                                    <div class="col-xs-12 no-padding">
                                        <textarea id="txtPaso" class="form-control txtArea" name="" rows="3"></textarea>
                                    </div>
                                </div>
                        </div>
                    </form>
                </div>
          </div>
        <br />
        <div class="form-group">
            <div id="pieTarea" class="pull-right">
                <div id="divGrabar">
                    <button id="btnGrabar" type="button" class="btn btn-primary">
                        <span>Grabar</span>
                    </button>
                </div>                
            </div>
        </div>   
       
        
    </div>
    <br />
    <br />
</body>

</html>
<script src="<% =Session["strServer"].ToString() %>scripts/string.js"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/jquery-ui.min.js"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/moment.locale.min.js"></script>
<script src="<% =Session["strServer"].ToString() %>scripts/accounting.min.js"></script>
<script src="<% =Session["strServer"].ToString() %>plugins/datatables/jquery.dataTables.min.js"></script>
<script src="<% =Session["strServer"].ToString() %>Capa_Presentacion/IAP30/scripts/JavaScript.js" type="text/javascript"></script>
<script src="../../Documentos/modelsDocumento.js"></script>
<script src="../../Documentos/modelsDocumentoList.js"></script>
<script src="../../Documentos/viewDocumentos.js"></script>
<script src="../../Documentos/appDocumentos.js"></script>

<script src="js/view.js"></script>
<script src="js/app.js"></script>


