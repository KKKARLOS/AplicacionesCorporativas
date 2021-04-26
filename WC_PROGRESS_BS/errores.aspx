<%@ Page Language="C#" AutoEventWireup="true" CodeFile="errores.aspx.cs" Inherits="errores" %>

<%@ Register Src="~/uc/SoloCabecera.ascx" TagPrefix="uc1" TagName="SoloCabecera" %>
<%@ Register Src="~/uc/HeaderMeta.ascx" TagPrefix="uc1" TagName="HeaderMeta" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <uc1:HeaderMeta runat="server" ID="HeaderMeta" />
    <title></title>
</head>
<body>
    <br /><br /><br />
    
    <uc1:SoloCabecera runat="server" ID="SoloCabecera" />
    <form id="form1" runat="server">
    <div class="container">
        <div class="row cajaTexto">
            <!--Error de aplicación-->
            <div id="Error" runat="server" style="display:none">
                <i class="fa fa-exclamation-triangle fa-5x pull-right text-danger"></i>
                <h3>Se ha producido un error en la aplicación.</h3>

                <br />
                <p>
                    Inténtelo de nuevo en unos momentos y si el problema persiste póngase en contacto con el CAU.
                </p>
            </div>

            <!--Error de Caducidad de Sesión-->
            <div id="errorSesion" runat="server" style="display: none">
                <i class="fa fa-clock-o fa-5x pull-right text-danger"></i>
                <h3>Su sesión ha caducado. </h3>
                <h4>Haga click <a href="/default.aspx">aquí</a> para volver a reconectarse.</h4>
            </div>
        </div>
    </div>
</form>
</body>
</html>
