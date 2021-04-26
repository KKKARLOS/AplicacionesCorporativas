<%@ Page Language="c#" Inherits="GESTAR.Capa_Presentacion.ASPX.cogerString" CodeFile="cogerString.aspx.cs" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
	<head runat="server">
		<title>
			<%=strTitulo%></title>
		<meta http-equiv='X-UA-Compatible' content='IE=8' />
		<script language="JavaScript" src="../../JavaScript/funciones.js" type="text/Javascript"></script>
        <script language="JavaScript" src="../../JavaScript/modal.js" type="text/Javascript"></script>
			<script type="text/javascript">
            <!--
            -->
			</script>
<%--			<script for="window" event="onbeforeunload">
            if ($I("txtString").value=="") event.returnValue = "";
			</script>--%>
	</head>
        <body style="margin-left:0px; margin-top:15px;" onload="init()">	
        <form id="frmDatos" runat="server">
		<script type="text/javascript">
		    var strServer = "<%=Session["strServer"].ToString()%>";
	        var intSession = <%=Session.Timeout%>; 
    	</script>					
		<br>
		<center>
		    <table  class="texto" width="98%" cellpadding="5">			    
			    <tr>
				    <td><asp:textbox id="txtString" runat="server" SkinID="Multi" width="500px" TextMode="MultiLine" Rows="12">
				    </asp:textbox>		
				    </td>
			    </tr>
		    </table>
		    <br/>
		    <button id="btnAceptar" type="button" onclick="aceptar();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
			    <img src="../../images/botones/imgAceptar.gif" /><span title="Salir aceptando los datos introducidos">Aceptar</span>
		    </button>		
        </center>
<script type="text/javascript">
<!--
    function init() {
        if (!mostrarErrores()) return;    
        $I("txtString").focus();
    }
	function aceptar(){
		try{
		    if ($I("txtString").value == ""){
		        mmoff("War", "Introduzca el término de búsqueda.", 240);
		        $I("txtString").focus();
			    return;
		    }
		    var returnValue = $I("txtString").value;
		    modalDialog.Close(window, returnValue);		 		    
        }catch(e){
            mostrarErrorAplicacion("Error en la función aceptar", e.message);	
        }		        
		
	}
-->
</script>
    <asp:textbox id="hdnErrores" runat="server" style="visibility:hidden" ></asp:textbox>
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
	</form>
	</body>
</html>
