<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default01.aspx.cs" Inherits="Capa_Presentacion_Pruebas_Word_Default01" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
    function prueba02() {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/Pruebas/Word/Default03.aspx";
        $I("iFrmDocs").src = strEnlace;
        setTimeout("ocultarProcesando();", 5000);
    }
</script>
    <!--<asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" /><br />-->
    
    <input type="button" id="btnPrueba02" onclick="prueba02()" value="Prueba"/>
    
    <iframe name='iFrmDocs' id='iFrmDocs' src='' frameborder='no' width='10px' height='100x' style='visibility:hidden' />
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">

</asp:Content>

