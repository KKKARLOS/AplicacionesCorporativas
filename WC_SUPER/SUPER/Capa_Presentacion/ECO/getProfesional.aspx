<%@ Page Language="c#" CodeFile="getProfesional.aspx.cs" Inherits="getProfesional" %>
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
                mostrarErrorSQL(aResul[3], aResul[2]);
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
                if ($I("hdnTipoProf").value=="" && $I("txtApellido1").value == "" && $I("txtApellido2").value == "" && $I("txtNombre").value == ""){
                    mmoff("War", "Debes introducir algún criterio de búsqueda", 300);
                    $I("txtApellido1").focus();
                    return;
                }
                var js_args = "profesionales@#@";
                js_args += Utilidades.escape($I("txtApellido1").value) +"@#@"; 
                js_args += Utilidades.escape($I("txtApellido2").value) +"@#@"; 
                js_args += Utilidades.escape($I("txtNombre").value) +"@#@"; 
                js_args += $I("hdnTipoProf").value; 
                
                //alert(js_args);
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
                var sTexto = $I("tblDatos").rows[indexFila].cells[1].innerText;
                var returnValue = $I("tblDatos").rows[indexFila].id + "@#@" + sTexto + "@#@" +
                                     $I("tblDatos").rows[indexFila].getAttribute("idficepi");
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
        var nTopScrollProf = -1;
        var nIDTimeProf = 0;
        function scrollTablaProf() {
            try {
                if ($I("tblDatos") == null) return;
                if ($I("divCatalogo").scrollTop != nTopScrollProf) {
                    nTopScrollProf = $I("divCatalogo").scrollTop;
                    clearTimeout(nIDTimeProf);
                    nIDTimeProf = setTimeout("scrollTablaProf()", 50);
                    return;
                }

                var tblDatos = $I("tblDatos");
                var nFilaVisible = Math.floor(nTopScrollProf / 18);
                var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight / 18 + 1, tblDatos.rows.length);
                var oFila;
                for (var i = nFilaVisible; i < nUltFila; i++) {
                    if (!tblDatos.rows[i].getAttribute("sw")) {
                        oFila = tblDatos.rows[i];
                        oFila.setAttribute("sw", 1);

                        if (oFila.getAttribute("sexo") == "V") {
                            switch (oFila.getAttribute("tipo")) {
                                case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(true), null); break;
                                case "N": oFila.cells[0].appendChild(oImgNV.cloneNode(true), null); break;
                                case "P": oFila.cells[0].appendChild(oImgPV.cloneNode(true), null); break;
                                case "F": oFila.cells[0].appendChild(oImgFV.cloneNode(true), null); break;
                            }
                        } else {
                            switch (oFila.getAttribute("tipo")) {
                                case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(true), null); break;
                                case "N": oFila.cells[0].appendChild(oImgNM.cloneNode(true), null); break;
                                case "P": oFila.cells[0].appendChild(oImgPM.cloneNode(true), null); break;
                                case "F": oFila.cells[0].appendChild(oImgFM.cloneNode(true), null); break;
                            }
                        }

                    }
                }
            }
            catch (e) {
                mostrarErrorAplicacion("Error al controlar el scroll de profesionales.", e.message);
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
	-->
    </script>
    <center>
	<table style="margin-top:10px; width:440px; text-align:left;">
		<tbody>
		    <tr>
		        <td>
                    <table id="tblApellidos" style="width:390px; margin-bottom:5px; text-align:left;">
                    <colgroup>
                    <col style="width:130px"/>
                    <col style="width:130px"/>
                    <col style="width:130px"/>
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
					<table id="tblTitulo" style="height:17px;width:420px">
						<tr class="TBLINI">
							<td>&nbsp;Profesional&nbsp;
								<IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa1')"
									height="11" src="../../Images/imgLupaMas.gif" width="20" tipolupa="2">
							    <IMG style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa1',event)"
									height="11" src="../../Images/imgLupa.gif" width="20" tipolupa="1"> 
							</td>
						</tr>
					</table>
					<div id="divCatalogo" style="overflow:auto; overflow-x:hidden; width:436px; height:378px" onscroll="scrollTablaProf();">
                        <div style='background-image:url(../../Images/imgFT18.gif); width:420px;'>
						</div>
	                </div>
                    <table style="height:17px;width:420px">
                        <tr class="TBLFIN">
                            <td></td>
                        </tr>
                    </table>
	            </td>
	        </tr>
	        <tr>
	            <td>
                    <img class="ICO" src="../../Images/imgUsuPVM.gif" />&nbsp;Interno&nbsp;&nbsp;&nbsp;
                    <img class="ICO" src="../../Images/imgUsuEVM.gif" />&nbsp;Externo&nbsp;&nbsp;&nbsp;
                    <img id="imgForaneo" class="ICO" src="../../Images/imgUsuFVM.gif" runat="server" />
                    <label id="lblForaneo" runat="server">Foráneo</label>
	            </td>
	        </tr>
	    </tbody>
	</table>
    <table style="margin-top:10px; width:100px;">
        <tr>
            <td>
                <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
                     onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../images/botones/imgCancelar.gif" /><span title="Cancelar">Cancelar</span>
                </button>	
            </td>
        </tr>
    </table>
	</center>
    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
    <input type="hidden" name="hdnForaneos" id="hdnForaneos" value="N" runat="server" />
    <input type="hidden" name="hdnTipoProf" id="hdnTipoProf" value="" runat="server" />
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
	</form>
</body>
</html>
