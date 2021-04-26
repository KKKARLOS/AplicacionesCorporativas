<%@ Page Language="C#" EnableViewState="false" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio"%>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
    var iAno = "<%=DateTime.Now.Year.ToString()%>";
    var sProvOld = "";
</script>
<asp:Table ID="tblContenedor" runat="server" CellPadding="0" CellSpacing="0" Width="100%">
    <asp:TableRow>
        <asp:TableCell Width="23%" VerticalAlign="top">
            <asp:Panel runat="server" ID="fstDatos" GroupingText="&nbsp;Ámbito&nbsp;" Width="210px">
                <table border="0" cellpadding="3" cellspacing="0">
                <tr>
                    <td colspan="2">
					    <asp:ImageButton ID="imgAnterior" runat="server" BorderWidth="0px" ImageUrl="~/Images/imgFlechaIzOff.gif"
                            OnClick="imgAnterior_Click" ToolTip="Año anterior" />
					    <asp:textbox id="txtAnno" Width="30px" runat="server"></asp:textbox>	
					    <asp:ImageButton id="imgSiguiente" runat="server" ImageUrl="~/Images/imgFlechaDrOff.gif" ToolTip="Año siguiente"
						    BorderWidth="0px" OnClick="imgSiguiente_Click"></asp:ImageButton>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label id="lblPais" class="label">País </label>
                    </td>
                    <td>
                        <asp:DropDownList id="cboPais" runat="server" style="width:130px;margin-bottom:3px;" onchange="obtenerProvinciasPais(this.value);" CssClass="combo">
                            <asp:ListItem Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label id="lblProvincia" class="label">Provincia </label>
                    </td>
                   <td>
                        <asp:DropDownList id="cboProvincia" runat="server" style="width:130px;margin-bottom:3px;" onchange="cargarFestivos(this.value);" AppendDataBoundItems="true" CssClass="combo">
                            <asp:ListItem Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                </table>
            </asp:Panel>
                 
        </asp:TableCell>
        <asp:TableCell Width="77%">
            <asp:Table ID="tblCalendarios" runat="server" CellPadding="0" CellSpacing="0" Width="100%">
                <asp:TableRow VerticalAlign="top">
                    <asp:TableCell>
                        <asp:Panel runat="server" ID="fstEnero" GroupingText="&nbsp;Enero&nbsp;" Width="185px">
                        <asp:Table ID="tblMes1" EnableViewState="true" runat="server" SkinID="Calendario" CellPadding="0" CellSpacing="0" Width="165px" Height="165px" BackColor="#ECF0EE">
                            <asp:TableRow CssClass="TBLINI" Height="17px" HorizontalAlign="center">
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
                        <asp:Table ID="tblMes2" EnableViewState="true" runat="server" SkinID="Calendario" CellPadding="0" CellSpacing="0" Width="175px" Height="165px" BackColor="#ECF0EE">
                            <asp:TableRow CssClass="TBLINI" Height="17px" HorizontalAlign="center">
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
                        <asp:Table ID="tblMes3" EnableViewState="true" runat="server" SkinID="Calendario" CellPadding="0" CellSpacing="0" Width="175px" Height="165px" BackColor="#ECF0EE">
                            <asp:TableRow CssClass="TBLINI" Height="17px" HorizontalAlign="center">
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
                        <asp:Table ID="tblMes4" EnableViewState="true" runat="server" SkinID="Calendario" CellPadding="0" CellSpacing="0" Width="175px" Height="165px" BackColor="#ECF0EE">
                            <asp:TableRow CssClass="TBLINI" Height="17px" HorizontalAlign="center">
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
                        <asp:Table ID="tblMes5" EnableViewState="true" runat="server" SkinID="Calendario" CellPadding="0" CellSpacing="0" Width="175px" Height="165px" BackColor="#ECF0EE">
                            <asp:TableRow CssClass="TBLINI" Height="17px" HorizontalAlign="center">
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
                        <asp:Table ID="tblMes6" EnableViewState="true" runat="server" SkinID="Calendario" CellPadding="0" CellSpacing="0" Width="175px" Height="165px" BackColor="#ECF0EE">
                            <asp:TableRow CssClass="TBLINI" Height="17px" HorizontalAlign="center">
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
                        <asp:Table ID="tblMes7" EnableViewState="true" runat="server" SkinID="Calendario" CellPadding="0" CellSpacing="0" Width="175px" Height="165px" BackColor="#ECF0EE">
                            <asp:TableRow CssClass="TBLINI" Height="17px" HorizontalAlign="center">
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
                        <asp:Table ID="tblMes8" EnableViewState="true" runat="server" SkinID="Calendario" CellPadding="0" CellSpacing="0" Width="175px" Height="165px" BackColor="#ECF0EE">
                            <asp:TableRow CssClass="TBLINI" Height="17px" HorizontalAlign="center">
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
                        <asp:Table ID="tblMes9" EnableViewState="true" runat="server" SkinID="Calendario" CellPadding="0" CellSpacing="0" Width="175px" Height="165px" BackColor="#ECF0EE">
                            <asp:TableRow CssClass="TBLINI" Height="17px" HorizontalAlign="center">
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
                        <asp:Table ID="tblMes10" EnableViewState="true" runat="server" SkinID="Calendario" CellPadding="0" CellSpacing="0" Width="175px" Height="165px" BackColor="#ECF0EE">
                            <asp:TableRow CssClass="TBLINI" Height="17px" HorizontalAlign="center">
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
                        <asp:Table ID="tblMes11" EnableViewState="true" runat="server" SkinID="Calendario" CellPadding="0" CellSpacing="0" Width="175px" Height="165px" BackColor="#ECF0EE">
                            <asp:TableRow CssClass="TBLINI" Height="17px" HorizontalAlign="center">
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
                        <asp:Table ID="tblMes12" EnableViewState="true" runat="server" SkinID="Calendario" CellPadding="0" CellSpacing="0" Width="175px" Height="165px" BackColor="#ECF0EE">
                            <asp:TableRow CssClass="TBLINI" Height="17px" HorizontalAlign="center">
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
        </asp:TableCell>    
    </asp:TableRow>
</asp:Table>
    <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="comprobarCambios"
        ErrorMessage="CustomValidator"></asp:CustomValidator>
    <asp:TextBox ID="hdnIDCalendario" runat="server" style="visibility: hidden" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();
			switch (strBoton){
				case "grabar": 
				{
				    bEnviar = false;
				    grabar();
					break;
				}
				case "festivo": 
				{
                    bEnviar = false;
                    establecerFestivo();
					break;
				}
				case "limpiar": 
				{
                    bEnviar = false;
                    mostrarProcesando();
                    setTimeout("limpiar()", 20);
                    setTimeout("activarGrabar()", 20);
					break;
				}
				//case "borrar": 
				//{
				//    bEnviar = false;

				//    jqConfirm("", "¿Deseas borrar el horario?", "", "", "war", 230).then(function (answer) {
				//        mostrarProcesando();
				//        setTimeout("borrar()", 20);
				//        fSubmit(bEnviar, eventTarget, eventArgument);            
				//    });

				//	break;
				//}
			    //case "regresar":
			    //    {
			    //        if (bCambios) {
			    //            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
			    //                if (answer) {
			    //                    bEnviar = false;
			    //                    bSalir = true;
			    //                    setTimeout("grabar()", 20);
			    //                } else {
			    //                    bEnviar = true;
			    //                    bCambios = false;
			    //                    fSubmit(bEnviar, eventTarget, eventArgument);
			    //                }
			    //            });
			    //        } else fSubmit(bEnviar, eventTarget, eventArgument);

			    //        break;

			    //    }
			}
			if (strBoton != "borrar" && strBoton != "regresar") fSubmit(bEnviar, eventTarget, eventArgument);
	    }
	    return;
    }
	function fSubmit(bEnviar, eventTarget, eventArgument)         
    {
        var theform = document.forms[0];
        theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
        theform.__EVENTARGUMENT.value = eventArgument;
        if (bEnviar) theform.submit();
        return;
    }    
</script>

</asp:Content>

