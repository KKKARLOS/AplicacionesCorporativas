<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_PSP_CONCEP_ESTRUC_Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<HEAD runat="server">
	<title> ::: SUPER ::: - Selección de elementos de la estructura</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script language="JavaScript" src="../../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../../Javascript/draganddrop.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../../Javascript/boxover.js" type="text/Javascript"></script>
    <script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../../../../Javascript/modal.js" type="text/Javascript"></script>
</HEAD>
<body style="overflow: hidden" leftmargin="15" topMargin="15" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
    <form id="form1" runat="server">
	<script type="text/javascript">

        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";

	</script>
	<center>   
    <table style="width: 820px;margin-top:10px; margin-left:20px;text-align:left; ">
            <colgroup>
            <col style="width:390px;" />
            <col style="width:40px;" />
            <col style="width:390px;" />
        </colgroup>
        <tr height="45px">
            <td>Nivel de Estructura&nbsp;&nbsp;
 		    <asp:dropdownlist id="cboNivelEstru" runat="server" Width="200px" CssClass="combo" onChange="javascript:CargarNivelEstructura(this.value)">
            </asp:dropdownlist>
            </td>
            <td></td>
            <td>
            <asp:Label ID="lblMostrarInactivos" runat="server" Text="Mostrar inactivos" /> 
            <input type="checkbox" id="chkMostrarInactivos" class="check" onclick="CargarNivelEstructura(this.value);" />
            </td>
        </tr>
        <tr>
            <td><!-- Relación de Items -->
                <table id="tblCatIni" style="width: 350px; height: 17px">
                    <tr class="TBLINI">
                        <td style="padding-left:3px">
                            Denominación
  		                    <img style="display: none; cursor: pointer;" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa1',event)" height="11" src="../../../../../Images/imgLupa.gif" width="20" />
		                    <img style="display: none; cursor: pointer;" id="imgLupa1" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa1')" height="11" src="../../../../../Images/imgLupaMas.gif" width="20" tipolupa="2" />                      
					    </td>                    
                    </tr>
                </table>
                <div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 366px; height:300px">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:350px">
                    <table id="tblDatos" style="width: 350px;">
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
                <asp:Image id="imgPapelera" style="cursor: pointer" runat="server" ImageUrl="../../../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this)" caso="4"></asp:Image>
            </td>
            <td ><!-- Items asignados -->
                <table id="tblAsignados" style="width: 350px; height: 17px">
                    <tr class="TBLINI">
                        <td style="padding-left:3px">
                            Elementos seleccionados
  		                    <img style="display: none; cursor: pointer;" onclick="buscarDescripcion('tblDatos2',0,'divCatalogo','imgLupa2',event)" height="11" src="../../../../../Images/imgLupa.gif" width="20" />
		                    <img style="display: none; cursor: pointer;" id="imgLupa2" onclick="buscarSiguiente('tblDatos2',0,'divCatalogo','imgLupa2')" height="11" src="../../../../../Images/imgLupaMas.gif" width="20" tipolupa="2" />                            
						</td>
                    </tr>
                </table>
                <div id="divCatalogo2" style="overflow: auto; overflow-x: hidden; width: 366px; height:300px" target="true" onmouseover="setTarget(this)" caso="2">
                    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT16.gif); width:350px">
                        <table id="tblDatos2" style="width: 350px;" class="texto MM">
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
    </table><br />
    <table style="width:300px; margin-top:10px;">
		<tr>
			<td align="center">
                <button id="btnAceptar" type="button" onclick="aceptarAux();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../../../images/imgAceptar.gif" /><span>Aceptar</span>
                </button>
			</td>
			<td align="center">
                <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../../../images/imgCancelar.gif" /><span>Cancelar</span>
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
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
    </form>
</body>
</html>
