<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" ValidateRequest="false" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio"%>
<asp:Content ID="ContenedorBotonera" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
    var bRes1024 = <%=((bool)Session["PLANT1024"]) ? "true":"false" %>;
</script>
<div id="div1024" style="Z-INDEX: 105; WIDTH: 32px; HEIGHT: 32px; POSITION: absolute; TOP: 93px; right: 10px;">
    <asp:Image ID="img1024" CssClass="MA" runat="server" Height="32" Width="32" ImageUrl="~/images/imgICO1024.gif" ondblclick="setResolucionPantalla()" ToolTip="Conmuta el tamaño de pantalla para adecuarla a la resolución 1024x768 o 1280x1024" />
</div>
<center>
    <table id="nombrePlantilla" style="width:768px; text-align:left" cellpadding="3px">
        <tr>
            <td>
                <asp:label ID="Label1" runat="server" Width="80px">Plantilla </asp:label>
                <asp:TextBox ID="txtDesPlantilla" Width="400px" CssClass="txtL" runat="server" readonly="true" ></asp:TextBox>&nbsp;
                <asp:label ID="Label2" runat="server">Ambito </asp:label>
                <asp:TextBox ID="lblTipo" Width="145px" CssClass="txtL" runat="server" readonly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:label ID="Label3" runat="server" Width="80px">Tipo </asp:label>
                <asp:TextBox ID="txtAmbito" Width="120px" CssClass="txtL" runat="server" readonly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
	        <td>
				<FIELDSET style="width:688px;">
					<LEGEND>Mantenimiento de estructura</LEGEND>
                    <table id="btnPT" style="margin-left:5px; margin-top:5px; width:670px;">
		                <tr>
		                    <td>
		                        <img src='../../../../Images/imgFlechaIzOff.gif' border='0' title="Desplaza hacia la izquierda las filas marcadas" onclick="desplazarFilasMarcadas('I')" style="cursor:pointer">&nbsp;&nbsp;&nbsp;
		                        <img src='../../../../Images/imgFlechaDrOff.gif' border='0' title="Desplaza hacia la derecha las filas marcadas" onclick="desplazarFilasMarcadas('D')" style="cursor:pointer">&nbsp;&nbsp;&nbsp;
		                        <img src='../../../../Images/imgFlechaUp.gif' border='0' title="Desplaza hacia arriba las filas marcadas" onclick="subirFilasMarcadas()" style="cursor:pointer">&nbsp;&nbsp;&nbsp;
		                        <img src='../../../../Images/imgFlechaDown.gif' border='0' title="Desplaza hacia abajo las filas marcadas" onclick="bajarFilasMarcadas()" style="cursor:pointer">&nbsp;&nbsp;&nbsp;&nbsp;
		                        <img src='../../../../Images/Botones/imgBorrar.gif' border='0' title="Borra la fila marcada" onclick="eliminarTarea('tblDatos')" style="cursor:pointer">&nbsp;&nbsp;&nbsp;
		                        <img src='../../../../Images/imgProyTec2.gif' border='0' title="Proyecto técnico" onclick="Tarea('tblDatos','P')" style="cursor:pointer">&nbsp;&nbsp;&nbsp;
		                        <img src='../../../../Images/imgFase2.gif' border='0' title="Fase" onclick="Tarea('tblDatos','F')" style="cursor:pointer">&nbsp;&nbsp;&nbsp;
		                        <img src='../../../../Images/imgActividad2.gif' border='0' title="Actividad" onclick="Tarea('tblDatos','A')" style="cursor:pointer">&nbsp;&nbsp;&nbsp;
		                        <img src='../../../../Images/imgTarea2.gif' border='0' title="Tarea" onclick="Tarea('tblDatos','T')" style="cursor:pointer">&nbsp;&nbsp;&nbsp;
		                        <img src='../../../../Images/imgHito2.gif' border='0' title="Hito" onclick="Tarea('tblDatos','H')" style="cursor:pointer">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
		                    </td>
                            <td>
                                <asp:RadioButtonList ID="rdbAccion" SkinID="rbl" runat="server" Height="20px" RepeatColumns="3" ToolTip="Acción a realizar" Width="220px">
                                    <asp:ListItem Selected="True" Value="A">Añadir</asp:ListItem>
                                    <asp:ListItem Value="I">Insertar</asp:ListItem>
                                    <asp:ListItem Value="M">Modificar</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
	                </table>
				</FIELDSET>
	        </td>
	    </tr>
        <tr>
	        <td>
		        <table id="tblDatos3" style="width:700px; height:17px">
		        <colgroup><col style="width:20px"/><col style="width:560px"/><col style="width:70px"/><col style="width:50px"/></colgroup>
			        <tr class="TBLINI">
				        <td></td>
				        <td><label>Denominación</label></td>
				        <td>Facturable</td>
				        <td title="Avance automático">Avance</td>
			        </tr>
		        </table>
		        <div id="divCatalogo" style="overflow: auto; width: 716px; height:400px">
 		            <div style='background-image:url(../../../../Images/imgFT20.gif); width:700px'>
 		                <%=strTablaHTMLTarea%>
 		            </div>
                </div>
		        <table style="width: 700px; height: 17px">
			        <tr class="TBLFIN">
				        <td></td>
			        </tr>
		        </table>
	        </td>
    </tr>
    <tr>
        <td>
			<FIELDSET style="width:457px;">
				<LEGEND>&nbsp;Mantenimiento de hitos de cumplimiento discontinuo&nbsp;</LEGEND>
                <table id="Table1" style="margin: 5px;">
	                <tr>
	                    <td>
	                        <img src='../../../../Images/imgFlechaUp.gif' border='0' title="Desplaza hacia arriba la fila marcada" onclick="subirHitosMarcados()" style="cursor:pointer">&nbsp;&nbsp;&nbsp;&nbsp;
	                        <img src='../../../../Images/imgFlechaDown.gif' border='0' title="Desplaza hacia abajo la fila marcada" onclick="bajarHitosMarcados()" style="cursor:pointer">&nbsp;&nbsp;&nbsp;&nbsp;
	                        <img src='../../../../Images/Botones/imgBorrar.gif' border='0' title="Borra la fila marcada" onclick="eliminarHito('tblDatos2')" style="cursor:pointer">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	                        <img src='../../../../Images/imgHito2.gif' border='0' title="Hito" onclick="Hito('tblDatos2')" style="cursor:pointer;">&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td>    
                            <asp:RadioButtonList ID="rdbAccion2" SkinID="rbl" runat="server" Height="20px" RepeatColumns="3" ToolTip="Acción a realizar" Width="200px">
                                <asp:ListItem Selected="True" Value="A">Añadir</asp:ListItem>
                                <asp:ListItem Value="I">Insertar</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
			</FIELDSET>
        </td>
    </tr>
    <tr>
        <td>
	        <table style="width: 470px;height: 17px">
	            <colgroup><col style="width:20px"/><col style="width:450px"/></colgroup>
		        <tr class="TBLINI">
			        <td></td>
			        <td>Denominación</td>
		        </tr>
	        </table>
	        <div id="divHitos" style="overflow: auto; width: 486px; height:80px">
	            <div style='background-image:url(../../../../Images/imgFT20.gif); width:470px'>
	                <%=strTablaHTMLHito%>
	            </div>
            </div>
	        <table style="width: 470px; height: 17px">
		        <tr class="TBLFIN">
			        <td></td>
		        </tr>
	        </table>
        </td>
    </tr>
	</table>
</center>	
<asp:TextBox ID="hdnIDPlantilla" runat="server" style="visibility: hidden"></asp:TextBox>    
<asp:TextBox ID="txtModificable" runat="server" MaxLength="1" style="visibility: hidden" Text="T"></asp:TextBox>
<asp:TextBox ID="txtPerfil" runat="server" MaxLength="1" style="visibility: hidden" Text="T"></asp:TextBox>
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

