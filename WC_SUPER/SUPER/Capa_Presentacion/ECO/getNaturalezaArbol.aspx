<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getNaturalezaArbol.aspx.cs" Inherits="getNaturaleza" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Selección de naturaleza de producción</title>
	<meta http-equiv='X-UA-Compatible' content='IE=8' />
	<script language="JavaScript" src="../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
	<!--
    function init(){
        try{
            if (!mostrarErrores()) return;
            //if (tblDatos1.rows.length == 1) tblDatos1.rows[0].click();
            
            ocultarProcesando();
        }catch(e){
            mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
        }
    }
	
    function aceptarClick(indexFila){
	    try{
            if (bProcesando()) return;

            var returnValue = $I("tblDatos").rows[indexFila].id + "@#@" +
	                             $I("tblDatos").rows[indexFila].cells[0].innerText + "@#@" +
	                             $I("tblDatos").rows[indexFila].getAttribute("idPlant") + "@#@" +
	                             $I("tblDatos").rows[indexFila].getAttribute("desPlant");
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
    
    function colorearNE(){
        try{
            switch(nNE){
                case 1:
                    $I("imgNE1").src = "../../images/imgNE1on.gif";
                    $I("imgNE2").src = "../../images/imgNE2off.gif";
                    $I("imgNE3").src = "../../images/imgNE3off.gif";
                    break;
                case 2:
                    $I("imgNE1").src = "../../images/imgNE1on.gif";
                    $I("imgNE2").src = "../../images/imgNE2on.gif";
                    $I("imgNE3").src = "../../images/imgNE3off.gif";
                    break;
                case 3:
                    $I("imgNE1").src = "../../images/imgNE1on.gif";
                    $I("imgNE2").src = "../../images/imgNE2on.gif";
                    $I("imgNE3").src = "../../images/imgNE3on.gif";
                    break;
            }
	    }catch(e){
		    mostrarErrorAplicacion("Error al establecer los colores del nivel de expansión", e.message);
        }
    }

    function mostrar(oImg){
        try{
            var oFila = oImg.parentNode.parentNode;
            var nIndexFila = oFila.rowIndex;
            var nNivel = oFila.getAttribute("nivel");
            //var nDesplegado = oFila.desplegado;
            if (oImg.src.indexOf("plus.gif") == -1) var opcion = "O"; //ocultar
            else var opcion = "M"; //mostrar
            //alert("nIndexFila: "+ nIndexFila +"\nnNivel: "+ nNivel +"\nOpción: "+ opcion +"\nDesplegado: "+ nDesplegado);
            
            //alert("nIndexFila: "+ nIndexFila);
            for (var i=nIndexFila+1; i< $I("tblDatos").rows.length; i++){
                if ($I("tblDatos").rows[i].getAttribute("nivel") > nNivel){
                    if (opcion == "O")
                    {
                        $I("tblDatos").rows[i].style.display = "none";
                        if ($I("tblDatos").rows[i].getAttribute("nivel") < 3)
                            $I("tblDatos").rows[i].cells[0].children[0].src = "../../images/plus.gif";
                    }
                    else if ($I("tblDatos").rows[i].getAttribute("nivel") - 1 == nNivel) $I("tblDatos").rows[i].style.display = "table-row";
                }else{
                    break;
                }
            }
            if (opcion == "O") oImg.src = "../../images/plus.gif";
            else oImg.src = "../../images/minus.gif"; 

            if (bMostrar) MostrarTodo(); 
            else ocultarProcesando();
	    }catch(e){
		    mostrarErrorAplicacion("Error al expandir/contraer", e.message);
        }
    }

    function MostrarOcultar(nMostrar){
        try{
            if ($I("tblDatos")==null){
                ocultarProcesando();
                return;
            }

            if (nMostrar == 0){//Contraer
                for (var i=0; i< $I("tblDatos").rows.length;i++){
                    if ($I("tblDatos").rows[i].getAttribute("nivel") > 1)
                    {
                        if ($I("tblDatos").rows[i].getAttribute("nivel") < 3)
                            $I("tblDatos").rows[i].cells[0].children[0].src = "../../images/plus.gif";
                        $I("tblDatos").rows[i].style.display = "none";
                    }
                    else 
                    {
                        $I("tblDatos").rows[i].cells[0].children[0].src = "../../images/plus.gif";
                    }                             
                }
                ocultarProcesando();
            }else{ //Expandir
                MostrarTodo();
            }
	    }catch(e){
		    mostrarErrorAplicacion("Error al expandir/contraer todo", e.message);
        }
    }

    var bMostrar=false;
    var nIndiceTodo = -1;
    function MostrarTodo(){
        try
        {
            if ($I("tblDatos")==null){
                ocultarProcesando();
                return;
            }

            var nIndiceAux = 0;
            if (nIndiceTodo > -1) nIndiceAux = nIndiceTodo;
            for (var i=nIndiceAux; i< $I("tblDatos").rows.length;i++){
                if ($I("tblDatos").rows[i].getAttribute("nivel") < nNE){ 
                    if ($I("tblDatos").rows[i].cells[0].children[0].src.indexOf("plus.gif") > -1){
                        bMostrar=true;
                        nIndiceTodo = i;
                        mostrar($I("tblDatos").rows[i].cells[0].children[0]);
                        return;
                    }
                }
            }
            bMostrar=false;
            nIndiceTodo = -1;
            ocultarProcesando();
	    }catch(e){
		    mostrarErrorAplicacion("Error al expandir toda la tabla", e.message);
        }
    }

    /* Función para establecer el nivel de expansión */
    var nNE = 1;
    function setNE(nValor){
        try{
            if ($I("tblDatos")==null){
                ocultarProcesando();
                return;
            }
            
            nNE = nValor;
            mostrarProcesando();
            
            colorearNE();
            setTimeout("setNE2()", 100);

	    }catch(e){
		    mostrarErrorAplicacion("Error al establecer el nivel de expansión", e.message);
        }
    }

    function setNE2(){
        try{
            MostrarOcultar(0);
            if (nNE > 1) MostrarOcultar(1);
	    }catch(e){
		    mostrarErrorAplicacion("Error al establecer el nivel de expansión", e.message);
        }
    }

	-->
    </script>
</head>
<body style="OVERFLOW: hidden; margin-left:10px;" class="texto" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
    <form id="form1" runat="server">
	<script type="text/javascript">
	<!--
        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";
	-->
	</script>
	<img src="<%=Session["strServer"] %>Images/imgSeparador.gif" width="1px" height="15px" />
    <table class="texto" style="margin-left:15px; width:520px;">
        <tr><td>
        <img id="imgNE1" src='../../images/imgNE1on.gif' class="ne" onclick="setNE(1);"><img id="imgNE2" src='../../images/imgNE2off.gif' class="ne" onclick="setNE(2);"><img id="imgNE3" src='../../images/imgNE3off.gif' class="ne" onclick="setNE(3);">
        </td></tr>
        <tr>
            <td>
                <TABLE id="tblTitulo" style="WIDTH: 500px; HEIGHT: 17px">
                    <TR class="TBLINI">
                        <td style="padding-left:25px">Grupo / Subgrupo / Naturaleza</td>
                    </TR>
                </TABLE>
                <DIV id="divCatalogo" style="OVERFLOW: auto; OVERFLOW-X: hidden; WIDTH: 516px; height:420px">
                    <%=strTablaHTML%>
                </DIV>
                <TABLE style="WIDTH: 500px; HEIGHT: 17px; margin-bottom:3px;">
                    <TR class="TBLFIN">
                        <TD></TD>
                    </TR>
                </TABLE>
            </td>
        </tr>
    </table>
    &nbsp;&nbsp;&nbsp;&nbsp;<img border="0" src="../../Images/imgGrupo.gif" />&nbsp;Grupo&nbsp;&nbsp;
    <img border="0" src="../../Images/imgSubgrupo.gif" />&nbsp;Subgrupo&nbsp;&nbsp;
    <img border="0" src="../../Images/imgNaturaleza.gif" />&nbsp;Naturaleza de producción&nbsp;&nbsp;
    <center>
        <table width="100px" align="center" style="margin-top:5px;">
		    <tr>
			    <td align="center">
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
<script type="text/javascript">
<!--
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
    	
-->
</script>
</body>
</html>
