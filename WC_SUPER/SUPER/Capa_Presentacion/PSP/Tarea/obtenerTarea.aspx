<%@ Page Language="c#" CodeFile="obtenerTarea.aspx.cs" Inherits="obtenerTarea" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self" />
	<title> ::: SUPER ::: - Selección de tarea</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/> 
	<script language="JavaScript" src="../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../Javascript/boxover.js" type="text/Javascript"></script>
   	<script language="JavaScript" src="../../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
	<!--
		function init(){
			if (strErrores != ""){
				mostrarError(strErrores);
			}
			buscarTareas();
			ocultarProcesando();
		}
		
        function buscarTareas(){
            try{
                var js_args = "T@#@";
                //var sAccion=getRadioButtonSelectedValue("rdbTipo",true);
                var sAccion="I";
                js_args += $I("txtNumPT").value + "@#@";
                js_args += $I("txtNumFase").value + "@#@";
                js_args += $I("txtNumActividad").value + "@#@";
                js_args += sAccion + "@#@";
                js_args += Utilidades.escape($I("hdnDesTarea").value)+ "@#@";
                //js_args += $I("hdnCRActual").value + "@#@";
                //js_args += $I("txtNumPE").value ;
                js_args += $I("hdnT305IdProy").value ;
                //alert(js_args);
                mostrarProcesando();
                RealizarCallBack(js_args, "");
                return false;
	        }catch(e){
		        mostrarErrorAplicacion("Error al buscar las tareas", e.message);
            }
		}

        function estructura(oFila){
            try {
                $I("txtNumPE").value = oFila.getAttribute("nPE").ToString("N", 9, 0);
                $I("txtPE").value = oFila.getAttribute("sPE");
                $I("hdnT305IdProy").value = oFila.getAttribute("sT305IdPr");
                $I("txtNumPT").value = oFila.getAttribute("nPT");
                $I("txtPT").value = oFila.getAttribute("sPT");
                $I("txtNumFase").value = oFila.getAttribute("nF");
                $I("txtFase").value = oFila.getAttribute("sF");
                $I("txtNumActividad").value = oFila.getAttribute("nA");
                $I("txtActividad").value = oFila.getAttribute("sA");    
                
	        }catch(e){
		        mostrarErrorAplicacion("Error al actualizar la estructura", e.message);
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
                    case "T":
		                $I("divCatalogo").children[0].innerHTML = aResul[2];
                        //$I("txtTarea").value = "";
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
<table style="width:500px;margin-top:20px;text-align:left;">		
<tr>
    <td>
	    <fieldset style="width:445px; margin-left:22px;"><legend>Estructura</legend>
	    <table style="width:435px" align="center">
             <tr>
                <td>
                    <label style="width:100px; margin-left:4px;">Proyecto económico</label>
                    <asp:TextBox ID="txtNumPE" runat="server" SkinID="Numero" style="width:50px" readonly="true" />
                    <asp:TextBox ID="txtPE" runat="server" style="width:254px" readonly="true" />
                </td>
            </tr>
             <tr>
                <td>
                    <label style="width:100px; margin-left:4px;">Proyecto técnico</label>
                    <asp:TextBox ID="txtPT" runat="server" style="width:310px" readonly="true" />
                </td>
            </tr>
             <tr>
                <td>
                    <label style="width:100px; margin-left:4px;">Fase</label>
                    <asp:TextBox ID="txtFase" runat="server" style="width:310px" readonly="true" />
                </td>
            </tr>
             <tr>
                <td>
                    <label style="width:100px; margin-left:4px;">Actividad</label>
                    <asp:TextBox ID="txtActividad" runat="server" style="width:310px" readonly="true" />
                </td>
            </tr>
	    </table>
	    </fieldset>
        <table style="margin-top:10px;margin-left:22px;width:440px">
        <tr>
            <td>			    
                <table id="tblTitulo" style="width: 440px; height: 17px">
	                <tr class="TBLINI">
		                <td>&nbsp;Denominación&nbsp;
			                <img id="imgLupa1" style="display: none; cursor: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa1')" height="11" src="../../../Images/imgLupaMas.gif" width="20">
			                <img style="CURSOR: pointer" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa1',event)" height="11" src="../../../Images/imgLupa.gif" width="20"> 
		                </td>
	                </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
	            <div id="divCatalogo" style="overflow: auto; width: 456px; height:340px">
		            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT18.gif'); width:440px;">
			            <table id="tblDatos" style="width: 440px;">
			            </table>
		            </div>
	            </div>					
	            <table style="width: 440px; height: 17px">
		            <tr class="TBLFIN">
			            <td></td>
		            </tr>
	            </table>
            </td>
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
<input type="hidden" name="hdnT305IdProy" runat="server" id="hdnT305IdProy" value=""/>
<asp:TextBox ID="hdnDesTarea" name="hdnDesTarea" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
<asp:TextBox ID="txtNumPT" runat="server" SkinID="Numero" style="width:5px;visibility:hidden;"/>
<asp:TextBox ID="txtNumFase" runat="server" SkinID="Numero" style="width:5px;visibility:hidden;"/>
<asp:TextBox ID="txtNumActividad" runat="server" SkinID="Numero" style="width:5px;visibility:hidden;"/>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</form>
<script type="text/javascript">
<!--

	function aceptarClick(indexFila){
	    var tblDatos = $I("tblDatos");
	    strDatos = tblDatos.rows[indexFila].id + "@#@" + tblDatos.rows[indexFila].innerText + "@#@" + tblDatos.rows[indexFila].getAttribute("nPE") + "@#@" + 
	               tblDatos.rows[indexFila].getAttribute("sPE") + "@#@" + tblDatos.rows[indexFila].getAttribute("nPT") + "@#@" + tblDatos.rows[indexFila].getAttribute("sPT") + "@#@" + 
	               tblDatos.rows[indexFila].getAttribute("nF") + "@#@" + tblDatos.rows[indexFila].getAttribute("sF") + "@#@" + tblDatos.rows[indexFila].getAttribute("nA") + "@#@" +
	               tblDatos.rows[indexFila].getAttribute("sA") + "@#@" + tblDatos.rows[iFila].getAttribute("sT305IdPr") + "@#@" + tblDatos.rows[iFila].getAttribute("sEst");

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
