<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self">
    <title> ::: SUPER ::: - Mantenimiento de campos</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<link href="../../../../PopCalendar/CSS/Classic.css"type="text/css" rel="stylesheet" />
    <script src="../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../PopCalendar/PopCalendar.js"type="text/Javascript"></script>
	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
    <style type="text/css">
    .txtLMA  /* Caja de texto transparente con cursor mano azul 2*/
    {
        border: 0px;
        padding: 2px 0px 0px 2px;
        margin: 0px;
        font-size: 11px;
        background-color: Transparent;
        font-family: Arial, Helvetica, sans-serif;
        height: 14px;
        cursor: url('../../../../../../images/imgManoAzul2.cur'),url('../../../../../images/imgManoAzul2.cur'),url('../../../../images/imgManoAzul2.cur'),url('../../../images/imgManoAzul2.cur'),url('../../images/imgManoAzul2.cur'),url('../images/imgManoAzul2.cur'),pointer;
    }
    #tblDatos tr { height: 20px; }
    </style>
</head>
<body style="OVERFLOW: hidden" leftMargin="10" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
<form id="form1" name="form1" runat="server" action="Default.aspx" method="POST" enctype="multipart/form-data">
<script type="text/javascript">
    var intSession = <%=Session.Timeout%>; 
    var strServer = "<%=Session["strServer"]%>";
    var sProfesional = "<%=sProfEntrada%>";
</script>  
    <center>
        <table style="width: 918px; text-align:left;margin-top:9px">
            <colgroup><col style="width:160px"/><col style="width:718px"/></colgroup>
               <tr>
                <td align="left" style="vertical-align:bottom;">
                    Ámbito&nbsp;
				        <asp:DropDownList ID="cboAmbito" runat="server" onchange="cargarAmbitoTipo();" style="width:95px;" AppendDataBoundItems=true>
					        <asp:ListItem Value=""  Text="" Selected="True"></asp:ListItem>    					        
                            <asp:ListItem Value="0" Text="Empresarial"></asp:ListItem>
					        <asp:ListItem Value="1" Text="Privado"></asp:ListItem>
					        <asp:ListItem Value="2" Text="Proyecto"></asp:ListItem>
					        <asp:ListItem Value="3" Text="Cliente"></asp:ListItem>
					        <asp:ListItem Value="4" Text="C.R."></asp:ListItem>
					        <asp:ListItem Value="5" Text="Equipo"></asp:ListItem>	
                            <asp:ListItem Value="99" Text="Todos"></asp:ListItem>                             					
                        
				        </asp:DropDownList>									
		        </td>
                <td align="left" style="vertical-align:bottom;">&nbsp;&nbsp;&nbsp;Tipo de dato&nbsp;
					<asp:DropDownList ID="cboTipoDato" runat="server" onchange="cargarAmbitoTipo();"  style="width:90px;" AppendDataBoundItems=true>
                    </asp:DropDownList> 
                </td>
            </tr>
            <tr>
                <td colspan="2">
		            <table style="width: 900px; height: 17px; margin-top:9px;">
		                <colgroup><col style="width:480px"/><col style="width:80px"/><col style="width:70px"/><col style="width:270px"/></colgroup>
			            <tr class="TBLINI">
				            <td>&nbsp;Denominación</td>
                            <td>Ámbito</td>
				            <td>Tipo</td>
				            <td>Creador</td>
			            </tr>
		            </table>
		            <div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 916px; height:440px">
		                <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:900px">
		                    <%=strTablaHTML %>
                        </div>
                    </div>
		            <table style="width: 900px; height: 17px;">
			            <tr class="TBLFIN">
				            <td> </td>
			            </tr>
		            </table>
                </td>
            </tr>
        </table>
        <table style="width:350px; margin-top:10px; margin-left:20px;">
	    <tr>
		    <td>
			    <button id="btnAnadir" type="button" onclick="nuevo()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../../../images/botones/imgAnadir.gif" /><span title="Añadir">Añadir</span>
			    </button>	
		    </td>
		    <td>
			    <button id="btnEliminar" type="button" onclick="eliminar()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../../../images/botones/imgEliminar.gif" /><span title="Borrar">Borrar</span>
			    </button>	
		    </td>
<%--		    <td>
			    <button id="btnGrabar" type="button" onclick="grabarAux()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../../../images/botones/imgGrabar.gif" /><span title="Grabar">Grabar</span>
			    </button>	
		    </td>
	        <td>
			    <button id="btnGrabarSalir" type="button" onclick="grabarSalir();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../../../images/botones/imgGrabarSalir.gif" /><span title="Grabar y salir">Grabar...</span>
			    </button>	
	        </td>
--%>
	        <td>
			    <button id="btnSalir" type="button" onclick="salir();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../../../images/botones/imgSalir.gif" /><span title="Salir">&nbsp;&nbsp;Salir</span>
			    </button>	 
	        </td>
	    </tr>
	    </table>
    </center>
    <input type="hidden" id="hdnErrores" value="<%=sErrores %>" />
    <input type="hidden" id="hdnT305_idproyectosubnodo" value="" runat="server"/>
    <input type="hidden" id="hdnCodTarea" value="" runat="server"/>  
    <asp:textbox id="hdn_ficepi_actual" runat="server" style="visibility:hidden"></asp:textbox>
    <uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
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