<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" EnableEventValidation="false" ValidateRequest="false" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self"/>
    <title> ::: SUPER ::: - Renovación de certificado</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <link rel="stylesheet" href="../../../../../App_Themes/Corporativo/Corporativo.css" type="text/css" />
    <link rel="stylesheet" href="../../../../../PopCalendar/CSS/Classic.css" type="text/css" />
    <script src="../../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../../../Javascript/funciones.js?21062018" type="text/Javascript"></script>
	<script src="../../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script src="../../../../../Javascript/dhtmltooltip.js" type="text/Javascript"></script>
    <script src="../../../../../Javascript/jquery.autocomplete.js" type="text/Javascript"></script>
 	<script src="../../../../../Javascript/documentos.js" type="text/Javascript"></script>
 	<script src="../../../../../PopCalendar/PopCalendar.js" type="text/Javascript"></script>	
    <script src="../../../../../Javascript/modal.js" type="text/Javascript"></script> 
    <script src="Functions/funciones.js" type="text/Javascript"></script>
    <style type="text/css">
        .label{ width: 200px; font-size: 16px; }
        .autocomplete-w1 { position:absolute; top:0px; left:0px; margin:6px 0 0 6px; /* IE6 fix: */ _background:none; _margin:1px 0 0 0; }
        .autocomplete { font-size:11px; border:1px solid #999; background:#FFF; cursor:default; text-align:left; max-height:350px; overflow:auto; margin:-6px 6px 6px -6px; /* IE6 specific: */ _height:350px;  _margin:0; _overflow-x:hidden; }
        .autocomplete .selected { background:#F0F0F0; }
        .autocomplete div { padding:2px 5px; white-space:nowrap; overflow:hidden; }
        .autocomplete strong { font-weight:normal; color:#3399FF; }
    </style>
</head>
<body style="OVERFLOW: hidden" leftMargin="10" onLoad="init()"  onbeforeunload="UnloadValor();">
<form id="formDetCert" name="formDetCert" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
    <script type="text/javascript">
    var strServer = "<% =Session["strServer"].ToString() %>";
    var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
    var bCambios = false;
    //Para grabar documento cuando el certificado no está grabado
    var sIDDocuAux = "<% =sIDDocuAux %>";   
    ocultarProcesando();    
</script>

<asp:Image ID="imgEstado" runat="server" ImageUrl="~/Images/imgSeparador.gif" style="visibility:visible; position:absolute; top:3px; right:40px;" />
<asp:Image ID="imgInfoEstado" runat="server" ImageUrl="~/Images/info.gif" style="visibility:hidden; position:absolute; top:16px; right:45px;" />
<asp:Image ID="imgHistorial" runat="server" ImageUrl="~/Images/imgHistorial.gif" style="visibility:hidden; position:absolute; top:14px; right:15px; cursor:pointer;" onclick="getHistorial()" title="Historial de estados" />
<fieldset id="fstDatosCert" style="width:470px; height:70px; margin-left:10px; margin-top:18px;">
    <legend>Certificado</legend>    
     <table id="Table1" style="width:470px; margin-top:5px;" cellspacing="3" cellpadding="3" border="0">
    <colgroup>
        <col style="width:100px;"/>
        <col style="width:100px;" />
        <col style="width:100px;" />
        <col style="width:170px;" />
    </colgroup>
    <tr>
        <td>
            <label class="label" style="display:inline;">Denominación</label><label style="color:Red;margin-left:3px">*</label>
        </td>
        <td colspan="3">
            <label title="" onmouseover="this.title=$I('txtDenom').value;TTip(event);">
                <input name="txtDenom" id="txtDenom" class="txtM" runat="server" style="width:330px;" maxlength="150" Text="" watermarktext="Ej: SQL FOR BEGGINERS"/>
            </label>            
        </td>
    </tr>
    <tr>
        <td>
            <label class="label" style="display:inline;">F. Obtención</label><asp:Label ID="Label1" runat="server" ForeColor="Red" style="margin-left:3px">*</asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtFechaO" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal"></asp:TextBox>
        </td>        
    </tr>
   
    </table>
    </fieldset>
    <fieldset id="fstAdjunto" style="width:470px; height:60px; margin-left:10px; margin-top:10px;">
    <legend>Archivo adjunto</legend>
    <table id="Table2" style="width:470px; margin-top:5px;" cellspacing="3" cellpadding="3" border="0">
    <colgroup>
        <col style="width:100px;"/>
        <col style="width:370px;" />
    </colgroup>
    <tr>
        <td>
            <label title="Documentación acreditativa del certificado">Documento</label><label style="color:Red;margin-left:3px">*</label>
        </td>
        <td>            
            <asp:TextBox ID="txtNombreDocumento" runat="server" style="width:294px;" ReadOnly="true" Text="" />
            <img id="imgUploadDoc" runat="server" src="../../../../../Images/imgUpload.png" title="Seleccionar documento" onclick="addDoc('doc');" style="cursor:pointer; vertical-align:middle;"/>
        </td>
    </tr>
    </table>
</fieldset>	
<center>
    <table id="tblBotones" style="width:430px; margin-top:10px; display:inline-table;" border="0" runat="server">
        <tr>
            <td>
		        <center>		       
		        <button id="btnEnviar" type="button" onclick="enviar();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
			         onmouseover="se(this, 25);mostrarCursor(this);" style="display:none;margin-left:10px; margin-top:5px;">
			        <img src="../../../../../Images/botones/imgGrabar.gif" /><span>Grabar</span><%--Images/imgEnviarValidar.png--%>
		        </button>			        
		        <button id="btnCancelar" type="button" onclick="salir();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
			         onmouseover="se(this, 25);mostrarCursor(this);" style="display:inline; margin-left:10px; margin-top:5px;">
			        <img src="../../../../../Images/botones/imgCancelar.gif" /><span title="Cerrar la pantalla actual">Cancelar</span>
		        </button>
		       
		        </center>	
            </td>
        </tr>
    </table>
</center>

<div class="ocultarcapa">
<input  name="hdnErrores" id="hdnErrores" runat="server" />
<input  name="hdnIdExamen" id="hdnIdExamen" runat="server" />
<input  name="hdnIdExamenOld" id="hdnIdExamenOld" runat="server" value="-1" />
<input  name="hdnIdFicepi" id="hdnIdFicepi" runat="server" />
<input  name="hdnEsEncargado" id="hdnEsEncargado" runat="server" />
<input  name="hdnCaso" id="hdnCaso" runat="server" />
<input  name="hdnIdCertificado" id="hdnIdCertificado" runat="server" />
<input  name="hdnIdFicepiCert" id="hdnIdFicepiCert" runat="server"/>
<input  name="hdnEstado" id="hdnEstado" runat="server" />
<input  name="hdnCambioDoc" id="hdnCambioDoc"  runat="server" value ="false"/>
<input  name="hdnEntCert" id="hdnEntCert" runat="server" />
<input  name="hdnEntorno" id="hdnEntorno" runat="server" />
<input  name="hdnOP" id="hdnOP"  runat="server" />
<input type="hidden" name="hdnTipo" id="hdnTipo" value="C" runat="server" />
<input type="hidden" name="hdnIdSolicitud" id="hdnIdSolicitud" value="-1" />
<input type="hidden"  name="hdnHayExamenes" id="hdnHayExamenes" value="N" runat="server" />
<input type="hidden" runat="server" name="hdnEsMiCV" id="hdnEsMiCV" value="N" />
<input type="hidden" runat="server" name="hdnUserAct" id="hdnUserAct" value="" />
<input type="hidden" runat="server" name="hdnProf" id="hdnProf" value="-1" />
<input type="hidden" name="hdnAccion" id="hdnAccion" value="" runat="server" />
<input type="hidden" name="hdnMotivo" id="hdnMotivo" value="" runat="server" />
<input type="hidden" name="hdnNomDoc" id="hdnNomDoc" value="" runat="server" />
<input type="hidden" name="hdnUsuTick" id="hdnUsuTick" value="" runat="server" />
<input type="hidden" name="hdnContentServer" id="hdnContentServer" value="" runat="server"/>
<input type="hidden" runat="server" name="hdnMsgCumplimentar" id="hdnMsgCumplimentar" value="" />

</div>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
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
    <iframe id="iFrmSubida" frameborder="no" name="iFrmSubida" width="10px" height="10px" style="visibility:hidden" ></iframe>
</body>
</html>
