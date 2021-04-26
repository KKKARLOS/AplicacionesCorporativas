<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> ::: SUPER ::: - Diagrama de Gantt del proyecto</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <link rel="stylesheet" href="../../../../PopCalendar/CSS/Classic.css" type="text/css" />
    <!--<link rel="stylesheet" href="css/estilo.css" type="text/css" /> -->
    <script src="../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script src="Functions/Funciones.js" type="text/Javascript"></script>
	<script src="Functions/TareaHito.js" type="text/Javascript"></script>
 	<script src="../../../../Javascript/boxover.js" type="text/Javascript"></script>
 	<script src="../../../../PopCalendar/PopCalendar.js" type="text/Javascript"></script>
 	<script src="../../../../Javascript/modal.js" type="text/Javascript"></script>
 	<style type="text/css">
        .divTitulo2 {
            background-image: url('../../../../Images/fondoTituloDoble.gif');
            background-repeat: repeat-x;
            height: 34px;
            color: #ffffff;
        }
 	</style>
</head>
<body style="overflow: hidden" onload="init()" onunload="salir()" >
<form id="Form1" name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
<script type="text/javascript">
    var strServer = "<% =Session["strServer"].ToString() %>";
    var bLectura = false;
    var bMostrarMsg = true;
    var sIniProy = "<%=dIniTotal.ToShortDateString() %>";
    var sFinProy = "<%=dFinTotal.ToShortDateString() %>";
    var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos. 
    var strHCM = "<%=strHCM %>";
    var bRes1024 = <%=((bool)Session["ESTRUCT1024"]) ? "true":"false" %>;
</script>
<br />

<table id="tblCab" style="width:1000px; text-align:left;" cellpadding="3px">
    <colgroup><col style="width:60px;" /><col style="width:30px;" /><col style="width:70px;" /><col style="width:600px;" /><col style="width:240px;" /></colgroup>
    <tr>
        <td>
            <label id="lblProy" runat="server" title="Proyecto económico" style="width:55px; padding-left:15px;" >Proyecto</label>
        </td>
        <td>
            <asp:Image ID="imgEstProy" runat="server" Height="16" Width="16" ImageUrl="~/images/imgSeparador.gif" CssClass="ICO" />
        </td>
        <td>
            <asp:TextBox ID="txtCodProy" runat="server" Text="" MaxLength="6" style="width:60px;" SkinID="Numero" readonly="true" />
        </td>
        <td>
            <div id="divPry" style="width:610px;">
                <asp:TextBox ID="txtNomProy" runat="server" Text="" style="width:580px;" readonly="true" />
            </div>
        </td>
        <td>
            <asp:CheckBox ID="chkCerradas" runat="server" Text="Mostrar tareas cerradas" Width="180px" TextAlign="Left" CssClass="check texto" onclick="mostrarCerradas();" />     
        </td>        
    </tr>
    <tr>
        <td colspan="5">&nbsp;&nbsp;&nbsp;&nbsp;
            <img id="imgNE1" src='../../../../images/imgNE1off.gif' class="ne" onclick="setNE(1);"/>
            <img id="imgNE2" src='../../../../images/imgNE2off.gif' class="ne" onclick="setNE(2);"/>
            &nbsp;&nbsp;&nbsp;Escala
            <asp:DropDownList ID="cboVista" runat="server" onchange="modificarVista(this.value);" style="vertical-align:middle; margin-left:5px;">
                <asp:ListItem Value="M" Text="Mensual" Selected="True"></asp:ListItem>
                <asp:ListItem Value="S" Text="Semanal"></asp:ListItem>
                <asp:ListItem Value="D" Text="Diaria"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
</table>
<table id="tblProyecto" style="height:20px; width:1250px; margin-left:10px;" cellpadding="0px">
    <colgroup><col style="width:590px;"/><col style="width:660px;"/></colgroup>
    <tr>
        <td>
            <div id="divTituloFijo" style="width:590px;" runat="server">
                <table id='tblTituloFijo' style='width:590px;' cellpadding='0' cellspacing='0' border='0'>
                    <colgroup>
                       <col style='width:40px;' />
                       <col style='width:270px;' />
                       <col style='width:70px;' />
                       <col style='width:70px;' />
                       <col style='width:70px;' />
                       <col style='width:70px;' />
                    </colgroup>
                    <tr style='height:34px; vertical-align:middle;' class="tituloDoble">
                       <td></td>
                       <td>Denominación</td>
                       <td title='Fecha inicio planificada' style="text-align:center;">FIPL</td>
                       <td title='Fecha fin planificada' style="text-align:center;">FFPL</td>
                       <td title='Estimación total prevista' style="text-align:right;">ETPR</td>
                       <td title='Fecha fin prevista' style="text-align:center;">FFPR</td>
                    </tr>
                </table>
            </div>
        </td>
        <td>
            <div id="divTituloMovil" style="width:630px; overflow:hidden;" runat="server">
                <table id='tblTituloMovil' style='font-size:8pt; width:1300px;' cellpadding='0' cellspacing='0' border='0'>
                </table>
            </div>
        </td>
    </tr>
    <tr>
        <td>
            <div id="divCatalogo" style="overflow:hidden; width:590px; height:700px; visibility:hidden;">
                <div style='background-image:url(../../../../Images/imgFT20.gif); width:590px'>
                    <%=strTablaHTMLTarea%>
                </div>
            </div>
        </td>
        <td style="vertical-align:top;">
            <div id="divBodyMovil" style="width:630px; height:700px; overflow-x:hidden; overflow-y:auto;" runat="server" onscroll="setScrollY()">
                <%=strTablaGantt%>
            </div>
        </td>
    </tr>
    <tr style="vertical-align:top;">
        <td>
            <div id="divPieFijo" style="width:590px;" class="TBLFIN" runat="server">
            </div>
        </td>
        <td>
            <div id="divPieMovil" style="width:630px; height:17px; overflow-x:scroll; overflow-y:hidden;" runat="server" onscroll="setScrollX()">
                <table id='tblPieMovil' style='font-size:9pt; width:1300px;' cellpadding='0' cellspacing='0' border='0'>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table style="margin-left:15px; margin-top:5px;">
    <tr>
        <td>
            <img src='../../../../Images/imgGanttV.gif' border='1' style='vertical-align:middle;' width='20px' height='14px'/>
            &nbsp;&nbsp;Avance teórico
        </td>
        <td>
            <img src='../../../../Images/imgAr.gif' border='1' style='vertical-align:middle;' width='20px' height='14px'/>
            &nbsp;&nbsp;Avance real
        </td>
    </tr>
    <tr>
        <td>
            <img src='../../../../Images/imgGanttR.gif' border='1' style='vertical-align:middle;' width='20px' height='14px'/>
            &nbsp;&nbsp;Pendiente teórico&nbsp;&nbsp;
        </td>
        <td>
            <img src='../../../../Images/imgGanttAvanMax.gif' border='1' style='vertical-align:middle;' width='20px' height='14px'/>
            &nbsp;&nbsp;Avance teórico superior al 100%
        </td>
    </tr>
</table>
<center>
<table style="width:360px; margin-left:20px;">
    <tr>
        <td>
		    <button id="btnGrabar" type="button" onclick="grabarAux();" class="btnH25W95" runat="server" disabled hidefocus="hidefocus" 
			     onmouseover="se(this, 25);mostrarCursor(this);">
			    <img src="../../../../images/botones/imgGrabar.gif" /><span title="Grabar">&nbsp;&nbsp;Grabar</span>
		    </button>	
        </td>
        <td>
		    <button id="btnGrabarSalir" type="button" onclick="grabarSalir();" class="btnH25W95" runat="server" disabled hidefocus="hidefocus" 
			     onmouseover="se(this, 25);mostrarCursor(this);">
			    <img src="../../../../images/botones/imgGrabarSalir.gif" /><span title="Grabar y salir">Grabar...</span>
		    </button>	
        </td>		
        <td>
		    <button id="btnSalir" type="button" onclick="salir();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
			     onmouseover="se(this, 25);mostrarCursor(this);">
			    <img src="../../../../images/botones/imgSalir.gif" /><span title="Salir">&nbsp;&nbsp;Salir</span>
		    </button>	 
        </td>
    </tr>
</table>
</center>

<asp:TextBox ID="hdnEsRtpt" name="hdnEsRtpt" runat="server" style="visibility:hidden" Text="N"></asp:TextBox>
<asp:TextBox ID="hdnPrint" name="hdnPrint" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
<asp:TextBox ID="hdnAcceso" name="hdnAcceso" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<input type="hidden" runat="server" name="hdnT305IdProy" id="hdnT305IdProy" value="" />
<input type="hidden" runat="server" name="hdnDenProy" id="hdnDenProy" value="" />
<input type="hidden" runat="server" name="hdnResp" id="hdnResp" value="" />
<input type="hidden" runat="server" name="hdnNodo" id="hdnNodo" value="" />
<input type="hidden" runat="server" name="hdnDenNodo" id="hdnDenNodo" value="" />
<input type="hidden" runat="server" name="hdnCliente" id="hdnCliente" value="" />
<asp:TextBox ID="txtPreCerr" runat="server" Text="" style="width:1px;display:none;" readonly="true" />
<asp:TextBox ID="txtUne" runat="server" Text="" style="width:1px;display:none;" readonly="true" />
<asp:TextBox ID="txtMotivo" runat="server" Text="" style="width:1px;display:none" readonly="true" />
<asp:TextBox ID="txtEstado" runat="server" style="width:1px;visibility:hidden" readonly="true" />
<asp:TextBox ID="MonedaPSN" runat="server" style="visibility:hidden" Text="" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
</form>
</body>
</html>

