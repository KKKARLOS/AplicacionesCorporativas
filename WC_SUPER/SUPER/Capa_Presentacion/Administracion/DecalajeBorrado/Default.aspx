<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">

    var nAnoMesActual = <%=nAnoMes %>;
    var strEstructuraNodo = "<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>";
    var strLinea ="<%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO2) %>";

</script>
<style type="text/css">  
#tblDatos TD{border-right: solid 1px #A6C3D2; padding-left:4px; padding-right:3px;}
</style>
<center>
    <table style="width: 1230px; text-align:left;">
    <colgroup>
        <col style="width:170px;" />
        <col style="width:65px;" />
        <col style="width:285px;" />
        <col style="width:190px;" />
        <col style="width:520px;" />
    </colgroup>
    <tr>
        <td colspan="3" style="padding-left:472px;">
        <img id="imgMarcar" src="../../../Images/Botones/imgMarcar.gif" border="0" style="cursor:pointer;" runat="server" onclick="marcardesmarcarNodo(1)" />
        <img id="imgDesmarcar" src="../../../Images/Botones/imgDesMarcar.gif" border="0" style="cursor:pointer;" runat="server" onclick="marcardesmarcarNodo(0)" />
        </td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td colspan="3">
            <table id="tblTituloNodo" style="width: 500px; height: 17px;">
                <colgroup>
                <col style="width:70px;" />
                <col style="width:430px;" />
                </colgroup>
                <tr class="TBLINI">
					<td style="text-align:right;"><IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblNodos',0,'divCatalogoNodo','imgLupaNodo1',event)"
							height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1">&nbsp;<IMG id="imgLupaNodo1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblNodos',0,'divCatalogoNodo','imgLupaNodo1')"
							height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2">&nbsp;Nº&nbsp;</td>
                    <td style="padding-left:5px;"><label id="lblNodo" runat="server"></label>&nbsp;<IMG id="imgLupaNodo2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblNodos',1,'divCatalogoNodo','imgLupaNodo2')"
							height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblNodos',1,'divCatalogoNodo','imgLupaNodo2',event)"
							height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1"></TD>
                </tr>
            </table>
            <div id="divCatalogoNodo" style="overflow: auto; width: 516px; height:200px;" runat="server">
		        <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:500px">
		        <%=strTablaHTML%>
		        </div>
            </div>
            <table id="tblResultado" style="width: 500px; height: 17px;">
                <tr class="TBLFIN">
                    <td>&nbsp;</td>
                </tr>
            </table>
        </td>
        <td></td>
        <td>
            <table border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td width="6" height="6" background="../../../Images/Tabla/7.gif"></td>
                <td height="6" background="../../../Images/Tabla/8.gif"></td>
                <td width="6" height="6" background="../../../Images/Tabla/9.gif"></td>
              </tr>
              <tr>
                <td width="6" background="../../../Images/Tabla/4.gif">&nbsp;</td>
                <td background="../../../Images/Tabla/5.gif" style="padding:5px">
	            <!-- Inicio del contenido propio de la página -->
            	
                <table id="tblEstadisticas" style="width: 490px; text-align:left;" cellpadding="8">
                    <colgroup>
                    <col style="width:430px;" />
                    <col style="width:60px;" />
                    </colgroup>
                    <tr>
                        <td>Nº de <%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>:</td>
                        <td id="cldEstNodo" style="text-align:right;">0</td>
                    </tr>
                    <tr>
                        <td>Nº de <%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %> seleccionados:</td>
                        <td id="cldEstNodoSel" style="text-align:right;">0</td>
                    </tr>
                    <tr>
                        <td>Nº total de órdenes de facturación en el mes: </td>
                        <td id="cldEstOrdenes" style="text-align:right;">0</td>
                    </tr>
                    <tr>
                        <td>Nº de órdenes de facturación en los <%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %> seleccionados: </td>
                        <td id="cldEstOrdenesSel" style="text-align:right;">0</td>
                    </tr>
                    <tr>
                        <td>Nº de órdenes marcadas para borrar:</td>
                        <td id="cldEstBorrar" style="text-align:right;">0</td>
                    </tr>
                    <tr>
                        <td>Nº de órdenes marcadas para decalar:</td>
                        <td id="cldEstDecalar" style="text-align:right;">0</td>
                    </tr>
                    </TABLE>

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
        </td>
    </tr>
    <tr>
        <td style=" vertical-align:middle; padding-top:10px;">
            <img title="Mes anterior" onclick="cambiarMes(-1)" src="../../../Images/btnAntRegOff.gif" style="cursor: pointer" />
            <asp:TextBox ID="txtMesVisible" style="width:90px; margin-bottom:5px; text-align:center; vertical-align:super" readonly="true" runat="server" Text=""></asp:TextBox>
            <img title="Siguiente mes" onclick="cambiarMes(1)" src="../../../Images/btnSigRegOff.gif" style="cursor: pointer" />
        </td>
        <td>&nbsp;<img src='../../../Images/imgObtenerAuto.gif' border='0' title="Obtiene la información automáticamente al cambiar el valor de algún criterio de selección" style="vertical-align:bottom;">
            <input type="checkbox" id="chkActuAuto" class="check" runat="server" />
        </td>
        <td>
			<button id="btnObtener" type="button" onclick="buscar();" class="btnH25W95" runat="server" hidefocus="hidefocus" onmouseover="mcur(this)">
				<img src="../../../images/imgObtener.gif" /><span title="Obtener">&nbsp;Obtener</span>
			</button>	
        </td>
        <td></td>
        <td style=" vertical-align:bottom; padding-left:405px;">
            <img id="img1" src="../../../Images/Botones/imgMarcar.gif" border="0" style="cursor:pointer;" runat="server" onclick="marcardesmarcarBorrado(1)" title="Marca todas las órdenes de facturación para su borrado" />
            <img id="img2" src="../../../Images/Botones/imgDesMarcar.gif" border="0" style="cursor:pointer;" runat="server" onclick="marcardesmarcarBorrado(0)" title="Desmarca todas las órdenes de facturación para su borrado" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <img id="img3" src="../../../Images/Botones/imgMarcar.gif" border="0" style="cursor:pointer;" runat="server" onclick="marcardesmarcarDecalaje(1)" title="Marca todas las órdenes de facturación para su decalaje" />
            <img id="img4" src="../../../Images/Botones/imgDesMarcar.gif" border="0" style="cursor:pointer;" runat="server" onclick="marcardesmarcarDecalaje(0)" title="Desmarca todas las órdenes de facturación para su decalaje" />
        </td>
    </tr>
    </table>
</center>
<table style="width:1250px;">
    <tr>
        <td>
            <table style="width: 1230px; height: 17px; text-align:center;">
                <colgroup>
                    <col style="width: 380px;" />
                    <col style="width: 320px;" />
                    <col style="width: 320px;" />
                    <col style="width: 90px;" />
                    <col style="width: 60px;" />
                    <col style="width: 60px;" />
                </colgroup>
	            <TR id="tblTitulo" class="TBLINI" align="center">
					<td>Proyecto&nbsp;<IMG id="imgLupa3" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa3')"
							height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa3',event)"
							height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1"></td>
					<td>Clase económica&nbsp;<IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa2')"
							height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa2',event)"
							height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1">
					</td>
                    <td>Motivo&nbsp;<IMG id="imgLupa5" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',2,'divCatalogo','imgLupa5')"
							height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',2,'divCatalogo','imgLupa5',event)"
							height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1"></TD>
                    <td>Importe</td>
                    <td>Borrado</td>
                    <td>Decalaje</td>
	            </TR>
            </table>
            <div id="divCatalogo" style="overflow:auto; width: 1247px; height:470px;">
                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:1231px">
                <table id="tblDatos"></table>
                </div>
            </div>
            <table id="tblTotales" style="width: 1230px; height: 17px; margin-bottom:3px; text-align: right;">
	            <tr class="TBLFIN">
                    <td>&nbsp;</td>
	            </tr>
            </table>
        </td>
    </tr>
</table>

<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();
			switch (strBoton){
				case "procesar": 
				{
                    bEnviar = false;
                    procesar();
					break;
				}
			}
		}

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

