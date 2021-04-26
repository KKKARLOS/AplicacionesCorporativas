<%@ Page Language="C#" EnableViewState="false" EnableEventValidation="false" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Administracion_Calendario_Detalle_Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
    var aCR_js = new Array();
</script>
<br /><br />
<center>
    <table border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="6" height="6" background="../../../../Images/Tabla/7.gif"></td>
        <td height="6" background="../../../../Images/Tabla/8.gif"></td>
        <td width="6" height="6" background="../../../../Images/Tabla/9.gif"></td>
      </tr>
      <tr>
        <td width="6" background="../../../../Images/Tabla/4.gif">&nbsp;</td>
        <td background="../../../../Images/Tabla/5.gif" style="padding:5px">
	    <!-- Inicio del contenido propio de la página -->
    	
    	    <table id="tblDatos" cellpadding="5px" style="width:580px;text-align:left">
    	        <colgroup>
    	            <col style="width:90px" />
    	            <col style="width:370px"/>
    	            <col style="width:130px" />
    	        </colgroup>
    	        <tr>
    	            <td>Denominación</td>
    	            <td colspan='2'><asp:TextBox ID="txtDesCalendario" runat="server" MaxLength="50" Width="350px" onKeyUp="activarGrabar();"></asp:TextBox></td>
    	        </tr>
    	        <tr>
    	            <td title="Centro de responsabilidad">C.R.</td>
    	            <td><asp:DropDownList ID="cboCR" runat="server" Width="355px" onChange="activarGrabar();">
                        </asp:DropDownList></td>
    	            <td>
    	                <label id="lblCRInactivo" style="visibility:hidden;width:70px;vertical-align:super" title="Mostrar CR's inactivos" runat="server" >CR's inactivos</label>
    	                <asp:CheckBox style="visibility: hidden" ID="chkCRInactivo" runat="server" onClick="cargarCR(this.checked)" Checked="true" />
    	            </td>

    	        </tr>
    	        <tr>
    	            <td>Tipo</td>
    	            <td colspan='2'><asp:DropDownList ID="cboTipo" runat="server" Width="120px" onChange="controlarTipoCal();activarGrabar();">
    	                <asp:ListItem Value="E">Empresarial</asp:ListItem>
    	                <asp:ListItem Value="D">Departamental</asp:ListItem>
                        </asp:DropDownList></td>
    	        </tr>
    	        <tr>
    	            <td>Activo</td>
    	            <td colspan='2'>
    	                <asp:CheckBox ID="chkActivo" runat="server" onClick="activarGrabar();" Checked="true" />&nbsp;&nbsp;
                        <!--
    	                <label id="lblNJLACV" style="width:30px;vertical-align:super" title="Número de jornadas laborables año contando las vacaciones (se calcula externamente)" runat="server" >NJLACV</label>
    	                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtNJLACV" runat="server" SkinID="Numero" style="width:40px;vertical-align:top" onFocus="fn(this,3,0);" onKeyUp="activarGrabar();" runat="server" ></asp:TextBox>
    	                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    	                <label id="lblNHLACV" style="width:30px;vertical-align:super" title="Número de horas laborables año contando las vacaciones (se calcula externamente)" runat="server" >NHLACV</label>
    	                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtNHLACV" runat="server" SkinID="Numero" style="width:40px;vertical-align:top" onFocus="fn(this,5,0);" onKeyUp="activarGrabar();" runat="server" ></asp:TextBox>
                        -->
    	            </td>
    	        </tr>
                <tr>
                    <td>
                        <label id="lblPais" class="label">País </label>
                    </td>
                    <td>
                        <asp:DropDownList id="cboPais" runat="server" style="width:180px;margin-bottom:3px;" onchange="obtenerProvinciasPais(this.value);" CssClass="combo">
                            <asp:ListItem Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label id="lblProvincia" class="label">Provincia </label>
                    </td>
                   <td>
                        <asp:DropDownList id="cboProvincia" runat="server" style="width:180px;margin-bottom:3px;"  AppendDataBoundItems="true" onChange="activarGrabar();" CssClass="combo">
                            <asp:ListItem Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label id="lblResponsable" style="width:70px" class="enlace" onclick="getResponsable()">Responsable</label>
                    </td>
                   <td>
                        <asp:TextBox ID="txtResponsable" style="width:283px;" Text="" runat="server" readonly="true" />&nbsp;
                        <asp:Image ID="Image8" runat="server" ImageUrl="~/Images/imgBorrar.gif" onclick="limpiarResponsable();" style="cursor:pointer; vertical-align:bottom" />
                    </td>
                </tr>
    	        <tr>
    	            <td>Observaciones</td>
    	            <td colspan='2'><asp:TextBox ID="txtObs" SkinID="Multi" runat="server" TextMode="MultiLine" Rows="5" Width="460px" onKeyUp="activarGrabar();" MaxLength="250"></asp:TextBox></td>
    	        </tr>
    	    </table>
	    <!-- Fin del contenido propio de la página -->
	    </td>
        <td width="6" background="../../../../Images/Tabla/6.gif">&nbsp;</td>
      </tr>
      <tr>
        <td width="6" height="6" background="../../../../Images/Tabla/1.gif"></td>
        <td height="6" background="../../../../Images/Tabla/2.gif"></td>
        <td width="6" height="6" background="../../../../Images/Tabla/3.gif"></td>
      </tr>
    </table>
</center>
<asp:TextBox ID="hdnIDCalendario" runat="server" style="visibility: hidden"></asp:TextBox>
<asp:TextBox ID="hdnIDCalendarioOriginal" runat="server" style="visibility: hidden"></asp:TextBox>
<asp:TextBox ID="hdnIDCR" runat="server" style="visibility: hidden"></asp:TextBox>
    <input type="hidden" runat="server" name="hdnIdFicResp" id="hdnIdFicResp" value="" /> 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">


<script type="text/javascript">

    <%=strArrayCR %>

	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();
			switch (strBoton){
				case "grabar": 
				{
                    bEnviar = false;
                    grabar();
					break;
				}
				case "grabarcomo": 
				{
                    bEnviar = false;
				    grabarComo();
					break;
				}
				case "horario": 
				{
				    bEnviar = false;
				    mostrarHorario();
					break;
				}
			    case "regresar":
			    {
			        if (bCambios) {
			            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
			                if (answer) {
			                    bEnviar = false;
			                    bSalir = true;
			                    setTimeout("grabar()", 20);
			                } else
			                {
			                    bEnviar = true;
			                    bCambios = false;
			                    fSubmit(bEnviar, eventTarget, eventArgument);
			                }

			            });
			        } else fSubmit(bEnviar, eventTarget, eventArgument);

			        break;

                }
        }
        if (strBoton != "regresar") fSubmit(bEnviar, eventTarget, eventArgument);
    }
    return;
}
function fSubmit(bEnviar, eventTarget, eventArgument) {
    var theform = document.forms[0];
    theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
    theform.__EVENTARGUMENT.value = eventArgument;
    if (bEnviar) theform.submit();
    return;
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
</script>
</asp:Content>

