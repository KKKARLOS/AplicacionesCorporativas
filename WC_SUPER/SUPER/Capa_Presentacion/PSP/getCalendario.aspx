<%@ Page Language="c#" CodeFile="getCalendario.aspx.cs" Inherits="SUPER.Capa_Presentacion.PSP.getCalendario" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title> ::: SUPER ::: - Selección de calendario</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script language="JavaScript" src="../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
	<!--
	function init(){
        try{
            if (!mostrarErrores()) return;
			//actualizarLupas("tblTitulo", "tblDatos");
            //seleccionar($I("rdbTipoBusqueda_0"));
            setTipo();
		    window.focus();
		    ocultarProcesando();
        }catch(e){
            mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
        }
	}

    function aceptarClick(indexFila){
        try{
            if (bProcesando()) return;
            
            strOpciones = "";
    	    
            strOpciones = $I("tblDatos").rows[indexFila].id + "@#@" + $I("tblDatos").rows[indexFila].innerText + "@#@" + $I("tblDatos").rows[indexFila].getAttribute("njl");

            var returnValue = strOpciones;
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

    var sAmb = "";
    function setTipo(){
        try{
            var sOp = getRadioButtonSelectedValue("rdbTipoBusqueda", true);
            if (sOp == sAmb) return;
            else{
                sAmb = sOp;
                if (sAmb == "E" || sAmb == "T"){
                    $I("cboCR").value = "";
                    $I("cboCR").disabled = true;
                    getCal();
                }else $I("cboCR").disabled = false;
                $I("divCatalogo").children[0].innerHTML = "";
                window.focus();
            }
        }catch(e){
            mostrarErrorAplicacion("Error al establecer el tipo de calendario", e.message);
        }
    }
    function getCal(){
        try{
            var sOp = getRadioButtonSelectedValue("rdbTipoBusqueda", true);
            var js_args = "getCal@#@";
            js_args += sOp + "@#@";
            js_args += $I("cboCR").value + "@#@";
            js_args += idficepi
            //alert(js_args);
            mostrarProcesando();
            RealizarCallBack(js_args, "");
        }catch(e){
            mostrarErrorAplicacion("Error al obtener los calendarios", e.message);
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
                case "getCal":
	                $I("divCatalogo").children[0].innerHTML = aResul[2];
                    actualizarLupas("tblTitulo", "tblDatos");
                    window.focus();
                    break;
            }
            ocultarProcesando();
        }
    }
-->
</script>
<style type="text/css">      
    #tblDatos td { padding-left: 5px; }
    #tblDatos tr { height: 16px; }
</style>    
</head>
<body style="overflow: hidden" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
	<form id="Form1" method="post" runat="server">
	<script type="text/javascript">
	<!--
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
	    var strServer = "<%=Session["strServer"]%>";
	    var idficepi = "<%=Request.QueryString["idficepi"]%>";
	-->
	</script>
	<br />
	<center>
	<table style="width:416px; text-align:left;">
	    <colgroup>
	        <col style="width:60px;" />
	        <col style="width:356px;" />
	    </colgroup>
		<tbody>
		    <tr style="vertical-align:super;">
		        <td><span style="vertical-align:super;">Ámbito</span></td>
                <td ><asp:RadioButtonList ID="rdbTipoBusqueda" SkinID="rbl" runat="server" Height="20px" Width="335px" RepeatColumns="3" onclick="setTipo();">
                    <asp:ListItem Value="E" Text="Empresarial" Selected="True" />
                    <asp:ListItem Value="D" Text="Departamental" />
                    <asp:ListItem Value="T" Text="Todos" />
                </asp:RadioButtonList>
		        </td>
		    </tr>
            <tr>
                <td><label id="lblNodo" runat="server" class="texto">Nodo</label></td>
                <td>&nbsp;&nbsp;<asp:DropDownList id="cboCR" runat="server" Width="335px" AppendDataBoundItems="true" onchange="getCal()" Enabled=false>
                    <asp:ListItem Value="" Text="" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
			<tr>
				<td colspan="2">
					<table id="tblTitulo" style="height:17px; width:400px; margin-top:5px;">
						<tr class="TBLINI">
							<td>&nbsp;<%=strColumna %>
								<IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa1')"
									height="11" src="../../Images/imgLupaMas.gif" width="20"  tipolupa="2">
							    <IMG style="CURSOR: pointer" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa1',event)"
									height="11" src="../../Images/imgLupa.gif" width="20" tipolupa="1"> 
							</td>
						</tr>
					</table>
					<div id="divCatalogo" style="OVERFLOW: auto; width: 416px; height:288px">
					    <div style='background-image:url(../../Images/imgFT16.gif); width:400px;'>
							<%=strTablaHtml %>
						</div>
	                </div>
	                <table id="tblResultado" style="height:17px;width:400px">
		                <tr class="TBLFIN">
			                <td>&nbsp;</td>
		                </tr>
		                <tr>
			                <td style="padding-top:6px;">
			                    <center>
				                    <table style="height:20px;margin-top:10px">
				                    <tr>
				                        <td>
					                        <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../images/imgCancelar.gif" /><span title="Cancelar">Cancelar</span></button>				
				                        </td>
				                    </tr>
				                    </table>
				                </center>
			                </td>
		                </tr>
	                </table>
	            </td>
	        </tr>
	    </tbody>
	</table>
	</center>
    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
	</form>
</body>
</html>
