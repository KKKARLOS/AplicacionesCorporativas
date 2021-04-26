<%@ Page Language="c#" CodeFile="obtenerTarea.aspx.cs" Inherits="SUPER.Capa_Presentacion.obtenerTarea" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>   
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
	<HEAD runat="server">
		<title> ::: SUPER ::: - Seleccione tareas</title>
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
					//$I("procesando").style.visibility = "hidden";
					ocultarProcesando();
				}
			-->
            </script>
            <style type="text/css">      
	            #tblDatos td { padding-left: 5px; }
	            #tblDatos tr { height: 16px; }
            </style>            
    </HEAD>
	<body style="OVERFLOW: hidden" leftMargin="15" topMargin="15" onload="init()">
        <ucproc:Procesando ID="Procesando" runat="server" />
		<script type="text/javascript">
		<!--
		    var strErrores = "<%=strErrores%>";
		    var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
		    var strServer = "<%=Session["strServer"]%>";
		-->
		</script>		
		    <form id="Form1" method="post" runat="server">
			<center>
			<table style="width:396px;text-align:left;margin-top:20px">
				<tr>
					<td>
						<table id="tblTitulo" style="height:17px;width:380px">
							<tr class="TBLINI">
								<td>&nbsp;Tarea&nbsp;
									<IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa1')"
										height="11" src="../../../../Images/imgLupaMas.gif" width="20">
								    <IMG style="CURSOR: pointer" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa1',event)"
										height="11" src="../../../../Images/imgLupa.gif" width="20"> 
								</td>
							</tr>
						</table>
						<div id="divCatalogo" style="overflow: auto; width:396px; height: 337px">
							<div style='background-image:url(../../../../Images/imgFT16.gif); width:380px'>
							<%=strTablaHtml %>
							</div>
		                </div>
	                    <table id="tblResultado" style="height:17px;width:380px">
		                    <tr class="TBLFIN"><td></td></tr>
	                    </table>
		            </td>
		        </tr>
			</table>
			<br />
	        <table width="260px" style="margin-top:15px;">
		        <tr>
			        <td>
				        <button id="btnAceptar" type="button" onclick="aceptar();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../../images/imgAceptar.gif" /><span title="Aceptar">Aceptar</span></button>								
			        </td>
			        <td>
				        <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../../images/imgCancelar.gif" /><span title="Cancelar">Cancelar</span></button>				
			        </td>
		        </tr>
	        </table>
			</center>
			<uc_mmoff:mmoff ID="mmoff1" runat="server" />
		</form>
		<script type="text/javascript">
<!--


	function aceptar(){
		strOpciones = "";
		for (i=0; i<$I("tblDatos").rows.length;i++){
		    if ($I("tblDatos").rows[i].className == "FS") {
		        strOpciones += $I("tblDatos").rows[i].id + "##" + $I("tblDatos").rows[i].cells[1].children[0].innerText + "@#@";
    		}
		}
		var returnValue = strOpciones;
		modalDialog.Close(window, returnValue);
	}

	function cerrarVentana(){
		var returnValue = null;
		modalDialog.Close(window, returnValue);
	}
-->
        </script>
	</body>
</HTML>
