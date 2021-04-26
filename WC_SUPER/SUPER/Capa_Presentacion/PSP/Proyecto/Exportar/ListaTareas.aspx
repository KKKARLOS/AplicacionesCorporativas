<%@ Page Language="c#" CodeFile="ListaTareas.aspx.cs" Inherits="SUPER.Capa_Presentacion.ListaTareas" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title> ::: SUPER ::: - Tareas con FFPR < FFPL</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
	<!--
		function init(){
			if (strErrores != ""){
				mostrarError(strErrores);
			}
			ocultarProcesando();
		}
	-->
    </script>
</head>
<body style="overflow:hidden; margin-left:15px; margin-top:15px;" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
<form id="Form1" method="post" runat="server">
<script type="text/javascript">
<!--
    var strErrores = "<%=strErrores%>";
    var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
    var strServer = "<%=Session["strServer"]%>";
-->
</script>
<br />
<center>
<table width="96%">
<tbody>
	<tr>
		<td>
			<table id="tblTitulo" style="height:17px;width:800px;text-align:left">
			<colgroup>
			    <col style="width:150px;"/>
			    <col style="width:100px;"/>
			    <col style="width:100px;"/>
			    <col style="width:60px;"/>
			    <col style="width:270px;"/>
			    <col style="width:60px;"/>
			    <col style="width:60px;"/>
			</colgroup>
				<tr class="TBLINI">
					<td>&nbsp;Proyecto Técnico</td>
					<td>Fase</td>
					<td>Actividad</td>
					<td align="right">Nº Tarea</td>
					<td>&nbsp;Tarea</td>
					<td>&nbsp;FFPL</td>
					<td>&nbsp;FFPR</td>
				</tr>
			</table>
			<div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 816px; height: 352px" align="left" runat="server">
			    <div id="divC" style='background-image:url(../../../../Images/imgFT16.gif); width:800px' runat="server">
			    </div>
            </div>
            <table id="tblResultado" style="height:17px;width:800px;">
            <tr class="TBLFIN">
	            <td><img height="1" src="../../../../Images/imgSeparador.gif" width="1" border="0"/>&nbsp;</td>
            </tr>
            <tr>
	            <td>&nbsp;</td>
            </tr>
            </table>
            <center>
            <table width="300px" style="margin-top:5px;">
	            <tr>
		            <td>
		                <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../../images/Botones/imgSalir.gif" /><span title="Cerrar">Cerrar</span></button>				
		            </td>
	            </tr>
            </table>  
            </center>          
        </td>
    </tr>
</tbody>
</table>
</center>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</form>
<script type="text/javascript">
<!--
    function init() {
        setExcelImg("imgExcel", "divCatalogo");
        ocultarProcesando();
    }
    function excel() {
        try {
            var tblDatos = $I("tblDatos");
            if (tblDatos == null) {
                ocultarProcesando();
                mmoff("Inf", "No hay información en pantalla para exportar.", 300);
                return;
            }

            var sb = new StringBuilder;
            sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
            sb.Append("	<TR align=center style='background-color: #BCD4DF;'>");
            sb.Append("        <td style='width:auto;'>Proyecto técnico</TD>");
            sb.Append("        <td style='width:auto;'>Fase</TD>");
            sb.Append("        <td style='width:auto;'>Actividad</TD>");
            sb.Append("        <td style='width:auto;'>Nº Tarea</TD>");
            sb.Append("        <td style='width:auto;'>Tarea</TD>");
            sb.Append("        <td style='width:auto;'>FFPL</TD>");
            sb.Append("        <td style='width:auto;'>FFPR</TD>");
            sb.Append("	</TR>");
            for (var i = 0; i < tblDatos.rows.length; i++) {
                sb.Append(tblDatos.rows[i].outerHTML);
            }
            sb.Append("</table>");

            crearExcel(sb.ToString());
            var sb = null;
        } catch (e) {
            mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
        }
    }
    function cerrarVentana() {
        //setTimeout("mostrarProcesando()", 50);
        var returnValue = null;
        modalDialog.Close(window, returnValue);
    }
-->
</script>
</body>
</html>
