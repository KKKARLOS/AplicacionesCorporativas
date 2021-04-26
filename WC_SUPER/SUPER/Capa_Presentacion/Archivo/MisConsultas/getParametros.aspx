<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getParametros.aspx.cs" Inherits="getParametros" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Parámetros de la consulta</title>
	<meta http-equiv='X-UA-Compatible' content='IE=8' />
    <link rel="stylesheet" href="../../../PopCalendar/CSS/Classic.css" type="text/css"/>
	<script language="JavaScript" src="../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../../PopCalendar/PopCalendar.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
    function init(){
        try{
            if (!mostrarErrores()) return;
            var nParametrosVisibles = 0;
            var aFila = FilasDe("tblDatos");
            for (var i=0; i<aFila.length; i++){
                if (aFila[i].style.display == "table-row")
                    nParametrosVisibles++;
            }
            ocultarProcesando();

            if (nParametrosVisibles==0) exportar();
            else{
                window.focus();
            }
        }catch(e){
            mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
        }
    }
	
    function exportar(){
	    try{
            if (bProcesando()) return;
            
            var sReturn = "";
            var aFila = FilasDe("tblDatos");
            for (var i=0; i<aFila.length; i++){
                sReturn += aFila[i].getAttribute("tipoparam") + "##";
                switch (aFila[i].getAttribute("tipoparam")) {
                    case "I":
                    case "M":
                        if (fTrim(aFila[i].cells[1].children[0].value) == "" && aFila[i].getAttribute("opcional") == 0) {
                            mmoff("War", "Es obligatorio asignar valor al parámetro \"" + aFila[i].cells[0].innerText + "\"", 400);
                            return;
                        }
                        sReturn += dfn(aFila[i].cells[1].children[0].value);
                        break;
                    case "V":
                    case "D":
                        if (fTrim(aFila[i].cells[1].children[0].value) == "" && aFila[i].getAttribute("opcional") == 0) {
                            mmoff("War", "Es obligatorio asignar valor al parámetro \"" + aFila[i].cells[0].innerText + "\"", 400);
                            return;
                        }
                        sReturn += aFila[i].cells[1].children[0].value;
                        break;
                    case "B":
                        sReturn += (aFila[i].cells[1].children[0].checked)? "1":"0";
                        break;
                    case "A":
                        if (fTrim(aFila[i].cells[1].children[0].value) == "" && aFila[i].getAttribute("opcional") == 0) {
                            mmoff("War", "Es obligatorio asignar valor al parámetro \"" + aFila[i].cells[0].innerText + "\"", 400);
                            return;
                        }
                        sReturn += DescLongToAnoMes(aFila[i].cells[1].children[0].value);
                        break;
                }
                sReturn += "///";
            }
            
            if (sReturn != "") sReturn = sReturn.substring(0, sReturn.length - 3);
	        var returnValue = sReturn;
	        modalDialog.Close(window, returnValue);
        }catch(e){
            mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
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
    
    function getMesValor(oControl){
        try{
            mostrarProcesando();
            //window.focus();
            modalDialog.Show(strServer + "Capa_Presentacion/ECO/getUnMes.aspx", self, sSize(270, 215))
                .then(function(ret) {
                if (ret != null) {
                    oControl.value = AnoMesToMesAnoDescLong(ret);
                }
            });

	        ocultarProcesando();
	    }catch(e){
		    mostrarErrorAplicacion("Error al obtener el nodo destino.", e.message);
        }
    }
    </script>
</head>
<body style="overflow: hidden" leftmargin="10" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
    <form id="form1" runat="server">
	<script type="text/javascript">
        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";
	</script>
    <table style="width:730px; margin-left:10px;text-align:left">
        <tr>
            <td>
                <table style="width: 700px; height: 17px; margin-top:10px; text-align:left">
                    <colgroup>
                        <col style='width:150px;' />
                        <col style='width:150px;' />
                        <col style='width:400px;' />
                    </colgroup>
                    <tr class="TBLINI">
                        <td>&nbsp;Parámetro</td>
				        <td>Valor</td>
				        <td>Comentario</td>
                    </tr>
                </table>
                <div id="divCatalogo" style="overflow: auto; overflow-x: hidden;  width: 716px; height:250px">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:700px">
                    <%=strTablaHTML %>
                    </div>
                </div>
                <table style="width: 700px; height: 17px;">
                    <tr class="TBLFIN">
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <center>
        <table width="300px" align="center" style="margin-top:5px;">
		    <tr>
			    <td align="center">
					<button id="btnExcel" type="button" onclick="exportar()" class="btnH25W95" runat="server" hidefocus="hidefocus" 
						 onmouseover="se(this, 25);mostrarCursor(this);">
						<img src="../../../images/botones/imgExcel.gif" /><span title="">&nbsp;&nbsp;Exportar</span>
					</button>				  
			    </td>
			    <td align="center">
					<button id="btnCancelar" type="button" onclick="cerrarVentana()" class="btnH25W95" runat="server" hidefocus="hidefocus" 
						 onmouseover="se(this, 25);mostrarCursor(this);">
						<img src="../../../images/botones/imgCancelar.gif" /><span title="">&nbsp;&nbsp;Cancelar</span>   
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
