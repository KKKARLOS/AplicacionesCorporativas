<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_ECO_Consultas_getCriterioTabla2_Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Selección de criterio</title>
	<meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<link href="../../../../App_Themes/Corporativo/Corporativo.css" type="text/css" rel="stylesheet" />
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/draganddrop.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/boxover.js" type="text/Javascript"></script>
    <script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body style="overflow:hidden; margin-top:10px;" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
    <form id="form1" runat="server">
	<script type="text/javascript">
        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";
	    var nTipo = "<%=Request.QueryString["nTipo"]%>";
    </script>

    <table style="margin-left:10px; table-layout:fixed; width:100%;" cellpadding="2" cellspacing="0" border="0">
        <colgroup>
            <col style="width:45%" />
            <col style="width:6%" />
            <col style="width:49%" />
        </colgroup>
        <tr style="height:40px;">
            <td>
                <span id="ambDenominacion" class="texto" runat="server">
                    &nbsp;Denominación<br />
	                <asp:TextBox ID="txtConcepto" runat="server" Width="350px" onKeyPress="javascript:if(event.keyCode==13){bMsg = false;buscarConcepto(this.value);event.keyCode=0;return false;}" />
                </span>
			</td>
            <td style="vertical-align:bottom;">
	            <asp:RadioButtonList ID="rdbTipo" SkinId="rbl" runat="server" RepeatColumns="2" ToolTip="Tipo de búsqueda" onclick="buscarConcepto($I('txtConcepto').value);" style="position:absolute; left:230px; top:5px;">
		            <asp:ListItem Value="I"><img src='../../../../Images/imgIniciaCon.gif' border='0' title="Inicia con" style="cursor:pointer" onclick="$I('rdbTipo_0').click();"></asp:ListItem>
		            <asp:ListItem Selected="True" Value="C"><img src='../../../../Images/imgContieneA.gif' border='0' title="Contiene" style="cursor:pointer" onclick="$I('rdbTipo_1').click();"></asp:ListItem>
	            </asp:RadioButtonList>
            </td>
            <td>
                <div id="divTexto1" runat="server"></div>
			</td>
        </tr>
        <tr>
            <td style="text-align:right;"><img src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos')" />&nbsp;<img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom; margin-right:35px;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos')" /></td>
            <td>&nbsp;</td>
            <td style="text-align:right;"><img src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos2')" />&nbsp;<img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom; margin-right:50px;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos2')" /></td>
        </tr>
        <tr>
            <td>
                <table id="tblTitulo" style="WIDTH: 350px;HEIGHT: 17px">
                    <tr class="TBLINI">
                        <td style="padding-left:3px">Denominación
                            &nbsp;<img id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa1')" height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
                            <img style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa1', event)" height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">					    
                        </td>                    
                    </tr>
                </table>
                <div id="divCatalogo" style="OVERFLOW: auto; WIDTH: 366px; height:260px">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif');width: 350px;">
                    <%=strTablaHTML%>
                    </div>
                </div>
                <table style="WIDTH: 350px; HEIGHT: 17px">
                    <tr class="TBLFIN">
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align:middle;">
                <asp:Image id="imgPapelera" style="CURSOR: pointer" runat="server" ImageUrl="../../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this)" caso="4"></asp:Image>
            </td>
            <td><!-- Items asignados -->
                <table id="tblAsignados" style="WIDTH: 370px; HEIGHT: 17px" >
                    <tr class="TBLINI">
                        <td style="padding-left:3px">
                            Elementos seleccionados
                            &nbsp;<img id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos2',0,'divCatalogo','imgLupa2')" height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
                            <img style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos2',0,'divCatalogo','imgLupa2', event)" height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">					    
						</td>
                        <td style="text-align:right;">Obligatorio</td>
                    </tr>
                </table>
                <div id="divCatalogo2" style="OVERFLOW: auto; WIDTH: 386px; height:260px" target="true" onmouseover="setTarget(this);" caso="3">
                    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif);width:370px">
                        <table id="tblDatos2" style="WIDTH: 370px;" class="texto MM" border="0" cellpadding="0">
                            <colgroup><col style="width:350px;" /><col style="width:20px;" /></colgroup>
                        </table>
                    </div>
                </div>
                <table style="width:370px; height:17px">
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
                <label id="lblObl" style="width:370px;"">
                </label>
            </td>
        </tr>
    </table>
    <center>
    <table style="width:300px; margin-left:20px; margin-top:10px;">
		<tr>
			<td>
				<button id="btnAceptar" type="button" onclick="aceptarAux();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../../images/imgAceptar.gif" /><span>Aceptar</span></button>								
			</td>
			<td>
				<button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../../images/imgCancelar.gif" /><span>Cancelar</span></button>				
			</td>
		</tr>
    </table>
    </center>
    <DIV class="clsDragWindow" id="DW" noWrap></DIV>
    <input type="hidden" id="hdnErrores" value="<%=sErrores %>" />
    <input type="hidden" id="hdnIdTipo" value="" runat="server"/>
    <input type="hidden" id="hdnCaso" value="0" runat="server"/>
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    </form>
</body>
</html>
