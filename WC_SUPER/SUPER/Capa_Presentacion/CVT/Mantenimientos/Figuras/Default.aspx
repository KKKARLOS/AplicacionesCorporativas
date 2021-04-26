<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_CVT_Mantenimientos_Figuras_Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
    <%=sConsultores %>
</script>

<table id="dragDropContainer" style="width:991px;" cellpadding="0" cellspacing="0" border="0">
<colgroup>
    <col style="width:491px;" />
    <col style="width:50px;" />
    <col style="width:450px;" />
</colgroup>
<tr>
    <td colspan="2">
		<table style="WIDTH: 486px;" cellpadding="2" cellspacing="0" border="0">
            <colgroup><col style="width:130px"/><col style="width:130px"/><col style="width:130px"/><col style="width:96px"/></colgroup>						
			<tr>
			    <td>&nbsp;Apellido1</td>
			    <td>&nbsp;Apellido2</td>
			    <td>&nbsp;Nombre</td>
                <td rowspan="2">
                    <input type="checkbox" id="chkCoste" onclick="getProfesionales()" class="check" runat="server" />&nbsp;Coste<br />
                    <input type="checkbox" id="chkExternos" onclick="getProfesionales()" class="check" runat="server" />&nbsp;Externos<br />
                    <input type="checkbox" id="chkBajas" onclick="getProfesionales()" class="check" runat="server" />&nbsp;Baja
                </td>
			</tr>
			<tr>
			    <td><asp:TextBox ID="txtApellido1" runat="server" style="width:110px"  onkeypress="javascript:if(event.keyCode==13){getProfesionales();event.keyCode=0;}" MaxLength="50" /></td>
			    <td><asp:TextBox ID="txtApellido2" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){getProfesionales();event.keyCode=0;}" MaxLength="50" /></td>
			    <td><asp:TextBox ID="txtNombre" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){getProfesionales();event.keyCode=0;}" MaxLength="50" /></td>
			</tr>
		</table>
    </td>
    <td>
        <fieldset class="fld" style="width:290px;">
        <legend class="Tooltip" title="Pinchar y arrastrar">Selector de figuras</legend>
            <div id="listOfItems" style="height:20px; margin-top:3px; margin-bottom:0px;">
                <ul id="allItems" style="width:280px;">
                    <li id="A" value="1">
                        <img src="../../../../Images/imgAdministador.gif" title="Administrador de Curvit" class="MM" ondragstart="return false;" /> 
                        Administrador
                    </li>
                    <li id="E" value="2"><img src="../../../../Images/imgEncargadoCV.gif" title="Encargado de Currículums" class="MM" ondragstart="return false;" /> Encargado</li>
                    <li id="C" value="3"><img src="../../../../Images/imgConsultorCV.gif" title="Consultor de Currículums" class="MM" ondragstart="return false;" /> Consultor</li>
                </ul>
            </div>
        </fieldset>
    </td>
</tr>
<tr>
    <td valign="top">
        <table id="tblCatIni" style="width:470px; height:17px">
            <colgroup><col style="width:415px" /><col style="width:20px" /><col style="width:20px" /><col style="width:15px" /></colgroup>
            <tr class="TBLINI">
                <td style="padding-left:3px">
                    Profesional
                    <img style="DISPLAY: none; CURSOR: pointer;" id="imgLupa1" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa1')" height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2" />                      
                    <img style="DISPLAY: none; CURSOR: pointer;" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa1',event)" height="11" src="../../../../Images/imgLupa.gif" width="20" />
			    </td>  
                <td title="Puede ver costes">C</td>                  
                <td title="Puede ver profesionales externos">E</td>                  
                <td title="Puede ver profesionales de baja">B</td>                  
            </tr>
        </table>
        <div id="divCatalogo" style="overflow:auto; overflow-x:hidden; width:486px; height:440px" runat="server" onscroll="scrollTabla()">
            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:470px;">
            <table id="tblDatos" style="width:470px;" class="MAM">
                <colgroup><col style="width:20px" /><col style="width:390px" /><col style="width:20px" /><col style="width:20px" /><col style="width:20px" /></colgroup>
            </table>
            </div>
        </div>
        <table style="width:470px; height:17px">
            <tr class="TBLFIN">
                <td></td>
            </tr>
        </table>              
    </td>
    <td valign="top" align="left">
        <asp:Image id="imgPapelera" style="CURSOR: pointer; margin-left:4px; margin-top:110px;" runat="server" ImageUrl="~/Images/imgEliminar32.gif" target="true" caso="3" onmouseover="setTarget(this)"></asp:Image>
    </td>
    <td valign="top">
		<table id="tblTituloFiguras" style="height:17px;width:420px">
		    <colgroup><col style="width:20px"/><col style="width:300px"/><col style="width:100px"/></colgroup>
			<tr class="TBLINI">
			    <td align="center"></td>
				<td><IMG style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#imgFiguras" border="0">
		            <MAP name="imgFiguras">
		                <AREA onclick="ot('tblFiguras', 2, 0, '')" shape="RECT" coords="0,0,6,5">
		                <AREA onclick="ot('tblFiguras', 2, 1, '')" shape="RECT" coords="0,6,6,11">
	                </MAP>&nbsp;Profesionales&nbsp;<IMG id="imgLupaFigurasA2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblFiguras',2,'divFiguras','imgLupaFigurasA2')"
                height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblFiguras',2,'divFiguras','imgLupaFigurasA2',event)"
                height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">
                </td>
			    <td>Figuras</td>
			</tr>
		</table>
		<div id="mainContainer">
		<div id="divFiguras" style="OVERFLOW: auto; width: 436px; height: 220px;" target="true" onmouseover="setTarget(this);" caso="1">
            <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT22.gif); width:420px; height:auto">
		        <table id="tblFiguras" class="texto MM" width="420px">							    
		            <colgroup>
		            <col style="width:20px;" />
		            <col style="width:20px;" />
		            <col style="width:280px;" />
		            <col style="width:100px;" />
		            </colgroup>
		            <tbody id='tbodyFiguras'>
		                <%=strTablaHTMLFiguras%>
		            </tbody>
		        </table>
		    </div>
		</div>
		</div>
		<table id="tblResultado2" style="height:17px;width:420px">
			<tr class="TBLFIN">
				<td>&nbsp;</td>
			</tr>
		</table>
		<fieldset id="fstCaracteristicas" style="width:410px; height:210px;">
		    <legend>
                <label id="lblEstructura" class="enlace" onclick="getEstructura();">Estructura organizativa</label>
                <label style="padding-left:5px;">(Del consultor seleccionado)</label>
		    </legend>
		    <table style="width:410px; " cellpadding="3" cellspacing="0" border="0">
		    <colgroup>
		        <col style="width:135px;" />
		        <col style="width:135px;" />
		        <col style="width:140px;" />
		    </colgroup>
		    <tr>
		        <td colspan="3">
                    <TABLE id="TABLE1" style="WIDTH: 380px;HEIGHT: 17px">
		            <colgroup>
		                <col style="width:20px;" />
		                <col style="width:360px;" />
		            </colgroup>
                        <TR class="TBLINI">
                            <td></td>
                            <td style="padding-left:3px">Denominación</td>                    
                        </TR>
                    </TABLE>
                    <DIV id="div1" style="OVERFLOW: auto; overflow-x:hidden; WIDTH: 396px; height:160px" runat="server">
                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); WIDTH: 380px;">
                            <table id="tblEstructura" class="texto MANO" style="width:380px;">							    
		                        <colgroup>
		                            <col style="width:20px;" />
		                            <col style="width:360px;" />
		                        </colgroup>
		                       
		                    </table>
                        </div>
                    </DIV>
                    <TABLE style="WIDTH: 380px; HEIGHT: 17px">
                        <TR class="TBLFIN">
                            <TD></TD>
                        </TR>
                    </TABLE>              
		        </td>
		    </tr>
		    </table>
		</fieldset>
    </td>
</tr>
</table>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<div class="clsDragWindow" id="DW" noWrap></div>
<ul id="dragContent" class="texto"></ul>
<div id="dragDropIndicator"><img src="../../../../Images/imgSeparador.gif"></div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
<!--
    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();
            switch (strBoton) {
                case "grabar":
                    {
                        bEnviar = false;
                        grabar();
                        break;
                    }
            }
        }

        var theform = document.forms[0];
        theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
        theform.__EVENTARGUMENT.value = eventArgument;
        if (bEnviar) {
            theform.submit();
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
    
-->
</script>
</asp:Content>

