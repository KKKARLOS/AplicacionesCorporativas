<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getLOTE.aspx.cs" Inherits="Capa_Presentacion_getECO" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
                
                setTotales();
                ocultarProcesando();
	        }catch(e){
		        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
            }
        }
    
	    function cerrarVentana(){
	        //        window.returnValue = null;
	        //        window.close();
	        var returnValue = null;
	        modalDialog.Close(window, returnValue);
	    }
	    
        function setTotales(){
            try{
                var nPeajes=0, nComidas=0, nTransporte=0, nHoteles=0;
                
                for (var i=0; i<tblDatos.rows.length; i++){
                    nPeajes += getFloat(tblDatos.rows[i].cells[1].innerText);
                    nComidas += getFloat(tblDatos.rows[i].cells[2].innerText);
                    nTransporte += getFloat(tblDatos.rows[i].cells[3].innerText);
                    nHoteles += getFloat(tblDatos.rows[i].cells[4].innerText);
                }
                $I("cldPeajes").innerText = nPeajes.ToString("N");
                $I("cldComidas").innerText = nComidas.ToString("N");
                $I("cldTransporte").innerText = nTransporte.ToString("N");
                $I("cldHoteles").innerText = nHoteles.ToString("N");
	        }catch(e){
		        mostrarErrorAplicacion("Error al calcular los totales.", e.message);
	        }
        }
    -->
    </script>    
</head>
<body onLoad="init()" onunload="unload()">
<form id="Form1" name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando1" runat="server" />
<script language="JavaScript">
<!--
    var strServer = "<% =Session["GVT_strServer"].ToString() %>";
    var intSession = <%=Session.Timeout%>;
-->
</script>
<br />
<table style="width:766px; margin-left:10px;">
	<tr>
		<td>
			<table id="tblTitulo" style="height:17px; width:750px;">
			    <colgroup>
                <col style='width:70px; padding-right:2px; text-align:right;' />
                <col style='width:70px; padding-right:2px; text-align:right;' />
                <col style='width:70px; padding-right:2px; text-align:right;' />
                <col style='width:70px; padding-right:2px; text-align:right;' />
                <col style='width:70px; padding-right:2px; text-align:right;' />
                <col style='width:100px; padding-left:5px;' />
                <col style='width:330px;' />
			    </colgroup>
				<tr class="TBLINI">
					<td>Referencia</td>
                    <td>Peajes</td>
                    <td>Comidas</td>
                    <td>Transporte</td>
                    <td>Hoteles</td>
					<td>Estado</td>
					<td>Aceptada por</td>
				</tr>
			</table>
			<div id="divCatalogo" style="OVERFLOW-X: hidden; OVERFLOW: auto; WIDTH: 766px; HEIGHT: 280px;">
                <div style="background-image:url('<%=Session["GVT_strServer"] %>Images/imgFT20.gif'); WIDTH: 750px;">
                <%=strTablaHTML%>
                </div>
            </div>
            <table id="tblResultado" style="height:17px; width:750px;">
			    <colgroup>
                <col style='width:70px; padding-left:2px;' />
                <col style='width:70px; padding-right:2px; text-align:right;' />
                <col style='width:70px; padding-right:2px; text-align:right;' />
                <col style='width:70px; padding-right:2px; text-align:right;' />
                <col style='width:70px; padding-right:2px; text-align:right;' />
                <col style='width:100px; padding-left:5px;' />
                <col style='width:330px;' />
			    </colgroup>
				<tr class="TBLINI">
					<td>Total</td>
                    <td id="cldPeajes"></td>
                    <td id="cldComidas"></td>
                    <td id="cldTransporte"></td>
                    <td id="cldHoteles"></td>
					<td></td>
					<td></td>
				</tr>
			</table>
		</td>
    </tr>
</table>
<table style="width:750px; margin-top:10px;text-align:center">
    <colgroup>
        <col style="width:750px" />
    </colgroup>
    <tr> 
        <td style="text-align:center">
            <button id="btnCancelar" type="button" onclick="salir();" class="btnH25W90" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../images/botones/imgCancelar.gif" /><span title="Cancelar">Cancelar</span></button>	
        </td>
    </tr>
</table>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</form>
</body>
</html>
