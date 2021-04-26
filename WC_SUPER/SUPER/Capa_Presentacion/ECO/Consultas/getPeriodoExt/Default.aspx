<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Selección de periodo</title>
	<meta http-equiv='X-UA-Compatible' content='IE=8' />
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesPestHorizontal.js" type="text/Javascript"></script>
    <script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body style="OVERFLOW: hidden; margin-left:10px;" onload="init()">
    <form id="form1" runat="server">
	<script type="text/javascript">
	<!--
        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";
	    var strEstructuraNodo = "<%=Estructura.getDefCorta(Estructura.sTipoElem.NODO) %>";
	-->
	</script>
<div id="divPestRetr" style="position:absolute; left:-190px; top:60px; width:160px; height:120px;">
    <table style="width:210px;">
    <colgroup>
        <col style="width:190px;" />
        <col style="width:20px;" />
    </colgroup>
    <tr>
        <td>
            <table class="texto" style="width:190px; height:120px;" cellpadding="0">
                <tr>
                    <td background="../../../../Images/Tabla/8.gif" height="6">
                    </td>
                    <td background="../../../../Images/Tabla/9.gif" height="6" width="6">
                    </td>
                </tr>
                <tr>
                    <td background="../../../../Images/Tabla/5.gif" style="padding: 5px">
                        <!-- Inicio del contenido propio de la página -->
                        <table class="texto" style="width:180px; table-layout:fixed;">
                        <colgroup>
                            <col style="width:80px;" />
                            <col style="width:100px;" />
                        </colgroup>
                        <tr style="height: 20px;">
                            <td>
                                <img src="../../../../Images/imgPeriodo1.gif" title="Año completo a seleccionar" style="vertical-align:middle; cursor:pointer;" onclick="this.nextSibling.click()" />
                                <input name="rdbCalendario" type="radio" value="1" style="vertical-align:middle; cursor:pointer;" 
                                    hidefocus="hidefocus" onclick="setCal(this.value)">
                            </td>
                            <td rowspan="5" style="vertical-align:middle; text-align:center;">
                                <span id="spanBotones" style="visibility:hidden; margin-right:8px;">
	                            <img name="btnAntReg" onclick="setAntSig('A')" style="cursor:pointer;" border="0" src="../../../../images/btnAntRegOn.gif" width="24" height="20">&nbsp;
	                            <img name="btnSigReg" onclick="setAntSig('S')" style="cursor:pointer;" border="0" src="../../../../images/btnSigRegOn.gif" width="24" height="20">
	                            </span>
                            </td>
                        </tr>
                        <tr style="height: 20px;">
                            <td><img src="../../../../Images/imgPeriodo2.gif" title="Mes completo a seleccionar" style="vertical-align:middle; cursor:pointer;" onclick="this.nextSibling.click()" /><input name="rdbCalendario" type="radio" value="2" style="vertical-align:middle; cursor:pointer;" hidefocus=hidefocus onclick="setCal(this.value)"></td>
                        </tr>
                        <tr style="height: 20px;">
                            <td><img src="../../../../Images/imgPeriodo3.gif" title="Desde enero hasta mes a seleccionar" style="vertical-align:middle; cursor:pointer;" onclick="this.nextSibling.click()" /><input name="rdbCalendario" type="radio" value="3" style="vertical-align:middle; cursor:pointer;" hidefocus=hidefocus onclick="setCal(this.value)"></td>
                        </tr>
                        <tr style="height: 20px;">
                            <td><img src="../../../../Images/imgPeriodo4.gif" title="Desde principio hasta mes a seleccionar" style="vertical-align:middle; cursor:pointer;" onclick="this.nextSibling.click()" /><input name="rdbCalendario" type="radio" value="4" style="vertical-align:middle; cursor:pointer;" hidefocus=hidefocus onclick="setCal(this.value)"></td>
                        </tr>
                        <tr style="height: 20px;">
                            <td><img src="../../../../Images/imgPeriodo5.gif" title="Desde principio hasta final" style="vertical-align:middle; cursor:pointer;" onclick="this.nextSibling.click()" /><input name="rdbCalendario" type="radio" value="5" style="vertical-align:middle; cursor:pointer;" hidefocus=hidefocus onclick="setCal(this.value)"></td>
                        </tr>
                        </table>
                        <!-- Fin del contenido propio de la página -->
                    </td>
                    <td background="../../../../Images/Tabla/6.gif" width="6">&nbsp;</td>
                </tr>
                <tr>
                    <td background="../../../../Images/Tabla/2.gif" height="6">
                    </td>
                    <td background="../../../../Images/Tabla/3.gif" height="6" width="6">
                    </td>
                </tr>
            </table>
        </td>
        <td><img src="../../../../Images/imgPestVertical4.gif" style="margin-top:10px;cursor:pointer;" onclick="javascript:mostrarOcultarPestHorizontal()" /></td>
    </tr>
    </table>
</div>
	<br />
	<img src="<%=Session["strServer"] %>Images/imgSeparador.gif" width="1px" height="1px" />
    <table border="0" class="texto" width="515px" style="margin-left:25px;" cellpadding="0" cellspacing="0">
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
            <td style="vertical-align:top; background-image: url(../../../../images/imgFondoCalendario.gif); background-repeat: no-repeat;">&nbsp;<select id="cboDesde" class=combo style="width:80px; position: absolute; top: 34px; left: 55px;" onchange="setMes('D')" runat="server">
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
                </select>&nbsp;&nbsp;&nbsp;&nbsp;<img src="../../../../Images/imgFlAnt.gif" onclick="setAno('D','A')" style="cursor: pointer; position: absolute; top: 38px; left: 165px;" border=0 />
                <asp:TextBox ID="txtDesde" style="width:30px; position: absolute; top: 36px; left: 180px;" readonly="true" runat="server" Text="" />
                <img src="../../../../Images/imgFlSig.gif" onclick="setAno('D','S')" style="cursor: pointer; position: absolute; top: 38px; left: 215px;" border=0 />
            </td>
            <td>&nbsp;</td>
            <td style="vertical-align:top; background-image: url(../../../../images/imgFondoCalendario.gif); background-repeat: no-repeat;"><select id="cboHasta" class=combo style="width:80px; position: absolute; top: 34px; left: 325px;" onchange="setMes('H')" runat="server">
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
                </select>&nbsp;&nbsp;&nbsp;&nbsp;<img src="../../../../Images/imgFlAnt.gif" onclick="setAno('H','A')" style="cursor: pointer; position: absolute; top: 38px; left: 435px;" border=0 />
                <asp:TextBox ID="txtHasta" style="width:30px; position: absolute; top: 36px; left: 450px;" readonly="true" runat="server" Text="" />
                <img src="../../../../Images/imgFlSig.gif" onclick="setAno('H','S')" style="cursor: pointer; position: absolute; top: 38px; left: 485px;" border=0 />
            </td>
        </tr>
    </table>
    <center>
		<table width="220px" align="center" style="margin-top:25px;">
			<tr>
				<td>
					<button id="btnAceptar" type="button" onclick="aceptar();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../../images/imgAceptar.gif" /><span title="Aceptar">Aceptar</span></button>								
				</td>
				<td>
					<button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../../images/imgCancelar.gif" /><span title="Cancelar">Cancelar</span></button>				
				</td>
			</tr>
		</table>
    </center>
    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    </form>
</body>
</html>
