<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_PSP_CONCEP_ESTRUC_Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
	<title> ::: SUPER ::: - Selección de figuras</title>
	<meta http-equiv='X-UA-Compatible' content='IE=8' />
	<script language="JavaScript" src="../../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../../Javascript/draganddrop.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../../Javascript/boxover.js" type="text/Javascript"></script>
    <script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../../../../Javascript/modal.js" type="text/Javascript"></script>
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
	<center>
    <table style="margin-left:10px; text-align:left; width:100%;" cellpadding="5">
        <colgroup><col style="width:47%;"/><col style="width:6%;"/><col style="width:47%;"/></colgroup>
        <tr style="height:45px;">
            <td>Tipo de ítem&nbsp;&nbsp;
                <asp:DropDownList id="cboTipoItem" runat="server" Width="150px" onChange="setFigura(this.value)" AppendDataBoundItems=true>
                </asp:DropDownList>                
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td><!-- Relación de Items -->
                <table id="tblCatIni" style="width: 350px; height: 17px">
                    <TR class="TBLINI">
                        <td style="padding-left:3px">
                        Figuras
  		                <img style="DISPLAY: none; CURSOR: pointer;" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa1',event)" height="11" src="../../../../../Images/imgLupa.gif" width="20" />
		                <img style="DISPLAY: none; CURSOR: pointer;" id="imgLupa1" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa1')" height="11" src="../../../../../Images/ imgLupaMas.gif" width="20" tipolupa="2" />                      
					    </TD>                    
                    </TR>
                </table>
                <DIV id="divCatalogo" style="overflow: auto; width: 366px; height:300px">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:350px">
                    <table id="tblDatos" style="WIDTH: 350px;" class='texto MAM'>
                        <colgroup><col style="width:350px;" /></colgroup>
                    </table>
                    </div>
                </DIV>
                <table style="width: 350px; height: 17px">
                    <TR class="TBLFIN">
                        <TD></TD>
                    </TR>
                </table>
            </td>
            <td style="vertical-align:middle;">
                <asp:Image id="imgPapelera" style="CURSOR: pointer" runat="server" ImageUrl="../../../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="4"></asp:Image>
            </td>
            <td><!-- Items asignados -->
                <table id="tblAsignados" style="WIDTH: 350px; BORDER-COLLAPSE: collapse; HEIGHT: 17px" cellspacing="0" border="0">
                    <TR class="TBLINI">
                        <td style="padding-left:3px">
                            Figuras seleccionadas
  		                    <img style="DISPLAY: none; CURSOR: pointer;" onclick="buscarDescripcion('tblDatos2',0,'divCatalogo','imgLupa2',event)" height="11" src="../../../../../Images/imgLupa.gif" width="20" />
		                    <img style="DISPLAY: none; CURSOR: pointer;" id="imgLupa2" onclick="buscarSiguiente('tblDatos2',0,'divCatalogo','imgLupa2')" height="11" src="../../../../../Images/imgLupaMas.gif" width="20" tipolupa="2" />                            
						</TD>
                    </TR>
                </table>
                <div id="divCatalogo2" style="OVERFLOW: auto; overflow-x:hidden; width: 366px; height:300px" target="true" onmouseover="setTarget(this);" caso="2">
                    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT16.gif); width:350px">
                    <table id="tblDatos2" style="WIDTH: 350px;" class="texto MM">
                        <colgroup><col style="width:350px;" /></colgroup>
                    </table>
                    </div>
                </div>
                <table style="WIDTH: 350px; HEIGHT: 17px">
                    <TR class="TBLFIN">
                        <TD></TD>
                    </TR>
                </table>
            </td>
        </tr>
    </table><br />
	<table width="300px" style="margin-top:15px;">
		<tr>
			<td>
				<button id="btnAceptar" type="button" onclick="aceptarAux();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../../../images/imgAceptar.gif" /><span title="Aceptar">Aceptar</span></button>								
			</td>
			<td>
				<button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../../../images/imgCancelar.gif" /><span title="Cancelar">Cancelar</span></button>				
			</td>
		</tr>
	</table>
</center>
<DIV class="clsDragWindow" id="DW" noWrap></DIV>
<input type="hidden" id="hdnErrores" value="<%=sErrores %>" />
<input type="hidden" id="hdnNodoActual" value="" runat="server"/>
<input type="hidden" id="hdnDesNodoActual" value="" runat="server"/>
<input type="hidden" id="hdnCualidad" value="" runat="server"/>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
    </form>
</body>
</html>
