<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/UCGusano.ascx" TagName="UCGusano" TagPrefix="ucgus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
    var bEs_superadministrador = <%=(Session["ADMINISTRADOR_PC_ACTUAL"].ToString()=="SA")? "true":"false" %>;
    var nPantallaPreferencia = <%=nPantallaPreferencia %>;
</script>

<style type="text/css">
#tblDatos TD{border-right: solid 1px #A6C3D2; padding-right:1px;}
</style>
<ucgus:UCGusano ID="UCGusano1" runat="server" />
<center>
<table id="tabla" cellpadding="2" style="width:756px; text-align:left;">
	<tr>
		<td>
            <table  style="width:750px;" cellpadding="3" border="0">
                <colgroup>
                    <col style="width:635px;" />
                    <col style="width:25px;" />
                    <col style="width:25px;" />
                    <col style="width:25px;" />
                    <col style="width:25px;" />
                    <col style="width:15px;" />
                </colgroup>
                <tr>
                    <td colspan="6">
                        <label style="width:25px;">Mes</label>
                        <img id="mesAnt" title="Mes anterior" onclick="cambiarMes(-1)" src="../../../Images/btnAntRegOff.gif" style="cursor: pointer;vertical-align:bottom" />
                        <asp:TextBox ID="txtMes" style="width:90px; text-align:center;" Text="" runat="server" readonly="true" />
                        <img id="MesSig" title="Siguiente mes" onclick="cambiarMes(1)" src="../../../Images/btnSigRegOff.gif" style="cursor: pointer;vertical-align:bottom;" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <label title="Sobreescribe la información económica que pudiera estar ya registrada">Forzar sobreescritura</label> 
                        <input type="checkbox" id="chkSobreescribir" class="check" runat="server" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <label title="Muestra únicamente proyectos con consumos reportados en IAP">Proyectos con consumos</label> 
                        <input type="checkbox" id="chkRPCCR" class="check" checked="checked" onclick="mostrarProyectos()" runat="server" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <label title="Muestra únicamente los profesionales con consumos reportados en IAP o con unidades económicas registradas">Profesionales con consumos</label> 
                        <input type="checkbox" id="chkProfCon" class="check" checked="checked" onclick="obtenerDatosPSN()" runat="server" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <img src='../../../Images/imgPreferenciasGet.gif' border='0' title="Muestra el catálogo de preferencias" onclick="getCatalogoPreferencias()" style="cursor:pointer; vertical-align:bottom;">&nbsp;
                        <img src='../../../Images/imgPreferenciasAdd.gif' border='0' title="Almacena preferencia" onclick="setPreferencia()" style="cursor:pointer; vertical-align:bottom;">&nbsp;
                        <img src='../../../Images/imgPreferenciasDel.gif' border='0' title="Elimina todas las preferencias" onclick="delPreferencia()" style="cursor:pointer; vertical-align:bottom;">
                    </td>
                </tr>
                <tr>
                    <td>
                        <label id="lblProy" class="enlace" onclick="getPE()" style="width:50px;">Proyecto</label>
                        <asp:TextBox ID="txtNumPE" style="width:60px;" SkinID="Numero" Text="" readonly="true" runat="server" />
                        <asp:TextBox ID="txtDesPE" style="width:490px;" Text="" runat="server" readonly="true" />
                    </td>
                    <td width="25px">
			            <table id="btnPriRegOff" border="0" cellspacing="0" cellpadding="0" onmouseover="mostrarCursor(this)">
                          <tr onclick="mostrarDatos(1)"><td><img src="../../../images/btnPriRegOff.gif" border="0" onmouseover="mostrarCursor(this)" align="absmiddle" title="Ir al primer proyecto económico"></td></tr>
                        </table>
                    </td>
                    <td width="25px">
                        <table id="btnAntRegOff" border="0" cellspacing="0" cellpadding="0" onmouseover="mostrarCursor(this)">
                          <tr onclick="mostrarDatos(2)"><td><img src="../../../images/btnAntRegOff.gif" border="0" onmouseover="mostrarCursor(this)" align="absmiddle" title="Ir al anterior proyecto económico"></td></tr>
                        </table>
                    </td>
                    <td width="25px">
                        <table id="btnSigRegOff" border="0" cellspacing="0" cellpadding="0" onmouseover="mostrarCursor(this)">
                          <tr onclick="mostrarDatos(3)"><td><img src="../../../images/btnSigRegOff.gif" border="0" onmouseover="mostrarCursor(this)" align="absmiddle" title="Ir al siguiente proyecto económico"></td></tr>
                        </table>
                    </td>
                    <td width="40px">
                        <table id="btnUltRegOff" border="0" cellspacing="0" cellpadding="0" onmouseover="mostrarCursor(this)">
                          <tr onclick="mostrarDatos(4)"><td><img src="../../../images/btnUltRegOff.gif" border="0" onmouseover="mostrarCursor(this)" align="absmiddle" title="Ir al ultimo proyecto económico"></td></tr>
                        </table>
                        <script type="text/javascript">
                            setOp($I("btnPriRegOff"), 30);
                            setOp($I("btnAntRegOff"), 30);
                            setOp($I("btnSigRegOff"), 30);
                            setOp($I("btnUltRegOff"), 30);
	                    </script>                     
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="6" style="height:35px">
                        <img src="../../../Images/imgFactAni.gif" id="imgCopImp" border="0" style="cursor:pointer; display:none; height:33px; width:240px; margin-left:450px;" onclick="copiarImportes(1)" title="" />
                    </td>
                </tr>
            </table>
            <table id="tblTitulo" style="width: 700px; height: 17px;">
                <colgroup>
                    <col style="width:20px;" />
                    <col style="width:440px;" />
                    <col style="width:60px;" />
                    <col style="width:60px;" />
                    <col style="width:60px;" />
                    <col style="width:60px;" />
                </colgroup>
                <tr class="TBLINI" align="center">
                    <td></td>
                    <td>Profesional</td>
                    <td title="Horas reportadas">HR</td>
                    <td title="Jornadas reportadas">JR</td>
                    <td><label id="lbjJA" style="visibility:visible" title="Jornadas adaptadas">JA</label></td>
                    <td title="Unidades económicas"><label id="lblHJE">UE</label></td>
                </tr>
            </table>
            <div id="divCatalogo" style="overflow:auto; overflow-x:hidden; width:717px; height:400px" runat="server">
                <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:701px;">
                </div>
            </div>
            <table id="tblResultado" style="width: 700px; height: 17px;">
                <colgroup>
                    <col style="width:60px;" />
                    <col style="width:400px;" />
                    <col style="width:60px;" />
                    <col style="width:60px;" />
                    <col style="width:60px;" />
                    <col style="width:60px;" />
                </colgroup>            
                <tr class="TBLFIN">
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td style='text-align:right;' ><input id='txtTotHR' type='text' class='txtNumL' style='width:55px;' value='0,00' readonly="readonly" /></td>
                    <td style='text-align:right;' ><input id='txtTotJR' type='text' class='txtNumL' style='width:55px;' value='0,00' readonly="readonly" /></td>
                    <td style='text-align:right;' ><input id='txtTotJA' type='text' class='txtNumL' style='width:55px;  visibility:visible;' value='0,00' readonly="readonly" /></td>
                    <td style='text-align:right;' ><input id='txtTotJE' type='text' class='txtNumL' style='width:55px;' value='0,00' readonly="readonly" /></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <img border="0" src="../../../Images/imgUsuPVM.gif" />&nbsp;Del <%=sNodo%> del proyecto&nbsp;&nbsp;&nbsp;
            <img border="0" src="../../../Images/imgUsuNVM.gif" />&nbsp;De otro <%=sNodo %>&nbsp;&nbsp;&nbsp;
            <img border="0" src="../../../Images/imgUsuEVM.gif" />&nbsp;Externo&nbsp;&nbsp;&nbsp;
            <img id="imgForaneo" class="ICO" src="../../../Images/imgUsuFVM.gif" runat="server" />
            <label id="lblForaneo" runat="server">Foráneo</label>
        </td>
    </tr>
</table>
</center>
<script type="text/javascript">
<%=strArrayPSN %>
</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();
			switch (strBoton){
				case "grabar": 
				{
                    bEnviar = false;
                    grabar();
					break;
				}
				case "grabarproy": 
				{
                    bEnviar = false;
                    grabarproy();
					break;
				}
				case "traspgrabproy": 
				{
                    bEnviar = false;
                    traspgrabproy();
					break;
				}
				case "traspglobal": 
				{
                    bEnviar = false;
                    traspglobal();
					break;
				}
				case "guia": 
				{
                    bEnviar = false;
                    mostrarGuia("TraspasoIAP.pdf");
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

