<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getUnMes.aspx.cs" Inherits="getUnMes" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> ::: SUPER ::: - Selección de mes</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script language="JavaScript" src="../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
	<!--
    function init(){
        try{
            if (!mostrarErrores()) return;
            window.focus();
       }catch(e){
            mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
        }
    }
	
    function aceptar(){
	    try{
	        var returnValue = parseInt($I("txtAnno").value, 10) * 100 + parseInt($I("cboMes").value, 10);
	        modalDialog.Close(window, returnValue);	
        }catch(e){
            mostrarErrorAplicacion("Error al aceptar", e.message);
        }
    }
	
    function setAnno(sOpcion){
	    try{
            if (sOpcion == "A") $I("txtAnno").value = parseInt($I("txtAnno").value, 10) - 1;
            else  $I("txtAnno").value = parseInt($I("txtAnno").value, 10) + 1;
        }catch(e){
            mostrarErrorAplicacion("Error al seleccionar el año", e.message);
        }
    }
	
    function cerrarVentana(){
	    try{

	        var returnValue = null;
	        modalDialog.Close(window, returnValue);	
        }catch(e){
            mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
        }
    }
	-->
    </script>
</head>
<body style="overflow:hidden;" onload="init()">
<form id="form1" runat="server">
<script type="text/javascript">
<!--
    var intSession = <%=Session.Timeout%>; 
    var strServer = "<%=Session["strServer"]%>";
-->
</script>
<table style="width:250px; margin-top:10px; margin-left:13px;">
    <colgroup><col style="width:250px;" /></colgroup>
    <tr style="height:135px;">
        <td style="vertical-align:top; background-image: url(../../images/imgFondoCalendario.gif); background-repeat: no-repeat;">&nbsp;<select id="cboMes" class=combo style="width:80px; position: absolute; top: 15px; left: 50px;" runat="server" onchange="window.focus();">
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
            </select>&nbsp;&nbsp;&nbsp;&nbsp;<img src="../../Images/imgFlAnt.gif" onclick="setAnno('A')" style="cursor: pointer; position: absolute; top: 20px; left: 150px;" border=0 />
            <asp:TextBox ID="txtAnno" style="width:30px; position: absolute; top: 17px; left: 165px;" readonly="true" runat="server" Text="" />
            <img src="../../Images/imgFlSig.gif" onclick="setAnno('S')" style="cursor: pointer; position: absolute; top: 20px; left: 200px;" border=0 />
        </td>
    </tr>
</table>
<table style="width:220px; margin-left:35px; margin-top:15px; text-align:left;" >
	<tr>
		<td>
            <button id="btnAceptar" type="button" onclick="aceptar();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
                 onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../images/imgAceptar.gif" /><span>Aceptar</span>
            </button>
		</td>
    	<td>
            <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
                 onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../images/imgCancelar.gif" /><span>Cancelar</span>
            </button>
		</td>
	</tr>
</table>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</form>
</body>
</html>
