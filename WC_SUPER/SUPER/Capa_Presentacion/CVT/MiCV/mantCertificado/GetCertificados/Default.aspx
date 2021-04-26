<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" ValidateRequest="false" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Búsqueda de certificado</title>
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
	<script language="JavaScript" src="../../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../../Javascript/dhtmltooltip.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../../Javascript/jquery.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onload="init()">
<form id="Form1" name="frmPrincipal" runat="server" >
<ucproc:Procesando ID="Procesando" runat="server" />
<script type="text/javascript">
    var strServer = "<% =Session["strServer"].ToString() %>";
    var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
    var bCambios = false;
    var bSalir = false;
    mostrarProcesando();
    jQuery.fn.extend({
        resaltar: function(busqueda, claseCSSbusqueda){
            var regex = new RegExp("(<[^>]*>)|("+ busqueda.replace(/([-.*+?^${}()|[\]\/\\])/g,"\\$1") +')', 'ig');
            var nuevoHtml=this.html(this.html().replace(regex, function(a, b, c){
                return (a.charAt(0) == "<") ? a : "<span class=\""+ claseCSSbusqueda +"\">" + c + "</span>";
            }));
            return nuevoHtml;
        }
    });
    function resaltarTexto(){
        var aResul = $I("txtDen").value.split(" ");
        if (aResul.length > 0){
            for (var i=0;i<aResul.length;i++)
            $("#divCatalogo").resaltar(aResul[i], "resaltarTexto");
        }
    }
</script>    
<style type="text/css">
.resaltarTexto{
    color:Navy;
    font-weight: bold;
}
</style>
<fieldset style="width:650px; margin-left:20px; margin-top:10px;">
    <legend>Criterios</legend>
    <table style="width:630px; margin-left:10px;" cellpadding="5" border="0">
        <colgroup><col style="width:110px;"/><col style="width:410px;"/><col style="width:110px;"/></colgroup>
        <tr>
            <td>
                <label>Denominación</label>
            </td>
            <td colspan="2">
                <asp:TextBox id="txtDen" style="width:505px;" MaxLength="200" runat="server" onkeypress="f_onkeypress(event)"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <label>Ent. certificadora</label>
            </td>
            <td>
		        <asp:DropDownList ID="cboEntCert" runat="server" onchange="borrarCatalogo();" style="width:350px;" AppendDataBoundItems="true" class="combo">
                </asp:DropDownList>
            </td>
            <td rowspan="2">
                <button id="btnObtener" type="button" class="btnH30W100" onmouseover="se(this, 30)" 
                        onclick="buscar();" runat="server" hidefocus="hidefocus">
                    <img src="../../../../../images/imgObtener.gif" /><span>Obtener</span>
                </button>
            </td>
        </tr>
        <tr>
            <td>
                <label>Ent. tecnológico</label>
            </td>
            <td>
		        <asp:DropDownList ID="cboEntorno" runat="server" onchange="borrarCatalogo();" style="width:350px;" AppendDataBoundItems="true" class="combo">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
</fieldset> 
<fieldset style="width:650px; margin-left:20px; margin-top:10px;">
    <legend>Certificados</legend>
    <table id="Table1" style="width:630px;" cellpadding="5">
	    <tr>
		    <td>
			    <table id="tblTitulo" style="width:630px">
				    <tr class="TBLINI" style="height:17px;">
					    <td>
                            &nbsp;Denominación
					    </td>
				    </tr>
			    </table>
			    <div id="divCatalogo" style="overflow: auto; width: 646px; height: 320px;" >
                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:630px">
                        <%=strTablaHTML%>
                        </div>
                </div>
                <table id="tblResultado" style="width:630px">
				    <tr class="TBLFIN"  style="height:17px;">
					    <td>&nbsp;</td>
				    </tr>
			    </table>
		    </td>
        </tr>
    </table>
</fieldset>
<table id="Table2" style="width:460px; margin-left:120px; margin-top:10px;" cellpadding="5">
	<tr> 
		<td align="center">
            <button id="btnSeleccionar" type="button" class="btnH25W105" onmouseover="se(this, 25)" 
                    onclick="seleccionar();" runat="server" hidefocus="hidefocus">
                <img src="../../../../../images/imgAceptar.gif" /><span>Seleccionar</span>
            </button>
		</td>
		<td align="center">
            <button id="btnSalir" type="button" class="btnH25W100" runat="server" hidefocus="hidefocus"
                onclick="salir();" onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../../../images/imgCancelar.gif" /><span>Cancelar</span>
            </button>
		</td>
		<td align="center">
            <button id="btnSolicitar" type="button" class="btnH25W150" runat="server" hidefocus="hidefocus" title="Solicitud de nuevo certificado"
                onclick="solicitarCert();" onmouseover="se(this, 25);mostrarCursor(this);">
                <img id="imgDocFact" src="../../../../../images/botones/imgCargarLista.gif" runat="server" /><span>Solicitar certificado</span>
            </button>
		</td>
	  </tr>
</table>
<br />
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<input type="hidden"  name="hdnIdFicepiCert" id="hdnIdFicepiCert" runat="server" value="-1" />
<uc_mmoff:mmoff ID="mmoff" runat="server" />
</form>
<script type="text/javascript">

    //setOp($I("btnAddMensaje"), 30);

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
