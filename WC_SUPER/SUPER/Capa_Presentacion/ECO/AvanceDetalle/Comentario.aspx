<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Comentario.aspx.cs" Inherits="Capa_Presentacion_ECO_AvanceDetalle_Comentario" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Indicaciones y observaciones</title>
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <script src="../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../Javascript/funciones.js" type="text/Javascript"></script>
 	<script src="../../../Javascript/modal.js" type="text/Javascript"></script>
<script type="text/javascript">
    var bSalir=false;
    
	function init(){
        try{
            if (!mostrarErrores()) return;
            setOp($I("btnGrabar"),30);
            setOp($I("btnGrabarSalir"),30);
		    $I("txtIndicaciones").focus();
    	    ocultarProcesando();
	    }catch(e){
		    mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
        }
	}
	function aceptar(){
	    var returnValue = "OK";
	    modalDialog.Close(window, returnValue);		
	}
	
    function aG(){
        try{
            if (!bCambios){//para que no esté constantemente asignando la op. de 100
                setOp($I("btnGrabar"), 100);
                setOp($I("btnGrabarSalir"), 100);
                bCambios = true;
            }
	    }catch(e){
		    mostrarErrorAplicacion("Error al activar la botón de grabar", e.message);
	    }
    }

    function grabar(){
        try{
            var js_args = "grabar@#@";
            js_args += $I("hdnTarea").value +"@#@";
            js_args += $I("hdnRecurso").value +"@#@";
            js_args += Utilidades.escape($I("txtIndicaciones").value);

            mostrarProcesando();
            RealizarCallBack(js_args, "");  
	    }catch(e){
		    mostrarErrorAplicacion("Error al ir a grabar los datos", e.message);
	    }
    }
       
    function salir() {
        var returnValue = "OK";

        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war",300).then(function (answer) {
                if (answer) {
                    bSalir = true;
                    grabar();
                }
                else{
                    bCambios = false;
                    modalDialog.Close(window, returnValue);
                }
            });
        }
        else modalDialog.Close(window, returnValue);
    }

    function grabarSalir(){
        bSalir = true;
        grabar();
    }
    

    function desActivarGrabar(){
        try{
            setOp($I("btnGrabar"),30);
            setOp($I("btnGrabarSalir"),30);
            bCambios = false;
	    }catch(e){
		    mostrarErrorAplicacion("Error al desactivar el botón de grabar", e.message);
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
                case "grabar":
                    desActivarGrabar();
                  
                    if (bSalir) setTimeout("salir();", 50);
                    else mmoff("Suc", "Grabación correcta", 160);
                                      
                    break;
                default:
                    ocultarProcesando();
                    mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                    break;
                    
            }
            ocultarProcesando();
        }
    }
</script>    
</head>
<body onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
<form id="Form1" method="post" runat="server">
<script type="text/javascript">
    var strErrores = "<%=sErrores%>";
    var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
    var strServer = "<%=Session["strServer"]%>";
</script>
<table style="margin-left:20px; width:470px;" class="texto">
    <tr>
        <td colspan="2">
            <img height="5" src="../../../images/imgSeparador.gif" width="1"></td>
    </tr>
    <tr> 
    <td colspan="2">Indicaciones al profesional:
      <br /> 
        <asp:TextBox ID="txtIndicaciones" SkinID="Multi" runat="server" TextMode="MultiLine" Rows="11" Width="460px" onKeyUp="aG();"></asp:TextBox>
    </td>
  </tr>
  <tr>
    <td colspan="2"><img src="../../../images/imgSeparador.gif" width="1" height="10"></td>
  </tr>
    <tr> 
    <td colspan="2">Observaciones del profesional:
      <br /> 
        <asp:TextBox ID="txtObservaciones" SkinID="Multi" runat="server" TextMode="MultiLine" Rows="11" Width="460px" readonly="true" ></asp:TextBox>
    </td>
  </tr>
  <tr>
    <td colspan="2"><img src="../../../images/imgSeparador.gif" width="1" height="10"></td>
  </tr>
</table>
<center>
    <table style="width:360px; margin-left:10px;" class="texto">
        <colgroup>
            <col style="width:120px" />
            <col style="width:120px" />
            <col style="width:120px" />
        </colgroup>
          <tr> 
            <td>
			    <button id="btnGrabar" type="button" onclick="grabar()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../images/botones/imgGrabar.gif" /><span title="Grabar">&nbsp;&nbsp;Grabar</span>
			    </button>	
            </td>
            <td>
			    <button id="btnGrabarSalir" type="button" onclick="grabarSalir()" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../images/botones/imgGrabarSalir.gif" /><span title="Grabar y salir">Grabar...</span>
			    </button>	
            </td>
            <td>
			    <button id="btnCancelar" type="button" onclick="salir();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../images/botones/imgSalir.gif" /><span title="Salir">&nbsp;&nbsp;Salir</span>
			    </button>	 
            </td>
          </tr>
    </table>
</center>
<input type="hidden" id="hdnErrores" value="<%=sErrores %>" />
<input type="hidden" id="hdnTarea" value="<%=nTarea %>" />
<input type="hidden" id="hdnRecurso" value="<%=nRecurso %>" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
</form>
</body>
</html>
