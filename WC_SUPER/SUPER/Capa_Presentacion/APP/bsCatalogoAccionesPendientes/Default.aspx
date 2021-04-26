<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_bsCatalogoAccionesPendientes_Default" %>

<%@ Register Src="~/Capa_Presentacion/bsUserControls/Head.ascx" TagPrefix="uc1" TagName="Head" %>
<%@ Register Src="~/Capa_Presentacion/bsUserControls/Menu/Menu.ascx" TagPrefix="uc1" TagName="Menu" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <uc1:Head runat="server" ID="Head" />
    <title></title>
</head>
<body>
    <uc1:Menu runat="server" ID="Menu" />
    <form id="form1" runat="server">
    <div class="container">
        <div class="col-sm-12">
            <h4>Catálogo de acciones pendientes</h4>
            <table class="table table-bordered">
                <thead class="bg-primary">
                    <tr>
                        <th>Acción</th>
                    </tr>
                    
                </thead>
                <tbody id="tbdAcciones">
                    
                </tbody>
            </table>
        </div>
    
    </div>
    </form>
    <script src="../../bsInicio/js/AccionesPendientesView.js"></script>    
    <script src="js/view.js"></script>
    <script src="js/app.js"></script>
</body>
</html>
