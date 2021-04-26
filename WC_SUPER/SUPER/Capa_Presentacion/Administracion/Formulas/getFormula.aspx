<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getFormula.aspx.cs" Inherits="getFormula" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Fórmula "<% =sTitulo %>"</title>
	<meta http-equiv='X-UA-Compatible' content='IE=8' />
	<script language="JavaScript" src="../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
    function init(){
        try{
            if (!mostrarErrores()) return;

            setExcelImg("imgExcel", "divCatalogo");
            ocultarProcesando();
        }catch(e){
            mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
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
    
    function excel(){
        try{
            if ($I("tblDatos")==null){
                ocultarProcesando();
                mmoff("Inf","No hay información en pantalla para exportar.", 300);
                return;
            }

            var sb = new StringBuilder;

            sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='1' border=1>");
		    sb.Append("	<colgroup>");
		    sb.Append("        <col style='width:auto;' />");
		    sb.Append("        <col style='width:auto;' />");
		    sb.Append("        <col style='width:auto;' />");
		    sb.Append("	</colgroup>");
		    sb.Append("	<TR>");
            sb.Append("        <td colspan='3' style='width:600px;background-color: #BCD4DF;text-align:center'>"+ fTrim(document.title) +"</TD>");
		    sb.Append("	</TR>");
		    sb.Append("	<TR>");
            sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Grupo económico</TD>");
            sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Subrupo económico</TD>");
            sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Concepto económico</TD>");
		    sb.Append("	</TR>");


            for (var i=0;i < $I("tblDatos").rows.length; i++){
                if ($I("tblDatos").rows[i].getAttribute("tipo") != 'C')
                    sb.Append($I("tblDatos").rows[i].outerHTML);
                else{
                    sb.Append("<tr>");
                    sb.Append($I("tblDatos").rows[i].cells[0].outerHTML);
                    sb.Append($I("tblDatos").rows[i].cells[1].outerHTML);
                    switch ($I("tblDatos").rows[i].getAttribute("coef")) {
                        case "1": sb.Append("<td style='color:green; font-weight:bold;'>" + $I("tblDatos").rows[i].cells[2].innerText +" (1)</td>"); break;
                        case "-1": sb.Append("<td style='color:red; font-weight:bold;'>" + $I("tblDatos").rows[i].cells[2].innerText + " (-1)</td>"); break;
                        case "0": sb.Append("<td style='color:gray;'>" + $I("tblDatos").rows[i].cells[2].innerText +"</td>"); break;
                    }
                    sb.Append("</tr>");
                }
            }

	        sb.Append("</table>");
    	    
            crearExcel(sb.ToString());
            var sb = null;
	    }catch(e){
		    mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
        }
    }
    </script>
</head>
<body style="OVERFLOW: hidden; margin-left:10px;" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
    <form id="form1" runat="server">
	<script type="text/javascript">
        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";
	</script>
	<img src="<%=Session["strServer"] %>Images/imgSeparador.gif" width="1px" height="1px" />
    <table style="margin-left:10px;width:720px" align="center" cellpadding="5">
        <tr>
            <td>
                <TABLE id="tblTitulo" style="width: 700px; height: 17px">
                    <colgroup>
                    <col style='width:145px;' />
                    <col style='width:250px;' />
                    <col style='width:305px;' />
                    </colgroup>
                    <tr class="TBLINI">
                        <td style='padding-left:3px;'>Grupo económico</td>
                        <td>Subrupo económico</td>
                        <td>Concepto económico</td>
                    </tr>
                </TABLE>
                <DIV id="divCatalogo" style="OVERFLOW: auto; WIDTH: 716px; height:480px;">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:700px;">
                    <%=strTablaHTML %>
                    </div>
                </DIV>
                <TABLE style="width: 700px; height: 17px">
                    <TR class="TBLFIN">
                        <TD></TD>
                    </TR>
                </TABLE>
            </td>
        </tr>
    </table>
<br /><br />
<center>
    <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../images/imgCancelar.gif" /><span>Cancelar</span></button>
</center>    	

    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    </form>
</body>
</html>
