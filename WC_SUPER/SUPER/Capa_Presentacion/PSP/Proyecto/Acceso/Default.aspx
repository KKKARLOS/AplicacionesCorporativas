<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" Title="Untitled Page" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio"%>

<asp:Content ID="ContenedorBotonera" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
	var oBody = document.getElementsByTagName("body")[0];
	oBody.style.backgroundImage = "url(<%=Session["strServer"].ToString() %>Images/imgFondoSuper4.png)";
	oBody.style.backgroundRepeat= "no-repeat";
	oBody.style.backgroundPosition= "left center";
	oBody.style.backgroundAttachment= "fixed";

    var nIndiceProy = -1;
    var bLectura = true;
    var sOpcion = "<%=sOpcion %>";
    var bMostrarMsg = true;
    var sUsuarioSuper = "<%=Session["UsuarioActual"].ToString() %>";
</script>
<br /><br /><br />
<center>
    <table id="nombreProyecto" height="20" width="400" style="visibility:hidden;" cellspacing="0" cellpadding="0" border="0">
        <tr>
            <td>
			    <fieldset style="width: 350px;">
				    <legend>&nbsp;Selección del tipo de elemento a acceder</legend>
                    <table id="btnPT" cellspacing="0" cellpadding="0" align="left" border="0" style="margin: 5px;">
	                    <tr>
                            <td>
                                <asp:RadioButtonList ID="rdbAccion" SkinID="rbl" runat="server" Height="20px" RepeatColumns="1" ToolTip="Tipo de elemento">
                                    <asp:ListItem Value="B">Bitácora</asp:ListItem>
                                    <asp:ListItem Value="C">Bitácora Proyecto técnico</asp:ListItem>
                                    <asp:ListItem Value="D">Bitácora Tarea</asp:ListItem>
                                    <asp:ListItem Value="P">Proyecto técnico</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="T">Tarea</asp:ListItem>
                                    <asp:ListItem Value="Y">Ordenación Proyecto técnico</asp:ListItem>
                                    <asp:ListItem Value="Z">Reestructuración tarea individual</asp:ListItem>
                                    <asp:ListItem Value="X">Reestructuración tarea bloque</asp:ListItem>
                                    <asp:ListItem Value="W">Duplicación tarea</asp:ListItem>
                                    <asp:ListItem Value="A">Disponibilidad de profesionales</asp:ListItem>
                                    <asp:ListItem Value="E">Escenario de proyecto</asp:ListItem>
                                    <asp:ListItem Value="F">Alta de forasteros</asp:ListItem>
                                    <asp:ListItem Value="G">Mantenimiento de forasteros</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
			    </fieldset><br /><br />
            </td>
        </tr>
        <tr>
            <td>
                <table height="20" cellspacing="0" cellpadding="0" align="center" border="0">
                    <tr style="CURSOR: pointer" onclick="javascript:mDetalle()">
	                    <td width="7"><IMG src="../../../../images/imgBtnIzda.gif" width="7"></td>
	                    <td style="vertical-align:middle; text-align:center; width:20px; background:'../../../../images/bckBoton.gif'">
	                    <A hideFocus href="#">
	                    <IMG src="../../../../images/imgAceptar.gif" align="absMiddle" border="0"></A>
	                    </td>
	                    <td class="txtBot" width="40" background="../../../../images/bckBoton.gif">
	                    <A class="txtBot" hideFocus href="#">&nbsp;&nbsp;Aceptar</A>
	                    </td>
	                    <td width="7"><img src="../../../../images/imgBtnDer.gif" width="7"></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</center>	
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>
<asp:Content ID="ContenedorPostBack" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
	function __doPostBack(eventTarget, eventArgument) {
		var bEnviar = true;
		var oReg = /\$/g;
		var oElement = document.getElementById(eventTarget.replace(oReg,"_"));
		if (eventTarget.split("$")[2] == "Botonera"){
		    var strBoton = oElement.botonID(eventArgument).toLowerCase();
		}

		var theform;
		if (window.navigator.appName.toLowerCase().indexOf("microsoft") > -1) {
			theform = document.forms[0];
		}
		else {
			theform = document.forms["frmDatos"];
		}
		theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
		theform.__EVENTARGUMENT.value = eventArgument;
		if (bEnviar){
			theform.submit();
		}
		else{
			//Si se ha "cortado" el submit, se restablece el estado original de la botonera.
			oElement.restablecer();
		}
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

