<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Administracion_AdmCambioRol_Default" %>

<%@ Register  Src="~/uc/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="cb1" %>
<%@ Register  Src="~/uc/PieMenu.ascx" TagName="PieMenu" TagPrefix="cb1" %>
<%@ Register  Src="~/uc/uc_Session.ascx" TagName="mensajeSession" TagPrefix="uc3" %>
<%@ Register Src="~/uc/HeaderMeta.ascx" TagPrefix="cb1" TagName="HeaderMeta" %>


<!DOCTYPE html>

<html>
<head>
    <cb1:HeaderMeta runat="server" ID="HeaderMeta" />
    <title>Cuadro de Mando de cambio de Rol</title>
    <link href="../../../js/plugins/tablesorter/css/style.css" rel="stylesheet" type="text/css" />
    
    <script>
        var strServer = "<% =Session["strServer"].ToString() %>";      
    </script>
  
</head>
<body>
    <uc3:mensajeSession ID="MensajeSession" runat="server" />
    <cb1:CabeceraMenu runat="server" ID="CabeceraMenu"></cb1:CabeceraMenu>
    
    <br class="hidden-xs" />
    <br class="hidden-xs" />
    <br /><br /><br />

    <div class="container">
        <!-- Cuadro de mando Cambio de roles -->
        <div class="tile pull-left">
            <span>
                <a href="SolicitudesAprobadas/Default.aspx">Solicitudes aprobadas pendientes de RRHH</a>
            </span> 
            <span id="numSolAprobadas" class="num"></span>
        </div>

        <div class="tile pull-right">
            <span>
                <a href="SolicitudesNoAprobadas/Default.aspx">Solicitudes rechazadas pendientes de RRHH</a>
            </span>
            <span id="numSolNoAprobadas" class="num"></span></div>

        <div class="clearfix"></div>
        <hr />

        <div class="tile pull-left">
            <span>
                <a href="SolicitudesAprobadasStandby/Default.aspx">Solicitudes aprobadas en Stand by, pendientes de RRHH</a>
            </span>
            <span id="numSolPendAprobacion" class="num"></span>
        </div>        

        <div class="tile pull-right">
            <span>
                <a href="SolicitudesNoAprobadasStandby/Default.aspx">Solicitudes rechazadas en Stand by, pendientes de RRHH</a>
            </span>
            <span id="numSolPendNoAprobacion" class="num"></span>
        </div>

        <div class="clearfix"></div>
    </div>

</body>
</html>
<cb1:PieMenu runat="server" ID="PieMenu"></cb1:PieMenu>
<script type="text/javascript" src="../../../js/plugins/tablesorter/jquery.tablesorter.min.js"></script> 
<script type="text/javascript" src="../../../js/moment.locale.min.js"></script>


 

 