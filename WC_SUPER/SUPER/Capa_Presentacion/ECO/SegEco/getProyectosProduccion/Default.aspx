<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title> ::: SUPER ::: - Producción de proyectos asociados al contrato</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<link rel="stylesheet" href="css/estilos.css" type="text/css"/>
    <script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/dhtmltooltip.js" type="text/Javascript"></script>
	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
   	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onload="init()" onunload="unload()">
<form name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
<script type="text/javascript">
<!--
    var strServer = "<% =Session["strServer"].ToString() %>";
    var intSession = <%=Session.Timeout%>;
-->
</script>    
<table id="Table1" style="width:610px; margin-left:15px; margin-top:10px;" border="0">
    <tr>
        <td id="tdMoneda" runat="server" style="text-align:right; padding-right:30px;"></td>
    </tr>
	<TR>
		<TD>
			<table id="tblTitulo" style="width:580px">
			    <colgroup>
			        <col style="width:125px" />
			        <col style="width:255px" />
			        <col style="width:200px;" />
			    </colgroup>
				<tr class="TBLINI" style="height:17px;">
					<td style="text-align:right;">
						<IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',3,'divCatalogo','imgLupa1')"
							height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
					    <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',3,'divCatalogo','imgLupa1',event)"
							height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
						<IMG style="CURSOR: pointer" height="11px" src="../../../../Images/imgFlechas.gif" width="6" useMap="#img1" border="0">
					    <MAP name="img1">
					        <AREA onclick="ot('tblDatos', 3, 0, 'num', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
					        <AREA onclick="ot('tblDatos', 3, 1, 'num', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
				        </MAP>&nbsp;Nº&nbsp;&nbsp;
					</td>
					<td><IMG style="CURSOR: pointer" height="11px" src="../../../../Images/imgFlechas.gif" width="6" useMap="#img2" border="0">
						    <MAP name="img2">
						        <AREA onclick="ot('tblDatos', 4, 0, '', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
						        <AREA onclick="ot('tblDatos', 4, 1, '', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
					        </MAP>&nbsp;Proyecto&nbsp;<IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',4,'divCatalogo','imgLupa2')"
							height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',4,'divCatalogo','imgLupa2',event)"
							height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">
					</td>
					<td style="text-align:right; padding-right:3px;"><IMG style="CURSOR: pointer" height="11px" src="../../../../Images/imgFlechas.gif" width="6" useMap="#img3" border="0">
						    <MAP name="img3">
						        <AREA onclick="ot('tblDatos', 5, 0, 'num', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
						        <AREA onclick="ot('tblDatos', 5, 1, 'num', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
					        </MAP>&nbsp;Total producción
					</td>
				</tr>
			</table>
			<div id="divCatalogo" style="overflow: auto; width: 596px; height: 260px;" onscroll="scrollTablaProy();">
                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:580px;">
                <%=strTablaHTML%>
                </div>
            </div>
            <table id="tblResultado" style="width:580px">
				<tr class="TBLFIN"  style="height:17px;">
					<td>&nbsp;</td>
				</tr>
			</table>
		</TD>
    </TR>
</table>
<table style="width:540px; margin-left:15px; margin-top:5px;" border="0">
    <colgroup>
        <col style="width:100px" />
        <col style="width:90px" />
        <col style="width:230px" />
        <col style="width:120px" />
    </colgroup>
	<tr> 
	    <td><img class="ICO" src="../../../../Images/imgProducto.gif" />Producto</td>
        <td><img class="ICO" src="../../../../Images/imgServicio.gif" />Servicio</td>
        <td></td>
		<td rowspan="3" style="vertical-align:top;"><br />
            <button id="btnCancelar" type="button" onclick="salir();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../../images/botones/imgSalir.gif" /><span>Salir</span>
            </button>				   
		</td>
	  </tr>
	<tr>
	    <td><img class="ICO" src="../../../../Images/imgIconoContratante.gif" />Contratante</td>
        <td></td>
        <td></td>
      </tr>
	<tr>
	    <td style="vertical-align:top;"><img class="ICO" src="../../../../Images/imgIconoProyAbierto.gif" title='Proyecto abierto' />Abierto</td>
        <td><img class="ICO" src="../../../../Images/imgIconoProyCerrado.gif" title='Proyecto cerrado' />Cerrado</td>
        <td>
            <img class="ICO" src="../../../../Images/imgIconoProyHistorico.gif" title='Proyecto histórico' />Histórico&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <img class="ICO" src="../../../../Images/imgIconoProyPresup.gif" title='Proyecto presupuestado' />Presupuestado
        </td>
      </tr>
</table>
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

