<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript" language="javascript">
var nNotasPendientesAprobar = <% =nNotasPendientesAprobar %>;
var nNotasPendientesAceptar = <% =nNotasPendientesAceptar %>;
var sOpcionSeleccionada = "<% =sOpcionSeleccionada %>";


//if (nNotasPendientesAprobar > 0){
//    jQuery(document).ready(function($){
//        $('div#divAP').imgbubbles({factor:1.3})
//    })
    
//}
//if (nNotasPendientesAceptar > 0){
//    jQuery(document).ready(function($){
//        $('div#divAC').imgbubbles({factor:1.3})
//    })
//}

/* Fin código ICONOS PEQUEÑOS */
</script>
<div id="divMarcoAcciones">
    <div id="divAP" class="bubblewrap"><li><img id="imgAP" src="../../Images/imgAprobar.gif" alt="Pendiente de aprobación" title="Pendiente de aprobación"></li>
        <span>Pendiente de aprobación</span>
    </div>
    <div id="divAC" class="bubblewrap"><li><img id="imgAC" src="../../Images/imgPagar.gif" alt="Pendiente de aceptación" title="Pendiente de aceptación" /></li>
        <span style="display:inline-block;margin-top:10px">Pendiente de aceptación</span>
    </div>
    <div id="divNumAP"></div>
    <div id="divNumAC"></div>

    <h3 id="visados">Visados</h3>
</div>
<div id="divTitulo"  style="text-align:center;width: 240px; height: 33px; position: absolute; top: 135px; left: 320px; padding-top: 5px;"><img id="imgPendiente" src="../../Images/imgSeparador.gif" /></div>
<table style="width:830px; margin-top:25px;">
<colgroup>
    <col style="width:415px;" />
    <col style="width:415px;" />
</colgroup>
<tr>
    <td colspan="2">
        <div  style="text-align:center;background-image: url('../../Images/imgFondo185.gif'); background-repeat:no-repeat;
            width: 185px; height: 23px; position: relative; top: 12px; left: 20px; padding-top: 5px;text-align:center;
        font:bold 12px Arial;
        color:#5894ae;">Solicitudes estándar</div>
        <table style="width:825px;" cellpadding="0">
            <tr>
                <td style="background-image:url(../../Images/Tabla/7.gif); height:6px; width:6px;"></td>
                <td style="background-image:url(../../Images/Tabla/8.gif);"></td>
                <td style="background-image:url(../../Images/Tabla/9.gif); height:6px; width:6px;"></td>
            </tr>
            <tr>
                <td style="background-image:url(../../Images/Tabla/4.gif);">&nbsp;</td>
                <td style="background-image:url(../../Images/Tabla/5.gif); padding:5px; vertical-align:top;">
                    <!-- Inicio del contenido propio de la página -->
                <table id="tblTituloEstandar" style="width:785px;height:17px; margin-top:10px;">
	                <colgroup>					
		                <col style="width:40px; text-align:center;" />
		                <col style="width:20px; text-align:center;" />
		                <col style="width:26px; text-align:center;" />
		                <col style="width:60px; text-align:right; padding-right:10px;" />
		                <col style="width:115px;" />
		                <col style="width:154px;" />
		                <col style="width:120px;" />
		                <col style="width:130px; padding-left:20px;" />
				        <col style="width:50px;" />
		                <col style='width:70px; ' />
	                </colgroup>
	                <tr class="TBLINI">
		                <td><img src="../../images/botones/imgMarcar.gif" onclick="mTabla('tblDatosEstandar')" title="Marca todas las líneas para ser procesadas" style="cursor:pointer; margin-top:1px;" />
			            <img src="../../images/botones/imgDesmarcar.gif" onclick="dTabla('tblDatosEstandar')" title="Desmarca todas las líneas" style="cursor:pointer; margin-top:1px;" />   
                        </td>
                        <td colspan="3" style="text-align:right; padding-right:10px;">Referencia</td>
		                <td>Beneficiario</td>
		                <td>Concepto</td>
		                <td>Motivo</td>
		                <td>Proyecto</td>
				        <td>Moneda</td>
		                <td style="text-align:right; padding-right:2px;">Importe</td>
	                </tr>
                </table>
                <div id="divCatalogoEstandar" style="OVERFLOW-X: hidden; OVERFLOW: auto; WIDTH: 801px; height:217px" runat="server">
	                <div style="background-image:url(<%=Session["GVT_strServer"] %>Images/imgFT20.gif); WIDTH: 785px;">
	                 <%=strHTMLTablaNotas%>
	                </div>
                </div>
               <%-- <table id="tblPieEstandar" style="width:785px;height:17px;">
	                <tr class="TBLFIN">
		                <td>&nbsp;</td>
	                </tr>
                </table>--%>
                    <!-- Fin del contenido propio de la página -->
                </td>
                    <td style="background-image:url(../../Images/Tabla/6.gif); width:4px">&nbsp;</td>
                </tr>
                <tr>
                    <td style="background-image:url(../../Images/Tabla/1.gif); height:6px; width:6px"></td>
                    <td style="background-image:url(../../Images/Tabla/2.gif); height:6px"></td>
                    <td style="background-image:url(../../Images/Tabla/3.gif); height:6px; width:6px"></td>
            </tr>
        </table>
    </td>
</tr>
<tr>
    <td>
        <div  style="text-align:center;background-image: url('../../Images/imgFondo185.gif'); background-repeat:no-repeat;
            width: 185px; height: 23px; position: relative; top: 12px; left: 20px; padding-top: 5px;text-align:center;
        font:bold 12px Arial;
        color:#5894ae;">Bonos de transporte</div>
        <table style="width:410px;"  cellpadding="0">
            <tr>
                <td style="background-image:url(../../Images/Tabla/7.gif); height:6px; width:6px; padding: 0px"></td>
                <td style="background-image:url(../../Images/Tabla/8.gif);"></td>
                <td style="background-image:url(../../Images/Tabla/9.gif);  height:6px; width:6px; padding: 0px"></td>
            </tr>
            <tr>
                <td style="background-image:url(../../Images/Tabla/4.gif);">&nbsp;</td>
                <td style="background-image:url(../../Images/Tabla/5.gif); padding:5px; vertical-align:top;">
                    <!-- Inicio del contenido propio de la página -->
                <table id="tblTituloBonoTrans" style="width:370px;height:17px; margin-top:10px;">
	                <colgroup>					
		                <col style="width:30px; text-align:center;" />
		                <col style="width:50px; text-align:right; padding-right:5px;" />
		                <col style="width:100px;" />
		                <col style="width:90px;" />
		                <col style="width:50px;" />
		                <col style='width:50px; ' />
	                </colgroup>
	                <tr class="TBLINI">				    
		                <td><img src="../../images/botones/imgMarcar.gif" onclick="mTabla('tblDatosBonoTrans')" title="Marca todas las líneas para ser procesadas" style="cursor:pointer; margin-top:1px; margin-left:1px;" /><img src="../../images/botones/imgDesmarcar.gif" onclick="dTabla('tblDatosBonoTrans')" title="Desmarca todas las líneas" style="cursor:pointer; margin-top:1px; margin-left:1px;" /></td>
		                <td title="Referencia">Ref.</td>
		                <td>Beneficiario</td>
		                <td>Mes</td>
		                <td>Moneda</td>
		                <td style="text-align:right; padding-right:2px;">Importe</td>
	                </tr>
                </table>
                <div id="divCatalogoBonoTrans" style="OVERFLOW-X: hidden; OVERFLOW: auto; WIDTH: 386px; height:117px" runat="server">
	                <div style="background-image:url(<%=Session["GVT_strServer"] %>Images/imgFT20.gif); WIDTH: 370px;">
	                 <%=strHTMLTablaBonos%>
	                </div>
                </div>
                <%--<table id="tblPieBonoTrans" style="width:370px;height:17px;">
	                <tr class="TBLFIN">
		                <td>&nbsp;</td>
	                </tr>
                </table>--%>
                    <!-- Fin del contenido propio de la página -->
                </td>
                    <td style="background-image:url(../../Images/Tabla/6.gif); width:4px">&nbsp;</td>
                </tr>
                <tr>
                    <td style="background-image:url(../../Images/Tabla/1.gif);  height:6px; width:6px"></td>
                    <td style="background-image:url(../../Images/Tabla/2.gif); height:6px"></td>
                    <td style="background-image:url(../../Images/Tabla/3.gif);  height:6px; width:6px"></td>
            </tr>
        </table>
    </td>
    <td>
        <div style="text-align:center;background-image: url('../../Images/imgFondo185.gif'); background-repeat:no-repeat;
            width: 185px; height: 23px; position: relative; top: 12px; left: 20px; padding-top: 5px;text-align:center;
        font:bold 12px Arial;
        color:#5894ae;">Pagos concertados</div>
        <table style="width:410px;"  cellpadding="0">
            <tr>
                <td style="background-image:url(../../Images/Tabla/7.gif);  height:6px; width:6px; padding: 0px"></td>
                <td style="background-image:url(../../Images/Tabla/8.gif);"></td>
                <td style="background-image:url(../../Images/Tabla/9.gif); height:6px; width:6px; padding: 0px"></td>
            </tr>
            <tr>
                <td style="background-image:url(../../Images/Tabla/4.gif);">&nbsp;</td>
                <td style="background-image:url(../../Images/Tabla/5.gif); padding:5px; vertical-align:top;">
                    <!-- Inicio del contenido propio de la página -->
                <table id="tblTituloPagosConcertados" style="width:370px;height:17px; margin-top:10px;">
	                <colgroup>					
		                <col style="width:30px; text-align:center;" />
		                <col style="width:50px; text-align:right; padding-right:5px;" />
		                <col style="width:190px;" />
		                <col style="width:50px;" />
		                <col style='width:50px; ' />
	                </colgroup>
	                <tr class="TBLINI">				    
		                <td><img id="imgMarcarPC" src="../../images/botones/imgMarcar.gif" onclick="mTabla('tblDatosPagosConcertados')" title="Marca todas las líneas para ser procesadas" style="cursor:pointer; margin-top:1px; margin-left:1px;" /><img id="imgDesmarcarPC" src="../../images/botones/imgDesmarcar.gif" onclick="dTabla('tblDatosPagosConcertados')" title="Desmarca todas las líneas" style="cursor:pointer; margin-top:1px; margin-left:1px;" /></td>
		                <td title="Referencia">Ref.</td>
		                <td>Beneficiario</td>
		                <td>Moneda</td>
		                <td style="text-align:right; padding-right:2px;">Importe</td>
	                </tr>
                </table>
                <div id="divCatalogoPagosConcertados" style="OVERFLOW-X: hidden; OVERFLOW: auto; WIDTH: 386px; height:117px" runat="server">
	                <div style="background-image:url(<%=Session["GVT_strServer"] %>Images/imgFT20.gif); WIDTH: 370px;">
	                 <%=strHTMLTablaPagos%>
	                </div>
                </div>
               <%-- <table id="tblPiePagosConcertados" style="width:370px;height:17px;">
	                <tr class="TBLFIN">
		                <td>&nbsp;</td>
	                </tr>
                </table>--%>
                    <!-- Fin del contenido propio de la página -->
                </td>
                    <td style="background-image:url(../../Images/Tabla/6.gif); width:4px">&nbsp;</td>
                </tr>
                <tr>
                    <td style="background-image:url(../../Images/Tabla/1.gif); height:6px; width:6px"></td>
                    <td style="background-image:url(../../Images/Tabla/2.gif); height:6px"></td>
                    <td style="background-image:url(../../Images/Tabla/3.gif); height:6px; width:6px"></td>
            </tr>
        </table>
    </td>
</tr>

</table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript" language="javascript">
    function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
		    var strBoton = Botonera.botonID(eventArgument).toLowerCase();
		    switch (strBoton){
			    case "aprobar": 
			    {
                    bEnviar = false;
                    aprobar();
				    break;
			    }
			    case "aceptarnota": 
			    {
                    bEnviar = false;
                    aceptar();
				    break;
			    }
			    case "regresar": 
			    {
                    bEnviar = false;
                    location.href = "../Inicio/Default.aspx";
				    break;
			    }
		    }
	    }
	    var theform;
	    theform = document.forms[0];
	    theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
	    theform.__EVENTARGUMENT.value = eventArgument;
	    if (bEnviar){
		    theform.submit();
	    }
//	    else{
//		    //Si se ha "cortado" el submit, se restablece el estado original de la botonera.
//		    $I("Botonera").restablecer();
//	    }
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
</asp:Content>

