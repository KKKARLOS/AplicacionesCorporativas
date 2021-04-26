<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_BonoTransporte_BonoNuevo_Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Import Namespace="GASVI.BLL" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head2" runat="server">
    <title> ::: GASVI 2.0 ::: - Detalle de bono transporte</title>
    <meta http-equiv='X-UA-Compatible' content='IE=edge' />
    <link rel="stylesheet" href="../../css/Mantenimiento.css" type="text/css" />
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
</head>
<body onload="init()" onunload="unload()" style="margin-top:10px">
    <form id="Form2" name="frmPrincipal" runat="server">
        <div id="procesando" class="procesando">
            <asp:Image ID="imgProcesando" runat="server" Height="33px" Width="152px" ImageUrl="~/images/imgProcesando.gif" />
            <div id="reloj" class="reloj">
                <asp:Image ID="Image3" runat="server" Height="32px" Width="32px" ImageUrl="~/images/imgRelojAnim.gif" />
            </div>
        </div>
        <script type="text/javascript" language="JavaScript">
        <!--
            var strServer = "<%=Session["GVT_strServer"].ToString() %>";
            var intSession = <%=Session.Timeout%>;
        -->
        </script> 
        <table style="width:300px; margin-top:10px; margin-left:10px;" cellpadding="5">
            <colgroup>
                <col style="width:90px" />
                <col style="width:210px" />
            </colgroup>
            <tr>
                <td>Denominación</td>
                <td><asp:TextBox ID="txtDenominacion" runat="server" MaxLength="25" style="width:190px" /></td>
            </tr>
            <tr>
                <td>Moneda</td>
                <td><asp:DropDownList ID="cboMoneda" runat="server" style="width:190px" AppendDataBoundItems="true"></asp:DropDownList></td>
            </tr>
            <tr>
                <td></td>
                <td>
                <fieldset style="cursor:pointer; width:170px;">
				    <legend>Estado</legend>
                    <asp:RadioButtonList ID="rdbEstado" style="cursor:pointer; width:170px; margin-left:10px;" SkinID="Radio" runat="server" RepeatColumns="2">
                        <asp:ListItem Value="A" Selected="True">Activo</asp:ListItem>
                        <asp:ListItem Value="B">Bloqueado</asp:ListItem>
                    </asp:RadioButtonList>
                </fieldset>
                </td>
            </tr>
            <tr>
                <td colspan="2">Descripción<br />
                    <asp:TextBox ID="txtDescripcion" Rows="15" Wrap="true" runat="server" TextMode="MultiLine" SkinID="Multi" Width="280px" Height="180px" />
                </td>
            </tr>
        </table>
        <center>
            <table style="width:200px; margin-top:20px">
            <tr>
                <td>
                    <button id="btnAceptar" type="button" onclick="aceptar();" class="btnH25W85" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../../images/botones/imgAceptar.gif" /><span title="Aceptar">Aceptar</span></button>	
                </td>
                <td>
                    <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../../images/botones/imgCancelar.gif" /><span title="Cancelar">Cancelar</span></button>
                </td>
            </tr>
        </table>
        </center>
        <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
        <asp:TextBox ID="hdnIdBono" style="width:5px; visibility:hidden;" Text="" ReadOnly="true" runat="server" />    
        <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    </form>
</body>
</html>
