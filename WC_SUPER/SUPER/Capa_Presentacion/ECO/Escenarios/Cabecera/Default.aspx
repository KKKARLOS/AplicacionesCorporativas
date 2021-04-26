<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_ECO_Escenarios_Cabecera_Default" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Creación de escenario</title>
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/dhtmltooltip.js" type="text/Javascript"></script>
	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
   	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onload="init()" onunload="unload()">
<form id="Form1" name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
<script type="text/javascript">

    var strServer = "<% =Session["strServer"].ToString() %>";
    var intSession = <%=Session.Timeout%>;

</script>
<style type="text/css">
#tblGeneral td { padding: 5px;}
#tblTituloEscenarios td, #tblDatos td, #tblPieEscenarios td { padding: 0px }
#trTituloCoste td, #tblDatosCoste td, 
#trTituloTarifa td, #tblDatosTarifa td, 
#trTituloPartidas td, #tblPartidas td { padding: 0px 2px 0px 2px;}
#tblDatosCoste tr, #tblDatosTarifa tr, #tblPartidas tr { height: 20px; }
#tblPartidas td {
	border: 1px solid #A6C3D2;
}
</style>
<table id="tblGeneral" style="width:900px; margin-left:10px; margin-top:10px;">
<colgroup>
    <col style="width:450px;" />
    <col style="width:450px;" />
</colgroup>
    <tr>
        <td colspan="2"><label style="width:70px; vertical-align: middle;" title="Denominación del escenario">Escenario</label>
            <asp:TextBox ID="txtDenominacion" style="width:500px;" Text="" runat="server" MaxLength="50" />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <label style="width:70px; vertical-align: middle;">Inicio</label>
            <asp:DropDownList ID="cboMesInicio" style="width:90px;" runat="server">
                <asp:ListItem Value="01" Text="Enero" />
                <asp:ListItem Value="02" Text="Febrero" />
                <asp:ListItem Value="03" Text="Marzo" />
                <asp:ListItem Value="04" Text="Abril" />
                <asp:ListItem Value="05" Text="Mayo" />
                <asp:ListItem Value="06" Text="Junio" />
                <asp:ListItem Value="07" Text="Julio" />
                <asp:ListItem Value="08" Text="Agosto" />
                <asp:ListItem Value="09" Text="Septiembre" />
                <asp:ListItem Value="10" Text="Octubre" />
                <asp:ListItem Value="11" Text="Noviembre" />
                <asp:ListItem Value="12" Text="Diciembre" />
            </asp:DropDownList>
            <asp:TextBox ID="txtAnnoInicio" style="width:40px; margin-right:40px; vertical-align: middle;" onfocus="fn(this,4,0)" SkinID="Numero" Text="" runat="server" />
            <label style="width:25px; vertical-align: middle;">Fin</label>
            <asp:DropDownList ID="cboMesFin" style="width:90px;" runat="server">
                <asp:ListItem Value="01" Text="Enero" />
                <asp:ListItem Value="02" Text="Febrero" />
                <asp:ListItem Value="03" Text="Marzo" />
                <asp:ListItem Value="04" Text="Abril" />
                <asp:ListItem Value="05" Text="Mayo" />
                <asp:ListItem Value="06" Text="Junio" />
                <asp:ListItem Value="07" Text="Julio" />
                <asp:ListItem Value="08" Text="Agosto" />
                <asp:ListItem Value="09" Text="Septiembre" />
                <asp:ListItem Value="10" Text="Octubre" />
                <asp:ListItem Value="11" Text="Noviembre" />
                <asp:ListItem Value="12" Text="Diciembre" />
            </asp:DropDownList>
            <asp:TextBox ID="txtAnnoFin" style="width:40px; margin-right:20px; vertical-align:middle; margin-right:60px;" onfocus="fn(this,4,0)" SkinID="Numero" Text="" runat="server" />
            <label style="width:110px; vertical-align: middle;">Plazo medio de cobro</label>
            <asp:DropDownList ID="cboPMC" style="width:150px;" runat="server">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <table style="width:400px;">
            <colgroup>
                <col style="width:300px;" />
                <col style="width:100px;" />
            </colgroup>
            <tr>
                <td colspan="2">
                    <fieldset style="width: 110px;">
	                    <legend>Coste</legend>   
                        <asp:RadioButtonList ID="rdbCoste" SkinId="rbl" runat="server" RepeatColumns="2" onclick="if(!$I('rdbCoste_0').disabled) {setCosteNivel();}">
                            <asp:ListItem Value="H" style="cursor:pointer"><img src='../../../../Images/Botones/imgHorario.gif' border='0' title="Por horas" style="cursor:pointer" onclick="this.parentNode.click()">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="J" style="cursor:pointer"><img src='../../../../Images/Botones/imgCalendario.gif' border='0' title="Por jornadas" style="cursor:pointer" onclick="this.parentNode.click()"></asp:ListItem>
                        </asp:RadioButtonList>
                     </fieldset>
                </td>
            </tr>
            <tr id="trTituloCoste" class="TBLINI">
                <td>Nivel</td>
                <td>Coste</td>
            </tr>
            </table>
            <div id="divCatalogoCoste" style="width:416px; height:150px; overflow:auto; overflow-x:hidden; " runat="server">
                <div style="background-image: url('../../../../Images/imgFT20.gif'); background-repeat:repeat; width:400px; height:auto;">
                    <table id='tblDatosCoste' style='width:400px;'> 
                        <colgroup>
                            <col style="width:300px;" />
                            <col style="width:100px;" />
                        </colgroup>
                        <tr><td>Nivel 1</td><td>125,03</td></tr>
                        <tr><td>Nivel 2</td><td>223,53</td></tr>
                    </table>
                </div>
            </div>
            <table style='width:400px; height:17px;'>
            <tr class="TBLFIN">
               <td></td>
            </tr>
            </table> 
        </td>
        <td rowspan="2">
            <table style="width:400px; margin-top:55px;">
                <colgroup>
                    <col style="width:350px;" />
                    <col style="width:50px;" />
                </colgroup>
            <tr id="trTituloPartidas" class="TBLINI">
                <td>Partidas económicas a presupuestar</td>
                <td align="center" title="Muestra las partidas seleccionadas en el escenario">Mostrar</td>
            </tr>
            </table>
            <div style="width:416px; height:400px; overflow:auto; overflow-x:hidden; ">
                <div id="divCatalogoPartidas" style="background-image: url('../../../../Images/imgFT20.gif'); background-repeat:repeat; width:400px; height:auto;" runat="server">
                    <table id='tblPartidas' style='width:400px;'> 
                        <colgroup>
                            <col style="width:350px;" />
                            <col style="width:50px;" />
                        </colgroup>
                        <tr><td>Nivel 1</td><td>125,03</td></tr>
                        <tr><td>Nivel 2</td><td>223,53</td></tr>
                    </table>
                </div>
            </div>
            <table style='width:400px; height:17px;'>
            <tr class="TBLFIN">
               <td></td>
            </tr>
            </table> 
        </td>
    </tr>
    <tr>
        <td>
            <table style="width:400px;">
                <colgroup>
                    <col style="width:300px;" />
                    <col style="width:100px;" />
                </colgroup>
                <tr>
                    <td colspan="2">
                        <fieldset style="width: 110px;">
			                <legend>Tarificación</legend>   
                            <asp:RadioButtonList ID="rdbTarificacion" SkinId="rbl" runat="server" RepeatColumns="2" onclick="if(!$I('rdbTarificacion_0').disabled){setTipoTarifa();}">
                                <asp:ListItem Value="H" style="cursor:pointer"><img src='../../../../Images/Botones/imgHorario.gif' border='0' title="Por horas" style="cursor:pointer" onclick="this.parentNode.click()">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="J" Selected="True" style="cursor:pointer"><img src='../../../../Images/Botones/imgCalendario.gif' border='0' title="Por jornadas" style="cursor:pointer" onclick="this.parentNode.click()"></asp:ListItem>
                            </asp:RadioButtonList>
                         </fieldset>
                    </td>
                </tr>
                <tr id="trTituloTarifa" class="TBLINI">
                    <td>Perfil</td>
                    <td>Tarifa</td>
                </tr>
             </table>
            <div id="divCatalogoTarifa" style="width:416px; height:150px; overflow:auto; overflow-x:hidden; " runat="server">
                <div style="background-image: url('../../../../Images/imgFT20.gif'); background-repeat:repeat; width:400px; height:auto;">
                    <table id='tblDatosTarifa' style='width:400px;'> 
                        <colgroup>
                            <col style="width:300px;" />
                            <col style="width:100px;" />
                        </colgroup>
                        <tr><td>Perfil 1</td><td>222,03</td></tr>
                        <tr><td>Perfil 2</td><td>433,53</td></tr>
                        <tr><td>Perfil 1</td><td>222,03</td></tr>
                        <tr><td>Perfil 2</td><td>433,53</td></tr>
                        <tr><td>Perfil 1</td><td>222,03</td></tr>
                        <tr><td>Perfil 2</td><td>433,53</td></tr>
                        <tr><td>Perfil 1</td><td>222,03</td></tr>
                        <tr><td>Perfil 2</td><td>433,53</td></tr>
                        <tr><td>Perfil 1</td><td>222,03</td></tr>
                        <tr><td>Perfil 2</td><td>433,53</td></tr>
                        <tr><td>Perfil 1</td><td>222,03</td></tr>
                        <tr><td>Perfil 2</td><td>433,53</td></tr>
                    </table>
                </div>
            </div>
            <table style='width:400px; height:17px;'>
            <tr class="TBLFIN">
               <td></td>
            </tr>
            </table> 
        </td>
    </tr>
</table>
<table id="tblBotonera" style="width:900px;" border="1px">
    <tr>
        <td align="left" style="padding-top:15px; padding-left:10px;">
            <button id="btnCancelar" type="button" onclick="Cancelar();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
                 onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../../images/botones/imgCancelar.gif" /><span>Cancelar</span>
            </button>
        </td>
        <td align="right" style="padding-top:5px;">
            <button id="btnSiguiente" type="button" onclick="Siguiente();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
                 onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../../images/botones/imgNuevo.gif" /><span>Siguiente</span>
            </button>
        </td>
    </tr>
</table>

<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<asp:TextBox ID="hdnIdProyectoSubNodo" runat="server" style="visibility:hidden" Text="" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</form>
</body>
</html>
