<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Ayuda_Contacto_Default" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
	<script language="javascript" type="text/javascript">
	<!--
	    function enviar() {
	        var sPantalla = strServer + "Capa_Presentacion/Ayuda/Contacto/Mensaje/Default.aspx";
	        var sEstadoAct;
	        modalDialog.Show(sPantalla, self, sSize(650, 530))
            .then(function(ret) {
                if (ret != null)
                    sEstadoAct = ret;
            });
	    }
	-->
    </script>
<div style="Z-INDEX: 1; VISIBILITY: visible; WIDTH: 125px; POSITION: absolute; TOP: 180px;  LEFT: 435px ;HEIGHT: 121px"><asp:Image ID="imgAqui" runat="server" Height="121" Width="125" ImageUrl="~/images/imgAqui.gif" /></DIV>
<br /><br /><br />
<center>
<div style="width:500px;" class="texto" align="left">
Desde esta opción, Ud. puede enviar un mensaje al administrador de SUPER. En caso de tratarse de un error de aplicación, le rogamos anexe un pantallazo del error producido.<br /><br />
<br /><br />Para escribir el mensaje, pinche <label class='enlace' onclick='enviar();'>aquí</label>. 
</div>
</center>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
</asp:Content>

