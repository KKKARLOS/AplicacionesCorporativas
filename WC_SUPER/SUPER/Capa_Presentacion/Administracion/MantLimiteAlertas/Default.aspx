<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="LIMITEALERTAS" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content runat="server" ContentPlaceHolderID="CPHB" ID="CPHBotonera">
</asp:Content>
<asp:Content runat="Server" ContentPlaceHolderID="CPHC" ID="CPHContenido">
	<script type="text/javascript">
	<!--
	    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";   
	-->
	</script>
<style type="text/css">

#tblDatos tr { height:20px; }
#tblDatos td {
	border-collapse: separate;
    border-spacing: 0px;
	border: 1px solid #A6C3D2;
	padding: 0px 2px 0px 2px;
}

</style>
<center>
    <table style="width:260px;text-align:left">
        <tr>
            <td align="center">
                <label style="margin-top:10px" class="label">Fecha límite de respuesta para<br />diálogos de alertas de proyecto.</label>
            </td>
        </tr>     
	    <tr>
	        <td>
                <table id="tblTitulo" style="width: 265px; height: 17px; margin-top:15px">
                    <colgroup>
                        <col style='width:190px' />
                        <col style='width:75px' />
                    </colgroup>    
                    <tr class="TBLINI">
                        <td align="center">Mes diálogo</td>
                        <td align="center" title="Fecha límite de respuesta">F.L.R.</td>
                    </tr>
                </table>
                <div id="divCatalogo" style="overflow-y: auto; overflow-x:hidden; width: 281px; height:460px">
                    <div style="background-image:url(<%=Session["strServer"]%>Images/imgFT20.gif); width:265px">
                     <%=strTabla%>
                    </div>
                </div>
                <table style="width: 265px;">
                    <tr class="TBLFIN">
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>    
</center>
<asp:textbox id="hdnFecAnnoMesActual" runat="server" style="visibility:hidden"></asp:textbox>   
<uc_mmoff:mmoff ID="mmoff1" runat="server" />

</asp:Content>
<asp:Content ID="CPHDoPostBack" runat="server" ContentPlaceHolderID="CPHD">
<script type="text/javascript">
<!--
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();
	        //alert("strBoton: "+ strBoton);
			switch (strBoton){			
				case "grabar": 
				{
					bEnviar = false;
					grabar();
					break;
				}
			}
		}

		var theform = document.forms[0];
		theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
		theform.__EVENTARGUMENT.value = eventArgument;
		if (bEnviar) {
		    theform.submit();
		}
	}
-->
</script>
</asp:Content>

