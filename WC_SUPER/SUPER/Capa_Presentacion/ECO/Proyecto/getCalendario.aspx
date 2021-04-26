<%@ Page Language="c#" CodeFile="getCalendario.aspx.cs" Inherits="SUPER.Capa_Presentacion.ECO.Proyecto.getCalendario" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title> ::: SUPER ::: - Selección de calendario</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script language="JavaScript" src="../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
	<!--
		function init(){
            try{
            if (!mostrarErrores()) return;
                //$I("rdbTipoBusqueda_0").click();
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
                var tblDatos = $I("tblDatos");
        	    
                strOpciones = $I("tblDatos").rows[indexFila].id + "@#@" + $I("tblDatos").rows[indexFila].innerText;

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
                    getCal(sAmb);
                }
            }catch(e){
                mostrarErrorAplicacion("Error al establecer el tipo de calendario", e.message);
            }
        }
        function getCal(sAmb){
            try{
                $I("divCatalogo").children[0].innerHTML = "";
                var js_args = "getCal@#@" + sAmb + "@#@" + idficepi + "@#@" + $I("hdnIdNodo").value;
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
</head>
<body style="OVERFLOW: hidden; margin-left:15px; margin-top:10px;" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
<form id="Form1" method="post" runat="server">
<script type="text/javascript">
<!--
    var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
    var strServer = "<%=Session["strServer"]%>";
    var idficepi = "<%=Request.QueryString["idficepi"]%>";
-->
</script>
<center>
<table style="width:420px;text-align:left">
    <tr>
        <td>
            <fieldset style="width:182px;">
            <legend>Ámbito</legend>
                <asp:RadioButtonList ID="rdbTipoBusqueda" SkinId="rbli" runat="server" Height="20px" RepeatColumns="2" onclick="setTipo();">
                    <asp:ListItem Value="E" Text="Empresarial" Selected="True" />
                    <asp:ListItem Value="D" Text="Departamental" />
                </asp:RadioButtonList>
            </fieldset>
        </td>
    </tr>
	<tr>
		<td>
			<table id="tblTitulo" style="height:17px; width:400px; margin-top:5px;">
				<tr class="TBLINI">
					<td>&nbsp;<%=strColumna %>
						<IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa1')"
							height="11" src="../../../Images/imgLupaMas.gif" width="20"  tipolupa="2">
					    <IMG style="CURSOR: pointer" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa1',event)"
							height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
					</td>
				</tr>
			</table>
			<div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 416px; height: 300px" align="left">
				<div style='background-image:url(../../../Images/imgFT16.gif); width:400px'>
				<%=strTablaHtml %>
				</div>
            </div>
            <table id="tblResultado" style="height:17px; width:400px;">
                <tr class="TBLFIN">
	                <td>&nbsp;</td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<table style="margin-top:15px; width:130px" align="center">
<tr> 
	<td>
		<button id="btnCancelar" type="button" onclick="cerrarVentana()" class="btnH25W100" runat="server" hidefocus="hidefocus" 
			 onmouseover="se(this, 25);mostrarCursor(this);">
			<img src="../../../images/imgCancelar.gif" /><span title="">&nbspCancelar</span>   
		</button>  		
	</td>
  </tr>
</table>
</center>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<input type="hidden" runat="server" name="hdnIdNodo" id="hdnIdNodo" value="" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</form>
</body>
</html>
