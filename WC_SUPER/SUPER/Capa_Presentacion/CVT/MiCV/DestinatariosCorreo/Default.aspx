<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_PSP_Conceptos_Profesionales_Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title> ::: SUPER ::: - Destinatarios del correo</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/draganddrop.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/boxover.js" type="text/Javascript"></script>
    <script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body style="OVERFLOW: hidden" leftmargin="15" topmargin="15" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
    <form id="form1" runat="server">
	<script type="text/javascript">
        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";
	</script>
	<center>
    <table style="width:1000px; text-align:left;" cellpadding="5">
        <colgroup>
            <col style="width:475px;" />
            <col style="width:50px;" />
            <col style="width:475px;" />
        </colgroup>
		<tr style="height:60px">
			<td>			
                <table style="width: 450px;">
                    <colgroup>
                        <col style="width:120px;" />
                        <col style="width:120px;" />
                        <col style="width:210px;" />
                    </colgroup>               
                    <tr>
                    <td>&nbsp;Apellido1</td>
                    <td>&nbsp;Apellido2</td>
                    <td>&nbsp;Nombre</td>
                    </tr>
                    <tr>
                    <td><asp:TextBox ID="txtApellido1" runat="server" style="width:100px"  onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos();event.keyCode=0;}" MaxLength="50" /></td>
                    <td><asp:TextBox ID="txtApellido2" runat="server" style="width:100px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos();event.keyCode=0;}" MaxLength="50" /></td>
                    <td><asp:TextBox ID="txtNombre" runat="server" style="width:100px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos();event.keyCode=0;}" MaxLength="50" />
                    </td>
                    </tr>
                </table>                
			</td>
			<td></td>
			<td></td>
		</tr>	
        <tr>
            <td style=" padding-bottom:2px; vertical-align:bottom; text-align:right;">
            <img src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos')" />&nbsp;<img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom; margin-right:15px;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos')" />
            </td>
            <td></td>
            <td style=" padding-bottom:2px; vertical-align:bottom;">
            <img src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom; margin-left:415px " title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos2')" />&nbsp;<img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom; margin-right:15px;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos2')" />
            </td>
        </tr>
        <tr>
            <td><!-- Relación de Items -->
                <table id="tblCatIni" style="width: 450px; height: 17px">
                    <tr class="TBLINI">
                        <td style="padding-left:3px">
                        Profesional
  		                <img style="DISPLAY: none; CURSOR: pointer;" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa1',event)" height="11" src="../../../../Images/imgLupa.gif" width="20" />
		                <img style="DISPLAY: none; CURSOR: pointer;" id="imgLupa1" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa1')" height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2" />                      
					    </td>                    
                    </tr>
                </table>
                <div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 466px; height:300px" runat="server" onscroll="scrollTabla()">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:450px">
                    <table id="tblDatos" style="width:450px;">
                        <colgroup><col style="width:20px;" /><col style="width:430px;" /></colgroup>
                    </table>
                    </div>
                </div>
                <table style="width: 450px; height: 17px">
                    <tr class="TBLFIN">
                        <td></td>
                    </tr>
                </table>              
            </td>
            <td style="vertical-align:middle; text-align:center;">
                <asp:Image id="imgPapelera" style="CURSOR: pointer" runat="server" ImageUrl="../../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="4"></asp:Image>
            </td>
            <td><!-- Items asignados -->
                <table id="tblAsignados" style="width: 450px; height: 17px">
                    <tr class="TBLINI">
                        <td style="padding-left:3px">
                            Destinatarios
  		                    <img style="DISPLAY: none; CURSOR: pointer;" onclick="buscarDescripcion('tblDatos2',1,'divCatalogo','imgLupa2',event)" height="11" src="../../../../Images/imgLupa.gif" width="20" />
		                    <img style="DISPLAY: none; CURSOR: pointer;" id="imgLupa2" onclick="buscarSiguiente('tblDatos2',1,'divCatalogo','imgLupa2')" height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2" />                            
						</td>
                    </tr>
                </table>
                <div id="divCatalogo2" style="overflow: auto; overflow-x: hidden; width: 466px; height:300px" target="true" onmouseover="setTarget(this)" caso="2" onscroll="scrollTablaAsig()">
                    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:450px">
                    <table id="tblDatos2" style="width: 450px;" class="texto MM" >
                        <colgroup><col style="width:20px;" /><col style="width:430px;" /></colgroup>
                    </table>
                    </div>
                </div>
                <table style="width: 450px; height: 17px">
                    <tr class="TBLFIN">
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <table style="witdh: 500px;text-align:left">
                    <tr>
                        <td>
                            &nbsp;<img class="ICO" src="../../../../Images/imgUsuIVM.gif" />&nbsp;&nbsp;&nbsp;Empleado interno&nbsp;&nbsp;&nbsp;
                            <img class="ICO" src="../../../../Images/imgUsuEVM.gif" />&nbsp;Colaborador externo&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>                    
                </table>         
            </td>
        </tr>
    </table>
    <br />
    <table style="width:300px; margin-top:10px;">
		<tr>
			<td align="center">
                <button id="btnAceptar" type="button" onclick="aceptarAux();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../../images/imgAceptar.gif" /><span>Aceptar</span>
                </button>
			</td>
			<td align="center">
                <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
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
    <input type="hidden" id="hdnIdNodo" value="" runat="server"/>
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    </form>
</body>
</html>
