<%@ Page Language="c#" CodeFile="obtenerPT.aspx.cs" Inherits="obtenerPT" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
	<title>Selección de proyecto técnico</title>
	<meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
  	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
	<!--
		function init(){
			if (!mostrarErrores()) return;
			buscarPTs("");
			ocultarProcesando();
		}
        function buscarPTs(strCli){
            var sNumPE;
            try{
                var js_args = "PT@#@"+ $I("txtPSN").value + "@#@I@#@"+ strCli;
                mostrarProcesando();
                RealizarCallBack(js_args, "");
                return false;
	        }catch(e){
		        mostrarErrorAplicacion("Error al obtener los proyectos técnicos(buscarPTs)", e.message);
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
		<table class="texto" style="width:400;">
            <tr>
                <td>
                    <label style="width:83px;">Proy. económico</label>
                    <asp:TextBox ID="txtNumPE" runat="server" SkinID="Numero" style="width:50px" readonly="true" />
                    <asp:TextBox ID="txtPE" runat="server" style="width:250px" readonly="true" />
                    <asp:TextBox ID="txtPSN" runat="server" style="visibility:hidden" />
                </td>
            </tr>
            <tr><td>&nbsp;</td></tr>
               <tr>
					<td>
						<table id="tblTitulo" style="height:17px; width:396px;" >
							<tr class="TBLINI">
								<td>&nbsp;Denominación&nbsp;
									<img id="imgLupa1" style="display:none; CURSOR:pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa1',event)" 
									    height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
								    <img style="display:none; CURSOR:pointer" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa1', event)" 
								        height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
								</td>
							</tr>
						</table>
				    </td>
				</tr>
                <tr>
                    <td>
                        <div id="divCatalogo" style="overflow:auto; width:412px; height:400px" >
							<table id="tblDatos" class="texto" style="width: 396px;">
    		                </table>
		                </div>
	                    <table id="tblResultado" style="height:17px; width:396px;">
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
	    strDatos = "";
	    var tblDatos = $I("tblDatos");
        strDatos += tblDatos.rows[indexFila].id + "///";
        strDatos += tblDatos.rows[indexFila].innerText + "///";
        strDatos += tblDatos.rows[indexFila].getAttribute("est") + "///";
        strDatos += tblDatos.rows[indexFila].getAttribute("une") + "///";
        strDatos += tblDatos.rows[indexFila].getAttribute("nPE") + "///";
        strDatos += tblDatos.rows[indexFila].getAttribute("dPE") + "///";
        strDatos += tblDatos.rows[indexFila].getAttribute("idP") + "///";
        strDatos += tblDatos.rows[indexFila].getAttribute("sAccesoBitacoraT")+ "///";
        strDatos += tblDatos.rows[indexFila].getAttribute("sAccBitacora");
        
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
