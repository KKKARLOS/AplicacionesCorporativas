<%@ Page Language="c#" CodeFile="obtenerPT2.aspx.cs" Inherits="obtenerPT2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title> ::: SUPER ::: - Selección de proyecto técnico</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script language="JavaScript" src="../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
   	<script language="JavaScript" src="../../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
	<!--
		function init(){
            if (!mostrarErrores()) return;
			$I("txtPT").focus();
			ocultarProcesando();
		}
		
        function buscarPTs(strCli){
            try{
                if (strCli == ""){
                    mmoff("Inf", "Introduce algún criterio de búsqueda", 265);
                    return;
                }
                //var sAccion="I";
                var sAccion=getRadioButtonSelectedValue("rdbTipo",true);
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
		                $I("divCatalogo").children[0].innerHTML = aResul[2];
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
    var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
    var strServer = "<%=Session["strServer"]%>";
-->
</script>
    <center>
	<table style="width:800px;text-align:left;margin-top:10px">		
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
            <asp:TextBox ID="txtPT" runat="server" Width="400px" onKeyPress="javascript:if(event.keyCode==13){buscarPTs(this.value);event.keyCode=0;return false;}"></asp:TextBox><br /><br />
        </td>
    </tr>
    <tr>
		<td>
			<table id="tblTitulo" style="height:17px; width:780px;text-align:left; ">
			    <colgroup>
			        <col style="width:390px;"/>
			        <col style="width:390px;"/>
			    </colgroup>
				<tr class="TBLINI">
					<td>
						&nbsp;Proyecto económico
						<img id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa1')" height="11" src="../../../Images/imgLupaMas.gif" width="20"/>
					    <img style="CURSOR: pointer" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa1',event)" height="11" src="../../../Images/imgLupa.gif" width="20"/> 
					</td>
					<td>
					    &nbsp;Proyecto técnico
						<img id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa2')" height="11" src="../../../Images/imgLupaMas.gif" width="20"/>
					    <img style="CURSOR: pointer" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa2',event)" height="11" src="../../../Images/imgLupa.gif" width="20"/> 
					</td>
				</tr>
			</table>
	    </td>
	</tr>
    <tr>
        <td>
            <div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 796px; height: 486px" align="left" name="divCatalogo">
				<div style='background-image:url(../../../Images/imgFT18.gif); width:780px'>
				    <table id="tblDatos" class="texto" style="width: 780px;">
                </table>
                </div>
            </div>
            <table id="tblResultado" style="height:17px; width:780px;">
                <tr class="TBLFIN"><td></td></tr>
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
    
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=strErrores %>" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</form>
<script type="text/javascript">
<!--
function aceptar(){
    if (iFila == -1) {
        mmoff("Inf", "Debes seleccionar algún proyecto técnico", 260);
	    return;
	}
    strDatos = tblDatos.rows[iFila].id + "@#@" + tblDatos.rows[iFila].getAttribute("des") + "@#@" + 
               tblDatos.rows[iFila].getAttribute("idPE") + "@#@" + tblDatos.rows[iFila].getAttribute("desPE") + "@#@" + 
               tblDatos.rows[iFila].getAttribute("idT305PE") + "@#@" + 
	           tblDatos.rows[iFila].getAttribute("est") + "@#@" + tblDatos.rows[iFila].getAttribute("une") + "@#@" +
	           tblDatos.rows[iFila].getAttribute("bRecPST"); 
	var returnValue = strDatos;
	modalDialog.Close(window, returnValue);
}
function aceptarClick(indexFila){
    strDatos = tblDatos.rows[iFila].id + "@#@" + tblDatos.rows[iFila].getAttribute("des") + "@#@" + 
               tblDatos.rows[iFila].getAttribute("idPE") + "@#@" + tblDatos.rows[iFila].getAttribute("desPE") + "@#@" + 
               tblDatos.rows[iFila].getAttribute("idT305PE") + "@#@" + 
	           tblDatos.rows[iFila].getAttribute("est") + "@#@" + tblDatos.rows[iFila].getAttribute("une") + "@#@" + 
	           tblDatos.rows[iFila].getAttribute("bRecPST"); 

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
