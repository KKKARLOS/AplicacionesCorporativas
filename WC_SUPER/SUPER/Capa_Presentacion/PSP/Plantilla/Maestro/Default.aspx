<%@ Page Language="C#" EnableViewState="false" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default"%>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio"%>

<asp:Content ID="ContenedorBotonera" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
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
    	    <table id="tblDatos" cellpadding="5px" style="width:480px; text-align:left;">
    	        <colgroup>
    	            <col style="width:100px" />
    	            <col style="width:380px" />
    	        </colgroup>
    	        <tr>
    	            <td>Denominación</td>
    	            <td><asp:TextBox id="txtDesPlantilla" runat="server" MaxLength="50" Width="350px" onKeyUp="activarGrabar();"></asp:TextBox></td>
    	        </tr>
    	        <tr>
    	            <td>Tipo</td>
    	            <td><asp:DropDownList id="cboAmbito" runat="server" Width="120px" onChange="establecerTipo();activarGrabar();">
    	            <asp:ListItem Value="E">Empresarial</asp:ListItem>
    	            <asp:ListItem Value="D">Departamental</asp:ListItem>
    	            <asp:ListItem Value="P">Personal</asp:ListItem>
    	            </asp:DropDownList></td>
    	        </tr>
    	        <tr>
    	            <td>
    	                <label id="lblNodo" runat="server" class="texto"></label>
    	            </td>
    	            <td><asp:DropDownList id="cboCR" runat="server" Width="350px" onChange="activarGrabar();"></asp:DropDownList></td>
    	        </tr>
    	        <tr>
    	            <td>Activa</td>
    	            <td><asp:CheckBox id="chkActivo" runat="server" Checked="true" onClick="activarGrabar();"/></td>
    	        </tr>
     	        <tr>
    	            <td style="vertical-align:top;">Descripción</td>
    	            <td><asp:TextBox id="txtObs" runat="server" MaxLength="250" Width="350px" Height="100px" TextMode="MultiLine" onKeyUp="activarGrabar();"></asp:TextBox></td>
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
    <asp:TextBox ID="hdnIDPlantilla" runat="server" style="visibility: hidden"></asp:TextBox>
    <asp:TextBox ID="hdnIDPlantillaOriginal" runat="server" style="visibility: hidden"></asp:TextBox>
    <asp:TextBox ID="hndCRActual" runat="server" style="visibility: hidden"></asp:TextBox>
    <asp:TextBox ID="txtTipo" runat="server" MaxLength="1" style="visibility: hidden"></asp:TextBox>
    <asp:TextBox ID="txtModificable" runat="server" MaxLength="1" style="visibility: hidden" Text="T"></asp:TextBox>
    <asp:TextBox ID="txtOrigen" runat="server" MaxLength="1" style="visibility: hidden" Text="T"></asp:TextBox>
</asp:Content>
<asp:Content ID="ContenedorPostBack" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
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
				case "estructura": 
				{
                    bEnviar = false;
//                    if (bHayCambios){alert("grabar");grabar();}
//                    else alert("no hay cambios");
                    desglosePlantilla();
					break;
				}
				//case "eliminar": 
				//{
				//	bEnviar = false;
				//	jqConfirm("", "¿Estás conforme?", "", "", "war", 200).then(function (answer) {
				//	    if (answer) {
				//	        eliminarPlantilla();
				//	    }
				//	    fSubmit(bEnviar, eventTarget, eventArgument);
				//	});
				//	break;
	            //}
			    case "regresar":
			        {
			            if (bCambios) {
			                jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
			                    if (answer) {
			                        setTimeout("grabar()", 20);
			                    }
			                    bCambios = false;
			                    bEnviar = true;
			                    fSubmit(bEnviar, eventTarget, eventArgument);
			                });
			            } else fSubmit(bEnviar, eventTarget, eventArgument);

			            break;

			        }
			}
			if (strBoton != "regresar") fSubmit(bEnviar, eventTarget, eventArgument);
        }
    }
    function fSubmit(bEnviar, eventTarget, eventArgument)
    {
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
</script>
</asp:Content>

