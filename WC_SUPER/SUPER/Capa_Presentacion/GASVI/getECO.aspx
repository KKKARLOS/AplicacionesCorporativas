<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getECO.aspx.cs" Inherits="Capa_Presentacion_getECO" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Selección de desplazamiento ECO</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script language="JavaScript" src="../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../Javascript/boxover.js" type="text/Javascript"></script>
   	<script language="JavaScript" src="../../Javascript/modal.js" type="text/Javascript"></script>
    <script type="text/javascript">   
    <!--        
        function init(){
            try{
                if (!mostrarErrores()) return;
                ocultarProcesando();
	        }catch(e){
		        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
            }
        }
    
        function aceptarClick(oControl){
            try{
                if (bProcesando()) return;
                
                var oFila;
                while (oControl != document.body){
                    if (oControl.tagName.toUpperCase() == "TR"){
                        oFila = oControl;
                        break;
                    }
                    oControl = oControl.parentNode;
                }
              
                var returnValue = oFila.id + "@#@" + oFila.cells[1].innerText + "@#@" + oFila.cells[3].innerText + "@#@" + oFila.cells[4].innerText;
                modalDialog.Close(window, returnValue);
            }catch(e){
                mostrarErrorAplicacion("Error seleccionar la fila", e.message);
            }
        }
    	
	    function cerrarVentana(){
		    var returnValue = null;
		    modalDialog.Close(window, returnValue);
	    }
    -->
    </script>    
</head>
<body onload="init()" onunload="unload()">
<form id="Form1" name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando1" runat="server" />
<script type="text/javascript">
<!--
    var strServer = "<% =Session["strServer"].ToString() %>";
    var intSession = <%=Session.Timeout%>;
-->
</script>
<br />
<table style="width:766px; margin-left:10px;" class="texto">
	<tr>
		<td>
			<table id="tblTitulo" class="MA" style="height:17px; width:750px;">
			    <colgroup>
                <col style='width:60px;' />
                <col style='width:220px;' />
                <col style='width:225px;' />
                <col style='width:95px;' />
                <col style='width:100px;' />
                <col style='width:50px;' />
			    </colgroup>
				<tr class="TBLINI">
					<td title="Referencia ECO" style="padding-right:10px; text-align:right;">Ref.</td>
					<td>Destino</td>
					<td>Observaciones</td>
					<td>Ida</td>
					<td>Vuelta</td>
					<td style="padding-right:2px; text-align:right;">Nº usos</td>
				</tr>
			</table>
			<div id="divCatalogo" style="OVERFLOW: auto; OVERFLOW-X: hidden; WIDTH: 766px; HEIGHT: 280px;">
                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:750px; height:auto">
                <%=strTablaHTML%>
                </div>
            </div>
            <table id="tblResultado" style="height:17px; width:750px;">
				<tr class="TBLFIN">
					<td>&nbsp;</td>
				</tr>
			</table>
		</td>
    </tr>
</table>
<center>
<table style="width:100px; margin-top:10px;">
    <tr> 
        <td>
            <button id="btnCancelar" type="button" onclick="salir();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
                 onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../images/imgCancelar.gif" /><span>Cancelar</span>
            </button>
        </td>
    </tr>
</table>
</center>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</form>
</body>
</html>
