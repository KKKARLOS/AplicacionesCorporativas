<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Administracion_EstructuraOrg_Default" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
    var nNEAux = <%= nNE.ToString() %>;
</script>
<table id="tblGeneral" style="width:991px;" cellpadding="0" cellspacing="0" border="0" runat="server">
<colgroup> 
    <col style="width:500px;" />
    <col style="width:491px;" />
</colgroup>
<tr>
    <td style="width:500px;">
    <table style="width: 500px;" cellpadding="0" cellspacing="0" border="0">
        <colgroup>
        <col style="width:110px;" />
        <col style="width:260px;" />
        <col style="width:130px;" />
        </colgroup>
        <tr style="height:35px;">
            <td>
                <img id="imgNE1" src='../../../images/imgNE1on.gif' class="ne" onclick="setNE(1);"><img id="imgNE2" src='../../../images/imgNE2off.gif' class="ne" onclick="setNE(2);"><img id="imgNE3" src='../../../images/imgNE3off.gif' class="ne" onclick="setNE(3);"><img id="imgNE4" src='../../../images/imgNE4off.gif' class="ne" onclick="setNE(4);"><img id="imgNE5" src='../../../images/imgNE5off.gif' class="ne" onclick="setNE(5);"><img id="imgNE6" src='../../../images/imgNE6off.gif' class="ne" onclick="setNE(6);">
            </td>
            <td>
			    <button id="btnSN4" type="button" onclick="insertarItem(1);" class="btnH25W35" runat="server" hidefocus="hidefocus" style="display:inline-block; margin-right:3px;" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../images/imgSN4.gif" /><span title=""></span>
			    </button>		
			    <button id="btnSN3" type="button" onclick="insertarItem(2);" class="btnH25W35" runat="server" hidefocus="hidefocus" style="display:inline-block; margin-right:3px;"  
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../images/imgSN3.gif" /><span title=""></span>
			    </button>			
			    <button id="btnSN2" type="button" onclick="insertarItem(3);" class="btnH25W35" runat="server" hidefocus="hidefocus" style="display:inline-block; margin-right:3px;"  
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../images/imgSN2.gif" /><span title=""></span>
			    </button>			
			    <button id="btnSN1" type="button" onclick="insertarItem(4);" class="btnH25W35" runat="server" hidefocus="hidefocus" style="display:inline-block; margin-right:3px;"  
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../images/imgSN1.gif" /><span title=""></span>
			    </button>			
			    <button id="btnNodo" type="button" onclick="insertarItem(5);" class="btnH25W35" runat="server" hidefocus="hidefocus" style="display:inline-block; margin-right:3px;"  
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../images/imgNodo.gif" /><span title=""></span>
			    </button>	
			    <button id="btnSUBN" type="button" onclick="insertarItem(6);" class="btnH25W35" runat="server" hidefocus="hidefocus" style="display:inline-block; margin-right:3px;"  
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../images/imgSubNodo.gif" /><span title=""></span>
			    </button>	
            </td>
            <td style=" text-align:right;padding-right:20px"><asp:Label ID="lblMostrarInactivos" runat="server" Text="Mostrar inactivos" /> <input type=checkbox id="chkMostrarInactivos" class="check" onclick="MostrarInactivos();" /></td>
        </tr>
    </table>
    </td>
    <td style="width:491px;" align="center">
		    <button id="btnAlertas1" type="button" onclick="setOpcionAlertas(1);" class="btnH25W50" runat="server" hidefocus="hidefocus" style="display:inline-block; margin-left:5px; margin-right:5px;" title="Traslada la configuración de las alertas del item seleccionado, a los elementos del siguiente nivel de la estructura"
			     onmouseover="se(this, 25);mostrarCursor(this);">
			    <img src="../../../images/imgTrasAlertas01.png" /><span title=""></span>
		    </button>	
		    <button id="btnAlertas2" type="button" onclick="setOpcionAlertas(2);" class="btnH25W50" runat="server" hidefocus="hidefocus" style="display:inline-block; margin-left:5px; margin-right:5px;" title="Traslada la configuración de las alertas del item seleccionado, a los elementos de todos los niveles inferiores de la estructura" 
			     onmouseover="se(this, 25);mostrarCursor(this);">
			    <img src="../../../images/imgTrasAlertas02.png" /><span title=""></span>
		    </button>	
		    <button id="btnAlertas3" type="button" onclick="setOpcionAlertas(3);" class="btnH25W50" runat="server" hidefocus="hidefocus" style="display:inline-block; margin-left:5px; margin-right:5px;" title="Traslada la configuración de las alertas del item seleccionado, a los elementos de todos los niveles inferiores de la estructura y a los proyectos asociados a los nodos afectados" 
			     onmouseover="se(this, 25);mostrarCursor(this);">
			    <img src="../../../images/imgTrasAlertas03.png" /><span title=""></span>
		    </button>	
		    <button id="btnAlertas4" type="button" onclick="setOpcionAlertas(4);" class="btnH25W50" runat="server" hidefocus="hidefocus" style="display:inline-block; margin-left:5px; margin-right:5px;" title="Traslada la configuración de las alertas del item seleccionado, a los proyectos asociados a los nodos afectados, sin modificar la configuración de la estructura dependiente" 
			     onmouseover="se(this, 25);mostrarCursor(this);">
			    <img src="../../../images/imgTrasAlertas04.png" /><span title=""></span>
		    </button>	
		    <img id="imgAlertasOn" src="../../../images/icoAbiertoG.gif" style="cursor:pointer; margin-left:90px; vertical-align:middle;" onclick="activarAlertas(1)" title="Desbloquea el grid de alertas" />    
		    <img id="imgAlertasOff" src="../../../images/icoCerradoG.gif" style="cursor:pointer; margin-left:10px; vertical-align:middle;" onclick="activarAlertas(0)" title="Bloquea el grid de alertas" />    
    </td>
</tr>
<tr>
    <td>
        <TABLE id="tblTitulo" style="WIDTH: 500px; BORDER-COLLAPSE: collapse; HEIGHT: 17px;" cellspacing="0" cellpadding="0" border="0">
            <TR class="TBLINI">
                <td style="padding-left:35px;">Denominación&nbsp;
                    <IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblBodyFijo',0,'divBodyFijo','imgLupa2', 'sincronizarCapaAlertas()')"
					        height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
					<IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblBodyFijo',0,'divBodyFijo','imgLupa2', event, 'sincronizarCapaAlertas()')"
					        height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1"></TD>
            </TR>
        </TABLE>
    </td>
    <td>
    <div id="divTituloMovil" style="width:471px; overflow:hidden;" runat="server">
    </div>
    </td>
</tr>
<tr>
    <td style="width:500px; vertical-align:top;">
        <div id="divBodyFijo" style="OVERFLOW: hidden; WIDTH: 516px; height:460px;" runat="server">
        </div>
        <table id="tblResultado" style="WIDTH:500px; HEIGHT: 17px;" cellspacing="0" cellpadding="0" border="0">
            <tr class="TBLFIN">
                <td>&nbsp;</td>
            </tr>
        </table>
    </td>
    <td style="width:491px;vertical-align:top;">
        <div id="divBodyMovil" style="width:487px; height:476px; overflow:auto; overflow-y:scroll;" runat="server" onscroll="setScroll()">
            <div style="background-image: url('../../../Images/imgFT20.gif'); background-repeat:repeat; width:1300px; height:auto;">
            <table id='tblBodyMovil' style='font-size:9pt; width:1300px;' cellpadding='0' cellspacing='0' border='0'>
            </table>
            </div>
        </div>
    </td>
</tr>
</table>
    <br />
    <img border="0" src="../../../Images/imgSN4.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO4) %>&nbsp;&nbsp;
    <img border="0" src="../../../Images/imgSN3.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO3)%>&nbsp;&nbsp;
    <img border="0" src="../../../Images/imgSN2.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO2)%>&nbsp;&nbsp;
    <img border="0" src="../../../Images/imgSN1.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO1)%>&nbsp;&nbsp;
    <img border="0" src="../../../Images/imgNodo.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO)%>&nbsp;&nbsp;
    <img border="0" src="../../../Images/imgSubNodo.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.SUBNODO)%>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();
            //alert("strBoton: "+ strBoton);
            switch (strBoton) {
                case "eliminar":
                    {
                        bEnviar = false;
                        jqConfirm("", "¿Estás conforme?", "", "", "war", 200).then(function (answer) {
                            if (answer) {
                                eliminar();
                            }
                            fSubmit(bEnviar, eventTarget, eventArgument);
                            return;
                        });
                        break;
                    }
            }
            if (strBoton != "eliminar") fSubmit(bEnviar, eventTarget, eventArgument);
        }
    }
    function fSubmit(bEnviar, eventTarget, eventArgument)
    {
        var theform = document.forms[0];
        theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
        theform.__EVENTARGUMENT.value = eventArgument;
        if (bEnviar) theform.submit();
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

