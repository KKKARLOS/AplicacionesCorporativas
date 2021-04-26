<%@ Page Language="c#" CodeFile="obtenerTarea.aspx.cs" Inherits="SUPER.Capa_Presentacion.obtenerTarea" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self" />
    <title> ::: SUPER ::: - Seleccione tareas</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script language="JavaScript" src="../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../Javascript/boxover.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
	<!--
	    function init() {
	        if (strErrores != "") {
	            mostrarError(strErrores);
	        }
	        //$I("procesando").style.visibility = "hidden";
	        ocultarProcesando();
	    }
	-->
    </script>
</head>
<body style="overflow: hidden" onload="init()">
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
		<table style="width:420px; margin-left:20px;">
			<tr>
				<td>
					<table id="tblTitulo" style="width:400px; height:17px;" >
					    <colgroup><col style="width:60px;"/><col style="width:340px;"/></colgroup>
						<tr class="TBLINI">
						    <td style="text-align:right;">&nbsp;Número</td>
							<td style="padding-left:5px;">&nbsp;Denominación&nbsp;
							    <img style="CURSOR: pointer" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa1', event)"
									height="11" src="../../../Images/imgLupa.gif" width="20" /> 
								<img id="imgLupa1" style="display: none; cursor: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa1', event)"
									height="11" src="../../../Images/imgLupaMas.gif" width="20" />
							</td>
						</tr>
					</table>
					<div id="divCatalogo" style="overflow: auto; width:416px; height: 337px">
						<div style='background-image:url(../../../Images/imgFT16.gif); width:400px;'>
						    <%=strTablaHtml %>
						</div>
	                </div>
	                <table id="tblResultado" style="width:400px; height:17px;">
		                <tr class="TBLFIN">
			                <td >
			                </td>
		                </tr>
	                </table>
                </td>
            </tr>
            <tr>
                <td>
                    <center>
                    <br />
                    <button id="btnAceptar" type="button" onclick="aceptar();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
	                     onmouseover="se(this, 25);mostrarCursor(this);" style="display:inline;">
	                    <img src="../../../images/imgAceptar.gif" /><span>&nbsp;Aceptar</span>
                    </button>
                    <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
	                     onmouseover="se(this, 25);mostrarCursor(this);" style="display:inline; margin-left:30px;">
	                    <img src="../../../images/botones/imgCancelar.gif" /><span>&nbsp;Cancelar</span>
                    </button>
                    </center>
                </td>
            </tr>
		</table>
        <uc_mmoff:mmoff ID="mmoff1" runat="server" />
	</form>
<script type="text/javascript">
<!--
    function aceptar() {
        strOpciones = "";
        for (i = 0; i < $I("tblDatos").rows.length; i++) {
            if ($I("tblDatos").rows[i].className == "FS") {
                strOpciones += $I("tblDatos").rows[i].id + "##" + $I("tblDatos").rows[i].cells[1].children[0].innerText + "##";
                //Paso los datos ocultos  ETPL, FIPL, FFPL, ETPR, FFPR, CONSUMO, AVANCE
                strOpciones += $I("tblDatos").rows[i].getAttribute("ETPL") + "##" + $I("tblDatos").rows[i].getAttribute("FIPL") + "##";
                strOpciones += $I("tblDatos").rows[i].getAttribute("FFPL") + "##" + $I("tblDatos").rows[i].getAttribute("ETPR") + "##";
                strOpciones += $I("tblDatos").rows[i].getAttribute("FFPR") + "##" + $I("tblDatos").rows[i].getAttribute("CONSUMO") + "##";
                strOpciones += $I("tblDatos").rows[i].getAttribute("AVANCE") + "@#@";
            }
        }
        //strOpciones = strOpciones.substring(0,strOpciones.length-2);
        var returnValue = strOpciones;
        modalDialog.Close(window, returnValue);
    }

    function cerrarVentana() {
        var returnValue = null;
        modalDialog.Close(window, returnValue);
    }
-->
</script>
</body>
</html>
