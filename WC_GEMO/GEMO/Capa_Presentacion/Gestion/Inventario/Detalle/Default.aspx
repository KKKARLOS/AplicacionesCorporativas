<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" ValidateRequest="false" %>
<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Import Namespace="GEMO.BLL" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 	"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: GEMO.net ::: - Detalle de la línea</title>
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <script src="../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/boxover.js" type="text/Javascript"></script>
	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onload="init()" onunload="unload()">
<form name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
<script type="text/javascript">
        var sRecargarCat = null;    
        var bNueva = "<%=Request.QueryString["bNueva"]%>";
        var strServer = "<% =Session["strServer"].ToString() %>";
        var sIDFICEPI = "<% =Session["IDFICEPI"].ToString() %>";
        var intSession = <%=Session.Timeout%>;
        var bCambios = false;
        var bLectura = <%=sLectura%>;
        var bSalir = false;
    </script>    
<br /><br />
<table id="tabla" style="padding:10px;width:980px;text-align:left">
	<tr>
		<td valign="top">
			<fieldset style="margin-top:10px;width:450px;height:280px">
                <legend>Línea</legend>  
					<table style="width:450px;" cellpadding="5">
						<colgroup>
							<col style="width:100px;" />
							<col style="width:350px;" />
						</colgroup>
						<tr>
							<td>
								Nº&nbsp;<span style="color:Red">*</span>
							</td>
							<td>
							    <asp:TextBox ID="txtNumlinea" style="width:90px;" Text="" MaxLength="12" onkeyup="aG();" CssClass="txtNumM" SkinID=numero runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						        Cod.País<asp:Image title="El código de país es obligatorio para líneas no españolas" ID="imgInfoEstado" runat="server" ImageUrl="~/Images/info.gif" style="margin-left:5px;position:relative; top:+5px;" />
						        <asp:TextBox ID="txtPrefijo"  style="width:25px;margin-left:5px" Text="" MaxLength="3" onkeyup="aG();" onkeypress='vtn2(event);' CssClass="txtNumM" SkinID="numero" runat="server" />
							</td>
						</tr>
						<tr>
							<td>
								Extensión
							</td>
							<td>
								<asp:TextBox ID="txtNumext" style="width:90px;" Text=""  MaxLength="6" onkeyup="aG();" CssClass="txtNumM" SkinID=numero runat="server" />
							</td>							
						</tr>                                                           
						<tr>
							<td>
								ICC
							</td>
							<td>
								<asp:TextBox ID="txtICC" runat="server" style="width:160px;" MaxLength="20" Text="" onkeyup="aG();"/>
							</td>               
						</tr>  
						<tr>
							<td>
								Proveedor&nbsp;<span style="color:Red">*</span>
							</td>
							<td>
								<asp:DropDownList ID="cboProveedor" runat="server" onchange="aG(0);" style="width:260px;" AppendDataBoundItems=true>
								<asp:ListItem Value="" Text="" Selected=true></asp:ListItem>
								</asp:DropDownList> 
							</td>                                  
						</tr>  
						<tr>
							<td>
								Perfil-Tarifa
							</td>
							<td>
								<asp:DropDownList ID="cboTarifa" runat="server" onchange="aG(0);" style="width:260px;" AppendDataBoundItems=true>
								<asp:ListItem Value="" Text="" Selected=true></asp:ListItem>
								</asp:DropDownList> 
							</td>
						</tr>  
						<tr>												
							<td>
								Tipo tarjeta
							</td>
							<td>
								<asp:DropDownList ID="cboTipoTarjeta" runat="server" onchange="aG(0);" style="width:260px;" AppendDataBoundItems=true>
								<asp:ListItem Value="" Text="" Selected=true></asp:ListItem>
								</asp:DropDownList> 
							</td>    
						</tr>  
						<tr>							
							<td>
								Estado
							</td>
							<td>
								<asp:DropDownList ID="cboEstado" runat="server" onchange="aG(0);" style="width:260px;" AppendDataBoundItems=true>
								</asp:DropDownList> 
							</td>                                       
						</tr>                                                                         
						<tr style="width:18px">
							<td valign=top>
								<label id="lblEmpresa" class="enlace" style="cursor:hand" onclick="if (this.className=='enlace') getEmpresa()">
									Empresa
								</label>&nbsp;<span style="color:Red">*</span>
							</td>
							<td>
							 <%--<asp:TextBox ID="txtIDEmpresa" style="width:60px;"  Text="" ReadOnly runat="server" />&nbsp;&nbsp;--%>
							 <asp:TextBox ID="txtEmpresa"   style="width:325px;" Text="" ReadOnly runat="server" />
								&nbsp;
							</td>                                
						</tr>
						<tr style="width:18px">
							<td>
								Quién es quién
							</td>
							<td>                                
								<asp:CheckBox id="chkQEQ" runat="server"  onclick="aG();" style="vertical-align:middle;"/>
							</td> 
						</tr>                     
					</table>
				</fieldset>												
        </td>
		<TD valign="top" align="left">
			<fieldset style="margin-top:10px;width:450px;">
                <legend>Medio</legend>  
					<table width="450px" cellpadding="5">
						<colgroup>
							<col style="width:100px;" />
							<col style="width:350px;" />
						</colgroup>
						<tr>						
							<td>
								Denominación
							</td>
							<td>
								<asp:DropDownList ID="cboMedio" runat="server" onchange="aG(0);" style="width:260px;" AppendDataBoundItems=true>
								<asp:ListItem Value="" Text="" Selected=true></asp:ListItem>
								</asp:DropDownList> 
							</td> 
						</tr>
					</table>
			</fieldset>	
			<fieldset style="margin-top:10px;width:450px;">
                <legend>Terminal</legend>  
					<table width="450px" cellpadding="5">
						<colgroup>
							<col style="width:100px;" />
							<col style="width:350px;" />
						</colgroup>
						<tr>
							<td>
								IMEI
							</td>
							<td>
								<asp:TextBox ID="txtIMEI" runat="server" style="width:160px;" MaxLength="20" Text="" onkeyup="aG();"/>
							</td>						
						</tr>
						<tr>
							<td>
								Modelo
							</td>
							<td>
								<asp:TextBox ID="txtModelo" runat="server" style="width:315px;" MaxLength="40" Text="" onkeyup="aG();"/>
							</td>
						</tr> 
					</table>
			</fieldset>		


			<fieldset style="margin-top:10px;width:450px;height:135px">
                <legend>Asignación</legend>  
					<table width="450px" cellpadding="5">
						<colgroup>
							<col style="width:100px;" />
							<col style="width:350px;" />
						</colgroup>
						<tr>
							<td valign=top style="margin-top:20px">
								<label id="lblResponsable" class="enlace" style="cursor:hand" onclick="if (this.className=='enlace') getResponsable()">
									Responsable
								</label>&nbsp;<span style="color:Red">*</span>
							</td>
							<td>
								<%--asp:TextBox ID="txtIDResponsable" style="width:60px;"  Text="" ReadOnly runat="server" />&nbsp;&nbsp;--%>
								<asp:TextBox ID="txtResponsable"   style="width:315px;" Text="" ReadOnly runat="server" />
							</td> 						
						</tr> 
						<tr>
							<td>
								Tipo de uso&nbsp;<span style="color:Red">*</span>
							</td>
							<td valign="middle"> 
								<asp:radiobuttonlist id="rdlTipoUso" onclick="javascript:aG();HabDes();" runat="server" CssClass="texto" width="240px" CellSpacing="0" CellPadding="0" RepeatLayout="Flow" RepeatDirection="horizontal">
									<asp:ListItem Value="P">Asignado profesional</asp:ListItem>
									<asp:ListItem Value="D">Departamental</asp:ListItem>
								</asp:radiobuttonlist>                     
							</td> 						
						</tr>
						<tr>
							<td valign=top>
								<label id="lblBeneficiario" class="enlace" style="cursor:hand" onclick="if (this.className=='enlace') getBeneficiario()">
									Beneficiario
								</label>
							</td>
							<td valign=top>
								<%--<asp:TextBox ID="txtIDBeneficiario" style="width:60px;"  Text="" ReadOnly runat="server" />&nbsp;&nbsp;--%>
								<asp:TextBox ID="txtBeneficiario"   style="width:315px;" Text="" ReadOnly runat="server" />                   								
							</td>						
						</tr>
						<tr>
							<td valign=top>
								<label id="lblDepartamento" class="enlace" style="cursor:hand" onclick="if (this.className=='enlace') getDepartamento()">
								 Departamento
								</label>
							</td>                     
							<td valign=top>                   
								<asp:TextBox ID="txtDepartamento" runat="server" style="width:315px;" MaxLength="50" onKeyUp="aG();" Text="" />
								<asp:image id="btnDepartamento" style="CURSOR: hand; visibility:visible; vertical-align:middle;" onclick="delDepartamento();" runat="server" ImageUrl="../../../../images/imgBorrar.gif"></asp:image>
						   </td> 						
						</tr>				
					</table>
			</fieldset>										
        </TD>		
    </tr>
	<TR>
		<TD colspan="2">
		<table align="left" width="95%" style="margin-left:8px;margin-top:5px">
		<tr>
		<td>
		    Observaciones
		</td>
		</tr>
	    <tr>
		<td>
			<asp:TextBox ID="txtObserva" SkinID=multi runat="server" TextMode="MultiLine" Rows="7" Width="980px" onKeyUp="aG();" />		
		</TD>
		</tr>
		</table>	
	</TR>
</table>
<center>
<table id="tblBotones" width="80%" style="margin-top:5px">
	<tr> 
		<td align="center">
            <button id="btnNuevo" type="button" onclick="nuevo()" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				onmouseover="se(this, 25);mostrarCursor(this);">		
                <img src="../../../../images/botones/imgNuevo.gif"/><span title="Nuevo">&nbsp;&nbsp;Nuevo</span>
            </button>    
		</td>
		<td align="center">
            <button id="btnDuplicar" type="button" onclick="Duplicar()" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../../images/botones/imgDuplicar.gif"/><span title="Crea copia de la línea actual">&nbsp;&nbsp;Duplicar</span>
            </button>    
		</td>			
		<td align="center">
            <button id="btnGrabar" type="button" onclick="grabarAux()" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../../images/botones/imgGrabar.gif" /><span title="Grabar">&nbsp;&nbsp;Grabar</span>
            </button>    
		</td>
		<td align="center">
            <button id="btnGrabarSalir" type="button" onclick="grabarSalir()" class="btnH25W100" runat="server" disabled hidefocus="hidefocus" 
				onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../../images/botones/imgGrabarSalir.gif" /><span title="Grabar y salir">&nbsp;Grabar...</span>
            </button>    
		</td>
		<td align="center">
            <button id="btnCronologia" type="button" onclick="getCronologia()" class="btnH25W110" runat="server" hidefocus="hidefocus" 
				onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../../images/imgHorario.gif" /><span title="Cronología">&nbsp;Cronología</span>
            </button>    
		</td>		
		<td align="center">
            <button id="btnSalir" type="button" onclick="salir()" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				onmouseover="se(this, 25);mostrarCursor(this);">			
                <img src="../../../../images/botones/imgSalir.gif" /><span title="Salir">&nbsp;&nbsp;Salir</span>
            </button>    
		</td>
	</tr>
</table>
</center>
    <input type="hidden" id="hdnErrores" value="<%=sErrores %>" />
    <asp:textbox id="hdnId" runat="server" style="visibility:hidden">0</asp:textbox> 
    <asp:textbox id="hdnEstado" runat="server" style="visibility:hidden">0</asp:textbox>
    <asp:textbox ID="hdnIdEmpresa" runat="server" style="visibility:hidden">0</asp:textbox>
    <asp:textbox ID="hdnIdBeneficiario" runat="server" style="visibility:hidden">0</asp:textbox>
    <asp:textbox ID="hdnIdResponsable" runat="server" style="visibility:hidden;">0</asp:textbox>
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
