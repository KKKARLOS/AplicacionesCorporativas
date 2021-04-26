<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getObra.aspx.cs" Inherits="getObra" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/Dtd/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title> ::: SUPER ::: - Página en construcción</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <script language="JavaScript" src="../../Javascript/funciones.js" type="text/Javascript"></script>
	<script type="text/javascript">
	<!--
    function salir(){
	    try{
	        var returnValue = null;
	        modalDialog.Close(window, returnValue);
        }catch(e){
            mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
        }
    }
	-->
    </script>
</head>
<body style="OVERFLOW: hidden" class=texto leftMargin="10">
    <form id="form1" runat="server">
	<script type="text/javascript">
	<!--
        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";
	-->
	</script>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <div align="center">
      <center>
      <table cellpadding="5" width="50%" style="border: 1 solid #83afc3">
        <tr>
          <td width="35%"><img border="0" src="<% =Session["strServer"].ToString() %>images/imgObras.gif" width="189px" height="185px"></td>
          <td width="65%" align="center">
            <b><font face="Verdana" color="#cc6699">Página en construcción</font></b>
          </td>
        </tr>
      </table>
      </center>
    </div>
    <table style="width:100px; margin-top:210px;" border="0" class="texto" align="center">
        <colgroup>
            <col style="width:100px" />
        </colgroup>
	      <tr> 
		    <td>
			<button id="btnCancelar" type="button" onclick="salir();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="<% =Session["strServer"].ToString() %>images/botones/imgSalir.gif" /><span title="Salir">&nbsp;&nbsp;Salir</span>
			</button>	  
		    </td>
	      </tr>
    </table>
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    </form>
</body>
</html>
