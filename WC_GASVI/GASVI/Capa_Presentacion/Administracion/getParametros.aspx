<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getParametros.aspx.cs" Inherits="getParametros" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Parámetros de la consulta</title>
        <meta http-equiv='X-UA-Compatible' content='IE=edge'/>
        <link rel="stylesheet" href="../../PopCalendar/CSS/Classic.css" type="text/css" />
	    <script language="JavaScript" src="../../Javascript/fechas.js" type="text/Javascript"></script>
	    <script language="JavaScript" src="../../Javascript/funciones.js" type="text/Javascript"></script>
	    <script language="JavaScript" src="../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
        <script language="JavaScript" src="../../PopCalendar/PopCalendar.js" type="text/Javascript"></script>
 	    <script language="JavaScript" src="../../Javascript/modal.js" type="text/Javascript"></script>
	    <script language="Javascript" type="text/javascript">
	    <!--
        document.onkeydown = function() {
            if (event.keyCode == 13) {
                event.keyCode = 0;
                return false;
            }
        }
	    
        function init(){
            try{
                if (!mostrarErrores()) return;
                var nParametrosVisibles = 0;
                var aFila = FilasDe("tblDatos");
                for (var i=0; i<aFila.length; i++){
                    if (aFila[i].style.display != "none")
                        nParametrosVisibles++;
                }
                ocultarProcesando();

                if (nParametrosVisibles == 0) exportar();
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
                    switch (aFila[i].getAttribute("tipoparam")){
                        case "I":
                        case "M":
                            if (fTrim(aFila[i].cells[1].children[0].value) == "" && aFila[i].getAttribute("opcional") == 0){
                                alert("Es obligatorio asignar valor al parámetro \"" + aFila[i].cells[0].innerText + "\"");
                                return;
                            }
                            sReturn += dfn(aFila[i].cells[1].children[0].value);
                            break;
                        case "V":
                        case "D":
                            if (fTrim(aFila[i].cells[1].children[0].value) == "" && aFila[i].getAttribute("opcional") == 0){
                                alert("Es obligatorio asignar valor al parámetro \"" + aFila[i].cells[0].innerText + "\"");
                                return;
                            }
                            sReturn += aFila[i].cells[1].children[0].value;
                            break;
                        case "B":
                            sReturn += (aFila[i].cells[1].children[0].checked) ? "1" : "0";
                            break;
                        case "A":
                            if (fTrim(aFila[i].cells[1].children[0].value) == "" && aFila[i].getAttribute("opcional") == 0){
                                alert("Es obligatorio asignar valor al parámetro \"" + aFila[i].cells[0].innerText + "\"");
                                return;
                            }
                            sReturn += DescLongToAnoMes(aFila[i].cells[1].children[0].value);
                            break;
                    }
                    sReturn += "///";
                }
                
                if (sReturn != "") sReturn = sReturn.substring(0, sReturn.length - 3);
//	            window.returnValue = sReturn;
//	            window.close();
                var returnValue = sReturn;
                modalDialog.Close(window, returnValue);	            
            }catch(e){
                mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
            }
        }
        function cerrarVentana(){
	        try{
                if (bProcesando()) return;
                
//	            window.returnValue = null;
//	            window.close();
                var returnValue = null;
                modalDialog.Close(window, returnValue);
            }catch(e){
                mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
            }
        }
        
        function getMesValor(oControl){
            try{
                mostrarProcesando();
                var sTamaino = sSize(270, 215);
//                  var ret = window.showModalDialog(strServer +  "Capa_Presentacion/getUnMes.aspx", self, sTamaino);
//                  window.focus();
//                  alert(ret);

                modalDialog.Show(strServer + "Capa_Presentacion/getUnMes.aspx", self, sTamaino)
                    .then(function(ret) {
                        if (ret != null) {
                            oControl.value = AnoMesToMesAnoDescLong(ret);
                        }
                        ocultarProcesando();
                    }); 	            	            	           
	        }catch(e){
		        mostrarErrorAplicacion("Error al obtener el mes.", e.message);
            }
        }
            
	    -->
        </script>
    </head>
    <body style="overflow:hidden; margin-left:10px;" onload="init()">
        <ucproc:Procesando ID="Procesando" runat="server" />
        <form id="form1" runat="server">
	        <script language="Javascript" type="text/javascript">
	        <!--
                var intSession = <%=Session.Timeout%>; 
	            var strServer = "<%=Session["GVT_strServer"]%>";
	        -->
	        </script>
            <table width="730px" style="margin-left:10px;">
                <tr>
                    <td>
                        <table style="width:700px; height:17px; margin-top:10px;">
                            <colgroup>
                                <col style="width:150px;" />
                                <col style="width:150px;" />
                                <col style="width:400px;" />
                            </colgroup>
                            <tr class="TBLINI">
                                <td>&nbsp;Parámetro</td>
				                <td>Valor</td>
				                <td>Comentario</td>
                            </tr>
                        </table>
                        <div id="divCatalogo" style="overflow-x:hidden; overflow:auto; width:716px; height:250px">
                            <div style="background-image:url('<%=Session["GVT_strServer"] %>Images/imgFT20.gif');  width:700px;">
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
            <center>
                <table width="300px" style="margin-top:5px;text-align:left">
		            <tr>
			            <td style="text-align:center">
                            <button id="btnExcel" type="button" onclick="exportar();" class="btnH25W85" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../images/botones/imgExcel.gif" /><span title="Exportar">Exportar</span></button>								
			            </td>
			            <td style="text-align:center">
			                <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../Images/Botones/imgCancelar.gif" /><span title="Cancelar">Cancelar</span></button>								
			            </td>
		            </tr>
                </table>
            </center>
            <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
        </form>
    </body>
</html>
