<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Evaluacion_DeMiEquipo_Default" %>

<%@ Register  Src="~/uc/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="cb1" %>
<%@ Register  Src="~/uc/PieMenu.ascx" TagName="PieMenu" TagPrefix="cb1" %>
<%@ Register  Src="~/uc/uc_Session.ascx" TagName="mensajeSession" TagPrefix="uc3" %>
<%@ Register Src="~/uc/HeaderMeta.ascx" TagPrefix="cb1" TagName="HeaderMeta" %>


<!DOCTYPE html>

<html>
<head>
    <cb1:HeaderMeta runat="server" ID="HeaderMeta" />
    <title>De mi equipo</title>
    <link href="../../../js/plugins/tablesorter/css/style.css" rel="stylesheet" type="text/css" />
    <link href="../../../js/plugins/jQueryUI/jquery-ui.min.css" rel="stylesheet" type="text/css" />
    
    <script>
         var strServer = "<% =Session["strServer"].ToString() %>";
        <%=idficepi%>
        <%=nombre%>
        <%=anyomesmin%>
        <%=filtrosDeMiEquipo%>
        <%=origen%>
    </script>

    <style>
        
        /*div.panel-body {
            max-height: 60vh;
            overflow: auto;
        }*/
        #tbdEval tr {
            cursor:pointer;
        }
        #tbdEval > tr.active {
            background-color: #D8D8D8 !important;
        }

        /*Tenemos que heredar la clase activa del padre(tr)*/
        #tbdEval > tr.active > td {
            background-color: inherit;
        }

        #cboProfundizacion, #cboEstado {
            width: 170px;
        }

        /*#spanEstado {
            margin-left : 49px;
        }*/

        #divNivel {
            margin-top:10px;
        }


        /*HEADER FIXED*/
        .header-fixed {
            width: 100%;
        }

        .header-fixed > thead,
        .header-fixed > tbody,
        .header-fixed > thead > tr,
        .header-fixed > tbody > tr,
        .header-fixed > thead > tr > th,
        .header-fixed > tbody > tr > td {
            display: block;
        }



        thead {
            width: 100%; /* scrollbar is average 1em/16px width, remove it from thead width */
        }

        .header-fixed > tbody > tr:after,
        .header-fixed > thead > tr:after {
            content: ' ';
            display: block;
            visibility: hidden;
            clear: both;
        }

        /*Alto del contenedor de la tabla (tbody)*/
        .header-fixed > tbody {
            overflow-y: auto;
            height: 30vh;
        }

        /*COLUMNA 1*/
        
         .header-fixed > tbody > tr > td:nth-child(1) {
            width: 5%;   
            float: left;
        }

        .header-fixed > thead > tr > th:nth-child(1) {
            width: 5%;  
            float: left;
        }
        /*COLUMNA 2*/
        .header-fixed > tbody > tr > td:nth-child(2) {
            width: 25%;   
            float: left;
        }

        .header-fixed > thead > tr > th:nth-child(2) {
            width: 25%;  
            float: left;
        }

        /*COLUMNA 3*/
        .header-fixed > tbody > tr > td:nth-child(3) {
            width: 25%;  
            float: left; 
        }

        .header-fixed > thead > tr > th:nth-child(3) {
            width: 25%;  
            float: left;
        }

        /*COLUMNA 4*/
        .header-fixed > tbody > tr > td:nth-child(4) {
            width: 20%;  
            float: left; 
        }

        .header-fixed > thead > tr > th:nth-child(4) {
            width: 20%;  
            float: left;
        }

        /*COLUMNA 5*/
        .header-fixed > tbody > tr > td:nth-child(5) {
            width: 10%;  
            float: left; 
        }

        .header-fixed > thead > tr > th:nth-child(5) {
            width: 10%;              
            float: left;
        }

        /*COLUMNA 6*/
        .header-fixed > tbody > tr > td:nth-child(6) {
            width: 10%;  
            float: left; 
        }

        .header-fixed > thead > tr > th:nth-child(6) {
            width: 10%;  
            float: left;
        }
         /*FIN HEADER FIXED*/

    </style>
</head>
<body data-codigopantalla="101">
    <uc3:mensajeSession ID="MensajeSession" runat="server" />
    <cb1:CabeceraMenu runat="server" ID="CabeceraMenu"></cb1:CabeceraMenu>
    
    <br class="hidden-xs" />
    <br class="hidden-xs" />
    
     <div class="container">

         <div class="row">
             <div class="col-xs-4 col-xs-offset-2">
                 
             </div>             
         </div>
         
         <div class="row">
             
             <div class="col-xs-12">
             <fieldset class="fieldset">

                 <legend class="fieldset">Filtros: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span>Para filtrar por personas, marca este check <input id="chkBuscarPersona" type="checkbox" /></span><button class="btn-xs btn-primary pull-right" id="btnRestablecer">Restablecer</button></legend>
                 
                 <div class="">
                     <div class="col-xs-7">
                         <fieldset id="fldPeriodo" class="fieldset W536">
                             <legend class="fieldset"><span class="spanFiltros">Período</span></legend>
                             <div class="col-xs-6">
                                 <select id="selMesIni" runat="server"></select>
                                 <select id="selAnoIni" runat="server"></select>
                             </div>
                             <div class="col-xs-6">
                                 <select id="selMesFin" runat="server"></select>
                                 <select id="selAnoFin" runat="server"></select>
                             </div>
                         </fieldset>
                     </div>



                     <div class="col-xs-5 text-right">
                         <div id="divNivel">
                             <span>Nivel de dependencia</span>

                             <select id="cboProfundizacion" runat="server">
                                 <option value="1">Primer nivel</option>
                                 <option value="2">Hasta segundo nivel</option>
                                 <option value="3">Todos los niveles</option>
                             </select>
                         </div>
                     </div>


                     <div class="col-xs-5 text-right" style="margin-top: 5px;">
                         <span id="spanEstado">Estado</span>
                         <select runat="server" id="cboEstado">
                             <option value="ABI">Abiertas</option>
                             <option value="CUR">En curso</option>
                             <option value="CER">Cerradas</option>                             
                         </select>
                     </div>

                 </div>

                 <div class="clearfix"></div>



                 <fieldset id="fldEvaluadorEvaluado" class="fieldset hide">
                     <legend class="fieldset">Para que se tenga en cuenta el período en las búsquedas por personas, marca este check <input id="chkPeriodo" type="checkbox" /></legend>
                     <div class="row">
                         <div class="col-xs-6" style="margin-top: 5px;">
                             <button id="lblEvaluado" style="margin-top: -3px" class="btn btn-link">Evaluado/a</button>
                             <input id="inputEvaluado" type="text" readonly="readonly" />
                             <i id="imgEvaluado" style="display: none" class="glyphicon glyphicon-repeat"></i>
                         </div>

                         <div class="col-xs-6" style="margin-top: 5px;">
                             <button id="lblEvaluador" style="margin-top: -3px" class="btn btn-link">Evaluador/a</button>
                             <input id="inputEvaluador" type="text" readonly="readonly" />
                             <i id="imgEvaluador" style="display: none" class="glyphicon glyphicon-repeat"></i>
                         </div>
                     </div>
                 </fieldset>



             </fieldset>
</div>
            

        </div>


         <div class="row">
             <div runat="server" id="checkMostrarEvalDesignacion" class="col-xs-12" style="visibility:hidden">
                 <span style="margin-left: 16px" id="">Mostrar evaluaciones</span>
                <input id="checkDesignacion" type="checkbox" style="display:none" />
                <select id="selectDesignacion">
                    <option value="A">de mi árbol de dependencias</option>
                    <option value="F">ajenas a mi árbol de dependencias</option>
                    <option value="T">de mi árbol de dependencias y ajenas</option>
                </select>
             </div>
         </div>

       
        <div class="row" style="margin-top:4px;">
            <div class="col-xs-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title" style="display:inline-block;margin-right:25px">Evaluaciones de mi equipo</h3>
                        (<span id="spanNumeroEvaluaciones"></span> <span id="spanTextoResultado"></span>)
                    </div>
                    <div class="panel-body">
                        <table id="tblEvalMiEquipo" class="table header-fixed tablesorter">
                            <thead>
                                <tr>
                                    <th>&nbsp;</th>                                    
                                    <th><span class="margenCabecerasOrdenables">Evaluado/a</span></th>
                                    <th><span class="margenCabecerasOrdenables">Evaluador/a</span></th>
                                    <th><span class="margenCabecerasOrdenables">Estado</span></th>
                                    <th <%--class="sorter-shortDate dateFormat-ddmmyyyy"--%>><span class="margenCabecerasOrdenables">Apertura</span></th>
                                    <th><span class="margenCabecerasOrdenables">Cierre</span></th>
                                </tr>
                            </thead>
                            <tbody runat="server" id="tbdEval">
                                
                            </tbody>
                        </table>

                    </div>
                </div>
            </div>
        </div>
         <div class="row">

             <div id="leyendaForzadas" class="col-xs-6" style="visibility:hidden">
                 <i class="glyphicon glyphicon-search rojo"></i>
                 <span>Evaluación ajena al árbol de dependencias</span>
             </div>

            
            <div class="col-xs-4 pull-right">
                <button type="button" class="btn btn-primary pull-right" id="btnAcceder" runat="server">Acceder a la evaluación</button>
            </div>
        </div>
    </div>




     <!--MODAL EVALUADOS-->
    <div class="modal fade" id="modal-evaluados">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <h4 class="modal-title">Selección de evaluado/a</h4>
                </div>
                <div class="modal-body">
                    
                    <div id="txtBusqueda" class="">
                        <div class="pull-left">
                             <label for="lblApellido1">Apellido 1º</label><br />
                                <input id="inputApellido1" type="text" />
                        </div>
                        <div class="pull-left" style="margin-left: 8px">
                             <label for="lblApellido2">Apellido 2º</label><br />
                                <input id="inputApellido2" type="text" />
                        </div>
                        <div class="pull-left" style="margin-left: 8px">
                            <label for="lblNombre">Nombre</label><br />
                            <input id="txtNombre" type="text" />
                        </div>
                        <div class="pull-right">
                            <button id="btnObtener" class="btn btn-primary btn-xs hide">Obtener</button>
                        </div>
                    </div>
                       

                    <br /><br />
                    <div class="row">
                        <div class="col-xs-12">
                            <ul class="list-group" runat="server" id="lisEvaluados">
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <b><button id="btnSeleccionar" class="btn btn-primary" style="display:none">Seleccionar</button></b>
                    <b><button id="btnCancelar" style="margin-left:7px" class="btn btn-default">Cancelar</button></b>                    
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->

    <!--FIN MODAL EVALUADS-->


     <!--MODAL EVALUADORES-->
    <div class="modal fade" id="modal-evaluadores">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <h4 class="modal-title">Selección de evaluadores</h4>
                </div>
                <div class="modal-body">
                    
                    <div id="txtBusquedaEvaluadores" class="">
                        <div class="pull-left">
                             <label for="lblApellido1">Apellido 1º</label><br />
                                <input id="inputApellido1Evaluador" type="text" />
                        </div>
                        <div class="pull-left" style="margin-left: 8px">
                             <label for="lblApellido2">Apellido 2º</label><br />
                                <input id="inputApellido2Evaluador" type="text" />
                        </div>
                        <div class="pull-left" style="margin-left: 8px">
                            <label for="lblNombre">Nombre</label><br />
                            <input id="txtNombreEvaluador" type="text" />
                        </div>
                        <div class="pull-right">
                            <button id="btnObtenerEvaluador" class="btn btn-primary btn-xs hide">Obtener</button>
                        </div>
                    </div>
                       

                    <br /><br />
                    <div class="row">
                        <div class="col-xs-12">
                            <ul class="list-group" runat="server" id="lisEvaluadores">
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <b><button id="btnSeleccionarEvaluador" class="btn btn-primary" style="display:none">Seleccionar</button></b>
                    <b><button id="btnCancelarEvaluador" class="btn btn-default">Cancelar</button></b>                    
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->

    <!--FIN MODAL EVALUADORES-->


</body>
</html>
<cb1:PieMenu runat="server" ID="PieMenu"></cb1:PieMenu>
<script type="text/javascript" src="../../../js/plugins/tablesorter/jquery.tablesorter.min.js"></script> 
<script type="text/javascript" src="../../../js/moment.locale.min.js"></script>
<script type="text/javascript" src="../../../js/procesando.js"></script>
<script type="text/javascript" src="../../../js/ios-orientationchange-fix.js"></script> 
