<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script language=Javascript>
    var nConsultas = <%=nConsultas.ToString() %>;
</script>
<center>
<table style="width:520px;text-align:left">
<tr>
    <td>
        <table style="width:500px;">
            <colgroup>
                <col style="width:500px;" />
            </colgroup>
            <tr>
                <td style="padding-bottom:10px; padding-left:3px;">
                    <div align="left" style="width: 400px;">
                        <div align="center" style="background-image: url('../../../Images/imgFondo185.gif');background-repeat:no-repeat;
                            width: 185px; height: 23px; position: relative; top: 12px; left: 20px; padding-top: 5px;">
                            &nbsp;Consultas personalizadas</div>
                        <table border="0" cellspacing="0" cellpadding="0">
                          <tr>
                            <td width="6" height="6" background="../../../Images/Tabla/7.gif"></td>
                            <td height="6" background="../../../Images/Tabla/8.gif"></td>
                            <td width="6" height="6" background="../../../Images/Tabla/9.gif"></td>
                          </tr>
                          <tr>
                            <td width="6" background="../../../Images/Tabla/4.gif">&nbsp;</td>
                            <td background="../../../Images/Tabla/5.gif" style="padding:3px; padding-top:10px;">
	                        <!-- Inicio del contenido propio de la página -->
                        	
                            <table id="tblEstadisticas" class="texto" style="WIDTH: 370px; BORDER-COLLAPSE: collapse; table-layout:fixed;" cellspacing="0" cellpadding="3" border="0">
                                <colgroup>
                                    <col style="width:65px;" />
                                    <col style="width:25px;" />
                                    <col style="width:55px;" />
                                    <col style="width:50px;" />
                                    <col style="width:25px;" />
                                    <col style="width:67px;" />
                                    <col style="width:55px;" />
                                    <col style="width:25px;" />
                                </colgroup>
                                <tr>
                                    <td>Disponibles:</td>
                                    <td id="cldTotal" style='text-align:right;'><%=nConsultas.ToString() %></td>
                                    <td></td>
                                    <td>Activas:</td>
                                    <td id="cldActivas" style='text-align:right;'>0</td>
                                    <td></td>
                                    <td>Inactivas:</td>
                                    <td id="cldInactivas" style='text-align:right;'>0</td>
                                </tr>
                                </table>

	                        <!-- Fin del contenido propio de la página -->
	                        </td>
                            <td width="6" background="../../../Images/Tabla/6.gif">&nbsp;</td>
                          </tr>
                          <tr>
                            <td width="6" height="6" background="../../../Images/Tabla/1.gif"></td>
                            <td height="6" background="../../../Images/Tabla/2.gif"></td>
                            <td width="6" height="6" background="../../../Images/Tabla/3.gif"></td>
                          </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr id="tblTitulo" class="TBLINI">
                <td style="padding-left:23px;">Denominación</td>
            </tr>
        </table>
        <div id="divCatalogo" style="overflow:auto; overflow-x:hidden;  width: 516px; height:440px;">
            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:500px">
            <%=strTablaHTML%>
            </div>
        </div>
        <table id="tblTotales" style="width: 500px;">
            <tr class="TBLFIN">
                <td>&nbsp;</td>
            </tr>
        </table>
    </td>
</tr>
</table>
</center> 
<center> 
<table width="520px" align="center" style="margin-top:5px;" border=0 cellpadding=0 cellspacing=0>
		<tr>
			<td align="center">
				<button id="btnExcel" type="button" onclick="ActivarDesactivar()" class="btnH25W135" runat="server" hidefocus="hidefocus" 
					 onmouseover="se(this, 25);mostrarCursor(this);">
					<img src="../../../images/imgActivarDesactivar.gif" /><span title="">Activar/Desactivar</span>
				</button>				 
			</td>
			<td align="center">
				<button id="btnCancelar" type="button" onclick="getTodasConsultas()" class="btnH25W135" runat="server" hidefocus="hidefocus" 
					 onmouseover="se(this, 25);mostrarCursor(this);">
					<img src="../../../images/imgMostrarInactivas.gif" /><span title="">Mostrar inactivas</span>
				</button>				 
			</td>
			<td align="center">
				<button id="Button1" type="button" onclick="mostrarGuia('MisConsultas.pdf')" class="btnH25W135" runat="server" hidefocus="hidefocus" 
					 onmouseover="se(this, 25);mostrarCursor(this);">
					<img src="../../../images/botones/imgGuia.gif" /><span title="">&nbsp;Guía</span>
				</button>					
			</td>
			<td style="width:20px;"></td>
		</tr>
    </table>
</center> 	
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
</asp:Content>

