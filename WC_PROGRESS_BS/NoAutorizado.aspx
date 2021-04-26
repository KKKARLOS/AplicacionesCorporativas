<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NoAutorizado.aspx.cs" Inherits="NoAutorizado" %>

<%@ Register Src="~/uc/SoloCabecera.ascx" TagPrefix="uc1" TagName="SoloCabecera" %>
<%@ Register Src="~/uc/HeaderMeta.ascx" TagPrefix="uc1" TagName="HeaderMeta" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <uc1:HeaderMeta runat="server" ID="HeaderMeta" />
</head>
<body>


    <uc1:SoloCabecera runat="server" ID="SoloCabecera" /><br /><br />

    <form id="form1" runat="server">
    <div class="container">
        <div class="row cajaTexto">
            <!--Error de aplicación-->
            <div id="Error" runat="server">
                <i class="fa fa-exclamation-triangle fa-5x pull-right text-danger"></i>
                <h3>No autorizado</h3>

                <br />
                <p>
                    Has intentado acceder a un área restringida. Si crees que es un error de la aplicación, contacta con el CAU. </br>
                    Gracias.
                </p>
            </div>

           
        </div>
    </div>
</form>
</body>
</html>
