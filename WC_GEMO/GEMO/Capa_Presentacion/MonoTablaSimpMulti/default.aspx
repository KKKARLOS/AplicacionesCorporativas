<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="Default" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Import Namespace="GEMO.BLL" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 	"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Selección de <%=sOpcion%></title>
	<script language="JavaScript" src="../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
	<!--
	-->
    </script>
</head>
<body style="overflow: hidden" leftMargin="10" onload="init()">
    <ucproc:Procesando ID="Procesando" runat="server" />
<form id="form1" runat="server">
	<script type="text/javascript">
	<!--
        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";
	    var sTipo = "<%=Request.QueryString["nT"]%>";
	    var sTS = "<%=Request.QueryString["sTS"]%>";
	-->
	</script>
<center>
    <table style="margin-left:10px;width:370px;margin-top:12px;text-align:left" cellpadding="5">
        <tr>
            <td>
                <table id="tblTitulo" style="width: 350px; height: 17px">
                    <TR class="TBLINI">
                        <td style="padding-left:3px;">Denominación
                             <img alt="" class="ICO" id="imgLupa" style="display:none; cursor:hand" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa')"
		                        height="11px" src="../../Images/imgLupaMas.gif" width="20px" tipolupa="2" /> 
		                    <img alt="" class="ICO" style="cursor:hand; display:none;" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa',event)"
		                        height="11px" src="../../Images/imgLupa.gif" width="20px" tipolupa="1" />
                        </TD>
                    </TR>
                </TABLE>
                <DIV id="divCatalogo" style="overflow-x: hidden; overflow-y: auto; width: 366px; height:380px">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:350px">
                    <%=strTablaHTML %>
                    </div>
                </DIV>
                <table style="width: 350px; height: 17px">
                    <TR class="TBLFIN">
                        <TD></TD>
                    </TR>
                </TABLE>
            </td>
        </tr>
    </table>
    <span  runat="server" id="seleccion_mul">
        <table width="300px" align=center style="margin-top:5px;">
		    <tr>
                <td align="center">
                    <button id="btnAceptar" type="button" onclick="aceptar()" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
                        <img src="../../images/imgAceptar.gif" /><span title="Aceptar">&nbsp;&nbsp;Aceptar</span>
                    </button>    
                </td>	

			    <td align=center>
			        <button id="btnCancelar" type="button" onclick="cerrarVentana()" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
                        <img src="../../images/imgCancelar.gif" /><span title="Cancelar">&nbsp;Cancelar</span>
                    </button>    
			    </td>
		    </tr>
        </table>
    </span>	
    <span runat="server" id="seleccion_sim">
        <table width="300px" align="center" style="margin-top:5px;">
		    <tr>
			    <td align=center>
			        <button id="btnCancelar2" type="button" onclick="cerrarVentana()" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
                        <img src="../../images/imgCancelar.gif" /><span title="Cancelar">&nbsp;Cancelar</span>
                    </button>    
			    </td>
		    </tr>
        </table>    
    </span>	
    </center>
    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
    </form>
</body>
</html>
