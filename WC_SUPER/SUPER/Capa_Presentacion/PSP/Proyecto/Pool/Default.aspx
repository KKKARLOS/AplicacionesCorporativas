<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title> ::: SUPER ::: - Pool de responsables técnicos de proyectos técnicos</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <script src="../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
   	<script src="../../../../Javascript/draganddrop.js" type="text/Javascript"></script>
	<script src="Functions/funciones.js" type="text/Javascript"></script>
  	<script src="../../../../Javascript/boxover.js" type="text/Javascript"></script>
   	<script src="../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onload="init()" onunload="unload()">
<form id="Form1" name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
    <script type="text/javascript">
        var strServer = "<% =Session["strServer"].ToString() %>";
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
        var bCambios = false;
        var bLectura = <%=sLectura%>;
        var bSalir = false;
        mostrarProcesando();
    </script>    
    <br />
<table style="margin-left:10px;width:880px;text-align:left">
    <colgroup><col style="width:415px;" /><col style="width:50px;" /><col style="width:415px;" /></colgroup>    
	<tr>
		<td colspan="3">
            <table style="width: 390px;">
            <colgroup><col style='width:130px;' /><col style='width:130px;' /><col style='width:130px;' /></colgroup>
                <tr>
                <td>&nbsp;Apellido1</td>
                <td>&nbsp;Apellido2</td>
                <td>&nbsp;Nombre</td>
                </tr>
                <tr>
                <td><asp:TextBox ID="txtApellido1" runat="server" style="width:115px"  onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="50" /></td>
                <td><asp:TextBox ID="txtApellido2" runat="server" style="width:115px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="50" /></td>
                <td><asp:TextBox ID="txtNombre" runat="server" style="width:115px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="50" /></td>
                </tr>
            </table>
		</td>
	</tr>
	<tr>
		<td>
			<table id="tblTitulo" style="height:17px; width:380px">
				<tr class="TBLINI">
					<td>&nbsp;Profesionales&nbsp;
						<img id="imgLupa1" style="DISPLAY: none; cursor: pointer" onclick="buscarSiguiente('tblOpciones',0,'divCatalogo','imgLupa1')"
							height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
					    <img id="imgLupa3" style="DISPLAY: none; cursor: pointer" onclick="buscarDescripcion('tblOpciones',0,'divCatalogo','imgLupa1',event)"
							height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
						</td>
				</tr>
			</table>
			<div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 396px; height: 380px;" align="left" onscroll="scrollTablaProf()">
				 <div style='background-image:url(../../../../Images/imgFT20.gif); width:380px'>
				 <table id="tblOpciones" style="width: 380px"></table>
				 </div>
			</div>
			<table id="tblResultado" style="height:17px; width:380px">
				<tr class="TBLFIN"><td></td></tr>
			</table>
		</td>
		<td align="center">
	        <asp:Image id="imgPapelera" style="cursor: pointer" runat="server" ImageUrl="~/Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="3"></asp:Image>
	    </td>
		<td>
			<table id="tblTitulo2" style="height:17px; width:390px">
				<tr class="TBLINI"><td>&nbsp;Responsables</td></tr>
			</table>
			<div id="divCatalogo2" style="overflow: auto; overflow-x: hidden; width: 406px; height: 380px;" target="true" onmouseover="setTarget(this);" caso="1" onscroll="scrollTablaProfAsig()">
			    <div style='background-image:url(../../../../Images/imgFT20.gif); width:390px'>
				<%=strTablaHTMLIntegrantes%>
				</div>
            </div>
            <table id="tblResultado2" style="height:17px; width:390px">
				<tr class="TBLFIN"><td></td></tr>
			</table>
		</td>
    </tr>
</table>
<table class="texto" style="margin-left:5px; text-align:left; margin-left:10px; width:90%;">
    <tr>
        <td>
            <img border="0" src="../../../../Images/imgUsuPVM.gif" />&nbsp;Del <%=sNodo%> actual&nbsp;&nbsp;&nbsp;
            <img border="0" src="../../../../Images/imgUsuNVM.gif" />&nbsp;De otro <%=sNodo %>&nbsp;&nbsp;&nbsp;
            <img border="0" src="../../../../Images/imgUsuEVM.gif" />&nbsp;Externo&nbsp;&nbsp;&nbsp;
            <img id="imgForaneo" src="../../../../Images/imgUsuFVM.gif" runat="server" />
            <label id="lblForaneo" runat="server">Foráneo</label>
        </td>
    </tr>
</table>
<center>
<table id="tblBotones" style="margin-top:15px; width:260px;">
    <tr>
        <td>
		    <button id="btnGrabarSalir" type="button" onclick="grabarSalir();" class="btnH25W95" runat="server" disabled hidefocus="hidefocus" 
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
</center>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<asp:TextBox ID="txtPE" name="txtPE" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
<input type="hidden" runat="server" name="hdnNodo" id="hdnNodo" value="" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
<uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
<div class="clsDragWindow" id="DW" noWrap></div>
</form>
<script type="text/javascript">
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

