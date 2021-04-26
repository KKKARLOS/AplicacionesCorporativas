<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title> ::: SUPER ::: - Reasignación</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<link href="css/estilos.css" type="text/css" rel="stylesheet"/>
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/draganddrop.js" type="text/Javascript"></script>
	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onload="init()" onunload="unload()">
<form id="Form1" name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
<script type="text/javascript">
<!--
    var strServer = "<% =Session["strServer"].ToString() %>";
    var sNumEmpleado = "<% =Session["NUM_EMPLEADO_ENTRADA"].ToString() %>";
    var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
    var bCambios = false;
    var bSalir = false;
    var bLectura = false;
    
-->
</script>  
<br />  
<table style="width:1000px; text-align:left; margin-left:4px;" cellpadding="5px">
    <colgroup><col style="width:500px;" /><col style="width:500px;" /></colgroup>
    <tr>
        <td>
            <fieldset id="fldOrigen" style="width:475px; height:520px; margin-top:10px; margin-left:5px">
                <legend><label id="lblOrigen">Origen</label></legend>
                <table style="width:470px; margin-top:15px;" cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td>
                            <asp:TextBox ID="txtOrigen" runat="server" style="width:445px;" Enabled="false" />
                        </td>
			        </tr> 
			        <tr>
			            <td>
                            <table id="tblTituloElementos" style="width:450px; height:17px; margin-top:7px;" cellpadding="0" cellspacing="0" border="0">
                                <tr class="TBLINI">
                                    <td style="width:353px;">Profesionales / elementos asociados</td>
                                    <td style="width:83px;">Tipo</td>
                                </tr>
                            </table>
	                        <div id="divEnt" style="overflow-x:hidden; overflow-y:auto; width: 466px; height:432px;">
	                            <div style='background-image:url(../../../../Images/imgFT16.gif); background-repeat:repeat; width:450px; height:auto;'>
	                                <%=strHTMLProfesionales%>
	                            </div>
                            </div>
	                        <table id="Table5" style="width:450px; height:17px;" cellspacing="0" border="0">
		                        <tr class="TBLFIN"><td></td></tr>
	                        </table>
			            </td>
			        </tr>                                       
                </table> 
            </fieldset>  	
        </td>
        <td>
            <fieldset style="width:475px; height:520px; margin-top:10px;">
                <legend><label id="lblDestino">Destino</label></legend>
                <table style="width:470px; margin-top:8px;" border="0">
                    <tr>
                        <td>	
                            <table style="width:465px;">
                                <colgroup><col style="width:75px;"/><col style="width:240px;"/><col style="width:150px;"/></colgroup>
                                <tr>
                                    <td><label id="Label1" >Denominación</label></td>
                                    <td>
                                        <asp:TextBox ID="txtDen" runat="server" style="width:230px"  
                                            onkeypress="javascript:if(event.keyCode==13){buscar();event.keyCode=0;}" MaxLength="50" />
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="rdbTipo" SkinId="rbli" runat="server" RepeatColumns="2" ToolTip="Tipo de búsqueda">
                                            <asp:ListItem style="cursor:pointer;" Value="I" onclick="buscar();">
                                                <img src='../../../../Images/imgIniciaCon.gif' border='0' title="Inicia con" style="cursor:pointer" hidefocus="hidefocus">
                                            </asp:ListItem>
                                            <asp:ListItem style="cursor:pointer;" Selected="True" Value="C" onclick="buscar();">
                                                <img src='../../../../Images/imgContieneA.gif' border='0' title="Contiene" style="cursor:pointer" hidefocus="hidefocus">
                                            </asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
	                    </td>
                    </tr> 
                    <tr>
                        <td>
                            <table id="tblTitulo" style="width:450px; height: 17px; margin-top:4px;" cellspacing="0" border="0">
                                <tr class="TBLINI">
                                    <td>
                                        <label id="lblDestino2" style="margin-left:3px;">Denominación</label>
                                    </td>
                                </tr>
                            </table>
                            <div id="divCatalogo" runat="server" style="overflow: auto; width: 466px; height:170px;">
                                <div style='background-image:url(../../../../Images/imgFT16.gif); width:450px;'>
                                    <%=strHTMLDestino%>
                                </div>
                            </div>
                            <table id="Table2" style="width:450px; height:17px;" cellspacing="0" border="0">
	                            <tr class="TBLFIN"><td></td></tr>
                            </table>
                            <asp:Image id="imgPapelera" style="CURSOR: pointer; margin-left:220px; margin-top: 13px; margin-bottom: 10px;" runat="server" ImageUrl="../../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="4"></asp:Image>
                            <table id="tblTitulo2" runat="server" style="width:450px; height: 17px;" cellspacing="0" border="0">
                                <tr class="TBLINI">
                                    <td>
                                        <label id="Label2" style="margin-left:3px;">A reasignar</label>
                                    </td>
                                </tr>
                            </table>
                            <div id="divCatalogo2" runat="server" style="overflow: auto; width: 466px; height:170px;" target="true" onmouseover="setTarget(this);" caso="2">
                                <div style='background-image:url(../../../../Images/imgFT16.gif); width:450px;'>
                                    <table id='tblDatos2' class='MM' style='width:450px;' cellpadding='0' cellspacing='0' border='0'>
                                    </table>
                                </div>
                            </div>
                            <table id="tblPie2" runat="server" style="width:450px; height:17px;" cellspacing="0" border="0">
	                            <tr class="TBLFIN"><td></td></tr>
                            </table>
                        </td>
                    </tr> 
                </table> 
            </fieldset>
	    </td>
    </tr>  		
</table>
<center>
    <table style="margin-top:15px;" width="240px">
        <tr>
	        <td align="center">
			    <button id="btnProcesar" type="button" onclick="procesar();" class="btnH25W100" runat="server" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../../images/imgAceptar.gif" /><span title="Asigna al elemento seleccionado en el cuadro destino los elementos asociados al origen">Aceptar</span>
			    </button>	
	        </td>		
	        <td align="center">
			    <button id="btnSalir" type="button" onclick="cerrarVentana();" class="btnH25W100" runat="server" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../../images/botones/imgCancelar.gif" /><span title="Salir">Cancelar</span>
			    </button>	 
	        </td>
        </tr>
    </table>
</center>
    
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<input type="hidden" runat="server" id="hdnOrigen" value="" />
<!-- E:entornos tecnologicos, T:titulaciones, C:clientes no Ibermatica-->
<input type="hidden" runat="server" id="hdnTipo" value="" />
<div class="clsDragWindow" id="DW" noWrap></div>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</form>
<script type="text/javascript">
	<!--
    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();
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
</body>
</html>


