<%@ Page Language="C#" AutoEventWireup="true" contentType="text/html; charset=iso-8859-15" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_ECO_SelProf_Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title> ::: SUPER ::: - Selección de profesionales</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <meta http-equiv='Content-Type' content='text/html; charset=iso-8859-15'>
	<script language="JavaScript" src="../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../Javascript/draganddrop.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../Javascript/boxover.js" type="text/Javascript"></script>
    <script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body style="overflow: hidden" leftmargin="15" topmargin="15" onload="init()" >
<ucproc:Procesando ID="Procesando" runat="server" />
    <form id="form1" runat="server">
	<script type="text/javascript">
	<!--
        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";
        var strEstructuraNodo = "<%=Estructura.getDefCorta(Estructura.sTipoElem.NODO) %>";
	-->
	</script>
    <table style="width:850px; margin-left:10px; text-align:left;" cellpadding="5">
        <colgroup>
            <col style="width:400px;" />
            <col style="width:50px;" />
            <col style="width:400px;" />
        </colgroup>	

        <tr style="height:45px">
            <td>
                <asp:RadioButtonList id="rdbAmbito" runat="server" RepeatDirection="horizontal" SkinId="rbl" onclick="seleccionAmbito(this.id)">
                <asp:ListItem Selected="True" Value="A" Text="Nombre&nbsp;&nbsp;&nbsp;" />
                <asp:ListItem Value="C" Text="Nodo" />
                <asp:ListItem Value="G" Text="Grupo funcional&nbsp;&nbsp;&nbsp;" />
                </asp:RadioButtonList>
            </td>
            <td>&nbsp;</td>
            <td>
            <span id="ambAp" style="display:block" class="texto">
            <table style="width: 360px;">
                <colgroup>
                    <col style="width:120px;" />
                    <col style="width:120px;" />
                    <col style="width:120px;" />
                </colgroup>              
                <tr>
                <td>&nbsp;Apellido1</td>
                <td>&nbsp;Apellido2</td>
                <td>&nbsp;Nombre</td>
                </tr>
                <tr>
                <td><asp:TextBox ID="txtApellido1" runat="server" style="width:100px"  onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos('N', '');event.keyCode=0;}" MaxLength="50" /></td>
                <td><asp:TextBox ID="txtApellido2" runat="server" style="width:100px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos('N', '');event.keyCode=0;}"  MaxLength="50" /></td>
                <td><asp:TextBox ID="txtNombre"    runat="server" style="width:100px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos('N', '');event.keyCode=0;}"  MaxLength="50" /></td>
                </tr>
            </table>
            </span>
            <span id="ambCR" style="display:none" class="texto"><label id="lblCR" class="enlace" style="width:28px" onclick="obtenerCR()" runat="server"></label> <asp:TextBox ID="txtCR" runat="server" Width="316px" readonly="true" /></span>
            <span id="ambGF" style="display:none" class="texto"><label id="lblGF" class="enlace" style="width:94px" onclick="obtenerGF()">Grupo funcional</label> <asp:TextBox ID="txtGF" runat="server" Width="250px" readonly="true" /></span>
            </td>
        </tr>
        <tr>
            <td style="vertical-align:bottom; text-align:right;">
            <img src="../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos')" />&nbsp;<img src="../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom; margin-right:36px;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos')" />
            </td>
            <td></td>
            <td style="vertical-align:bottom; text-align:right;">
            <img src="../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos2')" />&nbsp;<img src="../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom; margin-right:37px;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos2')" />
            </td>
        </tr>
        <tr>
            <td><!-- Relación de técnicos -->
                <table style="width: 350px; height: 17px">
                    <tr class="TBLINI">
                        <td style="padding-left:3px">Profesionales</td>
                    </tr>
                </table>
                <div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 366px; height:280px" onscroll="scrollTablaProf()">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:350px">
                    <table id="tblDatos" style="width: 350px;">
                        <colgroup><col style='width:20px;' /><col style='width:330px;' /></colgroup>
                    </table>
                    </div>
                </div>
                <table style="width: 350px; height: 17px">
                    <tr class="TBLFIN">
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align:middle;">
                <asp:Image id="imgPapelera" style="CURSOR: pointer" runat="server" ImageUrl="../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="4"></asp:Image>
            </td>
            <td><!-- Técnicos asignados -->
                <table id="tblTituloAsignados" style="width: 350px; height: 17px">
                    <tr class="TBLINI">
                        <td style="padding-left:3px"><IMG style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#imgEmp" border="0">
						    <map name="imgEmp">
						        <area onclick="ot('tblDatos2', 1, 0, '')" shape="RECT" coords="0,0,6,5">
						        <area onclick="ot('tblDatos2', 1, 1, '')" shape="RECT" coords="0,6,6,11">
					        </map>Profesionales a asignar</TD>
                    </tr>
                </table>
                <div id="divCatalogo2" style="overflow-x: hidden; overflow: auto; width: 366px; height:280px" target="true" onmouseover="setTarget(this);" caso="3" onscroll="scrollTablaProfAsig()">
                    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:350px">
                    <table id="tblDatos2" style="width: 350px;" class="texto MM" >
                        <colgroup><col style='width:20px;' /><col style='width:330px;' /></colgroup>
                    </table>
                    </div>
                </div>
                <table style="width: 350px; height: 17px">
                    <tr class="TBLFIN">
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td>
                <label id="lblForaneo" style="width:350px;height:17px; text-align:right;" runat="server" class="enlace" onclick="addForaneo();">Añadir foráneo</label>
            </td>
        </tr>
    </table><br />
    <center>
    <table style="width:300px;">
		<tr>
			<td align="center">
                <button id="btnAceptar" type="button" onclick="aceptarAux();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../images/imgAceptar.gif" /><span>Aceptar</span>
                </button>
			</td>
			<td align="center">
                <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../images/imgCancelar.gif" /><span>Cancelar</span>
                </button>
			</td>
		</tr>
    </table>
    </center>
<DIV class="clsDragWindow" id="DW" noWrap></DIV>
<input type="hidden" id="hdnErrores" value="<%=sErrores %>" />
<input type="hidden" id="hdnNodoActual" value="" runat="server"/>
<input type="hidden" id="hdnDesNodoActual" value="" runat="server"/>
<input type="hidden" id="hdnCualidad" value="" runat="server"/>
<input type="hidden" name="hdnEsReplicable" id="hdnEsReplicable" value="" runat="server" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</form>
</body>
</html>
