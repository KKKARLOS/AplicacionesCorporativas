<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getLista.aspx.cs" Inherits="Capa_Presentacion_getLista" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Pantalla de selección</title>
	<meta http-equiv='X-UA-Compatible' content='IE=8' />
	<script language="JavaScript" src="../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
	    function init(){
	        try{
	            if (!mostrarErrores()) return;
	            actualizarLupas("tblTitulo", "tblDatos");
	            ocultarProcesando();
	        }catch(e){
	            mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
	        }
	    }
	
	    function aceptarClick(indexFila){
	        try{
	            if (bProcesando()) return;
            
	            var returnValue = $I("tblDatos").rows[indexFila].id + "@#@" + $I("tblDatos").rows[indexFila].cells[0].innerText;
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
    </script>
</head>
<body style="overflow: hidden" style="margin-left:10px;" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
    <form id="form1" runat="server">
	<script type="text/javascript">
	    var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";
	</script>
	<img src="<%=Session["strServer"] %>Images/imgSeparador.gif" style="width:1px; height:1px;" />
    <table class="texto" style="margin-left:10px; margin-top:20px; width:410px;" cellpadding="5" >
        <tr>
            <td>
                <table id="tblTitulo" style="width:400px; height:17px">
                    <tr class="TBLINI">
                        <td style="padding-left:3px;">Denominación&nbsp;
                            <img id="imgLupa" style="display: none; cursor: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa')"
			                    height="11" src="../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
				            <img style="cursor: pointer; display: none;" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa', event)"
			                    height="11" src="../../Images/imgLupa.gif" width="20" tipolupa="1">
				        </td>
                    </tr>
                </table>
                <div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 416px; height:380px">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:400px;">
                    <%=strTablaHTML %>
                    </div>
                </div>
                <table style="width:400px; height:17px;">
                    <tr class="TBLFIN">
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <center>
    <table style="margin-top:5px; width:100px;" >
		<tr>
			<td align="center">
                <button id="btnCancelar" type="button" onclick="cerrarVentana()" class="btnH25W90"  runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../images/imgCancelar.gif" /><span title="Cancelar">Cancelar</span>
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

