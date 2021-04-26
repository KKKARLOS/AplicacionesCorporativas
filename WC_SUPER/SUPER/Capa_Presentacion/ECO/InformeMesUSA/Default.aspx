<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<asp:Content runat="server" ContentPlaceHolderID="CPHB" ID="CPHBotonera">
</asp:Content>
<asp:Content runat="Server" ContentPlaceHolderID="CPHC" ID="CPHContenido">
	<script type="text/javascript">
	
	    var nAnoMesActual = <%=iAnoMes %>;
	    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";

	    //SSRS
        var servidorSSRS ="<%=Session["ServidorSSRS"]%>";
	    //SSRS

	</script>
    <center>
        <TABLE style="width: 970px; text-align:left">
        <tr>
        <td width="200px" align=left>
            <img title="Mes anterior" onclick="cambiarMes(-1)" src="../../../Images/btnAntRegOff.gif" style="cursor: pointer;vertical-align:bottom" />
            <asp:TextBox ID="txtMesVisible" style="width:90px; text-align:center; vertical-align:super" readonly="true" runat="server" Text=""></asp:TextBox>
            <img title="Siguiente mes" onclick="cambiarMes(1)" src="../../../Images/btnSigRegOff.gif" style="cursor: pointer;vertical-align:bottom" />                    
        </td>                                    
        <td align="right"> 
        <img src="../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos')" />&nbsp;<img src="../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos')" />
        </td>
        </tr>
        </TABLE>                
        <TABLE id="tblCatIni" style="width: 970px; HEIGHT: 17px">
            <TR class="TBLINI">
                <td style="width:70px;padding-left:10px">Nº</td>
                <td style="width:300px">Proyecto</td>   
			    <td style="width:300px">Cliente</td>   					                       
			    <td style="width:300px">Centro responsabilidad</td>  					                       
            </TR>
        </TABLE>
        <div id="divCatalogo" style="OVERFLOW: auto; OVERFLOW-X: hidden; WIDTH: 986px; height:480px" runat="server">
            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:970px">
	        <%=strTablaHTML%>
            </div>
        </div>
        <TABLE style="width: 970px; HEIGHT: 17px">
            <TR class="TBLFIN">
                <TD></TD>
            </TR>
        </TABLE>              
    </center>
<asp:TextBox ID="FORMATO" runat="server" style="visibility:hidden" Text="PDF" />
<asp:TextBox ID="hdnIDS" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnAnoMes" runat="server" style="visibility:hidden" Text="" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>
<asp:Content ID="CPHDoPostBack" runat="server" ContentPlaceHolderID="CPHD">
<script type="text/javascript">
<!--
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();

			switch (strBoton){
				case "pdf": //Boton exportar pdf
				{
					bEnviar = false;
					Exportar();
					break;
				}
				case "excel":
				{
					bEnviar = false;
					ExportarExcel();
					break;
				}
			}
		}

		var theform = document.forms[0];
		theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
		theform.__EVENTARGUMENT.value = eventArgument;
		if (bEnviar) theform.submit();

	}

    function WebForm_CallbackComplete() {
        for (var i = 0; i < __pendingCallbacks.length; i++) {
            callbackObject = __pendingCallbacks[i];
            if (callbackObject && callbackObject.xmlRequest && (callbackObject.xmlRequest.readyState == 4)) {
                WebForm_ExecuteCallback(callbackObject);
                if (!__pendingCallbacks[i].async) {
                    __synchronousCallBackIndex = -1;
                }
                __pendingCallbacks[i] = null;
                var callbackFrameID = "__CALLBACKFRAME" + i;
                var xmlRequestFrame = document.getElementById(callbackFrameID);
                if (xmlRequestFrame) {
                    xmlRequestFrame.parentNode.removeChild(xmlRequestFrame);
                }
            }
        }
    }
-->
</script>
<script src="<% =Session["strServer"].ToString() %>scripts/ssrs.js?v=23/04/2018"></script>
</asp:Content>
