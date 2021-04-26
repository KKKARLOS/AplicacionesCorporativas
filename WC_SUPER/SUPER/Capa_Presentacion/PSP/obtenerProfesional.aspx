<%@ Page Language="c#" CodeFile="obtenerProfesional.aspx.cs" Inherits="SUPER.Capa_Presentacion.obtenerProfesional" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title> ::: SUPER ::: - Seleccione un profesional</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script language="JavaScript" src="../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
   	<script language="JavaScript" src="../../Javascript/boxover.js" type="text/Javascript"></script>
   	<script language="JavaScript" src="../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
	<!--
		function init(){
            try{
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
                        actualizarLupas("tblTitulo", "tblDatos");
                        scrollTablaProf();
                        break;
                }
                ocultarProcesando();
            }
        }
        function mostrarProfesionales(){
            try{
                if ($I("txtApellido1").value == "" && $I("txtApellido2").value == "" && $I("txtNombre").value == ""){
                    mmoff("War", "Debes introducir algún criterio de búsqueda", 300);
                    $I("txtApellido1").focus();
                    return;
                }
                var js_args = "profesionales@#@";
                js_args += Utilidades.escape($I("txtApellido1").value) +"@#@"; 
                js_args += Utilidades.escape($I("txtApellido2").value) +"@#@"; 
                js_args += Utilidades.escape($I("txtNombre").value); 
                
                //alert(js_args);
                mostrarProcesando();
                RealizarCallBack(js_args, ""); 
                return;
                
	        }catch(e){
		        mostrarErrorAplicacion("Error al obtener la relación de técnicos", e.message);
            }
        }
        
        function aceptar(){
            try{
                if (bProcesando()) return;
            
	            var strOpciones = "";
	            var sw = 0;
	            var tblDatos = $I("tblDatos");
	            for (var i=0; i<tblDatos.rows.length;i++){
	                if (tblDatos.rows[i].className == "FS"){
		                strOpciones = tblDatos.rows[i].id + "@#@" + tblDatos.rows[i].getAttribute("codred") + "@#@" + tblDatos.rows[i].cells[1].innerText  + "@#@";
		                sw = 1;
		                break;
		            }
	            }
	            if (sw == 0) {
	                mmoff("Inf", "Debes seleccionar algún elemento del catálogo", 330);
	                return;
	            }
	            var returnValue = strOpciones.substring(0,strOpciones.length-3);
	            modalDialog.Close(window, returnValue);
            }catch(e){
                mostrarErrorAplicacion("Error al aceptar", e.message);
            }
        }
        function aceptarClick(indexFila){
            try{
                if (bProcesando()) return;
                var tblDatos = $I("tblDatos");
                var returnValue = tblDatos.rows[indexFila].id + "@#@" + tblDatos.rows[indexFila].getAttribute("codred") + "@#@" + tblDatos.rows[indexFila].cells[1].innerText;
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


    var nTopScrollFICEPI = -1;
    var nIDTimeFICEPI = 0;
    function scrollTablaProf(){
        try{
            if ($I("divCatalogo").scrollTop != nTopScrollFICEPI){
                nTopScrollFICEPI = $I("divCatalogo").scrollTop;
                clearTimeout(nIDTimeFICEPI);
                nIDTimeFICEPI = setTimeout("scrollTablaProf()", 50);
                return;
            }
            var tblDatos = $I("tblDatos");
            var nFilaVisible = Math.floor(nTopScrollFICEPI/20);
            var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight/20+1, tblDatos.rows.length);
            var oFila;
            for (var i = nFilaVisible; i < nUltFila; i++) {
                if (!tblDatos.rows[i].getAttribute("sw")) {
                    oFila = tblDatos.rows[i];
                    oFila.setAttribute("sw",1);            
                    
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
            }
        }
        catch(e){
            mostrarErrorAplicacion("Error al controlar el scroll de la tabla de FICEPI.", e.message);
        }
    }       
	-->
    </script>
</head>
<body style="OVERFLOW: hidden; margin-left:15px;" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
<form id="Form1" method="post" runat="server">
<script type="text/javascript">
<!--
    var intSession  = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
    var strServer   = "<%=Session["strServer"]%>";
-->
</script>
<center>
<table style="margin-top:10px; width:412px; text-align:left">
	<tbody>
	    <tr>
	        <td>
                <table id="tblApellidos" style="width:360px; margin-bottom:5px;">
                    <colgroup>
                        <col style="width:120px"/>
                        <col style="width:120px"/>
                        <col style="width:120px"/>
                    </colgroup>
                    <tr>
                    <td>&nbsp;&nbsp;Apellido1</td>
                    <td>&nbsp;&nbsp;Apellido2</td>
                    <td>&nbsp;&nbsp;Nombre</td>
                    </tr>
                    <tr>
                    <td><asp:TextBox ID="txtApellido1" runat="server" style="width:100px"  onkeypress="javascript:if(event.keyCode==13){mostrarProfesionales();event.keyCode=0;}" MaxLength="50" /></td>
                    <td><asp:TextBox ID="txtApellido2" runat="server" style="width:100px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesionales();event.keyCode=0;}" MaxLength="50" /></td>
                    <td><asp:TextBox ID="txtNombre" runat="server" style="width:100px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesionales();event.keyCode=0;}" MaxLength="50" /></td>
                    </tr>
                </table>
	        </td>
	    </tr>
		<tr>
			<td>
				<table id="tblTitulo" style="height:17px; width:396px">
				    <colgroup><col style="width:20px"/><col style="width:376px"/></colgroup>
					<tr class="TBLINI">
					    <td width="20px"></td>
						<td width="376px">&nbsp;Profesional&nbsp;
							<IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa1')"
								height="11" src="../../Images/imgLupaMas.gif" width="20" tipolupa="2">
						    <IMG style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa1', event)"
								height="11" src="../../Images/imgLupa.gif" width="20" tipolupa="1"> 
						</td>
					</tr>
				</table>
				<div id="divCatalogo" style="overflow:auto; overflow-x:hidden; width:412px; height:280px" onscroll="scrollTablaProf()">
			         <div style='background-image:url(../../Images/imgFT20.gif); width:396px;'>
			             <table class="texto" id="tblDatos" style="width: 396px"></table>
			         </div>
                </div>
	                <table id="tblResultado" style="height:17px;width:396px">
		                <tr class="TBLFIN"><td></td></tr>
	                </table>
    			</td>
	        </tr>
	    </tbody>
	</table>
<br />
<table style="width:412px;">		
    <tr>
        <td style="margin-left:15px; vertical-align:top; height:23px;">
            <img border="0" src="../../Images/imgUsuPVM.gif" />&nbsp;Interno&nbsp;&nbsp;&nbsp;
            <img border="0" src="../../Images/imgUsuEVM.gif" />&nbsp;Externo&nbsp;&nbsp;&nbsp;
            <img id="imgForaneo" class="ICO" src="../../Images/imgUsuFVM.gif" runat="server" />
            <label id="lblForaneo" runat="server">Foráneo</label>
        </td>
    </tr>
</table>
<br />
<table style="margin-top:15px; width:300px; text-align:center;">
    <tr>
        <td>
            <button id="btnAceptar" type="button" onclick="aceptar();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../images/imgAceptar.gif" /><span title="Aceptar">Aceptar</span></button>								
        </td>
        <td>
            <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../images/imgCancelar.gif" /><span title="Cancelar">Cancelar</span></button>				
        </td>
    </tr>
</table>
</center>     
<uc_mmoff:mmoff ID="mmoff1" runat="server" />       
</form>
</body>
</html>
