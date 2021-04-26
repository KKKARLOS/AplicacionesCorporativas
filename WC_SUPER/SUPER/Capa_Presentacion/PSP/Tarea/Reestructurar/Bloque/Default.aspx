<%@ Page Language="C#" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Tarea_Default" EnableEventValidation="false" ValidateRequest="false" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Reestructuración de tareas</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/> 
    <script src="../../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>         	
  	<script language="JavaScript" src="../../../../../Javascript/draganddrop.js" type="text/Javascript"></script>
  	<script language="JavaScript" src="../../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onload="init()" onunload="unload()">
<form id="frmPrincipal" name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
    <script type="text/javascript">
        var strServer = "<% =Session["strServer"].ToString() %>";
        var sNumEmpleado = "<% =Session["NUM_EMPLEADO_ENTRADA"].ToString() %>";
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos. 
        var bCambios = false;
        var bLectura = false;
        var bSalir = false;
    </script>    
<center>
<table style="width:960px;text-align:left;margin-top:35px;">
<colgroup>
    <col style="width:456px"/>
    <col style="width:48px"/>
    <col style="width:456px"/>
</colgroup>
    <tr>
        <td style="vertical-align:top;">
			<fieldset style="width:440px;">
				<legend>Estructura actual</legend>
                <table align="center" border="0" class="texto" width="100%" cellpadding="5" cellspacing="0">
                    <tr>
                        <td>
                            <label id="lblProy" class="enlace" style="width:85px;height:17px" onClick="obtenerProyectos()">P. económico</label>
                            <asp:TextBox ID="txtNumPE" runat="server" SkinID="Numero" style="width:50px" readonly="true" />
                            <asp:TextBox ID="txtPE" runat="server" style="width:255px" readonly="true" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label id="lblPT" class="enlace" style="width:85px;height:17px" onClick="obtenerPTs()">P. técnico</label>
                            <asp:TextBox ID="txtPT" runat="server" style="width:308px" readonly="true" />
                            <asp:Image ID="Image6" runat="server" ImageUrl="~/Images/imgBorrar.gif" onclick="borrarPT();" style="cursor:pointer; vertical-align:middle;" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label id="lblFase" class="enlace" style="width:85px;height:17px" onclick="obtenerFases()">Fase</label>
                            <asp:TextBox ID="txtFase" runat="server" style="width:308px" readonly="true" />
                            <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/imgBorrar.gif" onclick="borrarFase();" style="cursor:pointer; vertical-align:middle;" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label id="lblActividad" class="enlace" style="width:85px;height:17px" onclick="obtenerActividades()">Actividad</label>
                            <asp:TextBox ID="txtActividad" runat="server" style="width:308px" readonly="true" />
                            <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/imgBorrar.gif" onclick="borrarActividad();" style="cursor:pointer; vertical-align:middle;" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        <br />
        </td>
        <td><br /></td>
        <td style="vertical-align:top;">
			<fieldset style="width:440px;">
				<legend>Nueva estructura</legend>
                <table align="center" border="0" class="texto" width="100%" cellpadding="5" cellspacing="0">
                    <tr>
                        <td>
                            <label id="lblProy2" style="width:85px;height:17px" >P. económico</label>
                            <asp:TextBox ID="txtNumPE2" runat="server" SkinID="Numero" style="width:50px" readonly="true"/>
                            <asp:TextBox ID="txtPE2" runat="server" style="width:255px" readonly="true" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label id="lblPT2" class="enlace" style="width:85px;height:17px" onclick="obtenerPTs2()">P. técnico</label>
                            <asp:TextBox ID="txtPT2" runat="server" style="width:308px" readonly="true" />
                            <asp:Image ID="Image7" runat="server" ImageUrl="~/Images/imgBorrar.gif" onclick="borrarPT2();" style="cursor:pointer; vertical-align:middle;" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label id="lblFase2" class="enlace" style="width:85px;height:17px" onclick="obtenerFases2()">Fase</label>
                            <asp:TextBox ID="txtFase2" runat="server" style="width:308px" readonly="true" />
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/imgBorrar.gif" onclick="borrarFase2();" style="cursor:pointer; vertical-align:middle;" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label id="lblActividad2" class="enlace" style="width:85px;height:17px" onclick="obtenerActividades2()">Actividad</label>
                            <asp:TextBox ID="txtActividad2" runat="server" style="width:308px" readonly="true" />
                            <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/imgBorrar.gif" onclick="borrarActividad2();" style="cursor:pointer; vertical-align:middle;" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        <br />
        </td>
    </tr>
    <tr>
        <td>
	        <table id="tblTitulo" style="height:17px; width:440px">
		        <tr class="TBLINI">
			        <td>&nbsp;Tareas a reestructurar
                        <IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblOpciones',1,'divCatalogo','imgLupa1')"
                            height="11" src="../../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
                        <IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblOpciones',1,'divCatalogo','imgLupa1',event)"
                            height="11" src="../../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
					</td>
		        </tr>
	        </table>
	        <div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 456px; height: 400px;" align="left">
		         <table class="texto" id="tblOpciones" style="width: 440px" align="left">
		         <colgroup><col style='width:440px' /></colgroup>
		         </table>
	        </div>
	        <table id="tblResultado" style="height:17px; width:440px">
		        <tr class="TBLFIN"><td></td></tr>
	        </table>
        </td>
        <td align="center">
	        <asp:Image id="imgPapelera" style="CURSOR: pointer; margin-left:5px;" runat="server" ImageUrl="~/Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="4"></asp:Image>
	    </td>
        <td>
	        <table id="tblTitulo2" style="height:17px; width:440px">
		        <tr class="TBLINI">
		            <td>&nbsp;Tareas
                        <IMG id="imgLupa3" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblOpciones2',0,'divCatalogo2','imgLupa3')"
                            height="11" src="../../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
                        <IMG id="imgLupa4" style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblOpciones2',0,'divCatalogo2','imgLupa3',event)"
                            height="11" src="../../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
		            
		            </td>
		        </tr>
	        </table>
            <div id="divCatalogo2" style="overflow: auto; overflow-x: hidden; width: 456px; height: 400px;" align="left" target="true" onmouseover="setTarget(this);" caso="2">
		        <div style="background-image:url(../../../../../Images/imgFT16.gif); width:440px">
		            <table class='texto MM' id="tblOpciones2" style="width: 440px">
		                <colgroup><col style='width:440px' /></colgroup>
		            </table>
                </div>
            </div>
            <table id="tblResultado2" style="height:17px; width:440px">
		        <tr class="TBLFIN"><td></td></tr>
	        </table>
        </td>
    </tr>
</table>
</center>
<center>
    <table style="margin-top:10px;" width="80%">
        <tr>
	        <td align="center">
			    <button id="btnBuscar" type="button" onclick="buscar();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../../../images/botones/imgBuscar.gif" /><span title="Buscar tareas">&nbsp;Buscar</span>
			    </button>	
	        </td>
			<td align="center">
			    <button id="btnNuevo" type="button" onclick="limpiar();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../../../images/botones/imgNuevo.gif" /><span title="Nueva tarea">Limpiar</span>
			    </button>	
	        </td>
	        <td align="center">
			    <button id="btnGrabar" type="button" onclick="grabarAux();" class="btnH25W95" runat="server" disabled hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../../../images/botones/imgGrabar.gif" /><span title="Grabar">&nbsp;&nbsp;Grabar</span>
			    </button>	
	        </td>
	        <td align="center">
			    <button id="btnGrabarSalir" type="button" onclick="grabarSalir();" class="btnH25W95" runat="server" disabled hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../../../images/botones/imgGrabarSalir.gif" /><span title="Grabar y salir">Grabar...</span>
			    </button>	
	        </td>		
	        <td align="center">
			    <button id="btnSalir" type="button" onclick="salir();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../../../images/botones/imgSalir.gif" /><span title="Salir">&nbsp;&nbsp;Salir</span>
			    </button>	 
	        </td>
        </tr>
    </table>
</center>
                 
    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
    <input type="hidden" name="__mpContenido_State__" id="__mpContenido_State__" value="" />
    <input type="hidden" name="__tsPestanas_State__" id="__tsPestanas_State__" value="" />
    <input type="hidden" name="hdnCRActual" id="hdnCRActual" value="" runat="server"/>
    <input type="hidden" name="hdnDesCRActual" id="hdnDesCRActual" value="" runat="server"/>
    <asp:TextBox ID="hdnOrden" name="hdnOrden" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
    <asp:TextBox ID="hdnIDPT" name="hdnIDPT" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
    <asp:TextBox ID="hdnIDAct" name="hdnIDAct" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
    <asp:TextBox ID="hdnIDFase" name="hdnIDFase" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
    <asp:TextBox ID="hdnIDPT2" name="hdnIDPT2" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
    <asp:TextBox ID="hdnIDAct2" name="hdnIDAct2" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
    <asp:TextBox ID="hdnIDFase2" name="hdnIDFase2" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
    <asp:TextBox ID="hdnEstado" name="hdnEstado" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
    <asp:TextBox ID="hdnAcceso" name="hdnAcceso" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
    <input type="hidden" name="nIdTarea" id="nIdTarea" value="" />
    <input type="hidden" name="Permiso" id="Permiso" value="" />
    <input type="hidden" runat="server" name="hdnT305IdProy" id="hdnT305IdProy" value="" />
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    <uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
<div class="clsDragWindow" id="DW" noWrap></div>
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
