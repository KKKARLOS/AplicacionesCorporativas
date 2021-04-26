<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getSoporteAdm.aspx.cs" Inherits="getHorizontal" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Selección del soporte administrativo</title>
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
            actualizarLupas("tblTitulo", "tblDatos");
            ocultarProcesando();
        }catch(e){
            mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
        }
    }
	
    function aceptarClick(indexFila){
	    try{
            if (bProcesando()) return;
            
            var returnValue = $I("tblDatos").rows[indexFila].id + "@#@" + $I("tblDatos").rows[indexFila].innerText + "@#@" + $I("tblDatos").rows[indexFila].getAttribute("idficepi");
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
<body style="OVERFLOW: hidden; margin-left:10px;" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
    <form id="form1" runat="server">
	<script type="text/javascript">
	<!--
        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";
	-->
	</script>
	<img src="<%=Session["strServer"] %>Images/imgSeparador.gif" width="1px" height="1px" />
    <table class="texto" style="margin-left:10px; width:420px;" cellpadding="5">
        <tr>
            <td>
                <TABLE id="tblTitulo" style="WIDTH: 400px; HEIGHT: 17px">
                    <TR class="TBLINI">
                        <td style="padding-left:3px;">Profesional&nbsp;
                            <IMG id="imgLupa" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa')"
				                    height="11" src="../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
				            <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa', event)"
				                    height="11" src="../../Images/imgLupa.gif" width="20" tipolupa="1">
				        </td>
                    </TR>
                </TABLE>
                <DIV id="divCatalogo" style="OVERFLOW: auto; OVERFLOW-X: hidden; WIDTH: 416px; height:384px">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:400px">
                    <%=strTablaHTML %>
                    </div>
                </DIV>
                <TABLE style="WIDTH:400px; HEIGHT:17px">
                    <TR class="TBLFIN">
                        <TD></TD>
                    </TR>
                </TABLE>
            </td>
        </tr>
    </table>
    <center>
        <table style="margin-top:5px; width:100px;">
		    <tr>
			    <td>
                    <button id="btnCancelar" type="button" onclick="cerrarVentana()" class="btnH25W90" style="display:inline;" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
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
