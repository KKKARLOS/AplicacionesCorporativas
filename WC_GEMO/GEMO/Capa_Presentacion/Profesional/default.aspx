<%@ Page Language="c#" CodeFile="default.aspx.cs" Inherits="Default" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 	"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Selección de profesional</title>
	<script language="JavaScript" src="../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
  	<script language="JavaScript" src="../../Javascript/boxover.js" type="text/Javascript"></script>
  	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
   	<script language="JavaScript" src="../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
	<!--
	-->
    </script>
</head>
<body style="overflow: hidden"  onload="init()">
    <ucproc:Procesando ID="Procesando" runat="server" />
	<form id="Form1" method="post" runat="server">
	<script type="text/javascript">
	<!--
	    var intSession  = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
	    var strServer   = "<%=Session["strServer"]%>";
	    var sTipo = "<%=Request.QueryString["nT"]%>";
	-->
    </script>
    <center>
		<table align="center" style="margin-top:10px;width:416px">
			    <tr>
			        <td>
                        <table id="tblApellidos" style="width: 400px;margin-bottom:5px;">
                        <colgroup>
                            <col style="width:135px"/>
                            <col style="width:135px"/>
                            <col style="width:130px"/>
                        </colgroup>
                            <tr>
                            <td>&nbsp;Apellido1</td>
                            <td>&nbsp;Apellido2</td>
                            <td>&nbsp;Nombre</td>
                            </tr>
                            <tr>
                            <td><asp:TextBox ID="txtApellido1" runat="server" style="width:110px"  onkeypress="javascript:if(event.keyCode==13){mostrarProfesionales();event.keyCode=0;}" MaxLength="50" /></td>
                            <td><asp:TextBox ID="txtApellido2" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesionales();event.keyCode=0;}" MaxLength="50" /></td>
                            <td><asp:TextBox ID="txtNombre" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesionales();event.keyCode=0;}" MaxLength="50" /></td>
                            </tr>
                        </table>
			        </td>
			    </tr>
				<tr>
					<td>
						<table id="tblTitulo" height="17" width="400" align="left">
							<tr class="TBLINI">
								<td>&nbsp;Profesional&nbsp;
									<img id="imgLupa1" style="DISPLAY: none; CURSOR: hand" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa1')"
										height="11" src="../../Images/imgLupaMas.gif" width="20" tipolupa="2">
								    <img style="DISPLAY: none; CURSOR: hand" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa1',event)"
										height="11" src="../../Images/imgLupa.gif" width="20" tipolupa="1"> 
								</td>
							</tr>
						</table>
			        </td>
			    </tr>
				<tr>
					<td>						
						<div id="divCatalogo" style="overflow-x: hidden; overflow: auto; width: 416px; height: 360px" align="left">
                            <div style='background-image:url(../../Images/imgFT20.gif); width:400px'>
                            <table id='tblDatos' style='width: 400px;'>
                            <colgroup><col style='padding-left:5px;' /></colgroup>
							</table>
							</div>
		                </DIV>
			        </td>
			    </tr>
				<tr>
					<td>		                
                        <table style="width: 400px; height: 17px">
                            <TR class="TBLFIN">
                                <TD></TD>
                            </TR>
                        </TABLE>
		</td></tr></table>
        <table width="300px" style="margin-top:12px;">
            <tr>
                <td align=center>
				    <button id="btnCancelar" type="button" onclick="cerrarVentana()" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
					    <img src="../../images/imgCancelar.gif" /><span title="Cancelar">&nbsp;Cancelar</span>
				    </button>   
                </td>
            </tr>
        </table>
    </center>		
    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
	</form>
</body>
</html>
