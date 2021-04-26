<%@ Page Language="C#" CodeFile="getNLO.aspx.cs" Inherits="getNLO" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
	<title> ::: SUPER ::: - Selección de nueva línea de oferta</title>
	<meta http-equiv='X-UA-Compatible' content='IE=8' />
	<script language="JavaScript" src="../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
		function init(){
		    try{
                if (!mostrarErrores()) return;
                nNE = nNEAux;
                colorearNE();
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
            
                var strDatos = Utilidades.unescape($I("tblDatos").rows[indexFila].getAttribute("idL"));

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
    
        function borrarCatalogo(){
            try{
                $I("divCatalogo").children[0].innerHTML = "";
	        }catch(e){
		        mostrarErrorAplicacion("Error al borrar el catálogo", e.message);
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
                    case "lineas":
	                    $I("divCatalogo").children[0].innerHTML = aResul[2];
                        actualizarLupas("tblTitulo", "tblDatos");
                        break;
                }
                ocultarProcesando();
            }
        }
    
        var oImagen;
        function me(oImg){
	        try{
	            mostrarProcesando();
	            oImagen = oImg;
	            setTimeout("mostrarNLO()", 20);
            }catch(e){
                mostrarErrorAplicacion("Error al mostrar / ocultar las nuevas líneas de oferta (me)", e.message);
            }
        }
    
        function mostrarNLO(){
	        try{
	            var oImg = oImagen;
                var idArea = oImg.parentNode.parentNode.id;
                var bMostrar = (oImg.src.indexOf("plus")==-1)? false:true;
	            //alert(idArea +" // "+ bMostrar);
                if (bMostrar) oImg.src = "../../images/minus.gif";
                else oImg.src = "../../images/plus.gif";
                //alert(oImg.parentNode.parentNode.rowIndex);
                var nInicio = oImg.parentNode.parentNode.rowIndex;
                for (var i = nInicio; i < $I("tblDatos").rows.length; i++) {
                    if ($I("tblDatos").rows[i].id != idArea) 
                        break;
                    if ($I("tblDatos").rows[i].id == idArea && tblDatos.rows[i].getAttribute("nNivel") == 2) {
                        if (bMostrar) 
                            $I("tblDatos").rows[i].style.display = "table-row";
                        else 
                            $I("tblDatos").rows[i].style.display = "none";
                    }
                }
                ocultarProcesando();
            }catch(e){
                mostrarErrorAplicacion("Error al mostrar / ocultar las nuevas líneas de oferta (mostrarNLO)", e.message);
            }
        }
        function buscar(){
            try{
                setNE(1);
                var js_args = "lineas@#@";
                js_args += ($I("chkTodos").checked==true)? "1@#@" : "0@#@";
                mostrarProcesando();
                RealizarCallBack(js_args, "");
           
            }catch(e){
                mostrarErrorAplicacion("Error al ir a obtener las líneas de oferta", e.message);
            }
        }
        function colorearNE() {
            try {
                switch (nNE) {
                    case 1:
                        $I("imgNE1").src = "../../images/imgNE1on.gif";
                        $I("imgNE2").src = "../../images/imgNE2off.gif";
                        break;
                    case 2:
                        $I("imgNE1").src = "../../images/imgNE1on.gif";
                        $I("imgNE2").src = "../../images/imgNE2on.gif";
                        break;
                }
            } catch (e) {
                mostrarErrorAplicacion("Error al establecer los colores del nivel de expansión", e.message);
            }
        }
        /* Función para establecer el nivel de expansión */
        var nNE = 1;
        function setNE(nValor) {
            try {
                nNE = nValor;
                mostrarProcesando();

                colorearNE();
                setTimeout("setNE2()", 100);
            } catch (e) {
                mostrarErrorAplicacion("Error al establecer el nivel de expansión", e.message);
            }
        }

        function setNE2() {
            try {
                MostrarOcultar(0);
                if (nNE > 1) MostrarOcultar(1);
            } catch (e) {
                mostrarErrorAplicacion("Error al establecer el nivel de expansión (2)", e.message);
            }
        }
        function MostrarOcultar(nMostrar) {
            try {
                if ($I("tbody") == null) {
                    ocultarProcesando();
                    return;
                }

                if (nMostrar == 0) {//Contraer
                    var tblBodyFijo = $I("tbody");
                    for (var i = 0; i < tblBodyFijo.rows.length; i++) {
                        if (tblBodyFijo.rows[i].getAttribute("nNivel") > 1) {
                            if (tblBodyFijo.rows[i].getAttribute("nNivel") < 2) 
                                tblBodyFijo.rows[i].cells[0].children[0].src = "../../images/plus.gif";
                            tblBodyFijo.rows[i].style.display = "none";
                        }
                        else {
                            if (tblBodyFijo.rows[i].getAttribute("nNivel") < 2) 
                                tblBodyFijo.rows[i].cells[0].children[0].src = "../../images/plus.gif";
                        }
                    }
                    ocultarProcesando();
                } else { //Expandir
                    MostrarTodo();
                }
            } catch (e) {
                mostrarErrorAplicacion("Error al expandir/contraer todo", e.message);
            }
        }

        var bMostrar = false;
        var nIndiceTodo = -1;
        function MostrarTodo() {
            try {
                if ($I("tbody") == null) {
                    ocultarProcesando();
                    return;
                }

                var nIndiceAux = 0;
                if (nIndiceTodo > -1) nIndiceAux = nIndiceTodo;
                var tblBodyFijo = $I("tbody");
        
                for (var i = nIndiceAux; i < tblBodyFijo.rows.length; i++) {
                    if (tblBodyFijo.rows[i].getAttribute("nNivel") < nNE) {
                        if (tblBodyFijo.rows[i].cells[0].children[0].src.indexOf("plus.gif") > -1) {
                            bMostrar = true;
                            nIndiceTodo = i;

                            mostrar(tblBodyFijo.rows[i].cells[0].children[0]);

                            return;
                        }
                    }
                }
                bMostrar = false;
                nIndiceTodo = -1;
                ocultarProcesando();
            } catch (e) {
                mostrarErrorAplicacion("Error al expandir toda la tabla", e.message);
            }
        }
        function mostrar(oImg) {
            try {
                var oFila = oImg.parentNode.parentNode;
                var nIndexFila = oFila.rowIndex;
                var nNivel = oFila.getAttribute("nNivel");
                if (oImg.src.indexOf("plus.gif") == -1) var opcion = "O"; //ocultar
                else var opcion = "M"; //mostrar

                var tblBodyFijo = $I("tbody");
                var sSrc = "";
                for (var i = nIndexFila + 1; i < tblBodyFijo.rows.length; i++) {
                    if (tblBodyFijo.rows[i].getAttribute("nNivel") > nNivel) {
                        if (opcion == "O") {
                            tblBodyFijo.rows[i].style.display = "none";
                            if (tblBodyFijo.rows[i].getAttribute("nNivel") < 2)
                                tblBodyFijo.rows[i].cells[0].children[0].src = "../../images/plus.gif";
                    
                        }
                        else if (tblBodyFijo.rows[i].getAttribute("nNivel") - 1 == nNivel) {
                            tblBodyFijo.rows[i].style.display = "table-row";
                        }

                    } else {
                        break;
                    }
                }
                if (opcion == "O") {
                    if (oFila.getAttribute("nNivel") < 2) oImg.src = "../../images/plus.gif";
                }
                else {
                    if (oFila.getAttribute("nNivel") < 2) oImg.src = "../../images/minus.gif";
                }

                if (bMostrar) MostrarTodo();
                else ocultarProcesando();
            } catch (e) {
                mostrarErrorAplicacion("Error al expandir/contraer", e.message);
            }
        }
    </script>
</head>
<body style="overflow:hidden; margin-left:15px; margin-top:15px;" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
	<form id="Form1" method="post" runat="server">
	<script type="text/javascript">
        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";
        var nNEAux = <%= nNE.ToString() %>;
	</script>
    <table border="0" class="texto" width="520px" style="margin-left:10px;" cellpadding="0" cellspacing="0">
        <tr style="height:25px;">
            <td style="vertical-align:bottom;">
                <img id="imgNE1" src='../../images/imgNE1on.gif' class="ne" onclick="setNE(1);">
                <img id="imgNE2" src='../../images/imgNE2off.gif' class="ne" onclick="setNE(2);">
            </td>
            <td style="vertical-align:top;">
                <label id="lblTodos" title="Muestra también las líneas de oferta inactivas" style="margin-left:50px;">Mostrar inactivos</label> 
                <input type=checkbox id="chkTodos" class="check" onclick="buscar();" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table id="tblTitulo" style="width:500px; height:17px;">
                    <colgroup>
                        <col style="width:200px;" /><col style="width:300px;" />
                    </colgroup>
                    <tr class="TBLINI">
                        <td style="padding-left:20px; width:200px;">Área / Nueva Línea de Oferta</td>
                        <td>
							<img id="imgLupa1" style="display:none; cursor: pointer;" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa1')"
							    height="11" src="../../Images/imgLupaMas.gif" width="20" tipolupa="2">
                            <img style="cursor: pointer; display: none;" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa1',event)"
							    height="11" src="../../Images/imgLupa.gif" width="20" tipolupa="1"> 
					    </td>
                    </tr>
                </table>
                <div id="divCatalogo" style="overflow: auto; overflow-x:hidden; width:516px; height:410px">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:500px;">
                    <%=strTablaHTML %>
                    </div>
                </div>
                <table style="width:500px; height:17px" >
                    <tr class="TBLFIN">
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <center>  
        <table style="margin-top:5px; width:100px;">
		    <tr>
			    <td align="center">
			    <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../images/imgCancelar.gif" /><span title="Cancelar">Cancelar</span></button>				
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
