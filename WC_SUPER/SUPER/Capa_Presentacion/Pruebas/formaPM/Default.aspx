<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Pruebas_formaPM_Default" %>

<%@ Register Src="~/Capa_Presentacion/bsUserControls/Head.ascx" TagPrefix="uc1" TagName="Head" %>


<!DOCTYPE html>

<html>
<head>
    <uc1:Head runat="server" ID="Head" />
    <title>Formación patrón modular</title>
</head>


<body>
    <form runat="server"></form>
    <div class="container">
        <div class="row">
            <input type="text" id="txtfiltro" /><input type="button" value="Buscar" id="btnBuscar"/>
        </div>

        <br /><br />

        <div class="row">

            <table>

                <tbody id="contenido">
                    

                </tbody>

            </table>
            <br /><br />
            <input type="button" id="btnLimpiar" value="Limpiar" />
        </div>
    </div>

    
<%--<script src="js/javascriptJQ.js"></script>--%>

    <script src="js/models.js"></script>
    <script src="js/view.js"></script>
    <script src="js/app.js"></script>
    
</body>
</html>



