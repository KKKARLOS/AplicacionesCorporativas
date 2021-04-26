<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" ValidateRequest="false" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> ::: SUPER ::: - Agrupación de órdenes de facturación</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onload="init()" onunload="unload()">
<form name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
<script type="text/javascript">
<!--
    var strServer = "<% =Session["strServer"].ToString() %>";
    var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
    var bCambios = false;
    var sNumEmpleado = "<%=Session["UsuarioActual"].ToString() %>";
-->
</script>
<br />
<table id="tbl" style="width:920px; margin:10px;">
    <colgroup>
        <col style="width:440px;" />
        <col style="width:40px;" />
        <col style="width:440px;" />
    </colgroup>
	<tr>
		<td>
		    <table style="width: 420px; height: 17px;">
		        <colgroup>
		            <col style="width:50px;" />
		            <col style="width:150px;" />
		            <col style="width:220px;" />
		        </colgroup>
			    <tr class="TBLINI">
				    <td style="padding-left:3px;" title="Referencia de agrupación">Agrup.</td>
				    <td>Denominación</td>
				    <td>Autor</td>
			    </tr>
		    </table>
		    <div id="divCatalogoAgrupaciones" style="overflow: auto; overflow-x: hidden; width: 436px; height:150px" runat="server">
		        <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:420px">
		        <%=strTablaHTML%>
		        </div>
		    </div>
		    <table id="tblTotal" style="width: 420px; height: 17px;">
			    <tr class="TBLFIN">
				    <td >&nbsp;</td>
			    </tr>
		    </table>
		    <center>
                <table style="margin-top:5px; width:330px;">
                    <tr>
                        <td>
						    <button id="btnAddAgrupacion" type="button" onclick="addAgrupacion();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
							     onmouseover="se(this, 25);mostrarCursor(this);">
							    <img src="../../../../images/botones/imgAnadir.gif" /><span title="Añadir agrupación">&nbsp;&nbsp;Añadir</span>
						    </button>						
                        </td>
                        <td>
						    <button id="btnModAgrupacion" type="button" onclick="modAgrupacion();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
							     onmouseover="se(this, 25);mostrarCursor(this);">
							    <img src="../../../../images/imgEdicion.gif" /><span title="Modificar agrupación">Modificar</span>
						    </button>					 
                        </td>
                        <td>			
						    <button id="btnDelAgrupacion" type="button" onclick="delAgrupacion();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
							     onmouseover="se(this, 25);mostrarCursor(this);">
							    <img src="../../../../images/botones/imgEliminar.gif" /><span title="Eliminar">Eliminar</span>
						    </button>	 
                        </td>
                    </tr>
                </table>		    
            </center>                
        </td>
		<td>
		
        </td>
		<td style="vertical-align:top;">
		    <table style="width: 420px; height: 17px;">
		        <colgroup>
		            <col style="width:70px;" />
		            <col style="width:350px;" />
		        </colgroup>
			    <tr class="TBLINI">
				    <td style="text-align:right; padding-right: 10px;">Proyecto</td>
				    <td>Responsable</td>
			    </tr>
		    </table>
		    <div id="divCatalogoProyectos" style="overflow: auto; overflow-x: hidden; width: 436px; height:150px" runat="server">
		        <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:420px">
		        </div>
		    </div>
		    <table style="width: 420px; height: 17px;">
			    <tr class="TBLFIN">
				    <td >&nbsp;</td>
			    </tr>
		    </table>
        </td>
    </tr>
</table>
<center>
    <table id="tblBotonera" style="width:260px; margin-top:20px;">
        <colgroup>
            <col style="width:130px;" />
            <col style="width:130px;" />
        </colgroup>
        <tr>
            <td> 
			    <button id="btnAparcar" type="button" onclick="aceptar();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../../images/imgAceptar.gif" /><span title="Aceptar">Aceptar</span>
			    </button>			
            </td>		        
            <td>
			    <button id="btnSalir" type="button" onclick="cerrarVentana();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../../images/botones/imgCancelar.gif" /><span title="Cancelar">Cancelar</span>
			    </button>		
            </td>
          </tr>
    </table>
</center>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<asp:TextBox ID="hdnT301IdProy" runat="server" style="visibility:hidden" Text="" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
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
