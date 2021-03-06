<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript" language="javascript">
    var strEstructuraNodo = "<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>";
</script>
<img id="imgCaution" src="../../../Images/imgCaution.gif" border=0 style="display:none; position:absolute; top:145px; left:800px;" title="Existen nodos a procesar con proyectos que ya tienen datos de obra en curso del a?o de referencia." />
<center>
<table style="width:620px; margin-left:8px; text-align:left">
    <tr> 
    <td>
        <div align="left" style="width: 600px;">
            <div align="center" style="background-image: url('../../../Images/imgFondo185.gif');background-repeat:no-repeat;
                width: 185px; height: 23px; position: absolute; top: 130px; left: 220px; padding-top: 5px;">
                &nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %></div>
            <table border="0" cellspacing="0" cellpadding="0" style="margin-top:5px;">
              <tr>
                <td width="6" height="6" background="../../../Images/Tabla/7.gif"></td>
                <td height="6" background="../../../Images/Tabla/8.gif"></td>
                <td width="6" height="6" background="../../../Images/Tabla/9.gif"></td>
              </tr>
              <tr>
                <td width="6" background="../../../Images/Tabla/4.gif">&nbsp;</td>
                <td background="../../../Images/Tabla/5.gif" style="padding:3px; padding-top:10px;">
	            <!-- Inicio del contenido propio de la p?gina -->
            	
                <table id="tblEstadisticas" class="texto" style="width: 560px;" cellpadding="3">
                    <colgroup>
                        <col style="width:220px;" />
                        <col style="width:30px;" />
                        <col style="width:50px;" />
                        <col style="width:230px;" />
                        <col style="width:30px;" />
                    </colgroup>
                    <tr>
                        <td>Mostrados (activos en estructura activa):</td>
                        <td id="cldEstNodo" align="right">0</td>
                        <td></td>
                        <td>Con POCAR: </td>
                        <td id="cldPocar" align="right">0</td>
                    </tr>
                    <tr>
                        <td>Seleccionados para proceso:</td>
                        <td id="cldEstNodoSel" align="right">0</td>
                        <td></td>
                        <td>Con POCAR y seleccionados para proceso: </td>
                        <td id="cldPocarSel" align="right">0</td>
                    </tr>
                    </TABLE>

	            <!-- Fin del contenido propio de la p?gina -->
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
</table>
<table style="width:620px;text-align:left" cellpadding="5">
<tr>
    <td>
        <table style="width: 600px; margin-top:10px;">
            <colgroup>
                <col style="width:50px;" />
                <col style="width:70px;" />
                <col style="width:400px;" />
                <col style="width:80px;" />
            </colgroup>
            <tr>
                <td style="padding-left:1px;">
                    <img id="imgMarcar" src="../../../Images/Botones/imgMarcar.gif" border="0" style="cursor:pointer;" runat="server" onclick="marcardesmarcarCalcular(1)" />
                    <img id="imgDesmarcar" src="../../../Images/Botones/imgDesMarcar.gif" border="0" style="cursor:pointer;" runat="server" onclick="marcardesmarcarCalcular(0)" />
                </td>
				<td style="vertical-align:middle;" align="right" colspan="2">
				    <label style="vertical-align:middle;">A?o de aplicaci?n de ajuste al 20% de obra en curso</label>
                    <img title="A?o anterior" onclick="setAnno('A')" src="../../../Images/btnAntRegOff.gif" style="cursor: pointer;vertical-align:middle" />
                    <asp:TextBox id="txtAnno" style="width:32px; margin-bottom:5px; text-align:center;vertical-align:baseline;" readonly="true" runat="server" Text=""></asp:TextBox>
                    <img title="Siguiente a?o" onclick="setAnno('S')" src="../../../Images/btnSigRegOff.gif" style="cursor: pointer;vertical-align:middle" />
				</td>
                <td align='center'></td>
            </tr>
            <tr id="tblTitulo" class="TBLINI">
                <td>&nbsp;Pasar</td>
				<td align="right"><IMG style="CURSOR: pointer; display: none;" onclick="buscarDescripcion('tblNodos',1,'divCatalogo','imgLupa1',event)"
							height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1" /> <IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblNodos',1,'divCatalogo','imgLupa1')"
							height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2" />
					<IMG style="cursor: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#imgN" border="0" />
				    <MAP name="imgN">
				        <AREA onclick="ot('tblNodos', 1, 0, 'num')" shape="RECT" coords="0,0,6,5">
				        <AREA onclick="ot('tblNodos', 1, 1, 'num')" shape="RECT" coords="0,6,6,11">
			        </MAP>N?</td>
				<td style="padding-left:3px;">&nbsp;<IMG style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#imgDenominacion" border="0" />
				    <MAP name="imgDenominacion">
				        <AREA onclick="ot('tblNodos', 2, 0, '')" shape="RECT" coords="0,0,6,5">
				        <AREA onclick="ot('tblNodos', 2, 1, '')" shape="RECT" coords="0,6,6,11">
			        </MAP><%= Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>&nbsp;<IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblNodos',2,'divCatalogo','imgLupa2')"
							height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2" /> <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblNodos',2,'divCatalogo','imgLupa2',event)"
							height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1" /></td>
                <td align="center" title="Proyectos con obra en curso en el a?o de referencia">POCAR</td>
            </tr>
        </table>
        <div id="divCatalogo" style="overflow:auto; width: 616px; height:420px;">
            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:600px">
            <%=strTablaHTML%>
            </div>
        </div>
        <table id="tblTotales" style="width: 600px;">
            <tr class="TBLFIN">
                <td>&nbsp;</TD>
            </tr>
        </table>
    </td>
</tr>
</table>
</center>
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
                    //mostrarProcesando();
                    setTimeout("procesar();", 20);
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

