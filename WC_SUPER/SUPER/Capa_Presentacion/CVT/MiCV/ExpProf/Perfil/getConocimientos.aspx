<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getConocimientos.aspx.cs" Inherits="Capa_Presentacion_CVT_MiCV_ExpProf_Perfil_getConocimientos" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Areas de conocimiento <%=TipoCono %>&nbsp;&nbsp;&nbsp;</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <link rel="stylesheet" href="../../../../../App_Themes/Corporativo/Corporativo.css" type="text/css" />
    <link rel="stylesheet" href="../../../../../PopCalendar/CSS/Classic.css" type="text/css" />
	<script language="JavaScript" src="../../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>	
    <script language="JavaScript" src="../../../../../Javascript/dhtmltooltip.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../../../../Javascript/draganddrop.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../../../../Javascript/modal.js" type="text/Javascript"></script>  	
    <script language="JavaScript" src="Functions/funcionesCono.js" type="text/Javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin-left:15px; margin-right:15px; margin-top:20px; ">
        <table>
        <tr>
        <td style="width:300px">
            <table id="Table3" style="width: 300px; height: 17px; margin-left:10px;">
                <colgroup><col style='width:20px;'/><col style='width:280px;'/></colgroup>
                <tr class="TBLINI">
                    <td></td>
                    <td>
                        &nbsp;&nbsp;Denominación
                    </td>
                </tr>
            </table>
            <div id="divCatalogo" style="overflow: auto; width: 316px; height:400px; margin-left:10px;">
                <div style='background-image:url(../../../../../Images/imgFT16.gif); width:300px;'>
                    <%=strTablaHtmlTodos%>
                </div>
            </div>
            <table id="Table5" style="width:300px; height:17px; margin-left:10px;" cellspacing="0" border="0">
                <tr class="TBLFIN"><td></td></tr>
            </table>
        </td>
        <td style="width:90px">
            <asp:Image id="imgPapelera" style="CURSOR: pointer; margin-left:30px; margin-top:100px;" runat="server" ImageUrl="~/Images/imgEliminar32.gif" target="true" caso="3" onmouseover="setTarget(this)"></asp:Image>
        </td>
        <td style="width:300px">
            <table id="Table1" style="width: 300px; height: 17px; margin-left:10px;">
                <colgroup><col style='width:20px;'/><col style='width:280px;'/></colgroup>
                <tr class="TBLINI">
                    <td></td>
                    <td>
                        &nbsp;&nbsp;Denominación
                    </td>
                </tr>
            </table>
            <div id="divCatalogo2" style="overflow: auto; width: 316px; height:400px; margin-left:10px;" target="true" onmouseover="setTarget(this);" caso="1">
                <div style='background-image:url(../../../../../Images/imgFT16.gif); width:300px;'>
                    <%=strTablaHtml%>
                </div>
            </div>
            <table style="width:300px; height:17px; margin-left:10px;">
                <tr class="TBLFIN"><td></td></tr>
            </table>
        </td>
        </tr>
        </table>
        <center>
            <table width="230px" style="margin-top:20px;">
            <tr>
                <td>

		            <button id="btnAceptar" type="button" onclick="aceptar();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
			             onmouseover="se(this, 25);mostrarCursor(this);" style="display:inline;margin-left:-20px; margin-top:5px;">
			            <img src="../../../../../Images/imgAceptar.gif" /><span title='Aceptar'>Aceptar</span>
		            </button>
		        </td>
		        <td> 
		            <button id="btnSalir" type="button" onclick="salir();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
			             onmouseover="se(this, 25);mostrarCursor(this);" style="display:inline;margin-left:10px; margin-top:5px;">
			            <img src="../../../../../Images/imgCancelar.gif" /><span>Cancelar</span>
		            </button>
		        </td>
            </tr>
           </table>
       </center>
    </div>
    <div class="clsDragWindow" id="DW" noWrap></div>
    
    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
    <input type="hidden" name="hdnTipo" id="hdnTipo" value="" runat="server"/>
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    </form>
</body>
</html>
