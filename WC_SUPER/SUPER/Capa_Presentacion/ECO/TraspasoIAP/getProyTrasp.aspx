<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getProyTrasp.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head  runat="server">
    <title> ::: SUPER ::: Traspaso de consumos - Selección de proyecto</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script language="JavaScript" src="../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
   	<script language="JavaScript" src="Functions/PSN.js" type="text/Javascript"></script>
   	<script language="JavaScript" src="../../../Javascript/modal.js" type="text/Javascript"></script>
   	<script type="text/javascript">
    var bLectura=false;
    var bReordenado=false;
    function init(){
        try{
            if (!mostrarErrores()) return;
//            alert(opener.aPSNtemp.length);
//            for (var i=0; i < opener.aPSNtemp.length; i++){
//                mostrarDatosPSN2(opener.aPSNtemp[i].idPSN);
//            }
            crearTablaProyectos();
            if ($I("tblDatos").rows.length > 0){
                scrollTablaProy();
                actualizarLupas("tblTitulo", "tblDatos");
            }
            
            ocultarProcesando();
        }catch(e){
	        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
        }
    }

    function crearTablaProyectos(){
        try{
            var sb = new StringBuilder;
            var objPSN;
            
            sb.Append("<table id='tblDatos' class='texto MA' style='width: 920px;' cellpadding='0'>");
            sb.Append("<colgroup>");
			sb.Append("<col style='width:20px' />");
			sb.Append("<col style='width:20px' />");
			sb.Append("<col style='width:20px' />");
			sb.Append("<col style='width:20px' />");
			sb.Append("<col style='width:65px;' />");
			sb.Append("<col style='width:335px' />");
            sb.Append("<col style='width:200px' />");
			sb.Append("<col style='width:240px' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");
            
            for (var i=0; i < opener.aPSNtemp.length; i++){
                objPSN = opener.aPSNtemp[i];
                sb.Append("<tr onDblClick='aceptarClick(this.rowIndex)' style='height:20px' ");
                sb.Append("id='"+ objPSN.idPSN +"' onmouseover='TTip(event)' ");
                sb.Append("categoria='" + objPSN.categoria + "' ");
                sb.Append("cualidad='" + objPSN.cualidad + "' ");
                sb.Append("estado='" + objPSN.estado + "' ");
                sb.Append("traspasoIAP='" + objPSN.traspasoIAP + "' ");
                sb.Append(">");

                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");

                sb.Append("<td style='text-align:right; padding-right:10px;'>" + objPSN.idProyecto.ToString("N", 9, 0) + "</td>");
                sb.Append("<td><nobr class='NBR W330'>" + Utilidades.unescape(objPSN.denominacion) + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W190'>" + Utilidades.unescape(objPSN.cliente) + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W230'>" + Utilidades.unescape(objPSN.nodo) + "</nobr></td>");
                sb.Append("</tr>");
            }
            sb.Append("<tbody>");
            sb.Append("</table>");

            $I("divCatalogo").children[0].innerHTML = sb.ToString();
	    }catch(e){
		    mostrarErrorAplicacion("Error al crear la tabla de los proyectos.", e.message);
        }
    }
    
    function buscarPSNEnArray(idPSN){
        try{
            for (var nIndice=0; nIndice < opener.aPSNtemp.length; nIndice++){
                if (opener.aPSNtemp[nIndice].idPSN == idPSN){
                    return opener.aPSNtemp[nIndice];
                }
            }
            return null;
	    }catch(e){
		    mostrarErrorAplicacion("Error al buscar un profesional en el array.", e.message);
        }
    }

    var oImgTraspasoIAP = document.createElement("img");
    oImgTraspasoIAP.setAttribute("src", "../../../images/imgIcoTras.gif");
    oImgTraspasoIAP.setAttribute("style", "width:16px;height:16px;margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

    var nTopScrollProy = 0;
    var nIDTimeProy = 0;
    function scrollTablaProy(){
        try{
            if ($I("divCatalogo").scrollTop != nTopScrollProy){
                nTopScrollProy = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTimeProy);
            nIDTimeProy = setTimeout("scrollTablaProy()", 50);
                return;
            }
            var tblDatos = $I("tblDatos");
            var nFilaVisible = Math.floor(nTopScrollProy/20);
            var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight/20 + 1, tblDatos.rows.length);
            var oFila;
            for (var i = nFilaVisible; i < nUltFila; i++){
                if (!tblDatos.rows[i].getAttribute("sw")){
                    oFila = tblDatos.rows[i];
                    oFila.setAttribute("sw", 1);
                    
                    if (oFila.getAttribute("categoria")=="P") oFila.cells[0].appendChild(oImgProducto.cloneNode(true), null);
                    else oFila.cells[0].appendChild(oImgServicio.cloneNode(true), null);
                    
                    switch (oFila.getAttribute("cualidad")){
                        case "C": oFila.cells[1].appendChild(oImgContratante.cloneNode(true), null); break;
                        case "J": oFila.cells[1].appendChild(oImgRepJor.cloneNode(true), null); break;
                        case "P": oFila.cells[1].appendChild(oImgRepPrecio.cloneNode(true), null); break;
                    }

                    switch (oFila.getAttribute("estado")){
                        case "A": oFila.cells[2].appendChild(oImgAbierto.cloneNode(true), null); break;
                        case "C": oFila.cells[2].appendChild(oImgCerrado.cloneNode(true), null); break;
                        case "H": oFila.cells[2].appendChild(oImgHistorico.cloneNode(true), null); break;
                        case "P": oFila.cells[2].appendChild(oImgPresup.cloneNode(true), null); break;
                    }
                    
                    if (oFila.getAttribute("traspasoIAP")=="1") oFila.cells[3].appendChild(oImgTraspasoIAP.cloneNode(true), null);
                }
            }
        }
        catch(e){
            mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales.", e.message);
        }
    }

    function aceptarClick(indexFila){
        try{
            if (bProcesando()) return;
            
            if (bReordenado){
                var aTmp = new Array();
                var tblDatos = $I("tblDatos");
                for (var i=0; i<tblDatos.rows.length;i++){
                    aTmp[i] = buscarPSNEnArray(tblDatos.rows[i].id);
                }
                opener.aPSNtemp = aTmp;
            }            
            var returnValue = indexFila;
            modalDialog.Close(window, returnValue);	
        }catch(e){
            mostrarErrorAplicacion("Error seleccionar la fila", e.message);
        }
    }

    function salir(){
        var returnValue = null;
        modalDialog.Close(window, returnValue);	
    }
       </script>
</head>
<body onload="init()" onunload="unload()">
<form name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
    <script type="text/javascript">
        var strServer = "<% =Session["strServer"].ToString() %>";
        var intSession = <%=Session.Timeout%>;
    </script>    
    <br />
<table style="width:940px" cellpadding="5" >
	<tr>
		<td>
			<table id="tblTitulo" style="width:920px; height:17px">
			    <colgroup>
			        <col style="width:20px" />
			        <col style="width:20px" />
			        <col style="width:20px" />
			        <col style="width:20px" />
			        <col style="width:65px" />
			        <col style="width:335px" />
			        <col style="width:200px" />
			        <col style="width:240px" />
			    </colgroup>
				<tr class="TBLINI">
				    <td></td>
				    <td></td>
					<td colspan="3" style="text-align:right;">
						<IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',4,'divCatalogo','imgLupa1')"
							height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
					    <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',4,'divCatalogo','imgLupa1', event)"
							height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
						<IMG style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#img1" border="0">
					    <MAP name="img1">
					        <AREA onclick="bReordenado=true;ot('tblDatos', 4, 0, 'num')" shape="RECT" coords="0,0,6,5">
					        <AREA onclick="bReordenado=true;ot('tblDatos', 4, 1, 'num')" shape="RECT" coords="0,6,6,11">
				        </MAP>&nbsp;Nº&nbsp;&nbsp;
					</td>
					<td><IMG style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#img2" border="0">
						    <MAP name="img2">
						        <AREA onclick="bReordenado=true;ot('tblDatos', 5, 0, '')" shape="RECT" coords="0,0,6,5">
						        <AREA onclick="bReordenado=true;ot('tblDatos', 5, 1, '')" shape="RECT" coords="0,6,6,11">
					        </MAP>&nbsp;Proyecto&nbsp;<IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',5,'divCatalogo','imgLupa2')"
							height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',5,'divCatalogo','imgLupa2',event)"
							height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1">
					</td>
					<td><IMG style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#img3" border="0">
						    <MAP name="img3">
						        <AREA onclick="bReordenado=true;ot('tblDatos', 6, 0, '')" shape="RECT" coords="0,0,6,5">
						        <AREA onclick="bReordenado=true;ot('tblDatos', 6, 1, '')" shape="RECT" coords="0,6,6,11">
					        </MAP>&nbsp;Cliente&nbsp;<IMG id="imgLupa3" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',5,'divCatalogo','imgLupa3')"
							height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',5,'divCatalogo','imgLupa3',event)"
							height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
					</td>
					<td><IMG style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#img4" border="0">
						    <MAP name="img4">
						        <AREA onclick="bReordenado=true;ot('tblDatos', 7, 0, '')" shape="RECT" coords="0,0,6,5">
						        <AREA onclick="bReordenado=true;ot('tblDatos', 7, 1, '')" shape="RECT" coords="0,6,6,11">
					        </MAP>&nbsp;<label id="lblNodo2" runat="server">Nodo</label>&nbsp;<IMG id="imgLupa4" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',6,'divCatalogo','imgLupa4')"
							height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',6,'divCatalogo','imgLupa4',event)"
							height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
					</td>
				</tr>
			</table>
			<div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 936px; height: 500px;" align="left" onscroll="scrollTablaProy();">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:920px">
                        <%=strTablaHTML%>
                    </div>
            </div>
            <table id="tblResultado" style="width:920px; height:17px">
				<tr class="TBLFIN">
					<td>&nbsp;</td>
				</tr>
			</table>
		</td>
    </tr>
</table>
<table width="940px" border="0" class="texto" align="center">
    <colgroup>
        <col style="width:100px" />
        <col style="width:90px" />
        <col style="width:210px" />
        <col style="width:540px" />
    </colgroup>
	  <tr> 
	    <td><img class="ICO" src="../../../Images/imgProducto.gif" />Producto</td>
        <td><img class="ICO" src="../../../Images/imgServicio.gif" />Servicio</td>
        <td><img class="ICO" src="../../../Images/imgIcoTras.gif" />Traspaso realizado</td>
		<td rowspan="3" style="vertical-align:top;">
            <button id="btnCancelar" type="button" onclick="salir();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../images/imgCancelar.gif" /><span title="Cancelar">Cancelar</span></button>				  
		</td>
	  </tr>
	  <tr><td><img class="ICO" src="../../../Images/imgIconoContratante.gif" />Contratante</td>
            <td colspan="2"><img class="ICO" src="../../../Images/imgIconoRepPrecio.gif" />Replicado con gestión propia</td>
      </tr>
	  <tr>
	    <td style="vertical-align:top;"><img class="ICO" src="../../../Images/imgIconoProyAbierto.gif" title='Proyecto abierto' />Abierto</td>
        <td><img class="ICO" src="../../../Images/imgIconoProyCerrado.gif" title='Proyecto cerrado' />Cerrado</td>
        <td>
            <img class="ICO" src="../../../Images/imgIconoProyHistorico.gif" title='Proyecto histórico' />Histórico&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <img class="ICO" src="../../../Images/imgIconoProyPresup.gif" title='Proyecto presupuestado' />Presupuestado
        </td>
      </tr>
</table>
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

