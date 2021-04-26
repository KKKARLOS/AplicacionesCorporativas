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
    <title> ::: SUPER ::: - <%=TipoPerfil %> en la Experiencia profesional</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <link rel="stylesheet" href="../../../../../App_Themes/Corporativo/Corporativo.css" type="text/css" />
    <link rel="stylesheet" href="../../../../../PopCalendar/CSS/Classic.css" type="text/css" />
    <script src="../../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script src="Functions/funciones.js" type="text/Javascript"></script>
  	<script src="../../../../../Javascript/boxover.js" type="text/Javascript"></script>
    <script src="../../../../../PopCalendar/PopCalendar.js" type="text/Javascript"></script>
    <script src="../../../../../Javascript/dhtmltooltip.js" type="text/Javascript"></script>
    <script src="../../../../../Javascript/modal.js" type="text/Javascript"></script>
    <style type="text/css">
    .camporeq{color:Red;}
    </style>
</head>
<body onload="init();" onunload="unload()">
<form id="frmPrincipal" name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
<script type="text/javascript">
    var intSession = <%=Session.Timeout%>; 
    var strServer = "<%=Session["strServer"]%>";  
    //Para el comportamiento de los calendarios
    var btnCal = "<%=Session["BTN_FECHA"].ToString() %>";
</script>
<asp:Image ID="imgEstado" runat="server" ImageUrl="~/Images/imgSeparador.gif" style="visibility:visible; position:absolute; top:3px; right:40px;" />
<asp:Image ID="imgInfoEstado" runat="server" ImageUrl="~/Images/info.gif" style="visibility:hidden; position:absolute; top:16px; right:45px;" />
<asp:Image ID="imgHistorial" runat="server" ImageUrl="~/Images/imgHistorial.gif" style="visibility:hidden; position:absolute; top:3px; right:15px; cursor:pointer;" onclick="getHistorial()" title="Historial de estados" />
<fieldset id="fstDatosExp" style="width:520px; height:60px; margin-left:10px; margin-top:10px;">
    <legend>Datos Experiencia Profesional</legend>    
     <table cellspacing="0"  style="width:520px; table-layout:fixed; margin-left:5px; margin-top:5px;" border="0" cellpadding="3">
        <colgroup>
            <col style="width:90px;"/>
            <col style="width:420px;" />
        </colgroup>
        <tr>
            <td>
                <label id="lblExp" class="label" title="Denominación de la experiencia profesional">Exp. Prof.</label>
            </td>
            <td>
                <asp:TextBox ID="txtDen" style="width:410px;" Text="" runat="server" maxlength='70' ReadOnly="true" />
            </td>
            
        </tr>
        <tr>
            <td>
                <label id="lblProfesional" class="label">Profesional</label>
            </td>
            <td>
                <asp:TextBox ID="txtDenProf" style="width:410px;" Text="" runat="server" ReadOnly="true" />
            </td>
        </tr>
    </table>
</fieldset>
<fieldset id="fstDatosPerfil" style="width:520px; height:170px; margin-left:10px; margin-top:10px;">
    <legend>Datos Perfil</legend>  
    <table style="width:515px; table-layout:fixed; margin-left:5px; margin-top:5px;" border="0" cellspacing="0" cellpadding="3">
        <colgroup>
            <col style="width:60px;"/>
            <col style="width:70px;"/>
            <col style="width:50px;"/>
            <col style="width:120px;"/>
            <col style="width:115px;" />
        </colgroup>
        <tr>
            <td>
                <label id="lblPerfil" class="label" >Perfil&nbsp;</label><label style="color:Red;">*</label>
            </td>
            <td colspan="4">
                <asp:DropDownList ID="cboPerfil" runat="server" onchange="activarGrabar();"></asp:DropDownList>
                &nbsp;
                <asp:Image ID="imgInfoPerfil" runat="server" ImageUrl="~/Images/imgInfoFechas.png" style="visibility:hidden; position:relative; vertical-align:middle;" />
            </td>
        </tr>
        <tr>
            <td><label id="Label1" class="label" >Idioma</label></td>
            <td colspan="4">
                <asp:DropDownList ID="cboIdioma" runat="server" onchange="activarGrabar();"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <label id="lblFI" class="label">Fecha Inicio&nbsp;</label><label style="color:Red;">*</label>
            </td>
            <td>
                <asp:TextBox ID="txtFI" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" onchange="activarGrabar();controlarFecha('I');" goma="0"></asp:TextBox>
            </td>
            <td>  
                <label id="lblFF" class="label">Fecha Fin</label>
            </td>
            <td  colspan="2">
                <label id="lblFFin" runat="server" style="color:Red;">*</label>
                <asp:TextBox ID="txtFF" runat="server" style="width:60px;cursor:pointer; margin-left:2px;" Calendar="oCal" cRef="txtFI" onchange="activarGrabar();controlarFecha('F');" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <label id="lblFun" class="label">Funciones.&nbsp;</label>
                <label id="Label4" style="font-weight:bold; text-decoration:underline;">Describe las tareas que has realizado en la experiencia&nbsp;</label>
                <label style="color:Red;">*</label>
                <br />
                <asp:TextBox ID="txtFun" SkinID="multi" runat="server" TextMode="MultiLine" Rows="4" style="width:490px; resize:none; margin-top:2px;" onkeydown="activarGrabar();"></asp:TextBox>
            </td>
        </tr>
        </table>
</fieldset>
<table>
<tr>
    <td>
        <fieldset style="width:520px; margin-left:10px; margin-top:10px;">
        <legend>Entornos tecnológicos/funcionales&nbsp;<label style="color:Red;">*</label></legend>
            <table style="width: 470px; height: 17px; margin-top:5px; margin-left:10px;">
                <tr class="TBLINI">
                    <td>
                        &nbsp;&nbsp;Denominación
                    </td>
                </tr>
            </table>
            <div id="divEnt" style="overflow: auto; width: 486px; height:48px; margin-left:10px;">
                <div style='background-image:url(../../../../../Images/imgFT16.gif); width:470px;'>
                    <%=strHTMLEntorno%>
                </div>
            </div>
            <table id="Table5" style="width:470px; height:17px; margin-left:10px;">
	            <tr class="TBLFIN"><td></td></tr>
            </table>
            <table style="width:300px; margin-top:5px; margin-left:130px;">
                <tr>
	                <td align="center" width="50%">
		                <button id="btnNewET" type="button" onclick="nuevoET()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
			                 onmouseover="se(this, 25);mostrarCursor(this);">
			                <img src="../../../../../images/botones/imgAnadir.gif" /><span title="Añadir">Añadir</span>
		                </button>	
	                </td>
	                <td width="50%">
		                <button id="btnDelET" type="button" onclick="EliminarET()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
			                 onmouseover="se(this, 25);mostrarCursor(this);">
			                <img src="../../../../../images/botones/imgEliminar.gif" /><span title="Eliminar">Eliminar</span>
		                </button>	
	                </td>
                </tr>
            </table>    
        </fieldset>
    </td>
</tr>
<tr>
    <td>
        <label id="lblObs" class="label" style="margin-left:10px; margin-top:8px;">Observaciones</label><br />
        <asp:TextBox ID="txtObs" SkinID="multi" runat="server" TextMode="MultiLine" Rows="6" style="width:514px; resize:none; margin-left:10px; margin-top:2px;" onkeydown="activarGrabar();"></asp:TextBox>
    </td>
</tr>
</table>
<center>
<table id="tblBotones" style="width:430px; margin-top:10px; display:inline-table;" border="0" runat="server">
        <tr>
            <td>
		        <center>
		        <button id="btnAparcar" type="button" onclick="aparcar();" class="btnH25W140" runat="server" hidefocus="hidefocus" 
			         onmouseover="se(this, 25);mostrarCursor(this);" style="display:none;margin-left:10px; margin-top:5px;">
			        <img src="../../../../../Images/imgBorrador.png" /><span title='Guarda la información como borrador'>Guardar borrador</span>
		        </button>
		        <button id="btnValidar" type="button" onclick="validar();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
			         onmouseover="se(this, 25);mostrarCursor(this);" style="display:none;margin-left:10px; margin-top:5px;"><%--Images/imgValidar.png--%>
			        <img src="../../../../../Images/botones/imgGrabar.gif" /><span title=''>Grabar</span>
		        </button>
		        <button id="btnEnviar" type="button" onclick="enviar();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
			         onmouseover="se(this, 25);mostrarCursor(this);" style="display:none;margin-left:10px; margin-top:5px;">
			        <img src="../../../../../Images/botones/imgGrabar.gif" /><span>Grabar</span><%--Images/imgEnviarValidar.png--%>
		        </button>	
		        <button id="btnCumplimentar" type="button" onclick="cumplimentar();" class="btnH25W170" runat="server" hidefocus="hidefocus" 
			         onmouseover="se(this, 25);mostrarCursor(this);" style="display:none;margin-left:10px; margin-top:5px;">
			        <img src="../../../../../Images/imgEnviarCumplimentar.png" /><span title='Devolver los datos al profesional para que los modifique/complete'>Enviar a cumplimentar</span>
		        </button>
		        <button id="btnRechazar" type="button" onclick="rechazar();" class="btnH25W130" runat="server" hidefocus="hidefocus" 
			         onmouseover="se(this, 25);mostrarCursor(this);" style="display:none;margin-left:10px; margin-top:5px;">
			        <img src="../../../../../Images/imgRechazar.png" /><span title='Este dato será visible únicamente por el profesional'>Info. privada</span>
		        </button>
		        <button id="btnCancelar" type="button" onclick="cancelar();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
			         onmouseover="se(this, 25);mostrarCursor(this);" style="display:inline-block; margin-left:10px; margin-top:5px;">
			        <img src="../../../../../Images/botones/imgCancelar.gif" /><span title="Cerrar la pantalla actual">Cancelar</span>
		        </button>
		        <button id="btnSalir" type="button" onclick="salir();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
			         onmouseover="se(this, 25);mostrarCursor(this);" style="display:none;margin-left:10px; margin-top:5px;">
			        <img src="../../../../../Images/botones/imgSalir.gif" /><span title="Salir">Salir</span>
		        </button>
		        </center>	
            </td>
        </tr>
    </table>
</center>

<div id="divFondoMotivo" style="z-index:10; position:absolute; left:0px; top:0px; width:600px; height:670px; background-image: url(../../../../../Images/imgFondoOscurecido.png); background-repeat:repeat; visibility:hidden;" runat="server">
    <div id="divMotivo" style="position:absolute; top:225px; left:80px;">
        <table border="0" cellspacing="0" cellpadding="0" style="width:420px;margin-top:5px;">
          <tr>
            <td width="6" height="6" background="../../../../../Images/Tabla/7.gif"></td>
            <td height="6" background="../../../../../Images/Tabla/8.gif"></td>
            <td width="6" height="6" background="../../../../../Images/Tabla/9.gif"></td>
          </tr>
          <tr>
            <td width="6" background="../../../../../Images/Tabla/4.gif">&nbsp;</td>
            <td background="../../../../../Images/Tabla/5.gif" style="padding:3px; vertical-align:top;">
            <!-- Inicio del contenido propio de la página -->
            <table id="Table1" class="texto" style="width:400px;" cellspacing="0" cellpadding="5" border="0">
                <tr>
                    <td colspan="2">
                        <label id="lblMotivo" style="display:inline;">Establece las indicaciones oportunas para que el profesional complete correctamente la información&nbsp;</label><asp:Label ID="Label3" runat="server" ForeColor="Red" style="display:inline;">*</asp:Label><br />
                        <asp:TextBox TextMode="MultiLine" id="txtMotivoRT" style="width:390px; height:100px; margin-top:2px;" runat="server" ></asp:TextBox>
                    </td>
                </tr>
            </table>
            <center>
            <table id="Table6" class="texto" style="width:240px; margin-top:10px;" cellspacing="0" cellpadding="5" border="0">
                <tr>
                    <td>
                        <button id="btnAceptarMotivo" type="button" class="btnH25W95" runat="server" hidefocus="hidefocus"
                            onclick="AceptarMotivo();" onmouseover="se(this, 25);">
                            <img src="../../../../../Images/imgAceptar.gif" /><span>Aceptar</span>
                        </button>
                    </td>
                    <td>
                        <button id="btnCancelarMotivo" type="button" class="btnH25W95" runat="server" hidefocus="hidefocus"
                            onclick="CancelarMotivo();" onmouseover="se(this, 25);">
                            <img src="../../../../../Images/Botones/imgCancelar.gif" /><span>Cancelar</span>
                        </button>
                    </td>
                </tr>
            </table>
            </center>
                <!-- Fin del contenido propio de la página -->
                </td>
                <td width="6" background="../../../../../Images/Tabla/6.gif">&nbsp;</td>
              </tr>
          <tr>
            <td width="6" height="6" background="../../../../../Images/Tabla/1.gif"></td>
            <td height="6" background="../../../../../Images/Tabla/2.gif"></td>
            <td width="6" height="6" background="../../../../../Images/Tabla/3.gif"></td>
          </tr>
        </table>
    </div>
</div>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<input type="hidden" runat="server" name="hdnProf" id="hdnProf" value="-1" />
<input type="hidden" runat="server" name="hdnEsAdmin" id="hdnEsAdmin" value="0" />
<input type="hidden" runat="server" name="hdnOrigen" id="hdnOrigen" value="-1" />
<input type="hidden" runat="server" name="hdnProfVal" id="hdnProfVal" value="-1" />
<input type="hidden" runat="server" name="hdnPlant" id="hdnPlant" value="" />
<input type="hidden" runat="server" name="hdnEP" id="hdnEP" value="-1" />
<input type="hidden" runat="server" name="hdnEPF" id="hdnEPF" value="-1" />
<input type="hidden" runat="server" name="hdnEFP" id="hdnEFP" value="-1" />
<input type="hidden" runat="server" name="hdnModo" id="hdnModo" value="W" />
<input type="hidden" runat="server" name="hdnEmp" id="hdnEmp" value="-1" />
<input type="hidden" runat="server" name="hdnEnIb" id="hdnEnIb" value="S" />
<input type="hidden" runat="server" name="hdnMantenedor" id="hdnMantenedor" value="N" />
<input type="hidden" runat="server" name="hdnValidador" id="hdnValidador" value="N" />
<input type="hidden" runat="server" name="hdnVisibleCV" id="hdnVisibleCV" value="1" />
<input type="hidden" runat="server" name="hdnEstadoNuevo" id="hdnEstadoNuevo" value="" />
<input type="hidden" runat="server" name="hdnEstadoInicial" id="hdnEstadoInicial" value="B" />
<input type="hidden" runat="server" name="hdnMotivo" id="hdnMotivo" value="" />
<input type="hidden" runat="server" name="hdnPlantilla" id="hdnPlantilla" value="M" />
<input type="hidden" runat="server" name="hdnFechaIni" id="hdnFechaIni" value="" />
<input type="hidden" runat="server" name="hdnFechaFin" id="hdnFechaFin" value="" />
<input type="hidden" runat="server" name="hdnSegmentoC" id="hdnSegmentoC" value="" />
<input type="hidden" runat="server" name="hdnEsMiCV" id="hdnEsMiCV" value="N" />
<input type="hidden" runat="server" name="hdnUserAct" id="hdnUserAct" value="" />
<input type="hidden" runat="server" name="hdnMsgCumplimentar" id="hdnMsgCumplimentar" value="" />
<input type="hidden" runat="server" name="hdnLigadoProy" id="hdnLigadoProy" value="N" />
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

