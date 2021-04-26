<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getECO.aspx.cs" Inherits="Capa_Presentacion_getECO" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Selección de desplazamiento ECO</title>
    <meta http-equiv='X-UA-Compatible' content='IE=edge' />
	<script language="JavaScript" src="../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../Javascript/boxover.js" type="text/Javascript"></script>
   	<script language="JavaScript" src="../Javascript/modal.js" type="text/Javascript"></script>
    <script language="JavaScript">   
    <!--        
        function init(){
            try{
                if (!mostrarErrores()) return;
                ocultarProcesando();

                if ($I("tblDatos") != null && $I("tblDatos").rows.length == 0) {
                    mmoff("War", "No dispone de desplazamientos ECO correspondientes a vehículo particular en las fechas indicadas.", 320, 10000, 45);
                }
                
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

	    function salir(){
	        //        window.returnValue = null;
	        //        window.close();
	        var returnValue = null;
	        modalDialog.Close(window, returnValue);
	    }
    -->
    </script>    
</head>
<body onload="init()" onunload="unload()">
<form id="Form1" name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando1" runat="server" />
<script type="text/Javascript">
    var strServer = "<% =Session["GVT_strServer"].ToString() %>";
    var intSession = <%=Session.Timeout%>;
</script>
<br />
<table style="width:766px; margin-left:10px;">
	<tr>
		<td>
			<table id="tblTitulo" class="MA" style="height:17px; width:750px;">
			    <colgroup>
                <col style='width:60px; padding-right:10px; text-align:right;' />
                <col style='width:220px;' />
                <col style='width:220px;' />
                <col style='width:100px;' />
                <col style='width:100px;' />
                <col style='width:50px; padding-right:2px; text-align:right;' />
			    </colgroup>
				<tr class="TBLINI">
					<td title="Referencia ECO">Ref.</td>
					<td>Destino</td>
					<td>Observaciones</td>
					<td>Ida</td>
					<td>Vuelta</td>
					<td>Nº usos</td>
				</tr>
			</table>
			<div id="divCatalogo" style="OVERFLOW-X: hidden; OVERFLOW: auto; WIDTH: 766px; HEIGHT: 280px;">
                <div style="background-image:url('<%=Session["GVT_strServer"] %>Images/imgFT20.gif');WIDTH: 750px;">
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
    <div class="W100" style="margin-top:20px">
        <button id="btnCancelar" type="button" onclick="salir();" class="btnH25W90" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../images/botones/imgCancelar.gif" /><span title="Cancelar">Cancelar</span></button>	
    </div>
</center>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</form>
</body>
</html>
