<%@ Page Language="C#" AutoEventWireup="true" CodeFile="reconexion.aspx.cs" Inherits="Default2" %>
<%@ Register Src="~/uc/SoloCabecera.ascx" TagPrefix="uc1" TagName="SoloCabecera" %>
<%@ Register Src="~/uc/HeaderMeta.ascx" TagPrefix="uc1" TagName="HeaderMeta" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <uc1:HeaderMeta runat="server" ID="HeaderMeta" />
    <title></title>
</head>
<body>
  <uc1:SoloCabecera runat="server" ID="SoloCabecera" /><br /><br />
  <form id="form1" runat="server">
    <div class="container">
        <div class="row cajaTexto">            
            <div>
                <i class="fa fa-clock-o fa-5x pull-right text-danger"></i>
                <h3>Tu sesión ha caducado. </h3>
                <h4>Haz click <a href="Default.aspx">aquí</a> para volver a conectarte.</h4>
            </div>
        </div>
    </div>
</form>
</body>
</html>
