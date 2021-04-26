<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<html xmlns:TSNS
		xmlns:MPNS>
<head id="Head1" runat="server">
    <title> ::: GASVI ::: - Profesionales excluidos del envío de correo</title>
    <link rel="stylesheet" href="../../../../App_Themes/Corporativo/Corporativo.css" type="text/css">
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
   	<script language="JavaScript" src="../../../../Javascript/draganddrop.js" type="text/Javascript"></script>
	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
  	<script language="JavaScript" src="../../../../Javascript/boxover.js" type="text/Javascript"></script>
   	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onLoad="init()" onunload="unload()">
<form id="Form1" name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
    <script type="text/javascript">
    <!--
        var strServer = "<% =Session["strServer"].ToString() %>";
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
        var bCambios = false;
        var bLectura = <%=sLectura%>;
        var bSalir = false;
        mostrarProcesando();
    -->
    </script>    
    <br />
<TABLE class="texto" id="Table1" cellSpacing="0" cellPadding="2" width="100%" border="0" style=" margin-left:10px;">
	<TR>
		<TD colspan="3" style="padding-left:2px">
            <table border="0" class="texto"style="WIDTH: 350px;" cellpadding="0" cellspacing="0">
                <tr>
                <td>&nbsp;Apellido1</td>
                <td>&nbsp;Apellido2</td>
                <td>&nbsp;Nombre</td>
                </tr>
                <tr>
                <td><asp:TextBox ID="txtApellido1" runat="server" style="width:110px"  onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="50" /></td>
                <td><asp:TextBox ID="txtApellido2" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="50" /></td>
                <td><asp:TextBox ID="txtNombre" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="50" /></td>
                </tr>
            </table>
		</TD>
	</TR>
    <tr height="20px">
        <td width="47%" valign=bottom align=right style="padding-right:36px;">
            <img src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblOpciones')" />&nbsp;<img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblOpciones')" />
        </td>	
        <TD width="4%"></td>	
        <TD width="49%" valign=bottom align=right style="padding-right:44px;">
            <img src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblOpciones2')" />&nbsp;<img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblOpciones2')" />
        </TD>	
	</tr>					
	<TR>
		<TD width="47%">
			<TABLE id="tblTitulo" height="17" cellSpacing="0" cellPadding="0" width="380px" border="0">
				<TR class="TBLINI">
					<TD>&nbsp;Profesionales&nbsp;
						<IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblOpciones',0,'divCatalogo','imgLupa1')"
							height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
					    <IMG id="imgLupa3" style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblOpciones',0,'divCatalogo','imgLupa1',event)"
							height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
					</TD>
				</TR>
			</TABLE>
			<DIV id="divCatalogo" style="OVERFLOW-X: hidden; OVERFLOW: auto; WIDTH: 396px; HEIGHT: 380px;" align="left" onscroll="scrollTablaProf()">
				 <div style='background-image:url(../../../../Images/imgFT20.gif); width:380px'>
				 <TABLE class="texto" id="tblOpciones" style="WIDTH: 380px" cellSpacing="0" align="left" border="0"></TABLE>
				 </DIV>
			</DIV>
			<TABLE id="tblResultado" height="17" cellSpacing="0" cellPadding="0" width="380px" align="left" border="0">
				<TR class="TBLFIN"><TD></TD></TR>
			</TABLE>
		</TD>
		<TD width="4%">
	        <asp:Image id="imgPapelera" style="CURSOR: pointer" runat="server" ImageUrl="~/Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this.id, 3);"></asp:Image>
		<TD width="49%">
			<TABLE id="tblTitulo2" height="17" cellSpacing="0" cellPadding="0" width="390px" border="0">
				<TR class="TBLINI"><TD>&nbsp;Excluidos</TD></TR>
			</TABLE>
			<DIV id="divCatalogo2" style="OVERFLOW-X: hidden; OVERFLOW: auto; WIDTH: 406px; HEIGHT: 380px;" align="left" target="true" onmouseover="setTarget(this.id, 1);" onscroll="scrollTablaProfAsig()">
			    <div style='background-image:url(../../../../Images/imgFT20.gif); width:390px'>
				<%=strTablaHTMLIntegrantes%>
				</DIV>
            </DIV>
            <TABLE id="tblResultado2" height="17" cellSpacing="0" cellPadding="0" width="390px" align="left" border="0">
				<TR class="TBLFIN"><TD></TD></TR>
			</TABLE>
		</TD>
    </TR>
</TABLE>
<table width="330px" border="0" class="texto" align="center" style="margin-top:5px;">
	<tr> 
		<td width="100px"> 
          <table id="tblGrabarSalir" align="center" height="20" border="0" cellpadding="0" cellspacing="0" onMouseOver="mostrarCursor(this)">
			  <tr onClick="grabarSalir()"> 
				<td width="7"><img src="../../../../images/imgBtnIzda.gif" width="7"></td>
				<td width="25" background="../../../../images/bckBoton.gif" valign="middle" align="center"><img src="../../../../images/botones/imgGrabarSalir.gif" border="0" align="absmiddle"></td>
				<td width="50" background="../../../../images/bckBoton.gif" class="txtBot"><a href="#" hidefocus title="Grabar y salir">&nbsp;&nbsp;Grabar...</a></td>
				<td width="7"><img src="../../../../images/imgBtnDer.gif" width="7"></td>
			  </tr>
		  </table>
		  </td>
		<td width="100px">&nbsp;</td>
		<td width="130px">
		  <table id="tblSalir" height="20" border="0" cellpadding="0" cellspacing="0" onMouseOver="mostrarCursor(this)">
			<tr onClick="salir()"> 
			<td width="7"><img src="../../../../images/imgBtnIzda.gif" width="7"></td>
			<td width="25" background="../../../../images/bckBoton.gif" valign="middle" align="center"><img src="../../../../images/botones/imgSalir.gif" border="0" align="absmiddle"></td>
			<td width="50" background="../../../../images/bckBoton.gif" class="txtBot"><a href="#" hidefocus>&nbsp;&nbsp;Salir</a></td>
			<td width="7"><img src="../../../../images/imgBtnDer.gif" width="7"></td>
		  </tr>
		  </table>
		</td>
	  </tr>
</table>
<table width="100%" border="0" class="texto" align=left style="margin-left:5px;">
    <tr>
        <td>
            <img border="0" src="../../../../Images/imgUsuIVM.gif" />&nbsp;Interno&nbsp;&nbsp;&nbsp;
            <img border="0" src="../../../../Images/imgUsuEVM.gif" />&nbsp;Externo&nbsp;&nbsp;&nbsp;
        </td>
    </tr>
</table>
    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
    <uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
    <DIV class="clsDragWindow" id="DW" noWrap></DIV>
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
</SCRIPT>
</body>
</html>

