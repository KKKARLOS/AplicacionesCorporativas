<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Selección de periodo</title>
    <meta http-equiv='X-UA-Compatible' content='IE=edge' />
	<script language="JavaScript" src="../../../Javascript/funciones.js" type="text/Javascript"></script>
    <script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../Javascript/modal.js" type="text/Javascript"></script>
    
</head>
<body style="OVERFLOW: hidden" leftMargin="10" onload="init()">
    <ucproc:Procesando ID="Procesando" runat="server" />
    <form id="form1" runat="server">
	<script language="Javascript">
	<!--
        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["GVT_strServer"]%>";
	-->
	</script>
	<img src="<%=Session["GVT_strServer"] %>Images/imgSeparador.gif" width="1px" height="1px" />
    <table style="margin-left:25px; width:515px;" cellpadding="8">
        <colgroup>
            <col style="width:250px;" />
            <col style="width:15px;" />
            <col style="width:250px;" />
        </colgroup>
        <tr style="text-align:center; font-weight: bold;">
            <td style="padding-bottom:5px;">Inicio</td>
            <td>&nbsp;</td>
            <td style="padding-bottom:5px;">Fin</td>
        </tr>
        <tr style="height:132px;">
            <td  style="background-image: url(../../../Images/imgFondoCalendario.gif); background-repeat: no-repeat;vertical-align: text-top;">&nbsp;<select id="cboDesde" class=combo style="width:80px; position: absolute; top: 34px; left: 55px;" onchange="setMes('D')" runat=server>
                <option value="1">Enero</option>
                <option value="2">Febrero</option>
                <option value="3">Marzo</option>
                <option value="4">Abril</option>
                <option value="5">Mayo</option>
                <option value="6">Junio</option>
                <option value="7">Julio</option>
                <option value="8">Agosto</option>
                <option value="9">Septiembre</option>
                <option value="10">Octubre</option>
                <option value="11">Noviembre</option>
                <option value="12">Diciembre</option>
                </select>&nbsp;&nbsp;&nbsp;&nbsp;<img src="../../../Images/imgFlAnt.gif" onclick="setAno('D','A')" style="cursor: pointer; position: absolute; top: 38px; left: 165px;" border=0 />
                <asp:TextBox ID="txtDesde" style="width:30px; position: absolute; top: 36px; left: 180px;" ReadOnly=true runat="server" Text="" />
                <img src="../../../Images/imgFlSig.gif" onclick="setAno('D','S')" style="cursor: pointer; position: absolute; top: 38px; left: 215px;" border=0 />
            </td>
            <td>&nbsp;</td>
            <td style="background-image: url(../../../Images/imgFondoCalendario.gif); background-repeat: no-repeat;vertical-align: text-top;"><select id="cboHasta" class=combo style="width:80px; position: absolute; top: 34px; left: 325px;" onchange="setMes('H')" runat=server>
                <option value="1">Enero</option>
                <option value="2">Febrero</option>
                <option value="3">Marzo</option>
                <option value="4">Abril</option>
                <option value="5">Mayo</option>
                <option value="6">Junio</option>
                <option value="7">Julio</option>
                <option value="8">Agosto</option>
                <option value="9">Septiembre</option>
                <option value="10">Octubre</option>
                <option value="11">Noviembre</option>
                <option value="12">Diciembre</option>
                </select>&nbsp;&nbsp;&nbsp;&nbsp;<img src="../../../Images/imgFlAnt.gif" onclick="setAno('H','A')" style="cursor: pointer; position: absolute; top: 38px; left: 435px;" border=0 />
                <asp:TextBox ID="txtHasta" style="width:30px; position: absolute; top: 36px; left: 450px;" ReadOnly=true runat="server" Text="" />
                <img src="../../../Images/imgFlSig.gif" onclick="setAno('H','S')" style="cursor: pointer; position: absolute; top: 38px; left: 485px;" border=0 />
            </td>
        </tr>
    </table>
    <center>
        <table style="margin-top:30px; width:200px;">
		<tr>
			<td>
                <button id="btnAceptar" type="button" onclick="aceptar();" class="btnH25W85" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../images/botones/imgAceptar.gif" /><span title="Aceptar">Aceptar</span></button>
			</td>
			<td>
                <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../images/botones/imgCancelar.gif" /><span title="Cancelar">Cancelar</span></button>
			</td>
		</tr>
    </table>
    </center>
    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
    </form>
</body>
</html>
