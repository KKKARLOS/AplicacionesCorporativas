<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Import Namespace="GEMO.BLL" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 	"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Líneas ligadas al responsable</title>
	<LINK href="../../../../App_Themes/Corporativo/Corporativo.css" type="text/css" rel="stylesheet">
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../Javascript/boxover.js" type="text/Javascript"></script> 	
 	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
	<script type="text/javascript">
	<!--
	-->
    </script>
</head>
<body style="OVERFLOW: hidden" leftMargin="10" onload="init()">
    <ucproc:Procesando ID="Procesando" runat="server" />
     <form id="form1" runat="server">
	        <script type="text/javascript">
	        <!--
                var intSession = <%=Session.Timeout%>; 
	            var strServer = "<%=Session["strServer"]%>";
	        -->
	        </script>

			<table align="center" border="0" style="width:700px;" cellpadding="5" cellspacing="0">
			<colgroup>
			    <col style="width:100px;" />
				<col style="width:500px;" />
				<col style="width:100px;" />

			</colgroup>
				<tr>
				    <td></td>
					<td style="vertical-align:bottom;">
						<table style="WIDTH: 500px; vertical-align:bottom;">
							<colgroup><col style='width:120px;' /><col style='width:120px;' /><col style='width:160px;' /></colgroup>
							<tr>
								<td><label id="lblA1" >&nbsp;Apellido1</label></td>
								<td><label id="lblA2" >&nbsp;&nbsp;Apellido2</label></td>
								<td><label id="lblN" >&nbsp;&nbsp;Nombre</label></td>
							</tr>
							<tr>
								<td>
									<asp:TextBox ID="txtApellido1" runat="server" style="width:118px;"  onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="50" />
								</td>
								<td>
									<asp:TextBox ID="txtApellido2" runat="server" style="width:118px;" onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="50" />
								</td>
								<td>
									<asp:TextBox ID="txtNombre" runat="server" style="width:118px;" onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="50" />
								</td>
							</tr>
						</table>
					</td>
					<td>	            
					    <span id="ambBajas" class="texto" runat="server">
	                    <input type=checkbox id="chkBajas" class="check" runat="server" />&nbsp;Mostrar bajas
	                    </span>
	                </td>
				</tr>        
				<tr valign=top>
				    <td></td>
					<td>
						<table id="tblTituloResponsables" style="WIDTH: 500px; HEIGHT: 17px;">
							<colgroup>
							<COL style="width:500px; padding-left:3px;" />
							</colgroup>
							<TR class="TBLINI">
								<td style="padding-left:5px;">Responsable&nbsp;<IMG id="imgLupaNodo2" style="DISPLAY: none; CURSOR: hand" onclick="buscarSiguiente('tblResponsables',0,'divCatalogoResponsables','imgLupaNodo2')"
										height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="CURSOR: hand; DISPLAY: none;" onclick="buscarDescripcion('tblResponsables',0,'divCatalogoResponsables','imgLupaNodo2',event)"
										height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"></TD>
							</TR>
						</TABLE>
						<DIV id="divCatalogoResponsables" style="OVERFLOW-X: hidden; OVERFLOW-Y: auto; table-layout:fixed; WIDTH: 516px; height:120px;" runat="server">
							<div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:500px">
							<%=strTablaHTML%>
							</div>
						</DIV>
						<table id="tblResultado" style="WIDTH: 500px; HEIGHT: 17px;">
							<TR class="TBLFIN">
								<TD>&nbsp;</TD>
							</TR>
						</TABLE>
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
				    <td></td>
					<td>
						<table id="tblTitulo" style="WIDTH: 500px; HEIGHT: 17px">
							<colgroup>
								<col style="width:20px;" />
								<col style="width:90px;" />
								<col style="width:60px;" />
								<col style="width:330px;" />
							</colgroup>
							<TR class="TBLINI">
								<TD></TD>
								<td><IMG style="CURSOR: hand" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#img1" border="0">
										<MAP name="img1">
											<AREA onclick="ot('tblDatos', 1, 0, '', 'scrollTablaLineas()')" shape="RECT" coords="0,0,6,5">
											<AREA onclick="ot('tblDatos', 1, 1, '', 'scrollTablaLineas()')" shape="RECT" coords="0,6,6,11">
										</MAP>&nbsp;Línea&nbsp;<IMG id="imgLupa1" style="DISPLAY: none; CURSOR: hand" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa1')"
										height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="CURSOR: hand; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa1',event)"
										height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">
								</TD>
								<TD>Ext</TD>
								<TD><IMG style="CURSOR: hand" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#img2" border="0">
										<MAP name="img2">
											<AREA onclick="ot('tblDatos', 3, 0, '', 'scrollTablaLineas()')" shape="RECT" coords="0,0,6,5">
											<AREA onclick="ot('tblDatos', 3, 1, '', 'scrollTablaLineas()')" shape="RECT" coords="0,6,6,11">
										</MAP>&nbsp;Beneficiario / Departamento&nbsp;<IMG id="imgLupa2" style="DISPLAY: none; CURSOR: hand" onclick="buscarSiguiente('tblDatos',3,'divCatalogo','imgLupa2')"
										height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="CURSOR: hand; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',3,'divCatalogo','imgLupa2',event)"
										height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
								</TD>
							</TR>
						</TABLE>
						<DIV id="divCatalogo" style="OVERFLOW-X: hidden; OVERFLOW-Y: auto; WIDTH: 516px; height:200px" onscroll="scrollTablaLineas()">
							<div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:500px">
							<table id="tblDatos"></TABLE>
							</div>
						</DIV>
						<table style="WIDTH: 500px; HEIGHT: 17px">
							<TR class="TBLFIN">
								<TD></TD>
							</TR>
						</TABLE>
					</td>
					<td>
					</td>
				</tr>
				<tr>   
				    <td></td> 
					<td style="padding-top:5px;">
						<img alt=""  src="../../../../Images/imgPreactiva.gif" class="ICO" />Preactiva&nbsp;&nbsp;&nbsp;
						<img alt=""  src="../../../../Images/imgActiva.gif" class="ICO" />Activa&nbsp;&nbsp;&nbsp;
						<img alt=""  src="../../../../Images/imgBloqueada.gif" class="ICO" />Bloqueada&nbsp;&nbsp;&nbsp;
					   <img  alt=""  src="../../../../Images/imgPreinactiva.gif" class="ICO" />Preinactiva&nbsp;&nbsp;&nbsp;
					</td> 	  
					<td></td>  
				</tr>
			    <td></td> 
				<td style="padding-top:8px;">				
					<table width="300px" align=center style="margin-top:5px;">
						<tr>
							<td align="center">
								<button id="btnCancelar" type="button" onclick="cerrarVentana()" class="btnH25W95" runat="server" hidefocus="hidefocus" 
									onmouseover="se(this, 25);mostrarCursor(this);">
									<img src="../../../../images/imgCancelar.gif" /><span title="Cancelar">&nbsp;Cancelar</span>
								</button>  
							</td>
						</tr>
					</table>			
				</td> 	  
				<td></td> 				
			</table>
		<DIV class="clsDragWindow" id="DW" noWrap></DIV>

		<input type="hidden" id="hdnIdResponsable" value="" runat="server"/>
		<input type="hidden" id="hdnIdNodo" value="" runat="server"/>

		<input type="hidden" id="hdnIdSubnodo" value="" runat="server"/>

		<input type="hidden" id="hdnResponsableOrigen" value="" runat="server"/>
        <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
		
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