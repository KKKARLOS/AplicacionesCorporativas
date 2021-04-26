<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_SIC_Test_bsconfirm_Default" %>

<%@ Register Src="~/Capa_Presentacion/bsUserControls/Head.ascx" TagPrefix="uc1" TagName="Head" %>
<%@ Register Src="~/Capa_Presentacion/bsUserControls/Menu/Menu.ascx" TagPrefix="uc1" TagName="Menu" %>




<!DOCTYPE html>

<html>
<head>
    <uc1:Head runat="server" ID="Head" />

</head>


<body>
    <uc1:Menu runat="server" ID="Menu" />

    <form id="form1" runat="server"></form>
    <input type="button" id="btn1" value="bsConfirm" />
    <script src="app.js"></script>
    

</body>
</html>



