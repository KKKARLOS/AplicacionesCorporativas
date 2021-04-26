<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getClasesClonar.aspx.cs" Inherits="getClasesClonar" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Selección de datos a clonar</title>
	<meta http-equiv='X-UA-Compatible' content='IE=8' />
	<script language="JavaScript" src="../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
	<!--
    function init(){
        try{
            if (!mostrarErrores()) return;

            $I("chkConsProf").checked = fOpener().bConsPersonas;
            $I("chkConsNivel").checked = fOpener().bConsNivel;
            $I("chkProdProf").checked = fOpener().bProdProfesional;
            $I("chkProdPerfil").checked = fOpener().bProdPerfil;
            $I("chkAvance").checked = fOpener().bAvance;
            $I("chkPeriodCons").checked = fOpener().bPeriodificacionC;
            $I("chkPeriodProd").checked = fOpener().bPeriodificacionP;

            var js_clases = fOpener().sClasesClonables.split(",");
            var tblDatos = $I("tblDatos");
            
            for (var i=0; i<tblDatos.rows.length; i++){
                if (tblDatos.rows[i].getAttribute("tipo") != "CL") continue;
                if (js_clases.isInArray(tblDatos.rows[i].id)!=null)
                    tblDatos.rows[i].cells[0].children[0].checked = true;
                else 
                    tblDatos.rows[i].cells[0].children[0].checked = false;
            }

            window.focus();
            ocultarProcesando();
        }catch(e){
            mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
        }
    }
	
    function aceptar(){
	    try{
            var sb = new StringBuilder; //sin paréntesis

            sb.Append(($I("chkConsProf").checked)? "1@#@":"0@#@");
            sb.Append(($I("chkConsNivel").checked)? "1@#@":"0@#@");
            sb.Append(($I("chkProdProf").checked)? "1@#@":"0@#@");
            sb.Append(($I("chkProdPerfil").checked)? "1@#@":"0@#@");
            sb.Append(($I("chkAvance").checked)? "1@#@":"0@#@");
            sb.Append(($I("chkPeriodCons").checked)? "1@#@":"0@#@");
            sb.Append(($I("chkPeriodProd").checked)? "1@#@":"0@#@");
            
            var tblDatos = $I("tblDatos");
            
            for (var i=0; i<tblDatos.rows.length; i++){
                if (tblDatos.rows[i].getAttribute("tipo") != "CL") continue;
                if (tblDatos.rows[i].cells[0].children[0].checked) 
                    sb.Append(tblDatos.rows[i].id+",");
            }
            
	        var returnValue = sb.ToString();
	        modalDialog.Close(window, returnValue);			        
        }catch(e){
            mostrarErrorAplicacion("Error al aceptar", e.message);
        }
    }
	
    function cerrarVentana(){
	    try{
            if (bProcesando()) return;
            
	        var returnValue = null;
	        modalDialog.Close(window, returnValue);	
        }catch(e){
            mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
        }
    }
	-->
    </script>
</head>
<body style="OVERFLOW: hidden" class=texto leftMargin="10" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
    <form id="form1" runat="server">
	<script type="text/javascript">
	<!--
        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";
	-->
	</script>
	<center>
    <table style="width:520px;margin-left:15px;margin-top:25px;text-align:left" cellpadding="3">
        <colgroup>
            <col style="width: 260px;" />
            <col style="width: 260px;" />
        </colgroup>
        <tr>
            <td>
            <fieldset id="fstConsumos" style="width: 240px; height: 101px;">
                <legend>Consumos</legend> 
                <table  style="width: 240px;"  cellpadding="3">
                    <tr>
                        <td><input type='checkbox' id="chkConsProf" class='checkTabla' style="vertical-align:middle;" checked /><label style="cursor:pointer; margin-left:5px;"  onclick="this.previousSibling.click()" />Por dedicación de profesionales</label></td>
                    </tr>
                    <tr>
                        <td><input type='checkbox' id="chkConsNivel" class='checkTabla' style="vertical-align:middle;" checked /><label style="cursor:pointer; margin-left:5px;"  onclick="this.previousSibling.click()" >Por nivel</label></td>
                    </tr>
                    <tr>
                        <td><input type='checkbox' id="chkPeriodCons" class='checkTabla' style="vertical-align:middle;" checked /><label style="cursor:pointer; margin-left:5px;"  onclick="this.previousSibling.click()" >Por periodificación</label></td>
                    </tr>
                </table>
            </fieldset>
            </td>
            <td>
            <fieldset id="fstProduccion" style="width: 240px; height: 101px;">
                <legend>Producción</legend> 
                <table  style="width: 240px;"  cellpadding="3">
                    <tr>
                        <td><input type='checkbox' id="chkProdProf" class='checkTabla' style="vertical-align:middle;" checked /><label style="cursor:pointer; margin-left:5px;"  onclick="this.previousSibling.click()" >Por dedicación de profesionales</label></td>
                    </tr>
                    <tr>
                        <td><input type='checkbox' id="chkProdPerfil" class='checkTabla' style="vertical-align:middle;" checked /><label style="cursor:pointer; margin-left:5px;"  onclick="this.previousSibling.click()" >Producción por perfil</label></td>
                    </tr>
                    <tr>
                        <td><input type='checkbox' id="chkPeriodProd" class='checkTabla' style="vertical-align:middle;" checked /><label style="cursor:pointer; margin-left:5px;"  onclick="this.previousSibling.click()" >Por periodificación</label></td>
                    </tr>
                    <tr>
                        <td><input type='checkbox' id="chkAvance" class='checkTabla' style="vertical-align:middle;" checked /><label style="cursor:pointer; margin-left:5px;"  onclick="this.previousSibling.click()" >Por avance</label></td>
                    </tr>
                </table>
            </fieldset>
            </td>
        </tr>
        <tr>
            <td style="padding-top:10px;" colspan="2">
                <table id="tblTitulo" style="width: 500px; height: 17px">
                    <tr class="TBLINI">
                        <td style="padding-left:5px">Grupo / Subgrupo / Concepto / Clase</td>
                    </tr>
                </table>
                <div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 516px; height:320px">
                    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:500px">
                    <%=strTablaHTML%>
                    </div>
                </div>
                <table style="width: 500px; height: 17px; margin-bottom:3px;">
                    <tr class="TBLFIN">
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="290px" style="margin-top:25px;">
		<tr>
			<td>
                <button id="btnAceptar" type="button" onclick="aceptar();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
                     onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../images/imgAceptar.gif" /><span>Aceptar</span>
                </button>
			</td>
			<td align="center">
                <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
                     onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../images/imgCancelar.gif" /><span>Cancelar</span>
                </button>
			</td>
		</tr>
    </table>
 </center>

    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    </form>
<script type="text/javascript">
<!--
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
    	
-->
</script>
</body>
</html>
