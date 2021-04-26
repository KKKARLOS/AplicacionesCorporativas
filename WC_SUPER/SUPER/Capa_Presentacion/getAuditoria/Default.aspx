<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title> ::: SUPER ::: - Auditoría de datos modificados</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <link rel="stylesheet" href="../../PopCalendar/CSS/Classic.css" type="text/css">
	<script language="JavaScript" src="../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../Javascript/boxover.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../Javascript/funcionesPestVertical.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../PopCalendar/PopCalendar.js" type="text/Javascript"></script>
	<script language="JavaScript" src="Functions/funciones.js?20180131" type="text/Javascript"></script>
   	<script language="JavaScript" src="../../Javascript/modal.js" type="text/Javascript"></script>
<style>
#tblDatos TD{border-right: solid 1px #A6C3D2; padding-right:1px;}
</style>
</head>
<body onload="init()" onunload="unload()">
<form name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
<script type="text/javascript">

    var strServer = "<% =Session["strServer"].ToString() %>";
    var intSession = <%=Session.Timeout%>;
    var nPantalla = <%=nPantalla%>;
    var sItem = "<%=sItem%>";

</script>    
<img id="imgPestHorizontalAux" src="../../Images/imgPestHorizontal.gif" style="Z-INDEX: 0;position:absolute; left:40px; top:0px; cursor:pointer;" onclick="mostrarOcultarPestVertical()" />
<div id="divPestRetr" style="position:absolute; left:20px; top:0px; width:755px; height:500px; clip:rect(auto auto 0 px auto)">
    <table class="texto" style="width:750px; height:500px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
        <tr>
            <td background="../../Images/Tabla/4.gif" width="6">&nbsp;</td>
            <td background="../../Images/Tabla/5.gif" style="padding: 5px">
            <table class="texto"style="WIDTH: 720px; table-layout:fixed; margin-top:5px;" cellpadding="2" cellspacing="1" border="0">
                <colgroup>
                <col style="width:105px;" />
                <col style="width:405px;" />
                <col style="width:100px;" />
                <col style="width:115px;" />
                </colgroup>
                <tr>
                    <td style="padding-left:7px; vertical-align:bottom;">
                        <img src='../../images/botones/imgmarcar.gif' onclick="mTabla('')" title='Marca todos los atributos' style='cursor:pointer; width:13px; height:12px; vertical-align:middle;' />
                        &nbsp;<img src='../../images/botones/imgdesmarcar.gif' onclick="dTabla('')" title='Desmarca todos los atributos' style='cursor:pointer; width:13px; height:12px; vertical-align:middle; margin-right: 5px;' />
                    </td>
                    <td align=right>
                     	<FIELDSET id="fldPeriodo" class="fld" style="height: 45px; width:125px;text-align:left;" runat="server">
				            <LEGEND class="Tooltip" title="Periodo temporal">&nbsp;Periodo&nbsp;</LEGEND>
					        &nbsp;&nbsp;Desde&nbsp;&nbsp;
					        <asp:TextBox ID="txtFechaInicio" runat="server" style="width:60px; cursor:pointer" Text="" Calendar="oCal" onchange="VerFecha('D');" goma=0 lectura=0 />
					        </br>
					        &nbsp;&nbsp;Hasta&nbsp;&nbsp;&nbsp;
					        <asp:TextBox ID="txtFechaFin" runat="server" style="width:60px; cursor:pointer" Text="" Calendar="oCal" onchange="VerFecha('H');" goma=0 lectura=0 />
				        </FIELDSET>	
                    </td>
                    <td><img src='../../Images/imgObtenerAuto.gif' border='0' title="Obtiene la información automáticamente al cambiar el valor de algún criterio de selección" style="margin-left:40px; vertical-align:bottom;">
                        <input type=checkbox id="chkActuAuto" class="check" runat="server" />
                    </td>
                    <td>
						<button id="btnObtener" type="button" onclick="buscar();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
							 onmouseover="se(this, 25);mostrarCursor(this);">
							<img src="../../images/botones/imgObtener.gif" /><span title="Obtener">&nbsp;Obtener</span>
						</button>	 
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                    <TABLE style='WIDTH: 700px; BORDER-COLLAPSE: collapse; HEIGHT: 17px; margin-top:5px;table-layout:fixed;' cellspacing='0' cellpadding='0' border='0'>
                        <tr class='TBLINI'>
                        <td style='width:700px'>&nbsp;Tablas / Campos</td>
                        </tr>
                    </TABLE>
		            <div id="divCatalogoTablas" style="OVERFLOW: auto; OVERFLOW-X: hidden; WIDTH: 716px; height:360px" runat="server">
                        <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:700px">
		                <%=strTablaHTML%>
		                </div>
		            </div>
		            <TABLE style="WIDTH:700px; HEIGHT: 17px;">
			            <TR class="TBLFIN">
				            <td>&nbsp;</td>
			            </TR>
		            </TABLE>
                    </td>
                </tr>
            </table>
            </td>
            <td background="../../Images/Tabla/6.gif" width="6">&nbsp;</td>
        </tr>
        <tr>
		    <td background="../../Images/Tabla/1.gif" height="6" width="6">
		    </td>
            <td background="../../Images/Tabla/2.gif" height="6">
            </td>
            <td background="../../Images/Tabla/3.gif" height="6" width="6">
            </td>
        </tr>
    </table>
</div>
<TABLE class="texto" id="tblGeneral" style="width:980px; margin-left:5px; margin-top:25px;" cellPadding="5">
	<TR>
		<TD>
			<TABLE id="tblTitulo" style="height:17px;width:960px">
                <colgroup>
                <col style='width:130px' />
                <col style='width:70px' />
                <col style='width:210px' />
                <col style='width:100px' />
                <col style='width:100px' />
                <col style='width:225px' />
                <col style='width:125px' />
                </colgroup>
				<TR class="TBLINI">
				    <TD>Campo</TD>
				    <TD>Acción</TD>
				    <TD>Qué</TD>
				    <TD>Valor antiguo</TD>
				    <TD>Valor nuevo</TD>
				    <TD>Quién</TD>
				    <TD>Cuándo</TD>
				</TR>
			</TABLE>
			<div id="divCatalogo" style="overflow: auto; width: 976px; height: 460px;">
                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:960px">
                </div>
            </DIV>
            <TABLE id="tblResultado" style="height:17px;width:960px">
				<TR class="TBLFIN">
					<TD>&nbsp;</TD>
				</TR>
			</TABLE>
		</TD>
    </TR>
</TABLE>
<center>
<table width="100px">
    <colgroup>
        <col style="width:100px" />
    </colgroup>
	  <tr> 
		<td>
			<button id="btnCancelar" type="button" onclick="salir();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../images/botones/imgSalir.gif" /><span title="Salir">&nbsp;&nbsp;Salir</span>
			</button>	
		</td>
	  </tr>
</table>
</center>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</form>
<script type="text/javascript">
        function WebForm_CallbackComplete() {
            for (var i = 0; i < __pendingCallbacks.length; i++) {
                callbackObject = __pendingCallbacks[i];
                if (callbackObject && callbackObject.xmlRequest && (callbackObject.xmlRequest.readyState == 4)) {
                    WebForm_ExecuteCallback(callbackObject);
                    if (!__pendingCallbacks[i].async) {
                        __synchronousCallBackIndex = -1;
                    }
                    __pendingCallbacks[i] = null;
                    var callbackFrameID = "__CALLBACKFRAME" + i;
                    var xmlRequestFrame = document.getElementById(callbackFrameID);
                    if (xmlRequestFrame) {
                        xmlRequestFrame.parentNode.removeChild(xmlRequestFrame);
                    }
                }
            }
        }
</script>
</body>
</html>

