<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getBonoTransporte.aspx.cs" Inherits="getBonoTransporte" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Selección de bono de transporte</title>
	<meta http-equiv='X-UA-Compatible' content='IE=8' />
	<script language="JavaScript" src="../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
	<!--
    function init(){
        try{
            if (!mostrarErrores()) return;
            actualizarLupas("tblTitulo", "tblDatos");
            window.focus();
            ocultarProcesando();
        }catch(e){
            mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
        }
    }
	
    function aceptarClick(indexFila){
	    try{
            if (bProcesando()) return;
            
	        var returnValue = $I("tblDatos").rows[indexFila].id + "@#@"
	                            + $I("tblDatos").rows[indexFila].getAttribute("fivb") + "@#@"
	                            + $I("tblDatos").rows[indexFila].getAttribute("ffvb") + "@#@"
	                            + Utilidades.escape($I("tblDatos").rows[indexFila].cells[0].innerText);
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
    function buscarBonos(){
        try{
            mostrarProcesando();
            
            var js_args = "buscarBonos@#@";
            js_args += sUsuario + "@#@";
            js_args += ($I("chkSoloVigentes").checked==true)? "1" : "0";
            
            RealizarCallBack(js_args, "");
           
        }catch(e){
	        mostrarErrorAplicacion("Error al ir a obtener los contratos", e.message);
        }
	}
	
    /*
    El resultado se envía en el siguiente formato:
    "opcion@#@OK@#@valor si hiciera falta, html,..." ó "ERROR@#@Descripción del error"
    */
    function RespuestaCallBack(strResultado, context){
        actualizarSession();
        var aResul = strResultado.split("@#@");
        if (aResul[1] != "OK"){
            ocultarProcesando();
            var reg = /\\n/g;
            mostrarError(aResul[2].replace(reg, "\n"));
        }else{
            switch (aResul[0]){
                case "buscarBonos":
	                $I("divCatalogo").children[0].innerHTML = aResul[2];
                    window.focus();
                    actualizarLupas("tblTitulo", "tblDatos");
                    break;
                default:
                    ocultarProcesando();
                    mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
            }
            ocultarProcesando();
        }
    }    
	-->
    </script>
</head>
<body style="overflow: hidden" leftmargin="10" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
    <form id="form1" runat="server">
	<script type="text/javascript">
	<!--
        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";
	    var sUsuario = "<%=sUsuario%>";
	-->
	</script>
	<center>
    <table style="width:520px; margin-left:10px ;margin-top:10px; text-align:left" cellpadding="5">
        <tr>
            <td style="text-align:right; padding-right:15px;">
                <label id="lblVigentes">Mostrar solo vigentes</label> <input type="checkbox" id="chkSoloVigentes" class="check" checked="true" onclick="buscarBonos();" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <table id="tblTitulo" style="width: 500px; height: 17px">
                    <colgroup>
                        <col style="width:300px;" />
                        <col style="width:100px;" />
                        <col style="width:100px;" />
                    </colgroup>
                    <TR class="TBLINI">
                        <td style="padding-left:3px;">Denominación&nbsp;
                            <IMG id="imgLupa" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa')"
			                    height="11" src="../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
				            <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa', event)"
			                    height="11" src="../../Images/imgLupa.gif" width="20" tipolupa="1">
				        </td>
				        <td title="Fecha de inicio de vigencia del bono">FIVB</td>
				        <td title="Fecha de fin de vigencia del bono">FFVB</td>
                    </tr>
                </table>
                <div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 516px; height:150px">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:500px">
                    <%=strTablaHTML %>
                    </div>
                </div>
                <table style="width: 500px; height: 17px" >
                    <tr class="TBLFIN">
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </center>
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
