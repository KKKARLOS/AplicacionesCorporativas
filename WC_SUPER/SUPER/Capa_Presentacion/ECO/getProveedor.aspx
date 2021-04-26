<%@ Page Language="c#" CodeFile="getProveedor.aspx.cs" Inherits="getProveedor" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title> ::: SUPER ::: - Selección de proveedor</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script language="JavaScript" src="../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
	<!--
		function init(){
		    try{
			    if (strErrores != ""){
				    mostrarError(strErrores);
			    }
			    $I("txtProveedor").focus();
			    ocultarProcesando();
            }catch(e){
                mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
            }
		}
		
        function buscarProveedor(strProv){
            try{
                //alert(strProv);
                if (strProv == ""){
                    mmoff("Inf", "Introduce algún criterio de búsqueda", 265);
                    return;
                }
                var js_args = "proveedor@#@";
                var sAccion=getRadioButtonSelectedValue("rdbTipo",true);
                js_args += sAccion + "@#@";
                js_args += strProv +"@#@";
                js_args += nProfesionales;

                //alert(js_args);
                mostrarProcesando();
                RealizarCallBack(js_args, "");
                return false;
	        }catch(e){
		        mostrarErrorAplicacion("Error al obtener los proveedores", e.message);
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
                    case "proveedor":
                        $I("divCatalogo").scrollTop = 0;
                        if (aResul[2] == "EXCEDE"){
		                    $I("divCatalogo").children[0].innerHTML = "";
		                    mmoff("War", "La selección realizada excede un límite razonable. Por favor, acote más su consulta.", 500, 2500);
                        }else{
		                    $I("divCatalogo").children[0].innerHTML = aResul[2];
                            $I("txtProveedor").value = "";
                            actualizarLupas("tblTitulo", "tblDatos");
                        }
                        break;
                }
                ocultarProcesando();
            }
        }
	    function aceptarClick(indexFila){
		    try{
                if (bProcesando()) return;

                var returnValue = $I("tblDatos").rows[indexFila].id + "@#@" + $I("tblDatos").rows[indexFila].cells[1].innerText;
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
	-->
    </script>
</head>
<body style="OVERFLOW: hidden; margin-left:15px; margin-top:15px;" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
<form id="Form1" method="post" runat="server">
<script type="text/javascript">
<!--
    var strErrores = "<%=strErrores%>";
    var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
    var strServer = "<%=Session["strServer"]%>";
    var nProfesionales = "<%=nProfesionales%>";
-->
</script>
<center>  
<table style="width:516px; text-align:left" >
	    <colgroup><col style="width:360px"/><col style="width:156px"/></colgroup>
	    <tr>
	        <td>Denominación 
            <asp:TextBox ID="txtProveedor" runat="server" Width="280px" onKeyPress="javascript:if(event.keyCode==13){buscarProveedor(this.value);event.keyCode=0;return false;}"></asp:TextBox>
            </td>
            <td onclick="buscarProveedor($I('txtProveedor').value);">
            <asp:RadioButtonList ID="rdbTipo" SkinId="rbli" runat="server" RepeatColumns="2" ToolTip="Tipo de búsqueda">
                <asp:ListItem Value="I"><img src='../../Images/imgIniciaCon.gif' border='0' title="Inicia con" style="cursor:pointer" onclick="$I('rdbTipo_0').checked=true;" hidefocus=hidefocus></asp:ListItem>
                <asp:ListItem Selected="True" Value="C"><img src='../../Images/imgContieneA.gif' border='0' title="Contiene" style="cursor:pointer" onclick="$I('rdbTipo_1').checked=true;" hidefocus=hidefocus></asp:ListItem>
            </asp:RadioButtonList>
            </td>
        </tr>
		<tr>
			<td colspan="2">
				<table id="tblTitulo" style="margin-top:5px; width:500px; height:17px; text-align:left;">
					<tr class="TBLINI">
					    <td width="100px">&nbsp;Código&nbsp;<IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa1')"
								height="11" src="../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa1',event)"
								height="11" src="../../Images/imgLupa.gif" width="20" tipolupa="1">
						</td>
						<td>&nbsp;Proveedor&nbsp;<IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa2')"
								height="11" src="../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa2',event)"
								height="11" src="../../Images/imgLupa.gif" width="20" tipolupa="1">
						</td>
					</tr>
				</table>
		    </td>
		</tr>
        <tr>
            <td colspan="2">
                <div id="divCatalogo" style="OVERFLOW: auto; width: 516px; height: 350px" align="left">
                    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT16.gif); width:500px">
                    </div>
                </div>
                <table id="tblResultado" style="width:500px; height:17px;">
	                <tr class="TBLFIN">
		                <td>&nbsp;</td>
	                </tr>
                </table>
            </td>
        </tr>
</table>		
<table style="margin-top:5px; width:100px;">
    <tr>
        <td>
            <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../images/imgCancelar.gif" /><span title="Cancelar">Cancelar</span>
             </button>				
        </td>
    </tr>
</table>
</center>		
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</form>
</body>
</html>
