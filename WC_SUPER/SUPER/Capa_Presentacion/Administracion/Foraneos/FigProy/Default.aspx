<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" ValidateRequest="false" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Mantenimiento de figuras de proyecto asignables a foráneos</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <script src="../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
   	<script language="JavaScript" src="../../../../Javascript/draganddrop.js" type="text/Javascript"></script>
	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
  	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
  	<script language="JavaScript" src="../../../../Javascript/boxover.js" type="text/Javascript"></script>
</head>
<body onload="init()" onunload="unload()">
<form id="Form1" name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />

<script type="text/javascript">
    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
    var strServer = "<% =Session["strServer"].ToString() %>";
    var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
</script>

<table cellpadding="4" style="width:790px; margin-top:10px; margin-left:5px;">
    <colgroup><col style="width:370px;"/><col style="width:50px;"/><col style="width:370px;"/></colgroup>
    <tr>
        <td><!-- Relación de Items --> 
            <table id="tblCatIni" style="width: 350px;height: 17px">
                <tr class="TBLINI">
                    <td style="padding-left:5px">Figuras de proyecto
				    </td>                    
                </tr>
            </table>
            <div id="divCatalogo" style="overflow: auto; width: 366px; height:440px" runat="server" >
                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif');width: 350px;">
                    <table id="tblFig" class='texto MAM'>
                        <colgroup><col style="width:20px;" /><col style="width:330px;" /></colgroup>
                        <tr id ="D" style="height:20px;" ondblclick='insertarItem(this)' onmousedown='DD(event)' >
                            <td style="vertical-align:middle;">
                                <img src="../../../../Images/imgDelegado.gif" onmouseover="mcur(this)" title="Delegado" ondragstart="return false;" /> 
                            </td>
                            <td>Delegado</td>
                        </tr>
                        <tr id ="C" style="height:20px;" ondblclick='insertarItem(this)' onmousedown='DD(event)' >
                            <td>
                                <img src="../../../../Images/imgColaborador.gif" onmouseover="mcur(this)" title="Colaborador" ondragstart="return false;" /> 
                            </td>
                            <td>Colaborador</td>
                        </tr>
                        <tr id ="I" style="height:20px;" ondblclick='insertarItem(this)' onmousedown='DD(event)' >
                            <td>
                                <img src="../../../../Images/imgInvitado.gif" onmouseover="mcur(this)" title="Invitado" ondragstart="return false;" /> 
                            </td>
                            <td>Invitado</td>
                        </tr>
                        <tr id ="J" style="height:20px;" ondblclick='insertarItem(this)' onmousedown='DD(event)' >
                            <td>
                                <img src="../../../../Images/imgJefeProyecto.gif" onmouseover="mcur(this)" ondragstart="return false;" title="Jefe" /> 
                            </td>
                            <td>Jefe</td>
                        </tr>
                        <tr id ="M" style="height:20px;" ondblclick='insertarItem(this)' onmousedown='DD(event)' >
                            <td>
                                <img src="../../../../Images/imgSubjefeProyecto.gif" onmouseover="mcur(this)" ondragstart="return false;" title="Responsable técnico de proyecto económico" /> 
                            </td>
                            <td>RTPE</td>
                        </tr>
                        <tr id ="B" style="height:20px;" ondblclick='insertarItem(this)' onmousedown='DD(event)' >
                            <td>
                                <img src="../../../../Images/imgBitacorico.gif" onmouseover="mcur(this)" title="Bitacórico" ondragstart="return false;" /> 
                            </td>
                            <td>Bitacórico</td>
                        </tr>
                        <tr id ="S" style="height:20px;" ondblclick='insertarItem(this)' onmousedown='DD(event)' >
                            <td>
                                <img src="../../../../Images/imgSecretaria.gif" onmouseover="mcur(this)" title="Asistente" ondragstart="return false;" /> 
                            </td>
                            <td>Asistente</td>
                        </tr>
                        <tr id ="W" style="height:20px;" ondblclick='insertarItem(this)' onmousedown='DD(event)' >
                            <td>
                                <img src="../../../../Images/imgRTPT.gif" onmouseover="mcur(this)" title="Responsable técnico de proyecto económico" ondragstart="return false;" /> 
                            </td>
                            <td>RTPT</td>
                        </tr>
                    </table>
                </div>
            </div>
            <table style="width: 350px; height: 17px">
                <tr class="TBLFIN">
                    <td></td>
                </tr>
            </table>              
        </td>
        <td style="vertical-align:middle; text-align:left;" >
            <asp:Image id="imgPapelera" style="CURSOR: pointer; margin-left:4px;" runat="server" ImageUrl="../../../../Images/imgEliminar32.gif" target="true" caso="3" onmouseover="setTarget(this)"></asp:Image>
        </td>
        <td><!-- Items asignados -->
            <table id="tblAsignados" style="width: 350px; height: 17px">
                <tr class="TBLINI">
                    <td style="padding-left:5px">Figuras seleccionadas
					</td>
                </tr>
            </table>
            <div id="divCatalogo2" style="overflow: auto; overflow-x: hidden;  width: 366px; height:440px" target="true" caso="2" onmouseover="setTarget(this);">
                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif');width: 350px;">
                 <%=strTablaFigAsig%>
                </div>
            </div>
            <table style="width: 350px; height: 17px">
                <tr class="TBLFIN">
                    <td></td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<table id="tblBotones" style="width:263px; margin-top:15px; margin-left:265px; text-align:left;" border="0">
<colgroup><col style="width:163px;"/><col style="width:100;"/></colgroup>
    <tr>
        <td style="text-align:right;">
		    <button id="btnGrabarSalir" type="button" onclick="grabarSalir();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
			     onmouseover="se(this, 25);mostrarCursor(this);">
			    <img src="../../../../images/botones/imgGrabarSalir.gif" /><span title="Grabar y salir">Grabar...</span>
		    </button>	
        </td>
        <td>
		    <button id="btnSalir" type="button" onclick="salir();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
			     onmouseover="se(this, 25);mostrarCursor(this);">
			    <img src="../../../../images/botones/imgSalir.gif" /><span title="Salir">&nbsp;&nbsp;Salir</span>
		    </button>	 
        </td>
    </tr>
</table>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<div class="clsDragWindow" id="DW" noWrap></div>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
<uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
<div class="clsDragWindow" id="Div1" noWrap></div>
</form>
<script type="text/javascript">
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
</script>
</body>
</html>
