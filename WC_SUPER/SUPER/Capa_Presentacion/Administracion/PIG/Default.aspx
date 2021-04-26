<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript" language="javascript">
    var strEstructuraNodo = "<%=Estructura.getDefCorta(Estructura.sTipoElem.NODO) %>";
    var aNat = new Array(); // aNN --> Array de Naturalezas
    var aNN = new Array(); // aNN --> Array de Nodos Naturalezas
    var btnCal = "<%=Session["BTN_FECHA"].ToString() %>";
</script>
<center>
<table style="width:1156px;text-align:left" border="0">
<colgroup>
    <col style="width:420px;" />
    <col style="width:330px;" />
    <col style="width:406px;" />
</colgroup>
<tr>
    <td>
        <div align="left" style="width: 300px;">
            <div align="center" style="background-image: url('../../../Images/imgFondo185.gif'); background-repeat:no-repeat;
                width: 185px; height: 23px; position: absolute; top: 150px; left: 80px; padding-top: 5px;">
                &nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %></div>
            <table border="0" cellspacing="0" cellpadding="0" style="margin-top:25px;">
              <tr>
                <td width="6" height="6" background="../../../Images/Tabla/7.gif"></td>
                <td height="6" background="../../../Images/Tabla/8.gif"></td>
                <td width="6" height="6" background="../../../Images/Tabla/9.gif"></td>
              </tr>
              <tr>
                <td width="6" background="../../../Images/Tabla/4.gif">&nbsp;</td>
                <td background="../../../Images/Tabla/5.gif" style="padding:3px; padding-top:10px;">
	            <!-- Inicio del contenido propio de la página -->
            	
                <table id="tblEstadisticas" style="width: 250px;" cellpadding="3">
                    <colgroup>
                    <col style="width:220px;" />
                    <col style="width:30px;" />
                    </colgroup>
                    <tr>
                        <td>Mostrados (activos en estructura activa):</td>
                        <td id="cldEstNodo" style="text-align:right;">0</td>
                    </tr>
                    <tr>
                        <td>Seleccionados para cálculo:</td>
                        <td id="cldEstNodoSel" style="text-align:right;">0</td>
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
        </div>
    </td>
    <td>
        <div align="left" style="width: 370px;">
            <div align="center" style="background-image: url('../../../Images/imgFondo185.gif');background-repeat:no-repeat;
                width:185px; height:23px; position:absolute; top:150px; left:500px; padding-top: 5px;">
                &nbsp;Naturalezas</div>
            <table border="0" cellspacing="0" cellpadding="0" style="margin-top:25px;">
              <tr>
                <td width="6" height="6" background="../../../Images/Tabla/7.gif"></td>
                <td height="6" background="../../../Images/Tabla/8.gif"></td>
                <td width="6" height="6" background="../../../Images/Tabla/9.gif"></td>
              </tr>
              <tr>
                <td width="6" background="../../../Images/Tabla/4.gif">&nbsp;</td>
                <td background="../../../Images/Tabla/5.gif" style="padding:3px; padding-top:10px;">
	            <!-- Inicio del contenido propio de la página -->
            	
                <table style="width: 290px;" cellpadding="3">
                    <colgroup>
                    <col style="width:260px;" />
                    <col style="width:30px;" />
                    </colgroup>
                    <tr>
                        <td>Naturalezas improductivas: </td>
                        <td id="cldNatImprod" style="text-align:right;">0</td>
                    </tr>
                    <tr>
                        <td>Con vigencia parametrizada diferente a 12 meses: </td>
                        <td id="cldVigParam" style="text-align:right;">0</td>
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
        </div>
    </td>
    <td title="Crea proyecto aunque exista ya uno para la misma naturaleza">
        <input type=checkbox id="chkForzar" class="check" runat="server" />
        Forzar creación
    </td>
</tr>
<tr>
    <td>
        <table style="width:380px; margin-top:47px;" border="0">
            <colgroup>
                <col style="width:50px;" />
                <col style="width:30px;" />
                <col style="width:270px;" />
                <col style="width:30px;" />
            </colgroup>
            <tr align="center" style="height:25px;">
                <td style="padding-left:1px;">
                    <img id="imgMarcar" src="../../../Images/Botones/imgMarcar.gif" border="0" style="cursor:pointer;" runat="server" onclick="marcardesmarcarCalcular(1)" />
                    <img id="imgDesmarcar" src="../../../Images/Botones/imgDesMarcar.gif" border="0" style="cursor:pointer;" runat="server" onclick="marcardesmarcarCalcular(0)" />
                </td>
				<td style="vertical-align:middle;text-align:right;" colspan="2">
                    <img title="Año anterior" onclick="cambiarAnno(-1)" src="../../../Images/btnAntRegOff.gif" style="cursor: pointer" />
                    <asp:TextBox ID="txtAnnoVisible" style="width:30px; margin-bottom:5px; text-align:center; vertical-align:super" readonly="true" runat="server" Text=""></asp:TextBox>
                    <img title="Siguiente año" onclick="cambiarAnno(1)" src="../../../Images/btnSigRegOff.gif" style="cursor: pointer" />
				</td>
                <td style="text-align:right; padding-right: 2px;"></td>
            </tr>
            <tr id="tblTitulo" class="TBLINI">
                <td>&nbsp;Grabar</td>
				<td style="text-align:right;">
                    <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblNodos',1,'divCatalogo','imgLupa1',event)"
							height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1"> <IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblNodos',1,'divCatalogo','imgLupa1')"
							height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
					<IMG style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#imgN" border="0">
				    <MAP name="imgN">
				        <AREA onclick="ot('tblNodos', 1, 0, 'num')" shape="RECT" coords="0,0,6,5">
				        <AREA onclick="ot('tblNodos', 1, 1, 'num')" shape="RECT" coords="0,6,6,11">
			        </MAP>Nº</td>
				<td style="padding-left:3px;">&nbsp;<IMG style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#imgDenominacion" border="0">
				    <MAP name="imgDenominacion">
				        <AREA onclick="ot('tblNodos', 2, 0, '')" shape="RECT" coords="0,0,6,5">
				        <AREA onclick="ot('tblNodos', 2, 1, '')" shape="RECT" coords="0,6,6,11">
			        </MAP><%= Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>&nbsp;<IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblNodos',2,'divCatalogo','imgLupa2')"
							height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblNodos',2,'divCatalogo','imgLupa2',event)"
							height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1"></td>
                <td title="Número de naturalezas marcadas para generar" style="text-align:right; padding-right: 2px;">Nat.</td>
            </tr>
        </table>
        <div id="divCatalogo" style="overflow:auto; width: 396px; height:580px;">
            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:380px">
            <%=strTablaHTML%>
            </div>
        </div>
        <table id="tblTotales" style="width: 380px;">
            <tr class="TBLFIN">
                <td>&nbsp;</td>
            </tr>
        </table>
        <label style="color:red;">Denominación de <%= Estructura.getDefCorta(Estructura.sTipoElem.NODO) %> en rojo indica que está parametrizado</label>
    </td>
    <td style="vertical-align:top;" colspan="2">
        <table style="width:720px;" border="0">
            <colgroup>
                <col style="width:30px;" />
                <col style="width:190px;" />
                <col style="width:20px;" />
                <col style="width:65px;" />
                <col style="width:65px;" />
                <col style="width:50px;" />
                <col style="width:50px;" />
                <col style="width:100px;" />
                <col style="width:50px;" />
                <col style="width:100px;" />
            </colgroup>
            <tr>
                <td colspan="10">
                    <table style="width:530px; margin-left:185px;" border="0">
                        <colgroup><col style="width:90px;"/><col style="width:410px;"/><col style="width:30px;"/></colgroup>
                        <tr>
                            <td title="Selección de responsable de proyecto para su asignación.">
                                <label id="lblResp" class="enlace" onClick="getResponsable()">Responsable</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtResponsable" runat="server" Text="" Width="400px" readonly="true" />
                                <input type="hidden" id="hdnIdResponsable" value=""/>
                            </td>
                            <td>
                                <img id="imgResponsable" src='../../../Images/imgAutomatico.gif' border='0' title="Asigna a las naturalezas seleccionadas el responsable de los proyectos a crear" 
                                    onclick="setResponsable()" style="cursor:pointer; vertical-align:middle; width:28px; height:26px;" />
                            </td>
                        </tr>
                        <tr>
                            <td title="Selección de validador GASVI">
                                <label id="lblValid" class="enlace" onClick="getValidador()">Validador</label>
                                <img id="Img3" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delValidador()" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:5px;">
                            </td>
                            <td>
                                <asp:TextBox ID="txtValidador" runat="server" Text="" Width="400px" readonly="true" />
                                <input type="hidden" id="hdnIdValidador" value=""/>
                            </td>
                            <td>
                                <img id="imgValidador" src='../../../Images/imgAutomatico.gif' border='0' title="Asigna a las naturalezas seleccionadas el validador GASVI de los proyectos a crear" 
                                    onclick="setValidador()"  style="cursor:pointer; vertical-align:middle; width:28px; height:26px;" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="5" style="margin-left:1px;">
                    <img id="img1" src="../../../Images/Botones/imgMarcar.gif" border="0" style="cursor:pointer;" runat="server" onclick="marcardesmarcarNaturalezas(1)" />
                    <img id="img2" src="../../../Images/Botones/imgDesMarcar.gif" border="0" style="cursor:pointer;" runat="server" onclick="marcardesmarcarNaturalezas(0)" />
                </td>
                <td style="text-align:center;">
                    <img id="img4" src="../../../Images/Botones/imgMarcar.gif" border="0" style="cursor:pointer;" runat="server" onclick="marcarDesmarcarCol(1,5)" />
                    <img id="img5" src="../../../Images/Botones/imgDesMarcar.gif" border="0" style="cursor:pointer;" runat="server" onclick="marcarDesmarcarCol(0,5)" />
                </td>
                <td style="text-align:center;">
                    <img id="img6" src="../../../Images/Botones/imgMarcar.gif" border="0" style="cursor:pointer;" runat="server" onclick="marcarDesmarcarCol(1,6)" />
                    <img id="img7" src="../../../Images/Botones/imgDesMarcar.gif" border="0" style="cursor:pointer;" runat="server" onclick="marcarDesmarcarCol(0,6)" />
                </td>
                <td></td>
                <td style="text-align:center;">
                    <img id="img8" src="../../../Images/Botones/imgMarcar.gif" border="0" style="cursor:pointer;" runat="server" onclick="marcarDesmarcarCol(1,8)" />
                    <img id="img9" src="../../../Images/Botones/imgDesMarcar.gif" border="0" style="cursor:pointer;" runat="server" onclick="marcarDesmarcarCol(0,8)" />
                </td>
                <td></td>
            </tr>
            <tr id="tblTituloNat" class="TBLINI">
                <td title="Grabar">Gra.</td>
				<td title="Naturalezas de producción improductivas">&nbsp;Naturalezas</td>
                <td></td>
				<td style="text-align:center;">FIV</td>
				<td style="text-align:center;">FFV</td>
				<td style="text-align:center;"title="Los proyectos PIG creados para estas naturalezas permiten o no ser replicados">Rep.</td>
				<td title="A los proyectos se les asigna por defecto los profesionales del C.R." style="text-align:center;">Hereda</td>
				<td title="Responsable del proyecto">Responsable</td>
				<td style="text-align:center;" title="Permite anotar gastos de viaje contra el proyecto" style="text-align:left;">Imp.</td>
				<td title="Validador GASVI">Validador</td>
            </tr>
        </table>
        <div id="divCatalogoNat" style="overflow:auto; width: 736px; height:580px;">
            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:720px">
                <table id='tblNaturalezas' class='texto MANO' style='width: 720px;' mantenimiento='1' border="0">
                <colgroup>
                    <col style='width:30px;' />
                    <col style='width:190px;' />
                    <col style='width:20px;' />
                    <col style='width:65px;' />
                    <col style='width:65px;' />
                    <col style="width:50px;" />
                    <col style="width:50px;" />
                    <col style="width:100px;" />
                    <col style="width:50px;" />
                    <col style="width:100px;" />
                </colgroup>
                </table>
            </div>
        </div>
        <table id="tblTotalesNat" style="width:720px;">
            <tr class="TBLFIN">
                <td>&nbsp;</td>
            </tr>
        </table>
    </td>
</tr>
<tr>
    <td></td>
    <td colspan="2"><img class="ICO" src="../../../Images/imgIconoEmpresarial.gif" />&nbsp;Plantilla empresarial</td>
</tr>
</table>
</center>
<script type="text/javascript" language="javascript">
    <%=strNat %>

    <%=strNN %>
</script>
    <uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();
			//alert("strBoton: "+ strBoton);
			switch (strBoton){
				case "procesar": 
				{
                    bEnviar = false;
                    setTimeout("procesar();", 20);
					break;
				}
			    case "parametrizar":
			        {
			            bEnviar = false;
			            parametrizar();
			            break;
			        }
			    case "eliminar":
			        {
			            bEnviar = false;
			            borrarParam();
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

