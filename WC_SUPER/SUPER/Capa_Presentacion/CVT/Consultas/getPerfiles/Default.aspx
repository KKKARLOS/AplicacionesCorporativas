<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_CVT_Consultas_getPerfiles_Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="HEAD1" runat="server">
	<title>Selección de criterio</title>
	<meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<link href="../../../../App_Themes/Corporativo/jquery-ui-1.8.17.custom.css" type="text/css" rel="stylesheet" />
	<link href="../../../../App_Themes/Corporativo/Corporativo.css" type="text/css" rel="stylesheet" />
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/jquery-1.7.1/jquery-1.7.1.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/draganddrop.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/boxover.js" type="text/Javascript"></script>
    <script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body style="overflow:hidden; margin-left:5px; margin-top:5px;" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
    <form id="form1" runat="server">
	<script type="text/javascript">
        var intSession = "<%=Session.Timeout%>"; 
	    var strServer = "<%=Session["strServer"]%>";
	    var nTipo = "<%=Request.QueryString["nTipo"]%>";
    </script>

    <table style="margin-left:10px; table-layout:fixed; width:920px;" cellpadding="0" cellspacing="0" border="0">
        <colgroup>
            <col style="width:380px;" />
            <col style="width:40px;" />
            <col style="width:510px;" />
        </colgroup>
        <tr style="height:40px;">
            <td>
                <span id="ambDenominacion" class="texto" runat="server">
                    &nbsp;Denominación<br />
	                <asp:TextBox ID="txtConcepto" runat="server" Width="350px" onkeypress="javascript:if(event.keyCode==13){bMsg = false;buscarConcepto(this.value);event.keyCode=0;return false;}" />
                </span>
			</td>
            <td colspan="2" style="vertical-align:bottom;">
	            <asp:RadioButtonList ID="rdbTipo" SkinId="rbl" runat="server" RepeatColumns="2" ToolTip="Tipo de búsqueda" onclick="buscarConcepto($I('txtConcepto').value);" >
		            <asp:ListItem Value="I"><img src='../../../../Images/imgIniciaCon.gif' border='0' title="Inicia con" style="cursor:pointer" onclick="$I('rdbTipo_0').click();"></asp:ListItem>
		            <asp:ListItem Selected="True" Value="C"><img src='../../../../Images/imgContieneA.gif' border='0' title="Contiene" style="cursor:pointer" onclick="$I('rdbTipo_1').click();"></asp:ListItem>
	            </asp:RadioButtonList>
			</td>
        </tr>
        <tr>
            <td style="text-align:right; height:17px;">
                <img id="imgMarcarP1" src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos')" />
                &nbsp;
                <img id="imgDesMarcarP1" src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom; margin-right:30px;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos')" />
            </td>
            <td>&nbsp;</td>
            <td>
                <label id="lblTexto1" style="width:440px; "></label>
                <img src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos2')" />
                &nbsp;<img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos2')" />
            </td>
        </tr>
        <tr>
            <td style="vertical-align:top;">
                <table id="tblTitulo" style="width:350px; height:17px">
                    <tr class="TBLINI">
                        <td style="padding-left:3px"><label id="lblCab1">Perfiles seleccionables</label> 
                            &nbsp;<IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa1')" height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
                            <img style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa1', event)" height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">					    
                        </td>                    
                    </tr>
                </table>
                <div id="divCatalogo" style="OVERFLOW: auto; width:366px; height:200px">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif');width:350px;">
                    <%=strTablaHTML%>
                    </div>
                </div>
                <table style="width:350px; height: 17px">
                    <tr class="TBLFIN">
                        <td></td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align:middle;"></td>
            <td rowspan="5" style="vertical-align:top;"><!-- Items asignados -->
                <table id="tblAsignados" style="width:480px; HEIGHT: 17px" >
                    <tr class="TBLINI">
                        <td style="padding-left:3px">
                            <label id="lblCab2">Perfiles / Familias seleccionados</label>
                            &nbsp;<img id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos2',1,'divCatalogo2','imgLupa2')" height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
                            <img style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos2',1,'divCatalogo2','imgLupa2', event)" height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">					    
						</td>
                    </tr>
                </table>
                <div id="divCatalogo2" style="overflow:auto; width:496px; height:200px;" target="true" onmouseover="setTarget(this);" caso="2">
                    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif);width:480px">
                        <table id="tblDatos2" style="width:480px;" class="texto MM" cellpadding="0" cellspacing="0">
                            <colgroup><col style="width:20px;" /><col style="width:430px;" /><col style="width:30px;" /></colgroup>
                        </table>
                    </div>
                </div>
                <table style="width:480px; HEIGHT: 17px">
                    <tr class="TBLFIN">
                        <td></td>
                    </tr>
                </table>
                <img id="imgDetalle" src='../../../../Images/imgDetVacio.png' style="margin-left:227px; margin-top:2px;" />
                <label id="lblCab5" style="margin-left:5px; vertical-align:top; margin-top:2px;">Acceso a la asignación de entornos al perfil</label>
                <br />
                <div id="capaEntornos" style="visibility:hidden; height:1px;">
                    <table id="tblTituloEntornos" style="width:480px; height:17px; margin-top:35px;" >
                        <colgroup><col style="width:400px;" /><col style="width:80px;" /></colgroup>
                        <tr class="TBLINI">
                            <td style="padding-left:3px;">
                                <label id="lblCab4">Entornos del perfil / familia seleccionado</label>
                                &nbsp;<img id="imgLupa4" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblEntornos',0,'divEntornos','imgLupa4')" height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
                                <img style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblEntornos',0,'divEntornos','imgLupa4', event)" height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">					    
						    </td>
                            <td style="text-align:right;" title="Obligatorio">Obl.</td>
                        </tr>
                    </table>
                    <div id="divEntornos" style="OVERFLOW: auto; width:496px; height:176px"  >
                        <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT16.gif);width:480px">
                            <table id="tblEntornos" style="width:480px;" class="texto" cellpadding="0" cellspacing="0">
                                <colgroup><col style="width:460px;" /><col style="width:20px;" /></colgroup>
                            </table>
                        </div>
                    </div>
                    <table style="width:480px; height:17px">
                        <tr class="TBLFIN">
                            <td></td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td></td>
            <td style="vertical-align:middle;">
                <asp:Image id="imgPapelera" style="CURSOR: pointer" runat="server" ImageUrl="../../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this)" caso="4"></asp:Image>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td style="height:17px; text-align:right;">
                <img src="../../../../Images/imgAddDel.png" style="cursor:pointer;margin-right:110px; " title="Gestión de familias" onclick="mtoFamilias()" />
                <asp:CheckBox ID="chkSoloPriv" runat="server" style="cursor:pointer; margin-right:50px;" onclick="mostrarFamilias();" Checked="true" Text="Solo familias privadas" TextAlign="Left" ToolTip="Indica si se desea mostrar únicamente las familias privadas o las privadas + las públicas" />
                <img id="imgMarcarF1" src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; " title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatosFam')" />
                &nbsp;
                <img id="imgDesMarcarF1" src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; margin-right:30px;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatosFam')" />
            </td>
            <td colspan="2">
                <label id="lblTexto2" style="width:500px; margin-left:40px;"></label>
            </td>
        </tr>
        <tr>
            <td>
                <table id="tblTituloFam" style="WIDTH: 350px;HEIGHT: 17px">
                    <tr class="TBLINI">
                        <td style="padding-left:3px">
                            <label id="lblCab3">Familias de perfiles seleccionables</label>
                            &nbsp;<IMG id="imgLupa3" style="display:none; cursor:pointer" onclick="buscarSiguiente('tblDatosFam',1,'divCatalogoFam','imgLupa3')" height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
                            <img style="cursor: pointer; display:none;" onclick="buscarDescripcion('tblDatosFam',1,'divCatalogoFam','imgLupa3', event)" height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">					    
                        </td>                    
                    </tr>
                </table>
                <div id="divCatalogoFam" style="OVERFLOW: auto; WIDTH: 366px; height:176px">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif');width:350px;">
                    <%=strTablaHTMLFam%>
                    </div>
                </div>
                <table style="width:350px; HEIGHT: 17px">
                    <tr class="TBLFIN">
                        <td></td>
                    </tr>
                </table>
            </td>
            <td></td>
        </tr>
        <tr>
            <td colspan="2" style="padding-left:5px;">
                <img src='../../../../Images/imgGF.gif' style="margin-top:3px;" alt='Familia pública' />&nbsp;Familia pública
            </td>
        </tr>
    </table>
    <center>
    <table style="width:300px; margin-top:10px; margin-left:50px;">
		<tr>
			<td>
				<button id="btnAceptar" type="button" onclick="aceptarAux();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../../images/imgAceptar.gif" /><span>Aceptar</span></button>								
			</td>
			<td>
				<button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../../images/imgCancelar.gif" /><span>Cancelar</span></button>				
			</td>
		</tr>
    </table>
    <div id="divPerFamGen" title="Componentes de la familia" style="display:none; width:440px; height:400px;">
        <table id="Table1" style="width:400px; height:17px; text-align:left; margin-top:10px;">
            <tr class="TBLINI">
                <td style="padding-left:3px">Denominación
                </td>                    
            </tr>
        </table>
        <div id="divPerFam" style="OVERFLOW:auto; width:416px; height:240px">
            <div id="divPerFam2" style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif');width:400px;">
            <%=strTablaHTML%>
            </div>
        </div>
        <table style="width:400px; height:17px">
            <tr class="TBLFIN">
                <td></td>
            </tr>
        </table>
    </div>
    </center>
    <div class="clsDragWindow" id="DW" noWrap></div>
    <input type="hidden" id="hdnErrores" value="<%=sErrores %>" />
    <input type="hidden" id="hdnIdTipo" value="" runat="server"/>
    <input type="hidden" id="hdnCaso" value="0" runat="server"/>
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    </form>
</body>
</html>
