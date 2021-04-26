<%@ Page Language="C#" EnableViewState="false" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio"%>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<asp:Table ID="tblContenedor" runat="server" CellPadding="0" CellSpacing="0" Width="100%">
    <asp:TableRow>
        <asp:TableCell Width="23%" VerticalAlign="top">
            <asp:Panel runat="server" ID="fstDatos" GroupingText="&nbsp;Datos generales&nbsp;" Width="210px">
                <table border="0" cellpadding="3" cellspacing="0">
                    <tr>
                        <td>
                            <asp:Label ID="lblDesCalendario" runat="server" Width="100%"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
					        <asp:ImageButton ID="imgAnterior" runat="server" BorderWidth="0px" ImageUrl="~/Images/imgFlechaIzOff.gif"
                                OnClick="imgAnterior_Click" ToolTip="Año anterior" />
					        <asp:textbox id="txtAnno" Width="30px" runat="server"></asp:textbox>	
					        <asp:ImageButton id="imgSiguiente" runat="server" ImageUrl="~/Images/imgFlechaDrOff.gif" ToolTip="Año siguiente"
						        BorderWidth="0px" OnClick="imgSiguiente_Click"></asp:ImageButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblJH" runat="server" Text="Jornadas hábiles" Width="120px"></asp:Label>
                            <asp:TextBox ID="txtJH" runat="server" Width="40px" SkinID="numero" ReadOnly="true" style="background-color:gainsboro"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblJV" runat="server" Text="Jornadas vacaciones" Width="110px"></asp:Label>
                            <asp:Label ID="lblDeno" runat="server" ForeColor="Red" Width="6px">*</asp:Label>
                            <asp:TextBox ID="txtJV" runat="server" Width="40px" onFocus="fn(this, 3, 0);" SkinID="numero" MaxLength="3" onchange="setJornDen()"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblJD" runat="server" Text="Jornadas laborables" Width="120px"></asp:Label>
                            <asp:TextBox ID="txtJD" runat="server" Width="40px" SkinID="numero" ReadOnly="true" style="background-color:gainsboro;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblHL" runat="server" Text="Horas laborables" Width="110px"></asp:Label>
                            <asp:Label ID="Label2" runat="server" ForeColor="Red" Width="6px">*</asp:Label>
                            <asp:TextBox ID="txtHL" runat="server" Width="40px" onFocus="fn(this, 4, 0);" SkinID="numero" MaxLength="5"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        
            <br />
            <br />
            <asp:Panel runat="server" ID="fstAsignacion" GroupingText="&nbsp;Asignación horaria&nbsp;" Width="210px">
            &nbsp;&nbsp;&nbsp;<asp:Label ID="lblInicio" runat="server" Text="Fecha inicio" Width="60px"></asp:Label>
            &nbsp;<asp:TextBox ID="txtFecIni" runat="server" Width="60px"></asp:TextBox>
            <br />
            &nbsp;&nbsp;&nbsp;<asp:Label ID="lblFin" runat="server" Text="Fecha fin" Width="60px"></asp:Label>
            &nbsp;<asp:TextBox ID="txtFecFin" runat="server" Width="60px"></asp:TextBox>
            <br />
            &nbsp;&nbsp;&nbsp;<asp:Label ID="lblHoras" runat="server" Text="Horas" Width="60px"></asp:Label>
            &nbsp;<asp:TextBox ID="txtHoras" runat="server" onFocus="fn(this, 2, 2);"
                 SkinID="numero" Width="40px"></asp:TextBox>
            <br />
            <br />
            <asp:CheckBoxList ID="chklstDias" runat="server" RepeatDirection=vertical TextAlign=right>
                <asp:ListItem Value="L" Text="Lunes"></asp:ListItem>
                <asp:ListItem Value="M" Text="Martes"></asp:ListItem>
                <asp:ListItem Value="X" Text="Miércoles"></asp:ListItem>
                <asp:ListItem Value="J" Text="Jueves"></asp:ListItem>
                <asp:ListItem Value="V" Text="Viernes"></asp:ListItem>
                <asp:ListItem Value="S" Text="Sábado"></asp:ListItem>
                <asp:ListItem Value="D" Text="Domingo"></asp:ListItem>
            </asp:CheckBoxList>  
            <br />
            <br />
            &nbsp;&nbsp;&nbsp;<asp:Label ID="Label1" runat="server" Text="Total horas asignadas:"></asp:Label>&nbsp;<asp:Label ID="lblTotalHoras" runat="server" Text="0"></asp:Label>
            </asp:Panel>
        
            <br />
            <br />
            <asp:Panel runat="server" ID="fstSemLaboral" GroupingText="&nbsp;Semana laboral&nbsp;" Width="210px" Height="20px">
                <asp:Table ID="tblSemLaboral" runat="server" CellPadding="0" CellSpacing="0" EnableViewState="true"
                    Height="20px" SkinID="Calendario" Style="margin: 10px;" Width="175px">
                    <asp:TableRow CssClass="TBLINI" Height="17px" HorizontalAlign="center">
                        <asp:TableCell CssClass="TitCal" ToolTip="Lunes">L</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Martes">M</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Miércoles">X</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Jueves">J</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Viernes">V</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Sábado">S</asp:TableCell>
                        <asp:TableCell CssClass="TitCal" ToolTip="Domingo">D</asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow HorizontalAlign="center">
                        <asp:TableCell><asp:CheckBox ID="chkSemLabL" runat="server" onclick="modificarSemLab(this.id)" /></asp:TableCell>
                        <asp:TableCell><asp:CheckBox ID="chkSemLabM" runat="server" onclick="modificarSemLab(this.id)" /></asp:TableCell>
                        <asp:TableCell><asp:CheckBox ID="chkSemLabX" runat="server" onclick="modificarSemLab(this.id)" /></asp:TableCell>
                        <asp:TableCell><asp:CheckBox ID="chkSemLabJ" runat="server" onclick="modificarSemLab(this.id)" /></asp:TableCell>
                        <asp:TableCell><asp:CheckBox ID="chkSemLabV" runat="server" onclick="modificarSemLab(this.id)" /></asp:TableCell>
                        <asp:TableCell><asp:CheckBox ID="chkSemLabS" runat="server" onclick="modificarSemLab(this.id)" /></asp:TableCell>
                        <asp:TableCell><asp:CheckBox ID="chkSemLabD" runat="server" onclick="modificarSemLab(this.id)" /></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
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
    <asp:TextBox ID="hdnCodProvincia" runat="server" style="visibility: hidden" />
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
				case "asignar": 
				{
                    bEnviar = false;
                    mostrarProcesando();
                    setTimeout("asignar()",20);
					break;
				}
				case "iniciofec": 
				{
                    bEnviar = false;
                    establecerInicio();
					break;
				}
				case "finfec": 
				{
                    bEnviar = false;
                    establecerFinal();
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
                    setTimeout("limpiar()",20);
					break;
				}
			    case "festivoprov":
			        {
			            bEnviar = false;
			            cargarFestivos($I("hdnCodProvincia").value);
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
			    case "regresar":
			        {
			            if (bCambios) {
			                jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
			                    if (answer) {
			                        bEnviar = false;
			                        bSalir = true;
			                        setTimeout("grabar()", 20);
			                    } else {
			                        bEnviar = true;
			                        bCambios = false;
			                        fSubmit(bEnviar, eventTarget, eventArgument);
			                    }
			                });
			            } else fSubmit(bEnviar, eventTarget, eventArgument);

			            break;

			        }
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

