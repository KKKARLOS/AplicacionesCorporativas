<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_ECO_Consultas_getCriterio_Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
	<title> ::: SUPER ::: - Selección de criterio</title>
	<meta http-equiv='X-UA-Compatible' content='IE=8' />
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/draganddrop.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/boxover.js" type="text/Javascript"></script>
    <script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body style="OVERFLOW: hidden; margin-left:15px; margin-top:5px;" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
<form id="form1" runat="server">
<script type="text/javascript">

    var intSession = <%=Session.Timeout%>; 
    var strServer = "<%=Session["strServer"]%>";
    var nTipo = "<%=Request.QueryString["nTipo"]%>";
    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";

</script>

<table style="margin-left:10px; width:800px;">
    <colgroup>
        <col style="width:375px;" />
        <col style="width:50px" />
        <col style="width:375px;" />
    </colgroup>
    <tr>
        <td>
            <span id="ambDenominacion" class="texto" runat="server">
                <label id ="lblDenominacion">Denominación</label><br />
                <asp:TextBox ID="txtConcepto" runat="server" Width="350px" onKeyPress="javascript:if(event.keyCode==13){bMsg = false;buscarConcepto(this.value);event.keyCode=0;}" />
            </span>
            <span id="ambAp" class="texto" runat="server">
            <table id="tblApellidos" style="WIDTH:350px; margin-bottom:5px;">
                <tr>
                    <td>&nbsp;Apellido1</td>
                    <td>&nbsp;Apellido2</td>
                    <td>&nbsp;Nombre</td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="txtApellido1" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesionales();event.keyCode=0;}" MaxLength="50" /></td>
                    <td><asp:TextBox ID="txtApellido2" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesionales();event.keyCode=0;}" MaxLength="50" /></td>
                    <td><asp:TextBox ID="txtNombre" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesionales();event.keyCode=0;}" MaxLength="50" /></td>
                </tr>
            </table>
            </span>
		</td>
        <td colspan="2" style="vertical-align:bottom;">
            <asp:RadioButtonList ID="rdbTipo" SkinId="rbli" runat="server" RepeatColumns="2" ToolTip="Tipo de búsqueda" onclick="buscarConcepto($I('txtConcepto').value);" style="position:absolute; left: 370; top: 20px;">
	            <asp:ListItem Value="I"><img src='../../../../Images/imgIniciaCon.gif' border='0' title="Inicia con" style="cursor:pointer" hidefocus=hidefocus onclick="$I('rdbTipo_0').click();"></asp:ListItem>
	            <asp:ListItem Selected="True" Value="C"><img src='../../../../Images/imgContieneA.gif' border='0' title="Contiene" style="cursor:pointer" hidefocus=hidefocus onclick="$I('rdbTipo_1').click();"></asp:ListItem>
            </asp:RadioButtonList>
            <span id="ambBajas" class="texto" runat="server">
            <input type="checkbox" id="chkBajas" class="check" runat="server" />&nbsp;Mostrar bajas
            </span>
		</td>
    </tr>
    <tr>
        <td style="text-align:right;">
            <img src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos')" />&nbsp;
            <img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom; margin-right:23px;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos')" />
        </td>
        <td>&nbsp;</td>
        <td style="text-align:right;">
            <img src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos2')" />&nbsp;
            <img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom; margin-right:23px;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos2')" />
        </td>
    </tr>
    <tr>
        <td>
            <TABLE id="tblTitulo" style="width:350px; height:17px">
                <tr class="TBLINI">
                    <td style="padding-left:3px">Denominación
                        &nbsp;<IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="lupaSiguiente()" height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
                        <img style="cursor:pointer; display:none;" onclick="lupaDescripcion(event)" height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">					    
                    </td>                    
                </tr>
            </TABLE>
            <DIV id="divCatalogo" style="overflow:auto; overflow-x:hidden; width:366px; height:260px">
                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:350px;">
                <%=strTablaHTML%>
                </div>
            </DIV>
            <table style="width:350px; height: 17px">
                <tr class="TBLFIN">
                    <td></td>
                </tr>
            </table>
        </td>
        <td style="vertical-align:middle;">
            <asp:Image id="imgPapelera" style="cursor:pointer" runat="server" ImageUrl="../../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this)" caso="4"></asp:Image>
        </td>
        <td><!-- Items asignados -->
            <table id="tblAsignados" style="WIDTH: 350px; HEIGHT: 17px" >
                <tr class="TBLINI">
                    <td style="padding-left:3px">
                        Elementos seleccionados
                        &nbsp;<img id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos2',0,'divCatalogo','imgLupa2')" height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
                        <img style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos2',0,'divCatalogo','imgLupa2', event)" height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">					    
					</td>
                </tr>
            </table>
            <div id="divCatalogo2" style="OVERFLOW:auto; overflow-x:hidden; WIDTH: 366px; height:260px" target="true" onmouseover="setTarget(this);" caso="2">
                <div style="background-image:url(<%=Session["strServer"]%>Images/imgFT20.gif);width:350px">
                    <table id="tblDatos2" style="WIDTH: 350px;" class="texto MM">
                        <colgroup><col style="width:350px;" /></colgroup>
                    </table>
                </div>
            </div>
            <table style="WIDTH: 350px; HEIGHT: 17px">
                <tr class="TBLFIN">
                    <td></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="padding-top:3px;" colspan="3">
            <span id="ambIconosResp" class="texto" runat="server">
            <img class="ICO" src="../../../../images/imgResponsable.gif" />Responsable&nbsp;&nbsp;&nbsp;
            <img id="NoRespon" class="ICO" src="../../../../images/imgResponsable.gif" />No responsable
            </span>
        </td>
    </tr>
</table>
<script type="text/javascript">

    setOp($I("NoRespon"), 30);

</script>
<br />
<center>
<table style="width:300px; text-align:center;">
	<tr>
		<td>
			<button id="btnAceptar" type="button" onclick="aceptarAux();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
			    <img src="../../../../images/imgAceptar.gif" /><span>Aceptar</span>
			</button>								
		</td>
		<td>
			<button id="btnCancelar" type="button" onclick="cerrarVentana();" style="margin-left:20px;" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
			    <img src="../../../../images/imgCancelar.gif" /><span>Cancelar</span>
			</button>				
		</td>
	</tr>
</table>
</center>
<div class="clsDragWindow" id="DW" noWrap></div>
<input type="hidden" id="hdnErrores" value="<%=sErrores %>" />
<input type="hidden" id="hdnIdTipo" value="" runat="server"/>
<input type="hidden" id="hdnCaso" value="0" runat="server"/>
<input type="hidden" id="hdnTipoRecurso" value="" runat="server"/>
   <uc_mmoff:mmoff ID="mmoff1" runat="server" />
</form>
</body>
</html>
