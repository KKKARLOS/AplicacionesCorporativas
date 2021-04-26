<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getVisionProf.aspx.cs" Inherits="getVisionProf" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title> ::: SUPER ::: - Relación de profesionales con visión sobre el proyecto</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script language="JavaScript" src="../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
  	<script language="JavaScript" src="../../../Javascript/boxover.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
	<!--
    function init(){
        try{
            if (!mostrarErrores()) return;
            //actualizarLupas("tblTitulo", "tblDatos");
            window.focus();
            ocultarProcesando();
        }catch(e){
            mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
        }
    }
	
    function aceptarClick(indexFila){
	    try{
            if (bProcesando()) return;
            
	        var returnValue = tblDatos.rows[indexFila].id + "@#@" + tblDatos.rows[indexFila].innerText;
	        modalDialog.Close(window, returnValue);	
        }catch(e){
            mostrarErrorAplicacion("Error seleccionar la fila", e.message);
        }
    }
	
    function cerrarVentana(){
	    try{
            if (bProcesando()) return;

            var returnValue = null;
            modalDialog.Close(window, returnValue);	
        }catch(e){
            mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
        }
    }
    function setModulo(){
        try {
            var tblDatos = $I("tblDatos");
	        if (tblDatos.rows.length == 0) return;
	        
	        var bPGE = $I('chkPGE').checked;
	        var bPST = $I('chkPST').checked;
	        
            if (!$I('chkPGE').checked){
                setOp($I("imgPGE"), 20);
                $I("imgPGE").style.cursor="pointer";
            }else{
                setOp($I("imgPGE"), 100);
            }

            if (!$I('chkPST').checked){
                setOp($I("imgPST"), 20);
                $I("imgPST").style.cursor="pointer";
            }else{
                setOp($I("imgPST"), 100);
            }

            for (var i=0;i<tblDatos.rows.length;i++){
                if (tblDatos.rows[i].getAttribute("bit")=="1") continue;
                if ((bPGE && tblDatos.rows[i].getAttribute("pge") == "1")
                    || (bPST && tblDatos.rows[i].getAttribute("pst")=="1"))
                     tblDatos.rows[i].style.display = "";
                else
                    tblDatos.rows[i].style.display = "none";
            }

            window.focus();
        }catch(e){
            mostrarErrorAplicacion("Error al establecer la visibilidad de las filas.", e.message);
        }
    }

    -->
    </script>
</head>
<body style="overflow: hidden; margin-left:10px; margin-top:10px;" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
<form id="form1" runat="server">
<script type="text/javascript">
<!--
    var intSession = <%=Session.Timeout%>; 
    var strServer = "<%=Session["strServer"]%>";
-->
</script>
<style>
#tblDatos tr { height: 20px; }
#tblDatos td { padding: 0px 2px 0px 2px;}
</style>
<table style="width:370px;" cellpadding="5px">
    <tr>
        <td>
        <FIELDSET id="fstEstado" style="width: 150px; text-align:left;">
            <LEGEND>Criterio de búsqueda</LEGEND>   
        	<asp:CheckBox id="chkPGE" runat="server" style="vertical-align:middle;" onclick="if (!this.checked && !$I('chkPST').checked){$I('chkPST').click();}setModulo();" checked /><img id="imgPGE" src='../../../Images/imgPGEon.gif' style='width:32px;height:32px; cursor:pointer;vertical-align:middle;' onclick="$I('chkPGE').click()" />
			<asp:CheckBox id="chkPST" runat="server" style="vertical-align:middle; margin-left:30px;" onclick="if (!this.checked && !$I('chkPGE').checked){$I('chkPGE').click();}setModulo();" checked /><img id="imgPST" src='../../../Images/imgPSTon.gif' style='width:32px;height:32px; margin-left:5px; cursor:pointer;vertical-align:middle;' onclick="$I('chkPST').click()" />
		</FIELDSET>
	    </td>
	</tr>
	<tr>
        <td>
	        <table style="width:700px; height:17px;">
            <colgroup>
                <col style='width:440px;' />
                <col style='width:60px;' />
                <col style='width:50px;' />
                <col style='width:50px;' />
                <col style='width:50px;' />
                <col style='width:50px;' />
            </colgroup>
                <tr style="height:20px;">
                    <td colspan="2"></td>
                    <td colspan="2" style='text-align:center;' class="colTabla1">PGE</td>
                    <td colspan="2"style='text-align:center;'  class="colTabla1">PST</td>
                </tr>
		        <tr class="TBLINI">
		            <td>&nbsp;Profesional</td>
		            <td style="padding-right:5px;">Bitácora</td>
		            <td>Modo</td>
		            <td>Ámbito</td>
		            <td style='text-align:center;'>Modo</td>
		            <td style='text-align:center;'>Ámbito</td>
		        </tr>
	        </table>
            <div id="divCatalogo" style="overflow:auto; overflow-x:hidden; width:716px; height:360px">
                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:700px;">
                <%=strTablaHTML %>
                </div>
            </div>
            <table style="width:700px; height:17px;">
                <tr class="TBLFIN">
                    <td></td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<table style="width:700px" cellspacing="2px" >
    <colgroup>
        <col style="width:95px" />
        <col style="width:95px" />
        <col style="width:510px" />
    </colgroup>
	  <tr> 
	    <td colspan="2"><img class="ICO" src="../../../Images/botones/imgBitacora.gif" />Acceso a Bitácora</td>
		<td>
			<button id="btnCancelar" type="button" onclick="salir();" class="btnH25W90" style="margin-left:150px;" runat="server" 
			    hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../images/botones/imgSalir.gif" /><span title="Salir">&nbsp;&nbsp;Salir</span>
			</button>	 
		</td>
	  </tr>
	  <tr><td><img class="ICO" src="../../../Images/imgAccesoW.gif" />Escritura</td>
            <td><img class="ICO" src="../../../Images/imgAccesoR.gif" />Lectura</td>
            <td><img class="ICO" src="../../../Images/imgAccesoN.gif" />Sin acceso</td>
      </tr>
	  <tr>
	    <td style="vertical-align:top; padding-top:5px;"><img class="ICO" src="../../../Images/imgVisionCompleta.gif" />Completo</td>
        <td style="padding-top:5px;"><img class="ICO" src="../../../Images/imgVisionRestringida.gif" title='Proyecto cerrado' />Restringido</td>
        <td style="padding-top:5px;"><img class="ICO" src="../../../Images/imgVisionNula.gif" title='Proyecto histórico' />Sin acceso</td>
      </tr>
</table>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</form>
</body>
</html>
