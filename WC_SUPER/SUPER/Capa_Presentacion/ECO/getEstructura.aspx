<%@ Page Language="c#" CodeFile="getEstructura.aspx.cs" Inherits="getItemEstructura" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
	<title> ::: SUPER ::: - Selección de zzz</title>
	<meta http-equiv='X-UA-Compatible' content='IE=8' />
	<script language="JavaScript" src="../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
   	<script language="JavaScript" src="../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
	<!--
		function init(){
            try{
                if (!mostrarErrores()) return;
    			ocultarProcesando();
            }catch(e){
                mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
            }
		}

        
        function aceptarClick(indexFila){
            try{
                if (bProcesando()) return;

                var returnValue = $I("tblDatos").rows[indexFila].id + "@#@" + $I("tblDatos").rows[indexFila].cells[0].innerText + "@#@" +
	                                 $I("tblDatos").rows[indexFila].getAttribute("cualificador") + "@#@" + 
	                                 $I("tblDatos").rows[indexFila].getAttribute("obligatoriedad");
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
<body style="OVERFLOW: hidden; margin-left:15px;" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
<form id="Form1" method="post" runat="server">
<script type="text/javascript">
<!--
    var intSession  = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
    var strServer   = "<%=Session["strServer"]%>";
-->
</script>
<center>
<table style="margin-top:10px; width:420px; text-align:left;">
	<tbody>
		<tr>
			<td>
				<table id="tblTitulo" style="width:400px; height:17px;">
					<tr class="TBLINI">
						<td>&nbsp;Denominación&nbsp;
							<IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa1')"
								height="11" src="../../Images/imgLupaMas.gif" width="20" tipolupa="2">
						    <IMG style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa1', event)"
								height="11" src="../../Images/imgLupa.gif" width="20" tipolupa="1"> 
						</td>
					</tr>
				</table>
				<div id="divCatalogo" style="OVERFLOW: auto; OVERFLOW-X: hidden; WIDTH: 416px; HEIGHT: 352px" align="left" runat="server">
                    <div id="divCapa" style="background-image:url(../../Images/imgFT16.gif); width:400px;" runat="server">
					</div>
                </div>
                <table style="WIDTH:400px; HEIGHT:17px">
                    <tr class="TBLFIN">
                        <td></td>
                    </tr>
                </table>
            </td>
         </tr>
    </tbody>
</table>
<table style="margin-top:10px; width:100px;">
    <tr>
        <td style="text-align:center;">
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
