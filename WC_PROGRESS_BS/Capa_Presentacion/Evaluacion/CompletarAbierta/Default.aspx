<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Evaluacion_CompletarAbierta_Default" %>

<%@ Register Src="~/uc/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="cb1" %>
<%@ Register Src="~/uc/PieMenu.ascx" TagName="PieMenu" TagPrefix="cb1" %>
<%@ Register  Src="~/uc/uc_Session.ascx" TagName="mensajeSession" TagPrefix="uc3" %>
<%@ Register Src="~/uc/HeaderMeta.ascx" TagPrefix="cb1" TagName="HeaderMeta" %>

<!DOCTYPE html>

<html>
<head>
    <cb1:HeaderMeta runat="server" ID="HeaderMeta" />
    <title>Completar abiertas</title>
    <link href="../../../js/plugins/tablesorter/css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        var strServer = "<%=Session["strServer"]%>";
        <%=nombre%>     
    </script>

    <style>
        .table > thead > tr > th, .table > tbody > tr > th, .table > tfoot > tr > th, .table > thead > tr > td, .table > tbody > tr > td, .table > tfoot > tr > td {
            padding: 6px !important;
        }
    </style>
</head>
<body data-codigopantalla="103">
    <uc3:mensajeSession ID="MensajeSession" runat="server" />
    <cb1:CabeceraMenu runat="server" ID="CabeceraMenu"></cb1:CabeceraMenu>
    <br />
    <br class="hidden-xs" />
    <br class="hidden-xs" />
    <form runat="server"></form>    
     <div class="container">
        <div class="row">
            <div id="divtablaProf" class="col-xs-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">Evaluaciones abiertas pendientes de completar</h3>
                    </div>
                    <div class="panel-body">
                        <table id="tablaProf" class="table table-hover tablesorter header-fixed">
                            <thead>
                                <tr>
                                    <th>&nbsp;</th>
                                    <th><span class="margenCabecerasOrdenables">Evaluado/a</span></th>
                                    <th><span class="margenCabecerasOrdenables">Apertura</span></th>
                                </tr>
                            </thead>
                            <tbody runat="server" id="tbdProf">
                                
                            </tbody>
                        </table>

                    </div>
                </div>
            </div>
        </div>
         <div class="row">
            <div class="col-xs-12">
                <button id="btnEliminar" type="button" class="btn btn-default pull-right">Eliminar la evaluación</button>



                <button type="button" class="btn btn-primary pull-right">Acceder a la evaluación</button>
            </div>
        </div>

          <div class="hide" id="divSinDatos">
                <span style="font-size:1.4em">No tienes evaluaciones abiertas para profesionales de tu equipo</span>
        </div>

    </div>


        <div class="modal fade" id="modal-confirmacion-Eliminacion">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header btn-primary">
                    <h4 class="modal-title">Confirmación de eliminación</h4>
                </div>
                <div class="modal-body">                    
                    <div class="row">
                        <div class="col-xs-12">
                            <span id="txtNombreEvaluador"></span>
                            <span style="margin-left:-5px">, has pulsado el botón 'Eliminar evaluación'. Pulsa 'Sí' para confirmar la eliminación.</span>                            
                            <br />                            
                        </div>
                    </div>
                    <br /><br />
                    <div class="row">
                        <div class="col-xs-12">
                            <input id="chkAvisar" type="checkbox" checked="checked" />
                            Comunicar la eliminación a 
                            <span id="txtNombreEvaluado"></span>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <b><button id="btnOkEnvio" class="btn btn-primary" data-toggle="modal" data-backdrop="static" data-keyboard="false">Sí</button></b>                    
                    <b><button id="btnNoEnvio" class="btn btn-default" data-toggle="modal" data-backdrop="static" data-keyboard="false">No</button></b>                    
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>




</body>
</html>
<cb1:PieMenu runat="server" ID="PieMenu"></cb1:PieMenu>
<script type="text/javascript" src="../../../js/plugins/tablesorter/jquery.tablesorter.min.js"></script> 
