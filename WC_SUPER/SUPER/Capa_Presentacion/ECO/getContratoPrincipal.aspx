<%@ Page Language="c#" CodeFile="getContratoPrincipal.aspx.cs" Inherits="getContratoPrincipal" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
	<title> ::: SUPER ::: - Selección de contrato</title>
	<meta http-equiv='X-UA-Compatible' content='IE=8' />
	<script language="JavaScript" src="../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
		function init(){
		    try{
			    if (strErrores != ""){
				    mostrarError(strErrores);
			    }
			    $I("txtContrato").focus();
			    ocultarProcesando();
            }catch(e){
                mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
            }
		}
		
        function buscarContrato(strContrato){
            try{
                //alert(strContrato);
                if (strContrato == ""){
                    mmoff("Inf", "Introduce algún criterio de búsqueda", 265);
                    return;
                }
                var js_args = "contrato@#@";
                var sAccion=getRadioButtonSelectedValue("rdbTipo",true);
                js_args += sAccion + "@#@";
                js_args += strContrato +"@#@";

                //alert(js_args);
                mostrarProcesando();
                RealizarCallBack(js_args, "");
                //return false;
	        }catch(e){
		        mostrarErrorAplicacion("Error al obtener los contratos", e.message);
            }
		}
		function RespuestaCallBack(strResultado, context) {
            actualizarSession();
            var aResul = strResultado.split("@#@");
            if (aResul[1] != "OK"){
                ocultarProcesando();
                var reg = /\\n/g;
                mostrarError(aResul[2].replace(reg, "\n"));
            }
            else{
                switch (aResul[0]) {
                    case "contrato":
                        $I("divCatalogo").children[0].innerHTML = aResul[2];
                        $I("txtContrato").value = "";
                        break;
                }
                ocultarProcesando();
            }
        }
        
    	
	    function aceptarClick(indexFila){
		    try{
                if (bProcesando()) return;

                var returnValue = $I("tblDatos").rows[indexFila].id + "@#@" + $I("tblDatos").rows[indexFila].innerText + "@#@" +
		                             $I("tblDatos").rows[indexFila].getAttribute("IdCliente") + "@#@" +
		                             $I("tblDatos").rows[indexFila].getAttribute("Cliente");
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
<body style="overflow:hidden; margin-left:15px; margin-top:15px;" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
<form id="Form1" method="post" runat="server">
<script type="text/javascript">
    var strErrores = "<%=strErrores%>";
    var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
    var strServer = "<%=Session["strServer"]%>";
</script>
<table class="texto" style="width:520px;">
    <colgroup><col style="width:360px" /><col style="width:160px" /></colgroup>
    <tr>
        <td>
            Nº de contrato o Denominación 
            <asp:TextBox ID="txtContrato" runat="server" style="width:270px; margin-left:5px;" 
                        onKeyPress="javascript:if(event.keyCode==13){buscarContrato(this.value);event.keyCode=0;return false;}">
            </asp:TextBox>
        </td>
        <td onclick="buscarContrato($I('txtContrato').value);">
            <asp:RadioButtonList ID="rdbTipo" SkinId="rbli" runat="server" RepeatColumns="2" ToolTip="Tipo de búsqueda">
                <asp:ListItem Value="I">
                    <img src='../../Images/imgIniciaCon.gif' border='0' title="Inicia con" style="cursor:pointer" onclick="$I('rdbTipo_0').checked=true;" hidefocus=hidefocus>
                </asp:ListItem>
                <asp:ListItem Selected="True" Value="C">
                    <img src='../../Images/imgContieneA.gif' border='0' title="Contiene" style="cursor:pointer" onclick="$I('rdbTipo_1').checked=true;" hidefocus=hidefocus>
                </asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
	<tr>
		<td colspan="2">
			<table id="tblTitulo" style="margin-top:5px; height:17px; width:500px;">
				<tr class="TBLINI">
					<td>
					    <img id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa1')"
							height="11" src="../../Images/imgLupaMas.gif" width="20">&nbsp;Contrato
					    <img style="CURSOR: pointer" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa1', event)" height="11" src="../../Images/imgLupa.gif" width="20"> 
					</td>
				</tr>
			</table>
	    </td>
	</tr>
    <tr>
        <td colspan="2">
            <div id="divCatalogo" style="OVERFLOW:auto; OVERFLOW-X:hidden; WIDTH:516px; HEIGHT:350px">
                <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT16.gif); width:500px;">
                </div>
            </div>
            <table id="tblResultado" style="height:17px; width:500px;">
                <tr class="TBLFIN">
	                <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="padding-top:10px; text-align:center;">
                        <button id="btnCancelar" type="button" onclick="cerrarVentana()" class="btnH25W90" style="display:inline;" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                            <img src="../../images/imgCancelar.gif" /><span title="Cancelar">Cancelar</span>
                        </button>    
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</form>
</body>
</html>
