<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Horario.aspx.cs" Inherits="Capa_Presentacion_IAP_Calendario_Horario" EnableEventValidation="false" ValidateRequest="false"  %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<base target="_self"/>
	<title> ::: SUPER ::: - Detalle horario del calendario</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<link href="Calendario.css" type="text/css" rel="stylesheet"/>
	<script language="JavaScript" src="../../../Javascript/funciones.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">

	    function cerrarVentana() {
	        var returnValue = null;
	        modalDialog.Close(window, returnValue);
	    }

	    function init() {
	        try {
	            if (!mostrarErrores()) return;
	            ocultarProcesando();
	        } catch (e) {
	            mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
	        }
	    }

    </script>
</HEAD>
<body style="OVERFLOW: hidden" leftMargin="20" topmargin="15px" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
	<form id="Form1" method="post" runat="server">
	<script type="text/javascript">

	    var strErrores = "<%=strErrores%>";
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
	    var strServer = "<%=Session["strServer"]%>";
	
	    var strServer   = "<%=Session["strServer"]%>";
	    var sApellido1  = "<%=Session["APELLIDO1"]%>";
	    var sApellido2  = "<%=Session["APELLIDO2"]%>";
	    var sNombre     = "<%=Session["NOMBRE"]%>";

    </script>
    <div align="center" style="width:100%; margin-top:15px;">
    <asp:Label ID="lblDesCalendario" runat="server" Width="100%"></asp:Label>&nbsp;&nbsp;
    <asp:ImageButton ID="imgAnterior" runat="server" BorderWidth="0px" ImageUrl="~/Images/imgFlechaIzOff.gif" OnClick="imgAnterior_Click" ToolTip="Año anterior" hidefocus=true />
    <asp:textbox id="txtAnno" Width="30px" runat="server" />
    <asp:ImageButton id="imgSiguiente" runat="server" ImageUrl="~/Images/imgFlechaDrOff.gif" ToolTip="Año siguiente" BorderWidth="0px" OnClick="imgSiguiente_Click" hidefocus=true />
    </div>
    <asp:Table ID="tblCalendarios" runat="server" CellPadding="0" CellSpacing="0" Width="800px" style="margin-left:10px;">
        <asp:TableRow VerticalAlign="top">
            <asp:TableCell>
                <asp:Panel runat="server" ID="fstEnero" GroupingText="&nbsp;Enero&nbsp;" Width="185px">
                <asp:Table ID="tblMes1" EnableViewState="true" runat="server" SkinID="Calendario" CellPadding="0" CellSpacing="0" Width="175" Height="170" BackColor="#ECF0EE">
                    <asp:TableRow CssClass="TBLINI" Height="17" HorizontalAlign="center">
                        <asp:TableCell CssClass="TitCal" ToolTip="Lunes">L</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Martes">M</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Miércoles">X</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Jueves">J</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Viernes">V</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Sábado">S</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Domingo">D</asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                </asp:Panel>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Panel runat="server" ID="fstFebrero" GroupingText="&nbsp;Febrero&nbsp;" Width="185px">
                <asp:Table ID="tblMes2" EnableViewState="true" runat="server" SkinID="Calendario" CellPadding="0" CellSpacing="0" Width="175" Height="170" BackColor="#ECF0EE">
                    <asp:TableRow CssClass="TBLINI" Height="17" HorizontalAlign="center">
                        <asp:TableCell CssClass="TitCal" ToolTip="Lunes">L</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Martes">M</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Miércoles">X</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Jueves">J</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Viernes">V</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Sábado">S</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Domingo">D</asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                 </asp:Panel>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Panel runat="server" ID="fstMarzo" GroupingText="&nbsp;Marzo&nbsp;" Width="185px">
                <asp:Table ID="tblMes3" EnableViewState="true" runat="server" SkinID="Calendario" CellPadding="0" CellSpacing="0" Width="175" Height="170" BackColor="#ECF0EE">
                    <asp:TableRow CssClass="TBLINI" Height="17" HorizontalAlign="center">
                        <asp:TableCell CssClass="TitCal" ToolTip="Lunes">L</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Martes">M</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Miércoles">X</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Jueves">J</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Viernes">V</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Sábado">S</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Domingo">D</asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                </asp:Panel>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Panel runat="server" ID="fstAbril" GroupingText="&nbsp;Abril&nbsp;" Width="185px">
                <asp:Table ID="tblMes4" EnableViewState="true" runat="server" SkinID="Calendario" CellPadding="0" CellSpacing="0" Width="175" Height="170" BackColor="#ECF0EE">
                    <asp:TableRow CssClass="TBLINI" Height="17" HorizontalAlign="center">
                        <asp:TableCell CssClass="TitCal" ToolTip="Lunes">L</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Martes">M</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Miércoles">X</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Jueves">J</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Viernes">V</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Sábado">S</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Domingo">D</asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                </asp:Panel>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow VerticalAlign="top">
            <asp:TableCell>
                <asp:Panel runat="server" ID="fstMayo" GroupingText="&nbsp;Mayo&nbsp;" Width="185px">
                <asp:Table ID="tblMes5" EnableViewState="true" runat="server" SkinID="Calendario" CellPadding="0" CellSpacing="0" Width="175" Height="170" BackColor="#ECF0EE">
                    <asp:TableRow CssClass="TBLINI" Height="17" HorizontalAlign="center">
                        <asp:TableCell CssClass="TitCal" ToolTip="Lunes">L</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Martes">M</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Miércoles">X</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Jueves">J</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Viernes">V</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Sábado">S</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Domingo">D</asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                </asp:Panel>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Panel runat="server" ID="fstJunio" GroupingText="&nbsp;Junio&nbsp;" Width="185px">
                <asp:Table ID="tblMes6" EnableViewState="true" runat="server" SkinID="Calendario" CellPadding="0" CellSpacing="0" Width="175" Height="170" BackColor="#ECF0EE">
                    <asp:TableRow CssClass="TBLINI" Height="17" HorizontalAlign="center">
                        <asp:TableCell CssClass="TitCal" ToolTip="Lunes">L</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Martes">M</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Miércoles">X</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Jueves">J</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Viernes">V</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Sábado">S</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Domingo">D</asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                </asp:Panel>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Panel runat="server" ID="fstJulio" GroupingText="&nbsp;Julio&nbsp;" Width="185px">
                <asp:Table ID="tblMes7" EnableViewState="true" runat="server" SkinID="Calendario" CellPadding="0" CellSpacing="0" Width="175" Height="170" BackColor="#ECF0EE">
                    <asp:TableRow CssClass="TBLINI" Height="17" HorizontalAlign="center">
                        <asp:TableCell CssClass="TitCal" ToolTip="Lunes">L</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Martes">M</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Miércoles">X</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Jueves">J</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Viernes">V</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Sábado">S</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Domingo">D</asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                </asp:Panel>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Panel runat="server" ID="fstAgosto" GroupingText="&nbsp;Agosto&nbsp;" Width="185px">
                <asp:Table ID="tblMes8" EnableViewState="true" runat="server" SkinID="Calendario" CellPadding="0" CellSpacing="0" Width="175" Height="170" BackColor="#ECF0EE">
                    <asp:TableRow CssClass="TBLINI" Height="17" HorizontalAlign="center">
                        <asp:TableCell CssClass="TitCal" ToolTip="Lunes">L</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Martes">M</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Miércoles">X</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Jueves">J</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Viernes">V</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Sábado">S</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Domingo">D</asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                </asp:Panel>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow VerticalAlign="top">
            <asp:TableCell>
                <asp:Panel runat="server" ID="fstSeptiembre" GroupingText="&nbsp;Septiembre&nbsp;" Width="185px">
                <asp:Table ID="tblMes9" EnableViewState="true" runat="server" SkinID="Calendario" CellPadding="0" CellSpacing="0" Width="175" Height="170" BackColor="#ECF0EE">
                    <asp:TableRow CssClass="TBLINI" Height="17" HorizontalAlign="center">
                        <asp:TableCell CssClass="TitCal" ToolTip="Lunes">L</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Martes">M</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Miércoles">X</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Jueves">J</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Viernes">V</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Sábado">S</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Domingo">D</asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                </asp:Panel>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Panel runat="server" ID="fstOctubre" GroupingText="&nbsp;Octubre&nbsp;" Width="185px">
                <asp:Table ID="tblMes10" EnableViewState="true" runat="server" SkinID="Calendario" CellPadding="0" CellSpacing="0" Width="175" Height="170" BackColor="#ECF0EE">
                    <asp:TableRow CssClass="TBLINI" Height="17" HorizontalAlign="center">
                        <asp:TableCell CssClass="TitCal" ToolTip="Lunes">L</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Martes">M</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Miércoles">X</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Jueves">J</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Viernes">V</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Sábado">S</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Domingo">D</asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                </asp:Panel>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Panel runat="server" ID="fstNoviembre" GroupingText="&nbsp;Noviembre&nbsp;" Width="185px">
                <asp:Table ID="tblMes11" EnableViewState="true" runat="server" SkinID="Calendario" CellPadding="0" CellSpacing="0" Width="175" Height="170" BackColor="#ECF0EE">
                    <asp:TableRow CssClass="TBLINI" Height="17" HorizontalAlign="center">
                        <asp:TableCell CssClass="TitCal" ToolTip="Lunes">L</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Martes">M</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Miércoles">X</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Jueves">J</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Viernes">V</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Sábado">S</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Domingo">D</asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                </asp:Panel>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Panel runat="server" ID="fstDiciembre" GroupingText="&nbsp;Diciembre&nbsp;" Width="185px">
                <asp:Table ID="tblMes12" EnableViewState="true" runat="server" SkinID="Calendario" CellPadding="0" CellSpacing="0" Width="175" Height="170" BackColor="#ECF0EE">
                    <asp:TableRow CssClass="TBLINI" Height="17" HorizontalAlign="center">
                        <asp:TableCell CssClass="TitCal" ToolTip="Lunes">L</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Martes">M</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Miércoles">X</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Jueves">J</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Viernes">V</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Sábado">S</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Domingo">D</asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                </asp:Panel>
            </asp:TableCell>
         </asp:TableRow>
        </asp:Table>
    <br /><br /><br />
    <center>
        <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W85" runat="server" hidefocus="hidefocus" 
             onmouseover="se(this, 25);mostrarCursor(this);">
            <img src="../../../images/botones/imgSalir.gif" /><span>Salir</span>
        </button>
    </center>
    <asp:TextBox ID="hdnIDCalendario" runat="server" style="visibility: hidden" />
    <input type="hidden" id="hdnErrores" value="<%=strErrores %>" />
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    </form>
</body>
</html>
