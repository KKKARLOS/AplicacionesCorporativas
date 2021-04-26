<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_SIC_Mantenimientos_area_subarea_Default" %>

<%@ Register Src="~/Capa_Presentacion/bsUserControls/Head.ascx" TagPrefix="uc1" TagName="Head" %>
<%@ Register Src="~/Capa_Presentacion/bsUserControls/Menu/Menu.ascx" TagPrefix="uc1" TagName="Menu" %>

<!DOCTYPE html>

<html>
<head>
    <uc1:Head runat="server" ID="Head" />
    <title>Mantenimiento Áreas y Subáreas</title>

    <link href="../../../../plugins/datatables/jquery.dataTables.min.css" rel="stylesheet" />
</head>


<body>
    <uc1:Menu runat="server" ID="Menu" />
   <form runat="server"></form>
        <section>
            <div class="container-fluid">              
                <div class="row">
                    <div class="ibox-title">
                        <h5 class="text-primary">Áreas preventa</h5>
                        <h5 class="pull-right"><a class="underline" href ="../../Guias/Mantenimiento_Areas_subareas.pdf" target="_blank">Guía</a></h5>
                    </div>
                    <div class="ibox-content  blockquote blockquote-info">
                        <div class="col-xs-12 table-responsive">
                            <table id="tblAreas" class="table table-bordered table-condensed">
                                <thead class="well text-primary">
                                    <tr>
                                        <th class="text-center">Ref.</th>
                                        <th class="text-center">Área</th>                                               
                                        <th class="text-center">Responsable</th>
                                        <th class="text-center">Unidad</th>                                                                                                                  
                                    </tr>
                                </thead>
                            </table>

                        </div>

                       

                         <div class="col-xs-12 table-responsive">
                            <table id="tblSubAreas" class="table table-bordered table-condensed">
                                <thead class="well text-primary">
                                    <tr>
                                        <th class="text-center">Ref.</th>
                                        <th class="text-center">Subárea</th>                                               
                                        <th class="text-center">Responsable</th>      
                                        <th class= "text-center">Área</th>                                  
                                    </tr>
                                </thead>
                            </table>

                        </div>

                         <div class="clearfix"></div>

                    </div>

                </div>
            </div>

        </section>


       

        <!--Modal Áreas-->        
        <div class="modal fade" id="modal-area" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <%--<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>--%>
                        <h4 class="modal-title">Área preventa</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row form-group">
                            <div class="col-xs-1">
                                <span>Unidad:</span>
                            </div>
                            <div class="col-xs-10">
                                <b><span id="txtUnidad" runat="server"></span></b>
                            </div>
                        </div>
                        
                        <br />

                        <div class="row form-group">
                            <div class="col-xs-2">
                                <span>Denominación:</span>                                
                            </div>
                            <div class="col-xs-10">
                                <input id="inputDenominacion" class="col-xs-12 form-control" runat="server" type="text" />
                            </div>
                        </div>
                        
                        <div class="row">
                            <div class="col-xs-2">
                                <span id="linkResponsable" class="underline">Responsable:</span>                                
                            </div>
                            <div class="col-xs-10">
                                <input id="inputResponsable" required="required" class="col-xs-12 form-control" type="text" disabled="disabled" />
                            </div>
                        </div>

                        <hr />


                        <span id="linkProfesionales" class="underline">Añadir o eliminar profesionales</span>
                        <table id="tablaFigurasArea" class="table table-bordered table-condensed table-fixed">
                            <thead>
                                <tr>
                                    <th>Profesional</th>
                                    <th>Delegado</th>
                                    <th>Colaborador</th>                                   
                                    <th>Invitado</th>
                                </tr>
                            </thead>
                            <tbody id="tblFiguras">                             
                            </tbody>
                            
                        </table>
   
                    </div>
                    <div class="modal-footer">                        
                        <button id="btnGrabar" type="button" class="btn btn-primary fk_grabar">Grabar</button>
                        <button type="button" id="btnCerrar" class="btn btn-default">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>
        <!--FIN Modal Áreas-->


        <!--Modal Subáreas-->
        <div class="modal fade" id="modal-subarea" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <%--<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>--%>
                        <h4 class="modal-title">Subárea preventa</h4>
                    </div>
                    <div class="modal-body">                      
                        <div class="row form-group">
                            <div class="col-xs-2">
                                <span>Denominación:</span>                                
                            </div>
                            <div class="col-xs-10">
                                <input id="txtDenominacionSubArea" class="col-xs-12 form-control" runat="server" type="text" />
                            </div>
                        </div>
                        
                        <div class="row">
                            <div class="col-xs-2">
                                <span id="linkResponsableSubArea" class="underline">Responsable:</span>                                
                            </div>
                            <div class="col-xs-10">
                                <input id="inputResponsableSubArea" required="required" class="col-xs-12 form-control" type="text" disabled="disabled" />
                            </div>
                        </div>
                        
                        <br />
                        <div class="row">
                            <div class="col-xs-5">
                                <span>Permitir autoasignación de líder</span>
                            </div>
                            <div class="col-xs-1">
                                <input id="chkAutoAsignacion" type="checkbox" class="col-xs-12 form-control" />
                            </div>

                        </div>
                        <hr />


                        <div id="divFiguras">
                            <span id="linkProfesionalesSubArea" class="underline">Añadir o eliminar profesionales</span>
                            <table id="tablaSubArea" class="table table-bordered table-condensed">
                                <thead>
                                    <tr>
                                        <th>Profesional</th>
                                        <th>Delegado</th>
                                        <th>Colaborador</th>
                                        <th>Líder</th>
                                    </tr>
                                </thead>
                                <tbody id="tblFigurasSubArea">
                                </tbody>

                            </table>
                        </div>

                    </div>
                    <div class="modal-footer">                        
                        <button id="btnGrabarSubArea" type="button" class="btn btn-primary fk_grabarSubArea">Grabar</button>
                        <button type="button" id="btnCerrarSubArea" class="btn btn-default">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>
        <!--FIN Modal Subáreas-->


    <!--Ayuda de Responsables-->
    <div id="divAyudaResponsables"></div>
    <div id="divAyudaResponsablesSubArea"></div>
    <script src="../../../../plugins/IB/buscaprof.js"></script>

    <div id="divAyudaProfesionales"></div>    
    <div id="divAyudaProfesionalesSubArea"></div>    
    <script src="../../../../plugins/IB/buscaprofmulti.js"></script>

    <script src="../../../../plugins/datatables/jquery.dataTables.min.js"></script>    
    <script src="js/view.js?version=22-01-2018_01"></script>
    <script src="js/view_modalArea.js"></script>
    <script src="js/view_modalSubarea.js"></script>
    <script src="js/app.js"></script>
    <script src="js/app_modalArea.js"></script>
    <script src="js/app_modalSubarea.js"></script>
    </body>

    </html>



