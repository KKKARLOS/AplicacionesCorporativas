<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Relación de responsabilidades y figuras</title>
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../Javascript/boxover.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onload="init()" onunload="unload()">
<script type="text/javascript">
    var strServer = '<% =Session["strServer"].ToString() %>';
    var intSession = <%=Session.Timeout%>;
    oImgSN4.title="<%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO4) %>";
    oImgSN3.title="<%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO3) %>";
    oImgSN2.title="<%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO2) %>";
    oImgSN1.title="<%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO1) %>";
    oImgNodo.title="<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>";
    oImgSubNodo.title="<%=Estructura.getDefLarga(Estructura.sTipoElem.SUBNODO) %>";     
</script>
<form name="frmPrincipal" runat="server">
<ucproc:Procesando id="Procesando" runat="server" />
<style type='text/css'>
.W575{width:575px;}
</style>
<br />
<center>
<table style="width: 840px; margin-left:10px" cellpadding="3px">
    <colgroup>
    <col style='width:70px;' />
    <col style='width:450px;' />
    <col style='width:280px;' />
    </colgroup>
    <tr>
        <td><label id="lblProfesional" runat="server" class="texto">Profesional</label></td>
		<td><asp:TextBox ID="txtProfesional" style="width:390px;" Text="" readonly="true" runat="server" /><asp:TextBox ID="hdnIdProfesional" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" /></td>
		<td rowspan="2" style="vertical-align:top;">
            <fieldset style="width: 270px;vertical-align:top;">
                <legend>Filtro por estado</legend>   
                <table style="width:260px; margin-left:5px; margin-top:5px;" cellpadding="1" cellspacing="0" border="0" class="texto">
                    <colgroup>
                        <col style="width: 260px;" />
                    </colgroup>
                    <tr>
                        <td>
                        <input id="chkPresupuestado" runat="server" onclick="buscar()" class="check" type="checkbox" checked />
                        <img class="ICO" src="../../../../Images/imgIconoProyPresup.gif" title='Proyecto presupuestado' />
                        <input id="chkAbierto" runat="server" onclick="buscar()" class="check" type="checkbox" checked style="margin-left:25px;" />
                        <img class="ICO" src="../../../../Images/imgIconoProyAbierto.gif" title='Proyecto abierto' />
                        <input id="chkCerrado" runat="server" onclick="buscar()" class="check" type="checkbox" style="margin-left:25px;" />
                        <img class="ICO" src="../../../../Images/imgIconoProyCerrado.gif" title='Proyecto cerrado' />
                        <input id="chkHistorico" runat="server" onclick="buscar()" class="check" type="checkbox" style="margin-left:25px;" />
                        <img class="ICO" src="../../../../Images/imgIconoProyHistorico.gif" title='Proyecto histórico' />
                        </td>
                    </tr>
                </table>
            </fieldset>	
		</td>
    </tr>
    <tr>
        <td>Tipo de ítem</td>
		<td>
            <asp:DropDownList id="cboTipoItem" runat="server" Width="260px" onChange="buscar()" AppendDataBoundItems=true>
            </asp:DropDownList>
		</td>  
    </tr>
</table>
<table class="texto" align="center" style="WIDTH: 840px; table-layout:fixed; BORDER-COLLAPSE: collapse; HEIGHT: 17px" cellSpacing="0" cellpadding="0" border="0">
    <colgroup>
    <col style='width:280px;' />
    <col style='width:280px;' />
    <col style='width:280px;' />
    </colgroup>
    <tr>
		<td style="padding-top:15px; vertical-align:bottom;" id="cldTec"><img id="imgTec" runat="server" src="../../../../Images/imgTecnicoV.gif" /> Técnico especialista</td>
        <td style="padding-top:15px; vertical-align:bottom;" id="cldCRP" runat="server"><img id="imgCRP" runat="server" src="../../../../Images/imgCRPV.gif" /> <label id="lblCRP" class="texto" runat="server"></label> </td>
		<td style="padding-top:15px; vertical-align:bottom;" id="cldAdm" runat="server"><img id="imgAdm" runat="server" src="../../../../Images/imgAdministradorV.gif" /> <label id="lblAdm" class="texto" runat="server"></label> </td>
    </tr>
</table>
<table id="tblGlobal" width="840px" style=" margin-top:5px; table-layout:fixed;" align="center" cellspacing="0" cellpadding="0" border="0">
    <tr>
        <td>
            <table class="texto" style="WIDTH: 820px; table-layout:fixed; BORDER-COLLAPSE: collapse; HEIGHT: 17px" cellspacing="0" cellpadding="0" border="0">
                <colgroup>
                <col style='width:25px;' />
                <col style='width:595px;' />
                <col style='width:200px;' />
                </colgroup>
	            <tr id="tblTitulo" class="TBLINI">
	                <td>&nbsp;</td>
					<td style="text-align:left;">Denominación de item&nbsp;<img id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa2')"
							height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <img style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa2',event)"
							height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">
					</td>
                    <td>Figuras</td>
	            </tr>
            </table>
            <div id="divCatalogo" style="overflow:auto; width: 836px; height:270px;" onscroll="scrollTablaProy();">
                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:820px;">
                    <%=strTablaHTML%>
                </div>
            </div>
            <table id="tblTotales" style="width: 820px;  height: 17px; margin-bottom:3px; text-align: right;">
	            <tr class="TBLFIN">
                    <td>&nbsp;</td>
	            </tr>
            </table>
            <table width="820px">
                <colgroup>
                <col style="width:410px;" />
                <col style="width:410px;" />
                </colgroup>
                <tr>
                <td>
                    <fieldset style="width: 405px; padding:3px; height:150px;">
                        <legend>Tipos de ítem</legend>   
                        <table style="width:400px; text-align:left;" cellpadding="1" cellspacing="0" border="0" class="texto">
                            <colgroup>
                                <col style="width: 200px;" />
                                <col style="width: 200px;" />
                            </colgroup>
                            <tr> 
	                        <td><img class="ICO" src="../../../../Images/imgSN4.gif" />&nbsp;<%=Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO4)%></td>
	                        <td><img class="ICO" src="../../../../Images/imgSN3.gif" />&nbsp;<%=Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO3)%></td>
	                      </tr>
	                      <tr> 
	                        <td><img class="ICO" src="../../../../Images/imgSN2.gif" />&nbsp;<%=Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO2)%></td>
	                        <td><img class="ICO" src="../../../../Images/imgSN1.gif" />&nbsp;<%=Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO1)%></td>
	                      </tr>
	                      <tr> 
	                        <td><img class="ICO" src="../../../../Images/imgNodo.gif" />&nbsp;<%=Estructura.getDefCorta(Estructura.sTipoElem.NODO)%></td>
	                        <td><img class="ICO" src="../../../../Images/imgSubNodo.gif" />&nbsp;<%=Estructura.getDefCorta(Estructura.sTipoElem.SUBNODO)%></td>
	                      </tr>
	                      <tr> 
	                        <td><img class="ICO" src="../../../../Images/imgIconoProyPresup.gif" title='Proyecto presupuestado' /><img class="ICO" src="../../../../Images/imgIconoProyAbierto.gif" title='Proyecto abierto' /><img class="ICO" src="../../../../Images/imgIconoProyCerrado.gif" title='Proyecto cerrado' /><img class="ICO" src="../../../../Images/imgIconoProyHistorico.gif" title='Proyecto histórico' />&nbsp;Proyecto</td>
	                        <td><img class="ICO" src="../../../../Images/imgContrato.gif" />&nbsp;Contrato</td>
	                      </tr>
	                      <tr> 
	                        <td><img class="ICO" src="../../../../Images/imgHorizontal.gif" />&nbsp;Horizontal</td>
	                        <td><img class="ICO" src="../../../../Images/imgClienteICO.gif" />&nbsp;Cliente</td>
	                      </tr>
	                      <tr> 
	                        <td><img class="ICO" src="../../../../Images/imgOT.gif" />&nbsp;Oficina Técnica</td>
	                        <td><img class="ICO" src="../../../../Images/imgGF.gif" />&nbsp;Grupo Funcional</td>
	                      </tr>
	                      <tr> 
	                        <td colspan="2"><img class="ICO" src="../../../../Images/imgQn.gif" /><img class="ICO" src="../../../../Images/imgQ1.gif" /><img class="ICO" src="../../../../Images/imgQ2.gif" /><img class="ICO" src="../../../../Images/imgQ3.gif" /><img class="ICO" src="../../../../Images/imgQ4.gif" />&nbsp;Cualificadores de proyecto</td>
	                      </tr>
                        </table>
                    </fieldset>
                </td>
                <td>
                    <fieldset style="width: 395px; margin-left:10px; padding:3px; height:150px;">
                        <legend>Figuras</legend>   
                        <table style="width:390px; text-align:left;" cellpadding="1" cellspacing="0" border="0" class="texto">
                            <colgroup>
                                <col style="width: 195px;" />
                                <col style="width: 195px;" />
                            </colgroup>
	                      <tr> 
	                        <td><img class="ICO" src="../../../../Images/imgResponsable.gif" />&nbsp;Responsable</td>
	                        <td><img class="ICO" src="../../../../Images/imgDelegado.gif" />&nbsp;Delegado</td>
	                      </tr>
	                      <tr> 
	                        <td><img class="ICO" src="../../../../Images/imgColaborador.gif" />&nbsp;Colaborador</td>
	                        <td><img class="ICO" src="../../../../Images/imgInvitado.gif" />&nbsp;Invitado</td>
	                      </tr>
	                      <tr> 
	                        <td><img class="ICO" src="../../../../Images/imgGestor.gif" />&nbsp;Gestor</td>
	                        <td><img class="ICO" src="../../../../Images/imgSecretaria.gif" />&nbsp;Asistente</td>
	                      </tr>
	                      <tr> 
	                        <td title="Destinatario del informe de control de imputaciones en IAP"><img class="ICO" src="../../../../Images/imgPerseguidor.gif" title="Receptor de Informes de Actividad" />&nbsp;RIA</td>
	                        <td><img class="ICO" src="../../../../Images/imgBitacorico.gif" />&nbsp;Bitacórico</td>
	                      </tr>
	                      <tr> 
	                        <td><img class="ICO" src="../../../../Images/imgJefeProyecto.gif" />&nbsp;Jefe de proyecto</td>
	                        <td><img class="ICO" src="../../../../Images/imgSubjefeProyecto.gif" title="Responsable técnico de proyecto económico" />&nbsp;RTPE</td>
	                      </tr>
	                      <tr> 
	                        <td><img class="ICO" src="../../../../Images/imgRTPT.gif" />&nbsp;Responsable de proyecto técnico</td>
	                        <td><img class="ICO" src="../../../../Images/imgMiembroOT.gif" />&nbsp;Miembro de Oficina Técnica</td>
	                      </tr>
	                      <tr> 
	                        <td title="Soporte titular de soporte administrativo"><img class="ICO" src="../../../../Images/imgSAT.gif" />&nbsp;SAT</td>
	                        <td title="Soporte alternativo de soporte administrativo"><img class="ICO" src="../../../../Images/imgSAA.gif" />&nbsp;SAA</td>
	                      </tr>
                        </table>
                    </fieldset>
                </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<br /><br />
<table style="margin-top:5px; width:120px;">
    <tr>
        <td >
            <button id="btnCancelar" type="button" onclick="salir()" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../../images/botones/imgSalir.gif" /><span title="Salir">Salir</span></button>				
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
