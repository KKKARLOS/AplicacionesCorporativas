<%@ Page Language="C#" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_ItemPlant_Default" ValidateRequest="false" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> ::: SUPER ::: - Detalle de elemento de plantilla</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <script src="../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script src="../../../Javascript/documentos.js" type="text/Javascript"></script>
   	<script src="../../../Javascript/draganddrop.js" type="text/Javascript"></script>
	<script src="Functions/funciones.js" type="text/Javascript"></script>
 	<script src="../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onLoad="init()" onunload="unload()">
    <script type="text/javascript">
        var strServer = "<% =Session["strServer"].ToString() %>";
        var sNumEmpleado = "<% =Session["NUM_EMPLEADO_ENTRADA"].ToString() %>";
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos. 
        var bCambios = false;
        var bLectura = true;
        var bSalir = false;
        
        //Variables a devolver a la estructura.
        var sDescripcion = "";
        var sFacturable = "";
        var sAvance="";
        var sObliga="";
    </script>    
<form name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
<center>
    <table class="texto" align="center" style="width:860px; margin-top:15px;" border="0">
        <tr>
            <td colspan="2">
				<fieldset style="width:850px; margin-left:4px;">
					<legend>Identificación de elemento</legend>
                    <table align="center" border="0" class="texto" width="100%" cellpadding="5" cellspacing="0">
                        <tr>
                            <td>
                                Denominación&nbsp;
                                <asp:TextBox ID="txtDesTarea" runat="server" Style="width: 350px" onKeyUp="activarGrabar();"></asp:TextBox>
                                <asp:TextBox ID="txtIdTarea" runat="server" SkinID="Numero" style="width:60px;visibility:hidden" readonly="true" ></asp:TextBox>
                            </td>
                        </tr>
                    </table>
            </fieldset>
            </td>
        </tr>
        <tr>
            <td>
				<fieldset style="width:850px; margin-left:4px; height:300px;">
					<legend>Atributos estadísticos</legend>
                    <table style="width:100%" cellpadding="5">
                        <tr>
                            <td style="width:200px">
                                <table style="width: 180px; height: 17px">
                                    <tr class="TBLINI">
	                                    <td align="center">Definidos en el C.R.</td>
                                    </tr>
                                </table>
                                <div id="divAECR" style="overflow: auto; width: 196px; height:208px">
                                    <div style='background-image:url(../../../Images/imgFT16.gif); width:180px'>
                                    <table id='tblAECR' class='texto MAM' style='width:180px;'>
                                        <colgroup><col style='width:15px' /><col style='width:165px' /></colgroup>
                                            <%=strTablaAECR %>
                                    </table>
                                    </div>
                                </div>
                                <table style="width: 180px; height: 17px">
                                    <tr class="TBLFIN">
	                                    <td></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="vertical-align:middle; width:50px;">
				                <asp:Image id="imgPapelera" style="CURSOR: pointer" runat="server" ImageUrl="../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="3"></asp:Image>
                            </td>
                            <td style="width:350px">
                                <table style="width: 330px; height: 17px">
                                    <colgroup><col style='width:210px' /><col style='width:120px' /></colgroup>
                                    <tr class="TBLINI">
	                                    <td style="text-align:center;">Asociados a la tarea</td>
	                                    <td style="text-align:center;">Valor</td>
                                    </tr>
                                </table>
                                <div id="divAET" style="overflow: auto; width: 346px; height:208px" target="true" onmouseover="setTarget(this);" caso="1">
                                    <div style='background-image:url(../../../Images/imgFT16.gif); width:330px'>
                                        <table id='tblAET' class='texto MM' style='width: 330px;'>
                                        <colgroup><col style='width:10px' /><col style='width:15px' /><col style='width:215px' /><col style='width:100px' /></colgroup>
                                            <%=strTablaAET%>
                                        </table>
                                    </div>
                                </div>
                                <table style="width: 330px; height: 17px">
                                    <tr class="TBLFIN"> <td></td> </tr>
                                </table>
                            </td>
                            <td style="width:200px">
                                <table style="width:180px; height:17px">
                                    <tr class="TBLINI"> <td align="center">Valores definidos</td></tr>
                                </table>
                                <div id="divAEVD" style="overflow: auto; width: 196px; height:208px">
                                    <div style='background-image:url(../../../Images/imgFT16.gif); width:180px'>
                                        <table id='tblAEVD' class='texto MA' style='width: 180px;'></table>
                                    </div>
                                </div>
                                <table style="width:180px; height:17px">
                                    <tr class="TBLFIN"> <td></td></tr>
                                </table>
                            </td>
                        </tr>
                    </table>
            </fieldset>
            <br />
            </td>
        </tr>
        <tr>
            <td>
                <fieldset style="width:850px; margin-left:4px;">
                <legend>Otros datos</legend>
                    <table style="width:100%" cellpadding="5" >
                        <tr>
                            <td>
                                <label id="lblFact" style="vertical-align:super">Facturable</label>
                                <asp:CheckBox ID="chkFacturable" runat="server" style="cursor:pointer;" onClick="activarGrabar();" />
                            </td>
                            <td>
                                <label id="lblAv" style="vertical-align:super">Avance automático</label>
                                <asp:CheckBox ID="chkAvance" runat="server" style="cursor:pointer;" onClick="activarGrabar();" />
                            </td>
                            <td>
                                <label id="lblOb" style="vertical-align:super">Obliga estimación</label>
                                <asp:CheckBox ID="chkObliga" runat="server" style="cursor:pointer;" onClick="activarGrabar();" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
    <table id="tblBotones" align="center" style="margin-top:15px;"  width="50%">
        <tr>
	        <td align="center">
			    <button id="btnGrabar" type="button" onclick="grabarAux();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../images/botones/imgGrabar.gif" /><span title="Grabar">&nbsp;&nbsp;Grabar</span>
			    </button>	
	        </td>
	        <td align="center">
			    <button id="btnGrabarSalir" type="button" onclick="grabarsalir();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../images/botones/imgGrabarSalir.gif" /><span title="Grabar y salir">Grabar...</span>
			    </button>	
	        </td>						
	        <td align="center">
			    <button id="btnSalir" type="button" onclick="salir();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../images/botones/imgSalir.gif" /><span title="Salir">&nbsp;&nbsp;Salir</span>
			    </button>	 
	        </td>
        </tr>
    </table>	
</center>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<input type="hidden" name="hdnIdTarea" id="hdnIdTarea" value="<%=nIdTarea %>" />
<asp:TextBox ID="hdnOrden" name="hdnOrden" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
<asp:TextBox ID="hdnEstado" name="hdnEstado" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
<asp:TextBox ID="hdnAcceso" name="hdnAcceso" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
<asp:TextBox ID="hdnTipo" name="hdnTipo" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
<asp:TextBox ID="hdnMargen" name="hdnMargen" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
<asp:TextBox ID="hdnIdPlant" name="hdnIdPlant" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
<script type="text/javascript">
    <%=strArrayVAE %>
</script>
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    <uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
    <div class="clsDragWindow" id="DW" noWrap></div>
</form>
<script type="text/javascript">
		function __doPostBack(eventTarget, eventArgument) {
	        var bEnviar = true;
	        if (eventTarget.split("$")[2] == "Botonera") {
		        var strBoton = Botonera.botonID(eventArgument).toLowerCase();
				switch (strBoton){
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
</script>
</body>
</html>
