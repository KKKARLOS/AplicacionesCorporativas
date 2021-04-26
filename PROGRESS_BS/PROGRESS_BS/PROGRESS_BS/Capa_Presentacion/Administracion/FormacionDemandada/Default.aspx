<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Administracion_FormacionDemandada_Default" %>

<%@ Register Src="~/uc/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="cb1" %>
<%@ Register Src="~/uc/PieMenu.ascx" TagName="PieMenu" TagPrefix="cb1" %>
<%@ Register  Src="~/uc/uc_Session.ascx" TagName="mensajeSession" TagPrefix="uc3" %>
<%@ Register Src="~/uc/HeaderMeta.ascx" TagPrefix="cb1" TagName="HeaderMeta" %>


<!DOCTYPE html>

<html>
<head>
    <cb1:HeaderMeta runat="server" ID="HeaderMeta" />    
    <title>Formación demandada</title>
    <link href="../../../js/plugins/tablesorter/css/style.css" rel="stylesheet" type="text/css" />
    <link href="../../../js/plugins/jQueryUI/jquery-ui.min.css" rel="stylesheet" type="text/css" />

    <script>
        var strServer = "<% =Session["strServer"].ToString() %>";
        <%=idficepi%>
        <%=nombre%>
        <%=filtrosFormacionDemandada%>
    </script>

    <style>
       
        /*FIN HEADER FIXED*/
    </style>
</head>
<body data-codigopantalla="406">
    <uc3:mensajeSession ID="MensajeSession" runat="server" />
    <cb1:CabeceraMenu runat="server" ID="CabeceraMenu"></cb1:CabeceraMenu>
    
    <br class="hidden-xs" />
    <br class="hidden-xs" />

    <div class="container">
        <div class="row">

            <div class="col-xs-12">
                <fieldset class="fieldset">

                    <legend class="fieldset">Filtros</legend>

                    <div class="">
                        <div class="col-xs-7">
                            <fieldset class="fieldset">
                                <legend class="fieldset"><span>Período</span></legend>
                                <div class="col-xs-6">
                                    <select id="selMesIni" runat="server" onchange="cargarFormacionDemandada()"></select>
                                    <select id="selAnoIni" runat="server" onchange="cargarFormacionDemandada()"></select>
                                </div>
                                <div class="col-xs-6">
                                    <select id="selMesFin" runat="server" onchange="cargarFormacionDemandada()"></select>
                                    <select id="selAnoFin" runat="server" onchange="cargarFormacionDemandada()"></select>
                                </div>
                            </fieldset>
                        </div>



                        <div class="col-xs-4 col-xs-offset-1">
                            <div id="divColectivo">
                                <span>Colectivo</span>
                                <select id="cboColectivo" runat="server" onchange="cargarFormacionDemandada()">
                                    <option value="0">Todos</option>
                                </select>
                            </div>
                        </div>

                    </div>


                </fieldset>
            </div>


        </div>

        <br />


        <button id="btnExportExcel" class="btn btn-primary btn-xs pull-right" style="margin-bottom:5px">Exportar a Excel</button> 
        <div class="clearfix"></div>

        <div class="row">
            <div class="col-xs-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title" style="display:inline-block; margin-right:15px">Formación demandada</h3>
                        (Nº de evaluaciones: <label id="numEvaluaciones" style="font-weight:normal"></label>)
                    </div>
                    <div class="panel-body">
                        <table id="tablaFormacion" class="table header-fixed tablesorter">
                            <thead>
                                <tr>
                                    <th>&nbsp;</th>
                                    <th><span class="margenCabecerasOrdenables">Evaluador/a</span></th>
                                    <th><span class="margenCabecerasOrdenables">Evaluado/a</span></th>
                                    <th><span>Formación</span></th>
                                </tr>
                            </thead>
                            <tbody runat="server" id="tbdFormacion">
                            </tbody>
                        </table>

                    </div>
                </div>
            </div>
        </div>

        <div class="">

          <%--  <div class="pull-left">
                <span>Nº de evaluaciones en las que se solicita formación:</span>
                <label id="numEvaluaciones"></label>

            </div>--%>
            <div class="pull-right">
                <button type="button" class="btn btn-primary" id="btnAcceder" runat="server">Acceder a la evaluación</button>
            </div>
        </div>


    </div>

    <iframe width="0" height="0" id="ifrmExportExcel" style="display:none">

    </iframe>
</body>
</html>
<cb1:PieMenu runat="server" ID="PieMenu"></cb1:PieMenu>
<script type="text/javascript" src="../../../js/plugins/tablesorter/jquery.tablesorter.min.js"></script>
<script type="text/javascript" src="../../../js/procesando.js"></script>





