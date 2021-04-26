<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Acuerdo_AcuerdoNuevo_Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Import Namespace="GASVI.BLL" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head2" runat="server">
    <title> ::: GASVI 2.0 ::: - Detalle del acuerdo</title>
    <meta http-equiv='X-UA-Compatible' content='IE=edge' />
    <link rel="stylesheet" href="../../css/Mantenimiento.css" type="text/css" />
    <link rel="stylesheet" href="../../../../PopCalendar/CSS/Classic.css" type="text/css" />
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/fechas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../PopCalendar/PopCalendar.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
</head>
<body onload="init()" style="margin-top:10px">
    <form id="Form2" name="frmPrincipal" runat="server">
        <div id="procesando" class="procesando">
            <asp:Image ID="imgProcesando" runat="server" Height="33px" Width="152px" ImageUrl="~/images/imgProcesando.gif" />
            <div id="reloj" class="reloj">
                <asp:Image ID="Image3" runat="server" Height="32px" Width="32px" ImageUrl="~/images/imgRelojAnim.gif" />
            </div>
        </div>
        <script type="text/javascript" language="JavaScript">
        <!--
            var strServer = "<%=Session["GVT_strServer"].ToString() %>";
            var intSession = <%=Session.Timeout%>;
        -->
        </script> 
        <table style="width:370px; margin-top:10px; margin-left:10px;"  cellpadding="5">
            <colgroup>
                <col style="width:80px" />
                <col style="width:290px" />
            </colgroup>
            <tr>
                <td>Denominación</td>
                <td><asp:TextBox ID="txtDenominacion" runat="server" MaxLength="50" Width="260px" onKeyUp="aG();" /></td>
            </tr>
            <tr>
                <td><label id="lblResponsable" class="enlace" onclick="getResponsable()" onmouseover="mostrarCursor(this)" style="width:80px;">Responsable</label></td>
                <td><asp:TextBox ID="txtResponsable" runat="server" MaxLength="25" Width="260px" ReadOnly="true" onchange="aG();" /></td>
            </tr>
            <tr>
                <td></td>
                <td>
                <fieldset style="width:245px; height:51px;">
                    <legend>Vigencia</legend>
                    <table style="width:235px; text-align:center; margin-top:5px; margin-left:15px;">
                        <tr>
                            <td>
                                Inicio
                                <asp:TextBox ID="txtInicio" runat="server" style="width:60px; cursor:pointer; margin-right:23px;" Calendar="oCal" onclick="mc(this);" onchange="aG(); comprobarFechas(this);" ReadOnly="true" goma="0"></asp:TextBox>
                                Fin
                                <asp:TextBox ID="txtFin" runat="server" style="width:60px; cursor:pointer; margin-right:23px;" Calendar="oCal" onclick="mc(this);" onchange="aG(); comprobarFechas(this);" ReadOnly="true" goma="0"></asp:TextBox>
                           </td>
                        </tr>
                    </table>
                </fieldset>
                </td>
            </tr>
            <tr>
                <td>Moneda</td>
                <td><asp:DropDownList ID="cboMoneda" onchange="aG();" runat="server" Width="260px" AppendDataBoundItems="true"></asp:DropDownList></td>
            </tr>
            <tr>
                <td style="vertical-align: text-top;">Descripción</td>
                <td><asp:TextBox ID="txtDescripcion" Rows="14" Wrap="true" runat="server" TextMode="MultiLine" SkinID="Multi" Width="260px" Height="170px" onKeyUp="aG();" /></td>
            </tr>
        </tbody>
        </table>
        <center>
            <table style="width:210px; margin-top:30px">
                <tr>
                    <td style="text-align:center">
                        <button id="btnAceptar" type="button" onclick="aceptar();" class="btnH25W85" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../../images/botones/imgAceptar.gif" /><span title="Aceptar">Aceptar</span></button>
                    </td>
                    <td style="text-align:center">
                        <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../../images/botones/imgCancelar.gif" /><span title="Cancelar">Cancelar</span></button>	
                    </td>
                </tr>
            </table>
        </center>
        <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
        <asp:TextBox ID="hdnIdAcuerdo" style="width:5px; visibility:hidden;" Text="" ReadOnly="true" runat="server" />    
        <asp:TextBox ID="hdnIdResp" style="width:5px; visibility:hidden;" Text="" ReadOnly="true" runat="server" />    
        <asp:TextBox ID="hdnIdRespOld" style="width:5px; visibility:hidden;" Text="" ReadOnly="true" runat="server" />    
        <asp:TextBox ID="hdnIdMod" style="width:5px; visibility:hidden;" Text="" ReadOnly="true" runat="server" />    
        <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    </form>
</body>
</html>
