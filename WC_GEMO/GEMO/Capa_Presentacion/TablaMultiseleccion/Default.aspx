<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 	"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head  runat="server">
<base target="_self" />
	<title>Selección de criterio</title>
	<meta http-equiv='X-UA-Compatible' content='IE=8' />
	<script language="JavaScript" src="../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../Javascript/draganddrop.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../Javascript/boxover.js" type="text/Javascript"></script>
    <script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body style="OVERFLOW: hidden" leftMargin="15" topMargin="15" onload="init()">
    <ucproc:Procesando ID="Procesando" runat="server" />
    <form id="form1" runat="server">
	<script type="text/javascript">
	<!--
        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";
	    var nTipo = "<%=Request.QueryString["nT"]%>";
	-->
    </script>
    <center>
    <table align="center" border="0" width="100%" style="margin-left:10px;text-align:left;margin-top:20px" cellpadding="5" cellspacing="0">
        <colgroup>
            <col style="width:47%" />
            <col style="width:6%" />
            <col style="width:47%" />
        </colgroup>
        <tr style="height:40px;">
            <td>
                <span id="ambDenominacion" class="texto" runat="server">
                    &nbsp;Denominación<br />
	                <asp:TextBox ID="txtConcepto" runat="server" width="350px" onKeyPress="javascript:if(event.keyCode==13){bMsg = false;buscarConcepto(this.value);event.keyCode=0;}" />
                </span>
                <span id="ambAp" class="texto" runat="server">
                <table id="tblApellidos" style="width: 350px;margin-bottom:5px;">
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
            <td colspan="2" valign=bottom>
	            <asp:RadioButtonList ID="rdbTipo" CssClass="texto" runat="server" RepeatColumns="2" ToolTip="Tipo de búsqueda" onclick="buscarConcepto($I('txtConcepto').value);" style="position:absolute; left: 370; top: 20px;">
		            <asp:ListItem Value="I"><img src='../../Images/imgIniciaCon.gif' border='0' title="Inicia con" style="cursor:hand" hidefocus=hidefocus onclick="$I('rdbTipo_0').click();"></asp:ListItem>
		            <asp:ListItem Selected="True" Value="C"><img src='../../Images/imgContieneA.gif' border='0' title="Contiene" style="cursor:hand" hidefocus=hidefocus onclick="$I('rdbTipo_1').click();"></asp:ListItem>
	            </asp:RadioButtonList>
	            <span id="ambBajas" class="texto" runat="server">
	            <input type=checkbox id="chkBajas" class="check" runat="server" />&nbsp;Mostrar bajas
	            </span>
			</td>
        </tr>
        <tr>
            <td align=right><img src="../../Images/imgMarcarTodo.gif" style="cursor:hand; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos')" />&nbsp;<img src="../../Images/imgDesmarcarTodo.gif"  style="cursor:hand; vertical-align:bottom; margin-right:35px;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos')" /></td>
            <td>&nbsp;</td>
            <td align=right><img src="../../Images/imgMarcarTodo.gif" style="cursor:hand; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos2')" />&nbsp;<img src="../../Images/imgDesmarcarTodo.gif"  style="cursor:hand; vertical-align:bottom; margin-right:35px;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos2')" /></td>
        </tr>
        <tr>
            <td>
                <table id="tblTitulo" style="width: 350px; height: 17px">
                    <TR class="TBLINI">
                        <td style="padding-left:3px">Denominación
                            &nbsp;<IMG id="imgLupa1" style="DISPLAY: none; CURSOR: hand" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa1')" height="11" src="../../Images/imgLupaMas.gif" width="20" tipolupa="2">
                            <IMG style="CURSOR: hand; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa1',event)" height="11" src="../../Images/imgLupa.gif" width="20" tipolupa="1">					    
                        </TD>                    
                    </TR>
                </TABLE>
                <DIV id="divCatalogo" style="overflow-x: hidden; overflow-y: auto; width: 366px; height:240px">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:350px">
                    <%=strTablaHTML%>
                    </div>
                </DIV>
                <table style="width: 350px; height: 17px">
                    <TR class="TBLFIN">
                        <TD></TD>
                    </TR>
                </TABLE>
            </td>
            <td valign="middle">
                <asp:Image id="imgPapelera" style="CURSOR: hand" runat="server" ImageUrl="../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="4"></asp:Image>
            </td>
            <td><!-- Items asignados -->
                <table id="tblAsignados" style="width: 350px; height: 17px">
                    <TR class="TBLINI">
                        <td style="padding-left:3px">
                            Elementos seleccionados
                            &nbsp;<IMG id="imgLupa2" style="DISPLAY: none; CURSOR: hand" onclick="buscarSiguiente('tblDatos2',0,'divCatalogo','imgLupa2')" height="11" src="../../Images/imgLupaMas.gif" width="20" tipolupa="2">
                            <IMG style="CURSOR: hand; DISPLAY: none;" onclick="buscarDescripcion('tblDatos2',0,'divCatalogo','imgLupa2',event)" height="11" src="../../Images/imgLupa.gif" width="20" tipolupa="1">					    
						</TD>
                    </TR>
                </TABLE>
                <DIV id="divCatalogo2" style="overflow-x: hidden; overflow-y: auto; width: 366px; height:240px" target="true" onmouseover="setTarget(this);" caso="2">
                    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:350px">
                    <table id="tblDatos2" style="width: 350px;" class="texto MM">
                        <colgroup><col style="width:350px; padding-left:3px;" /></colgroup>
                    </TABLE>
                    </div>
                </DIV>
                <table style="width: 350px; height: 17px">
                    <TR class="TBLFIN">
                        <TD></TD>
                    </TR>
                </TABLE>
            </td>
        </tr>
        <tr>
            <td style="padding-top:3px;" colspan="3">
                <span id="ambIconosResp" class="texto" runat="server">
<%--                <img class="ICO" src="../../images/imgResponsable.gif" />Responsable&nbsp;&nbsp;&nbsp;
                <img class="ICO" src="../../images/imgResponsable.gif" style="filter:progid:DXImageTransform.Microsoft.Alpha(opacity=30)" />No responsable
--%>                </span>
            </td>
        </tr>
    </table><br />
	    <table width="300px" align=center style="margin-top:5px;">
		    <tr>
			    <td align="center">
				    <button id="btnAceptar" type="button" onclick="aceptarAux()" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
					    <img src="../../images/imgAceptar.gif" /><span title="Aceptar">&nbsp;&nbsp;Aceptar</span>
				    </button>    
			    </td>	

			    <td align=center>
				    <button id="btnCancelar" type="button" onclick="cerrarVentana()" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
					    <img src="../../images/imgCancelar.gif" /><span title="Cancelar">&nbsp;Cancelar</span>
				    </button>    
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
