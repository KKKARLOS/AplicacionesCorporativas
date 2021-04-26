<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_ECO_Escenarios_ProfesionalEscenario_Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
	<title> ::: SUPER ::: - Selección de profesionales en escenario</title>
	<meta http-equiv='X-UA-Compatible' content='IE=8' />
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/draganddrop.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/boxover.js" type="text/Javascript"></script>
    <script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body style="OVERFLOW: hidden; margin-left:15px; margin-top:15px;" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
    <form id="form1" runat="server">
	<script type="text/javascript">
	<!--
        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";
	-->
	</script>
    <table style="margin-left:10px; margin-top:10px; width:980px;">
        <colgroup>
            <col style="width:390px;" />
            <col style="width:40px;" />
            <col style="width:550px;" />
        </colgroup>
        <tr>
            <td>
                <table style="WIDTH:350px; display:inline-block;">
                    <tr>
                        <td style="padding-left:2px;">Apellido1</td>
                        <td style="padding-left:2px;">Apellido2</td>
                        <td style="padding-left:2px;">Nombre</td>
                    </tr>
                    <tr>
                        <td><asp:TextBox ID="txtApellido1" runat="server" style="width:110px"  onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos('N', '');event.keyCode=0;}" MaxLength="50" /></td>
                        <td><asp:TextBox ID="txtApellido2" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos('N', '');event.keyCode=0;}" MaxLength="50" /></td>
                        <td><asp:TextBox ID="txtNombre" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos('N', '');event.keyCode=0;}" MaxLength="50" /></td>
                    </tr>
                </table>
                <img src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom; display:inline-block;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos')" />&nbsp;<img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom; display:inline-block;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos')" />
            </td>
            <td></td>
            <td style="vertical-align:bottom; text-align:right;">
            <img src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos2')" />&nbsp;<img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom; margin-right:18px;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos2')" />
            </td>
        </tr>
        <tr>
            <td><!-- Relación de técnicos -->
                <table style="width:350px; height:17px">
                    <tr class="TBLINI">
                        <td style="padding-left:3px">Profesionales</td>
                    </tr>
                </table>
                <div id="divCatalogo" style="OVERFLOW: auto; OVERFLOW-X: hidden; WIDTH: 366px; height:280px" onscroll="scrollTablaProf()">
                    <div style="background-image: url('../../../../Images/imgFT20.gif'); background-repeat:repeat; width:350px; height:auto;">
                    <table id="tblDatos" style="WIDTH: 350px;">
                        <colgroup><col style='width:350px' /></colgroup>
                    </table>
                    </div>
                </div>
                <table style="width:350px; height:17px">
                    <tr class="TBLFIN">
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align:middle;">
                <asp:Image id="imgPapelera" style="CURSOR: pointer" runat="server" ImageUrl="../../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="4"></asp:Image>
            </td>
            <td><!-- Técnicos asignados -->
                <table id="tblTituloAsignados" style="WIDTH: 530px; HEIGHT: 17px">
                    <colgroup>
                        <col style="width:250px" />
                        <col style="width:65px" />
                        <col style="width:65px" />
                        <col style="width:150px" />
                    </colgroup>
                    <tr class="TBLINI">
                        <td style="padding-left:3px"><IMG style="CURSOR: pointer; display:inline-block;" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#imgEmp" border="0">
						    <map name="imgEmp" style="display:inline-block;">
						        <area onclick="ot('tblDatos2', 1, 0, '')" shape="rect" coords="0,0,6,5">
						        <area onclick="ot('tblDatos2', 1, 1, '')" shape="rect" coords="0,6,6,11">
					        </map>Profesionales en el escenario</td>
					    <td>F. Alta</td>
					    <td>F. Baja</td>
					    <td>Perfil por defecto</td>
                    </tr>
                </table>
                <div id="divCatalogo2" style="OVERFLOW:auto; OVERFLOW-X:hidden; WIDTH: 546px; height:280px" target="true" onmouseover="setTarget(this);" caso="2" onscroll="scrollTablaProfAsig()">
                    <div style="background-image: url('../../../../Images/imgFT20.gif'); background-repeat:repeat; width:490px; height:auto;">
                    <table id="tblDatos2" style="WIDTH: 530px;" class="texto MM">
                        <colgroup><col style='width:530px' /></colgroup>
                    </table>
                    </div>
                </div>
                <table style="width:530px; height:17px">
                    <tr class="TBLFIN">
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table><br />
    <center>
    <table style="width:300px; margin-top:10px; text-align:center;">
		<tr>
        	<td>
                <button id="btnAceptar" type="button" onclick="aceptarAux();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
                     onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../../images/imgAceptar.gif" /><span>Aceptar</span>
                </button>
			</td>
        	<td>
                <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
                     onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../../images/imgCancelar.gif" /><span>Cancelar</span>
                </button>
			</td>
		</tr>
    </table>
    </center>
<div class="clsDragWindow" id="DW" noWrap></div>
<input type="hidden" id="hdnErrores" value="<%=sErrores %>" />
<input type="hidden" id="hdnNodoActual" value="" runat="server"/>
<input type="hidden" id="hdnDesNodoActual" value="" runat="server"/>
<input type="hidden" id="hdnCualidad" value="" runat="server"/>
<input type="hidden" name="hdnEsReplicable" id="hdnEsReplicable" value="" runat="server"/>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</form>
</body>
</html>
