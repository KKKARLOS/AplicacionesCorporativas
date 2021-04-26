<%@ Page Language="c#" CodeFile="getProfesionalVision.aspx.cs" Inherits="getProfesionalVision" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title> ::: SUPER ::: - Selección de profesional</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script language="JavaScript" src="../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
   	<script language="JavaScript" src="../../Javascript/boxover.js" type="text/Javascript"></script>
   	<script language="JavaScript" src="../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
	<!--
	function init(){
        try{
            if (!mostrarErrores()) return;
            if (es_administrador == "A" || es_administrador == "SA"){
                $I("lblNodo").className = "enlace";
                $I("lblNodo").onclick = function(){getNodo()};
                sValorNodo = $I("hdnIdNodo").value;
            }else sValorNodo = $I("cboCR").value;
		    $I("txtApellido1").focus();
			ocultarProcesando();
        }catch(e){
            mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
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
                case "profesionales":
	                $I("divCatalogo").children[0].innerHTML = aResul[2];
	                $I("divCatalogo").scrollTop = 0;
                    actualizarLupas("tblTitulo", "tblDatos");
                    scrollTabla();
                    window.focus();
                    break;
            }
            ocultarProcesando();
        }
    }

    function mostrarProfesionales(){
        try{
            if ($I("txtApellido1").value == "" && 
                $I("txtApellido2").value == "" && 
                $I("txtNombre").value == "" &&
                sValorNodo == ""){
                mmoff("War", "Debes introducir algún criterio de búsqueda", 300);
                $I("txtApellido1").focus();
                return;
            }

            var js_args = "profesionales@#@";
            js_args += Utilidades.escape($I("txtApellido1").value) +"@#@"; 
            js_args += Utilidades.escape($I("txtApellido2").value) +"@#@"; 
            js_args += Utilidades.escape($I("txtNombre").value) +"@#@";  
            js_args += sValorNodo+"@#@";  
            if ($I("chkBajas").checked) 
                js_args += "1"; 
            else
                js_args += "0"; 
            
            mostrarProcesando();
            RealizarCallBack(js_args, ""); 
            return;
            
        }catch(e){
	        mostrarErrorAplicacion("Error al obtener la relación de técnicos", e.message);
        }
    }
    
    function aceptarClick(indexFila){
        try{
            if (bProcesando()) return;
            var tbl = $I("tblDatos");
            var returnValue = tbl.rows[indexFila].id + "@#@" + tbl.rows[indexFila].getAttribute("tipo") + "@#@" + tbl.rows[indexFila].cells[1].innerText;
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
    function getNodo(){
        try{
            mostrarProcesando();
            var sPantalla = strServer + "Capa_Presentacion/ECO/getNodoAdmin.aspx";
            modalDialog.Show(sPantalla, self, sSize(500, 450))
                .then(function(ret) {
                    if (ret != null) {
                        var aDatos = ret.split("@#@");
                        sValorNodo = aDatos[0];
                        $I("hdnIdNodo").value = aDatos[0];
                        $I("txtDesNodo").value = aDatos[1];
                        borrarCatalogo();
                        if ($I("chkActuAuto").checked) mostrarProfesionales();
                        else ocultarProcesando();
                    }
                });
            window.focus();
            ocultarProcesando();
        }catch(e){
	        mostrarErrorAplicacion("Error al ir a obtener elemento.", e.message);
        }
    }

    var nTopScroll = -1;
    var nIDTime = 0;
    
    function scrollTabla(){
        try{
            if ($I("divCatalogo").scrollTop != nTopScroll){
                nTopScroll = $I("divCatalogo").scrollTop;
                clearTimeout(nIDTime);
                nIDTime = setTimeout("scrollTabla()", 50);
                return;
            }
            var tblDatos = $I("tblDatos");
            var nFilaVisible = Math.floor(nTopScroll/20);
            var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight/20, tblDatos.rows.length);
            var nContador = 0;
            var oFila;
            for (var i = nFilaVisible; i < tblDatos.rows.length; i++){
                if (!tblDatos.rows[i].getAttribute("sw")) {
                    oFila = tblDatos.rows[i];
                    oFila.setAttribute("sw", 1);

                    if (oFila.getAttribute("sexo") == "V") {
                        switch (oFila.getAttribute("tipo")) {
                            case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(true), null); break;
                            case "P": oFila.cells[0].appendChild(oImgIV.cloneNode(true), null); break;
                            case "F": oFila.cells[0].appendChild(oImgFV.cloneNode(true), null); break;
                        }
                    }else{
                    switch (oFila.getAttribute("tipo")) {
                            case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(true), null); break;
                            case "P": oFila.cells[0].appendChild(oImgIM.cloneNode(true), null); break;
                            case "F": oFila.cells[0].appendChild(oImgFM.cloneNode(true), null); break;
                        }
                    }

                    if (oFila.getAttribute("baja") == "1") 
                        setOp(oFila.cells[0].children[0], 20);
                }
                nContador++;
                if (nContador > $I("divCatalogo").offsetHeight/20 +1) break;
            }
        }
        catch(e){
	        mostrarErrorAplicacion("Error al controlar el scroll de la tabla.", e.message);
        }
    }
    function borrarNodo(){
        try{
            mostrarProcesando();
            if (es_administrador == "A" || es_administrador == "SA"){
                $I("hdnIdNodo").value = "";
                $I("txtDesNodo").value = "";
                sValorNodo = "";
            }else{
                $I("cboCR").value = "";
            }        
            sValorNodo = "";
            
            $I("divCatalogo").children[0].innerHTML = "";
            if ($I("chkActuAuto").checked) mostrarProfesionales();
            else ocultarProcesando();
	    }catch(e){
		    mostrarErrorAplicacion("Error al borrar al limpiar.", e.message);
        }
    }
    function borrarCatalogo(){
        try{
            $I("divCatalogo").children[0].innerHTML = "";
	    }catch(e){
		    mostrarErrorAplicacion("Error al borrar el catálogo.", e.message);
	    }
    }
//    function setActuAuto(){
//        try{
//            if ($I("chkActuAuto").checked){
//                setOp($I("btnObtener"), 30);
//                mostrarProfesionales();
//            }else{
//                setOp($I("btnObtener"), 100);
//            }

//	    }catch(e){
//		    mostrarErrorAplicacion("Error al modificar la opción de obtener de forma automática.", e.message);
//	    }
//    }
    function setCombo(){
        try{
            borrarCatalogo();
            if ($I('chkActuAuto').checked){
                mostrarProfesionales();
            }
	    }catch(e){
		    mostrarErrorAplicacion("Error al modificar los criterios.", e.message);
        }
    }
    function buscar(){
        try{
            borrarCatalogo();
            if ($I('chkActuAuto').checked){
                mostrarProfesionales();
            }
	    }catch(e){
		    mostrarErrorAplicacion("Error al modificar los criterios.", e.message);
        }
    }
	-->
    </script>
</head>
<body style="overflow: hidden" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
<form id="Form1" method="post" runat="server">
<script type="text/javascript">
<!--
    var intSession  = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
    var strServer   = "<%=Session["strServer"]%>";
    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
    var sValorNodo = "";
-->
</script>
<center>
<table style="width: 600px;text-align:left;margin-top:15px">
    <tr>
        <td>
        <fieldset style="width: 590px;">
        <legend>Criterios de selección</legend>   
            <table id="tblFiltros" style="width: 580px;" cellpadding="2" cellspacing="1">
            <colgroup>
                <col style="width:80px;" />
                <col style="width:300px;" />
                <col style="width:40px;" />
                <col style="width:60px;" />
                <col style="width:90px;" />
            </colgroup>
                <tr>
                    <td><label id="lblNodo" runat="server" class="texto">Nodo</label></td>
                    <td>
                        <asp:DropDownList id="cboCR" runat="server" style="width:300px" onChange="sValorNodo=this.value;setCombo()" AppendDataBoundItems=true>
                        <asp:ListItem Value="" Text=""></asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="txtDesNodo" style="width:290px;" Text="" readonly="true" runat="server" />
                    </td>
                    <td><img id="gomaNodo" src='../../Images/Botones/imgBorrar.gif' border='0' onclick="borrarNodo()" runat="server" style="cursor:pointer"></td>
                    <td>
                        <img src='../../Images/imgObtenerAuto.gif' border='0' title="Obtiene la información automáticamente al cambiar el valor de algún criterio de selección" style="vertical-align:bottom;">
                        <input type="checkbox" id="chkActuAuto" class="check" checked="checked" runat="server" />
                    </td>
                    <td>
					<button id="btnObtener" type="button" onclick="mostrarProfesionales();" class="btnH25W95" runat="server" hidefocus="hidefocus" onmouseover="mcur(this)">
						<img src="../../images/botones/imgObtener.gif" /><span title="Obtener">&nbsp;Obtener</span>
					</button>
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <table style="width: 480px;">
                            <colgroup>
                                <col style="width:130px"/>
                                <col style="width:130px"/>
                                <col style="width:130px"/>
                                <col style="width:90px"/>
                            </colgroup>                        
                            <tr>
                                <td>Apellido1</td>
                                <td>&nbsp;Apellido2</td>
                                <td>&nbsp;Nombre</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td><asp:TextBox ID="txtApellido1" runat="server" style="width:115px"  onkeypress="javascript:if(event.keyCode==13){buscar();event.keyCode=0;}" MaxLength="50" /></td>
                                <td><asp:TextBox ID="txtApellido2" runat="server" style="width:115px" onkeypress="javascript:if(event.keyCode==13){buscar();event.keyCode=0;}" MaxLength="50" /></td>
                                <td><asp:TextBox ID="txtNombre" runat="server" style="width:115px" onkeypress="javascript:if(event.keyCode==13){buscar();event.keyCode=0;}" MaxLength="50" /></td>
                                <td>
                                    <input id="chkBajas" hidefocus class="check" type="checkbox" runat="server" onclick="javascript:buscar();">Mostrar bajas
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </fieldset>
        </td>
    </tr>
	<tr>
		<td>
			<table id="tblTitulo" style="width: 600px; height: 17px; margin-top:10px">
				<tr class="TBLINI">
					<td>&nbsp;Profesional&nbsp;
						<IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa1')"
							height="11" src="../../Images/imgLupaMas.gif" width="20" tipolupa="2">
					    <IMG style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa1', event)"
							height="11" src="../../Images/imgLupa.gif" width="20" tipolupa="1"> 
					</td>
				</tr>
			</table>
	        <div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 616px; height:400px" runat="server" onscroll="scrollTabla();">
	            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:600px">
	                <table id='tblDatos' style='width: 600px;'>
	                </table>
	            </div>
            </div>
            <table style="width: 600px; height: 17px">
                <tr class="TBLFIN">
                    <td></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;<img class="ICO" src="../../Images/imgUsuIVM.gif" />&nbsp;&nbsp;&nbsp;Empleado interno&nbsp;&nbsp;&nbsp;
            <img class="ICO" src="../../Images/imgUsuEVM.gif" />&nbsp;Colaborador externo&nbsp;&nbsp;&nbsp;
            <img id="imgForaneo" class="ICO" src="../../Images/imgUsuFVM.gif" runat="server" />
            <label id="lblForaneo" runat="server">Foráneo</label>
        </td>
    </tr>
    <tr>
        <td align="center" style="padding-top:10px;">
	        <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../images/imgCancelar.gif" /><span title="Cancelar">Cancelar</span></button>				
        </td>
    </tr>
</table>
</center>
<asp:TextBox ID="hdnIdNodo" runat="server" style="width:1px;visibility:hidden" Text="" runat="server" />
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</form>
</body>
</html>
