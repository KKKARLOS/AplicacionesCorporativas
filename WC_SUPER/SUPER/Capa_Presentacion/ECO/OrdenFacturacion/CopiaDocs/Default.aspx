<%@ Page Language="c#" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title> ::: SUPER ::: - Seleccione el modo de copia de los documentos asociados</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
	<!--
		function init(){
			if (!mostrarErrores()) return;
			ocultarProcesando();
			window.focus();
		}
        function aceptar(){
            var returnValue = getRadioButtonSelectedValue("rdbEstado", true);
            modalDialog.Close(window, returnValue);	
        }
        function cerrarVentana(){
            var returnValue = null;
            modalDialog.Close(window, returnValue);	
        }
    -->
    </script>
</head>
<body style="overflow:hidden; margin-top:15px;" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
<form id="Form1" method="post" runat="server">
<script type="text/javascript">
    var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
    var strServer = "<%=Session["strServer"]%>";
</script>
<center>
    <table border="0" class="texto" style="width:500px; text-align:left;" cellpadding="5" cellspacing="0">
        <colgroup><col style="width:500px;"/></colgroup>
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" class="texto" width="495px">
                    <tr>
                        <td background="../../../../Images/Tabla/7.gif" height="6" width="6"></td>
                        <td background="../../../../Images/Tabla/8.gif" height="6"></td>
                        <td background="../../../../Images/Tabla/9.gif" height="6" width="6"></td>
                    </tr>
                    <tr>
                        <td background="../../../../Images/Tabla/4.gif" width="6">&nbsp;</td>
                        <td background="../../../../Images/Tabla/5.gif" style="padding: 5px 15px 15px 15px; color:Red;">
                            <!-- Inicio del contenido propio de la página -->
                                <br />
                                El proceso ha detectado que existen documentos asociados a los elementos a copiar.
                                <br />
                                Puede seleccionar entre las siguientes opciones respecto a qué acción tomar sobre esos documentos
                                <br /><br />
                                 1-> No copiar<br />
                                 2-> Generar una copia del documento<br />
                                 <!--<label id="lblCompartir" runat="server">3-> Compartir el mismo documento entre el elemento origen y su copia</label>-->
                            <!-- Fin del contenido propio de la página -->
                        </td>
                        <td background="../../../../Images/Tabla/6.gif" width="6">&nbsp;</td>
                    </tr>
                    <tr>
                        <td background="../../../../Images/Tabla/1.gif" height="6" width="6"></td>
                        <td background="../../../../Images/Tabla/2.gif" height="6"></td>
                        <td background="../../../../Images/Tabla/3.gif" height="6" width="6"></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table style="width:250px; text-align:left; margin-top:15px;">
        <colgroup><col style="width:100%" /></colgroup>
        <tr> 
            <td>
              <div> 
              <fieldset style="width:220px;">
                <legend id="lyd1" runat="server">&nbsp;Acción sobre documentos asociados&nbsp;</legend>
                <asp:RadioButtonList ID="rdbEstado" SkinID="rbl" runat="server" style="width:200px;height:60px; margin-left:50px;">
                </asp:RadioButtonList>
              </fieldset>
              </div>
            </td>
        </tr>
    </table>
    <table style="margin-top:40px; margin-left:20px; width:250px;" border="0">
        <colgroup><col style="width:125px;" /><col style="width:125px;" /></colgroup>
        <tr>
            <td>
                <button id="btnAceptar" type="button" onclick="aceptar();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../../images/imgAceptar.gif" /><span title="Aceptar">Aceptar</span>
                </button>								
            </td>
            <td>
                <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../../images/imgCancelar.gif" /><span title="Cancelar">Cancelar</span>
                </button>				
            </td>
        </tr>
    </table>
</center>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=strErrores %>" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</form>
</body>
</html>
