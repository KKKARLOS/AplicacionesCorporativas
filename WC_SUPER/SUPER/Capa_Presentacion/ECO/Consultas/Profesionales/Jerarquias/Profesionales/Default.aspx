<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_PSP_Conceptos_Profesionales_Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
	<title> ::: SUPER ::: - Selecci?n de profesionales</title>
	<meta http-equiv='X-UA-Compatible' content='IE=8' />
	<script language="JavaScript" src="../../../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../../../Javascript/draganddrop.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../../../Javascript/boxover.js" type="text/Javascript"></script>
    <script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body style="OVERFLOW: hidden; margin-left:15px; margin-top:15px;" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
<form id="form1" runat="server">
<script type="text/javascript">
    var intSession = <%=Session.Timeout%>; 
    var strServer = "<%=Session["strServer"]%>";
    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
    var nTipo = "<%=Request.QueryString["nTipo"]%>";
</script>
<br /><br />
<table style="margin-left:5px; width:1000px; text-align:left">
    <colgroup>
        <col style="width:570px;" />
        <col style="width:40px;" />
        <col style="width:390px;" />
    </colgroup>        
	<tr>
		<td>
            <table style="width: 550px;">
                <colgroup>
                    <col style="width:140px"/>
                    <col style="width:140px"/>
                    <col style="width:135px"/>
                    <col style="width:135px"/>
                </colgroup>
                <tr>
                    <td>&nbsp;Apellido1</td>
                    <td>&nbsp;Apellido2</td>
                    <td>&nbsp;Nombre</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="txtApellido1" runat="server" style="width:110px"  onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos();event.keyCode=0;}" MaxLength="50" /></td>
                    <td><asp:TextBox ID="txtApellido2" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos();event.keyCode=0;}" MaxLength="50" /></td>
                    <td><asp:TextBox ID="txtNombre" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos();event.keyCode=0;}" MaxLength="50" />
                    </td>
				    <td style="vertical-align:bottom;">
				        <input id="chkBajas" hidefocus class="check" type="checkbox" runat="server" style="margin-left:5px;" onclick="javascript:mostrarRelacionTecnicos();">&nbsp;Mostrar bajas<img src="../../../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;margin-left:9px" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos')" />&nbsp;<img src="../../../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selecci?n a todas las filas" onclick="DesmarcarTodo('tblDatos')" />
				    </td>                    
                </tr>
            </table>
		</td>
		<td></td>
		<td style="vertical-align:bottom; text-align:right">
           <img src="../../../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos2')" />&nbsp;
           <img src="../../../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selecci?n a todas las filas" onclick="DesmarcarTodo('tblDatos2')" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;	
		</td>
	</tr>	
    <tr>
        <td><!-- Relaci?n de Items -->
            <table id="tblCatIni" style="width: 550px; height: 17px">
                <colgroup>
                    <col style="width:290px"/>
                    <col style="width:260px"/>
                </colgroup>
                <tr class="TBLINI">
                    <td style="padding-left:3px;" >
                        Profesional
	                    <img style="DISPLAY: none; CURSOR: pointer;" id="imgLupa1" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa1')" height="11" src="../../../../../../Images/imgLupaMas.gif" width="20" tipolupa="2" />                      
	                    <img style="DISPLAY: none; CURSOR: pointer;" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa1',event)" height="11" src="../../../../../../Images/imgLupa.gif" width="20" />
				    </td> 
                    <td>
                        <label id="lblNodo" runat="server">Nodo</label>                            
	                    <img style="DISPLAY: none; CURSOR: pointer;" id="imgLupa3" onclick="buscarSiguiente('tblDatos',2,'divCatalogo','imgLupa3')" height="11" src="../../../../../../Images/imgLupaMas.gif" width="20" tipolupa="2" />                      
	                    <img style="DISPLAY: none; CURSOR: pointer;" onclick="buscarDescripcion('tblDatos',2,'divCatalogo','imgLupa3',event)" height="11" src="../../../../../../Images/imgLupa.gif" width="20" />
				    </td>  					                       
                </tr>
            </table>
            <div id="divCatalogo" style="overflow: auto; width: 566px; height:300px" runat="server" onscroll="scrollTabla()">
                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:550px">
                <table id="tblDatos" style="width: 550px;">
                    <colgroup><col style='width:20px' /><col style='width:265px;' /><col style='width:265px;' /></colgroup>
                </table>
                </div>
            </div>
            <table style="width: 550px; height: 17px">
                <tr class="TBLFIN">
                    <td></td>
                </tr>
            </table>              
        </td>
        <td style="vertical-align:middle;">
            <asp:Image id="imgPapelera" style="CURSOR:pointer" runat="server" ImageUrl="../../../../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="4"></asp:Image>
        </td>
        <td><!-- Items asignados -->
            <table id="tblAsignados" style="width:350px; height:17px">
                <TR class="TBLINI">
                    <td style="padding-left:3px">
                        Profesionales seleccionados
	                    <img style="DISPLAY: none; CURSOR: pointer;" id="imgLupa2" onclick="buscarSiguiente('tblDatos2',1,'divCatalogo2','imgLupa2')" height="11" src="../../../../../../Images/imgLupaMas.gif" width="20" tipolupa="2" />                            
	                    <img style="DISPLAY: none; CURSOR: pointer;" onclick="buscarDescripcion('tblDatos2',1,'divCatalogo2','imgLupa2', event)" height="11" src="../../../../../../Images/imgLupa.gif" width="20" />
					</TD>
                </TR>
            </table>
            <div id="divCatalogo2" style="OVERFLOW: auto; width: 366px; height:300px; " target="true" onmouseover="setTarget(this);" caso="2" onscroll="scrollTablaAsig()">
                <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:350px">
                <table id="tblDatos2" style="WIDTH: 350px;">
                    <colgroup><col style="width:20px;" /><col style="width:330px;" /><col style="width:0px;" /></colgroup>
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
        <td colspan="3">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <img class="ICO" src="../../../../../../Images/imgUsuIVM.gif" />&nbsp;&nbsp;&nbsp;Empleado interno&nbsp;&nbsp;&nbsp;
            <img class="ICO" src="../../../../../../Images/imgUsuEVM.gif" />&nbsp;Colaborador externo
            <img id="imgForaneo" src="../../../../../../Images/imgUsuFVM.gif" runat="server" />
            <label id="lblForaneo" runat="server">For?neo</label>
        </td>
    </tr>                    
</table>
<br />
<center>
<table style="width:300px; margin-top:10px; text-align:center;">
	<tr>
		<td>
            <button id="btnAceptar" type="button" onclick="aceptarAux();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../../../../images/imgAceptar.gif" /><span>Aceptar</span>
            </button>
		</td>
		<td>
            <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../../../../images/imgCancelar.gif" /><span>Cancelar</span>
            </button>
		</td>
	</tr>
</table>
</center>


<div class="clsDragWindow" id="DW" noWrap></div>
<input type="hidden" id="hdnErrores" value="<%=sErrores %>" />
<input type="hidden" id="hdnNodoActual" value="" runat="server"/>
<input type="hidden" id="hdnDesNodoActual" value="" runat="server"/>
<input type="hidden" id="hdnIdNodo" value="" runat="server"/>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</form>
</body>
</html>
