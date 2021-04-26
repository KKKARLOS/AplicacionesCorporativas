<%@ Page Language="c#" CodeFile="Importar.aspx.cs" Inherits="SUPER.Capa_Presentacion.Consultas.Seguimiento.obtenerProyectos" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
	<title> ::: SUPER ::: - Escriba o pegue la lista deseada</title>
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
		<script language="JavaScript" src="../../../Javascript/funciones.js" type="text/Javascript"></script>
		<script language="JavaScript" src="../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
     	<script language="JavaScript" src="../../../Javascript/modal.js" type="text/Javascript"></script>
		<script type="text/javascript">
		    function init(){
		        
		        if (gsNodo=="N"){
		            $I("lblNodo").style.visibility="hidden";
		        }
				if (strErrores != ""){
					mostrarError(strErrores);
				}
				$I("txtComentario").focus();
				ocultarProcesando();
			}

            var indexFila = -1;

            function aceptar(){
	            var returnValue = $I("txtComentario").value;
	            modalDialog.Close(window, returnValue);
            }
        	
            function cerrarVentana(){
	            var returnValue = null;
	            modalDialog.Close(window, returnValue);
            }
    </script>
</head>
<body style="OVERFLOW: hidden" leftMargin="15" topMargin="15" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<form id="Form1" method="post" runat="server">
	<script type="text/javascript">
        var strErrores = "<%=strErrores%>";
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
	    var strServer = "<%=Session["strServer"]%>";
	    var gsNodo = "<%=gsNodo%>"; //Indica si hay que mostrar el label contrato ; nodo
	</script>
	<br />
        <table width="100%">
            <tr>
                <td><img height="5" src="../../../images/imgSeparador.gif" width="1"></td>
            </tr>
            <tr> 
            <td>
              <div align="center">Los elementos de la lista, deberán estar separados por un salto de línea y sin formatear. No puede haber elementos repetidos.<br /><br />
              La longitud máxima de la lista no debe sobrepasar los 7000 caracteres.<br />
              <br />
              <b>Formatos admitidos:</b>
              <br />
              Código
              <br />
              <label id="lblNodo">Código ; Nodo destino</label>
              <br /><br />
                <textarea name="txtComentario" id="txtComentario" cols="30" rows="13" class="txtMultiM"></textarea>
              </div>
            </td>
          </tr>
          <tr>
            <td><img src="../../../images/imgSeparador.gif" width="1" height="10"></td>
          </tr>
        </table>
        <center>
	        <table style="margin-top:15px; width:240px;">
		        <tr>
			        <td>
				        <button id="btnAceptar" type="button" onclick="aceptar();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../images/imgAceptar.gif" /><span title="Aceptar">Aceptar</span></button>								
			        </td>
			        <td>
				        <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" style="margin-left:30px;" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
				            <img src="../../../images/imgCancelar.gif" /><span title="Cancelar">Cancelar</span>
				        </button>				
			        </td>
		        </tr>
	        </table>
	    </center>
	    <uc_mmoff:mmoff ID="mmoff2" runat="server" />
</form>
</body>
</html>
