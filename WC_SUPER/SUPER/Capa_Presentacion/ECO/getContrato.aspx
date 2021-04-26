<%@ Page Language="c#" CodeFile="getContrato.aspx.cs" Inherits="getContrato" %>
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
	<!--
		function init(){
		    try{
            if (!mostrarErrores()) return;
            //actualizarLupas("tblTitulo", "tblDatos");
            window.focus();
            $I("txtIdContrato").focus();
			ocultarProcesando();
            }catch(e){
                mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
            }
		}
		
    function aceptarClick(indexFila){
	    try{
            if (bProcesando()) return;
            
            var strDatos = Utilidades.unescape($I("tblDatos").rows[indexFila].id);

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
    
    function getCliente(){
        try{
            mostrarProcesando();
            //var ret = window.showModalDialog(strServer + "Capa_Presentacion/ECO/getCliente.aspx?interno=0&sSoloActivos=0", self, sSize(600, 480));
            modalDialog.Show(strServer + "Capa_Presentacion/ECO/getCliente.aspx?interno=0&sSoloActivos=0", self, sSize(600, 480))
	        .then(function(ret) {
	            if (ret != null){
		            var aDatos = ret.split("@#@");
                    $I("hdnIdCliente").value = aDatos[0];
                    $I("txtDesCliente").value = aDatos[1];
                    borrarCatalogo();
	            }
                ocultarProcesando();
	        }); 
	    }catch(e){
		    mostrarErrorAplicacion("Error al obtener los clientes", e.message);
        }
    }
    function borrarCliente(){
        try{
            $I("hdnIdCliente").value = "";
            $I("txtDesCliente").value = "";
	    }catch(e){
		    mostrarErrorAplicacion("Error al borrar el cliente", e.message);
        }
    } 
    function setIdContrato(){
        try{
            
            $I("txtDesContra").value = "";
    		
            borrarCatalogo();
	    }catch(e){
		    mostrarErrorAplicacion("Error al introducir el número de contrato", e.message);
        }
    }

    function setDesContra(){
        try{
// igual interesa los contratos que empiezan por 'x' de un cliente determinado        
            $I("txtIdContrato").value = "";

            borrarCatalogo();
	    }catch(e){
		    mostrarErrorAplicacion("Error al introducir la denominación del contrato", e.message);
        }
    }    
    
    function borrarCatalogo(){
        try{
            $I("divCatalogo").children[0].innerHTML = "";
	    }catch(e){
		    mostrarErrorAplicacion("Error al borrar el catálogo", e.message);
        }
    }
    function buscarContratos(){
        try{
            var js_args = "contrato@#@";
            //Por indicación de Víctor, el 03/03/2015 No tenemos en cuenta el nodo a la hora de buscar
            //if (opener.$I("hdnIdNodo")==null) js_args += "@#@";
            //else js_args += opener.$I("hdnIdNodo").value + "@#@";
            //if ($I("hdnIdNodo").value != "") js_args += "@#@";
            //else js_args += $I("hdnIdNodo").value + "@#@";
            js_args += "@#@";
            js_args += ($I("chkTodos").checked==true)? "1@#@" : "0@#@";
            js_args += sOrigen + "@#@";
            js_args += $I("txtIdContrato").value + "@#@";
            js_args += Utilidades.escape($I("txtDesContra").value) + "@#@";
            var sAccion=getRadioButtonSelectedValue("rdbTipoBusqueda",true);
            js_args += sAccion + "@#@";
            js_args += $I("hdnIdCliente").value;
            
            //alert('parametros: '+js_args);
            mostrarProcesando();
            RealizarCallBack(js_args, "");
           
        }catch(e){
	        mostrarErrorAplicacion("Error al ir a obtener los contratos", e.message);
        }
	}
	
    function numContrato(){
        try{
            if ($I("txtIdContrato").value=="") return;      
            mostrarProcesando();
            var js_args = "contratoID@#@"+$I("txtIdContrato").value;
            
            RealizarCallBack(js_args, "");
        }catch(e){
            mostrarErrorAplicacion("Error al buscar contrato por su número.", e.message);
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
                case "contrato":
	                $I("divCatalogo").children[0].innerHTML = aResul[2];
                    actualizarLupas("tblTitulo", "tblDatos");
                    break;
               case "contratoID":
                    $I("txtDesContra").value = Utilidades.unescape(aResul[2]);
                    setTimeout("buscarContratos()",250);             
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
	        setTimeout("mostrarExtensiones()", 20);
        }catch(e){
            mostrarErrorAplicacion("Error al mostrar / ocultar las extensiones", e.message);
        }
    }
    
    function mostrarExtensiones(){
	    try{
	        var oImg = oImagen;
            var idContrato = oImg.parentNode.parentNode.id;
            var bMostrar = (oImg.src.indexOf("plus")==-1)? false:true;
            //alert(idContrato +" // "+ bMostrar);
            if (bMostrar) oImg.src = "../../images/minus.gif";
            else oImg.src = "../../images/plus.gif";
            //alert(oImg.parentNode.parentNode.rowIndex);
            var nInicio = oImg.parentNode.parentNode.rowIndex;
            //for (var i=0; i< tblDatos.rows.length; i++){
            for (var i = nInicio; i < $I("tblDatos").rows.length; i++) {
                if ($I("tblDatos").rows[i].id != idContrato) break;
                if ($I("tblDatos").rows[i].id == idContrato && tblDatos.rows[i].getAttribute("nNivel") == 2) {
                    if (bMostrar) $I("tblDatos").rows[i].style.display = "table-row";
                    else $I("tblDatos").rows[i].style.display = "none";
                }
            }
            ocultarProcesando();
        }catch(e){
            mostrarErrorAplicacion("Error al mostrar / ocultar las extensiones", e.message);
        }
    }
   	    
	-->
    </script>
</head>
<body style="overflow:hidden; margin-left:15px; margin-top:15px;" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
	<form id="Form1" method="post" runat="server">
	<script type="text/javascript">
	<!--
        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";
	    var sOrigen = "<%=sOrigen%>";
	-->
	</script>
	<img src="<%=Session["strServer"] %>Images/imgSeparador.gif" width="1px" height="1px" />
    <table border="0" class="texto" width="990px" style="margin-left:10px;" cellpadding="5" cellspacing="0">
        <tr>
            <td>
		        <fieldset style="width:970px;">
		            <legend>Criterios de búsqueda</legend>
		            <table class="texto" style="margin-left:10px; width:950px;" cellpadding="3" >
		                <colgroup>
		                    <col style="width:70px;" />
		                    <col style="width:500px;" />
		                    <col style="width:180px;" />
		                    <col style="width:100px;" />
		                    <col style="width:100px;" />
		                </colgroup>
			            <tr>
	                        <td>Contrato</td>
			                <td><asp:TextBox ID="txtIdContrato" style="width:60px;" Text="" SkinID="Numero" runat="server" onkeypress="if(event.keyCode==13){event.keyCode=0;numContrato();}else{vtn2(event);setIdContrato();}" />
			                <asp:TextBox ID="txtDesContra" style="width:422px;" Text="" MaxLength="70" runat="server" onkeypress="if(event.keyCode==13){buscarContratos();event.keyCode=0;}else{setDesContra();}" />
			                </td>
			                <td><asp:RadioButtonList ID="rdbTipoBusqueda" SkinId="rbli" runat="server" Height="20px" RepeatColumns="2">
						                <asp:ListItem Value="I"><img src='../../Images/imgIniciaCon.gif' border='0' title="Inicia con" style="cursor:pointer" hidefocus=hidefocus></asp:ListItem>
						                <asp:ListItem Selected="True" Value="C"><img src='../../Images/imgContieneA.gif' border='0' title="Contiene" style="cursor:pointer" hidefocus=hidefocus></asp:ListItem>
				                </asp:RadioButtonList>
				            </td>
				            <td colspan="2">
				                <label id="lblTodos" title="Muestra también los contratos sin importe pendiente">Mostrar todos</label> <input type=checkbox id="chkTodos" class="check" onclick="buscarContratos();" runat="server" />
			                </td>
			            </tr>
					    <tr>
						    <td><label id="lblCliente" class="enlace" onclick="getCliente()" onmouseover="mostrarCursor(this)">Cliente</label></td>
						    <td><asp:TextBox ID="txtDesCliente" style="width:460px;" Text="" readonly="true" runat="server" />&nbsp;
						    <img src='../../Images/Botones/imgBorrar.gif' border='0' title="Borra el cliente" onclick="borrarCliente()" style="cursor:pointer; vertical-align:middle;"><asp:TextBox ID="hdnIdCliente" style="width:1px;visibility:hidden" Text="" readonly="true" SkinID="Numero" runat="server" />
						    </td>
						    <td style="vertical-align:bottom;">
				                <button id="btnObtener" type="button" onclick="buscarContratos();" class="btnH25W85" runat="server" hidefocus="hidefocus" 
					                 onmouseover="se(this, 25);mostrarCursor(this);">
					                <img src="../../Images/imgObtener.gif" /><span>Obtener</span>
				                </button>					
						    </td>
						    <td colspan="2">
						        
						    </td>
                        </tr>				
                    </table>
		        </fieldset>	
		        <br />	
                <table id="tblTitulo" style="margin-top:10px; width:970px; height:17px;">
                    <colgroup>
                        <col style="width:310px;" />
                        <col style="width:120px;" />
                        <col style="width:80px;" />
                        <col style="width:80px;" />
                        <col style="width:80px;" />
                        <col style="width:80px;" />
                        <col style="width:220px;" />
                    </colgroup>
	                <tr class="texto" style="height:20px;">
                        <td colspan="2">&nbsp;</td>
                        <td width="160px" colspan="2" class="colTabla" style="text-align:center">Producto</td>
                        <td width="160px" colspan="2" class="colTabla1" style="text-align:center">Servicio</td>
                        <td style="text-align: right;"></td>
	                </tr>
                    <tr class="TBLINI">
                        <td style="padding-left:20px;">Oportunidad / Extensión</td>
                        <td style="text-align:right; padding-right:5px;">
                            <img style="cursor: pointer; display: none;" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa1',event)"
							    height="11" src="../../Images/imgLupa.gif" width="20" tipolupa="1"> 
							<img id="imgLupa1" style="display: none; cursor: pointer" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa1')"
							    height="11" src="../../Images/imgLupaMas.gif" width="20" tipolupa="2">&nbsp;Contrato
					    </td>
                        <td style="text-align:right; padding-right:5px;">Contratado</td>
                        <td style="text-align:right; padding-right:5px;">Pendiente</td>
                        <td style="text-align:right; padding-right:5px;">Contratado</td>
                        <td style="text-align:right; padding-right:5px;">Pendiente</td>
                        <td style="padding-left:5px;">&nbsp;Cliente&nbsp;
                            <img id="imgLupa3" style="display: none; cursor: pointer" onclick="buscarSiguiente('tblDatos',6,'divCatalogo','imgLupa3')"
							    height="11" src="../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
							<img style="cursor: pointer; display: none;" onclick="buscarDescripcion('tblDatos',6,'divCatalogo','imgLupa3',event)"
							    height="11" src="../../Images/imgLupa.gif" width="20" tipolupa="1"> 
					    </td>
                    </tr>
                </table>
                <div id="divCatalogo" style="overflow: auto; overflow-x:hidden; width: 986px; height:310px">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:970px;">
                    <%=strTablaHTML %>
                    </div>
                </div>
                <table style="width:970px; height:17px" >
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
    <input type="hidden" name="hdnIdNodo" id="hdnIdNodo" value="" runat="server"/>
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
