<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Anual.aspx.cs" Inherits="Calendario_Anual" EnableEventValidation="false" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
	<title>Detalle horario del calendario</title>
	<meta http-equiv='X-UA-Compatible' content='IE=edge' />
	<base target="_self">
	<LINK href="Calendario.css" type="text/css" rel="stylesheet">
	<script language="JavaScript" src="../../../Javascript/funciones.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../../Javascript/modal.js" type="text/Javascript"></script>
	<script language="Javascript">
	<!--
        var aDiaSem = new Array("L", "M", "X", "J", "V", "S", "D");
        var bSemLab = new Array(false, false, false, false, false, false, false);

	    function cerrarVentana(){
	        //        window.returnValue = null;
	        //        window.close();
	        var returnValue = null;
	        modalDialog.Close(window, returnValue);
	    }

		function init(){
            try{
                if (!mostrarErrores()) return;
    	        ocultarProcesando();
	        }catch(e){
		        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
            }
		}
        var objSeleccionado = "";
        function selFecha(IDCelda){
            try{
                if ((objSeleccionado != "")&&(objSeleccionado != IDCelda)){
                    $I(objSeleccionado).className = "TDCal";
                }
                objSeleccionado = IDCelda;
                if ($I(IDCelda).className != "TDCalSel") $I(IDCelda).className = "TDCalSel";
                else if (bSemLab[$I(IDCelda).cellIndex]) $I(IDCelda).className = "TDCal";
                     else $I(IDCelda).className = "textoCalFinde";
	        }catch(e){
		        mostrarErrorAplicacion("Error al seleccionar el día indicado", e.message);
	        }
        }
		
	-->
    </script>
</HEAD>
<body style="OVERFLOW: hidden" leftMargin="20" topmargin="15px" onload="init()">
    <ucproc:Procesando ID="Procesando" runat="server" />
	<form id="Form1" method="post" runat="server">
	<script language="Javascript">
	<!--
	    var strErrores = "<%=strErrores%>";
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
	    var strServer = "<%=Session["GVT_strServer"]%>";
	
	    var strServer   = "<%=Session["GVT_strServer"]%>";
	    var sApellido1  = "<%=Session["GVT_APELLIDO1"]%>";
	    var sApellido2  = "<%=Session["GVT_APELLIDO2"]%>";
	    var sNombre     = "<%=Session["GVT_NOMBRE"]%>";
	-->
    </script>
    <div style="text-align:center; width:100%; margin-top:15px;">
    <asp:Label ID="lblDesCalendario" runat="server" Width="100%"></asp:Label>&nbsp;&nbsp;
    <asp:ImageButton ID="imgAnterior" runat="server" BorderWidth="0px" ImageUrl="~/Images/imgFlechaIzOff.gif" OnClick="imgAnterior_Click" ToolTip="Año anterior" hidefocus=true />
    <asp:textbox id="txtAnno" Width="30px" runat="server" />
    <asp:ImageButton id="imgSiguiente" runat="server" ImageUrl="~/Images/imgFlechaDrOff.gif" ToolTip="Año siguiente" BorderWidth="0px" OnClick="imgSiguiente_Click" hidefocus=true />
    </div>
    <asp:Table ID="tblCalendarios" runat="server" style="margin-left:10px; width:740px">
        <asp:TableRow VerticalAlign="top">
            <asp:TableCell>
                <asp:Panel runat="server" ID="fstEnero" GroupingText="&nbsp;Enero&nbsp;" Width="165">
                <asp:Table ID="tblMes1" EnableViewState="true" runat="server" SkinID="Calendario" style="margin:5px; width:15px; height:150px;" BackColor="#ECF0EE">
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
                <asp:Panel runat="server" ID="fstFebrero" GroupingText="&nbsp;Febrero&nbsp;" Width="165">
                <asp:Table ID="tblMes2" EnableViewState="true" runat="server" SkinID="Calendario" style="margin:5px; width:15px; height:150px;" BackColor="#ECF0EE">
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
                <asp:Panel runat="server" ID="fstMarzo" GroupingText="&nbsp;Marzo&nbsp;" Width="165">
                <asp:Table ID="tblMes3" EnableViewState="true" runat="server" SkinID="Calendario" style="margin:5px; width:15px; height:150px;" BackColor="#ECF0EE">
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
                <asp:Panel runat="server" ID="fstAbril" GroupingText="&nbsp;Abril&nbsp;" Width="165">
                <asp:Table ID="tblMes4" EnableViewState="true" runat="server" SkinID="Calendario" style="margin:5px; width:15px; height:150px;" BackColor="#ECF0EE">
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
                <asp:Panel runat="server" ID="fstMayo" GroupingText="&nbsp;Mayo&nbsp;" Width="165">
                <asp:Table ID="tblMes5" EnableViewState="true" runat="server" SkinID="Calendario" style="margin:5px; width:15px; height:150px;" BackColor="#ECF0EE">
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
                <asp:Panel runat="server" ID="fstJunio" GroupingText="&nbsp;Junio&nbsp;" Width="165">
                <asp:Table ID="tblMes6" EnableViewState="true" runat="server" SkinID="Calendario" style="margin:5px; width:15px; height:150px;" BackColor="#ECF0EE">
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
                <asp:Panel runat="server" ID="fstJulio" GroupingText="&nbsp;Julio&nbsp;" Width="165">
                <asp:Table ID="tblMes7" EnableViewState="true" runat="server" SkinID="Calendario" style="margin:5px; width:15px; height:150px;" BackColor="#ECF0EE">
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
                <asp:Panel runat="server" ID="fstAgosto" GroupingText="&nbsp;Agosto&nbsp;" Width="165">
                <asp:Table ID="tblMes8" EnableViewState="true" runat="server" SkinID="Calendario" style="margin:5px; width:15px; height:150px;" BackColor="#ECF0EE">
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
                <asp:Panel runat="server" ID="fstSeptiembre" GroupingText="&nbsp;Septiembre&nbsp;" Width="165">
                <asp:Table ID="tblMes9" EnableViewState="true" runat="server" SkinID="Calendario" style="margin:5px; width:15px; height:150px;" BackColor="#ECF0EE">
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
                <asp:Panel runat="server" ID="fstOctubre" GroupingText="&nbsp;Octubre&nbsp;" Width="165">
                <asp:Table ID="tblMes10" EnableViewState="true" runat="server" SkinID="Calendario" style="margin:5px; width:15px; height:150px;" BackColor="#ECF0EE">
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
                <asp:Panel runat="server" ID="fstNoviembre" GroupingText="&nbsp;Noviembre&nbsp;" Width="165">
                <asp:Table ID="tblMes11" EnableViewState="true" runat="server" SkinID="Calendario" style="margin:5px; width:15px; height:150px;" BackColor="#ECF0EE">
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
                <asp:Panel runat="server" ID="fstDiciembre" GroupingText="&nbsp;Diciembre&nbsp;" Width="165">
                <asp:Table ID="tblMes12" EnableViewState="true" runat="server" SkinID="Calendario" style="margin:5px; width:15px; height:150px;" BackColor="#ECF0EE">
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
    <br />
    <center>
        <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../Images/Botones/imgSalir.gif" /><span title="Salir">Salir</span></button>		
    </center>
    <asp:TextBox ID="hdnIDCalendario" runat="server" style="visibility: hidden" />
    <input type="hidden" id="hdnErrores" value="<%=strErrores %>" />
    </form>
</body>
</html>
