<%@ Page Language="c#" CodeFile="getCliente.aspx.cs" Inherits="getCliente" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title> ::: SUPER ::: - Selecci?n de cliente</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
		function init(){
		    try{
            if (!mostrarErrores()) return;
			    $I("txtCliente").focus();
			    ocultarProcesando();
            }catch(e){
                mostrarErrorAplicacion("Error en la inicializaci?n de la p?gina", e.message);
            }
		}
		
		var bMsg = false;
        function buscarClientes(strCli){
            try{
                //alert(strCli);
                if (strCli == ""){
                    bMsg = !bMsg;
                    if (bMsg) mmoff("Inf", "Introduce alg?n criterio de b?squeda", 265);
                    return;
                }
                var js_args = "cliente@#@";
                var sAccion=getRadioButtonSelectedValue("rdbTipo",true);
                js_args += sAccion + "@#@";
                js_args += strCli + "@#@";
                js_args += sInterno + "@#@";
                js_args += sSoloActivos + "@#@";
                js_args += sTipo
                //alert(js_args);
                mostrarProcesando();
                RealizarCallBack(js_args, "");
	        }catch(e){
		        mostrarErrorAplicacion("Error al obtener los clientes", e.message);
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
                    case "cliente":
                        $I("divCatalogo").scrollTop = 0;
                        if (aResul[2] == "EXCEDE"){
		                    $I("divCatalogo").children[0].innerHTML = "";
		                    mmoff("War", "La selecci?n realizada excede un l?mite razonable. Por favor, acote m?s su consulta.", 500, 2500);
                        }else{
		                    $I("divCatalogo").children[0].innerHTML = aResul[2];
                            $I("txtCliente").value = "";
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
                
	            strDatos = "";
	            strDatos = $I("tblDatos").rows[indexFila].id + "@#@" + $I("tblDatos").rows[indexFila].cells[0].innerText + "@#@" + 
                           $I("tblDatos").rows[indexFila].getAttribute("nif") + "@#@" +
	                       $I("tblDatos").rows[indexFila].getAttribute("ef");
	            //strDatos = strDatos.substring(0,strDatos.length-3);

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
		
    </script>
</head>
<body style="OVERFLOW: hidden" leftMargin="15" topMargin="15" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
	<form id="Form1" method="post" runat="server">
	<script type="text/javascript">
	    var sInterno = "<%=sInterno%>";
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
	    var strServer = "<%=Session["strServer"]%>";
	    var sSoloActivos = "<%=sSoloActivos%>";
	    var sTipo = "<%=sTipo%>";
    </script>
		<br />
		
		<fieldset style="margin-left:17px; width:540px; height:45px;text-align:left">
		    <legend>B?squeda por denominaci?n, c?digo SAP o CIF</legend>
		    <table>
			    <tr>
			        <td>&nbsp;Cadena de b?squeda 
                    <asp:TextBox ID="txtCliente" runat="server" Width="280px" onKeyPress="javascript:if(event.keyCode==13){bMsg = false;buscarClientes(this.value);event.keyCode=0;return false;}" />
                    </td>
                    <td>
                    <asp:RadioButtonList ID="rdbTipo" SkinId="rbli" runat="server" RepeatColumns="2" ToolTip="Tipo de b?squeda" onclick="buscarClientes($I('txtCliente').value);">
                        <asp:ListItem Value="I"><img src='../../../../Images/imgIniciaCon.gif' border='0' title="Inicia con" style="cursor:pointer" hidefocus=hidefocus onclick="$I('rdbTipo_0').click();"></asp:ListItem>
                        <asp:ListItem Selected="True" Value="C"><img src='../../../../Images/imgContieneA.gif' border='0' title="Contiene" style="cursor:pointer" hidefocus=hidefocus onclick="$I('rdbTipo_1').click();"></asp:ListItem>
                    </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
		</fieldset>
		<center>
		<table style="width:566px; text-align:left">
			<tbody>
				<tr>
					<td colspan="2">
						<table id="tblTitulo" style="margin-top:10px;height:17px;width:550px">
						    <colgroup>
						        <col style="width:450px;" />
						        <col style="width:100px;" />
						    </colgroup>
							<tr class="TBLINI">
								<td>&nbsp;Denominaci?n&nbsp;<IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa1')"
										height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa1',event)"
										height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"></td>
								<td>NIF</td>
							</tr>
						</table>
				    </td>
				</tr>
                <tr>
                    <td colspan="2">
                        <div id="divCatalogo" style="overflow: auto; width: 566px; height: 290px; text-align:left">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:550px">
							    <%=strTablaHTML%>
    		                </div>
		                </div>
		                <table id="tblResultado" style="height:17px;width:550px;text-align:left">
			                <tr class="TBLFIN">
				                <td>&nbsp;</td>
			                </tr>
                            <tr>
                                <td style="padding-top:3px;">
                                    <img class="ICO" src="../../../../images/imgM.gif" />Matriz&nbsp;&nbsp;&nbsp;
                                    <img class="ICO" src="../../../../images/imgF.gif" />Filial
                                </td>
                            </tr>
			                <tr>
				                <td align="center" style="padding-top:5px;">
                                    <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../../images/imgCancelar.gif" /><span title="Cancelar">Cancelar</span></button>				
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
