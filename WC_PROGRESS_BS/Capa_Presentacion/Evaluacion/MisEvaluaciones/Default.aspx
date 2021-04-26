<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_MisEvaluaciones_Default" %>

<%@ Register  Src="~/uc/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="cb1" %>
<%@ Register  Src="~/uc/PieMenu.ascx" TagName="PieMenu" TagPrefix="cb1" %>
<%@ Register  Src="~/uc/uc_Session.ascx" TagName="mensajeSession" TagPrefix="uc3" %>
<%@ Register Src="~/uc/HeaderMeta.ascx" TagPrefix="cb1" TagName="HeaderMeta" %>


<!DOCTYPE html>

<html>
<head>
    <cb1:HeaderMeta runat="server" ID="HeaderMeta" />          
    <title>Mis evaluaciones</title>
    <link href="../../../js/plugins/tablesorter/css/style.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript">
        var strServer = "<%=Session["strServer"]%>";
         <%=filtros%>
    </script>
    <style>
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
    height: 40vh;
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

.header-fixed > tbody > tr > td:nth-child(2) {
    width: 45%;
    float: left;
}

.header-fixed > thead > tr > th:nth-child(2) {
    width: 45%;
    float: left;
}

/*COLUMNA 2*/
.header-fixed > tbody > tr > td:nth-child(3) {
    width: 20%;
    float: left;
}

.header-fixed > thead > tr > th:nth-child(3) {
    width: 20%;
    float: left;
}

/*COLUMNA 3*/
.header-fixed > tbody > tr > td:nth-child(4) {
    width: 15%;
    float: left;
}

.header-fixed > thead > tr > th:nth-child(4) {
    width: 15%;
    float: left;
}

/*COLUMNA 4*/
.header-fixed > tbody > tr > td:nth-child(5) {
    width: 15%;
    float: left;
}

.header-fixed > thead > tr > th:nth-child(5) {
    width: 15%;
    float: left;
}

#spanRol {
    margin-left:8px;
}

/*.iconoayuda {
    position:fixed;
    top:30%;
    z-index:5000;
    display:inline; 
    padding:5px;
    background:#428bca; 
    color:#FFF;
    cursor:pointer;
}*/
/*FIN HEADER FIXED*/

    </style>
</head>
<body data-codigopantalla="100">
     <uc3:mensajeSession ID="MensajeSession" runat="server" />
    <cb1:CabeceraMenu runat="server" ID="CabeceraMenu"></cb1:CabeceraMenu>
   
    <br class="hidden-xs" />
    <br class="hidden-xs" />
    
    <%--<div class="iconoayuda">        
        Ayuda
         <i class="fa fa-question " aria-hidden="true"></i>        
    </div>--%>

     <div class="container">
         <div class="row">
            <div class="col-xs-12">
                <div id="divRol" style="display:none" runat="server" class="pull-left">
                    <span>Mi rol actual:</span><span id="spanRol" runat="server"></span>
                </div>
                
                <div class="pull-right">
                    <span>Estado</span>
                    <select runat="server" id="cboEstado">
                        <option value="0">En curso</option>
                        <option value="1">Cerradas</option>
                        <option value="2">Todas</option>
                    </select>
                    
                </div>
                
            </div>
        </div>
         <br />

        
        <div class="row">
            <div class="col-xs-12">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title" style="display:inline-block;margin-right:25px">Mis evaluaciones</h3>
                        (<span id="spanNumeroEvaluaciones"></span> <span id="spanTextoResultado"></span>)
                    </div>
                    <div class="panel-body">
                        <table id="tableEval" class="table header-fixed table-hover tablesorter">
                            <thead>
                                <tr>
                                    <th>&nbsp;</th>
                                    <th><span class="margenCabecerasOrdenables"></span>Evaluador/a</th>
                                    <th><span class="margenCabecerasOrdenables"></span>Estado</th>
                                    <th><span class="margenCabecerasOrdenables"></span>Apertura</th>
                                    <th><span class="margenCabecerasOrdenables"></span>Cierre</th>
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
            
            <div class="col-xs-12">
                <button type="button" class="btn btn-primary pull-right" id="btnAcceder" runat="server">Acceder a la evaluación</button>
            </div>
        </div>

         <div class="hide" id="divSinDatos">
                <span id="txtSinEvaluaciones" style="font-size:1.4em"></span>
        </div>
    </div>
</body>
</html>
<cb1:PieMenu runat="server" ID="PieMenu"></cb1:PieMenu>
<script type="text/javascript" src="../../../js/plugins/tablesorter/jquery.tablesorter.min.js"></script> 
<script type="text/javascript" src="../../../js/ios-orientationchange-fix.js"></script> 

