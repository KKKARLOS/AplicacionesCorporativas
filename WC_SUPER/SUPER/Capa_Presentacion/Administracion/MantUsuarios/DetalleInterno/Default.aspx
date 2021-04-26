<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Detalle de empleado interno</title>
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <link rel="stylesheet" href="../../../../PopCalendar/CSS/Classic.css" type="text/css" />
    <link rel="stylesheet" href="../../../../PopCalendar/CSS/Classic.css" type="text/css" />
    <script src="../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script src="Functions/funciones.js?v=20180417" type="text/Javascript"></script>
    <script src="../../../../PopCalendar/PopCalendar.js" type="text/Javascript"></script>
 	<script src="../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onload="init()" onunload="unload()">
<form name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
    <script type="text/javascript">
        var strServer = "<% =Session["strServer"].ToString() %>";
        var intSession = <%=Session.Timeout%>;
        var nIdUsuario = <%=nIdUsuario.ToString()%>;
        var nIdFicepi = <%=nIdFicepi.ToString()%>;
        var strEstructuraNodo = "<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>";
        var bCambios = false;
        var bLectura = false;
        var bSalir = false;
        var btnCal = "<%=Session["BTN_FECHA"].ToString() %>";
    </script>    
<table id="tblDatos" style="width:810px; margin-top:10px;" cellpadding="5px" align="center" border="0">
    <colgroup>
        <col style="width:100px;" />
        <col style="width:410px;" />
        <col style="width:300px;" />
    </colgroup>
    <tr>
        <td>Usuario</td>
        <td><asp:TextBox ID="txtUsuario" style="width:50px; text-align:right;" Text="" readonly="true" runat="server" />
            <label id="Label4" style="margin-left:70px;">Alias</label> <asp:TextBox ID="txtAlias" runat="server" style="width:240px" MaxLength="30" onkeyup="aG()" />    
        </td>
        <td rowspan="3">
            <fieldset style="width:285px;">
                <legend>IAP</legend>  
                <table style="width: 280px;" cellpadding="4px">
                <tr>
                    <td><label id="lblUltImp">Última imputación registrada</label> <asp:TextBox ID="txtUltImp" runat="server" style="width:60px; margin-left:2px;" readonly="true" /></td>
                </tr>
                <tr>
                    <td><asp:CheckBox ID="chkHuecos" runat="server" Text="" style="cursor:pointer; vertical-align:middle;" onclick="this.blur();aG();" checked="true" />&nbsp;Control huecos
                        <asp:CheckBox ID="chkMailIAP" runat="server" Text="" style="cursor:pointer; vertical-align:middle; margin-left:35px;" onclick="this.blur();aG();" checked="true" />&nbsp;Correo recordatorio
                    </td>
                </tr>
                </table>
             </fieldset> 
        </td>
    </tr>
    <tr>
        <td style="vertical-align:top;">Profesional</td>
        <td><asp:TextBox ID="txtDesProfesional" style="width:390px;" Text="" readonly="true" runat="server" />
	        <asp:TextBox ID="hdnIDFICEPI" style="width:1px; visibility:hidden;" Text="" readonly="true" runat="server" /></td>
    </tr>
    <tr>
        <td style="vertical-align:top;"><label id="lblNodo" runat="server"></label></td>
        <td><asp:TextBox ID="txtDesNodo" style="width:390px;" Text="" readonly="true" runat="server" />
	        <asp:TextBox ID="hdnIdNodo" style="width:1px; visibility:hidden;" Text="" readonly="true" runat="server" /></td>
   </tr>
    <tr>
        <td style="vertical-align:top;"><label id="lblEmpresa" runat="server" class="enlace" onclick="getEmpresa()">Empresa</label></td>
        <td><asp:TextBox ID="txtDesEmpresa" style="width:390px;" Text="" readonly="true" runat="server" />
	        <asp:TextBox ID="hdnIDEmpresa" style="width:1px; visibility:hidden;" Text="" readonly="true" runat="server" /></td>
        <td rowspan="3">
            <fieldset style="width:285px;">
                <legend>Coste</legend>  
                <table class=texto style="width: 280px; height:31px;" cellpadding="4">
                <tr>
                    <td>
                        Moneda&nbsp;&nbsp;<asp:DropDownList ID="cboMoneda" runat="server" Width="173px" onchange="aG();" AppendDataBoundItems="true"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <img src='../../../../Images/Botones/imgHorario.gif' class="ICO" title="Hora" hidefocus=hidefocus /> <asp:TextBox ID="txtCosteHora" SkinID="Numero" runat="server" style="width:60px;" onfocus="fn(this, 6, 4)" onkeyup="aG();if (this.value=='') this.value='0,0000';" />
                        <img src='../../../../Images/Botones/imgCalendario.gif' class="ICO" title="Jornada" style="margin-left:48px;" hidefocus=hidefocus /> <asp:TextBox ID="txtCosteJornada" SkinID="Numero" runat="server" style="width:60px;" onfocus="fn(this, 6, 4)" onkeyup="aG();if (this.value=='') this.value='0,0000';" />
                    </td>
                </tr>
                </table>
             </fieldset> 
        </td>
    </tr>
    <tr>
        <td style="vertical-align:top;">Oficina</td>
        <td><asp:TextBox ID="txtDesOficina" style="width:390px;" Text="" readonly="true" runat="server" />
	        <asp:TextBox ID="txtIDOficina" style="width:1px; visibility:hidden;" Text="" readonly="true" runat="server" />
        </td>
    </tr>
    <tr>
        <td style="vertical-align:top;"><label id="lblOficina" runat="server" class="enlace" onclick="abrirCalendario();">Calendario</label></td>
        <td><asp:TextBox ID="txtDesCalendario" style="width:250px;" Text="" class="enlace" readonly="true" runat="server" />
	        <asp:TextBox ID="hdnIdCalendario" style="width:1px; visibility:hidden;" Text="" readonly="true" runat="server" />
            <label id="lblNJornLab" style="margin-left:10px;">N. Jorn. Lab.</label> <asp:TextBox ID="txtNJornLab" style="width:50px;" Text="" readonly="true" runat="server"/>
        </td>
    </tr>
    <tr>
        <td>Fecha alta</td>
        <td><asp:TextBox ID="txtFecAlta" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" onchange="aG();" goma="0" />
            <label id="lblFecBaja" style="margin-left:20px;">Fecha baja</label> <asp:TextBox ID="txtFecBaja" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" onchange="aG();" />
            <asp:CheckBox ID="chkNuevoGasvi" runat="server" Text="" style="cursor:pointer; vertical-align:middle;margin-left:5px;" onclick="this.blur();aG();" title="Permite crear notas de GASVI" />&nbsp;Tramitar GASVI
            <asp:CheckBox ID="chkACS" runat="server" Text="" style="cursor:pointer; vertical-align:middle;margin-left:20px;" onclick="this.blur();aG();" title="Acceso coral SUPER" />&nbsp;ACS
        </td>
        <td>
            <asp:CheckBox ID="chkRelevo" runat="server" Text="" style="cursor:pointer; vertical-align:middle;margin-left:10px;" onclick="this.blur();aG();" title="Contrato de relevo" />&nbsp;Contrato relevo
        </td>
    </tr>
    <tr>
        <td>Login HERMES</td>
        <td><asp:TextBox ID="txtLoginHermes" style="width:180px;" Text="" MaxLength="25" runat="server" onkeyup="aG();" />
            <label id="Label3" style="margin-left:30px;">ID comercial SAP</label> <asp:TextBox ID="txtComSAP" style="width:35px;" Text="" MaxLength="3" runat="server" onkeyup="aG();" />
        </td>
       
    </tr>
    <tr>
        <td title="Categoría de confidencialidad">Confidencialidad</td>
        <td style="vertical-align:middle;"><asp:DropDownList id="cboCategoria" runat="server" Width="180px" onChange="setCategoria();aG();window.focus();" AppendDataBoundItems="true">
                        <asp:ListItem Value="" Text=""></asp:ListItem>
                        </asp:DropDownList>
            <label id="Label5" style="margin-left:30px;" title="Sistema de cálculo de las jornadas adaptadas">Cálculo JA</label>
            <asp:DropDownList id="cboCJA" runat="server" Width="70px" onChange="aG();window.focus();" AppendDataBoundItems="true">
                        <asp:ListItem Value="0" Text="Diario"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Mensual"></asp:ListItem>
                        </asp:DropDownList>
        </td>
         <td rowspan="2">
            <fieldset style="width:285px;">
                <legend>Jornada especial&nbsp;<asp:CheckBox ID="chkTieneJE" runat="server" Text="" style="cursor:pointer; vertical-align:middle;" onclick="this.blur();setJE();aG();" /></legend>  
                <table class="texto" style="width:293px; height:28px;" cellpadding="3px">
                <tr>
                    <td>Horas 
                        <asp:TextBox ID="txtHorasJE" SkinID="Numero" runat="server" style="width:40px;" onfocus="fn(this)" onkeyup="aG();" />
                        <label id="Label2" style="margin-left:10px;">Inicio</label> 
                        <asp:TextBox ID="txtInicioJE" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" onchange="aG();" />
                        <label id="Label1" style="margin-left:10px;">Fin</label> 
                        <asp:TextBox ID="txtFinJE" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" onchange="aG();" />
                    </td>
                </tr>
                </table>
             </fieldset> 
        </td>
    </tr>
    <tr>
        <td>Margen de cesión</td>
        <td><asp:TextBox ID="txtMargenCesion" style="width:40px;" Text="0,00" SkinID="Numero" onfocus="fn(this, 5, 2);" runat="server" onchange="aG();" />%
        <asp:CheckBox ID="chkAlertas" runat="server" Text="" style="margin-left:10px; cursor:pointer; vertical-align:middle;" onclick="this.blur();aG();" checked="false" />&nbsp;Excluido de alertas
        </td>
        
    </tr>
</table>
<center>
<table id="tblBotones" align="center" style="margin-top:15px;" width="80%">
    <tr>
	    <td align="center">
			<button id="btnGrabar" type="button" onclick="grabarAux();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../../images/botones/imgGrabar.gif" /><span title="Grabar">&nbsp;&nbsp;Grabar</span>
			</button>	
	    </td>
	    <td align="center">
			<button id="btnGrabarSalir" type="button" onclick="grabarSalir();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../../images/botones/imgGrabarSalir.gif" /><span title="Grabar y salir">Grabar...</span>
			</button>	
	    </td>	
		<td align="center">
			<button id="btnEliminar" type="button" onclick="eliminar()" class="btnH25W90" runat="server" disabled hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../../images/botones/imgEliminar.gif" /><span title="Eliminar">Eliminar</span>
			</button>	
		</td>
		<td align="center">
			<button id="btnFiguras" type="button" onclick="figuras()" class="btnH25W90" runat="server" disabled hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../../images/imgInvitado.gif" /><span title="Figuras">Figuras</span>
			</button>	
		</td>
	    <td align="center">
			<button id="btnSalir" type="button" onclick="salir();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../../images/botones/imgSalir.gif" /><span title="Salir">&nbsp;&nbsp;Salir</span>
			</button>	 
	    </td>
    </tr>
</table>
</center>	
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<input type="hidden" name="hdnFAltaIni" id="hdnFAltaIni" value="" runat="server" />
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
