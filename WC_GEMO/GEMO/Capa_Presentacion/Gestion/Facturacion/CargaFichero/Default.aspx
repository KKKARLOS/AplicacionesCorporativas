<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
<!--
-->
</script>
<br /><br /> 
<table  width="870px"  cellSpacing="0" cellPadding="0" border="0" align=center>
    <tr>
        <td>
            <table border="0" cellspacing="0" cellpadding="0" align="center">
              <tr>
                <td width="6" height="6" background="../../../../Images/Tabla/7.gif"></td>
                <td height="6" background="../../../../Images/Tabla/8.gif"></td>
                <td width="6" height="6" background="../../../../Images/Tabla/9.gif"></td>
              </tr>
              <tr>
                <td width="6" background="../../../../Images/Tabla/4.gif">&nbsp;</td>
                <td background="../../../../Images/Tabla/5.gif" style="padding:5px">
                    <table id="tblDatos2" class="texto" border="0" cellspacing="7" cellpadding="5" width="830px">
                        <colgroup>
                            <col style="width:270px;" />
                            <col style="width:50px; text-align:right;" />
                            <col style="width:170px;" />
                            <col style="width:150px;" />
                            <col style="width:50px; text-align:right;" />
                            <col style="width:140px;" />
                        </colgroup>
                        <!--
                        -->
                        <tr>
		                    <td colspan="5" style="vertical-align:middle">Fichero&nbsp;&nbsp;
		                        <input id="uplTheFile" type="file" name="uplTheFile" size="100" class="txtIF" runat="server" onchange="HabCarga();return;LeerFichero(this.value);" contenteditable="false">
		                    </td>
                            <td>
                                <button id="btnSubir" type="button" disabled onclick='LeerFichero($I("uplTheFile").value);' class="btnH25W105" hidefocus=hidefocus onmouseover="se(this, 25);mostrarCursor(this);" runat="server">
									<img src="../../../../images/imgSubir.gif" /><span title="Carga el fichero">&nbsp;Cargar</span>
                                </button>
                            </td>
                        </tr>	
                        <tr>                       
                            <td colspan="5">
                                <div id="panel" visible=false Runat="server">Fichero subido correctamente.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <label class="titulo">Nombre:</label>&nbsp;&nbsp;<asp:Label id="lblFileName" Runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                <label class="titulo">Tamaño(bytes):</label>&nbsp;&nbsp;<asp:Label id="lblFileLength" Runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;<label class="titulo">Tipo:</label>&nbsp;&nbsp;<asp:Label id="lblFileType" Runat="server"></asp:Label>
                                </div>
                            </td>
                            <td>
                                <button id="btnCronologia" type="button" disabled onclick="getCronologia()" class="btnH25W105" hidefocus=hidefocus onmouseover="se(this, 25);mostrarCursor(this);" runat="server">
									<img src="../../../../images/imgHorario.gif" /><span title="Cronología de cargas">&nbsp;Cronología</span>
                                </button>
                            </td>		
                        </tr>
                    </table>
                </td>
                <td width="6" background="../../../../Images/Tabla/6.gif">&nbsp;</td>
              </tr>
              <tr>
                <td width="6" height="6" background="../../../../Images/Tabla/1.gif"></td>
                <td height="6" background="../../../../Images/Tabla/2.gif"></td>
                <td width="6" height="6" background="../../../../Images/Tabla/3.gif"></td>
              </tr>
            </table>
            <br />
        </td>
    </tr>
</TABLE>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
<!--
    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();
			//alert("strBoton: "+ strBoton);
			switch (strBoton){
				case "procesar": 
				{
                    bEnviar = false;
                    mostrarProcesando();
                    setTimeout("procesar();", 20);
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
</asp:Content>

