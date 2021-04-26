<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Administracion_Mantenimientos_CategoriaColectivo_Default" %>

<%@ Register Src="~/uc/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="cb1" %>
<%@ Register Src="~/uc/PieMenu.ascx" TagName="PieMenu" TagPrefix="cb1" %>
<%@ Register Src="~/uc/uc_Session.ascx" TagName="mensajeSession" TagPrefix="uc3" %>
<%@ Register Src="~/uc/HeaderMeta.ascx" TagPrefix="cb1" TagName="HeaderMeta" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <cb1:HeaderMeta runat="server" ID="HeaderMeta" />
    <title></title>
    <link href="../../../../js/plugins/tablesorter/css/style.css" rel="stylesheet" type="text/css" />
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
            height: 300px;
        }

        /*COLUMNA 1*/
        .header-fixed > tbody > tr > td:nth-child(1) {
            width: 50%;   
            float: left;
        }

        .header-fixed > thead > tr > th:nth-child(1) {
            width: 50%;  
            float: left;
        }

        /*COLUMNA 2*/
        .header-fixed > tbody > tr > td:nth-child(2) {
            width: 50%;  
            float: left; 
        }

        .header-fixed > thead > tr > th:nth-child(2) {
            width: 50%;  
            float: left;
        }

       
         /*FIN HEADER FIXED*/
    </style>
</head>
<body data-codigopantalla="413">
    <uc3:mensajeSession ID="MensajeSession" runat="server" />
    <cb1:CabeceraMenu runat="server" ID="CabeceraMenu"></cb1:CabeceraMenu>

    <br />
    <br class="hidden-xs" />
    <br class="hidden-xs" />

    <div class="container">
        <div class="row">
            <div class="col-xs-10 col-xs-offset-1">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">Relación entre categoría profesional y colectivo Progress
                    </div>
                    <div class="panel-body">
                        <table id="tableEval" class="table table-hover header-fixed tablesorter">
                            <thead>
                                <tr>
                                    <th><span class=""></span>Categoría</th>
                                    <th><span class=""></span>Colectivo</th>
                                </tr>
                            </thead>
                            <tbody id="tbdCategoriaColectivo">
                             
                            </tbody>
                        </table>

                    </div>
                </div>
            </div>
        </div>
    </div>

</body>
</html>
<cb1:PieMenu runat="server" ID="PieMenu"></cb1:PieMenu>
<script type="text/javascript" src="../../../../js/plugins/tablesorter/jquery.tablesorter.min.js"></script> 
<script type="text/javascript" src="../../../../js/plugins/tablesorter/parser.js"></script>
