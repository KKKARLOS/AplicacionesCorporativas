<%@ Page Language="c#" CodeFile="obtenerPT.aspx.cs" Inherits="obtenerPT" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title> ::: SUPER ::: - Selección de proyecto técnico</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script language="JavaScript" src="../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
  	<script language="JavaScript" src="../../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
	<!--
		function init(){
			if (strErrores != ""){
				mostrarError(strErrores);
			}
			//$I("txtPT").focus();
			buscarPTs();
			ocultarProcesando();
		}
        function buscarPTs(){
            try{
                var js_args = "PT@#@"+ $I("txtPSN").value + "@#@I@#@";
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
                        break;
                }
                ocultarProcesando();
            }
        }
		
	-->
    </script>
   <style type="text/css">      
        #tblDatos td { padding-left: 5px; }
        #tblDatos tr { height: 18px; }
    </style>        
</head>
<body style="overflow: hidden" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
	<form id="Form1" method="post" runat="server">
	<script type="text/javascript">
	<!--
	    var strErrores = "<%=strErrores%>";
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
	    var strServer = "<%=Session["strServer"]%>";
	-->
	</script>
    <center>
	<table style="width:412px;text-align:left;margin-top:20px">		
	<tr>
		<td>
			<label style="width:83px;">Proy. económico</label>
			<asp:TextBox ID="txtNumPE" runat="server" SkinID="Numero" style="width:50px" readonly="true" />
			<asp:TextBox ID="txtPE" runat="server" style="width:250px" readonly="true" />
			<asp:TextBox ID="txtPSN" runat="server" style="visibility:hidden" />
		</td>
	</tr>
	   <tr>
			<td>
				<table id="tblTitulo" style="width: 396px; height: 17px">
					<tr class="TBLINI">
						<td>&nbsp;Denominación&nbsp;
							<img id="imgLupa1" style="display:none; cursor: pointer; height:11px; width:20px;" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa1')" src="../../../Images/imgLupaMas.gif" />
							<img style="cursor: pointer; height:11px; width:20px;" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa1',event)" src="../../../Images/imgLupa.gif" /> 
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td>
				<div id="divCatalogo" style="overflow: auto; width: 412px; height:396px">
					<div style="background-image:url('<%=Session["strServer"] %>Images/imgFT18.gif'); width:396px;">
						<table id="tblDatos" style="width: 396px;">
						</table>
					</div>
				</div>					
				<table style="width: 396px; height: 17px">
					<tr class="TBLFIN">
						<td></td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
	<br />
	<table width="300px" align="center" style="margin-top:5px;">
		<tr>
			<td align="center">
				<button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../images/imgCancelar.gif" /><span title="Cancelar">Cancelar</span></button>				
			</td>
		</tr>
	</table>	
    </center>
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
	</form>
	<script type="text/javascript">
<!--
	function aceptarClick(indexFila){
	    var tblDatos = $I("tblDatos");
	    strDatos = $I("tblDatos").rows[indexFila].id + "@#@" + $I("tblDatos").rows[indexFila].innerText;

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
</html>
