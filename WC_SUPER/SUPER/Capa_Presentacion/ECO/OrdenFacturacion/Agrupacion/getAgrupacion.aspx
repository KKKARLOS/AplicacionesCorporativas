<%@ Page Language="c#" CodeFile="getAgrupacion.aspx.cs" Inherits="getCliente" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title> ::: SUPER ::: - Datos de agrupación de órdenes de facturación</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
	<!--
		function init(){
		    try{
            if (!mostrarErrores()) return;
			    $I("txtDenominacion").focus();
			    ocultarProcesando();
            }catch(e){
                mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
            }
		}
		
        function comprobarDatos(){
            try{
                if ($I("txtDenominacion").value=="")
                {
                    mmoff("War", "La denominación de la agrupación es dato obligatorio",320);
                    $I("txtDenominacion").focus();
                    return false;
                }   
                if ($I("txtProyectos").value=="")
                {
                    mmoff("War", "Debes indicar los proyectos que componen a agrupación",320);            
                    $I("txtProyectos").focus();
                    return false;
                }   
                
                var aProyectos = $I("txtProyectos").value.split(";");
                for (var i=0;i<aProyectos.length;i++){
                    if (aProyectos[i] == "") continue;
                    if (isNaN(aProyectos[i])){
                        mmoff("War", "Se ha detectado que hay proyectos que no tienen formato numérico",400);
                        return false;
                    }
                }
            
                return true;
	        }catch(e){
		        mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
            }
        }		
	    function aceptar(){
		    try{
                if (!comprobarDatos()) return;

		        var strDatos = $I("hdnIdAgrupacion").value + "@#@" + Utilidades.escape($I("txtDenominacion").value) +"@#@"+ dfn($I("txtProyectoInicio").value) +",";
                var aProyectos = $I("txtProyectos").value.split(";");
                for (var i=0;i<aProyectos.length;i++){
                    if (aProyectos[i] == "") continue;
                    strDatos += dfn(aProyectos[i]) +",";
                }
                var returnValue = strDatos;
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
		
	-->
    </script>
</head>
<body style="overflow: hidden; margin-left:5px; margin-top:15px;" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
<form id="Form1" method="post" runat="server">
<script type="text/javascript">
<!--
    var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
    var strServer = "<%=Session["strServer"]%>";
-->
</script>
<table class="texto" cellpadding="5px" style="width:430px;">
    <tr>
        <td>
            <label id="lblDenominacion" style="width:70px;">Denominación</label>
            <asp:TextBox ID="txtDenominacion" runat="server" style="width:335px;" MaxLength="50" TabIndex="1" />
        </td>
    </tr>
    <tr>
        <td>
            <label id="lblProyectos" style="width:70px;">Proyectos</label>
            <asp:TextBox ID="txtProyectoInicio" runat="server" SkinID="Numero" style="width:60px" readonly="true" />
            &nbsp;+ <asp:TextBox ID="txtProyectos" runat="server" style="width:255px;" MaxLength="100" TabIndex="2" />
        </td>
    </tr>
    <tr>
        <td><label style="margin-left:150px;">(Números de proyectos separados por punto y coma)</label></td>
    </tr>
</table>
<center>
    <table style="margin-top:20px; width:220px;">
		<tr>
			<td>
                <button id="btnAceptar" type="button" onclick="aceptar();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
                     onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../../images/imgAceptar.gif" /><span>Aceptar</span>
                </button>
			</td>
			<td align="center">
                <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
                     onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../../images/imgCancelar.gif" /><span>Cancelar</span>
                </button>
			</td>
		</tr>
    </table>
</center>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<asp:TextBox ID="hdnIdAgrupacion" runat="server" style="visibility:hidden" Text="0" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</form>
</body>
</html>
