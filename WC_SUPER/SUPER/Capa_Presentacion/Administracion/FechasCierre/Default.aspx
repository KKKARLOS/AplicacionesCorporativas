<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Administradores" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content runat="server" ContentPlaceHolderID="CPHB" ID="CPHBotonera">
</asp:Content>
<asp:Content runat="Server" ContentPlaceHolderID="CPHC" ID="CPHContenido">
<script language="javascript" type="text/javascript">
    //Para el comportamiento de los calendarios
    var btnCal = "<%=Session["BTN_FECHA"].ToString() %>";
</script>
<br /><br />
<fieldset style="width: 140px; height:50px; margin-left:257px;">
    <legend>Año</legend>
    <table style="margin-top:7px;">
    <tr>
        <td><center>
            <img title="Año anterior" id="btnAnnoAnt" onclick="setAnno('A')" src="../../../Images/btnAntRegOff.gif" style="cursor: pointer;vertical-align:middle; margin-left:25px;" />
            <asp:TextBox id="txtAnno" style="width:32px; text-align:center;vertical-align:middle;" readonly="true" runat="server" Text=""></asp:TextBox>
            <img title="Siguiente año" id="btnAnnoSig" onclick="setAnno('S')" src="../../../Images/btnSigRegOff.gif" style="cursor: pointer;vertical-align:middle;" />
        </td>
    </tr>
    </table>
</fieldset>
<br /><br />
<center>
    <table style="width:481px;text-align:left">
	    <tr>
	        <td>
                <table id="tblTitulo" style="width: 455px; height: 17px; margin-top:15px">
                    <colgroup>
                            <col style='width:15px;' />
                            <col style='width:100px;' />
                            <col style='width:160px;' />
                            <col style='width:90px;' />
                            <col style='width:90px;' />
                    </colgroup>    
                    <tr class="TBLINI" style="text-align:center;">
                        <td></td>
                        <td style="text-align:left; margin-left:2px;">Mes</td>
                        <td title="Fecha límite para la generación de órdenes de facturación">L.O.F.</td>
                        <td title="Fecha límite de respuesta de los diálogos de alertas de producción">F.L.R.</td>
                        <td title="Fecha prevista de cierre económico de empresa">F.P.C.E.</td>
                    </tr>
                </table>
                <div id="divCatalogo" style="overflow:hidden; width: 471px; height:264px">
                    <div style="background-image:url(<%=Session["strServer"]%>Images/imgFT22.gif); width:455px">
                     <%//=strTabla%>
                    </div>
                </div>
                <table style="width: 455px;">
                    <tr class="TBLFIN">
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>    
</center>
</asp:Content>
<asp:Content ID="CPHDoPostBack" runat="server" ContentPlaceHolderID="CPHD">
<script type="text/javascript">

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
		if (bEnviar) theform.submit();

	}

</script>
</asp:Content>

