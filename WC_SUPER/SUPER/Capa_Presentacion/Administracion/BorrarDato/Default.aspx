<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<style>
    #tblDatos TD{border-right: solid 1px #A6C3D2; padding-right:1px;}
</style>
<center>
<table  style="width:970px; text-align:left;" cellspacing="0" cellpadding="0">
  <tr>
    <td width="6" height="6" background="../../../Images/Tabla/7.gif"></td>
    <td height="6" background="../../../Images/Tabla/8.gif"></td>
    <td width="6" height="6" background="../../../Images/Tabla/9.gif"></td>
  </tr>
  <tr>
    <td width="6" background="../../../Images/Tabla/4.gif">&nbsp;</td>
    <td background="../../../Images/Tabla/5.gif" style="padding:5px; padding-left:15px">
    <!-- Inicio del contenido propio de la página -->
        <table class="texto" style="width:965px; text-align:left;">
        <colgroup>
            <col style="width:345px;" />
            <col style="width:310px;" />
            <col style="width:310px;" />
        </colgroup>
        <tr>
            <td style="vertical-align:bottom;">
                <span style="color:Red; font-weight:bold; text-align:center; width:100px;">¡ Atención ! La acción de procesar supone el </span>
                <br />
                <span style="color:Red; font-weight:bold; text-align:center; width:100px;">borrado de datos económicos de los proyectos.</span><br /><br />
                <fieldset style="width:140px; height:60px; padding:5px; display:inline;">
                    <legend><label id="Label1" class="enlace" onclick="getPeriodo()">Periodo</label></legend>
                        <table style="width:135px;" cellpadding="3px" >
                            <colgroup><col style="width:35px;"/><col style="width:100px;"/></colgroup>
                            <tr>
                                <td>Inicio</td>
                                <td>
                                    <asp:TextBox ID="txtDesde" style="width:90px;" Text="" readonly="true" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>Fin</td>
                                <td>
                                    <asp:TextBox ID="txtHasta" style="width:90px;" Text="" readonly="true" runat="server" />
                                </td>
                            </tr>
                        </table>
                </fieldset>
                <input type='checkbox' id="chkCerrados" class='checkTabla' style="vertical-align:middle; margin-left:15px; display:inline" />
                <label style="cursor:pointer; margin-left:5px;"  onclick="this.previousSibling.click()" >Incluir meses cerrados</label>
            </td>
            <td>
            <FIELDSET id="fstConsumos" style="width: 240px; height: 101px;">
                <LEGEND>Consumos</LEGEND> 
                <table class="texto" style="width:240px;" cellpadding="3px">
                    <tr>
                        <td>
                            <input type='checkbox' id="chkConsProf" class='checkTabla' style="vertical-align:middle;" />
                            <label style="cursor:pointer; margin-left:5px;"  onclick="this.previousSibling.click()">Por dedicación de profesionales</label>
                        </td>
                    </tr>
                    <tr>
                        <td><input type='checkbox' id="chkConsNivel" class='checkTabla' style="vertical-align:middle;" /><label style="cursor:pointer; margin-left:5px;"  onclick="this.previousSibling.click()" >Por nivel</label></td>
                    </tr>
                    <tr>
                        <td><input type='checkbox' id="chkPeriodCons" class='checkTabla' style="vertical-align:middle;" /><label style="cursor:pointer; margin-left:5px;"  onclick="this.previousSibling.click()" >Por periodificación</label></td>
                    </tr>
                    <tr>
                        <td><input type='checkbox' id="chkCirculante" class='checkTabla' style="vertical-align:middle;" /><label style="cursor:pointer; margin-left:5px;"  onclick="this.previousSibling.click()" >Financieros de circulante</label></td>
                    </tr>
                </table>
            </FIELDSET>
            </td>
            <td>
            <FIELDSET id="fstProduccion" style="width: 240px; height: 101px;">
                <LEGEND>Producción</LEGEND> 
                <table class="texto"  style="width:240px;" cellpadding="3px">
                    <tr>
                        <td><input type='checkbox' id="chkProdProf" class='checkTabla' style="vertical-align:middle;" /><label style="cursor:pointer; margin-left:5px;"  onclick="this.previousSibling.click()" >Por dedicación de profesionales</label></td>
                    </tr>
                    <tr>
                        <td><input type='checkbox' id="chkProdPerfil" class='checkTabla' style="vertical-align:middle;" /><label style="cursor:pointer; margin-left:5px;"  onclick="this.previousSibling.click()" >Producción por perfil</label></td>
                    </tr>
                    <tr>
                        <td><input type='checkbox' id="chkPeriodProd" class='checkTabla' style="vertical-align:middle;" /><label style="cursor:pointer; margin-left:5px;"  onclick="this.previousSibling.click()" >Por periodificación</label></td>
                    </tr>
                    <tr>
                        <td><input type='checkbox' id="chkAvance" class='checkTabla' style="vertical-align:middle;" /><label style="cursor:pointer; margin-left:5px;"  onclick="this.previousSibling.click()" >Por avance</label></td>
                    </tr>
                </table>
            </FIELDSET>
            </td>
        </tr>
        <tr>
            <td>
                <FIELDSET style="width: 290px; height:120px; padding:5px;">
                    <LEGEND><label id="lblAmbito" class="enlace" onclick="getCriterios(1)" runat="server">Ámbito</label>
                    <img id="Img14" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(1)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                    <DIV id="divAmbito" style="overflow:auto; overflow-x:hidden; width: 276px; height:100px; margin-top:2px">
                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:260px; height:auto">
                         <table id="tblAmbito" class="texto" style="width:260px;">
                         </table>
                        </div>
                    </DIV>
                </FIELDSET>
            </td>
            <td colspan="2" rowspan="3" style="padding-top:10px;">
                <TABLE id="tblTitulo" style="WIDTH: 560px; HEIGHT: 17px;">
                    <TR class="TBLINI">
                        <td style="padding-left:5px">Grupo / Subgrupo / Concepto / Clase</td>
                    </TR>
                </TABLE>
                <DIV id="divCatalogo" style="OVERFLOW: auto; OVERFLOW-X: hidden; WIDTH: 576px; height:360px">
                    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:560px; height:auto">
                    <%=strTablaHTML%>
                    </div>
                </DIV>
                <TABLE style="WIDTH: 560px; HEIGHT: 17px; margin-bottom:3px;">
                    <TR class="TBLFIN">
                        <TD></TD>
                    </TR>
                </TABLE>
            </td>
        </tr>
        <tr>
            <td>
                <FIELDSET style="width: 290px; height:120px; padding:5px;">
                    <LEGEND>
                        <label id="Label2" class="enlace" onclick="getCriterios(2)" runat="server">Responsable de proyecto</label>
                        <img id="Img2" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(2)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;">
                    </LEGEND>
                    <DIV id="divResponsable" style="overflow:auto; overflow-x:hidden; WIDTH: 276px; height:96px; margin-top:2px">
                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px; height:auto">
                         <table id="tblResponsable" class="texto" style="width:260px; table-layout:fixed;" >
                         </table>
                        </div>
                    </DIV>
                </FIELDSET>
            </td>
        </tr>
        <tr>
            <td>						
                <FIELDSET style="width: 290px; height:120px; padding:5px;">
                    <LEGEND>
                        <label id="Label10" class="enlace" onclick="getCriterios(16)" runat="server">Proyecto</label>
                        <img id="Img15" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(16)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;">
                    </LEGEND>
                    <DIV id="divProyecto" style="overflow:auto; OVERFLOW-X:hidden;  WIDTH: 276px; height:100px; margin-top:2px">
                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:260px; height:auto">
                         <table id="tblProyecto" class="texto" style="width:260px; table-layout:fixed;">
                         </table>
                        </div>
                    </DIV>
                </FIELDSET>
            </td>
        </tr>
        </table>
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
</center>
<asp:TextBox ID="hdnDesde" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" />
<asp:TextBox ID="hdnHasta" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
<!--
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();

			switch (strBoton){
				case "procesar": 
				{
                    bEnviar = false;
                    //AccionBotonera("procesar", "D");
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
    	
-->
</script>
</asp:Content>

