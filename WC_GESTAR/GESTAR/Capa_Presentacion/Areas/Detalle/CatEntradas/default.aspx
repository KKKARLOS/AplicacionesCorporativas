<%@ Page language="c#" Inherits="GESTAR.Capa_Presentacion.ASPX.CatEntradas" EnableEventValidation="false" CodeFile="default.aspx.cs" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="cabecera" runat="server">
	    <title>Catálogo de entradas</title>
        <meta http-equiv='X-UA-Compatible' content='IE=8' />
	    <link href="../../../../App_Themes/Corporativo/Corporativo.css" type="text/css" rel="stylesheet"/> 
        <script src="../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
        <script src="../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	    <script language="JavaScript" src="../../../../JavaScript/funciones.js" type="text/Javascript"></script>
	    <script language="JavaScript" src="../../../../JavaScript/funcionesTablas.js" type="text/Javascript"></script>
	    <script language="JavaScript" src="../../../../JavaScript/documentos.js" type="text/Javascript"></script>
	    <script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
	    <script language="JavaScript" src="../../../../JavaScript/modal.js" type="text/Javascript"></script>
    </head>
	<body class="FondoBody" onload="init()">
	    <ucproc:Procesando ID="Procesando1" runat="server" />
		<form id="frmDatos" method="post" runat="server">
		    <script type="text/javascript">
	            var sNumEmpleado = "<% =Session["IDFICEPI"].ToString() %>";
		        var strServer = "<%=Session["strServer"].ToString()%>";
	            var intSession = <%=Session.Timeout%>;
		    </script>		
 			<br />
 			<center>
	            <table style="width:90%;text-align:left;margin-top:20px">
		            <tr>
			            <td>
				            <table id="tblTitulo" style="width: 830px;height:17px;text-align:left">
					            <tr class="TBLINI">
						            <td width="45%">&nbsp;&nbsp;<img style="cursor: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#imgDescripcion"
								            border="0"> <map name="imgDescripcion">
								            <area onclick="ordenarTabla(1,0)" shape="RECT" coords="0,0,6,5">
								            <area onclick="ordenarTabla(1,1)" shape="RECT" coords="0,6,6,11">
							            </map><img style="cursor: pointer" onclick="buscarDescripcion('tblCatalogoEntrada',1,'divCatalogo','imgLupa1',event)"
								            height="11" src="../../../../Images/imgLupa.gif" width="20"> <img id="imgLupa1" style="DISPLAY: none; cursor: pointer" onclick="buscarSiguiente('tblCatalogoEntrada',1,'divCatalogo','imgLupa1')"
								            height="11" src="../../../../Images/imgLupaMas.gif" width="20"> &nbsp;Descripción</td>
						            <td width="25%"><img style="cursor: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#imgOrigen"
								            border="0"> <map name="imgOrigen">
								            <area onclick="ordenarTabla(2,0)" shape="RECT" coords="0,0,6,5">
								            <area onclick="ordenarTabla(2,1)" shape="RECT" coords="0,6,6,11">
							            </map>Origen</td>
						            <td width="25%"><img style="cursor: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#imgCreador"
								            border="0"> <map name="imgCreador">
								            <area onclick="ordenarTabla(3,0)" shape="RECT" coords="0,0,6,5">
								            <area onclick="ordenarTabla(3,1)" shape="RECT" coords="0,6,6,11">
							            </map>Creador</td>
						            <td width="5%" align="right"><img onmouseover="javascript:bMover=true;moverTablaUp()" style="cursor: pointer" onmouseout="javascript:bMover=false;"
								            height="8" src="../../../../Images/imgFleUp.gif" width="11">&nbsp;&nbsp;&nbsp;&nbsp;</td>
					            </tr>
				            </table>
				            <div id="divCatalogo" style="overflow-x: hidden; overflow: auto; width: 846px; height: 460px" align="left">
					            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:830px">
					                <%=strTablaHtmlCatalogo%>
					            </div>
				            </div>
				            <table id="tblResultado" style="width: 830px; height:17px">
					            <tr class="TBLFIN">
						            <td>&nbsp;<img height="1" src="../../../../Images/imgSeparador.gif" width="800"> <img onmouseover="javascript:bMover=true;moverTablaDown()" style="cursor: pointer" onmouseout="javascript:bMover=false;"
								            height="8" src="../../../../Images/imgFleDown.gif" width="11"></td>
					            </tr>

				            </table>
			            </td>
		            </tr>                			  
	            </table>
				<table style="margin-top:20px; width:500px;">
					<tr>
						<td>
							<button id="btnNuevaEntrada" type="button" style="visibility:visible;" onclick="nuevaEntrada();" class="btnH25W95" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
							    <img src="../../../../Images/botones/imgNuevo.gif" /><span title="Añade un nuevo elemento"">Añadir</span>										        
							</button>								
						</td>				
						<td>
							<button id="btnEliminarEntrada" type="button" onclick="eliminarEntrada();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
							    <img src="../../../../Images/botones/imgEliminar.gif" /><span title="Elimina el elemento seleccionado">Eliminar</span>
							</button>								
						</td>
						<td>
							<button id="btnSalir" type="button" onclick="salir();" class="btnH25W95" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
								<img src="../../../../Images/botones/imgSalir.gif" /><span title="Regresa a la pantalla anterior">Salir</span>
							</button>				
						</td>
					</tr>
				</table>	                
        </center>	                
			<br />
			<asp:textbox id="hdnPromotor" runat="server" style="visibility:hidden" >N</asp:textbox>
            <asp:textbox id="hdnIDArea" runat="server" style="visibility:hidden" ></asp:textbox>
			<asp:textbox id="hdnIDEntrada" runat="server" style="visibility:hidden" ></asp:textbox>
			<asp:textbox id="hdnErrores" runat="server" style="visibility:hidden" ></asp:textbox>
			<asp:textbox id="hdnbNueva" runat="server" style="visibility:hidden" ></asp:textbox>
	        <asp:textbox id="hdnOrden" runat="server" style="visibility:hidden" >1</asp:textbox>
	        <asp:textbox id="hdnAscDesc" runat="server" style="visibility:hidden" >0</asp:textbox>		
			<asp:textbox id="hdnFilaSeleccionada" runat="server" style="visibility:hidden" >-1</asp:textbox>
			<asp:TextBox ID="hdnModoLectura" runat="server" style="visibility:hidden" Text="" />
            <uc_mmoff:mmoff ID="mmoff1" runat="server" />
            <uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
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
