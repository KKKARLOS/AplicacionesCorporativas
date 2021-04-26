<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Pruebas_Msg_Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript" language="javascript">
    function mostrarMMOff(nOpcion) {
        switch (nOpcion) {
            case 1: mmoff2("Inf", "Texto a título informativo", 300); break;
            case 2: mmoff2("War", "Texto de alerta: validaciones erróneas, etc.", 300); break;
            case 3: mmoff2("Suc", "Grabaciones o procesos correctos", 300); break;
            case 4: mmoff2("Err", "Mensajes de error", 200); break;
            case 5: mmoff2("Inf", "Texto a título informativo", 300, null, null, 500, 200); break;
            case 6: mmoff2("Err", "Texto a título informativo largo. Texto a título informativo largo. <br>Texto a título informativo largo. Texto a título informativo largo. <br>Texto a título informativo largo. Texto a título informativo largo. <br>Texto a título informativo largo. Texto a título informativo largo. <br>Texto a título informativo largo. Texto a título informativo largo. <br>", 400, 5000); break;
        }
    }
</script>
<label style="cursor:pointer;" onclick="mostrarMMOff(1)">Informativo</label><br /><br />
<label style="cursor:pointer;" onclick="mostrarMMOff(2)">Warning / Alerta</label><br /><br />
<label style="cursor:pointer;" onclick="mostrarMMOff(3)">Éxito</label><br /><br />
<label style="cursor:pointer;" onclick="mostrarMMOff(4)">Error</label><br /><br />
<label style="cursor:pointer;" onclick="mostrarMMOff(5)">Texto en posición determinada</label><br /><br />
<label style="cursor:pointer;" onclick="mostrarMMOff(6)">Texto largo durante 5 segundos.</label><br /><br />


<uc1:mmoff ID="mmoff1" runat="server" />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
</asp:Content>

