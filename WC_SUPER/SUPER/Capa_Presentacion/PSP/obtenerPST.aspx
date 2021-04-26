<%@ Page Language="c#" CodeFile="obtenerPST.aspx.cs" Inherits="SUPER.Capa_Presentacion.obtenerPST" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title> ::: SUPER ::: - Seleccione una orden de trabajo codificada</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script language="JavaScript" src="../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
   	<script language="JavaScript" src="../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
	    function init() {
	        try {
	            if (strErrores != "") {
	                mostrarError(strErrores);
	            }
	            if ($I("hdnIdCliente").value =="272"){
	                $I("divArea").className = "mostrarcapa";
	                $I("divActivas").className = "mostrarcapa";
	                $I("divObtener").className = "mostrarcapa";
	            }
	            $I("chkActivas").style.verticalAlign ="middle";
	            $I("chkActivas").nextSibling.style.verticalAlign ="middle";
	            $I("chkCliente").style.verticalAlign ="middle";
	            $I("chkCliente").nextSibling.style.verticalAlign = "middle";
	            $I("chkClienteNulo").style.verticalAlign ="middle";
	            $I("chkClienteNulo").nextSibling.style.verticalAlign = "middle";
	            $I("chkCR").style.verticalAlign ="middle";
	            $I("chkCR").nextSibling.style.verticalAlign = "middle";

	            //scrollTabla();
	            window.focus();
	            ocultarProcesando();
	        } catch (e) {
	            mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
	        }
	    }

	    function controlarNulos(){
	        if(!($I("chkCliente").checked)){
	            $I("chkClienteNulo").checked= false;
	            $I("chkClienteNulo").disabled = true;

	        }
	        else{
	           $I("chkClienteNulo").disabled = false;
	        }
	        restringir();
	    }

	    function comprobarDatos(){
	        
	        if (!($I("chkCliente").checked) && !($I("chkCR").checked)) {
	            mmoff("Inf", "Debes indicar algún criterio para la búsqueda", 410);
	            return false;
	        }

	        return true;
	    }

	    function obtener() {
	        try {
	            if(!comprobarDatos()) return false;
	            var sb = new StringBuilder;
	            sb.Append("obtener@#@");

	            sb.Append((($I("chkCliente").checked)? $I("hdnIdCliente").value : "") + "@#@");
                sb.Append((($I("chkCR").checked)? $I("hdnCR").value : "") + "@#@");
                sb.Append($I("cboArea").value + "@#@");	            
                sb.Append($I("chkActivas").checked + "@#@");
                sb.Append($I("chkClienteNulo").checked + "@#@");                
	            mostrarProcesando();
	            RealizarCallBack(sb.ToString(), "");
	            return false;
	        } catch (e) {
	            mostrarErrorAplicacion("Error al obtener las OTC", e.message);
	        }
	    }
	    function RespuestaCallBack(strResultado, context) {
	        actualizarSession();
	        var aResul = strResultado.split("@#@");
	        if (aResul[1] != "OK") {
	            ocultarProcesando();
	            var reg = /\\n/g;
	            mostrarError(aResul[2].replace(reg, "\n"));
	        } else {
	            switch (aResul[0]) {
	                case "obtener":
	                    $I("divCatalogo").children[0].innerHTML = aResul[2];
	                    //scrollTabla();
	                    window.focus();
	                    break;
	            }
	            ocultarProcesando();
	        }
	    }
	    function aceptarClick(indexFila) {
	        try {
	            if (bProcesando()) return;
	            var tblOpciones = $I("tblOpciones");

	            strOpciones = tblOpciones.rows[indexFila].id + "@#@" + tblOpciones.rows[indexFila].cells[0].innerText + "@#@" + tblOpciones.rows[indexFila].cells[1].innerText + "@#@";
	            strOpciones = strOpciones.substring(0, strOpciones.length - 3);

	            var returnValue = strOpciones;
	            modalDialog.Close(window, returnValue);
	        } catch (e) {
	            mostrarErrorAplicacion("Error seleccionar la fila", e.message);
	        }
	    }

	    function cerrarVentana() {
	        try {
	            if (bProcesando()) return;

	            var returnValue = null;
	            modalDialog.Close(window, returnValue);
	        } catch (e) {
	            mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
	        }
	    }

	    function restringir() {
	        try {
	            obtener();
	        } catch (e) {
	            mostrarErrorAplicacion("Error al restringir las ordenes de trabajo codificadas", e.message);
	        }
	    }
    </script>
<style type="text/css">
    #tblOpciones tr { height:20px; }
</style>    
</head>
<body style="OVERFLOW: hidden" leftMargin="15" topMargin="15" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
	<form id="Form1" method="post" runat="server">
	<script type="text/javascript">
	    var strErrores = "<%=strErrores%>";
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
	    var strServer = "<%=Session["strServer"]%>";
	</script>
	<br />
	<center>
    <table style="width:870px;text-align:left" >
    <colgroup>
    <col style="width:225px; " />
    <col style="width:225px;" />
    <col style="width:225px;" />
    <col style="width:95px;" />
    </colgroup>
        <tr>
		    <td>
                 <fieldset style="width:200px; margin-left:15px;vertical-align:bottom;">
					<legend><asp:CheckBox ID="chkCliente" runat="server" Text="Restringidos al cliente" onclick="controlarNulos();" style="cursor:pointer; margin-left:8px" Checked=true />   
                     </legend>
					 <div id="clienteNulo" style="padding-left:20px;">
                        <asp:CheckBox ID="chkClienteNulo" runat="server" Text="Incluir sin cliente " onclick="restringir();"  style="cursor:pointer; margin-left:8px" Checked=false />
                    </div>  
                </fieldset>
		        
		    </td>
		    <td>               
               <asp:CheckBox ID="chkCR" runat="server" Text="Restringidos al CR del proyecto" onclick="restringir();"  style="cursor:pointer; margin-left:8px" Checked=TRUE />	      
               <div id="divArea" style="margin-bottom:-5px" class="ocultarcapa">
		            <label >Área</label>
		            <asp:DropDownList runat="server" id="cboArea" onchange="obtener();">
		                <asp:ListItem Value="">Todas</asp:ListItem>
		                <asp:ListItem Value="C">A. Comunes</asp:ListItem>
		                <asp:ListItem Value="E">Ecofin</asp:ListItem>
		                <asp:ListItem Value="H">RRHH</asp:ListItem>
		                <asp:ListItem Value="J">Juego</asp:ListItem>
		                <asp:ListItem Value="S">SSAA</asp:ListItem>
		                <asp:ListItem Value="W">Web</asp:ListItem>
		                <asp:ListItem Value="V">Juegos ONCE</asp:ListItem>
		            </asp:DropDownList>
		        </div>
		    </td>
		    <td>                
                <div id="divActivas" class="ocultarcapa">
		            <asp:CheckBox ID="chkActivas" runat="server" onclick="obtener();" Text="OTCs activas " Width="800" style="cursor:pointer; margin-left:8px" Checked=true />
		        </div>
		    </td>
		    <td>
                <div id="divObtener" class="ocultarcapa">
                    <button id="btnObtener" type="button" onclick="obtener();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../Images/imgObtener.gif" /><span title="Obtener">Obtener</span></button>				
                </div>
		    </td>
        </tr>
        <tr><td colspan="4">&nbsp;</td></tr>
		<tr>
			<td colspan="4">
			    <table id="tblTitulo" style="width: 850px; height: 17px">
				    <colgroup>
				        <col style="width:200px"/>
				        <col style="width:275px"/>
				        <col style="width:250px"/>
				        <col style="width:125px"/>
				    </colgroup>			    
					<tr class="TBLINI">
						<td>&nbsp;
                            <img style="CURSOR: pointer" height="11" src="../../Images/imgFlechas.gif" width="6" useMap="#imgCod" border="0"> 
					        <map name="imgCod">
						        <area onclick="ot('tblOpciones', 0, 0, '', '');" shape="RECT" coords="0,0,6,5">
						        <area onclick="ot('tblOpciones', 0, 1, '', '');" shape="RECT" coords="0,6,6,11">
					        </map>					
						    &nbsp;Código&nbsp;
							<img id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblOpciones',0,'divCatalogo','imgLupa1')"
								height="11" src="../../Images/imgLupaMas.gif" width="20" tipolupa="2">
						    <img style="CURSOR: pointer" onclick="buscarDescripcion('tblOpciones',0,'divCatalogo','imgLupa1',event)"
								height="11" src="../../Images/imgLupa.gif" width="20" tipolupa="1"> 
						</td>
						<td>
                            <img style="CURSOR: pointer" height="11" src="../../Images/imgFlechas.gif" width="6" useMap="#imgDes" border="0"> 
					        <map name="imgDes">
						        <area onclick="ot('tblOpciones', 1, 0, '', '');" shape="RECT" coords="0,0,6,5">
						        <area onclick="ot('tblOpciones', 1, 1, '', '');" shape="RECT" coords="0,6,6,11">
					        </map>					
						    &nbsp;Descripción&nbsp;
							<img id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblOpciones',1,'divCatalogo','imgLupa2')"
								height="11" src="../../Images/imgLupaMas.gif" width="20"  tipolupa="2">
						    <img style="CURSOR: pointer" onclick="buscarDescripcion('tblOpciones',1,'divCatalogo','imgLupa2',event)"
								height="11" src="../../Images/imgLupa.gif" width="20" tipolupa="1"> 
						</td>
					    <td>
                            <img style="CURSOR: pointer" height="11" src="../../Images/imgFlechas.gif" width="6" useMap="#imgCli" border="0"> 
					        <map name="imgCli">
						        <area onclick="ot('tblOpciones', 2, 0, '', '');" shape="RECT" coords="0,0,6,5">
						        <area onclick="ot('tblOpciones', 2, 1, '', '');" shape="RECT" coords="0,6,6,11">
					        </map>					
					        &nbsp;Cliente&nbsp;
						    <img id="imgLupa3" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblOpciones',2,'divCatalogo','imgLupa3')"
							    height="11" src="../../Images/imgLupaMas.gif" width="20">
					        <img style="CURSOR: pointer" onclick="buscarDescripcion('tblOpciones',2,'divCatalogo','imgLupa3',event)"
							    height="11" src="../../Images/imgLupa.gif" width="20"> 
				        </td>
					    <td>
                            <img style="CURSOR: pointer" height="11" src="../../Images/imgFlechas.gif" width="6" useMap="#imgFec" border="0"> 
					        <map name="imgFec">
						        <area onclick="ot('tblOpciones', 3, 0, 'fec', '');" shape="RECT" coords="0,0,6,5">
						        <area onclick="ot('tblOpciones', 3, 1, 'fec', '');" shape="RECT" coords="0,6,6,11">
					        </map>					
					        &nbsp;Fecha Ref.&nbsp;
						    <img id="imgLupa4" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblOpciones',3,'divCatalogo','imgLupa4')"
							    height="11" src="../../Images/imgLupaMas.gif" width="20">
					        <img style="CURSOR: pointer" onclick="buscarDescripcion('tblOpciones',3,'divCatalogo','imgLupa4',event)"
							    height="11" src="../../Images/imgLupa.gif" width="20"> 
				        </td>
					</tr>
				</table>
                <div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 866px; height: 350px">
					<div style='background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:850px'>
					<%=strTablaHtml %>
					</div>
                </div>
                <table id="tblResultado" style="width: 850px; height: 17px">
	                <tr class="TBLFIN"><td></td></tr>
                </table>
            </td>
        </tr>
	</table>
    <br />
    <table width="300px" align="center" style="margin-top:5px;">
        <tr>
            <td align="center">
                <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../images/imgCancelar.gif" /><span title="Cancelar">Cancelar</span></button>				
            </td>
        </tr>
    </table>
    <input type="hidden" name="hdnIdCliente" id="hdnIdCliente" value="" runat="server" />
    <input type="hidden" name="hdnCR" id="hdnCR" value="" runat="server" />
    </center>    
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
	</form>
</body>
</html>
