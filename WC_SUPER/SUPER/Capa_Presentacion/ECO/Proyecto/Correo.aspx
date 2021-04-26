<%@ Page Language="c#" CodeFile="Correo.aspx.cs" Inherits="SUPER.Capa_Presentacion.Consultas.Seguimiento.obtenerProyectos" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<base target="_self" />
	<title> ::: SUPER ::: - Envío de correo al CAU-DEF</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script language="JavaScript" src="../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
		function init(){
			if (strErrores != ""){
				mostrarError(strErrores);
            }
            ocultarProcesando();
		}
        function aceptar() {
            if (fTrim($I("txtComentario").value) == "") {
                mmoff("War", "Para enviar la petición, es necesario indicar la misma.", 400);
                return;
            }
            var returnValue = $I("txtComentario").value;
            modalDialog.Close(window, returnValue);	
        }
        function cerrarVentana(){
            var returnValue = null;
            modalDialog.Close(window, returnValue);	
        }
    </script>
</head>
<body style="overflow:hidden; margin-top:20px;" onload="init()" class="texto">
<ucproc:Procesando ID="Procesando" runat="server" />
<form id="Form1" method="post" runat="server">
<script type="text/javascript">
    var strErrores = "<%=strErrores%>";
    var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
    var strServer = "<%=Session["strServer"]%>";
</script>
<center>
<textarea id="txtComentario" class="txtMultiM" cols="60" rows="20" style="width:495px; height:300px;" ></textarea>
<table class="texto" style="width:100px; margin-top:20px;">
    <tr> 
        <td> 
            <button id="btnGrabar" type="button" onclick="aceptar();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
                 onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../images/botones/imgTramitar.gif" /><span>Enviar</span>
            </button>
          </td>
      </tr>
</table>
</center>
</form>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</body>
</html>
