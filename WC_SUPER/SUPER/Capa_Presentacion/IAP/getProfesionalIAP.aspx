<%@ Page Language="c#" CodeFile="getProfesionalIAP.aspx.cs" Inherits="SUPER.Capa_Presentacion.getProfesionalIAP" %>
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
            if (sPerfil != "P"){
                $I("tblApellidos").style.visibility = "visible";
            }else{
                $I("txtApellido1").value = sApellido1;
                $I("txtApellido2").value = sApellido2;
                $I("txtNombre").value = sNombre;
                mostrarProfesionales();
            }
            if (sPerfil == "RG") $I("chkTodos").disabled = false;
			ocultarProcesando();
			
			try{
			    $I("txtApellido1").focus();
			}catch(e){}
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
                    case "profesionales":
                        $I("divCatalogo").children[0].innerHTML = aResul[2];
                        scrollTablaProf();
                        $I("txtApellido1").value = ""; 
                        $I("txtApellido2").value = ""; 
                        $I("txtNombre").value = ""; 
                        break;
                    case "setUsuarioIAP":
                        sRetorno = aResul[2];
                        aceptarSalir();
                        break;
                }
                ocultarProcesando();
            }
        }

        function mostrarProfesionales(){
            try{
                if ($I("txtApellido1").value == "" && $I("txtApellido2").value == "" && $I("txtNombre").value == ""){
                    mmoff("Inf", "Debes introducir algún criterio de búsqueda",300);
                    $I("txtApellido1").focus();
                    return;
                }
                var js_args = "profesionales@#@";
                js_args += sPerfil +"@#@"; 
                js_args += Utilidades.escape($I("txtApellido1").value) +"@#@"; 
                js_args += Utilidades.escape($I("txtApellido2").value) +"@#@"; 
                js_args += Utilidades.escape($I("txtNombre").value); 
                mostrarProcesando();
                RealizarCallBack(js_args, ""); 
                $I("chkTodos").checked = false;
               
	        }catch(e){
		        mostrarErrorAplicacion("Error al obtener la relación de técnicos", e.message);
            }
        }

        function mostrarTodos(){
            try{
                if ($I("chkTodos").checked == false) return;

                $I("txtApellido1").value = "";
                $I("txtApellido2").value = "";
                $I("txtNombre").value = "";
                
                var js_args = "profesionales@#@";
                js_args += sPerfil +"@#@"; 
                js_args += Utilidades.escape($I("txtApellido1").value) +"@#@"; 
                js_args += Utilidades.escape($I("txtApellido2").value) +"@#@"; 
                js_args += Utilidades.escape($I("txtNombre").value); 
                mostrarProcesando();
                RealizarCallBack(js_args, ""); 
                return;
                
	        }catch(e){
		        mostrarErrorAplicacion("Error al obtener la relación de técnicos", e.message);
            }
        }
                
        var sRetorno = "";
        function aceptarClick(indexFila){
            try{
                if (bProcesando()) return;
                
                mostrarProcesando();
                
                var js_args = "setUsuarioIAP@#@";
                js_args += $I("tblDatos").rows[indexFila].id;
               
                RealizarCallBack(js_args, "");
            }catch(e){
                mostrarErrorAplicacion("Error seleccionar la fila", e.message);
            }
        }

        function aceptarSalir(){
            try{
                var returnValue = sRetorno;
                modalDialog.Close(window, returnValue);
            }catch(e){
                mostrarErrorAplicacion("Error seleccionar la fila", e.message);
            }
        }

        function cerrarVentana(){
	        var returnValue = null;
	        modalDialog.Close(window, returnValue);
        }
               
               
        var nTopScrollFICEPI = -1;
        var nIDTimeFICEPI = 0;
        function scrollTablaProf() {
            try {
                if ($I("divCatalogo").scrollTop != nTopScrollFICEPI) {
                    nTopScrollFICEPI = $I("divCatalogo").scrollTop;
                    clearTimeout(nIDTimeFICEPI);
                    nIDTimeFICEPI = setTimeout("scrollTablaProf()", 50);
                    return;
                }

                var nFilaVisible = Math.floor(nTopScrollFICEPI / 20);
                var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight / 20 + 1, $I("tblDatos").rows.length);
                var oFila;
                for (var i = nFilaVisible; i < nUltFila; i++) {
                    //if (!tblDatos.rows[i].sw){
                    if (!$I("tblDatos").rows[i].getAttribute("sw")) {
                        oFila = $I("tblDatos").rows[i];
                        //oFila.sw = 1;
                        oFila.setAttribute("sw", 1);

                        //if (oFila.sexo=="V"){
                        if (oFila.getAttribute("sexo") == "V") {
                            //switch (oFila.tipo) {
                            switch (oFila.getAttribute("tipo")) {
                                case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(true), null); break;
                                case "P": oFila.cells[0].appendChild(oImgPV.cloneNode(true), null); break;
                                case "F": oFila.cells[0].appendChild(oImgFV.cloneNode(true), null); break;
                            }
                        } else {
                            //switch (oFila.tipo){
                            switch (oFila.getAttribute("tipo")) {
                                case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(true), null); break;
                                case "P": oFila.cells[0].appendChild(oImgPM.cloneNode(true), null); break;
                                case "F": oFila.cells[0].appendChild(oImgFM.cloneNode(true), null); break;
                            }
                        }

                        if (oFila.getAttribute("baja") == "1") setOp(oFila.cells[0].children[0], 20);
                    }
                }
            }
            catch (e) {
                mostrarErrorAplicacion("Error al controlar el scroll de la tabla de FICEPI.", e.message);
            }
        }           
	-->
    </script>
</head>
<body style="overflow: hidden"  onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
	<form id="Form1" method="post" runat="server">
	<script type="text/javascript">
	<!--
	    var intSession  = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
	    var strServer   = "<%=Session["strServer"]%>";
	    var sPerfil     = "<%=Session["perfil_iap"]%>";
	    var sApellido1  = "<%=Session["APELLIDO1"]%>";
	    var sApellido2  = "<%=Session["APELLIDO2"]%>";
	    var sNombre     = "<%=Session["NOMBRE"]%>";
	-->
    </script>
        <center>
		<table  width="412px" align="center" border="0" style="margin-top:10px;">
			<tbody>
			    <tr>
			        <td>
                        <table id="tblApellidos" border="0" class="texto" style="WIDTH: 390px;visibility:hidden;margin-bottom:5px;" cellpadding="0" cellspacing="0">
                            <colgroup>
                            <col style="width:130px" /><col style="width:130px" /><col style="width:130px" />
                            </colgroup>
                            <tr>
                            <td>&nbsp;Apellido1</td>
                            <td>&nbsp;Apellido2</td>
                            <td>&nbsp;Nombre</td>
                            </tr>
                            <tr>
                            <td><asp:TextBox ID="txtApellido1" runat="server" style="width:110px"  onkeypress="javascript:if(event.keyCode==13){mostrarProfesionales();event.keyCode=0;}" MaxLength="50" /></td>
                            <td><asp:TextBox ID="txtApellido2" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesionales();event.keyCode=0;}" MaxLength="50" /></td>
                            <td><asp:TextBox ID="txtNombre" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesionales();event.keyCode=0;}" MaxLength="50" /></td>
                            </tr>
                        </table>
			        </td>
			    </tr>
				<tr>
					<td>
						<table id="tblTitulo" style="width:396px; height:17px"  align="left">
							<tr class="TBLINI">
								<td align="left">&nbsp;Profesional
										<img id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa1')"height="11" src="../../Images/imgLupaMas.gif" width="20">
								        <img style="CURSOR: pointer" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa1',event)" height="11" src="../../Images/imgLupa.gif" width="20">
										</td>
							</tr>
						</table>
			        </td>
			    </tr>
				<tr>
					<td>
						<div id="divCatalogo" style="overflow: auto; width: 412px; height: 267px;" onscroll="scrollTablaProf()">
						    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif);width: 396px;" >
                                <table id="tblDatos" class="texto MA" style="WIDTH: 396px;">
                                    <colgroup><col style="width:20px;" /><col style="width:376px;"/></colgroup>
                                </table>
                             </div>
		                </div>
		                <table id="tblResultado" style="width:396px; height:17px" align="left" >
			                <tr class="TBLFIN">
				                <td>&nbsp;</td>
			                </tr>
			                <tr height="5px">
				                <td >&nbsp;</td>
			                </tr>
		                </table>
		            </td>
		        </tr>
				<tr>
					<td>	
                        <table style="width:396px;margin-top:20px">
                            <colgroup><col style='width:150px;' /><col style='width:246px;'/></colgroup>
                            <tr>
                                <td>
                                    Mostrar todos <input type="checkbox" id="chkTodos" onclick="mostrarTodos()" hidefocus=hidefocus disabled="disabled" />
                                </td>
                                <td>            
                                    <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../images/imgCancelar.gif" /><span>Cancelar</span></button>
                                </td>
                            </tr>                
                        </table>						        
		            </td>
		        </tr>		        
		</tbody>
		</table>
	    </center>
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
	</form>
</body>
</html>
