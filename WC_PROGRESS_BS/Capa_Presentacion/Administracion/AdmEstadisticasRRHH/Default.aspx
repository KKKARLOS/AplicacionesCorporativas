<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Administracion_AdmEstadisticasRRHH_Default" %>

<%@ Register Src="~/uc/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="cb1" %>
<%@ Register Src="~/uc/HeaderMeta.ascx" TagName="HeaderMeta" TagPrefix="cb1" %>
<%@ Register Src="~/uc/PieMenu.ascx" TagName="PieMenu" TagPrefix="cb1" %>
<%@ Register  Src="~/uc/uc_Session.ascx" TagName="mensajeSession" TagPrefix="uc3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <cb1:HeaderMeta runat="server" ID="HeaderMeta"></cb1:HeaderMeta>
    <title></title>
     <link rel="Stylesheet" href="../../../js/plugins/jQueryUI/jquery-ui.css"/>
    <style>
        .ui-datepicker-calendar {
            display: block;
        }

        .calendar-off table.ui-datepicker-calendar {
            display: none !important;
        }

        .ui-widget-header {       
            border: 1px solid rgb(80,132,159);
            background: rgb(80,132,159);
            color: #666;
        }

    </style>
     <script>
         <%=idficepi%>
         <%=nombre%>
         <%=sexo%> 
         <%=defectoAntiguedad%> 
    </script>
    
</head>

<body data-codigopantalla="402">
     <uc3:mensajeSession ID="MensajeSession" runat="server" />
    <cb1:CabeceraMenu runat="server" ID="CabeceraMenu"></cb1:CabeceraMenu>
    <br />
    <br class="hidden-xs" />
    <br class="hidden-xs" />

     <div class="container" style="margin-top: -20px">

        <div class="row">
            <div class="col-xs-12">
                <fieldset class="fieldset">
                    <legend class="fieldset">Evaluaciones</legend>

                    <!--Contenido filtros-->
                    <div id="filtros">
                        <!--Fila 1-->
                        <div class="row">

                        <div class="col-xs-6">
                            <fieldset class="fieldset">
                                <legend class="fieldset"><span>Período</span></legend>
                                <div class="col-xs-6">
                                    <select id="selMesIni" runat="server" onchange=""></select>
                                    <select id="selAnoIni" runat="server" onchange=""></select>
                                </div>
                                <div class="col-xs-6">
                                    <select id="selMesFin" runat="server" onchange=""></select>
                                    <select id="selAnoFin" runat="server" onchange=""></select>
                                </div>
                            </fieldset>
                        </div>
                        


                        <div class="col-xs-6">
                            <fieldset class="fieldset">
                                <legend class="fieldset">Profesionales</legend>
                             <div id="divtxtFantiguedad" runat="server" class="fk-ocultar">
                                <span>Antigüedad de referencia</span>
                               <input type="text" runat="server" id="txtFantiguedad" name="to" readonly="readonly"/>                                              
                            </div>

                        
                            
                             <div id="divEstado" runat="server">
                                <span>Estado</span>
                                <select id="cboEstado" maxlength="50" runat="server" onchange="">
                                    <option value="">Todos</option>
                                    <option value="A">Alta</option>
                                    <option value="B">Baja</option>
                                </select>
                            </div>
                           
                        </fieldset>
                            
                           
                        </div>

                        </div>

                        <!--FIN Fila 1-->

                        <div class="row">
                             <div id="divCR" runat="server" class="col-xs-6 fk-ocultar">
                                <button id="lblCR" class="btn btn-link">C.R</button>
                                <input id="CR" type="text" readonly="readonly"/>
                                 <i id="imgCREvaluacion" style="display: none" class="glyphicon glyphicon-repeat"></i>
                            </div>
                        </div>

                        <div class="row" style="margin-bottom:-8px">
                             <div id="divColectivo">
                                <span>Colectivo</span>
                                 <select id="cboColectivo" maxlength="50" runat="server" onchange="cargarEstadisticas()">
                                     <option value="">Todos</option>                                   
                                </select>              
                            </div>
                        </div>

                      

                        <!-- FIN Filtros-->
                    </div>

                    
                    <span style="margin-left:47.1%; font-weight:bold;">Total</span>
                    <span style="margin-left:77px;font-weight:bold;">Válidas</span>
                    <span style="margin-left:58px;font-weight:bold;">No válidas</span>
                    <span style="margin-left:38px;font-weight:bold;">Evaluadores</span>
                    

                    <!--RESULTADOS EVALUACIONES-->
                     <div class="row">
                         <div class="col-xs-12">
                             <div id="panelEvaluaciones" class="panel panel-primary" style="margin-bottom:4px">                                
                                 <div class="panel-contenido" style="margin-left:0">

                                     <div class="pull-left" style="padding-left: 0; padding-right: 0; width: 330px">
                                         <table style="margin-bottom: 0" class="table">
                                             <tbody>
                                                 <tr>

                                                     <td>Abiertas</td>

                                                 </tr>
                                                 <tr>
                                                     <td>En curso</td>
                                                 </tr>
                                                 <tr>
                                                     <td>Cerradas</td>
                                                 </tr>

                                                 <tr>
                                                     <td style="padding-left: 30px !important;">Firmadas</td>
                                                 </tr>

                                                 <tr>
                                                     <td style="padding-left: 30px !important;">No firmadas</td>
                                                 </tr>

                                                 <tr>
                                                     <td>Total</td>
                                                 </tr>


                                             </tbody>

                                         </table>
                                     </div>


                                   

                                     <div class="pull-left" style="width: 500px">
                                         <table id="tblEvaluaciones" class="table">
                                         </table>
                                     </div>

                                 </div>
                                 <div class="clearfix"></div>
                                 
                             </div>
                         </div>
        </div>


                </fieldset>
            </div>
        </div>


         <br />


           <div class="row">
            <div class="">
               
                    <!--Contenido filtros-->
                    <div id="filtrosEvaluadores" class="col-xs-12" style="margin-top:-10px">
                       
                        <div class="row col-xs-5">
                            <fieldset class="fieldset" style="width:390px">
                                <legend class="fieldset">Evaluadores/as</legend>
                             <div id="divCREvaluadores" runat="server">
                                <button id="lblCREvaluadores" class="btn btn-link">C.R</button>
                                <input id="CREvaluadores" type="text" readonly="readonly" style="width:280px"/>
                                 <i id="imgCREvaluadores" style="display: none" class="glyphicon glyphicon-repeat"></i>
                            </div>

                             <div >                                 
                                <div id="panelProfesionalesEvaluadores" class="panel panel-primary" style="width:353px">                                 
                                    <div class="panel-contenido" style="margin-left:0">

                                        
                                        <div class="pull-left" style="padding-left: 0; padding-right: 0; width: 265px">
                                            <table style="margin-bottom: 0" class="table">
                                                <tbody>
                                                    <tr>
                                                        <td>Sin confirmar equipo</td>
                                                    </tr>
                                                    <tr>
                                                        <td>Con el equipo confirmado</td>
                                                    </tr>
                                                    
                                                    <tr>
                                                        <td>Total</td>
                                                    </tr>

                                                    <tr>
                                                        <td>Profesionales sin evaluador/a</td>

                                                    </tr>
                                                </tbody>

                                            </table>
                                        </div>

                                        <div class="pull-left">
                                            <table id="tblProfesionalesEvaluadores" class="table">
                                            </table>
                                        </div>

                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                            </div>

                        </fieldset>
                        </div>








                        <div class="col-xs-7" style="margin-left:30px">


                               <fieldset class="fieldset" style="width:530px">
                    <legend class="fieldset">Profesionales</legend>

                    <!--Contenido filtros-->
                    <div id="filtrosColectivoProgress" class="col-xs-12">
                      <div class="row">

                        <div class="row" style="width:430px; margin-left:25px">
                            <fieldset class="fieldset">
                                <legend class="fieldset"><span>Período</span></legend>
                                <div class="col-xs-6">
                                    <select id="SelMesIniProgress" runat="server" onchange=""></select>
                                    <select id="SelAnoIniProgress" runat="server" onchange=""></select>
                                </div>
                                <div class="col-xs-6">
                                    <select id="SelMesFinProgress" runat="server" onchange=""></select>
                                    <select id="SelAnoFinProgress" runat="server" onchange=""></select>
                                </div>
                            </fieldset>
                        </div>
                        
                          <div class="row" style="margin-top:5px">
                             <div id="divEvaluador" runat="server">
                                <button id="lblEvaluador" class="btn btn-link">Evaluador</button>                                
                                <input id="evaluador" type="text" readonly="readonly" style="width:375px"/>
                                 <i id="imgEvaluadorColectivoProgress" style="display: none" class="glyphicon glyphicon-repeat"></i>
                            </div>
                        </div>


                            <div class="row">
                             <div id="divCRColectivos" runat="server" style="margin-left:-13px" class="fk-ocultar">
                                <button id="lblCRColectivos" style="margin-left:50px" class="btn btn-link">C.R</button>
                                <input id="CRColectivos" style="width:375px;margin-left:3px" type="text" readonly="readonly"/>
                                 <i id="imgCRColectivos" style="display: none" class="glyphicon glyphicon-repeat"></i>
                            </div>
                        </div>


                        <div class="row">   
                            <div class="col-xs-6">
                              <div id="divColectivoProgress">
                                <span>Colectivo</span>
                                 <select id="cboColectivoProgress"  maxlength="50" runat="server" style="margin-left:16px;width:150px">
                                     <option value="">Todos</option>                                   
                                </select>              
                            </div>
                                </div>                        

                            <div class="col-xs-6">

                            <div id="divtxtFantiguedadProgress" style="margin-left:-15px;margin-bottom:8px" runat="server" class="fk-ocultar">
                                <span>Antigüedad de referencia</span>
                               <input type="text" runat="server" id="txtFantiguedadProgress" name="to" readonly="readonly"/>                                              
                            </div>
                            </div>
                        </div>



                        </div>
                        
                       
                      
                          

                        <!-- FIN Filtros-->
                    </div>


                     <!--RESULTADOS EVALUACIONES-->
              
                    
            <div class="row col-xs-12">                
                <div id="panelProfesionales" class="panel panel-primary" style="width: 488px">
                   
                  <div class="panel-contenido" style="margin-left:0" >
                       <div class="pull-left" style="padding-left:0; padding-right:0;width:385px">
                            <table style="margin-bottom:0" class="table">
                            <tbody>
                                <tr>

                                    <td>Con evaluaciones abiertas</td>
                                    
                                </tr>
                                <tr>
                                    <td>Con evaluaciones en curso</td>
                                </tr>
                                <tr>
                                    <td>Con evaluaciones cerradas</td>
                                </tr>

                                <tr>
                                    <td style="padding-left:30px !important;">Firmadas</td>
                                </tr>

                                <tr>
                                    <td style="padding-left:30px !important;">No firmadas</td>
                                </tr>
                              
                                <tr>
                                    <td><a href="#" data-toggle="popover" data-placement="top" title="Información" data-content="Profesionales que tienen evaluaciones firmadas por su evaluador/a."><i class="glyphicon glyphicon-info-sign"></i></a><span style="margin-left:5px">Evaluados</span></td>
                                </tr>
                                  
                                 <tr>
                                    <td><a href="#" data-toggle="popover" data-placement="top" title="Información" data-content="Profesionales que no tienen evaluaciones firmadas por su evaluador/a."><i class="glyphicon glyphicon-info-sign"></i></a><span style="margin-left:5px">No evaluados</span></td>
                                </tr>

                              


                            </tbody>

                        </table>
                        </div>

                      <div class="pull-left">
                          <table id="tblColectivoProgress" class="table"></table>                          
                      </div>

                    </div>
                    <div class="clearfix">
                    </div>
                </div>
            </div>
       

                </fieldset>


                        </div>

                        <!-- FIN Filtros-->
                    </div>

              
            </div>
        </div>

         <br />

        

    </div>

    
     <!--MODAL CR EVALUACIONES-->
    <div class="modal fade" id="modal-CR">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <h4 class="modal-title">Selección de CR</h4>
                </div>

                <div class="modal-body dual-list">                  
                    <div class="">
                        <h4>Catálogo de CR's utilizados en alguna evaluación</h4>
                        <div class="well">
                            <div class="row">                           
                            <div class="col-xs-11 input-group" style="margin-left:15px">
                                <input type="text" name="SearchDualList" class="form-control" placeholder="Buscar" />
                            </div>
                        </div>
                            <ul class="list-group" runat="server" id="lisCR">
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <b>
                        <button id="btnSeleccionarCR" class="btn btn-primary">Seleccionar</button></b>
                    <b>
                        <button id="btnCancelarCR" class="btn btn-default" style="margin-left:7px">Cancelar</button></b>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->

    <!--FIN MODAL CR EVALUACIONES-->

   
     <!--MODAL EVALUADORES-->
    <div class="modal fade" id="modal-evaluadores">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <h4 class="modal-title">Selección de evaluador</h4>
                </div>
                <div class="modal-body">

                    <div id="txtBusqueda" class="row">
                        <div class="col-xs-3">
                             <label for="lblApellido1">Apellido 1º</label>
                                <input id="txtApellido1" type="text" />
                        </div>
                        <div class="col-xs-3" style="margin-left: 8px">
                             <label for="lblApellido2">Apellido 2º</label>
                                <input id="txtApellido2" type="text" />
                        </div>
                        <div class="col-xs-3" style="margin-left: 8px">
                            <label for="lblNombre">Nombre</label>
                            <input id="txtNombre" type="text" />
                        </div>
                        <div class="col-xs-1 col-xs-offset-1">
                            <button id="btnObtener" style="margin-top: 22px" class="btn btn-primary btn-xs">Obtener</button>
                        </div>
                    </div>


                    <br />
                    <div class="row">
                        <div class="col-xs-12">
                            <ul class="list-group" runat="server" id="lisEvaluadores">
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <b><button id="btnSeleccionar" class="btn btn-primary">Seleccionar</button></b>
                    <b><button id="btnCancelar" class="btn btn-default" style="margin-left:7px">Cancelar</button></b>                    
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->

    <!--FIN MODAL EVALUADORES-->


     <!--MODAL CR EVALUADORES-->
    <div class="modal fade" id="modal-CREvaluadores">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <h4 class="modal-title">Selección de CR</h4>
                </div>

                <div class="modal-body dual-list">                  
                    <div class="">
                        <h4>Catálogo de CR's utilizados en alguna evaluación</h4>
                        <div class="well">
                            <div class="row">                           
                            <div class="col-xs-11 input-group" style="margin-left:15px">
                                <input type="text" name="SearchDualList" class="form-control" placeholder="Buscar" />
                            </div>
                        </div>
                            <ul class="list-group" runat="server" id="lisCREvaluadores">
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <b>
                        <button id="btnSeleccionarCREvaluadores" class="btn btn-primary">Seleccionar</button></b>
                    <b>
                        <button id="btnCancelarCREvaluadores" class="btn btn-default" style="margin-left:7px">Cancelar</button></b>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->

    <!--FIN MODAL CR EVALUADORES-->


    <div class="modal fade" id="modal-CRColectivos">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <h4 class="modal-title">Selección de CR</h4>
                </div>

                <div class="modal-body dual-list">                  
                    <div class="">
                        <h4>Catálogo de CR's utilizados en alguna evaluación</h4>
                        <div class="well">
                            <div class="row">                           
                            <div class="col-xs-11 input-group" style="margin-left:15px">
                                <input type="text" name="SearchDualList" class="form-control" placeholder="Buscar" />
                            </div>
                        </div>
                            <ul class="list-group" runat="server" id="lisCRColectivos">
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <b>
                        <button id="btnSeleccionarCRColectivos" class="btn btn-primary">Seleccionar</button></b>
                    <b>
                        <button id="btnCancelarCRColectivos" class="btn btn-default" style="margin-left:7px">Cancelar</button></b>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->

    
   
</body>
</html>

<cb1:PieMenu runat="server" ID="PieMenu"></cb1:PieMenu>
<script type="text/javascript" src="js/models.js"></script>



