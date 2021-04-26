<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" ValidateRequest="false" EnableEventValidation="false" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Detalle de acción</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <link rel="stylesheet" href="../../../../../PopCalendar/CSS/Classic.css" type="text/css" />
    <script src="../../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script src="../../../../../Javascript/draganddrop.js" type="text/Javascript"></script>
	<script src="Functions/funciones.js" type="text/Javascript"></script>
	<script src="../../../../../Javascript/documentos.js" type="text/Javascript"></script>
    <script src="../../../../../PopCalendar/PopCalendar.js" type="text/Javascript"></script>
 	<script src="../../../../../Javascript/boxover.js" type="text/Javascript"></script>
 	<script src="../../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onload="init()" onunload="unload()">
<form id="Form1" name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
    <style type="text/css">
        #tsPestanas table { table-layout:auto; }
    </style>
    <script type="text/javascript">
        var strServer = "<% =Session["strServer"].ToString() %>";
        var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
        var sNumEmpleado = "<% =Session["NUM_EMPLEADO_ENTRADA"].ToString() %>";
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
        var bCambios = false;
        var bLectura = <%=sLectura%>;
        var bSalir = false;
        mostrarProcesando();
        //Variables a devolver a la estructura.
        var sIdAccion="";
        var sDescripcion = "";
        var sFLI = "";
        var sFFV = "";
        var sAvance="";
        /* Objeto calendario */
	    var oCal=PopCalendar.newCalendar();
	    oCal.imgDir = strServer + 'PopCalendar/CSS/Classic_Images/';
	    oCal.initCalendar();
        //Para el comportamiento de los calendarios
        var btnCal = "<%=Session["BTN_FECHA"].ToString() %>";
    </script>    

<table id="tabla" style="width:985px; margin-top:10px; margin-left:20px;">
	<tr>
		<td>
            <eo:TabStrip runat="server" id="tsPestanas" ControlSkinID="None" Width="980px" MultiPageID="mpContenido" ClientSideOnLoad="CrearPestanas">
	            <TopGroup OverlapDepth="0" Style-CssClass="TabStrip">
		            <Items>
				            <eo:TabItem Text-Html="General" ToolTip="" Width="120"></eo:TabItem>
				            <eo:TabItem Text-Html="Documentación" ToolTip="" Width="120"></eo:TabItem>
		            </Items>
	             </TopGroup>
            <LookItems>
                <eo:TabItem ItemID="_Default" 
                    LeftIcon-Url="~/Images/Pestanas/normal_left.gif"
                    LeftIcon-HoverUrl="~/Images/Pestanas/hover_left.gif"
                    LeftIcon-SelectedUrl="~/Images/Pestanas/selected_left.gif"
                    Image-Url="~/Images/Pestanas/normal_bg.gif"
                    Image-HoverUrl="~/Images/Pestanas/hover_bg.gif" 
                    Image-SelectedUrl="~/Images/Pestanas/selected_bg.gif" 
                    RightIcon-Url="~/Images/Pestanas/normal_right.gif"
                    RightIcon-HoverUrl="~/Images/Pestanas/hover_right.gif"
                    RightIcon-SelectedUrl="~/Images/Pestanas/selected_right.gif"
                    NormalStyle-CssClass="TabItemNormal"
                    HoverStyle-CssClass="TabItemHover"
                    SelectedStyle-CssClass="TabItemSelected"
                    DisabledStyle-CssClass="TabItemDisabled"
                    Image-Mode="TextBackground" 
                    Image-BackgroundRepeat="RepeatX">
                </eo:TabItem>
            </LookItems>
            </eo:TabStrip>
            <eo:MultiPage runat="server" id="mpContenido" CssClass="FMP" Width="980px" Height="560px">
                <eo:PageView ID="PageView1" CssClass="PageView" runat="server">				
				<!-- Pestaña 1 General-->
                <table class="texto" style="width:100%;">
                    <tr>
	                    <td>
							<fieldset style="width:932px; margin-left:4px;"><legend>Asunto</legend>
                                <table class="texto" style="width:100%;" cellpadding="5px" cellspacing="0">
                                    <tr>
                                        <td>&nbsp;&nbsp;Número
					                        <asp:TextBox ID="txtIdAsunto" runat="server" SkinID="Numero" style="width:60px;" readonly="true"></asp:TextBox>
					                        &nbsp;&nbsp;&nbsp;&nbsp;Denominación
					                        <asp:TextBox ID="txtDesAsunto" runat="server" style="width:400px" readonly="true" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <br />
	                    </td>
                    </tr>
                    <tr>
	                    <td>
							<fieldset style="width:932px; margin-left:4px;"><legend>Acción</legend>
                                <table class="texto" style="width:100%;" cellpadding="5" cellspacing="0">
                                    <tr>
                                        <td colspan="2">&nbsp;&nbsp;Número
                                            <asp:TextBox ID="txtIdAccion" runat="server" SkinID="Numero" style="width:60px;" readonly="true"></asp:TextBox>
                                            &nbsp;&nbsp;&nbsp;&nbsp;Denominación
                                            <asp:TextBox ID="txtDesAccion" runat="server" TabIndex="1" Style="width: 400px" MaxLength=50 onKeyUp="activarGrabar();"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <label style="width:37px; margin-left:6px;">Límite</label>
                                            <asp:TextBox ID="txtValLim" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" onchange="activarGrabar();focoAvance();" ></asp:TextBox>
                                            &nbsp;&nbsp;&nbsp;&nbsp;Finalización<asp:TextBox ID="txtValFin" runat="server" style="width:60px; cursor:pointer; margin-left:13px;" Calendar="oCal" onchange="activarGrabar();focoAvance();" ></asp:TextBox>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Avance
                                            <asp:DropDownList ID="cboAvance" runat="server" TabIndex="2" style="width:60px;" onchange="activarGrabar();">
                                                <asp:ListItem Value="0" Text="  0"></asp:ListItem>
                                                <asp:ListItem Value="10" Text=" 10"></asp:ListItem>
                                                <asp:ListItem Value="20" Text=" 20"></asp:ListItem>
                                                <asp:ListItem Value="30" Text=" 30"></asp:ListItem>
                                                <asp:ListItem Value="40" Text=" 40"></asp:ListItem>
                                                <asp:ListItem Value="50" Text=" 50"></asp:ListItem>
                                                <asp:ListItem Value="60" Text=" 60"></asp:ListItem>
                                                <asp:ListItem Value="70" Text=" 70"></asp:ListItem>
                                                <asp:ListItem Value="80" Text=" 80"></asp:ListItem>
                                                <asp:ListItem Value="90" Text=" 90"></asp:ListItem>
                                                <asp:ListItem Value="100" Text="100"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
	                                    <td>&nbsp;Descripción<br />
                                        <asp:TextBox ID="txtDescripcion" SkinID="Multi" runat="server" TabIndex="11" TextMode="MultiLine" Rows="5" Width="430px" onKeyUp="activarGrabar();"></asp:TextBox>
	                                    </td>
	                                    <td>&nbsp;&nbsp;Observaciones<br />
                                            <asp:TextBox ID="txtObs" SkinID="Multi" runat="server" TabIndex="12" TextMode="MultiLine" Rows="5" Width="430px" onKeyUp="activarGrabar();"></asp:TextBox>
	                                    </td>
                                    </tr>
                                </table>
                            </fieldset>
	                    </td>
                    </tr>
                    <tr>
                        <td>
							<fieldset style="width:932px; margin-left:4px;"><legend>Asignado a</legend>
                                <table width="100%" cellpadding="5" >
                                    <tr>
                                        <td>
                                            &nbsp;Departamento / Grupo<br />
                                            <asp:TextBox ID="txtDpto" SkinID="Multi" runat="server" TabIndex="13" TextMode="MultiLine" Rows="3" style="width:430px;" onKeyUp="activarGrabar();"></asp:TextBox>
                                        </td>
                                        <td>&nbsp;Notificar a (e-mail separados por ;)<br />
                                            <asp:TextBox ID="txtAlerta" SkinID="Multi" runat="server" TabIndex="14" TextMode="MultiLine" Rows="3" style="width:430px;" onKeyUp="activarGrabar();"></asp:TextBox>
                                       </td>
                                    </tr>
                                </table>
                                <table id="Table6" cellpadding="5" width="100%" >
                                    <colgroup><col style="width:420px;" /><col style="width:40px;" /><col style="width:460px;" /></colgroup>
	                                <tbody>
		                                <tr>
			                                <td colspan="3">
                                                <table style="width: 360px;">
                                                    <colgroup><col style="width:120px;" /><col style="width:120px;" /><col style="width:120px;" /></colgroup>
                                                    <tr>
                                                        <td>&nbsp;Apellido1</td>
                                                        <td>&nbsp;Apellido2</td>
                                                        <td>&nbsp;Nombre</td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:TextBox ID="txtApellido1" runat="server" TabIndex="15" style="width:110px"  onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="50" /></td>
                                                        <td><asp:TextBox ID="txtApellido2" runat="server" TabIndex="16" style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="50" /></td>
                                                        <td><asp:TextBox ID="txtNombre" runat="server" TabIndex="17" style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" onblur="girar();" MaxLength="50" /></td>
                                                    </tr>
                                                </table>
			                                </td>
		                                </tr>
		                                <tr>
			                                <td >
		                                        <table id="tblTitulo" style="height:17px; width:390px;">
			                                        <tr class="TBLINI">
				                                        <td>&nbsp;Profesionales&nbsp;
								                            <img id="imgLupa1" style="display: none; cursor: pointer" onclick="buscarSiguiente('tblOpciones',1,'divCatalogo','imgLupa1', event)"
									                            height="11" src="../../../../../Images/imgLupaMas.gif" width="20" tipolupa="2" />
							                                <img id="imgLupa3" style="display: none; cursor: pointer" onclick="buscarDescripcion('tblOpciones',1,'divCatalogo','imgLupa1', event)"
									                            height="11" src="../../../../../Images/imgLupa.gif" width="20" tipolupa="1" /> 
			                                            </td>
			                                        </tr>
		                                        </table>
		                                        <div id="divCatalogo" style="overflow:auto; width:406px; height:80px;" onscroll="scrollTablaProf()">
			                                         <div style='background-image:url(../../../../../Images/imgFT20.gif); width:390px;'>
			                                             <table class="texto MAM" id="tblOpciones" style="width: 390px"></table>
			                                         </div>
		                                        </div>
		                                        <table id="tblResultado" style="height:17px; width:390px;">
			                                        <tr class="TBLFIN"><td></td></tr>
		                                        </table>
			                                </td>
	                                        <td>
					                            <asp:Image id="imgPapelera" style="CURSOR: pointer" runat="server" ImageUrl="~/Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="3"></asp:Image>
					                        </td>
	                                        <td>
		                                        <table id="tblTitulo2" style="height:17px; width:435px;" >
			                                        <tr class="TBLINI">
				                                        <td width="80%">&nbsp;Asignados&nbsp;
								                            <img id="imgLupa2" style="display: none; cursor:pointer" onclick="buscarSiguiente('tblOpciones2',2,'divCatalogo2','imgLupa2', event)"
									                            height="11" src="../../../../../Images/imgLupaMas.gif" width="20" tipolupa="2" />
							                                <img id="imgLupa4" style="display: none; cursor:pointer" onclick="buscarDescripcion('tblOpciones2',2,'divCatalogo2','imgLupa2', event)"
									                            height="11" src="../../../../../Images/imgLupa.gif" width="20" tipolupa="1" /> 
						                                </td>
						                                <td width="20%" style="text-align:right;">Notificar&nbsp;</td>
			                                        </tr>
		                                        </table>
		                                        <div id="divCatalogo2" style="overflow:auto; width:451px; height:80px;" runat="server" target="true" onmouseover="setTarget(this);" caso="1" onscroll="scrollTablaProfAsig()">
		                                            <div id="divR" style='background-image:url(../../../../../Images/imgFT20.gif); width:435px;' runat="server">
		                                            </div>
                                                </div>
                                                <table id="tblResultado2" style="height:17px; width:435px">
			                                        <tr class="TBLFIN"><td></td></tr>
		                                        </table>
	                                        </td>
	                                    </tr>
                                        <tr>
                                            <td style="padding-top:4px;">
                                                <img class="ICO" src="../../../../../Images/imgUsuPVM.gif" />&nbsp;Del <%=sNodo%> actual&nbsp;&nbsp;&nbsp;
                                                <img class="ICO" src="../../../../../Images/imgUsuNVM.gif" />&nbsp;De otro <%=sNodo %>&nbsp;&nbsp;&nbsp;
                                                <img class="ICO" src="../../../../../Images/imgUsuEVM.gif" />&nbsp;Externo&nbsp;&nbsp;&nbsp;
                                                <img id="imgForaneo" class="ICO" src="../../../../../Images/imgUsuFVM.gif" runat="server" />
                                                <label id="lblForaneo" runat="server">Foráneo</label>
                                            </td>
                                        </tr>
                                    </table>
                            </fieldset>
                        </td>
                    </tr>
                </table>
                </eo:PageView>
                <eo:PageView ID="PageView2" CssClass="PageView" runat="server">	
                <!-- Pestaña 2 Documentación -->
                    <table style="width:950px; margin-left:15px;">
                        <tr>
                            <td>
                                <table id="Table2" style="width: 920px; height: 17px" >
	                                <tr class="TBLINI">
		                                <td width="312px">&nbsp;Descripción</td>
		                                <td width="213px">Archivo</td>
		                                <td width="225px">Link</td>
		                                <td width="170px">Autor</td>
	                                </tr>
                                </table>
                                <div id="divCatalogoDoc" style="overflow: auto; width: 936px; height:440px" runat="server">
                                    <div id="divDoc" style='background-image:url(../../../../../Images/imgFT20.gif); width:920px;' runat="server">
                                    </div>
                                </div>
                                <table id="Table1" style="width: 920px; height:17px" >
                                    <tr class="TBLFIN"><td>&nbsp;</td></tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <center>
		                        <button id="Button1" type="button" onclick="nuevoDoc1();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
			                         onmouseover="se(this, 25);mostrarCursor(this);" style="display:inline; margin-top:11px;">
			                        <img src="../../../../../images/botones/imgAnadir.gif" /><span>&nbsp;Añadir</span>
		                        </button>
		                        <button id="Button2" type="button" onclick="eliminarDoc1();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
			                         onmouseover="se(this, 25);mostrarCursor(this);" style="display:inline; margin-left:30px;">
			                        <img src="../../../../../images/botones/imgEliminar.gif" /><span>&nbsp;Eliminar</span>
		                        </button>
		                        </center>
                            </td>
                        </tr>
                    </table>
                    <iframe id="iFrmSubida" frameborder="no" name="iFrmSubida" width="10px" height="10px" style="visibility:hidden" ></iframe>
                </eo:PageView>
           </eo:MultiPage>
        </td>
    </tr>
</table>
<table style="width:400px; margin-top:15px; margin-left:330px;">
	<tr> 
        <td>
		    <button id="btnGrabar" type="button" onclick="grabarAux();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
			     onmouseover="se(this, 25);mostrarCursor(this);" style="display:inline;">
			    <img src="../../../../../images/botones/imgGrabar.gif" /><span>&nbsp;Grabar</span>
		    </button>
		    <button id="btnGrabarSalir" type="button" onclick="grabarSalir();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
			     onmouseover="se(this, 25);mostrarCursor(this);" style="display:inline;margin-left:30px;">
			    <img src="../../../../../images/botones/imgGrabarSalir.gif" /><span title='Graba y cierra la pantalla'>Grabar...</span>
		    </button>
		    <button id="btnSalir" type="button" onclick="salir();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
			     onmouseover="se(this, 25);mostrarCursor(this);" style="display:inline; margin-left:30px;">
			    <img src="../../../../../images/botones/imgSalir.gif" /><span title="Salir">&nbsp;&nbsp;Salir</span>
		    </button>
		</td>
	  </tr>
</table>

<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<input type="hidden" name="hdnIdT" id="hdnIdT" value="<%=nIdT %>" />
<input type="hidden" runat="server" name="hdnNodo" id="hdnNodo" value="" />
<asp:TextBox ID="hdnAcceso" name="hdnAcceso" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
<asp:TextBox ID="txtIdResponsable" runat="server" Text="" style="width:2px;visibility:hidden;" />
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    <uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
    <uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
    <div class="clsDragWindow" id="DW" noWrap></div>
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
