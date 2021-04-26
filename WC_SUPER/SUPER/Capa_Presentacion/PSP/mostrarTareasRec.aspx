<%@ Page Language="c#" CodeFile="mostrarTareasRec.aspx.cs" Inherits="SUPER.Capa_Presentacion.mostrarTareasRec" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
	<head runat="server">
		<title> ::: SUPER ::: - Tareas asignadas a profesionales</title>
        <meta http-equiv="X-UA-Compatible" content="IE=8"/>
		<script language="JavaScript" src="../../Javascript/funciones.js" type="text/Javascript"></script>
		<script language="JavaScript" src="../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
     	<script language="JavaScript" src="../../Javascript/modal.js" type="text/Javascript"></script>
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
	<body style="OVERFLOW: hidden" leftMargin="15" topMargin="15" onload="init()">
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
			<table style="width:396px;text-align:left">
				<tbody>
					<tr>
						<td>
							<table id="tblTitulo" style="height:17px;width:380px">
							    <colgroup>
							        <col style="width:50px" />
							        <col style="width:330px" />
							    </colgroup>
								<tr class="TBLINI">
									<td align="center">&nbsp;Nº</td>
									<td>Denominación</td>
								</tr>
							</table>
							<div id="divCatalogo" style="overflow: auto; width: 396px; height: 350px" align="left">
							    <div id="divC" style='background-image:url(../../Images/imgFT16.gif); width:380px' runat="server">
							    </div>
			                </div>
                            <table id="tblResultado" style="width: 380px; height: 17px">
	                            <tr class="TBLFIN"><td></td></tr>
                            </table>
                        </td>
				    </tr>
			    </tbody>
			</table>
            <table width="300px" align="center" style="margin-top:15px;">
	            <tr>
		            <td align="center">
		                <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../images/botones/imgSalir.gif" /><span title="Cerrar">Cerrar</span></button>				
		            </td>
	            </tr>
            </table>
		</center>
		<uc_mmoff:mmoff ID="mmoff1" runat="server" />
		</form>
		<script type="text/javascript">
        <!--
	        function cerrarVentana(){
		        var returnValue = null;
		        modalDialog.Close(window, returnValue);
	        }
        -->
        </script>
	</body>
</html>
