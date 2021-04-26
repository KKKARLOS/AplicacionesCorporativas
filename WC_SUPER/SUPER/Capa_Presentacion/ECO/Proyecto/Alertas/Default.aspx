<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title> ::: SUPER ::: - Alertas por proyecto</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../Javascript/dhtmltooltip.js" type="text/Javascript"></script>
   	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onload="init()" onunload="unload()">
<form id="Form1" name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
<script type="text/javascript">
<!--
    var strServer = "<% =Session["strServer"].ToString() %>";
    var intSession = <%=Session.Timeout%>;
    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
-->
</script>  
<center>  
<table style="width:690px; margin-top:15px; margin-left:0px; text-align:left;">
    <tr>
		<td>
			<table id="tblTitulo" style="width:670px; height:17px;" cellpadding="0" cellspacing="0" border="0">
			    <colgroup>
			        <col style='width:30px;' />
	                <col style='width:320px;' />
	                <col style='width:60px;' />
	                <col style='width:100px;' />
	                <col style='width:120px;' />
	                <col style='width:40px;' />
			    </colgroup>
				<tr class="TBLINI" style="height:17px; text-align:center;">
                    <td>Id</td>							
					<td style="padding-left:3px; text-align:left;">Asunto</td>
					<td>Activado</td>
					<td>Ini. Standby</td>
					<td>Fin Standby</td>
					<td></td>
				</tr>
			</table>
			<div id="divCatalogo" style="overflow: auto; width: 686px; height: 420px;" onscroll="scrollTabla()">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:670px">
                    <%=strTablaHTML%>
                    </div>
            </div>
            <table id="tblResultado" style="width:670px">
				<tr class="TBLFIN"  style="height:17px;">
					<td>&nbsp;</td>
				</tr>
			</table>
		</td>
    </tr>
</table>
<table id="tblBotonesADM" runat="server" style="width:200px; margin-top:10px;">
	<tr> 
		<td>
            <button id="btnGrabar" type="button" onclick="grabar();" class="btnH25W95" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../../images/botones/imgGrabarSalir.gif" /><span title="Grabar y salir">Grabar...</span>
            </button>				   
		</td>
		<td>
            <button id="btnCancelar" type="button" onclick="cancelar();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../../images/botones/imgCancelar.gif" alt="Salir" /><span>Cancelar</span>
            </button>				   
		</td>
	</tr>
</table>
<table id="tblBotonesUSU" runat="server" style="width:100px; margin-top:10px;">
	<tr> 
		<td>
            <button id="btnSalir" type="button" onclick="salir();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../../images/botones/imgSalir.gif" alt="Salir" /><span>Salir</span>
            </button>				   
		</td>
	</tr>
</table>
</center>
<div id="divTotal" style="z-index:10; position:absolute; left:0px; top:0px; width:800px; height:550px; background-image: url(../../../../Images/imgFondoPixelado2.gif); background-repeat:repeat; display:none;" runat="server">
    <div id="divSeguimiento" style="position:absolute; top:100px; left:140px;">
        <table border="0" cellspacing="0" cellpadding="0" style="width:420px;margin-top:5px;">
          <tr>
            <td width="6" height="6" background="../../../../Images/Tabla/7.gif"></td>
            <td height="6" background="../../../../Images/Tabla/8.gif"></td>
            <td width="6" height="6" background="../../../../Images/Tabla/9.gif"></td>
          </tr>
          <tr>
            <td width="6" background="../../../../Images/Tabla/4.gif">&nbsp;</td>
            <td background="../../../../Images/Tabla/5.gif" style="padding:3px; vertical-align:top;">
            <!-- Inicio del contenido propio de la página -->
            <table id="tblSeguimiento" class="texto" style="width:400px; height:200px;" cellspacing="2" cellpadding="0" border="0">
                <tr>
                    <td>
                        <label id="lblTextoSeguimiento">Para ACTIVAR un seguimiento, es preciso indicar el motivo del mismo.</label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Motivo<br />
                        <asp:TextBox id="txtSeguimiento" SkinID="Multi" TextMode="multiLine" runat="server" style="width:390px; height:100px; margin-top:5px;" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <button id="btnActivarDesactivar" type="button" class="btnH25W100" runat="server" hidefocus="hidefocus" 
                            style="float:left; margin-left:100px" onmouseover="se(this, 25);">
                            <img id="imgBotonActivar" src="../../../../images/imgSegAdd.png" /><span id="lblBoton">Activar</span>
                        </button>
                        <button id="btnCancelarSeg" type="button" class="btnH25W100" runat="server" hidefocus="hidefocus" style="float:left; margin-left:20px"
                            onclick="CancelarSeguimiento();" onmouseover="se(this, 25);">
                            <img src="../../../../images/Botones/imgCancelar.gif" /><span>Cancelar</span>
                        </button>
                    </td>
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
    </div>
</div>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<input type="hidden" name="hdnEstado" id="hdnEstado" value="C" runat="server" />
<input type="hidden" name="hdnPSN" id="hdnPSN" value="" runat="server" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</form>
<script type="text/javascript">
	<!--
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

