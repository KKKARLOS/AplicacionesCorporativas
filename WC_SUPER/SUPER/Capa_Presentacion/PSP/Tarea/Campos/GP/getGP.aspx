<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getGP.aspx.cs" Inherits="getGP" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> ::: SUPER ::: - Selección de equipo </title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script language="JavaScript" src="../../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
    function init(){
        try{
            if (!mostrarErrores()) return;
            //$I("divCatalogo").children[0].children[0].style.backgroundImage = "url(../../../../Images/imgFT16.gif)";
            actualizarLupas("tblTitulo", "tblDatos");

            ocultarProcesando();
        }catch(e){
            mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
        }
    }
    function MtoGP()
    {
        try{
            mostrarProcesando();
            //window.focus();
            var strEnlace = strServer + "Capa_Presentacion/PSP/Tarea/Campos/GP/Catalogo/Default.aspx";
            modalDialog.Show(strEnlace, self, sSize(950, 560))
                .then(function (ret) {
                    if (ret != null) {
                        ObtenerDatos();
                    }
                });

            ocultarProcesando();
        }catch(e){
            mostrarErrorAplicacion("Error al ir al mantenimiento de grupos de profesionales", e.message);
        }
    }
    function ObtenerDatos() {
        try {
            mostrarProcesando();
            var js_args = "getDatos@#@";
            RealizarCallBack(js_args, "");
            return;
        } catch (e) {
            mostrarErrorAplicacion("Error al ir a obtener los datos", e.message);
        }
    }
    function RespuestaCallBack(strResultado, context){
        actualizarSession();
        var aResul = strResultado.split("@#@");
        if (aResul[1] != "OK"){
            ocultarProcesando();
            var reg = /\\n/g;
            mostrarError(aResul[2].replace(reg, "\n"));
        }else{
            switch (aResul[0]){
                case "getDatos":
                    $I("divCatalogo").children[0].innerHTML = aResul[2];
                    break;
            }
            ocultarProcesando();
        }
    }
    function aceptarClick(indexFila){
	    try{
            if (bProcesando()) return;
            var sTexto = $I("tblDatos").rows[indexFila].cells[0].innerText;

            var returnValue = $I("tblDatos").rows[indexFila].id + "@#@" + sTexto;

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
<body style="overflow: hidden; margin-left:10px;" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
    <form id="form1" runat="server">
	<script type="text/javascript">
        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";
	</script>
	<br />
	<center>
    <table style="width:470px;text-align:left;" cellpadding="5" >
        <tr>
            <td>
                <table id="tblTitulo" style="width: 450px; height: 17px">
                    <colgroup><col style="width: 450px;"/></colgroup>
                    <tr class="TBLINI">
                        <td><img style="cursor: pointer" height="11" src="../../../../../Images/imgFlechas.gif" width="6" usemap="#img2" border="0">
						    <map name="img2">
						        <area onclick="ot('tblDatos', 1, 0, '')" shape="rect" coords="0,0,6,5" alt=""/>
						        <area onclick="ot('tblDatos', 1, 1, '')" shape="rect" coords="0,6,6,11" alt=""/>
					        </map>&nbsp;Denominación&nbsp;<img id="imgLupa2" style="display: none; cursor: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa2')"
							height="11" src="../../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <img style="cursor: pointer; display: none;" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa2',event)"
							height="11" src="../../../../../Images/imgLupa.gif" width="20" tipolupa="1"></td>
                    </tr>
                </table>
                <div id="divCatalogo" style="overflow:auto;width:466px;height:352px">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:450px;">
                        <%=strTablaHtmlGP%>
                    </div>
                </div>
                <table style="width: 450px; height: 17px">
                    <tr class="TBLFIN">
                        <TD></TD>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <table width="390px" align="center" style="margin-top:5px;">
        <colgroup><col style="width:240px"/><col style="width:150px"/></colgroup>
	    <tr>
		    <td align="center"> 
			<button id="btnValores" type="button" onclick="MtoGP()" class="btnH25W200" runat="server" hidefocus="hidefocus" 
				    onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../../../images/botones/imgEntrada.gif" /><span title="Accede al mantenimiento de equipos de profesionales">Mantenimiento de equipos</span>
			</button>	
		    </td>
		    <td align="center">
		    <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../../../images/imgCancelar.gif" /><span title="Cancelar">Cancelar</span></button>				
		    </td>
	    </tr>
    </table>
    </center>
    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    </form>
<script type="text/javascript">
    function WebForm_CallbackComplete() {
        for (var i = 0; i < __pendingCallbacks.length; i++) {
            callbackObject = __pendingCallbacks[i];
            if (callbackObject && callbackObject.xmlRequest && (callbackObject.xmlRequest.readyState == 4)) {
                WebForm_ExecuteCallback(callbackObject);
                if (!__pendingCallbacks[i].async) {
                    __synchronousCallBackIndex = -1;
                }
                __pendingCallbacks[i] = null;
                var callbackFrameID = "__CALLBACKFRAME" + i;
                var xmlRequestFrame = document.getElementById(callbackFrameID);
                if (xmlRequestFrame) {
                    xmlRequestFrame.parentNode.removeChild(xmlRequestFrame);
                }
            }
        }
    }
</script>
</body>
</html>
