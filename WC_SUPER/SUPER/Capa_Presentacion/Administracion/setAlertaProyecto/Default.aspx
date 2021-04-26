<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Administracion_EstructuraOrg_Default" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<img id="imgPestHorizontalAux" src="../../../Images/imgPestHorizontal.gif" style="Z-INDEX: 0;position:absolute; left:40px; top:125px; cursor:pointer;" onclick="mostrarOcultarPestVertical()" />
<div id="divPestRetr" style="position:absolute; left:20px; top:125px; width:965px; height:120px; clip:rect(auto auto 0px auto)">
    <table style="width:960px; height:120px;" cellpadding="0" cellspacing="0" border="0">
        <tr> 
            <td background="../../../Images/Tabla/4.gif" width="6">&nbsp;</td>
            <td background="../../../Images/Tabla/5.gif" style="padding: 5px">
            <table id="tblCriterios" style="width: 950px; margin-top:0px;" cellpadding="2" cellspacing="1" border="0">
                <colgroup>
                <col style="width:80px;" />
                <col style="width:378px;" />
                <col style="width:75px;" />
                <col style="width:285px;" />
                <col style="width:132px;" />
                </colgroup>
                <tr>
                    <td>Proyecto</td>
                    <td><asp:TextBox ID="txtNumPE" style="width:60px;" Text="" SkinID="Numero" runat="server" onkeypress="if(event.keyCode==13){event.keyCode=0;buscar();}else{vtn2(event);setNumPE();}" />
                    <asp:TextBox ID="txtDesPE" style="width:272px;" Text="" MaxLength="70" runat="server" onkeypress="if(event.keyCode==13){buscar();event.keyCode=0;}else{setDesPE();}" /></td>
                    <td><asp:RadioButtonList ID="rdbTipoBusqueda" SkinId="rbli" runat="server" Height="20px" RepeatColumns="2" style="position:absolute; top:15px; left: 440px;">
                            <asp:ListItem Value="I"><img src='../../../Images/imgIniciaCon.gif' border='0' title="Inicia con" style="cursor:pointer"></asp:ListItem>
                            <asp:ListItem Selected="True" Value="C"><img src='../../../Images/imgContieneA.gif' border='0' title="Contiene" style="cursor:pointer"></asp:ListItem>
                        </asp:RadioButtonList></td>
                    <td align="center"> 
                        <button id="btnAlertas" type="button" title="Permite el acceso a la pantalla de alertas globales" onclick="getAlertasGlobales();" class="btnH25W110" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                            <span>Alertas globales</span>
                        </button>  
                    </td>
                    <td>        
                        <button id="btnObtener" style="margin-left:10px" type="button" onclick="buscar();" class="btnH25W85" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                            <img src="../../../images/imgObtener.gif" /><span>Obtener</span>
                        </button>
                    </td>
                </tr>
                <tr>
                    <td><label id="lblNodo" runat="server" class="texto">Nodo</label></td>
                    <td>
                        <asp:DropDownList ID="cboCR" runat="server" AppendDataBoundItems="true" style="width:340px;" onchange="borrarCatalogo();">
                            <asp:ListItem Value="" Text=""></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td><label style="vertical-align:middle;">Estado</label></td>
                    <td colspan="2"><asp:DropDownList id="cboEstado" runat="server" Width="100px">
                            <asp:ListItem Value="" Text=""></asp:ListItem>
                            <asp:ListItem Value="A" Text="Abierto" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="C" Text="Cerrado"></asp:ListItem>
                            <asp:ListItem Value="H" Text="Histórico"></asp:ListItem>
                            <asp:ListItem Value="P" Text="Presupuestado"></asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td><label id="lblCliente" class="enlace" onclick="getCliente()" onmouseover="mostrarCursor(this)">Cliente</label></td>
                    <td><asp:TextBox ID="txtDesCliente" style="width:335px;" Text="" readonly="true" runat="server" /><asp:TextBox ID="hdnIdCliente" style="width:1px;visibility:hidden" Text="" readonly="true" SkinID="Numero" runat="server" />
                    <img src='../../../Images/Botones/imgBorrar.gif' border='0' title="Borra el cliente" onclick="borrarCliente()" style="cursor:pointer; vertical-align:middle;"></td>
                    <td><label id="lblContrato" class="enlace" onclick="getContrato()">Contrato</label></td>
                    <td colspan="2"><asp:TextBox ID="txtIDContrato" style="width:60px;" Text="" readonly="true" SkinID="Numero" runat="server" />
                                    <asp:TextBox ID="txtDesContrato" style="width:307px;" Text="" readonly="true" runat="server" />&nbsp;
                                    <img src='../../../Images/Botones/imgBorrar.gif' border='0' title="Borra el contrato" onclick="borrarContrato()" style="cursor:pointer; vertical-align:bottom;"></td>
                </tr>
                <tr>
                    <td><label id="lblResponsable" class="enlace" onclick="getResponsable()" onmouseover="mostrarCursor(this)">Responsable</label></td>
                    <td><asp:TextBox ID="txtResponsable" style="width:335px;" Text="" readonly="true" runat="server" /><asp:TextBox ID="hdnIdResponsable" style="width:1px;visibility:hidden" Text="" readonly="true" SkinID="Numero" runat="server" />
                    <img src='../../../Images/Botones/imgBorrar.gif' border='0' title="Borra el responsable" onclick="borrarResponsable()" style="cursor:pointer; vertical-align:middle;"></td>
                    <td><label id="lblHorizontal" class="enlace" onclick="getHorizontal()">Horizontal</label></td>
                    <td colspan="2"><asp:TextBox ID="txtDesHorizontal" style="width:373px;" Text="" readonly="true" runat="server" /><asp:TextBox ID="hdnIdHorizontal" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" />
                     <img src='../../../Images/Botones/imgBorrar.gif' border='0' title="Borra el ámbito horizontal" onclick="borrarHorizontal()" style="cursor:pointer; vertical-align:middle;"></td>
                </tr>
            </table>
            </td>
            <td background="../../../Images/Tabla/6.gif" width="6">&nbsp;</td>
        </tr>
        <tr>
		    <td background="../../../Images/Tabla/1.gif" height="6" width="6">
		    </td>
            <td background="../../../Images/Tabla/2.gif" height="6">
            </td>
            <td background="../../../Images/Tabla/3.gif" height="6" width="6">
            </td>
        </tr>
    </table>
</div>
<table id="tblGeneral" style="width:991px;" cellpadding="0" cellspacing="0" border="0" runat="server">
<colgroup> 
    <col style="width:500px;" />
    <col style="width:491px;" />
</colgroup>
<tr>
    <td style="width:500px;"></td>
    <td style="width:491px; padding-bottom:3px;">
        <button id="btnActivar" type="button" title="Activa todas las alertas de los proyectos marcados" onclick="setAlertaTodos(1);" style="display:inline;" class="btnH25W90" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
            <img src="../../../images/botones/imgmarcar.gif" /><span>Activar</span>
        </button>  
        <button id="btnDesactivar" type="button" title="Desactiva todas las alertas de los proyectos marcados" onclick="setAlertaTodos(0);" style="display:inline; margin-left;40px;" class="btnH25W90" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
            <img src="../../../images/botones/imgdesmarcar.gif" /><span>Desactivar</span>
        </button>  
    </td>
</tr>
<tr>
    <td style="width:500px;">
        <table id="tblTitulo" style="WIDTH: 500px; BORDER-COLLAPSE: collapse; HEIGHT: 17px;" cellspacing="0" cellpadding="0" border="0">
            <colgroup>
            <col style="width:80px;" />
            <col style="width:420px;" />
            </colgroup>
            <tr class="TBLINI">
                <td>
                    <img src="../../../images/botones/imgmarcar.gif" onclick="mdTabla(1)" title="Marca todas las líneas para ser procesadas" style="cursor:pointer; margin-left:2px;" />
                    <img src="../../../images/botones/imgdesmarcar.gif" onclick="mdTabla(0)" title="Desmarca todas las líneas" style="cursor:pointer;" />   
                </td>
                <td style="padding-left:10px;">Denominación&nbsp;
                    <IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblBodyFijo',2,'divBodyFijo','imgLupa2');setScrollBuscar();"
					        height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
					<IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblBodyFijo',2,'divBodyFijo','imgLupa2', event);setScrollBuscar();"
					        height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1">
			    </td>
            </tr>
        </table>
    </td>
    <td style="width:491px;">
        <div id="divTituloMovil" style="width:471px; overflow:hidden; height:17px; background-image: url('../../../Images/fondoEncabezamientoListas.gif'); background-repeat:repeat;" runat="server">
        </div>
    </td>
</tr>
<tr>
    <td style="width:500px; vertical-align:top;">
        <div id="divBodyFijo" style="OVERFLOW:hidden; width:500px; height:500px;" runat="server">
        </div>
        <TABLE id="tblResultado" style="width:500px; height:17px;" >
            <TR class="TBLFIN">
                <TD>&nbsp;</TD>
            </TR>
        </TABLE>
    </td>
    <td style="width:491px;vertical-align:top;">
        <div id="divBodyMovil" style="width:487px; height:516px; overflow:auto; overflow-y:scroll;" runat="server" onscroll="setScroll()">
            <div style="background-image: url('../../../Images/imgFT20.gif'); background-repeat:repeat; width:1300px; height:auto;">
            <table id='tblBodyMovil' style='font-size:9pt; width:1300px;' cellpadding='0' cellspacing='0' border='0'>
            </table>
            </div>
        </div>
    </td>
</tr>
</table>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
<!--
    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();

            switch (strBoton) {
                case "grabar":
                    {
                        bEnviar = false;
                        setTimeout("grabar()", 100);
                        break;
                    }
            }
        }

        var theform = document.forms[0];
        theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
        theform.__EVENTARGUMENT.value = eventArgument;
        if (bEnviar) theform.submit();
    }
-->
</script>
</asp:Content>

