<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" ValidateRequest="false" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Experiencia profesional en Ibermática</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <link rel="stylesheet" href="../../../../../App_Themes/Corporativo/Corporativo.css" type="text/css" />
    <script src="../../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script src="Functions/funciones.js" type="text/Javascript"></script>
  	<script src="../../../../../Javascript/boxover.js" type="text/Javascript"></script>
  	<script src="../../../../../Javascript/dhtmltooltip.js" type="text/Javascript"></script>
    <script src="../../../../../Javascript/modal.js" type="text/Javascript"></script>
    <style type="text/css">
    .camporeq{
        color:Red;
    }
    </style>
</head>
<body onload="init();" onbeforeunload="unload()">
<form id="Form1" name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
<script type="text/javascript">
    var intSession = <%=Session.Timeout%>; 
    var strServer = "<%=Session["strServer"]%>";  
    //Para el comportamiento de los calendarios
    var btnCal = "<%=Session["BTN_FECHA"].ToString() %>";
    //Para mensajes emergentes
    var sTareasPendientes = "<% =sTareasPendientes %>";  
    var sMOSTRAR_SOLODIS = "<%=ConfigurationManager.AppSettings["MOSTRAR_SOLODIS"].ToString() %>";
    var es_DIS = <%=(User.IsInRole("DIS"))? "true":"false" %>;       
</script>
<br />
<fieldset style="width:930px; margin-left:13px;">
<legend>Experiencia profesional</legend>
<table cellspacing="0" class="texto" style="width: 920px; table-layout:fixed; " border="0" cellpadding="3">
    <colgroup>
        <col style="width:465px" />
        <col style="width:455px" />
    </colgroup>
    <tr>
        <td>
            <table cellpadding="3px" border="0">
                <colgroup><col style="width:90px"/><col style="width:375px"/></colgroup>
                <tr>
                    <td style="vertical-align:top;">
                        <label style="width:70px;">Denominación</label><label style="color:Red;">*</label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDen" style="width:365px; " Text="" runat="server" maxlength='70' onkeydown="activarGrabar1();"/>
                        <asp:Image ID="imgInfoExpProy" runat="server" ImageUrl="~/Images/imgInfoExpProy.png" style="position:absolute; top:80px; left:35px; cursor:pointer;" onclick="getProyExp()" Visible="false" ToolTip="Muestra la relación de proyectos asociados a la experiencia profesional." />
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align:top;">
                        <label style="width:65px;">Descripción</label><label style="color:Red;">*</label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDes" SkinID="Multi" TextMode="MultiLine" onkeydown="activarGrabar1();"
                            style="width:365px; height:260px; resize:none; " Text="" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align:middle;">
                        <label id="lblCliente" style="width:40px;" onclick="getCliente();" class="enlace">Cliente</label><label style="color:Red;">*</label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCliente" style="width:345px;" Text="" runat="server" ReadOnly="true" />
                        <img id="imgGomaCliente" src='../../../../../images/botones/imgBorrar.gif' title="Borra el cliente" onclick="borrarCliente();" style="cursor:pointer; vertical-align:middle; border:0px;" runat="server"/>
                    </td>
                </tr>
            </table>
        </td>
        <td>
            <fieldset style="width:440px;">
            <legend>Áreas de conocimiento tecnológico&nbsp;<label style="color:Red;">*</label></legend>
                <table id="tblCab1" style="width: 420px; height: 17px; margin-left:10px; margin-top:5px;" cellspacing="0" border="0">
                    <tr class="TBLINI">
                        <td>
                            &nbsp;&nbsp;Áreas
                        </td>
                    </tr>
                </table>
	            <div id="divConTec" style="overflow: auto; width: 436px; height:64px; margin-left:10px;">
	                <div style='background-image:url(../../../../../Images/imgFT16.gif); width:420px;'>
	                    <%=strTablaHtmlConTec %>
	                </div>
                </div>
	            <table id="Table1" style="width:420px; height:17px; margin-left:10px;" cellspacing="0" border="0">
		            <tr class="TBLFIN"><td></td></tr>
	            </table>
	            <table style="width:300px; margin-top:5px; margin-left:100px;">
	            <tr>
		            <td align="center" width="50%" >
			            <button id="btnNewACT" type="button" onclick="nuevoACT()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				             onmouseover="se(this, 25);mostrarCursor(this);">
				            <img src="../../../../../images/botones/imgAnadir.gif" /><span title="Añadir">Añadir</span>
			            </button>	
		            </td>
		            <td width="50%">
			            <button id="btnDelACT" type="button" onclick="EliminarACT()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				             onmouseover="se(this, 25);mostrarCursor(this);">
				            <img src="../../../../../images/botones/imgEliminar.gif" /><span title="Eliminar">Eliminar</span>
			            </button>	
		            </td>
	            </tr>
	            </table>    
            </fieldset>
            <fieldset style="margin-top:10px; width:440px;">
            <legend>Áreas de conocimiento sectorial&nbsp;<label style="color:Red;">*</label></legend>
                <table id="Table2" style="width: 420px; height: 17px; margin-top:5px; margin-left:10px;" cellspacing="0" border="0">
                    <tr class="TBLINI">
                        <td>
                            &nbsp;&nbsp;Áreas
                        </td>
                    </tr>
                </table>
	            <div id="divConSec" style="overflow: auto; width: 436px; height:64px; margin-left:10px;">
	                <div style='background-image:url(../../../../../Images/imgFT16.gif); width:420px;'>
	                    <%=strTablaHtmlConSec %>
	                </div>
                </div>
	            <table id="Table4" style="width:420px; height:17px; margin-left:10px;" cellspacing="0" border="0">
		            <tr class="TBLFIN"><td></td></tr>
	            </table>
	            <table style="width:300px; margin-top:5px; margin-left:100px;">
	            <tr>
		            <td align="center" width="50%">
			            <button id="btnNewACS" type="button" onclick="nuevoACS()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				             onmouseover="se(this, 25);mostrarCursor(this);">
				            <img src="../../../../../images/botones/imgAnadir.gif" /><span title="Añadir">Añadir</span>
			            </button>	
		            </td>
		            <td width="50%">
			            <button id="btnDelACS" type="button" onclick="EliminarACS()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				             onmouseover="se(this, 25);mostrarCursor(this);">
				            <img src="../../../../../images/botones/imgEliminar.gif" /><span title="Eliminar">Eliminar</span>
			            </button>	
		            </td>
	            </tr>
	            </table>    
            </fieldset>
        </td>
    </tr>
</table>
</fieldset>
<table cellspacing="0" class="texto" style="width: 950px; table-layout:fixed; margin-left:10px;" border="0" cellpadding="3">
    <tr>
        <td>
            <fieldset style="margin-left:150px; margin-top:5px; width:620px;">
            <legend>Perfiles. Añade el perfil y tareas realizadas en la experiencia <label style="color:Red;">*</label></legend>
                <table id="Table3" style="width: 580px; height: 17px; margin-top:5px; margin-left:10px;" cellspacing="0" border="0">
                    <colgroup>
                        <col style="width:20px" />
                        <col style="width:20px" />
                        <col style="width:400px" />
                        <col style="width:70px" />
                        <col style="width:70px" />
                    </colgroup>
                    <tr class="TBLINI">
                        <td></td>
                        <td></td>
                        <td>Denominación</td>
                        <td>Inicio</td>
                        <td>Fin</td>
                    </tr>
                </table>
	            <div id="divEnt" style="overflow: auto; width: 596px; height:140px; margin-left:10px;">
	                <div style='background-image:url(../../../../../Images/imgFT20.gif); background-repeat:repeat; width:580px; height:auto;'>
	                    <%=strTablaHtml%>
	                </div>
                </div>
	            <table id="Table5" style="width:580px; height:17px; margin-left:10px;" cellspacing="0" border="0">
		            <tr class="TBLFIN"><td></td></tr>
	            </table>
	            <table style="width:300px; margin-top:5px; margin-left:175px;">
	                <tr>
		                <td align="center" ><%--width="50%"--%>
			                <button id="btnNewPerf" type="button" onclick="nuevoPerf()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				                 onmouseover="se(this, 25);mostrarCursor(this);">
				                <img src="../../../../../images/botones/imgAnadir.gif" /><span title="Añadir">Añadir</span>
			                </button>	
		                </td>
		                <td>
			                <button id="btnDelPerf" type="button" onclick="EliminarPerf()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				                 onmouseover="se(this, 25);mostrarCursor(this);">
				                <img src="../../../../../images/botones/imgEliminar.gif" /><span title="Eliminar">Eliminar</span>
			                </button>	
		                </td>
	                </tr>
	            </table>    
                <div style="margin-top: 10px; margin-left:5px;" class="texto">
                    <img src="../../../../../Images/imgPenCumplimentar.png" class="ICO" />Pendiente de cumplimentar 
                    <img src="../../../../../Images/imgBorrador.png" class="ICO" style="margin-left:15px;" />Borrador 
                    <%--<img src="../../../../../Images/imgPseudovalidado.png" class="ICO" style="margin-left:15px;" />Pseudovalidado--%>
                    <img src="../../../../../Images/imgPetBorrado.png" class="ICO" style="margin-left:15px;" />Pdte de eliminar
                    <img src="../../../../../Images/imgPenValidar.png" class="ICO" style="visibility:hidden;" /> 
                    <img src="../../../../../Images/imgRechazar.png" class="ICO" style="visibility:hidden;" />
                </div>
            </fieldset>
        </td>
    </tr>
</table>
<table id="tblBotones" style="width:340px; margin-left:240px; margin-top:10px;">
    <tr>
	    <td align="center">
            <button id="btnExpRevisada" type="button" onclick="setRevisadaExper();" class="btnH25W100" style="display:none" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);" title="Doy por finalizada la revisión y actualización de todos los perfiles de la experiencia.">
                <img src="../../../../../Images/imgCVOK.png" /><span>&nbsp;&nbsp;Revisado</span>
            </button>			    
	    </td>    
	    <td align="center">
			<button id="btnGrabar" type="button" onclick="grabarAux();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../../../images/botones/imgGrabar.gif" /><span>&nbsp;&nbsp;Grabar</span>
			</button>	
	    </td>
        <!--<td align="center">
		    <button id="btnGuia" type="button" onclick="mostrarGuia('ExperienciaProfesional.pdf')" class="btnH25W95" runat="server" hidefocus="hidefocus" 
			     onmouseover="se(this, 25);mostrarCursor(this);">
			    <img src="../../../../../images/botones/imgGuia.gif" /><span>&nbsp;&nbsp;Guía</span>
		    </button>	
        </td>-->	
	    <td align="center">
			<button id="btnSalir" type="button" onclick="salir();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../../../images/botones/imgSalir.gif" /><span>&nbsp;&nbsp;Salir</span>
			</button>	 
	    </td>
    </tr>
</table>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<input type="hidden" runat="server" name="hdnUserAct" id="hdnUserAct" value="" />
<input type="hidden" runat="server" name="hdnNomProf" id="hdnNomProf" value="" />
<input type="hidden" runat="server" name="hdnProf" id="hdnProf" value="-1" />
<input type="hidden" runat="server" name="hdnProfVal" id="hdnProfVal" value="-1" />
<input type="hidden" runat="server" name="hdnEsPlant" id="hdnEsPlant" value="N" />
<input type="hidden" runat="server" name="hdnTieneProy" id="hdnTieneProy" value="N" />
<input type="hidden" runat="server" name="hdnEP" id="hdnEP" value="-1" />
<input type="hidden" runat="server" name="hdnModo" id="hdnModo" value="R" />
<input type="hidden" runat="server" name="hdnCtaOri" id="hdnCtaOri" value="-1" />
<input type="hidden" runat="server" name="hdnCtaDes" id="hdnCtaDes" value="-1" />
<input type="hidden" runat="server" name="hdnCli" id="hdnCli" value="-1" />
<input type="hidden" runat="server" name="hdnEmp" id="hdnEmp" value="-1" />
<input type="hidden" runat="server" name="hdnEnIb" id="hdnEnIb" value="S" />
<input type="hidden" runat="server" name="hdnEsEncargado" id="hdnEsEncargado" value="N" />
<input type="hidden" runat="server" name="hdnVisibleCV" id="hdnVisibleCV" value="1" />
<input type="hidden" runat="server" name="hdnEsMiCV" id="hdnEsMiCV" value="N" />
<input type="hidden" runat="server" name="hdnidExpFicepi" id="hdnidExpFicepi" value="-1" />
<asp:TextBox ID="txtValidador" style="width:5px; visibility:hidden;" Text="" runat="server" readonly="true" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
<uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
</form>
<script type="text/javascript">
	function __doPostBack(eventTarget, eventArgument) {
		var bEnviar = true;
		var theform;
		if (window.navigator.appName.toLowerCase().indexOf("microsoft") > -1) {
			theform = document.forms[0];
		}
		else {
			theform = document.forms["frmPrincipal"];
		}
			
		theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
		theform.__EVENTARGUMENT.value = eventArgument;
		if (bEnviar){
			theform.submit();
		}
		else{
			$I("Botonera").restablecer();
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
</body>
</html>

