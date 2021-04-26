<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" ValidateRequest="false" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Cerrar diálogo de alerta</title>
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <script src="../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script src="Functions/funciones.js?q=2" type="text/Javascript"></script>
 	<script src="../../../../Javascript/dhtmltooltip.js" type="text/Javascript"></script>
 	<script src="../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onLoad="init()">
<form id="Form1" name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
<script type="text/javascript">
    var strServer = "<% =Session["strServer"].ToString() %>";
    var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
    var sTooltipInfoProy = "<% =sTooltipInfoProy %>";
    var bCambios = false;
    var bSalir = false;
    mostrarProcesando();
</script>
<style>
    #tblGeneral td { padding: 5px 5px 5px 5px;}

    #tblOtrasAlertas tr, #tblOtrosDialogos tr { height:20px; }
    #tblOtrasAlertas td {
        text-align: center;
	    border-collapse: separate;
        border-spacing: 0px;
	    border: 1px solid #A6C3D2;
	    padding: 0px 2px 0px 2px;
    }
    #tblTituloAlertas td, #tblPieAlertas td,
    #tblTituloOtrosDialogos td, #tblPieOtrosDialogos td { padding: 0px; }
    #tblOtrosDialogos td {padding: 0px 2px 0px 2px;}
</style>
<asp:Panel runat="server" ID="pnDatos">

<table id="tblGeneral" style="width:940px; margin-left:10px;" border="0">
    <colgroup>
        <col style='width: 400px;' />
        <col style='width: 540px;' />
    </colgroup>
    <tr>
        <td>
            <fieldset id="Fieldset1" style="width:380px; height:170px;">
            <legend>Alerta actual</legend>
            <table id="tblAlertaActual" style="width:380px; margin-top:5px; margin-left:2px;" border="0">
            <colgroup>
                <col style='width: 70px;' />
                <col style='width: 60px;' />
                <col style='width: 250px;' />
            </colgroup>
                <tr id="trAlertaActual" runat="server" bd="" 
                idPSN=""
                idAlerta=""
                inistandby=""
                finstandby=""
                seg="">
                    <td>Asunto</td>
                    <td colspan="2"><asp:TextBox ID="txtAsunto" runat="server" style="width:290px" readonly="true" /></td>
                </tr>
                <tr>
                    <td>Activada</td>
                    <td><input type="checkbox" id="chkHabilitada" class="check" style="cursor:pointer;" runat="server" onclick="setHabilitada(this, true);" /> </td>
                    <td>Seguimiento <input type="checkbox" id="chkSeguimiento" class="check" onclick="setSeguimiento()" style="cursor:pointer;" runat="server" /><img id="imgSegAlertaActual" src="../../../../images/imgSeguimiento.png" onclick="ModificarSeguimiento(this)" style="cursor:pointer; vertical-align:middle; margin-left:2px; border: 0px;visibility:visible; width:16px; height:16px;" runat="server" /></td>
                </tr>
                <tr style="height:60px;">
                    <td colspan="3" valign="top">
                        <fieldset id="fstStandby" style="width:310px; margin-left:20px; margin-top:-5px; height:45px; visibility:hidden;">
                        <legend id="lgdStandby"><label id="lblStandby" onclick="getPeriodo()">Periodo Standby</label></legend>
                        <table id="tblPeriodo" style="width:270px" cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td style="width:30px;" title="Inicio de periodo en el que, aún estándo activada la alerta, no se tiene en cuenta en los controles pertinentes">Inicio</td>
                                <td style="width:120px;"><asp:TextBox ID="txtIniStby" runat="server" style="width:90px; text-align:center;" readonly="true" runat="server" /></td>
                                <td style="width:20px;"title="Fin de periodo en el que, aún estándo activada la alerta, no se tiene en cuenta en los controles pertinentes">Fin</td>
                                <td style="width:100px;"><asp:TextBox ID="txtFinStby" runat="server" style="width:90px; text-align:center;" readonly="true" runat="server" /></td>
                            </tr>
                        </table>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                    <span id="spanProyecto" runat="server"><img src="../../../../Images/info.gif" style="vertical-align:middle;" /> Proyecto</span>
                    </td>
                   </tr>
            </table>
            </fieldset>
        </td>
        <td>
            <fieldset id="fstOtrasAlertas" style="width:520px; height:170px;">
            <legend>Otras alertas bajo mi gestión</legend>
            <table id="tblTituloAlertas" style="width:500px; height:17px; margin-top:5px; margin-left:2px;" border="0">
            <colgroup>
                <col style='width: 215px;' />
                <col style='width: 25px;' />
                <col style='width: 100px;' />
                <col style='width: 120px;' />
                <col style='width: 40px;' />
            </colgroup>
                <tr class="TBLINI" align="center">
                    <td>Asunto</td>
                    <td title="Una alerta activada se tiene en cuenta en los controles pertinentes">Act.</td>
                    <td title="Inicio de periodo en el que, aún estándo activada la alerta, no se tiene en cuenta en los controles pertinentes">Ini. Stby</td>
                    <td title="Fin de periodo en el que, aún estándo activada la alerta, no se tiene en cuenta en los controles pertinentes">Fin. Stby</td>
                    <td title="Seguimiento">Seg.</td>
                </tr>
            </table>
            <div id="divCatalogoAlertas" style="width:516px; height:120px; overflow:auto; overflow-x:hidden; margin-left:2px;" runat="server">
                <div style="background-image: url('../../../../Images/imgFT20.gif'); background-repeat:repeat; width:500px; height:auto;">
                <%=strTablaHTMLAlertas%>
                </div>
            </div>
            <table id="tblPieAlertas" style="width:500px; height:17px; margin-left:2px;">
                <tr class="TBLFIN">
                    <td>&nbsp;</td>
                </tr>
            </table>
            </fieldset>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <fieldset id="fstDialogos" style="width:920px; height:120px;">
            <legend>Otros diálogos abiertos bajo mi gestión</legend>
            <table id="tblTituloOtrosDialogos" style="width:900px; height:17px; margin-top:5px; margin-left:2px;" border="0">
            <colgroup>
                <col style='width: 300px;' />
                <col style='width: 300px;' />
                <col style='width: 90px;' />
                <col style='width: 100px;' />
                <col style='width: 110px;' />
            </colgroup>
                <tr class="TBLINI" align="center">
                    <td>Asunto</td>
                    <td>Estado</td>
                    <td>Mes</td>
                    <td title="Cerrar el diálogo en estado conforme">Cerrar conforme</td>
                    <td title="Cerrar el diálogo en estado no conforme">Cerrar no conf.</td>
                </tr>
            </table>
            <div id="divCatalogoOtrosDialogos" style="width:916px; height:60px; overflow:auto; overflow-x:hidden; margin-left:2px;" runat="server">
                <div style="background-image: url('../../../../Images/imgFT20.gif'); background-repeat:repeat; width:900px; height:auto;">
                <%=strTablaHTMLOtrosDialogos%>
                </div>
            </div>
            <table id="tblPieOtrosDialogos" style="width:900px; height:17px; margin-left:2px;">
                <tr class="TBLFIN">
                    <td>&nbsp;</td>
                </tr>
            </table>
            </fieldset>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <fieldset id="fstObraCurso" style="width:920px; height:175px;">
            <legend>Cualificación de la obra en curso y facturación anticipada</legend>
            <table id="tblObraCurso" style="width:900px;" border="0">
            <colgroup>
                <col style='width: 450px;' />
                <col style='width: 450px;' />
            </colgroup>
            <tr>
                <td>
                    ¿Se justificada la existencia? 
                    <asp:RadioButtonList ID="rdbOC" SkinId="rbl" runat="server" RepeatLayout="Flow" style="height:20px;" RepeatColumns="2">
                        <asp:ListItem Value="1">Sí&nbsp;&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem  Value="0">No</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                    Motivo
                    <asp:DropDownList ID="cboMotivo" runat="server" style="vertical-align:middle;width:160px;" AppendDataBoundItems=True >
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Información complementaria<br /><asp:TextBox ID="txtCausa" SkinID="Multi" runat="server" TextMode="MultiLine" Rows="7" style="width:420px" onKeyUp="fm(event);"></asp:TextBox></td>
                <td>Acciones acordadas<br /><asp:TextBox ID="txtAcciones" SkinID="Multi" runat="server" TextMode="MultiLine" Rows="7" style="width:420px" onKeyUp="fm(event);"></asp:TextBox></td>
            </tr>
            </table>            
            </fieldset>
        </td>
    </tr>
</table>

</asp:Panel>
<center>
<table style="width:60%; margin-top:10px;">
	<tr> 
		<td align="center">
            <button id="btnNewAnot" type="button" class="btnH30W145" onmouseover="se(this, 30)" style="margin-top:3px;"
                    onclick="cerrarDialogo(4);" runat="server" hidefocus="hidefocus">
                <img src="../../../../images/imgConforme.png" /><span>Cerrar conforme</span>
            </button>
		</td>
		<td align="center">
            <button id="btnDocumentacion" type="button" class="btnH30W145" runat="server" hidefocus="hidefocus" title="Cerrar no conforme"
                onclick="cerrarDialogo(5);" onmouseover="se(this, 30);mostrarCursor(this);">
                <img id="imgDocFact" src="../../../../images/imgNoConforme.png" runat="server" /><span>Cerrar no conf...</span>
            </button>
		</td>
		<td align="center">
            <button id="btnSalir" type="button" class="btnH30W140" runat="server" hidefocus="hidefocus" 
                onclick="cancelar();" onmouseover="se(this, 30);mostrarCursor(this);">
                <img src="../../../../images/imgCancelarAspa.png" /><span>Cancelar</span>
            </button>
		</td>
	  </tr>
</table>
</center>
<div id="divTotal" style="z-index:10; position:absolute; left:0px; top:0px; width:1100px; height:900px; background-image: url(../../../../Images/imgFondoPixelado2.gif); background-repeat:repeat; display:none;" runat="server">
    <div id="divSeguimiento" style="position:absolute; top:170px; left:290px;">
        <table border="0" cellspacing="0" cellpadding="0" style="width:420px;margin-top:5px;">
          <tr>
            <td width="6" height="6" background="../../../../Images/Tabla/7.gif"></td>
            <td height="6" background="../../../../Images/Tabla/8.gif"></td>
            <td width="6" height="6" background="../../../../Images/Tabla/9.gif"></td>
          </tr>
          <tr>
            <td width="6" background="../../../../Images/Tabla/4.gif">&nbsp;</td>
            <td background="../../../../Images/Tabla/5.gif" style="padding:3px; vertical-align:top;">
            <!-- Inicio del contenido propio de la página -->
            <table id="tblSeguimiento" class="texto" style="width:400px; height:200px; " cellspacing="2" cellpadding="0" border="0">
                <tr>
                    <td>
                        <label id="lblTextoSeguimiento">Para ACTIVAR un seguimiento, es preciso indicar el motivo del mismo.</label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Motivo<br />
                        <asp:TextBox id="txtSeguimiento" SkinID="Multi" TextMode="multiLine" runat="server" style="width:390px; height:100px; margin-top:5px;" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <button id="btnActivarDesactivar" type="button" class="btnH25W100" runat="server" hidefocus="hidefocus" 
                            style="float:left; margin-left:100px" onmouseover="se(this, 25);">
                            <img id="imgBotonActivar" src="../../../../images/imgSegAdd.png" /><span id="lblBoton">Activar</span>
                        </button>
                        <button id="btnCancelar" type="button" class="btnH25W100" runat="server" hidefocus="hidefocus" style="float:left; margin-left:20px"
                            onclick="CancelarSeguimiento();" onmouseover="se(this, 25);">
                            <img src="../../../../images/Botones/imgCancelar.gif" /><span>Cancelar</span>
                        </button>
                    </td>
                </tr>
            </table>
                <!-- Fin del contenido propio de la página -->
                </td>
                <td width="6" background="../../../../Images/Tabla/6.gif">&nbsp;</td>
              </tr>
          <tr>
            <td width="6" height="6" background="../../../../Images/Tabla/1.gif"></td>
            <td height="6" background="../../../../Images/Tabla/2.gif"></td>
            <td width="6" height="6" background="../../../../Images/Tabla/3.gif"></td>
          </tr>
        </table>
    </div>
</div>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<input type="hidden" name="hdnIdDialogo" id="hdnIdDialogo" value="-1" runat="server" />
<input type="hidden" name="hdnIdEstado" id="hdnIdEstado" value="-1" runat="server" />
<input type="hidden" name="hdnIdPSN" id="hdnIdPSN" value="-1" runat="server" />
<input type="hidden" name="hdnIdAlerta" id="hdnIdAlerta" value="-1" runat="server" />
<input type="hidden" name="hdnGrupoOC" id="hdnGrupoOC" value="0" runat="server" />
<input type="hidden" name="hdnTipoOCFA" id="hdnTipoOCFA" value="" runat="server" />
<input type="hidden" name="hdnMotivoOCFA" id="hdnMotivoOCFA" value="" runat="server" />
<uc_mmoff:mmoff ID="mmoff" runat="server" />
<uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
</form>
<script type="text/javascript">

    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        var theform;
        theform = document.forms[0];
//        if (window.navigator.appName.toLowerCase().indexOf("microsoft") > -1) {
//            theform = document.forms[0];
//        }
//        else {
//            theform = document.forms["frmPrincipal"];
//        }

        theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
        theform.__EVENTARGUMENT.value = eventArgument;
        if (bEnviar) {
            theform.submit();
        }
        else {
            document.getElementById("Botonera").restablecer();
        }
    }

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
