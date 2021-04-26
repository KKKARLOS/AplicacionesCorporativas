<%@ Page Language="c#" CodeFile="obtenerPT2.aspx.cs" Inherits="obtenerPT2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
	<title> ::: SUPER ::: - Selección de proyecto técnico</title>
	<meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
   	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
	<!--
		function init(){
            if (!mostrarErrores()) return;
			$I("txtPT").focus();
			ocultarProcesando();
		}
		
        function buscarPTs(strCli){
            var sNumPE;
            try{
                if (strCli == ""){
                    mmoff("Inf", "Introduce algún criterio de búsqueda", 265);
                    return;
                }
                var sAccion="I";
                var js_args = "PT@#@"+ sAccion + "@#@"+ strCli;//no pasamos codigo de PE
                mostrarProcesando();
                RealizarCallBack(js_args, "");
                return false;
	        }catch(e){
		        mostrarErrorAplicacion("Error al obtener los proyectos técnicos(buscarPTs)", e.message);
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
                    case "PT":
		                $I("divCatalogo").innerHTML = aResul[2];
                        //$I("txtPT").value = "";
                        actualizarLupas("tblTitulo", "tblDatos");
                        break;
                }
                ocultarProcesando();
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
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
	    var strServer = "<%=Session["strServer"]%>";
	-->
	</script>
		<br />
		<table class="texto" style="width:600px;">
            <tr>
                <td align="center">
                    <asp:RadioButtonList ID="rdbTipo" SkinID="rbl" runat="server" Height="20px" RepeatColumns="2" ToolTip="Tipo de búsqueda">
                        <asp:ListItem Selected="True" Value="I">Inicia con</asp:ListItem>
                        <asp:ListItem Value="C">Contiene</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
		    <tr>
		        <td align="center">Proyecto técnico 
                    <asp:TextBox ID="txtPT" runat="server" Width="400px" onKeyPress="javascript:if(event.keyCode==13){buscarPTs(this.value);event.keyCode=0;}"></asp:TextBox><br /><br />
                </td>
            </tr>
            <tr>
				<td>
					<table id="tblTitulo" style="height:17px; width:596px;">
						<tr class="TBLINI">
							<td width="50%">&nbsp;Proyecto económico&nbsp;
								<img id="imgLupa1" style="display: none; cursor: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa1',event)" 
								    height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2" />
							    <img style="display: none; cursor: pointer" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa1',event)" 
							        height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1" /> 
							</td>
							<td width="50%">&nbsp;Proyecto técnico&nbsp;
								<img id="imgLupa2" style="display: none; cursor: pointer" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa2', event)" 
								        height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
							    <img style="display: none; cursor: pointer" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa2',event)" 
							            height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
							</td>
						</tr>
					</table>
			    </td>
			</tr>
            <tr>
                <td>
                    <div id="divCatalogo" style="overflow:auto; width: 612px; height: 450px">
						<table id="tblDatos" class="texto" style="width: 596px;">
		                </table>
	                </div>
	                <table id="tblResultado" style="height:17;" width="596" border="0">
		                <tr class="TBLFIN"><td></td></tr>
                    </table>
	            </td>
	        </tr>
            <tr>
                <td>
				    <center>
				        <button id="btnCancelar" type="button" style="margin-top:10px;" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
				            <img src="../../../../images/imgCancelar.gif" /><span title="Cancelar">Cancelar</span>
				        </button>	
				    </center>			
                </td>
            </tr>
        </table>
    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=strErrores %>" />
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
	</form>
	<script type="text/javascript">
<!--
	function aceptarClick(indexFila){
	    var tblDatos = $I("tblDatos");
	    strDatos = tblDatos.rows[indexFila].id + "///" + tblDatos.rows[indexFila].getAttribute("des") + "///" + 
		           tblDatos.rows[indexFila].getAttribute("est") + "///" + tblDatos.rows[indexFila].getAttribute("une") + "///" +
	               tblDatos.rows[indexFila].getAttribute("idPE") + "///" + tblDatos.rows[indexFila].getAttribute("desPE") + "///" + 
	               tblDatos.rows[indexFila].getAttribute("idT305PE")+ "///" + tblDatos.rows[indexFila].getAttribute("desune") + "///" + 
	               tblDatos.rows[indexFila].getAttribute("sAccesoBitacoraT");

		var returnValue = strDatos;
		modalDialog.Close(window, returnValue);
	}
	function cerrarVentana(){
		var returnValue = null;
		modalDialog.Close(window, returnValue);
	}
-->
    </script>
</body>
</HTML>
